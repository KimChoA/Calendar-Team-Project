using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace calendar4.Services
{
    internal class CalendarDbRepository
    {
        private readonly DBConnection dbConnection = new();

        public int Add(int userId, int calendarId, CalendarScheduleEntry schedule, DateTime date)
        {
            using var connection = dbConnection.GetConnection();
            connection.Open();

            const string sql = @"
        INSERT INTO user_cal
        (user_id, calendar_id, title, start_date, end_date,
         category_id, color, important, notification,
         repeat_type, repeat_interval, repeat_end_date, repeat_group_id)
        VALUES
        (@user_id, @calendar_id, @title, @start_date, @end_date,
         @category_id, @color, @important, @notification,
         @repeat_type, @repeat_interval, @repeat_end_date, @repeat_group_id)";

            using var command = new MySqlCommand(sql, connection);

            DateTime startDate =
                (schedule.StartDate != default
                    ? schedule.StartDate.Date
                    : date.Date)
                .AddHours(
                    schedule.StartHour);

            DateTime endDate =
                (schedule.EndDate != default
                    ? schedule.EndDate.Date
                    : date.Date)
                .AddHours(
                    schedule.EndHour);

            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@calendar_id", calendarId);
            command.Parameters.AddWithValue("@title", schedule.Text);
            command.Parameters.AddWithValue("@start_date", startDate);
            command.Parameters.AddWithValue("@end_date", endDate);

            command.Parameters.AddWithValue(
                "@category_id",
                schedule.CategoryId);

            command.Parameters.AddWithValue(
                "@color",
                schedule.CustomColorArgb.HasValue
                    ? schedule.CustomColorArgb.Value
                    : DBNull.Value);

            command.Parameters.AddWithValue(
                "@important",
                schedule.IsHighPriority);

            command.Parameters.AddWithValue(
                "@notification",
                schedule.NotificationOffset);

            command.Parameters.AddWithValue(
                "@repeat_type",
                string.IsNullOrWhiteSpace(schedule.RepeatType)
                    ? "NONE"
                    : schedule.RepeatType);

            command.Parameters.AddWithValue(
                "@repeat_interval",
                schedule.RepeatInterval <= 0
                    ? 1
                    : schedule.RepeatInterval);

            command.Parameters.AddWithValue(
                "@repeat_end_date",
                schedule.RepeatEndDate.HasValue
                    ? schedule.RepeatEndDate.Value.Date
                    : DBNull.Value);

            command.Parameters.AddWithValue(
                "@repeat_group_id",
                string.IsNullOrWhiteSpace(schedule.RepeatGroupId)
                    ? DBNull.Value
                    : schedule.RepeatGroupId);

            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        public void Update(int userId, int calendarId, CalendarScheduleEntry schedule, DateTime date)
        {
            if (schedule.CalId is null)
                return;

            using var connection = dbConnection.GetConnection();
            connection.Open();

            const string sql = @"
                UPDATE user_cal
                SET title = @title,
                    start_date = @start_date,
                    end_date = @end_date,
                    category_id = @category_id,
                    color = @color,
                    important = @important,
                    notification = @notification,
                    repeat_type = @repeat_type,
                    repeat_interval = @repeat_interval,
                    repeat_end_date = @repeat_end_date,
                    repeat_group_id = @repeat_group_id
                WHERE cal_id = @cal_id AND calendar_id = @calendar_id AND user_id = @user_id";

            using var command = new MySqlCommand(sql, connection);

            DateTime startDate =
                (schedule.StartDate != default
                    ? schedule.StartDate.Date
                    : date.Date)
                .AddHours(
                    schedule.StartHour);

            DateTime endDate =
                (schedule.EndDate != default
                    ? schedule.EndDate.Date
                    : date.Date)
                .AddHours(
                    schedule.EndHour);

            command.Parameters.AddWithValue(
                "@cal_id",
                schedule.CalId.Value);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);
            command.Parameters.AddWithValue(
                "@calendar_id",
                calendarId);

            command.Parameters.AddWithValue(
                "@title",
                schedule.Text);

            command.Parameters.AddWithValue(
                "@start_date",
                startDate);

            command.Parameters.AddWithValue(
                "@end_date",
                endDate);

            command.Parameters.AddWithValue(
                "@category_id",
                schedule.CategoryId);

            command.Parameters.AddWithValue(
                "@color",
                schedule.CustomColorArgb.HasValue
                    ? schedule.CustomColorArgb.Value
                    : DBNull.Value);

            command.Parameters.AddWithValue(
                "@important",
                schedule.IsHighPriority);

            command.Parameters.AddWithValue(
                "@notification",
                schedule.NotificationOffset);

            command.Parameters.AddWithValue(
                "@repeat_type",
                string.IsNullOrWhiteSpace(schedule.RepeatType)
                    ? "NONE"
                    : schedule.RepeatType);

            command.Parameters.AddWithValue(
                "@repeat_interval",
                schedule.RepeatInterval <= 0
                    ? 1
                    : schedule.RepeatInterval);

            command.Parameters.AddWithValue(
                "@repeat_end_date",
                schedule.RepeatEndDate.HasValue
                    ? schedule.RepeatEndDate.Value.Date
                    : DBNull.Value);

            command.Parameters.AddWithValue(
                "@repeat_group_id",
                string.IsNullOrWhiteSpace(schedule.RepeatGroupId)
                    ? DBNull.Value
                    : schedule.RepeatGroupId);

            command.ExecuteNonQuery();
        }


        public void Delete(int userId, int calendarId, CalendarScheduleEntry schedule)
        {
            if (schedule.CalId is null)
                return;

            using var connection = dbConnection.GetConnection();
            connection.Open();

            const string sql = @"
                DELETE FROM user_cal
                WHERE cal_id = @cal_id
                  AND user_id = @user_id AND calendar_id = @calendar_id";

            using var command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@cal_id",
                schedule.CalId.Value);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            command.Parameters.AddWithValue(
                "@calendar_id",
                calendarId);

            command.ExecuteNonQuery();
        }



        // ============================================================
        // 같은 반복 그룹의 개인 일정 전체 삭제
        // ============================================================
        public void DeleteRepeatGroup(
            int userId,
            int calendarId,
            string repeatGroupId)
        {
            if (string.IsNullOrWhiteSpace(repeatGroupId))
                return;

            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                DELETE FROM user_cal
                WHERE user_id = @user_id
                  AND calendar_id = @calendar_id
                  AND repeat_group_id = @repeat_group_id";

            using var command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            command.Parameters.AddWithValue(
                "@calendar_id",
                calendarId);

            command.Parameters.AddWithValue(
                "@repeat_group_id",
                repeatGroupId);

            command.ExecuteNonQuery();
        }


        public Dictionary<DateTime, List<CalendarScheduleEntry>> Load(
            int userId, int calendarId)
        {
            var scheduleMap =
                new Dictionary<DateTime, List<CalendarScheduleEntry>>();

            using var connection = dbConnection.GetConnection();
            connection.Open();

            const string sql = @"
                SELECT
                    cal_id,
                    title,
                    start_date,
                    end_date,
                    category_id,
                    color,
                    important,
                    notification,
                    repeat_type,
                    repeat_interval,
                    repeat_end_date,
                    repeat_group_id
                FROM user_cal
                WHERE user_id = @user_id AND calendar_id = @calendar_id
                ORDER BY start_date";

            using var command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            command.Parameters.AddWithValue(
                "@calendar_id",
                calendarId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int calId =
                    reader.GetInt32("cal_id");

                string title =
                    reader.GetString("title");

                DateTime startDate =
                    reader.GetDateTime("start_date");

                DateTime endDate =
                    reader.GetDateTime("end_date");

                DateTime date =
                    startDate.Date;

                var schedule = new CalendarScheduleEntry
                {
                    CalId = calId,
                    Text = title,

                    StartDate =
                        startDate.Date,

                    EndDate =
                        endDate.Date,

                    StartHour = startDate.Hour,
                    EndHour = endDate.Hour,

                    CategoryId =
                        reader.GetString("category_id"),

                    CustomColorArgb =
                        reader.IsDBNull(
                            reader.GetOrdinal("color"))
                        ? null
                        : reader.GetInt32("color"),

                    IsHighPriority =
                        reader.GetBoolean("important"),

                    NotificationOffset =
                        reader.GetInt32("notification"),

                    RepeatType =
                        reader.IsDBNull(reader.GetOrdinal("repeat_type"))
                            ? "NONE"
                            : reader.GetString("repeat_type"),

                    RepeatInterval =
                        reader.IsDBNull(reader.GetOrdinal("repeat_interval"))
                            ? 1
                            : reader.GetInt32("repeat_interval"),

                    RepeatEndDate =
                        reader.IsDBNull(reader.GetOrdinal("repeat_end_date"))
                            ? null
                            : reader.GetDateTime("repeat_end_date"),

                    RepeatGroupId =
                        reader.IsDBNull(reader.GetOrdinal("repeat_group_id"))
                            ? null
                            : reader.GetString("repeat_group_id")
                };

                // 시작일부터 종료일까지 모든 날짜에 표시
                DateTime displayDate =
                    startDate.Date;

                DateTime lastDate =
                    endDate.Date;

                while (displayDate <= lastDate)
                {
                    if (!scheduleMap.ContainsKey(
                            displayDate))
                    {
                        scheduleMap[displayDate] =
                            new List<CalendarScheduleEntry>();
                    }

                    scheduleMap[displayDate].Add(
                        schedule);

                    displayDate =
                        displayDate.AddDays(1);
                }
            }

            return scheduleMap;
        }
        public Dictionary<DateTime, string> LoadDdays(int userId)
        {
            var ddayMap = new Dictionary<DateTime, string>();

            using var connection = dbConnection.GetConnection();
            connection.Open();

            const string sql = @"
        SELECT date, content
        FROM user_dday
        WHERE user_id = @user_id
        ORDER BY date";

            using var command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            using var reader =
                command.ExecuteReader();

            while (reader.Read())
            {
                DateTime date =
                    reader.GetDateTime("date");

                string content =
                    reader.GetString("content");

                ddayMap[date.Date] =
                    content;
            }

            return ddayMap;
        }

        public void DeleteDday(
            int userId,
            DateTime date)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        DELETE FROM user_dday
        WHERE user_id = @user_id
          AND date = @date";

            using var command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            command.Parameters.AddWithValue(
                "@date",
                date.Date);

            command.ExecuteNonQuery();
        }
        // 새로운 개인 캘린더 공간 생성
        public int CreateCalendar(
            int userId,
            string calendarName)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        INSERT INTO user_calendar
        (
            user_id,
            calendar_name
        )
        VALUES
        (
            @user_id,
            @calendar_name
        )";

            using var command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            command.Parameters.AddWithValue(
                "@calendar_name",
                calendarName);

            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }
        // 사용자의 가장 첫 번째 개인 캘린더 ID 가져오기
        public int? GetFirstCalendarId(int userId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        SELECT calendar_id
        FROM user_calendar
        WHERE user_id = @user_id
        ORDER BY calendar_id
        LIMIT 1";

            using var command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            object? result =
                command.ExecuteScalar();

            if (result == null ||
                result == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(result);
        }
    }
}