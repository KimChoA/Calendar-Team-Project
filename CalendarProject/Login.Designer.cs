namespace tap
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            label2 = new Label();
            label3 = new Label();
            txtLoginId = new TextBox();
            txtPassword = new TextBox();
            chkRememberId = new CheckBox();
            btnLogin = new Button();
            btnSignup = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(45, 45, 45);
            label2.Location = new Point(29, 201);
            label2.Name = "label2";
            label2.Size = new Size(47, 17);
            label2.TabIndex = 1;
            label2.Text = "아이디";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(45, 45, 45);
            label3.Location = new Point(29, 248);
            label3.Name = "label3";
            label3.Size = new Size(60, 17);
            label3.TabIndex = 2;
            label3.Text = "비밀번호";
            // 
            // txtLoginId
            // 
            txtLoginId.BackColor = Color.White;
            txtLoginId.BorderStyle = BorderStyle.FixedSingle;
            txtLoginId.ForeColor = Color.FromArgb(45, 45, 45);
            txtLoginId.Location = new Point(140, 195);
            txtLoginId.Name = "txtLoginId";
            txtLoginId.Size = new Size(220, 23);
            txtLoginId.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.ForeColor = Color.FromArgb(45, 45, 45);
            txtPassword.Location = new Point(140, 242);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(220, 23);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkRememberId
            // 
            chkRememberId.AutoSize = true;
            chkRememberId.Font = new Font("한컴산뜻돋움", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            chkRememberId.Location = new Point(29, 291);
            chkRememberId.Name = "chkRememberId";
            chkRememberId.Size = new Size(90, 21);
            chkRememberId.TabIndex = 6;
            chkRememberId.Text = "아이디 저장";
            chkRememberId.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.SteelBlue;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(99, 341);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(220, 30);
            btnLogin.TabIndex = 7;
            btnLogin.TabStop = false;
            btnLogin.Text = "로그인";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnSignup
            // 
            btnSignup.BackColor = Color.FromArgb(225, 230, 235);
            btnSignup.FlatAppearance.BorderSize = 0;
            btnSignup.FlatStyle = FlatStyle.Flat;
            btnSignup.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSignup.ForeColor = Color.DimGray;
            btnSignup.Location = new Point(99, 392);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(220, 30);
            btnSignup.TabIndex = 8;
            btnSignup.TabStop = false;
            btnSignup.Text = "회원가입";
            btnSignup.UseVisualStyleBackColor = false;
            btnSignup.Click += btnSignup_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(29, 34);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(365, 122);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // Login
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(421, 451);
            Controls.Add(pictureBox1);
            Controls.Add(btnSignup);
            Controls.Add(btnLogin);
            Controls.Add(chkRememberId);
            Controls.Add(txtPassword);
            Controls.Add(txtLoginId);
            Controls.Add(label3);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += Login_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private TextBox txtLoginId;
        private TextBox txtPassword;
        private CheckBox chkRememberId;
        private Button btnLogin;
        private Button btnSignup;
        private PictureBox pictureBox1;
    }
}
