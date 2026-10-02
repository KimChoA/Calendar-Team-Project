namespace calendar4
{
    partial class CalendarScheduleEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarScheduleEditorForm));
            lblDate = new Label();
            txtSchedule = new TextBox();
            btnChooseColor = new Button();
            cboStartTime = new ComboBox();
            chkImportant = new CheckBox();
            pnlColorPreview = new Panel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cboEndTime = new ComboBox();
            btnDefaultColor = new Button();
            label6 = new Label();
            label7 = new Label();
            cboCategory = new ComboBox();
            btnCategory = new Button();
            cboAlarm = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            groupBox1 = new GroupBox();
            dtpRepeatEndDate = new DateTimePicker();
            radioNone = new RadioButton();
            radioDay = new RadioButton();
            radioWeek = new RadioButton();
            radioMonth = new RadioButton();
            radioYear = new RadioButton();
            label1 = new Label();
            dtpScheduleEndDate = new DateTimePicker();
            label8 = new Label();
            label9 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblDate.Location = new Point(41, 44);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(38, 12);
            lblDate.TabIndex = 0;
            lblDate.Text = "label1";
            // 
            // txtSchedule
            // 
            txtSchedule.BackColor = Color.FromArgb(248, 249, 250);
            txtSchedule.BorderStyle = BorderStyle.FixedSingle;
            txtSchedule.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtSchedule.ForeColor = Color.FromArgb(45, 45, 45);
            txtSchedule.Location = new Point(41, 98);
            txtSchedule.Name = "txtSchedule";
            txtSchedule.Size = new Size(326, 21);
            txtSchedule.TabIndex = 1;
            // 
            // btnChooseColor
            // 
            btnChooseColor.BackColor = Color.White;
            btnChooseColor.Cursor = Cursors.Hand;
            btnChooseColor.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnChooseColor.FlatStyle = FlatStyle.System;
            btnChooseColor.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnChooseColor.ForeColor = Color.DimGray;
            btnChooseColor.Location = new Point(41, 396);
            btnChooseColor.Name = "btnChooseColor";
            btnChooseColor.Size = new Size(104, 23);
            btnChooseColor.TabIndex = 2;
            btnChooseColor.Text = "직접 선택";
            btnChooseColor.UseVisualStyleBackColor = false;
            // 
            // cboStartTime
            // 
            cboStartTime.BackColor = Color.White;
            cboStartTime.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cboStartTime.ForeColor = Color.FromArgb(45, 45, 45);
            cboStartTime.FormattingEnabled = true;
            cboStartTime.Location = new Point(41, 160);
            cboStartTime.Name = "cboStartTime";
            cboStartTime.Size = new Size(100, 20);
            cboStartTime.TabIndex = 3;
            // 
            // chkImportant
            // 
            chkImportant.AutoSize = true;
            chkImportant.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            chkImportant.Location = new Point(177, 367);
            chkImportant.Name = "chkImportant";
            chkImportant.Size = new Size(76, 16);
            chkImportant.TabIndex = 4;
            chkImportant.Text = "중요 일정";
            chkImportant.UseVisualStyleBackColor = true;
            // 
            // pnlColorPreview
            // 
            pnlColorPreview.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            pnlColorPreview.Location = new Point(109, 368);
            pnlColorPreview.Name = "pnlColorPreview";
            pnlColorPreview.Size = new Size(50, 15);
            pnlColorPreview.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(41, 72);
            label2.Name = "label2";
            label2.Size = new Size(57, 12);
            label2.TabIndex = 0;
            label2.Text = "일정 내용";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("돋움", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(151, 160);
            label3.Name = "label3";
            label3.Size = new Size(20, 16);
            label3.TabIndex = 0;
            label3.Text = "~";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(41, 371);
            label4.Name = "label4";
            label4.Size = new Size(57, 12);
            label4.TabIndex = 0;
            label4.Text = "일정 색상";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(41, 436);
            label5.Name = "label5";
            label5.Size = new Size(29, 12);
            label5.TabIndex = 0;
            label5.Text = "알림";
            // 
            // cboEndTime
            // 
            cboEndTime.BackColor = Color.White;
            cboEndTime.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cboEndTime.ForeColor = Color.FromArgb(45, 45, 45);
            cboEndTime.FormattingEnabled = true;
            cboEndTime.Location = new Point(183, 160);
            cboEndTime.Name = "cboEndTime";
            cboEndTime.Size = new Size(100, 20);
            cboEndTime.TabIndex = 3;
            // 
            // btnDefaultColor
            // 
            btnDefaultColor.BackColor = Color.White;
            btnDefaultColor.Cursor = Cursors.Hand;
            btnDefaultColor.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnDefaultColor.FlatStyle = FlatStyle.System;
            btnDefaultColor.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnDefaultColor.ForeColor = Color.DimGray;
            btnDefaultColor.Location = new Point(149, 396);
            btnDefaultColor.Name = "btnDefaultColor";
            btnDefaultColor.Size = new Size(104, 23);
            btnDefaultColor.TabIndex = 2;
            btnDefaultColor.Text = "기본색 사용";
            btnDefaultColor.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(41, 288);
            label6.Name = "label6";
            label6.Size = new Size(53, 12);
            label6.TabIndex = 0;
            label6.Text = "카테고리";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(41, 136);
            label7.Name = "label7";
            label7.Size = new Size(29, 12);
            label7.TabIndex = 0;
            label7.Text = "시간";
            // 
            // cboCategory
            // 
            cboCategory.BackColor = Color.White;
            cboCategory.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cboCategory.ForeColor = Color.FromArgb(45, 45, 45);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(41, 322);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(258, 20);
            cboCategory.TabIndex = 3;
            // 
            // btnCategory
            // 
            btnCategory.BackColor = Color.White;
            btnCategory.Cursor = Cursors.Hand;
            btnCategory.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnCategory.FlatStyle = FlatStyle.System;
            btnCategory.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnCategory.ForeColor = Color.DimGray;
            btnCategory.Location = new Point(263, 396);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(104, 23);
            btnCategory.TabIndex = 2;
            btnCategory.Text = "카테고리 추가";
            btnCategory.UseVisualStyleBackColor = false;
            // 
            // cboAlarm
            // 
            cboAlarm.BackColor = Color.White;
            cboAlarm.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cboAlarm.ForeColor = Color.FromArgb(45, 45, 45);
            cboAlarm.FormattingEnabled = true;
            cboAlarm.Location = new Point(41, 460);
            cboAlarm.Name = "cboAlarm";
            cboAlarm.Size = new Size(258, 20);
            cboAlarm.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SteelBlue;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(69, 632);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 2;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(225, 230, 235);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.DimGray;
            btnCancel.Location = new Point(237, 632);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dtpRepeatEndDate);
            groupBox1.Controls.Add(radioNone);
            groupBox1.Controls.Add(radioDay);
            groupBox1.Controls.Add(radioWeek);
            groupBox1.Controls.Add(radioMonth);
            groupBox1.Controls.Add(radioYear);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.Location = new Point(41, 501);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(326, 122);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "반복 설정";
            // 
            // dtpRepeatEndDate
            // 
            dtpRepeatEndDate.Location = new Point(16, 80);
            dtpRepeatEndDate.Name = "dtpRepeatEndDate";
            dtpRepeatEndDate.Size = new Size(200, 21);
            dtpRepeatEndDate.TabIndex = 7;
            // 
            // radioNone
            // 
            radioNone.AutoSize = true;
            radioNone.Checked = true;
            radioNone.Location = new Point(14, 20);
            radioNone.Name = "radioNone";
            radioNone.Size = new Size(71, 16);
            radioNone.TabIndex = 0;
            radioNone.TabStop = true;
            radioNone.Text = "반복없음";
            radioNone.UseVisualStyleBackColor = true;
            // 
            // radioDay
            // 
            radioDay.AutoSize = true;
            radioDay.Location = new Point(251, 20);
            radioDay.Name = "radioDay";
            radioDay.Size = new Size(35, 16);
            radioDay.TabIndex = 0;
            radioDay.TabStop = true;
            radioDay.Text = "일";
            radioDay.UseVisualStyleBackColor = true;
            // 
            // radioWeek
            // 
            radioWeek.AutoSize = true;
            radioWeek.Location = new Point(196, 20);
            radioWeek.Name = "radioWeek";
            radioWeek.Size = new Size(35, 16);
            radioWeek.TabIndex = 0;
            radioWeek.TabStop = true;
            radioWeek.Text = "주";
            radioWeek.UseVisualStyleBackColor = true;
            // 
            // radioMonth
            // 
            radioMonth.AutoSize = true;
            radioMonth.Location = new Point(142, 20);
            radioMonth.Name = "radioMonth";
            radioMonth.Size = new Size(35, 16);
            radioMonth.TabIndex = 0;
            radioMonth.TabStop = true;
            radioMonth.Text = "월";
            radioMonth.UseVisualStyleBackColor = true;
            // 
            // radioYear
            // 
            radioYear.AutoSize = true;
            radioYear.Location = new Point(91, 20);
            radioYear.Name = "radioYear";
            radioYear.Size = new Size(35, 16);
            radioYear.TabIndex = 0;
            radioYear.TabStop = true;
            radioYear.Text = "년";
            radioYear.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 55);
            label1.Name = "label1";
            label1.Size = new Size(69, 12);
            label1.TabIndex = 0;
            label1.Text = "반복 종료일";
            // 
            // dtpScheduleEndDate
            // 
            dtpScheduleEndDate.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpScheduleEndDate.Location = new Point(41, 238);
            dtpScheduleEndDate.Name = "dtpScheduleEndDate";
            dtpScheduleEndDate.Size = new Size(200, 21);
            dtpScheduleEndDate.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("돋움", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(41, 206);
            label8.Name = "label8";
            label8.Size = new Size(69, 12);
            label8.TabIndex = 0;
            label8.Text = "일정 종료일";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(161, 9);
            label9.Name = "label9";
            label9.Size = new Size(95, 25);
            label9.TabIndex = 8;
            label9.Text = "일정 추가";
            // 
            // CalendarScheduleEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(400, 674);
            Controls.Add(label9);
            Controls.Add(dtpScheduleEndDate);
            Controls.Add(groupBox1);
            Controls.Add(pnlColorPreview);
            Controls.Add(chkImportant);
            Controls.Add(cboEndTime);
            Controls.Add(cboAlarm);
            Controls.Add(cboCategory);
            Controls.Add(cboStartTime);
            Controls.Add(btnDefaultColor);
            Controls.Add(btnCategory);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnChooseColor);
            Controls.Add(txtSchedule);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblDate);
            ForeColor = Color.FromArgb(45, 45, 45);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CalendarScheduleEditorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CalendarScheduleEditorForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            // Original event connections preserved from base project
            btnChooseColor.Click += btnChooseColor_Click;
            btnDefaultColor.Click += btnDefaultColor_Click;
            cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
            btnCategory.Click += btnCategory_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDate;
        private TextBox txtSchedule;
        private Button btnChooseColor;
        private ComboBox cboStartTime;
        private CheckBox chkImportant;
        private Panel pnlColorPreview;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cboEndTime;
        private Button btnDefaultColor;
        private Label label6;
        private Label label7;
        private ComboBox cboCategory;
        private Button btnCategory;
        private ComboBox cboAlarm;
        private Button btnSave;
        private Button btnCancel;
        private GroupBox groupBox1;
        private RadioButton radioDay;
        private RadioButton radioWeek;
        private RadioButton radioMonth;
        private RadioButton radioYear;
        private DateTimePicker dtpRepeatEndDate;
        private Label label1;
        private RadioButton radioNone;
        private DateTimePicker dtpScheduleEndDate;
        private Label label8;
        private Label label9;
    }
}
