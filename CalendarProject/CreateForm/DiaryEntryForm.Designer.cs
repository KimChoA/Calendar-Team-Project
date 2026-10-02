namespace calendar4
{
    partial class DiaryEntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiaryEntryForm));
            titleLabel = new Label();
            txtTitle = new TextBox();
            contentLabel = new Label();
            txtContent = new TextBox();
            btnSave = new Button();
            btnDelete = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("한컴 말랑말랑 Bold", 9.749998F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.FromArgb(90, 80, 90);
            titleLabel.Location = new Point(15, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(52, 17);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "일기제목";
            // 
            // txtTitle
            // 
            txtTitle.BackColor = Color.FromArgb(255, 253, 254);
            txtTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTitle.ForeColor = Color.FromArgb(70, 65, 70);
            txtTitle.Location = new Point(67, 12);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(405, 23);
            txtTitle.TabIndex = 1;
            // 
            // contentLabel
            // 
            contentLabel.AutoSize = true;
            contentLabel.Font = new Font("한컴 말랑말랑 Bold", 9.749998F, FontStyle.Bold, GraphicsUnit.Point);
            contentLabel.ForeColor = Color.FromArgb(90, 80, 90);
            contentLabel.Location = new Point(15, 55);
            contentLabel.Name = "contentLabel";
            contentLabel.Size = new Size(55, 17);
            contentLabel.TabIndex = 2;
            contentLabel.Text = "일기 내용";
            // 
            // txtContent
            // 
            txtContent.BackColor = Color.FromArgb(255, 253, 254);
            txtContent.BorderStyle = BorderStyle.FixedSingle;
            txtContent.Font = new Font("한컴 말랑말랑 Regular", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtContent.ForeColor = Color.FromArgb(70, 65, 70);
            txtContent.Location = new Point(15, 75);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(450, 280);
            txtContent.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(229, 190, 184);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("한컴 말랑말랑 Regular", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(279, 361);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 25);
            btnSave.TabIndex = 4;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(245, 225, 230);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("한컴 말랑말랑 Regular", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnDelete.ForeColor = Color.FromArgb(135, 86, 95);
            btnDelete.Location = new Point(183, 361);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 25);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "삭제";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(235, 232, 238);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("한컴 말랑말랑 Regular", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.FromArgb(90, 85, 95);
            btnCancel.Location = new Point(375, 362);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 25);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // DiaryEntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(252, 248, 250);
            ClientSize = new Size(484, 399);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(txtContent);
            Controls.Add(contentLabel);
            Controls.Add(txtTitle);
            Controls.Add(titleLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DiaryEntryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DiaryEntryForm";

            // Original event connections preserved from base project
            btnSave.Click += btnSave_Click;
            btnDelete.Click += btnDelete_Click;
            btnCancel.Click += btnCancel_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titleLabel;
        private TextBox txtTitle;
        private Label contentLabel;
        private TextBox txtContent;
        private Button btnSave;
        private Button btnDelete;
        private Button btnCancel;
    }
}
