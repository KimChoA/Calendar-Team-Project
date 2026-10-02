using System.Drawing.Drawing2D;
using calendar4.Services;
using static calendar4.CalendarControl;

namespace calendar4;

internal sealed class CalendarMonthCellRenderer
{
    private const int HorizontalPadding = 6;
    private const int ScheduleRowHeight = 20;

    private readonly UserCategoryStore categoryStore;


    public CalendarMonthCellRenderer(
        UserCategoryStore categoryStore)
    {
        this.categoryStore =
            categoryStore;
    }


    // ============================================================
    // 달력 셀 전체 그리기
    // ============================================================

    public void Draw(
        Graphics graphics,
        Rectangle bounds,
        DateTime date,
        string? holidayName,
        IReadOnlyList<CalendarScheduleEntry> schedules,
        Font baseFont,
        Color dateColor,
        bool isSelected,
        CalendarDataMode dataMode)
    {
        var previousSmoothingMode =
            graphics.SmoothingMode;

        graphics.SmoothingMode =
            SmoothingMode.AntiAlias;


        // 날짜
        DrawDate(
            graphics,
            bounds,
            date,
            baseFont,
            dateColor);


        int scheduleTop =
            bounds.Top + 25;


        // 공휴일
        if (!string.IsNullOrWhiteSpace(
                holidayName))
        {
            DrawHoliday(
                graphics,
                bounds,
                holidayName,
                baseFont);

            scheduleTop +=
                16;
        }


        // 일정
        DrawSchedules(
            graphics,
            bounds,
            scheduleTop,
            schedules,
            baseFont,
            dataMode);


        // 오늘 / 선택 테두리
        DrawCellOutline(
            graphics,
            bounds,
            date.Date ==
            DateTime.Today,
            isSelected);


        graphics.SmoothingMode =
            previousSmoothingMode;
    }


    // ============================================================
    // 날짜
    // ============================================================

    private static void DrawDate(
        Graphics graphics,
        Rectangle bounds,
        DateTime date,
        Font font,
        Color color)
    {
        TextRenderer.DrawText(
            graphics,
            date.Day.ToString(),
            font,
            new Rectangle(
                bounds.Left +
                HorizontalPadding,
                bounds.Top + 3,
                bounds.Width - 12,
                20),
            color,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.NoPadding);
    }


    // ============================================================
    // 공휴일
    // ============================================================

    private static void DrawHoliday(
        Graphics graphics,
        Rectangle bounds,
        string holidayName,
        Font baseFont)
    {
        using var holidayFont =
            new Font(
                baseFont.FontFamily,
                8F,
                FontStyle.Regular);


        TextRenderer.DrawText(
            graphics,
            holidayName,
            holidayFont,
            new Rectangle(
                bounds.Left +
                HorizontalPadding,
                bounds.Top + 22,
                bounds.Width - 12,
                16),
            Color.Firebrick,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis |
            TextFormatFlags.NoPadding);
    }


    // ============================================================
    // 일정 목록
    // ============================================================

    private void DrawSchedules(
        Graphics graphics,
        Rectangle bounds,
        int scheduleTop,
        IReadOnlyList<CalendarScheduleEntry> schedules,
        Font baseFont,
        CalendarDataMode dataMode)
    {
        if (schedules.Count == 0)
        {
            return;
        }


        int availableHeight =
            bounds.Bottom -
            scheduleTop -
            4;


        int visibleSlots =
            Math.Max(
                0,
                availableHeight /
                ScheduleRowHeight);


        if (visibleSlots == 0)
        {
            return;
        }


        var orderedSchedules =
            schedules
                .OrderByDescending(
                    item =>
                        item.IsHighPriority)
                .ThenBy(
                    item =>
                        item.StartHour)
                .ThenBy(
                    item =>
                        item.EndHour)
                .ToList();


        bool showMore =
            orderedSchedules.Count >
            visibleSlots;


        int schedulesToDraw =
            showMore
                ? Math.Max(
                    0,
                    visibleSlots - 1)
                : orderedSchedules.Count;


        using var normalFont =
            new Font(
                baseFont.FontFamily,
                8.5F,
                FontStyle.Regular);


        using var priorityFont =
            new Font(
                baseFont.FontFamily,
                8.5F,
                FontStyle.Bold);


        for (int index = 0;
             index < schedulesToDraw;
             index++)
        {
            var schedule =
                orderedSchedules[index];


            var rowBounds =
                new Rectangle(
                    bounds.Left +
                    HorizontalPadding,

                    scheduleTop +
                    index *
                    ScheduleRowHeight,

                    bounds.Width -
                    HorizontalPadding * 2,

                    ScheduleRowHeight);


            DrawSchedule(
                graphics,
                rowBounds,
                schedule,
                normalFont,
                priorityFont,
                dataMode);
        }


        if (!showMore)
        {
            return;
        }


        int hiddenCount =
            orderedSchedules.Count -
            schedulesToDraw;


        int moreTop =
            scheduleTop +
            schedulesToDraw *
            ScheduleRowHeight;


        TextRenderer.DrawText(
            graphics,
            $"+{hiddenCount}개 더보기",
            normalFont,
            new Rectangle(
                bounds.Left + 17,
                moreTop,
                bounds.Width - 23,
                ScheduleRowHeight),
            Color.DimGray,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis |
            TextFormatFlags.NoPadding);
    }


