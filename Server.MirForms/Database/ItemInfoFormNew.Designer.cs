
namespace Server.Database
{
    partial class ItemInfoFormNew
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            itemInfoGridView = new DataGridView();
            Modified = new DataGridViewCheckBoxColumn();
            ItemIndex = new DataGridViewTextBoxColumn();
            ItemName = new DataGridViewTextBoxColumn();
            ItemType = new DataGridViewComboBoxColumn();
            ItemGrade = new DataGridViewComboBoxColumn();
            ItemRequiredType = new DataGridViewComboBoxColumn();
            ItemRequiredGender = new DataGridViewComboBoxColumn();
            ItemRequiredClass = new DataGridViewComboBoxColumn();
            ItemSet = new DataGridViewComboBoxColumn();
            ItemRandomStatsId = new DataGridViewTextBoxColumn();
            ItemRequiredAmount = new DataGridViewTextBoxColumn();
            ItemImage = new DataGridViewTextBoxColumn();
            ItemShape = new DataGridViewTextBoxColumn();
            ItemEffect = new DataGridViewTextBoxColumn();
            ItemStackSize = new DataGridViewTextBoxColumn();
            ItemSlots = new DataGridViewTextBoxColumn();
            ItemWeight = new DataGridViewTextBoxColumn();
            ItemLightRange = new DataGridViewTextBoxColumn();
            ItemLightIntensity = new DataGridViewTextBoxColumn();
            ItemDurability = new DataGridViewTextBoxColumn();
            ItemPrice = new DataGridViewTextBoxColumn();
            ItemToolTip = new DataGridViewTextBoxColumn();
            StartItem = new DataGridViewCheckBoxColumn();
            NeedIdentify = new DataGridViewCheckBoxColumn();
            ShowGroupPickup = new DataGridViewCheckBoxColumn();
            GlobalDropNotify = new DataGridViewCheckBoxColumn();
            ClassBased = new DataGridViewCheckBoxColumn();
            LevelBased = new DataGridViewCheckBoxColumn();
            CanMine = new DataGridViewCheckBoxColumn();
            CanFastRun = new DataGridViewCheckBoxColumn();
            CanAwakening = new DataGridViewCheckBoxColumn();
            panel1 = new Panel();
            panel3 = new Panel();
            Gameshop_button = new Button();
            btnExport = new Button();
            btnImport = new Button();
            lblFilterType = new Label();
            groupView = new GroupBox();
            rBtnViewSpecial = new RadioButton();
            rbtnViewAll = new RadioButton();
            rbtnViewBinding = new RadioButton();
            rbtnViewBasic = new RadioButton();
            rbtnViewStats = new RadioButton();
            drpFilterType = new ComboBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)itemInfoGridView).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            groupView.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // itemInfoGridView
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            itemInfoGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            itemInfoGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            itemInfoGridView.Columns.AddRange(new DataGridViewColumn[] { Modified, ItemIndex, ItemName, ItemType, ItemGrade, ItemRequiredType, ItemRequiredGender, ItemRequiredClass, ItemSet, ItemRandomStatsId, ItemRequiredAmount, ItemImage, ItemShape, ItemEffect, ItemStackSize, ItemSlots, ItemWeight, ItemLightRange, ItemLightIntensity, ItemDurability, ItemPrice, ItemToolTip, StartItem, NeedIdentify, ShowGroupPickup, GlobalDropNotify, ClassBased, LevelBased, CanMine, CanFastRun, CanAwakening });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            itemInfoGridView.DefaultCellStyle = dataGridViewCellStyle3;
            itemInfoGridView.Dock = DockStyle.Fill;
            itemInfoGridView.Location = new Point(0, 0);
            itemInfoGridView.Margin = new Padding(4, 3, 4, 3);
            itemInfoGridView.Name = "itemInfoGridView";
            itemInfoGridView.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            itemInfoGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            itemInfoGridView.Size = new Size(1115, 567);
            itemInfoGridView.TabIndex = 0;
            itemInfoGridView.CurrentCellDirtyStateChanged += CurrentCellDirtyStateChanged;
            itemInfoGridView.DataError += itemInfoGridView_DataError;
            itemInfoGridView.DefaultValuesNeeded += itemInfoGridView_DefaultValuesNeeded;
            itemInfoGridView.UserDeletingRow += itemInfoGridView_UserDeletingRow;
            // 
            // Modified
            // 
            Modified.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            Modified.DataPropertyName = "Modified";
            Modified.Frozen = true;
            Modified.HeaderText = "修改";
            Modified.Name = "Modified";
            Modified.ReadOnly = true;
            Modified.Width = 38;
            // 
            // ItemIndex
            // 
            ItemIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ItemIndex.DataPropertyName = "ItemIndex";
            ItemIndex.Frozen = true;
            ItemIndex.HeaderText = "物品编号";
            ItemIndex.Name = "ItemIndex";
            ItemIndex.ReadOnly = true;
            ItemIndex.Width = 81;
            // 
            // ItemName
            // 
            ItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ItemName.DataPropertyName = "ItemName";
            ItemName.Frozen = true;
            ItemName.HeaderText = "物品名称";
            ItemName.Name = "ItemName";
            ItemName.Width = 81;
            // 
            // ItemType
            // 
            ItemType.DataPropertyName = "ItemType";
            ItemType.HeaderText = "物品类型";
            ItemType.Name = "ItemType";
            ItemType.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // ItemGrade
            // 
            ItemGrade.DataPropertyName = "ItemGrade";
            ItemGrade.HeaderText = "物品品质";
            ItemGrade.Name = "ItemGrade";
            // 
            // ItemRequiredType
            // 
            ItemRequiredType.DataPropertyName = "ItemRequiredType";
            ItemRequiredType.HeaderText = "需要类型";
            ItemRequiredType.Name = "ItemRequiredType";
            // 
            // ItemRequiredGender
            // 
            ItemRequiredGender.DataPropertyName = "ItemRequiredGender";
            ItemRequiredGender.HeaderText = "性别类型";
            ItemRequiredGender.Name = "ItemRequiredGender";
            // 
            // ItemRequiredClass
            // 
            ItemRequiredClass.DataPropertyName = "ItemRequiredClass";
            ItemRequiredClass.HeaderText = "职业类型";
            ItemRequiredClass.Name = "ItemRequiredClass";
            // 
            // ItemSet
            // 
            ItemSet.DataPropertyName = "ItemSet";
            ItemSet.HeaderText = "套装设置";
            ItemSet.Name = "ItemSet";
            // 
            // ItemRandomStatsId
            // 
            ItemRandomStatsId.DataPropertyName = "ItemRandomStatsId";
            ItemRandomStatsId.HeaderText = "随机附加";
            ItemRandomStatsId.Name = "ItemRandomStatsId";
            // 
            // ItemRequiredAmount
            // 
            ItemRequiredAmount.DataPropertyName = "ItemRequiredAmount";
            ItemRequiredAmount.HeaderText = "需求等级";
            ItemRequiredAmount.Name = "ItemRequiredAmount";
            // 
            // ItemImage
            // 
            ItemImage.DataPropertyName = "ItemImage";
            ItemImage.HeaderText = "内观";
            ItemImage.Name = "ItemImage";
            // 
            // ItemShape
            // 
            ItemShape.DataPropertyName = "ItemShape";
            ItemShape.HeaderText = "外观";
            ItemShape.Name = "ItemShape";
            // 
            // ItemEffect
            // 
            ItemEffect.DataPropertyName = "ItemEffect";
            ItemEffect.HeaderText = "特效";
            ItemEffect.Name = "ItemEffect";
            // 
            // ItemStackSize
            // 
            ItemStackSize.DataPropertyName = "ItemStackSize";
            ItemStackSize.HeaderText = "叠加数量";
            ItemStackSize.Name = "ItemStackSize";
            // 
            // ItemSlots
            // 
            ItemSlots.DataPropertyName = "ItemSlots";
            ItemSlots.HeaderText = "镶嵌孔位";
            ItemSlots.Name = "ItemSlots";
            // 
            // ItemWeight
            // 
            ItemWeight.DataPropertyName = "ItemWeight";
            ItemWeight.HeaderText = "重量";
            ItemWeight.Name = "ItemWeight";
            // 
            // ItemLightRange
            // 
            ItemLightRange.DataPropertyName = "ItemLightRange";
            ItemLightRange.HeaderText = "发光范围";
            ItemLightRange.Name = "ItemLightRange";
            // 
            // ItemLightIntensity
            // 
            ItemLightIntensity.DataPropertyName = "ItemLightIntensity";
            ItemLightIntensity.HeaderText = "发光强度";
            ItemLightIntensity.Name = "ItemLightIntensity";
            // 
            // ItemDurability
            // 
            ItemDurability.DataPropertyName = "ItemDurability";
            ItemDurability.HeaderText = "持久度";
            ItemDurability.Name = "ItemDurability";
            // 
            // ItemPrice
            // 
            ItemPrice.DataPropertyName = "ItemPrice";
            ItemPrice.HeaderText = "商店价格";
            ItemPrice.Name = "ItemPrice";
            // 
            // ItemToolTip
            // 
            ItemToolTip.DataPropertyName = "ItemToolTip";
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            ItemToolTip.DefaultCellStyle = dataGridViewCellStyle2;
            ItemToolTip.HeaderText = "物品说明";
            ItemToolTip.Name = "ItemToolTip";
            ItemToolTip.Width = 68;
            // 
            // StartItem
            // 
            StartItem.DataPropertyName = "StartItem";
            StartItem.HeaderText = "新手物品";
            StartItem.Name = "StartItem";
            StartItem.Resizable = DataGridViewTriState.True;
            // 
            // NeedIdentify
            // 
            NeedIdentify.DataPropertyName = "NeedIdentify";
            NeedIdentify.HeaderText = "需要鉴定";
            NeedIdentify.Name = "NeedIdentify";
            // 
            // ShowGroupPickup
            // 
            ShowGroupPickup.DataPropertyName = "ShowGroupPickup";
            ShowGroupPickup.HeaderText = "组队收集";
            ShowGroupPickup.Name = "ShowGroupPickup";
            // 
            // GlobalDropNotify
            // 
            GlobalDropNotify.DataPropertyName = "GlobalDropNotify";
            GlobalDropNotify.HeaderText = "掉落通告";
            GlobalDropNotify.Name = "GlobalDropNotify";
            // 
            // ClassBased
            // 
            ClassBased.DataPropertyName = "ClassBased";
            ClassBased.HeaderText = "基于职业";
            ClassBased.Name = "ClassBased";
            // 
            // LevelBased
            // 
            LevelBased.DataPropertyName = "LevelBased";
            LevelBased.HeaderText = "基本等级";
            LevelBased.Name = "LevelBased";
            // 
            // CanMine
            // 
            CanMine.DataPropertyName = "CanMine";
            CanMine.HeaderText = "允许挖矿";
            CanMine.Name = "CanMine";
            // 
            // CanFastRun
            // 
            CanFastRun.DataPropertyName = "CanFastRun";
            CanFastRun.HeaderText = "允许助跑";
            CanFastRun.Name = "CanFastRun";
            // 
            // CanAwakening
            // 
            CanAwakening.DataPropertyName = "CanAwakening";
            CanAwakening.HeaderText = "允许觉醒";
            CanAwakening.Name = "CanAwakening";
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1115, 61);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(Gameshop_button);
            panel3.Controls.Add(btnExport);
            panel3.Controls.Add(btnImport);
            panel3.Controls.Add(lblFilterType);
            panel3.Controls.Add(groupView);
            panel3.Controls.Add(drpFilterType);
            panel3.Controls.Add(lblSearch);
            panel3.Controls.Add(txtSearch);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(1115, 61);
            panel3.TabIndex = 5;
            // 
            // Gameshop_button
            // 
            Gameshop_button.Location = new Point(943, 28);
            Gameshop_button.Margin = new Padding(4, 3, 4, 3);
            Gameshop_button.Name = "Gameshop_button";
            Gameshop_button.Size = new Size(107, 31);
            Gameshop_button.TabIndex = 30;
            Gameshop_button.Text = "添加到游戏商城";
            Gameshop_button.UseVisualStyleBackColor = true;
            Gameshop_button.Click += Gameshop_button_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(853, 28);
            btnExport.Margin = new Padding(4, 3, 4, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(88, 31);
            btnExport.TabIndex = 6;
            btnExport.Text = "导出";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(764, 28);
            btnImport.Margin = new Padding(4, 3, 4, 3);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(88, 31);
            btnImport.TabIndex = 5;
            btnImport.Text = "导入";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // lblFilterType
            // 
            lblFilterType.AutoSize = true;
            lblFilterType.Location = new Point(444, 11);
            lblFilterType.Margin = new Padding(4, 0, 4, 0);
            lblFilterType.Name = "lblFilterType";
            lblFilterType.Size = new Size(39, 17);
            lblFilterType.TabIndex = 3;
            lblFilterType.Text = "类型 :";
            // 
            // groupView
            // 
            groupView.Controls.Add(rBtnViewSpecial);
            groupView.Controls.Add(rbtnViewAll);
            groupView.Controls.Add(rbtnViewBinding);
            groupView.Controls.Add(rbtnViewBasic);
            groupView.Controls.Add(rbtnViewStats);
            groupView.Location = new Point(4, 3);
            groupView.Margin = new Padding(4, 3, 4, 3);
            groupView.Name = "groupView";
            groupView.Padding = new Padding(4, 3, 4, 3);
            groupView.Size = new Size(428, 53);
            groupView.TabIndex = 4;
            groupView.TabStop = false;
            groupView.Text = "查看模式";
            // 
            // rBtnViewSpecial
            // 
            rBtnViewSpecial.AutoSize = true;
            rBtnViewSpecial.Location = new Point(342, 26);
            rBtnViewSpecial.Margin = new Padding(4, 3, 4, 3);
            rBtnViewSpecial.Name = "rBtnViewSpecial";
            rBtnViewSpecial.Size = new Size(74, 21);
            rBtnViewSpecial.TabIndex = 4;
            rBtnViewSpecial.TabStop = true;
            rBtnViewSpecial.Text = "特殊属性";
            rBtnViewSpecial.UseVisualStyleBackColor = true;
            rBtnViewSpecial.CheckedChanged += rBtnViewSpecial_CheckedChanged;
            // 
            // rbtnViewAll
            // 
            rbtnViewAll.AutoSize = true;
            rbtnViewAll.Checked = true;
            rbtnViewAll.Location = new Point(11, 25);
            rbtnViewAll.Margin = new Padding(4, 3, 4, 3);
            rbtnViewAll.Name = "rbtnViewAll";
            rbtnViewAll.Size = new Size(74, 21);
            rbtnViewAll.TabIndex = 0;
            rbtnViewAll.TabStop = true;
            rbtnViewAll.Text = "所有属性";
            rbtnViewAll.UseVisualStyleBackColor = true;
            rbtnViewAll.CheckedChanged += rbtnViewAll_CheckedChanged;
            // 
            // rbtnViewBinding
            // 
            rbtnViewBinding.AutoSize = true;
            rbtnViewBinding.Location = new Point(261, 25);
            rbtnViewBinding.Margin = new Padding(4, 3, 4, 3);
            rbtnViewBinding.Name = "rbtnViewBinding";
            rbtnViewBinding.Size = new Size(74, 21);
            rbtnViewBinding.TabIndex = 3;
            rbtnViewBinding.TabStop = true;
            rbtnViewBinding.Text = "绑定属性";
            rbtnViewBinding.UseVisualStyleBackColor = true;
            rbtnViewBinding.CheckedChanged += rbtnViewBinding_CheckedChanged;
            // 
            // rbtnViewBasic
            // 
            rbtnViewBasic.AutoSize = true;
            rbtnViewBasic.Location = new Point(95, 25);
            rbtnViewBasic.Margin = new Padding(4, 3, 4, 3);
            rbtnViewBasic.Name = "rbtnViewBasic";
            rbtnViewBasic.Size = new Size(74, 21);
            rbtnViewBasic.TabIndex = 1;
            rbtnViewBasic.TabStop = true;
            rbtnViewBasic.Text = "常规属性";
            rbtnViewBasic.UseVisualStyleBackColor = true;
            rbtnViewBasic.CheckedChanged += rbtnViewBasic_CheckedChanged;
            // 
            // rbtnViewStats
            // 
            rbtnViewStats.AutoSize = true;
            rbtnViewStats.Location = new Point(178, 25);
            rbtnViewStats.Margin = new Padding(4, 3, 4, 3);
            rbtnViewStats.Name = "rbtnViewStats";
            rbtnViewStats.Size = new Size(74, 21);
            rbtnViewStats.TabIndex = 2;
            rbtnViewStats.TabStop = true;
            rbtnViewStats.Text = "基本属性";
            rbtnViewStats.UseVisualStyleBackColor = true;
            rbtnViewStats.CheckedChanged += rbtnViewStats_CheckedChanged;
            // 
            // drpFilterType
            // 
            drpFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
            drpFilterType.FormattingEnabled = true;
            drpFilterType.Location = new Point(440, 32);
            drpFilterType.Margin = new Padding(4, 3, 4, 3);
            drpFilterType.Name = "drpFilterType";
            drpFilterType.Size = new Size(140, 25);
            drpFilterType.TabIndex = 2;
            drpFilterType.SelectedIndexChanged += drpFilterType_SelectedIndexChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(590, 11);
            lblSearch.Margin = new Padding(4, 0, 4, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(35, 17);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "搜索:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(587, 33);
            txtSearch.Margin = new Padding(4, 3, 4, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(164, 23);
            txtSearch.TabIndex = 0;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // panel2
            // 
            panel2.Controls.Add(itemInfoGridView);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 61);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1115, 567);
            panel2.TabIndex = 2;
            // 
            // ItemInfoFormNew
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1115, 628);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ItemInfoFormNew";
            Text = "物品信息";
            FormClosing += ItemInfoFormNew_FormClosing;
            ((System.ComponentModel.ISupportInitialize)itemInfoGridView).EndInit();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            groupView.ResumeLayout(false);
            groupView.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView itemInfoGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupView;
        private System.Windows.Forms.RadioButton rbtnViewAll;
        private System.Windows.Forms.RadioButton rbtnViewBinding;
        private System.Windows.Forms.RadioButton rbtnViewBasic;
        private System.Windows.Forms.RadioButton rbtnViewStats;
        private System.Windows.Forms.RadioButton rBtnViewSpecial;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFilterType;
        private System.Windows.Forms.ComboBox drpFilterType;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button Gameshop_button;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Modified;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemType;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemGrade;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemRequiredType;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemRequiredGender;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemRequiredClass;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemRandomStatsId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemRequiredAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemShape;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemEffect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemStackSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemSlots;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemLightRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemLightIntensity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDurability;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemToolTip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn StartItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NeedIdentify;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ShowGroupPickup;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GlobalDropNotify;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ClassBased;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LevelBased;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CanMine;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CanFastRun;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CanAwakening;
    }
}