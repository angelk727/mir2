namespace AutoPatcherAdmin
{
    partial class AMain
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
            this.ClientTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HostTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FileLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ListButton = new System.Windows.Forms.Button();
            this.AllowCleanCheckBox = new System.Windows.Forms.CheckBox();
            this.DownloadExistingButton = new System.Windows.Forms.Button();
            this.btnFixGZ = new System.Windows.Forms.Button();
            this.ProtocolDropDown = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClientTextBox
            // 
            this.ClientTextBox.Location = new System.Drawing.Point(115, 16);
            this.ClientTextBox.Name = "ClientTextBox";
            this.ClientTextBox.Size = new System.Drawing.Size(296, 23);
            this.ClientTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "客户端:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "主机地址:";
            // 
            // HostTextBox
            // 
            this.HostTextBox.Location = new System.Drawing.Point(115, 50);
            this.HostTextBox.Name = "HostTextBox";
            this.HostTextBox.Size = new System.Drawing.Size(296, 23);
            this.HostTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "登录:";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(115, 84);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(296, 23);
            this.LoginTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "密码:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(115, 118);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(296, 23);
            this.PasswordTextBox.TabIndex = 7;
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(14, 182);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(88, 30);
            this.ProcessButton.TabIndex = 9;
            this.ProcessButton.Text = "文件校验";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 238);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(434, 21);
            this.progressBar1.TabIndex = 12;
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.Location = new System.Drawing.Point(69, 217);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(32, 17);
            this.ActionLabel.TabIndex = 11;
            this.ActionLabel.Text = "空闲";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "操作:";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(14, 311);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(434, 13);
            this.progressBar2.TabIndex = 13;
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(69, 290);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(32, 17);
            this.SpeedLabel.TabIndex = 15;
            this.SpeedLabel.Text = "空闲";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 290);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "速度:";
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Location = new System.Drawing.Point(69, 273);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(32, 17);
            this.FileLabel.TabIndex = 17;
            this.FileLabel.Text = "空闲";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 273);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "文件:";
            // 
            // ListButton
            // 
            this.ListButton.Location = new System.Drawing.Point(360, 182);
            this.ListButton.Name = "ListButton";
            this.ListButton.Size = new System.Drawing.Size(88, 30);
            this.ListButton.TabIndex = 20;
            this.ListButton.Text = "创建列表";
            this.ListButton.UseVisualStyleBackColor = true;
            this.ListButton.Click += new System.EventHandler(this.ListButton_Click);
            // 
            // AllowCleanCheckBox
            // 
            this.AllowCleanCheckBox.AutoSize = true;
            this.AllowCleanCheckBox.Location = new System.Drawing.Point(115, 152);
            this.AllowCleanCheckBox.Name = "AllowCleanCheckBox";
            this.AllowCleanCheckBox.Size = new System.Drawing.Size(75, 21);
            this.AllowCleanCheckBox.TabIndex = 22;
            this.AllowCleanCheckBox.Text = "允许整理";
            this.AllowCleanCheckBox.UseVisualStyleBackColor = true;
            // 
            // DownloadExistingButton
            // 
            this.DownloadExistingButton.Location = new System.Drawing.Point(197, 182);
            this.DownloadExistingButton.Name = "DownloadExistingButton";
            this.DownloadExistingButton.Size = new System.Drawing.Size(156, 30);
            this.DownloadExistingButton.TabIndex = 23;
            this.DownloadExistingButton.Text = "导入现有数据";
            this.DownloadExistingButton.UseVisualStyleBackColor = true;
            this.DownloadExistingButton.Click += new System.EventHandler(this.DownloadExistingButton_Click);
            // 
            // btnFixGZ
            // 
            this.btnFixGZ.Location = new System.Drawing.Point(108, 182);
            this.btnFixGZ.Name = "btnFixGZ";
            this.btnFixGZ.Size = new System.Drawing.Size(88, 30);
            this.btnFixGZ.TabIndex = 24;
            this.btnFixGZ.Text = "修复 *.gz";
            this.btnFixGZ.UseVisualStyleBackColor = true;
            this.btnFixGZ.Click += new System.EventHandler(this.btnFixGZ_Click);
            // 
            // ProtocolDropDown
            // 
            this.ProtocolDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProtocolDropDown.FormattingEnabled = true;
            this.ProtocolDropDown.Items.AddRange(new object[] {
            "Ftp",
            "SFtp"});
            this.ProtocolDropDown.Location = new System.Drawing.Point(306, 149);
            this.ProtocolDropDown.Name = "ProtocolDropDown";
            this.ProtocolDropDown.Size = new System.Drawing.Size(106, 25);
            this.ProtocolDropDown.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(268, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "协议:";
            // 
            // AMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 336);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ProtocolDropDown);
            this.Controls.Add(this.btnFixGZ);
            this.Controls.Add(this.DownloadExistingButton);
            this.Controls.Add(this.AllowCleanCheckBox);
            this.Controls.Add(this.ListButton);
            this.Controls.Add(this.FileLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ActionLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HostTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClientTextBox);
            this.Name = "AMain";
            this.Text = "自动更新管理窗口";
            this.Load += new System.EventHandler(this.AMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ClientTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HostTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ListButton;
        private System.Windows.Forms.CheckBox AllowCleanCheckBox;
        private System.Windows.Forms.Button DownloadExistingButton;
        private System.Windows.Forms.Button btnFixGZ;
        private System.Windows.Forms.ComboBox ProtocolDropDown;
        private System.Windows.Forms.Label label6;
    }
}

