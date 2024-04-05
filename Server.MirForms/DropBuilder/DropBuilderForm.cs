using Server.MirEnvir;

namespace Server.MirForms.DropBuilder
{
    public class MonsterDropInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public partial class DropGenForm : Form
    {
        string Gold = "0", GoldOdds;

        List<DropItem>
            武器 = new List<DropItem>(),
            盔甲 = new List<DropItem>(),
            头盔 = new List<DropItem>(),
            项链 = new List<DropItem>(),
            手镯 = new List<DropItem>(),
            戒指 = new List<DropItem>(),
            护身符 = new List<DropItem>(),
            腰带 = new List<DropItem>(),
            靴子 = new List<DropItem>(),
            守护石 = new List<DropItem>(),
            照明物 = new List<DropItem>(),
            药水 = new List<DropItem>(),
            矿石 = new List<DropItem>(),
            肉 = new List<DropItem>(),
            工艺材料 = new List<DropItem>(),
            卷轴 = new List<DropItem>(),
            宝玉神珠 = new List<DropItem>(),
            坐骑 = new List<DropItem>(),
            技能书 = new List<DropItem>(),
            杂物 = new List<DropItem>(),
            特殊消耗品 = new List<DropItem>(),
            缰绳 = new List<DropItem>(),
            铃铛 = new List<DropItem>(),
            马鞍 = new List<DropItem>(),
            蝴蝶结 = new List<DropItem>(),
            面甲 = new List<DropItem>(),
            坐骑食物 = new List<DropItem>(),
            鱼钩 = new List<DropItem>(),
            鱼漂 = new List<DropItem>(),
            鱼饵 = new List<DropItem>(),
            探鱼器 = new List<DropItem>(),
            摇轮 = new List<DropItem>(),
            鱼 = new List<DropItem>(),
            任务物品 = new List<DropItem>(),
            觉醒物品 = new List<DropItem>(),
            灵物 = new List<DropItem>(),
            外形物品 = new List<DropItem>();

        List<DropItem>[] ItemLists;
        ListBox[] ItemListBoxes;

        public DropGenForm()
        {
            InitializeComponent();

            // Array of items
            ItemLists = new List<DropItem>[37]
            {
                武器,
                盔甲,
                头盔,
                项链,
                手镯,
                戒指,
                护身符,
                腰带,
                靴子,
                守护石,
                照明物,
                药水,
                矿石,
                肉,
                工艺材料,
                卷轴,
                宝玉神珠,
                坐骑,
                技能书,
                杂物,
                特殊消耗品,
                缰绳,
                铃铛,
                马鞍,
                蝴蝶结,
                面甲,
                坐骑食物,
                鱼钩,
                鱼漂,
                鱼饵,
                探鱼器,
                摇轮,
                鱼,
                任务物品,
                觉醒物品,
                灵物,
                外形物品
            };

            // Array of item list boxes
            ItemListBoxes = new ListBox[37]
            {
                listBoxWeapon,
                listBoxArmour,
                listBoxHelmet,
                listBoxNecklace,
                listBoxBracelet,
                listBoxRing,
                listBoxAmulet,
                listBoxBelt,
                listBoxBoot,
                listBoxStone,
                listBoxTorch,
                listBoxPotion,
                listBoxOre,
                listBoxMeat,
                listBoxCraftingMaterial,
                listBoxScroll,
                listBoxGem,
                listBoxMount,
                listBoxBook,
                listBoxNothing,
                listBoxScript,
                listBoxReins,
                listBoxBells,
                listBoxSaddle,
                listBoxRibbon,
                listBoxMask,
                listBoxFood,
                listBoxHook,
                listBoxFloat,
                listBoxBait,
                listBoxFinder,
                listBoxReel,
                listBoxFish,
                listBoxQuest,
                listBoxAwakening,
                listBoxPets,
                listBoxTransform
            };

            // Add monsters to list
            for (int i = 0; i < Envir.MonsterInfoList.Count; i++)
            {
                listBoxMonsters.Items.Add(new MonsterDropInfo { Name = Envir.MonsterInfoList[i].Name, Path = Envir.MonsterInfoList[i].DropPath });
            }

            tabControlSeperateItems_SelectedIndexChanged(tabControlSeperateItems, null);
            listBoxMonsters.SelectedIndex = 0;
            labelMonsterList.Text = $"怪物总数: {Envir.MonsterInfoList.Count}";
        }

