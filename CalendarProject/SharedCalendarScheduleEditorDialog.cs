using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using calendar4.Services;

// ============================================================
// [포트폴리오 담당 연계] 공유 일정 참여자 선택 및 색상 정보 저장
// - 전체 참여/특정 참여자 선택
// - 선택 사용자별 ColorIndex를 일정 데이터에 함께 저장
// ============================================================

namespace calendar4
{
    public partial class SharedCalendarScheduleEditorDialog : Form
    {
        private readonly int sharedCalendarId;
        private readonly int loggedInUserId;
        private readonly DateTime selectedDate;

        private readonly SharedCalendarRepository repository =
            new();

        private List<
            (
                int UserId,
                string Name,
                int ColorIndex
            )> members =
            new();

        private readonly CalendarScheduleEntry? existingSchedule;


        // ============================================================
        // 밖으로 전달할 최종 일정
        // ============================================================

        public CalendarScheduleEntry Schedule
        {
            get;
            private set;
        } = new();


        public List<int> SelectedMemberUserIds
        {
            get;
            private set;
        } = new();


        // ============================================================
        // 디자이너용 기본 생성자
        // ============================================================

        public SharedCalendarScheduleEditorDialog()
        {
            InitializeComponent();

            sharedCalendarId = 0;
            loggedInUserId = 0;
            selectedDate = DateTime.Today;
            existingSchedule = null;
        }


        // ============================================================
        // 이전 호출부 호환용 생성자
        // ============================================================

        public SharedCalendarScheduleEditorDialog(
            int sharedCalendarId,
            int loggedInUserId)
            : this(
                sharedCalendarId,
                loggedInUserId,
                DateTime.Today)
        {
        }


        // ============================================================
        // 새 일정 추가용 생성자
        // ============================================================

        public SharedCalendarScheduleEditorDialog(
            int sharedCalendarId,
            int loggedInUserId,
            DateTime date)
        {
            InitializeComponent();

            this.sharedCalendarId =
                sharedCalendarId;

            this.loggedInUserId =
                loggedInUserId;

            selectedDate =
                date.Date;

            existingSchedule =
                null;


            LoadTimeItems();

            LoadNotificationItems();

            LoadMembers();

            InitializeDateAndRepeatControls();


            Text =
                "공유 일정 추가";
        }


        // ============================================================
        // 이전 수정 호출부 호환용 생성자
        // ============================================================

        public SharedCalendarScheduleEditorDialog(
            int sharedCalendarId,
            int loggedInUserId,
            CalendarScheduleEntry existingSchedule)
            : this(
                sharedCalendarId,
                loggedInUserId,
                existingSchedule.StartDate != default
                    ? existingSchedule.StartDate.Date
                    : DateTime.Today,
                existingSchedule)
        {
        }


        // ============================================================
        // 기존 일정 수정용 생성자
        // ============================================================

        public SharedCalendarScheduleEditorDialog(
            int sharedCalendarId,
            int loggedInUserId,
            DateTime date,
            CalendarScheduleEntry existingSchedule)
        {
            InitializeComponent();

            this.sharedCalendarId =
                sharedCalendarId;

            this.loggedInUserId =
                loggedInUserId;

            this.existingSchedule =
                existingSchedule;

            selectedDate =
                existingSchedule.StartDate != default
                    ? existingSchedule.StartDate.Date
                    : date.Date;


            LoadTimeItems();

            LoadNotificationItems();

            LoadMembers();

            InitializeDateAndRepeatControls();

            LoadExistingSchedule();


            Text =
                "공유 일정 수정";
        }


        // ============================================================
        // 시간 목록
        // ============================================================

        private void LoadTimeItems()
        {
            cmbStartTime.Items.Clear();

            cmbEndTime.Items.Clear();


            for (int hour = 0;
                 hour <= 23;
                 hour++)
            {
                string timeText =
                    $"{hour:00}:00";

                cmbStartTime.Items.Add(
                    timeText);

                cmbEndTime.Items.Add(
                    timeText);
            }


            cmbStartTime.SelectedItem =
                "09:00";

            cmbEndTime.SelectedItem =
                "10:00";
        }


        // ============================================================
        // 알림 목록
        // ============================================================

        private void LoadNotificationItems()
        {
            cmbNotification.Items.Clear();

            cmbNotification.Items.AddRange(
                new object[]
                {
                    "알림 없음",
                    "5분 전",
                    "10분 전",
                    "30분 전",
                    "1시간 전",
                    "2시간 전"
                });

            cmbNotification.SelectedIndex =
                0;
        }


        // ============================================================
        // 날짜 / 반복 기본값
        // ============================================================

        private void InitializeDateAndRepeatControls()
        {
            dtpScheduleEndDate.Value =
                selectedDate;

            dtpRepeatEndDate.Value =
                selectedDate.AddMonths(1);

            radioNone.Checked =
                true;
        }


