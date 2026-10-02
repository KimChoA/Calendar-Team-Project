using calendar4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace calendar4.CreateForm
{
    public partial class SharecalendarManager : Form
    {
        // ============================================================
        // 현재 공유 캘린더 / 로그인 사용자
        // ============================================================

        private readonly int sharedCalendarId;
        private readonly int loggedInUserId;


        private readonly SharedCalendarRepository repository =
            new SharedCalendarRepository();


        // 라디오버튼 초기화 중 DB UPDATE 방지
        private bool isLoadingPermission =
            false;


        // ============================================================
        // ListBox에 넣을 멤버 객체
        // ============================================================

        private sealed class MemberListItem
        {
            public int UserId
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            } = string.Empty;


            // ListBox에는 이름만 표시
            public override string ToString()
            {
                return Name;
            }
        }


        // ============================================================
        // 디자이너용
        // ============================================================

        public SharecalendarManager()
        {
            InitializeComponent();

            sharedCalendarId =
                0;

            loggedInUserId =
                0;
        }


        // ============================================================
        // 실제 사용하는 생성자
        // ============================================================

        public SharecalendarManager(
            int sharedCalendarId,
            int loggedInUserId)
        {
            InitializeComponent();

            this.sharedCalendarId =
                sharedCalendarId;

            this.loggedInUserId =
                loggedInUserId;


            LoadMembers();

            LoadPermission();


            // 라디오 버튼 변경 이벤트
            radioRead.CheckedChanged +=
                Permission_CheckedChanged;

            radioAll.CheckedChanged +=
                Permission_CheckedChanged;


            UpdateButtonState();
        }


        // ============================================================
        // 현재 멤버 목록
        // ============================================================

        private void LoadMembers()
        {
            try
            {
                listBox1.Items.Clear();


                var members =
                    repository.GetMembers(
                        sharedCalendarId);


                foreach (var member in members)
                {
                    listBox1.Items.Add(
                        new MemberListItem
                        {
                            UserId =
                                member.UserId,

                            Name =
                                member.Name
                        });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "멤버 목록을 불러오지 못했습니다.\n\n" +
                    ex.Message,
                    "공유 캘린더",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 현재 수정 권한 불러오기
        // ============================================================

        private void LoadPermission()
        {
            try
            {
                isLoadingPermission =
                    true;


                bool ownerOnly =
                    repository.GetOwnerOnlyEdit(
                        sharedCalendarId);


                // 관리자만 가능
                radioRead.Checked =
                    ownerOnly;


                // 모든 멤버 가능
                radioAll.Checked =
                    !ownerOnly;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "수정 권한 정보를 불러오지 못했습니다.\n\n" +
                    ex.Message,
                    "공유 캘린더",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoadingPermission =
                    false;
            }
        }


        // ============================================================
        // 방장 여부에 따라 관리 기능 활성화
        // ============================================================

        private void UpdateButtonState()
        {
            bool isOwner =
                repository.IsOwner(
                    sharedCalendarId,
                    loggedInUserId);


            // 방장만 멤버 내보내기 가능
            btnReject.Enabled =
                isOwner;


            // 방장만 캘린더 자체 삭제 가능
            btnDelete.Enabled =
                isOwner;


            // 방장만 수정 권한 변경 가능
            radioRead.Enabled =
                isOwner;

            radioAll.Enabled =
                isOwner;


            // 방장은 나가기 대신
            // 캘린더 삭제를 사용
            btnOut.Enabled =
                !isOwner;
        }


        // ============================================================
        // 초대하기
        // ============================================================

        private void btnInvite_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string roomName =
                    repository.GetRoomName(
                        sharedCalendarId)
                    ?? "공유 캘린더";


                string? roomCode =
                    repository.GetRoomCode(
                        sharedCalendarId);


                if (string.IsNullOrWhiteSpace(
                        roomCode))
                {
                    MessageBox.Show(
                        "공유 캘린더 참가 코드를 찾을 수 없습니다.",
                        "초대",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                using var inviteForm =
                    new Shareinvite(
                        roomName,
                        roomCode);


                inviteForm.ShowDialog(
                    this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "초대 창을 여는 중 오류가 발생했습니다.\n\n" +
                    ex.Message,
                    "초대",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 공유 캘린더 나가기
        // ============================================================

        private void btnOut_Click(
            object sender,
            EventArgs e)
        {
            bool isOwner =
                repository.IsOwner(
                    sharedCalendarId,
                    loggedInUserId);


            if (isOwner)
            {
                MessageBox.Show(
                    "관리자는 공유 캘린더에서 바로 나갈 수 없습니다.",
                    "공유 캘린더",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            DialogResult result =
                MessageBox.Show(
                    "이 공유 캘린더에서 나가시겠습니까?",
                    "공유 캘린더 나가기",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


            if (result !=
                DialogResult.Yes)
            {
                return;
            }


            try
            {
                repository.RemoveMember(
                    sharedCalendarId,
                    loggedInUserId);


                MessageBox.Show(
                    "공유 캘린더에서 나갔습니다.",
                    "공유 캘린더",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                DialogResult =
                    DialogResult.OK;


                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "공유 캘린더에서 나가는 중 오류가 발생했습니다.\n\n" +
                    ex.Message,
                    "공유 캘린더",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 돌아가기
        // ============================================================

        private void btnBack_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }


        // ============================================================
        // 멤버 내보내기
        // ============================================================

        private void btnReject_Click(
            object sender,
            EventArgs e)
        {
            // 방장만 가능
            if (!repository.IsOwner(
                    sharedCalendarId,
                    loggedInUserId))
            {
                MessageBox.Show(
                    "관리자만 멤버를 내보낼 수 있습니다.",
                    "멤버 관리",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            if (listBox1.SelectedItem
                is not MemberListItem selectedMember)
            {
                MessageBox.Show(
                    "내보낼 멤버를 선택해주세요.",
                    "멤버 관리",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            // 자기 자신(방장)은 내보내기 불가
            if (selectedMember.UserId ==
                loggedInUserId)
            {
                MessageBox.Show(
                    "관리자 본인은 내보낼 수 없습니다.",
                    "멤버 관리",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            DialogResult result =
                MessageBox.Show(
                    $"'{selectedMember.Name}' 님을 " +
                    "공유 캘린더에서 내보내시겠습니까?",
                    "멤버 내보내기",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


            if (result !=
                DialogResult.Yes)
            {
                return;
            }


            try
            {
                repository.RemoveMember(
                    sharedCalendarId,
                    selectedMember.UserId);


                MessageBox.Show(
                    $"'{selectedMember.Name}' 님을 내보냈습니다.",
                    "멤버 관리",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                // ListBox 다시 불러오기
                LoadMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "멤버를 내보내는 중 오류가 발생했습니다.\n\n" +
                    ex.Message,
                    "멤버 관리",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 수정 권한 변경
        // ============================================================

        private void Permission_CheckedChanged(object? sender,EventArgs e)
        {
            if (isLoadingPermission)
                return;

            if (!repository.IsOwner(sharedCalendarId,loggedInUserId))
            {
                return;
            }

            if (sender is RadioButton radio && !radio.Checked)
            {
                return;
            }

            try
            {
                bool ownerOnly = radioRead.Checked;
                repository.SetOwnerOnlyEdit(sharedCalendarId,ownerOnly);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "수정 권한을 변경하지 못했습니다.\n\n" +
                    ex.Message,
                    "공유 캘린더",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);


                LoadPermission();
            }
        }

        // ============================================================
        // 공유 캘린더 완전 삭제
        // ============================================================

        private void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            // ========================================================
            // 1. 방장인지 확인
            // ========================================================

            bool isOwner =
                repository.IsOwner(
                    sharedCalendarId,
                    loggedInUserId);


            if (!isOwner)
            {
                MessageBox.Show(
                    "공유 캘린더는 관리자만 삭제할 수 있습니다.",
                    "공유 캘린더 삭제",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ========================================================
            // 2. 방 이름 가져오기
            // ========================================================

            string roomName =
                repository.GetRoomName(
                    sharedCalendarId)
                ?? "공유 캘린더";


            // ========================================================
            // 3. 삭제 확인
            // ========================================================

            DialogResult result =
                MessageBox.Show(
                    $"'{roomName}' 공유 캘린더를 삭제하시겠습니까?\n\n" +
                    "캘린더의 일정, 멤버, 채팅 기록이 모두 삭제됩니다.\n" +
                    "삭제한 공유 캘린더는 복구할 수 없습니다.",
                    "공유 캘린더 삭제",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);


            if (result !=
                DialogResult.Yes)
            {
                return;
            }


            try
            {
                // ====================================================
                // 4. DB에서 공유 캘린더 완전 삭제
                // ====================================================

                repository.DeleteSharedCalendar(
                    sharedCalendarId);


                MessageBox.Show(
                    $"'{roomName}' 공유 캘린더가 삭제되었습니다.",
                    "공유 캘린더 삭제",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                // ====================================================
                // 5. mainForm에게
                // "공유방이 삭제됐다"고 알려주기
                // ====================================================

                DialogResult =
                    DialogResult.Abort;


                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "공유 캘린더를 삭제하지 못했습니다.\n\n" +
                    ex.Message,
                    "공유 캘린더 삭제",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