    // ============================================================
    // 일정 하나 그리기
    // ============================================================

    private void DrawSchedule(
        Graphics graphics,
        Rectangle bounds,
        CalendarScheduleEntry schedule,
        Font normalFont,
        Font priorityFont,
        CalendarDataMode dataMode)
    {
        // ========================================================
        // 개인 캘린더
        // ========================================================

        if (dataMode ==
            CalendarDataMode.Personal)
        {
            DrawPersonalScheduleColor(
                graphics,
                bounds,
                schedule);
        }

        // ========================================================
        // 공유 캘린더
        // ========================================================

        else
        {
            DrawSharedScheduleColor(
                graphics,
                bounds,
                schedule);
        }


        // 공유 캘린더도 이름은 표시하지 않고
        // 일정 내용만 표시
        string displayText =
            schedule.Text;


        string title =
            schedule.IsHighPriority
                ? $"★ {displayText}"
                : displayText;


        Color titleColor =
            schedule.IsHighPriority
                ? Color.FromArgb(
                    180,
                    48,
                    70)
                : UiThemeService.TextColor;


        // ========================================================
        // 공유 캘린더는 색상 막대가 여러 개 생길 수 있으므로
        // 제목 시작 위치를 조금 더 오른쪽으로 이동
        // ========================================================

        int titleLeft;

        if (dataMode ==
                CalendarDataMode.Shared
            &&
            !schedule.IsAllMembers
            &&
            schedule.MemberColorIndexes != null
            &&
            schedule.MemberColorIndexes.Count > 1)
        {
            int colorCount =
                schedule.MemberColorIndexes
                    .Distinct()
                    .Count();

            const int barWidth = 4;
            const int gap = 1;

            titleLeft =
                bounds.Left +
                colorCount *
                (barWidth + gap) +
                4;
        }
        else
        {
            titleLeft =
                bounds.Left + 11;
        }


        int titleWidth =
            bounds.Right -
            titleLeft;


        TextRenderer.DrawText(
            graphics,
            title,
            schedule.IsHighPriority
                ? priorityFont
                : normalFont,
            new Rectangle(
                titleLeft,
                bounds.Top,
                Math.Max(
                    1,
                    titleWidth),
                bounds.Height),
            titleColor,
            TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.EndEllipsis |
            TextFormatFlags.NoPadding);
    }


    // ============================================================
    // 개인 캘린더 색상
    // ============================================================

    private void DrawPersonalScheduleColor(
        Graphics graphics,
        Rectangle bounds,
        CalendarScheduleEntry schedule)
    {
        Color accentColor =
            categoryStore
                .GetScheduleAccentColor(
                    schedule.CategoryId,
                    schedule.CustomColorArgb);


        using var accentBrush =
            new SolidBrush(
                accentColor);


        using var path =
            CreateRoundedRectangle(
                new Rectangle(
                    bounds.Left,
                    bounds.Top + 3,
                    5,
                    bounds.Height - 6),
                2);


        graphics.FillPath(
            accentBrush,
            path);
    }


    // ============================================================
    // ★ 공유 캘린더 사람별 색상
    //
    // 예:
    //
    // 1명
    // █ 발표
    //
    // 2명
    // ██ 회의
    //
    // 3명
    // ███ asd
    //
    // 초아 프로젝트에서 보신 형태
    // ============================================================

