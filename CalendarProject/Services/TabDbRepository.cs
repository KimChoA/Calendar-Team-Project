using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace calendar4.Services
{
    internal class TabDbRepository
    {
        private readonly DBConnection dbConnection = new();


        // ============================================================
        // 로그인한 사용자의 탭 목록 불러오기
        // ============================================================

        public List<TabData> Load(int userId)
        {
            var tabs =
                new List<TabData>();


            using var connection =
                dbConnection.GetConnection();

            connection.Open();


            const string sql = @"
                SELECT
                    title,
                    tab_type,
                    calendar_id,
                    shared_calendar_id

                FROM user_tab

                WHERE user_id = @user_id

                ORDER BY
                    sort_order,
                    tab_id";


            using var command =
                new MySqlCommand(
                    sql,
                    connection);


            command.Parameters.AddWithValue(
                "@user_id",
                userId);


            using var reader =
                command.ExecuteReader();


            while (reader.Read())
            {
                tabs.Add(
                    new TabData
                    {
                        Title =
                            reader.GetString(
                                "title"),


                        Type =
                            (mainForm.TabType)
                            reader.GetInt32(
                                "tab_type"),


                        // --------------------------------------------
                        // 개인 캘린더 ID
                        // --------------------------------------------

                        CalendarId =
                            reader.IsDBNull(
                                reader.GetOrdinal(
                                    "calendar_id"))
                                ? null
                                : reader.GetInt32(
                                    "calendar_id"),


                        // --------------------------------------------
                        // ★ 공유 캘린더 ID
                        // --------------------------------------------

                        SharedCalendarId =
                            reader.IsDBNull(
                                reader.GetOrdinal(
                                    "shared_calendar_id"))
                                ? null
                                : reader.GetInt32(
                                    "shared_calendar_id")
                    });
            }


            return tabs;
        }


        // ============================================================
        // 현재 탭 전체 저장
        // ============================================================

        public void Save(
            int userId,
            IReadOnlyList<TabData> tabs)
        {
            using var connection =
                dbConnection.GetConnection();

            connection.Open();


            using var transaction =
                connection.BeginTransaction();


            try
            {
                // ====================================================
                // 1. 기존 탭 구성 삭제
                // ====================================================

                const string deleteSql = @"
                    DELETE FROM user_tab
                    WHERE user_id = @user_id";


                using (var command =
                    new MySqlCommand(
                        deleteSql,
                        connection,
                        transaction))
                {
                    command.Parameters.AddWithValue(
                        "@user_id",
                        userId);


                    command.ExecuteNonQuery();
                }


                // ====================================================
                // 2. 현재 탭 구성 다시 저장
                // ====================================================

                const string insertSql = @"
                    INSERT INTO user_tab
                    (
                        user_id,
                        title,
                        tab_type,
                        calendar_id,
                        shared_calendar_id,
                        sort_order
                    )
                    VALUES
                    (
                        @user_id,
                        @title,
                        @tab_type,
                        @calendar_id,
                        @shared_calendar_id,
                        @sort_order
                    )";


                for (int i = 0; i < tabs.Count; i++)
                {
                    TabData tab =
                        tabs[i];


                    using var command =
                        new MySqlCommand(
                            insertSql,
                            connection,
                            transaction);


                    command.Parameters.AddWithValue(
                        "@user_id",
                        userId);


                    command.Parameters.AddWithValue(
                        "@title",
                        tab.Title);


                    command.Parameters.AddWithValue(
                        "@tab_type",
                        (int)tab.Type);


                    // -----------------------------------------------
                    // 개인 캘린더
                    // -----------------------------------------------

                    command.Parameters.AddWithValue(
                        "@calendar_id",
                        tab.CalendarId.HasValue
                            ? tab.CalendarId.Value
                            : DBNull.Value);


                    // -----------------------------------------------
                    // ★ 공유 캘린더
                    // -----------------------------------------------

                    command.Parameters.AddWithValue(
                        "@shared_calendar_id",
                        tab.SharedCalendarId.HasValue
                            ? tab.SharedCalendarId.Value
                            : DBNull.Value);


                    command.Parameters.AddWithValue(
                        "@sort_order",
                        i);


                    command.ExecuteNonQuery();
                }


                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

                throw;
            }
        }
    }
}