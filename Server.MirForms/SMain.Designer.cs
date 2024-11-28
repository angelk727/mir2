using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Server
{
    partial class SMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            components = new Container();
            MainTabs = new TabControl();
            tabPage1 = new TabPage();
            LogTextBox = new TextBox();
            tabPage2 = new TabPage();
            DebugLogTextBox = new TextBox();
            tabPage3 = new TabPage();
            groupBox1 = new GroupBox();
            GlobalMessageButton = new Button();
            GlobalMessageTextBox = new TextBox();
            ChatLogTextBox = new TextBox();
            tabPage4 = new TabPage();
            PlayersOnlineListView = new CustomFormControl.ListViewNF();
            indexHeader = new ColumnHeader();
            nameHeader = new ColumnHeader();
            levelHeader = new ColumnHeader();
            classHeader = new ColumnHeader();
            genderHeader = new ColumnHeader();
            tabPage5 = new TabPage();
            GuildListView = new CustomFormControl.ListViewNF();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            tabPage6 = new TabPage();
            LoadMonstersButton = new Button();
            MonsterListView = new ListView();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            StatusBar = new StatusStrip();
            PlayersLabel = new ToolStripStatusLabel();
            MonsterLabel = new ToolStripStatusLabel();
            ConnectionsLabel = new ToolStripStatusLabel();
            BlockedIPsLabel = new ToolStripStatusLabel();
            CycleDelayLabel = new ToolStripStatusLabel();
            MainMenu = new MenuStrip();
            controlToolStripMenuItem = new ToolStripMenuItem();
            startServerToolStripMenuItem = new ToolStripMenuItem();
            stopServerToolStripMenuItem = new ToolStripMenuItem();
            rebootServerToolStripMenuItem = new ToolStripMenuItem();
            clearBlockedIPsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            toolStripSeparator1 = new ToolStripSeparator();
            closeServerToolStripMenuItem = new ToolStripMenuItem();
            reloadToolStripMenuItem = new ToolStripMenuItem();
            nPCsToolStripMenuItem = new ToolStripMenuItem();
            dropsToolStripMenuItem = new ToolStripMenuItem();
            lineMessageToolStripMenuItem = new ToolStripMenuItem();
            物品信息ToolStripMenuItem = new ToolStripMenuItem();
            怪物信息ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            任务信息ToolStripMenuItem = new ToolStripMenuItem();
            配方信息ToolStripMenuItem = new ToolStripMenuItem();
            商城物品ToolStripMenuItem = new ToolStripMenuItem();
            accountToolStripMenuItem = new ToolStripMenuItem();
            accountsToolStripMenuItem1 = new ToolStripMenuItem();
            marketToolStripMenuItem = new ToolStripMenuItem();
            namelistsToolStripMenuItem = new ToolStripMenuItem();
            databaseFormsToolStripMenuItem = new ToolStripMenuItem();
            mapInfoToolStripMenuItem = new ToolStripMenuItem();
            itemInfoToolStripMenuItem = new ToolStripMenuItem();
            monsterInfoToolStripMenuItem = new ToolStripMenuItem();
            itemNEWToolStripMenuItem = new ToolStripMenuItem();
            monsterExperimentalToolStripMenuItem = new ToolStripMenuItem();
            nPCInfoToolStripMenuItem = new ToolStripMenuItem();
            questInfoToolStripMenuItem = new ToolStripMenuItem();
            magicInfoToolStripMenuItem = new ToolStripMenuItem();
            gameshopToolStripMenuItem = new ToolStripMenuItem();
            recipeToolStripMenuItem = new ToolStripMenuItem();
            configToolStripMenuItem1 = new ToolStripMenuItem();
            serverToolStripMenuItem = new ToolStripMenuItem();
            balanceToolStripMenuItem = new ToolStripMenuItem();
            systemToolStripMenuItem = new ToolStripMenuItem();
            dragonSystemToolStripMenuItem = new ToolStripMenuItem();
            miningToolStripMenuItem = new ToolStripMenuItem();
            guildsToolStripMenuItem = new ToolStripMenuItem();
            fishingToolStripMenuItem = new ToolStripMenuItem();
            mailToolStripMenuItem = new ToolStripMenuItem();
            goodsToolStripMenuItem = new ToolStripMenuItem();
            refiningToolStripMenuItem = new ToolStripMenuItem();
            relationshipToolStripMenuItem = new ToolStripMenuItem();
            mentorToolStripMenuItem = new ToolStripMenuItem();
            gemToolStripMenuItem = new ToolStripMenuItem();
            conquestToolStripMenuItem = new ToolStripMenuItem();
            respawnsToolStripMenuItem = new ToolStripMenuItem();
            heroesToolStripMenuItem = new ToolStripMenuItem();
            monsterTunerToolStripMenuItem = new ToolStripMenuItem();
            dropBuilderToolStripMenuItem = new ToolStripMenuItem();
            CharacterToolStripMenuItem = new ToolStripMenuItem();
            UpTimeLabel = new ToolStripTextBox();
            InterfaceTimer = new Timer(components);
            MainTabs.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage6.SuspendLayout();
            StatusBar.SuspendLayout();
            MainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // MainTabs
            // 
            MainTabs.Controls.Add(tabPage1);
            MainTabs.Controls.Add(tabPage2);
            MainTabs.Controls.Add(tabPage3);
            MainTabs.Controls.Add(tabPage4);
            MainTabs.Controls.Add(tabPage5);
            MainTabs.Controls.Add(tabPage6);
            MainTabs.Dock = DockStyle.Fill;
            MainTabs.Location = new Point(0, 25);
            MainTabs.Margin = new Padding(4, 3, 4, 3);
            MainTabs.Name = "MainTabs";
            MainTabs.SelectedIndex = 0;
            MainTabs.Size = new Size(566, 465);
            MainTabs.TabIndex = 5;
            MainTabs.SelectedIndexChanged += MainTabs_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(LogTextBox);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Margin = new Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 3, 4, 3);
            tabPage1.Size = new Size(558, 435);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "日志";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // LogTextBox
            // 
            LogTextBox.Dock = DockStyle.Fill;
            LogTextBox.Location = new Point(4, 3);
            LogTextBox.Margin = new Padding(4, 3, 4, 3);
            LogTextBox.Multiline = true;
            LogTextBox.Name = "LogTextBox";
            LogTextBox.ReadOnly = true;
            LogTextBox.ScrollBars = ScrollBars.Vertical;
            LogTextBox.Size = new Size(550, 429);
            LogTextBox.TabIndex = 2;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(DebugLogTextBox);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Margin = new Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 3, 4, 3);
            tabPage2.Size = new Size(558, 435);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "调试日志";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // DebugLogTextBox
            // 
            DebugLogTextBox.Dock = DockStyle.Fill;
            DebugLogTextBox.Location = new Point(4, 3);
            DebugLogTextBox.Margin = new Padding(4, 3, 4, 3);
            DebugLogTextBox.Multiline = true;
            DebugLogTextBox.Name = "DebugLogTextBox";
            DebugLogTextBox.ReadOnly = true;
            DebugLogTextBox.ScrollBars = ScrollBars.Vertical;
            DebugLogTextBox.Size = new Size(550, 429);
            DebugLogTextBox.TabIndex = 3;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(groupBox1);
            tabPage3.Controls.Add(ChatLogTextBox);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Margin = new Padding(4, 3, 4, 3);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(4, 3, 4, 3);
            tabPage3.Size = new Size(558, 435);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "聊天日志";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(GlobalMessageButton);
            groupBox1.Controls.Add(GlobalMessageTextBox);
            groupBox1.Dock = DockStyle.Bottom;
            groupBox1.Location = new Point(4, 372);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(550, 60);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "发送信息";
            // 
            // GlobalMessageButton
            // 
            GlobalMessageButton.Location = new Point(457, 18);
            GlobalMessageButton.Margin = new Padding(4, 3, 4, 3);
            GlobalMessageButton.Name = "GlobalMessageButton";
            GlobalMessageButton.Size = new Size(85, 32);
            GlobalMessageButton.TabIndex = 0;
            GlobalMessageButton.Text = "发送";
            GlobalMessageButton.UseVisualStyleBackColor = true;
            GlobalMessageButton.Click += GlobalMessageButton_Click;
            // 
            // GlobalMessageTextBox
            // 
            GlobalMessageTextBox.Location = new Point(7, 23);
            GlobalMessageTextBox.Margin = new Padding(4, 3, 4, 3);
            GlobalMessageTextBox.Name = "GlobalMessageTextBox";
            GlobalMessageTextBox.Size = new Size(443, 23);
            GlobalMessageTextBox.TabIndex = 0;
            // 
            // ChatLogTextBox
            // 
            ChatLogTextBox.Location = new Point(4, 3);
            ChatLogTextBox.Margin = new Padding(4, 3, 4, 3);
            ChatLogTextBox.Multiline = true;
            ChatLogTextBox.Name = "ChatLogTextBox";
            ChatLogTextBox.ReadOnly = true;
            ChatLogTextBox.ScrollBars = ScrollBars.Vertical;
            ChatLogTextBox.Size = new Size(549, 351);
            ChatLogTextBox.TabIndex = 4;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = SystemColors.Control;
            tabPage4.Controls.Add(PlayersOnlineListView);
            tabPage4.Location = new Point(4, 26);
            tabPage4.Margin = new Padding(4, 3, 4, 3);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(4, 3, 4, 3);
            tabPage4.Size = new Size(558, 435);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "在线玩家";
            // 
            // PlayersOnlineListView
            // 
            PlayersOnlineListView.Activation = ItemActivation.OneClick;
            PlayersOnlineListView.BackColor = SystemColors.Window;
            PlayersOnlineListView.Columns.AddRange(new ColumnHeader[] { indexHeader, nameHeader, levelHeader, classHeader, genderHeader });
            PlayersOnlineListView.Dock = DockStyle.Fill;
            PlayersOnlineListView.FullRowSelect = true;
            PlayersOnlineListView.GridLines = true;
            PlayersOnlineListView.Location = new Point(4, 3);
            PlayersOnlineListView.Margin = new Padding(4, 3, 4, 3);
            PlayersOnlineListView.Name = "PlayersOnlineListView";
            PlayersOnlineListView.Size = new Size(550, 429);
            PlayersOnlineListView.Sorting = SortOrder.Ascending;
            PlayersOnlineListView.TabIndex = 0;
            PlayersOnlineListView.UseCompatibleStateImageBehavior = false;
            PlayersOnlineListView.View = View.Details;
            PlayersOnlineListView.ColumnWidthChanging += PlayersOnlineListView_ColumnWidthChanging;
            PlayersOnlineListView.DoubleClick += PlayersOnlineListView_DoubleClick;
            // 
            // indexHeader
            // 
            indexHeader.Text = "序号";
            indexHeader.Width = 71;
            // 
            // nameHeader
            // 
            nameHeader.Text = "名称";
            nameHeader.Width = 93;
            // 
            // levelHeader
            // 
            levelHeader.Text = "等级";
            levelHeader.Width = 90;
            // 
            // classHeader
            // 
            classHeader.Text = "职业";
            classHeader.Width = 100;
            // 
            // genderHeader
            // 
            genderHeader.Text = "性别";
            genderHeader.Width = 98;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(GuildListView);
            tabPage5.Location = new Point(4, 26);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(558, 435);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "公会";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // GuildListView
            // 
            GuildListView.Activation = ItemActivation.OneClick;
            GuildListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
            GuildListView.Dock = DockStyle.Fill;
            GuildListView.FullRowSelect = true;
            GuildListView.GridLines = true;
            GuildListView.Location = new Point(3, 3);
            GuildListView.Name = "GuildListView";
            GuildListView.Size = new Size(552, 429);
            GuildListView.TabIndex = 1;
            GuildListView.UseCompatibleStateImageBehavior = false;
            GuildListView.View = View.Details;
            GuildListView.SelectedIndexChanged += GuildListView_SelectedIndexChanged;
            GuildListView.DoubleClick += GuildListView_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "序号";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "名称";
            columnHeader2.Width = 115;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "会长";
            columnHeader3.Width = 130;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "会员人数";
            columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "公会等级";
            columnHeader5.Width = 75;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "金币";
            columnHeader6.Width = 75;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(LoadMonstersButton);
            tabPage6.Controls.Add(MonsterListView);
            tabPage6.Location = new Point(4, 26);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(558, 435);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "怪物";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // LoadMonstersButton
            // 
            LoadMonstersButton.Location = new Point(480, 7);
            LoadMonstersButton.Name = "LoadMonstersButton";
            LoadMonstersButton.Size = new Size(75, 23);
            LoadMonstersButton.TabIndex = 1;
            LoadMonstersButton.Text = "刷新";
            LoadMonstersButton.UseVisualStyleBackColor = true;
            LoadMonstersButton.Click += LoadMonstersButton_Click;
            // 
            // MonsterListView
            // 
            MonsterListView.Columns.AddRange(new ColumnHeader[] { columnHeader7, columnHeader8, columnHeader9, columnHeader10, columnHeader11, columnHeader12 });
            MonsterListView.Dock = DockStyle.Bottom;
            MonsterListView.GridLines = true;
            MonsterListView.Location = new Point(0, 92);
            MonsterListView.Name = "MonsterListView";
            MonsterListView.Size = new Size(558, 343);
            MonsterListView.TabIndex = 0;
            MonsterListView.UseCompatibleStateImageBehavior = false;
            MonsterListView.View = View.Details;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "编号";
            columnHeader7.Width = 50;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "地图名";
            columnHeader8.Width = 120;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "地图文件名";
            columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "当前怪物";
            columnHeader10.Width = 110;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "最大怪物数";
            columnHeader11.Width = 110;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "错误";
            columnHeader12.Width = 55;
            // 
            // StatusBar
            // 
            StatusBar.Items.AddRange(new ToolStripItem[] { PlayersLabel, MonsterLabel, ConnectionsLabel, BlockedIPsLabel, CycleDelayLabel });
            StatusBar.Location = new Point(0, 490);
            StatusBar.Name = "StatusBar";
            StatusBar.Padding = new Padding(1, 0, 16, 0);
            StatusBar.Size = new Size(566, 26);
            StatusBar.SizingGrip = false;
            StatusBar.TabIndex = 4;
            StatusBar.Text = "statusStrip1";
            // 
            // PlayersLabel
            // 
            PlayersLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            PlayersLabel.Name = "PlayersLabel";
            PlayersLabel.Size = new Size(50, 21);
            PlayersLabel.Text = "玩家: 0";
            // 
            // MonsterLabel
            // 
            MonsterLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            MonsterLabel.Name = "MonsterLabel";
            MonsterLabel.Size = new Size(50, 21);
            MonsterLabel.Text = "怪物: 0";
            // 
            // ConnectionsLabel
            // 
            ConnectionsLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            ConnectionsLabel.Name = "ConnectionsLabel";
            ConnectionsLabel.Size = new Size(50, 21);
            ConnectionsLabel.Text = "链接: 0";
            // 
            // BlockedIPsLabel
            // 
            BlockedIPsLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            BlockedIPsLabel.Name = "BlockedIPsLabel";
            BlockedIPsLabel.Size = new Size(95, 21);
            BlockedIPsLabel.Text = "屏蔽锁定 IPs: 0";
            // 
            // CycleDelayLabel
            // 
            CycleDelayLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            CycleDelayLabel.Name = "CycleDelayLabel";
            CycleDelayLabel.Size = new Size(74, 21);
            CycleDelayLabel.Text = "延迟周期: 0";
            // 
            // MainMenu
            // 
            MainMenu.BackColor = Color.Transparent;
            MainMenu.Items.AddRange(new ToolStripItem[] { controlToolStripMenuItem, accountToolStripMenuItem, databaseFormsToolStripMenuItem, configToolStripMenuItem1, CharacterToolStripMenuItem, UpTimeLabel });
            MainMenu.Location = new Point(0, 0);
            MainMenu.Name = "MainMenu";
            MainMenu.Padding = new Padding(7, 2, 0, 2);
            MainMenu.Size = new Size(566, 25);
            MainMenu.TabIndex = 3;
            MainMenu.Text = "menuStrip1";
            // 
            // controlToolStripMenuItem
            // 
            controlToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { startServerToolStripMenuItem, stopServerToolStripMenuItem, rebootServerToolStripMenuItem, closeServerToolStripMenuItem, clearBlockedIPsToolStripMenuItem, toolStripMenuItem1, toolStripSeparator1, reloadToolStripMenuItem });
            controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            controlToolStripMenuItem.Size = new Size(44, 21);
            controlToolStripMenuItem.Text = "控制";
            // 
            // startServerToolStripMenuItem
            // 
            startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            startServerToolStripMenuItem.Size = new Size(180, 22);
            startServerToolStripMenuItem.Text = "开启服务器";
            startServerToolStripMenuItem.Click += startServerToolStripMenuItem_Click;
            // 
            // stopServerToolStripMenuItem
            // 
            stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            stopServerToolStripMenuItem.Size = new Size(180, 22);
            stopServerToolStripMenuItem.Text = "暂停服务器";
            stopServerToolStripMenuItem.Click += stopServerToolStripMenuItem_Click;
            // 
            // rebootServerToolStripMenuItem
            // 
            rebootServerToolStripMenuItem.Name = "rebootServerToolStripMenuItem";
            rebootServerToolStripMenuItem.Size = new Size(180, 22);
            rebootServerToolStripMenuItem.Text = "重启服务器";
            rebootServerToolStripMenuItem.Click += rebootServerToolStripMenuItem_Click;
            // 
            // clearBlockedIPsToolStripMenuItem
            // 
            clearBlockedIPsToolStripMenuItem.Name = "clearBlockedIPsToolStripMenuItem";
            clearBlockedIPsToolStripMenuItem.Size = new Size(180, 22);
            clearBlockedIPsToolStripMenuItem.Text = "清除锁定 IPs";
            clearBlockedIPsToolStripMenuItem.Click += clearBlockedIPsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(177, 6);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // closeServerToolStripMenuItem
            // 
            closeServerToolStripMenuItem.Name = "closeServerToolStripMenuItem";
            closeServerToolStripMenuItem.Size = new Size(180, 22);
            closeServerToolStripMenuItem.Text = "关闭服务器";
            closeServerToolStripMenuItem.Click += closeServerToolStripMenuItem_Click;
            // 
            // reloadToolStripMenuItem
            // 
            reloadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nPCsToolStripMenuItem, dropsToolStripMenuItem, lineMessageToolStripMenuItem, 物品信息ToolStripMenuItem, 怪物信息ToolStripMenuItem, toolStripMenuItem2, 任务信息ToolStripMenuItem, 配方信息ToolStripMenuItem, 商城物品ToolStripMenuItem });
            reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            reloadToolStripMenuItem.Size = new Size(180, 22);
            reloadToolStripMenuItem.Text = "重新加载";
            // 
            // nPCsToolStripMenuItem
            // 
            nPCsToolStripMenuItem.Name = "nPCsToolStripMenuItem";
            nPCsToolStripMenuItem.Size = new Size(180, 22);
            nPCsToolStripMenuItem.Text = "NPC信息";
            nPCsToolStripMenuItem.Click += nPCsToolStripMenuItem_Click;
            // 
            // dropsToolStripMenuItem
            // 
            dropsToolStripMenuItem.Name = "dropsToolStripMenuItem";
            dropsToolStripMenuItem.Size = new Size(180, 22);
            dropsToolStripMenuItem.Text = "掉落数据";
            dropsToolStripMenuItem.Click += dropsToolStripMenuItem_Click;
            // 
            // lineMessageToolStripMenuItem
            // 
            lineMessageToolStripMenuItem.Name = "lineMessageToolStripMenuItem";
            lineMessageToolStripMenuItem.Size = new Size(180, 22);
            lineMessageToolStripMenuItem.Text = "连接信息";
            lineMessageToolStripMenuItem.Click += lineMessageToolStripMenuItem_Click;
            // 
            // 物品信息ToolStripMenuItem
            // 
            物品信息ToolStripMenuItem.Name = "物品信息ToolStripMenuItem";
            物品信息ToolStripMenuItem.Size = new Size(180, 22);
            物品信息ToolStripMenuItem.Text = "物品信息";
            物品信息ToolStripMenuItem.Click += 物品信息ToolStripMenuItem_Click;
            // 
            // 怪物信息ToolStripMenuItem
            // 
            怪物信息ToolStripMenuItem.Name = "怪物信息ToolStripMenuItem";
            怪物信息ToolStripMenuItem.Size = new Size(180, 22);
            怪物信息ToolStripMenuItem.Text = "怪物信息";
            怪物信息ToolStripMenuItem.Click += 怪物信息ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(180, 22);
            toolStripMenuItem2.Text = "技能信息";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // 任务信息ToolStripMenuItem
            // 
            任务信息ToolStripMenuItem.Name = "任务信息ToolStripMenuItem";
            任务信息ToolStripMenuItem.Size = new Size(180, 22);
            任务信息ToolStripMenuItem.Text = "任务信息";
            任务信息ToolStripMenuItem.Click += 任务信息ToolStripMenuItem_Click;
            // 
            // 配方信息ToolStripMenuItem
            // 
            配方信息ToolStripMenuItem.Name = "配方信息ToolStripMenuItem";
            配方信息ToolStripMenuItem.Size = new Size(180, 22);
            配方信息ToolStripMenuItem.Text = "配方和BUFF";
            配方信息ToolStripMenuItem.Click += 配方信息ToolStripMenuItem_Click;
            // 
            // 商城物品ToolStripMenuItem
            // 
            商城物品ToolStripMenuItem.Name = "商城物品ToolStripMenuItem";
            商城物品ToolStripMenuItem.Size = new Size(180, 22);
            商城物品ToolStripMenuItem.Text = "商城物品";
            商城物品ToolStripMenuItem.Click += 商城物品ToolStripMenuItem_Click;
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { accountsToolStripMenuItem1, marketToolStripMenuItem, namelistsToolStripMenuItem });
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Size = new Size(44, 21);
            accountToolStripMenuItem.Text = "账户";
            // 
            // accountsToolStripMenuItem1
            // 
            accountsToolStripMenuItem1.Name = "accountsToolStripMenuItem1";
            accountsToolStripMenuItem1.Size = new Size(124, 22);
            accountsToolStripMenuItem1.Text = "玩家账户";
            accountsToolStripMenuItem1.Click += accountsToolStripMenuItem1_Click;
            // 
            // marketToolStripMenuItem
            // 
            marketToolStripMenuItem.Name = "marketToolStripMenuItem";
            marketToolStripMenuItem.Size = new Size(124, 22);
            marketToolStripMenuItem.Text = "游戏市场";
            marketToolStripMenuItem.Click += marketToolStripMenuItem_Click;
            // 
            // namelistsToolStripMenuItem
            // 
            namelistsToolStripMenuItem.Name = "namelistsToolStripMenuItem";
            namelistsToolStripMenuItem.Size = new Size(124, 22);
            namelistsToolStripMenuItem.Text = "列表文件";
            namelistsToolStripMenuItem.Click += namelistsToolStripMenuItem_Click;
            // 
            // databaseFormsToolStripMenuItem
            // 
            databaseFormsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mapInfoToolStripMenuItem, itemInfoToolStripMenuItem, monsterInfoToolStripMenuItem, itemNEWToolStripMenuItem, monsterExperimentalToolStripMenuItem, nPCInfoToolStripMenuItem, questInfoToolStripMenuItem, magicInfoToolStripMenuItem, gameshopToolStripMenuItem, recipeToolStripMenuItem });
            databaseFormsToolStripMenuItem.Name = "databaseFormsToolStripMenuItem";
            databaseFormsToolStripMenuItem.Size = new Size(56, 21);
            databaseFormsToolStripMenuItem.Text = "数据库";
            // 
            // mapInfoToolStripMenuItem
            // 
            mapInfoToolStripMenuItem.Name = "mapInfoToolStripMenuItem";
            mapInfoToolStripMenuItem.Size = new Size(181, 22);
            mapInfoToolStripMenuItem.Text = "地图信息(导入导出)";
            mapInfoToolStripMenuItem.Click += mapInfoToolStripMenuItem_Click;
            // 
            // itemInfoToolStripMenuItem
            // 
            itemInfoToolStripMenuItem.Name = "itemInfoToolStripMenuItem";
            itemInfoToolStripMenuItem.Size = new Size(181, 22);
            itemInfoToolStripMenuItem.Text = "物品信息";
            itemInfoToolStripMenuItem.Click += itemInfoToolStripMenuItem_Click;
            // 
            // monsterInfoToolStripMenuItem
            // 
            monsterInfoToolStripMenuItem.Name = "monsterInfoToolStripMenuItem";
            monsterInfoToolStripMenuItem.Size = new Size(181, 22);
            monsterInfoToolStripMenuItem.Text = "怪物信息";
            monsterInfoToolStripMenuItem.Click += monsterInfoToolStripMenuItem_Click;
            // 
            // itemNEWToolStripMenuItem
            // 
            itemNEWToolStripMenuItem.Name = "itemNEWToolStripMenuItem";
            itemNEWToolStripMenuItem.Size = new Size(181, 22);
            itemNEWToolStripMenuItem.Text = "物品编辑(导入导出)";
            itemNEWToolStripMenuItem.Click += itemNEWToolStripMenuItem_Click;
            // 
            // monsterExperimentalToolStripMenuItem
            // 
            monsterExperimentalToolStripMenuItem.Name = "monsterExperimentalToolStripMenuItem";
            monsterExperimentalToolStripMenuItem.Size = new Size(181, 22);
            monsterExperimentalToolStripMenuItem.Text = "怪物编辑(导入导出)";
            monsterExperimentalToolStripMenuItem.Click += monsterExperimentalToolStripMenuItem_Click;
            // 
            // nPCInfoToolStripMenuItem
            // 
            nPCInfoToolStripMenuItem.Name = "nPCInfoToolStripMenuItem";
            nPCInfoToolStripMenuItem.Size = new Size(181, 22);
            nPCInfoToolStripMenuItem.Text = "NPC信息(导入导出)";
            nPCInfoToolStripMenuItem.Click += nPCInfoToolStripMenuItem_Click;
            // 
            // questInfoToolStripMenuItem
            // 
            questInfoToolStripMenuItem.Name = "questInfoToolStripMenuItem";
            questInfoToolStripMenuItem.Size = new Size(181, 22);
            questInfoToolStripMenuItem.Text = "任务系统(导入导出)";
            questInfoToolStripMenuItem.Click += questInfoToolStripMenuItem_Click;
            // 
            // magicInfoToolStripMenuItem
            // 
            magicInfoToolStripMenuItem.Name = "magicInfoToolStripMenuItem";
            magicInfoToolStripMenuItem.Size = new Size(181, 22);
            magicInfoToolStripMenuItem.Text = "技能信息";
            magicInfoToolStripMenuItem.Click += magicInfoToolStripMenuItem_Click;
            // 
            // gameshopToolStripMenuItem
            // 
            gameshopToolStripMenuItem.Name = "gameshopToolStripMenuItem";
            gameshopToolStripMenuItem.Size = new Size(181, 22);
            gameshopToolStripMenuItem.Text = "游戏商城";
            gameshopToolStripMenuItem.Click += gameshopToolStripMenuItem_Click;
            // 
            // recipeToolStripMenuItem
            // 
            recipeToolStripMenuItem.Name = "recipeToolStripMenuItem";
            recipeToolStripMenuItem.Size = new Size(181, 22);
            recipeToolStripMenuItem.Text = "合成配方";
            recipeToolStripMenuItem.Click += recipeToolStripMenuItem_Click;
            // 
            // configToolStripMenuItem1
            // 
            configToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { serverToolStripMenuItem, balanceToolStripMenuItem, systemToolStripMenuItem, monsterTunerToolStripMenuItem, dropBuilderToolStripMenuItem });
            configToolStripMenuItem1.Name = "configToolStripMenuItem1";
            configToolStripMenuItem1.Size = new Size(44, 21);
            configToolStripMenuItem1.Text = "配置";
            // 
            // serverToolStripMenuItem
            // 
            serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            serverToolStripMenuItem.Size = new Size(160, 22);
            serverToolStripMenuItem.Text = "服务器配置";
            serverToolStripMenuItem.Click += serverToolStripMenuItem_Click;
            // 
            // balanceToolStripMenuItem
            // 
            balanceToolStripMenuItem.Name = "balanceToolStripMenuItem";
            balanceToolStripMenuItem.Size = new Size(160, 22);
            balanceToolStripMenuItem.Text = "游戏平衡性调整";
            balanceToolStripMenuItem.Click += balanceToolStripMenuItem_Click;
            // 
            // systemToolStripMenuItem
            // 
            systemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dragonSystemToolStripMenuItem, miningToolStripMenuItem, guildsToolStripMenuItem, fishingToolStripMenuItem, mailToolStripMenuItem, goodsToolStripMenuItem, refiningToolStripMenuItem, relationshipToolStripMenuItem, mentorToolStripMenuItem, gemToolStripMenuItem, conquestToolStripMenuItem, respawnsToolStripMenuItem, heroesToolStripMenuItem });
            systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            systemToolStripMenuItem.Size = new Size(160, 22);
            systemToolStripMenuItem.Text = "游戏系统";
            // 
            // dragonSystemToolStripMenuItem
            // 
            dragonSystemToolStripMenuItem.Name = "dragonSystemToolStripMenuItem";
            dragonSystemToolStripMenuItem.Size = new Size(124, 22);
            dragonSystemToolStripMenuItem.Text = "破天魔龙";
            dragonSystemToolStripMenuItem.Click += dragonSystemToolStripMenuItem_Click;
            // 
            // miningToolStripMenuItem
            // 
            miningToolStripMenuItem.Name = "miningToolStripMenuItem";
            miningToolStripMenuItem.Size = new Size(124, 22);
            miningToolStripMenuItem.Text = "矿石";
            miningToolStripMenuItem.Click += miningToolStripMenuItem_Click;
            // 
            // guildsToolStripMenuItem
            // 
            guildsToolStripMenuItem.Name = "guildsToolStripMenuItem";
            guildsToolStripMenuItem.Size = new Size(124, 22);
            guildsToolStripMenuItem.Text = "行会系统";
            guildsToolStripMenuItem.Click += guildsToolStripMenuItem_Click;
            // 
            // fishingToolStripMenuItem
            // 
            fishingToolStripMenuItem.Name = "fishingToolStripMenuItem";
            fishingToolStripMenuItem.Size = new Size(124, 22);
            fishingToolStripMenuItem.Text = "钓鱼";
            fishingToolStripMenuItem.Click += fishingToolStripMenuItem_Click;
            // 
            // mailToolStripMenuItem
            // 
            mailToolStripMenuItem.Name = "mailToolStripMenuItem";
            mailToolStripMenuItem.Size = new Size(124, 22);
            mailToolStripMenuItem.Text = "邮寄系统";
            mailToolStripMenuItem.Click += mailToolStripMenuItem_Click;
            // 
            // goodsToolStripMenuItem
            // 
            goodsToolStripMenuItem.Name = "goodsToolStripMenuItem";
            goodsToolStripMenuItem.Size = new Size(124, 22);
            goodsToolStripMenuItem.Text = "个人商店";
            goodsToolStripMenuItem.Click += goodsToolStripMenuItem_Click;
            // 
            // refiningToolStripMenuItem
            // 
            refiningToolStripMenuItem.Name = "refiningToolStripMenuItem";
            refiningToolStripMenuItem.Size = new Size(124, 22);
            refiningToolStripMenuItem.Text = "精炼系统";
            refiningToolStripMenuItem.Click += refiningToolStripMenuItem_Click;
            // 
            // relationshipToolStripMenuItem
            // 
            relationshipToolStripMenuItem.Name = "relationshipToolStripMenuItem";
            relationshipToolStripMenuItem.Size = new Size(124, 22);
            relationshipToolStripMenuItem.Text = "结婚";
            relationshipToolStripMenuItem.Click += relationshipToolStripMenuItem_Click;
            // 
            // mentorToolStripMenuItem
            // 
            mentorToolStripMenuItem.Name = "mentorToolStripMenuItem";
            mentorToolStripMenuItem.Size = new Size(124, 22);
            mentorToolStripMenuItem.Text = "师徒";
            mentorToolStripMenuItem.Click += mentorToolStripMenuItem_Click;
            // 
            // gemToolStripMenuItem
            // 
            gemToolStripMenuItem.Name = "gemToolStripMenuItem";
            gemToolStripMenuItem.Size = new Size(124, 22);
            gemToolStripMenuItem.Text = "宝石系统";
            gemToolStripMenuItem.Click += gemToolStripMenuItem_Click;
            // 
            // conquestToolStripMenuItem
            // 
            conquestToolStripMenuItem.Name = "conquestToolStripMenuItem";
            conquestToolStripMenuItem.Size = new Size(124, 22);
            conquestToolStripMenuItem.Text = "攻城系统";
            conquestToolStripMenuItem.Click += conquestToolStripMenuItem_Click;
            // 
            // respawnsToolStripMenuItem
            // 
            respawnsToolStripMenuItem.Name = "respawnsToolStripMenuItem";
            respawnsToolStripMenuItem.Size = new Size(124, 22);
            respawnsToolStripMenuItem.Text = "刷新周期";
            respawnsToolStripMenuItem.Click += respawnsToolStripMenuItem_Click;
            // 
            // heroesToolStripMenuItem
            // 
            heroesToolStripMenuItem.Name = "heroesToolStripMenuItem";
            heroesToolStripMenuItem.Size = new Size(124, 22);
            heroesToolStripMenuItem.Text = "英雄系统";
            heroesToolStripMenuItem.Click += heroesToolStripMenuItem_Click;
            // 
            // monsterTunerToolStripMenuItem
            // 
            monsterTunerToolStripMenuItem.Name = "monsterTunerToolStripMenuItem";
            monsterTunerToolStripMenuItem.Size = new Size(160, 22);
            monsterTunerToolStripMenuItem.Text = "怪物调整";
            monsterTunerToolStripMenuItem.Click += monsterTunerToolStripMenuItem_Click;
            // 
            // dropBuilderToolStripMenuItem
            // 
            dropBuilderToolStripMenuItem.Name = "dropBuilderToolStripMenuItem";
            dropBuilderToolStripMenuItem.Size = new Size(160, 22);
            dropBuilderToolStripMenuItem.Text = "物品掉落设置";
            dropBuilderToolStripMenuItem.Click += dropBuilderToolStripMenuItem_Click;
            // 
            // CharacterToolStripMenuItem
            // 
            CharacterToolStripMenuItem.Name = "CharacterToolStripMenuItem";
            CharacterToolStripMenuItem.Size = new Size(44, 21);
            CharacterToolStripMenuItem.Text = "角色";
            CharacterToolStripMenuItem.Click += CharacterToolStripMenuItem_Click;
            // 
            // UpTimeLabel
            // 
            UpTimeLabel.Alignment = ToolStripItemAlignment.Right;
            UpTimeLabel.BorderStyle = BorderStyle.None;
            UpTimeLabel.Name = "UpTimeLabel";
            UpTimeLabel.ReadOnly = true;
            UpTimeLabel.Size = new Size(200, 21);
            UpTimeLabel.Text = "运行时间:";
            // 
            // InterfaceTimer
            // 
            InterfaceTimer.Enabled = true;
            InterfaceTimer.Tick += InterfaceTimer_Tick;
            // 
            // SMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(566, 516);
            Controls.Add(MainTabs);
            Controls.Add(StatusBar);
            Controls.Add(MainMenu);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "SMain";
            Text = "Legend of Mir 2 Server";
            FormClosing += SMain_FormClosing;
            Load += SMain_Load;
            MainTabs.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage6.ResumeLayout(false);
            StatusBar.ResumeLayout(false);
            StatusBar.PerformLayout();
            MainMenu.ResumeLayout(false);
            MainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl MainTabs;
        private TabPage tabPage1;
        private TextBox LogTextBox;
        private StatusStrip StatusBar;
        private ToolStripStatusLabel PlayersLabel;
        private ToolStripStatusLabel MonsterLabel;
        private ToolStripStatusLabel ConnectionsLabel;
        private MenuStrip MainMenu;
        private ToolStripMenuItem controlToolStripMenuItem;
        private ToolStripMenuItem startServerToolStripMenuItem;
        private ToolStripMenuItem stopServerToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem closeServerToolStripMenuItem;
        private Timer InterfaceTimer;
        private TabPage tabPage2;
        private TextBox DebugLogTextBox;
        private TabPage tabPage3;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem databaseFormsToolStripMenuItem;
        private ToolStripMenuItem mapInfoToolStripMenuItem;
        private ToolStripMenuItem itemInfoToolStripMenuItem;
        private ToolStripMenuItem monsterInfoToolStripMenuItem;
        private ToolStripMenuItem nPCInfoToolStripMenuItem;
        private ToolStripMenuItem questInfoToolStripMenuItem;
        private ToolStripMenuItem configToolStripMenuItem1;
        private ToolStripMenuItem serverToolStripMenuItem;
        private ToolStripMenuItem balanceToolStripMenuItem;
        private ToolStripMenuItem systemToolStripMenuItem;
        private ToolStripMenuItem dragonSystemToolStripMenuItem;
        private ToolStripMenuItem guildsToolStripMenuItem;
        private ToolStripMenuItem miningToolStripMenuItem;
        private ToolStripMenuItem fishingToolStripMenuItem;
        private TabPage tabPage4;
        private GroupBox groupBox1;
        private Button GlobalMessageButton;
        private TextBox GlobalMessageTextBox;
        private CustomFormControl.ListViewNF PlayersOnlineListView;
        private ColumnHeader nameHeader;
        private ColumnHeader levelHeader;
        private ColumnHeader classHeader;
        private ColumnHeader genderHeader;
        private ColumnHeader indexHeader;
        private ToolStripMenuItem mailToolStripMenuItem;
        private ToolStripMenuItem goodsToolStripMenuItem;
        private ToolStripStatusLabel CycleDelayLabel;
        private ToolStripMenuItem magicInfoToolStripMenuItem;
        private ToolStripMenuItem refiningToolStripMenuItem;
        private ToolStripMenuItem relationshipToolStripMenuItem;
        private ToolStripMenuItem mentorToolStripMenuItem;
        private ToolStripMenuItem gameshopToolStripMenuItem;
        private ToolStripMenuItem gemToolStripMenuItem;
        private ToolStripMenuItem conquestToolStripMenuItem;
        private ToolStripMenuItem rebootServerToolStripMenuItem;
        private ToolStripMenuItem respawnsToolStripMenuItem;
        private ToolStripMenuItem monsterTunerToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem itemNEWToolStripMenuItem;
        private ToolStripMenuItem monsterExperimentalToolStripMenuItem;
        private ToolStripMenuItem dropBuilderToolStripMenuItem;
        private ToolStripStatusLabel BlockedIPsLabel;
        private ToolStripMenuItem clearBlockedIPsToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem nPCsToolStripMenuItem;
        private ToolStripMenuItem dropsToolStripMenuItem;
        private ToolStripMenuItem lineMessageToolStripMenuItem;
        private TabPage tabPage5;
        private CustomFormControl.ListViewNF GuildListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ToolStripTextBox UpTimeLabel;
        private ToolStripMenuItem heroesToolStripMenuItem;
        private ToolStripMenuItem CharacterToolStripMenuItem;
        private TabPage tabPage6;
        private Button LoadMonstersButton;
        private ListView MonsterListView;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ToolStripMenuItem recipeToolStripMenuItem;
        private ToolStripMenuItem accountsToolStripMenuItem1;
        private ToolStripMenuItem marketToolStripMenuItem;
        private ToolStripMenuItem namelistsToolStripMenuItem;
        internal TextBox ChatLogTextBox;
        private ToolStripMenuItem 物品信息ToolStripMenuItem;
        private ToolStripMenuItem 怪物信息ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem 任务信息ToolStripMenuItem;
        private ToolStripMenuItem 配方信息ToolStripMenuItem;
        private ToolStripMenuItem 商城物品ToolStripMenuItem;
    }
}

