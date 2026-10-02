using System;

namespace calendar4
{
    /// <summary>
    /// GitHub에 비밀번호/API 키를 직접 올리지 않도록 환경 변수에서 설정값을 읽습니다.
    /// 실행 전 README의 환경 변수 설정 항목을 참고하세요.
    /// </summary>
    internal static class AppSettings
    {
        public static string DbConnectionString =>
            Environment.GetEnvironmentVariable("MOBLE_DB_CONNECTION")
            ?? throw new InvalidOperationException(
                "MOBLE_DB_CONNECTION 환경 변수가 설정되지 않았습니다.");

        public static string HolidayApiKey =>
            Environment.GetEnvironmentVariable("MOBLE_HOLIDAY_API_KEY")
            ?? throw new InvalidOperationException(
                "MOBLE_HOLIDAY_API_KEY 환경 변수가 설정되지 않았습니다.");

        public static string SmtpEmail =>
            Environment.GetEnvironmentVariable("MOBLE_SMTP_EMAIL")
            ?? throw new InvalidOperationException(
                "MOBLE_SMTP_EMAIL 환경 변수가 설정되지 않았습니다.");

        public static string SmtpAppPassword =>
            Environment.GetEnvironmentVariable("MOBLE_SMTP_APP_PASSWORD")
            ?? throw new InvalidOperationException(
                "MOBLE_SMTP_APP_PASSWORD 환경 변수가 설정되지 않았습니다.");
    }
}
