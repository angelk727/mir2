namespace LibraryViewer
{
    partial class LoadSettings
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
            button1 = new Button();
            cbPrefix = new ComboBox();
            label1 = new Label();
            cbFront = new CheckBox();
            cbManualPrefix = new CheckBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(159, 51);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(88, 30);
            button1.TabIndex = 0;
            button1.Text = "开始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // cbPrefix
            // 
            cbPrefix.Enabled = false;
            cbPrefix.FormattingEnabled = true;
            cbPrefix.Items.AddRange(new object[] { "00", "000", "0000" });
            cbPrefix.Location = new Point(63, 16);
            cbPrefix.Margin = new Padding(4, 4, 4, 4);
            cbPrefix.Name = "cbPrefix";
            cbPrefix.Size = new Size(62, 25);
            cbPrefix.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 20);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(35, 17);
            label1.TabIndex = 2;
            label1.Text = "前缀:";
            // 
            // cbFront
            // 
            cbFront.AutoSize = true;
            cbFront.Location = new Point(18, 56);
            cbFront.Margin = new Padding(4, 4, 4, 4);
            cbFront.Name = "cbFront";
            cbFront.Size = new Size(75, 21);
            cbFront.TabIndex = 3;
            cbFront.Text = "正面图像";
            cbFront.UseVisualStyleBackColor = true;
            // 
            // cbManualPrefix
            // 
            cbManualPrefix.AutoSize = true;
            cbManualPrefix.Location = new Point(145, 18);
            cbManualPrefix.Margin = new Padding(4, 4, 4, 4);
            cbManualPrefix.Name = "cbManualPrefix";
            cbManualPrefix.Size = new Size(99, 21);
            cbManualPrefix.TabIndex = 4;
            cbManualPrefix.Text = "手动设置前缀";
            cbManualPrefix.UseVisualStyleBackColor = true;
            cbManualPrefix.CheckedChanged += cbManualPrefix_CheckedChanged;
            // 
            // LoadSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(260, 93);
            Controls.Add(cbManualPrefix);
            Controls.Add(cbFront);
            Controls.Add(label1);
            Controls.Add(cbPrefix);
            Controls.Add(button1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "LoadSettings";
            Text = "加载设置";
            EventHandler LoadSettings_Load = null;
            Load += LoadSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ComboBox cbPrefix;
        private Label label1;
        private CheckBox cbFront;
        private CheckBox cbManualPrefix;
    }
}