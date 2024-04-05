using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;

namespace Client.MirScenes.Dialogs
{
    public sealed class HelpDialog : MirImageControl
    {
        public List<HelpPage> Pages = new List<HelpPage>();

        public MirButton CloseButton, NextButton, PreviousButton;
        public MirLabel PageLabel;
        public HelpPage CurrentPage;

        public int CurrentPageNumber = 0;

        public HelpDialog()
        {
            Index = 920;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;

            Location = Center;

            MirImageControl TitleLabel = new MirImageControl
            {
                Index = 57,
                Library = Libraries.Title,
                Location = new Point(18, 9),
                Parent = this
            };

            PreviousButton = new MirButton
            {
                Index = 240,
                HoverIndex = 241,
                PressedIndex = 242,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(210, 485),
                Sound = SoundList.ButtonA,
            };
            PreviousButton.Click += (o, e) =>
            {
                CurrentPageNumber--;

                if (CurrentPageNumber < 0) CurrentPageNumber = Pages.Count - 1;

                DisplayPage(CurrentPageNumber);
            };

            NextButton = new MirButton
            {
                Index = 243,
                HoverIndex = 244,
                PressedIndex = 245,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(310, 485),
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                CurrentPageNumber++;

                if (CurrentPageNumber > Pages.Count - 1) CurrentPageNumber = 0;

                DisplayPage(CurrentPageNumber);
            };

            PageLabel = new MirLabel
            {
                Text = "",
                Font = new Font(Settings.FontName, 9F),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
                Location = new Point(230, 480),
                Size = new Size(80, 20)
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(509, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            LoadImagePages();

            DisplayPage();
        }

        private void LoadImagePages()
        {
            Point location = new Point(12, 35);

            Dictionary<string, string> keybinds = new Dictionary<string, string>();

            List<HelpPage> imagePages = new List<HelpPage> { 
                new HelpPage("快捷键信息", -1, new ShortcutPage1 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("快捷键信息", -1, new ShortcutPage2 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("游戏命令", -1, new ShortcutPage3 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("如何移动", 0, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("如何攻击", 1, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage("拾取物品", 2, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("生命药水使用", 3, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("技能学习", 4, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("技能使用", 5, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("魔法药水使用", 6, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("聊天功能", 7, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("公会功能", 8, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("装备耐久度", 9, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("购买物品", 10, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("卖出物品", 11, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("修理装备", 12, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("交易", 13, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("查看玩家", 14, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("查看属性", 15, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("查看属性", 16, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("查看属性", 17, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("查看属性", 18, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("查看属性", 19, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("查看属性", 20, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("任务", 21, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("任务", 22, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("任务", 23, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("任务", 24, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("坐骑", 25, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("坐骑", 26, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("钓鱼", 27, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("神珠宝玉", 28, null) { Parent = this, Location = location, Visible = false },
                new HelpPage("英雄", 29, null) { Parent = this, Location = location, Visible = false },
            };

            Pages.AddRange(imagePages);
        }


        public void DisplayPage(string pageName)
        {
            if (Pages.Count < 1) return;

            for (int i = 0; i < Pages.Count; i++)
            {
                if (Pages[i].Title.ToLower() != pageName.ToLower()) continue;

                DisplayPage(i);
                break;
            }
        }

        public void DisplayPage(int id = 0)
        {
            if (Pages.Count < 1) return;

            if (id > Pages.Count - 1) id = Pages.Count - 1;
            if (id < 0) id = 0;

            if (CurrentPage != null)
            {
                CurrentPage.Visible = false;
                if (CurrentPage.Page != null) CurrentPage.Page.Visible = false;
            }

            CurrentPage = Pages[id];

            if (CurrentPage == null) return;

            CurrentPage.Visible = true;
            if (CurrentPage.Page != null) CurrentPage.Page.Visible = true;
            CurrentPageNumber = id;

            CurrentPage.PageTitleLabel.Text = id + 1 + ". " + CurrentPage.Title;

            PageLabel.Text = string.Format("{0} / {1}", id + 1, Pages.Count);

            Show();
        }


        public void Toggle()
        {
            if (!Visible)
                Show();
            else
                Hide();
        }
    }

    public class ShortcutPage1 : ShortcutInfoPage
    {
        public ShortcutPage1()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Exit), "退出游戏"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Logout), "游戏小退"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill1) + "-" + CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill8), "技能键"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Inventory), "背包界面 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Equipment), "人物界面 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skills), "技能界面 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Group), "组队窗口 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Trade), "交易界面 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Friends), "好友界面 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Minimap), "小地图 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Guilds), "公会窗口 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.GameShop), "游戏商城 (开 / 关)"),
                //Shortcuts.Add(new ShortcutInfo("K", "Rental window (open / close)"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Relationship), "夫妻窗口 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Belt), "物品快捷窗口 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Options), "设置界面 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Help), "帮助窗口 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mount), "坐骑 (上 / 下)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.TargetSpellLockOn), "魔法锁定目标上而非光标位置")
            };

            LoadKeyBinds();
        }
    }
    public class ShortcutPage2 : ShortcutInfoPage
    {
        public ShortcutPage2()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangePetmode), "切换属下攻击模式"),
                //Shortcuts.Add(new ShortcutInfo("Ctrl + F", "Change the font in the chat box"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangeAttackmode), "切换人物攻击模式"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodePeace), "和平模式 - 仅攻击怪物"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGroup), "组队模式 - 攻击组队成员以外的所有"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGuild), "公会模式 - 攻击公会成员以外的所有"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeRedbrown), "善恶模式 - 仅攻击 PK 玩家和怪物"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeAll), "全攻模式 - 攻击所有"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bigmap), "显示大地图"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skillbar), "显示技能栏"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Autorun), "自动行走 (开 / 关)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Cameramode), "显示 / 隐藏 界面"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Pickup), "高亮显示 / 拾取物品"),
                new ShortcutInfo("Ctrl + 鼠标右击", "显示其他玩家装备栏"),
                //Shortcuts.Add(new ShortcutInfo("F12", "Chat macros"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Screenshot), "截屏"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Fishing), "(开 / 关)钓鱼窗口"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mentor), "师徒窗口 (开 / 关"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreaturePickup), "灵物拾取 (目标鼠标双击)"),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreatureAutoPickup), "灵物拾取 (S目标鼠标单击)")
            };

            LoadKeyBinds();
        }
    }
    public class ShortcutPage3 : ShortcutInfoPage
    {
        public ShortcutPage3()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                //Shortcuts.Add(new ShortcutInfo("` / Ctrl", "Change the skill bar"));
                new ShortcutInfo("/(玩家名)", "发送一条私密消息给指定的玩家"),
                new ShortcutInfo("!(信息)", "发送一条消息给附近的所有玩家"),
                new ShortcutInfo("!~(信息)", "发送一条消息给公会的所有玩家"),
				new ShortcutInfo("@加入行会", "行会开启-允许加入行会"),
				new ShortcutInfo("@退出行会", "脱离行会-退出当前行会"),
				new ShortcutInfo("@天人合一", "记忆传送-组长传送命令"),
				new ShortcutInfo("@经天纬地", "记忆传送-组员允许命令"),
				new ShortcutInfo("@允许交易", "开启交易"),
				new ShortcutInfo("@探测(玩家名)", "探测项链-玩家所在位置"),
				new ShortcutInfo("@传送(X Y)", "传送戒指-传送到本地图的所有位置"),
				new ShortcutInfo("@心心相映", "夫妻传送"),
				new ShortcutInfo("开启行会战争", "@STARTWAR(交战行会名)")
            };

            LoadKeyBinds();
        }
    }

    public class ShortcutInfo
    {
        public string Shortcut { get; set; }
        public string Information { get; set; }

        public ShortcutInfo(string shortcut, string info)
        {
            Shortcut = shortcut.Replace("\n", " + ");
            Information = info;
        }
    }

    public class ShortcutInfoPage : MirControl
    {
        protected List<ShortcutInfo> Shortcuts = new List<ShortcutInfo>();

        public ShortcutInfoPage()
        {
            Visible = false;

            MirLabel shortcutTitleLabel = new MirLabel
            {
                Text = "快捷键",
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(13, 75),
                Size = new Size(100, 30)
            };

            MirLabel infoTitleLabel = new MirLabel
            {
                Text = "信息",
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(114, 75),
                Size = new Size(405, 30)
            };
        }

        public void LoadKeyBinds()
        {
            if (Shortcuts == null) return;

            for (int i = 0; i < Shortcuts.Count; i++)
            {
                MirLabel shortcutLabel = new MirLabel
                {
                    Text = Shortcuts[i].Shortcut,
                    ForeColour = Color.Yellow,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(18, 107 + (20 * i)),
                    Size = new Size(95, 23),
                };

                MirLabel informationLabel = new MirLabel
                {
                    Text = Shortcuts[i].Information,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    ForeColour = Color.White,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(119, 107 + (20 * i)),
                    Size = new Size(400, 23),
                };
            }  
        }
    }

    public class HelpPage : MirControl
    {
        public string Title;
        public int ImageID;
        public MirControl Page;

        public MirLabel PageTitleLabel;

        public HelpPage(string title, int imageID, MirControl page)
        {
            Title = title;
            ImageID = imageID;
            Page = page;

            NotControl = true;
            Size = new System.Drawing.Size(508, 396 + 40);

            BeforeDraw += HelpPage_BeforeDraw;

            PageTitleLabel = new MirLabel
            {
                Text = Title,
                Font = new Font(Settings.FontName, 10F, FontStyle.Bold),
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Size = new System.Drawing.Size(242, 30),
                Location = new Point(135, 4)
            };
        }

        void HelpPage_BeforeDraw(object sender, EventArgs e)
        {
            if (ImageID < 0) return;

            Libraries.Help.Draw(ImageID, new Point(DisplayLocation.X, DisplayLocation.Y + 40), Color.White, false);
        }
    }
}
