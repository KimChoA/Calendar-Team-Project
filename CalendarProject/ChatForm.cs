using calendar4.Services;
using MySql.Data.MySqlClient;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calendar4
{
    public partial class ChatForm : Form
    {
        // ============================================================
        // 현재 채팅방 정보
        // ============================================================

        private readonly int sharedCalendarId;
        private readonly int loggedInUserId;
        private readonly SharedCalendarRepository sharedCalendarRepository
        = new SharedCalendarRepository();

        // ============================================================
        // TCP 연결
        // ============================================================

        private TcpClient? client;
        private NetworkStream? stream;

        private bool isClosing = false;


        // ============================================================
        // 생성자
        // ============================================================

        public ChatForm(
            int sharedCalendarId,
            int userId)
        {
            InitializeComponent();

            this.sharedCalendarId =
                sharedCalendarId;

            loggedInUserId =
                userId;


            // 폼이 열릴 때 서버 연결
            Load += ChatForm_Load;

            // 폼이 닫힐 때 연결 종료
            FormClosing += ChatForm_FormClosing;
            Shown +=
    ChatForm_Shown;
        }


        // ============================================================
        // 폼 로드
        // ============================================================
        private void ChatForm_Shown(
    object sender,
    EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                rtbChat.SelectionStart =
                    rtbChat.TextLength;

                rtbChat.SelectionLength =
                    0;

                rtbChat.ScrollToCaret();

                rtbChat.Refresh();
            }));
        }

        private async void ChatForm_Load(
            object? sender,
            EventArgs e)
        {
            LoadRoomInfo();

            // 프로그램을 다시 실행해도 이전 채팅 기록 복원
            LoadChatHistory();

            // 과거 기록을 모두 표시한 뒤 실시간 채팅 서버 연결
            await ConnectToServer();
        }


        // ============================================================
        // 서버 연결
        // ============================================================
        private void LoadRoomInfo()
        {
            try
            {
                string roomName =
                    sharedCalendarRepository
                        .GetRoomName(
                            sharedCalendarId)
                    ?? "공유 캘린더";

                int memberCount =
                    sharedCalendarRepository
                        .GetMemberCount(
                            sharedCalendarId);

                lblRoomName.Text =
                    roomName;

                lblMemberCount.Text =
                    $"멤버 {memberCount}명";

                Text =
                    $"{roomName} - 채팅";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"공유 캘린더 정보를 불러오지 못했습니다.\n\n{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // DB에 저장된 이전 채팅 기록 불러오기
        // ============================================================

        private void LoadChatHistory()
        {
            try
            {
                // 채팅창 초기화
                rtbChat.Clear();


                using var connection =
                    new DBConnection().GetConnection();

                connection.Open();


                const string sql = @"
            SELECT
                c.chat_id,
                c.sender_user_id,
                c.message,
                c.created_at,
                u.name AS sender_name
            FROM shared_calendar_chat c

            LEFT JOIN user u
                ON c.sender_user_id = u.user_id

            WHERE c.shared_calendar_id = @shared_calendar_id

            ORDER BY
                c.created_at ASC,
                c.chat_id ASC";


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
                    int senderUserId =
                        reader.GetInt32(
                            "sender_user_id");


                    string senderName;


                    int senderNameOrdinal =
                        reader.GetOrdinal(
                            "sender_name");


                    if (reader.IsDBNull(
                            senderNameOrdinal))
                    {
                        senderName =
                            $"사용자 {senderUserId}";
                    }
                    else
                    {
                        senderName =
                            reader.GetString(
                                senderNameOrdinal);
                    }


                    string message =
                        reader.GetString(
                            "message");


                    DateTime createdAt =
                        reader.GetDateTime(
                            "created_at");


                    string timeText =
                        createdAt.ToString(
                            "HH:mm");


                    // ========================================================
                    // ★ 과거 채팅도 실시간 채팅과 동일한 서식으로 출력
                    // ========================================================

                    AppendFormattedChatMessage(
                        senderName,
                        message,
                        timeText);
                }


                // 가장 최근 채팅으로 스크롤
                // ============================================================
                // 가장 최근 채팅 위치로 이동
                // ============================================================

                rtbChat.SelectionStart =
                    rtbChat.TextLength;

                rtbChat.SelectionLength =
                    0;

                rtbChat.ScrollToCaret();


                // ============================================================
                // RichTextBox 화면 강제 갱신
                // ============================================================

                rtbChat.Invalidate();

                rtbChat.Update();

                rtbChat.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "이전 채팅 기록을 불러오지 못했습니다.\n\n" +
                    ex.Message,
                    "채팅 기록 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AppendFormattedChatMessage(
    string senderName,
    string message,
    string timeText)
        {
            // ============================================================
            // 이름
            // 10pt / 굵게 / 기본 배경
            // ============================================================

            rtbChat.SelectionStart =
                rtbChat.TextLength;

            rtbChat.SelectionLength =
                0;

            rtbChat.SelectionBackColor =
                rtbChat.BackColor;

            rtbChat.SelectionFont =
                new Font(
                    rtbChat.Font.FontFamily,
                    10f,
                    FontStyle.Bold);

            rtbChat.AppendText(
                $"{senderName}   ");


            // ============================================================
            // 메시지
            // 10pt / 일반 / 흰색 배경
            // ============================================================

            rtbChat.SelectionStart =
                rtbChat.TextLength;

            rtbChat.SelectionLength =
                0;

            rtbChat.SelectionBackColor =
                Color.White;

            rtbChat.SelectionFont =
                new Font(
                    rtbChat.Font.FontFamily,
                    10f,
                    FontStyle.Regular);

            rtbChat.AppendText(
                $" {message} ");


            // ============================================================
            // 시간
            // 7pt / 일반 / 기본 배경
            // ============================================================

            rtbChat.SelectionStart =
                rtbChat.TextLength;

            rtbChat.SelectionLength =
                0;

            rtbChat.SelectionBackColor =
                rtbChat.BackColor;

            rtbChat.SelectionFont =
                new Font(
                    rtbChat.Font.FontFamily,
                    7f,
                    FontStyle.Regular);

            rtbChat.AppendText(
                $" [{timeText}]\n");


            // 줄바꿈
            rtbChat.SelectionBackColor =
                rtbChat.BackColor;

            rtbChat.AppendText(
                Environment.NewLine);


            // 최신 메시지로 스크롤
            rtbChat.SelectionStart =
                rtbChat.TextLength;

            rtbChat.ScrollToCaret();
        }

        private async Task ConnectToServer()
        {
            try
            {
                client =
                    new TcpClient();


                await client.ConnectAsync(
                    "192.168.0.23",
                    5000);


                stream =
                    client.GetStream();


                // ====================================================
                // 서버에게 내가 어느 공유 캘린더 채팅방인지 전달
                //
                // JOIN | 공유캘린더ID | 사용자ID
                //
                // 예:
                // JOIN|3|15
                // ====================================================

                string userName =
    GetLoggedInUserName();

                string joinMessage =
                    $"JOIN|{sharedCalendarId}|{loggedInUserId}|{userName}\n";


                byte[] joinData =
                    Encoding.UTF8.GetBytes(
                        joinMessage);


                await stream.WriteAsync(
                    joinData,
                    0,
                    joinData.Length);


                // 메시지 수신 시작
                _ = ReceiveMessages();
            }
            catch (Exception ex)
            {
                if (isClosing)
                    return;


                MessageBox.Show(
                    "채팅 서버 연결 실패\n\n" +
                    ex.Message,
                    "채팅 연결 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 메시지 전송 버튼
        // ============================================================

        private async void btnSend_Click(
            object sender,
            EventArgs e)
        {
            if (stream == null ||
                client == null ||
                !client.Connected)
            {
                MessageBox.Show(
                    "채팅 서버에 연결되어 있지 않습니다.",
                    "채팅",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            string message =
                txtMessage.Text.Trim();


            if (string.IsNullOrWhiteSpace(message))
                return;


            try
            {
                // ====================================================
                // 서버에 보낼 메시지
                //
                // MSG | 공유캘린더ID | 사용자ID | 내용
                //
                // 예:
                // MSG|3|15|회의 시작합니다
                // ====================================================

                string sendText =
                    $"MSG|{sharedCalendarId}|{loggedInUserId}|{message}\n";


                byte[] data =
                    Encoding.UTF8.GetBytes(
                        sendText);


                await stream.WriteAsync(
                    data,
                    0,
                    data.Length);


                txtMessage.Clear();

                txtMessage.Focus();
            }
            catch (Exception ex)
            {
                if (isClosing)
                    return;


                MessageBox.Show(
                    "메시지 전송 실패\n\n" +
                    ex.Message,
                    "채팅 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 서버에서 메시지 수신
        // ============================================================

        private async Task ReceiveMessages()
        {
            if (stream == null)
                return;


            byte[] buffer =
                new byte[4096];


            try
            {
                while (!isClosing)
                {
                    int received =
                        await stream.ReadAsync(
                            buffer,
                            0,
                            buffer.Length);


                    // 서버가 연결을 끊음
                    if (received == 0)
                        break;


                    string message =
                        Encoding.UTF8.GetString(
                            buffer,
                            0,
                            received);


                    // 혹시 서버가 \r\n / \n을 포함해서 보내면
                    // 끝의 줄바꿈만 제거
                    message =
                        message.TrimEnd(
                            '\r',
                            '\n');


                    // =================================================
                    // UI 스레드에서 채팅창 갱신
                    // =================================================

                    if (rtbChat.InvokeRequired)
                    {
                        rtbChat.Invoke(
                            new Action(
                                () =>
                                {
                                    AppendChatMessage(
                                        message);
                                }));
                    }
                    else
                    {
                        AppendChatMessage(
                            message);
                    }
                }
            }
            catch (Exception ex)
            {
                if (isClosing)
                    return;


                try
                {
                    if (InvokeRequired)
                    {
                        Invoke(
                            new Action(
                                () =>
                                {
                                    MessageBox.Show(
                                        "채팅 서버와 연결이 종료되었습니다.\n\n" +
                                        ex.Message,
                                        "채팅 연결 종료",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                }));
                    }
                    else
                    {
                        MessageBox.Show(
                            "채팅 서버와 연결이 종료되었습니다.\n\n" +
                            ex.Message,
                            "채팅 연결 종료",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch
                {
                    // 폼이 종료되는 중이면 무시
                }
            }
        }


        // ============================================================
        // 채팅 내용 표시
        // ============================================================

        private void AppendChatMessage(
    string text)
        {
            if (string.IsNullOrWhiteSpace(
                    text))
            {
                return;
            }


            // 서버에서 오는 형식:
            // 홍길동 : 안녕하세요  [13:30]

            int nameSeparator =
                text.IndexOf(" : ");

            int timeStart =
                text.LastIndexOf("  [");


            // 예상한 형식이 아니면 그냥 출력
            if (nameSeparator < 0 ||
                timeStart < 0 ||
                timeStart <= nameSeparator)
            {
                rtbChat.AppendText(
                    text +
                    Environment.NewLine);

                return;
            }


            string senderName =
                text.Substring(
                    0,
                    nameSeparator);


            string message =
                text.Substring(
                    nameSeparator + 3,
                    timeStart - (nameSeparator + 3));


            string timeText =
                text.Substring(
                    timeStart + 3)
                    .TrimEnd(']');


            // ★ 실시간 채팅도 똑같은 스타일 적용
            AppendFormattedChatMessage(
                senderName,
                message,
                timeText);
        }


        // ============================================================
        // 폼 종료
        // ============================================================

        private void ChatForm_FormClosing(
            object? sender,
            FormClosingEventArgs e)
        {
            isClosing = true;


            try
            {
                stream?.Close();
            }
            catch
            {
            }


            try
            {
                client?.Close();
            }
            catch
            {
            }


            stream = null;

            client = null;
        }

        private string GetLoggedInUserName()
        {
            using var connection =
                new DBConnection().GetConnection();

            connection.Open();

            const string sql = @"
        SELECT name
        FROM user
        WHERE user_id = @user_id
        LIMIT 1";

            using var command =
                new MySqlCommand(
                    sql,
                    connection);

            command.Parameters.AddWithValue(
                "@user_id",
                loggedInUserId);

            object? result =
                command.ExecuteScalar();

            if (result == null ||
                result == DBNull.Value)
            {
                return $"사용자 {loggedInUserId}";
            }

            return result.ToString()!;
        }
    }
}