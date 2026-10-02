namespace calendar4
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            pnlHeader = new Panel();
            lblMemberCount = new Label();
            lblRoomName = new Label();
            pnlInput = new Panel();
            txtMessage = new TextBox();
            btnSend = new Button();
            rtbChat = new RichTextBox();
            pnlHeader.SuspendLayout();
            pnlInput.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            resources.ApplyResources(pnlHeader, "pnlHeader");
            pnlHeader.BackColor = Color.LightSteelBlue;
            pnlHeader.Controls.Add(lblMemberCount);
            pnlHeader.Controls.Add(lblRoomName);
            pnlHeader.Name = "pnlHeader";
            // 
            // lblMemberCount
            // 
            resources.ApplyResources(lblMemberCount, "lblMemberCount");
            lblMemberCount.Name = "lblMemberCount";
            // 
            // lblRoomName
            // 
            resources.ApplyResources(lblRoomName, "lblRoomName");
            lblRoomName.Name = "lblRoomName";
            // 
            // pnlInput
            // 
            resources.ApplyResources(pnlInput, "pnlInput");
            pnlInput.BackColor = SystemColors.Window;
            pnlInput.Controls.Add(txtMessage);
            pnlInput.Controls.Add(btnSend);
            pnlInput.Name = "pnlInput";
            // 
            // txtMessage
            // 
            resources.ApplyResources(txtMessage, "txtMessage");
            txtMessage.Name = "txtMessage";
            // 
            // btnSend
            // 
            resources.ApplyResources(btnSend, "btnSend");
            btnSend.BackColor = Color.Yellow;
            btnSend.Name = "btnSend";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // rtbChat
            // 
            resources.ApplyResources(rtbChat, "rtbChat");
            rtbChat.BackColor = Color.LightSteelBlue;
            rtbChat.Name = "rtbChat";
            rtbChat.ReadOnly = true;
            // 
            // ChatForm
            // 
            AcceptButton = btnSend;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            Controls.Add(rtbChat);
            Controls.Add(pnlInput);
            Controls.Add(pnlHeader);
            Name = "ChatForm";
            pnlHeader.ResumeLayout(false);
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblMemberCount;
        private Label lblRoomName;
        private Panel pnlInput;
        private RichTextBox rtbChat;
        private TextBox txtMessage;
        private Button btnSend;
    }
}