using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using tap;
using MySql.Data.MySqlClient;
using calendar4.Services;
using calendar4.CreateForm;

namespace calendar4
{
    public partial class mainForm : Form
    {
        private readonly int loggedInUserId;

        private ContextMenuStrip tabAddMenu;
        private ContextMenuStrip tabContextMenu;
        private Panel? tabHeaderFillPanel;
        private ChatForm? chatForm;
        private int? currentChatSharedCalendarId;


        private TabPage targetTab = null;
        private TextBox txtRenameEditor;
        private TabPage editingTab = null;

        private readonly HolidayService holidayService = new();
        private readonly SummaryService summaryService = new();
        private readonly TabDbRepository tabDbRepository = new();
        private readonly CalendarDbRepository calendarDbRepository = new();
        // ============================================================
        // 공유 캘린더 멤버 상태 확인 타이머
        // ============================================================

        private readonly System.Windows.Forms.Timer
            sharedCalendarMemberCheckTimer =
                new System.Windows.Forms.Timer();

        // 회원탈퇴 완료 여부
        // 탈퇴 후 타이머/탭 저장이 다시 실행되는 것을 방지
        private bool isAccountDeleted = false;

        private AlarmManager? alarmManager;

        private DateTime currentMonth = DateTime.Now;

        private Dictionary<DateTime, string> holidayMap =
            new Dictionary<DateTime, string>();


        public enum TabType
        {
            Diary,
            Planner,
            Calendar,
            SharedCalendar,
            Timetable
        }


        // ============================================================
        // 생성자
        // ============================================================

        public mainForm()
        {
            InitializeComponent();
            InitCalendarViewOptions();
        }

        public mainForm(int userId) : this()
        {
            loggedInUserId = userId;
        }


        // ============================================================
        // 테마 선택
        // ============================================================

        // [담당 기능] 사용자가 선택한 테마를 저장하고 열린 모든 탭에 즉시 반영
        private void ApplySelectedTheme(AppTheme theme)
        {
            // 프리미엄 전용 테마 검사
            if (UiThemeService.IsPremiumTheme(theme) &&
                !CurrentUser.IsPremium)
            {
                MessageBox.Show(
                    "프리미엄 사용자만 사용할 수 있는 테마입니다.",
                    "Premium",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // 현재 프로그램 테마 변경
            UiThemeService.SetTheme(theme);

            // 메인폼에 테마 적용
            UiThemeService.ApplyTheme(this);
            tabControl1.BackColor =
    UiThemeService.BackgroundColor;

            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.BackColor =
                    UiThemeService.BackgroundColor;

                tab.ForeColor =
                    UiThemeService.TextColor;
            }

            tabControl1.Invalidate();

            // 열려있는 모든 탭에도 테마 적용
            ApplyThemeToAllTabs();
            UpdateTabHeaderFillPanel();

            // 선택한 테마를 DB에 저장
            SaveUserTheme(theme);
        }


        // ============================================================
        // 열려있는 모든 탭에 현재 테마 적용
        // ============================================================

        private void ApplyThemeToAllTabs()
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                // + 탭 등 컨트롤이 없는 경우
                if (tab.Controls.Count == 0)
                    continue;

                foreach (Control control in tab.Controls)
                {
                    if (control is CalendarControl calendarControl)
                    {
                        calendarControl.ApplyCurrentTheme();
                    }
                    else if (control is DiaryControl diaryControl)
                    {
                        diaryControl.ApplyCurrentTheme();
                    }
                    else if (control is PlannerControl plannerControl)
                    {
                        plannerControl.ApplyCurrentTheme();
                    }
                    else if (control is Timetable timetable)
                    {
                        timetable.ApplyCurrentTheme();
                    }
                    else
                    {
                        // 일반 컨트롤
                        UiThemeService.ApplyTheme(control);
                    }
                }
            }
        }


        // ============================================================
        // AppTheme → DB 번호
        //
        // 0 = Light
        // 1 = Dark
        // 2 = Blossom
        // 3 = Mint
        // 4 = Lavender
        // 5 = Cozy
        // ============================================================

        private int GetThemeNumber(AppTheme theme)
        {
            return theme switch
            {
                AppTheme.Dark => 1,
                AppTheme.Blossom => 2,
                AppTheme.Mint => 3,
                AppTheme.Lavender => 4,
                AppTheme.Cozy => 5,

                _ => 0
            };
        }


        // ============================================================
        // 사용자 테마 DB 저장
        // ============================================================

        private void SaveUserTheme(AppTheme theme)
        {
            try
            {
                using var connection =
                    new DBConnection().GetConnection();

                connection.Open();

                const string sql = @"
                    UPDATE user
                    SET theme = @theme
                    WHERE user_id = @user_id";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue(
                    "@theme",
                    GetThemeNumber(theme));

                command.Parameters.AddWithValue(
                    "@user_id",
                    loggedInUserId);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"테마 저장 중 오류가 발생했습니다.\n\n{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 사용자 테마 DB 불러오기
        // ============================================================

        private AppTheme LoadUserTheme()
        {
            try
            {
                using var connection =
                    new DBConnection().GetConnection();

                connection.Open();

                const string sql = @"
                    SELECT theme
                    FROM user
                    WHERE user_id = @user_id";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue(
                    "@user_id",
                    loggedInUserId);

                object? result =
                    command.ExecuteScalar();

                // DB 값이 없으면 기본 Light
                if (result == null ||
                    result == DBNull.Value)
                {
                    return AppTheme.Light;
                }

                int themeValue =
                    Convert.ToInt32(result);

                return themeValue switch
                {
                    1 => AppTheme.Dark,
                    2 => AppTheme.Blossom,
                    3 => AppTheme.Mint,
                    4 => AppTheme.Lavender,
                    5 => AppTheme.Cozy,

                    _ => AppTheme.Light
                };
            }
            catch
            {
                // DB 문제가 있어도 프로그램은 실행되도록
                return AppTheme.Light;
            }
        }
        private int GetFontNumber(AppFontType font)
        {
            return (int)font;
        }