        // Gets server data
        public Envir Envir => SMain.EditEnvir;

        // Updates the drop file text
        private void UpdateDropFile()
        {
            textBoxDropList.Clear();

            textBoxDropList.Text += $";Gold{Environment.NewLine}";
            if (Gold != "0")
            {
                textBoxDropList.Text += $"1/{GoldOdds} Gold {Gold}{Environment.NewLine}";
                textBoxGoldAmount.Text = Gold;
                textBoxGoldOdds.Text = GoldOdds;
            }
            else
            {
                textBoxGoldAmount.Text = "0";
                textBoxGoldOdds.Text = string.Empty;
            }
                
            textBoxDropList.Text += string.Format("{0};武器{0}", Environment.NewLine);
            for (int i = 0; i < 武器.Count; i++)
                textBoxDropList.Text += $"{武器[i].Odds} {武器[i].Name} {武器[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};盔甲{0}", Environment.NewLine);
            for (int i = 0; i < 盔甲.Count; i++)
                textBoxDropList.Text += $"{盔甲[i].Odds} {盔甲[i].Name} {盔甲[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};头盔{0}", Environment.NewLine);
            for (int i = 0; i < 头盔.Count; i++)
                textBoxDropList.Text += $"{头盔[i].Odds} {头盔[i].Name} {头盔[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};项链{0}", Environment.NewLine);
            for (int i = 0; i < 项链.Count; i++)
                textBoxDropList.Text += $"{项链[i].Odds} {项链[i].Name} {项链[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};手镯{0}", Environment.NewLine);
            for (int i = 0; i < 手镯.Count; i++)
                textBoxDropList.Text += $"{手镯[i].Odds} {手镯[i].Name} {手镯[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};戒指{0}", Environment.NewLine);
            for (int i = 0; i < 戒指.Count; i++)
                textBoxDropList.Text += $"{戒指[i].Odds} {戒指[i].Name} {戒指[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};护身符{0}", Environment.NewLine);
            for (int i = 0; i < 护身符.Count; i++)
                textBoxDropList.Text += $"{护身符[i].Odds} {护身符[i].Name} {护身符[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};腰带{0}", Environment.NewLine);
            for (int i = 0; i < 腰带.Count; i++)
                textBoxDropList.Text += $"{腰带[i].Odds} {腰带[i].Name} {腰带[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};靴子{0}", Environment.NewLine);
            for (int i = 0; i < 靴子.Count; i++)
                textBoxDropList.Text += $"{靴子[i].Odds} {靴子[i].Name} {靴子[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};守护石{0}", Environment.NewLine);
            for (int i = 0; i < 守护石.Count; i++)
                textBoxDropList.Text += $"{守护石[i].Odds} {守护石[i].Name} {守护石[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};照明物{0}", Environment.NewLine);
            for (int i = 0; i < 照明物.Count; i++)
                textBoxDropList.Text += $"{照明物[i].Odds} {照明物[i].Name} {照明物[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};药水{0}", Environment.NewLine);
            for (int i = 0; i < 药水.Count; i++)
                textBoxDropList.Text += $"{药水[i].Odds} {药水[i].Name} {药水[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};矿石{0}", Environment.NewLine);
            for (int i = 0; i < 矿石.Count; i++)
                textBoxDropList.Text += $"{矿石[i].Odds} {矿石[i].Name} {矿石[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};肉{0}", Environment.NewLine);
            for (int i = 0; i < 肉.Count; i++)
                textBoxDropList.Text += $"{肉[i].Odds} {肉[i].Name} {肉[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};工艺材料{0}", Environment.NewLine);
            for (int i = 0; i < 工艺材料.Count; i++)
                textBoxDropList.Text += $"{工艺材料[i].Odds} {工艺材料[i].Name} {工艺材料[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};卷轴{0}", Environment.NewLine);
            for (int i = 0; i < 卷轴.Count; i++)
                textBoxDropList.Text += $"{卷轴[i].Odds} {卷轴[i].Name} {卷轴[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};宝玉神珠{0}", Environment.NewLine);
            for (int i = 0; i < 宝玉神珠.Count; i++)
                textBoxDropList.Text += $"{宝玉神珠[i].Odds} {宝玉神珠[i].Name} {宝玉神珠[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};坐骑{0}", Environment.NewLine);
            for (int i = 0; i < 坐骑.Count; i++)
                textBoxDropList.Text += $"{坐骑[i].Odds} {坐骑[i].Name} {坐骑[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};技能书{0}", Environment.NewLine);
            for (int i = 0; i < 技能书.Count; i++)
                textBoxDropList.Text += $"{技能书[i].Odds} {技能书[i].Name} {技能书[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};杂物{0}", Environment.NewLine);
            for (int i = 0; i < 杂物.Count; i++)
                textBoxDropList.Text += $"{杂物[i].Odds} {杂物[i].Name} {杂物[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};特殊消耗品{0}", Environment.NewLine);
            for (int i = 0; i < 特殊消耗品.Count; i++)
                textBoxDropList.Text += $"{特殊消耗品[i].Odds} {特殊消耗品[i].Name} {特殊消耗品[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};缰绳{0}", Environment.NewLine);
            for (int i = 0; i < 缰绳.Count; i++)
                textBoxDropList.Text += $"{缰绳[i].Odds} {缰绳[i].Name} {缰绳[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};铃铛{0}", Environment.NewLine);
            for (int i = 0; i < 铃铛.Count; i++)
                textBoxDropList.Text += $"{铃铛[i].Odds} {铃铛[i].Name} {铃铛[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};马鞍{0}", Environment.NewLine);
            for (int i = 0; i < 马鞍.Count; i++)
                textBoxDropList.Text += $"{马鞍[i].Odds} {马鞍[i].Name} {马鞍[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};蝴蝶结{0}", Environment.NewLine);
            for (int i = 0; i < 蝴蝶结.Count; i++)
                textBoxDropList.Text += $"{蝴蝶结[i].Odds} {蝴蝶结[i].Name} {蝴蝶结[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};面甲{0}", Environment.NewLine);
            for (int i = 0; i < 面甲.Count; i++)
                textBoxDropList.Text += $"{面甲[i].Odds} {面甲[i].Name} {面甲[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};坐骑食物{0}", Environment.NewLine);
            for (int i = 0; i < 坐骑食物.Count; i++)
                textBoxDropList.Text += $"{坐骑食物[i].Odds} {坐骑食物[i].Name} {坐骑食物[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};鱼钩{0}", Environment.NewLine);
            for (int i = 0; i < 鱼钩.Count; i++)
                textBoxDropList.Text += $"{鱼钩[i].Odds} {鱼钩[i].Name} {鱼钩[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};鱼漂{0}", Environment.NewLine);
            for (int i = 0; i < 鱼漂.Count; i++)
                textBoxDropList.Text += $"{鱼漂[i].Odds} {鱼漂[i].Name} {鱼漂[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};鱼饵{0}", Environment.NewLine);
            for (int i = 0; i < 鱼饵.Count; i++)
                textBoxDropList.Text += $"{鱼饵[i].Odds} {鱼饵[i].Name} {鱼饵[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};探鱼器{0}", Environment.NewLine);
            for (int i = 0; i < 探鱼器.Count; i++)
                textBoxDropList.Text += $"{探鱼器[i].Odds} {探鱼器[i].Name} {探鱼器[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};摇轮{0}", Environment.NewLine);
            for (int i = 0; i < 摇轮.Count; i++)
                textBoxDropList.Text += $"{摇轮[i].Odds} {摇轮[i].Name} {摇轮[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};鱼{0}", Environment.NewLine);
            for (int i = 0; i < 鱼.Count; i++)
                textBoxDropList.Text += $"{鱼[i].Odds} {鱼[i].Name} {鱼[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};任务物品{0}", Environment.NewLine);
            for (int i = 0; i < 任务物品.Count; i++)
                textBoxDropList.Text += $"{任务物品[i].Odds} {任务物品[i].Name} {任务物品[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};觉醒物品{0}", Environment.NewLine);
            for (int i = 0; i < 觉醒物品.Count; i++)
                textBoxDropList.Text += $"{觉醒物品[i].Odds} {觉醒物品[i].Name} {觉醒物品[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};灵物{0}", Environment.NewLine);
            for (int i = 0; i < 灵物.Count; i++)
                textBoxDropList.Text += $"{灵物[i].Odds} {灵物[i].Name} {灵物[i].Quest}{Environment.NewLine}";

            textBoxDropList.Text += string.Format("{0};外形物品{0}", Environment.NewLine);
            for (int i = 0; i < 外形物品.Count; i++)
                textBoxDropList.Text += $"{外形物品[i].Odds} {外形物品[i].Name} {外形物品[i].Quest}{Environment.NewLine}";

            SaveDropFile();
        }

        // Item tab change, draw appropriate items
        private void tabControlSeperateItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl Tab = (TabControl)sender;

            foreach (var list in ItemListBoxes)
                list.Items.Clear();

            ListBox TempListBox = new ListBox();
            for (int i = 0; i < Envir.ItemInfoList.Count; i++)
            {
                if (Envir.ItemInfoList[i].Type.ToString() == Tab.SelectedTab.Tag.ToString())
                {
                    try
                    {
                        if (textBoxMinLevel.Text == string.Empty || textBoxMaxLevel.Text == string.Empty)                            
                            TempListBox.Items.Add(Envir.ItemInfoList[i].Name);
                        else if (Envir.ItemInfoList[i].RequiredAmount >= int.Parse(textBoxMinLevel.Text) &
                            Envir.ItemInfoList[i].RequiredAmount <= int.Parse(textBoxMaxLevel.Text))
                            TempListBox.Items.Add(Envir.ItemInfoList[i].Name);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("无法读取等级过滤");
                        break;
                    }
                }
            }

            switch (Tab.SelectedTab.Tag.ToString())
            {
                case "武器":
                    listBoxWeapon.Items.AddRange(TempListBox.Items);
                    break;
                case "盔甲":
                    listBoxArmour.Items.AddRange(TempListBox.Items);
                    break;
                case "头盔":
                    listBoxHelmet.Items.AddRange(TempListBox.Items);
                    break;
                case "项链":
                    listBoxNecklace.Items.AddRange(TempListBox.Items);
                    break;
                case "手镯":
                    listBoxBracelet.Items.AddRange(TempListBox.Items);
                    break;
                case "戒指":
                    listBoxRing.Items.AddRange(TempListBox.Items);
                    break;
                case "护身符":
                    listBoxAmulet.Items.AddRange(TempListBox.Items);
                    break;
                case "腰带":
                    listBoxBelt.Items.AddRange(TempListBox.Items);
                    break;
                case "靴子":
                    listBoxBoot.Items.AddRange(TempListBox.Items);
                    break;
                case "守护石":
                    listBoxStone.Items.AddRange(TempListBox.Items);
                    break;
                case "照明物":
                    listBoxTorch.Items.AddRange(TempListBox.Items);
                    break;
                case "药水":
                    listBoxPotion.Items.AddRange(TempListBox.Items);
                    break;
                case "矿石":
                    listBoxOre.Items.AddRange(TempListBox.Items);
                    break;
                case "肉":
                    listBoxMeat.Items.AddRange(TempListBox.Items);
                    break;
                case "工艺材料":
                    listBoxCraftingMaterial.Items.AddRange(TempListBox.Items);
                    break;
                case "卷轴":
                    listBoxScroll.Items.AddRange(TempListBox.Items);
                    break;
                case "宝玉神珠":
                    listBoxGem.Items.AddRange(TempListBox.Items);
                    break;
                case "坐骑":
                    listBoxMount.Items.AddRange(TempListBox.Items);
                    break;
                case "技能书":
                    listBoxBook.Items.AddRange(TempListBox.Items);
                    break;
                case "杂物":
                    listBoxNothing.Items.AddRange(TempListBox.Items);
                    break;
                case "特殊消耗品":
                    listBoxScript.Items.AddRange(TempListBox.Items);
                    break;
                case "缰绳":
                    listBoxReins.Items.AddRange(TempListBox.Items);
                    break;
                case "铃铛":
                    listBoxBells.Items.AddRange(TempListBox.Items);
                    break;
                case "马鞍":
                    listBoxSaddle.Items.AddRange(TempListBox.Items);
                    break;
                case "蝴蝶结":
                    listBoxRibbon.Items.AddRange(TempListBox.Items);
                    break;
                case "面甲":
                    listBoxMask.Items.AddRange(TempListBox.Items);
                    break;
                case "坐骑食物":
                    listBoxFood.Items.AddRange(TempListBox.Items);
                    break;
                case "鱼钩":
                    listBoxHook.Items.AddRange(TempListBox.Items);
                    break;
                case "鱼漂":
                    listBoxFloat.Items.AddRange(TempListBox.Items);
                    break;
                case "鱼饵":
                    listBoxBait.Items.AddRange(TempListBox.Items);
                    break;
                case "探鱼器":
                    listBoxFinder.Items.AddRange(TempListBox.Items);
                    break;
                case "摇轮":
                    listBoxReel.Items.AddRange(TempListBox.Items);
                    break;
                case "鱼":
                    listBoxFish.Items.AddRange(TempListBox.Items);
                    break;
                case "任务物品":
                    listBoxQuest.Items.AddRange(TempListBox.Items);
                    break;
                case "觉醒物品":
                    listBoxAwakening.Items.AddRange(TempListBox.Items);
                    break;
                case "灵物":
                    listBoxPets.Items.AddRange(TempListBox.Items);
                    break;
                case "外形物品":
                    listBoxTransform.Items.AddRange(TempListBox.Items);
                    break;
            }
        }
        
        // Update the results to show them filtered
        private void FilterValueChange(object sender, EventArgs e)
        {
            tabControlSeperateItems_SelectedIndexChanged(tabControlSeperateItems, null);
        }

        // Add the item to the drop list
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int dropChance;

            int.TryParse(textBoxItemOdds.Text, out dropChance);

            if (dropChance < 1) dropChance = 1;

            string quest = QuestOnlyCheckBox.Checked ? "Q" : "";

            try
            {
                switch (tabControlSeperateItems.SelectedTab.Tag.ToString())
                {
                    case "武器":
                        武器.Add(new DropItem { Name = listBoxWeapon.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "盔甲":
                        盔甲.Add(new DropItem { Name = listBoxArmour.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "头盔":
                        头盔.Add(new DropItem { Name = listBoxHelmet.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "项链":
                        项链.Add(new DropItem { Name = listBoxNecklace.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "手镯":
                        手镯.Add(new DropItem { Name = listBoxBracelet.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "戒指":
                        戒指.Add(new DropItem { Name = listBoxRing.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "护身符":
                        护身符.Add(new DropItem { Name = listBoxAmulet.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "腰带":
                        腰带.Add(new DropItem { Name = listBoxBelt.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "靴子":
                        靴子.Add(new DropItem { Name = listBoxBoot.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "守护石":
                        守护石.Add(new DropItem { Name = listBoxStone.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "照明物":
                        照明物.Add(new DropItem { Name = listBoxTorch.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "药水":
                        药水.Add(new DropItem { Name = listBoxPotion.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "矿石":
                        矿石.Add(new DropItem { Name = listBoxOre.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "肉":
                        肉.Add(new DropItem { Name = listBoxMeat.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "工艺材料":
                        工艺材料.Add(new DropItem { Name = listBoxCraftingMaterial.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}"
                        });
                        break;
                    case "卷轴":
                        卷轴.Add(new DropItem { Name = listBoxScroll.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "宝玉神珠":
                        宝玉神珠.Add(new DropItem { Name = listBoxGem.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "坐骑":
                        坐骑.Add(new DropItem { Name = listBoxMount.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "技能书":
                        技能书.Add(new DropItem { Name = listBoxBook.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "杂物":
                        杂物.Add(new DropItem { Name = listBoxNothing.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "特殊消耗品":
                        特殊消耗品.Add(new DropItem { Name = listBoxScript.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "缰绳":
                        缰绳.Add(new DropItem { Name = listBoxReins.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "铃铛":
                        铃铛.Add(new DropItem { Name = listBoxBells.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "马鞍":
                        马鞍.Add(new DropItem { Name = listBoxSaddle.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "蝴蝶结":
                        蝴蝶结.Add(new DropItem { Name = listBoxRibbon.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "面甲":
                        面甲.Add(new DropItem { Name = listBoxMask.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "坐骑食物":
                        坐骑食物.Add(new DropItem { Name = listBoxFood.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "鱼钩":
                        鱼钩.Add(new DropItem { Name = listBoxHook.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "鱼漂":
                        鱼漂.Add(new DropItem { Name = listBoxFloat.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "鱼饵":
                        鱼饵.Add(new DropItem { Name = listBoxBait.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "探鱼器":
                        探鱼器.Add(new DropItem { Name = listBoxFinder.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "摇轮":
                        摇轮.Add(new DropItem { Name = listBoxReel.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "鱼":
                        鱼.Add(new DropItem { Name = listBoxFish.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "任务物品":
                        任务物品.Add(new DropItem { Name = listBoxQuest.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "觉醒物品":
                        觉醒物品.Add(new DropItem { Name = listBoxAwakening.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "灵物":
                        灵物.Add(new DropItem { Name = listBoxPets.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                    case "外形物品":
                        外形物品.Add(new DropItem { Name = listBoxTransform.SelectedItem.ToString().Replace(" ", string.Empty), Odds =
                            $"1/{dropChance}", Quest = quest });
                        break;
                }

                UpdateDropFile();
            }
            catch
            {
                //No item selected when trying to add an item to the drop
            }
        }

        // Choose another monster.
        private void listBoxMonsters_SelectedItemChanged(object sender, EventArgs e)
        {
            // Empty List<DropItem>'s
            foreach (var item in ItemLists)
                item.Clear();

            LoadDropFile(false);
            UpdateDropFile();

            textBoxMinLevel.Text = string.Empty;
            textBoxMaxLevel.Text = string.Empty;
            checkBoxCap.Checked = false;

            labelMobLevel.Text =
                $"当前编辑: {((MonsterDropInfo)listBoxMonsters.SelectedItem).Name} - 等级: {Envir.MonsterInfoList[listBoxMonsters.SelectedIndices[0]].Level}";
        }

        public string GetPathOfSelectedItem()
        {
            var selectedItem = (MonsterDropInfo)listBoxMonsters.SelectedItem;

            if (selectedItem == null) return null;

            var path = "";

            if (string.IsNullOrEmpty(selectedItem.Path))
            {
                path = Path.Combine(Settings.DropPath, $"{selectedItem.Name}.txt");
            }
            else
            {
                path = Path.Combine(Settings.DropPath, selectedItem.Path + ".txt");
            }

            return path;

        }

        // Load the monster.txt drop file.
        private void LoadDropFile(bool edit)
        {
            var lines = (edit == false) ? File.ReadAllLines(GetPathOfSelectedItem()) : textBoxDropList.Lines;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(";Gold"))
                {
                    if (lines[i + 1].StartsWith("1/"))
                    {
                        var workingLine = lines[i + 1].Split(' ');
                        GoldOdds = workingLine[0].Remove(0,2);
                        Gold = workingLine[2];
                        break;
                    }
                    else
                    {
                        GoldOdds = "0";
                        Gold = "0";
                    }
                }
            }

            string[] Headers = new string[37]
            {
            ";武器",
            ";盔甲",
            ";头盔",
            ";项链",
            ";手镯",
            ";戒指",
            ";护身符",
            ";腰带",
            ";靴子",
            ";守护石",
            ";照明物",
            ";药水",
            ";矿石",
            ";肉",
            ";工艺材料",
            ";卷轴",
            ";宝玉神珠",
            ";坐骑",
            ";技能书",
            ";杂物",
            ";特殊消耗品",
            ";缰绳",
            ";铃铛",
            ";马鞍",
            ";蝴蝶结",
            ";面甲",
            ";坐骑食物",
            ";鱼钩",
            ";鱼漂",
            ";鱼饵",
            ";探鱼器",
            ";摇轮",
            ";鱼",
            ";任务物品",
            ";觉醒物品",
            ";灵物",
            ";外形物品"
            };

            for (int i = 0; i < Headers.Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (lines[j].StartsWith(Headers[i]))
                    {
                        for (int k = j + 1; k < lines.Length; k++)
                        {
                            if (lines[k].StartsWith(";")) break;

                            var workingLine = lines[k].Split(' ');
                            if (workingLine.Length < 2) continue;

                            var quest = "";

                            if(workingLine.Length > 2 && workingLine[2] == "Q")
                            {
                                quest = workingLine[2];
                            }

                            DropItem newDropItem = new DropItem { Odds = workingLine[0], Name = workingLine[1], Quest = quest };
                            switch (i)
                            {
                                case 0:
                                    武器.Add(newDropItem);
                                    break;
                                case 1:
                                    盔甲.Add(newDropItem);
                                    break;
                                case 2:
                                    头盔.Add(newDropItem);
                                    break;
                                case 3:
                                    项链.Add(newDropItem);
                                    break;
                                case 4:
                                    手镯.Add(newDropItem);
                                    break;
                                case 5:
                                    戒指.Add(newDropItem);
                                    break;
                                case 6:
                                    护身符.Add(newDropItem);
                                    break;
                                case 7:
                                    腰带.Add(newDropItem);
                                    break;
                                case 8:
                                    靴子.Add(newDropItem);
                                    break;
                                case 9:
                                    守护石.Add(newDropItem);
                                    break;
                                case 10:
                                    照明物.Add(newDropItem);
                                    break;
                                case 11:
                                    药水.Add(newDropItem);
                                    break;
                                case 12:
                                    矿石.Add(newDropItem);
                                    break;
                                case 13:
                                    肉.Add(newDropItem);
                                    break;
                                case 14:
                                    工艺材料.Add(newDropItem);
                                    break;
                                case 15:
                                    卷轴.Add(newDropItem);
                                    break;
                                case 16:
                                    宝玉神珠.Add(newDropItem);
                                    break;
                                case 17:
                                    坐骑.Add(newDropItem);
                                    break;
                                case 18:
                                    技能书.Add(newDropItem);
                                    break;
                                case 19:
                                    杂物.Add(newDropItem);
                                    break;
                                case 20:
                                    特殊消耗品.Add(newDropItem);
                                    break;
                                case 21:
                                    缰绳.Add(newDropItem);
                                    break;
                                case 22:
                                    铃铛.Add(newDropItem);
                                    break;
                                case 23:
                                    马鞍.Add(newDropItem);
                                    break;
                                case 24:
                                    蝴蝶结.Add(newDropItem);
                                    break;
                                case 25:
                                    面甲.Add(newDropItem);
                                    break;
                                case 26:
                                    坐骑食物.Add(newDropItem);
                                    break;
                                case 27:
                                    鱼钩.Add(newDropItem);
                                    break;
                                case 28:
                                    鱼漂.Add(newDropItem);
                                    break;
                                case 29:
                                    鱼饵.Add(newDropItem);
                                    break;
                                case 30:
                                    探鱼器.Add(newDropItem);
                                    break;
                                case 31:
                                    摇轮.Add(newDropItem);
                                    break;
                                case 32:
                                    鱼.Add(newDropItem);
                                    break;
                                case 33:
                                    任务物品.Add(newDropItem);
                                    break;
                                case 34:
                                    觉醒物品.Add(newDropItem);
                                    break;
                                case 35:
                                    灵物.Add(newDropItem);
                                    break;
                                case 36:
                                    外形物品.Add(newDropItem);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }

        // Save the monster.txt drop file
        private void SaveDropFile()
        {
            var dropFile = GetPathOfSelectedItem();

            if (dropFile == null) return;

            using (FileStream fs = new FileStream(dropFile, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (string line in textBoxDropList.Lines)
                        sw.Write(line + sw.NewLine);
                }
            }
        }

        //Edit gold amount/odds
        private void GoldDropChange(object sender, EventArgs e)
        {
            if (textBoxGoldOdds.Text != GoldOdds || textBoxGoldAmount.Text != Gold)
                buttonUpdateGold.Enabled = true;
            else
                buttonUpdateGold.Enabled = false;
        }

        //Switch to Edit mode
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Text == "接受")
            {
                textBoxDropList.ReadOnly = true;
                textBoxDropList.BackColor = System.Drawing.Color.Cornsilk;
                buttonEdit.Text = "编辑掉落文件";
                //buttonEdit.Image = Properties.Resources.edit;

                // Empty List<DropItem>'s
                foreach (var item in ItemLists)
                    item.Clear();

                LoadDropFile(true);
                UpdateDropFile();

                buttonAdd.Enabled = true;
                listBoxMonsters.Enabled = true;
                tabControlSeperateItems.Enabled = true;
                groupBoxGold.Enabled = true;
                groupBoxItem.Enabled = true;
            }
            else
            {
                textBoxDropList.ReadOnly = false;
                textBoxDropList.BackColor = System.Drawing.Color.Honeydew;
                buttonEdit.Text = "接受";
                //buttonEdit.Image = Properties.Resources.accept;

                buttonAdd.Enabled = false;
                listBoxMonsters.Enabled = false;
                tabControlSeperateItems.Enabled = false;
                groupBoxGold.Enabled = false;
                groupBoxItem.Enabled = false;
            }
        }

        //Cap item range to monsters level
        private void checkBoxCap_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCap.Checked == true)
            {
                textBoxMinLevel.Text = "0";
                textBoxMaxLevel.Text = Envir.MonsterInfoList[listBoxMonsters.SelectedIndices[0]].Level.ToString();
                tabControlSeperateItems_SelectedIndexChanged(tabControlSeperateItems, null);
            }
            else
            {
                textBoxMinLevel.Text = string.Empty;
                textBoxMaxLevel.Text = string.Empty;
                tabControlSeperateItems_SelectedIndexChanged(tabControlSeperateItems, null);
            }
        }

        private void buttonUpdateGold_Click(object sender, EventArgs e)
        {
            Gold = textBoxGoldAmount.Text;
            GoldOdds = textBoxGoldOdds.Text;

            UpdateDropFile();

            buttonUpdateGold.Enabled = false;
            tabControlSeperateItems.Focus();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            int index = listBoxMonsters.FindString(textBoxSearch.Text, -1);

            if (index != -1)
            {
                textBoxSearch.BackColor = System.Drawing.SystemColors.Info;
                listBoxMonsters.SetSelected(index, true);
            }
            else
            {
                textBoxSearch.BackColor = System.Drawing.Color.FromArgb(0xCC, 0x33, 0x33);
            }
        }
    }

    // Item setup
    public class DropItem
    {
        public string Name, Odds, Quest;
    }
}
