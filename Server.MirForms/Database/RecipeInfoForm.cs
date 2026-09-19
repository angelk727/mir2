namespace Server.Database
{
    public partial class RecipeInfoForm : Form
    {
        private string currentFilePath;
        private bool isModified;
        private bool _ignoreItemSelectionEvent;
        private bool _loadingRecipe;
        private readonly List<string> _itemNames = new List<string>();

        private string RecipeDirectory
        {
            get
            {
                return Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Envir",
                    "Recipe");
            }
        }

        public RecipeInfoForm()
        {
            InitializeComponent();

            CraftAmountTextBox.TextChanged += RecipeControlChanged;
            ChanceTextBox.TextChanged += RecipeControlChanged;
            GoldTextBox.TextChanged += RecipeControlChanged;

            Tool1ComboBox.SelectedIndexChanged += RecipeControlChanged;
            Tool2ComboBox.SelectedIndexChanged += RecipeControlChanged;
            Tool3ComboBox.SelectedIndexChanged += RecipeControlChanged;

            IngredientName1ComboBox.SelectedIndexChanged += RecipeControlChanged;
            IngredientName2ComboBox.SelectedIndexChanged += RecipeControlChanged;
            IngredientName3ComboBox.SelectedIndexChanged += RecipeControlChanged;
            IngredientName4ComboBox.SelectedIndexChanged += RecipeControlChanged;
            IngredientName5ComboBox.SelectedIndexChanged += RecipeControlChanged;
            IngredientName6ComboBox.SelectedIndexChanged += RecipeControlChanged;

            IngredientAmount1TextBox.TextChanged += RecipeControlChanged;
            IngredientAmount2TextBox.TextChanged += RecipeControlChanged;
            IngredientAmount3TextBox.TextChanged += RecipeControlChanged;
            IngredientAmount4TextBox.TextChanged += RecipeControlChanged;
            IngredientAmount5TextBox.TextChanged += RecipeControlChanged;
            IngredientAmount6TextBox.TextChanged += RecipeControlChanged;

            IngredientDura1TextBox.TextChanged += RecipeControlChanged;
            IngredientDura2TextBox.TextChanged += RecipeControlChanged;
            IngredientDura3TextBox.TextChanged += RecipeControlChanged;
            IngredientDura4TextBox.TextChanged += RecipeControlChanged;
            IngredientDura5TextBox.TextChanged += RecipeControlChanged;
            IngredientDura6TextBox.TextChanged += RecipeControlChanged;
        }

        private void RecipeInfoForm_Load(object sender, EventArgs e)
        {
            _loadingRecipe = true;

            try
            {
                currentFilePath = null;
                isModified = false;

                EnsureRecipeDirectory();
                LoadItemNames();
                LoadItemsIntoComboBox();
                LoadRecipeList();
                ClearRecipeControls();
                ItemComboBox.SelectedIndex = -1;
                UpdateRecipeCount();
            }
            finally
            {
                _loadingRecipe = false;
            }
        }

        private bool EnsureRecipeDirectory()
        {
            if (Directory.Exists(RecipeDirectory))
                return true;

            try
            {
                Directory.CreateDirectory(RecipeDirectory);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"无法创建配方目录。\n\n错误信息：{ex.Message}",
                    "目录错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        private void LoadItemNames()
        {
            _itemNames.Clear();

            if (SMain.EditEnvir == null)
                return;

            if (SMain.EditEnvir.ItemInfoList == null)
                return;

            foreach (var item in SMain.EditEnvir.ItemInfoList)
            {
                if (item == null)
                    continue;

                if (string.IsNullOrWhiteSpace(item.Name))
                    continue;

                _itemNames.Add(item.Name);
            }
        }

        private void LoadItemsIntoComboBox()
        {
            ItemComboBox.BeginUpdate();

            try
            {
                ItemComboBox.Items.Clear();

                foreach (string itemName in _itemNames)
                    ItemComboBox.Items.Add(itemName);

                ItemComboBox.SelectedIndex = -1;
            }
            finally
            {
                ItemComboBox.EndUpdate();
            }
        }

        private void LoadRecipeList()
        {
            RecipeList.BeginUpdate();

            try
            {
                RecipeList.Items.Clear();

                if (!Directory.Exists(RecipeDirectory))
                    return;

                string[] recipeFiles = Directory.GetFiles(
                    RecipeDirectory,
                    "*.txt",
                    SearchOption.TopDirectoryOnly);

                Array.Sort(recipeFiles, StringComparer.OrdinalIgnoreCase);

                foreach (string recipeFile in recipeFiles)
                {
                    string recipeName = Path.GetFileNameWithoutExtension(recipeFile);

                    if (string.IsNullOrWhiteSpace(recipeName))
                        continue;

                    RecipeList.Items.Add(recipeName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"读取配方列表失败。\n\n错误信息：{ex.Message}",
                    "读取失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                RecipeList.EndUpdate();
            }
        }

        private void UpdateRecipeCount()
        {
            int recipeCount = RecipeList.Items.Count;
            RecipeCountLabel.Text = $"配方总数：{recipeCount}";
        }

        private void RecipeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingRecipe)
                return;

            if (RecipeList.SelectedIndex < 0)
                return;

            if (RecipeList.SelectedItem == null)
                return;

            string recipeName = RecipeList.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(recipeName))
                return;

            LoadRecipe(recipeName);
        }

        private void LoadRecipe(string recipeName)
        {
            if (string.IsNullOrWhiteSpace(recipeName))
                return;

            string filePath = Path.Combine(
                RecipeDirectory,
                recipeName + ".txt");

            if (!File.Exists(filePath))
            {
                MessageBox.Show(
                    "选择的配方文件不存在。",
                    "文件错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                _loadingRecipe = true;

                string[] fileLines = File.ReadAllLines(filePath);

                ClearRecipeControls();
                ParseAndDisplayRecipe(fileLines);

                currentFilePath = filePath;
                isModified = false;

                SetItemComboBoxSelection(recipeName);
            }
            catch (Exception ex)
            {
                currentFilePath = null;
                isModified = false;

                MessageBox.Show(
                    $"读取配方失败。\n\n错误信息：{ex.Message}",
                    "读取失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _loadingRecipe = false;
            }
        }

        private void SetItemComboBoxSelection(string recipeName)
        {
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                ItemComboBox.SelectedIndex = -1;
                return;
            }

            int index = ItemComboBox.FindStringExact(recipeName);

            _ignoreItemSelectionEvent = true;

            try
            {
                if (index >= 0)
                    ItemComboBox.SelectedIndex = index;
                else
                    ItemComboBox.SelectedIndex = -1;
            }
            finally
            {
                _ignoreItemSelectionEvent = false;
            }
        }

        private void ParseAndDisplayRecipe(string[] fileLines)
        {
            string amount = string.Empty;
            string chance = string.Empty;
            string gold = string.Empty;

            string[] tools = new string[3];
            string[] ingredientNames = new string[6];
            string[] ingredientAmounts = new string[6];
            string[] ingredientDurabilities = new string[6];

            bool inToolsSection = false;
            bool inIngredientsSection = false;

            int toolIndex = 0;
            int ingredientIndex = 0;

            foreach (string originalLine in fileLines)
            {
                if (string.IsNullOrWhiteSpace(originalLine))
                    continue;

                string line = originalLine.Trim();

                if (line.Equals("[Recipe]", StringComparison.OrdinalIgnoreCase))
                {
                    inToolsSection = false;
                    inIngredientsSection = false;
                    continue;
                }

                if (line.Equals("[Tools]", StringComparison.OrdinalIgnoreCase))
                {
                    inToolsSection = true;
                    inIngredientsSection = false;
                    continue;
                }

                if (line.Equals("[Ingredients]", StringComparison.OrdinalIgnoreCase))
                {
                    inToolsSection = false;
                    inIngredientsSection = true;
                    continue;
                }

                if (line.StartsWith("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    amount = GetValueFromLine(line);
                    continue;
                }

                if (line.StartsWith("Chance", StringComparison.OrdinalIgnoreCase))
                {
                    chance = GetValueFromLine(line);
                    continue;
                }

                if (line.StartsWith("Gold", StringComparison.OrdinalIgnoreCase))
                {
                    gold = GetValueFromLine(line);
                    continue;
                }

                if (inToolsSection)
                {
                    if (toolIndex >= tools.Length)
                        continue;

                    tools[toolIndex] = line;
                    toolIndex++;
                    continue;
                }

                if (inIngredientsSection)
                {
                    if (ingredientIndex >= ingredientNames.Length)
                        continue;

                    string[] parts = line.Split(
                        new[] { ' ', '\t' },
                        StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 0)
                        continue;

                    ingredientNames[ingredientIndex] = parts[0];

                    if (parts.Length >= 2)
                        ingredientAmounts[ingredientIndex] = parts[1];

                    if (parts.Length >= 3)
                        ingredientDurabilities[ingredientIndex] = parts[2];

                    ingredientIndex++;
                }
            }

            CraftAmountTextBox.Text = amount;
            ChanceTextBox.Text = chance;
            GoldTextBox.Text = gold;

            PopulateItemComboBox(Tool1ComboBox, tools[0]);
            PopulateItemComboBox(Tool2ComboBox, tools[1]);
            PopulateItemComboBox(Tool3ComboBox, tools[2]);

            PopulateItemComboBox(IngredientName1ComboBox, ingredientNames[0]);
            PopulateItemComboBox(IngredientName2ComboBox, ingredientNames[1]);
            PopulateItemComboBox(IngredientName3ComboBox, ingredientNames[2]);
            PopulateItemComboBox(IngredientName4ComboBox, ingredientNames[3]);
            PopulateItemComboBox(IngredientName5ComboBox, ingredientNames[4]);
            PopulateItemComboBox(IngredientName6ComboBox, ingredientNames[5]);

            IngredientAmount1TextBox.Text = ingredientAmounts[0];
            IngredientAmount2TextBox.Text = ingredientAmounts[1];
            IngredientAmount3TextBox.Text = ingredientAmounts[2];
            IngredientAmount4TextBox.Text = ingredientAmounts[3];
            IngredientAmount5TextBox.Text = ingredientAmounts[4];
            IngredientAmount6TextBox.Text = ingredientAmounts[5];

            IngredientDura1TextBox.Text = ingredientDurabilities[0];
            IngredientDura2TextBox.Text = ingredientDurabilities[1];
            IngredientDura3TextBox.Text = ingredientDurabilities[2];
            IngredientDura4TextBox.Text = ingredientDurabilities[3];
            IngredientDura5TextBox.Text = ingredientDurabilities[4];
            IngredientDura6TextBox.Text = ingredientDurabilities[5];
        }

        private string GetValueFromLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return string.Empty;

            int index = -1;

            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsWhiteSpace(line[i]))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
                return string.Empty;

            return line.Substring(index + 1).Trim();
        }

        private void PopulateItemComboBox(ComboBox comboBox, string selectedName)
        {
            comboBox.BeginUpdate();

            try
            {
                comboBox.Items.Clear();
                comboBox.Items.Add("None");

                foreach (string itemName in _itemNames)
                    comboBox.Items.Add(itemName);

                if (string.IsNullOrWhiteSpace(selectedName))
                {
                    comboBox.SelectedIndex = 0;
                    return;
                }

                int index = comboBox.FindStringExact(selectedName);

                if (index >= 0)
                    comboBox.SelectedIndex = index;
                else
                    comboBox.SelectedIndex = 0;
            }
            finally
            {
                comboBox.EndUpdate();
            }
        }

        private void RecipeControlChanged(object sender, EventArgs e)
        {
            if (_loadingRecipe)
                return;

            if (string.IsNullOrWhiteSpace(currentFilePath))
                return;

            isModified = true;
        }

        private void ClearRecipeControls()
        {
            CraftAmountTextBox.Text = string.Empty;
            ChanceTextBox.Text = string.Empty;
            GoldTextBox.Text = string.Empty;

            SetComboBoxIndex(Tool1ComboBox, 0);
            SetComboBoxIndex(Tool2ComboBox, 0);
            SetComboBoxIndex(Tool3ComboBox, 0);

            SetComboBoxIndex(IngredientName1ComboBox, 0);
            SetComboBoxIndex(IngredientName2ComboBox, 0);
            SetComboBoxIndex(IngredientName3ComboBox, 0);
            SetComboBoxIndex(IngredientName4ComboBox, 0);
            SetComboBoxIndex(IngredientName5ComboBox, 0);
            SetComboBoxIndex(IngredientName6ComboBox, 0);

            IngredientAmount1TextBox.Text = string.Empty;
            IngredientAmount2TextBox.Text = string.Empty;
            IngredientAmount3TextBox.Text = string.Empty;
            IngredientAmount4TextBox.Text = string.Empty;
            IngredientAmount5TextBox.Text = string.Empty;
            IngredientAmount6TextBox.Text = string.Empty;

            IngredientDura1TextBox.Text = string.Empty;
            IngredientDura2TextBox.Text = string.Empty;
            IngredientDura3TextBox.Text = string.Empty;
            IngredientDura4TextBox.Text = string.Empty;
            IngredientDura5TextBox.Text = string.Empty;
            IngredientDura6TextBox.Text = string.Empty;
        }

        private void SetComboBoxIndex(ComboBox comboBox, int index)
        {
            if (comboBox == null)
                return;

            if (comboBox.Items.Count == 0)
            {
                comboBox.SelectedIndex = -1;
                return;
            }

            if (index < 0 || index >= comboBox.Items.Count)
            {
                comboBox.SelectedIndex = -1;
                return;
            }

            comboBox.SelectedIndex = index;
        }

        private void SaveRecipe(bool showMessage)
        {
            if (string.IsNullOrWhiteSpace(currentFilePath))
                return;

            if (!File.Exists(currentFilePath))
            {
                MessageBox.Show(
                    "当前配方文件不存在，无法保存。",
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            string tempFilePath = currentFilePath + ".tmp";

            try
            {
                if (File.Exists(tempFilePath))
                    File.Delete(tempFilePath);

                using (StreamWriter writer = new StreamWriter(tempFilePath, false))
                {
                    writer.WriteLine("[Recipe]");
                    writer.WriteLine($"Amount {CraftAmountTextBox.Text}");
                    writer.WriteLine($"Chance {ChanceTextBox.Text}");
                    writer.WriteLine($"Gold {GoldTextBox.Text}");
                    writer.WriteLine();
                    writer.WriteLine("[Tools]");

                    WriteTool(writer, Tool1ComboBox);
                    WriteTool(writer, Tool2ComboBox);
                    WriteTool(writer, Tool3ComboBox);

                    writer.WriteLine();
                    writer.WriteLine("[Ingredients]");

                    WriteIngredient(writer, IngredientName1ComboBox, IngredientAmount1TextBox, IngredientDura1TextBox);
                    WriteIngredient(writer, IngredientName2ComboBox, IngredientAmount2TextBox, IngredientDura2TextBox);
                    WriteIngredient(writer, IngredientName3ComboBox, IngredientAmount3TextBox, IngredientDura3TextBox);
                    WriteIngredient(writer, IngredientName4ComboBox, IngredientAmount4TextBox, IngredientDura4TextBox);
                    WriteIngredient(writer, IngredientName5ComboBox, IngredientAmount5TextBox, IngredientDura5TextBox);
                    WriteIngredient(writer, IngredientName6ComboBox, IngredientAmount6TextBox, IngredientDura6TextBox);
                }

                File.Replace(tempFilePath, currentFilePath, null);

                isModified = false;

                if (showMessage)
                {
                    MessageBox.Show(
                        "配方保存成功。",
                        "保存成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (PlatformNotSupportedException)
            {
                try
                {
                    File.Copy(tempFilePath, currentFilePath, true);
                    DeleteTemporaryFile(tempFilePath);

                    isModified = false;

                    if (showMessage)
                    {
                        MessageBox.Show(
                            "配方保存成功。",
                            "保存成功",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    DeleteTemporaryFile(tempFilePath);

                    MessageBox.Show(
                        $"保存配方失败。\n\n错误信息：{ex.Message}",
                        "保存失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                DeleteTemporaryFile(tempFilePath);

                MessageBox.Show(
                    $"保存配方失败。\n\n错误信息：{ex.Message}",
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void WriteTool(StreamWriter writer, ComboBox comboBox)
        {
            if (comboBox == null)
                return;

            if (comboBox.SelectedItem == null)
                return;

            string toolName = comboBox.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(toolName))
                return;

            if (toolName.Equals("None", StringComparison.OrdinalIgnoreCase))
                return;

            writer.WriteLine(toolName);
        }

        private void WriteIngredient(StreamWriter writer, ComboBox nameComboBox, TextBox amountTextBox, TextBox duraTextBox)
        {
            if (nameComboBox == null)
                return;

            if (nameComboBox.SelectedItem == null)
                return;

            string ingredientName = nameComboBox.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(ingredientName))
                return;

            if (ingredientName.Equals("None", StringComparison.OrdinalIgnoreCase))
                return;

            string amount = string.Empty;
            string dura = string.Empty;

            if (amountTextBox != null)
                amount = amountTextBox.Text.Trim();

            if (duraTextBox != null)
                dura = duraTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(amount))
            {
                if (string.IsNullOrWhiteSpace(dura))
                {
                    writer.WriteLine(ingredientName);
                    return;
                }

                writer.WriteLine($"{ingredientName} {dura}");
                return;
            }

            if (string.IsNullOrWhiteSpace(dura))
            {
                writer.WriteLine($"{ingredientName} {amount}");
                return;
            }

            writer.WriteLine($"{ingredientName} {amount} {dura}");
        }

        private void RecipeInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isModified)
                return;

            SaveRecipe(false);
        }

        private void NewRecipeButton_Click(object sender, EventArgs e)
        {
            if (!EnsureRecipeDirectory())
                return;

            string newRecipeName = "NewRecipe";
            string newRecipePath = Path.Combine(
                RecipeDirectory,
                newRecipeName + ".txt");

            int counter = 1;

            while (File.Exists(newRecipePath))
            {
                newRecipeName = $"NewRecipe{counter}";
                newRecipePath = Path.Combine(
                    RecipeDirectory,
                    newRecipeName + ".txt");

                counter++;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(newRecipePath, false))
                {
                    writer.WriteLine("[Recipe]");
                    writer.WriteLine("Amount ");
                    writer.WriteLine("Chance ");
                    writer.WriteLine("Gold ");
                    writer.WriteLine();
                    writer.WriteLine("[Tools]");
                    writer.WriteLine();
                    writer.WriteLine("[Ingredients]");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"创建新配方失败。\n\n错误信息：{ex.Message}",
                    "创建失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            _loadingRecipe = true;

            try
            {
                currentFilePath = newRecipePath;
                isModified = true;

                ClearRecipeControls();

                if (ItemComboBox.Items.Count > 0)
                    ItemComboBox.SelectedIndex = 0;
                else
                    ItemComboBox.SelectedIndex = -1;

                RecipeList.Items.Add(newRecipeName);
                RecipeList.SelectedIndex = RecipeList.Items.Count - 1;
            }
            finally
            {
                _loadingRecipe = false;
            }

            UpdateRecipeCount();

            MessageBox.Show(
                $"新配方已创建：{newRecipeName}.txt",
                "创建成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreItemSelectionEvent)
                return;

            if (_loadingRecipe)
                return;

            if (string.IsNullOrWhiteSpace(currentFilePath))
                return;

            if (ItemComboBox.SelectedItem == null)
                return;

            string selectedItemName = ItemComboBox.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(selectedItemName))
                return;

            string oldRecipeName = Path.GetFileNameWithoutExtension(currentFilePath);

            if (oldRecipeName.Equals(
                selectedItemName,
                StringComparison.OrdinalIgnoreCase))
                return;

            int oldSelectedIndex = RecipeList.SelectedIndex;

            bool renamed = UpdateRecipeFileName(selectedItemName);

            if (!renamed)
            {
                _ignoreItemSelectionEvent = true;

                try
                {
                    SetItemComboBoxSelection(oldRecipeName);
                }
                finally
                {
                    _ignoreItemSelectionEvent = false;
                }

                return;
            }

            if (oldSelectedIndex >= 0 &&
                oldSelectedIndex < RecipeList.Items.Count)
            {
                _loadingRecipe = true;

                try
                {
                    RecipeList.Items[oldSelectedIndex] = selectedItemName;
                    RecipeList.SelectedIndex = oldSelectedIndex;
                }
                finally
                {
                    _loadingRecipe = false;
                }
            }

            isModified = true;
            UpdateRecipeCount();
        }

        private bool UpdateRecipeFileName(string newItemName)
        {
            if (string.IsNullOrWhiteSpace(currentFilePath))
                return false;

            if (string.IsNullOrWhiteSpace(newItemName))
                return false;

            string trimmedName = newItemName.Trim();

            if (!IsValidFileName(trimmedName))
            {
                MessageBox.Show(
                    "物品名称不能作为配方文件名。\n\n名称中包含 Windows 不允许使用的字符或保留名称。",
                    "重命名失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            string newFilePath = Path.Combine(
                RecipeDirectory,
                trimmedName + ".txt");

            string oldFullPath;
            string newFullPath;

            try
            {
                oldFullPath = Path.GetFullPath(currentFilePath);
                newFullPath = Path.GetFullPath(newFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"生成配方文件路径失败。\n\n错误信息：{ex.Message}",
                    "重命名失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            if (oldFullPath.Equals(
                newFullPath,
                StringComparison.OrdinalIgnoreCase))
                return true;

            if (!File.Exists(oldFullPath))
            {
                MessageBox.Show(
                    "原配方文件不存在，无法重命名。",
                    "重命名失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            if (File.Exists(newFullPath))
            {
                MessageBox.Show(
                    $"配方“{trimmedName}”已经存在，无法重命名。",
                    "重命名失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            try
            {
                File.Move(oldFullPath, newFullPath);
                currentFilePath = newFullPath;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"重命名配方文件失败。\n\n错误信息：{ex.Message}",
                    "重命名失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        private bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            char[] invalidCharacters = Path.GetInvalidFileNameChars();

            foreach (char character in invalidCharacters)
            {
                if (fileName.IndexOf(character) >= 0)
                    return false;
            }

            if (fileName.EndsWith("."))
                return false;

            if (fileName.EndsWith(" "))
                return false;

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            if (nameWithoutExtension.Equals("CON", StringComparison.OrdinalIgnoreCase))
                return false;

            if (nameWithoutExtension.Equals("PRN", StringComparison.OrdinalIgnoreCase))
                return false;

            if (nameWithoutExtension.Equals("AUX", StringComparison.OrdinalIgnoreCase))
                return false;

            if (nameWithoutExtension.Equals("NUL", StringComparison.OrdinalIgnoreCase))
                return false;

            if (nameWithoutExtension.Equals("CLOCK$", StringComparison.OrdinalIgnoreCase))
                return false;

            if (nameWithoutExtension.StartsWith("COM", StringComparison.OrdinalIgnoreCase))
            {
                string number = nameWithoutExtension.Substring(3);

                if (number == "1" ||
                    number == "2" ||
                    number == "3" ||
                    number == "4" ||
                    number == "5" ||
                    number == "6" ||
                    number == "7" ||
                    number == "8" ||
                    number == "9")
                    return false;
            }

            if (nameWithoutExtension.StartsWith("LPT", StringComparison.OrdinalIgnoreCase))
            {
                string number = nameWithoutExtension.Substring(3);

                if (number == "1" ||
                    number == "2" ||
                    number == "3" ||
                    number == "4" ||
                    number == "5" ||
                    number == "6" ||
                    number == "7" ||
                    number == "8" ||
                    number == "9")
                    return false;
            }

            return true;
        }

        private void OpenRecipeButton_Click(object sender, EventArgs e)
        {
            if (RecipeList.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "请先选择一个配方。",
                    "没有选择配方",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (RecipeList.SelectedItem == null)
                return;

            string selectedRecipeName = RecipeList.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(selectedRecipeName))
                return;

            string filePath = Path.Combine(
                RecipeDirectory,
                selectedRecipeName + ".txt");

            if (!File.Exists(filePath))
            {
                MessageBox.Show(
                    "选择的配方文件不存在。",
                    "文件错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"打开配方文件失败。\n\n错误信息：{ex.Message}",
                    "打开失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveRecipe(true);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (RecipeList.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "请先选择一个要删除的配方。",
                    "没有选择配方",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (RecipeList.SelectedItem == null)
                return;

            string selectedRecipeName = RecipeList.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(selectedRecipeName))
                return;

            string filePath = Path.Combine(
                RecipeDirectory,
                selectedRecipeName + ".txt");

            DialogResult result = MessageBox.Show(
                $"确定要删除配方“{selectedRecipeName}”吗？\n\n删除后无法通过此窗口恢复。",
                "确认删除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show(
                        "选择的配方文件不存在。",
                        "文件错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    LoadRecipeList();
                    UpdateRecipeCount();
                    return;
                }

                File.Delete(filePath);

                _loadingRecipe = true;

                try
                {
                    int selectedIndex = RecipeList.SelectedIndex;

                    if (selectedIndex >= 0 &&
                        selectedIndex < RecipeList.Items.Count)
                        RecipeList.Items.RemoveAt(selectedIndex);

                    RecipeList.SelectedIndex = -1;
                    ClearRecipeControls();
                    ItemComboBox.SelectedIndex = -1;
                    currentFilePath = null;
                    isModified = false;
                }
                finally
                {
                    _loadingRecipe = false;
                }

                UpdateRecipeCount();

                MessageBox.Show(
                    $"配方“{selectedRecipeName}.txt”已删除。",
                    "删除成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"删除配方失败。\n\n错误信息：{ex.Message}",
                    "删除失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DeleteTemporaryFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch
            {
            }
        }
    }
}