        // ============================================================
        // 공유캘린더 회원 목록
        // ============================================================

        // [담당 기능] 공유 캘린더 멤버 목록을 불러와 참여자 선택 UI 구성
        private void LoadMembers()
        {
            clbMembers.Items.Clear();

            members =
                repository.GetMembers(
                    sharedCalendarId);


            foreach (var member
                     in members)
            {
                clbMembers.Items.Add(
                    member.Name);
            }


            chkAllMembers.Checked =
                true;

            clbMembers.Enabled =
                false;
        }


        // ============================================================
        // 기존 일정 정보 폼에 표시
        // ============================================================

        private void LoadExistingSchedule()
        {
            if (existingSchedule == null)
            {
                return;
            }


            txtTitle.Text =
                existingSchedule.Text;


            cmbStartTime.SelectedItem =
                $"{existingSchedule.StartHour:00}:00";

            cmbEndTime.SelectedItem =
                $"{existingSchedule.EndHour:00}:00";


            // 일정 종료일 복원
            dtpScheduleEndDate.Value =
                existingSchedule.EndDate != default
                    ? existingSchedule.EndDate.Date
                    : selectedDate;


            // 반복 설정 복원
            switch (existingSchedule.RepeatType)
            {
                case "DAILY":

                    radioDay.Checked =
                        true;

                    break;


                case "WEEKLY":

                    radioWeek.Checked =
                        true;

                    break;


                case "MONTHLY":

                    radioMonth.Checked =
                        true;

                    break;


                case "YEARLY":

                    radioYear.Checked =
                        true;

                    break;


                default:

                    radioNone.Checked =
                        true;

                    break;
            }


            if (existingSchedule.RepeatEndDate.HasValue)
            {
                dtpRepeatEndDate.Value =
                    existingSchedule.RepeatEndDate.Value.Date;
            }


            switch (
                existingSchedule.NotificationOffset)
            {
                case 5:

                    cmbNotification.SelectedItem =
                        "5분 전";

                    break;


                case 10:

                    cmbNotification.SelectedItem =
                        "10분 전";

                    break;


                case 30:

                    cmbNotification.SelectedItem =
                        "30분 전";

                    break;


                case 60:

                    cmbNotification.SelectedItem =
                        "1시간 전";

                    break;


                case 120:

                    cmbNotification.SelectedItem =
                        "2시간 전";

                    break;


                default:

                    cmbNotification.SelectedItem =
                        "알림 없음";

                    break;
            }


            if (existingSchedule.IsAllMembers)
            {
                chkAllMembers.Checked =
                    true;

                clbMembers.Enabled =
                    false;

                return;
            }


            chkAllMembers.Checked =
                false;

            clbMembers.Enabled =
                true;


            // 1순위: user_id
            if (existingSchedule.MemberUserIds != null &&
                existingSchedule.MemberUserIds.Count > 0)
            {
                for (int i = 0;
                     i < members.Count;
                     i++)
                {
                    bool selected =
                        existingSchedule.MemberUserIds
                            .Contains(
                                members[i].UserId);

                    clbMembers.SetItemChecked(
                        i,
                        selected);
                }

                return;
            }


            // 2순위: 이름
            if (existingSchedule.MemberNames != null &&
                existingSchedule.MemberNames.Count > 0)
            {
                for (int i = 0;
                     i < members.Count;
                     i++)
                {
                    bool selected =
                        existingSchedule.MemberNames
                            .Contains(
                                members[i].Name);

                    clbMembers.SetItemChecked(
                        i,
                        selected);
                }

                return;
            }


            // 3순위: color_index
            if (existingSchedule.MemberColorIndexes != null &&
                existingSchedule.MemberColorIndexes.Count > 0)
            {
                for (int i = 0;
                     i < members.Count;
                     i++)
                {
                    bool selected =
                        existingSchedule.MemberColorIndexes
                            .Contains(
                                members[i].ColorIndex);

                    clbMembers.SetItemChecked(
                        i,
                        selected);
                }
            }
        }


        // ============================================================
        // 반복 종류
        // ============================================================

        private string GetRepeatType()
        {
            if (radioDay.Checked)
                return "DAILY";

            if (radioWeek.Checked)
                return "WEEKLY";

            if (radioMonth.Checked)
                return "MONTHLY";

            if (radioYear.Checked)
                return "YEARLY";

            return "NONE";
        }


        // ============================================================
        // 전체 회원 체크 변경
        // ============================================================

        private void chkAllMembers_CheckedChanged(
            object sender,
            EventArgs e)
        {
            clbMembers.Enabled =
                !chkAllMembers.Checked;


            if (chkAllMembers.Checked)
            {
                for (int i = 0;
                     i < clbMembers.Items.Count;
                     i++)
                {
                    clbMembers.SetItemChecked(
                        i,
                        false);
                }
            }
        }