        // ============================================================
        // 사용자 글꼴 DB 저장
        // ============================================================
        private void SaveUserFont(AppFontType font)
        {
            try
            {
                using var connection =
                    new DBConnection().GetConnection();

                connection.Open();

                const string sql = @"
            UPDATE user
            SET font = @font
            WHERE user_id = @user_id";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue(
                    "@font",
                    GetFontNumber(font));

                command.Parameters.AddWithValue(
                    "@user_id",
                    loggedInUserId);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"글꼴 저장 중 오류가 발생했습니다.\n\n{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 사용자 글꼴 DB 불러오기
        // ============================================================
        private AppFontType LoadUserFont()
        {
            try
            {
                using var connection =
                    new DBConnection().GetConnection();

                connection.Open();

                const string sql = @"
            SELECT font
            FROM user
            WHERE user_id = @user_id";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue(
                    "@user_id",
                    loggedInUserId);

                object? result =
                    command.ExecuteScalar();

                if (result == null ||
                    result == DBNull.Value)
                {
                    return AppFontType.MalgunGothic;
                }

                int fontValue =
                    Convert.ToInt32(result);

                return fontValue switch
                {
                    1 => AppFontType.Batang,
                    2 => AppFontType.Dotum,
                    3 => AppFontType.HancomMalang,
                    4 => AppFontType.HunminHorizontal,
                    5 => AppFontType.HancomSanzDotum,

                    _ => AppFontType.MalgunGothic
                };
            }
            catch
            {
                return AppFontType.MalgunGothic;
            }
        }
        // [담당 기능] 사용자 글꼴 선택을 저장하고 캘린더/플래너 등 각 화면에 반영
        private void ApplySelectedFont(AppFontType font)
        {
            // ============================================================
            // 1. 프리미엄 글꼴인지 확인
            // ============================================================

            if (AppFontService.IsPremiumFont(font) &&
                !CurrentUser.IsPremium)
            {
                MessageBox.Show(
                    "프리미엄 사용자만 사용할 수 있는 글꼴입니다.",
                    "Premium",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            // ============================================================
            // 2. 현재 글꼴 변경
            // ============================================================

            AppFontService.SetFont(font);


            // ============================================================
            // 3. 메인폼의 일반 컨트롤 글꼴 변경
            // ============================================================

            ApplyFontToControlRecursive(this);


            // ============================================================
            // 4. 탭 내부 전용 컨트롤 글꼴 변경
            // ============================================================

            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Controls.Count == 0)
                    continue;

                Control control =
                    tab.Controls[0];

                if (control is CalendarControl calendarControl)
                {
                    calendarControl.ApplyCurrentFont();
                }

                // PlannerControl은 다음 단계에서
                // ApplyCurrentFont()를 추가한 뒤 활성화
                /*
                else if (control is PlannerControl plannerControl)
                {
                    plannerControl.ApplyCurrentFont();
                }
                */

                // Timetable도 다음 단계에서
                // ApplyCurrentFont()를 추가한 뒤 활성화
                /*
                else if (control is Timetable timetable)
                {
                    timetable.ApplyCurrentFont();
                }
                */
            }


            // ============================================================
            // 5. 현재 선택된 글꼴 메뉴 체크 표시
            // ============================================================

            UpdateFontMenu();


            // ============================================================
            // 6. 사용자 선택 글꼴 DB 저장
            // ============================================================

            SaveUserFont(font);


            // ============================================================
            // 7. 화면 다시 그리기
            // ============================================================

            Invalidate(true);
            Refresh();
        }
        private void ApplyFontToControlRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // 자체적으로 글꼴을 처리하는 컨트롤은 제외
                if (control is CalendarControl ||
                    control is PlannerControl ||
                    control is Timetable)
                {
                    continue;
                }

                float originalSize =
                    control.Font.Size;

                FontStyle originalStyle =
                    control.Font.Style;

                control.Font =
                    AppFontService.CreateFont(
                        originalSize,
                        originalStyle);

