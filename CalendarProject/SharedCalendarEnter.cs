using System;
using System.Windows.Forms;
using calendar4.Services;

namespace calendar4
{
    public partial class SharedCalendarEnter : UserControl
    {
        private readonly int loggedInUserId;

        private readonly SharedCalendarRepository
            sharedCalendarRepository = new();

        private readonly emailSend
            emailSender = new();

        public event Action<int>? SharedCalendarEntered;


        // ============================================================
        // 생성자
        // ============================================================

        public SharedCalendarEnter(
            int userId)
        {
            InitializeComponent();

            loggedInUserId =
                userId;
        }


        // ============================================================
        // 새 공유 캘린더 만들기
        // ============================================================

        private void btnCreate_Click(
            object sender,
            EventArgs e)
        {
            string roomName =
                txtCalname.Text.Trim();

            string roomCode =
                txtCreatecode.Text.Trim();

            string emailText =
                txtEmail.Text.Trim();


            // --------------------------------------------------------
            // 1. 캘린더 이름 확인
            // --------------------------------------------------------

            if (string.IsNullOrWhiteSpace(
                    roomName))
            {
                MessageBox.Show(
                    "캘린더 이름을 입력해주세요.",
                    "공유 캘린더 생성",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtCalname.Focus();

                return;
            }


            // --------------------------------------------------------
            // 2. 참가 코드 확인
            // --------------------------------------------------------

            if (string.IsNullOrWhiteSpace(
                    roomCode))
            {
                MessageBox.Show(
                    "참가 코드를 입력해주세요.",
                    "공유 캘린더 생성",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtCreatecode.Focus();

                return;
            }


            try
            {
                // ----------------------------------------------------
                // 3. 참가 코드 중복 확인
                // ----------------------------------------------------

                if (sharedCalendarRepository
                    .RoomCodeExists(
                        roomCode))
                {
                    MessageBox.Show(
                        "이미 사용 중인 참가 코드입니다.\n" +
                        "다른 참가 코드를 입력해주세요.",
                        "공유 캘린더 생성",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtCreatecode.SelectAll();
                    txtCreatecode.Focus();

                    return;
                }


                // ----------------------------------------------------
                // 4. 공유 캘린더 생성
                // ----------------------------------------------------

                int sharedCalendarId =
                    sharedCalendarRepository
                        .CreateRoom(
                            loggedInUserId,
                            roomName,
                            roomCode);


                // ----------------------------------------------------
                // 5. 이메일 초대
                //
                // 이메일 입력이 비어 있으면 그냥 넘어감
                // ----------------------------------------------------

                int sentCount = 0;

                if (!string.IsNullOrWhiteSpace(
                        emailText))
                {
                    sentCount =
                        emailSender.SendInviteCode(
                            emailText,
                            roomName,
                            roomCode);
                }


                // ----------------------------------------------------
                // 6. 생성 완료 메시지
                // ----------------------------------------------------

                string emailResultText =
                    string.IsNullOrWhiteSpace(
                        emailText)
                        ? ""
                        : $"\n초대 메일 : {sentCount}명 발송 완료";


                MessageBox.Show(
                    $"공유 캘린더가 생성되었습니다.\n\n" +
                    $"캘린더 이름 : {roomName}\n" +
                    $"참가 코드 : {roomCode}" +
                    emailResultText,
                    "공유 캘린더 생성 완료",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                // ----------------------------------------------------
                // 7. 입력칸 초기화
                // ----------------------------------------------------

                txtCalname.Clear();

                txtCreatecode.Clear();

                txtEmail.Clear();


                // ----------------------------------------------------
                // 8. 실제 공유 캘린더 화면으로 이동
                // ----------------------------------------------------

                SharedCalendarEntered?.Invoke(
                    sharedCalendarId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"공유 캘린더 생성 중 오류가 발생했습니다.\n\n" +
                    $"{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 기존 공유 캘린더 입장
        // ============================================================

        private void btnEnter_Click(
            object sender,
            EventArgs e)
        {
            string roomCode =
                txtEntercode.Text.Trim();


            // --------------------------------------------------------
            // 1. 참가 코드 확인
            // --------------------------------------------------------

            if (string.IsNullOrWhiteSpace(
                    roomCode))
            {
                MessageBox.Show(
                    "참가 코드를 입력해주세요.",
                    "공유 캘린더 입장",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtEntercode.Focus();

                return;
            }


            try
            {
                // ----------------------------------------------------
                // 2. 참가 코드로 방 찾기
                // ----------------------------------------------------

                int? sharedCalendarId =
                    sharedCalendarRepository
                        .FindRoomByCode(
                            roomCode);


                if (sharedCalendarId == null)
                {
                    MessageBox.Show(
                        "존재하지 않는 참가 코드입니다.",
                        "공유 캘린더 입장",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtEntercode.SelectAll();
                    txtEntercode.Focus();

                    return;
                }


                // ----------------------------------------------------
                // 3. 이미 참가 중인지 확인
                // ----------------------------------------------------

                bool alreadyMember =
                    sharedCalendarRepository
                        .IsMember(
                            sharedCalendarId.Value,
                            loggedInUserId);


                // 아직 멤버가 아니라면 등록
                if (!alreadyMember)
                {
                    sharedCalendarRepository
                        .JoinRoom(
                            sharedCalendarId.Value,
                            loggedInUserId);
                }


                // ----------------------------------------------------
                // 4. 방 이름 가져오기
                // ----------------------------------------------------

                string roomName =
                    sharedCalendarRepository
                        .GetRoomName(
                            sharedCalendarId.Value)
                    ?? "공유 캘린더";


                // ----------------------------------------------------
                // 5. 안내 메시지
                // ----------------------------------------------------

                if (alreadyMember)
                {
                    MessageBox.Show(
                        $"이미 참가 중인 공유 캘린더입니다.\n\n" +
                        $"'{roomName}' 캘린더로 이동합니다.",
                        "공유 캘린더",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"공유 캘린더에 참가했습니다.\n\n" +
                        $"캘린더 이름 : {roomName}",
                        "공유 캘린더 참가 완료",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }


                txtEntercode.Clear();


                // ----------------------------------------------------
                // 6. 실제 공유 캘린더 화면으로 이동
                // ----------------------------------------------------

                SharedCalendarEntered?.Invoke(
                    sharedCalendarId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"공유 캘린더 참가 중 오류가 발생했습니다.\n\n" +
                    $"{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}