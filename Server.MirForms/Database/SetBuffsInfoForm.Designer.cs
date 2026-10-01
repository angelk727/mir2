namespace Server.Database
{
    partial class SetBuffsInfoForm
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
            lstBuffs = new ListBox();
            lblBuffName = new Label();
            dgvStats = new DataGridView();
            colStat = new DataGridViewTextBoxColumn();
            colValue = new DataGridViewTextBoxColumn();
            cmbStat = new ComboBox();
            btnAddStat = new Button();
            btnDeleteStat = new Button();
            btnAddBuff = new Button();
            btnDeleteBuff = new Button();
            btnSave = new Button();
            btnClose = new Button();
            btnImport = new Button();
            btnExport = new Button();
            cmbBuffType = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvStats).BeginInit();
            SuspendLayout();
            // 
            // lstBuffs
            // 
            lstBuffs.FormattingEnabled = true;
            lstBuffs.HorizontalScrollbar = true;
            lstBuffs.ItemHeight = 17;
            lstBuffs.Location = new Point(10, 10);
            lstBuffs.Name = "lstBuffs";
            lstBuffs.Size = new Size(200, 327);
            lstBuffs.TabIndex = 0;
            lstBuffs.SelectedIndexChanged += lstBuffs_SelectedIndexChanged;
            // 
            // lblBuffName
            // 
            lblBuffName.AutoSize = true;
            lblBuffName.Location = new Point(248, 25);
            lblBuffName.Name = "lblBuffName";
            lblBuffName.Size = new Size(55, 17);
            lblBuffName.TabIndex = 1;
            lblBuffName.Text = "Buff名称";
            // 
            // dgvStats
            // 
            dgvStats.AllowUserToAddRows = false;
            dgvStats.AllowUserToDeleteRows = false;
            dgvStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStats.Columns.AddRange(new DataGridViewColumn[] { colStat, colValue });
            dgvStats.Location = new Point(225, 63);
            dgvStats.MultiSelect = false;
            dgvStats.Name = "dgvStats";
            dgvStats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStats.Size = new Size(563, 210);
            dgvStats.TabIndex = 3;
            dgvStats.CellValueChanged += dgvStats_CellValueChanged;
            // 
            // colStat
            // 
            colStat.HeaderText = "属性";
            colStat.Name = "colStat";
            // 
            // colValue
            // 
            colValue.HeaderText = "数值";
            colValue.Name = "colValue";
            // 
            // cmbStat
            // 
            cmbStat.FormattingEnabled = true;
            cmbStat.Location = new Point(225, 299);
            cmbStat.Name = "cmbStat";
            cmbStat.Size = new Size(200, 25);
            cmbStat.TabIndex = 4;
            // 
            // btnAddStat
            // 
            btnAddStat.Location = new Point(431, 299);
            btnAddStat.Name = "btnAddStat";
            btnAddStat.Size = new Size(75, 23);
            btnAddStat.TabIndex = 5;
            btnAddStat.Text = "添加属性";
            btnAddStat.UseVisualStyleBackColor = true;
            btnAddStat.Click += btnAddStat_Click;
            // 
            // btnDeleteStat
            // 
            btnDeleteStat.Location = new Point(512, 299);
            btnDeleteStat.Name = "btnDeleteStat";
            btnDeleteStat.Size = new Size(75, 23);
            btnDeleteStat.TabIndex = 6;
            btnDeleteStat.Text = "删除属性";
            btnDeleteStat.UseVisualStyleBackColor = true;
            btnDeleteStat.Click += btnDeleteStat_Click;
            // 
            // btnAddBuff
            // 
            btnAddBuff.Location = new Point(479, 22);
            btnAddBuff.Name = "btnAddBuff";
            btnAddBuff.Size = new Size(75, 23);
            btnAddBuff.TabIndex = 7;
            btnAddBuff.Text = "新增 Buff";
            btnAddBuff.UseVisualStyleBackColor = true;
            btnAddBuff.Click += btnAddBuff_Click;
            // 
            // btnDeleteBuff
            // 
            btnDeleteBuff.Location = new Point(58, 355);
            btnDeleteBuff.Name = "btnDeleteBuff";
            btnDeleteBuff.Size = new Size(75, 23);
            btnDeleteBuff.TabIndex = 8;
            btnDeleteBuff.Text = "删除 Buff";
            btnDeleteBuff.UseVisualStyleBackColor = true;
            btnDeleteBuff.Click += btnDeleteBuff_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(632, 355);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 9;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(713, 355);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 10;
            btnClose.Text = "关闭";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(623, 25);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(75, 23);
            btnImport.TabIndex = 11;
            btnImport.Text = "导入 TXT";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(704, 25);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 12;
            btnExport.Text = "导出 TXT";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // cmbBuffType
            // 
            cmbBuffType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBuffType.FormattingEnabled = true;
            cmbBuffType.Location = new Point(306, 21);
            cmbBuffType.Name = "cmbBuffType";
            cmbBuffType.Size = new Size(167, 25);
            cmbBuffType.TabIndex = 13;
            // 
            // SetBuffsInfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 401);
            Controls.Add(cmbBuffType);
            Controls.Add(btnExport);
            Controls.Add(btnImport);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(btnDeleteBuff);
            Controls.Add(btnAddBuff);
            Controls.Add(btnDeleteStat);
            Controls.Add(btnAddStat);
            Controls.Add(cmbStat);
            Controls.Add(dgvStats);
            Controls.Add(lblBuffName);
            Controls.Add(lstBuffs);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SetBuffsInfoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Buffs设置窗口";
            ((System.ComponentModel.ISupportInitialize)dgvStats).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstBuffs;
        private Label lblBuffName;
        private DataGridView dgvStats;
        private DataGridViewTextBoxColumn colStat;
        private DataGridViewTextBoxColumn colValue;
        private ComboBox cmbStat;
        private Button btnAddStat;
        private Button btnDeleteStat;
        private Button btnAddBuff;
        private Button btnDeleteBuff;
        private Button btnSave;
        private Button btnClose;
        private Button btnImport;
        private Button btnExport;
        private ComboBox cmbBuffType;
    }
}