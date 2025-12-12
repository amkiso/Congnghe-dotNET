namespace KhachSanSaoBang.ChatBox
{
    partial class Chat
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
            this.btn_ClearHistory = new System.Windows.Forms.Button();
            this.btn_ViewHistory = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.rtb_Chat = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ClearHistory
            // 
            this.btn_ClearHistory.Location = new System.Drawing.Point(744, 100);
            this.btn_ClearHistory.Name = "btn_ClearHistory";
            this.btn_ClearHistory.Size = new System.Drawing.Size(99, 54);
            this.btn_ClearHistory.TabIndex = 13;
            this.btn_ClearHistory.Text = "XÓA LỊCH SỬ CHAT";
            this.btn_ClearHistory.UseVisualStyleBackColor = true;
            // 
            // btn_ViewHistory
            // 
            this.btn_ViewHistory.Location = new System.Drawing.Point(594, 100);
            this.btn_ViewHistory.Name = "btn_ViewHistory";
            this.btn_ViewHistory.Size = new System.Drawing.Size(99, 54);
            this.btn_ViewHistory.TabIndex = 12;
            this.btn_ViewHistory.Text = "HISTORY";
            this.btn_ViewHistory.UseVisualStyleBackColor = true;
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(442, 100);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(99, 54);
            this.btn_Send.TabIndex = 11;
            this.btn_Send.Text = "SEND";
            this.btn_Send.UseVisualStyleBackColor = true;
            // 
            // txt_Message
            // 
            this.txt_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_Message.Location = new System.Drawing.Point(426, 21);
            this.txt_Message.Multiline = true;
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(417, 45);
            this.txt_Message.TabIndex = 9;
            // 
            // rtb_Chat
            // 
            this.rtb_Chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Chat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rtb_Chat.Location = new System.Drawing.Point(3, 3);
            this.rtb_Chat.Name = "rtb_Chat";
            this.rtb_Chat.ReadOnly = true;
            this.rtb_Chat.Size = new System.Drawing.Size(1629, 583);
            this.rtb_Chat.TabIndex = 10;
            this.rtb_Chat.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.rtb_Chat, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.1713F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.8287F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1635, 864);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Message);
            this.groupBox1.Controls.Add(this.btn_Send);
            this.groupBox1.Controls.Add(this.btn_ViewHistory);
            this.groupBox1.Controls.Add(this.btn_ClearHistory);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 592);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1629, 269);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1635, 864);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Chat";
            this.Text = "Chat";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ClearHistory;
        private System.Windows.Forms.Button btn_ViewHistory;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.RichTextBox rtb_Chat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}