using MySql.Data.MySqlClient;

namespace tap // 회원가입 폼과 동일한 네임스페이스
{
    public static class DBHelper
    {
        // GitHub 공개 저장소에 DB 비밀번호가 노출되지 않도록 환경 변수 사용
        private static string connectionString =>
            calendar4.AppSettings.DbConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}