    private static void DrawSharedScheduleColor(
        Graphics graphics,
        Rectangle bounds,
        CalendarScheduleEntry schedule)
    {
        // ========================================================
        // 전체 회원 일정
        // → 전체회원 전용 색 하나
        // ========================================================

        if (schedule.IsAllMembers)
        {
            Color color =
                SharedCalendarColorService
                    .GetAllMembersColor();


            Rectangle barBounds =
                new Rectangle(
                    bounds.Left,
                    bounds.Top + 3,
                    7,
                    bounds.Height - 6);


            using var brush =
                new SolidBrush(
                    color);


            using var path =
                CreateRoundedRectangle(
                    barBounds,
                    2);


            graphics.FillPath(
                brush,
                path);


            return;
        }


        // ========================================================
        // 일부 회원인데 색 정보가 없는 예외 상황
        // ========================================================

        if (schedule.MemberColorIndexes == null ||
            schedule.MemberColorIndexes.Count == 0)
        {
            Color color =
                SharedCalendarColorService
                    .GetMemberColor(1);


            Rectangle barBounds =
                new Rectangle(
                    bounds.Left,
                    bounds.Top + 3,
                    7,
                    bounds.Height - 6);


            using var brush =
                new SolidBrush(
                    color);


            using var path =
                CreateRoundedRectangle(
                    barBounds,
                    2);


            graphics.FillPath(
                brush,
                path);


            return;
        }


        // ========================================================
        // 참여자 color_index
        // ========================================================

        var colorIndexes =
            schedule.MemberColorIndexes
                .Distinct()
                .OrderBy(
                    index =>
                        index)
                .ToList();


        // ========================================================
        // 참여자 1명
        // ========================================================

        if (colorIndexes.Count == 1)
        {
            Color color =
                SharedCalendarColorService
                    .GetMemberColor(
                        colorIndexes[0]);


            Rectangle barBounds =
                new Rectangle(
                    bounds.Left,
                    bounds.Top + 3,
                    7,
                    bounds.Height - 6);


            using var brush =
                new SolidBrush(
                    color);


            using var path =
                CreateRoundedRectangle(
                    barBounds,
                    2);


            graphics.FillPath(
                brush,
                path);


            return;
        }


        // ========================================================
        // 참여자 여러 명
        //
        // ★ 초아 프로젝트처럼
        // 각 사람의 색 막대를 가로로 나란히 표시
        // ========================================================

        const int singleBarWidth =
            4;

        const int barGap =
            1;


        for (int i = 0;
             i < colorIndexes.Count;
             i++)
        {
            Color memberColor =
                SharedCalendarColorService
                    .GetMemberColor(
                        colorIndexes[i]);


            Rectangle memberBarBounds =
                new Rectangle(
                    bounds.Left +
                    i *
                    (
                        singleBarWidth +
                        barGap
                    ),

                    bounds.Top + 3,

                    singleBarWidth,

                    bounds.Height - 6);


            using var brush =
                new SolidBrush(
                    memberColor);


            using var path =
                CreateRoundedRectangle(
                    memberBarBounds,
                    1);


            graphics.FillPath(
                brush,
                path);
        }
    }


    // ============================================================
    // 오늘 / 선택 날짜 테두리
    // ============================================================

    private static void DrawCellOutline(
        Graphics graphics,
        Rectangle bounds,
        bool isToday,
        bool isSelected)
    {
        if (!isToday &&
            !isSelected)
        {
            return;
        }


        Color color =
            isSelected
                ? Color.FromArgb(
                    79,
                    107,
                    237)
                : Color.FromArgb(
                    78,
                    177,
                    169);


        using var pen =
            new Pen(
                color,
                2F);


        using var path =
            CreateRoundedRectangle(
                Rectangle.Inflate(
                    bounds,
                    -3,
                    -3),
                8);


        graphics.DrawPath(
            pen,
            path);
    }


    // ============================================================
    // 둥근 사각형
    // ============================================================

    private static GraphicsPath
        CreateRoundedRectangle(
            Rectangle bounds,
            int radius)
    {
        int diameter =
            radius * 2;


        var path =
            new GraphicsPath();


        path.AddArc(
            bounds.Left,
            bounds.Top,
            diameter,
            diameter,
            180,
            90);


        path.AddArc(
            bounds.Right - diameter,
            bounds.Top,
            diameter,
            diameter,
            270,
            90);


        path.AddArc(
            bounds.Right - diameter,
            bounds.Bottom - diameter,
            diameter,
            diameter,
            0,
            90);


        path.AddArc(
            bounds.Left,
            bounds.Bottom - diameter,
            diameter,
            diameter,
            90,
            90);


        path.CloseFigure();


        return path;
    }
}