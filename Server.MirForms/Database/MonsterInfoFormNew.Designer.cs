
namespace Server.Database
{
    partial class MonsterInfoFormNew
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            monsterInfoGridView = new DataGridView();
            Modified = new DataGridViewCheckBoxColumn();
            MonsterIndex = new DataGridViewTextBoxColumn();
            MonsterName = new DataGridViewTextBoxColumn();
            MonsterImage = new DataGridViewComboBoxColumn();
            MonsterAI = new DataGridViewTextBoxColumn();
            MonsterEffect = new DataGridViewTextBoxColumn();
            MonsterLevel = new DataGridViewTextBoxColumn();
            MonsterLight = new DataGridViewTextBoxColumn();
            MonsterAttackSpeed = new DataGridViewTextBoxColumn();
            MonsterMoveSpeed = new DataGridViewTextBoxColumn();
            MonsterViewRange = new DataGridViewTextBoxColumn();
            MonsterCoolEye = new DataGridViewTextBoxColumn();
            MonsterExperience = new DataGridViewTextBoxColumn();
            MonsterCanPush = new DataGridViewCheckBoxColumn();
            MonsterAutoRev = new DataGridViewCheckBoxColumn();
            MonsterUndead = new DataGridViewCheckBoxColumn();
            MonsterCanTame = new DataGridViewCheckBoxColumn();
            MonsterIsBoss = new DataGridViewCheckBoxColumn();
            MonsterRecall = new DataGridViewCheckBoxColumn();
            MonsterDropPath = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            panel3 = new Panel();
            btnExport = new Button();
            btnImport = new Button();
            groupView = new GroupBox();
            rbtnViewAll = new RadioButton();
            rbtnViewBasic = new RadioButton();
            txtSearch = new TextBox();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)monsterInfoGridView).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            groupView.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // monsterInfoGridView
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            monsterInfoGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            monsterInfoGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            monsterInfoGridView.Columns.AddRange(new DataGridViewColumn[] { Modified, MonsterIndex, MonsterName, MonsterImage, MonsterAI, MonsterEffect, MonsterLevel, MonsterLight, MonsterAttackSpeed, MonsterMoveSpeed, MonsterViewRange, MonsterCoolEye, MonsterExperience, MonsterCanPush, MonsterAutoRev, MonsterUndead, MonsterCanTame, MonsterIsBoss, MonsterRecall, MonsterDropPath });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            monsterInfoGridView.DefaultCellStyle = dataGridViewCellStyle2;
            monsterInfoGridView.Dock = DockStyle.Fill;
            monsterInfoGridView.Location = new Point(0, 0);
            monsterInfoGridView.Margin = new Padding(4, 3, 4, 3);
            monsterInfoGridView.Name = "monsterInfoGridView";
            monsterInfoGridView.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            monsterInfoGridView.Size = new Size(1037, 567);
            monsterInfoGridView.TabIndex = 0;
            monsterInfoGridView.CellValidating += monsterInfoGridView_CellValidating;
            monsterInfoGridView.DataError += monsterInfoGridView_DataError;
            monsterInfoGridView.DefaultValuesNeeded += monsterInfoGridView_DefaultValuesNeeded;
            monsterInfoGridView.UserDeletingRow += monsterInfoGridView_UserDeletingRow;
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
            // MonsterIndex
            // 
            MonsterIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            MonsterIndex.DataPropertyName = "MonsterIndex";
            MonsterIndex.Frozen = true;
            MonsterIndex.HeaderText = "编号";
            MonsterIndex.Name = "MonsterIndex";
            MonsterIndex.ReadOnly = true;
            MonsterIndex.Width = 57;
            // 
            // MonsterName
            // 
            MonsterName.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            MonsterName.DataPropertyName = "MonsterName";
            MonsterName.Frozen = true;
            MonsterName.HeaderText = "怪物名称";
            MonsterName.Name = "MonsterName";
            MonsterName.Width = 81;
            // 
            // MonsterImage
            // 
            MonsterImage.DataPropertyName = "MonsterImage";
            MonsterImage.HeaderText = "数据编号";
            MonsterImage.Name = "MonsterImage";
            MonsterImage.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MonsterAI
            // 
            MonsterAI.DataPropertyName = "MonsterAI";
            MonsterAI.HeaderText = "AI";
            MonsterAI.Name = "MonsterAI";
            // 
            // MonsterEffect
            // 
            MonsterEffect.DataPropertyName = "MonsterEffect";
            MonsterEffect.HeaderText = "特效编号";
            MonsterEffect.Name = "MonsterEffect";
            // 
            // MonsterLevel
            // 
            MonsterLevel.DataPropertyName = "MonsterLevel";
            MonsterLevel.HeaderText = "怪物等级";
            MonsterLevel.Name = "MonsterLevel";
            // 
            // MonsterLight
            // 
            MonsterLight.DataPropertyName = "MonsterLight";
            MonsterLight.HeaderText = "光亮度";
            MonsterLight.Name = "MonsterLight";
            // 
            // MonsterAttackSpeed
            // 
            MonsterAttackSpeed.DataPropertyName = "MonsterAttackSpeed";
            MonsterAttackSpeed.HeaderText = "攻击速度";
            MonsterAttackSpeed.Name = "MonsterAttackSpeed";
            // 
            // MonsterMoveSpeed
            // 
            MonsterMoveSpeed.DataPropertyName = "MonsterMoveSpeed";
            MonsterMoveSpeed.HeaderText = "移动速度";
            MonsterMoveSpeed.Name = "MonsterMoveSpeed";
            // 
            // MonsterViewRange
            // 
            MonsterViewRange.DataPropertyName = "MonsterViewRange";
            MonsterViewRange.HeaderText = "视觉范围";
            MonsterViewRange.Name = "MonsterViewRange";
            // 
            // MonsterCoolEye
            // 
            MonsterCoolEye.DataPropertyName = "MonsterCoolEye";
            MonsterCoolEye.HeaderText = "是否反隐";
            MonsterCoolEye.Name = "MonsterCoolEye";
            // 
            // MonsterExperience
            // 
            MonsterExperience.DataPropertyName = "MonsterExperience";
            MonsterExperience.HeaderText = "怪物经验";
            MonsterExperience.Name = "MonsterExperience";
            // 
            // MonsterCanPush
            // 
            MonsterCanPush.DataPropertyName = "MonsterCanPush";
            MonsterCanPush.HeaderText = "可推动";
            MonsterCanPush.Name = "MonsterCanPush";
            MonsterCanPush.Resizable = DataGridViewTriState.True;
            MonsterCanPush.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MonsterAutoRev
            // 
            MonsterAutoRev.DataPropertyName = "MonsterAutoRev";
            MonsterAutoRev.HeaderText = "非卫士";
            MonsterAutoRev.Name = "MonsterAutoRev";
            MonsterAutoRev.Resizable = DataGridViewTriState.True;
            MonsterAutoRev.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MonsterUndead
            // 
            MonsterUndead.DataPropertyName = "MonsterUndead";
            MonsterUndead.HeaderText = "亡灵类";
            MonsterUndead.Name = "MonsterUndead";
            MonsterUndead.Resizable = DataGridViewTriState.True;
            MonsterUndead.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MonsterCanTame
            // 
            MonsterCanTame.DataPropertyName = "MonsterCanTame";
            MonsterCanTame.HeaderText = "可诱惑";
            MonsterCanTame.Name = "MonsterCanTame";
            MonsterCanTame.Resizable = DataGridViewTriState.True;
            MonsterCanTame.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MonsterIsBoss
            // 
            MonsterIsBoss.DataPropertyName = "MonsterIsBoss";
            MonsterIsBoss.HeaderText = "Boss诱惑";
            MonsterIsBoss.Name = "MonsterIsBoss";
            MonsterIsBoss.Resizable = DataGridViewTriState.True;
            MonsterIsBoss.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MonsterRecall
            // 
            MonsterRecall.DataPropertyName = "MonsterRecall";
            MonsterRecall.HeaderText = "可召回";
            MonsterRecall.Name = "MonsterRecall";
            MonsterRecall.Resizable = DataGridViewTriState.True;
            MonsterRecall.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MonsterDropPath
            // 
            MonsterDropPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            MonsterDropPath.DataPropertyName = "MonsterDropPath";
            MonsterDropPath.HeaderText = "爆率文件路径";
            MonsterDropPath.Name = "MonsterDropPath";
            MonsterDropPath.Width = 105;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1037, 61);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnExport);
            panel3.Controls.Add(btnImport);
            panel3.Controls.Add(groupView);
            panel3.Controls.Add(txtSearch);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new Size(1037, 61);
            panel3.TabIndex = 5;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(808, 17);
            btnExport.Margin = new Padding(4);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(88, 30);
            btnExport.TabIndex = 6;
            btnExport.Text = "导出";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(714, 17);
            btnImport.Margin = new Padding(4);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(88, 30);
            btnImport.TabIndex = 5;
            btnImport.Text = "导入";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // groupView
            // 
            groupView.Controls.Add(rbtnViewAll);
            groupView.Controls.Add(rbtnViewBasic);
            groupView.Enabled = false;
            groupView.Location = new Point(4, 4);
            groupView.Margin = new Padding(4);
            groupView.Name = "groupView";
            groupView.Padding = new Padding(4);
            groupView.Size = new Size(239, 55);
            groupView.TabIndex = 4;
            groupView.TabStop = false;
            groupView.Text = "查看模式";
            // 
            // rbtnViewAll
            // 
            rbtnViewAll.AutoSize = true;
            rbtnViewAll.Checked = true;
            rbtnViewAll.Location = new Point(26, 27);
            rbtnViewAll.Margin = new Padding(4);
            rbtnViewAll.Name = "rbtnViewAll";
            rbtnViewAll.Size = new Size(74, 21);
            rbtnViewAll.TabIndex = 0;
            rbtnViewAll.TabStop = true;
            rbtnViewAll.Text = "所有属性";
            rbtnViewAll.UseVisualStyleBackColor = true;
            rbtnViewAll.CheckedChanged += rbtnViewAll_CheckedChanged;
            // 
            // rbtnViewBasic
            // 
            rbtnViewBasic.AutoSize = true;
            rbtnViewBasic.Location = new Point(114, 27);
            rbtnViewBasic.Margin = new Padding(4);
            rbtnViewBasic.Name = "rbtnViewBasic";
            rbtnViewBasic.Size = new Size(74, 21);
            rbtnViewBasic.TabIndex = 1;
            rbtnViewBasic.Text = "基本属性";
            rbtnViewBasic.UseVisualStyleBackColor = true;
            rbtnViewBasic.CheckedChanged += rbtnViewBasic_CheckedChanged;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(542, 21);
            txtSearch.Margin = new Padding(4, 3, 4, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "搜索";
            txtSearch.Size = new Size(164, 23);
            txtSearch.TabIndex = 0;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // panel2
            // 
            panel2.Controls.Add(monsterInfoGridView);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 61);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1037, 567);
            panel2.TabIndex = 2;
            // 
            // MonsterInfoFormNew
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1037, 628);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "MonsterInfoFormNew";
            Text = "怪物信息编辑";
            FormClosed += monsterInfoFormNew_FormClosed;
            ((System.ComponentModel.ISupportInitialize)monsterInfoGridView).EndInit();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            groupView.ResumeLayout(false);
            groupView.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView monsterInfoGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupView;
        private System.Windows.Forms.RadioButton rbtnViewAll;
        private System.Windows.Forms.RadioButton rbtnViewBasic;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Modified;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterName;
        private System.Windows.Forms.DataGridViewComboBoxColumn MonsterImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterAI;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterEffect;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterLight;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterAttackSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterMoveSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterViewRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterCoolEye;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterExperience;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MonsterCanPush;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MonsterAutoRev;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MonsterUndead;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MonsterCanTame;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MonsterIsBoss;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MonsterRecall;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterDropPath;
    }
}