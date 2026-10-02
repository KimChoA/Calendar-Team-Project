namespace calendar4
{
    partial class UserCategoryManagerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserCategoryManagerForm));
            list = new ListBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // list
            // 
            list.BackColor = Color.White;
            list.BorderStyle = BorderStyle.FixedSingle;
            list.ForeColor = Color.FromArgb(45, 45, 45);
            list.FormattingEnabled = true;
            list.IntegralHeight = false;
            list.ItemHeight = 15;
            list.Location = new Point(12, 12);
            list.Name = "list";
            list.Size = new Size(386, 210);
            list.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.White;
            btnAdd.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.DimGray;
            btnAdd.Location = new Point(12, 232);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(76, 30);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "추가";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.White;
            btnEdit.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.DimGray;
            btnEdit.Location = new Point(96, 232);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(76, 30);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "수정";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(220, 200, 200);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.Firebrick;
            btnDelete.Location = new Point(180, 232);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(76, 30);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "삭제";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SteelBlue;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(240, 267);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(76, 34);
            btnSave.TabIndex = 4;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(225, 230, 235);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.DimGray;
            btnCancel.Location = new Point(322, 267);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(76, 34);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // UserCategoryManagerForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            CancelButton = btnCancel;
            ClientSize = new Size(410, 314);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(list);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserCategoryManagerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "카테고리 관리";

            // Original event connections preserved from base project
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            ResumeLayout(false);
        }

        #endregion

        private ListBox list;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
    }
}
