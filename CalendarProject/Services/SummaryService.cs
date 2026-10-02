using System.Text;

namespace calendar4;

public sealed class SummaryService
{
    public string CreateDiarySummary(IReadOnlyDictionary<string, DiaryEntry> diaries)
    {
        var text = new StringBuilder("[다이어리 목록]\n\n");
        foreach (var entry in diaries.OrderBy(item => item.Key).Select(item => item.Value))
            text.AppendLine($"• {entry.DateStr} : {entry.Title}");
        return text.ToString();
    }

    public string CreateCalendarSummary(
    IReadOnlyDictionary<
        DateTime,
        List<CalendarScheduleEntry>> schedules,
    bool showCategory = true)
    {
        var text =
            new StringBuilder(
                "[일정 요약]\n\n");


        // ============================================================
        // 공유 캘린더
        // 같은 일정이 여러 날짜에 들어있어도 한 번만 표시
        // ============================================================

        if (!showCategory)
        {
            var uniqueSchedules =
                schedules
                    .SelectMany(
                        pair =>
                            pair.Value)
                    .GroupBy(
                        entry =>
                            entry.CalId.HasValue
                                ? $"DB_{entry.CalId.Value}"
                                : $"ID_{entry.Id}")
                    .Select(
                        group =>
                            group.First())
                    .OrderBy(
                        entry =>
                            entry.StartDate)
                    .ThenBy(
                        entry =>
                            entry.StartHour)
                    .ToList();


            foreach (
                var entry
                in uniqueSchedules)
            {
                DateTime startDate =
                    entry.StartDate != default
                        ? entry.StartDate.Date
                        : DateTime.Today;


                DateTime endDate =
                    entry.EndDate != default
                        ? entry.EndDate.Date
                        : startDate;


                // ========================================================
                // 하루 일정
                // ========================================================

                if (startDate ==
                    endDate)
                {
                    text.AppendLine(
                        $"• {startDate:yy-MM-dd} " +
                        $"({entry.StartHour:00}:00~{entry.EndHour:00}:00)");
                }

                // ========================================================
                // 2일 이상 일정
                // ========================================================

                else
                {
                    text.AppendLine(
                        $"• {startDate:yy-MM-dd} ~ {endDate:yy-MM-dd} " +
                        $"({entry.StartHour:00}:00~{entry.EndHour:00}:00)");
                }


                text.AppendLine(
                    $"  {entry.Text}");


                text.AppendLine();
            }


            return text.ToString();
        }


        // ============================================================
        // 개인 캘린더
        // 기존 방식 유지
        // ============================================================

        foreach (
            var dateSchedules
            in schedules.OrderBy(
                item =>
                    item.Key))
        {
            foreach (
                var entry
                in dateSchedules.Value
                    .OrderBy(
                        item =>
                            item.StartHour)
                    .ThenBy(
                        item =>
                            item.EndHour))
            {
                text.AppendLine(
                    $"• {dateSchedules.Key:yy-MM-dd} " +
                    $"({entry.StartHour:00}:00~{entry.EndHour:00}:00)");


                string categoryName =
                    PersonalCategoryStores.Calendar
                        .Get(
                            entry.CategoryId)
                        .Name;


                text.AppendLine(
                    $"  [{categoryName}] {entry.Text}");


                text.AppendLine();
            }
        }


        return text.ToString();
    }
}
