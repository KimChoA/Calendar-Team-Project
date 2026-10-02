namespace calendar4.CreateForm
{
    partial class SharecalendarManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharecalendarManager));
            listBox1 = new ListBox();
            btnReject = new Button();
            btnInvite = new Button();
            groupBox1 = new GroupBox();
            radioAll = new RadioButton();
            radioRead = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            btnBack = new Button();
            label3 = new Label();
            btnOut = new Button();
            btnDelete = new Button();
            label4 = new Label();
            label5 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.White;
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.ForeColor = Color.FromArgb(45, 45, 45);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(41, 44);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(441, 107);
            listBox1.TabIndex = 0;
            // 
            // btnReject
            // 
            btnReject.BackColor = Color.White;
            btnReject.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnReject.ForeColor = Color.DimGray;
            btnReject.Location = new Point(389, 159);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(93, 25);
            btnReject.TabIndex = 1;
            btnReject.Text = "멤버 내보내기";
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;
            // 
            // btnInvite
            // 
            btnInvite.BackColor = Color.SteelBlue;
            btnInvite.FlatAppearance.BorderSize = 0;
            btnInvite.FlatStyle = FlatStyle.Flat;
            btnInvite.ForeColor = Color.White;
            btnInvite.Location = new Point(140, 209);
            btnInvite.Name = "btnInvite";
            btnInvite.Size = new Size(93, 33);
            btnInvite.TabIndex = 1;
            btnInvite.Text = "초대하기";
            btnInvite.UseVisualStyleBackColor = false;
            btnInvite.Click += btnInvite_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioAll);
            groupBox1.Controls.Add(radioRead);
            groupBox1.Location = new Point(41, 267);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(441, 59);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "수정 권한";
            // 
            // radioAll
            // 
            radioAll.AutoSize = true;
            radioAll.Location = new Point(269, 22);
            radioAll.Name = "radioAll";
            radioAll.Size = new Size(105, 19);
            radioAll.TabIndex = 0;
            radioAll.TabStop = true;
            radioAll.Text = "모든 멤버 가능";
            radioAll.UseVisualStyleBackColor = true;
            // 
            // radioRead
            // 
            radioRead.AutoSize = true;
            radioRead.Location = new Point(58, 22);
            radioRead.Name = "radioRead";
            radioRead.Size = new Size(101, 19);
            radioRead.TabIndex = 0;
            radioRead.TabStop = true;
            radioRead.Text = "관리자만 가능";
            radioRead.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 265);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 218);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 4;
            label2.Text = "멤버 초대";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(225, 230, 235);
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.DimGray;
            btnBack.Location = new Point(207, 332);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(93, 33);
            btnBack.TabIndex = 1;
            btnBack.Text = "돌아가기";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(279, 218);
            label3.Name = "label3";
            label3.Size = new Size(83, 15);
            label3.TabIndex = 4;
            label3.Text = "캘린더 나가기";
            // 
            // btnOut
            // 
            btnOut.BackColor = Color.White;
            btnOut.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnOut.FlatStyle = FlatStyle.Flat;
            btnOut.ForeColor = Color.DimGray;
            btnOut.Location = new Point(385, 209);
            btnOut.Name = "btnOut";
            btnOut.Size = new Size(93, 33);
            btnOut.TabIndex = 1;
            btnOut.Text = "나가기";
            btnOut.UseVisualStyleBackColor = false;
            btnOut.Click += btnOut_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.FlatAppearance.BorderColor = Color.FromArgb(220, 200, 200);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.Firebrick;
            btnDelete.Location = new Point(140, 160);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(93, 33);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "삭제하기";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(41, 169);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 4;
            label4.Text = "캘린더 삭제하기";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(41, 26);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 4;
            label5.Text = "멤버 보기";
            // 
            // SharecalendarManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(515, 375);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBack);
            Controls.Add(btnDelete);
            Controls.Add(groupBox1);
            Controls.Add(btnOut);
            Controls.Add(btnInvite);
            Controls.Add(btnReject);
            Controls.Add(listBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SharecalendarManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SharecalendarManager";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Button btnReject;
        private Button btnInvite;
        private GroupBox groupBox1;
        private RadioButton radioAll;
        private RadioButton radioRead;
        private Label label1;
        private Label label2;
        private Button btnBack;
        private Label label3;
        private Button btnOut;
        private Button btnDelete;
        private Label label4;
        private Label label5;
    }
}
