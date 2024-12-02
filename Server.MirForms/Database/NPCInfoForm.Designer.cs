namespace Server
{
    partial class NPCInfoForm
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
            ReloadScriptButton = new Button();
            NPCPreview = new PictureBox();
            TeleportToCheckBox = new CheckBox();
            label15 = new Label();
            BigMapIconTextBox = new TextBox();
            ShowBigMapCheckBox = new CheckBox();
            label14 = new Label();
            ConquestHidden_combo = new ComboBox();
            label2 = new Label();
            MapComboBox = new ComboBox();
            label11 = new Label();
            OpenNButton = new Button();
            NFileNameTextBox = new TextBox();
            label29 = new Label();
            NRateTextBox = new TextBox();
            ClearHButton = new Button();
            NNameTextBox = new TextBox();
            label13 = new Label();
            NPCIndexTextBox = new TextBox();
            label24 = new Label();
            label1 = new Label();
            NImageTextBox = new TextBox();
            NXTextBox = new TextBox();
            label28 = new Label();
            label30 = new Label();
            NYTextBox = new TextBox();
            tabPage2 = new TabPage();
            ConquestVisible_checkbox = new CheckBox();
            Flag_textbox = new TextBox();
            label12 = new Label();
            label10 = new Label();
            Day_combo = new ComboBox();
            Class_combo = new ComboBox();
            EndMin_num = new NumericUpDown();
            EndHour_combo = new ComboBox();
            label8 = new Label();
            label9 = new Label();
            StartMin_num = new NumericUpDown();
            StartHour_combo = new ComboBox();
            TimeVisible_checkbox = new CheckBox();
            label7 = new Label();
            MaxLev_textbox = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            MinLev_textbox = new TextBox();
            RemoveButton = new Button();
            AddButton = new Button();
            NPCInfoListBox = new ListBox();
            PasteMButton = new Button();
            CopyMButton = new Button();
            ExportButton = new Button();
            ImportButton = new Button();
            ExportSelectedButton = new Button();
            NPCSearchTxt = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NPCPreview).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EndMin_num).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartMin_num).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(298, 53);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(606, 384);
            tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(ReloadScriptButton);
            tabPage1.Controls.Add(NPCPreview);
            tabPage1.Controls.Add(TeleportToCheckBox);
            tabPage1.Controls.Add(label15);
            tabPage1.Controls.Add(BigMapIconTextBox);
            tabPage1.Controls.Add(ShowBigMapCheckBox);
            tabPage1.Controls.Add(label14);
            tabPage1.Controls.Add(ConquestHidden_combo);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(MapComboBox);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(OpenNButton);
            tabPage1.Controls.Add(NFileNameTextBox);
            tabPage1.Controls.Add(label29);
            tabPage1.Controls.Add(NRateTextBox);
            tabPage1.Controls.Add(ClearHButton);
            tabPage1.Controls.Add(NNameTextBox);
            tabPage1.Controls.Add(label13);
            tabPage1.Controls.Add(NPCIndexTextBox);
            tabPage1.Controls.Add(label24);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(NImageTextBox);
            tabPage1.Controls.Add(NXTextBox);
            tabPage1.Controls.Add(label28);
            tabPage1.Controls.Add(label30);
            tabPage1.Controls.Add(NYTextBox);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 3, 4, 3);
            tabPage1.Size = new Size(598, 354);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "信息";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // ReloadScriptButton
            // 
            ReloadScriptButton.Location = new Point(68, 231);
            ReloadScriptButton.Margin = new Padding(4, 3, 4, 3);
            ReloadScriptButton.Name = "ReloadScriptButton";
            ReloadScriptButton.Size = new Size(88, 31);
            ReloadScriptButton.TabIndex = 65;
            ReloadScriptButton.Text = "重载脚本";
            ReloadScriptButton.UseVisualStyleBackColor = true;
            ReloadScriptButton.Click += ReloadScriptButton_Click;
            // 
            // NPCPreview
            // 
            NPCPreview.Location = new Point(314, 3);
            NPCPreview.Name = "NPCPreview";
            NPCPreview.Size = new Size(282, 348);
            NPCPreview.TabIndex = 64;
            NPCPreview.TabStop = false;
            // 
            // TeleportToCheckBox
            // 
            TeleportToCheckBox.AutoSize = true;
            TeleportToCheckBox.Location = new Point(220, 311);
            TeleportToCheckBox.Margin = new Padding(4, 3, 4, 3);
            TeleportToCheckBox.Name = "TeleportToCheckBox";
            TeleportToCheckBox.Size = new Size(87, 21);
            TeleportToCheckBox.TabIndex = 63;
            TeleportToCheckBox.Text = "是否可传送";
            TeleportToCheckBox.UseVisualStyleBackColor = true;
            TeleportToCheckBox.CheckedChanged += TeleportToCheckBox_CheckedChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(105, 312);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(59, 17);
            label15.TabIndex = 62;
            label15.Text = "显示图标:";
            // 
            // BigMapIconTextBox
            // 
            BigMapIconTextBox.Location = new Point(167, 309);
            BigMapIconTextBox.Margin = new Padding(4, 3, 4, 3);
            BigMapIconTextBox.MaxLength = 5;
            BigMapIconTextBox.Name = "BigMapIconTextBox";
            BigMapIconTextBox.Size = new Size(42, 23);
            BigMapIconTextBox.TabIndex = 61;
            BigMapIconTextBox.TextChanged += BigMapIconTextBox_TextChanged;
            // 
            // ShowBigMapCheckBox
            // 
            ShowBigMapCheckBox.AutoSize = true;
            ShowBigMapCheckBox.Location = new Point(16, 311);
            ShowBigMapCheckBox.Margin = new Padding(4, 3, 4, 3);
            ShowBigMapCheckBox.Name = "ShowBigMapCheckBox";
            ShowBigMapCheckBox.Size = new Size(87, 21);
            ShowBigMapCheckBox.TabIndex = 60;
            ShowBigMapCheckBox.Text = "大地图显示";
            ShowBigMapCheckBox.UseVisualStyleBackColor = true;
            ShowBigMapCheckBox.CheckedChanged += ShowBigMapCheckBox_CheckedChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(16, 272);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(83, 17);
            label14.TabIndex = 59;
            label14.Text = "所属攻城城堡:";
            // 
            // ConquestHidden_combo
            // 
            ConquestHidden_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            ConquestHidden_combo.FormattingEnabled = true;
            ConquestHidden_combo.Items.AddRange(new object[] { "", "战士", "法师", "道士", "刺客", "弓箭" });
            ConquestHidden_combo.Location = new Point(102, 268);
            ConquestHidden_combo.Margin = new Padding(4, 3, 4, 3);
            ConquestHidden_combo.Name = "ConquestHidden_combo";
            ConquestHidden_combo.Size = new Size(153, 25);
            ConquestHidden_combo.TabIndex = 58;
            ConquestHidden_combo.SelectedIndexChanged += ConquestHidden_combo_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 133);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(59, 17);
            label2.TabIndex = 32;
            label2.Text = "所在地图:";
            // 
            // MapComboBox
            // 
            MapComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            MapComboBox.FormattingEnabled = true;
            MapComboBox.Location = new Point(102, 129);
            MapComboBox.Margin = new Padding(4, 3, 4, 3);
            MapComboBox.Name = "MapComboBox";
            MapComboBox.Size = new Size(153, 25);
            MapComboBox.TabIndex = 31;
            MapComboBox.SelectedIndexChanged += MapComboBox_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(16, 64);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(83, 17);
            label11.TabIndex = 23;
            label11.Text = "脚本文件路径:";
            // 
            // OpenNButton
            // 
            OpenNButton.Location = new Point(163, 21);
            OpenNButton.Margin = new Padding(4, 3, 4, 3);
            OpenNButton.Name = "OpenNButton";
            OpenNButton.Size = new Size(88, 31);
            OpenNButton.TabIndex = 30;
            OpenNButton.Text = "打开脚本";
            OpenNButton.UseVisualStyleBackColor = true;
            OpenNButton.Click += OpenNButton_Click;
            // 
            // NFileNameTextBox
            // 
            NFileNameTextBox.Location = new Point(102, 61);
            NFileNameTextBox.Margin = new Padding(4, 3, 4, 3);
            NFileNameTextBox.MaxLength = 50;
            NFileNameTextBox.Name = "NFileNameTextBox";
            NFileNameTextBox.Size = new Size(209, 23);
            NFileNameTextBox.TabIndex = 22;
            NFileNameTextBox.TextChanged += NFileNameTextBox_TextChanged;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(150, 200);
            label29.Margin = new Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.Size = new Size(59, 17);
            label29.TabIndex = 21;
            label29.Text = "交易费率:";
            // 
            // NRateTextBox
            // 
            NRateTextBox.Location = new Point(212, 197);
            NRateTextBox.Margin = new Padding(4, 3, 4, 3);
            NRateTextBox.MaxLength = 3;
            NRateTextBox.Name = "NRateTextBox";
            NRateTextBox.Size = new Size(42, 23);
            NRateTextBox.TabIndex = 20;
            NRateTextBox.TextChanged += NRateTextBox_TextChanged;
            // 
            // ClearHButton
            // 
            ClearHButton.Location = new Point(181, 231);
            ClearHButton.Margin = new Padding(4, 3, 4, 3);
            ClearHButton.Name = "ClearHButton";
            ClearHButton.Size = new Size(88, 31);
            ClearHButton.TabIndex = 19;
            ClearHButton.Text = "清除回收记录";
            ClearHButton.UseVisualStyleBackColor = true;
            ClearHButton.Click += ClearHButton_Click;
            // 
            // NNameTextBox
            // 
            NNameTextBox.Location = new Point(102, 95);
            NNameTextBox.Margin = new Padding(4, 3, 4, 3);
            NNameTextBox.Name = "NNameTextBox";
            NNameTextBox.Size = new Size(209, 23);
            NNameTextBox.TabIndex = 14;
            NNameTextBox.TextChanged += NNameTextBox_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(29, 98);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(59, 17);
            label13.TabIndex = 15;
            label13.Text = "游戏名称:";
            // 
            // NPCIndexTextBox
            // 
            NPCIndexTextBox.Location = new Point(102, 25);
            NPCIndexTextBox.Margin = new Padding(4, 3, 4, 3);
            NPCIndexTextBox.Name = "NPCIndexTextBox";
            NPCIndexTextBox.ReadOnly = true;
            NPCIndexTextBox.Size = new Size(54, 23);
            NPCIndexTextBox.TabIndex = 0;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(21, 201);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(78, 17);
            label24.TabIndex = 13;
            label24.Text = "NPC外形LIB:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 28);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(60, 17);
            label1.TabIndex = 4;
            label1.Text = "NPC序号:";
            // 
            // NImageTextBox
            // 
            NImageTextBox.Location = new Point(102, 198);
            NImageTextBox.Margin = new Padding(4, 3, 4, 3);
            NImageTextBox.MaxLength = 5;
            NImageTextBox.Name = "NImageTextBox";
            NImageTextBox.Size = new Size(42, 23);
            NImageTextBox.TabIndex = 11;
            NImageTextBox.TextChanged += NImageTextBox_TextChanged;
            // 
            // NXTextBox
            // 
            NXTextBox.Location = new Point(102, 164);
            NXTextBox.Margin = new Padding(4, 3, 4, 3);
            NXTextBox.MaxLength = 5;
            NXTextBox.Name = "NXTextBox";
            NXTextBox.Size = new Size(42, 23);
            NXTextBox.TabIndex = 2;
            NXTextBox.TextChanged += NXTextBox_TextChanged;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(163, 167);
            label28.Margin = new Padding(4, 0, 4, 0);
            label28.Name = "label28";
            label28.Size = new Size(46, 17);
            label28.TabIndex = 10;
            label28.Text = "坐标 Y:";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(52, 167);
            label30.Margin = new Padding(4, 0, 4, 0);
            label30.Name = "label30";
            label30.Size = new Size(47, 17);
            label30.TabIndex = 3;
            label30.Text = "坐标 X:";
            // 
            // NYTextBox
            // 
            NYTextBox.Location = new Point(212, 164);
            NYTextBox.Margin = new Padding(4, 3, 4, 3);
            NYTextBox.MaxLength = 5;
            NYTextBox.Name = "NYTextBox";
            NYTextBox.Size = new Size(42, 23);
            NYTextBox.TabIndex = 3;
            NYTextBox.TextChanged += NYTextBox_TextChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(ConquestVisible_checkbox);
            tabPage2.Controls.Add(Flag_textbox);
            tabPage2.Controls.Add(label12);
            tabPage2.Controls.Add(label10);
            tabPage2.Controls.Add(Day_combo);
            tabPage2.Controls.Add(Class_combo);
            tabPage2.Controls.Add(EndMin_num);
            tabPage2.Controls.Add(EndHour_combo);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(StartMin_num);
            tabPage2.Controls.Add(StartHour_combo);
            tabPage2.Controls.Add(TimeVisible_checkbox);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(MaxLev_textbox);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(MinLev_textbox);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Margin = new Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(598, 354);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "定时可见";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ConquestVisible_checkbox
            // 
            ConquestVisible_checkbox.AutoSize = true;
            ConquestVisible_checkbox.CheckAlign = ContentAlignment.MiddleRight;
            ConquestVisible_checkbox.Location = new Point(200, 182);
            ConquestVisible_checkbox.Margin = new Padding(4, 3, 4, 3);
            ConquestVisible_checkbox.Name = "ConquestVisible_checkbox";
            ConquestVisible_checkbox.Size = new Size(99, 21);
            ConquestVisible_checkbox.TabIndex = 56;
            ConquestVisible_checkbox.Text = "攻城期间可见";
            ConquestVisible_checkbox.UseVisualStyleBackColor = true;
            ConquestVisible_checkbox.CheckedChanged += ConquestVisible_checkbox_CheckedChanged;
            // 
            // Flag_textbox
            // 
            Flag_textbox.Location = new Point(131, 142);
            Flag_textbox.Margin = new Padding(4, 3, 4, 3);
            Flag_textbox.MaxLength = 3;
            Flag_textbox.Name = "Flag_textbox";
            Flag_textbox.Size = new Size(56, 23);
            Flag_textbox.TabIndex = 55;
            Flag_textbox.TextChanged += Flag_textbox_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(72, 146);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(56, 17);
            label12.TabIndex = 54;
            label12.Text = "需要标志";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(72, 110);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(56, 17);
            label10.TabIndex = 53;
            label10.Text = "日期显示";
            // 
            // Day_combo
            // 
            Day_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            Day_combo.FormattingEnabled = true;
            Day_combo.Items.AddRange(new object[] { "", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });
            Day_combo.Location = new Point(131, 106);
            Day_combo.Margin = new Padding(4, 3, 4, 3);
            Day_combo.Name = "Day_combo";
            Day_combo.Size = new Size(190, 25);
            Day_combo.TabIndex = 52;
            Day_combo.SelectedIndexChanged += Day_combo_SelectedIndexChanged;
            // 
            // Class_combo
            // 
            Class_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            Class_combo.FormattingEnabled = true;
            Class_combo.Items.AddRange(new object[] { "", "战士", "法师", "道士", "刺客", "弓箭" });
            Class_combo.Location = new Point(131, 66);
            Class_combo.Margin = new Padding(4, 3, 4, 3);
            Class_combo.Name = "Class_combo";
            Class_combo.Size = new Size(101, 25);
            Class_combo.TabIndex = 51;
            Class_combo.SelectedIndexChanged += Class_combo_SelectedIndexChanged;
            // 
            // EndMin_num
            // 
            EndMin_num.Location = new Point(297, 251);
            EndMin_num.Margin = new Padding(4, 3, 4, 3);
            EndMin_num.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            EndMin_num.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            EndMin_num.Name = "EndMin_num";
            EndMin_num.Size = new Size(55, 23);
            EndMin_num.TabIndex = 50;
            EndMin_num.Value = new decimal(new int[] { 1, 0, 0, 0 });
            EndMin_num.ValueChanged += EndMin_num_ValueChanged;
            // 
            // EndHour_combo
            // 
            EndHour_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            EndHour_combo.FormattingEnabled = true;
            EndHour_combo.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" });
            EndHour_combo.Location = new Point(131, 250);
            EndHour_combo.Margin = new Padding(4, 3, 4, 3);
            EndHour_combo.Name = "EndHour_combo";
            EndHour_combo.Size = new Size(56, 25);
            EndHour_combo.TabIndex = 49;
            EndHour_combo.SelectedIndexChanged += EndHour_combo_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(203, 254);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(91, 17);
            label8.TabIndex = 48;
            label8.Text = "结束时间(分钟):";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(37, 254);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(91, 17);
            label9.TabIndex = 47;
            label9.Text = "结束时间(小时):";
            // 
            // StartMin_num
            // 
            StartMin_num.Location = new Point(297, 217);
            StartMin_num.Margin = new Padding(4, 3, 4, 3);
            StartMin_num.Maximum = new decimal(new int[] { 58, 0, 0, 0 });
            StartMin_num.Name = "StartMin_num";
            StartMin_num.Size = new Size(55, 23);
            StartMin_num.TabIndex = 46;
            StartMin_num.ValueChanged += StartMin_num_ValueChanged;
            // 
            // StartHour_combo
            // 
            StartHour_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            StartHour_combo.FormattingEnabled = true;
            StartHour_combo.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" });
            StartHour_combo.Location = new Point(131, 216);
            StartHour_combo.Margin = new Padding(4, 3, 4, 3);
            StartHour_combo.Name = "StartHour_combo";
            StartHour_combo.Size = new Size(56, 25);
            StartHour_combo.TabIndex = 45;
            StartHour_combo.SelectedIndexChanged += StartHour_combo_SelectedIndexChanged;
            // 
            // TimeVisible_checkbox
            // 
            TimeVisible_checkbox.AutoSize = true;
            TimeVisible_checkbox.CheckAlign = ContentAlignment.MiddleRight;
            TimeVisible_checkbox.Location = new Point(35, 182);
            TimeVisible_checkbox.Margin = new Padding(4, 3, 4, 3);
            TimeVisible_checkbox.Name = "TimeVisible_checkbox";
            TimeVisible_checkbox.Size = new Size(123, 21);
            TimeVisible_checkbox.TabIndex = 44;
            TimeVisible_checkbox.Text = "仅在设定时间可见";
            TimeVisible_checkbox.UseVisualStyleBackColor = true;
            TimeVisible_checkbox.CheckedChanged += TimeVisible_checkbox_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(205, 35);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(83, 17);
            label7.TabIndex = 43;
            label7.Text = "使用最高等级:";
            // 
            // MaxLev_textbox
            // 
            MaxLev_textbox.Location = new Point(291, 32);
            MaxLev_textbox.Margin = new Padding(4, 3, 4, 3);
            MaxLev_textbox.MaxLength = 3;
            MaxLev_textbox.Name = "MaxLev_textbox";
            MaxLev_textbox.Size = new Size(56, 23);
            MaxLev_textbox.TabIndex = 42;
            MaxLev_textbox.TextChanged += MaxLev_textbox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(48, 70);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(80, 17);
            label6.TabIndex = 40;
            label6.Text = "使用所需职业";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(203, 220);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(91, 17);
            label5.TabIndex = 37;
            label5.Text = "出现时间(分钟):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 220);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(91, 17);
            label4.TabIndex = 36;
            label4.Text = "出现时间(小时):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 36);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(83, 17);
            label3.TabIndex = 34;
            label3.Text = "使用最低等级:";
            // 
            // MinLev_textbox
            // 
            MinLev_textbox.Location = new Point(131, 32);
            MinLev_textbox.Margin = new Padding(4, 3, 4, 3);
            MinLev_textbox.MaxLength = 3;
            MinLev_textbox.Name = "MinLev_textbox";
            MinLev_textbox.Size = new Size(56, 23);
            MinLev_textbox.TabIndex = 33;
            MinLev_textbox.TextChanged += MinLev_textbox_TextChanged;
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(108, 16);
            RemoveButton.Margin = new Padding(4, 3, 4, 3);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(88, 31);
            RemoveButton.TabIndex = 14;
            RemoveButton.Text = "删除";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(14, 16);
            AddButton.Margin = new Padding(4, 3, 4, 3);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(88, 31);
            AddButton.TabIndex = 13;
            AddButton.Text = "添加";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // NPCInfoListBox
            // 
            NPCInfoListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            NPCInfoListBox.FormattingEnabled = true;
            NPCInfoListBox.ItemHeight = 17;
            NPCInfoListBox.Location = new Point(14, 53);
            NPCInfoListBox.Margin = new Padding(4, 3, 4, 3);
            NPCInfoListBox.Name = "NPCInfoListBox";
            NPCInfoListBox.SelectionMode = SelectionMode.MultiExtended;
            NPCInfoListBox.Size = new Size(276, 378);
            NPCInfoListBox.TabIndex = 15;
            NPCInfoListBox.SelectedIndexChanged += NPCInfoListBox_SelectedIndexChanged;
            // 
            // PasteMButton
            // 
            PasteMButton.Location = new Point(298, 16);
            PasteMButton.Margin = new Padding(4, 3, 4, 3);
            PasteMButton.Name = "PasteMButton";
            PasteMButton.Size = new Size(88, 31);
            PasteMButton.TabIndex = 22;
            PasteMButton.Text = "粘贴";
            PasteMButton.UseVisualStyleBackColor = true;
            PasteMButton.Click += PasteMButton_Click;
            // 
            // CopyMButton
            // 
            CopyMButton.Location = new Point(203, 16);
            CopyMButton.Margin = new Padding(4, 3, 4, 3);
            CopyMButton.Name = "CopyMButton";
            CopyMButton.Size = new Size(88, 31);
            CopyMButton.TabIndex = 21;
            CopyMButton.Text = "复制";
            CopyMButton.UseVisualStyleBackColor = true;
            CopyMButton.Click += CopyMButton_Click;
            // 
            // ExportButton
            // 
            ExportButton.Location = new Point(818, 16);
            ExportButton.Margin = new Padding(4, 3, 4, 3);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(88, 31);
            ExportButton.TabIndex = 23;
            ExportButton.Text = "全部导出";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportAllButton_Click;
            // 
            // ImportButton
            // 
            ImportButton.Location = new Point(581, 16);
            ImportButton.Margin = new Padding(4, 3, 4, 3);
            ImportButton.Name = "ImportButton";
            ImportButton.Size = new Size(88, 31);
            ImportButton.TabIndex = 24;
            ImportButton.Text = "导入";
            ImportButton.UseVisualStyleBackColor = true;
            ImportButton.Click += ImportButton_Click;
            // 
            // ExportSelectedButton
            // 
            ExportSelectedButton.Location = new Point(674, 16);
            ExportSelectedButton.Margin = new Padding(4, 3, 4, 3);
            ExportSelectedButton.Name = "ExportSelectedButton";
            ExportSelectedButton.Size = new Size(136, 31);
            ExportSelectedButton.TabIndex = 25;
            ExportSelectedButton.Text = "选择导出";
            ExportSelectedButton.UseVisualStyleBackColor = true;
            ExportSelectedButton.Click += ExportSelected_Click;
            // 
            // NPCSearchTxt
            // 
            NPCSearchTxt.Location = new Point(394, 20);
            NPCSearchTxt.Margin = new Padding(4, 3, 4, 3);
            NPCSearchTxt.Name = "NPCSearchTxt";
            NPCSearchTxt.Size = new Size(177, 23);
            NPCSearchTxt.TabIndex = 65;
            NPCSearchTxt.KeyUp += NPCSearchTxt_KeyUp;
            // 
            // NPCInfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(917, 453);
            Controls.Add(NPCSearchTxt);
            Controls.Add(ExportSelectedButton);
            Controls.Add(ImportButton);
            Controls.Add(ExportButton);
            Controls.Add(PasteMButton);
            Controls.Add(CopyMButton);
            Controls.Add(tabControl1);
            Controls.Add(RemoveButton);
            Controls.Add(AddButton);
            Controls.Add(NPCInfoListBox);
            Margin = new Padding(4, 3, 4, 3);
            Name = "NPCInfoForm";
            Text = "NPC信息列表";
            FormClosed += NPCInfoForm_FormClosed;
            Load += NPCInfoForm_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NPCPreview).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EndMin_num).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartMin_num).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox NPCIndexTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button PasteMButton;
        private System.Windows.Forms.Button CopyMButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button ExportSelectedButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button OpenNButton;
        private System.Windows.Forms.TextBox NFileNameTextBox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox NRateTextBox;
        private System.Windows.Forms.Button ClearHButton;
        private System.Windows.Forms.TextBox NNameTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox NImageTextBox;
        private System.Windows.Forms.TextBox NXTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox NYTextBox;
        private System.Windows.Forms.ListBox NPCInfoListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox MapComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MinLev_textbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox Day_combo;
        private System.Windows.Forms.ComboBox Class_combo;
        private System.Windows.Forms.NumericUpDown EndMin_num;
        private System.Windows.Forms.ComboBox EndHour_combo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown StartMin_num;
        private System.Windows.Forms.ComboBox StartHour_combo;
        private System.Windows.Forms.CheckBox TimeVisible_checkbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MaxLev_textbox;
        private System.Windows.Forms.TextBox Flag_textbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox ConquestHidden_combo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox BigMapIconTextBox;
        private System.Windows.Forms.CheckBox ShowBigMapCheckBox;
        private System.Windows.Forms.CheckBox TeleportToCheckBox;
        private CheckBox ConquestVisible_checkbox;
        private PictureBox NPCPreview;
        private TextBox NPCSearchTxt;
        private Button ReloadScriptButton;
    }
}