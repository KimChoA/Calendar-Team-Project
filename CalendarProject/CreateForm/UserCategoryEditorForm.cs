using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace calendar4
{
    public partial class UserCategoryEditorForm : Form
    {
        private readonly UserCategory? existingCategory;

        public UserCategory Category { get; private set; } = null!;


        // ============================================================
        // 디자이너용 기본 생성자
        // ============================================================

        public UserCategoryEditorForm()
        {
            InitializeComponent();

            existingCategory = null;

            pnlColor.BackColor =
                Color.CornflowerBlue;
        }


        // ============================================================
        // 새 카테고리 추가용
        // ============================================================

        public UserCategoryEditorForm(
            string title)
        {
            InitializeComponent();

            existingCategory = null;

            Text = title;

            pnlColor.BackColor =
                Color.CornflowerBlue;
        }


        // ============================================================
        // 기존 카테고리 수정용
        // ============================================================

        public UserCategoryEditorForm(
            UserCategory category)
        {
            InitializeComponent();

            existingCategory = category;

            Text = "카테고리 수정";

            txtName.Text =
                category.Name;

            pnlColor.BackColor =
                Color.FromArgb(
                    category.ColorArgb);
        }


        // ============================================================
        // 색상 선택
        // ============================================================

        private void btnColor_Click(
            object sender,
            EventArgs e)
        {
            using var colorDialog =
                new ColorDialog
                {
                    Color =
                        pnlColor.BackColor,

                    FullOpen =
                        true
                };


            if (colorDialog.ShowDialog(this)
                != DialogResult.OK)
            {
                return;
            }


            pnlColor.BackColor =
                colorDialog.Color;
        }


        // ============================================================
        // 저장
        // ============================================================

        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            string name =
                txtName.Text.Trim();


            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "카테고리 이름을 입력해주세요.",
                    "입력 확인",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtName.Focus();

                return;
            }


            Category =
                new UserCategory
                {
                    Id =
                        existingCategory?.Id
                        ?? Guid.NewGuid().ToString(),

                    Name =
                        name,

                    ColorArgb =
                        pnlColor.BackColor.ToArgb(),

                    IsDefault =
                        existingCategory?.IsDefault
                        ?? false
                };


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
    }
}
