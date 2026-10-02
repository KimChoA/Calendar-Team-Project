namespace calendar4
{
    partial class SharedCalendarEnter
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            btnCreate = new Button();
            label1 = new Label();
            txtCalname = new TextBox();
            groupBox2 = new GroupBox();
            txtEmail = new TextBox();
            txtCreatecode = new TextBox();
            label4 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            txtEntercode = new TextBox();
            btnEnter = new Button();
            label2 = new Label();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(288, 93);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(66, 23);
            btnCreate.TabIndex = 0;
            btnCreate.Text = "만들기";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 31);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 1;
            label1.Text = "캘린더 이름";
            // 
            // txtCalname
            // 
            txtCalname.Location = new Point(131, 28);
            txtCalname.Name = "txtCalname";
            txtCalname.Size = new Size(140, 23);
            txtCalname.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtEmail);
            groupBox2.Controls.Add(txtCreatecode);
            groupBox2.Controls.Add(txtCalname);
            groupBox2.Controls.Add(btnCreate);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(210, 119);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(373, 139);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "공유 캘린더";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(131, 93);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(140, 23);
            txtEmail.TabIndex = 2;
            // 
            // txtCreatecode
            // 
            txtCreatecode.Location = new Point(131, 61);
            txtCreatecode.Name = "txtCreatecode";
            txtCreatecode.Size = new Size(140, 23);
            txtCreatecode.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 97);
            label4.Name = "label4";
            label4.Size = new Size(111, 15);
            label4.TabIndex = 1;
            label4.Text = "초대할 회원 이메일";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 64);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 1;
            label3.Text = "참가 코드";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtEntercode);
            groupBox1.Controls.Add(btnEnter);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(210, 278);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(373, 83);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "공유 캘린더";
            // 
            // txtEntercode
            // 
            txtEntercode.Location = new Point(131, 34);
            txtEntercode.Name = "txtEntercode";
            txtEntercode.Size = new Size(140, 23);
            txtEntercode.TabIndex = 2;
            // 
            // btnEnter
            // 
            btnEnter.Location = new Point(288, 34);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(66, 23);
            btnEnter.TabIndex = 0;
            btnEnter.Text = "입장하기";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.Click += btnEnter_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(39, 37);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 1;
            label2.Text = "참가 코드";
            // 
            // SharedCalendarEnter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "SharedCalendarEnter";
            Size = new Size(808, 496);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCreate;
        private Label label1;
        private TextBox txtCalname;
        private GroupBox groupBox2;
        private TextBox txtCreatecode;
        private Label label3;
        private GroupBox groupBox1;
        private TextBox txtEntercode;
        private Button btnEnter;
        private Label label2;
        private TextBox txtEmail;
        private Label label4;
    }
}
