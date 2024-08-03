namespace Server.Database
{
    partial class RecipeInfoForm
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
            RecipeList = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            RecipeGroupBox = new GroupBox();
            GoldTextBox = new TextBox();
            ChanceTextBox = new TextBox();
            CraftAmountTextBox = new TextBox();
            ItemTextBox = new TextBox();
            label6 = new Label();
            ToolsGroupBox = new GroupBox();
            ToolTextBox = new TextBox();
            IngredientsGroupBox = new GroupBox();
            label8 = new Label();
            Quality4TextBox = new TextBox();
            Quality3TextBox = new TextBox();
            Quality2TextBox = new TextBox();
            Quality1TextBox = new TextBox();
            IngredientName4TextBox = new TextBox();
            IngredientName3TextBox = new TextBox();
            IngredientName2TextBox = new TextBox();
            IngredientName1TextBox = new TextBox();
            label7 = new Label();
            label5 = new Label();
            IngredientAmount4TextBox = new TextBox();
            IngredientAmount3TextBox = new TextBox();
            IngredientAmount2TextBox = new TextBox();
            IngredientAmount1TextBox = new TextBox();
            NewRecipeButton = new Button();
            RecipeGroupBox.SuspendLayout();
            ToolsGroupBox.SuspendLayout();
            IngredientsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // RecipeList
            // 
            RecipeList.FormattingEnabled = true;
            RecipeList.ItemHeight = 17;
            RecipeList.Location = new Point(12, 14);
            RecipeList.Name = "RecipeList";
            RecipeList.Size = new Size(135, 361);
            RecipeList.TabIndex = 0;
            RecipeList.SelectedIndexChanged += RecipeList_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 50);
            label1.Name = "label1";
            label1.Size = new Size(63, 17);
            label1.TabIndex = 1;
            label1.Text = "合成数量: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 80);
            label2.Name = "label2";
            label2.Size = new Size(63, 17);
            label2.TabIndex = 2;
            label2.Text = "成功几率: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 112);
            label3.Name = "label3";
            label3.Size = new Size(63, 17);
            label3.TabIndex = 3;
            label3.Text = "制作费用: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 23);
            label4.Name = "label4";
            label4.Size = new Size(63, 17);
            label4.TabIndex = 4;
            label4.Text = "辅助工具: ";
            // 
            // RecipeGroupBox
            // 
            RecipeGroupBox.Controls.Add(GoldTextBox);
            RecipeGroupBox.Controls.Add(ChanceTextBox);
            RecipeGroupBox.Controls.Add(CraftAmountTextBox);
            RecipeGroupBox.Controls.Add(ItemTextBox);
            RecipeGroupBox.Controls.Add(label6);
            RecipeGroupBox.Controls.Add(label1);
            RecipeGroupBox.Controls.Add(label2);
            RecipeGroupBox.Controls.Add(label3);
            RecipeGroupBox.Location = new Point(153, 14);
            RecipeGroupBox.Name = "RecipeGroupBox";
            RecipeGroupBox.Size = new Size(200, 148);
            RecipeGroupBox.TabIndex = 6;
            RecipeGroupBox.TabStop = false;
            RecipeGroupBox.Text = "合成配方(必填)";
            // 
            // GoldTextBox
            // 
            GoldTextBox.Location = new Point(74, 109);
            GoldTextBox.Name = "GoldTextBox";
            GoldTextBox.Size = new Size(97, 23);
            GoldTextBox.TabIndex = 8;
            // 
            // ChanceTextBox
            // 
            ChanceTextBox.Location = new Point(74, 77);
            ChanceTextBox.Name = "ChanceTextBox";
            ChanceTextBox.Size = new Size(40, 23);
            ChanceTextBox.TabIndex = 7;
            // 
            // CraftAmountTextBox
            // 
            CraftAmountTextBox.Location = new Point(74, 47);
            CraftAmountTextBox.Name = "CraftAmountTextBox";
            CraftAmountTextBox.Size = new Size(40, 23);
            CraftAmountTextBox.TabIndex = 6;
            // 
            // ItemTextBox
            // 
            ItemTextBox.Location = new Point(50, 18);
            ItemTextBox.Name = "ItemTextBox";
            ItemTextBox.Size = new Size(121, 23);
            ItemTextBox.TabIndex = 5;
            ItemTextBox.TextChanged += ItemTextBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 21);
            label6.Name = "label6";
            label6.Size = new Size(39, 17);
            label6.TabIndex = 4;
            label6.Text = "物品: ";
            // 
            // ToolsGroupBox
            // 
            ToolsGroupBox.Controls.Add(ToolTextBox);
            ToolsGroupBox.Controls.Add(label4);
            ToolsGroupBox.Location = new Point(153, 169);
            ToolsGroupBox.Name = "ToolsGroupBox";
            ToolsGroupBox.Size = new Size(200, 53);
            ToolsGroupBox.TabIndex = 7;
            ToolsGroupBox.TabStop = false;
            ToolsGroupBox.Text = "辅助工具(可选)";
            // 
            // ToolTextBox
            // 
            ToolTextBox.Location = new Point(70, 20);
            ToolTextBox.Name = "ToolTextBox";
            ToolTextBox.Size = new Size(113, 23);
            ToolTextBox.TabIndex = 20;
            // 
            // IngredientsGroupBox
            // 
            IngredientsGroupBox.Controls.Add(label8);
            IngredientsGroupBox.Controls.Add(Quality4TextBox);
            IngredientsGroupBox.Controls.Add(Quality3TextBox);
            IngredientsGroupBox.Controls.Add(Quality2TextBox);
            IngredientsGroupBox.Controls.Add(Quality1TextBox);
            IngredientsGroupBox.Controls.Add(IngredientName4TextBox);
            IngredientsGroupBox.Controls.Add(IngredientName3TextBox);
            IngredientsGroupBox.Controls.Add(IngredientName2TextBox);
            IngredientsGroupBox.Controls.Add(IngredientName1TextBox);
            IngredientsGroupBox.Controls.Add(label7);
            IngredientsGroupBox.Controls.Add(label5);
            IngredientsGroupBox.Controls.Add(IngredientAmount4TextBox);
            IngredientsGroupBox.Controls.Add(IngredientAmount3TextBox);
            IngredientsGroupBox.Controls.Add(IngredientAmount2TextBox);
            IngredientsGroupBox.Controls.Add(IngredientAmount1TextBox);
            IngredientsGroupBox.Location = new Point(153, 229);
            IngredientsGroupBox.Name = "IngredientsGroupBox";
            IngredientsGroupBox.Size = new Size(299, 181);
            IngredientsGroupBox.TabIndex = 8;
            IngredientsGroupBox.TabStop = false;
            IngredientsGroupBox.Text = "所需材料(必填)";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(215, 25);
            label8.Name = "label8";
            label8.Size = new Size(65, 17);
            label8.TabIndex = 24;
            label8.Text = "持久/品质";
            // 
            // Quality4TextBox
            // 
            Quality4TextBox.Location = new Point(207, 150);
            Quality4TextBox.Name = "Quality4TextBox";
            Quality4TextBox.Size = new Size(83, 23);
            Quality4TextBox.TabIndex = 23;
            // 
            // Quality3TextBox
            // 
            Quality3TextBox.Location = new Point(207, 117);
            Quality3TextBox.Name = "Quality3TextBox";
            Quality3TextBox.Size = new Size(83, 23);
            Quality3TextBox.TabIndex = 22;
            // 
            // Quality2TextBox
            // 
            Quality2TextBox.Location = new Point(207, 84);
            Quality2TextBox.Name = "Quality2TextBox";
            Quality2TextBox.Size = new Size(83, 23);
            Quality2TextBox.TabIndex = 21;
            // 
            // Quality1TextBox
            // 
            Quality1TextBox.Location = new Point(207, 51);
            Quality1TextBox.Name = "Quality1TextBox";
            Quality1TextBox.Size = new Size(83, 23);
            Quality1TextBox.TabIndex = 20;
            // 
            // IngredientName4TextBox
            // 
            IngredientName4TextBox.Location = new Point(6, 150);
            IngredientName4TextBox.Name = "IngredientName4TextBox";
            IngredientName4TextBox.Size = new Size(113, 23);
            IngredientName4TextBox.TabIndex = 19;
            // 
            // IngredientName3TextBox
            // 
            IngredientName3TextBox.Location = new Point(6, 117);
            IngredientName3TextBox.Name = "IngredientName3TextBox";
            IngredientName3TextBox.Size = new Size(113, 23);
            IngredientName3TextBox.TabIndex = 18;
            // 
            // IngredientName2TextBox
            // 
            IngredientName2TextBox.Location = new Point(6, 84);
            IngredientName2TextBox.Name = "IngredientName2TextBox";
            IngredientName2TextBox.Size = new Size(113, 23);
            IngredientName2TextBox.TabIndex = 17;
            // 
            // IngredientName1TextBox
            // 
            IngredientName1TextBox.Location = new Point(6, 51);
            IngredientName1TextBox.Name = "IngredientName1TextBox";
            IngredientName1TextBox.Size = new Size(113, 23);
            IngredientName1TextBox.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(29, 25);
            label7.Name = "label7";
            label7.Size = new Size(56, 17);
            label7.TabIndex = 15;
            label7.Text = "材料名称";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(145, 25);
            label5.Name = "label5";
            label5.Size = new Size(32, 17);
            label5.TabIndex = 14;
            label5.Text = "数量";
            // 
            // IngredientAmount4TextBox
            // 
            IngredientAmount4TextBox.Location = new Point(135, 150);
            IngredientAmount4TextBox.Name = "IngredientAmount4TextBox";
            IngredientAmount4TextBox.Size = new Size(59, 23);
            IngredientAmount4TextBox.TabIndex = 13;
            // 
            // IngredientAmount3TextBox
            // 
            IngredientAmount3TextBox.Location = new Point(135, 117);
            IngredientAmount3TextBox.Name = "IngredientAmount3TextBox";
            IngredientAmount3TextBox.Size = new Size(59, 23);
            IngredientAmount3TextBox.TabIndex = 12;
            // 
            // IngredientAmount2TextBox
            // 
            IngredientAmount2TextBox.Location = new Point(135, 84);
            IngredientAmount2TextBox.Name = "IngredientAmount2TextBox";
            IngredientAmount2TextBox.Size = new Size(59, 23);
            IngredientAmount2TextBox.TabIndex = 11;
            // 
            // IngredientAmount1TextBox
            // 
            IngredientAmount1TextBox.Location = new Point(135, 51);
            IngredientAmount1TextBox.Name = "IngredientAmount1TextBox";
            IngredientAmount1TextBox.Size = new Size(59, 23);
            IngredientAmount1TextBox.TabIndex = 10;
            // 
            // NewRecipeButton
            // 
            NewRecipeButton.Location = new Point(40, 382);
            NewRecipeButton.Name = "NewRecipeButton";
            NewRecipeButton.Size = new Size(75, 26);
            NewRecipeButton.TabIndex = 9;
            NewRecipeButton.Text = "新配方";
            NewRecipeButton.UseVisualStyleBackColor = true;
            NewRecipeButton.Click += NewRecipeButton_Click;
            // 
            // RecipeInfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 418);
            Controls.Add(NewRecipeButton);
            Controls.Add(IngredientsGroupBox);
            Controls.Add(ToolsGroupBox);
            Controls.Add(RecipeGroupBox);
            Controls.Add(RecipeList);
            Name = "RecipeInfoForm";
            Text = "合成配方信息窗口";
            FormClosing += RecipeInfoForm_FormClosing;
            RecipeGroupBox.ResumeLayout(false);
            RecipeGroupBox.PerformLayout();
            ToolsGroupBox.ResumeLayout(false);
            ToolsGroupBox.PerformLayout();
            IngredientsGroupBox.ResumeLayout(false);
            IngredientsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox RecipeList;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private GroupBox RecipeGroupBox;
        private GroupBox ToolsGroupBox;
        private GroupBox IngredientsGroupBox;
        private Label label5;
        private TextBox IngredientAmount4TextBox;
        private TextBox IngredientAmount3TextBox;
        private TextBox IngredientAmount2TextBox;
        private TextBox IngredientAmount1TextBox;
        private Label label6;
        private TextBox GoldTextBox;
        private TextBox ChanceTextBox;
        private TextBox CraftAmountTextBox;
        private TextBox ItemTextBox;
        private Label label7;
        private TextBox ToolTextBox;
        private TextBox IngredientName4TextBox;
        private TextBox IngredientName3TextBox;
        private TextBox IngredientName2TextBox;
        private TextBox IngredientName1TextBox;
        private Button NewRecipeButton;
        private Label label8;
        private TextBox Quality4TextBox;
        private TextBox Quality3TextBox;
        private TextBox Quality2TextBox;
        private TextBox Quality1TextBox;
    }
}