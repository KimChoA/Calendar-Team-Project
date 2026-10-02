namespace calendar4;

public sealed class TabData
{
    public string Title { get; set; } = string.Empty;

    public mainForm.TabType Type { get; set; }


    // ============================================================
    // 개인 캘린더 ID
    // ============================================================

    public int? CalendarId { get; set; }


    // ============================================================
    // ★ 공유 캘린더 ID
    //
    // 아직 공유방에 들어가지 않은 탭
    // → null
    //
    // 공유방에 들어간 탭
    // → shared_calendar.shared_calendar_id
    // ============================================================

    public int? SharedCalendarId { get; set; }
}