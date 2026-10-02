using System.Drawing;

// ============================================================
// [포트폴리오 담당] 공유 캘린더 참여자 식별 색상
// - 사용자별 ColorIndex를 실제 표시 색상으로 변환
// - 테마별 색상 팔레트를 분리해 공유 일정의 가독성을 유지
// ============================================================

namespace calendar4.Services
{
    public static class SharedCalendarColorService
    {
        // ============================================================
        // 전체 회원 일정 색상
        //
        // 전체 참여 일정은 항상 빨강 계열
        // 단, 현재 테마에 맞게 밝기만 조절
        // ============================================================

        public static Color GetAllMembersColor()
        {
            return UiThemeService.CurrentTheme switch
            {
                AppTheme.Dark =>
                    Color.FromArgb(
                        240,
                        105,
                        115),

                AppTheme.Blossom =>
                    Color.FromArgb(
                        225,
                        80,
                        105),

                AppTheme.Mint =>
                    Color.FromArgb(
                        220,
                        75,
                        85),

                AppTheme.Lavender =>
                    Color.FromArgb(
                        220,
                        85,
                        105),

                AppTheme.Cozy =>
                    Color.FromArgb(
                        205,
                        80,
                        70),

                _ =>
                    Color.FromArgb(
                        220,
                        70,
                        80)
            };
        }


        // ============================================================
        // 공유 캘린더 회원별 색상
        //
        // colorIndex
        // 1 → 첫 번째 회원
        // 2 → 두 번째 회원
        // 3 → 세 번째 회원
        // ...
        //
        // 모든 공유캘린더에서 같은 번호는 같은 계열 사용
        // ============================================================

        public static Color GetMemberColor(
            int colorIndex)
        {
            if (colorIndex < 1)
            {
                colorIndex = 1;
            }


            return UiThemeService.CurrentTheme switch
            {
                AppTheme.Dark =>
                    GetDarkColor(
                        colorIndex),

                AppTheme.Blossom =>
                    GetBlossomColor(
                        colorIndex),

                AppTheme.Mint =>
                    GetMintColor(
                        colorIndex),

                AppTheme.Lavender =>
                    GetLavenderColor(
                        colorIndex),

                AppTheme.Cozy =>
                    GetCozyColor(
                        colorIndex),

                _ =>
                    GetDefaultColor(
                        colorIndex)
            };
        }


        // ============================================================
        // 기본 테마
        // ============================================================

        private static Color GetDefaultColor(
            int index)
        {
            Color[] colors =
            {
                Color.FromArgb(70, 125, 235),   // 1 파랑
                Color.FromArgb(60, 175, 125),   // 2 초록
                Color.FromArgb(150, 95, 220),   // 3 보라
                Color.FromArgb(235, 150, 55),   // 4 주황
                Color.FromArgb(45, 175, 190),   // 5 청록
                Color.FromArgb(215, 95, 160),   // 6 핑크
                Color.FromArgb(125, 145, 65),   // 7 올리브
                Color.FromArgb(105, 120, 165)   // 8 남청
            };

            return GetColorFromArray(
                colors,
                index);
        }


        // ============================================================
        // 다크 테마
        // ============================================================

        private static Color GetDarkColor(
            int index)
        {
            Color[] colors =
            {
                Color.FromArgb(105, 165, 255),
                Color.FromArgb(90, 205, 150),
                Color.FromArgb(185, 135, 255),
                Color.FromArgb(255, 180, 95),
                Color.FromArgb(80, 205, 220),
                Color.FromArgb(245, 130, 190),
                Color.FromArgb(175, 190, 105),
                Color.FromArgb(145, 160, 215)
            };

            return GetColorFromArray(
                colors,
                index);
        }


        // ============================================================
        // Blossom 테마
        // ============================================================

        private static Color GetBlossomColor(
            int index)
        {
            Color[] colors =
            {
                Color.FromArgb(90, 135, 215),
                Color.FromArgb(90, 170, 135),
                Color.FromArgb(160, 110, 205),
                Color.FromArgb(220, 150, 85),
                Color.FromArgb(75, 165, 175),
                Color.FromArgb(205, 100, 155),
                Color.FromArgb(135, 155, 85),
                Color.FromArgb(115, 125, 175)
            };

            return GetColorFromArray(
                colors,
                index);
        }


        // ============================================================
        // Mint 테마
        // ============================================================

        private static Color GetMintColor(
            int index)
        {
            Color[] colors =
            {
                Color.FromArgb(70, 135, 205),
                Color.FromArgb(55, 165, 120),
                Color.FromArgb(145, 105, 200),
                Color.FromArgb(215, 145, 65),
                Color.FromArgb(45, 160, 170),
                Color.FromArgb(200, 100, 145),
                Color.FromArgb(120, 150, 75),
                Color.FromArgb(100, 125, 165)
            };

            return GetColorFromArray(
                colors,
                index);
        }


        // ============================================================
        // Lavender 테마
        // ============================================================

        private static Color GetLavenderColor(
            int index)
        {
            Color[] colors =
            {
                Color.FromArgb(85, 135, 225),
                Color.FromArgb(75, 165, 125),
                Color.FromArgb(155, 105, 220),
                Color.FromArgb(220, 145, 75),
                Color.FromArgb(65, 165, 180),
                Color.FromArgb(210, 100, 165),
                Color.FromArgb(130, 150, 80),
                Color.FromArgb(105, 120, 180)
            };

            return GetColorFromArray(
                colors,
                index);
        }


        // ============================================================
        // Cozy 테마
        // ============================================================

        private static Color GetCozyColor(
            int index)
        {
            Color[] colors =
            {
                Color.FromArgb(75, 125, 185),
                Color.FromArgb(85, 150, 105),
                Color.FromArgb(140, 105, 175),
                Color.FromArgb(195, 130, 65),
                Color.FromArgb(65, 145, 150),
                Color.FromArgb(180, 95, 125),
                Color.FromArgb(125, 140, 75),
                Color.FromArgb(105, 110, 145)
            };

            return GetColorFromArray(
                colors,
                index);
        }


        // ============================================================
        // 배열에서 색 가져오기
        //
        // 8명이 넘어가도 오류 안 나도록 반복 사용
        //
        // 1 → colors[0]
        // 2 → colors[1]
        // ...
        // 8 → colors[7]
        // 9 → colors[0]
        // ============================================================

        private static Color GetColorFromArray(
            Color[] colors,
            int colorIndex)
        {
            int arrayIndex =
                (colorIndex - 1) %
                colors.Length;

            return colors[arrayIndex];
        }
    }
}