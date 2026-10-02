using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace calendar4
{
    public partial class emailSend : UserControl
    {
        public emailSend()
        {
            InitializeComponent();
        }


        // ============================================================
        // 공유 캘린더 초대 메일 보내기
        //
        // inputEmails 예:
        // aaa@gmail.com
        //
        // 또는
        // aaa@gmail.com,bbb@gmail.com,ccc@gmail.com
        // ============================================================

        public int SendInviteCode(
            string inputEmails,
            string calendarName,
            string inviteCode)
        {
            if (string.IsNullOrWhiteSpace(inputEmails))
            {
                return 0;
            }


            // 쉼표 기준으로 여러 이메일 분리
            string[] emails =
                inputEmails
                    .Split(
                        ',',
                        StringSplitOptions.RemoveEmptyEntries |
                        StringSplitOptions.TrimEntries)
                    .Distinct(
                        StringComparer.OrdinalIgnoreCase)
                    .ToArray();


            int successCount = 0;


            foreach (string email in emails)
            {
                // 이메일 형식 확인
                if (!IsValidEmail(email))
                {
                    continue;
                }


                bool success =
                    SendEmail(
                        email,
                        calendarName,
                        inviteCode);


                if (success)
                {
                    successCount++;
                }
            }


            return successCount;
        }


        // ============================================================
        // 실제 이메일 발송
        // ============================================================

        private bool SendEmail(
            string toEmail,
            string calendarName,
            string inviteCode)
        {
            try
            {
                using var smtp =
                    new SmtpClient(
                        "smtp.gmail.com",
                        587);


                smtp.EnableSsl = true;

                smtp.DeliveryMethod =
                    SmtpDeliveryMethod.Network;

                smtp.UseDefaultCredentials =
                    false;


                // Gmail 계정과 앱 비밀번호는 환경 변수에서 읽어 보안 정보 노출 방지
                smtp.Credentials =
                    new NetworkCredential(
                        AppSettings.SmtpEmail,
                        AppSettings.SmtpAppPassword);


                using var mail =
                    new MailMessage();


                mail.From =
                    new MailAddress(
                        AppSettings.SmtpEmail,
                        "Moble Calendar");


                mail.To.Add(
                    toEmail);


                mail.Subject =
                    "[Moble Calendar] 공유 캘린더 초대";


                mail.Body =
                    $"안녕하세요.\n\n" +
                    $"공유 캘린더에 초대되었습니다.\n\n" +
                    $"캘린더 이름 : {calendarName}\n" +
                    $"참가 코드 : {inviteCode}\n\n" +
                    $"Moble Calendar에서 공유 캘린더 탭을 열고\n" +
                    $"참가 코드를 입력해주세요.";


                smtp.Send(
                    mail);


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"메일 발송 중 오류가 발생했습니다.\n\n" +
                    $"받는 사람 : {toEmail}\n" +
                    $"{ex.Message}",
                    "메일 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);


                return false;
            }
        }


        // ============================================================
        // 이메일 형식 검사
        // ============================================================

        private bool IsValidEmail(
            string email)
        {
            try
            {
                var address =
                    new MailAddress(
                        email);


                return
                    address.Address.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}