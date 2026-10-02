using calendar4.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static calendar4.CalendarControl;

namespace calendar4
{
    public partial class CalenderScheduleListForm : Form
    {
        private readonly DateTime date;

        private readonly List<CalendarScheduleEntry> schedules;

        private readonly CalendarDataMode dataMode;

        private readonly int calendarId;

        private readonly int loggedInUserId;


        // ============================================================
        // 공유 캘린더 Repository
        // 수정 권한 확인에 사용
        // ============================================================

        private readonly SharedCalendarRepository
            sharedCalendarRepository =
                new SharedCalendarRepository();


        // ============================================================
        // 반복 일정 전체 삭제를 선택한 그룹 ID
        // ============================================================

        public HashSet<string> RepeatGroupsToDelete { get; }
            = new HashSet<string>();


        // ============================================================
        // 외부에서 최종 일정 목록 가져가기
        // ============================================================

        public List<CalendarScheduleEntry> Schedules =>
            schedules
                .OrderBy(
                    item =>
                        item.StartHour)
                .ThenBy(
                    item =>
                        item.EndHour)
                .Select(
                    item =>
                        item.Copy())
                .ToList();


        // ============================================================
        // 디자이너용 기본 생성자
        // ============================================================

        public CalenderScheduleListForm()
        {
            InitializeComponent();


            date =
                DateTime.Today;


            schedules =
                new List<CalendarScheduleEntry>();


            dataMode =
                CalendarDataMode.Personal;


            calendarId =
                0;


            loggedInUserId =
                0;
        }


        // ============================================================
        // 실제 프로그램에서 사용하는 생성자
        // ============================================================

        public CalenderScheduleListForm(
            DateTime date,
            IEnumerable<CalendarScheduleEntry> existing,
            CalendarDataMode dataMode,
            int calendarId,
            int loggedInUserId)
            : this()
        {
            this.date =
                date.Date;


            schedules =
                existing
                    .Select(
                        item =>
                            item.Copy())
                    .ToList();


            this.dataMode =
                dataMode;


            this.calendarId =
                calendarId;


            this.loggedInUserId =
                loggedInUserId;


            // ========================================================
            // 폼 제목
            // ========================================================

            Text =
                $"{date:yyyy-MM-dd} 일정 관리";


            title.Text =
                $"{date:M월 d일} 일정";


            // ========================================================
            // ListBox 설정
            // ========================================================

            ScheduleList.DrawMode =
                DrawMode.OwnerDrawFixed;


            ScheduleList.ItemHeight =
                36;


            ScheduleList.DrawItem +=
                DrawScheduleItem;


            ScheduleList.DoubleClick +=
                (_, _) =>
                    EditSelected();


            RefreshList();


            // ========================================================
            // ★ 공유 캘린더 수정 권한 적용
            //
            // 관리자만 수정 가능:
            // 일반 멤버는 목록만 볼 수 있음
            // ========================================================

            ApplyEditPermission();
        }


        // ============================================================
        // 공유 캘린더 수정 권한을 버튼 상태에 적용
        // ============================================================

