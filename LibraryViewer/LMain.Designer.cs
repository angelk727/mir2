namespace LibraryViewer
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
            MainMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openMenuItem = new ToolStripMenuItem();
            ImageList = new ImageList(components);
            LibraryFolderDialog = new FolderBrowserDialog();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            DebugBox = new TextBox();
            checkCenter = new CheckBox();
            checkBackground = new CheckBox();
            HeightLabel = new Label();
            LblHeight = new Label();
            LibNameLabel = new Label();
            LibCountLabel = new Label();
            WidthLabel = new Label();
            LblLibName = new Label();
            LblLibCount = new Label();
            LblWidth = new Label();
            ImageBox = new PictureBox();
            PreviewListView = new CustomFormControl.FixedListView();
            ExportImagesButton = new Button();
            MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageBox).BeginInit();
            SuspendLayout();
            // 
            // MainMenu
            // 
            MainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            MainMenu.Location = new Point(0, 0);
            MainMenu.Name = "MainMenu";
            MainMenu.Padding = new Padding(7, 3, 0, 3);
            MainMenu.Size = new Size(1007, 27);
            MainMenu.TabIndex = 0;
            MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(92, 21);
            fileToolStripMenuItem.Text = "选择库文件夹";
            // 
            // openMenuItem
            // 
            openMenuItem.Name = "openMenuItem";
            openMenuItem.Size = new Size(100, 22);
            openMenuItem.Text = "打开";
            openMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // ImageList
            // 
            ImageList.ColorDepth = ColorDepth.Depth32Bit;
            ImageList.ImageSize = new Size(64, 64);
            ImageList.TransparentColor = Color.Transparent;
            // 
            // LibraryFolderDialog
            // 
            LibraryFolderDialog.ShowNewFolderButton = false;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 27);
            splitContainer1.Margin = new Padding(4);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(PreviewListView);
            splitContainer1.Size = new Size(1007, 667);
            splitContainer1.SplitterDistance = 328;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(4);
            splitContainer2.MinimumSize = new Size(0, 262);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(ExportImagesButton);
            splitContainer2.Panel1.Controls.Add(DebugBox);
            splitContainer2.Panel1.Controls.Add(checkCenter);
            splitContainer2.Panel1.Controls.Add(checkBackground);
            splitContainer2.Panel1.Controls.Add(HeightLabel);
            splitContainer2.Panel1.Controls.Add(LblHeight);
            splitContainer2.Panel1.Controls.Add(LibNameLabel);
            splitContainer2.Panel1.Controls.Add(LibCountLabel);
            splitContainer2.Panel1.Controls.Add(WidthLabel);
            splitContainer2.Panel1.Controls.Add(LblLibName);
            splitContainer2.Panel1.Controls.Add(LblLibCount);
            splitContainer2.Panel1.Controls.Add(LblWidth);
            splitContainer2.Panel1MinSize = 150;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(ImageBox);
            splitContainer2.Panel2MinSize = 250;
            splitContainer2.Size = new Size(1007, 328);
            splitContainer2.SplitterDistance = 201;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 0;
            // 
            // DebugBox
            // 
            DebugBox.Location = new Point(4, 238);
            DebugBox.Margin = new Padding(4);
            DebugBox.Multiline = true;
            DebugBox.Name = "DebugBox";
            DebugBox.ScrollBars = ScrollBars.Both;
            DebugBox.Size = new Size(195, 84);
            DebugBox.TabIndex = 6;
            DebugBox.Visible = false;
            // 
            // checkCenter
            // 
            checkCenter.AutoSize = true;
            checkCenter.Checked = true;
            checkCenter.CheckState = CheckState.Checked;
            checkCenter.Location = new Point(14, 139);
            checkCenter.Margin = new Padding(4);
            checkCenter.Name = "checkCenter";
            checkCenter.Size = new Size(51, 21);
            checkCenter.TabIndex = 5;
            checkCenter.Text = "居中";
            checkCenter.UseVisualStyleBackColor = true;
            checkCenter.CheckedChanged += checkCenter_CheckedChanged;
            // 
            // checkBackground
            // 
            checkBackground.AutoSize = true;
            checkBackground.Checked = true;
            checkBackground.CheckState = CheckState.Checked;
            checkBackground.Location = new Point(14, 109);
            checkBackground.Margin = new Padding(4);
            checkBackground.Name = "checkBackground";
            checkBackground.Size = new Size(51, 21);
            checkBackground.TabIndex = 4;
            checkBackground.Text = "背景";
            checkBackground.UseVisualStyleBackColor = true;
            checkBackground.CheckedChanged += checkBackground_CheckedChanged;
            // 
            // HeightLabel
            // 
            HeightLabel.AutoSize = true;
            HeightLabel.Location = new Point(65, 76);
            HeightLabel.Margin = new Padding(4, 0, 4, 0);
            HeightLabel.Name = "HeightLabel";
            HeightLabel.Size = new Size(38, 17);
            HeightLabel.TabIndex = 3;
            HeightLabel.Text = "<空>";
            // 
            // LblHeight
            // 
            LblHeight.AutoSize = true;
            LblHeight.Location = new Point(14, 76);
            LblHeight.Margin = new Padding(4, 0, 4, 0);
            LblHeight.Name = "LblHeight";
            LblHeight.Size = new Size(35, 17);
            LblHeight.TabIndex = 2;
            LblHeight.Text = "高度:";
            // 
            // LibNameLabel
            // 
            LibNameLabel.AutoSize = true;
            LibNameLabel.Location = new Point(69, 31);
            LibNameLabel.Margin = new Padding(4, 0, 4, 0);
            LibNameLabel.Name = "LibNameLabel";
            LibNameLabel.Size = new Size(62, 17);
            LibNameLabel.TabIndex = 1;
            LibNameLabel.Text = "<无选择>";
            // 
            // LibCountLabel
            // 
            LibCountLabel.AutoSize = true;
            LibCountLabel.Location = new Point(69, 12);
            LibCountLabel.Margin = new Padding(4, 0, 4, 0);
            LibCountLabel.Name = "LibCountLabel";
            LibCountLabel.Size = new Size(15, 17);
            LibCountLabel.TabIndex = 1;
            LibCountLabel.Text = "0";
            // 
            // WidthLabel
            // 
            WidthLabel.AutoSize = true;
            WidthLabel.Location = new Point(65, 59);
            WidthLabel.Margin = new Padding(4, 0, 4, 0);
            WidthLabel.Name = "WidthLabel";
            WidthLabel.Size = new Size(38, 17);
            WidthLabel.TabIndex = 1;
            WidthLabel.Text = "<空>";
            // 
            // LblLibName
            // 
            LblLibName.AutoSize = true;
            LblLibName.Location = new Point(14, 31);
            LblLibName.Margin = new Padding(4, 0, 4, 0);
            LblLibName.Name = "LblLibName";
            LblLibName.Size = new Size(57, 17);
            LblLibName.TabIndex = 0;
            LblLibName.Text = "LIB 文件:";
            // 
            // LblLibCount
            // 
            LblLibCount.AutoSize = true;
            LblLibCount.Location = new Point(14, 12);
            LblLibCount.Margin = new Padding(4, 0, 4, 0);
            LblLibCount.Name = "LblLibCount";
            LblLibCount.Size = new Size(45, 17);
            LblLibCount.TabIndex = 0;
            LblLibCount.Text = "LIB 数:";
            // 
            // LblWidth
            // 
            LblWidth.AutoSize = true;
            LblWidth.Location = new Point(14, 59);
            LblWidth.Margin = new Padding(4, 0, 4, 0);
            LblWidth.Name = "LblWidth";
            LblWidth.Size = new Size(35, 17);
            LblWidth.TabIndex = 0;
            LblWidth.Text = "宽度:";
            // 
            // ImageBox
            // 
            ImageBox.BackColor = Color.White;
            ImageBox.Dock = DockStyle.Fill;
            ImageBox.Location = new Point(0, 0);
            ImageBox.Margin = new Padding(4);
            ImageBox.Name = "ImageBox";
            ImageBox.Size = new Size(801, 328);
            ImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            ImageBox.TabIndex = 0;
            ImageBox.TabStop = false;
            // 
            // PreviewListView
            // 
            PreviewListView.BackgroundImageTiled = true;
            PreviewListView.Dock = DockStyle.Fill;
            PreviewListView.LargeImageList = ImageList;
            PreviewListView.Location = new Point(0, 0);
            PreviewListView.Margin = new Padding(4);
            PreviewListView.Name = "PreviewListView";
            PreviewListView.ShowItemToolTips = true;
            PreviewListView.Size = new Size(1007, 334);
            PreviewListView.TabIndex = 0;
            PreviewListView.UseCompatibleStateImageBehavior = false;
            PreviewListView.VirtualMode = true;
            PreviewListView.RetrieveVirtualItem += PreviewListView_RetrieveVirtualItem;
            PreviewListView.SelectedIndexChanged += PreviewListView_SelectedIndexChanged;
            // 
            // ExportImagesButton
            // 
            ExportImagesButton.Location = new Point(14, 170);
            ExportImagesButton.Margin = new Padding(4);
            ExportImagesButton.Name = "ExportImagesButton";
            ExportImagesButton.Size = new Size(136, 30);
            ExportImagesButton.TabIndex = 7;
            ExportImagesButton.Text = "导出图像";
            ExportImagesButton.UseVisualStyleBackColor = true;
            ExportImagesButton.Click += ExportImagesButton_Click;
            // 
            // LMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1007, 694);
            Controls.Add(splitContainer1);
            Controls.Add(MainMenu);
            MainMenuStrip = MainMenu;
            Margin = new Padding(4);
            Name = "LMain";
            Text = "C# Lib库查看器";
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
            ((System.ComponentModel.ISupportInitialize)ImageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openMenuItem;
        private ImageList ImageList;
        private FolderBrowserDialog LibraryFolderDialog;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private CheckBox checkCenter;
        private CheckBox checkBackground;
        private Label HeightLabel;
        private Label LblHeight;
        private Label LibCountLabel;
        private Label WidthLabel;
        private Label LblLibCount;
        private Label LblWidth;
        private PictureBox ImageBox;
        private CustomFormControl.FixedListView PreviewListView;
        private Label LibNameLabel;
        private Label LblLibName;
        private TextBox DebugBox;
        private Button ExportImagesButton;
    }
}

