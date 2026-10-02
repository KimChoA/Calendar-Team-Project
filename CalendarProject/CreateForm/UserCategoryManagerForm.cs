using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace calendar4
{
    public partial class UserCategoryManagerForm : Form
    {
        private readonly UserCategoryStore store;
        private readonly List<UserCategory> categories;

        // 디자이너에서 열기 위한 기본 생성자
        public UserCategoryManagerForm()
        {
            InitializeComponent();

            store = PersonalCategoryStores.Calendar;
            categories = store.Categories
                .Select(CopyCategory)
                .ToList();

            InitializeList();
            RefreshList();
        }

        // 실제 프로그램에서 사용하는 생성자
        public UserCategoryManagerForm(
            string title,
            UserCategoryStore store)
        {
            InitializeComponent();

            this.store = store;

            categories = store.Categories
                .Select(CopyCategory)
                .ToList();

            Text = title;

            InitializeList();
            RefreshList();
        }

        private void InitializeList()
        {
            list.DrawMode = DrawMode.OwnerDrawFixed;
            list.ItemHeight = 34;

            list.DrawItem -= DrawCategory;
            list.DrawItem += DrawCategory;

            list.DoubleClick -= List_DoubleClick;
            list.DoubleClick += List_DoubleClick;
        }

        private void List_DoubleClick(
            object? sender,
            EventArgs e)
        {
            EditSelected();
        }

        private static UserCategory CopyCategory(
            UserCategory item)
        {
            return new UserCategory
            {
                Id = item.Id,
                Name = item.Name,
                ColorArgb = item.ColorArgb,
                IsDefault = item.IsDefault
            };
        }

        private void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            AddCategory();
        }

        private void AddCategory()
        {
            using var editor =
                new UserCategoryEditorForm();

            if (editor.ShowDialog(this) != DialogResult.OK)
                return;

            if (HasDuplicateName(editor.Category.Name))
            {
                MessageBox.Show(
                    "같은 이름의 카테고리가 이미 있습니다.",
                    "카테고리 확인",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            categories.Add(
                CopyCategory(editor.Category));

            RefreshList(
                editor.Category.Id);
        }

        private void btnEdit_Click(
            object sender,
            EventArgs e)
        {
            EditSelected();
        }

        private void EditSelected()
        {
            if (list.SelectedItem is not UserCategory selected)
            {
                MessageBox.Show(
                    "수정할 카테고리를 선택해주세요.",
                    "카테고리 수정",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using var editor =
                new UserCategoryEditorForm(
                    selected);

            if (editor.ShowDialog(this) != DialogResult.OK)
                return;

            if (HasDuplicateName(
                    editor.Category.Name,
                    selected.Id))
            {
                MessageBox.Show(
                    "같은 이름의 카테고리가 이미 있습니다.",
                    "카테고리 확인",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int index =
                categories.FindIndex(
                    item => item.Id == selected.Id);

            if (index >= 0)
            {
                categories[index] =
                    CopyCategory(editor.Category);
            }

            RefreshList(
                editor.Category.Id);
        }

        private bool HasDuplicateName(
            string name,
            string? exceptId = null)
        {
            return categories.Any(
                item =>
                    item.Id != exceptId &&
                    string.Equals(
                        item.Name,
                        name,
                        StringComparison.OrdinalIgnoreCase));
        }

        private void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            if (list.SelectedItem is not UserCategory selected)
            {
                MessageBox.Show(
                    "삭제할 카테고리를 선택해주세요.",
                    "카테고리 삭제",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (selected.IsDefault)
            {
                MessageBox.Show(
                    "기본 카테고리는 삭제할 수 없습니다.",
                    "카테고리 삭제",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (MessageBox.Show(
                    $"'{selected.Name}' 카테고리를 삭제하시겠습니까?",
                    "카테고리 삭제",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            categories.RemoveAll(
                item => item.Id == selected.Id);

            RefreshList();
        }

        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            store.Save(categories);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(
            object sender,
            EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void RefreshList(
            string? selectedId = null)
        {
            list.BeginUpdate();

            try
            {
                list.Items.Clear();

                foreach (var category in categories)
                    list.Items.Add(category);
            }
            finally
            {
                list.EndUpdate();
            }

            if (selectedId is null)
                return;

            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i] is UserCategory category &&
                    category.Id == selectedId)
                {
                    list.SelectedIndex = i;
                    break;
                }
            }
        }

        private void DrawCategory(
            object? sender,
            DrawItemEventArgs e)
        {
            if (e.Index < 0 ||
                e.Index >= list.Items.Count)
            {
                return;
            }

            e.DrawBackground();

            UserCategory category =
                (UserCategory)list.Items[e.Index];

            using var brush =
                new SolidBrush(
                    Color.FromArgb(
                        category.ColorArgb));

            e.Graphics.FillRectangle(
                brush,
                e.Bounds.Left + 8,
                e.Bounds.Top + 7,
                18,
                18);

            string text =
                category.Name +
                (category.IsDefault
                    ? "  (기본)"
                    : string.Empty);

            TextRenderer.DrawText(
                e.Graphics,
                text,
                list.Font,
                new Rectangle(
                    e.Bounds.Left + 36,
                    e.Bounds.Top,
                    e.Bounds.Width - 40,
                    e.Bounds.Height),
                e.ForeColor,
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis);

            e.DrawFocusRectangle();
        }
    }
}