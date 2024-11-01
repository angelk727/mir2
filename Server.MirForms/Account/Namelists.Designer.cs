namespace Server.Account
{
    partial class Namelists
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
            NamelistsGroupBox = new GroupBox();
            NamelistView = new ListView();
            columnHeader1 = new ColumnHeader();
            PlayersGroupBox = new GroupBox();
            NamelistViewBox = new ListView();
            columnHeader2 = new ColumnHeader();
            groupBox1 = new GroupBox();
            FindPlayerBox = new TextBox();
            RefreshButton = new Button();
            NamelistCountLabel = new Label();
            label1 = new Label();
            PlayerActionsGroupBox = new GroupBox();
            AddPlayerButton = new Button();
            DeletePlayerButton = new Button();
            NamelistActionsGroupBox = new GroupBox();
            DeleteNamelistButton = new Button();
            CreateNamelistButton = new Button();
            StatisticsGroupBox = new GroupBox();
            TotalUniquePlayerLabel = new Label();
            TotalPlayerLabel = new Label();
            NamelistCount = new Label();
            NamelistsGroupBox.SuspendLayout();
            PlayersGroupBox.SuspendLayout();
            groupBox1.SuspendLayout();
            PlayerActionsGroupBox.SuspendLayout();
            NamelistActionsGroupBox.SuspendLayout();
            StatisticsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // NamelistsGroupBox
            // 
            NamelistsGroupBox.Controls.Add(NamelistView);
            NamelistsGroupBox.Location = new Point(12, 14);
            NamelistsGroupBox.Name = "NamelistsGroupBox";
            NamelistsGroupBox.Size = new Size(230, 630);
            NamelistsGroupBox.TabIndex = 0;
            NamelistsGroupBox.TabStop = false;
            NamelistsGroupBox.Text = "名称列表";
            // 
            // NamelistView
            // 
            NamelistView.BorderStyle = BorderStyle.None;
            NamelistView.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            NamelistView.FullRowSelect = true;
            NamelistView.GridLines = true;
            NamelistView.HeaderStyle = ColumnHeaderStyle.None;
            NamelistView.Location = new Point(6, 25);
            NamelistView.MultiSelect = false;
            NamelistView.Name = "NamelistView";
            NamelistView.Size = new Size(215, 596);
            NamelistView.TabIndex = 0;
            NamelistView.UseCompatibleStateImageBehavior = false;
            NamelistView.View = View.Details;
            NamelistView.SelectedIndexChanged += NamelistView_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Width = 215;
            // 
            // PlayersGroupBox
            // 
            PlayersGroupBox.Controls.Add(NamelistViewBox);
            PlayersGroupBox.Location = new Point(248, 14);
            PlayersGroupBox.Name = "PlayersGroupBox";
            PlayersGroupBox.Size = new Size(230, 630);
            PlayersGroupBox.TabIndex = 1;
            PlayersGroupBox.TabStop = false;
            PlayersGroupBox.Text = "玩家";
            // 
            // NamelistViewBox
            // 
            NamelistViewBox.BorderStyle = BorderStyle.None;
            NamelistViewBox.Columns.AddRange(new ColumnHeader[] { columnHeader2 });
            NamelistViewBox.FullRowSelect = true;
            NamelistViewBox.GridLines = true;
            NamelistViewBox.HeaderStyle = ColumnHeaderStyle.None;
            NamelistViewBox.Location = new Point(6, 25);
            NamelistViewBox.MultiSelect = false;
            NamelistViewBox.Name = "NamelistViewBox";
            NamelistViewBox.Size = new Size(215, 596);
            NamelistViewBox.TabIndex = 1;
            NamelistViewBox.UseCompatibleStateImageBehavior = false;
            NamelistViewBox.View = View.Details;
            // 
            // columnHeader2
            // 
            columnHeader2.Width = 215;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FindPlayerBox);
            groupBox1.Controls.Add(RefreshButton);
            groupBox1.Controls.Add(NamelistCountLabel);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(484, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(280, 90);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "搜索";
            // 
            // FindPlayerBox
            // 
            FindPlayerBox.Location = new Point(51, 25);
            FindPlayerBox.Name = "FindPlayerBox";
            FindPlayerBox.Size = new Size(129, 23);
            FindPlayerBox.TabIndex = 9;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(199, 25);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(75, 26);
            RefreshButton.TabIndex = 8;
            RefreshButton.Text = "刷新";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // NamelistCountLabel
            // 
            NamelistCountLabel.AutoSize = true;
            NamelistCountLabel.Location = new Point(6, 63);
            NamelistCountLabel.Name = "NamelistCountLabel";
            NamelistCountLabel.Size = new Size(91, 17);
            NamelistCountLabel.TabIndex = 7;
            NamelistCountLabel.Text = "找到在:  名单中";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 28);
            label1.Name = "label1";
            label1.Size = new Size(32, 17);
            label1.TabIndex = 6;
            label1.Text = "玩家";
            // 
            // PlayerActionsGroupBox
            // 
            PlayerActionsGroupBox.Controls.Add(AddPlayerButton);
            PlayerActionsGroupBox.Controls.Add(DeletePlayerButton);
            PlayerActionsGroupBox.Location = new Point(484, 110);
            PlayerActionsGroupBox.Name = "PlayerActionsGroupBox";
            PlayerActionsGroupBox.Size = new Size(280, 68);
            PlayerActionsGroupBox.TabIndex = 3;
            PlayerActionsGroupBox.TabStop = false;
            PlayerActionsGroupBox.Text = "操作 - 玩家";
            // 
            // AddPlayerButton
            // 
            AddPlayerButton.Location = new Point(147, 25);
            AddPlayerButton.Name = "AddPlayerButton";
            AddPlayerButton.Size = new Size(100, 26);
            AddPlayerButton.TabIndex = 7;
            AddPlayerButton.Text = "添加玩家";
            AddPlayerButton.UseVisualStyleBackColor = true;
            AddPlayerButton.Click += AddPlayerButton_Click;
            // 
            // DeletePlayerButton
            // 
            DeletePlayerButton.Location = new Point(27, 25);
            DeletePlayerButton.Name = "DeletePlayerButton";
            DeletePlayerButton.Size = new Size(100, 26);
            DeletePlayerButton.TabIndex = 6;
            DeletePlayerButton.Text = "删除玩家";
            DeletePlayerButton.UseVisualStyleBackColor = true;
            DeletePlayerButton.Click += DeletePlayerButton_Click;
            // 
            // NamelistActionsGroupBox
            // 
            NamelistActionsGroupBox.Controls.Add(DeleteNamelistButton);
            NamelistActionsGroupBox.Controls.Add(CreateNamelistButton);
            NamelistActionsGroupBox.Location = new Point(484, 185);
            NamelistActionsGroupBox.Name = "NamelistActionsGroupBox";
            NamelistActionsGroupBox.Size = new Size(280, 68);
            NamelistActionsGroupBox.TabIndex = 4;
            NamelistActionsGroupBox.TabStop = false;
            NamelistActionsGroupBox.Text = "操作 - 名称列表";
            // 
            // DeleteNamelistButton
            // 
            DeleteNamelistButton.Location = new Point(147, 25);
            DeleteNamelistButton.Name = "DeleteNamelistButton";
            DeleteNamelistButton.Size = new Size(100, 26);
            DeleteNamelistButton.TabIndex = 9;
            DeleteNamelistButton.Text = "删除名单列表";
            DeleteNamelistButton.UseVisualStyleBackColor = true;
            DeleteNamelistButton.Click += DeleteNamelistButton_Click;
            // 
            // CreateNamelistButton
            // 
            CreateNamelistButton.Location = new Point(27, 25);
            CreateNamelistButton.Name = "CreateNamelistButton";
            CreateNamelistButton.Size = new Size(100, 26);
            CreateNamelistButton.TabIndex = 8;
            CreateNamelistButton.Text = "创建名单列表";
            CreateNamelistButton.UseVisualStyleBackColor = true;
            CreateNamelistButton.Click += CreateNamelistButton_Click;
            // 
            // StatisticsGroupBox
            // 
            StatisticsGroupBox.Controls.Add(TotalUniquePlayerLabel);
            StatisticsGroupBox.Controls.Add(TotalPlayerLabel);
            StatisticsGroupBox.Controls.Add(NamelistCount);
            StatisticsGroupBox.Location = new Point(484, 260);
            StatisticsGroupBox.Name = "StatisticsGroupBox";
            StatisticsGroupBox.Size = new Size(278, 113);
            StatisticsGroupBox.TabIndex = 5;
            StatisticsGroupBox.TabStop = false;
            StatisticsGroupBox.Text = "统计数据";
            // 
            // TotalUniquePlayerLabel
            // 
            TotalUniquePlayerLabel.AutoSize = true;
            TotalUniquePlayerLabel.Location = new Point(7, 85);
            TotalUniquePlayerLabel.Name = "TotalUniquePlayerLabel";
            TotalUniquePlayerLabel.Size = new Size(83, 17);
            TotalUniquePlayerLabel.TabIndex = 10;
            TotalUniquePlayerLabel.Text = "独立玩家总数:";
            // 
            // TotalPlayerLabel
            // 
            TotalPlayerLabel.AutoSize = true;
            TotalPlayerLabel.Location = new Point(7, 60);
            TotalPlayerLabel.Name = "TotalPlayerLabel";
            TotalPlayerLabel.Size = new Size(59, 17);
            TotalPlayerLabel.TabIndex = 9;
            TotalPlayerLabel.Text = "玩家总数:";
            // 
            // NamelistCount
            // 
            NamelistCount.AutoSize = true;
            NamelistCount.Location = new Point(7, 33);
            NamelistCount.Name = "NamelistCount";
            NamelistCount.Size = new Size(111, 17);
            NamelistCount.TabIndex = 8;
            NamelistCount.Text = "名称列表文件 数量:";
            // 
            // Namelists
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 651);
            Controls.Add(StatisticsGroupBox);
            Controls.Add(NamelistActionsGroupBox);
            Controls.Add(PlayerActionsGroupBox);
            Controls.Add(groupBox1);
            Controls.Add(PlayersGroupBox);
            Controls.Add(NamelistsGroupBox);
            Name = "Namelists";
            Text = "列表文件窗口";
            NamelistsGroupBox.ResumeLayout(false);
            PlayersGroupBox.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            PlayerActionsGroupBox.ResumeLayout(false);
            NamelistActionsGroupBox.ResumeLayout(false);
            StatisticsGroupBox.ResumeLayout(false);
            StatisticsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox NamelistsGroupBox;
        private GroupBox PlayersGroupBox;
        private GroupBox groupBox1;
        private GroupBox PlayerActionsGroupBox;
        private GroupBox NamelistActionsGroupBox;
        private ListView NamelistView;
        private ListView NamelistViewBox;
        private Label NamelistCountLabel;
        private Label label1;
        private GroupBox StatisticsGroupBox;
        private Label TotalUniquePlayerLabel;
        private Label TotalPlayerLabel;
        private Label NamelistCount;
        private Button DeletePlayerButton;
        private TextBox FindPlayerBox;
        private Button RefreshButton;
        private Button AddPlayerButton;
        private Button DeleteNamelistButton;
        private Button CreateNamelistButton;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}