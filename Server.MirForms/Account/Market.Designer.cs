namespace Server.Database
{
    partial class Market
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
            MarketListing = new ListView();
            ItemName = new ColumnHeader();
            AID = new ColumnHeader();
            Price = new ColumnHeader();
            Seller = new ColumnHeader();
            Expiry = new ColumnHeader();
            SearchBox = new TextBox();
            FilterByPlayer = new CheckBox();
            FilterByItem = new CheckBox();
            SearchLabel = new Label();
            RefreshListings = new Button();
            SearchGroupBox = new GroupBox();
            TotalItemsOwnedLabel = new Label();
            TotalItemsLabel = new Label();
            DeleteListingButton = new Button();
            ActionsGroupBox = new GroupBox();
            ReasonTextBox = new TextBox();
            ReasonLabel = new Label();
            ExpireListingButton = new Button();
            SearchGroupBox.SuspendLayout();
            ActionsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // MarketListing
            // 
            MarketListing.BorderStyle = BorderStyle.None;
            MarketListing.Columns.AddRange(new ColumnHeader[] { ItemName, AID, Price, Seller, Expiry });
            MarketListing.Dock = DockStyle.Bottom;
            MarketListing.FullRowSelect = true;
            MarketListing.GridLines = true;
            MarketListing.Location = new Point(0, 203);
            MarketListing.Name = "MarketListing";
            MarketListing.Size = new Size(562, 459);
            MarketListing.TabIndex = 0;
            MarketListing.UseCompatibleStateImageBehavior = false;
            MarketListing.View = View.Details;
            MarketListing.SelectedIndexChanged += MarketListing_SelectedIndexChanged;
            // 
            // ItemName
            // 
            ItemName.Text = "物品名称";
            ItemName.Width = 120;
            // 
            // AID
            // 
            AID.Text = "拍卖编号";
            AID.Width = 80;
            // 
            // Price
            // 
            Price.Text = "价格";
            Price.Width = 100;
            // 
            // Seller
            // 
            Seller.Text = "卖家";
            Seller.Width = 120;
            // 
            // Expiry
            // 
            Expiry.Text = "失效期";
            Expiry.Width = 140;
            // 
            // SearchBox
            // 
            SearchBox.Location = new Point(72, 25);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(100, 23);
            SearchBox.TabIndex = 1;
            // 
            // FilterByPlayer
            // 
            FilterByPlayer.AutoSize = true;
            FilterByPlayer.Location = new Point(176, 27);
            FilterByPlayer.Name = "FilterByPlayer";
            FilterByPlayer.Size = new Size(87, 21);
            FilterByPlayer.TabIndex = 2;
            FilterByPlayer.Text = "按玩家筛选";
            FilterByPlayer.UseVisualStyleBackColor = true;
            // 
            // FilterByItem
            // 
            FilterByItem.AutoSize = true;
            FilterByItem.Location = new Point(278, 27);
            FilterByItem.Name = "FilterByItem";
            FilterByItem.Size = new Size(87, 21);
            FilterByItem.TabIndex = 3;
            FilterByItem.Text = "按物品筛选";
            FilterByItem.UseVisualStyleBackColor = true;
            // 
            // SearchLabel
            // 
            SearchLabel.AutoSize = true;
            SearchLabel.Location = new Point(24, 29);
            SearchLabel.Name = "SearchLabel";
            SearchLabel.Size = new Size(32, 17);
            SearchLabel.TabIndex = 4;
            SearchLabel.Text = "查找";
            // 
            // RefreshListings
            // 
            RefreshListings.Location = new Point(421, 25);
            RefreshListings.Name = "RefreshListings";
            RefreshListings.Size = new Size(75, 26);
            RefreshListings.TabIndex = 5;
            RefreshListings.Text = "刷新";
            RefreshListings.UseVisualStyleBackColor = true;
            RefreshListings.Click += RefreshListings_Click;
            // 
            // SearchGroupBox
            // 
            SearchGroupBox.Controls.Add(TotalItemsOwnedLabel);
            SearchGroupBox.Controls.Add(TotalItemsLabel);
            SearchGroupBox.Controls.Add(FilterByPlayer);
            SearchGroupBox.Controls.Add(RefreshListings);
            SearchGroupBox.Controls.Add(SearchBox);
            SearchGroupBox.Controls.Add(SearchLabel);
            SearchGroupBox.Controls.Add(FilterByItem);
            SearchGroupBox.Location = new Point(0, 0);
            SearchGroupBox.Name = "SearchGroupBox";
            SearchGroupBox.Size = new Size(562, 97);
            SearchGroupBox.TabIndex = 6;
            SearchGroupBox.TabStop = false;
            SearchGroupBox.Text = "搜索/统计";
            // 
            // TotalItemsOwnedLabel
            // 
            TotalItemsOwnedLabel.AutoSize = true;
            TotalItemsOwnedLabel.Location = new Point(133, 66);
            TotalItemsOwnedLabel.Name = "TotalItemsOwnedLabel";
            TotalItemsOwnedLabel.Size = new Size(107, 17);
            TotalItemsOwnedLabel.TabIndex = 7;
            TotalItemsOwnedLabel.Text = "玩家拍卖物品数量:";
            // 
            // TotalItemsLabel
            // 
            TotalItemsLabel.AutoSize = true;
            TotalItemsLabel.Location = new Point(24, 66);
            TotalItemsLabel.Name = "TotalItemsLabel";
            TotalItemsLabel.Size = new Size(63, 17);
            TotalItemsLabel.TabIndex = 6;
            TotalItemsLabel.Text = "物品总数: ";
            // 
            // DeleteListingButton
            // 
            DeleteListingButton.Location = new Point(6, 29);
            DeleteListingButton.Name = "DeleteListingButton";
            DeleteListingButton.Size = new Size(96, 26);
            DeleteListingButton.TabIndex = 4;
            DeleteListingButton.Text = "删除列表";
            DeleteListingButton.UseVisualStyleBackColor = true;
            DeleteListingButton.Click += DeleteListingButton_Click;
            // 
            // ActionsGroupBox
            // 
            ActionsGroupBox.Controls.Add(DeleteListingButton);
            ActionsGroupBox.Controls.Add(ReasonTextBox);
            ActionsGroupBox.Controls.Add(ReasonLabel);
            ActionsGroupBox.Controls.Add(ExpireListingButton);
            ActionsGroupBox.Location = new Point(0, 104);
            ActionsGroupBox.Name = "ActionsGroupBox";
            ActionsGroupBox.Size = new Size(562, 95);
            ActionsGroupBox.TabIndex = 7;
            ActionsGroupBox.TabStop = false;
            ActionsGroupBox.Text = "操作";
            // 
            // ReasonTextBox
            // 
            ReasonTextBox.Location = new Point(161, 45);
            ReasonTextBox.Name = "ReasonTextBox";
            ReasonTextBox.Size = new Size(385, 23);
            ReasonTextBox.TabIndex = 3;
            // 
            // ReasonLabel
            // 
            ReasonLabel.AutoSize = true;
            ReasonLabel.Location = new Point(122, 49);
            ReasonLabel.Name = "ReasonLabel";
            ReasonLabel.Size = new Size(35, 17);
            ReasonLabel.TabIndex = 2;
            ReasonLabel.Text = "因由:";
            // 
            // ExpireListingButton
            // 
            ExpireListingButton.Location = new Point(6, 60);
            ExpireListingButton.Name = "ExpireListingButton";
            ExpireListingButton.Size = new Size(96, 26);
            ExpireListingButton.TabIndex = 1;
            ExpireListingButton.Text = "过期列表";
            ExpireListingButton.UseVisualStyleBackColor = true;
            ExpireListingButton.Click += ExpireListingButton_Click;
            // 
            // Market
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 662);
            Controls.Add(ActionsGroupBox);
            Controls.Add(SearchGroupBox);
            Controls.Add(MarketListing);
            Name = "Market";
            Text = "游戏市场窗口";
            Load += Market_Load;
            SearchGroupBox.ResumeLayout(false);
            SearchGroupBox.PerformLayout();
            ActionsGroupBox.ResumeLayout(false);
            ActionsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListView MarketListing;
        private ColumnHeader ItemName;
        private ColumnHeader AID;
        private ColumnHeader Price;
        private ColumnHeader Seller;
        private ColumnHeader Expiry;
        private TextBox SearchBox;
        private CheckBox FilterByPlayer;
        private CheckBox FilterByItem;
        private Label SearchLabel;
        private Button RefreshListings;
        private GroupBox SearchGroupBox;
        private Label TotalItemsOwnedLabel;
        private Label TotalItemsLabel;
        private GroupBox ActionsGroupBox;
        private TextBox ReasonTextBox;
        private Label ReasonLabel;
        private Button ExpireListingButton;
        private Button DeleteListingButton;
    }
}