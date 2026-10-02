namespace calendar4
{
    partial class DdaySettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DdaySettingForm));
            lblFormTitle = new Label();
            lblSchedule = new Label();
            lstSchedules = new ListBox();
            lblSelected = new Label();
            btnNewDday = new Button();
            grpCountMode = new GroupBox();
            rdoOne = new RadioButton();
            rdoZero = new RadioButton();
            btnSave = new Button();
            btnCancel = new Button();
            btnDelete = new Button();
            grpCountMode.SuspendLayout();
            SuspendLayout();
            // 
            // lblFormTitle
            // 
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("맑은 고딕", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblFormTitle.ForeColor = Color.FromArgb(45, 45, 45);
            lblFormTitle.Location = new Point(20, 20);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(142, 32);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "D-Day 설정";
            // 
            // lblSchedule
            // 
            lblSchedule.AutoSize = true;
            lblSchedule.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblSchedule.ForeColor = Color.FromArgb(70, 70, 70);
            lblSchedule.Location = new Point(20, 67);
            lblSchedule.Name = "lblSchedule";
            lblSchedule.Size = new Size(174, 20);
            lblSchedule.TabIndex = 1;
            lblSchedule.Text = "내 캘린더 일정에서 선택";
            // 
            // lstSchedules
            // 
            lstSchedules.BackColor = Color.White;
            lstSchedules.BorderStyle = BorderStyle.FixedSingle;
            lstSchedules.ForeColor = Color.FromArgb(45, 45, 45);
            lstSchedules.FormattingEnabled = true;
            lstSchedules.ItemHeight = 15;
            lstSchedules.Location = new Point(20, 90);
            lstSchedules.Name = "lstSchedules";
            lstSchedules.Size = new Size(440, 122);
            lstSchedules.TabIndex = 2;
            // 
            // lblSelected
            // 
            lblSelected.AutoSize = true;
            lblSelected.Location = new Point(20, 219);
            lblSelected.Name = "lblSelected";
            lblSelected.Size = new Size(138, 15);
            lblSelected.TabIndex = 3;
            lblSelected.Text = "선택된 일정이 없습니다.";
            // 
            // btnNewDday
            // 
            btnNewDday.BackColor = Color.White;
            btnNewDday.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnNewDday.FlatStyle = FlatStyle.Flat;
            btnNewDday.ForeColor = Color.DimGray;
            btnNewDday.Location = new Point(20, 237);
            btnNewDday.Name = "btnNewDday";
            btnNewDday.Size = new Size(440, 23);
            btnNewDday.TabIndex = 4;
            btnNewDday.Text = "새 D-Day 만들기";
            btnNewDday.UseVisualStyleBackColor = false;
            // 
            // grpCountMode
            // 
            grpCountMode.Controls.Add(rdoOne);
            grpCountMode.Controls.Add(rdoZero);
            grpCountMode.Location = new Point(20, 278);
            grpCountMode.Name = "grpCountMode";
            grpCountMode.Size = new Size(440, 55);
            grpCountMode.TabIndex = 5;
            grpCountMode.TabStop = false;
            grpCountMode.Text = "카운트 방식";
            // 
            // rdoOne
            // 
            rdoOne.AutoSize = true;
            rdoOne.Location = new Point(259, 22);
            rdoOne.Name = "rdoOne";
            rdoOne.Size = new Size(96, 19);
            rdoOne.TabIndex = 1;
            rdoOne.Text = "1일부터 시작";
            rdoOne.UseVisualStyleBackColor = true;
            // 
            // rdoZero
            // 
            rdoZero.AutoSize = true;
            rdoZero.Checked = true;
            rdoZero.Location = new Point(47, 22);
            rdoZero.Name = "rdoZero";
            rdoZero.Size = new Size(96, 19);
            rdoZero.TabIndex = 0;
            rdoZero.TabStop = true;
            rdoZero.Text = "0일부터 시작";
            rdoZero.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SteelBlue;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(262, 339);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 6;
            btnSave.Text = "설정";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(225, 230, 235);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.DimGray;
            btnCancel.Location = new Point(364, 339);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(220, 200, 200);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.Firebrick;
            btnDelete.Location = new Point(20, 341);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(150, 30);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "현재 디데이 해제";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // DdaySettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(484, 399);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(grpCountMode);
            Controls.Add(btnNewDday);
            Controls.Add(lblSelected);
            Controls.Add(lstSchedules);
            Controls.Add(lblSchedule);
            Controls.Add(lblFormTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DdaySettingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DdaySettingForm";
            grpCountMode.ResumeLayout(false);
            grpCountMode.PerformLayout();

            // Original event connections preserved from base project
            lstSchedules.SelectedIndexChanged += lstSchedules_SelectedIndexChanged;
            btnNewDday.Click += btnNewDday_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnDelete.Click += btnDelete_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFormTitle;
        private Label lblSchedule;
        private ListBox lstSchedules;
        private Label lblSelected;
        private Button btnNewDday;
        private GroupBox grpCountMode;
        private RadioButton rdoOne;
        private RadioButton rdoZero;
        private Button btnSave;
        private Button btnCancel;
        private Button btnDelete;
    }
}
