namespace Server
{
    partial class ConfigForm
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
            SaveButton = new Button();
            configTabs = new TabControl();
            tabPage1 = new TabPage();
            groupBox1 = new GroupBox();
            label11 = new Label();
            DBVersionLabel = new Label();
            ServerVersionLabel = new Label();
            label10 = new Label();
            VersionCheckBox = new CheckBox();
            VPathBrowseButton = new Button();
            VPathTextBox = new TextBox();
            label1 = new Label();
            tabPage2 = new TabPage();
            gbServerSettings = new GroupBox();
            SaveDelayTextBox = new TextBox();
            label6 = new Label();
            label12 = new Label();
            gbHTTPService = new GroupBox();
            label15 = new Label();
            HTTPTrustedIPAddressTextBox = new TextBox();
            label14 = new Label();
            HTTPIPAddressTextBox = new TextBox();
            label13 = new Label();
            StartHTTPCheckBox = new CheckBox();
            gbConnectionSettings = new GroupBox();
            RelogDelayTextBox = new TextBox();
            label7 = new Label();
            maxConnectionsPerIP = new TextBox();
            lblMaxConnectionsPerIP = new Label();
            MaxUserTextBox = new TextBox();
            label5 = new Label();
            TimeOutTextBox = new TextBox();
            label4 = new Label();
            gbServerConnection = new GroupBox();
            IPAddressTextBox = new TextBox();
            label2 = new Label();
            PortTextBox = new TextBox();
            label3 = new Label();
            tabPage3 = new TabPage();
            gbGameWorld = new GroupBox();
            SafeZoneHealingCheckBox = new CheckBox();
            SafeZoneBorderCheckBox = new CheckBox();
            ObserveCheckBox = new CheckBox();
            WarehousePasswordCheckBox = new CheckBox();
            gbCharacterScreen = new GroupBox();
            StartGameCheckBox = new CheckBox();
            NCharacterCheckBox = new CheckBox();
            DCharacterCheckBox = new CheckBox();
            AllowAssassinCheckBox = new CheckBox();
            AllowArcherCheckBox = new CheckBox();
            gbLoginScreen = new GroupBox();
            AccountCheckBox = new CheckBox();
            PasswordCheckBox = new CheckBox();
            LoginCheckBox = new CheckBox();
            tabPage6 = new TabPage();
            gbRestedExpRates = new GroupBox();
            label22 = new Label();
            label23 = new Label();
            label21 = new Label();
            label20 = new Label();
            tbRestedPeriod = new TextBox();
            tbRestedBuffLength = new TextBox();
            tbMaxRestedBonus = new TextBox();
            tbRestedExpBonus = new TextBox();
            lblMaxRestedBonus = new Label();
            lblRestedExpBonus = new Label();
            lblRestedBuffLength = new Label();
            lblPeriod = new Label();
            gbGlobals = new GroupBox();
            label19 = new Label();
            label18 = new Label();
            dropRateInput = new NumericUpDown();
            lblDropRate = new Label();
            expRateInput = new NumericUpDown();
            lblExpRate = new Label();
            tabPage5 = new TabPage();
            groupBox2 = new GroupBox();
            ReaddArcDrops = new Button();
            ReaddSinDrops = new Button();
            RemoveArcDrops = new Button();
            RemoveSinDrops = new Button();
            Resolution_textbox = new TextBox();
            label9 = new Label();
            label16 = new Label();
            lineMessageTimeTextBox = new TextBox();
            label17 = new Label();
            gameMasterEffect_CheckBox = new CheckBox();
            label8 = new Label();
            VPathDialog = new OpenFileDialog();
            configTabs.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            gbServerSettings.SuspendLayout();
            gbHTTPService.SuspendLayout();
            gbConnectionSettings.SuspendLayout();
            gbServerConnection.SuspendLayout();
            tabPage3.SuspendLayout();
            gbGameWorld.SuspendLayout();
            gbCharacterScreen.SuspendLayout();
            gbLoginScreen.SuspendLayout();
            tabPage6.SuspendLayout();
            gbRestedExpRates.SuspendLayout();
            gbGlobals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dropRateInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)expRateInput).BeginInit();
            tabPage5.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SaveButton.Location = new Point(411, 452);
            SaveButton.Margin = new Padding(5, 7, 5, 7);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(88, 30);
            SaveButton.TabIndex = 6;
            SaveButton.Text = "关闭";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // configTabs
            // 
            configTabs.Controls.Add(tabPage1);
            configTabs.Controls.Add(tabPage2);
            configTabs.Controls.Add(tabPage3);
            configTabs.Controls.Add(tabPage6);
            configTabs.Controls.Add(tabPage5);
            configTabs.Location = new Point(14, 16);
            configTabs.Margin = new Padding(5, 7, 5, 7);
            configTabs.Name = "configTabs";
            configTabs.SelectedIndex = 0;
            configTabs.Size = new Size(484, 426);
            configTabs.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(VersionCheckBox);
            tabPage1.Controls.Add(VPathBrowseButton);
            tabPage1.Controls.Add(VPathTextBox);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(5, 7, 5, 7);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(5, 7, 5, 7);
            tabPage1.Size = new Size(476, 396);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "版本信息";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(DBVersionLabel);
            groupBox1.Controls.Add(ServerVersionLabel);
            groupBox1.Controls.Add(label10);
            groupBox1.Location = new Point(7, 300);
            groupBox1.Margin = new Padding(5, 7, 5, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5, 7, 5, 7);
            groupBox1.Size = new Size(461, 84);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "服务器版本信息";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(29, 55);
            label11.Name = "label11";
            label11.Size = new Size(44, 17);
            label11.TabIndex = 23;
            label11.Text = "数据库";
            // 
            // DBVersionLabel
            // 
            DBVersionLabel.AutoSize = true;
            DBVersionLabel.Location = new Point(89, 55);
            DBVersionLabel.Name = "DBVersionLabel";
            DBVersionLabel.Size = new Size(49, 17);
            DBVersionLabel.TabIndex = 24;
            DBVersionLabel.Text = "DB版本";
            // 
            // ServerVersionLabel
            // 
            ServerVersionLabel.AutoSize = true;
            ServerVersionLabel.Location = new Point(89, 27);
            ServerVersionLabel.Name = "ServerVersionLabel";
            ServerVersionLabel.Size = new Size(68, 17);
            ServerVersionLabel.TabIndex = 7;
            ServerVersionLabel.Text = "服务器版本";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(29, 28);
            label10.Name = "label10";
            label10.Size = new Size(44, 17);
            label10.TabIndex = 22;
            label10.Text = "服务器";
            // 
            // VersionCheckBox
            // 
            VersionCheckBox.AutoSize = true;
            VersionCheckBox.Location = new Point(104, 58);
            VersionCheckBox.Margin = new Padding(5, 7, 5, 7);
            VersionCheckBox.Name = "VersionCheckBox";
            VersionCheckBox.Size = new Size(111, 21);
            VersionCheckBox.TabIndex = 3;
            VersionCheckBox.Text = "检查登录器版本";
            VersionCheckBox.UseVisualStyleBackColor = true;
            // 
            // VPathBrowseButton
            // 
            VPathBrowseButton.Location = new Point(433, 17);
            VPathBrowseButton.Margin = new Padding(5, 7, 5, 7);
            VPathBrowseButton.Name = "VPathBrowseButton";
            VPathBrowseButton.Size = new Size(33, 30);
            VPathBrowseButton.TabIndex = 2;
            VPathBrowseButton.Text = "...";
            VPathBrowseButton.UseVisualStyleBackColor = true;
            VPathBrowseButton.Click += VPathBrowseButton_Click;
            // 
            // VPathTextBox
            // 
            VPathTextBox.Location = new Point(104, 21);
            VPathTextBox.Margin = new Padding(5, 7, 5, 7);
            VPathTextBox.Name = "VPathTextBox";
            VPathTextBox.ReadOnly = true;
            VPathTextBox.Size = new Size(324, 23);
            VPathTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 24);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 0;
            label1.Text = "登录器路径";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(gbServerSettings);
            tabPage2.Controls.Add(gbHTTPService);
            tabPage2.Controls.Add(gbConnectionSettings);
            tabPage2.Controls.Add(gbServerConnection);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Margin = new Padding(3, 4, 3, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 4, 3, 4);
            tabPage2.Size = new Size(476, 396);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "网络";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbServerSettings
            // 
            gbServerSettings.Controls.Add(SaveDelayTextBox);
            gbServerSettings.Controls.Add(label6);
            gbServerSettings.Controls.Add(label12);
            gbServerSettings.Location = new Point(17, 101);
            gbServerSettings.Name = "gbServerSettings";
            gbServerSettings.Size = new Size(196, 60);
            gbServerSettings.TabIndex = 3;
            gbServerSettings.TabStop = false;
            gbServerSettings.Text = "数据库设置";
            // 
            // SaveDelayTextBox
            // 
            SaveDelayTextBox.Location = new Point(78, 25);
            SaveDelayTextBox.Margin = new Padding(3, 4, 3, 4);
            SaveDelayTextBox.MaxLength = 5;
            SaveDelayTextBox.Name = "SaveDelayTextBox";
            SaveDelayTextBox.Size = new Size(61, 23);
            SaveDelayTextBox.TabIndex = 25;
            SaveDelayTextBox.TextChanged += CheckUShort;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 28);
            label6.Name = "label6";
            label6.Size = new Size(59, 17);
            label6.TabIndex = 24;
            label6.Text = "保存延迟:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(145, 28);
            label12.Name = "label12";
            label12.Size = new Size(32, 17);
            label12.TabIndex = 26;
            label12.Text = "分钟";
            // 
            // gbHTTPService
            // 
            gbHTTPService.Controls.Add(label15);
            gbHTTPService.Controls.Add(HTTPTrustedIPAddressTextBox);
            gbHTTPService.Controls.Add(label14);
            gbHTTPService.Controls.Add(HTTPIPAddressTextBox);
            gbHTTPService.Controls.Add(label13);
            gbHTTPService.Controls.Add(StartHTTPCheckBox);
            gbHTTPService.Location = new Point(17, 167);
            gbHTTPService.Name = "gbHTTPService";
            gbHTTPService.Size = new Size(440, 161);
            gbHTTPService.TabIndex = 2;
            gbHTTPService.TabStop = false;
            gbHTTPService.Text = "HTTP服务";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(26, 329);
            label15.Name = "label15";
            label15.Size = new Size(173, 17);
            label15.TabIndex = 22;
            label15.Text = "(HTTP 服务只允许受信任的 IP)";
            // 
            // HTTPTrustedIPAddressTextBox
            // 
            HTTPTrustedIPAddressTextBox.Location = new Point(152, 282);
            HTTPTrustedIPAddressTextBox.Margin = new Padding(5, 7, 5, 7);
            HTTPTrustedIPAddressTextBox.MaxLength = 30;
            HTTPTrustedIPAddressTextBox.Name = "HTTPTrustedIPAddressTextBox";
            HTTPTrustedIPAddressTextBox.Size = new Size(198, 23);
            HTTPTrustedIPAddressTextBox.TabIndex = 21;
            HTTPTrustedIPAddressTextBox.TextChanged += HTTPTrustedIPAddressTextBox_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(40, 285);
            label14.Name = "label14";
            label14.Size = new Size(109, 17);
            label14.TabIndex = 20;
            label14.Text = "HTTP 可信 IP 地址";
            // 
            // HTTPIPAddressTextBox
            // 
            HTTPIPAddressTextBox.Location = new Point(152, 239);
            HTTPIPAddressTextBox.Margin = new Padding(5, 7, 5, 7);
            HTTPIPAddressTextBox.MaxLength = 30;
            HTTPIPAddressTextBox.Name = "HTTPIPAddressTextBox";
            HTTPIPAddressTextBox.Size = new Size(198, 23);
            HTTPIPAddressTextBox.TabIndex = 19;
            HTTPIPAddressTextBox.TextChanged += HTTPIPAddressTextBox_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(72, 242);
            label13.Name = "label13";
            label13.Size = new Size(77, 17);
            label13.TabIndex = 18;
            label13.Text = "HTTP IP地址";
            // 
            // StartHTTPCheckBox
            // 
            StartHTTPCheckBox.AutoSize = true;
            StartHTTPCheckBox.Location = new Point(18, 22);
            StartHTTPCheckBox.Margin = new Padding(3, 4, 3, 4);
            StartHTTPCheckBox.Name = "StartHTTPCheckBox";
            StartHTTPCheckBox.Size = new Size(105, 21);
            StartHTTPCheckBox.TabIndex = 23;
            StartHTTPCheckBox.Text = "开启HTTP服务";
            StartHTTPCheckBox.UseVisualStyleBackColor = true;
            StartHTTPCheckBox.CheckedChanged += StartHTTPCheckBox_CheckedChanged;
            // 
            // gbConnectionSettings
            // 
            gbConnectionSettings.Controls.Add(RelogDelayTextBox);
            gbConnectionSettings.Controls.Add(label7);
            gbConnectionSettings.Controls.Add(maxConnectionsPerIP);
            gbConnectionSettings.Controls.Add(lblMaxConnectionsPerIP);
            gbConnectionSettings.Controls.Add(MaxUserTextBox);
            gbConnectionSettings.Controls.Add(label5);
            gbConnectionSettings.Controls.Add(TimeOutTextBox);
            gbConnectionSettings.Controls.Add(label4);
            gbConnectionSettings.Location = new Point(232, 18);
            gbConnectionSettings.Name = "gbConnectionSettings";
            gbConnectionSettings.Size = new Size(225, 143);
            gbConnectionSettings.TabIndex = 1;
            gbConnectionSettings.TabStop = false;
            gbConnectionSettings.Text = "连接设置";
            // 
            // RelogDelayTextBox
            // 
            RelogDelayTextBox.Location = new Point(92, 16);
            RelogDelayTextBox.Margin = new Padding(3, 4, 3, 4);
            RelogDelayTextBox.MaxLength = 5;
            RelogDelayTextBox.Name = "RelogDelayTextBox";
            RelogDelayTextBox.Size = new Size(108, 23);
            RelogDelayTextBox.TabIndex = 27;
            RelogDelayTextBox.TextChanged += CheckUShort;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 19);
            label7.Name = "label7";
            label7.Size = new Size(83, 17);
            label7.TabIndex = 26;
            label7.Text = "重新记录延迟:";
            // 
            // maxConnectionsPerIP
            // 
            maxConnectionsPerIP.Location = new Point(92, 82);
            maxConnectionsPerIP.Margin = new Padding(3, 4, 3, 4);
            maxConnectionsPerIP.MaxLength = 5;
            maxConnectionsPerIP.Name = "maxConnectionsPerIP";
            maxConnectionsPerIP.Size = new Size(64, 23);
            maxConnectionsPerIP.TabIndex = 25;
            // 
            // lblMaxConnectionsPerIP
            // 
            lblMaxConnectionsPerIP.AutoSize = true;
            lblMaxConnectionsPerIP.Location = new Point(12, 85);
            lblMaxConnectionsPerIP.Name = "lblMaxConnectionsPerIP";
            lblMaxConnectionsPerIP.Size = new Size(75, 17);
            lblMaxConnectionsPerIP.TabIndex = 24;
            lblMaxConnectionsPerIP.Text = "最大链接/IP:";
            // 
            // MaxUserTextBox
            // 
            MaxUserTextBox.Location = new Point(93, 111);
            MaxUserTextBox.Margin = new Padding(5, 7, 5, 7);
            MaxUserTextBox.MaxLength = 5;
            MaxUserTextBox.Name = "MaxUserTextBox";
            MaxUserTextBox.Size = new Size(62, 23);
            MaxUserTextBox.TabIndex = 17;
            MaxUserTextBox.TextChanged += CheckUShort;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 114);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 16;
            label5.Text = "最大登录数";
            // 
            // TimeOutTextBox
            // 
            TimeOutTextBox.Location = new Point(92, 52);
            TimeOutTextBox.Margin = new Padding(5, 7, 5, 7);
            TimeOutTextBox.MaxLength = 5;
            TimeOutTextBox.Name = "TimeOutTextBox";
            TimeOutTextBox.Size = new Size(108, 23);
            TimeOutTextBox.TabIndex = 15;
            TimeOutTextBox.TextChanged += CheckUShort;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 55);
            label4.Name = "label4";
            label4.Size = new Size(56, 17);
            label4.TabIndex = 14;
            label4.Text = "连接超时";
            // 
            // gbServerConnection
            // 
            gbServerConnection.Controls.Add(IPAddressTextBox);
            gbServerConnection.Controls.Add(label2);
            gbServerConnection.Controls.Add(PortTextBox);
            gbServerConnection.Controls.Add(label3);
            gbServerConnection.Location = new Point(17, 18);
            gbServerConnection.Name = "gbServerConnection";
            gbServerConnection.Size = new Size(196, 75);
            gbServerConnection.TabIndex = 0;
            gbServerConnection.TabStop = false;
            gbServerConnection.Text = "服务器连接";
            // 
            // IPAddressTextBox
            // 
            IPAddressTextBox.Location = new Point(79, 18);
            IPAddressTextBox.Margin = new Padding(5, 7, 5, 7);
            IPAddressTextBox.MaxLength = 15;
            IPAddressTextBox.Name = "IPAddressTextBox";
            IPAddressTextBox.Size = new Size(108, 23);
            IPAddressTextBox.TabIndex = 11;
            IPAddressTextBox.TextChanged += IPAddressCheck;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 22);
            label2.Name = "label2";
            label2.Size = new Size(43, 17);
            label2.TabIndex = 10;
            label2.Text = "IP地址";
            // 
            // PortTextBox
            // 
            PortTextBox.Location = new Point(79, 48);
            PortTextBox.Margin = new Padding(3, 4, 3, 4);
            PortTextBox.MaxLength = 5;
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(48, 23);
            PortTextBox.TabIndex = 13;
            PortTextBox.TextChanged += CheckUShort;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 51);
            label3.Name = "label3";
            label3.Size = new Size(35, 17);
            label3.TabIndex = 12;
            label3.Text = "接口:";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(gbGameWorld);
            tabPage3.Controls.Add(gbCharacterScreen);
            tabPage3.Controls.Add(gbLoginScreen);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Margin = new Padding(3, 4, 3, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(5, 7, 5, 7);
            tabPage3.Size = new Size(476, 396);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "权限";
            tabPage3.UseVisualStyleBackColor = true;
            tabPage3.Click += tabPage3_Click;
            // 
            // gbGameWorld
            // 
            gbGameWorld.Controls.Add(SafeZoneHealingCheckBox);
            gbGameWorld.Controls.Add(SafeZoneBorderCheckBox);
            gbGameWorld.Controls.Add(ObserveCheckBox);
            gbGameWorld.Controls.Add(WarehousePasswordCheckBox);
            gbGameWorld.Location = new Point(190, 20);
            gbGameWorld.Name = "gbGameWorld";
            gbGameWorld.Size = new Size(272, 296);
            gbGameWorld.TabIndex = 2;
            gbGameWorld.TabStop = false;
            gbGameWorld.Text = "游戏世界";
            // 
            // SafeZoneHealingCheckBox
            // 
            SafeZoneHealingCheckBox.AutoSize = true;
            SafeZoneHealingCheckBox.Location = new Point(6, 50);
            SafeZoneHealingCheckBox.Margin = new Padding(3, 4, 3, 4);
            SafeZoneHealingCheckBox.Name = "SafeZoneHealingCheckBox";
            SafeZoneHealingCheckBox.Size = new Size(111, 21);
            SafeZoneHealingCheckBox.TabIndex = 1;
            SafeZoneHealingCheckBox.Text = "安全区自动恢复";
            SafeZoneHealingCheckBox.UseVisualStyleBackColor = true;
            SafeZoneHealingCheckBox.CheckedChanged += SafeZoneHealingCheckBox_CheckedChanged;
            // 
            // SafeZoneBorderCheckBox
            // 
            SafeZoneBorderCheckBox.AutoSize = true;
            SafeZoneBorderCheckBox.Location = new Point(6, 23);
            SafeZoneBorderCheckBox.Margin = new Padding(3, 4, 3, 4);
            SafeZoneBorderCheckBox.Name = "SafeZoneBorderCheckBox";
            SafeZoneBorderCheckBox.Size = new Size(87, 21);
            SafeZoneBorderCheckBox.TabIndex = 0;
            SafeZoneBorderCheckBox.Text = "安全区边框";
            SafeZoneBorderCheckBox.UseVisualStyleBackColor = true;
            SafeZoneBorderCheckBox.CheckedChanged += SafeZoneBorderCheckBox_CheckedChanged;
            // 
            // ObserveCheckBox
            // 
            ObserveCheckBox.AutoSize = true;
            ObserveCheckBox.Location = new Point(6, 77);
            ObserveCheckBox.Margin = new Padding(3, 4, 3, 4);
            ObserveCheckBox.Name = "ObserveCheckBox";
            ObserveCheckBox.Size = new Size(75, 21);
            ObserveCheckBox.TabIndex = 30;
            ObserveCheckBox.Text = "观察模式";
            ObserveCheckBox.UseVisualStyleBackColor = true;
            // 
            // WarehousePasswordCheckBox
            // 
            WarehousePasswordCheckBox.AutoSize = true;
            WarehousePasswordCheckBox.Location = new Point(6, 104);
            WarehousePasswordCheckBox.Margin = new Padding(3, 4, 3, 4);
            WarehousePasswordCheckBox.Name = "WarehousePasswordCheckBox";
            WarehousePasswordCheckBox.Size = new Size(187, 19);
            WarehousePasswordCheckBox.TabIndex = 31;
            WarehousePasswordCheckBox.Text = "Warehouse Password";
            WarehousePasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // gbCharacterScreen
            // 
            gbCharacterScreen.Controls.Add(StartGameCheckBox);
            gbCharacterScreen.Controls.Add(NCharacterCheckBox);
            gbCharacterScreen.Controls.Add(DCharacterCheckBox);
            gbCharacterScreen.Controls.Add(AllowAssassinCheckBox);
            gbCharacterScreen.Controls.Add(AllowArcherCheckBox);
            gbCharacterScreen.Location = new Point(17, 154);
            gbCharacterScreen.Name = "gbCharacterScreen";
            gbCharacterScreen.Size = new Size(157, 162);
            gbCharacterScreen.TabIndex = 1;
            gbCharacterScreen.TabStop = false;
            gbCharacterScreen.Text = "角色屏幕";
            // 
            // StartGameCheckBox
            // 
            StartGameCheckBox.AutoSize = true;
            StartGameCheckBox.Location = new Point(28, 177);
            StartGameCheckBox.Margin = new Padding(5, 7, 5, 7);
            StartGameCheckBox.Name = "StartGameCheckBox";
            StartGameCheckBox.Size = new Size(123, 21);
            StartGameCheckBox.TabIndex = 11;
            StartGameCheckBox.Text = "允许角色登录游戏";
            StartGameCheckBox.UseVisualStyleBackColor = true;
            // 
            // NCharacterCheckBox
            // 
            NCharacterCheckBox.AutoSize = true;
            NCharacterCheckBox.Location = new Point(6, 23);
            NCharacterCheckBox.Margin = new Padding(3, 4, 3, 4);
            NCharacterCheckBox.Name = "NCharacterCheckBox";
            NCharacterCheckBox.Size = new Size(75, 21);
            NCharacterCheckBox.TabIndex = 9;
            NCharacterCheckBox.Text = "角色创建";
            NCharacterCheckBox.UseVisualStyleBackColor = true;
            // 
            // DCharacterCheckBox
            // 
            DCharacterCheckBox.AutoSize = true;
            DCharacterCheckBox.Location = new Point(6, 131);
            DCharacterCheckBox.Margin = new Padding(5, 7, 5, 7);
            DCharacterCheckBox.Name = "DCharacterCheckBox";
            DCharacterCheckBox.Size = new Size(99, 21);
            DCharacterCheckBox.TabIndex = 10;
            DCharacterCheckBox.Text = "允许删除角色";
            DCharacterCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowAssassinCheckBox
            // 
            AllowAssassinCheckBox.AutoSize = true;
            AllowAssassinCheckBox.Location = new Point(6, 77);
            AllowAssassinCheckBox.Margin = new Padding(3, 4, 3, 4);
            AllowAssassinCheckBox.Name = "AllowAssassinCheckBox";
            AllowAssassinCheckBox.Size = new Size(75, 21);
            AllowAssassinCheckBox.TabIndex = 12;
            AllowAssassinCheckBox.Text = "刺客职业";
            AllowAssassinCheckBox.UseVisualStyleBackColor = true;
            // 
            // AllowArcherCheckBox
            // 
            AllowArcherCheckBox.AutoSize = true;
            AllowArcherCheckBox.Location = new Point(6, 104);
            AllowArcherCheckBox.Margin = new Padding(3, 4, 3, 4);
            AllowArcherCheckBox.Name = "AllowArcherCheckBox";
            AllowArcherCheckBox.Size = new Size(75, 21);
            AllowArcherCheckBox.TabIndex = 13;
            AllowArcherCheckBox.Text = "弓箭职业";
            AllowArcherCheckBox.UseVisualStyleBackColor = true;
            // 
            // gbLoginScreen
            // 
            gbLoginScreen.Controls.Add(AccountCheckBox);
            gbLoginScreen.Controls.Add(PasswordCheckBox);
            gbLoginScreen.Controls.Add(LoginCheckBox);
            gbLoginScreen.Location = new Point(17, 18);
            gbLoginScreen.Name = "gbLoginScreen";
            gbLoginScreen.Size = new Size(157, 130);
            gbLoginScreen.TabIndex = 0;
            gbLoginScreen.TabStop = false;
            gbLoginScreen.Text = "登录界面";
            // 
            // AccountCheckBox
            // 
            AccountCheckBox.AutoSize = true;
            AccountCheckBox.Location = new Point(6, 23);
            AccountCheckBox.Margin = new Padding(3, 4, 3, 4);
            AccountCheckBox.Name = "AccountCheckBox";
            AccountCheckBox.Size = new Size(75, 21);
            AccountCheckBox.TabIndex = 6;
            AccountCheckBox.Text = "帐户注册";
            AccountCheckBox.UseVisualStyleBackColor = true;
            // 
            // PasswordCheckBox
            // 
            PasswordCheckBox.AutoSize = true;
            PasswordCheckBox.Location = new Point(28, 53);
            PasswordCheckBox.Margin = new Padding(5, 7, 5, 7);
            PasswordCheckBox.Name = "PasswordCheckBox";
            PasswordCheckBox.Size = new Size(123, 21);
            PasswordCheckBox.TabIndex = 7;
            PasswordCheckBox.Text = "允许账户更改密码";
            PasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // LoginCheckBox
            // 
            LoginCheckBox.AutoSize = true;
            LoginCheckBox.Location = new Point(6, 84);
            LoginCheckBox.Margin = new Padding(3, 4, 3, 4);
            LoginCheckBox.Name = "LoginCheckBox";
            LoginCheckBox.Size = new Size(75, 21);
            LoginCheckBox.TabIndex = 8;
            LoginCheckBox.Text = "帐户登录";
            LoginCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(gbRestedExpRates);
            tabPage6.Controls.Add(gbGlobals);
            tabPage6.Location = new Point(4, 26);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(476, 396);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "费率";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // gbRestedExpRates
            // 
            gbRestedExpRates.Controls.Add(label22);
            gbRestedExpRates.Controls.Add(label23);
            gbRestedExpRates.Controls.Add(label21);
            gbRestedExpRates.Controls.Add(label20);
            gbRestedExpRates.Controls.Add(tbRestedPeriod);
            gbRestedExpRates.Controls.Add(tbRestedBuffLength);
            gbRestedExpRates.Controls.Add(tbMaxRestedBonus);
            gbRestedExpRates.Controls.Add(tbRestedExpBonus);
            gbRestedExpRates.Controls.Add(lblMaxRestedBonus);
            gbRestedExpRates.Controls.Add(lblRestedExpBonus);
            gbRestedExpRates.Controls.Add(lblRestedBuffLength);
            gbRestedExpRates.Controls.Add(lblPeriod);
            gbRestedExpRates.Location = new Point(17, 133);
            gbRestedExpRates.Name = "gbRestedExpRates";
            gbRestedExpRates.Size = new Size(228, 150);
            gbRestedExpRates.TabIndex = 8;
            gbRestedExpRates.TabStop = false;
            gbRestedExpRates.Text = "恢复经验";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(158, 88);
            label22.Name = "label22";
            label22.Size = new Size(19, 17);
            label22.TabIndex = 12;
            label22.Text = "%";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(158, 121);
            label23.Name = "label23";
            label23.Size = new Size(14, 17);
            label23.TabIndex = 12;
            label23.Text = "x";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(158, 55);
            label21.Name = "label21";
            label21.Size = new Size(32, 17);
            label21.TabIndex = 12;
            label21.Text = "分钟";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(158, 25);
            label20.Name = "label20";
            label20.Size = new Size(32, 17);
            label20.TabIndex = 12;
            label20.Text = "分钟";
            // 
            // tbRestedPeriod
            // 
            tbRestedPeriod.Location = new Point(96, 21);
            tbRestedPeriod.Name = "tbRestedPeriod";
            tbRestedPeriod.Size = new Size(56, 23);
            tbRestedPeriod.TabIndex = 11;
            tbRestedPeriod.KeyPress += tbRestedPeriod_KeyPress;
            // 
            // tbRestedBuffLength
            // 
            tbRestedBuffLength.Location = new Point(96, 51);
            tbRestedBuffLength.Name = "tbRestedBuffLength";
            tbRestedBuffLength.Size = new Size(56, 23);
            tbRestedBuffLength.TabIndex = 11;
            tbRestedBuffLength.KeyPress += tbRestedBuffLength_KeyPress;
            // 
            // tbMaxRestedBonus
            // 
            tbMaxRestedBonus.Location = new Point(96, 118);
            tbMaxRestedBonus.Name = "tbMaxRestedBonus";
            tbMaxRestedBonus.Size = new Size(56, 23);
            tbMaxRestedBonus.TabIndex = 11;
            tbMaxRestedBonus.KeyPress += tbMaxRestedBonus_KeyPress;
            // 
            // tbRestedExpBonus
            // 
            tbRestedExpBonus.Location = new Point(96, 85);
            tbRestedExpBonus.Name = "tbRestedExpBonus";
            tbRestedExpBonus.Size = new Size(56, 23);
            tbRestedExpBonus.TabIndex = 11;
            tbRestedExpBonus.KeyPress += tbRestedExpBonus_KeyPress;
            // 
            // lblMaxRestedBonus
            // 
            lblMaxRestedBonus.AutoSize = true;
            lblMaxRestedBonus.Location = new Point(31, 121);
            lblMaxRestedBonus.Name = "lblMaxRestedBonus";
            lblMaxRestedBonus.Size = new Size(59, 17);
            lblMaxRestedBonus.TabIndex = 0;
            lblMaxRestedBonus.Text = "最大收益:";
            // 
            // lblRestedExpBonus
            // 
            lblRestedExpBonus.AutoSize = true;
            lblRestedExpBonus.Location = new Point(31, 88);
            lblRestedExpBonus.Name = "lblRestedExpBonus";
            lblRestedExpBonus.Size = new Size(59, 17);
            lblRestedExpBonus.TabIndex = 0;
            lblRestedExpBonus.Text = "经验收益:";
            // 
            // lblRestedBuffLength
            // 
            lblRestedBuffLength.AutoSize = true;
            lblRestedBuffLength.Location = new Point(31, 55);
            lblRestedBuffLength.Name = "lblRestedBuffLength";
            lblRestedBuffLength.Size = new Size(59, 17);
            lblRestedBuffLength.TabIndex = 0;
            lblRestedBuffLength.Text = "增益时常:";
            // 
            // lblPeriod
            // 
            lblPeriod.AutoSize = true;
            lblPeriod.Location = new Point(55, 25);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(35, 17);
            lblPeriod.TabIndex = 0;
            lblPeriod.Text = "周期:";
            // 
            // gbGlobals
            // 
            gbGlobals.Controls.Add(label19);
            gbGlobals.Controls.Add(label18);
            gbGlobals.Controls.Add(dropRateInput);
            gbGlobals.Controls.Add(lblDropRate);
            gbGlobals.Controls.Add(expRateInput);
            gbGlobals.Controls.Add(lblExpRate);
            gbGlobals.Location = new Point(17, 18);
            gbGlobals.Name = "gbGlobals";
            gbGlobals.Size = new Size(228, 100);
            gbGlobals.TabIndex = 7;
            gbGlobals.TabStop = false;
            gbGlobals.Text = "全局变量";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(139, 63);
            label19.Name = "label19";
            label19.Size = new Size(14, 17);
            label19.TabIndex = 11;
            label19.Text = "x";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(139, 24);
            label18.Name = "label18";
            label18.Size = new Size(14, 17);
            label18.TabIndex = 12;
            label18.Text = "x";
            // 
            // dropRateInput
            // 
            dropRateInput.DecimalPlaces = 2;
            dropRateInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            dropRateInput.Location = new Point(82, 61);
            dropRateInput.Name = "dropRateInput";
            dropRateInput.Size = new Size(51, 23);
            dropRateInput.TabIndex = 9;
            // 
            // lblDropRate
            // 
            lblDropRate.AutoSize = true;
            lblDropRate.Location = new Point(20, 64);
            lblDropRate.Name = "lblDropRate";
            lblDropRate.Size = new Size(59, 17);
            lblDropRate.TabIndex = 7;
            lblDropRate.Text = "掉落几率:";
            // 
            // expRateInput
            // 
            expRateInput.DecimalPlaces = 2;
            expRateInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            expRateInput.Location = new Point(82, 22);
            expRateInput.Name = "expRateInput";
            expRateInput.Size = new Size(51, 23);
            expRateInput.TabIndex = 10;
            // 
            // lblExpRate
            // 
            lblExpRate.AutoSize = true;
            lblExpRate.Location = new Point(20, 25);
            lblExpRate.Name = "lblExpRate";
            lblExpRate.Size = new Size(59, 17);
            lblExpRate.TabIndex = 8;
            lblExpRate.Text = "经验几率:";
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(groupBox2);
            tabPage5.Controls.Add(Resolution_textbox);
            tabPage5.Controls.Add(label9);
            tabPage5.Controls.Add(label16);
            tabPage5.Controls.Add(lineMessageTimeTextBox);
            tabPage5.Controls.Add(label17);
            tabPage5.Controls.Add(gameMasterEffect_CheckBox);
            tabPage5.Location = new Point(4, 26);
            tabPage5.Margin = new Padding(5, 7, 5, 7);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(5, 7, 5, 7);
            tabPage5.Size = new Size(476, 396);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "其他选项";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ReaddArcDrops);
            groupBox2.Controls.Add(ReaddSinDrops);
            groupBox2.Controls.Add(RemoveArcDrops);
            groupBox2.Controls.Add(RemoveSinDrops);
            groupBox2.Location = new Point(6, 199);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(160, 142);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "掉落管理";
            // 
            // ReaddArcDrops
            // 
            ReaddArcDrops.Location = new Point(8, 109);
            ReaddArcDrops.Name = "ReaddArcDrops";
            ReaddArcDrops.Size = new Size(144, 23);
            ReaddArcDrops.TabIndex = 3;
            ReaddArcDrops.Text = "增加弓箭掉落";
            ReaddArcDrops.UseVisualStyleBackColor = true;
            ReaddArcDrops.Click += ReaddArcDrops_Click;
            // 
            // ReaddSinDrops
            // 
            ReaddSinDrops.Location = new Point(8, 51);
            ReaddSinDrops.Name = "ReaddSinDrops";
            ReaddSinDrops.Size = new Size(144, 23);
            ReaddSinDrops.TabIndex = 2;
            ReaddSinDrops.Text = "增加刺客掉落";
            ReaddSinDrops.UseVisualStyleBackColor = true;
            ReaddSinDrops.Click += ReaddSinDrops_Click;
            // 
            // RemoveArcDrops
            // 
            RemoveArcDrops.Location = new Point(8, 80);
            RemoveArcDrops.Name = "RemoveArcDrops";
            RemoveArcDrops.Size = new Size(144, 23);
            RemoveArcDrops.TabIndex = 1;
            RemoveArcDrops.Text = "移除弓箭掉落";
            RemoveArcDrops.UseVisualStyleBackColor = true;
            RemoveArcDrops.Click += RemoveArcDrops_Click;
            // 
            // RemoveSinDrops
            // 
            RemoveSinDrops.Location = new Point(8, 22);
            RemoveSinDrops.Name = "RemoveSinDrops";
            RemoveSinDrops.Size = new Size(144, 23);
            RemoveSinDrops.TabIndex = 0;
            RemoveSinDrops.Text = "移除刺客掉落";
            RemoveSinDrops.UseVisualStyleBackColor = true;
            RemoveSinDrops.Click += RemoveSinDrops_Click;
            // 
            // Resolution_textbox
            // 
            Resolution_textbox.Location = new Point(166, 60);
            Resolution_textbox.Margin = new Padding(3, 4, 3, 4);
            Resolution_textbox.Name = "Resolution_textbox";
            Resolution_textbox.Size = new Size(93, 23);
            Resolution_textbox.TabIndex = 32;
            Resolution_textbox.TextChanged += Resolution_textbox_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(94, 64);
            label9.Name = "label9";
            label9.Size = new Size(68, 17);
            label9.TabIndex = 31;
            label9.Text = "最大分辨率";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(227, 122);
            label16.Name = "label16";
            label16.Size = new Size(32, 17);
            label16.TabIndex = 29;
            label16.Text = "分钟";
            // 
            // lineMessageTimeTextBox
            // 
            lineMessageTimeTextBox.Location = new Point(183, 116);
            lineMessageTimeTextBox.Margin = new Padding(5, 7, 5, 7);
            lineMessageTimeTextBox.MaxLength = 5;
            lineMessageTimeTextBox.Name = "lineMessageTimeTextBox";
            lineMessageTimeTextBox.Size = new Size(41, 23);
            lineMessageTimeTextBox.TabIndex = 28;
            lineMessageTimeTextBox.Text = "10";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(76, 120);
            label17.Name = "label17";
            label17.Size = new Size(104, 17);
            label17.TabIndex = 27;
            label17.Text = "在线信息显示频率";
            // 
            // gameMasterEffect_CheckBox
            // 
            gameMasterEffect_CheckBox.AutoSize = true;
            gameMasterEffect_CheckBox.Location = new Point(22, 25);
            gameMasterEffect_CheckBox.Margin = new Padding(3, 4, 3, 4);
            gameMasterEffect_CheckBox.Name = "gameMasterEffect_CheckBox";
            gameMasterEffect_CheckBox.Size = new Size(111, 21);
            gameMasterEffect_CheckBox.TabIndex = 2;
            gameMasterEffect_CheckBox.Text = "游戏管理员特效";
            gameMasterEffect_CheckBox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(100, 23);
            label8.TabIndex = 0;
            // 
            // VPathDialog
            // 
            VPathDialog.FileName = "Mir2.Exe";
            VPathDialog.Filter = "Executable Files (*.exe)|*.exe";
            VPathDialog.Multiselect = true;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 487);
            Controls.Add(SaveButton);
            Controls.Add(configTabs);
            Margin = new Padding(5, 7, 5, 7);
            Name = "ConfigForm";
            Text = "服务器设置";
            FormClosed += ConfigForm_FormClosed;
            configTabs.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            gbServerSettings.ResumeLayout(false);
            gbServerSettings.PerformLayout();
            gbHTTPService.ResumeLayout(false);
            gbHTTPService.PerformLayout();
            gbConnectionSettings.ResumeLayout(false);
            gbConnectionSettings.PerformLayout();
            gbServerConnection.ResumeLayout(false);
            gbServerConnection.PerformLayout();
            tabPage3.ResumeLayout(false);
            gbGameWorld.ResumeLayout(false);
            gbGameWorld.PerformLayout();
            gbCharacterScreen.ResumeLayout(false);
            gbCharacterScreen.PerformLayout();
            gbLoginScreen.ResumeLayout(false);
            gbLoginScreen.PerformLayout();
            tabPage6.ResumeLayout(false);
            gbRestedExpRates.ResumeLayout(false);
            gbRestedExpRates.PerformLayout();
            gbGlobals.ResumeLayout(false);
            gbGlobals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dropRateInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)expRateInput).EndInit();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TabControl configTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox RelogDelayTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox VersionCheckBox;
        private System.Windows.Forms.Button VPathBrowseButton;
        private System.Windows.Forms.TextBox VPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox MaxUserTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TimeOutTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IPAddressTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog VPathDialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox StartGameCheckBox;
        private System.Windows.Forms.CheckBox DCharacterCheckBox;
        private System.Windows.Forms.CheckBox NCharacterCheckBox;
        private System.Windows.Forms.CheckBox LoginCheckBox;
        private System.Windows.Forms.CheckBox PasswordCheckBox;
        private System.Windows.Forms.CheckBox AccountCheckBox;
        private System.Windows.Forms.TextBox SaveDelayTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox SafeZoneBorderCheckBox;
        private System.Windows.Forms.CheckBox SafeZoneHealingCheckBox;
        private System.Windows.Forms.CheckBox AllowArcherCheckBox;
        private System.Windows.Forms.CheckBox AllowAssassinCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Resolution_textbox;
        private System.Windows.Forms.Label ServerVersionLabel;
        private System.Windows.Forms.Label DBVersionLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox gameMasterEffect_CheckBox;
        private System.Windows.Forms.TextBox HTTPIPAddressTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox HTTPTrustedIPAddressTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox StartHTTPCheckBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox lineMessageTimeTextBox;
        private System.Windows.Forms.Label label17;
        private TextBox maxConnectionsPerIP;
        private Label lblMaxConnectionsPerIP;
        private TabPage tabPage6;
        private GroupBox gbRestedExpRates;
        private GroupBox gbGlobals;
        private Label label19;
        private Label label18;
        private NumericUpDown dropRateInput;
        private Label lblDropRate;
        private NumericUpDown expRateInput;
        private Label lblExpRate;
        private Label lblMaxRestedBonus;
        private Label lblRestedExpBonus;
        private Label lblRestedBuffLength;
        private Label lblPeriod;
        private TextBox tbRestedBuffLength;
        private TextBox tbMaxRestedBonus;
        private TextBox tbRestedExpBonus;
        private TextBox tbRestedPeriod;
        private Label label22;
        private Label label23;
        private Label label21;
        private Label label20;
        private GroupBox groupBox2;
        private Button ReaddArcDrops;
        private Button ReaddSinDrops;
        private Button RemoveArcDrops;
        private Button RemoveSinDrops;
        private CheckBox ObserveCheckBox;
        private CheckBox WarehousePasswordCheckBox;
        private GroupBox gbHTTPService;
        private GroupBox gbConnectionSettings;
        private GroupBox gbServerConnection;
        private GroupBox gbServerSettings;
        private GroupBox gbLoginScreen;
        private GroupBox gbCharacterScreen;
        private GroupBox gbGameWorld;
    }
}