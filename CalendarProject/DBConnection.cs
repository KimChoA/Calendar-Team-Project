using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace calendar4
{
    internal class DBConnection
    {
        // 민감한 DB 접속 정보는 환경 변수로 분리
        private readonly string connectionString =
            AppSettings.DbConnectionString;

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}