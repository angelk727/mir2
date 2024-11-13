namespace Server.Database
{
    public partial class RecipeInfoForm : Form
    {
        private string currentFilePath;
        private bool isModified = false;
        private readonly string originalCraftAmount;
        private readonly string originalChance;
        private readonly string originalGold;
        private readonly string originalTool;
        private readonly string originalIngredientName1;
        private readonly string originalIngredientAmount1;
        private readonly string Quality1;
        private readonly string originalIngredientName2;
        private readonly string originalIngredientAmount2;
        private readonly string Quality2;
        private readonly string originalIngredientName3;
        private readonly string originalIngredientAmount3;
        private readonly string Quality3;
        private readonly string originalIngredientName4;
        private readonly string originalIngredientAmount4;
        private readonly string Quality4;
        private bool isUpdatingTextBox = false;

        public RecipeInfoForm()
        {
            InitializeComponent();
            this.Load += RecipeInfoForm_Load;
            RecipeList.SelectedIndexChanged += RecipeList_SelectedIndexChanged;

            #region Text Box Changed
            CraftAmountTextBox.TextChanged += TextBox_TextChanged;
            ChanceTextBox.TextChanged += TextBox_TextChanged;
            GoldTextBox.TextChanged += TextBox_TextChanged;
            ToolTextBox.TextChanged += TextBox_TextChanged;
            IngredientName1TextBox.TextChanged += TextBox_TextChanged;
            IngredientAmount1TextBox.TextChanged += TextBox_TextChanged;
            Quality1TextBox.TextChanged += TextBox_TextChanged;
            IngredientName2TextBox.TextChanged += TextBox_TextChanged;
            IngredientAmount2TextBox.TextChanged += TextBox_TextChanged;
            Quality3TextBox.TextChanged += TextBox_TextChanged;
            IngredientName3TextBox.TextChanged += TextBox_TextChanged;
            IngredientAmount3TextBox.TextChanged += TextBox_TextChanged;
            IngredientName4TextBox.TextChanged += TextBox_TextChanged;
            IngredientAmount4TextBox.TextChanged += TextBox_TextChanged;
            Quality4TextBox.TextChanged += TextBox_TextChanged;
            #endregion
        }

        #region Form Load
        private void RecipeInfoForm_Load(object sender, EventArgs e)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.Combine(currentDirectory, "Envir", "Recipe");

            if (Directory.Exists(directoryPath))
            {
                string[] recipeFiles = Directory.GetFiles(directoryPath, "*.txt");
                RecipeList.Items.Clear();

                for (int i = 0; i < recipeFiles.Length; i++)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(recipeFiles[i]);
                    string listItem = $"{i + 1}. {fileNameWithoutExtension}";
                    RecipeList.Items.Add(listItem);
                }
            }
            else
            {
                MessageBox.Show("配方目录不存在", "目录错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Recipe Selected Index Changed
        private void RecipeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RecipeList.SelectedIndex == -1)
            {
                ClearTextBoxes();
                return;
            }

            if (isModified)
            {
                SaveRecipe();
            }

            string selectedItem = RecipeList.SelectedItem.ToString();
            string fileNameWithoutExtension = selectedItem.Substring(selectedItem.IndexOf(' ') + 1);

            ItemTextBox.Text = fileNameWithoutExtension;
            currentFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Envir", "Recipe", fileNameWithoutExtension + ".txt");

            if (File.Exists(currentFilePath))
            {
                string[] fileLines = File.ReadAllLines(currentFilePath);
                ParseAndDisplayRecipe(fileLines);
            }
            else
            {
                MessageBox.Show("所选的配方文件不存在", "文件错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Parse and Display Recipe file
        private void ParseAndDisplayRecipe(string[] fileLines)
        {
            string currentSection = "";
            int ingredientIndex = 1;

            ClearTextBoxes();

            foreach (string line in fileLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line;
                    continue;
                }

                switch (currentSection)
                {
                    case "[Recipe]":
                        var recipeParts = line.Split(new[] { ' ' }, 2);
                        if (recipeParts.Length == 2)
                        {
                            string key = recipeParts[0].Trim();
                            string value = recipeParts[1].Trim();

                            if (key == "Amount")
                            {
                                CraftAmountTextBox.Text = value;
                            }
                            else if (key == "Chance")
                            {
                                ChanceTextBox.Text = value;
                            }
                            else if (key == "Gold")
                            {
                                GoldTextBox.Text = value;
                            }
                        }
                        break;

                    case "[Tools]":
                        ToolTextBox.Text = line.Trim();
                        break;

                    case "[Ingredients]":
                        string[] parts = line.Split(new[] { ' ' }, 3);
                        string ingredientName = parts[0].Trim();
                        string ingredientAmount = parts.Length > 1 ? parts[1].Trim() : "";
                        string ingredientQuality = parts.Length > 2 ? parts[2].Trim() : "";

                        switch (ingredientIndex)
                        {
                            case 1:
                                IngredientName1TextBox.Text = ingredientName;
                                IngredientAmount1TextBox.Text = ingredientAmount;
                                Quality1TextBox.Text = ingredientQuality;
                                break;
                            case 2:
                                IngredientName2TextBox.Text = ingredientName;
                                IngredientAmount2TextBox.Text = ingredientAmount;
                                Quality2TextBox.Text = ingredientQuality;
                                break;
                            case 3:
                                IngredientName3TextBox.Text = ingredientName;
                                IngredientAmount3TextBox.Text = ingredientAmount;
                                Quality3TextBox.Text = ingredientQuality;
                                break;
                            case 4:
                                IngredientName4TextBox.Text = ingredientName;
                                IngredientAmount4TextBox.Text = ingredientAmount;
                                Quality4TextBox.Text = ingredientQuality;
                                break;
                        }

                        ingredientIndex++;
                        break;
                }
            }
        }
        #endregion

        #region Clear Text Boxes
        private void ClearTextBoxes()
        {
            CraftAmountTextBox.Clear();
            ChanceTextBox.Clear();
            GoldTextBox.Clear();
            ToolTextBox.Clear();
            IngredientName1TextBox.Clear();
            IngredientAmount1TextBox.Clear();
            Quality1TextBox.Clear();
            IngredientName2TextBox.Clear();
            IngredientAmount2TextBox.Clear();
            Quality2TextBox.Clear();
            IngredientName3TextBox.Clear();
            IngredientAmount3TextBox.Clear();
            Quality3TextBox.Clear();
            IngredientName4TextBox.Clear();
            IngredientAmount4TextBox.Clear();
            Quality4TextBox.Clear();

            isModified = false;
        }

        private void ClearTextButton_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }
        #endregion

        #region Save Recipe
        private void SaveRecipe()
        {
            string recipeName = ItemTextBox.Text.Trim();

            if (string.IsNullOrEmpty(recipeName))
            {
                MessageBox.Show("配方名不能为空!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.Combine(currentDirectory, "Envir", "Recipe");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, recipeName + ".txt");

            bool isNewRecipe = string.IsNullOrEmpty(currentFilePath);

            if (isNewRecipe)
            {
                if (File.Exists(filePath))
                {
                    var confirmation = MessageBox.Show($"文件 \"{recipeName}.txt\" 已存在，是否覆盖?", "确认覆盖", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmation == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            else
            {
                filePath = currentFilePath;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("[Recipe]");
                    writer.WriteLine($"Amount {CraftAmountTextBox.Text}");
                    writer.WriteLine($"Chance {ChanceTextBox.Text}");
                    writer.WriteLine($"Gold {GoldTextBox.Text}");
                    writer.WriteLine();

                    writer.WriteLine("[Tools]");
                    writer.WriteLine(ToolTextBox.Text);
                    writer.WriteLine();

                    writer.WriteLine("[Ingredients]");
                    if (!string.IsNullOrEmpty(IngredientName1TextBox.Text))
                    {
                        writer.WriteLine($"{IngredientName1TextBox.Text} {IngredientAmount1TextBox.Text} {Quality1TextBox.Text}");
                    }
                    if (!string.IsNullOrEmpty(IngredientName2TextBox.Text))
                    {
                        writer.WriteLine($"{IngredientName2TextBox.Text} {IngredientAmount2TextBox.Text} {Quality1TextBox.Text}");
                    }
                    if (!string.IsNullOrEmpty(IngredientName3TextBox.Text))
                    {
                        writer.WriteLine($"{IngredientName3TextBox.Text} {IngredientAmount3TextBox.Text} {Quality1TextBox.Text}");
                    }
                    if (!string.IsNullOrEmpty(IngredientName4TextBox.Text))
                    {
                        writer.WriteLine($"{IngredientName4TextBox.Text} {IngredientAmount4TextBox.Text} {Quality1TextBox.Text}");
                    }
                }

                isModified = false;

                currentFilePath = filePath;

                if (isNewRecipe)
                {
                    isModified = false;
                    LoadRecipes();
                    MessageBox.Show($"配方 \"{recipeName}.txt\" 已成功保存!", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配方时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveRecipeButton_Click(object sender, EventArgs e)
        {
            SaveRecipe();
        }
        #endregion

        #region Delete Recipe
        private void DeleteRecipeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath) || !File.Exists(currentFilePath)) return;

            var confirmation = MessageBox.Show("确定要删除这个配方吗？", "删除配方", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    RecipeList.Items.RemoveAt(RecipeList.SelectedIndex);

                    File.Delete(currentFilePath);

                    MessageBox.Show("配方已删除", "删除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (RecipeList.Items.Count > 0)
                    {
                        RecipeList.SelectedIndex = 0;
                    }

                    isModified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除配方时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Form Closing Event
        private void RecipeInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModified)
            {
                var result = MessageBox.Show("您有未保存的更改，是否保存？", "保存更改", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveRecipe();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Text Box Change Events
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (CraftAmountTextBox.Text != originalCraftAmount ||
                ChanceTextBox.Text != originalChance ||
                GoldTextBox.Text != originalGold ||
                ToolTextBox.Text != originalTool ||
                IngredientName1TextBox.Text != originalIngredientName1 ||
                IngredientAmount1TextBox.Text != originalIngredientAmount1 ||
                Quality1TextBox.Text != Quality1 ||
                IngredientName2TextBox.Text != originalIngredientName2 ||
                IngredientAmount2TextBox.Text != originalIngredientAmount2 ||
                Quality3TextBox.Text != Quality2 ||
                IngredientName3TextBox.Text != originalIngredientName3 ||
                IngredientAmount3TextBox.Text != originalIngredientAmount3 ||
                IngredientName4TextBox.Text != originalIngredientName4 ||
                IngredientAmount4TextBox.Text != originalIngredientAmount4 ||
                Quality4TextBox.Text != Quality4)
            {
                isModified = true;
            }
        }
        #endregion

        #region Item Text Box TextChanged
        private void ItemTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RecipeList.SelectedIndex == -1 || isUpdatingTextBox)
                return;

            string newDisplayName = ItemTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newDisplayName))
            {
                MessageBox.Show("文件名不能为空", "无效文件名", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newFileName = $"{newDisplayName}.txt";
            string oldDisplayName = RecipeList.SelectedItem.ToString().Substring(RecipeList.SelectedItem.ToString().IndexOf(' ') + 1).Trim();
            string oldFileName = $"{oldDisplayName}.txt";

            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Envir", "Recipe");
            string oldFilePath = Path.Combine(directoryPath, oldFileName);
            string newFilePath = Path.Combine(directoryPath, newFileName);

            if (oldFileName == newFileName)
                return;

            if (File.Exists(newFilePath))
            {
                ItemTextBox.Text = oldDisplayName;
                MessageBox.Show("该文件名已存在", "文件名冲突", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                isUpdatingTextBox = true;

                if (File.Exists(oldFilePath))
                {
                    File.Move(oldFilePath, newFilePath);
                    RecipeList.Items[RecipeList.SelectedIndex] = $"{RecipeList.SelectedIndex + 1}. {newDisplayName}";
                    currentFilePath = newFilePath;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"重命名文件时发生错误: {ex.Message}", "重命名错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isUpdatingTextBox = false;
            }
        }
        #endregion

        #region New Recipe Button Click
        private void NewRecipeButton_Click(object sender, EventArgs e)
        {
            isUpdatingTextBox = true;

            ClearTextBoxes();
            ItemTextBox.Text = string.Empty;
            RecipeList.ClearSelected();
            isModified = false;
            currentFilePath = string.Empty;
            ItemTextBox.Focus();

            isUpdatingTextBox = false;
        }
        #endregion

        #region ClearLoadRecipes
        private void LoadRecipes()
        {
            RecipeList.Items.Clear();

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.Combine(currentDirectory, "Envir", "Recipe");

            if (Directory.Exists(directoryPath))
            {
                string[] recipeFiles = Directory.GetFiles(directoryPath, "*.txt");

                for (int i = 0; i < recipeFiles.Length; i++)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(recipeFiles[i]);
                    string listItem = $"{i + 1}. {fileNameWithoutExtension}";
                    RecipeList.Items.Add(listItem);
                }
            }
            else
            {
                MessageBox.Show("配方目录不存在", "目录错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}