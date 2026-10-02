namespace calendar4;

public sealed class CalendarScheduleEntry
{
    // 프로그램 내부에서 사용하는 ID
    public Guid Id { get; set; } = Guid.NewGuid();

    // 개인 캘린더에서는 user_cal.cal_id
    // 공유 캘린더에서는 shared_calendar_schedule.schedule_id
    public int? CalId { get; set; }

    public string Text { get; set; } = string.Empty;

    // ============================================================
    // 일정 기간
    // ============================================================

    // 일정 시작 날짜
    public DateTime StartDate { get; set; }

    // 일정 종료 날짜
    public DateTime EndDate { get; set; }

    public int StartHour { get; set; } = 9;

    public int EndHour { get; set; } = 10;

    public string CategoryId { get; set; } =
        UserCategoryStore.HomeId;

    public int? CustomColorArgb { get; set; }

    public bool IsHighPriority { get; set; }

    public int NotificationOffset { get; set; }


    // ============================================================
    // 반복 일정 정보
    // ============================================================

    // NONE / DAILY / WEEKLY / MONTHLY / YEARLY
    public string RepeatType { get; set; } = "NONE";

    public int RepeatInterval { get; set; } = 1;

    public DateTime? RepeatEndDate { get; set; }

    public string? RepeatGroupId { get; set; }


    // ============================================================
    // 공유 캘린더 멤버 지정 관련
    // ============================================================

    public bool IsAllMembers { get; set; } = true;

    public List<int> MemberUserIds { get; set; }
        = new List<int>();

    public List<string> MemberNames { get; set; }
        = new List<string>();

    public List<int> MemberColorIndexes { get; set; }
        = new List<int>();

    public string? WriterName { get; set; }


    // ============================================================
    // 복사
    // ============================================================

    public CalendarScheduleEntry Copy() => new()
    {
        Id = Id,

        CalId = CalId,

        Text = Text,

        StartDate = StartDate,

        EndDate = EndDate,

        StartHour = StartHour,

        EndHour = EndHour,

        CategoryId = CategoryId,

        CustomColorArgb = CustomColorArgb,

        IsHighPriority = IsHighPriority,

        NotificationOffset = NotificationOffset,

        WriterName = WriterName,

        RepeatType = RepeatType,

        RepeatInterval = RepeatInterval,

        RepeatEndDate = RepeatEndDate,

        RepeatGroupId = RepeatGroupId,

        IsAllMembers = IsAllMembers,

        MemberUserIds =
            new List<int>(
                MemberUserIds),

        MemberNames =
            new List<string>(
                MemberNames),

        MemberColorIndexes =
            new List<int>(
                MemberColorIndexes)
    };
}
