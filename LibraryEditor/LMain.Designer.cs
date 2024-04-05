using System;

namespace LibraryEditor
{
    partial class LMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LMain));
            MainMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            openReferenceFileToolStripMenuItem = new ToolStripMenuItem();
            openReferenceImageToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            functionsToolStripMenuItem = new ToolStripMenuItem();
            copyToToolStripMenuItem = new ToolStripMenuItem();
            countBlanksToolStripMenuItem = new ToolStripMenuItem();
            removeBlanksToolStripMenuItem = new ToolStripMenuItem();
            safeToolStripMenuItem = new ToolStripMenuItem();
            convertToolStripMenuItem = new ToolStripMenuItem();
            populateFramesToolStripMenuItem = new ToolStripMenuItem();
            defaultMonsterFramesToolStripMenuItem = new ToolStripMenuItem();
            defaultNPCFramesToolStripMenuItem = new ToolStripMenuItem();
            defaultPlayerFramesToolStripMenuItem = new ToolStripMenuItem();
            autofillFromCodeToolStripMenuItem = new ToolStripMenuItem();
            importShadowsToolStripMenuItem = new ToolStripMenuItem();
            skinToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            numericUpDownY = new NumericUpDown();
            numericUpDownX = new NumericUpDown();
            BulkButton = new Button();
            checkBox1 = new CheckBox();
            groupBox1 = new GroupBox();
            RButtonOverlay = new RadioButton();
            RButtonImage = new RadioButton();
            checkboxRemoveBlackOnImport = new CheckBox();
            nudJump = new NumericUpDown();
            checkBoxPreventAntiAliasing = new CheckBox();
            checkBoxQuality = new CheckBox();
            buttonSkipPrevious = new Button();
            buttonSkipNext = new Button();
            buttonReplace = new Button();
            pictureBox = new PictureBox();
            ZoomTrackBar = new TrackBar();
            ExportButton = new Button();
            InsertImageButton = new Button();
            DeleteButton = new Button();
            AddButton = new Button();
            label10 = new Label();
            label8 = new Label();
            HeightLabel = new Label();
            label6 = new Label();
            WidthLabel = new Label();
            label1 = new Label();
            panel = new Panel();
            ImageBox = new PictureBox();
            tabControl = new TabControl();
            tabImages = new TabPage();
            PreviewListView = new CustomFormControl.FixedListView();
            ImageList = new ImageList(components);
            tabFrames = new TabPage();
            frameGridView = new DataGridView();
            FrameAction = new DataGridViewComboBoxColumn();
            FrameStart = new DataGridViewTextBoxColumn();
            FrameCount = new DataGridViewTextBoxColumn();
            FrameSkip = new DataGridViewTextBoxColumn();
            FrameInterval = new DataGridViewTextBoxColumn();
            FrameEffectStart = new DataGridViewTextBoxColumn();
            FrameEffectCount = new DataGridViewTextBoxColumn();
            FrameEffectSkip = new DataGridViewTextBoxColumn();
            FrameEffectInterval = new DataGridViewTextBoxColumn();
            FrameReverse = new DataGridViewCheckBoxColumn();
            FrameBlend = new DataGridViewCheckBoxColumn();
            OpenLibraryDialog = new OpenFileDialog();
            SaveLibraryDialog = new SaveFileDialog();
            ImportImageDialog = new OpenFileDialog();
            OpenWeMadeDialog = new OpenFileDialog();
            toolTip = new ToolTip(components);
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            FolderLibraryDialog = new FolderBrowserDialog();
            FrameAnimTimer = new System.Windows.Forms.Timer(components);
            MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudJump).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZoomTrackBar).BeginInit();
            panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageBox).BeginInit();
            tabControl.SuspendLayout();
            tabImages.SuspendLayout();
            tabFrames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)frameGridView).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainMenu
            // 
            MainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, functionsToolStripMenuItem, skinToolStripMenuItem });
            MainMenu.Location = new Point(0, 0);
            MainMenu.Name = "MainMenu";
            MainMenu.Padding = new Padding(7, 2, 0, 2);
            MainMenu.RenderMode = ToolStripRenderMode.Professional;
            MainMenu.Size = new Size(1209, 25);
            MainMenu.TabIndex = 0;
            MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripMenuItem1, openReferenceFileToolStripMenuItem, openReferenceImageToolStripMenuItem, toolStripSeparator1, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripMenuItem2, closeToolStripMenuItem });
            fileToolStripMenuItem.Image = (Image)resources.GetObject("fileToolStripMenuItem.Image");
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(60, 21);
            fileToolStripMenuItem.Text = "文件";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(148, 22);
            newToolStripMenuItem.Text = "新建";
            newToolStripMenuItem.ToolTipText = "创建一个新的LIB文件";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(148, 22);
            openToolStripMenuItem.Text = "打开";
            openToolStripMenuItem.ToolTipText = "打开一个LIB文件";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(145, 6);
            // 
            // openReferenceFileToolStripMenuItem
            // 
            openReferenceFileToolStripMenuItem.Name = "openReferenceFileToolStripMenuItem";
            openReferenceFileToolStripMenuItem.Size = new Size(148, 22);
            openReferenceFileToolStripMenuItem.Text = "打开引用文件";
            openReferenceFileToolStripMenuItem.Click += openReferenceFileToolStripMenuItem_Click;
            // 
            // openReferenceImageToolStripMenuItem
            // 
            openReferenceImageToolStripMenuItem.Name = "openReferenceImageToolStripMenuItem";
            openReferenceImageToolStripMenuItem.Size = new Size(148, 22);
            openReferenceImageToolStripMenuItem.Text = "打开引用图像";
            openReferenceImageToolStripMenuItem.Click += openReferenceImageToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(145, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(148, 22);
            saveToolStripMenuItem.Text = "保存";
            saveToolStripMenuItem.ToolTipText = "保存修改到当前的LIB文件";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Image = (Image)resources.GetObject("saveAsToolStripMenuItem.Image");
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(148, 22);
            saveAsToolStripMenuItem.Text = "另存";
            saveAsToolStripMenuItem.ToolTipText = "另存到一个LIB文件";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(145, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Image = (Image)resources.GetObject("closeToolStripMenuItem.Image");
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(148, 22);
            closeToolStripMenuItem.Text = "关闭";
            closeToolStripMenuItem.ToolTipText = "退出当前编辑器";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // functionsToolStripMenuItem
            // 
            functionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { copyToToolStripMenuItem, countBlanksToolStripMenuItem, removeBlanksToolStripMenuItem, convertToolStripMenuItem, populateFramesToolStripMenuItem, importShadowsToolStripMenuItem });
            functionsToolStripMenuItem.Image = (Image)resources.GetObject("functionsToolStripMenuItem.Image");
            functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            functionsToolStripMenuItem.Size = new Size(60, 21);
            functionsToolStripMenuItem.Text = "功能";
            // 
            // copyToToolStripMenuItem
            // 
            copyToToolStripMenuItem.Image = (Image)resources.GetObject("copyToToolStripMenuItem.Image");
            copyToToolStripMenuItem.Name = "copyToToolStripMenuItem";
            copyToToolStripMenuItem.Size = new Size(130, 22);
            copyToToolStripMenuItem.Text = "所选图片复制到...";
            copyToToolStripMenuItem.ToolTipText = "复制到新的LIB文件或已有的LIB文件末尾";
            copyToToolStripMenuItem.Click += copyToToolStripMenuItem_Click;
            // 
            // countBlanksToolStripMenuItem
            // 
            countBlanksToolStripMenuItem.Image = (Image)resources.GetObject("countBlanksToolStripMenuItem.Image");
            countBlanksToolStripMenuItem.Name = "countBlanksToolStripMenuItem";
            countBlanksToolStripMenuItem.Size = new Size(130, 22);
            countBlanksToolStripMenuItem.Text = "空白计数";
            countBlanksToolStripMenuItem.ToolTipText = "对LIB文件中的空白图片计数";
            countBlanksToolStripMenuItem.Click += countBlanksToolStripMenuItem_Click;
            // 
            // removeBlanksToolStripMenuItem
            // 
            removeBlanksToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { safeToolStripMenuItem });
            removeBlanksToolStripMenuItem.Image = (Image)resources.GetObject("removeBlanksToolStripMenuItem.Image");
            removeBlanksToolStripMenuItem.Name = "removeBlanksToolStripMenuItem";
            removeBlanksToolStripMenuItem.Size = new Size(130, 22);
            removeBlanksToolStripMenuItem.Text = "删除空白";
            removeBlanksToolStripMenuItem.ToolTipText = "快速清除空白图片";
            removeBlanksToolStripMenuItem.Click += removeBlanksToolStripMenuItem_Click;
            // 
            // safeToolStripMenuItem
            // 
            safeToolStripMenuItem.Image = (Image)resources.GetObject("safeToolStripMenuItem.Image");
            safeToolStripMenuItem.Name = "safeToolStripMenuItem";
            safeToolStripMenuItem.Size = new Size(124, 22);
            safeToolStripMenuItem.Text = "安全删除";
            safeToolStripMenuItem.ToolTipText = "用安全方式删除空白图";
            safeToolStripMenuItem.Click += safeToolStripMenuItem_Click;
            // 
            // convertToolStripMenuItem
            // 
            convertToolStripMenuItem.Image = (Image)resources.GetObject("convertToolStripMenuItem.Image");
            convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            convertToolStripMenuItem.Size = new Size(130, 22);
            convertToolStripMenuItem.Text = "转换为LIB";
            convertToolStripMenuItem.ToolTipText = "转换 Wil/Wzl/Miz 到LIB文件";
            convertToolStripMenuItem.Click += convertToolStripMenuItem_Click;
            // 
            // populateFramesToolStripMenuItem
            // 
            populateFramesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { defaultMonsterFramesToolStripMenuItem, defaultNPCFramesToolStripMenuItem, defaultPlayerFramesToolStripMenuItem, autofillFromCodeToolStripMenuItem });
            populateFramesToolStripMenuItem.Image = (Image)resources.GetObject("populateFramesToolStripMenuItem.Image");
            populateFramesToolStripMenuItem.Name = "populateFramesToolStripMenuItem";
            populateFramesToolStripMenuItem.Size = new Size(130, 22);
            populateFramesToolStripMenuItem.Text = "动作填充";
            // 
            // defaultMonsterFramesToolStripMenuItem
            // 
            defaultMonsterFramesToolStripMenuItem.Image = (Image)resources.GetObject("defaultMonsterFramesToolStripMenuItem.Image");
            defaultMonsterFramesToolStripMenuItem.Name = "defaultMonsterFramesToolStripMenuItem";
            defaultMonsterFramesToolStripMenuItem.Size = new Size(149, 22);
            defaultMonsterFramesToolStripMenuItem.Text = "默认怪物动作";
            defaultMonsterFramesToolStripMenuItem.Click += defaultMonsterFramesToolStripMenuItem_Click;
            // 
            // defaultNPCFramesToolStripMenuItem
            // 
            defaultNPCFramesToolStripMenuItem.Image = (Image)resources.GetObject("defaultNPCFramesToolStripMenuItem.Image");
            defaultNPCFramesToolStripMenuItem.Name = "defaultNPCFramesToolStripMenuItem";
            defaultNPCFramesToolStripMenuItem.Size = new Size(149, 22);
            defaultNPCFramesToolStripMenuItem.Text = "默认NPC动作";
            defaultNPCFramesToolStripMenuItem.Click += defaultNPCFramesToolStripMenuItem_Click;
            // 
            // defaultPlayerFramesToolStripMenuItem
            // 
            defaultPlayerFramesToolStripMenuItem.Image = (Image)resources.GetObject("defaultPlayerFramesToolStripMenuItem.Image");
            defaultPlayerFramesToolStripMenuItem.Name = "defaultPlayerFramesToolStripMenuItem";
            defaultPlayerFramesToolStripMenuItem.Size = new Size(149, 22);
            defaultPlayerFramesToolStripMenuItem.Text = "清空现有动作";
            defaultPlayerFramesToolStripMenuItem.Click += defaultPlayerFramesToolStripMenuItem_Click;
            // 
            // autofillFromCodeToolStripMenuItem
            // 
            autofillFromCodeToolStripMenuItem.Image = (Image)resources.GetObject("autofillFromCodeToolStripMenuItem.Image");
            autofillFromCodeToolStripMenuItem.Name = "autofillFromCodeToolStripMenuItem";
            autofillFromCodeToolStripMenuItem.Size = new Size(149, 22);
            autofillFromCodeToolStripMenuItem.Text = "自动填充模式";
            autofillFromCodeToolStripMenuItem.Click += autofillNpcFramesToolStripMenuItem_Click;
            // 
            // importShadowsToolStripMenuItem
            // 
            importShadowsToolStripMenuItem.Name = "importShadowsToolStripMenuItem";
            importShadowsToolStripMenuItem.Size = new Size(130, 22);
            importShadowsToolStripMenuItem.Text = "导入阴影";
            importShadowsToolStripMenuItem.Click += importShadowsToolStripMenuItem_Click;
            // 
            // skinToolStripMenuItem
            // 
            skinToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            skinToolStripMenuItem.Image = (Image)resources.GetObject("skinToolStripMenuItem.Image");
            skinToolStripMenuItem.Name = "skinToolStripMenuItem";
            skinToolStripMenuItem.Size = new Size(60, 21);
            skinToolStripMenuItem.Text = "Skin";
            skinToolStripMenuItem.Visible = false;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1MinSize = 325;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl);
            splitContainer1.Size = new Size(1209, 903);
            splitContainer1.SplitterDistance = 514;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(4, 3, 4, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(numericUpDownY);
            splitContainer2.Panel1.Controls.Add(numericUpDownX);
            splitContainer2.Panel1.Controls.Add(BulkButton);
            splitContainer2.Panel1.Controls.Add(checkBox1);
            splitContainer2.Panel1.Controls.Add(groupBox1);
            splitContainer2.Panel1.Controls.Add(checkboxRemoveBlackOnImport);
            splitContainer2.Panel1.Controls.Add(nudJump);
            splitContainer2.Panel1.Controls.Add(checkBoxPreventAntiAliasing);
            splitContainer2.Panel1.Controls.Add(checkBoxQuality);
            splitContainer2.Panel1.Controls.Add(buttonSkipPrevious);
            splitContainer2.Panel1.Controls.Add(buttonSkipNext);
            splitContainer2.Panel1.Controls.Add(buttonReplace);
            splitContainer2.Panel1.Controls.Add(pictureBox);
            splitContainer2.Panel1.Controls.Add(ZoomTrackBar);
            splitContainer2.Panel1.Controls.Add(ExportButton);
            splitContainer2.Panel1.Controls.Add(InsertImageButton);
            splitContainer2.Panel1.Controls.Add(DeleteButton);
            splitContainer2.Panel1.Controls.Add(AddButton);
            splitContainer2.Panel1.Controls.Add(label10);
            splitContainer2.Panel1.Controls.Add(label8);
            splitContainer2.Panel1.Controls.Add(HeightLabel);
            splitContainer2.Panel1.Controls.Add(label6);
            splitContainer2.Panel1.Controls.Add(WidthLabel);
            splitContainer2.Panel1.Controls.Add(label1);
            splitContainer2.Panel1.ForeColor = Color.Black;
            splitContainer2.Panel1MinSize = 240;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(panel);
            splitContainer2.Size = new Size(1209, 514);
            splitContainer2.SplitterDistance = 240;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 0;
            // 
            // numericUpDownY
            // 
            numericUpDownY.Location = new Point(145, 89);
            numericUpDownY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownY.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDownY.Name = "numericUpDownY";
            numericUpDownY.Size = new Size(82, 23);
            numericUpDownY.TabIndex = 27;
            numericUpDownY.ValueChanged += numericUpDownY_ValueChanged;
            // 
            // numericUpDownX
            // 
            numericUpDownX.Location = new Point(145, 62);
            numericUpDownX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownX.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numericUpDownX.Name = "numericUpDownX";
            numericUpDownX.Size = new Size(82, 23);
            numericUpDownX.TabIndex = 26;
            numericUpDownX.ValueChanged += numericUpDownX_ValueChanged;
            // 
            // BulkButton
            // 
            BulkButton.Location = new Point(44, 116);
            BulkButton.Name = "BulkButton";
            BulkButton.Size = new Size(77, 26);
            BulkButton.TabIndex = 25;
            BulkButton.Text = "图像偏移";
            BulkButton.UseVisualStyleBackColor = true;
            BulkButton.Click += BulkButton_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(127, 120);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(99, 21);
            checkBox1.TabIndex = 24;
            checkBox1.Text = "应用图像偏移";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(RButtonOverlay);
            groupBox1.Controls.Add(RButtonImage);
            groupBox1.Location = new Point(9, 371);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(218, 50);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "显示模式";
            // 
            // RButtonOverlay
            // 
            RButtonOverlay.AutoSize = true;
            RButtonOverlay.Location = new Point(121, 21);
            RButtonOverlay.Margin = new Padding(4, 3, 4, 3);
            RButtonOverlay.Name = "RButtonOverlay";
            RButtonOverlay.Size = new Size(74, 21);
            RButtonOverlay.TabIndex = 1;
            RButtonOverlay.Text = "静态显示";
            RButtonOverlay.UseVisualStyleBackColor = true;
            RButtonOverlay.CheckedChanged += RButtonViewMode_CheckedChanged;
            // 
            // RButtonImage
            // 
            RButtonImage.AutoSize = true;
            RButtonImage.Checked = true;
            RButtonImage.Location = new Point(23, 21);
            RButtonImage.Margin = new Padding(4, 3, 4, 3);
            RButtonImage.Name = "RButtonImage";
            RButtonImage.Size = new Size(74, 21);
            RButtonImage.TabIndex = 0;
            RButtonImage.TabStop = true;
            RButtonImage.Text = "动态显示";
            RButtonImage.UseVisualStyleBackColor = true;
            RButtonImage.CheckedChanged += RButtonViewMode_CheckedChanged;
            // 
            // checkboxRemoveBlackOnImport
            // 
            checkboxRemoveBlackOnImport.AutoSize = true;
            checkboxRemoveBlackOnImport.Checked = true;
            checkboxRemoveBlackOnImport.CheckState = CheckState.Checked;
            checkboxRemoveBlackOnImport.Location = new Point(13, 436);
            checkboxRemoveBlackOnImport.Margin = new Padding(4, 3, 4, 3);
            checkboxRemoveBlackOnImport.Name = "checkboxRemoveBlackOnImport";
            checkboxRemoveBlackOnImport.Size = new Size(99, 21);
            checkboxRemoveBlackOnImport.TabIndex = 22;
            checkboxRemoveBlackOnImport.Text = "导入删除黑色";
            checkboxRemoveBlackOnImport.UseVisualStyleBackColor = true;
            // 
            // nudJump
            // 
            nudJump.Location = new Point(90, 287);
            nudJump.Margin = new Padding(4, 3, 4, 3);
            nudJump.Maximum = new decimal(new int[] { 650000, 0, 0, 0 });
            nudJump.Name = "nudJump";
            nudJump.Size = new Size(90, 23);
            nudJump.TabIndex = 21;
            nudJump.ValueChanged += nudJump_ValueChanged;
            nudJump.KeyDown += nudJump_KeyDown;
            // 
            // checkBoxPreventAntiAliasing
            // 
            checkBoxPreventAntiAliasing.AutoSize = true;
            checkBoxPreventAntiAliasing.Location = new Point(111, 467);
            checkBoxPreventAntiAliasing.Margin = new Padding(4, 3, 4, 3);
            checkBoxPreventAntiAliasing.Name = "checkBoxPreventAntiAliasing";
            checkBoxPreventAntiAliasing.Size = new Size(87, 21);
            checkBoxPreventAntiAliasing.TabIndex = 20;
            checkBoxPreventAntiAliasing.Text = "抗锯齿效果";
            checkBoxPreventAntiAliasing.UseVisualStyleBackColor = true;
            checkBoxPreventAntiAliasing.CheckedChanged += checkBoxPreventAntiAliasing_CheckedChanged;
            // 
            // checkBoxQuality
            // 
            checkBoxQuality.AutoSize = true;
            checkBoxQuality.Location = new Point(13, 467);
            checkBoxQuality.Margin = new Padding(4, 3, 4, 3);
            checkBoxQuality.Name = "checkBoxQuality";
            checkBoxQuality.Size = new Size(75, 21);
            checkBoxQuality.TabIndex = 19;
            checkBoxQuality.Text = "模糊效果";
            checkBoxQuality.UseVisualStyleBackColor = true;
            checkBoxQuality.CheckedChanged += checkBoxQuality_CheckedChanged;
            // 
            // buttonSkipPrevious
            // 
            buttonSkipPrevious.ForeColor = SystemColors.ControlText;
            buttonSkipPrevious.Image = (Image)resources.GetObject("buttonSkipPrevious.Image");
            buttonSkipPrevious.Location = new Point(49, 282);
            buttonSkipPrevious.Margin = new Padding(4, 3, 4, 3);
            buttonSkipPrevious.Name = "buttonSkipPrevious";
            buttonSkipPrevious.Size = new Size(35, 34);
            buttonSkipPrevious.TabIndex = 17;
            buttonSkipPrevious.Tag = "";
            buttonSkipPrevious.TextImageRelation = TextImageRelation.TextBeforeImage;
            buttonSkipPrevious.UseVisualStyleBackColor = true;
            buttonSkipPrevious.Click += buttonSkipPrevious_Click;
            // 
            // buttonSkipNext
            // 
            buttonSkipNext.ForeColor = SystemColors.ControlText;
            buttonSkipNext.Image = (Image)resources.GetObject("buttonSkipNext.Image");
            buttonSkipNext.Location = new Point(186, 282);
            buttonSkipNext.Margin = new Padding(4, 3, 4, 3);
            buttonSkipNext.Name = "buttonSkipNext";
            buttonSkipNext.Size = new Size(35, 34);
            buttonSkipNext.TabIndex = 16;
            buttonSkipNext.Tag = "";
            buttonSkipNext.TextImageRelation = TextImageRelation.TextBeforeImage;
            buttonSkipNext.UseVisualStyleBackColor = true;
            buttonSkipNext.Click += buttonSkipNext_Click;
            // 
            // buttonReplace
            // 
            buttonReplace.ForeColor = SystemColors.ControlText;
            buttonReplace.Image = (Image)resources.GetObject("buttonReplace.Image");
            buttonReplace.ImageAlign = ContentAlignment.MiddleRight;
            buttonReplace.Location = new Point(16, 191);
            buttonReplace.Margin = new Padding(4, 3, 4, 3);
            buttonReplace.Name = "buttonReplace";
            buttonReplace.Size = new Size(90, 34);
            buttonReplace.TabIndex = 15;
            buttonReplace.Tag = "";
            buttonReplace.Text = "替换图像";
            buttonReplace.TextImageRelation = TextImageRelation.TextBeforeImage;
            buttonReplace.UseVisualStyleBackColor = true;
            buttonReplace.Click += buttonReplace_Click;
            // 
            // pictureBox
            // 
            pictureBox.Image = (Image)resources.GetObject("pictureBox.Image");
            pictureBox.Location = new Point(12, 11);
            pictureBox.Margin = new Padding(4, 3, 4, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(16, 16);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 14;
            pictureBox.TabStop = false;
            toolTip.SetToolTip(pictureBox, "将黑色背景切换到白色背景");
            pictureBox.Click += pictureBox_Click;
            // 
            // ZoomTrackBar
            // 
            ZoomTrackBar.LargeChange = 1;
            ZoomTrackBar.Location = new Point(49, 324);
            ZoomTrackBar.Margin = new Padding(4, 3, 4, 3);
            ZoomTrackBar.Minimum = 1;
            ZoomTrackBar.Name = "ZoomTrackBar";
            ZoomTrackBar.Size = new Size(172, 45);
            ZoomTrackBar.TabIndex = 4;
            ZoomTrackBar.TickStyle = TickStyle.TopLeft;
            ZoomTrackBar.Value = 1;
            ZoomTrackBar.Scroll += ZoomTrackBar_Scroll;
            // 
            // ExportButton
            // 
            ExportButton.ForeColor = SystemColors.ControlText;
            ExportButton.Image = (Image)resources.GetObject("ExportButton.Image");
            ExportButton.ImageAlign = ContentAlignment.MiddleRight;
            ExportButton.Location = new Point(125, 229);
            ExportButton.Margin = new Padding(4, 3, 4, 3);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(93, 34);
            ExportButton.TabIndex = 3;
            ExportButton.Tag = "";
            ExportButton.Text = "导出图像";
            ExportButton.TextImageRelation = TextImageRelation.TextBeforeImage;
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportButton_Click;
            // 
            // InsertImageButton
            // 
            InsertImageButton.ForeColor = SystemColors.ControlText;
            InsertImageButton.Image = (Image)resources.GetObject("InsertImageButton.Image");
            InsertImageButton.ImageAlign = ContentAlignment.MiddleRight;
            InsertImageButton.Location = new Point(125, 190);
            InsertImageButton.Margin = new Padding(4, 3, 4, 3);
            InsertImageButton.Name = "InsertImageButton";
            InsertImageButton.Size = new Size(93, 34);
            InsertImageButton.TabIndex = 1;
            InsertImageButton.Tag = "";
            InsertImageButton.Text = "插入图像";
            InsertImageButton.TextImageRelation = TextImageRelation.TextBeforeImage;
            InsertImageButton.UseVisualStyleBackColor = true;
            InsertImageButton.Click += InsertImageButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.ForeColor = SystemColors.ControlText;
            DeleteButton.Image = (Image)resources.GetObject("DeleteButton.Image");
            DeleteButton.ImageAlign = ContentAlignment.MiddleRight;
            DeleteButton.Location = new Point(127, 150);
            DeleteButton.Margin = new Padding(4, 3, 4, 3);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(93, 34);
            DeleteButton.TabIndex = 2;
            DeleteButton.Tag = "";
            DeleteButton.Text = "删除图像";
            DeleteButton.TextImageRelation = TextImageRelation.TextBeforeImage;
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // AddButton
            // 
            AddButton.ForeColor = SystemColors.ControlText;
            AddButton.Image = (Image)resources.GetObject("AddButton.Image");
            AddButton.ImageAlign = ContentAlignment.MiddleRight;
            AddButton.Location = new Point(17, 151);
            AddButton.Margin = new Padding(4, 3, 4, 3);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(91, 34);
            AddButton.TabIndex = 0;
            AddButton.Tag = "";
            AddButton.Text = "添加图像";
            AddButton.TextImageRelation = TextImageRelation.TextBeforeImage;
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = SystemColors.ControlText;
            label10.Location = new Point(73, 92);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(70, 17);
            label10.TabIndex = 12;
            label10.Text = "偏移坐标 Y:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(72, 65);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(71, 17);
            label8.TabIndex = 11;
            label8.Text = "偏移坐标 X:";
            // 
            // HeightLabel
            // 
            HeightLabel.AutoSize = true;
            HeightLabel.ForeColor = SystemColors.ControlText;
            HeightLabel.Location = new Point(144, 34);
            HeightLabel.Margin = new Padding(4, 0, 4, 0);
            HeightLabel.Name = "HeightLabel";
            HeightLabel.Size = new Size(74, 17);
            HeightLabel.TabIndex = 10;
            HeightLabel.Text = "<空>";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(83, 34);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(56, 17);
            label6.TabIndex = 9;
            label6.Text = "图像高度";
            // 
            // WidthLabel
            // 
            WidthLabel.AutoSize = true;
            WidthLabel.ForeColor = SystemColors.ControlText;
            WidthLabel.Location = new Point(144, 11);
            WidthLabel.Margin = new Padding(4, 0, 4, 0);
            WidthLabel.Name = "WidthLabel";
            WidthLabel.Size = new Size(74, 17);
            WidthLabel.TabIndex = 8;
            WidthLabel.Text = "<空>";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(84, 11);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 7;
            label1.Text = "图像宽度";
            // 
            // panel
            // 
            panel.AutoScroll = true;
            panel.BackColor = Color.Black;
            panel.BorderStyle = BorderStyle.Fixed3D;
            panel.Controls.Add(ImageBox);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            panel.Margin = new Padding(4, 3, 4, 3);
            panel.Name = "panel";
            panel.Size = new Size(962, 512);
            panel.TabIndex = 1;
            // 
            // ImageBox
            // 
            ImageBox.BackColor = Color.Transparent;
            ImageBox.Location = new Point(0, 0);
            ImageBox.Margin = new Padding(4, 3, 4, 3);
            ImageBox.Name = "ImageBox";
            ImageBox.Size = new Size(64, 64);
            ImageBox.SizeMode = PictureBoxSizeMode.AutoSize;
            ImageBox.TabIndex = 0;
            ImageBox.TabStop = false;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabImages);
            tabControl.Controls.Add(tabFrames);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1207, 381);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabImages
            // 
            tabImages.Controls.Add(PreviewListView);
            tabImages.Location = new Point(4, 26);
            tabImages.Margin = new Padding(4, 3, 4, 3);
            tabImages.Name = "tabImages";
            tabImages.Padding = new Padding(4, 3, 4, 3);
            tabImages.Size = new Size(1199, 351);
            tabImages.TabIndex = 0;
            tabImages.Text = "图像";
            tabImages.UseVisualStyleBackColor = true;
            // 
            // PreviewListView
            // 
            PreviewListView.Activation = ItemActivation.OneClick;
            PreviewListView.BackColor = Color.GhostWhite;
            PreviewListView.Dock = DockStyle.Fill;
            PreviewListView.ForeColor = Color.FromArgb(142, 152, 156);
            PreviewListView.LargeImageList = ImageList;
            PreviewListView.Location = new Point(4, 3);
            PreviewListView.Margin = new Padding(4, 3, 4, 3);
            PreviewListView.Name = "PreviewListView";
            PreviewListView.Size = new Size(1191, 345);
            PreviewListView.TabIndex = 0;
            PreviewListView.UseCompatibleStateImageBehavior = false;
            PreviewListView.VirtualMode = true;
            PreviewListView.RetrieveVirtualItem += PreviewListView_RetrieveVirtualItem;
            PreviewListView.SelectedIndexChanged += PreviewListView_SelectedIndexChanged;
            PreviewListView.VirtualItemsSelectionRangeChanged += PreviewListView_VirtualItemsSelectionRangeChanged;
            // 
            // ImageList
            // 
            ImageList.ColorDepth = ColorDepth.Depth32Bit;
            ImageList.ImageSize = new Size(64, 64);
            ImageList.TransparentColor = Color.Transparent;
            // 
            // tabFrames
            // 
            tabFrames.Controls.Add(frameGridView);
            tabFrames.Location = new Point(4, 26);
            tabFrames.Margin = new Padding(4, 3, 4, 3);
            tabFrames.Name = "tabFrames";
            tabFrames.Size = new Size(1199, 351);
            tabFrames.TabIndex = 1;
            tabFrames.Text = "动作";
            tabFrames.UseVisualStyleBackColor = true;
            // 
            // frameGridView
            // 
            frameGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            frameGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            frameGridView.Columns.AddRange(new DataGridViewColumn[] { FrameAction, FrameStart, FrameCount, FrameSkip, FrameInterval, FrameEffectStart, FrameEffectCount, FrameEffectSkip, FrameEffectInterval, FrameReverse, FrameBlend });
            frameGridView.Dock = DockStyle.Fill;
            frameGridView.Location = new Point(0, 0);
            frameGridView.Margin = new Padding(4, 3, 4, 3);
            frameGridView.Name = "frameGridView";
            frameGridView.Size = new Size(1199, 351);
            frameGridView.TabIndex = 2;
            frameGridView.CellContentClick += frameGridView_CellContentClick;
            frameGridView.CellValidating += frameGridView_CellValidating;
            frameGridView.DefaultValuesNeeded += frameGridView_DefaultValuesNeeded;
            frameGridView.RowEnter += frameGridView_RowEnter;
            // 
            // FrameAction
            // 
            FrameAction.HeaderText = "动作形象";
            FrameAction.Name = "FrameAction";
            FrameAction.Resizable = DataGridViewTriState.True;
            FrameAction.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // FrameStart
            // 
            FrameStart.HeaderText = "开始图像";
            FrameStart.Name = "FrameStart";
            // 
            // FrameCount
            // 
            FrameCount.HeaderText = "动作计数";
            FrameCount.Name = "FrameCount";
            // 
            // FrameSkip
            // 
            FrameSkip.HeaderText = "跳过图像";
            FrameSkip.Name = "FrameSkip";
            // 
            // FrameInterval
            // 
            FrameInterval.HeaderText = "动作间隔";
            FrameInterval.Name = "FrameInterval";
            // 
            // FrameEffectStart
            // 
            FrameEffectStart.HeaderText = "特效开始";
            FrameEffectStart.Name = "FrameEffectStart";
            // 
            // FrameEffectCount
            // 
            FrameEffectCount.HeaderText = "特效计数";
            FrameEffectCount.Name = "FrameEffectCount";
            // 
            // FrameEffectSkip
            // 
            FrameEffectSkip.HeaderText = "特效跳过";
            FrameEffectSkip.Name = "FrameEffectSkip";
            // 
            // FrameEffectInterval
            // 
            FrameEffectInterval.HeaderText = "特效间隔";
            FrameEffectInterval.Name = "FrameEffectInterval";
            // 
            // FrameReverse
            // 
            FrameReverse.HeaderText = "倒放效果";
            FrameReverse.Name = "FrameReverse";
            // 
            // FrameBlend
            // 
            FrameBlend.HeaderText = "混合效果";
            FrameBlend.Name = "FrameBlend";
            // 
            // OpenLibraryDialog
            // 
            OpenLibraryDialog.Filter = "Library|*.Lib";
            // 
            // SaveLibraryDialog
            // 
            SaveLibraryDialog.Filter = "Library|*.Lib";
            // 
            // ImportImageDialog
            // 
            ImportImageDialog.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            ImportImageDialog.Multiselect = true;
            // 
            // OpenWeMadeDialog
            // 
            OpenWeMadeDialog.Filter = "WeMade|*.Wil;*.Wtl|Shanda|*.Wzl;*.Miz|Lib|*.Lib";
            OpenWeMadeDialog.Multiselect = true;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel, toolStripProgressBar });
            statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStrip.Location = new Point(0, 928);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1209, 26);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(56, 21);
            toolStripStatusLabel.Text = "选择图像";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Alignment = ToolStripItemAlignment.Right;
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new Size(233, 20);
            toolStripProgressBar.Step = 1;
            toolStripProgressBar.Style = ProgressBarStyle.Continuous;
            // 
            // FolderLibraryDialog
            // 
            FolderLibraryDialog.ShowNewFolderButton = false;
            // 
            // FrameAnimTimer
            // 
            FrameAnimTimer.Tick += FrameAnimTimer_Tick;
            // 
            // LMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1209, 954);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip);
            Controls.Add(MainMenu);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MainMenu;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(756, 576);
            Name = "LMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "传奇LIB编辑器";
            Resize += LMain_Resize;
            MainMenu.ResumeLayout(false);
            MainMenu.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDownY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudJump).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZoomTrackBar).EndInit();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageBox).EndInit();
            tabControl.ResumeLayout(false);
            tabImages.ResumeLayout(false);
            tabFrames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)frameGridView).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;

        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OpenLibraryDialog;
        private System.Windows.Forms.SaveFileDialog SaveLibraryDialog;
        private System.Windows.Forms.OpenFileDialog ImportImageDialog;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.OpenFileDialog OpenWeMadeDialog;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeBlanksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countBlanksToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button InsertImageButton;
        private System.Windows.Forms.ToolStripMenuItem safeToolStripMenuItem;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TrackBar ZoomTrackBar;
        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripMenuItem skinToolStripMenuItem;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonSkipPrevious;
        private System.Windows.Forms.Button buttonSkipNext;
        private System.Windows.Forms.CheckBox checkBoxQuality;
        private System.Windows.Forms.CheckBox checkBoxPreventAntiAliasing;
        private System.Windows.Forms.NumericUpDown nudJump;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabImages;
        private CustomFormControl.FixedListView PreviewListView;
        private System.Windows.Forms.TabPage tabFrames;
        private System.Windows.Forms.DataGridView frameGridView;
        private System.Windows.Forms.ToolStripMenuItem populateFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultMonsterFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultPlayerFramesToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog FolderLibraryDialog;
        private System.Windows.Forms.ToolStripMenuItem autofillFromCodeToolStripMenuItem;
        private System.Windows.Forms.DataGridViewComboBoxColumn FrameAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameSkip;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameInterval;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameEffectStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameEffectCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameEffectSkip;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameEffectInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FrameReverse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FrameBlend;
        private System.Windows.Forms.ToolStripMenuItem defaultNPCFramesToolStripMenuItem;
        private System.Windows.Forms.Timer FrameAnimTimer;
        private System.Windows.Forms.CheckBox checkboxRemoveBlackOnImport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RButtonOverlay;
        private System.Windows.Forms.RadioButton RButtonImage;
        private ToolStripMenuItem openReferenceFileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private CheckBox checkBox1;
        private ToolStripMenuItem importShadowsToolStripMenuItem;
        private ToolStripMenuItem openReferenceImageToolStripMenuItem;
        private Button BulkButton;
        private NumericUpDown numericUpDownY;
        private NumericUpDown numericUpDownX;
    }
}

