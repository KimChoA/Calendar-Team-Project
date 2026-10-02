namespace calendar4
{
    partial class SearchResultsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchResultsForm));
            lblHeader = new Label();
            listResults = new ListBox();
            buttonPanel = new FlowLayoutPanel();
            btnCancel = new Button();
            btnSelect = new Button();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeader.ForeColor = Color.FromArgb(45, 45, 45);
            lblHeader.Location = new Point(0, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(74, 20);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "검색 결과";
            // 
            // listResults
            // 
            listResults.Dock = DockStyle.Fill;
            listResults.DrawMode = DrawMode.OwnerDrawFixed;
            listResults.FormattingEnabled = true;
            listResults.HorizontalScrollbar = true;
            listResults.IntegralHeight = false;
            listResults.ItemHeight = 48;
            listResults.Location = new Point(0, 20);
            listResults.Name = "listResults";
            listResults.Size = new Size(620, 410);
            listResults.TabIndex = 1;
            // 
            // buttonPanel
            // 
            buttonPanel.BackColor = Color.White;
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnSelect);
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Location = new Point(0, 378);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(620, 52);
            buttonPanel.TabIndex = 2;
            buttonPanel.WrapContents = false;
            // 
            // btnCancel
            // 
            btnCancel.AutoSize = true;
            btnCancel.BackColor = Color.FromArgb(225, 230, 235);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.DimGray;
            btnCancel.Location = new Point(542, 9);
            btnCancel.Margin = new Padding(3, 9, 3, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 30);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "닫기";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSelect
            // 
            btnSelect.AutoSize = true;
            btnSelect.BackColor = Color.SteelBlue;
            btnSelect.FlatAppearance.BorderSize = 0;
            btnSelect.FlatStyle = FlatStyle.Flat;
            btnSelect.ForeColor = Color.White;
            btnSelect.Location = new Point(425, 9);
            btnSelect.Margin = new Padding(3, 9, 3, 3);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(111, 30);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "일간 보기로 이동";
            btnSelect.UseVisualStyleBackColor = false;
            // 
            // SearchResultsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(620, 430);
            Controls.Add(buttonPanel);
            Controls.Add(listResults);
            Controls.Add(lblHeader);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(520, 360);
            Name = "SearchResultsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "검색 결과";
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();

            // Original event connections preserved from base project
            listResults.DoubleClick += listResults_DoubleClick;
            btnCancel.Click += btnCancel_Click;
            btnSelect.Click += btnSelect_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHeader;
        private ListBox listResults;
        private FlowLayoutPanel buttonPanel;
        private Button btnCancel;
        private Button btnSelect;
    }
}
