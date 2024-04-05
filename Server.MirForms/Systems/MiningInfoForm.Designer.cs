namespace Server
{
    partial class MiningInfoForm
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
            groupBox7 = new GroupBox();
            label79 = new Label();
            MineNametextBox = new TextBox();
            MineSlotstextBox = new TextBox();
            label70 = new Label();
            label69 = new Label();
            label68 = new Label();
            MineDropRatetextBox = new TextBox();
            MineHitRatetextBox = new TextBox();
            MineAttemptstextBox = new TextBox();
            MineRegenDelaytextBox = new TextBox();
            label67 = new Label();
            label66 = new Label();
            label65 = new Label();
            tabPage2 = new TabPage();
            label78 = new Label();
            MineDropsIndexcomboBox = new ComboBox();
            groupBox8 = new GroupBox();
            MineMaxBonustextBox = new TextBox();
            label77 = new Label();
            MineBonusChancetextBox = new TextBox();
            label76 = new Label();
            MineMaxQualitytextBox = new TextBox();
            label75 = new Label();
            MineMinQualitytextBox = new TextBox();
            label74 = new Label();
            MineMaxSlottextBox = new TextBox();
            label73 = new Label();
            MineMinSlottextBox = new TextBox();
            label72 = new Label();
            MineItemNametextBox = new TextBox();
            label71 = new Label();
            MineRemoveDropbutton = new Button();
            MineAddDropbutton = new Button();
            MineRemoveIndexbutton = new Button();
            MineAddIndexbutton = new Button();
            MineIndexcomboBox = new ComboBox();
            label64 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox7.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox8.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 33);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(427, 263);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox7);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(419, 233);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "采矿设置";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label79);
            groupBox7.Controls.Add(MineNametextBox);
            groupBox7.Controls.Add(MineSlotstextBox);
            groupBox7.Controls.Add(label70);
            groupBox7.Controls.Add(label69);
            groupBox7.Controls.Add(label68);
            groupBox7.Controls.Add(MineDropRatetextBox);
            groupBox7.Controls.Add(MineHitRatetextBox);
            groupBox7.Controls.Add(MineAttemptstextBox);
            groupBox7.Controls.Add(MineRegenDelaytextBox);
            groupBox7.Controls.Add(label67);
            groupBox7.Controls.Add(label66);
            groupBox7.Controls.Add(label65);
            groupBox7.Location = new Point(10, 6);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(403, 121);
            groupBox7.TabIndex = 27;
            groupBox7.TabStop = false;
            groupBox7.Text = "矿石";
            // 
            // label79
            // 
            label79.AutoSize = true;
            label79.Location = new Point(16, 20);
            label79.Name = "label79";
            label79.Size = new Size(80, 17);
            label79.TabIndex = 23;
            label79.Text = "采矿方案名称";
            // 
            // MineNametextBox
            // 
            MineNametextBox.Location = new Point(97, 16);
            MineNametextBox.Name = "MineNametextBox";
            MineNametextBox.Size = new Size(100, 23);
            MineNametextBox.TabIndex = 22;
            MineNametextBox.TextChanged += MineNametextBox_TextChanged;
            // 
            // MineSlotstextBox
            // 
            MineSlotstextBox.Location = new Point(97, 92);
            MineSlotstextBox.Name = "MineSlotstextBox";
            MineSlotstextBox.Size = new Size(34, 23);
            MineSlotstextBox.TabIndex = 10;
            MineSlotstextBox.TextChanged += MineSlotstextBox_TextChanged;
            // 
            // label70
            // 
            label70.AutoSize = true;
            label70.Location = new Point(63, 96);
            label70.Name = "label70";
            label70.Size = new Size(32, 17);
            label70.TabIndex = 9;
            label70.Text = "总计";
            // 
            // label69
            // 
            label69.AutoSize = true;
            label69.Location = new Point(292, 45);
            label69.Name = "label69";
            label69.Size = new Size(68, 17);
            label69.TabIndex = 8;
            label69.Text = "矿石开采率";
            // 
            // label68
            // 
            label68.AutoSize = true;
            label68.Location = new Point(291, 20);
            label68.Name = "label68";
            label68.Size = new Size(68, 17);
            label68.TabIndex = 7;
            label68.Text = "采矿命中率";
            // 
            // MineDropRatetextBox
            // 
            MineDropRatetextBox.Location = new Point(362, 42);
            MineDropRatetextBox.Name = "MineDropRatetextBox";
            MineDropRatetextBox.Size = new Size(34, 23);
            MineDropRatetextBox.TabIndex = 6;
            MineDropRatetextBox.TextChanged += MineDropRatetextBox_TextChanged;
            // 
            // MineHitRatetextBox
            // 
            MineHitRatetextBox.Location = new Point(362, 16);
            MineHitRatetextBox.Name = "MineHitRatetextBox";
            MineHitRatetextBox.Size = new Size(34, 23);
            MineHitRatetextBox.TabIndex = 5;
            MineHitRatetextBox.TextChanged += MineHitRatetextBox_TextChanged;
            // 
            // MineAttemptstextBox
            // 
            MineAttemptstextBox.Location = new Point(97, 68);
            MineAttemptstextBox.Name = "MineAttemptstextBox";
            MineAttemptstextBox.Size = new Size(34, 23);
            MineAttemptstextBox.TabIndex = 4;
            MineAttemptstextBox.TextChanged += MineAttemptstextBox_TextChanged;
            // 
            // MineRegenDelaytextBox
            // 
            MineRegenDelaytextBox.Location = new Point(97, 42);
            MineRegenDelaytextBox.Name = "MineRegenDelaytextBox";
            MineRegenDelaytextBox.Size = new Size(34, 23);
            MineRegenDelaytextBox.TabIndex = 3;
            MineRegenDelaytextBox.TextChanged += MineRegenDelaytextBox_TextChanged;
            // 
            // label67
            // 
            label67.AutoSize = true;
            label67.Location = new Point(40, 71);
            label67.Name = "label67";
            label67.Size = new Size(56, 17);
            label67.TabIndex = 2;
            label67.Text = "矿物再生";
            // 
            // label66
            // 
            label66.AutoSize = true;
            label66.Location = new Point(133, 47);
            label66.Name = "label66";
            label66.Size = new Size(40, 17);
            label66.TabIndex = 1;
            label66.Text = "(分钟)";
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Location = new Point(14, 46);
            label65.Name = "label65";
            label65.Size = new Size(80, 17);
            label65.TabIndex = 0;
            label65.Text = "原地挖矿时长";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label78);
            tabPage2.Controls.Add(MineDropsIndexcomboBox);
            tabPage2.Controls.Add(groupBox8);
            tabPage2.Controls.Add(MineRemoveDropbutton);
            tabPage2.Controls.Add(MineAddDropbutton);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(419, 233);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "矿石开采率";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label78
            // 
            label78.AutoSize = true;
            label78.Location = new Point(7, 40);
            label78.Name = "label78";
            label78.Size = new Size(56, 17);
            label78.TabIndex = 26;
            label78.Text = "采矿几率";
            // 
            // MineDropsIndexcomboBox
            // 
            MineDropsIndexcomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            MineDropsIndexcomboBox.FormattingEnabled = true;
            MineDropsIndexcomboBox.Location = new Point(12, 12);
            MineDropsIndexcomboBox.Name = "MineDropsIndexcomboBox";
            MineDropsIndexcomboBox.Size = new Size(129, 25);
            MineDropsIndexcomboBox.TabIndex = 22;
            MineDropsIndexcomboBox.SelectedIndexChanged += MineDropsIndexcomboBox_SelectedIndexChanged;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(MineMaxBonustextBox);
            groupBox8.Controls.Add(label77);
            groupBox8.Controls.Add(MineBonusChancetextBox);
            groupBox8.Controls.Add(label76);
            groupBox8.Controls.Add(MineMaxQualitytextBox);
            groupBox8.Controls.Add(label75);
            groupBox8.Controls.Add(MineMinQualitytextBox);
            groupBox8.Controls.Add(label74);
            groupBox8.Controls.Add(MineMaxSlottextBox);
            groupBox8.Controls.Add(label73);
            groupBox8.Controls.Add(MineMinSlottextBox);
            groupBox8.Controls.Add(label72);
            groupBox8.Controls.Add(MineItemNametextBox);
            groupBox8.Controls.Add(label71);
            groupBox8.Location = new Point(10, 39);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(188, 188);
            groupBox8.TabIndex = 25;
            groupBox8.TabStop = false;
            // 
            // MineMaxBonustextBox
            // 
            MineMaxBonustextBox.Location = new Point(99, 153);
            MineMaxBonustextBox.Name = "MineMaxBonustextBox";
            MineMaxBonustextBox.Size = new Size(34, 23);
            MineMaxBonustextBox.TabIndex = 34;
            MineMaxBonustextBox.TextChanged += MineMaxBonustextBox_TextChanged;
            // 
            // label77
            // 
            label77.AutoSize = true;
            label77.Location = new Point(41, 156);
            label77.Name = "label77";
            label77.Size = new Size(56, 17);
            label77.TabIndex = 33;
            label77.Text = "意外收获";
            // 
            // MineBonusChancetextBox
            // 
            MineBonusChancetextBox.Location = new Point(99, 130);
            MineBonusChancetextBox.Name = "MineBonusChancetextBox";
            MineBonusChancetextBox.Size = new Size(34, 23);
            MineBonusChancetextBox.TabIndex = 32;
            MineBonusChancetextBox.TextChanged += MineBonusChancetextBox_TextChanged;
            // 
            // label76
            // 
            label76.AutoSize = true;
            label76.Location = new Point(41, 132);
            label76.Name = "label76";
            label76.Size = new Size(56, 17);
            label76.TabIndex = 31;
            label76.Text = "意外几率";
            // 
            // MineMaxQualitytextBox
            // 
            MineMaxQualitytextBox.Location = new Point(99, 106);
            MineMaxQualitytextBox.Name = "MineMaxQualitytextBox";
            MineMaxQualitytextBox.Size = new Size(34, 23);
            MineMaxQualitytextBox.TabIndex = 30;
            MineMaxQualitytextBox.TextChanged += MineMaxQualitytextBox_TextChanged;
            // 
            // label75
            // 
            label75.AutoSize = true;
            label75.Location = new Point(41, 110);
            label75.Name = "label75";
            label75.Size = new Size(56, 17);
            label75.TabIndex = 29;
            label75.Text = "最高品质";
            // 
            // MineMinQualitytextBox
            // 
            MineMinQualitytextBox.Location = new Point(99, 83);
            MineMinQualitytextBox.Name = "MineMinQualitytextBox";
            MineMinQualitytextBox.Size = new Size(34, 23);
            MineMinQualitytextBox.TabIndex = 28;
            MineMinQualitytextBox.TextChanged += MineMinQualitytextBox_TextChanged;
            // 
            // label74
            // 
            label74.AutoSize = true;
            label74.Location = new Point(41, 86);
            label74.Name = "label74";
            label74.Size = new Size(56, 17);
            label74.TabIndex = 27;
            label74.Text = "最低品质";
            // 
            // MineMaxSlottextBox
            // 
            MineMaxSlottextBox.Location = new Point(99, 60);
            MineMaxSlottextBox.Name = "MineMaxSlottextBox";
            MineMaxSlottextBox.Size = new Size(34, 23);
            MineMaxSlottextBox.TabIndex = 26;
            MineMaxSlottextBox.TextChanged += MineMaxSlottextBox_TextChanged;
            // 
            // label73
            // 
            label73.AutoSize = true;
            label73.Location = new Point(41, 63);
            label73.Name = "label73";
            label73.Size = new Size(56, 17);
            label73.TabIndex = 25;
            label73.Text = "最大几率";
            // 
            // MineMinSlottextBox
            // 
            MineMinSlottextBox.Location = new Point(99, 37);
            MineMinSlottextBox.Name = "MineMinSlottextBox";
            MineMinSlottextBox.Size = new Size(34, 23);
            MineMinSlottextBox.TabIndex = 24;
            MineMinSlottextBox.TextChanged += MineMinSlottextBox_TextChanged;
            // 
            // label72
            // 
            label72.AutoSize = true;
            label72.Location = new Point(41, 41);
            label72.Name = "label72";
            label72.Size = new Size(56, 17);
            label72.TabIndex = 23;
            label72.Text = "最小几率";
            // 
            // MineItemNametextBox
            // 
            MineItemNametextBox.Location = new Point(99, 13);
            MineItemNametextBox.Name = "MineItemNametextBox";
            MineItemNametextBox.Size = new Size(83, 23);
            MineItemNametextBox.TabIndex = 22;
            MineItemNametextBox.TextChanged += MineItemNametextBox_TextChanged;
            // 
            // label71
            // 
            label71.AutoSize = true;
            label71.Location = new Point(41, 17);
            label71.Name = "label71";
            label71.Size = new Size(56, 17);
            label71.TabIndex = 21;
            label71.Text = "矿石名称";
            // 
            // MineRemoveDropbutton
            // 
            MineRemoveDropbutton.Location = new Point(176, 12);
            MineRemoveDropbutton.Name = "MineRemoveDropbutton";
            MineRemoveDropbutton.Size = new Size(21, 21);
            MineRemoveDropbutton.TabIndex = 24;
            MineRemoveDropbutton.Text = "-";
            MineRemoveDropbutton.UseVisualStyleBackColor = true;
            MineRemoveDropbutton.Click += MineRemoveDropbutton_Click;
            // 
            // MineAddDropbutton
            // 
            MineAddDropbutton.Location = new Point(148, 12);
            MineAddDropbutton.Name = "MineAddDropbutton";
            MineAddDropbutton.Size = new Size(21, 21);
            MineAddDropbutton.TabIndex = 23;
            MineAddDropbutton.Text = "+";
            MineAddDropbutton.UseVisualStyleBackColor = true;
            MineAddDropbutton.Click += MineAddDropbutton_Click;
            // 
            // MineRemoveIndexbutton
            // 
            MineRemoveIndexbutton.Location = new Point(218, 6);
            MineRemoveIndexbutton.Name = "MineRemoveIndexbutton";
            MineRemoveIndexbutton.Size = new Size(21, 21);
            MineRemoveIndexbutton.TabIndex = 26;
            MineRemoveIndexbutton.Text = "-";
            MineRemoveIndexbutton.UseVisualStyleBackColor = true;
            MineRemoveIndexbutton.Click += MineRemoveIndexbutton_Click;
            // 
            // MineAddIndexbutton
            // 
            MineAddIndexbutton.Location = new Point(189, 6);
            MineAddIndexbutton.Name = "MineAddIndexbutton";
            MineAddIndexbutton.Size = new Size(21, 21);
            MineAddIndexbutton.TabIndex = 25;
            MineAddIndexbutton.Text = "+";
            MineAddIndexbutton.UseVisualStyleBackColor = true;
            MineAddIndexbutton.Click += MineAddIndexbutton_Click;
            // 
            // MineIndexcomboBox
            // 
            MineIndexcomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            MineIndexcomboBox.FormattingEnabled = true;
            MineIndexcomboBox.Location = new Point(91, 6);
            MineIndexcomboBox.Name = "MineIndexcomboBox";
            MineIndexcomboBox.Size = new Size(92, 25);
            MineIndexcomboBox.TabIndex = 23;
            MineIndexcomboBox.SelectedIndexChanged += MineIndexcomboBox_SelectedIndexChanged;
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Location = new Point(9, 10);
            label64.Name = "label64";
            label64.Size = new Size(80, 17);
            label64.TabIndex = 24;
            label64.Text = "选择矿石方案";
            // 
            // MiningInfoForm
            // 
            ClientSize = new Size(450, 305);
            Controls.Add(MineRemoveIndexbutton);
            Controls.Add(tabControl1);
            Controls.Add(MineAddIndexbutton);
            Controls.Add(label64);
            Controls.Add(MineIndexcomboBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MiningInfoForm";
            Text = "矿石信息列表";
            FormClosed += MiningInfoForm_FormClosed;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button MineRemoveIndexbutton;
        private System.Windows.Forms.Button MineAddIndexbutton;
        private System.Windows.Forms.ComboBox MineIndexcomboBox;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox MineNametextBox;
        private System.Windows.Forms.TextBox MineSlotstextBox;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.TextBox MineDropRatetextBox;
        private System.Windows.Forms.TextBox MineHitRatetextBox;
        private System.Windows.Forms.TextBox MineAttemptstextBox;
        private System.Windows.Forms.TextBox MineRegenDelaytextBox;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox MineDropsIndexcomboBox;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox MineMaxBonustextBox;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox MineBonusChancetextBox;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TextBox MineMaxQualitytextBox;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox MineMinQualitytextBox;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox MineMaxSlottextBox;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox MineMinSlottextBox;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox MineItemNametextBox;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Button MineRemoveDropbutton;
        private System.Windows.Forms.Button MineAddDropbutton;
    }
}