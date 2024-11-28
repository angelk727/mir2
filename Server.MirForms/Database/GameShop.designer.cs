namespace Server
{
    partial class GameShop
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
            GameShopListBox = new ListBox();
            label14 = new Label();
            GoldPrice_textbox = new TextBox();
            label21 = new Label();
            GPPrice_textbox = new TextBox();
            label29 = new Label();
            ItemDetails_gb = new GroupBox();
            GoldOnlyBox = new CheckBox();
            CreditOnlyBox = new CheckBox();
            label6 = new Label();
            Count_textbox = new TextBox();
            LeftinStock_label = new Label();
            label3 = new Label();
            label5 = new Label();
            TotalSold_label = new Label();
            TopItem_checkbox = new CheckBox();
            DealofDay_checkbox = new CheckBox();
            Individual_checkbox = new CheckBox();
            label1 = new Label();
            Stock_textbox = new TextBox();
            Category_textbox = new TextBox();
            label4 = new Label();
            Class_combo = new ComboBox();
            groupBox3 = new GroupBox();
            ServerLog_button = new Button();
            label2 = new Label();
            CredxGold_textbox = new TextBox();
            Remove_button = new Button();
            ClassFilter_lb = new ComboBox();
            SectionFilter_lb = new ComboBox();
            CategoryFilter_lb = new ComboBox();
            ResetFilter_button = new Button();
            ItemDetails_gb.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // GameShopListBox
            // 
            GameShopListBox.FormattingEnabled = true;
            GameShopListBox.ItemHeight = 17;
            GameShopListBox.Location = new Point(14, 101);
            GameShopListBox.Margin = new Padding(4, 4, 4, 4);
            GameShopListBox.Name = "GameShopListBox";
            GameShopListBox.ScrollAlwaysVisible = true;
            GameShopListBox.SelectionMode = SelectionMode.MultiExtended;
            GameShopListBox.Size = new Size(234, 429);
            GameShopListBox.TabIndex = 11;
            GameShopListBox.SelectedIndexChanged += GameShopListBox_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(27, 137);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(63, 17);
            label14.TabIndex = 90;
            label14.Text = "金币 价格:";
            // 
            // GoldPrice_textbox
            // 
            GoldPrice_textbox.Location = new Point(100, 133);
            GoldPrice_textbox.Margin = new Padding(4, 4, 4, 4);
            GoldPrice_textbox.MaxLength = 0;
            GoldPrice_textbox.Name = "GoldPrice_textbox";
            GoldPrice_textbox.Size = new Size(131, 23);
            GoldPrice_textbox.TabIndex = 86;
            GoldPrice_textbox.TextChanged += GoldPrice_textbox_TextChanged;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(21, 101);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(71, 17);
            label21.TabIndex = 91;
            label21.Text = "元宝价格:";
            // 
            // GPPrice_textbox
            // 
            GPPrice_textbox.Location = new Point(100, 97);
            GPPrice_textbox.Margin = new Padding(4, 4, 4, 4);
            GPPrice_textbox.MaxLength = 0;
            GPPrice_textbox.Name = "GPPrice_textbox";
            GPPrice_textbox.Size = new Size(131, 23);
            GPPrice_textbox.TabIndex = 87;
            GPPrice_textbox.TextChanged += GPPrice_textbox_TextChanged;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(30, 208);
            label29.Margin = new Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.Size = new Size(59, 17);
            label29.TabIndex = 93;
            label29.Text = "职业选项:";
            // 
            // ItemDetails_gb
            // 
            ItemDetails_gb.BackColor = Color.White;
            ItemDetails_gb.Controls.Add(GoldOnlyBox);
            ItemDetails_gb.Controls.Add(CreditOnlyBox);
            ItemDetails_gb.Controls.Add(label6);
            ItemDetails_gb.Controls.Add(Count_textbox);
            ItemDetails_gb.Controls.Add(LeftinStock_label);
            ItemDetails_gb.Controls.Add(label3);
            ItemDetails_gb.Controls.Add(label5);
            ItemDetails_gb.Controls.Add(TotalSold_label);
            ItemDetails_gb.Controls.Add(TopItem_checkbox);
            ItemDetails_gb.Controls.Add(DealofDay_checkbox);
            ItemDetails_gb.Controls.Add(Individual_checkbox);
            ItemDetails_gb.Controls.Add(label1);
            ItemDetails_gb.Controls.Add(Stock_textbox);
            ItemDetails_gb.Controls.Add(GoldPrice_textbox);
            ItemDetails_gb.Controls.Add(label14);
            ItemDetails_gb.Controls.Add(label21);
            ItemDetails_gb.Controls.Add(Category_textbox);
            ItemDetails_gb.Controls.Add(GPPrice_textbox);
            ItemDetails_gb.Controls.Add(label4);
            ItemDetails_gb.Controls.Add(label29);
            ItemDetails_gb.Controls.Add(Class_combo);
            ItemDetails_gb.Location = new Point(255, 103);
            ItemDetails_gb.Margin = new Padding(4, 4, 4, 4);
            ItemDetails_gb.Name = "ItemDetails_gb";
            ItemDetails_gb.Padding = new Padding(4, 4, 4, 4);
            ItemDetails_gb.Size = new Size(312, 428);
            ItemDetails_gb.TabIndex = 98;
            ItemDetails_gb.TabStop = false;
            ItemDetails_gb.Text = "物品详细信息";
            // 
            // GoldOnlyBox
            // 
            GoldOnlyBox.AutoSize = true;
            GoldOnlyBox.Location = new Point(239, 133);
            GoldOnlyBox.Margin = new Padding(4, 4, 4, 4);
            GoldOnlyBox.Name = "GoldOnlyBox";
            GoldOnlyBox.Size = new Size(51, 21);
            GoldOnlyBox.TabIndex = 119;
            GoldOnlyBox.Text = "金币";
            GoldOnlyBox.UseVisualStyleBackColor = true;
            GoldOnlyBox.CheckedChanged += GoldOnlyBox_CheckedChanged;
            // 
            // CreditOnlyBox
            // 
            CreditOnlyBox.AutoSize = true;
            CreditOnlyBox.Location = new Point(239, 98);
            CreditOnlyBox.Margin = new Padding(4, 4, 4, 4);
            CreditOnlyBox.Name = "CreditOnlyBox";
            CreditOnlyBox.Size = new Size(63, 21);
            CreditOnlyBox.TabIndex = 118;
            CreditOnlyBox.Text = "元宝";
            CreditOnlyBox.UseVisualStyleBackColor = true;
            CreditOnlyBox.CheckedChanged += CreditOnly_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(51, 171);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(35, 17);
            label6.TabIndex = 117;
            label6.Text = "数量:";
            // 
            // Count_textbox
            // 
            Count_textbox.Location = new Point(100, 167);
            Count_textbox.Margin = new Padding(4, 4, 4, 4);
            Count_textbox.MaxLength = 0;
            Count_textbox.Name = "Count_textbox";
            Count_textbox.Size = new Size(131, 23);
            Count_textbox.TabIndex = 116;
            Count_textbox.TextChanged += Count_textbox_TextChanged;
            // 
            // LeftinStock_label
            // 
            LeftinStock_label.AutoSize = true;
            LeftinStock_label.Location = new Point(97, 61);
            LeftinStock_label.Margin = new Padding(4, 0, 4, 0);
            LeftinStock_label.Name = "LeftinStock_label";
            LeftinStock_label.RightToLeft = RightToLeft.No;
            LeftinStock_label.Size = new Size(29, 17);
            LeftinStock_label.TabIndex = 115;
            LeftinStock_label.Text = "100";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 61);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 114;
            label3.Text = "库存剩余:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 27);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(59, 17);
            label5.TabIndex = 113;
            label5.Text = "销售总额:";
            // 
            // TotalSold_label
            // 
            TotalSold_label.AutoSize = true;
            TotalSold_label.Location = new Point(96, 27);
            TotalSold_label.Margin = new Padding(4, 0, 4, 0);
            TotalSold_label.Name = "TotalSold_label";
            TotalSold_label.RightToLeft = RightToLeft.No;
            TotalSold_label.Size = new Size(29, 17);
            TotalSold_label.TabIndex = 112;
            TotalSold_label.Text = "100";
            // 
            // TopItem_checkbox
            // 
            TopItem_checkbox.AutoSize = true;
            TopItem_checkbox.Location = new Point(31, 335);
            TopItem_checkbox.Margin = new Padding(4, 4, 4, 4);
            TopItem_checkbox.Name = "TopItem_checkbox";
            TopItem_checkbox.RightToLeft = RightToLeft.Yes;
            TopItem_checkbox.Size = new Size(78, 21);
            TopItem_checkbox.TabIndex = 106;
            TopItem_checkbox.Text = ":热销物品";
            TopItem_checkbox.UseVisualStyleBackColor = true;
            TopItem_checkbox.CheckedChanged += TopItem_checkbox_CheckedChanged;
            // 
            // DealofDay_checkbox
            // 
            DealofDay_checkbox.AutoSize = true;
            DealofDay_checkbox.Location = new Point(31, 309);
            DealofDay_checkbox.Margin = new Padding(4, 4, 4, 4);
            DealofDay_checkbox.Name = "DealofDay_checkbox";
            DealofDay_checkbox.RightToLeft = RightToLeft.Yes;
            DealofDay_checkbox.Size = new Size(78, 21);
            DealofDay_checkbox.TabIndex = 105;
            DealofDay_checkbox.Text = ":推荐物品";
            DealofDay_checkbox.UseVisualStyleBackColor = true;
            DealofDay_checkbox.CheckedChanged += DealofDay_checkbox_CheckedChanged;
            // 
            // Individual_checkbox
            // 
            Individual_checkbox.AutoSize = true;
            Individual_checkbox.Location = new Point(190, 277);
            Individual_checkbox.Margin = new Padding(4, 4, 4, 4);
            Individual_checkbox.Name = "Individual_checkbox";
            Individual_checkbox.Size = new Size(75, 21);
            Individual_checkbox.TabIndex = 110;
            Individual_checkbox.Text = "物品限制";
            Individual_checkbox.UseVisualStyleBackColor = true;
            Individual_checkbox.CheckedChanged += Individual_checkbox_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 279);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(35, 17);
            label1.TabIndex = 111;
            label1.Text = "库存:";
            // 
            // Stock_textbox
            // 
            Stock_textbox.Location = new Point(100, 275);
            Stock_textbox.Margin = new Padding(4, 4, 4, 4);
            Stock_textbox.MaxLength = 0;
            Stock_textbox.Name = "Stock_textbox";
            Stock_textbox.Size = new Size(83, 23);
            Stock_textbox.TabIndex = 109;
            Stock_textbox.TextChanged += Stock_textbox_TextChanged;
            // 
            // Category_textbox
            // 
            Category_textbox.Location = new Point(100, 239);
            Category_textbox.Margin = new Padding(4, 4, 4, 4);
            Category_textbox.MaxLength = 0;
            Category_textbox.Name = "Category_textbox";
            Category_textbox.Size = new Size(201, 23);
            Category_textbox.TabIndex = 108;
            Category_textbox.TextChanged += Category_textbox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 243);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(59, 17);
            label4.TabIndex = 106;
            label4.Text = "物品类别:";
            // 
            // Class_combo
            // 
            Class_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            Class_combo.FormattingEnabled = true;
            Class_combo.Items.AddRange(new object[] { "All", "Warrior", "Assassin", "Taoist", "Wizard", "Archer" });
            Class_combo.Location = new Point(100, 204);
            Class_combo.Margin = new Padding(4, 4, 4, 4);
            Class_combo.Name = "Class_combo";
            Class_combo.Size = new Size(201, 25);
            Class_combo.TabIndex = 105;
            Class_combo.SelectedIndexChanged += Class_combo_SelectedIndexChanged;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.White;
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(CredxGold_textbox);
            groupBox3.Location = new Point(255, 7);
            groupBox3.Margin = new Padding(4, 4, 4, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 4, 4, 4);
            groupBox3.Size = new Size(312, 89);
            groupBox3.TabIndex = 105;
            groupBox3.TabStop = false;
            groupBox3.Text = "商城设置";
            // 
            // ServerLog_button
            // 
            ServerLog_button.Location = new Point(257, 532);
            ServerLog_button.Margin = new Padding(4, 4, 4, 4);
            ServerLog_button.Name = "ServerLog_button";
            ServerLog_button.Size = new Size(310, 30);
            ServerLog_button.TabIndex = 112;
            ServerLog_button.Text = "重置购买日志 (库存重置)";
            ServerLog_button.UseVisualStyleBackColor = true;
            ServerLog_button.Click += ServerLog_button_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 34);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(113, 17);
            label2.TabIndex = 92;
            label2.Text = "元宝 x 金币 比例:";
            // 
            // CredxGold_textbox
            // 
            CredxGold_textbox.Location = new Point(128, 29);
            CredxGold_textbox.Margin = new Padding(4, 4, 4, 4);
            CredxGold_textbox.MaxLength = 0;
            CredxGold_textbox.Name = "CredxGold_textbox";
            CredxGold_textbox.Size = new Size(75, 23);
            CredxGold_textbox.TabIndex = 88;
            CredxGold_textbox.TextChanged += CredxGold_textbox_TextChanged;
            // 
            // Remove_button
            // 
            Remove_button.Location = new Point(131, 532);
            Remove_button.Margin = new Padding(4, 4, 4, 4);
            Remove_button.Name = "Remove_button";
            Remove_button.Size = new Size(119, 30);
            Remove_button.TabIndex = 106;
            Remove_button.Text = "选择删除";
            Remove_button.UseVisualStyleBackColor = true;
            Remove_button.Click += Remove_button_Click;
            // 
            // ClassFilter_lb
            // 
            ClassFilter_lb.DropDownStyle = ComboBoxStyle.DropDownList;
            ClassFilter_lb.FormattingEnabled = true;
            ClassFilter_lb.Location = new Point(14, 7);
            ClassFilter_lb.Margin = new Padding(4, 4, 4, 4);
            ClassFilter_lb.Name = "ClassFilter_lb";
            ClassFilter_lb.Size = new Size(170, 25);
            ClassFilter_lb.TabIndex = 107;
            ClassFilter_lb.SelectedIndexChanged += ClassFilter_lb_SelectedIndexChanged;
            // 
            // SectionFilter_lb
            // 
            SectionFilter_lb.DropDownStyle = ComboBoxStyle.DropDownList;
            SectionFilter_lb.FormattingEnabled = true;
            SectionFilter_lb.Items.AddRange(new object[] { "All Items", "Top Items", "Sale Items", "New Items" });
            SectionFilter_lb.Location = new Point(14, 37);
            SectionFilter_lb.Margin = new Padding(4, 4, 4, 4);
            SectionFilter_lb.Name = "SectionFilter_lb";
            SectionFilter_lb.Size = new Size(170, 25);
            SectionFilter_lb.TabIndex = 108;
            SectionFilter_lb.SelectedIndexChanged += SectionFilter_lb_SelectedIndexChanged;
            // 
            // CategoryFilter_lb
            // 
            CategoryFilter_lb.DropDownStyle = ComboBoxStyle.DropDownList;
            CategoryFilter_lb.FormattingEnabled = true;
            CategoryFilter_lb.Location = new Point(14, 67);
            CategoryFilter_lb.Margin = new Padding(4, 4, 4, 4);
            CategoryFilter_lb.Name = "CategoryFilter_lb";
            CategoryFilter_lb.Size = new Size(170, 25);
            CategoryFilter_lb.TabIndex = 109;
            CategoryFilter_lb.SelectedIndexChanged += CategoryFilter_lb_SelectedIndexChanged;
            // 
            // ResetFilter_button
            // 
            ResetFilter_button.Location = new Point(191, 5);
            ResetFilter_button.Margin = new Padding(4, 4, 4, 4);
            ResetFilter_button.Name = "ResetFilter_button";
            ResetFilter_button.Size = new Size(57, 90);
            ResetFilter_button.TabIndex = 110;
            ResetFilter_button.Text = "重置";
            ResetFilter_button.UseVisualStyleBackColor = true;
            ResetFilter_button.Click += ResetFilter_button_Click;
            // 
            // GameShop
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(581, 570);
            Controls.Add(ServerLog_button);
            Controls.Add(ResetFilter_button);
            Controls.Add(CategoryFilter_lb);
            Controls.Add(SectionFilter_lb);
            Controls.Add(ClassFilter_lb);
            Controls.Add(Remove_button);
            Controls.Add(groupBox3);
            Controls.Add(ItemDetails_gb);
            Controls.Add(GameShopListBox);
            Margin = new Padding(4, 4, 4, 4);
            Name = "GameShop";
            Text = "游戏商城";
            FormClosed += GameShop_FormClosed;
            Load += GameShop_Load;
            ItemDetails_gb.ResumeLayout(false);
            ItemDetails_gb.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox GameShopListBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox GoldPrice_textbox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox GPPrice_textbox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox ItemDetails_gb;
        private System.Windows.Forms.ComboBox Class_combo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox DealofDay_checkbox;
        private System.Windows.Forms.CheckBox TopItem_checkbox;
        private System.Windows.Forms.Button Remove_button;
        private System.Windows.Forms.TextBox Category_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Individual_checkbox;
        private System.Windows.Forms.TextBox Stock_textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CredxGold_textbox;
        private System.Windows.Forms.Label TotalSold_label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LeftinStock_label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Count_textbox;
        private System.Windows.Forms.ComboBox ClassFilter_lb;
        private System.Windows.Forms.ComboBox SectionFilter_lb;
        private System.Windows.Forms.ComboBox CategoryFilter_lb;
        private System.Windows.Forms.Button ResetFilter_button;
        private System.Windows.Forms.Button ServerLog_button;
        private System.Windows.Forms.CheckBox GoldOnlyBox;
        private System.Windows.Forms.CheckBox CreditOnlyBox;
    }
}