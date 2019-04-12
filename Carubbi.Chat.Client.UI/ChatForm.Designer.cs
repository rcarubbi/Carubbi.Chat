using System.Windows.Forms;
using System.Drawing;
namespace Carubbi.Chat.Client.UI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblMensagem = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSend = new System.Windows.Forms.Button();
            this.picToogleConnect = new System.Windows.Forms.PictureBox();
            this.picBroadcast = new System.Windows.Forms.PictureBox();
            this.chkFalar = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picToogleConnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBroadcast)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Location = new System.Drawing.Point(13, 514);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(653, 28);
            this.txtMessage.TabIndex = 2;
            // 
            // lstUsers
            // 
            this.lstUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstUsers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.ItemHeight = 20;
            this.lstUsers.Location = new System.Drawing.Point(560, 66);
            this.lstUsers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(159, 382);
            this.lstUsers.TabIndex = 5;
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.SystemColors.Window;
            this.txtChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChat.Location = new System.Drawing.Point(13, 66);
            this.txtChat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(539, 392);
            this.txtChat.TabIndex = 6;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Carubbi\'s Chat";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // lblMensagem
            // 
            this.lblMensagem.AutoSize = true;
            this.lblMensagem.Location = new System.Drawing.Point(11, 495);
            this.lblMensagem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensagem.Name = "lblMensagem";
            this.lblMensagem.Size = new System.Drawing.Size(117, 20);
            this.lblMensagem.TabIndex = 8;
            this.lblMensagem.Text = "Mensagem:";
            // 
            // txtLogin
            // 
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Location = new System.Drawing.Point(13, 29);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLogin.Multiline = true;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(539, 30);
            this.txtLogin.TabIndex = 9;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(15, 13);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(69, 20);
            this.lblLogin.TabIndex = 10;
            this.lblLogin.Text = "Login:";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.BackgroundImage = global::Carubbi.Chat.Client.UI.Properties.Resources.Chat;
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(673, 493);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(46, 45);
            this.btnSend.TabIndex = 7;
            this.tooltip.SetToolTip(this.btnSend, "Enviar mensagem");
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // picToogleConnect
            // 
            this.picToogleConnect.BackgroundImage = global::Carubbi.Chat.Client.UI.Properties.Resources.logoff;
            this.picToogleConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picToogleConnect.Location = new System.Drawing.Point(560, 12);
            this.picToogleConnect.Name = "picToogleConnect";
            this.picToogleConnect.Size = new System.Drawing.Size(48, 48);
            this.picToogleConnect.TabIndex = 13;
            this.picToogleConnect.TabStop = false;
            this.tooltip.SetToolTip(this.picToogleConnect, "Conectar");
            this.picToogleConnect.Click += new System.EventHandler(this.picToogleConnect_Click);
            // 
            // picBroadcast
            // 
            this.picBroadcast.BackColor = System.Drawing.Color.White;
            this.picBroadcast.BackgroundImage = global::Carubbi.Chat.Client.UI.Properties.Resources.megaphone_sign_3d_icon;
            this.picBroadcast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBroadcast.Location = new System.Drawing.Point(614, 12);
            this.picBroadcast.Name = "picBroadcast";
            this.picBroadcast.Size = new System.Drawing.Size(48, 48);
            this.picBroadcast.TabIndex = 12;
            this.picBroadcast.TabStop = false;
            this.tooltip.SetToolTip(this.picBroadcast, "Broadcast");
            this.picBroadcast.Click += new System.EventHandler(this.picBroadcast_Click);
            // 
            // chkFalar
            // 
            this.chkFalar.AutoSize = true;
            this.chkFalar.Location = new System.Drawing.Point(560, 464);
            this.chkFalar.Name = "chkFalar";
            this.chkFalar.Size = new System.Drawing.Size(82, 24);
            this.chkFalar.TabIndex = 14;
            this.chkFalar.Text = "Falar";
            this.chkFalar.UseVisualStyleBackColor = true;
            // 
            // ChatForm
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(735, 548);
            this.Controls.Add(this.chkFalar);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblMensagem);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.picToogleConnect);
            this.Controls.Add(this.picBroadcast);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChatForm";
            this.Text = "Carubbi\'s Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Resize += new System.EventHandler(this.ChatForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picToogleConnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBroadcast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

     

        private TextBox txtMessage;
        private ListBox lstUsers;
        private TextBox txtChat;
        private Button btnSend;
        private NotifyIcon notifyIcon1;
        private Label lblMensagem;
        private TextBox txtLogin;
        private Label lblLogin;
        private PictureBox picToogleConnect;
        private PictureBox picBroadcast;
        private ToolTip tooltip;

        #endregion
        private CheckBox chkFalar;
    }
}

