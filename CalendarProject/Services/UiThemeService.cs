using System.Drawing;
using System.Windows.Forms;

// ============================================================
// [포트폴리오 담당] 사용자 맞춤형 테마 관리
// - Light/Dark 및 사용자 선택 테마의 공통 색상 규칙 관리
// - 컨트롤 트리를 순회하며 UI에 동일한 테마 규칙 적용
// ============================================================

namespace calendar4
{
    public enum AppTheme
    {
        Light,
        Dark,
        Blossom,
        Mint,
        Lavender,
        Cozy
    }

    public static class UiThemeService
    {
        // [담당 기능] 애플리케이션 전체 테마 상태 관리
        public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;


        // =========================================================
        // 글꼴 일괄 적용
        // 하위 컨트롤까지 재귀적으로 동일 글꼴 적용
        // =========================================================
        public static void ApplyFont(Control parent, Font font)
        {
            parent.Font = font;

            foreach (Control control in parent.Controls)
            {
                ApplyFont(control, font);
            }
        }


        // =========================================================
        // 현재 테마 변경
        // =========================================================
        public static void SetTheme(AppTheme theme)
        {
            CurrentTheme = theme;
        }


        // =========================================================
        // 프리미엄 전용 테마 여부 확인
        // =========================================================
        public static bool IsPremiumTheme(AppTheme theme)
        {
            return theme == AppTheme.Blossom ||
                   theme == AppTheme.Mint ||
                   theme == AppTheme.Lavender ||
                   theme == AppTheme.Cozy;
        }


        // =========================================================
        // 전체 배경색
        // =========================================================
        public static Color BackgroundColor
        {
            get
            {
                return CurrentTheme switch
                {
                    AppTheme.Dark =>
                        Color.FromArgb(30, 30, 30),

                    AppTheme.Blossom =>
                        Color.FromArgb(255, 245, 248),

                    AppTheme.Mint =>
                        Color.FromArgb(242, 252, 248),

                    AppTheme.Lavender =>
                        Color.FromArgb(248, 245, 255),

                    AppTheme.Cozy =>
                        Color.FromArgb(250, 246, 238),

                    _ =>
                        Color.FromArgb(248, 248, 248)
                };
            }
        }


        // =========================================================
        // 패널 / 카드 배경색
        // =========================================================
        public static Color SurfaceColor
        {
            get
            {
                return CurrentTheme switch
                {
                    AppTheme.Dark =>
                        Color.FromArgb(45, 45, 45),

                    AppTheme.Blossom =>
                        Color.FromArgb(255, 230, 238),

                    AppTheme.Mint =>
                        Color.FromArgb(220, 245, 235),

                    AppTheme.Lavender =>
                        Color.FromArgb(235, 228, 250),

                    AppTheme.Cozy =>
                        Color.FromArgb(241, 229, 209),

                    _ =>
                        Color.White
                };
            }
        }


        // =========================================================
        // 버튼 / 포인트 색상
        // =========================================================
        public static Color PrimaryColor
        {
            get
            {
                return CurrentTheme switch
                {
                    AppTheme.Dark =>
                        Color.FromArgb(110, 100, 170),

                    AppTheme.Blossom =>
                        Color.FromArgb(244, 143, 177),

                    AppTheme.Mint =>
                        Color.FromArgb(102, 187, 160),

                    AppTheme.Lavender =>
                        Color.FromArgb(160, 135, 210),

                    AppTheme.Cozy =>
                        Color.FromArgb(190, 150, 110),

                    _ =>
                        Color.FromArgb(120, 140, 210)
                };
            }
        }


        // =========================================================
        // 기본 글자색
        // =========================================================
        public static Color TextColor
        {
            get
            {
                return CurrentTheme switch
                {
                    AppTheme.Dark =>
                        Color.FromArgb(240, 240, 240),

                    AppTheme.Cozy =>
                        Color.FromArgb(80, 65, 55),

                    AppTheme.Blossom =>
                        Color.FromArgb(80, 60, 65),

                    AppTheme.Mint =>
                        Color.FromArgb(55, 75, 70),

                    AppTheme.Lavender =>
                        Color.FromArgb(65, 60, 80),

                    _ =>
                        Color.FromArgb(60, 60, 60)
                };
            }
        }


        // =========================================================
        // TextBox / ComboBox / RichTextBox 배경색
        // =========================================================
        public static Color InputColor
        {
            get
            {
                return CurrentTheme switch
                {
                    AppTheme.Dark =>
                        Color.FromArgb(55, 55, 55),

                    AppTheme.Blossom =>
                        Color.FromArgb(255, 250, 252),

                    AppTheme.Mint =>
                        Color.FromArgb(250, 255, 253),

                    AppTheme.Lavender =>
                        Color.FromArgb(253, 251, 255),

                    AppTheme.Cozy =>
                        Color.FromArgb(255, 252, 246),

                    _ =>
                        Color.White
                };
            }
        }


        // =========================================================
        // 전체 테마 적용
        // =========================================================
        public static void ApplyTheme(Control parent)
        {
            // 현재 컨트롤부터 테마 적용
            ApplyControlTheme(parent);

            // 자식 컨트롤에도 재귀적으로 적용
            foreach (Control control in parent.Controls)
            {
                ApplyTheme(control);
            }
        }


        // =========================================================
        // 컨트롤 종류별 테마 규칙 적용
        // =========================================================
        private static void ApplyControlTheme(Control control)
        {
            // Form
            if (control is Form)
            {
                control.BackColor = BackgroundColor;
                control.ForeColor = TextColor;
            }

            // TabPage / UserControl
            else if (control is TabPage || control is UserControl)
            {
                control.BackColor = BackgroundColor;
                control.ForeColor = TextColor;
            }

            // Panel / GroupBox
            else if (control is Panel || control is GroupBox)
            {
                control.BackColor = SurfaceColor;
                control.ForeColor = TextColor;
            }

            // Button
            else if (control is Button button)
            {
                button.BackColor = PrimaryColor;
                button.ForeColor = TextColor;

                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.UseVisualStyleBackColor = false;
            }

            // TextBox
            else if (control is TextBox textBox)
            {
                textBox.BackColor = InputColor;
                textBox.ForeColor = TextColor;
            }

            // RichTextBox
            else if (control is RichTextBox richTextBox)
            {
                richTextBox.BackColor = InputColor;
                richTextBox.ForeColor = TextColor;
            }

            // ComboBox
            else if (control is ComboBox comboBox)
            {
                comboBox.BackColor = InputColor;
                comboBox.ForeColor = TextColor;
            }

            // Label
            else if (control is Label label)
            {
                label.ForeColor = TextColor;
            }

            // TabControl
            else if (control is TabControl tabControl)
            {
                tabControl.BackColor = BackgroundColor;
                tabControl.ForeColor = TextColor;
            }
        }
    }
}