namespace calendar4
{
    partial class UserCategoryEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserCategoryEditorForm));
            btnColor = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            nameLabel1 = new Label();
            colorlabel1 = new Label();
            txtName = new TextBox();
            pnlColor = new Panel();
            SuspendLayout();
            // 
            // btnColor
            // 
            btnColor.BackColor = Color.White;
            btnColor.Cursor = Cursors.Hand;
            btnColor.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 220);
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.ForeColor = Color.DimGray;
            btnColor.Location = new Point(184, 68);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(98, 30);
            btnColor.TabIndex = 0;
            btnColor.Text = "색상 선택";
            btnColor.UseVisualStyleBackColor = false;
            btnColor.Click += btnColor_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SteelBlue;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(124, 122);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(76, 32);
            btnSave.TabIndex = 1;
            btnSave.Text = "확인";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(225, 230, 235);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.DimGray;
            btnCancel.Location = new Point(206, 122);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(76, 32);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "취소";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // nameLabel1
            // 
            nameLabel1.AutoSize = true;
            nameLabel1.ForeColor = Color.FromArgb(45, 45, 45);
            nameLabel1.Location = new Point(24, 32);
            nameLabel1.Name = "nameLabel1";
            nameLabel1.Size = new Size(31, 15);
            nameLabel1.TabIndex = 3;
            nameLabel1.Text = "이름";
            // 
            // colorlabel1
            // 
            colorlabel1.AutoSize = true;
            colorlabel1.ForeColor = Color.FromArgb(45, 45, 45);
            colorlabel1.Location = new Point(24, 76);
            colorlabel1.Name = "colorlabel1";
            colorlabel1.Size = new Size(31, 15);
            colorlabel1.TabIndex = 4;
            colorlabel1.Text = "색상";
            // 
            // txtName
            // 
            txtName.BackColor = Color.White;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(45, 45, 45);
            txtName.Location = new Point(84, 28);
            txtName.Name = "txtName";
            txtName.Size = new Size(198, 23);
            txtName.TabIndex = 5;
            // 
            // pnlColor
            // 
            pnlColor.BackColor = Color.White;
            pnlColor.BorderStyle = BorderStyle.FixedSingle;
            pnlColor.Location = new Point(84, 68);
            pnlColor.Name = "pnlColor";
            pnlColor.Size = new Size(90, 30);
            pnlColor.TabIndex = 6;
            // 
            // UserCategoryEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(313, 166);
            Controls.Add(pnlColor);
            Controls.Add(txtName);
            Controls.Add(colorlabel1);
            Controls.Add(nameLabel1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnColor);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UserCategoryEditorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "카테고리 추가";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnColor;
        private Button btnSave;
        private Button btnCancel;
        private Label nameLabel1;
        private Label colorlabel1;
        private TextBox txtName;
        private Panel pnlColor;
    }
}
