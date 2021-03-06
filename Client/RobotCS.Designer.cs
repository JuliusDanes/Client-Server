﻿namespace Client
{
    partial class RobotCS
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
            this.gbxBS = new System.Windows.Forms.GroupBox();
            this.tbxIPBS = new System.Windows.Forms.TextBox();
            this.lblPortBS = new System.Windows.Forms.Label();
            this.tbxPortBS = new System.Windows.Forms.TextBox();
            this.lblIPBS = new System.Windows.Forms.Label();
            this.btnConnectBS = new System.Windows.Forms.Button();
            this.btnDisconnectBS = new System.Windows.Forms.Button();
            this.gbxEncoder = new System.Windows.Forms.GroupBox();
            this.tbxX = new System.Windows.Forms.TextBox();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.tbxY = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxStatus = new System.Windows.Forms.RichTextBox();
            this.gbxRobot = new System.Windows.Forms.GroupBox();
            this.tbxIPRobot = new System.Windows.Forms.TextBox();
            this.lblPortRobot = new System.Windows.Forms.Label();
            this.tbxPortRobot = new System.Windows.Forms.TextBox();
            this.lblIPRobot = new System.Windows.Forms.Label();
            this.btnOpenServer = new System.Windows.Forms.Button();
            this.btnCloseServer = new System.Windows.Forms.Button();
            this.tbxMessage = new System.Windows.Forms.RichTextBox();
            this.gbxGoto = new System.Windows.Forms.GroupBox();
            this.tbxGotoX = new System.Windows.Forms.TextBox();
            this.lblGotoX = new System.Windows.Forms.Label();
            this.lblGotoY = new System.Windows.Forms.Label();
            this.tbxGotoY = new System.Windows.Forms.TextBox();
            this.gbxBS.SuspendLayout();
            this.gbxEncoder.SuspendLayout();
            this.gbxRobot.SuspendLayout();
            this.gbxGoto.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxBS
            // 
            this.gbxBS.Controls.Add(this.tbxIPBS);
            this.gbxBS.Controls.Add(this.lblPortBS);
            this.gbxBS.Controls.Add(this.tbxPortBS);
            this.gbxBS.Controls.Add(this.lblIPBS);
            this.gbxBS.Controls.Add(this.btnConnectBS);
            this.gbxBS.Controls.Add(this.btnDisconnectBS);
            this.gbxBS.Location = new System.Drawing.Point(12, 106);
            this.gbxBS.Name = "gbxBS";
            this.gbxBS.Size = new System.Drawing.Size(303, 85);
            this.gbxBS.TabIndex = 14;
            this.gbxBS.TabStop = false;
            this.gbxBS.Text = "BaseStation";
            // 
            // tbxIPBS
            // 
            this.tbxIPBS.Location = new System.Drawing.Point(37, 19);
            this.tbxIPBS.Name = "tbxIPBS";
            this.tbxIPBS.Size = new System.Drawing.Size(150, 20);
            this.tbxIPBS.TabIndex = 4;
            this.tbxIPBS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Connection_KeyEnter);
            // 
            // lblPortBS
            // 
            this.lblPortBS.AutoSize = true;
            this.lblPortBS.Location = new System.Drawing.Point(5, 52);
            this.lblPortBS.Name = "lblPortBS";
            this.lblPortBS.Size = new System.Drawing.Size(26, 13);
            this.lblPortBS.TabIndex = 6;
            this.lblPortBS.Text = "Port";
            // 
            // tbxPortBS
            // 
            this.tbxPortBS.Location = new System.Drawing.Point(37, 45);
            this.tbxPortBS.Name = "tbxPortBS";
            this.tbxPortBS.Size = new System.Drawing.Size(50, 20);
            this.tbxPortBS.TabIndex = 5;
            this.tbxPortBS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Connection_KeyEnter);
            // 
            // lblIPBS
            // 
            this.lblIPBS.AutoSize = true;
            this.lblIPBS.Location = new System.Drawing.Point(14, 22);
            this.lblIPBS.Name = "lblIPBS";
            this.lblIPBS.Size = new System.Drawing.Size(17, 13);
            this.lblIPBS.TabIndex = 5;
            this.lblIPBS.Text = "IP";
            // 
            // btnConnectBS
            // 
            this.btnConnectBS.Location = new System.Drawing.Point(202, 17);
            this.btnConnectBS.Name = "btnConnectBS";
            this.btnConnectBS.Size = new System.Drawing.Size(75, 23);
            this.btnConnectBS.TabIndex = 6;
            this.btnConnectBS.Text = "Connect";
            this.btnConnectBS.UseVisualStyleBackColor = true;
            this.btnConnectBS.Click += new System.EventHandler(this.btnConnectBS_Click);
            // 
            // btnDisconnectBS
            // 
            this.btnDisconnectBS.Location = new System.Drawing.Point(202, 47);
            this.btnDisconnectBS.Name = "btnDisconnectBS";
            this.btnDisconnectBS.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnectBS.TabIndex = 7;
            this.btnDisconnectBS.Text = "Disconnect";
            this.btnDisconnectBS.UseVisualStyleBackColor = true;
            // 
            // gbxEncoder
            // 
            this.gbxEncoder.Controls.Add(this.tbxX);
            this.gbxEncoder.Controls.Add(this.lblX);
            this.gbxEncoder.Controls.Add(this.lblY);
            this.gbxEncoder.Controls.Add(this.tbxY);
            this.gbxEncoder.Location = new System.Drawing.Point(12, 197);
            this.gbxEncoder.Name = "gbxEncoder";
            this.gbxEncoder.Size = new System.Drawing.Size(97, 69);
            this.gbxEncoder.TabIndex = 30;
            this.gbxEncoder.TabStop = false;
            this.gbxEncoder.Text = "Encoder";
            // 
            // tbxX
            // 
            this.tbxX.Location = new System.Drawing.Point(37, 19);
            this.tbxX.Name = "tbxX";
            this.tbxX.Size = new System.Drawing.Size(50, 20);
            this.tbxX.TabIndex = 8;
            this.tbxX.Text = "0";
            this.tbxX.TextChanged += new System.EventHandler(this.tbxXY_TextChanged);
            this.tbxX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.changeCounter);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(17, 22);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 2;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(17, 46);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 3;
            this.lblY.Text = "Y";
            // 
            // tbxY
            // 
            this.tbxY.Location = new System.Drawing.Point(37, 43);
            this.tbxY.Name = "tbxY";
            this.tbxY.Size = new System.Drawing.Size(50, 20);
            this.tbxY.TabIndex = 9;
            this.tbxY.Text = "0";
            this.tbxY.TextChanged += new System.EventHandler(this.tbxXY_TextChanged);
            this.tbxY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.changeCounter);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(697, 64);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbxStatus
            // 
            this.tbxStatus.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbxStatus.ForeColor = System.Drawing.SystemColors.Info;
            this.tbxStatus.Location = new System.Drawing.Point(322, 106);
            this.tbxStatus.Name = "tbxStatus";
            this.tbxStatus.Size = new System.Drawing.Size(450, 140);
            this.tbxStatus.TabIndex = 14;
            this.tbxStatus.Text = "";
            this.tbxStatus.TextChanged += new System.EventHandler(this.tbxStatus_TextChanged);
            // 
            // gbxRobot
            // 
            this.gbxRobot.Controls.Add(this.tbxIPRobot);
            this.gbxRobot.Controls.Add(this.lblPortRobot);
            this.gbxRobot.Controls.Add(this.tbxPortRobot);
            this.gbxRobot.Controls.Add(this.lblIPRobot);
            this.gbxRobot.Controls.Add(this.btnOpenServer);
            this.gbxRobot.Controls.Add(this.btnCloseServer);
            this.gbxRobot.Location = new System.Drawing.Point(12, 8);
            this.gbxRobot.Name = "gbxRobot";
            this.gbxRobot.Size = new System.Drawing.Size(303, 85);
            this.gbxRobot.TabIndex = 13;
            this.gbxRobot.TabStop = false;
            this.gbxRobot.Text = "Robot";
            // 
            // tbxIPRobot
            // 
            this.tbxIPRobot.Location = new System.Drawing.Point(37, 19);
            this.tbxIPRobot.Name = "tbxIPRobot";
            this.tbxIPRobot.Size = new System.Drawing.Size(150, 20);
            this.tbxIPRobot.TabIndex = 0;
            this.tbxIPRobot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Connection_KeyEnter);
            // 
            // lblPortRobot
            // 
            this.lblPortRobot.AutoSize = true;
            this.lblPortRobot.Location = new System.Drawing.Point(5, 52);
            this.lblPortRobot.Name = "lblPortRobot";
            this.lblPortRobot.Size = new System.Drawing.Size(26, 13);
            this.lblPortRobot.TabIndex = 6;
            this.lblPortRobot.Text = "Port";
            // 
            // tbxPortRobot
            // 
            this.tbxPortRobot.Location = new System.Drawing.Point(37, 45);
            this.tbxPortRobot.Name = "tbxPortRobot";
            this.tbxPortRobot.Size = new System.Drawing.Size(50, 20);
            this.tbxPortRobot.TabIndex = 1;
            this.tbxPortRobot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Connection_KeyEnter);
            // 
            // lblIPRobot
            // 
            this.lblIPRobot.AutoSize = true;
            this.lblIPRobot.Location = new System.Drawing.Point(14, 22);
            this.lblIPRobot.Name = "lblIPRobot";
            this.lblIPRobot.Size = new System.Drawing.Size(17, 13);
            this.lblIPRobot.TabIndex = 5;
            this.lblIPRobot.Text = "IP";
            // 
            // btnOpenServer
            // 
            this.btnOpenServer.Location = new System.Drawing.Point(202, 17);
            this.btnOpenServer.Name = "btnOpenServer";
            this.btnOpenServer.Size = new System.Drawing.Size(75, 23);
            this.btnOpenServer.TabIndex = 2;
            this.btnOpenServer.Text = "Open";
            this.btnOpenServer.UseVisualStyleBackColor = true;
            this.btnOpenServer.Click += new System.EventHandler(this.btnOpenServer_Click);
            // 
            // btnCloseServer
            // 
            this.btnCloseServer.Location = new System.Drawing.Point(202, 47);
            this.btnCloseServer.Name = "btnCloseServer";
            this.btnCloseServer.Size = new System.Drawing.Size(75, 23);
            this.btnCloseServer.TabIndex = 3;
            this.btnCloseServer.Text = "Close";
            this.btnCloseServer.UseVisualStyleBackColor = true;
            this.btnCloseServer.Click += new System.EventHandler(this.btnCloseServer_Click);
            // 
            // tbxMessage
            // 
            this.tbxMessage.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMessage.Location = new System.Drawing.Point(322, 8);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(450, 50);
            this.tbxMessage.TabIndex = 12;
            this.tbxMessage.Text = "";
            this.tbxMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxMessage_KeyDown);
            // 
            // gbxGoto
            // 
            this.gbxGoto.Controls.Add(this.tbxGotoX);
            this.gbxGoto.Controls.Add(this.lblGotoX);
            this.gbxGoto.Controls.Add(this.lblGotoY);
            this.gbxGoto.Controls.Add(this.tbxGotoY);
            this.gbxGoto.Location = new System.Drawing.Point(115, 197);
            this.gbxGoto.Name = "gbxGoto";
            this.gbxGoto.Size = new System.Drawing.Size(97, 69);
            this.gbxGoto.TabIndex = 31;
            this.gbxGoto.TabStop = false;
            this.gbxGoto.Text = "Goto";
            // 
            // tbxGotoX
            // 
            this.tbxGotoX.Location = new System.Drawing.Point(37, 19);
            this.tbxGotoX.Name = "tbxGotoX";
            this.tbxGotoX.Size = new System.Drawing.Size(50, 20);
            this.tbxGotoX.TabIndex = 10;
            this.tbxGotoX.Text = "0";
            this.tbxGotoX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxGoto_KeyDown);
            // 
            // lblGotoX
            // 
            this.lblGotoX.AutoSize = true;
            this.lblGotoX.Location = new System.Drawing.Point(17, 22);
            this.lblGotoX.Name = "lblGotoX";
            this.lblGotoX.Size = new System.Drawing.Size(14, 13);
            this.lblGotoX.TabIndex = 2;
            this.lblGotoX.Text = "X";
            // 
            // lblGotoY
            // 
            this.lblGotoY.AutoSize = true;
            this.lblGotoY.Location = new System.Drawing.Point(17, 46);
            this.lblGotoY.Name = "lblGotoY";
            this.lblGotoY.Size = new System.Drawing.Size(14, 13);
            this.lblGotoY.TabIndex = 3;
            this.lblGotoY.Text = "Y";
            // 
            // tbxGotoY
            // 
            this.tbxGotoY.Location = new System.Drawing.Point(37, 43);
            this.tbxGotoY.Name = "tbxGotoY";
            this.tbxGotoY.Size = new System.Drawing.Size(50, 20);
            this.tbxGotoY.TabIndex = 11;
            this.tbxGotoY.Text = "0";
            this.tbxGotoY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxGoto_KeyDown);
            // 
            // RobotCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.gbxGoto);
            this.Controls.Add(this.gbxEncoder);
            this.Controls.Add(this.gbxBS);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbxStatus);
            this.Controls.Add(this.gbxRobot);
            this.Controls.Add(this.tbxMessage);
            this.Name = "RobotCS";
            this.Text = "Robot CS";
            this.Load += new System.EventHandler(this.RobotCS_Load);
            this.gbxBS.ResumeLayout(false);
            this.gbxBS.PerformLayout();
            this.gbxEncoder.ResumeLayout(false);
            this.gbxEncoder.PerformLayout();
            this.gbxRobot.ResumeLayout(false);
            this.gbxRobot.PerformLayout();
            this.gbxGoto.ResumeLayout(false);
            this.gbxGoto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxBS;
        private System.Windows.Forms.TextBox tbxIPBS;
        private System.Windows.Forms.Label lblPortBS;
        private System.Windows.Forms.TextBox tbxPortBS;
        private System.Windows.Forms.Label lblIPBS;
        private System.Windows.Forms.Button btnConnectBS;
        private System.Windows.Forms.Button btnDisconnectBS;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox tbxStatus;
        private System.Windows.Forms.GroupBox gbxRobot;
        private System.Windows.Forms.TextBox tbxIPRobot;
        private System.Windows.Forms.Label lblPortRobot;
        private System.Windows.Forms.TextBox tbxPortRobot;
        private System.Windows.Forms.Label lblIPRobot;
        private System.Windows.Forms.Button btnOpenServer;
        private System.Windows.Forms.Button btnCloseServer;
        private System.Windows.Forms.RichTextBox tbxMessage;
        private System.Windows.Forms.GroupBox gbxEncoder;
        private System.Windows.Forms.TextBox tbxX;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.TextBox tbxY;
        private System.Windows.Forms.GroupBox gbxGoto;
        private System.Windows.Forms.TextBox tbxGotoX;
        private System.Windows.Forms.Label lblGotoX;
        private System.Windows.Forms.Label lblGotoY;
        private System.Windows.Forms.TextBox tbxGotoY;
    }
}

