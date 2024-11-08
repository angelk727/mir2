namespace Server.Systems
{
    partial class GuildItemForm
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
            GuildItemListView = new ListView();
            indexHeader = new ColumnHeader();
            PlaceHeader = new ColumnHeader();
            nameHeader = new ColumnHeader();
            countHeader = new ColumnHeader();
            DuraHeader = new ColumnHeader();
            MemberListView = new ListView();
            Members = new ColumnHeader();
            Rank = new ColumnHeader();
            DeleteButton = new Button();
            GuildNoticeBox = new RichTextBox();
            MemberCountLabel = new Label();
            BuffListView = new ListView();
            BuffID = new ColumnHeader();
            BuffName = new ColumnHeader();
            BuffActivity = new ColumnHeader();
            BuffTime = new ColumnHeader();
            GuildNoticeGroupBox = new GroupBox();
            RefreshNoticeButton = new Button();
            GuildStorageGroupBox = new GroupBox();
            GuildMembersGroupBox = new GroupBox();
            GuildRanksListView = new ListView();
            GuildRank = new ColumnHeader();
            GuildEXPLabel = new Label();
            GuildBuffsGroupBox = new GroupBox();
            GuildPointsLabel = new Label();
            SendGuildMesageBox = new TextBox();
            SendGuildMessageButton = new Button();
            GuildChatGroupBox = new GroupBox();
            GuildChatBox = new RichTextBox();
            GuildNoticeGroupBox.SuspendLayout();
            GuildStorageGroupBox.SuspendLayout();
            GuildMembersGroupBox.SuspendLayout();
            GuildBuffsGroupBox.SuspendLayout();
            GuildChatGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // GuildItemListView
            // 
            GuildItemListView.Columns.AddRange(new ColumnHeader[] { indexHeader, PlaceHeader, nameHeader, countHeader, DuraHeader });
            GuildItemListView.GridLines = true;
            GuildItemListView.Location = new Point(10, 25);
            GuildItemListView.Name = "GuildItemListView";
            GuildItemListView.Size = new Size(520, 469);
            GuildItemListView.TabIndex = 0;
            GuildItemListView.UseCompatibleStateImageBehavior = false;
            GuildItemListView.View = View.Details;
            // 
            // indexHeader
            // 
            indexHeader.Text = "物品ID";
            indexHeader.Width = 70;
            // 
            // PlaceHeader
            // 
            PlaceHeader.Text = "储存者";
            PlaceHeader.Width = 130;
            // 
            // nameHeader
            // 
            nameHeader.Text = "物品名称";
            nameHeader.Width = 115;
            // 
            // countHeader
            // 
            countHeader.Text = "数量";
            countHeader.Width = 95;
            // 
            // DuraHeader
            // 
            DuraHeader.Text = "持久";
            DuraHeader.Width = 155;
            // 
            // MemberListView
            // 
            MemberListView.Columns.AddRange(new ColumnHeader[] { Members, Rank });
            MemberListView.FullRowSelect = true;
            MemberListView.GridLines = true;
            MemberListView.Location = new Point(8, 25);
            MemberListView.Name = "MemberListView";
            MemberListView.Scrollable = false;
            MemberListView.Size = new Size(291, 418);
            MemberListView.TabIndex = 1;
            MemberListView.UseCompatibleStateImageBehavior = false;
            MemberListView.View = View.Details;
            // 
            // Members
            // 
            Members.Text = "成员";
            Members.Width = 160;
            // 
            // Rank
            // 
            Rank.Text = "官阶";
            Rank.Width = 180;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(104, 456);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(100, 26);
            DeleteButton.TabIndex = 2;
            DeleteButton.Text = "删除";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // GuildNoticeBox
            // 
            GuildNoticeBox.Location = new Point(8, 25);
            GuildNoticeBox.Name = "GuildNoticeBox";
            GuildNoticeBox.Size = new Size(399, 281);
            GuildNoticeBox.TabIndex = 3;
            GuildNoticeBox.Text = "";
            // 
            // MemberCountLabel
            // 
            MemberCountLabel.AutoSize = true;
            MemberCountLabel.Location = new Point(305, 456);
            MemberCountLabel.Name = "MemberCountLabel";
            MemberCountLabel.Size = new Size(59, 17);
            MemberCountLabel.TabIndex = 4;
            MemberCountLabel.Text = "成员数量:";
            // 
            // BuffListView
            // 
            BuffListView.Columns.AddRange(new ColumnHeader[] { BuffID, BuffName, BuffActivity, BuffTime });
            BuffListView.FullRowSelect = true;
            BuffListView.GridLines = true;
            BuffListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            BuffListView.Location = new Point(6, 45);
            BuffListView.Name = "BuffListView";
            BuffListView.Size = new Size(422, 288);
            BuffListView.TabIndex = 5;
            BuffListView.UseCompatibleStateImageBehavior = false;
            BuffListView.View = View.Details;
            // 
            // BuffID
            // 
            BuffID.Text = "特效ID";
            // 
            // BuffName
            // 
            BuffName.Text = "特效名称";
            BuffName.Width = 140;
            // 
            // BuffActivity
            // 
            BuffActivity.Text = "当前状态";
            BuffActivity.Width = 80;
            // 
            // BuffTime
            // 
            BuffTime.Text = "时长(分)";
            BuffTime.Width = 135;
            // 
            // GuildNoticeGroupBox
            // 
            GuildNoticeGroupBox.Controls.Add(RefreshNoticeButton);
            GuildNoticeGroupBox.Controls.Add(GuildNoticeBox);
            GuildNoticeGroupBox.Location = new Point(442, 512);
            GuildNoticeGroupBox.Name = "GuildNoticeGroupBox";
            GuildNoticeGroupBox.Size = new Size(413, 346);
            GuildNoticeGroupBox.TabIndex = 6;
            GuildNoticeGroupBox.TabStop = false;
            GuildNoticeGroupBox.Text = "公会公告";
            // 
            // RefreshNoticeButton
            // 
            RefreshNoticeButton.Location = new Point(166, 313);
            RefreshNoticeButton.Name = "RefreshNoticeButton";
            RefreshNoticeButton.Size = new Size(94, 26);
            RefreshNoticeButton.TabIndex = 4;
            RefreshNoticeButton.Text = "更新公告";
            RefreshNoticeButton.UseVisualStyleBackColor = true;
            RefreshNoticeButton.Click += RefreshNoticeButton_Click;
            // 
            // GuildStorageGroupBox
            // 
            GuildStorageGroupBox.Controls.Add(GuildItemListView);
            GuildStorageGroupBox.Location = new Point(2, 0);
            GuildStorageGroupBox.Name = "GuildStorageGroupBox";
            GuildStorageGroupBox.Size = new Size(539, 505);
            GuildStorageGroupBox.TabIndex = 7;
            GuildStorageGroupBox.TabStop = false;
            GuildStorageGroupBox.Text = "公会仓库";
            // 
            // GuildMembersGroupBox
            // 
            GuildMembersGroupBox.Controls.Add(GuildRanksListView);
            GuildMembersGroupBox.Controls.Add(GuildEXPLabel);
            GuildMembersGroupBox.Controls.Add(MemberListView);
            GuildMembersGroupBox.Controls.Add(DeleteButton);
            GuildMembersGroupBox.Controls.Add(MemberCountLabel);
            GuildMembersGroupBox.Location = new Point(547, 0);
            GuildMembersGroupBox.Name = "GuildMembersGroupBox";
            GuildMembersGroupBox.Size = new Size(595, 505);
            GuildMembersGroupBox.TabIndex = 8;
            GuildMembersGroupBox.TabStop = false;
            GuildMembersGroupBox.Text = "公会成员/公会官级";
            // 
            // GuildRanksListView
            // 
            GuildRanksListView.Columns.AddRange(new ColumnHeader[] { GuildRank });
            GuildRanksListView.FullRowSelect = true;
            GuildRanksListView.GridLines = true;
            GuildRanksListView.Location = new Point(305, 25);
            GuildRanksListView.Name = "GuildRanksListView";
            GuildRanksListView.Size = new Size(284, 418);
            GuildRanksListView.TabIndex = 7;
            GuildRanksListView.UseCompatibleStateImageBehavior = false;
            GuildRanksListView.View = View.Details;
            // 
            // GuildRank
            // 
            GuildRank.Text = "公会官级";
            GuildRank.Width = 284;
            // 
            // GuildEXPLabel
            // 
            GuildEXPLabel.AutoSize = true;
            GuildEXPLabel.Location = new Point(307, 482);
            GuildEXPLabel.Name = "GuildEXPLabel";
            GuildEXPLabel.Size = new Size(59, 17);
            GuildEXPLabel.TabIndex = 5;
            GuildEXPLabel.Text = "公会经验:";
            // 
            // GuildBuffsGroupBox
            // 
            GuildBuffsGroupBox.Controls.Add(GuildPointsLabel);
            GuildBuffsGroupBox.Controls.Add(BuffListView);
            GuildBuffsGroupBox.Location = new Point(2, 512);
            GuildBuffsGroupBox.Name = "GuildBuffsGroupBox";
            GuildBuffsGroupBox.Size = new Size(434, 346);
            GuildBuffsGroupBox.TabIndex = 9;
            GuildBuffsGroupBox.TabStop = false;
            GuildBuffsGroupBox.Text = "公会特效";
            // 
            // GuildPointsLabel
            // 
            GuildPointsLabel.AutoSize = true;
            GuildPointsLabel.Location = new Point(6, 22);
            GuildPointsLabel.Name = "GuildPointsLabel";
            GuildPointsLabel.Size = new Size(35, 17);
            GuildPointsLabel.TabIndex = 6;
            GuildPointsLabel.Text = "点数:";
            // 
            // SendGuildMesageBox
            // 
            SendGuildMesageBox.Location = new Point(6, 314);
            SendGuildMesageBox.Name = "SendGuildMesageBox";
            SendGuildMesageBox.Size = new Size(188, 23);
            SendGuildMesageBox.TabIndex = 10;
            // 
            // SendGuildMessageButton
            // 
            SendGuildMessageButton.Location = new Point(200, 313);
            SendGuildMessageButton.Name = "SendGuildMessageButton";
            SendGuildMessageButton.Size = new Size(75, 26);
            SendGuildMessageButton.TabIndex = 11;
            SendGuildMessageButton.Text = "发送";
            SendGuildMessageButton.UseVisualStyleBackColor = true;
            SendGuildMessageButton.Click += SendGuildMessageButton_Click;
            // 
            // GuildChatGroupBox
            // 
            GuildChatGroupBox.Controls.Add(GuildChatBox);
            GuildChatGroupBox.Controls.Add(SendGuildMessageButton);
            GuildChatGroupBox.Controls.Add(SendGuildMesageBox);
            GuildChatGroupBox.Location = new Point(861, 512);
            GuildChatGroupBox.Name = "GuildChatGroupBox";
            GuildChatGroupBox.Size = new Size(281, 346);
            GuildChatGroupBox.TabIndex = 12;
            GuildChatGroupBox.TabStop = false;
            GuildChatGroupBox.Text = "公会聊天";
            // 
            // GuildChatBox
            // 
            GuildChatBox.Location = new Point(6, 25);
            GuildChatBox.Name = "GuildChatBox";
            GuildChatBox.ReadOnly = true;
            GuildChatBox.Size = new Size(269, 281);
            GuildChatBox.TabIndex = 12;
            GuildChatBox.Text = "";
            // 
            // GuildItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1154, 860);
            Controls.Add(GuildChatGroupBox);
            Controls.Add(GuildBuffsGroupBox);
            Controls.Add(GuildMembersGroupBox);
            Controls.Add(GuildStorageGroupBox);
            Controls.Add(GuildNoticeGroupBox);
            Name = "GuildItemForm";
            Text = "公会管理窗口";
            Load += GuildItemForm_Load;
            GuildNoticeGroupBox.ResumeLayout(false);
            GuildStorageGroupBox.ResumeLayout(false);
            GuildMembersGroupBox.ResumeLayout(false);
            GuildMembersGroupBox.PerformLayout();
            GuildBuffsGroupBox.ResumeLayout(false);
            GuildBuffsGroupBox.PerformLayout();
            GuildChatGroupBox.ResumeLayout(false);
            GuildChatGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ColumnHeader indexHeader;
        private ColumnHeader PlaceHeader;
        private ColumnHeader nameHeader;
        private ColumnHeader countHeader;
        private ColumnHeader DuraHeader;
        private ColumnHeader Members;
        private ColumnHeader Rank;
        private Button DeleteButton;
        public ListView GuildItemListView;
        public ListView MemberListView;
        private RichTextBox GuildNoticeBox;
        private Label MemberCountLabel;
        private ListView BuffListView;
        private ColumnHeader BuffID;
        private ColumnHeader BuffName;
        private ColumnHeader BuffActivity;
        private ColumnHeader BuffTime;
        private GroupBox GuildNoticeGroupBox;
        private GroupBox GuildStorageGroupBox;
        private GroupBox GuildMembersGroupBox;
        private GroupBox GuildBuffsGroupBox;
        private Label GuildPointsLabel;
        private Label GuildEXPLabel;
        private Button RefreshNoticeButton;
        private TextBox SendGuildMesageBox;
        private Button SendGuildMessageButton;
        private ListView GuildRanksListView;
        private ColumnHeader GuildRank;
        private GroupBox GuildChatGroupBox;
        private RichTextBox GuildChatBox;
    }
}