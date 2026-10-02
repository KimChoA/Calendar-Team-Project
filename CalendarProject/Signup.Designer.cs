namespace tap
{
    partial class Signup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Signup));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnSignup = new Button();
            txtSignupId = new TextBox();
            txtSignupPassword = new TextBox();
            txtPasswordCheck = new TextBox();
            txtName = new TextBox();
            txtEmail = new TextBox();
            btnBack = new Button();
            label7 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(160, 20);
            label1.Name = "label1";
            label1.Size = new Size(97, 30);
            label1.TabIndex = 0;
            label1.Text = "회원가입";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(45, 45, 45);
            label2.Location = new Point(46, 76);
            label2.Name = "label2";
            label2.Size = new Size(47, 17);
            label2.TabIndex = 1;
            label2.Text = "아이디";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(45, 45, 45);
            label3.Location = new Point(46, 134);
            label3.Name = "label3";
            label3.Size = new Size(60, 17);
            label3.TabIndex = 2;
            label3.Text = "비밀번호";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(45, 45, 45);
            label4.Location = new Point(46, 192);
            label4.Name = "label4";
            label4.Size = new Size(91, 17);
            label4.TabIndex = 3;
            label4.Text = "비밀번호 확인";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(45, 45, 45);
            label5.Location = new Point(46, 250);
            label5.Name = "label5";
            label5.Size = new Size(34, 17);
            label5.TabIndex = 4;
            label5.Text = "이름";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.FromArgb(45, 45, 45);
            label6.Location = new Point(46, 308);
            label6.Name = "label6";
            label6.Size = new Size(47, 17);
            label6.TabIndex = 5;
            label6.Text = "E-mail";
            // 
            // btnSignup
            // 
            btnSignup.BackColor = Color.SteelBlue;
            btnSignup.FlatAppearance.BorderSize = 0;
            btnSignup.FlatStyle = FlatStyle.Flat;
            btnSignup.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnSignup.ForeColor = Color.White;
            btnSignup.Location = new Point(109, 359);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(220, 30);
            btnSignup.TabIndex = 7;
            btnSignup.Text = "회원가입";
            btnSignup.UseVisualStyleBackColor = false;
            btnSignup.Click += btnSignup_Click;
            // 
            // txtSignupId
            // 
            txtSignupId.BackColor = Color.White;
            txtSignupId.BorderStyle = BorderStyle.FixedSingle;
            txtSignupId.ForeColor = Color.FromArgb(45, 45, 45);
            txtSignupId.Location = new Point(147, 70);
            txtSignupId.Name = "txtSignupId";
            txtSignupId.Size = new Size(220, 23);
            txtSignupId.TabIndex = 8;
            // 
            // txtSignupPassword
            // 
            txtSignupPassword.BackColor = Color.White;
            txtSignupPassword.BorderStyle = BorderStyle.FixedSingle;
            txtSignupPassword.ForeColor = Color.FromArgb(45, 45, 45);
            txtSignupPassword.Location = new Point(147, 128);
            txtSignupPassword.Name = "txtSignupPassword";
            txtSignupPassword.Size = new Size(220, 23);
            txtSignupPassword.TabIndex = 9;
            // 
            // txtPasswordCheck
            // 
            txtPasswordCheck.BackColor = Color.White;
            txtPasswordCheck.BorderStyle = BorderStyle.FixedSingle;
            txtPasswordCheck.ForeColor = Color.FromArgb(45, 45, 45);
            txtPasswordCheck.Location = new Point(147, 186);
            txtPasswordCheck.Name = "txtPasswordCheck";
            txtPasswordCheck.Size = new Size(220, 23);
            txtPasswordCheck.TabIndex = 10;
            // 
            // txtName
            // 
            txtName.BackColor = Color.White;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(45, 45, 45);
            txtName.Location = new Point(147, 244);
            txtName.Name = "txtName";
            txtName.Size = new Size(220, 23);
            txtName.TabIndex = 11;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.ForeColor = Color.FromArgb(45, 45, 45);
            txtEmail.Location = new Point(147, 302);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(220, 23);
            txtEmail.TabIndex = 12;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(225, 230, 235);
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("한컴산뜻돋움", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnBack.ForeColor = Color.DimGray;
            btnBack.Location = new Point(109, 409);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(220, 30);
            btnBack.TabIndex = 7;
            btnBack.Text = "돌아가기";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("맑은 고딕", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(45, 45, 45);
            label7.Location = new Point(147, 270);
            label7.Name = "label7";
            label7.Size = new Size(134, 13);
            label7.TabIndex = 4;
            label7.Text = "* 본명으로 기입 해주세요";
            // 
            // Signup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(421, 451);
            Controls.Add(txtEmail);
            Controls.Add(txtName);
            Controls.Add(txtPasswordCheck);
            Controls.Add(txtSignupPassword);
            Controls.Add(txtSignupId);
            Controls.Add(btnBack);
            Controls.Add(btnSignup);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Signup";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Signup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnSignup;
        private TextBox txtSignupId;
        private TextBox txtSignupPassword;
        private TextBox txtPasswordCheck;
        private TextBox txtName;
        private TextBox txtEmail;
        private Button btnBack;
        private Label label7;
    }
}
