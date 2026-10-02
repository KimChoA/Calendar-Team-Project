using System;
using System.Windows.Forms;

namespace calendar4
{
    public partial class Shareinvite : Form
    {
        private readonly string calendarName;
        private readonly string inviteCode;

        private readonly emailSend
            emailSender = new();


        public Shareinvite(
            string calendarName,
            string inviteCode)
        {
            InitializeComponent();

            this.calendarName =
                calendarName;

            this.inviteCode =
                inviteCode;
        }


        private void btnInvite_Click(
            object sender,
            EventArgs e)
        {
            string emailText =
                txtEmail.Text.Trim();


            if (string.IsNullOrWhiteSpace(
                    emailText))
            {
                MessageBox.Show(
                    "초대할 이메일을 입력해주세요.",
                    "공유 캘린더 초대",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtEmail.Focus();

                return;
            }


            try
            {
                int sentCount =
                    emailSender.SendInviteCode(
                        emailText,
                        calendarName,
                        inviteCode);


                if (sentCount <= 0)
                {
                    MessageBox.Show(
                        "전송된 초대 메일이 없습니다.\n" +
                        "이메일 주소를 확인해주세요.",
                        "초대 실패",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                MessageBox.Show(
                    $"{sentCount}명에게 초대 메일을 전송했습니다.",
                    "초대 완료",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                DialogResult =
                    DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"초대 메일 전송 중 오류가 발생했습니다.\n\n{ex.Message}",
                    "초대 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}