                if (control.HasChildren)
                {
                    ApplyFontToControlRecursive(control);
                }
            }
        }
        private void UpdateFontMenu()
        {
            AppFontType currentFont =
                AppFontService.CurrentFont;

            맑은고딕ToolStripMenuItem.Checked =
                currentFont == AppFontType.MalgunGothic;

            바탕체ToolStripMenuItem.Checked =
                currentFont == AppFontType.Batang;

            돋움ToolStripMenuItem.Checked =
                currentFont == AppFontType.Dotum;

            한컴말랑말랑ToolStripMenuItem.Checked =
                currentFont == AppFontType.HancomMalang;

            훈민정음가로쓰기ToolStripMenuItem.Checked =
                currentFont == AppFontType.HunminHorizontal;

            한컴산뜻돋움ToolStripMenuItem.Checked =
                currentFont == AppFontType.HancomSanzDotum;
        }

        // ============================================================
        // Form Load
        // ============================================================

        private async void mainForm_Load(
            object sender,
            EventArgs e)
        {
            tabControl1.Multiline = false;

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem -= tabControl1_DrawItem;
            tabControl1.DrawItem += tabControl1_DrawItem;
            InitTabAddMenu();
            InitTabContextMenu();
            InitRenameEditor();

            tabControl1.Selecting +=
                tabControl1_Selecting;

            tabControl1.SelectedIndexChanged +=
                tabControl1_SelectedIndexChanged;

            tabControl1.DoubleClick +=
                tabControl1_DoubleClick;

            tabControl1.MouseDown +=
                tabControl1_MouseDown;

            this.MouseDown +=
                Form_MouseDown_ApplyRename;

            InitSmallCalendarEvent();

            // 저장된 탭 생성
            LoadTabs();
            LoadSavedDday();
            InitializeTabHeaderFillPanel();
            // ========================================================
            // 로그인한 사용자의 저장된 테마 불러오기
            // ========================================================

            AppTheme savedTheme =
                LoadUserTheme();

            UiThemeService.SetTheme(
                savedTheme);

            // 메인폼 적용
            UiThemeService.ApplyTheme(
                this);

            // LoadTabs()에서 이미 생성된 탭에도 적용
            ApplyThemeToAllTabs();

            // =============================================
            // 저장된 사용자 글꼴 불러오기
            // =============================================

            AppFontType savedFont =
                LoadUserFont();

            AppFontService.SetFont(
                savedFont);

            ApplyFontToControlRecursive(
                this);

            UpdateFontMenu();
            // ========================================================
            // 알람 시작
            // ========================================================

            alarmManager =
                new AlarmManager(tabControl1);

            alarmManager.Start();

            SyncSmallCalendar();

            await LoadHolidaysAsync(
                currentMonth.Year,
                currentMonth.Month);

            RefreshAllViews();

            if (!IsPremiumUser())
            {
                AD ad = new AD();
                ad.ShowDialog();
            }
            LoadUserName();
            // ============================================================
            // 공유 캘린더 강퇴 여부 확인 타이머 시작
            // ============================================================

            sharedCalendarMemberCheckTimer.Interval =
                2000; // 2초

            sharedCalendarMemberCheckTimer.Tick +=
                SharedCalendarMemberCheckTimer_Tick;

            sharedCalendarMemberCheckTimer.Start();
        }
        // ============================================================
        // 공유 캘린더에서 내보내기 당했는지 주기적으로 확인
        // ============================================================

        private void SharedCalendarMemberCheckTimer_Tick(
    object? sender,
    EventArgs e)
        {
            // 회원탈퇴가 완료된 상태에서는 더 이상 확인하지 않음
            if (isAccountDeleted)
            {
                sharedCalendarMemberCheckTimer.Stop();
                return;
            }

            sharedCalendarMemberCheckTimer.Stop();

            try
            {
                var repository =
                    new SharedCalendarRepository();

                var tabsToRemove =
                    new List<TabPage>();


                foreach (
                    TabPage tab
                    in tabControl1.TabPages)
                {
                    if (tab.Text == "+")
                        continue;


                    if (tab.Tag
                        is not TabData tabData)
                    {
                        continue;
                    }


                    if (tabData.Type !=
                        TabType.SharedCalendar)
                    {
                        continue;
                    }


                    if (!tabData.SharedCalendarId.HasValue)
                    {
                        continue;
                    }


                    int sharedCalendarId =
                        tabData.SharedCalendarId.Value;


                    // ========================================================
                    // 1. 내가 아직 이 공유 캘린더의 멤버인지 확인
                    // ========================================================

                    bool isMember =
                        repository.IsMember(
                            sharedCalendarId,
                            loggedInUserId);

                    if (!isMember)
                    {
                        tabsToRemove.Add(
                            tab);

                        continue;
                    }


                    // ========================================================
                    // 3. 아직 멤버라면 공유 일정 최신화
                    // ========================================================

                    if (tab.Controls.Count > 0 &&
                        tab.Controls[0]
                        is CalendarControl calendarControl)
                    {
                        calendarControl.LoadSchedules();

                        calendarControl.UpdateView();
                    }
                }


                // ============================================================
                // 4. 내보내기 당한 공유 탭 제거
                // ============================================================

                foreach (
                    TabPage tab
                    in tabsToRemove)
                {
                    if (tab.Tag
                            is TabData tabData
                        &&
                        tabData.SharedCalendarId.HasValue
                        &&
                        currentChatSharedCalendarId ==
                            tabData.SharedCalendarId.Value)
                    {
                        if (chatForm != null &&
                            !chatForm.IsDisposed)
                        {
                            chatForm.Close();
                        }


                        chatForm =
                            null;

                        currentChatSharedCalendarId =
                            null;
                    }


                    tabControl1.TabPages.Remove(
                        tab);

                    tab.Dispose();
                }


                // ============================================================
                // 5. 탭이 삭제됐으면 DB에도 현재 탭 상태 저장
                // ============================================================

                if (tabsToRemove.Count > 0 &&
                    !isAccountDeleted)
                {
                    SaveTabs();

                    UpdateTabHeaderFillPanel();

                    tabControl1.Invalidate();


                    MessageBox.Show(
                        "관리자에 의해 공유 캘린더에서 내보내졌습니다.",
                        "공유 캘린더",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch
            {
                // DB 연결이 잠깐 실패했다고
                // 공유탭을 삭제하거나 오류창을 계속 띄우지 않음
            }
            finally
            {
                if (!isAccountDeleted)
                {
                    sharedCalendarMemberCheckTimer.Start();
                }
            }
        }


        private bool IsPremiumUser()
        {
            try
            {
                using var connection =
                    new DBConnection().GetConnection();

                connection.Open();

                const string sql = @"
            SELECT premium
            FROM user
            WHERE user_id = @user_id";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue(
                    "@user_id",
                    loggedInUserId);

                object? result =
                    command.ExecuteScalar();

                if (result == null ||
                    result == DBNull.Value)
                {
                    return false;
                }

                return Convert.ToInt32(result) == 1;
            }
            catch
            {
                return false;
            }
        }


        // ============================================================
        // 공휴일
        // ============================================================

        // [담당 기능] 선택 연/월의 공휴일을 비동기로 받아 UI 응답성을 유지
        private async Task LoadHolidaysAsync(
            int year,
            int month)
        {
            try
            {
                holidayMap =
                    await holidayService.GetHolidaysAsync(
                        year,
                        month);

                ApplyHolidayMapToCalendarControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "공휴일 정보를 가져오지 못했습니다.\n\n" +
                    ex.Message,
                    "공휴일 API 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // [담당 기능] 한 번 조회한 공휴일 데이터를 열려 있는 모든 캘린더 화면에 공통 적용
        private void ApplyHolidayMapToCalendarControls()
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Controls.Count > 0 &&
                    tab.Controls[0] is CalendarControl calCtrl)
                {
                    calCtrl.SetHolidayMap(
                        holidayMap);
                }
            }
        }


        // ============================================================
        // Form Closing
        // ============================================================

        private void mainForm_FormClosing(
    object sender,
    FormClosingEventArgs e)
        {
            sharedCalendarMemberCheckTimer.Stop();

            alarmManager?.Dispose();

            // 회원탈퇴 후에는 user가 이미 삭제된 상태이므로
            // user_tab 저장을 시도하지 않음
            if (!isAccountDeleted)
            {
                SaveTabs();
            }

            Application.Exit();
        }


        // ============================================================
        // 달력 보기 옵션
        // ============================================================

        private void InitCalendarViewOptions()
        {
            if (tool_month != null)
            {
                tool_month.Click += (_, _) =>
                    ChangeCalendarView(
                        CalendarControl.CalendarViewMode.Month);
            }

            if (tool_week != null)
            {
                tool_week.Click += (_, _) =>
                    ChangeCalendarView(
                        CalendarControl.CalendarViewMode.Week);
            }

            if (tool_day != null)
            {
                tool_day.Click += (_, _) =>
                    ChangeCalendarView(
                        CalendarControl.CalendarViewMode.Day);
            }

            UpdateCalendarViewMenu();
        }

        private void ChangeCalendarView(
            CalendarControl.CalendarViewMode viewMode)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Controls.Count == 0)
                    continue;

                if (tab.Controls[0] is CalendarControl calCtrl)
                {
                    calCtrl.SetViewMode(
                        viewMode);
                }
                else if (tab.Controls[0] is DiaryControl diaryCtrl)
                {
                    diaryCtrl.SetViewMode(
                        viewMode);
                }
            }

            UpdateCalendarViewMenu();
            UpdateCalendarTitle();
        }

        private void UpdateCalendarViewMenu()
        {
            CalendarControl.CalendarViewMode currentMode =
                GetSelectedViewMode();

            if (tool_month != null)
            {
                tool_month.Checked =
                    currentMode ==
                    CalendarControl.CalendarViewMode.Month;
            }

            if (tool_week != null)
            {
                tool_week.Checked =
                    currentMode ==
                    CalendarControl.CalendarViewMode.Week;
            }

            if (tool_day != null)
            {
                tool_day.Checked =
                    currentMode ==
                    CalendarControl.CalendarViewMode.Day;
            }
        }

        private CalendarControl.CalendarViewMode
            GetSelectedViewMode()
        {
            if (tabControl1.SelectedTab?.Controls.Count > 0)
            {
                Control selectedControl =
                    tabControl1.SelectedTab.Controls[0];

                if (selectedControl
                    is CalendarControl calendarControl)
                {
                    return calendarControl.GetViewMode();
                }

                if (selectedControl
                    is DiaryControl diaryControl)
                {
                    return diaryControl.GetViewMode();
                }
            }

            return CalendarControl.CalendarViewMode.Month;
        }


        // ============================================================
        // 작은 달력
        // ============================================================

        private void InitSmallCalendarEvent()
        {
            if (this.Controls.Find(
                "monthCalendar1",
                true).Length > 0)
            {
                MonthCalendar miniCal =
                    this.Controls.Find(
                        "monthCalendar1",
                        true)[0]
                    as MonthCalendar;

                if (miniCal != null)
                {
                    miniCal.DateChanged +=
                        MiniCal_DateChanged;
                }
            }
        }

        private async void MiniCal_DateChanged(
            object sender,
            DateRangeEventArgs e)
        {
            ApplyTabRename();

            currentMonth =
                e.Start.Date;

            await LoadHolidaysAsync(
                currentMonth.Year,
                currentMonth.Month);

            RefreshAllViews();
        }

        private void SyncSmallCalendar()
        {
            if (this.Controls.Find(
                "monthCalendar1",
                true).Length > 0)
            {
                MonthCalendar miniCal =
                    this.Controls.Find(
                        "monthCalendar1",
                        true)[0]
                    as MonthCalendar;

                if (miniCal != null)
                {
                    miniCal.DateChanged -=
                        MiniCal_DateChanged;

                    miniCal.SetDate(
                        currentMonth);

                    miniCal.DateChanged +=
                        MiniCal_DateChanged;
                }
            }
        }


        // ============================================================
        // 탭 변경
        // ============================================================

        private void tabControl1_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {

            UpdateSummaryView();
            UpdateCalendarViewMenu();
            UpdateCalendarTitle();


            // ============================================================
            // 실제 입장된 공유 캘린더에서만 버튼 표시
            // ============================================================

            btnChat.Visible =
                tabControl1.SelectedTab?.Tag is TabData tabData &&
                tabData.Type == TabType.SharedCalendar &&
                tabData.SharedCalendarId.HasValue;


            // ============================================================
            // 채팅창 처리
            // ============================================================

            if (tabControl1.SelectedTab == null)
            {
                HideChatForm();
                return;
            }


            if (tabControl1.SelectedTab.Tag
                is not TabData selectedTabData)
            {
                HideChatForm();
                return;
            }
        }

        private void ShowChatForm(
    int sharedCalendarId)
        {
            // ============================================================
            // 이미 같은 공유방 채팅창이 열려 있으면 재사용
            // ============================================================

            if (chatForm != null &&
                !chatForm.IsDisposed &&
                currentChatSharedCalendarId == sharedCalendarId)
            {
                if (!chatForm.Visible)
                {
                    chatForm.Show();
                }

                chatForm.BringToFront();

                return;
            }


            // ============================================================
            // 다른 공유방 채팅창이 열려 있었다면 종료
            // ============================================================

            if (chatForm != null &&
                !chatForm.IsDisposed)
            {
                chatForm.Close();
            }


            // ============================================================
            // 새 공유방 채팅창 생성
            // ============================================================

            currentChatSharedCalendarId =
                sharedCalendarId;


            chatForm =
                new ChatForm(
                    sharedCalendarId,
                    loggedInUserId);


            chatForm.FormClosed +=
                (s, e) =>
                {
                    chatForm = null;

                    currentChatSharedCalendarId =
                        null;
                };

            chatForm.StartPosition =
    FormStartPosition.Manual;

            chatForm.Location =
                new Point(
                    this.Right - 12,
                    this.Top);

            chatForm.Show(
                this);
        }

        private void HideChatForm()
        {
            if (chatForm == null ||
                chatForm.IsDisposed)
            {
                return;
            }

            chatForm.Hide();
        }


        // ============================================================
        // 전체 View 날짜 갱신
        // ============================================================

        // [담당 기능] 날짜 변경 시 캘린더와 스터디 플래너를 같은 날짜 기준으로 동기화
        private void RefreshAllViews()
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Controls.Count == 0)
                    continue;

                if (tab.Controls[0] is DiaryControl diaryCtrl)
                {
                    diaryCtrl.SetTargetDate(
                        currentMonth);
                }
                else if (tab.Controls[0] is CalendarControl calCtrl)
                {
                    calCtrl.SetHolidayMap(
                        holidayMap);

                    calCtrl.SetTargetDate(
                        currentMonth);
                }
                else if (tab.Controls[0] is PlannerControl plannerCtrl)
                {
                    plannerCtrl.SetDate(
                        currentMonth);
                }
            }

            UpdateSummaryView();
            UpdateCalendarTitle();
        }


        // ============================================================
        // 일정 요약
        // ============================================================

        private void UpdateSummaryView()
        {
            if (tabControl1.SelectedTab is null)
                return;


            var summaryBoxes =
                Controls.Find(
                    "richTextBox1",
                    true);


            if (summaryBoxes.Length == 0 ||
                summaryBoxes[0]
                is not RichTextBox summaryBox)
            {
                return;
            }


            // ============================================================
            // 다이어리
            // ============================================================

            if (tabControl1.SelectedTab.Controls.Count > 0 &&
                tabControl1.SelectedTab.Controls[0]
                is DiaryControl diaryControl)
            {
                summaryBox.Text =
                    summaryService.CreateDiarySummary(
                        diaryControl.GetDiaryMap());

                return;
            }


            // ============================================================
            // 캘린더
            // ============================================================

            if (tabControl1.SelectedTab.Controls.Count > 0 &&
                tabControl1.SelectedTab.Controls[0]
                is CalendarControl calendarControl)
            {
                // 현재 선택된 탭이 공유 캘린더인지 확인
                bool isSharedCalendar =
                    tabControl1.SelectedTab.Tag
                        is TabData tabData
                    &&
                    tabData.Type ==
                        TabType.SharedCalendar;


                summaryBox.Text =
                    summaryService.CreateCalendarSummary(
                        calendarControl.GetScheduleMap(),

                        // 개인 캘린더 = true
                        // 공유 캘린더 = false
                        showCategory:
                            !isSharedCalendar);


                return;
            }


            // ============================================================
            // 플래너 / 시간표 등
            // 일정 요약 표시 안 함
            // ============================================================

            summaryBox.Clear();
        }


        // ============================================================
        // 상단 날짜 제목
        // ============================================================

        private void UpdateCalendarTitle()
        {
            if (lbmain_title is null)
                return;

            lbmain_title.Text =
                CalendarTitleFormatter.Format(
                    currentMonth,
                    GetSelectedViewMode());
        }

        // ============================================================
        // + 탭 메뉴
        // ============================================================

        private void InitTabAddMenu()
        {
            tabAddMenu =
                new ContextMenuStrip();

            tabAddMenu.Items.Add(
                "다이어리",
                null,
                (s, ev) =>
                    AddNewCustomTab(
                        "다이어리",
                        TabType.Diary));

            tabAddMenu.Items.Add(
                "스터디 플래너",
                null,
                (s, ev) =>
                    AddNewCustomTab(
                        "스터디 플래너",
                        TabType.Planner));

            tabAddMenu.Items.Add(
                "시간표",
                null,
                (s, ev) =>
                    AddNewCustomTab(
                        "시간표",
                        TabType.Timetable));

            tabAddMenu.Items.Add(
                "기본 캘린더",
                null,
                (s, ev) =>
                    AddNewCustomTab(
                        "기본 캘린더",
                        TabType.Calendar));

            tabAddMenu.Items.Add(
                "공유 캘린더",
                null,
                (s, ev) =>
                    AddNewCustomTab(
                        "공유 캘린더",
                        TabType.SharedCalendar));
        }

        private void tabControl1_Selecting(
            object sender,
            TabControlCancelEventArgs e)
        {
            ApplyTabRename();

            if (e.TabPage != null &&
                e.TabPage.Text == "+")
            {
                e.Cancel = true;

                tabAddMenu?.Show(
                    Cursor.Position);
            }
        }

        private void AddNewCustomTab(
            string title,
            TabType type)
        {
            int plusIndex =
                tabControl1.TabPages.Count - 1;

            if (plusIndex >= 0 &&
                tabControl1.TabPages[plusIndex].Text == "+")
            {
                tabControl1.TabPages.RemoveAt(
                    plusIndex);
            }

            int? calendarId = null;

            // ========================================
            // 개인 캘린더 탭이라면
            // DB에 새로운 달력 공간을 먼저 만든다.
            // ========================================
            if (type == TabType.Calendar)
            {
                calendarId =
                    calendarDbRepository.CreateCalendar(
                        loggedInUserId,
                        title);
            }

            TabPage newTab =
                CreateTabPage(
                    title,
                    type,
                    calendarId);

            tabControl1.TabPages.Add(
                new TabPage("+"));

            tabControl1.SelectedTab =
                newTab;

            // 새 탭 정보를 DB에 저장
            SaveTabs();

            // 새 탭이 추가되었으므로 탭 헤더의 오른쪽 빈 영역 위치 갱신
            UpdateTabHeaderFillPanel();

            // 탭 색상 다시 그리기
            tabControl1.Invalidate();
        }


        // ============================================================
        // 탭 우클릭
        // ============================================================

        private void InitTabContextMenu()
        {
            tabContextMenu =
                new ContextMenuStrip();

            tabContextMenu.Items.Add(
                "이름 변경",
                null,
                (s, ev) =>
                {
                    if (targetTab != null)
                    {
                        StartInlineRename(
                            targetTab);
                    }
                });

            tabContextMenu.Items.Add(
                "탭 삭제",
                null,
                DeleteItem_Click);
        }

        private void tabControl1_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            ApplyTabRename();

            if (e.Button ==
                MouseButtons.Right)
            {
                for (int i = 0;
                     i < tabControl1.TabPages.Count;
                     i++)
                {
                    if (tabControl1.GetTabRect(i)
                        .Contains(e.Location))
                    {
                        TabPage clickedTab =
                            tabControl1.TabPages[i];

                        if (clickedTab.Text != "+")
                        {
                            targetTab =
                                clickedTab;

                            tabContextMenu?.Show(
                                tabControl1,
                                e.Location);
                        }

                        break;
                    }
                }
            }
        }

        private void DeleteItem_Click(
            object sender,
            EventArgs e)
        {
            if (targetTab == null)
                return;

            if (tabControl1.TabPages.Count - 1 <= 1)
            {
                MessageBox.Show(
                    "최소 하나의 탭은 유지해야 합니다.",
                    "알림",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (MessageBox.Show(
                    $"[{targetTab.Text}] 탭을 정말 삭제하시겠습니까?",
                    "탭 삭제 확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                tabControl1.TabPages.Remove(
                    targetTab);

                targetTab =
                    null;

                // 삭제된 탭 상태를 DB에 저장
                SaveTabs();

                // 탭 개수가 바뀌었으므로 오른쪽 빈 영역 위치 갱신
                UpdateTabHeaderFillPanel();

                // 탭 색상 다시 그리기
                tabControl1.Invalidate();
            }
        }


        // ============================================================
        // 탭 이름 변경
        // ============================================================

        private void InitRenameEditor()
        {
            txtRenameEditor =
                new TextBox
                {
                    Visible = false,
                    BorderStyle =
                        BorderStyle.FixedSingle
                };

            txtRenameEditor.KeyDown +=
                TxtRenameEditor_KeyDown;

            txtRenameEditor.Leave +=
                (s, ev) =>
                    ApplyTabRename();

            this.Controls.Add(
                txtRenameEditor);
        }

        private void tabControl1_DoubleClick(
            object sender,
            EventArgs e)
        {
            Point clientPoint =
                tabControl1.PointToClient(
                    Cursor.Position);

            for (int i = 0;
                 i < tabControl1.TabPages.Count;
                 i++)
            {
                if (tabControl1.GetTabRect(i)
                        .Contains(clientPoint) &&
                    tabControl1.TabPages[i].Text != "+")
                {
                    StartInlineRename(
                        tabControl1.TabPages[i]);

                    break;
                }
            }
        }

        private void StartInlineRename(
            TabPage tab)
        {
            if (txtRenameEditor == null)
                return;

            editingTab =
                tab;

            Rectangle tabRect =
                tabControl1.GetTabRect(
                    tabControl1.TabPages.IndexOf(
                        tab));

            Point formPoint =
                this.PointToClient(
                    tabControl1.PointToScreen(
                        tabRect.Location));

            txtRenameEditor.Bounds =
                new Rectangle(
                    formPoint.X + 4,
                    formPoint.Y + 3,
                    tabRect.Width - 8,
                    tabRect.Height - 6);

            txtRenameEditor.Text =
                editingTab.Text;

            txtRenameEditor.Visible =
                true;

            txtRenameEditor.BringToFront();

            txtRenameEditor.Focus();

            txtRenameEditor.SelectAll();
        }

        private void TxtRenameEditor_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode ==
                Keys.Enter)
            {
                ApplyTabRename();

                e.SuppressKeyPress =
                    true;
            }
            else if (e.KeyCode ==
                     Keys.Escape)
            {
                txtRenameEditor.Visible =
                    false;

                editingTab =
                    null;
            }
        }

        private void Form_MouseDown_ApplyRename(
            object sender,
            MouseEventArgs e)
        {
            ApplyTabRename();
        }

        private void ApplyTabRename()
        {
            if (editingTab != null &&
                txtRenameEditor != null &&
                txtRenameEditor.Visible)
            {
                bool renamed = false;

                if (!string.IsNullOrWhiteSpace(
                        txtRenameEditor.Text))
                {
                    editingTab.Text =
                        txtRenameEditor.Text;

                    renamed = true;
                }

                txtRenameEditor.Visible =
                    false;

                editingTab =
                    null;

                if (renamed)
                {
                    // 변경된 탭 이름을 DB에 저장
                    SaveTabs();

                    // 탭 글자 길이가 바뀌었을 수 있으므로 빈 영역 위치 갱신
                    UpdateTabHeaderFillPanel();

                    // 탭을 다시 그림
                    tabControl1.Invalidate();
                }
            }
        }


        // ============================================================
        // 실제 탭 생성
        // ============================================================

        private TabPage CreateTabPage(
    string title,
    TabType type,
    int? calendarId = null,
    int? sharedCalendarId = null)
        {
            TabPage newTab = new TabPage(title)
            {
                Tag = new TabData
                {
                    Title = title,
                    Type = type,
                    CalendarId = calendarId,

                    // ★ 이 탭이 연결된 공유 캘린더 ID
                    SharedCalendarId = sharedCalendarId
                }
            };

            Control content;

            switch (type)
            {
                case TabType.Diary:
                    var diaryCtrl =
                        new DiaryControl(
                            loggedInUserId)
                        {
                            Dock =
                                DockStyle.Fill
                        };

                    diaryCtrl.DataChanged +=
                        (s, ev) =>
                            UpdateSummaryView();

                    diaryCtrl.DateOrScheduleChanged +=
                        async (s, ev) =>
                        {
                            currentMonth =
                                diaryCtrl.GetTargetDate();

                            SyncSmallCalendar();

                            // ★ 다이어리에서 날짜가 바뀌어도
                            // 다른 캘린더 탭의 공휴일을 맞춰줌
                            await LoadHolidaysAsync(
                                currentMonth.Year,
                                currentMonth.Month);

                            RefreshAllViews();
                        };

                    content =
                        diaryCtrl;

                    break;


                case TabType.Planner:

                    content =
                        new PlannerControl(
                            loggedInUserId)
                        {
                            Dock =
                                DockStyle.Fill
                        };

                    break;


                case TabType.Timetable:

                    content =
                        new Timetable(
                            loggedInUserId)
                        {
                            Dock =
                                DockStyle.Fill
                        };

                    break;


                case TabType.SharedCalendar:

                    // ====================================================
                    // ★ 이미 이 탭에 연결된 공유방이 있는 경우
                    //
                    // 프로그램을 다시 실행한 경우에는
                    // 참가 코드 입력 화면을 띄우지 않고
                    // 바로 기존 공유 캘린더를 연다.
                    // ====================================================

                    if (sharedCalendarId.HasValue)
                    {
                        var sharedCalendar =
                            CreateSharedCalendarControl(
                                sharedCalendarId.Value);

                        content =
                            sharedCalendar;
                    }
                    else
                    {
                        // =================================================
                        // 아직 연결된 공유방이 없는 새 탭
                        // → 생성 / 참가 화면 표시
                        // =================================================

                        var sharedEnter =
                            new SharedCalendarEnter(
                                loggedInUserId)
                            {
                                Dock =
                                    DockStyle.Fill
                            };


                        sharedEnter.SharedCalendarEntered +=
                            enteredSharedCalendarId =>
                            {
                                if (newTab.Tag is TabData tabData)
                                {
                                    tabData.SharedCalendarId =
                                        enteredSharedCalendarId;
                                }
                                // 실제 공유 캘린더 열기
                                OpenSharedCalendar(
                                    newTab,
                                    enteredSharedCalendarId);


                                // -----------------------------------------
                                // ★ DB에 즉시 저장
                                //
                                // 여기서 저장해야 프로그램을 바로 종료해도
                                // 다음 실행 때 방이 유지된다.
                                // -----------------------------------------

                                SaveTabs();
                            };


                        content =
                            sharedEnter;
                    }

                    break;


                case TabType.Calendar:
                default:

                    // calendarId가 반드시 있어야 함
                    if (calendarId is null)
                    {
                        throw new InvalidOperationException(
                            "개인 캘린더의 calendar_id가 없습니다.");
                    }

                    var calCtrl =
                        new CalendarControl(
                            loggedInUserId,
                            calendarId.Value)
                        {
                            Dock = DockStyle.Fill
                        };

                    calCtrl.SetHolidayMap(
                        holidayMap);

                    calCtrl.DateOrScheduleChanged +=
                        async (s, ev) =>
                        {
                            currentMonth =
                                calCtrl.GetTargetDate();

                            SyncSmallCalendar();

                            // ★ 이동한 날짜의 월 공휴일 다시 불러오기
                            await LoadHolidaysAsync(
                                currentMonth.Year,
                                currentMonth.Month);

                            LoadSavedDday();

                            RefreshAllViews();
                        };

                    content = calCtrl;

                    break;
            }


            newTab.Controls.Add(
                content);

            tabControl1.TabPages.Add(
                newTab);


            // ========================================================
            // 새로 만든 탭에 현재 선택된 테마 적용
            // ========================================================

            UiThemeService.ApplyTheme(
                newTab);

            if (content is CalendarControl calendarControl)
            {
                calendarControl.ApplyCurrentTheme();
            }
            else if (content is DiaryControl diaryControl)
            {
                diaryControl.ApplyCurrentTheme();
            }
            else if (content is PlannerControl plannerControl)
            {
                plannerControl.ApplyCurrentTheme();
            }
            else if (content is Timetable timetable)
            {
                timetable.ApplyCurrentTheme();
            }


            return newTab;
        }


        // ============================================================
        // 탭 저장
        // ============================================================

        private void SaveTabs()
        {
            try
            {
                var tabs =
                    tabControl1.TabPages
                        .Cast<TabPage>()
                        .Where(tab => tab.Text != "+")
                        .Select(tab =>
                        {
                            var data =
                                tab.Tag as TabData;

                            return new TabData
                            {
                                Title =
                                    tab.Text,

                                Type =
                                    data?.Type
                                    ?? TabType.Calendar,

                                CalendarId =
                                 data?.CalendarId,

                                // ★ 공유 캘린더 ID도 DB에 저장
                                SharedCalendarId =
                                data?.SharedCalendarId
                            };
                        })
                        .ToList();

                tabDbRepository.Save(
                    loggedInUserId,
                    tabs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"탭 정보를 DB에 저장하지 못했습니다.\n\n{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // 탭 불러오기
        // ============================================================

        private void LoadTabs()
        {
            List<TabData> tabs;

            try
            {
                tabs =
                    tabDbRepository.Load(
                        loggedInUserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"탭 정보를 DB에서 불러오지 못했습니다.\n\n{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                tabs =
                    new List<TabData>();
            }


            if (tabs.Count == 0)
            {
                SetupDefaultFirstTab();

                // 처음 생성한 기본 탭도 DB에 바로 저장
                SaveTabs();
            }
            else
            {
                tabControl1.TabPages.Clear();

                foreach (var tab in tabs)
                {
                    CreateTabPage(
                        tab.Title,
                        tab.Type,
                        tab.CalendarId,
                        tab.SharedCalendarId);
                }
            }

            tabControl1.TabPages.Add(
                new TabPage("+"));

            // 이미 헤더 채움 패널이 만들어진 상태라면 위치 갱신
            UpdateTabHeaderFillPanel();
        }

        private void SetupDefaultFirstTab()
        {
            tabControl1.TabPages.Clear();

            int? calendarId =
                calendarDbRepository.GetFirstCalendarId(
                    loggedInUserId);

            // DB에 기본 캘린더 공간이 아직 없으면 하나 생성
            if (calendarId is null)
            {
                calendarId =
                    calendarDbRepository.CreateCalendar(
                        loggedInUserId,
                        "기본 캘린더");
            }

            CreateTabPage(
                "기본 캘린더",
                TabType.Calendar,
                calendarId);
        }

        private void btnMy_Click(object sender, EventArgs e)
        {
            // 마이페이지에서 회원탈퇴가 진행되는 동안
            // 공유캘린더 타이머가 DB를 다시 조회하지 않도록 일시 정지
            sharedCalendarMemberCheckTimer.Stop();

            using var mypage =
                new Mypage(loggedInUserId);

            DialogResult result =
                mypage.ShowDialog(this);

            // 회원탈퇴한 경우
            if (result == DialogResult.Abort)
            {
                isAccountDeleted = true;

                sharedCalendarMemberCheckTimer.Stop();

                alarmManager?.Dispose();

                this.Dispose();

                Login login =
                    new Login();

                login.Show();

                // 절대로 this.Show() 하지 않음
                return;
            }

            // 단순히 마이페이지만 닫은 경우 다시 감시 시작
            if (!isAccountDeleted)
            {
                sharedCalendarMemberCheckTimer.Start();
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }


        private void menuThemeLight_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedTheme(
                AppTheme.Light);
        }

        private void menuThemeDark_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedTheme(
                AppTheme.Dark);
        }

        private void menuThemeBlossom_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedTheme(
                AppTheme.Blossom);
        }

        private void menuThemeMint_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedTheme(
                AppTheme.Mint);
        }

        private void menuThemeLavender_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedTheme(
                AppTheme.Lavender);
        }

        private void menuThemeCozy_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedTheme(
                AppTheme.Cozy);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            SearchActiveTab();
        }

        private void SearchActiveTab()
        {
            string keyword = tb_search.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show(
                    "검색할 내용을 입력해주세요.",
                    "검색",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                tb_search.Focus();
                return;
            }

            if (tabControl1.SelectedTab?.Controls.Count is not > 0)
                return;

            Control activeControl =
                tabControl1.SelectedTab.Controls[0];

            List<SearchResultItem> results;
            string searchScope;

            if (activeControl is CalendarControl calendarControl)
            {
                searchScope = "기본 캘린더";

                results =
                    SearchCalendar(
                        calendarControl,
                        keyword);
            }
            else if (activeControl is DiaryControl diaryControl)
            {
                searchScope = "다이어리";

                results =
                    SearchDiary(
                        diaryControl,
                        keyword);
            }
            else
            {
                MessageBox.Show(
                    "기본 캘린더 또는 다이어리 탭에서 검색해주세요.",
                    "검색할 수 없는 탭",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (results.Count == 0)
            {
                MessageBox.Show(
                    $"{searchScope}에서 ‘{keyword}’와 관련된 내용을 찾지 못했습니다.",
                    "검색 결과 없음",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using var dialog =
                new SearchResultsForm(
                    searchScope,
                    keyword,
                    results);

            if (dialog.ShowDialog(this) != DialogResult.OK ||
                dialog.SelectedResult is null)
            {
                return;
            }

            MoveToSearchResult(
                dialog.SelectedResult.Date);
        }
        private static List<SearchResultItem> SearchCalendar(CalendarControl calendarControl, string keyword)
        {
            return calendarControl
                .GetScheduleMap()
                .SelectMany(
                    pair =>
                        pair.Value.Select(
                            schedule => new
                            {
                                Date = pair.Key.Date,
                                Schedule = schedule
                            }))
                .Where(
                    item =>
                        item.Schedule.Text.Contains(
                            keyword,
                            StringComparison.OrdinalIgnoreCase))
                .OrderBy(item => item.Date)
                .ThenBy(item => item.Schedule.StartHour)
                .Select(
                    item =>
                        new SearchResultItem(
                            item.Date,
                            item.Schedule.Text,
                            $"{item.Date:yyyy년 M월 d일}  " +
                            $"{item.Schedule.StartHour:00}:00~{item.Schedule.EndHour:00}:00"))
                .ToList();
        }
        private static List<SearchResultItem> SearchDiary(DiaryControl diaryControl, string keyword)
        {
            return diaryControl
                .GetDiaryMap()
                .Select(
                    pair => new
                    {
                        Date =
                            DateTime.TryParse(
                                pair.Key,
                                out DateTime date)
                                ? date.Date
                                : DateTime.MinValue,

                        Diary = pair.Value
                    })
                .Where(
                    item =>
                        item.Date != DateTime.MinValue &&
                        (
                            item.Diary.Title.Contains(
                                keyword,
                                StringComparison.OrdinalIgnoreCase)
                            ||
                            item.Diary.Content.Contains(
                                keyword,
                                StringComparison.OrdinalIgnoreCase)
                        ))
                .OrderBy(item => item.Date)
                .Select(
                    item =>
                        new SearchResultItem(
                            item.Date,

                            string.IsNullOrWhiteSpace(
                                item.Diary.Title)
                                ? "[제목 없음]"
                                : item.Diary.Title,

                            $"{item.Date:yyyy년 M월 d일}  " +
                            CreateSearchPreview(
                                item.Diary.Content)))
                .ToList();
        }
        private static string CreateSearchPreview(string content)
        {
            string preview =
                content
                    .Replace("\r", " ")
                    .Replace("\n", " ")
                    .Trim();

            return preview.Length > 60
                ? preview[..60] + "…"
                : preview;
        }
        private void MoveToSearchResult(DateTime date)
        {
            if (tabControl1.SelectedTab?.Controls.Count is not > 0)
                return;

            currentMonth =
                date.Date;

            SyncSmallCalendar();

            Control activeControl =
                tabControl1.SelectedTab.Controls[0];

            if (activeControl is CalendarControl calendarControl)
            {
                calendarControl.SetTargetDate(
                    currentMonth);

                calendarControl.SetViewMode(
                    CalendarControl.CalendarViewMode.Day);
            }
            else if (activeControl is DiaryControl diaryControl)
            {
                diaryControl.SetTargetDate(
                    currentMonth);

                diaryControl.SetViewMode(
                    CalendarControl.CalendarViewMode.Day);
            }

            UpdateCalendarViewMenu();
            UpdateCalendarTitle();
            UpdateSummaryView();
        }

        private void LoadSavedDday()
        {
            try
            {
                using var connection =
                    new DBConnection().GetConnection();

                connection.Open();

                const string sql = @"
            SELECT date, content, type
            FROM user_dday
            WHERE user_id = @user_id
            ORDER BY dday_id DESC
            LIMIT 1";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue(
                    "@user_id",
                    loggedInUserId);

                using var reader =
                    command.ExecuteReader();

                // 저장된 D-Day가 하나도 없는 경우
                if (!reader.Read())
                {
                    // D-Day가 없으면 화면에는 아무것도 표시하지 않음
                    lbDday.Text = "";
                    return;
                }

                DateTime targetDate =
                    reader.GetDateTime("date");

                string title =
                    reader.GetString("content");

                int type =
                    reader.GetInt32("type");

                bool startFromOne =
                    type == 1;


                // 오늘 날짜와 D-Day 날짜 차이 계산
                int dayDifference =
                    (DateTime.Today - targetDate.Date).Days;

                int dday;

                if (startFromOne)
                {
                    if (dayDifference >= 0)
                        dday = dayDifference + 1;
                    else
                        dday = dayDifference;
                }
                else
                {
                    dday = dayDifference;
                }


                // 화면에 표시
                if (dday > 0)
                {
                    lbDday.Text =
                        $"D+{dday} | {title}";
                }
                else if (dday < 0)
                {
                    lbDday.Text =
                        $"D{dday} | {title}";
                }
                else
                {
                    lbDday.Text =
                        $"D-Day | {title}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"D-Day 정보를 불러오는 중 오류가 발생했습니다.\n\n{ex.Message}",
                    "DB 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btn_Dday_Click(object sender, EventArgs e)
        {
            // 현재 선택된 탭의 정보 가져오기
            if (tabControl1.SelectedTab?.Tag
                is not TabData tabData)
            {
                MessageBox.Show(
                    "기본 캘린더 탭을 선택해주세요.");
                return;
            }

            // 개인 캘린더가 아니거나 calendarId가 없는 경우
            if (tabData.Type != TabType.Calendar ||
                tabData.CalendarId is null)
            {
                MessageBox.Show(
                    "기본 캘린더 탭에서 D-Day를 설정해주세요.");
                return;
            }

            using var form =
                new DdaySettingForm(
                    loggedInUserId,
                    tabData.CalendarId.Value);

            if (form.ShowDialog(this)
                != DialogResult.OK)
            {
                return;
            }

            // ========================================================
            // 중요:
            // D-Day 설정/해제 후에는 form.SelectedDate를 직접 계산하지 않고
            // DB를 다시 읽어서 메인 라벨을 갱신한다.
            //
            // D-Day를 해제한 경우 SelectedDate가 DateTime.MinValue 상태일 수 있어
            // 직접 계산하면 D+739841 같은 잘못된 값이 나타날 수 있다.
            // ========================================================
            LoadSavedDday();

            // D-Day 변경 후 개인달력 새로고침
            foreach (TabPage tab
                in tabControl1.TabPages)
            {
                if (tab.Controls.Count > 0 &&
                    tab.Controls[0]
                    is CalendarControl calCtrl)
                {
                    calCtrl.LoadSchedules();
                    calCtrl.UpdateView();
                }
            }

            UpdateSummaryView();
        }

        private void 맑은고딕ToolStripMenuItem_Click(
    object sender,
    EventArgs e)
        {
            ApplySelectedFont(
                AppFontType.MalgunGothic);
        }

        private void 바탕체ToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedFont(
                AppFontType.Batang);
        }

        private void 돋움ToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedFont(
                AppFontType.Dotum);
        }

        private void 한컴말랑말랑ToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedFont(
                AppFontType.HancomMalang);
        }

        private void 훈민정음가로쓰기ToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedFont(
                AppFontType.HunminHorizontal);
        }

        private void 한컴산뜻돋움ToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            ApplySelectedFont(
                AppFontType.HancomSanzDotum);
        }
        // ============================================================
        // 탭 헤더 오른쪽 빈 영역 처리
        // ============================================================

        private void InitializeTabHeaderFillPanel()
        {
            if (tabHeaderFillPanel != null)
                return;

            tabHeaderFillPanel =
                new Panel
                {
                    BackColor =
                        UiThemeService.BackgroundColor,

                    TabStop =
                        false
                };

            Controls.Add(
                tabHeaderFillPanel);

            tabControl1.SizeChanged +=
                (_, _) =>
                    UpdateTabHeaderFillPanel();

            UpdateTabHeaderFillPanel();
        }


        private void UpdateTabHeaderFillPanel()
        {
            if (tabHeaderFillPanel == null ||
                tabControl1 == null ||
                tabControl1.TabPages.Count == 0 ||
                !tabControl1.IsHandleCreated)
            {
                return;
            }

            int lastIndex =
                tabControl1.TabPages.Count - 1;

            Rectangle lastTabRect =
                tabControl1.GetTabRect(
                    lastIndex);

            // 마지막 탭 오른쪽 끝
            Point startPoint =
                PointToClient(
                    tabControl1.PointToScreen(
                        new Point(
                            lastTabRect.Right,
                            lastTabRect.Top)));

            // TabControl 전체 오른쪽 끝
            Point rightPoint =
                PointToClient(
                    tabControl1.PointToScreen(
                        new Point(
                            tabControl1.ClientSize.Width,
                            lastTabRect.Top)));

            int width =
                Math.Max(
                    0,
                    rightPoint.X -
                    startPoint.X);

            if (width <= 0)
            {
                tabHeaderFillPanel.Visible =
                    false;

                return;
            }

            tabHeaderFillPanel.Visible =
                true;

            tabHeaderFillPanel.BackColor =
                UiThemeService.BackgroundColor;

            tabHeaderFillPanel.Bounds =
                new Rectangle(
                    startPoint.X,
                    startPoint.Y,
                    width,
                    lastTabRect.Height);

            tabHeaderFillPanel.BringToFront();
        }


        // ============================================================
        // 탭 버튼 직접 그리기
        // ============================================================

        private void tabControl1_DrawItem(
            object sender,
            DrawItemEventArgs e)
        {
            TabPage tabPage =
                tabControl1.TabPages[e.Index];

            Rectangle rect =
                tabControl1.GetTabRect(e.Index);

            bool isSelected =
                e.Index ==
                tabControl1.SelectedIndex;

            Color backColor;
            Color textColor;


            if (isSelected)
            {
                // 선택된 탭
                backColor =
                    UiThemeService.PrimaryColor;

                textColor =
                    UiThemeService.CurrentTheme ==
                    AppTheme.Dark
                        ? Color.White
                        : Color.FromArgb(
                            45,
                            45,
                            55);
            }
            else
            {
                // 선택되지 않은 탭
                backColor =
                    UiThemeService.CurrentTheme switch
                    {
                        AppTheme.Dark =>
                            Color.FromArgb(
                                48,
                                48,
                                52),

                        AppTheme.Blossom =>
                            Color.FromArgb(
                                252,
                                235,
                                241),

                        AppTheme.Mint =>
                            Color.FromArgb(
                                232,
                                246,
                                240),

                        AppTheme.Lavender =>
                            Color.FromArgb(
                                239,
                                233,
                                248),

                        AppTheme.Cozy =>
                            Color.FromArgb(
                                244,
                                235,
                                222),

                        _ =>
                            Color.FromArgb(
                                240,
                                242,
                                246)
                    };

                textColor =
                    UiThemeService.TextColor;
            }


            using (SolidBrush backBrush =
                   new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(
                    backBrush,
                    rect);
            }


            TextRenderer.DrawText(
                e.Graphics,
                tabPage.Text,
                tabControl1.Font,
                rect,
                textColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter);


            // 선택된 탭 아래 포인트 선
            if (isSelected)
            {
                using (Pen pen =
                       new Pen(
                           UiThemeService.PrimaryColor,
                           2))
                {
                    e.Graphics.DrawLine(
                        pen,
                        rect.Left,
                        rect.Bottom - 1,
                        rect.Right,
                        rect.Bottom - 1);
                }
            }
        }
        // ============================================================
        // 공유 캘린더 컨트롤 생성
        // ============================================================

        private CalendarControl CreateSharedCalendarControl(
            int sharedCalendarId)
        {
            var sharedCalendar =
                new CalendarControl(
                    loggedInUserId,
                    sharedCalendarId,
                    CalendarControl.CalendarDataMode.Shared)
                {
                    Dock =
                        DockStyle.Fill
                };


            sharedCalendar.SetHolidayMap(
                holidayMap);


            sharedCalendar.DateOrScheduleChanged +=
                async (s, ev) =>
                {
                    currentMonth =
                        sharedCalendar.GetTargetDate();

                    SyncSmallCalendar();

                    // ★ 공유 캘린더에서도 월이 바뀌면 공휴일 갱신
                    await LoadHolidaysAsync(
                        currentMonth.Year,
                        currentMonth.Month);

                    RefreshAllViews();
                };


            sharedCalendar.ApplyCurrentTheme();

            sharedCalendar.ApplyCurrentFont();


            return sharedCalendar;
        }

        private void OpenSharedCalendar(
    TabPage tab,
    int sharedCalendarId)
        {
            // ========================================================
            // ★ 이 탭이 어떤 공유방인지 기억
            // ========================================================

            if (tab.Tag is TabData tabData)
            {
                tabData.SharedCalendarId =
                    sharedCalendarId;
            }


            // 기존 입장 화면 제거
            tab.Controls.Clear();


            // 공용 메서드로 공유 캘린더 생성
            CalendarControl sharedCalendar =
                CreateSharedCalendarControl(
                    sharedCalendarId);


            tab.Controls.Add(
                sharedCalendar);


            // ========================================================
            // 공유방 이름으로 탭 이름 변경
            // ========================================================

            string? roomName =
                new SharedCalendarRepository()
                    .GetRoomName(
                        sharedCalendarId);


            if (!string.IsNullOrWhiteSpace(
                    roomName))
            {
                tab.Text =
                    roomName;

                // Tag의 제목도 맞춰준다.
                if (tab.Tag is TabData data)
                {
                    data.Title =
                        roomName;
                }
            }


            tabControl1.Invalidate();

            if (tabControl1.SelectedTab == tab)
            {
                btnChat.Visible = true;
            }
        }

        private void btnChat_Click(
    object sender,
    EventArgs e)
        {
            // 현재 선택된 탭이 있는지 확인
            if (tabControl1.SelectedTab == null)
                return;


            // 현재 탭의 정보 가져오기
            if (tabControl1.SelectedTab.Tag
                is not TabData tabData)
            {
                return;
            }


            // 공유 캘린더가 아니면 실행하지 않음
            if (tabData.Type != TabType.SharedCalendar)
                return;


            // 아직 실제 방에 입장하지 않은 상태면 실행하지 않음
            if (!tabData.SharedCalendarId.HasValue)
                return;


            // 현재 공유 캘린더의 채팅창 열기
            ShowChatForm(
                tabData.SharedCalendarId.Value);
        }

        private void lbName_Click(object sender, EventArgs e)
        {
            // 이름을 눌러 마이페이지를 연 경우에도
            // 회원탈퇴 중 공유캘린더 타이머가 실행되지 않도록 정지
            sharedCalendarMemberCheckTimer.Stop();

            using var mypageForm =
                new Mypage(loggedInUserId);

            DialogResult result =
                mypageForm.ShowDialog(this);

            // 회원탈퇴한 경우
            if (result == DialogResult.Abort)
            {
                isAccountDeleted = true;

                sharedCalendarMemberCheckTimer.Stop();

                alarmManager?.Dispose();

                this.Dispose();

                Login login =
                    new Login();

                login.Show();

                return;
            }

            // 단순히 마이페이지만 닫은 경우 다시 감시 시작
            if (!isAccountDeleted)
            {
                sharedCalendarMemberCheckTimer.Start();
            }
        }
        private void LoadUserName()
        {
            try
            {
                using (var conn = new DBConnection().GetConnection())
                {
                    conn.Open();

                    string sql = "SELECT name FROM user WHERE user_id = @user_id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", loggedInUserId);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            lbName.Text = $"{result}님";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("사용자 이름을 불러오지 못했습니다.\n" + ex.Message);
            }
        }

        private void 공유캘린더ToolStripMenuItem_Click(
    object sender,
    EventArgs e)
        {
            if (tabControl1.SelectedTab == null)
            {
                return;
            }

            if (tabControl1.SelectedTab.Tag
                is not TabData tabData)
            {
                return;
            }

            if (tabData.Type !=
                TabType.SharedCalendar)
            {
                MessageBox.Show(
                    "공유 캘린더 탭에서 사용해주세요.",
                    "공유 캘린더 관리",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (!tabData.SharedCalendarId.HasValue)
            {
                MessageBox.Show(
                    "아직 공유 캘린더에 입장하지 않았습니다.",
                    "공유 캘린더 관리",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int sharedCalendarId =
                tabData.SharedCalendarId.Value;


            // ============================================================
            // 관리창이 열려있는 동안 강퇴 감지 Timer 정지
            // ============================================================

            sharedCalendarMemberCheckTimer.Stop();


            using var manager =
                new SharecalendarManager(
                    sharedCalendarId,
                    loggedInUserId);


            DialogResult result =
                manager.ShowDialog(this);


            // ============================================================
            // 관리자가 공유 캘린더 자체를 삭제한 경우
            // ============================================================

            if (result ==
                DialogResult.Abort)
            {
                TabPage? currentTab =
                    tabControl1.SelectedTab;


                if (currentTab != null &&
                    currentTab.Tag
                        is TabData currentTabData &&
                    currentTabData.Type ==
                        TabType.SharedCalendar &&
                    currentTabData.SharedCalendarId ==
                        sharedCalendarId)
                {
                    tabControl1.TabPages.Remove(
                        currentTab);

                    currentTab.Dispose();


                    SaveTabs();

                    UpdateTabHeaderFillPanel();

                    tabControl1.Invalidate();
                }


                // 방 자체를 삭제했으므로
                // 이 과정에서는 Timer를 바로 다시 시작할 필요 없음
                // 아래에서 정상적으로 다시 시작
            }


            // ============================================================
            // 관리창을 닫은 후 다시 강퇴/공유일정 감시 시작
            // ============================================================

            if (!isAccountDeleted)
            {
                sharedCalendarMemberCheckTimer.Start();
            }
        }
    }
}