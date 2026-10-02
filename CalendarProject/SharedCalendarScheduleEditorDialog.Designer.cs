namespace calendar4
{
    partial class SharedCalendarScheduleEditorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharedCalendarScheduleEditorDialog));
            lblTitle = new Label();
            lblTime = new Label();
            lblParticipants = new Label();
            lblNotification = new Label();
            txtTitle = new TextBox();
            cmbStartTime = new ComboBox();
            cmbEndTime = new ComboBox();
            chkAllMembers = new CheckBox();
            clbMembers = new CheckedListBox();
            cmbNotification = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            label1 = new Label();
            dtpScheduleEndDate = new DateTimePicker();
            groupBox1 = new GroupBox();
            dtpRepeatEndDate = new DateTimePicker();
            radioNone = new RadioButton();
            radioDay = new RadioButton();
            radioWeek = new RadioButton();
            radioMonth = new RadioButton();
            radioYear = new RadioButton();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(31, 53);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(55, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "일정제목";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(38, 95);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(31, 15);
            lblTime.TabIndex = 1;
            lblTime.Text = "시간";
            // 
            // lblParticipants
            // 
            lblParticipants.AutoSize = true;
            lblParticipants.Location = new Point(31, 183);
            lblParticipants.Name = "lblParticipants";
            lblParticipants.Size = new Size(43, 15);
            lblParticipants.TabIndex = 2;
            lblParticipants.Text = "참여자";
            // 
            // lblNotification
            // 
            lblNotification.AutoSize = true;
            lblNotification.Location = new Point(43, 388);
            lblNotification.Name = "lblNotification";
            lblNotification.Size = new Size(31, 15);
            lblNotification.TabIndex = 4;
            lblNotification.Text = "알림";
            // 
            // txtTitle
            // 
            txtTitle.BackColor = Color.White;
            txtTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTitle.ForeColor = Color.FromArgb(45, 45, 45);
            txtTitle.Location = new Point(119, 53);
            txtTitle.MaxLength = 100;
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(295, 23);
            txtTitle.TabIndex = 5;
            // 
            // cmbStartTime
            // 
            cmbStartTime.BackColor = Color.White;
            cmbStartTime.CausesValidation = false;
            cmbStartTime.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStartTime.ForeColor = Color.FromArgb(45, 45, 45);
            cmbStartTime.FormattingEnabled = true;
            cmbStartTime.Location = new Point(120, 92);
            cmbStartTime.Name = "cmbStartTime";
            cmbStartTime.Size = new Size(121, 23);
            cmbStartTime.TabIndex = 6;
            // 
            // cmbEndTime
            // 
            cmbEndTime.BackColor = Color.White;
            cmbEndTime.CausesValidation = false;
            cmbEndTime.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEndTime.ForeColor = Color.FromArgb(45, 45, 45);
            cmbEndTime.FormattingEnabled = true;
            cmbEndTime.Location = new Point(293, 95);
            cmbEndTime.Name = "cmbEndTime";
            cmbEndTime.Size = new Size(121, 23);
            cmbEndTime.TabIndex = 7;
            // 
            // chkAllMembers
            // 
            chkAllMembers.AutoSize = true;
            chkAllMembers.Checked = true;
            chkAllMembers.CheckState = CheckState.Checked;
            chkAllMembers.ForeColor = Color.FromArgb(55, 55, 55);
            chkAllMembers.Location = new Point(120, 184);
            chkAllMembers.Name = "chkAllMembers";
            chkAllMembers.Size = new Size(78, 19);
            chkAllMembers.TabIndex = 8;
            chkAllMembers.Text = "전체 회원";
            chkAllMembers.UseVisualStyleBackColor = true;
            // 
            // clbMembers
            // 
            clbMembers.BackColor = Color.White;
            clbMembers.BorderStyle = BorderStyle.FixedSingle;
            clbMembers.CheckOnClick = true;
            clbMembers.ForeColor = Color.FromArgb(45, 45, 45);
            clbMembers.FormattingEnabled = true;
            clbMembers.Location = new Point(234, 184);
            clbMembers.Name = "clbMembers";
            clbMembers.Size = new Size(120, 92);
            clbMembers.TabIndex = 9;
            // 
            // cmbNotification
            // 
            cmbNotification.BackColor = Color.White;
            cmbNotification.CausesValidation = false;
            cmbNotification.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNotification.ForeColor = Color.FromArgb(45, 45, 45);
            cmbNotification.FormattingEnabled = true;
            cmbNotification.Items.AddRange(new object[] { "알림 없음", "5분 전", "10분 전", "30분 전", "1시간 전", "2시간 전" });
            cmbNotification.Location = new Point(120, 385);
            cmbNotification.Name = "cmbNotification";
            cmbNotification.Size = new Size(122, 23);
            cmbNotification.TabIndex = 11;
            // 
            // btnSave
            // 
            btnSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSave.BackColor = Color.SteelBlue;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(103, 438);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 12;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(225, 230, 235);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.DimGray;
            btnCancel.Location = new Point(234, 438);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 133);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 1;
            label1.Text = "일정 종료일";
            // 
            // dtpScheduleEndDate
            // 
            dtpScheduleEndDate.CalendarMonthBackground = Color.White;
            dtpScheduleEndDate.Location = new Point(120, 133);
            dtpScheduleEndDate.Name = "dtpScheduleEndDate";
            dtpScheduleEndDate.Size = new Size(200, 23);
            dtpScheduleEndDate.TabIndex = 14;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dtpRepeatEndDate);
            groupBox1.Controls.Add(radioNone);
            groupBox1.Controls.Add(radioDay);
            groupBox1.Controls.Add(radioWeek);
            groupBox1.Controls.Add(radioMonth);
            groupBox1.Controls.Add(radioYear);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(38, 282);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(376, 85);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "반복 설정";
            // 
            // dtpRepeatEndDate
            // 
            dtpRepeatEndDate.Location = new Point(107, 47);
            dtpRepeatEndDate.Name = "dtpRepeatEndDate";
            dtpRepeatEndDate.Size = new Size(200, 23);
            dtpRepeatEndDate.TabIndex = 7;
            // 
            // radioNone
            // 
            radioNone.AutoSize = true;
            radioNone.Checked = true;
            radioNone.Location = new Point(16, 22);
            radioNone.Name = "radioNone";
            radioNone.Size = new Size(73, 19);
            radioNone.TabIndex = 0;
            radioNone.TabStop = true;
            radioNone.Text = "반복없음";
            radioNone.UseVisualStyleBackColor = true;
            // 
            // radioDay
            // 
            radioDay.AutoSize = true;
            radioDay.Location = new Point(320, 22);
            radioDay.Name = "radioDay";
            radioDay.Size = new Size(37, 19);
            radioDay.TabIndex = 0;
            radioDay.TabStop = true;
            radioDay.Text = "일";
            radioDay.UseVisualStyleBackColor = true;
            // 
            // radioWeek
            // 
            radioWeek.AutoSize = true;
            radioWeek.Location = new Point(253, 22);
            radioWeek.Name = "radioWeek";
            radioWeek.Size = new Size(37, 19);
            radioWeek.TabIndex = 0;
            radioWeek.TabStop = true;
            radioWeek.Text = "주";
            radioWeek.UseVisualStyleBackColor = true;
            // 
            // radioMonth
            // 
            radioMonth.AutoSize = true;
            radioMonth.Location = new Point(186, 22);
            radioMonth.Name = "radioMonth";
            radioMonth.Size = new Size(37, 19);
            radioMonth.TabIndex = 0;
            radioMonth.TabStop = true;
            radioMonth.Text = "월";
            radioMonth.UseVisualStyleBackColor = true;
            // 
            // radioYear
            // 
            radioYear.AutoSize = true;
            radioYear.Location = new Point(119, 22);
            radioYear.Name = "radioYear";
            radioYear.Size = new Size(37, 19);
            radioYear.TabIndex = 0;
            radioYear.TabStop = true;
            radioYear.Text = "년";
            radioYear.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 55);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 0;
            label2.Text = "반복 종료일";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(260, 98);
            label3.Name = "label3";
            label3.Size = new Size(15, 15);
            label3.TabIndex = 16;
            label3.Text = "~";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(45, 45, 45);
            label4.Location = new Point(31, 9);
            label4.Name = "label4";
            label4.Size = new Size(140, 25);
            label4.TabIndex = 17;
            label4.Text = "공유 일정 설정";
            // 
            // SharedCalendarScheduleEditorDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(451, 485);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(groupBox1);
            Controls.Add(dtpScheduleEndDate);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cmbNotification);
            Controls.Add(clbMembers);
            Controls.Add(chkAllMembers);
            Controls.Add(cmbEndTime);
            Controls.Add(cmbStartTime);
            Controls.Add(txtTitle);
            Controls.Add(lblNotification);
            Controls.Add(lblParticipants);
            Controls.Add(label1);
            Controls.Add(lblTime);
            Controls.Add(lblTitle);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "SharedCalendarScheduleEditorDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SharedCalendarScheduleEditorDialog";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            // Original event connections preserved from base project
            chkAllMembers.CheckedChanged += chkAllMembers_CheckedChanged;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblTime;
        private Label lblParticipants;
        private Label lblNotification;
        private TextBox txtTitle;
        private ComboBox cmbStartTime;
        private ComboBox cmbEndTime;
        private CheckBox chkAllMembers;
        private CheckedListBox clbMembers;
        private ComboBox cmbNotification;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private DateTimePicker dtpScheduleEndDate;
        private GroupBox groupBox1;
        private DateTimePicker dtpRepeatEndDate;
        private RadioButton radioNone;
        private RadioButton radioDay;
        private RadioButton radioWeek;
        private RadioButton radioMonth;
        private RadioButton radioYear;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
