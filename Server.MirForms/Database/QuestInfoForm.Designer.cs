namespace Server
{
    partial class QuestInfoForm
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            QuestInfoPanel = new Panel();
            label5 = new Label();
            TimeLimitTextBox = new TextBox();
            label4 = new Label();
            RequiredMaxLevelTextBox = new TextBox();
            label3 = new Label();
            QFlagTextBox = new TextBox();
            label14 = new Label();
            label12 = new Label();
            label10 = new Label();
            QItemTextBox = new TextBox();
            QKillTextBox = new TextBox();
            QGotoTextBox = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            RequiredClassComboBox = new ComboBox();
            RequiredQuestComboBox = new ComboBox();
            RequiredMinLevelTextBox = new TextBox();
            label2 = new Label();
            QTypeComboBox = new ComboBox();
            label11 = new Label();
            OpenQButton = new Button();
            QFileNameTextBox = new TextBox();
            label29 = new Label();
            QGroupTextBox = new TextBox();
            QNameTextBox = new TextBox();
            label13 = new Label();
            QuestIndexTextBox = new TextBox();
            label1 = new Label();
            RemoveButton = new Button();
            AddButton = new Button();
            QuestInfoListBox = new ListBox();
            PasteMButton = new Button();
            CopyMButton = new Button();
            ExportButton = new Button();
            ImportButton = new Button();
            ExportSelectedButton = new Button();
            QuestSearchBox = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            QuestInfoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(203, 54);
            tabControl1.Margin = new Padding(4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(702, 375);
            tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(QuestInfoPanel);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4);
            tabPage1.Size = new Size(694, 345);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "信息";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // QuestInfoPanel
            // 
            QuestInfoPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            QuestInfoPanel.Controls.Add(label5);
            QuestInfoPanel.Controls.Add(TimeLimitTextBox);
            QuestInfoPanel.Controls.Add(label4);
            QuestInfoPanel.Controls.Add(RequiredMaxLevelTextBox);
            QuestInfoPanel.Controls.Add(label3);
            QuestInfoPanel.Controls.Add(QFlagTextBox);
            QuestInfoPanel.Controls.Add(label14);
            QuestInfoPanel.Controls.Add(label12);
            QuestInfoPanel.Controls.Add(label10);
            QuestInfoPanel.Controls.Add(QItemTextBox);
            QuestInfoPanel.Controls.Add(QKillTextBox);
            QuestInfoPanel.Controls.Add(QGotoTextBox);
            QuestInfoPanel.Controls.Add(label9);
            QuestInfoPanel.Controls.Add(label8);
            QuestInfoPanel.Controls.Add(label7);
            QuestInfoPanel.Controls.Add(RequiredClassComboBox);
            QuestInfoPanel.Controls.Add(RequiredQuestComboBox);
            QuestInfoPanel.Controls.Add(RequiredMinLevelTextBox);
            QuestInfoPanel.Controls.Add(label2);
            QuestInfoPanel.Controls.Add(QTypeComboBox);
            QuestInfoPanel.Controls.Add(label11);
            QuestInfoPanel.Controls.Add(OpenQButton);
            QuestInfoPanel.Controls.Add(QFileNameTextBox);
            QuestInfoPanel.Controls.Add(label29);
            QuestInfoPanel.Controls.Add(QGroupTextBox);
            QuestInfoPanel.Controls.Add(QNameTextBox);
            QuestInfoPanel.Controls.Add(label13);
            QuestInfoPanel.Controls.Add(QuestIndexTextBox);
            QuestInfoPanel.Controls.Add(label1);
            QuestInfoPanel.Enabled = false;
            QuestInfoPanel.Location = new Point(4, 8);
            QuestInfoPanel.Margin = new Padding(4);
            QuestInfoPanel.Name = "QuestInfoPanel";
            QuestInfoPanel.Size = new Size(682, 326);
            QuestInfoPanel.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(453, 196);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(76, 17);
            label5.TabIndex = 59;
            label5.Text = "任务时限(秒)";
            // 
            // TimeLimitTextBox
            // 
            TimeLimitTextBox.Location = new Point(532, 193);
            TimeLimitTextBox.Margin = new Padding(4);
            TimeLimitTextBox.Name = "TimeLimitTextBox";
            TimeLimitTextBox.Size = new Size(140, 23);
            TimeLimitTextBox.TabIndex = 58;
            TimeLimitTextBox.TextChanged += TimeLimitTextBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(425, 43);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(104, 17);
            label4.TabIndex = 57;
            label4.Text = "任务要求最高等级";
            // 
            // RequiredMaxLevelTextBox
            // 
            RequiredMaxLevelTextBox.Location = new Point(532, 40);
            RequiredMaxLevelTextBox.Margin = new Padding(4);
            RequiredMaxLevelTextBox.MaxLength = 3;
            RequiredMaxLevelTextBox.Name = "RequiredMaxLevelTextBox";
            RequiredMaxLevelTextBox.Size = new Size(140, 23);
            RequiredMaxLevelTextBox.TabIndex = 56;
            RequiredMaxLevelTextBox.TextChanged += RequiredMaxLevelTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 286);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 55;
            label3.Text = "标志文本";
            // 
            // QFlagTextBox
            // 
            QFlagTextBox.Location = new Point(102, 283);
            QFlagTextBox.Margin = new Padding(4);
            QFlagTextBox.Name = "QFlagTextBox";
            QFlagTextBox.Size = new Size(209, 23);
            QFlagTextBox.TabIndex = 54;
            QFlagTextBox.TextChanged += QFlagTextBox_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(43, 251);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(56, 17);
            label14.TabIndex = 53;
            label14.Text = "物品文本";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(43, 215);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(56, 17);
            label12.TabIndex = 52;
            label12.Text = "终止文本";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(43, 181);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(56, 17);
            label10.TabIndex = 51;
            label10.Text = "发送文本";
            // 
            // QItemTextBox
            // 
            QItemTextBox.Location = new Point(102, 248);
            QItemTextBox.Margin = new Padding(4);
            QItemTextBox.Name = "QItemTextBox";
            QItemTextBox.Size = new Size(209, 23);
            QItemTextBox.TabIndex = 49;
            QItemTextBox.TextChanged += QItemTextBox_TextChanged;
            // 
            // QKillTextBox
            // 
            QKillTextBox.Location = new Point(102, 212);
            QKillTextBox.Margin = new Padding(4);
            QKillTextBox.Name = "QKillTextBox";
            QKillTextBox.Size = new Size(209, 23);
            QKillTextBox.TabIndex = 48;
            QKillTextBox.TextChanged += QKillTextBox_TextChanged;
            // 
            // QGotoTextBox
            // 
            QGotoTextBox.Location = new Point(102, 178);
            QGotoTextBox.Margin = new Padding(4);
            QGotoTextBox.Name = "QGotoTextBox";
            QGotoTextBox.Size = new Size(209, 23);
            QGotoTextBox.TabIndex = 47;
            QGotoTextBox.TextChanged += QGotoTextBox_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(449, 112);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(80, 17);
            label9.TabIndex = 46;
            label9.Text = "任务职业要求";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(473, 78);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(56, 17);
            label8.TabIndex = 45;
            label8.Text = "前置任务";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(425, 10);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(104, 17);
            label7.TabIndex = 44;
            label7.Text = "任务要求最低等级";
            // 
            // RequiredClassComboBox
            // 
            RequiredClassComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            RequiredClassComboBox.FormattingEnabled = true;
            RequiredClassComboBox.Location = new Point(532, 108);
            RequiredClassComboBox.Margin = new Padding(4);
            RequiredClassComboBox.Name = "RequiredClassComboBox";
            RequiredClassComboBox.Size = new Size(140, 25);
            RequiredClassComboBox.TabIndex = 43;
            RequiredClassComboBox.SelectedIndexChanged += RequiredClassComboBox_SelectedIndexChanged;
            // 
            // RequiredQuestComboBox
            // 
            RequiredQuestComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            RequiredQuestComboBox.FormattingEnabled = true;
            RequiredQuestComboBox.Location = new Point(532, 74);
            RequiredQuestComboBox.Margin = new Padding(4);
            RequiredQuestComboBox.Name = "RequiredQuestComboBox";
            RequiredQuestComboBox.Size = new Size(140, 25);
            RequiredQuestComboBox.TabIndex = 42;
            RequiredQuestComboBox.SelectedIndexChanged += RequiredQuestComboBox_SelectedIndexChanged;
            // 
            // RequiredMinLevelTextBox
            // 
            RequiredMinLevelTextBox.Location = new Point(532, 6);
            RequiredMinLevelTextBox.Margin = new Padding(4);
            RequiredMinLevelTextBox.MaxLength = 3;
            RequiredMinLevelTextBox.Name = "RequiredMinLevelTextBox";
            RequiredMinLevelTextBox.Size = new Size(140, 23);
            RequiredMinLevelTextBox.TabIndex = 41;
            RequiredMinLevelTextBox.TextChanged += RequiredMinLevelTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 112);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 32;
            label2.Text = "任务类型";
            // 
            // QTypeComboBox
            // 
            QTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            QTypeComboBox.FormattingEnabled = true;
            QTypeComboBox.Location = new Point(102, 108);
            QTypeComboBox.Margin = new Padding(4);
            QTypeComboBox.Name = "QTypeComboBox";
            QTypeComboBox.Size = new Size(209, 25);
            QTypeComboBox.TabIndex = 31;
            QTypeComboBox.SelectedIndexChanged += QTypeComboBox_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(42, 145);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(56, 17);
            label11.TabIndex = 23;
            label11.Text = "文件名称";
            // 
            // OpenQButton
            // 
            OpenQButton.Location = new Point(316, 140);
            OpenQButton.Margin = new Padding(4);
            OpenQButton.Name = "OpenQButton";
            OpenQButton.Size = new Size(88, 30);
            OpenQButton.TabIndex = 30;
            OpenQButton.Text = "打开脚本";
            OpenQButton.UseVisualStyleBackColor = true;
            OpenQButton.Click += OpenQButton_Click;
            // 
            // QFileNameTextBox
            // 
            QFileNameTextBox.Location = new Point(102, 143);
            QFileNameTextBox.Margin = new Padding(4);
            QFileNameTextBox.Name = "QFileNameTextBox";
            QFileNameTextBox.Size = new Size(209, 23);
            QFileNameTextBox.TabIndex = 22;
            QFileNameTextBox.TextChanged += QFileNameTextBox_TextChanged;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(43, 77);
            label29.Margin = new Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.Size = new Size(56, 17);
            label29.TabIndex = 21;
            label29.Text = "组队地图";
            // 
            // QGroupTextBox
            // 
            QGroupTextBox.Location = new Point(102, 74);
            QGroupTextBox.Margin = new Padding(4);
            QGroupTextBox.MaxLength = 20;
            QGroupTextBox.Name = "QGroupTextBox";
            QGroupTextBox.Size = new Size(209, 23);
            QGroupTextBox.TabIndex = 20;
            QGroupTextBox.TextChanged += QGroupTextBox_TextChanged;
            // 
            // QNameTextBox
            // 
            QNameTextBox.Location = new Point(102, 40);
            QNameTextBox.Margin = new Padding(4);
            QNameTextBox.MaxLength = 30;
            QNameTextBox.Name = "QNameTextBox";
            QNameTextBox.Size = new Size(209, 23);
            QNameTextBox.TabIndex = 14;
            QNameTextBox.TextChanged += QNameTextBox_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(43, 43);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(56, 17);
            label13.TabIndex = 15;
            label13.Text = "任务名称";
            // 
            // QuestIndexTextBox
            // 
            QuestIndexTextBox.Location = new Point(102, 6);
            QuestIndexTextBox.Margin = new Padding(4);
            QuestIndexTextBox.Name = "QuestIndexTextBox";
            QuestIndexTextBox.ReadOnly = true;
            QuestIndexTextBox.Size = new Size(54, 23);
            QuestIndexTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 4;
            label1.Text = "任务序号";
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(108, 16);
            RemoveButton.Margin = new Padding(4);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(88, 30);
            RemoveButton.TabIndex = 14;
            RemoveButton.Text = "删除";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(14, 16);
            AddButton.Margin = new Padding(4);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(88, 30);
            AddButton.TabIndex = 13;
            AddButton.Text = "添加";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // QuestInfoListBox
            // 
            QuestInfoListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            QuestInfoListBox.FormattingEnabled = true;
            QuestInfoListBox.ItemHeight = 17;
            QuestInfoListBox.Location = new Point(14, 88);
            QuestInfoListBox.Margin = new Padding(4);
            QuestInfoListBox.Name = "QuestInfoListBox";
            QuestInfoListBox.SelectionMode = SelectionMode.MultiExtended;
            QuestInfoListBox.Size = new Size(181, 327);
            QuestInfoListBox.TabIndex = 15;
            QuestInfoListBox.SelectedIndexChanged += QuestInfoListBox_SelectedIndexChanged;
            // 
            // PasteMButton
            // 
            PasteMButton.Location = new Point(298, 16);
            PasteMButton.Margin = new Padding(4);
            PasteMButton.Name = "PasteMButton";
            PasteMButton.Size = new Size(88, 30);
            PasteMButton.TabIndex = 22;
            PasteMButton.Text = "粘贴";
            PasteMButton.UseVisualStyleBackColor = true;
            PasteMButton.Click += PasteMButton_Click;
            // 
            // CopyMButton
            // 
            CopyMButton.Location = new Point(203, 16);
            CopyMButton.Margin = new Padding(4);
            CopyMButton.Name = "CopyMButton";
            CopyMButton.Size = new Size(88, 30);
            CopyMButton.TabIndex = 21;
            CopyMButton.Text = "复制";
            CopyMButton.UseVisualStyleBackColor = true;
            // 
            // ExportButton
            // 
            ExportButton.Location = new Point(818, 16);
            ExportButton.Margin = new Padding(4);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(88, 30);
            ExportButton.TabIndex = 23;
            ExportButton.Text = "全部导出";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportAllButton_Click;
            // 
            // ImportButton
            // 
            ImportButton.Location = new Point(581, 16);
            ImportButton.Margin = new Padding(4);
            ImportButton.Name = "ImportButton";
            ImportButton.Size = new Size(88, 30);
            ImportButton.TabIndex = 24;
            ImportButton.Text = "导入";
            ImportButton.UseVisualStyleBackColor = true;
            ImportButton.Click += ImportButton_Click;
            // 
            // ExportSelectedButton
            // 
            ExportSelectedButton.Location = new Point(674, 16);
            ExportSelectedButton.Margin = new Padding(4);
            ExportSelectedButton.Name = "ExportSelectedButton";
            ExportSelectedButton.Size = new Size(136, 30);
            ExportSelectedButton.TabIndex = 25;
            ExportSelectedButton.Text = "选择导出";
            ExportSelectedButton.UseVisualStyleBackColor = true;
            ExportSelectedButton.Click += ExportSelected_Click;
            // 
            // QuestSearchBox
            // 
            QuestSearchBox.Location = new Point(14, 54);
            QuestSearchBox.Name = "QuestSearchBox";
            QuestSearchBox.PlaceholderText = "搜索";
            QuestSearchBox.Size = new Size(182, 23);
            QuestSearchBox.TabIndex = 26;
            QuestSearchBox.TextChanged += QuestSearchBox_TextChanged;
            // 
            // QuestInfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 435);
            Controls.Add(QuestSearchBox);
            Controls.Add(ExportSelectedButton);
            Controls.Add(ImportButton);
            Controls.Add(ExportButton);
            Controls.Add(PasteMButton);
            Controls.Add(CopyMButton);
            Controls.Add(tabControl1);
            Controls.Add(RemoveButton);
            Controls.Add(AddButton);
            Controls.Add(QuestInfoListBox);
            Margin = new Padding(4);
            Name = "QuestInfoForm";
            Text = "任务信息列表";
            FormClosed += QuestInfoForm_FormClosed;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            QuestInfoPanel.ResumeLayout(false);
            QuestInfoPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel QuestInfoPanel;
        private System.Windows.Forms.TextBox QuestIndexTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button PasteMButton;
        private System.Windows.Forms.Button CopyMButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button ExportSelectedButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button OpenQButton;
        private System.Windows.Forms.TextBox QFileNameTextBox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox QGroupTextBox;
        private System.Windows.Forms.TextBox QNameTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox QuestInfoListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox QTypeComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox RequiredClassComboBox;
        private System.Windows.Forms.ComboBox RequiredQuestComboBox;
        private System.Windows.Forms.TextBox RequiredMinLevelTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox QItemTextBox;
        private System.Windows.Forms.TextBox QKillTextBox;
        private System.Windows.Forms.TextBox QGotoTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox QFlagTextBox;
        private System.Windows.Forms.TextBox RequiredMaxLevelTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TimeLimitTextBox;
        private TextBox QuestSearchBox;
    }
}