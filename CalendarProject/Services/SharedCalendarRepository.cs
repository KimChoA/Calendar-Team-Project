using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using calendar4.Services;

namespace calendar4.Services
{
    internal class SharedCalendarRepository
    {
        private readonly DBConnection dbConnection = new();


        // ============================================================
        // 참가 코드로 공유 캘린더 찾기
        // ============================================================

        public int? FindRoomByCode(
            string roomCode)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                SELECT shared_calendar_id
                FROM shared_calendar
                WHERE room_code = @room_code
                LIMIT 1";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@room_code",
                roomCode);

            object? result =
                command.ExecuteScalar();

            if (result == null ||
                result == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(result);
        }


        // ============================================================
        // 참가 코드 중복 확인
        // ============================================================

        public bool RoomCodeExists(
            string roomCode)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                SELECT COUNT(*)
                FROM shared_calendar
                WHERE room_code = @room_code";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@room_code",
                roomCode);

            int count =
                Convert.ToInt32(
                    command.ExecuteScalar());

            return count > 0;
        }
        // ============================================================
        // 공유 캘린더 완전 삭제
        // 방장만 호출하도록 UI에서도 검사
        // ============================================================

        public void DeleteSharedCalendar(
            int sharedCalendarId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();


            using var transaction =
                connection.BeginTransaction();


            try
            {
                // ========================================================
                // 1. 일정별 참여자 삭제
                // ========================================================

                const string deleteScheduleMembersSql = @"
            DELETE scm
            FROM shared_calendar_schedule_member scm

            INNER JOIN shared_calendar_schedule s
                ON scm.schedule_id = s.schedule_id

            WHERE s.shared_calendar_id = @shared_calendar_id";


                using (
                    var command =
                        new MySqlCommand(
                            deleteScheduleMembersSql,
                            connection,
                            transaction))
                {
                    command.Parameters.AddWithValue(
                        "@shared_calendar_id",
                        sharedCalendarId);

                    command.ExecuteNonQuery();
                }


                // ========================================================
                // 2. 공유 일정 삭제
                // ========================================================

                const string deleteSchedulesSql = @"
            DELETE FROM shared_calendar_schedule
            WHERE shared_calendar_id = @shared_calendar_id";


                using (
                    var command =
                        new MySqlCommand(
                            deleteSchedulesSql,
                            connection,
                            transaction))
                {
                    command.Parameters.AddWithValue(
                        "@shared_calendar_id",
                        sharedCalendarId);

                    command.ExecuteNonQuery();
                }


                // ========================================================
                // 3. 채팅 기록 삭제
                // ========================================================

                const string deleteChatSql = @"
            DELETE FROM shared_calendar_chat
            WHERE shared_calendar_id = @shared_calendar_id";


                using (
                    var command =
                        new MySqlCommand(
                            deleteChatSql,
                            connection,
                            transaction))
                {
                    command.Parameters.AddWithValue(
                        "@shared_calendar_id",
                        sharedCalendarId);

                    command.ExecuteNonQuery();
                }


                // ========================================================
                // 4. 멤버 삭제
                // ========================================================

                const string deleteMembersSql = @"
            DELETE FROM shared_calendar_member
            WHERE shared_calendar_id = @shared_calendar_id";


                using (
                    var command =
                        new MySqlCommand(
                            deleteMembersSql,
                            connection,
                            transaction))
                {
                    command.Parameters.AddWithValue(
                        "@shared_calendar_id",
                        sharedCalendarId);

                    command.ExecuteNonQuery();
                }


                // ========================================================
                // 5. 공유 캘린더 자체 삭제
                // ========================================================

                const string deleteCalendarSql = @"
            DELETE FROM shared_calendar
            WHERE shared_calendar_id = @shared_calendar_id";


                using (
                    var command =
                        new MySqlCommand(
                            deleteCalendarSql,
                            connection,
                            transaction))
                {
                    command.Parameters.AddWithValue(
                        "@shared_calendar_id",
                        sharedCalendarId);

                    command.ExecuteNonQuery();
                }


                // 모두 성공했을 때만 실제 반영
                transaction.Commit();
            }
            catch
            {
                // 하나라도 실패하면 전부 원상복구
                transaction.Rollback();

                throw;
            }
        }

        // ============================================================
        // 공유 캘린더 방 생성
        //
        // 1. shared_calendar에 방 생성
        // 2. 방 만든 사용자도 멤버로 자동 등록
        // ============================================================

        public int CreateRoom(
            int ownerUserId,
            string roomName,
            string roomCode)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            using var transaction =
                connection.BeginTransaction();

            try
            {
                // ----------------------------------------------------
                // 공유방 생성
                // ----------------------------------------------------

                const string roomSql = @"
                    INSERT INTO shared_calendar
                    (
                        owner_user_id,
                        room_name,
                        room_code
                    )
                    VALUES
                    (
                        @owner_user_id,
                        @room_name,
                        @room_code
                    )";

                using var roomCommand =
                    new MySqlCommand(
                        roomSql,
                        connection,
                        transaction);

                roomCommand.Parameters.AddWithValue(
                    "@owner_user_id",
                    ownerUserId);

                roomCommand.Parameters.AddWithValue(
                    "@room_name",
                    roomName);

                roomCommand.Parameters.AddWithValue(
                    "@room_code",
                    roomCode);

                roomCommand.ExecuteNonQuery();

                int sharedCalendarId =
                    Convert.ToInt32(
                        roomCommand.LastInsertedId);


                // ----------------------------------------------------
                // 방 만든 사람도 멤버로 등록
                // ----------------------------------------------------

                const string memberSql = @"
                    INSERT INTO shared_calendar_member
                    (
                        shared_calendar_id,
                        user_id,
                        color_index
                    )
                    VALUES
                    (
                        @shared_calendar_id,
                        @user_id,
                        1
                    )";

                using var memberCommand =
                    new MySqlCommand(
                        memberSql,
                        connection,
                        transaction);

                memberCommand.Parameters.AddWithValue(
                    "@shared_calendar_id",
                    sharedCalendarId);

                memberCommand.Parameters.AddWithValue(
                    "@user_id",
                    ownerUserId);

                memberCommand.ExecuteNonQuery();


                transaction.Commit();

                return sharedCalendarId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        // ============================================================
        // 공유 캘린더 참가 여부 확인
        // ============================================================

        public bool IsMember(
            int sharedCalendarId,
            int userId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                SELECT COUNT(*)
                FROM shared_calendar_member
                WHERE shared_calendar_id = @shared_calendar_id
                  AND user_id = @user_id";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            int count =
                Convert.ToInt32(
                    command.ExecuteScalar());

            return count > 0;
        }


        // ============================================================
        // 공유 캘린더 참가
        // ============================================================

        // [담당 기능] 공유 캘린더 참가 시 기존 멤버와 겹치지 않는 ColorIndex를 자동 배정
        public void JoinRoom(
            int sharedCalendarId,
            int userId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            using var transaction =
                connection.BeginTransaction();

            try
            {
                // 이 공유방에서 사용 중인 가장 큰 색상 번호 다음 값 부여
                const string colorSql = @"
                    SELECT
                        COALESCE(MAX(color_index), 0) + 1
                    FROM shared_calendar_member
                    WHERE shared_calendar_id = @shared_calendar_id";

                using var colorCommand =
                    new MySqlCommand(
                        colorSql,
                        connection,
                        transaction);

                colorCommand.Parameters.AddWithValue(
                    "@shared_calendar_id",
                    sharedCalendarId);

                int nextColorIndex =
                    Convert.ToInt32(
                        colorCommand.ExecuteScalar());

                const string sql = @"
                    INSERT INTO shared_calendar_member
                    (
                        shared_calendar_id,
                        user_id,
                        color_index
                    )
                    VALUES
                    (
                        @shared_calendar_id,
                        @user_id,
                        @color_index
                    )";

                using var command =
                    new MySqlCommand(
                        sql,
                        connection,
                        transaction);

                command.Parameters.AddWithValue(
                    "@shared_calendar_id",
                    sharedCalendarId);

                command.Parameters.AddWithValue(
                    "@user_id",
                    userId);

                command.Parameters.AddWithValue(
                    "@color_index",
                    nextColorIndex);

                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        // ============================================================
        // 공유 캘린더 이름 가져오기
        // ============================================================

        public string? GetRoomName(
            int sharedCalendarId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                SELECT room_name
                FROM shared_calendar
                WHERE shared_calendar_id = @shared_calendar_id
                LIMIT 1";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            object? result =
                command.ExecuteScalar();

            if (result == null ||
                result == DBNull.Value)
            {
                return null;
            }

            return result.ToString();
        }


        // ============================================================
        // 공유 캘린더 참가 코드 가져오기
        // ============================================================

        public string? GetRoomCode(
            int sharedCalendarId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                SELECT room_code
                FROM shared_calendar
                WHERE shared_calendar_id = @shared_calendar_id
                LIMIT 1";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            object? result =
                command.ExecuteScalar();

            if (result == null ||
                result == DBNull.Value)
            {
                return null;
            }

            return result.ToString();
        }
        // ============================================================
        // 공유 캘린더 현재 멤버 수 가져오기
        // ============================================================

        public int GetMemberCount(
            int sharedCalendarId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        SELECT COUNT(*)
        FROM shared_calendar_member
        WHERE shared_calendar_id = @shared_calendar_id";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            return Convert.ToInt32(
                command.ExecuteScalar());
        }


        // ============================================================
        // 공유 캘린더 현재 멤버 이름 가져오기
        // ============================================================

        public List<string> GetMemberNames(
            int sharedCalendarId)
        {
            var memberNames =
                new List<string>();

            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        SELECT u.name
        FROM shared_calendar_member m

        INNER JOIN user u
            ON m.user_id = u.user_id

        WHERE m.shared_calendar_id = @shared_calendar_id

        ORDER BY u.name";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            using var reader =
                command.ExecuteReader();

            while (reader.Read())
            {
                string name =
                    reader.IsDBNull(
                        reader.GetOrdinal("name"))
                        ? "이름 없음"
                        : reader.GetString("name");

                memberNames.Add(
                    name);
            }

            return memberNames;
        }

        // ============================================================
        // 공유 캘린더 멤버 목록 가져오기
        // ============================================================

        // ============================================================
        // 공유 캘린더 멤버 목록 가져오기
        // ============================================================

        // ============================================================
        // 공유 캘린더 멤버 정보 가져오기
        // UserId + Name + ColorIndex
        // ============================================================

        // [담당 기능] 일정 편집/표시에 필요한 사용자 ID, 이름, ColorIndex를 함께 조회
        public List<(int UserId, string Name, int ColorIndex)>
            GetMembers(
                int sharedCalendarId)
        {
            var members =
                new List<
                    (
                        int UserId,
                        string Name,
                        int ColorIndex
                    )>();


            using var connection =
                dbConnection.GetConnection();

            connection.Open();


            const string sql = @"
        SELECT
            m.user_id,
            u.name,
            m.color_index

        FROM shared_calendar_member m

        INNER JOIN user u
            ON m.user_id = u.user_id

        WHERE m.shared_calendar_id =
            @shared_calendar_id

        ORDER BY m.color_index";


            using var command =
                new MySqlCommand(
                    sql,
                    connection);


            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);


            using var reader =
                command.ExecuteReader();


            while (reader.Read())
            {
                int userId =
                    reader.GetInt32(
                        "user_id");


                string name =
                    reader.IsDBNull(
                        reader.GetOrdinal(
                            "name"))
                        ? $"사용자 {userId}"
                        : reader.GetString(
                            "name");


                int colorIndex =
                    reader.IsDBNull(
                        reader.GetOrdinal(
                            "color_index"))
                        ? 1
                        : reader.GetInt32(
                            "color_index");


                members.Add(
                    (
                        userId,
                        name,
                        colorIndex
                    ));
            }


            return members;
        }

        // ============================================================
        // 방장 여부 확인
        // ============================================================

        public bool IsOwner(
            int sharedCalendarId,
            int userId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                SELECT COUNT(*)
                FROM shared_calendar
                WHERE shared_calendar_id = @shared_calendar_id
                  AND owner_user_id = @user_id";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            int count =
                Convert.ToInt32(
                    command.ExecuteScalar());

            return count > 0;
        }


        // ============================================================
        // ★ 공유 캘린더 일정 추가
        // ============================================================

        // ============================================================
        // ★ 공유 캘린더 일정 추가
        // 일정 본체 + 선택 멤버 같이 저장
        // ============================================================

        public int AddSchedule(
            int sharedCalendarId,
            int writerUserId,
            CalendarScheduleEntry schedule,
            DateTime date)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            using var transaction =
                connection.BeginTransaction();

            try
            {
                const string sql = @"
            INSERT INTO shared_calendar_schedule
            (
                shared_calendar_id,
                writer_user_id,
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
                repeat_group_id,
                is_all_members
            )
            VALUES
            (
                @shared_calendar_id,
                @writer_user_id,
                @title,
                @start_date,
                @end_date,
                @category_id,
                @color,
                @important,
                @notification,
                @repeat_type,
                @repeat_interval,
                @repeat_end_date,
                @repeat_group_id,
                @is_all_members
            )";

                using var command =
                    new MySqlCommand(
                        sql,
                        connection,
                        transaction);


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
                    "@shared_calendar_id",
                    sharedCalendarId);

                command.Parameters.AddWithValue(
                    "@writer_user_id",
                    writerUserId);

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
                    string.IsNullOrWhiteSpace(
                        schedule.RepeatType)
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
                    string.IsNullOrWhiteSpace(
                        schedule.RepeatGroupId)
                        ? DBNull.Value
                        : schedule.RepeatGroupId);

                command.Parameters.AddWithValue(
                    "@is_all_members",
                    schedule.IsAllMembers);


                command.ExecuteNonQuery();


                int scheduleId =
                    Convert.ToInt32(
                        command.LastInsertedId);


                // ========================================================
                // 전체 멤버 일정이 아니라면
                // 선택된 멤버들을 연결 테이블에 저장
                // ========================================================

                if (!schedule.IsAllMembers)
                {
                    foreach (
                        int memberUserId
                        in schedule.MemberUserIds.Distinct())
                    {
                        const string memberSql = @"
                    INSERT INTO shared_calendar_schedule_member
                    (
                        schedule_id,
                        user_id
                    )
                    VALUES
                    (
                        @schedule_id,
                        @user_id
                    )";

                        using var memberCommand =
                            new MySqlCommand(
                                memberSql,
                                connection,
                                transaction);

                        memberCommand.Parameters.AddWithValue(
                            "@schedule_id",
                            scheduleId);

                        memberCommand.Parameters.AddWithValue(
                            "@user_id",
                            memberUserId);

                        memberCommand.ExecuteNonQuery();
                    }
                }


                transaction.Commit();

                return scheduleId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        // ============================================================
        // ★ 공유 캘린더 일정 불러오기
        // ============================================================

        // ============================================================
        // ★ 공유 캘린더 일정 불러오기
        // 작성자 이름(user.name)까지 같이 불러옴
        // ============================================================

        // ============================================================
        // ★ 공유 캘린더 일정 불러오기
        // 작성자 + 참여자 + 색상 + 반복정보 복원
        // ============================================================

        public Dictionary<DateTime, List<CalendarScheduleEntry>>
            LoadSchedules(
                int sharedCalendarId)
        {
            var scheduleMap =
                new Dictionary<
                    DateTime,
                    List<CalendarScheduleEntry>>();


            using var connection =
                dbConnection.GetConnection();

            connection.Open();


            // ========================================================
            // 1. 일정 본체 먼저 불러오기
            // ========================================================

            const string sql = @"
            SELECT
            s.schedule_id,
            s.writer_user_id,
            u.name AS writer_name,
            s.title,
            s.start_date,
            s.end_date,
            s.category_id,
            s.color,
            s.important,
            s.notification,
            s.repeat_type,
            s.repeat_interval,
            s.repeat_end_date,
            s.repeat_group_id,
            s.is_all_members

        FROM shared_calendar_schedule s

        INNER JOIN user u
            ON s.writer_user_id = u.user_id

        WHERE s.shared_calendar_id =
            @shared_calendar_id

        ORDER BY s.start_date";


            using var command =
                new MySqlCommand(
                    sql,
                    connection);


            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);


            // 멤버 정보를 나중에 불러오기 위해 임시 저장
            var loadedSchedules =
                new List<
                    (
                        DateTime Date,
                        CalendarScheduleEntry Schedule
                    )>();


            using (var reader =
                command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int scheduleId =
                        reader.GetInt32(
                            "schedule_id");


                    DateTime startDate =
                        reader.GetDateTime(
                            "start_date");


                    DateTime endDate =
                        reader.GetDateTime(
                            "end_date");


                    DateTime date =
                        startDate.Date;


                    var schedule =
                        new CalendarScheduleEntry
                        {
                            CalId =
                                scheduleId,

                            Text =
                                reader.GetString(
                                    "title"),

                            StartDate =
                                startDate.Date,

                            EndDate =
                                endDate.Date,

                            WriterName =
                                reader.IsDBNull(
                                    reader.GetOrdinal(
                                        "writer_name"))
                                    ? "알 수 없음"
                                    : reader.GetString(
                                        "writer_name"),

                            StartHour =
                                startDate.Hour,

                            EndHour =
                                endDate.Hour,

                            CategoryId =
                                reader.IsDBNull(
                                    reader.GetOrdinal(
                                        "category_id"))
                                    ? UserCategoryStore.HomeId
                                    : reader.GetString(
                                        "category_id"),

                            CustomColorArgb =
                                reader.IsDBNull(
                                    reader.GetOrdinal(
                                        "color"))
                                    ? null
                                    : reader.GetInt32(
                                        "color"),

                            IsHighPriority =
                                reader.GetBoolean(
                                    "important"),

                            NotificationOffset =
                                reader.GetInt32(
                                    "notification"),

                            RepeatType =
                                reader.IsDBNull(
                                    reader.GetOrdinal(
                                        "repeat_type"))
                                    ? "NONE"
                                    : reader.GetString(
                                        "repeat_type"),

                            RepeatInterval =
                                reader.IsDBNull(
                                    reader.GetOrdinal(
                                        "repeat_interval"))
                                    ? 1
                                    : reader.GetInt32(
                                        "repeat_interval"),

                            RepeatEndDate =
                                reader.IsDBNull(
                                    reader.GetOrdinal(
                                        "repeat_end_date"))
                                    ? null
                                    : reader.GetDateTime(
                                        "repeat_end_date"),

                            RepeatGroupId =
                                reader.IsDBNull(
                                    reader.GetOrdinal(
                                        "repeat_group_id"))
                                    ? null
                                    : reader.GetString(
                                        "repeat_group_id"),

                            IsAllMembers =
                                reader.GetBoolean(
                                    "is_all_members")
                        };


                    loadedSchedules.Add(
                        (
                            date,
                            schedule
                        ));
                }
            }


            // ========================================================
            // 2. 각 일정의 참여자 + color_index 불러오기
            // ========================================================

            foreach (
                var item
                in loadedSchedules)
            {
                CalendarScheduleEntry schedule =
                    item.Schedule;


                // --------------------------------------------------------
                // 전체 멤버 일정
                //
                // MemberUserIds를 굳이 채우지 않아도
                // IsAllMembers = true로 구분 가능
                // --------------------------------------------------------

                if (!schedule.IsAllMembers &&
                    schedule.CalId.HasValue)
                {
                    const string memberSql = @"
                SELECT
                    sm.user_id,
                    u.name,
                    m.color_index

                FROM shared_calendar_schedule_member sm

                INNER JOIN user u
                    ON sm.user_id = u.user_id

                INNER JOIN shared_calendar_member m
                    ON m.shared_calendar_id =
                        @shared_calendar_id
                   AND m.user_id =
                        sm.user_id

                WHERE sm.schedule_id =
                    @schedule_id

                ORDER BY m.color_index";


                    using var memberCommand =
                        new MySqlCommand(
                            memberSql,
                            connection);


                    memberCommand.Parameters.AddWithValue(
                        "@shared_calendar_id",
                        sharedCalendarId);

                    memberCommand.Parameters.AddWithValue(
                        "@schedule_id",
                        schedule.CalId.Value);


                    using var memberReader =
                        memberCommand.ExecuteReader();


                    while (memberReader.Read())
                    {
                        int userId =
                            memberReader.GetInt32(
                                "user_id");


                        string memberName =
                            memberReader.IsDBNull(
                                memberReader.GetOrdinal(
                                    "name"))
                                ? $"사용자 {userId}"
                                : memberReader.GetString(
                                    "name");


                        int colorIndex =
                            memberReader.IsDBNull(
                                memberReader.GetOrdinal(
                                    "color_index"))
                                ? 1
                                : memberReader.GetInt32(
                                    "color_index");


                        schedule.MemberUserIds.Add(
                            userId);

                        schedule.MemberNames.Add(
                            memberName);

                        schedule.MemberColorIndexes.Add(
                            colorIndex);
                    }
                }


                // ========================================================
                // scheduleMap에 최종 추가
                // ========================================================

                DateTime displayDate =
                    schedule.StartDate != default
                        ? schedule.StartDate.Date
                        : item.Date;

                DateTime lastDate =
                    schedule.EndDate != default
                        ? schedule.EndDate.Date
                        : item.Date;


                while (displayDate <= lastDate)
                {
                    if (!scheduleMap.ContainsKey(
                            displayDate))
                    {
                        scheduleMap[displayDate] =
                            new List<
                                CalendarScheduleEntry>();
                    }


                    scheduleMap[displayDate].Add(
                        schedule);


                    displayDate =
                        displayDate.AddDays(1);
                }
            }


            return scheduleMap;
        }


        // ============================================================
        // ★ 공유 캘린더 일정 수정
        // ============================================================

        // ============================================================
        // ★ 공유 캘린더 일정 수정
        // 일정 본체 + 참여자 목록 갱신
        // ============================================================

        public void UpdateSchedule(
            int sharedCalendarId,
            int writerUserId,
            CalendarScheduleEntry schedule,
            DateTime date)
        {
            if (schedule.CalId is null)
                return;


            using var connection =
                dbConnection.GetConnection();

            connection.Open();


            using var transaction =
                connection.BeginTransaction();


            try
            {
                const string sql = @"
            UPDATE shared_calendar_schedule
            SET
                title = @title,
                start_date = @start_date,
                end_date = @end_date,
                category_id = @category_id,
                color = @color,
                important = @important,
                notification = @notification,
                repeat_type = @repeat_type,
                repeat_interval = @repeat_interval,
                repeat_end_date = @repeat_end_date,
                repeat_group_id = @repeat_group_id,
                is_all_members = @is_all_members

            WHERE schedule_id =
                @schedule_id

              AND shared_calendar_id =
                @shared_calendar_id";


                using var command =
                    new MySqlCommand(
                        sql,
                        connection,
                        transaction);


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
                    "@schedule_id",
                    schedule.CalId.Value);

                command.Parameters.AddWithValue(
                    "@shared_calendar_id",
                    sharedCalendarId);

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
                    string.IsNullOrWhiteSpace(
                        schedule.RepeatType)
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
                    string.IsNullOrWhiteSpace(
                        schedule.RepeatGroupId)
                        ? DBNull.Value
                        : schedule.RepeatGroupId);

                command.Parameters.AddWithValue(
                    "@is_all_members",
                    schedule.IsAllMembers);


                command.ExecuteNonQuery();


                // ========================================================
                // 기존 선택 멤버 연결 삭제
                // ========================================================

                const string deleteMemberSql = @"
            DELETE FROM shared_calendar_schedule_member
            WHERE schedule_id =
                @schedule_id";


                using var deleteMemberCommand =
                    new MySqlCommand(
                        deleteMemberSql,
                        connection,
                        transaction);


                deleteMemberCommand.Parameters.AddWithValue(
                    "@schedule_id",
                    schedule.CalId.Value);


                deleteMemberCommand.ExecuteNonQuery();


                // ========================================================
                // 수정된 멤버 목록 다시 저장
                // ========================================================

                if (!schedule.IsAllMembers)
                {
                    foreach (
                        int memberUserId
                        in schedule.MemberUserIds.Distinct())
                    {
                        const string insertMemberSql = @"
                    INSERT INTO shared_calendar_schedule_member
                    (
                        schedule_id,
                        user_id
                    )
                    VALUES
                    (
                        @schedule_id,
                        @user_id
                    )";


                        using var insertMemberCommand =
                            new MySqlCommand(
                                insertMemberSql,
                                connection,
                                transaction);


                        insertMemberCommand.Parameters.AddWithValue(
                            "@schedule_id",
                            schedule.CalId.Value);

                        insertMemberCommand.Parameters.AddWithValue(
                            "@user_id",
                            memberUserId);


                        insertMemberCommand.ExecuteNonQuery();
                    }
                }


                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        // ============================================================
        // ★ 공유 캘린더 일정 삭제
        // ============================================================

        public void DeleteSchedule(
            int sharedCalendarId,
            CalendarScheduleEntry schedule)
        {
            if (schedule.CalId is null)
                return;


            using var connection =
                dbConnection.GetConnection();

            connection.Open();


            const string sql = @"
                DELETE FROM shared_calendar_schedule
                WHERE schedule_id = @schedule_id
                  AND shared_calendar_id = @shared_calendar_id";


            using var command =
                new MySqlCommand(
                    sql,
                    connection);


            command.Parameters.AddWithValue(
                "@schedule_id",
                schedule.CalId.Value);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);


            command.ExecuteNonQuery();
        }

        // ============================================================
        // 같은 반복 그룹의 공유 일정 전체 삭제
        // ============================================================
        public void DeleteRepeatGroup(
            int sharedCalendarId,
            string repeatGroupId)
        {
            if (string.IsNullOrWhiteSpace(repeatGroupId))
                return;

            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
                DELETE FROM shared_calendar_schedule
                WHERE shared_calendar_id = @shared_calendar_id
                  AND repeat_group_id = @repeat_group_id";

            using var command =
                new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            command.Parameters.AddWithValue(
                "@repeat_group_id",
                repeatGroupId);

            command.ExecuteNonQuery();
        }
        // ============================================================
        // 공유 캘린더 멤버 내보내기
        // ============================================================

        public void RemoveMember(
            int sharedCalendarId,
            int userId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        DELETE FROM shared_calendar_member
        WHERE shared_calendar_id = @shared_calendar_id
          AND user_id = @user_id";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            command.Parameters.AddWithValue(
                "@user_id",
                userId);

            command.ExecuteNonQuery();
        }
        // ============================================================
        // 공유 캘린더 수정 권한 변경
        // true  = 관리자만 수정 가능
        // false = 모든 멤버 수정 가능
        // ============================================================

        public void SetOwnerOnlyEdit(
            int sharedCalendarId,
            bool ownerOnly)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        UPDATE shared_calendar
        SET owner_only_edit = @owner_only_edit
        WHERE shared_calendar_id = @shared_calendar_id";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@owner_only_edit",
                ownerOnly);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            command.ExecuteNonQuery();
        }
        // ============================================================
        // 현재 공유 캘린더 수정 권한
        // ============================================================

        public bool GetOwnerOnlyEdit(
            int sharedCalendarId)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();

            const string sql = @"
        SELECT owner_only_edit
        FROM shared_calendar
        WHERE shared_calendar_id = @shared_calendar_id
        LIMIT 1";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@shared_calendar_id",
                sharedCalendarId);

            object? result =
                command.ExecuteScalar();

            if (result == null ||
                result == DBNull.Value)
            {
                return false;
            }

            return Convert.ToBoolean(
                result);
        }
        public bool CanEditSchedule(
    int sharedCalendarId,
    int userId)
        {
            bool ownerOnly =
                GetOwnerOnlyEdit(
                    sharedCalendarId);

            // 모든 멤버 수정 가능
            if (!ownerOnly)
            {
                return true;
            }

            // 관리자만 수정 가능
            return IsOwner(
                sharedCalendarId,
                userId);
        }

    }
}