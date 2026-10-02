namespace calendar4
{
    partial class CalenderScheduleListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalenderScheduleListForm));
            title = new Label();
            ScheduleList = new ListBox();
            addButton = new Button();
            editButton = new Button();
            deleteButton = new Button();
            doneButton = new Button();
            categoriesButton = new Button();
            SuspendLayout();
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("한컴 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            title.Location = new Point(22, 18);
            title.Name = "title";
            title.Size = new Size(74, 21);
            title.TabIndex = 0;
            title.Text = "임시 일정";
            // 
            // ScheduleList
            // 
            ScheduleList.FormattingEnabled = true;
            ScheduleList.ItemHeight = 15;
            ScheduleList.Location = new Point(22, 54);
            ScheduleList.Name = "ScheduleList";
            ScheduleList.Size = new Size(500, 244);
            ScheduleList.TabIndex = 1;
            // 
            // addButton
            // 
            addButton.BackColor = Color.SteelBlue;
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(22, 304);
            addButton.Name = "addButton";
            addButton.Size = new Size(88, 34);
            addButton.TabIndex = 2;
            addButton.Text = "새 일정";
            addButton.UseVisualStyleBackColor = false;
            // 
            // editButton
            // 
            editButton.BackColor = Color.White;
            editButton.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            editButton.FlatStyle = FlatStyle.Flat;
            editButton.ForeColor = Color.DimGray;
            editButton.Location = new Point(120, 304);
            editButton.Name = "editButton";
            editButton.Size = new Size(88, 34);
            editButton.TabIndex = 3;
            editButton.Text = "수정";
            editButton.UseVisualStyleBackColor = false;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.White;
            deleteButton.FlatAppearance.BorderColor = Color.FromArgb(220, 200, 200);
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.ForeColor = Color.Firebrick;
            deleteButton.Location = new Point(218, 304);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(88, 34);
            deleteButton.TabIndex = 4;
            deleteButton.Text = "삭제";
            deleteButton.UseVisualStyleBackColor = false;
            // 
            // doneButton
            // 
            doneButton.BackColor = Color.FromArgb(225, 230, 235);
            doneButton.FlatAppearance.BorderSize = 0;
            doneButton.FlatStyle = FlatStyle.Flat;
            doneButton.ForeColor = Color.DimGray;
            doneButton.Location = new Point(444, 304);
            doneButton.Name = "doneButton";
            doneButton.Size = new Size(78, 34);
            doneButton.TabIndex = 5;
            doneButton.Text = "완료";
            doneButton.UseVisualStyleBackColor = false;
            // 
            // categoriesButton
            // 
            categoriesButton.BackColor = Color.White;
            categoriesButton.FlatAppearance.BorderColor = Color.FromArgb(210, 230, 235);
            categoriesButton.FlatStyle = FlatStyle.Flat;
            categoriesButton.ForeColor = Color.DimGray;
            categoriesButton.Location = new Point(316, 304);
            categoriesButton.Name = "categoriesButton";
            categoriesButton.Size = new Size(118, 34);
            categoriesButton.TabIndex = 6;
            categoriesButton.Text = "카테고리 관리";
            categoriesButton.UseVisualStyleBackColor = false;
            // 
            // CalenderScheduleListForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(540, 361);
            Controls.Add(categoriesButton);
            Controls.Add(doneButton);
            Controls.Add(deleteButton);
            Controls.Add(editButton);
            Controls.Add(addButton);
            Controls.Add(ScheduleList);
            Controls.Add(title);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CalenderScheduleListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CalenderScheduleListForm";

            // Original event connections preserved from base project
            addButton.Click += addButton_Click;
            editButton.Click += editButton_Click;
            deleteButton.Click += deleteButton_Click;
            doneButton.Click += doneButton_Click;
            categoriesButton.Click += categoriesButton_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title;
        private ListBox ScheduleList;
        private Button addButton;
        private Button editButton;
        private Button deleteButton;
        private Button doneButton;
        private Button categoriesButton;
    }
}