        private void ApplyEditPermission()
        {
            // ========================================================
            // 개인 캘린더
            // 항상 수정 가능
            // ========================================================

            if (dataMode ==
                CalendarDataMode.Personal)
            {
                addButton.Enabled =
                    true;


                editButton.Enabled =
                    true;


                deleteButton.Enabled =
                    true;


                return;
            }


            // ========================================================
            // 공유 캘린더
            // ========================================================

            try
            {
                bool canEdit =
                    sharedCalendarRepository
                        .CanEditSchedule(
                            calendarId,
                            loggedInUserId);


                addButton.Enabled =
                    canEdit;


                editButton.Enabled =
                    canEdit;


                deleteButton.Enabled =
                    canEdit;
            }
            catch (Exception ex)
            {
                // 권한 확인 실패 시 안전하게 수정 차단
                addButton.Enabled =
                    false;


                editButton.Enabled =
                    false;


                deleteButton.Enabled =
                    false;


                MessageBox.Show(
                    "공유 캘린더 수정 권한을 확인하지 못했습니다.\n\n" +
                    ex.Message,
                    "수정 권한",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 현재 사용자가 일정 수정 가능한지 확인
        //
        // 버튼 비활성화만으로는
        // ListBox 더블클릭 등을 완전히 막지 못하기 때문에
        // 실제 추가/수정/삭제 직전에도 다시 확인
        // ============================================================

        private bool CanEditCurrentCalendar(
            bool showMessage = true)
        {
            // 개인 캘린더
            if (dataMode ==
                CalendarDataMode.Personal)
            {
                return true;
            }


            try
            {
                bool canEdit =
                    sharedCalendarRepository
                        .CanEditSchedule(
                            calendarId,
                            loggedInUserId);


                if (!canEdit &&
                    showMessage)
                {
                    MessageBox.Show(
                        "이 공유 캘린더는 관리자만 일정을 수정할 수 있습니다.",
                        "수정 권한",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }


                return canEdit;
            }
            catch (Exception ex)
            {
                if (showMessage)
                {
                    MessageBox.Show(
                        "공유 캘린더 수정 권한을 확인하지 못했습니다.\n\n" +
                        ex.Message,
                        "수정 권한",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }


                return false;
            }
        }


        // ============================================================
        // 새 일정 버튼
        // ============================================================

        private void addButton_Click(
            object sender,
            EventArgs e)
        {
            AddSchedule();
        }


        // ============================================================
        // 새 일정 추가
        // ============================================================

        private void AddSchedule()
        {
            // ========================================================
            // 수정 권한 확인
            // ========================================================

            if (!CanEditCurrentCalendar())
            {
                return;
            }


            // ========================================================
            // 공유 캘린더
            // ========================================================

            if (dataMode ==
                CalendarDataMode.Shared)
            {
                using var editor =
                    new SharedCalendarScheduleEditorDialog(
                        calendarId,
                        loggedInUserId,
                        date);


                if (editor.ShowDialog(this)
                    != DialogResult.OK)
                {
                    return;
                }


                schedules.Add(
                    editor.Schedule);


                RefreshList(
                    editor.Schedule.Id);


                return;
            }


            // ========================================================
            // 개인 캘린더
            // ========================================================

            using var personalEditor =
                new CalendarScheduleEditorForm(
                    date);


            if (personalEditor.ShowDialog(this)
                != DialogResult.OK)
            {
                return;
            }


            schedules.Add(
                personalEditor.Schedule);


            RefreshList(
                personalEditor.Schedule.Id);
        }


        // ============================================================
        // 일정 수정 버튼
        // ============================================================

        private void editButton_Click(
            object sender,
            EventArgs e)
        {
            EditSelected();
        }


        // ============================================================
        // 일정 수정
        // ============================================================

        private void EditSelected()
        {
            // ========================================================
            // ★ 공유 캘린더 수정 권한 확인
            //
            // 버튼이 비활성화되어도
            // ListBox 더블클릭으로 들어올 수 있기 때문에
            // 여기에서 다시 검사
            // ========================================================

            if (!CanEditCurrentCalendar())
            {
                return;
            }


            // ========================================================
            // 선택한 일정 없으면 종료
            // ========================================================

            if (ScheduleList.SelectedItem
                is not CalendarScheduleEntry selected)
            {
                return;
            }


            // ========================================================
            // ★ 공유 캘린더 일정 수정
            // ========================================================

            if (dataMode ==
                CalendarDataMode.Shared)
            {
                using var editor =
                    new SharedCalendarScheduleEditorDialog(
                        calendarId,
                        loggedInUserId,
                        date,
                        selected);


                if (editor.ShowDialog(this)
                    != DialogResult.OK)
                {
                    return;
                }


                int index =
                    schedules.FindIndex(
                        item =>
                            item.Id ==
                            selected.Id);


                if (index >= 0)
                {
                    // 기존 ID 유지
                    editor.Schedule.Id =
                        selected.Id;


                    editor.Schedule.CalId =
                        selected.CalId;


                    schedules[index] =
                        editor.Schedule;
                }


                RefreshList(
                    selected.Id);


                return;
            }


            // ========================================================
            // 개인 캘린더 일정 수정
            // ========================================================

            using var personalEditor =
                new CalendarScheduleEditorForm(
                    date,
                    selected);


            if (personalEditor.ShowDialog(this)
                != DialogResult.OK)
            {
                return;
            }


            int personalIndex =
                schedules.FindIndex(
                    item =>
                        item.Id ==
                        selected.Id);


            if (personalIndex >= 0)
            {
                personalEditor.Schedule.Id =
                    selected.Id;


                personalEditor.Schedule.CalId =
                    selected.CalId;


                schedules[personalIndex] =
                    personalEditor.Schedule;
            }


            RefreshList(
                selected.Id);
        }


        // ============================================================
        // 일정 삭제 버튼
        // ============================================================

        private void deleteButton_Click(
            object sender,
            EventArgs e)
        {
            DeleteSelected();
        }


        // ============================================================
        // 일정 삭제
        // ============================================================

        private void DeleteSelected()
        {
            // ========================================================
            // 수정 권한 확인
            // ========================================================

            if (!CanEditCurrentCalendar())
            {
                return;
            }


            if (ScheduleList.SelectedItem
                is not CalendarScheduleEntry selected)
            {
                return;
            }


            // ========================================================
            // 반복 일정
            // ========================================================

            if (!string.IsNullOrWhiteSpace(
                    selected.RepeatGroupId))
            {
                DialogResult result =
                    MessageBox.Show(
                        "이 일정은 반복 일정입니다.\n\n" +
                        "예 : 반복 일정 전체 삭제\n" +
                        "아니오 : 선택한 일정만 삭제\n" +
                        "취소 : 삭제하지 않음",
                        "반복 일정 삭제",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);


                // 취소
                if (result ==
                    DialogResult.Cancel)
                {
                    return;
                }


                // ====================================================
                // 반복 일정 전체 삭제
                // ====================================================

                if (result ==
                    DialogResult.Yes)
                {
                    RepeatGroupsToDelete.Add(
                        selected.RepeatGroupId);


                    schedules.RemoveAll(
                        item =>
                            item.RepeatGroupId ==
                            selected.RepeatGroupId);


                    RefreshList();


                    return;
                }


                // ====================================================
                // 현재 일정 하나만 삭제
                // ====================================================

                schedules.RemoveAll(
                    item =>
                        item.Id ==
                        selected.Id);


                RefreshList();


                return;
            }


            // ========================================================
            // 일반 일정
            // ========================================================

            DialogResult normalResult =
                MessageBox.Show(
                    $"'{selected.Text}' 일정을 삭제하시겠습니까?",
                    "일정 삭제",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);


            if (normalResult !=
                DialogResult.Yes)
            {
                return;
            }


            schedules.RemoveAll(
                item =>
                    item.Id ==
                    selected.Id);


            RefreshList();
        }


        // ============================================================
        // 카테고리 관리 버튼
        // ============================================================

        private void categoriesButton_Click(
            object sender,
            EventArgs e)
        {
            EditCategories();
        }


        // ============================================================
        // 카테고리 관리
        // ============================================================

        private void EditCategories()
        {
            // 공유 캘린더는 카테고리 기능 사용 안 함
            if (dataMode ==
                CalendarDataMode.Shared)
            {
                return;
            }


            using var dialog =
                new UserCategoryManagerForm(
                    "개인 캘린더 카테고리 관리",
                    PersonalCategoryStores.Calendar);


            if (dialog.ShowDialog(this)
                == DialogResult.OK)
            {
                ScheduleList.Invalidate();
            }
        }


        // ============================================================
        // 완료 버튼
        // ============================================================

        private void doneButton_Click(
            object sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.OK;


            Close();
        }


        // ============================================================
        // 일정 목록 새로고침
        // ============================================================

        private void RefreshList(
            Guid? selectedId = null)
        {
            ScheduleList.BeginUpdate();


            ScheduleList.Items.Clear();


            foreach (
                var schedule
                in schedules
                    .OrderBy(
                        item =>
                            item.StartHour)
                    .ThenBy(
                        item =>
                            item.EndHour))
            {
                ScheduleList.Items.Add(
                    schedule);
            }


            ScheduleList.EndUpdate();


            if (!selectedId.HasValue)
            {
                return;
            }


            for (int i = 0;
                 i < ScheduleList.Items.Count;
                 i++)
            {
                if (((CalendarScheduleEntry)
                        ScheduleList.Items[i])
                        .Id
                    != selectedId.Value)
                {
                    continue;
                }


                ScheduleList.SelectedIndex =
                    i;


                break;
            }
        }


        // ============================================================
        // ListBox 일정 직접 그리기
        // ============================================================

        private void DrawScheduleItem(
            object? sender,
            DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }


            e.DrawBackground();


            var schedule =
                (CalendarScheduleEntry)
                ScheduleList.Items[e.Index];


            // ========================================================
            // 왼쪽 색상 바 영역
            // ========================================================

            Rectangle barBounds =
                new Rectangle(
                    e.Bounds.Left + 5,
                    e.Bounds.Top + 5,
                    8,
                    e.Bounds.Height - 10);


            // ========================================================
            // 공유 캘린더 색상
            // ========================================================

            if (dataMode ==
                CalendarDataMode.Shared)
            {
                // ====================================================
                // 전체 회원 일정
                // ====================================================

                if (schedule.IsAllMembers)
                {
                    Color color =
                        SharedCalendarColorService
                            .GetAllMembersColor();


                    using var brush =
                        new SolidBrush(
                            color);


                    e.Graphics.FillRectangle(
                        brush,
                        barBounds);
                }


                // ====================================================
                // 일부 회원 일정
                // ====================================================

                else if (
                    schedule.MemberColorIndexes != null &&
                    schedule.MemberColorIndexes.Count > 0)
                {
                    var colorIndexes =
                        schedule.MemberColorIndexes
                            .Distinct()
                            .OrderBy(
                                index =>
                                    index)
                            .ToList();


                    int count =
                        colorIndexes.Count;


                    // ================================================
                    // 참여자 1명
                    // ================================================

                    if (count == 1)
                    {
                        Color color =
                            SharedCalendarColorService
                                .GetMemberColor(
                                    colorIndexes[0]);


                        using var brush =
                            new SolidBrush(
                                color);


                        e.Graphics.FillRectangle(
                            brush,
                            barBounds);
                    }


                    // ================================================
                    // 참여자 여러 명
                    // ================================================

                    else
                    {
                        for (int i = 0;
                             i < count;
                             i++)
                        {
                            int top =
                                barBounds.Top +
                                barBounds.Height *
                                i /
                                count;


                            int bottom =
                                barBounds.Top +
                                barBounds.Height *
                                (i + 1) /
                                count;


                            int height =
                                Math.Max(
                                    1,
                                    bottom - top);


                            Color color =
                                SharedCalendarColorService
                                    .GetMemberColor(
                                        colorIndexes[i]);


                            using var brush =
                                new SolidBrush(
                                    color);


                            e.Graphics.FillRectangle(
                                brush,
                                new Rectangle(
                                    barBounds.Left,
                                    top,
                                    barBounds.Width,
                                    height));
                        }
                    }
                }


                // ====================================================
                // 색상 정보 없는 경우
                // ====================================================

                else
                {
                    Color color =
                        SharedCalendarColorService
                            .GetMemberColor(1);


                    using var brush =
                        new SolidBrush(
                            color);


                    e.Graphics.FillRectangle(
                        brush,
                        barBounds);
                }
            }


            // ========================================================
            // 개인 캘린더 색상
            // ========================================================

            else
            {
                Color accent =
                    PersonalCategoryStores.Calendar
                        .GetScheduleAccentColor(
                            schedule.CategoryId,
                            schedule.CustomColorArgb);


                using var brush =
                    new SolidBrush(
                        accent);


                e.Graphics.FillRectangle(
                    brush,
                    barBounds);
            }


            // ========================================================
            // 일정 내용
            // ========================================================

            string text =
                $"{schedule.StartHour:00}:00~{schedule.EndHour:00}:00   " +
                $"{schedule.Text}";


            // ========================================================
            // 공유 캘린더 참여자
            // ========================================================

            if (dataMode ==
                CalendarDataMode.Shared)
            {
                string memberText;


                if (schedule.IsAllMembers)
                {
                    memberText =
                        "전체 회원";
                }
                else
                {
                    memberText =
                        schedule.MemberNames != null &&
                        schedule.MemberNames.Count > 0
                            ? string.Join(
                                ", ",
                                schedule.MemberNames)
                            : "참여자 없음";
                }


                text +=
                    $"   [참여자: {memberText}]";
            }


            // ========================================================
            // 개인 캘린더 카테고리
            // ========================================================

            if (dataMode ==
                CalendarDataMode.Personal)
            {
                text +=
                    $"   [{PersonalCategoryStores.Calendar.Get(schedule.CategoryId).Name}]";
            }


            // ========================================================
            // 텍스트 그리기
            // ========================================================

            TextRenderer.DrawText(
                e.Graphics,
                text,
                ScheduleList.Font,
                new Rectangle(
                    e.Bounds.Left + 21,
                    e.Bounds.Top,
                    e.Bounds.Width - 24,
                    e.Bounds.Height),
                e.ForeColor,
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis);


            e.DrawFocusRectangle();
        }
    }
}