        // ============================================================
        // 저장
        // ============================================================

        // [담당 기능] 선택된 참여자와 각 사용자의 ColorIndex를 일정 데이터에 함께 저장
        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtTitle.Text))
            {
                MessageBox.Show(
                    "일정 제목을 입력해주세요.",
                    "공유 일정",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtTitle.Focus();

                return;
            }


            if (cmbStartTime.SelectedIndex == -1 ||
                cmbEndTime.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "시간을 선택해주세요.",
                    "공유 일정",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            int startHour =
                int.Parse(
                    cmbStartTime
                        .SelectedItem!
                        .ToString()!
                        .Substring(
                            0,
                            2));


            int endHour =
                int.Parse(
                    cmbEndTime
                        .SelectedItem!
                        .ToString()!
                        .Substring(
                            0,
                            2));


            DateTime scheduleEndDate =
                dtpScheduleEndDate.Value.Date;


            if (scheduleEndDate < selectedDate)
            {
                MessageBox.Show(
                    "일정 종료일은 시작일보다 빠를 수 없습니다.",
                    "공유 일정",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dtpScheduleEndDate.Focus();

                return;
            }


            // 같은 날일 때만 종료 시간이 시작 시간보다 늦어야 함
            if (scheduleEndDate == selectedDate &&
                endHour <= startHour)
            {
                MessageBox.Show(
                    "같은 날짜의 일정은 종료 시간이 시작 시간보다 늦어야 합니다.",
                    "공유 일정",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            string repeatType =
                GetRepeatType();


            if (repeatType != "NONE" &&
                dtpRepeatEndDate.Value.Date < selectedDate)
            {
                MessageBox.Show(
                    "반복 종료일은 일정 시작일보다 빠를 수 없습니다.",
                    "반복 설정",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dtpRepeatEndDate.Focus();

                return;
            }


            int notificationOffset =
                0;


            switch (
                cmbNotification
                    .SelectedItem?
                    .ToString())
            {
                case "5분 전":
                    notificationOffset = 5;
                    break;

                case "10분 전":
                    notificationOffset = 10;
                    break;

                case "30분 전":
                    notificationOffset = 30;
                    break;

                case "1시간 전":
                    notificationOffset = 60;
                    break;

                case "2시간 전":
                    notificationOffset = 120;
                    break;
            }


            var selectedMemberNames =
                new List<string>();

            var selectedColorIndexes =
                new List<int>();

            SelectedMemberUserIds.Clear();


            if (!chkAllMembers.Checked)
            {
                if (clbMembers.CheckedIndices.Count == 0)
                {
                    MessageBox.Show(
                        "참여자를 한 명 이상 선택해주세요.",
                        "공유 일정",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }


                foreach (
                    int checkedIndex
                    in clbMembers.CheckedIndices)
                {
                    if (checkedIndex < 0 ||
                        checkedIndex >= members.Count)
                    {
                        continue;
                    }


                    var member =
                        members[checkedIndex];


                    SelectedMemberUserIds.Add(
                        member.UserId);

                    selectedMemberNames.Add(
                        member.Name);

                    selectedColorIndexes.Add(
                        member.ColorIndex);
                }
            }


            Schedule =
                new CalendarScheduleEntry
                {
                    Id =
                        existingSchedule?.Id
                        ?? Guid.NewGuid(),

                    CalId =
                        existingSchedule?.CalId,

                    Text =
                        txtTitle.Text.Trim(),

                    // 일정 실제 기간
                    StartDate =
                        selectedDate,

                    EndDate =
                        scheduleEndDate,

                    StartHour =
                        startHour,

                    EndHour =
                        endHour,

                    CategoryId =
                        UserCategoryStore.HomeId,

                    NotificationOffset =
                        notificationOffset,

                    IsAllMembers =
                        chkAllMembers.Checked,

                    MemberUserIds =
                        new List<int>(
                            SelectedMemberUserIds),

                    MemberNames =
                        new List<string>(
                            selectedMemberNames),

                    MemberColorIndexes =
                        new List<int>(
                            selectedColorIndexes),

                    // 공유 캘린더에도 반복 설정 저장
                    RepeatType =
                        repeatType,

                    RepeatInterval =
                        1,

                    RepeatEndDate =
                        repeatType == "NONE"
                            ? null
                            : dtpRepeatEndDate.Value.Date,

                    RepeatGroupId =
                        existingSchedule?.RepeatGroupId,

                    WriterName =
                        existingSchedule?.WriterName,

                    IsHighPriority =
                        existingSchedule?.IsHighPriority
                        ?? false,

                    CustomColorArgb =
                        existingSchedule?.CustomColorArgb
                };


            DialogResult =
                DialogResult.OK;

            Close();
        }


        // ============================================================
        // 취소
        // ============================================================

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }
    }
}
