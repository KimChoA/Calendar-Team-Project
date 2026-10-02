using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Windows.Forms;

namespace calendar4
{
    public partial class DiaryEntryForm : Form
    {
        private readonly DiaryEntry? existingEntry;

        public string DiaryTitle =>
            txtTitle.Text;

        public string DiaryContent =>
            txtContent.Text;

        public bool IsEmpty =>
            string.IsNullOrWhiteSpace(DiaryTitle) &&
            string.IsNullOrWhiteSpace(DiaryContent);


        // 디자이너용
        public DiaryEntryForm()
        {
            InitializeComponent();

            existingEntry = null;

            btnDelete.Visible = false;
        }


        // 실제 사용
        public DiaryEntryForm(
            DateTime date,
            DiaryEntry? entry)
        {
            InitializeComponent();

            existingEntry = entry;

            Text =
                $"{date:yyyy년 MM월 dd일} 다이어리 작성";

            txtTitle.Text =
                entry?.Title ?? string.Empty;

            txtContent.Text =
                entry?.Content ?? string.Empty;

            // 기존 일기가 있을 때만 삭제 버튼 표시
            btnDelete.Visible =
                entry is not null;
        }


        // 저장
        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.OK;

            Close();
        }


        // 삭제
        private void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (existingEntry is null)
                return;

            DialogResult result =
                MessageBox.Show(
                    "이 일기를 삭제하시겠습니까?",
                    "다이어리 삭제",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // DiaryControl에서 Yes를 삭제 신호로 사용
            DialogResult =
                DialogResult.Yes;

            Close();
        }


        // 취소
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