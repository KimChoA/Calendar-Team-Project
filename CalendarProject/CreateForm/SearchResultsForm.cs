using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace calendar4
{
    internal partial class SearchResultsForm : Form
    {
        private readonly List<SearchResultItem> results;

        public SearchResultItem? SelectedResult =>
            listResults.SelectedItem as SearchResultItem;


        // 디자이너용
        public SearchResultsForm()
        {
            InitializeComponent();

            results =
                new List<SearchResultItem>();

            InitializeResultList();
        }


        // 실제 사용
        public SearchResultsForm(
            string searchScope,
            string keyword,
            IReadOnlyCollection<SearchResultItem> results)
        {
            InitializeComponent();

            this.results =
                results.ToList();

            Text =
                $"{searchScope} 검색 결과";

            lblHeader.Text =
                $"‘{keyword}’ 검색 결과  {results.Count}개";

            InitializeResultList();

            LoadResults();
        }


        // ============================================================
        // ListBox 초기 설정
        // ============================================================

        private void InitializeResultList()
        {
            listResults.DrawMode =
                DrawMode.OwnerDrawFixed;

            listResults.ItemHeight =
                48;

            listResults.IntegralHeight =
                false;

            listResults.HorizontalScrollbar =
                true;

            listResults.DrawItem -=
                listResults_DrawItem;

            listResults.DrawItem +=
                listResults_DrawItem;

            listResults.DoubleClick -=
                listResults_DoubleClick;

            listResults.DoubleClick +=
                listResults_DoubleClick;
        }


        // ============================================================
        // 결과 넣기
        // ============================================================

        private void LoadResults()
        {
            listResults.BeginUpdate();

            try
            {
                listResults.Items.Clear();

                foreach (var item in results)
                {
                    listResults.Items.Add(item);
                }
            }
            finally
            {
                listResults.EndUpdate();
            }

            if (listResults.Items.Count > 0)
            {
                listResults.SelectedIndex = 0;
            }
        }


        // ============================================================
        // 선택 버튼
        // ============================================================

        private void btnSelect_Click(
            object sender,
            EventArgs e)
        {
            SelectCurrentResult();
        }


        private void SelectCurrentResult()
        {
            if (SelectedResult == null)
            {
                MessageBox.Show(
                    "이동할 검색 결과를 선택해주세요.",
                    "검색 결과",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DialogResult =
                DialogResult.OK;

            Close();
        }


        // ============================================================
        // 취소
        // ============================================================

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }


        // ============================================================
        // 더블클릭
        // ============================================================

        private void listResults_DoubleClick(
            object? sender,
            EventArgs e)
        {
            SelectCurrentResult();
        }


        // ============================================================
        // 검색 결과 직접 그리기
        // ============================================================

        private void listResults_DrawItem(
            object? sender,
            DrawItemEventArgs e)
        {
            if (e.Index < 0 ||
                e.Index >= listResults.Items.Count)
            {
                return;
            }

            e.DrawBackground();

            if (listResults.Items[e.Index]
                is not SearchResultItem item)
            {
                return;
            }

            Color textColor =
                (e.State & DrawItemState.Selected) != 0
                    ? SystemColors.HighlightText
                    : SystemColors.ControlText;


            Rectangle titleBounds =
                new Rectangle(
                    e.Bounds.X + 8,
                    e.Bounds.Y + 4,
                    e.Bounds.Width - 16,
                    20);


            Rectangle detailBounds =
                new Rectangle(
                    e.Bounds.X + 8,
                    e.Bounds.Y + 25,
                    e.Bounds.Width - 16,
                    18);


            using Font titleFont =
                new Font(
                    listResults.Font,
                    FontStyle.Bold);


            TextRenderer.DrawText(
                e.Graphics,
                item.Title,
                titleFont,
                titleBounds,
                textColor,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis);


            TextRenderer.DrawText(
                e.Graphics,
                item.Detail,
                listResults.Font,
                detailBounds,
                textColor,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis);


            e.DrawFocusRectangle();
        }
    }
}