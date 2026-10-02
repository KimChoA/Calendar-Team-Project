using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calendar4
{
    public partial class CalendarScheduleEditorForm : Form
    {
        private readonly CalendarScheduleEntry? existing;
        private readonly DateTime selectedDate;

        private int? customColorArgb;

        private static readonly int[] AlarmOffsets =
        {
            0, 5, 10, 30, 60, 120
        };

        public CalendarScheduleEntry Schedule { get; private set; } = null!;

        public CalendarScheduleEditorForm()
        {
            InitializeComponent();

            existing = null;
            selectedDate = DateTime.Today;
        }

        public CalendarScheduleEditorForm(
            DateTime date,
            CalendarScheduleEntry? schedule = null)
        {
            InitializeComponent();

            existing = schedule;
            customColorArgb = schedule?.CustomColorArgb;

            // 여러 날짜 일정의 중간 날짜에서 수정창을 열어도
            // 실제 시작일을 유지
            selectedDate =
                schedule != null &&
                schedule.StartDate != default
                    ? schedule.StartDate.Date
                    : date.Date;

            Text = schedule is null ? "일정 추가" : "일정 수정";

            lblDate.Text = $"{selectedDate:yyyy년 M월 d일}";

            txtSchedule.Text = schedule?.Text ?? string.Empty;

            for (int hour = 8; hour < 22; hour++)
                cboStartTime.Items.Add($"{hour:00}:00");

            for (int hour = 9; hour <= 22; hour++)
                cboEndTime.Items.Add($"{hour:00}:00");

            cboStartTime.SelectedIndex =
                Math.Clamp((schedule?.StartHour ?? 9) - 8, 0, cboStartTime.Items.Count - 1);

            cboEndTime.SelectedIndex =
                Math.Clamp((schedule?.EndHour ?? 10) - 9, 0, cboEndTime.Items.Count - 1);

            LoadCategories(schedule?.CategoryId);

            chkImportant.Checked =
                schedule?.IsHighPriority ?? false;

            cboAlarm.Items.AddRange(new object[]
            {
                "알림 없음",
                "5분 전",
                "10분 전",
                "30분 전",
                "1시간 전",
                "2시간 전"
            });

            int alarmIndex =
                Array.IndexOf(
                    AlarmOffsets,
                    schedule?.NotificationOffset ?? 0);

            cboAlarm.SelectedIndex =
                alarmIndex >= 0 ? alarmIndex : 0;

            // ============================================================
            // 일정 종료일 초기화
            // ============================================================

            if (schedule != null &&
                schedule.EndDate != default)
            {
                dtpScheduleEndDate.Value =
                    schedule.EndDate.Date;
            }
            else
            {
                dtpScheduleEndDate.Value =
                    selectedDate;
            }

            // ============================================================
            // 반복 설정 초기화
            // ============================================================

            if (schedule == null)
            {
                // 새 일정은 기본적으로 반복 없음
                radioNone.Checked = true;

                // 기본 종료일은 일정 날짜 기준 한 달 뒤
                dtpRepeatEndDate.Value =
                    date.Date.AddMonths(1);
            }
            else
            {
                // 기존 일정 수정 시 저장되어 있던 반복 설정 표시
                switch (schedule.RepeatType)
                {
                    case "DAILY":
                        radioDay.Checked = true;
                        break;

                    case "WEEKLY":
                        radioWeek.Checked = true;
                        break;

                    case "MONTHLY":
                        radioMonth.Checked = true;
                        break;

                    case "YEARLY":
                        radioYear.Checked = true;
                        break;

                    default:
                        radioNone.Checked = true;
                        break;
                }

                if (schedule.RepeatEndDate.HasValue)
                {
                    dtpRepeatEndDate.Value =
                        schedule.RepeatEndDate.Value;
                }
                else
                {
                    dtpRepeatEndDate.Value =
                        date.Date.AddMonths(1);
                }
            }
            RefreshColorPreview();
        }

        private void LoadCategories(string? selectedId)
        {
            cboCategory.BeginUpdate();
            cboCategory.Items.Clear();

            foreach (var category in PersonalCategoryStores.Calendar.Categories)
                cboCategory.Items.Add(category);

            cboCategory.EndUpdate();

            var targetId =
                selectedId ?? UserCategoryStore.HomeId;

            for (int i = 0; i < cboCategory.Items.Count; i++)
            {
                if (((UserCategory)cboCategory.Items[i]).Id == targetId)
                {
                    cboCategory.SelectedIndex = i;
                    return;
                }
            }

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            ChooseCustomColor();
        }

        private void btnDefaultColor_Click(object sender, EventArgs e)
        {
            customColorArgb = null;
            RefreshColorPreview();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            EditCategories();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshColorPreview();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSchedule();
        }

        private void ChooseCustomColor()
        {
            var categoryId =
                (cboCategory.SelectedItem as UserCategory)?.Id
                ?? UserCategoryStore.HomeId;

            using var picker = new ColorDialog
            {
                Color = PersonalCategoryStores.Calendar.GetScheduleAccentColor(
                    categoryId,
                    customColorArgb),
                FullOpen = true
            };

            if (picker.ShowDialog(this) != DialogResult.OK)
                return;

            customColorArgb = picker.Color.ToArgb();
            RefreshColorPreview();
        }

        private void EditCategories()
        {
            var selectedId =
                (cboCategory.SelectedItem as UserCategory)?.Id;

            using var dialog =
                new UserCategoryManagerForm(
                    "개인 캘린더 카테고리 관리",
                    PersonalCategoryStores.Calendar);

            if (dialog.ShowDialog(this) == DialogResult.OK)
                LoadCategories(selectedId);
        }

        private void RefreshColorPreview()
        {
            if (cboCategory.SelectedIndex < 0)
                return;

            pnlColorPreview.BackColor =
                PersonalCategoryStores.Calendar.GetScheduleAccentColor(
                    (cboCategory.SelectedItem as UserCategory)?.Id,
                    customColorArgb);
        }

        private void SaveSchedule()
        {
            // ============================================================
            // 1. 일정 내용 확인
            // ============================================================

            string text =
                txtSchedule.Text.Trim();

            if (text.Length == 0)
            {
                MessageBox.Show(
                    "일정 내용을 입력해 주세요.",
                    "입력 확인",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtSchedule.Focus();

                return;
            }


            // ============================================================
            // 2. 시작 / 종료 시간
            // ============================================================

            int startHour =
                8 + cboStartTime.SelectedIndex;

            int endHour =
                9 + cboEndTime.SelectedIndex;


            DateTime scheduleEndDate =
                dtpScheduleEndDate.Value.Date;


            // 일정 종료일은 시작일보다 빠를 수 없음
            if (scheduleEndDate < selectedDate)
            {
                MessageBox.Show(
                    "일정 종료일은 시작일보다 빠를 수 없습니다.",
                    "일정 기간",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dtpScheduleEndDate.Focus();

                return;
            }


            // 같은 날짜 일정일 때만
            // 종료 시간이 시작 시간보다 늦어야 함
            if (scheduleEndDate == selectedDate &&
                endHour <= startHour)
            {
                MessageBox.Show(
                    "같은 날짜의 일정은 종료 시간이 시작 시간보다 늦어야 합니다.",
                    "시간 확인",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            // ============================================================
            // 3. 반복 종류 확인
            // ============================================================

            string repeatType =
                GetRepeatType();


            // ============================================================
            // 4. 반복 종료일 확인
            // ============================================================

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


            // ============================================================
            // 5. 일정 객체 생성
            // ============================================================

            Schedule =
                new CalendarScheduleEntry
                {
                    // 기존 일정 수정이면 기존 ID 유지
                    Id =
                        existing?.Id
                        ?? Guid.NewGuid(),

                    // DB 일정 ID도 수정 시 유지
                    CalId =
                        existing?.CalId,


                    // --------------------------------------------------------
                    // 기본 일정 정보
                    // --------------------------------------------------------

                    Text =
                        text,

                    // --------------------------------------------------------
                    // 일정 실제 기간
                    // --------------------------------------------------------

                    StartDate =
                        selectedDate,

                    EndDate =
                        scheduleEndDate,

                    StartHour =
                        startHour,

                    EndHour =
                        endHour,

                    CategoryId =
                        (cboCategory.SelectedItem
                            as UserCategory)?.Id
                        ?? UserCategoryStore.HomeId,

                    CustomColorArgb =
                        customColorArgb,

                    IsHighPriority =
                        chkImportant.Checked,

                    NotificationOffset =
                        AlarmOffsets[
                            cboAlarm.SelectedIndex],


                    // --------------------------------------------------------
                    // 공유 캘린더 작성자 이름 유지
                    // --------------------------------------------------------

                    WriterName =
                        existing?.WriterName,


                    // ========================================================
                    // ★ 반복 일정 정보
                    // ========================================================

                    RepeatType =
                        repeatType,

                    // 현재는 항상 1단위 반복
                    //
                    // DAILY   → 매일
                    // WEEKLY  → 매주
                    // MONTHLY → 매월
                    // YEARLY  → 매년
                    RepeatInterval =
                        1,

                    RepeatEndDate =
                        repeatType == "NONE"
                            ? null
                            : dtpRepeatEndDate.Value.Date,

                    // 기존 반복 일정을 수정하는 경우에는
                    // 기존 반복 그룹 ID를 유지
                    //
                    // 새 반복 일정이라면 아직 null이고
                    // 실제 CalendarControl에서 반복 일정을 생성할 때
                    // 하나의 GUID를 만들어 넣어줄 예정
                    RepeatGroupId =
                        existing?.RepeatGroupId
                };


            // ============================================================
            // 6. 저장 완료
            // ============================================================

            DialogResult =
                DialogResult.OK;

            Close();
        }
        private string GetRepeatType()
        {
            if (radioDay.Checked)
            {
                return "DAILY";
            }

            if (radioWeek.Checked)
            {
                return "WEEKLY";
            }

            if (radioMonth.Checked)
            {
                return "MONTHLY";
            }

            if (radioYear.Checked)
            {
                return "YEARLY";
            }

            return "NONE";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

