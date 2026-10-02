namespace calendar4
{
    partial class Shareinvite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shareinvite));
            label1 = new Label();
            txtEmail = new TextBox();
            btnInvite = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(45, 45, 45);
            label1.Location = new Point(23, 52);
            label1.Name = "label1";
            label1.Size = new Size(42, 17);
            label1.TabIndex = 0;
            label1.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.ForeColor = Color.FromArgb(45, 45, 45);
            txtEmail.Location = new Point(23, 86);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(272, 23);
            txtEmail.TabIndex = 1;
            // 
            // btnInvite
            // 
            btnInvite.BackColor = Color.SteelBlue;
            btnInvite.FlatAppearance.BorderSize = 0;
            btnInvite.FlatStyle = FlatStyle.Flat;
            btnInvite.ForeColor = Color.White;
            btnInvite.Location = new Point(205, 133);
            btnInvite.Name = "btnInvite";
            btnInvite.Size = new Size(90, 30);
            btnInvite.TabIndex = 2;
            btnInvite.Text = "초대";
            btnInvite.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(45, 45, 45);
            label2.Location = new Point(53, 9);
            label2.Name = "label2";
            label2.Size = new Size(216, 25);
            label2.TabIndex = 3;
            label2.Text = "공유캘린더 코드 보내기";
            // 
            // Shareinvite
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(320, 175);
            Controls.Add(label2);
            Controls.Add(btnInvite);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            ForeColor = Color.Coral;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Shareinvite";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shareinvite";

            // Original event connections preserved from base project
            btnInvite.Click += btnInvite_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtEmail;
        private Button btnInvite;
        private Label label2;
    }
}
