using System.Text.RegularExpressions;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using SlimDX;
using Font = System.Drawing.Font;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class MainDialog : MirImageControl
    {
        public static UserObject User
        {
            get { return MapObject.User; }
            set { MapObject.User = value; }
        }

        public MirImageControl ExperienceBar, WeightBar, LeftCap, RightCap;
        public MirButton GameShopButton, MenuButton, InventoryButton, CharacterButton, SkillButton, QuestButton, OptionButton;
        public MirControl HealthOrb;
        public MirLabel HealthLabel, ManaLabel, TopLabel, BottomLabel, LevelLabel, CharacterName, ExperienceLabel, GoldLabel, WeightLabel, SpaceLabel, AModeLabel, PModeLabel, SModeLabel;
        public HeroInfoPanel HeroInfoPanel;
        public HeroBehaviourPanel HeroBehaviourPanel;
        public HeroAIDialog HeroAIDialog;

        public MirButton HeroMenuButton, HeroSummonButton;

        public bool HPOnly
        {
            get { return User != null && User.Class == MirClass.战士 && User.Level < 26; }
        }

        public MainDialog()
        {
            Index = Settings.Resolution == 800 ? 0 : Settings.Resolution == 1024 ? 1 : 2;
            Library = Libraries.Prguse;
            Location = new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)), Settings.ScreenHeight - Size.Height);
            PixelDetect = true;

            LeftCap = new MirImageControl
            {
                Index = 12,
                Library = Libraries.Prguse,
                Location = new Point(-67, this.Size.Height - 96),
                Parent = this,
                Visible = false
            };
            RightCap = new MirImageControl
            {
                Index = 13,
                Library = Libraries.Prguse,
                Location = new Point(1024, this.Size.Height - 104),
                Parent = this,
                Visible = false
            };

            if (Settings.Resolution > 1024)
            {
                LeftCap.Visible = true;
                RightCap.Visible = true;
            }

            InventoryButton = new MirButton
            {
                HoverIndex = 1904,
                Index = 1903,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 96, 76),
                Parent = this,
                PressedIndex = 1905,
                Sound = SoundList.ButtonA,
                Hint = string.Format(GameLanguage.Inventory, CMain.InputKeys.GetKey(KeybindOptions.Inventory))
            };
            InventoryButton.Click += (o, e) =>
            {
                if (GameScene.Scene.InventoryDialog.Visible)
                    GameScene.Scene.InventoryDialog.Hide();
                else
                    GameScene.Scene.InventoryDialog.Show();
            };

            CharacterButton = new MirButton
            {
                HoverIndex = 1901,
                Index = 1900,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 119, 76),
                Parent = this,
                PressedIndex = 1902,
                Sound = SoundList.ButtonA,
                Hint = string.Format(GameLanguage.Character, CMain.InputKeys.GetKey(KeybindOptions.Equipment))
            };
            CharacterButton.Click += (o, e) =>
            {
                if (GameScene.Scene.CharacterDialog.Visible && GameScene.Scene.CharacterDialog.CharacterPage.Visible)
                    GameScene.Scene.CharacterDialog.Hide();
                else
                {
                    GameScene.Scene.CharacterDialog.Show();
                    GameScene.Scene.CharacterDialog.ShowCharacterPage();
                }
            };

            SkillButton = new MirButton
            {
                HoverIndex = 1907,
                Index = 1906,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 73, 76),
                Parent = this,
                PressedIndex = 1908,
                Sound = SoundList.ButtonA,
                Hint = string.Format(GameLanguage.Skills, CMain.InputKeys.GetKey(KeybindOptions.Skills))
            };
            SkillButton.Click += (o, e) =>
            {
                if (GameScene.Scene.CharacterDialog.Visible && GameScene.Scene.CharacterDialog.SkillPage.Visible)
                    GameScene.Scene.CharacterDialog.Hide();
                else
                {
                    GameScene.Scene.CharacterDialog.Show();
                    GameScene.Scene.CharacterDialog.ShowSkillPage();
                }
            };

            QuestButton = new MirButton
            {
                HoverIndex = 1910,
                Index = 1909,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 50, 76),
                Parent = this,
                PressedIndex = 1911,
                Sound = SoundList.ButtonA,
                Hint = string.Format(GameLanguage.Quests, CMain.InputKeys.GetKey(KeybindOptions.Quests))
            };
            QuestButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.QuestLogDialog.Visible)
                    GameScene.Scene.QuestLogDialog.Show();
                else GameScene.Scene.QuestLogDialog.Hide();
            };

            OptionButton = new MirButton
            {
                HoverIndex = 1913,
                Index = 1912,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 27, 76),
                Parent = this,
                PressedIndex = 1914,
                Sound = SoundList.ButtonA,
                Hint = string.Format(GameLanguage.Options, CMain.InputKeys.GetKey(KeybindOptions.Options))
            };
            OptionButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.OptionDialog.Visible)
                    GameScene.Scene.OptionDialog.Show();
                else GameScene.Scene.OptionDialog.Hide();
            };

            MenuButton = new MirButton
            {
                HoverIndex = 1961,
                Index = 1960,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 55, 35),
                Parent = this,
                PressedIndex = 1962,
                Sound = SoundList.ButtonC,
                Hint = GameLanguage.Menu
            };
            MenuButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.MenuDialog.Visible) GameScene.Scene.MenuDialog.Show();
                else GameScene.Scene.MenuDialog.Hide();
            };

            GameShopButton = new MirButton
            {
                HoverIndex = 827,
                Index = 826,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 105, 35),
                Parent = this,
                PressedIndex = 828,
                Sound = SoundList.ButtonC,
                Hint = string.Format(GameLanguage.GameShop, CMain.InputKeys.GetKey(KeybindOptions.GameShop))
            };
            GameShopButton.Click += (o, e) =>
            {
                if (!GameScene.Scene.GameShopDialog.Visible) GameScene.Scene.GameShopDialog.Show();
                else GameScene.Scene.GameShopDialog.Hide();
            };

            HealthOrb = new MirControl
            {
                Parent = this,
                Location = new Point(0, 30),
                NotControl = true,
            };

            HealthOrb.BeforeDraw += HealthOrb_BeforeDraw;

            HealthLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(0, 27),
                Parent = HealthOrb
            };
            HealthLabel.SizeChanged += Label_SizeChanged;

            ManaLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(0, 42),
                Parent = HealthOrb
            };
            ManaLabel.SizeChanged += Label_SizeChanged;

            TopLabel = new MirLabel
            {
                Size = new Size(85, 30),
                DrawFormat = TextFormatFlags.HorizontalCenter,
                Location = new Point(9, 20),
                Parent = HealthOrb,
            };

            BottomLabel = new MirLabel
            {
                Size = new Size(85, 30),
                DrawFormat = TextFormatFlags.HorizontalCenter,
                Location = new Point(9, 50),
                Parent = HealthOrb,
            };

            LevelLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(5, 108)
            };

            CharacterName = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(6, 120),
                Size = new Size(90, 16)
            };


            ExperienceBar = new MirImageControl
            {
                Index = Settings.Resolution != 800 ? 8 : 7,
                Library = Libraries.Prguse,
                Location = new Point(9, 143),
                Parent = this,
                DrawImage = false,
                NotControl = true,
            };
            ExperienceBar.BeforeDraw += ExperienceBar_BeforeDraw;

            ExperienceLabel = new MirLabel
            {
                AutoSize = true,
                Parent = ExperienceBar,
                NotControl = true,
            };

            GoldLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.VerticalCenter,
                Font = new Font(Settings.FontName, 8F),
                Location = new Point(this.Size.Width - 105, 119),
                Parent = this,
                Size = new Size(99, 13),
                Sound = SoundList.Gold,
            };
            GoldLabel.Click += (o, e) =>
            {
                if (GameScene.SelectedCell == null)
                    GameScene.PickedUpGold = !GameScene.PickedUpGold && GameScene.Gold > 0;
            };

            WeightBar = new MirImageControl
            {
                Index = 76,
                Library = Libraries.Prguse,
                Location = new Point(this.Size.Width - 105, 103),
                Parent = this,
                DrawImage = false,
                NotControl = true,
            };
            WeightBar.BeforeDraw += WeightBar_BeforeDraw;

            WeightLabel = new MirLabel
            {
                Parent = this,
                Location = new Point(this.Size.Width - 105, 101),
                Size = new Size(40, 14),
            };

            SpaceLabel = new MirLabel
            {
                Parent = this,
                Location = new Point(this.Size.Width - 30, 101),
                Size = new Size(26, 14),
            };

            HeroMenuButton = new MirButton
            {
                Index = 2164,
                HoverIndex = 2165,
                PressedIndex = 2166,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(Size.Width - 160, 65),
                Size = new Size(30, 30),
                Sound = SoundList.ButtonA,
                Visible = false
            };
            HeroMenuButton.Click += (o, e) =>
            {
                GameScene.Scene.HeroMenuPanel.Toggle();
            };

            HeroSummonButton = new MirButton
            {
                Index = 2167,
                HoverIndex = 2168,
                PressedIndex = 2169,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(this.Size.Width - 160, 90),
                Size = new Size(20, 20),
                Sound = SoundList.ButtonA,
                Visible = false,
                Hint = string.Format(GameLanguage.HeroSummon)
            };
            HeroSummonButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.Chat
                {
                    Message = "@SUMMONHERO",
                });
            };

            HeroInfoPanel = new HeroInfoPanel { Parent = this, Visible = false };            

            AModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 899 : 675, Settings.Resolution != 800 ? -448 : -280),
                Visible = Settings.ModeView
            };

            PModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Orange,
                OutLineColour = Color.Black,
                Parent = this,
                Location = new Point(230, 125),
                Visible = Settings.ModeView
            };

            SModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.LimeGreen,
                OutLineColour = Color.Black,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 899 : 675, Settings.Resolution != 800 ? -463 : -295),
                Visible = Settings.ModeView
            };
        }

        public void Process()
        {
            switch (GameScene.Scene.AMode)
            {
                case AttackMode.Peace:
                    AModeLabel.Text = GameLanguage.AttackMode_Peace;
                    break;
                case AttackMode.Group:
                    AModeLabel.Text = GameLanguage.AttackMode_Group;
                    break;
                case AttackMode.Guild:
                    AModeLabel.Text = GameLanguage.AttackMode_Guild;
                    break;
                case AttackMode.EnemyGuild:
                    AModeLabel.Text = GameLanguage.AttackMode_EnemyGuild;
                    break;
                case AttackMode.RedBrown:
                    AModeLabel.Text = GameLanguage.AttackMode_RedBrown;
                    break;
                case AttackMode.All:
                    AModeLabel.Text = GameLanguage.AttackMode_All;
                    break;
            }

            switch (GameScene.Scene.PMode)
            {
                case PetMode.Both:
                    PModeLabel.Text = GameLanguage.PetMode_Both;
                    break;
                case PetMode.MoveOnly:
                    PModeLabel.Text = GameLanguage.PetMode_MoveOnly;
                    break;
                case PetMode.AttackOnly:
                    PModeLabel.Text = GameLanguage.PetMode_AttackOnly;
                    break;
                case PetMode.None:
                    PModeLabel.Text = GameLanguage.PetMode_None;
                    break;
                case PetMode.FocusMasterTarget:
                    PModeLabel.Text = GameLanguage.PetMode_FocusMasterTarget;
                    break;
            }

            switch (Settings.SkillMode)
            {
                case true:
                    SModeLabel.Text = "[技能模式 ~]";
                    break;
                case false:
                    SModeLabel.Text = "[技能模式 Ctrl]";
                    break;
            }

            if (Settings.HPView)
            {
                HealthLabel.Text = string.Format("生命值 {0}/{1}", User.HP, User.Stats[Stat.HP]);
                ManaLabel.Text = HPOnly ? "" : string.Format("法力值 {0}/{1} ", User.MP, User.Stats[Stat.MP]);
                TopLabel.Text = string.Empty;
                BottomLabel.Text = string.Empty;
            }
            else
            {
                if (HPOnly)
                {
                    TopLabel.Text = string.Format("{0}\n" + "--", User.HP);
                    BottomLabel.Text = string.Format("{0}", User.Stats[Stat.HP]);
                }
                else
                {
                    TopLabel.Text = string.Format(" {0}    {1} \n" + "---------------", User.HP, User.MP);
                    BottomLabel.Text = string.Format(" {0}    {1} ", User.Stats[Stat.HP], User.Stats[Stat.MP]);
                }
                HealthLabel.Text = string.Empty;
                ManaLabel.Text = string.Empty;
            }

            LevelLabel.Text = User.Level.ToString();
            ExperienceLabel.Text = string.Format("{0:#0.##%}", User.Experience / (double)User.MaxExperience);
            ExperienceLabel.Location = new Point((ExperienceBar.Size.Width / 2) - 20, -10);
            GoldLabel.Text = GameScene.Gold.ToString("###,###,##0");
            CharacterName.Text = User.Name;
            SpaceLabel.Text = User.Inventory.Count(t => t == null).ToString();
            WeightLabel.Text = (MapObject.User.Stats[Stat.背包负重] - MapObject.User.CurrentBagWeight).ToString();
        }

        private void Label_SizeChanged(object sender, EventArgs e)
        {
            if (!(sender is MirLabel l)) return;

            l.Location = new Point(50 - (l.Size.Width / 2), l.Location.Y);
        }

        private void HealthOrb_BeforeDraw(object sender, EventArgs e)
        {
            if (Libraries.Prguse == null) return;

            int height;
            if (User != null && User.HP != User.Stats[Stat.HP])
                height = (int)(80 * User.HP / (float)User.Stats[Stat.HP]);
            else
                height = 80;

            if (height < 0) height = 0;
            if (height > 80) height = 80;

            int orbImage = 4;

            bool hpOnly = false;

            if (HPOnly)
            {
                hpOnly = true;
                orbImage = 6;
            }

            Rectangle r = new Rectangle(0, 80 - height, hpOnly ? 100 : 50, height);
            Libraries.Prguse.Draw(orbImage, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)), HealthOrb.DisplayLocation.Y + 80 - height), Color.White, false);

            if (hpOnly) return;

            if (User.MP != User.Stats[Stat.MP])
                height = (int)(80 * User.MP / (float)User.Stats[Stat.MP]);
            else
                height = 80;

            if (height < 0) height = 0;
            if (height > 80) height = 80;
            r = new Rectangle(51, 80 - height, 50, height);

            Libraries.Prguse.Draw(4, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)) + 51, HealthOrb.DisplayLocation.Y + 80 - height), Color.White, false);
        }

        private void ExperienceBar_BeforeDraw(object sender, EventArgs e)
        {
            if (ExperienceBar.Library == null) return;

            double percent = MapObject.User.Experience / (double)MapObject.User.MaxExperience;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((ExperienceBar.Size.Width - 3) * percent), ExperienceBar.Size.Height)
            };

            ExperienceBar.Library.Draw(ExperienceBar.Index, section, ExperienceBar.DisplayLocation, Color.White, false);
        }

        private void WeightBar_BeforeDraw(object sender, EventArgs e)
        {
            if (WeightBar.Library == null) return;
            double percent = MapObject.User.CurrentBagWeight / (double)MapObject.User.Stats[Stat.背包负重];
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)((WeightBar.Size.Width - 2) * percent), WeightBar.Size.Height)
            };

            WeightBar.Library.Draw(WeightBar.Index, section, WeightBar.DisplayLocation, Color.White, false);
        }
    }
    public sealed class ChatDialog : MirImageControl
    {
        public List<ChatHistory> FullHistory = new List<ChatHistory>();
        public List<ChatHistory> History = new List<ChatHistory>();
        public List<MirLabel> ChatLines = new List<MirLabel>();

        public List<ChatItem> LinkedItems = new List<ChatItem>();
        public List<MirLabel> LinkedItemButtons = new List<MirLabel>();

        public MirButton HomeButton, UpButton, EndButton, DownButton, PositionBar;
        public MirImageControl CountBar;
        public MirTextBox ChatTextBox;
        public Font ChatFont = new Font(Settings.FontName, 8F);
        public string LastPM = string.Empty;

        public int StartIndex, LineCount = 4, WindowSize;
        public string ChatPrefix = "";

        public bool Transparent;

        public ChatDialog()
        {
            Index = Settings.Resolution != 800 ? 2221 : 2201;
            Library = Libraries.Prguse;
            Location = new Point(GameScene.Scene.MainDialog.Location.X + 230, Settings.ScreenHeight - 97);
            PixelDetect = true;

            KeyPress += ChatPanel_KeyPress;
            KeyDown += ChatPanel_KeyDown;
            MouseWheel += ChatPanel_MouseWheel;

            ChatTextBox = new MirTextBox
            {
                BackColour = Color.DarkGray,
                ForeColour = Color.Black,
                Parent = this,
                Size = new Size(Settings.Resolution != 800 ? 627 : 403, 13),
                Location = new Point(1, 54),
                MaxLength = Globals.MaxChatLength,
                Visible = false,
                Font = ChatFont,
            };
            ChatTextBox.TextBox.KeyPress += ChatTextBox_KeyPress;
            ChatTextBox.TextBox.KeyDown += ChatTextBox_KeyDown;
            ChatTextBox.TextBox.KeyUp += ChatTextBox_KeyUp;

            HomeButton = new MirButton
            {
                Index = 2018,
                HoverIndex = 2019,
                Library = Libraries.Prguse,
                Location = new Point(Settings.Resolution != 800 ? 618 : 394, 1),
                Parent = this,
                PressedIndex = 2020,
                Sound = SoundList.ButtonA,
            };
            HomeButton.Click += (o, e) =>
            {
                if (StartIndex == 0) return;
                StartIndex = 0;
                Update();
            };


            UpButton = new MirButton
            {
                Index = 2021,
                HoverIndex = 2022,
                Library = Libraries.Prguse,
                Location = new Point(Settings.Resolution != 800 ? 618 : 394, 9),
                Parent = this,
                PressedIndex = 2023,
                Sound = SoundList.ButtonA,
            };
            UpButton.Click += (o, e) =>
            {
                if (StartIndex == 0) return;
                StartIndex--;
                Update();
            };


            EndButton = new MirButton
            {
                Index = 2027,
                HoverIndex = 2028,
                Library = Libraries.Prguse,
                Location = new Point(Settings.Resolution != 800 ? 618 : 394, 45),
                Parent = this,
                PressedIndex = 2029,
                Sound = SoundList.ButtonA,
            };
            EndButton.Click += (o, e) =>
            {
                if (StartIndex == History.Count - 1) return;
                StartIndex = History.Count - 1;
                Update();
            };

            DownButton = new MirButton
            {
                Index = 2024,
                HoverIndex = 2025,
                Library = Libraries.Prguse,
                Location = new Point(Settings.Resolution != 800 ? 618 : 394, 39),
                Parent = this,
                PressedIndex = 2026,
                Sound = SoundList.ButtonA,
            };
            DownButton.Click += (o, e) =>
            {
                if (StartIndex == History.Count - 1) return;
                StartIndex++;
                Update();
            };



            CountBar = new MirImageControl
            {
                Index = 2012,
                Library = Libraries.Prguse,
                Location = new Point(Settings.Resolution != 800 ? 622 : 398, 16),
                Parent = this,
            };

            PositionBar = new MirButton
            {
                Index = 2015,
                HoverIndex = 2016,
                Library = Libraries.Prguse,
                Location = new Point(Settings.Resolution != 800 ? 619 : 395, 16),
                Parent = this,
                PressedIndex = 2017,
                Movable = true,
                Sound = SoundList.None,
            };
            PositionBar.OnMoving += PositionBar_OnMoving;
        }

        public void SetChatText(string newText)
        {
            string newMsg = ChatTextBox.Text += newText;

            if (newMsg.Length > Globals.MaxChatLength) return;

            ChatTextBox.Text = newMsg;
            ChatTextBox.SetFocus();
            ChatTextBox.Visible = true;
            ChatTextBox.TextBox.SelectionLength = 0;
            ChatTextBox.TextBox.SelectionStart = ChatTextBox.Text.Length;
        }

        private void ChatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    e.Handled = true;
                    if (!string.IsNullOrEmpty(ChatTextBox.Text))
                    {
                        string msg = ChatTextBox.Text;

                        if (msg.ToUpper() == "@LEVELEFFECT")
                        {
                            Settings.LevelEffect = !Settings.LevelEffect;
                        }

                        if (msg.ToUpper() == "@TARGETDEAD")
                        {
                            Settings.TargetDead = !Settings.TargetDead;
                        }

                        Network.Enqueue(new C.Chat
                        {
                            Message = msg,
                            LinkedItems = new List<ChatItem>(LinkedItems)
                        });

                        if (ChatTextBox.Text[0] == '/')
                        {
                            string[] parts = ChatTextBox.Text.Split(' ');
                            if (parts.Length > 0)
                                LastPM = parts[0];
                        }
                    }
                    ChatTextBox.Visible = false;
                    ChatTextBox.Text = string.Empty;
                    LinkedItems.Clear();
                    break;
                case (char)Keys.Escape:
                    e.Handled = true;
                    ChatTextBox.Visible = false;
                    ChatTextBox.Text = string.Empty;
                    LinkedItems.Clear();
                    break;
            }
        }

        void PositionBar_OnMoving(object sender, MouseEventArgs e)
        {
            int x = Settings.Resolution != 800 ? 619 : 395;
            int y = PositionBar.Location.Y;
            if (y >= 16 + CountBar.Size.Height - PositionBar.Size.Height) y = 16 + CountBar.Size.Height - PositionBar.Size.Height;
            if (y < 16) y = 16;

            int h = CountBar.Size.Height - PositionBar.Size.Height;
            h = (int)((y - 16) / (h / (float)(History.Count - 1)));

            if (h != StartIndex)
            {
                StartIndex = h;
                Update();
            }

            PositionBar.Location = new Point(x, y);
        }

        public void ReceiveChat(string text, ChatType type)
        {
            Color foreColour, backColour;

            switch (type)
            {
                case ChatType.Hint:
                    backColour = Color.White;
                    foreColour = Color.DarkGreen;
                    break;
                case ChatType.Announcement:
                    backColour = Color.Blue;
                    foreColour = Color.White;
                    GameScene.Scene.ChatNoticeDialog.ShowNotice(RegexFunctions.CleanChatString(text));
                    break;
                case ChatType.LineMessage:
                    backColour = Color.Blue;
                    foreColour = Color.White;
                    break;
                case ChatType.Shout:
                    backColour = Color.Yellow;
                    foreColour = Color.Black;
                    break;
                case ChatType.Shout2:
                    backColour = Color.Green;
                    foreColour = Color.White;
                    break;
                case ChatType.Shout3:
                    backColour = Color.Purple;
                    foreColour = Color.White;
                    break;
                case ChatType.System:
                    backColour = Color.Red;
                    foreColour = Color.White;
                    break;
                case ChatType.System2:
                    backColour = Color.DarkRed;
                    foreColour = Color.White;
                    break;
                case ChatType.Group:
                    backColour = Color.White;
                    foreColour = Color.Brown;
                    break;
                case ChatType.WhisperOut:
                    foreColour = Color.CornflowerBlue;
                    backColour = Color.White;
                    break;
                case ChatType.WhisperIn:
                    foreColour = Color.DarkBlue;
                    backColour = Color.White;
                    break;
                case ChatType.Guild:
                    backColour = Color.White;
                    foreColour = Color.Green;
                    break;
                case ChatType.LevelUp:
                    backColour = Color.FromArgb(255, 225, 185, 250);
                    foreColour = Color.Blue;
                    break;
                case ChatType.Relationship:
                    backColour = Color.Transparent;
                    foreColour = Color.HotPink;
                    break;
                case ChatType.Mentor:
                    backColour = Color.White;
                    foreColour = Color.Purple;
                    break;
                default:
                    backColour = Color.White;
                    foreColour = Color.Black;
                    break;
            }

            List<string> chat = new List<string>();

            int chatWidth = Settings.Resolution != 800 ? 614 : 390;
            int index = 0;

            for (int i = 1; i < text.Length; i++)
            {
                if (i - index < 0) continue;

                if (TextRenderer.MeasureText(CMain.Graphics, text.Substring(index, i - index), ChatFont).Width > chatWidth)
                {
                    int offset = i - index;
                    int newIndex = i - 1;

                    var itemLinkMatches = RegexFunctions.ChatItemLinks.Matches(text.Substring(index)).Cast<Match>();

                    if (itemLinkMatches.Any())
                    {
                        var match = itemLinkMatches.SingleOrDefault(x => (x.Index < (i - index)) && (x.Index + x.Length > offset - 1));

                        if (match != null)
                        {
                            offset = match.Index;
                            newIndex = match.Index;
                        }
                    }

                    chat.Add(text.Substring(index, offset - 1));
                    index = newIndex;
                }
            }

            chat.Add(text.Substring(index, text.Length - index));
            
            if (StartIndex == History.Count - LineCount)
                StartIndex += chat.Count;

            for (int i = 0; i < chat.Count; i++)
                FullHistory.Add(new ChatHistory { Text = chat[i], BackColour = backColour, ForeColour = foreColour, Type = type });

            Update();
        }

        public void Update()
        {
            History = new List<ChatHistory>();

            for (int i = 0; i < FullHistory.Count; i++)
            {
                switch (FullHistory[i].Type)
                {
                    case ChatType.Normal:
                    case ChatType.LineMessage:
                        if (Settings.FilterNormalChat) continue;
                        break;
                    case ChatType.WhisperIn:
                    case ChatType.WhisperOut:
                        if (Settings.FilterWhisperChat) continue;
                        break;
                    case ChatType.Shout:
                    case ChatType.Shout2:
                    case ChatType.Shout3:
                        if (Settings.FilterShoutChat) continue;
                        break;
                    case ChatType.System:
                    case ChatType.System2:
                        if (Settings.FilterSystemChat) continue;
                        break;
                    case ChatType.Group:
                        if (Settings.FilterGroupChat) continue;
                        break;
                    case ChatType.Guild:
                        if (Settings.FilterGuildChat) continue;
                        break;
                }

                History.Add(FullHistory[i]);
            }

            for (int i = 0; i < ChatLines.Count; i++)
                ChatLines[i].Dispose();

            for (int i = 0; i < LinkedItemButtons.Count; i++)
                LinkedItemButtons[i].Dispose();

            ChatLines.Clear();
            LinkedItemButtons.Clear();

            if (StartIndex >= History.Count) StartIndex = History.Count - 1;
            if (StartIndex < 0) StartIndex = 0;

            if (History.Count > 1)
            {
                int h = CountBar.Size.Height - PositionBar.Size.Height;
                h = (int)((h / (float)(History.Count - 1)) * StartIndex);
                PositionBar.Location = new Point(Settings.Resolution != 800 ? 619 : 395, 16 + h);
            }

            int y = 1;

            for (int i = StartIndex; i < History.Count; i++)
            {
                MirLabel temp = new MirLabel
                {
                    AutoSize = true,
                    BackColour = History[i].BackColour,
                    ForeColour = History[i].ForeColour,
                    Location = new Point(1, y),
                    OutLine = false,
                    Parent = this,
                    Text = History[i].Text,
                    Font = ChatFont,
                };
                temp.MouseWheel += ChatPanel_MouseWheel;
                ChatLines.Add(temp);

                temp.Click += (o, e) =>
                {
                    if (!(o is MirLabel l)) return;

                    string[] parts = l.Text.Split(':', ' ');
                    if (parts.Length == 0) return;

                    string name = Regex.Replace(parts[0], "[^A-Za-z0-9\u4E00-\u9FA5]", "");

                    ChatTextBox.SetFocus();
                    ChatTextBox.Text = string.Format("/{0} ", name);
                    ChatTextBox.Visible = true;
                    ChatTextBox.TextBox.SelectionLength = 0;
                    ChatTextBox.TextBox.SelectionStart = ChatTextBox.Text.Length;
                };

                string currentLine = History[i].Text;

                int oldLength = currentLine.Length;

                Capture capture = null;

                foreach (Match match in RegexFunctions.ChatItemLinks.Matches(currentLine).Cast<Match>().OrderBy(o => o.Index).ToList())
                {
                    try
                    {
                        int offSet = oldLength - currentLine.Length;

                        capture = match.Groups[1].Captures[0];
                        string[] values = capture.Value.Split('/');
                        currentLine = currentLine.Remove(capture.Index - 1 - offSet, capture.Length + 2).Insert(capture.Index - 1 - offSet, values[0]);
                        string text = currentLine.Substring(0, capture.Index - 1 - offSet) + " ";
                        Size size = TextRenderer.MeasureText(CMain.Graphics, text, temp.Font, temp.Size, TextFormatFlags.TextBoxControl);

                        ChatLink(values[0], ulong.Parse(values[1]), temp.Location.Add(new Point(size.Width - 10, 0)));
                    }
                    catch(Exception ex)
                    {
						//Temporary debug to catch unknown error
                        CMain.SaveError(ex.ToString());
                        CMain.SaveError(currentLine);
                        CMain.SaveError(capture.Value);
                        throw;
                    }
                }

                temp.Text = currentLine;

                y += 13;
                if (i - StartIndex == LineCount - 1) break;
            }

        }

        private void ChatLink(string name, ulong uniqueID, Point p)
        {
            UserItem item = GameScene.ChatItemList.FirstOrDefault(x => x.UniqueID == uniqueID);

            if (item != null)
            {
                MirLabel temp = new MirLabel
                {
                    AutoSize = true,
                    Visible = true,
                    Parent = this,
                    Location = p,
                    Text = name,
                    ForeColour = Color.Blue,
                    Sound = SoundList.ButtonC,
                    Font = ChatFont,
                    OutLine = false,
                };

                temp.MouseEnter += (o, e) => temp.ForeColour = Color.Red;
                temp.MouseLeave += (o, e) =>
                {
                    GameScene.Scene.DisposeItemLabel();
                    temp.ForeColour = Color.Blue;
                };
                temp.MouseDown += (o, e) => temp.ForeColour = Color.Blue;
                temp.MouseUp += (o, e) => temp.ForeColour = Color.Red;

                temp.Click += (o, e) =>
                {
                    GameScene.Scene.CreateItemLabel(item);
                };

                LinkedItemButtons.Add(temp);
            }
        }


        private void ChatPanel_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (StartIndex == 0) return;
                    StartIndex--;
                    break;
                case Keys.Home:
                    if (StartIndex == 0) return;
                    StartIndex = 0;
                    break;
                case Keys.Down:
                    if (StartIndex == History.Count - 1) return;
                    StartIndex++;
                    break;
                case Keys.End:
                    if (StartIndex == History.Count - 1) return;
                    StartIndex = History.Count - 1;
                    break;
                case Keys.PageUp:
                    if (StartIndex == 0) return;
                    StartIndex -= LineCount;
                    break;
                case Keys.PageDown:
                    if (StartIndex == History.Count - 1) return;
                    StartIndex += LineCount;
                    break;
                default:
                    return;
            }
            Update();
            e.Handled = true;
        }
        private void ChatPanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '@':
                case '!':
                case ' ':
                case (char)Keys.Enter:
                    ChatTextBox.SetFocus();
                    if (e.KeyChar == '!') ChatTextBox.Text = "!";
                    if (e.KeyChar == '@') ChatTextBox.Text = "@";
                    if (ChatPrefix != "") ChatTextBox.Text = ChatPrefix;

                    ChatTextBox.Visible = true;
                    ChatTextBox.TextBox.SelectionLength = 0;
                    ChatTextBox.TextBox.SelectionStart = ChatTextBox.Text.Length;
                    e.Handled = true;
                    break;
                case '/':
                    ChatTextBox.SetFocus();
                    ChatTextBox.Text = LastPM + " ";
                    ChatTextBox.Visible = true;
                    ChatTextBox.TextBox.SelectionLength = 0;
                    ChatTextBox.TextBox.SelectionStart = ChatTextBox.Text.Length;
                    e.Handled = true;
                    break;
            }
        }
        private void ChatPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int count = e.Delta / SystemInformation.MouseWheelScrollDelta;

            if (StartIndex == 0 && count >= 0) return;
            if (StartIndex == History.Count - 1 && count <= 0) return;

            StartIndex -= count;
            Update();
        }
        private void ChatTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                    CMain.CMain_KeyUp(sender, e);
                    break;

            }
        }
        private void ChatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            CMain.Shift = e.Shift;
            CMain.Alt = e.Alt;
            CMain.Ctrl = e.Control;

            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                case Keys.Tab:
                    CMain.CMain_KeyDown(sender, e);
                    break;

            }
        }


        public void ChangeSize()
        {
            if (++WindowSize >= 3) WindowSize = 0;

            int y = DisplayRectangle.Bottom;
            switch (WindowSize)
            {
                case 0:
                    LineCount = 4;
                    Index = Settings.Resolution != 800 ? 2221 : 2201;
                    CountBar.Index = 2012;
                    DownButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 39);
                    EndButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 45);
                    ChatTextBox.Location = new Point(1, 54);
                    break;
                case 1:
                    LineCount = 7;
                    Index = Settings.Resolution != 800 ? 2224 : 2204;
                    CountBar.Index = 2013;
                    DownButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 39 + 48);
                    EndButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 45 + 48);
                    ChatTextBox.Location = new Point(1, 54 + 48);
                    break;
                case 2:
                    LineCount = 11;
                    Index = Settings.Resolution != 800 ? 2227 : 2207;
                    CountBar.Index = 2014;
                    DownButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 39 + 96);
                    EndButton.Location = new Point(Settings.Resolution != 800 ? 618 : 394, 45 + 96);
                    ChatTextBox.Location = new Point(1, 54 + 96);
                    break;
            }

            Location = new Point(Location.X, y - Size.Height);

            UpdateBackground();

            Update();
        }

        public void UpdateBackground()
        {
            int offset = Transparent ? 1 : 0;

            switch (WindowSize)
            {
                case 0:
                    Index = Settings.Resolution != 800 ? 2221 : 2201;
                    break;
                case 1:
                    Index = Settings.Resolution != 800 ? 2224 : 2204;
                    break;
                case 2:
                    Index = Settings.Resolution != 800 ? 2227 : 2207;
                    break;
            }

            Index -= offset;
        }

        public class ChatHistory
        {
            public string Text;
            public Color ForeColour, BackColour;
            public ChatType Type;
        }
    }
    public sealed class ChatControlBar : MirImageControl
    {
        public MirButton SizeButton, SettingsButton, NormalButton, ShoutButton, WhisperButton, LoverButton, MentorButton, GroupButton, GuildButton, ReportButton, TradeButton;

        public ChatControlBar()
        {
            Index = Settings.Resolution != 800 ? 2034 : 2035;
            Library = Libraries.Prguse;
            Location = new Point(GameScene.Scene.MainDialog.Location.X + 230, Settings.ScreenHeight - 112);

            SizeButton = new MirButton
            {
                Index = 2057,
                HoverIndex = 2058,
                PressedIndex = 2059,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 574 : 350, 1),
                Visible = true,
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Size
            };
            SizeButton.Click += (o, e) =>
            {
                GameScene.Scene.ChatDialog.ChangeSize();
                Location = new Point(Location.X, GameScene.Scene.ChatDialog.DisplayRectangle.Top - Size.Height);
                if (GameScene.Scene.BeltDialog.Index == 1932)
                    GameScene.Scene.BeltDialog.Location = new Point(GameScene.Scene.MainDialog.Location.X + 230, Location.Y - GameScene.Scene.BeltDialog.Size.Height);
            };

            SettingsButton = new MirButton
            {
                Index = 2060,
                HoverIndex = 2061,
                PressedIndex = 2062,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 596 : 372, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.ChatSettings
            };
            SettingsButton.Click += (o, e) =>
            {
                if (GameScene.Scene.ChatOptionDialog.Visible)
                    GameScene.Scene.ChatOptionDialog.Hide();
                else
                    GameScene.Scene.ChatOptionDialog.Show();

                //GameScene.Scene.ChatDialog.Transparent = !GameScene.Scene.ChatDialog.Transparent;
                //GameScene.Scene.ChatDialog.UpdateBackground();
            };

            NormalButton = new MirButton
            {
                Index = 2036,
                HoverIndex = 2037,
                PressedIndex = 2038,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(12, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Chat_All
            };
            NormalButton.Click += (o, e) =>
            {
                ToggleChatFilter("All");
            };

            ShoutButton = new MirButton
            {
                Index = 2039,
                HoverIndex = 2040,
                PressedIndex = 2041,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(34, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Chat_Short
            };
            ShoutButton.Click += (o, e) =>
            {
                ToggleChatFilter("Shout");
            };

            WhisperButton = new MirButton
            {
                Index = 2042,
                HoverIndex = 2043,
                PressedIndex = 2044,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(56, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Chat_Whisper
            };
            WhisperButton.Click += (o, e) =>
            {
                ToggleChatFilter("Whisper");
            };

            LoverButton = new MirButton
            {
                Index = 2045,
                HoverIndex = 2046,
                PressedIndex = 2047,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(78, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Chat_Lover
            };
            LoverButton.Click += (o, e) =>
            {
                ToggleChatFilter("Lover");
            };

            MentorButton = new MirButton
            {
                Index = 2048,
                HoverIndex = 2049,
                PressedIndex = 2050,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(100, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Chat_Mentor
            };
            MentorButton.Click += (o, e) =>
            {
                ToggleChatFilter("Mentor");
            };

            GroupButton = new MirButton
            {
                Index = 2051,
                HoverIndex = 2052,
                PressedIndex = 2053,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(122, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Chat_Group
            };
            GroupButton.Click += (o, e) =>
            {
                ToggleChatFilter("Group");
            };

            GuildButton = new MirButton
            {
                Index = 2054,
                HoverIndex = 2055,
                PressedIndex = 2056,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(144, 1),
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Chat_Guild
            };
            GuildButton.Click += (o, e) =>
            {
                Settings.ShowGuildChat = !Settings.ShowGuildChat;
                ToggleChatFilter("Guild");
            };

            TradeButton = new MirButton
            {
                Index = 2004,
                HoverIndex = 2005,
                PressedIndex = 2006,
                Library = Libraries.Prguse,
                Location = new Point(166, 1),
                Parent = this,
                Sound = SoundList.ButtonC,
                Hint = string.Format(GameLanguage.Trade, CMain.InputKeys.GetKey(KeybindOptions.Trade)),
            };
            TradeButton.Click += (o, e) => Network.Enqueue(new C.TradeRequest());

            ReportButton = new MirButton
            {
                Index = 2063,
                HoverIndex = 2064,
                PressedIndex = 2065,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(Settings.Resolution != 800 ? 552 : 328, 1),
                Sound = SoundList.ButtonA,
                Hint = "投诉",
                Visible = false
            };
            ReportButton.Click += (o, e) =>
            {
                GameScene.Scene.ReportDialog.Visible = !GameScene.Scene.ReportDialog.Visible;
            };

            ToggleChatFilter("All");
        }

        public void ToggleChatFilter(string chatFilter)
        {
            NormalButton.Index = 2036;
            NormalButton.HoverIndex = 2037;
            ShoutButton.Index = 2039;
            ShoutButton.HoverIndex = 2040;
            WhisperButton.Index = 2042;
            WhisperButton.HoverIndex = 2043;
            LoverButton.Index = 2045;
            LoverButton.HoverIndex = 2046;
            MentorButton.Index = 2048;
            MentorButton.HoverIndex = 2049;
            GroupButton.Index = 2051;
            GroupButton.HoverIndex = 2052;
            GuildButton.Index = 2054;
            GuildButton.HoverIndex = 2055;

            GameScene.Scene.ChatDialog.ChatPrefix = "";

            switch (chatFilter)
            {
                case "All":
                    NormalButton.Index = 2038;
                    NormalButton.HoverIndex = 2038;
                    GameScene.Scene.ChatDialog.ChatPrefix = "";
                    break;
                case "Shout":
                    ShoutButton.Index = 2041;
                    ShoutButton.HoverIndex = 2041;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!";
                    break;
                case "Whisper":
                    WhisperButton.Index = 2044;
                    WhisperButton.HoverIndex = 2044;
                    GameScene.Scene.ChatDialog.ChatPrefix = "/";
                    break;
                case "Group":
                    GroupButton.Index = 2053;
                    GroupButton.HoverIndex = 2053;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!!";
                    break;
                case "Guild":
                    GuildButton.Index = 2056;
                    GuildButton.HoverIndex = 2056;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!~";
                    break;
                case "Lover":
                    LoverButton.Index = 2047;
                    LoverButton.HoverIndex = 2047;
                    GameScene.Scene.ChatDialog.ChatPrefix = ":)";
                    break;
                case "Mentor":
                    MentorButton.Index = 2050;
                    MentorButton.HoverIndex = 2050;
                    GameScene.Scene.ChatDialog.ChatPrefix = "!#";
                    break;
            }
        }
    }

    public sealed class SkillBarDialog : MirImageControl
    {
        private readonly MirButton _switchBindsButton;

        public bool AltBind;
        public bool HasSkill = false;
        public byte BarIndex;

        //public bool TopBind = !Settings.SkillMode;
        public MirImageControl[] Cells = new MirImageControl[8];
        public MirLabel[] KeyNameLabels = new MirLabel[8];
        public MirLabel BindNumberLabel = new MirLabel();

        public MirImageControl[] CoolDowns = new MirImageControl[8];

        public SkillBarDialog()
        {
            Index = 2190;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;
            Location = new Point(0, BarIndex * 20);
            Visible = true;

            BeforeDraw += MagicKeyDialog_BeforeDraw;

            _switchBindsButton = new MirButton
            {
                Index = 2247,
                Library = Libraries.Prguse,
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(16, 28),
                Location = new Point(0, 0)
            };
            _switchBindsButton.Click += (o, e) =>
            {
                //Settings.SkillSet = !Settings.SkillSet;

                Update();
            };

            for (var i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new MirImageControl
                {
                    Index = -1,
                    Library = Libraries.MagIcon,
                    Parent = this,
                    Location = new Point(i * 25 + 15, 3),
                };
                int j = i + 1;
                Cells[i].Click += (o, e) =>
                    {
                        GameScene.Scene.UseSpell(j + (8 * BarIndex));
                    };

                CoolDowns[i] = new MirImageControl
                {
                    Library = Libraries.Prguse2,
                    Parent = this,
                    Location = new Point(i * 25 + 15, 3),
                    NotControl = true,
                    UseOffSet = true,
                    Opacity = 0.6F
                };
            }

            BindNumberLabel = new MirLabel
            {
                Text = "1",
                Font = new Font(Settings.FontName, 8F),
                ForeColour = Color.White,
                Parent = this,
                Location = new Point(0, 1),
                Size = new Size(10, 25),
                NotControl = true
            };

            for (var i = 0; i < KeyNameLabels.Length; i++)
            {
                KeyNameLabels[i] = new MirLabel
                {
                    Text = "F" + (i + 1),
                    Font = new Font(Settings.FontName, 8F),
                    ForeColour = Color.White,
                    Parent = this,
                    Location = new Point(i * 25 + 13, 0),
                    Size = new Size(25, 25),
                    NotControl = true
                };
            }
            OnMoving += SkillBar_OnMoving;
        }

        private void SkillBar_OnMoving(object sender, MouseEventArgs e)
        {
            if (BarIndex * 2 >= Settings.SkillbarLocation.Length) return;
            Settings.SkillbarLocation[BarIndex, 0] = this.Location.X;
            Settings.SkillbarLocation[BarIndex, 1] = this.Location.Y;
        }

        private string GetKey(int barindex, int i)
        {
            //KeybindOptions Type = KeybindOptions.Bar1Skill1;
            if ((barindex == 0) && (i == 1))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill1);
            if ((barindex == 0) && (i == 2))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill2);
            if ((barindex == 0) && (i == 3))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill3);
            if ((barindex == 0) && (i == 4))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill4);
            if ((barindex == 0) && (i == 5))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill5);
            if ((barindex == 0) && (i == 6))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill6);
            if ((barindex == 0) && (i == 7))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill7);
            if ((barindex == 0) && (i == 8))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill8);
            if ((barindex == 1) && (i == 1))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill1);
            if ((barindex == 1) && (i == 2))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill2);
            if ((barindex == 1) && (i == 3))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill3);
            if ((barindex == 1) && (i == 4))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill4);
            if ((barindex == 1) && (i == 5))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill5);
            if ((barindex == 1) && (i == 6))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill6);
            if ((barindex == 1) && (i == 7))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill7);
            if ((barindex == 1) && (i == 8))
                return CMain.InputKeys.GetKey(KeybindOptions.Bar2Skill8);
            return "";
        }
                    

        void MagicKeyDialog_BeforeDraw(object sender, EventArgs e)
        {
            Libraries.Prguse.Draw(2193, new Point(DisplayLocation.X + 12, DisplayLocation.Y), Color.White, true, 0.5F);
        }

        public void Update()
        {
            HasSkill = false;
            foreach (var m in GameScene.User.Magics)
            {
                if ((m.Key < (BarIndex * 8)+1) || (m.Key > ((BarIndex + 1) * 8)+1)) continue;
                HasSkill = true;
            }

            if (!Visible) return;
            Index = 2190;
            _switchBindsButton.Index = 2247;
            BindNumberLabel.Text = (BarIndex + 1).ToString();
            BindNumberLabel.Location = new Point(0, 1);

            for (var i = 1; i <= 8; i++)
            {
                Cells[i - 1].Index = -1;

                int offset = BarIndex * 8;
                string key = GetKey(BarIndex, i);
                KeyNameLabels[i - 1].Text = key;

                foreach (var m in GameScene.User.Magics)
                {
                    if (m.Key != i + offset) continue;
                    HasSkill = true;
                    ClientMagic magic = MapObject.User.GetMagic(m.Spell);
                    if (magic == null) continue;

                    //string key = m.Key > 8 ? string.Format("CTRL F{0}", i) : string.Format("F{0}", m.Key);

                    Cells[i - 1].Index = magic.Icon*2;
                    Cells[i - 1].Hint = string.Format("{0}\n法力值消耗 {1}\n技能冷却 {2}\n快捷键 {3}", magic.Name,
                        (magic.BaseCost + (magic.LevelCost * magic.Level)), Functions.PrintTimeSpanFromMilliSeconds(magic.Delay), key);

                    KeyNameLabels[i - 1].Text = "";
                }
            }
        }


        public void Process()
        {
            ProcessSkillDelay();
        }

        private void ProcessSkillDelay()
        {
            if (!Visible) return;

            int offset = BarIndex * 8;

            for (int i = 0; i < Cells.Length; i++)
            {
                foreach (var magic in GameScene.User.Magics)
                {
                    if (magic.Key != i + offset + 1) continue;

                    int totalFrames = 22;
                    long timeLeft = magic.CastTime + magic.Delay - CMain.Time;

                    if (timeLeft < 100)
                    {
                        if (timeLeft > 0)
                        {
                            CoolDowns[i].Visible = false;
                           // CoolDowns[i].Dispose();
                        }
                        else
                            continue;
                    }

                    int delayPerFrame = (int)(magic.Delay / totalFrames);
                    int startFrame = totalFrames - (int)(timeLeft / delayPerFrame);

                    if ((CMain.Time <= magic.CastTime + magic.Delay))
                    {
                        CoolDowns[i].Visible = true;
                        CoolDowns[i].Index = 1260 + startFrame;
                    }
                }
            }
        }

        public override void Show()
        {
            if (Visible) return;
            if (!HasSkill) return;
            Settings.SkillBar = true;
            Visible = true;
            Update();
        }

        public override void Hide()
        {
            if (!Visible) return;
            Settings.SkillBar = false;
            Visible = false;
        }
    }
    
    public sealed class MiniMapDialog : MirImageControl
    {
        public MirImageControl LightSetting, NewMail;
        public MirButton ToggleButton, BigMapButton, MailButton;
        public MirLabel LocationLabel, MapNameLabel;
        private float _fade = 1F;
        private bool _bigMode = true;

        public MirLabel AModeLabel, PModeLabel;

        public List<MirLabel> QuestIcons = new List<MirLabel>();

        public MiniMapDialog()
        {
            Index = 2090;
            Library = Libraries.Prguse;
            Location = new Point(Settings.ScreenWidth - 126, 0);
            PixelDetect = true;

            BeforeDraw += MiniMap_BeforeDraw;
            AfterDraw += MiniMapDialog_AfterDraw;

            MapNameLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Size = new Size(120, 18),
                Location = new Point(2, 2),
                NotControl = true,
            };

            LocationLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Size = new Size(56, 18),
                Location = new Point(46, 131),
                NotControl = true,
            };

            MailButton = new MirButton
            {
                Index = 2099,
                HoverIndex = 2100,
                PressedIndex = 2101,
                Parent = this,
                Location = new Point(4, 131),
                Library = Libraries.Prguse,
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.Mail
            };
            MailButton.Click += (o, e) => GameScene.Scene.MailListDialog.Toggle();

            NewMail = new MirImageControl
            {
                Index = 544,
                Location = new Point(5, 132),
                Parent = this,
                Library = Libraries.Prguse,
                Visible = false,
                NotControl = true
            };

            BigMapButton = new MirButton
            {
                Index = 2096,
                HoverIndex = 2097,
                PressedIndex = 2098,
                Parent = this,
                Location = new Point(25, 131),
                Library = Libraries.Prguse,
                Sound = SoundList.ButtonA,
                Hint = string.Format(GameLanguage.BigMap, CMain.InputKeys.GetKey(KeybindOptions.Bigmap))
            };
            BigMapButton.Click += (o, e) => GameScene.Scene.BigMapDialog.Toggle();

            ToggleButton = new MirButton
            {
                Index = 2102,
                HoverIndex = 2103,
                PressedIndex = 2104,
                Parent = this,
                Location = new Point(109, 3),
                Library = Libraries.Prguse,
                Sound = SoundList.ButtonA,
                Hint = string.Format(GameLanguage.MiniMap, CMain.InputKeys.GetKey(KeybindOptions.Minimap))
                //Hint = "MiniMap (" + CMain.InputKeys.GetKey(KeybindOptions.Minimap) + ")"
            };
            ToggleButton.Click += (o, e) => Toggle();

            LightSetting = new MirImageControl
            {
                Index = 2093,
                Library = Libraries.Prguse,
                Parent = this,
                Location = new Point(102, 131),
            };


            AModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
                Parent = this,
                Location = new Point(115, 125)
            };

            PModeLabel = new MirLabel
            {
                AutoSize = true,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
                Parent = this,
                Location = new Point(230, 125),
                Visible = false
            };
        }

        private void MiniMapDialog_AfterDraw(object sender, EventArgs e)
        {

        }

        private void MiniMap_BeforeDraw(object sender, EventArgs e)
        {

            foreach (var icon in QuestIcons)
                icon.Dispose();

            QuestIcons.Clear();

            MapControl map = GameScene.Scene.MapControl;
            if (map == null) return;

            if (map.MiniMap == 0 && Index != 2091)
            {
                SetSmallMode();
            }
            else if (map.MiniMap > 0 && _bigMode && Index == 2091)
            {
                SetBigMode();
            }

            if (map.MiniMap <= 0 || Index != 2090 || Libraries.MiniMap == null)
            {
                return;
            }

            Rectangle viewRect = new Rectangle(0, 0, 120, 108);
            Point drawLocation = Location;
            drawLocation.Offset(3, 22);

            Size miniMapSize = Libraries.MiniMap.GetSize(map.MiniMap);
            float scaleX = miniMapSize.Width / (float)map.Width;
            float scaleY = miniMapSize.Height / (float)map.Height;

            viewRect.Location = new Point(
                (int)(scaleX * MapObject.User.CurrentLocation.X) - viewRect.Width / 2,
                (int)(scaleY * MapObject.User.CurrentLocation.Y) - viewRect.Height / 2);

            //   viewRect.Location = viewRect.Location.Subtract(1, 1);
            if (viewRect.Right >= miniMapSize.Width)
                viewRect.X = miniMapSize.Width - viewRect.Width;
            if (viewRect.Bottom >= miniMapSize.Height)
                viewRect.Y = miniMapSize.Height - viewRect.Height;

            if (viewRect.X < 0) viewRect.X = 0;
            if (viewRect.Y < 0) viewRect.Y = 0;

            Libraries.MiniMap.Draw(map.MiniMap, viewRect, drawLocation, Color.FromArgb(255, 255, 255), _fade);


            int startPointX = (int)(viewRect.X / scaleX);
            int startPointY = (int)(viewRect.Y / scaleY);

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];

                if (ob.Race == ObjectType.Item || ob.Dead || ob.Race == ObjectType.Spell || ob.Sneaking) continue;
                float x = ((ob.CurrentLocation.X - startPointX) * scaleX) + drawLocation.X;
                float y = ((ob.CurrentLocation.Y - startPointY) * scaleY) + drawLocation.Y;

                Color colour;

                if ((GroupDialog.GroupList.Contains(ob.Name) && MapObject.User != ob) || ob.Name.EndsWith(string.Format("({0})", MapObject.User.Name)))
                    colour = Color.FromArgb(0, 0, 255);
                else
                    if (ob is PlayerObject)
                {
                    colour = Color.FromArgb(255, 255, 255);
                }
                else if (ob is NPCObject || ob.AI == 980)
                {
                    colour = Color.FromArgb(0, 255, 50);
                }
                else
                    colour = Color.FromArgb(255, 0, 0);

                DXManager.Draw(DXManager.RadarTexture, new Rectangle(0, 0, 2, 2), new Vector3((float)(x - 0.5), (float)(y - 0.5), 0.0F), colour);

                #region NPC Quest Icons

                if (ob is NPCObject npc && npc.GetAvailableQuests(true).Any())
                {
                    string text = "";
                    Color color = Color.Empty;

                    switch (npc.QuestIcon)
                    {
                        case QuestIcon.ExclamationBlue:
                            color = Color.DodgerBlue;
                            text = "!";
                            break;
                        case QuestIcon.ExclamationYellow:
                            color = Color.Yellow;
                            text = "!";
                            break;
                        case QuestIcon.ExclamationGreen:
                            color = Color.Green;
                            text = "!";
                            break;
                        case QuestIcon.QuestionBlue:
                            color = Color.DodgerBlue;
                            text = "?";
                            break;
                        case QuestIcon.QuestionWhite:
                            color = Color.White;
                            text = "?";
                            break;
                        case QuestIcon.QuestionYellow:
                            color = Color.Yellow;
                            text = "?";
                            break;
                        case QuestIcon.QuestionGreen:
                            color = Color.Green;
                            text = "?";
                            break;
                    }

                    QuestIcons.Add(new MirLabel
                    {
                        AutoSize = true,
                        Parent = GameScene.Scene.MiniMapDialog,
                        Font = new Font(Settings.FontName, 9f, FontStyle.Bold),
                        DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                        Text = text,
                        ForeColour = color,
                        Location = new Point((int)(x - Settings.ScreenWidth + GameScene.Scene.MiniMapDialog.Size.Width) - 6, (int)(y) - 10),
                        NotControl = true,
                        Visible = true,
                        Modal = true
                    });
                }

                #endregion

            }
        }

        public void Toggle()
        {
            if (_fade == 0F)
            {
                _bigMode = true;
                SetBigMode();
                _fade = 1F;
            }
            //else if(_fade == 1F)
            //{
            //    _bigMode = true;
            //    SetBigMode();
            //    _fade = 0.8F;
            //}
            else
            {
                _bigMode = false;
                SetSmallMode();
                _fade = 0;
            }

            Redraw();
        }

        private void SetSmallMode()
        {
            Index = 2091;
            int y = Size.Height - 23;
            MailButton.Location = new Point(4, y);
            NewMail.Location = new Point(5, y + 1);
            BigMapButton.Location = new Point(25, y);
            LocationLabel.Location = new Point(46, y);
            LightSetting.Location = new Point(102, y);

            GameScene.Scene.DuraStatusPanel.Location = new Point(GameScene.Scene.MiniMapDialog.Location.X + 83,
            GameScene.Scene.MiniMapDialog.Size.Height);
            GameScene.Scene.GroupStatusPanel.Location = new Point(GameScene.Scene.MiniMapDialog.Location.X + 63,
            GameScene.Scene.MiniMapDialog.Size.Height);
        }

        private void SetBigMode()
        {
            Index = 2090;
            int y = Size.Height - 23;
            MailButton.Location = new Point(4, y);
            NewMail.Location = new Point(5, y + 1);
            BigMapButton.Location = new Point(25, y);
            LocationLabel.Location = new Point(46, y);
            LightSetting.Location = new Point(102, y);

            GameScene.Scene.DuraStatusPanel.Location = new Point(GameScene.Scene.MiniMapDialog.Location.X + 83,
            GameScene.Scene.MiniMapDialog.Size.Height);
            GameScene.Scene.GroupStatusPanel.Location = new Point(GameScene.Scene.MiniMapDialog.Location.X + 63,
            GameScene.Scene.MiniMapDialog.Size.Height);
        }

        public void Process()
        {
            MapControl map = GameScene.Scene.MapControl;
            if (map == null) return;

            MapNameLabel.Text = map.Title;
            LocationLabel.Text = Functions.PointToString(MapObject.User.CurrentLocation);

            GameScene.Scene.MainDialog.SModeLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            (GameScene.Scene.MiniMapDialog.Size.Height + 150) - Settings.ScreenHeight);
            GameScene.Scene.MainDialog.AModeLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            (GameScene.Scene.MiniMapDialog.Size.Height + 165) - Settings.ScreenHeight);
            GameScene.Scene.MainDialog.PModeLabel.Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 3) - GameScene.Scene.MainDialog.Location.X,
            (GameScene.Scene.MiniMapDialog.Size.Height + 180) - Settings.ScreenHeight);

            if (GameScene.Scene.NewMail)
            {
                double time = (CMain.Time) / 100D;

                if (Math.Round(time) % 10 < 5 || GameScene.Scene.NewMailCounter >= 10)
                {
                    NewMail.Visible = true;
                }
                else
                {
                    if (NewMail.Visible)
                    {
                        GameScene.Scene.NewMailCounter++;
                    }

                    NewMail.Visible = false;
                }
            }
            else
            {
                NewMail.Visible = false;
            }
        }
    }
    public sealed class InspectDialog : MirImageControl
    {
        public static UserItem[] Items = new UserItem[14];
        public static uint InspectID;

        public string Name;
        public string GuildName;
        public string GuildRank;
        public MirClass Class;
        public MirGender Gender;
        public byte Hair;
        public ushort Level;
        public string LoverName;
        public bool AllowObserve;

        public MirButton CloseButton, GroupButton, FriendButton, MailButton, TradeButton, LoverButton, ObserveButton;
        public MirImageControl CharacterPage, ClassImage;
        public MirLabel NameLabel;
        public MirLabel GuildLabel, LoverLabel;



        public MirItemCell
            WeaponCell,
            ArmorCell,
            HelmetCell,
            TorchCell,
            NecklaceCell,
            BraceletLCell,
            BraceletRCell,
            RingLCell,
            RingRCell,
            AmuletCell,
            BeltCell,
            BootsCell,
            StoneCell,
            MountCell;

        public InspectDialog()
        {
            Index = 430;
            Library = Libraries.Prguse;
            Location = new Point(536, 0);
            Movable = true;
            Sort = true;

            CharacterPage = new MirImageControl
            {
                Index = 340,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(8, 70),
            };
            CharacterPage.AfterDraw += (o, e) =>
            {
                if (Libraries.StateItems == null) return;

                ItemInfo RealItem = null;

                if (ArmorCell.Item != null)
                {
                    RealItem = Functions.GetRealItem(ArmorCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                    Libraries.StateItems.Draw(RealItem.Image, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);

                    if (RealItem.Effect > 0)
                    {
                        int genderOffset = MapObject.User.Gender == MirGender.男性 ? 0 : 1;

                        switch (RealItem.Effect)
                        {
                            case 1:
                                if (RealItem.Effect == 1)
                                    Libraries.Prguse2.DrawBlend(1202 + genderOffset, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                break;
                            case 2:
                                if (RealItem.Effect == 2)
                                    Libraries.Prguse2.DrawBlend(1204 + genderOffset, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                break;
                            case 58:
                                if (RealItem.Effect == 58)
                                    Libraries.Prguse2.DrawBlend(1530 + genderOffset, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                break;
                            case 59:
                                if (RealItem.Effect == 59)
                                    Libraries.Prguse2.DrawBlend(1532 + genderOffset, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (WeaponCell.Item != null)
                {
                    RealItem = Functions.GetRealItem(WeaponCell.Item.Info, Level, Class, GameScene.ItemInfoList);
                    Libraries.StateItems.Draw(RealItem.Image, new Point(DisplayLocation.X, DisplayLocation.Y - 20),
                    Color.White, true, 1F);
                    {
                        if (RealItem.Effect > 0)
                        {
                            switch (RealItem.Effect)
                            {
                                case 21:
                                    if (RealItem.Effect == 21)
                                        Libraries.StateitemEffect.DrawBlend(4, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 22:
                                    if (RealItem.Effect == 22)
                                        Libraries.StateitemEffect.DrawBlend(20, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 23:
                                    if (RealItem.Effect == 23)
                                        Libraries.StateitemEffect.DrawBlend(0, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 26:
                                    if (RealItem.Effect == 26)
                                        Libraries.StateitemEffect.DrawBlend(24, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 27:
                                    if (RealItem.Effect == 27)
                                        Libraries.StateitemEffect.DrawBlend(28, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 28:
                                    if (RealItem.Effect == 28)
                                        Libraries.StateitemEffect.DrawBlend(32, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 29:
                                    if (RealItem.Effect == 29)
                                        Libraries.StateitemEffect.DrawBlend(12, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 30:
                                    if (RealItem.Effect == 30)
                                        Libraries.StateitemEffect.DrawBlend(16, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 31:
                                    if (RealItem.Effect == 31)
                                        Libraries.StateitemEffect.DrawBlend(8, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 32:
                                    if (RealItem.Effect == 32)
                                        Libraries.StateitemEffect.DrawBlend(36, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 33:
                                    if (RealItem.Effect == 33)
                                        Libraries.StateitemEffect.DrawBlend(40, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 34:
                                    if (RealItem.Effect == 34)
                                        Libraries.StateitemEffect.DrawBlend(44, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 35:
                                    if (RealItem.Effect == 35)
                                        Libraries.StateitemEffect.DrawBlend(52, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 36:
                                    if (RealItem.Effect == 36)
                                        Libraries.StateitemEffect.DrawBlend(60, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 37:
                                    if (RealItem.Effect == 37)
                                        Libraries.StateitemEffect.DrawBlend(56, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 38:
                                    if (RealItem.Effect == 38)
                                        Libraries.StateitemEffect.DrawBlend(64, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 39:
                                    if (RealItem.Effect == 39)
                                        Libraries.StateitemEffect.DrawBlend(68, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 40:
                                    if (RealItem.Effect == 40)
                                        Libraries.StateitemEffect.DrawBlend(72, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 41:
                                    if (RealItem.Effect == 41)
                                        Libraries.StateitemEffect.DrawBlend(48, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 43:
                                    if (RealItem.Effect == 43)
                                        Libraries.StateItems.DrawBlend(922, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 45:
                                    if (RealItem.Effect == 45)
                                        Libraries.StateitemEffect.DrawBlend(76, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 51:
                                    if (RealItem.Effect == 51)
                                        Libraries.StateitemEffect.DrawBlend(108, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 52:
                                    if (RealItem.Effect == 52)
                                        Libraries.StateitemEffect.DrawBlend(124, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 53:
                                    if (RealItem.Effect == 53)
                                        Libraries.StateitemEffect.DrawBlend(104, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 56:
                                    if (RealItem.Effect == 56)
                                        Libraries.StateitemEffect.DrawBlend(128, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 57:
                                    if (RealItem.Effect == 57)
                                        Libraries.StateitemEffect.DrawBlend(132, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 58:
                                    if (RealItem.Effect == 58)
                                        Libraries.StateitemEffect.DrawBlend(136, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 59:
                                    if (RealItem.Effect == 59)
                                        Libraries.StateitemEffect.DrawBlend(116, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 60:
                                    if (RealItem.Effect == 60)
                                        Libraries.StateitemEffect.DrawBlend(120, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 61:
                                    if (RealItem.Effect == 61)
                                        Libraries.StateitemEffect.DrawBlend(112, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 62:
                                    if (RealItem.Effect == 62)
                                        Libraries.StateitemEffect.DrawBlend(140, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 63:
                                    if (RealItem.Effect == 63)
                                        Libraries.StateitemEffect.DrawBlend(144, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 64:
                                    if (RealItem.Effect == 64)
                                        Libraries.StateitemEffect.DrawBlend(148, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 65:
                                    if (RealItem.Effect == 65)
                                        Libraries.StateitemEffect.DrawBlend(156, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 66:
                                    if (RealItem.Effect == 66)
                                        Libraries.StateitemEffect.DrawBlend(164, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 67:
                                    if (RealItem.Effect == 67)
                                        Libraries.StateitemEffect.DrawBlend(160, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 68:
                                    if (RealItem.Effect == 68)
                                        Libraries.StateitemEffect.DrawBlend(168, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 69:
                                    if (RealItem.Effect == 69)
                                        Libraries.StateitemEffect.DrawBlend(172, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 70:
                                    if (RealItem.Effect == 70)
                                        Libraries.StateitemEffect.DrawBlend(176, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 71:
                                    if (RealItem.Effect == 71)
                                        Libraries.StateitemEffect.DrawBlend(152, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 75:
                                    if (RealItem.Effect == 75)
                                        Libraries.StateitemEffect.DrawBlend(180, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 104:
                                    if (RealItem.Effect == 104)
                                        Libraries.StateitemEffect.DrawBlend(80, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 105:
                                    if (RealItem.Effect == 105)
                                        Libraries.StateitemEffect.DrawBlend(84, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 106:
                                    if (RealItem.Effect == 106)
                                        Libraries.StateitemEffect.DrawBlend(88, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 107:
                                    if (RealItem.Effect == 107)
                                        Libraries.StateitemEffect.DrawBlend(92, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 108:
                                    if (RealItem.Effect == 108)
                                        Libraries.StateitemEffect.DrawBlend(96, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 109:
                                    if (RealItem.Effect == 109)
                                        Libraries.StateitemEffect.DrawBlend(100, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break; 
                                case 114:
                                    if (RealItem.Effect == 114)
                                        Libraries.StateitemEffect.DrawBlend(184, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 115:
                                    if (RealItem.Effect == 115)
                                        Libraries.StateitemEffect.DrawBlend(188, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 116:
                                    if (RealItem.Effect == 116)
                                        Libraries.StateitemEffect.DrawBlend(192, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 117:
                                    if (RealItem.Effect == 117)
                                        Libraries.StateitemEffect.DrawBlend(196, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 118:
                                    if (RealItem.Effect == 118)
                                        Libraries.StateitemEffect.DrawBlend(200, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break;
                                case 119:
                                    if (RealItem.Effect == 119)
                                        Libraries.StateitemEffect.DrawBlend(204, new Point(DisplayLocation.X + 0, DisplayLocation.Y + -20), Color.White, true, 1F);
                                    break; 
                                default:
                                    break;
                            }
                        }
                    }
                }

                if (HelmetCell.Item != null)
                    Libraries.StateItems.Draw(HelmetCell.Item.Info.Image, new Point(DisplayLocation.X + 0, DisplayLocation.Y - 20), Color.White, true, 1F);
                else
                {
                    int hair = 441 + Hair + (Class == MirClass.刺客 ? 20 : 0) + (Gender == MirGender.男性 ? 0 : 40);

                    int offSetX = Class == MirClass.刺客 ? (Gender == MirGender.男性 ? 6 : 4) : 0;
                    int offSetY = Class == MirClass.刺客 ? (Gender == MirGender.男性 ? 25 : 18) : 0;

                    Libraries.Prguse.Draw(hair, new Point(DisplayLocation.X + offSetX, DisplayLocation.Y + offSetY - 20), Color.White, true, 1F);
                }
            };


            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(241, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();



            GroupButton = new MirButton
            {
                HoverIndex = 432,
                Index = 431,
                Location = new Point(75, 357),
                Library = Libraries.Prguse,
                Parent = this,
                PressedIndex = 433,
                Sound = SoundList.ButtonA,
                Hint = "邀请加入组",
            };
            GroupButton.Click += (o, e) =>
            {

                if (GroupDialog.GroupList.Count >= Globals.MaxGroup)
                {
                    GameScene.Scene.ChatDialog.ReceiveChat("行会人数已达上限", ChatType.System);
                    return;
                }
                if (GroupDialog.GroupList.Count > 0 && GroupDialog.GroupList[0] != MapObject.User.Name)
                {

                    GameScene.Scene.ChatDialog.ReceiveChat("没有行会权限", ChatType.System);
                }

                Network.Enqueue(new C.AddMember { Name = Name });
                return;
            };

            FriendButton = new MirButton
            {
                HoverIndex = 435,
                Index = 434,
                Location = new Point(105, 357),
                Library = Libraries.Prguse,
                Parent = this,
                PressedIndex = 436,
                Sound = SoundList.ButtonA,
                Hint = "添加到好友列表",
            };
            FriendButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.AddFriend { Name = Name, Blocked = false });
            };

            MailButton = new MirButton
            {
                HoverIndex = 438,
                Index = 437,
                Location = new Point(135, 357),
                Library = Libraries.Prguse,
                Parent = this,
                PressedIndex = 439,
                Sound = SoundList.ButtonA,
                Hint = "发送邮件",
            };
            MailButton.Click += (o, e) => GameScene.Scene.MailComposeLetterDialog.ComposeMail(Name);

            TradeButton = new MirButton
            {
                HoverIndex = 524,
                Index = 523,
                Location = new Point(165, 357),
                Library = Libraries.Prguse,
                Parent = this,
                PressedIndex = 525,
                Sound = SoundList.ButtonA,
                Hint = "交易",
            };
            TradeButton.Click += (o, e) => Network.Enqueue(new C.TradeRequest());

            ObserveButton = new MirButton
            {
                Index = 854,
                HoverIndex = 855,
                PressedIndex = 856,
                Location = new Point(16, 357),
                Library = Libraries.Title,
                Parent = this,
                Sound = SoundList.ButtonA,
                Visible = false,
                Hint = "观察",
            };
            ObserveButton.Click += (o, e) =>
            {
                Network.Enqueue(new C.Observe { Name = Name });
            };

            NameLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(50, 12),
                Size = new Size(190, 20),
                NotControl = true
            };
            NameLabel.Click += (o, e) =>
            {
                GameScene.Scene.ChatDialog.ChatTextBox.SetFocus();
                GameScene.Scene.ChatDialog.ChatTextBox.Text = string.Format("/{0} ", Name);
                GameScene.Scene.ChatDialog.ChatTextBox.Visible = true;
                GameScene.Scene.ChatDialog.ChatTextBox.TextBox.SelectionLength = 0;
                GameScene.Scene.ChatDialog.ChatTextBox.TextBox.SelectionStart = Name.Length + 2;

            };
            LoverButton = new MirButton
            {
                Index = 604,
                Location = new Point(17, 17),
                Library = Libraries.Prguse,
                Parent = this,
                Sound = SoundList.None
            };

            GuildLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(50, 33),
                Size = new Size(190, 30),
                NotControl = true,
            };

            ClassImage = new MirImageControl
            {
                Index = 100,
                Library = Libraries.Prguse,
                Location = new Point(15, 33),
                Parent = this,
                NotControl = true,
            };


            WeaponCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.武器,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(125, 8), ///(123, 7)
            };

            ArmorCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.盔甲,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(165, 8), //(163, 7)
            };

            HelmetCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.头盔,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(205, 8), //(203, 7)
            };


            TorchCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.照明物,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(205, 135), //(203, 134)
            };

            NecklaceCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.项链,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(205, 99), //(203, 98)
            };

            BraceletLCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.左手镯,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(10, 170), //(8, 170)
            };
            BraceletRCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.右手镯,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(205, 171), //(203, 170)
            };
            RingLCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.左戒指,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(10, 207), //(8, 206)
            };
            RingRCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.右戒指,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(205, 207), //(203, 206)
            };

            AmuletCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.护身符,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(10, 242), //(8, 242)
            };

            BootsCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.靴子,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(50, 242), //(48, 242)
            };
            BeltCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.腰带,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(90, 242), //(88, 242)
            };

            StoneCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.守护石,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(129, 242), //(128, 242)
            };

            MountCell = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.坐骑,
                GridType = MirGridType.Inspect,
                Parent = CharacterPage,
                Location = new Point(204, 63), //(203, 62)
            };
        }

        public void RefreshInferface(bool IsHero)
        {
            int offSet = Gender == MirGender.男性 ? 0 : 1;

            CharacterPage.Index = 340 + offSet;

            switch (Class)
            {
                case MirClass.战士:
                    ClassImage.Index = 100;// + offSet * 5;
                    break;
                case MirClass.法师:
                    ClassImage.Index = 101;// + offSet * 5;
                    break;
                case MirClass.道士:
                    ClassImage.Index = 102;// + offSet * 5;
                    break;
                case MirClass.刺客:
                    ClassImage.Index = 103;// + offSet * 5;
                    break;
                case MirClass.弓箭:
                    ClassImage.Index = 104;// + offSet * 5;
                    break;
            }

            NameLabel.Text = Name;
            GuildLabel.Text = GuildName + " " + GuildRank;
            if (LoverName != "")
            {
                LoverButton.Visible = true;
                LoverButton.Hint = LoverName;
            }
            else
                LoverButton.Visible = false;


            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null) continue;
                GameScene.Bind(Items[i]);
            }

            ObserveButton.Visible = AllowObserve;

            TradeButton.Visible = !IsHero;
            MailButton.Visible = !IsHero;
            FriendButton.Visible = !IsHero;
            GroupButton.Visible = !IsHero;

        }


    }
    public sealed class OptionDialog : MirImageControl
    {
        public MirButton SkillModeOn, SkillModeOff;
        public MirButton SkillBarOn, SkillBarOff;
        public MirButton EffectOn, EffectOff;
        public MirButton DropViewOn, DropViewOff;
        public MirButton NameViewOn, NameViewOff;
        public MirButton HPViewOn, HPViewOff;
        public MirButton NewMoveOn, NewMoveOff;
        public MirButton ObserveOn, ObserveOff;
        public MirImageControl SoundBar, MusicSoundBar;
        public MirImageControl VolumeBar, MusicVolumeBar;

        public MirButton CloseButton;


        public OptionDialog()
        {
            Index = 411;
            Library = Libraries.Title;
            Movable = true;
            Sort = true;

            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);

            BeforeDraw += OptionPanel_BeforeDraw;

            CloseButton = new MirButton
            {
                Index = 360,
                HoverIndex = 361,
                Library = Libraries.Prguse2,
                Location = new Point(Size.Width - 26, 5),
                Parent = this,
                Sound = SoundList.ButtonA,
                PressedIndex = 362,
            };
            CloseButton.Click += (o, e) => Hide();

            //tilde option
            SkillModeOn = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(159, 68),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 451,
            };
            SkillModeOn.Click += (o, e) =>
            {
                GameScene.Scene.ChangeSkillMode(false);
            };

            //ctrl option
            SkillModeOff = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(201, 68),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 454
            };
            SkillModeOff.Click += (o, e) =>
            {
                GameScene.Scene.ChangeSkillMode(true);
            };

            SkillBarOn = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(159, 93),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 457,
            };
            SkillBarOn.Click += (o, e) => Settings.SkillBar = true;

            SkillBarOff = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(201, 93),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 460
            };
            SkillBarOff.Click += (o, e) => Settings.SkillBar = false;

            EffectOn = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(159, 118),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 457,
            };
            EffectOn.Click += (o, e) => Settings.Effect = true;

            EffectOff = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(201, 118),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 460
            };
            EffectOff.Click += (o, e) => Settings.Effect = false;

            DropViewOn = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(159, 143),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 457,
            };
            DropViewOn.Click += (o, e) => Settings.DropView = true;

            DropViewOff = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(201, 143),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 460
            };
            DropViewOff.Click += (o, e) => Settings.DropView = false;

            NameViewOn = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(159, 168),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 457,
            };
            NameViewOn.Click += (o, e) => Settings.NameView = true;

            NameViewOff = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(201, 168),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 460
            };
            NameViewOff.Click += (o, e) => Settings.NameView = false;

            HPViewOn = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(159, 193),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 463,
            };
            HPViewOn.Click += (o, e) =>
            {
                Settings.HPView = true;
                GameScene.Scene.ChatDialog.ReceiveChat("[HP/MP Mode 1]", ChatType.Hint);
            };

            HPViewOff = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(201, 193),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 466
            };
            HPViewOff.Click += (o, e) =>
            {
                Settings.HPView = false;
                GameScene.Scene.ChatDialog.ReceiveChat("[HP/MP Mode 2]", ChatType.Hint);
            };

            SoundBar = new MirImageControl
            {
                Index = 468,
                Library = Libraries.Prguse2,
                Location = new Point(159, 225),
                Parent = this,
                DrawImage = false,
            };
            SoundBar.MouseDown += SoundBar_MouseMove;
            SoundBar.MouseMove += SoundBar_MouseMove;
            SoundBar.BeforeDraw += SoundBar_BeforeDraw;

            VolumeBar = new MirImageControl
            {
                Index = 20,
                Library = Libraries.Prguse,
                Location = new Point(155, 218),
                Parent = this,
                NotControl = true,
            };

            MusicSoundBar = new MirImageControl
            {
                Index = 468,
                Library = Libraries.Prguse2,
                Location = new Point(159, 251),
                Parent = this,
                DrawImage = false
            };
            MusicSoundBar.MouseDown += MusicSoundBar_MouseMove;
            MusicSoundBar.MouseMove += MusicSoundBar_MouseMove;
            MusicSoundBar.BeforeDraw += MusicSoundBar_BeforeDraw;

            MusicVolumeBar = new MirImageControl
            {
                Index = 20,
                Library = Libraries.Prguse,
                Location = new Point(155, 244),
                Parent = this,
                NotControl = true,
            };

            NewMoveOn = new MirButton
            {
                Library = Libraries.Title,
                Location = new Point(159, 296),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 853,
            };
            NewMoveOn.Click += (o, e) =>
            {
                Settings.NewMove = true;
                GameScene.Scene.ChatDialog.ReceiveChat("[新的行走模式]", ChatType.Hint);
            };

            NewMoveOff = new MirButton
            {
                Library = Libraries.Title,
                Location = new Point(201, 296),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 850
            };
            NewMoveOff.Click += (o, e) =>
            {
                Settings.NewMove = false;
                GameScene.Scene.ChatDialog.ReceiveChat("[老的行走模式]", ChatType.Hint);
            };

            ObserveOn = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(159, 271),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 457,
            };
            ObserveOn.Click += (o, e) => ToggleObserve(true);

            ObserveOff = new MirButton
            {
                Library = Libraries.Prguse2,
                Location = new Point(201, 271),
                Parent = this,
                Sound = SoundList.ButtonA,
                Size = new Size(36, 17),
                PressedIndex = 460
            };
            ObserveOff.Click += (o, e) => ToggleObserve(false);
        }

        private void ToggleObserve(bool allow)
        {
            if (GameScene.AllowObserve == allow) return;

            Network.Enqueue(new C.Chat
            {
                Message = "@ALLOWOBSERVE",
            });
        }

        public void ToggleSkillButtons(bool Ctrl)
        {
            foreach (KeyBind KeyCheck in CMain.InputKeys.Keylist)
            {
                if (KeyCheck.Key == Keys.None)
                    continue;
                if ((KeyCheck.function < KeybindOptions.Bar1Skill1) || (KeyCheck.function > KeybindOptions.Bar2Skill8)) continue;
                //need to test this 
                if ((KeyCheck.RequireCtrl != 1) && (KeyCheck.RequireTilde != 1)) continue;
                KeyCheck.RequireCtrl = (byte)(Ctrl ? 1 : 0);
                KeyCheck.RequireTilde = (byte)(Ctrl ? 0 : 1);
            }
        }

        private void SoundBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || SoundBar != ActiveControl) return;

            Point p = e.Location.Subtract(SoundBar.DisplayLocation);

            byte volume = (byte)(p.X / (double)SoundBar.Size.Width * 100);
            Settings.Volume = volume;

            double percent = Settings.Volume / 100D;

            SoundBar.Hint = $"{Settings.Volume}%";

            if (percent > 1) percent = 1;

            VolumeBar.Location = percent > 0 ? new Point(159 + (int)((SoundBar.Size.Width - 2) * percent), 218) : new Point(159, 218);
        }

        private void SoundBar_BeforeDraw(object sender, EventArgs e)
        {
            if (SoundBar.Library == null) return;

            double percent = Settings.Volume / 100D;

            SoundBar.Hint = $"{Settings.Volume}%";

            if (percent > 1) percent = 1;
            if (percent > 0)
            {
                Rectangle section = new Rectangle
                {
                    Size = new Size((int)((SoundBar.Size.Width - 2) * percent), SoundBar.Size.Height)
                };

                SoundBar.Library.Draw(SoundBar.Index, section, SoundBar.DisplayLocation, Color.White, false);
                VolumeBar.Location = new Point(159 + section.Size.Width, 218);
            }
            else
                VolumeBar.Location = new Point(159, 218);
        }

        private void MusicSoundBar_BeforeDraw(object sender, EventArgs e)
        {
            if (MusicSoundBar.Library == null) return;

            double percent = Settings.MusicVolume / 100D;

            MusicSoundBar.Hint = $"{Settings.MusicVolume}%";

            if (percent > 1) percent = 1;
            if (percent > 0)
            {
                Rectangle section = new Rectangle
                {
                    Size = new Size((int)((MusicSoundBar.Size.Width - 2) * percent), MusicSoundBar.Size.Height)
                };

                MusicSoundBar.Library.Draw(MusicSoundBar.Index, section, MusicSoundBar.DisplayLocation, Color.White, false);
                MusicVolumeBar.Location = new Point(159 + section.Size.Width, 244);
            }
            else
                MusicVolumeBar.Location = new Point(159, 244);
        }

        private void MusicSoundBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || MusicSoundBar != ActiveControl) return;

            Point p = e.Location.Subtract(MusicSoundBar.DisplayLocation);

            byte volume = (byte)(p.X / (double)MusicSoundBar.Size.Width * 100);
            Settings.MusicVolume = volume;

            double percent = Settings.MusicVolume / 100D;

            MusicSoundBar.Hint = $"{Settings.MusicVolume}%";

            if (percent > 1) percent = 1;

            MusicVolumeBar.Location = percent > 0 ? new Point(159 + (int)((MusicSoundBar.Size.Width - 2) * percent), 244) : new Point(159, 244);
        }

        private void OptionPanel_BeforeDraw(object sender, EventArgs e)
        {
            if (Settings.SkillMode)
            {
                SkillModeOn.Index = 452;
                SkillModeOff.Index = 453;
            }
            else
            {
                SkillModeOn.Index = 450;
                SkillModeOff.Index = 455;
            }

            if (Settings.SkillBar)
            {
                SkillBarOn.Index = 458;
                SkillBarOff.Index = 459;
            }
            else
            {
                SkillBarOn.Index = 456;
                SkillBarOff.Index = 461;
            }

            if (Settings.Effect)
            {
                EffectOn.Index = 458;
                EffectOff.Index = 459;
            }
            else
            {
                EffectOn.Index = 456;
                EffectOff.Index = 461;
            }

            if (Settings.DropView)
            {
                DropViewOn.Index = 458;
                DropViewOff.Index = 459;
            }
            else
            {
                DropViewOn.Index = 456;
                DropViewOff.Index = 461;
            }

            if (Settings.NameView)
            {
                NameViewOn.Index = 458;
                NameViewOff.Index = 459;
            }
            else
            {
                NameViewOn.Index = 456;
                NameViewOff.Index = 461;
            }

            if (Settings.HPView)
            {
                HPViewOn.Index = 464;
                HPViewOff.Index = 465;
            }
            else
            {
                HPViewOn.Index = 462;
                HPViewOff.Index = 467;
            }

            if (Settings.NewMove)
            {
                NewMoveOn.Index = 853;
                NewMoveOff.Index = 848;
            }
            else
            {
                NewMoveOn.Index = 851;
                NewMoveOff.Index = 850;
            }

            if (GameScene.AllowObserve)
            {
                ObserveOn.Index = 458;
                ObserveOff.Index = 459;
            }
            else
            {        
                ObserveOn.Index = 456;
                ObserveOff.Index = 461;
            }
        }

    }
    public sealed class MenuDialog : MirImageControl
    {
        public MirButton ExitButton,
                         LogOutButton,
                         HelpButton,
                         KeyboardLayoutButton,
                         RankingButton,
                         CraftingButton,
                         IntelligentCreatureButton,
                         RideButton,
                         FishingButton,
                         FriendButton,
                         MentorButton,
                         RelationshipButton,
                         GroupButton,
                         GuildButton;

        public MenuDialog()
        {
            Index = 567;
            Parent = GameScene.Scene;
            Library = Libraries.Title;
            Location = new Point(Settings.ScreenWidth - Size.Width, GameScene.Scene.MainDialog.Location.Y - this.Size.Height + 15);
            Sort = true;
            Visible = false;
            Movable = true;

            ExitButton = new MirButton
            {
                HoverIndex = 634,
                Index = 633,
                Parent = this,
                Library = Libraries.Title,
                Location = new Point(3, 12),
                PressedIndex = 635,
                Hint = string.Format(GameLanguage.Exit, CMain.InputKeys.GetKey(KeybindOptions.Exit))
            };
            ExitButton.Click += (o, e) => GameScene.Scene.QuitGame();

            LogOutButton = new MirButton
            {
                HoverIndex = 637,
                Index = 636,
                Parent = this,
                Library = Libraries.Title,
                Location = new Point(3, 31),
                PressedIndex = 638,
                Hint = string.Format(GameLanguage.LogOut, CMain.InputKeys.GetKey(KeybindOptions.Logout))
            };
            LogOutButton.Click += (o, e) => GameScene.Scene.LogOut();


            HelpButton = new MirButton
            {
                Index = 1970,
                HoverIndex = 1971,
                PressedIndex = 1972,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 50),
                Hint = string.Format(GameLanguage.Help, CMain.InputKeys.GetKey(KeybindOptions.Help))
            };
            HelpButton.Click += (o, e) =>
            {
                if (GameScene.Scene.HelpDialog.Visible)
                    GameScene.Scene.HelpDialog.Hide();
                else GameScene.Scene.HelpDialog.Show();
            };

            KeyboardLayoutButton = new MirButton
            {
                Index = 1973,
                HoverIndex = 1974,
                PressedIndex = 1975,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 69),
                Visible = true,
                Hint = "键盘设置 (" + CMain.InputKeys.GetKey(KeybindOptions.Keybind) + ")"
            };
            KeyboardLayoutButton.Click += (o, e) =>
            {
                if (GameScene.Scene.KeyboardLayoutDialog.Visible)
                    GameScene.Scene.KeyboardLayoutDialog.Hide();
                else GameScene.Scene.KeyboardLayoutDialog.Show();
            };

            RankingButton = new MirButton
            {
                Index = 2000,
                HoverIndex = 2001,
                PressedIndex = 2002,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 88),
                Hint = string.Format(GameLanguage.Ranking, CMain.InputKeys.GetKey(KeybindOptions.Ranking))
                //Visible = false
            };
            RankingButton.Click += (o, e) =>
            {
                if (GameScene.Scene.RankingDialog.Visible)
                    GameScene.Scene.RankingDialog.Hide();
                else GameScene.Scene.RankingDialog.Show();
            };

            CraftingButton = new MirButton
            {
                Index = 2000,
                HoverIndex = 2001,
                PressedIndex = 2002,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 107),
                Visible = false
            };
            CraftingButton.Click += (o, e) =>
            {

            };

            IntelligentCreatureButton = new MirButton
            {
                Index = 431,
                HoverIndex = 432,
                PressedIndex = 433,
                Parent = this,
                Library = Libraries.Prguse2,
                Location = new Point(3, 126),
                Hint = string.Format(GameLanguage.Creatures, CMain.InputKeys.GetKey(KeybindOptions.Creature))
            };
            IntelligentCreatureButton.Click += (o, e) =>
            {
                if (GameScene.Scene.IntelligentCreatureDialog.Visible)
                    GameScene.Scene.IntelligentCreatureDialog.Hide();
                else GameScene.Scene.IntelligentCreatureDialog.Show();
            };
            RideButton = new MirButton
            {
                Index = 1976,
                HoverIndex = 1977,
                PressedIndex = 1978,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 145),
                Hint = string.Format(GameLanguage.Mount, CMain.InputKeys.GetKey(KeybindOptions.MountWindow))
            };
            RideButton.Click += (o, e) =>
            {
                if (GameScene.Scene.MountDialog.Visible)
                    GameScene.Scene.MountDialog.Hide();
                else GameScene.Scene.MountDialog.Show();
            };

            FishingButton = new MirButton
            {
                Index = 1979,
                HoverIndex = 1980,
                PressedIndex = 1981,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 164),
                Hint = string.Format(GameLanguage.Fishing, CMain.InputKeys.GetKey(KeybindOptions.Fishing))
            };
            FishingButton.Click += (o, e) =>
            {
                if (GameScene.Scene.FishingDialog.Visible)
                    GameScene.Scene.FishingDialog.Hide();
                else GameScene.Scene.FishingDialog.Show();
            };

            FriendButton = new MirButton
            {
                Index = 1982,
                HoverIndex = 1983,
                PressedIndex = 1984,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 183),
                Visible = true,
                Hint = string.Format(GameLanguage.Friends, CMain.InputKeys.GetKey(KeybindOptions.Friends))
            };
            FriendButton.Click += (o, e) =>
            {
                if (GameScene.Scene.FriendDialog.Visible)
                    GameScene.Scene.FriendDialog.Hide();
                else GameScene.Scene.FriendDialog.Show();
            };

            MentorButton = new MirButton
            {
                Index = 1985,
                HoverIndex = 1986,
                PressedIndex = 1987,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 202),
                Visible = true,
                Hint = string.Format(GameLanguage.Mentor, CMain.InputKeys.GetKey(KeybindOptions.Mentor))
            };
            MentorButton.Click += (o, e) =>
            {
                if (GameScene.Scene.MentorDialog.Visible)
                    GameScene.Scene.MentorDialog.Hide();
                else GameScene.Scene.MentorDialog.Show();
            };


            RelationshipButton = new MirButton  /* lover button */
            {
                Index = 1988,
                HoverIndex = 1989,
                PressedIndex = 1990,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 221),
                Visible = true,
                Hint = string.Format(GameLanguage.Relationship, CMain.InputKeys.GetKey(KeybindOptions.Relationship))
            };
            RelationshipButton.Click += (o, e) =>
            {
                if (GameScene.Scene.RelationshipDialog.Visible)
                    GameScene.Scene.RelationshipDialog.Hide();
                else GameScene.Scene.RelationshipDialog.Show();
            };

            GroupButton = new MirButton
            {
                Index = 1991,
                HoverIndex = 1992,
                PressedIndex = 1993,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 240),
                Hint = string.Format(GameLanguage.Groups, CMain.InputKeys.GetKey(KeybindOptions.Group))
            };
            GroupButton.Click += (o, e) =>
            {
                if (GameScene.Scene.GroupDialog.Visible)
                    GameScene.Scene.GroupDialog.Hide();
                else GameScene.Scene.GroupDialog.Show();
            };

            GuildButton = new MirButton
            {
                Index = 1994,
                HoverIndex = 1995,
                PressedIndex = 1996,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(3, 259),
                Hint = string.Format(GameLanguage.Guild, CMain.InputKeys.GetKey(KeybindOptions.Guilds))
            };
            GuildButton.Click += (o, e) =>
            {
                if (GameScene.Scene.GuildDialog.Visible)
                    GameScene.Scene.GuildDialog.Hide();
                else GameScene.Scene.GuildDialog.Show();
            };

        }


    }
    public sealed class MagicButton : MirControl
    {
        public MirImageControl LevelImage, ExpImage;
        public MirButton SkillButton;
        public MirLabel LevelLabel, NameLabel, ExpLabel, KeyLabel;
        public ClientMagic Magic;
        public MirImageControl CoolDown;
        public bool HeroMagic;

        string[] Prefixes = new string[] { "", "CTRL", "Shift" };

        public MagicButton()
        {
            Size = new Size(231, 33);

            SkillButton = new MirButton
            {
                Index = 0,
                PressedIndex = 1,
                Library = Libraries.MagIcon2,
                Parent = this,
                Location = new Point(36, 0),
                Sound = SoundList.ButtonA,
            };
            SkillButton.Click += (o, e) =>
            {
                if (HeroMagic)
                {
                    if (GameScene.Hero == null || GameScene.Hero.Dead)
                        return;
                    new AssignKeyPanel(Magic, 17, new string[]
                        {
                            "Shift" + Environment.NewLine + "F1",
                            "Shift" + Environment.NewLine + "F2",
                            "Shift" + Environment.NewLine + "F3",
                            "Shift" + Environment.NewLine + "F4",
                            "Shift" + Environment.NewLine + "F5",
                            "Shift" + Environment.NewLine + "F6",
                            "Shift" + Environment.NewLine + "F7",
                            "Shift" + Environment.NewLine + "F8"
                        })
                    { Actor = GameScene.Hero };
                }
                else
                {
                    new AssignKeyPanel(Magic, 1, new string[]
                        {
                            "F1",
                            "F2",
                            "F3",
                            "F4",
                            "F5",
                            "F6",
                            "F7",
                            "F8",
                            "Ctrl" + Environment.NewLine + "F1",
                            "Ctrl" + Environment.NewLine + "F2",
                            "Ctrl" + Environment.NewLine + "F3",
                            "Ctrl" + Environment.NewLine + "F4",
                            "Ctrl" + Environment.NewLine + "F5",
                            "Ctrl" + Environment.NewLine + "F6",
                            "Ctrl" + Environment.NewLine + "F7",
                            "Ctrl" + Environment.NewLine + "F8"
                        })
                    { Actor = GameScene.User };
                }
            };

            LevelImage = new MirImageControl
            {
                Index = 516,
                Library = Libraries.Title,
                Location = new Point(73, 7),
                Parent = this,
                NotControl = true,
            };

            ExpImage = new MirImageControl
            {
                Index = 517,
                Library = Libraries.Title,
                Location = new Point(73, 19),
                Parent = this,
                NotControl = true,
            };

            LevelLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(88, 2),
                NotControl = true,
            };

            NameLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(109, 2),
                NotControl = true,
            };

            ExpLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(109, 15),
                NotControl = true,
            };

            KeyLabel = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(2, 2),
                NotControl = true,
            };

            CoolDown = new MirImageControl
            {
                Library = Libraries.Prguse2,
                Parent = this,
                Location = new Point(36, 0),
                Opacity = 0.6F,
                NotControl = true,
                UseOffSet = true,
            };
        }

        public void Update(ClientMagic magic)
        {
            Magic = magic;

            NameLabel.Text = Magic.Name;

            LevelLabel.Text = Magic.Level.ToString();
            switch (Magic.Level)
            {
                case 0:
                    ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need1);
                    break;
                case 1:
                    ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need2);
                    break;
                case 2:
                    ExpLabel.Text = string.Format("{0}/{1}", Magic.Experience, Magic.Need3);
                    break;
                case 3:
                    ExpLabel.Text = "-";
                    break;
            }

            KeyLabel.Text = Magic.Key == 0 ? string.Empty : string.Format("{0}{1}F{2}",
                Prefixes[(Magic.Key - 1) / 8],
                Magic.Key > 8 ? Environment.NewLine : string.Empty,
                (Magic.Key - 1) % 8 + 1);

            switch (magic.Spell)
            {  //Warrior
                case Spell.Fencing:
                    SkillButton.Hint = string.Format("基本剑术：\n被动技能\n根据技能等级提高准确", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0);
                    break;
                case Spell.Slaying:
                    SkillButton.Hint = string.Format("攻杀剑术：\n被动技能\n根据技能等级提高准确和攻击", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0);
                    break;
                case Spell.Thrusting:
                    SkillButton.Hint = string.Format("刺杀剑术：\n主动技能(需开启)\n同时攻击同一方向两格目标\n第二格伤害取决与技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0);
                    break;
                case Spell.Rage:
                    SkillButton.Hint = string.Format("剑气爆：\n主动技能\n凝聚内力在一定时间内增加攻击力\n提升的攻击力和持续时间取决于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ProtectionField:
                    SkillButton.Hint = string.Format("护身气幕：\n主动技能\n凝聚内力在一定时间内提升防御力\n提升的防御力和持续时间取决于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.HalfMoon:
                    SkillButton.Hint = string.Format("半月弯刀：\n主动技能(需开启)\n快速挥舞武器引发震动的波浪\n对半圈的目标造成伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FlamingSword:
                    SkillButton.Hint = string.Format("烈火剑法：\n主动技能\n召唤火焰精魂附着到武器上增加伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ShoulderDash:
                    SkillButton.Hint = string.Format("野蛮冲撞：\n主动技能\n用肩膀把敌人撞开\n如果撞到障碍物将会对自身造成伤害\n只能撞开等级低于自己的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.CrossHalfMoon:
                    SkillButton.Hint = string.Format("狂风斩：\n主动技能(需开启)\n发两道强力的半月对周围的所有目标造成伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.TwinDrakeBlade:
                    SkillButton.Hint = string.Format("双龙斩：\n主动技能\n造成多重伤害的技艺\n一定几率麻痹目标\n攻击被麻痹的目标造成1.5倍伤害\n充能和使用分别消耗魔法值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Entrapment:
                    SkillButton.Hint = string.Format("捕绳剑： \n主动技能\n麻痹目标并把它们拖向施法者\n可拖动等级不高于自身等级 + 8级的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.LionRoar:
                    SkillButton.Hint = string.Format("狮子吼：\n主动技能\n麻痹周围目标持续时间随等级增长\n可麻痹等级不高于自身等级 + 3级的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.CounterAttack:
                    SkillButton.Hint = string.Format("天务：\n主动技能\n提升自身防御力和魔法防御力\n有一定几率防御伤害并反弹伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ImmortalSkin:
                    SkillButton.Hint = string.Format("金刚不坏：\n主动技能\n金刚护体降低自身攻击力来提升防御力", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Fury:
                    SkillButton.Hint = string.Format("血龙剑法：\n主动技能\n血龙附体使攻击速度 +4 持续时间 1分钟", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SlashingBurst:
                    SkillButton.Hint = string.Format("日闪：\n主动技能\n突刺攻击前方目标自身向前瞬移两格", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.BladeAvalanche:
                    SkillButton.Hint = string.Format("空破闪：\n主动技能\n数道刺杀同时发出\n对锥形区域的目标造成伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.EntrapmentRare:
                    SkillButton.Hint = string.Format("捕绳剑-秘籍：\n主动技能\n麻痹目标并把它们拖向施法者\n可拖动等级不高于自身等级 + 8级的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0);
                    break;
                case Spell.ImmortalSkinRare:
                    SkillButton.Hint = string.Format("金刚不坏-秘籍：\n主动技能\n金刚护体降低自身攻击力来提升防御力", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0);
                    break;
                case Spell.LionRoarRare:
                    SkillButton.Hint = string.Format("狮子吼-秘籍：\n主动技能\n麻痹周围目标持续时间随等级增长\n可麻痹等级不高于自身等级 + 3级的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.DimensionalSword:
                    SkillButton.Hint = string.Format("时空剑：\n主动技能\n将时空劈砍发挥到极限\n瞬移至敌人背后造成伤害\n刺斩范围2格内的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.DimensionalSwordRare:
                    SkillButton.Hint = string.Format("时空剑-秘籍：\n主动技能\n将时空劈砍发挥到极限\n瞬移至敌人背后造成伤害\n刺斩范围3格内的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                //Wizard
                case Spell.FireBall:
                    SkillButton.Hint = string.Format("火球术：\n主动技能\n掷出一枚火球造成远程伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ThunderBolt:
                    SkillButton.Hint = string.Format("雷电术：\n主动技能\n召唤一道雷电来攻击目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.GreatFireBall:
                    SkillButton.Hint = string.Format("大火球：\n主动技能\n掷出强力火球造成远程伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Repulsion:
                    SkillButton.Hint = string.Format("抗拒火环：\n主动技能\n推开周围目标但不造成伤害\n推开目标等级不能高于自身等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.HellFire:
                    SkillButton.Hint = string.Format("地狱火：\n主动技能\n攻击一个方向的5格目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Lightning:
                    SkillButton.Hint = string.Format("疾光电影：\n主动技能\n发出一束强力闪电\n攻击一个方向范围内的所有目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ElectricShock:
                    SkillButton.Hint = string.Format("诱惑之光：\n主动技能\n麻痹或迷惑怪物\n并可以驯服怪物\n可驯服等级不高于自身等级 + 2\n不可驯服不死系怪物", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Teleport:
                    SkillButton.Hint = string.Format("瞬息移动：\n主动技能\n在回城点地图内随机移动\n接近城镇的几率随技能等级提升", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FireWall:
                    SkillButton.Hint = string.Format("火墙：\n主动技能\n在地面上制造一堵火墙", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FireBang:
                    SkillButton.Hint = string.Format("爆裂火焰：\n主动技能\n制造一个3×3的火焰来伤害目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ThunderStorm:
                    SkillButton.Hint = string.Format("地狱雷光：\n主动技能\n以自身为中心\n制造5×5的雷电风暴攻击周围目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MagicShield:
                    SkillButton.Hint = string.Format("魔法盾：\n主动技能\n制造出魔法盾来保护自身\n减伤效果及时间取决于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.TurnUndead:
                    SkillButton.Hint = string.Format("圣言术：\n主动技能\n有几率一次击杀不死系怪物\n成功几率取决于技能等级\n可圣言等级不能高于自身等级 + 2的怪物", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.IceStorm:
                    SkillButton.Hint = string.Format("冰咆哮：\n主动技能\n召唤出3×3的冰旋风攻击目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FlameDisruptor:
                    SkillButton.Hint = string.Format("灭天火：\n主动技能\n召唤出火柱来攻击目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FrostCrunch:
                    SkillButton.Hint = string.Format("寒冰掌：\n主动技能\n扔出一个冰球\n造成伤害并随机减速", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Mirroring:
                    SkillButton.Hint = string.Format("分身术：\n主动技能\n复制出自身的镜像\n镜像被攻击消耗自身法力值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FlameField:
                    SkillButton.Hint = string.Format("火龙气焰：\n主动技能\n以自身为中心\n召唤火焰攻击周围的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Vampirism:
                    SkillButton.Hint = string.Format("嗜血术：\n主动技能\n从目标身上汲取生命值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Blizzard:
                    SkillButton.Hint = string.Format("天霜冰环：\n主动技能\n召唤从天而降的冰块\n攻击5×5范围内的目标\n并降低范围内目标的移动速度\n释放该技能需要集中精力引导", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MeteorStrike:
                    SkillButton.Hint = string.Format("天上落焰：\n主动技能\n召唤从天而降的火焰\n攻击5×5范围内的目标\n施法时间长可被攻击打断施法\n释放该技能需要集中精力引导", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.IceThrust:
                    SkillButton.Hint = string.Format("冰焰术：\n主动技能\n召唤冰柱来攻击目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MagicBooster:
                    SkillButton.Hint = string.Format("深延术：\n主动技能\n提升自身的魔法攻击\n但同时会增加法力值的消耗", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FastMove:
                    SkillButton.Hint = string.Format("FastMove \n\nChanneling Casting\nMana Cost {2}\n\nIncrease movemoent with rooted skills.\n\nCurrent Skill Level {0}\nNext Level {1}", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.StormEscape:
                    SkillButton.Hint = string.Format("雷仙风：\n主动技能\n麻痹周围目标\n瞬移到指定位置并恢复生命值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Blink:
                    SkillButton.Hint = string.Format("时光涌动：\n主动技能\n", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.HeavenlySecrets:
                    SkillButton.Hint = string.Format("天上秘术：\n主动技能\n开启后以下技能无需引导\n-天霜冰环\n-天上落焰", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.GreatFireBallRare:
                    SkillButton.Hint = string.Format("大火球-秘籍：\n主动技能\n召唤强力火球攻击目标并持续伤害\n施法时间长可被攻击打断施法", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.StormEscapeRare:
                    SkillButton.Hint = string.Format("雷仙风-秘籍：\n主动技能\n麻痹周围目标\n瞬移到指定位置并恢复生命值\n20秒内使用的一个技能不消耗法力值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                //Taoist
                case Spell.SpiritSword:
                    SkillButton.Hint = string.Format("精神力战法：\n被动技能\n提升自身准确", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Healing:
                    SkillButton.Hint = string.Format("治愈术：\n主动技能\n治疗自身或目标\n未选择目标或目标无效时治疗自身", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Poisoning:
                    SkillButton.Hint = string.Format("施毒术：\n主动技能\n绿色毒药可造成持续伤害\n红色毒药可降低目标防御", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SoulFireBall:
                    SkillButton.Hint = string.Format("灵魂火符：\n主动技能\n驱动护身符来攻击目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SoulShield:
                    SkillButton.Hint = string.Format("幽灵盾：\n主动技能\n为5×5范围内的友方目标\n增加魔法防御", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.BlessedArmour:
                    SkillButton.Hint = string.Format("神圣战甲术：\n主动技能\n为5×5范围内的友方目标\n增加物理防御", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.TrapHexagon:
                    SkillButton.Hint = string.Format("困魔咒：\n主动技能\n困住怪物\n怪物等级高于自身等级时无效", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SummonSkeleton:
                    SkillButton.Hint = string.Format("召唤骷髅：\n主动技能\n召唤一只骷髅协助作战\n骷髅的初始等级等同技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Hiding:
                    SkillButton.Hint = string.Format("隐身术：\n主动技能\n让非反隐身怪物无视", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MassHiding:
                    SkillButton.Hint = string.Format("集体隐身术：\n主动技能\n让3×3范围内的友方目标\n被非反隐身怪物无视", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Revelation:
                    SkillButton.Hint = string.Format("心灵启示：\n主动技能\n显示目标的生命值\n该技能2级以上必定成功", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MassHealing:
                    SkillButton.Hint = string.Format("群体治疗术：\n主动技能\n治疗3×3范围内的友方目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SummonShinsu:
                    SkillButton.Hint = string.Format("召唤神兽：\n主动技能\n召唤一只神兽\n神兽初始等级等同于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.UltimateEnhancer:
                    SkillButton.Hint = string.Format("无极真气：\n主动技能\n根据不同职业增加相应的攻击力\n每5点道术增加0-1上限为0-8", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.EnergyRepulsor:
                    SkillButton.Hint = string.Format("气功波：\n主动技能\n发出能量波推开周围目标\n推开目标等级大于自身等级无效", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Purification:
                    SkillButton.Hint = string.Format("净化术：\n主动技能\n净化中毒和麻痹负面效果\n成功几率随技能等级提升", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SummonHolyDeva:
                    SkillButton.Hint = string.Format("精魂召唤术：\n主动技能\n召唤一只月灵\n可于神兽或骷髅同时存在\n月灵初始等级等同技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Curse:
                    SkillButton.Hint = string.Format("诅咒术：\n主动技能\n降低目标的各种攻击及攻速", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Hallucination:
                    SkillButton.Hint = string.Format("迷魂术：\n主动技能\n迷惑怪物帮你作战\n可迷惑等级不高于自身等级 +2", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Reincarnation:
                    SkillButton.Hint = string.Format("苏生术：\n主动技能\n复活其他玩家\n成功几率随等级提升", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.PoisonCloud:
                    SkillButton.Hint = string.Format("毒雾：\n主动技能\n扔出会在目标区域造成毒云的护符\n需装备绿色毒药\n基础伤害等于道术平均值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.EnergyShield:
                    SkillButton.Hint = string.Format("先天气功：\n主动技能\n受到攻击一定几率缓慢恢复生命值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Plague:
                    SkillButton.Hint = string.Format("烦脑：\n主动技能\n损耗目标的魔法值\n并附带其他目标效果", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.PetEnhancer:
                    SkillButton.Hint = string.Format("血龙兽：\n主动技能\n强化自身的召唤物", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.HealingCircle:
                    SkillButton.Hint = string.Format("阴阳五行阵：\n主动技能\n治疗区域内友方目标\n并对敌方造成法术伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.HealingRare:
                    SkillButton.Hint = string.Format("治愈术-秘籍：\n主动技能\n高效治疗自身或目标\n未选择目标或目标无效时治疗自身", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.HealingcircleRare:
                    SkillButton.Hint = string.Format("阴阳五行阵-秘籍：\n主动技能\n治疗区域内友方目标\n对敌方造成法术伤害并附加毒伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.PetEnhancerRare:
                    SkillButton.Hint = string.Format("血龙兽-秘籍：\n主动技能\n当拥有血龙兽的能量时，你将能够一次性召唤所有召唤物\n召唤物会受到怪物的伤害减免", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MultipleEffects:
                    SkillButton.Hint = string.Format("万效符：\n主动技能\n为自己和队友提供幽灵盾、神圣战甲、先天气功、无极真气的效果\n自身受到的伤害减少10%等效果", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MultipleEffectsRare:
                    SkillButton.Hint = string.Format("万效符-秘籍：\n主动技能\n为自己和队友提供幽灵盾、神圣战甲、先天气功、无极真气的效果\n并施加净化术，自身受到的伤害减少20%等效果", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;


                //Assassin
                case Spell.FatalSword:
                    SkillButton.Hint = string.Format("绝命剑法：\n被动技能\n增加攻击伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.DoubleSlash:
                    SkillButton.Hint = string.Format("风剑术：\n主动技能(需开启)\n在一次攻击动作中快速攻击两次", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Haste:
                    SkillButton.Hint = string.Format("体迅风：\n主动技能\n使用风的力量来增加攻击速度", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FlashDash:
                    SkillButton.Hint = string.Format("拔刀术：\n主动技能\n攻击一个目标时\n有一定几率令其立即沉默且不可移动", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.HeavenlySword:
                    SkillButton.Hint = string.Format("迁移剑：\n主动技能\n集中力量到武器上攻击三格内的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.FireBurst:
                    SkillButton.Hint = string.Format("烈风击：\n主动技能\n推开等级低于施法者的目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Trap:
                    SkillButton.Hint = string.Format("捕缚术：\n主动技能\n设置一个屏障来捕缚目标\n目标受到攻击时屏障失效", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MoonLight:
                    SkillButton.Hint = string.Format("月影术：\n主动技能\n通过隐身来隐藏自己\n在此状态下造成更高伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MPEater:
                    SkillButton.Hint = string.Format("吸气：\n被动技能\n汲取目标法力值来恢复自身法力值", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SwiftFeet:
                    SkillButton.Hint = string.Format("轻身步：\n主动技能\n增加移动速度", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.LightBody:
                    SkillButton.Hint = string.Format("风身术：\n主动技能\n提升自身敏捷\n持续时间取决于攻击力和技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.PoisonSword:
                    SkillButton.Hint = string.Format("猛毒剑气：\n主动技能\n在攻击时施毒\n一定时间内对目标造成持续伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.DarkBody:
                    SkillButton.Hint = string.Format("烈火身：\n主动技能\n进入月影术并制造一个幻象攻击目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.CrescentSlash:
                    SkillButton.Hint = string.Format("月华乱舞：\n主动技能\n爆发剑的力量\n攻击扇形范围的所有目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Hemorrhage:
                    SkillButton.Hint = string.Format("血风击：\n被动技能\n一定几率暴击并造成流血伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MoonMist:
                    SkillButton.Hint = string.Format("月影雾：\n主动技能\n围绕自身产生雾气进入潜行\n雾气散去同时造成范围爆炸\n技能等级越高技能重置时间越短", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.CatTongue:
                    SkillButton.Hint = string.Format("猫舌兰：\n主动技能\n发射猫舌形状的暗器\n击中目标自身有几率异常状态\n技能等级越高技能重置时间越短\n进入异常状态几率越大", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Focus:
                    SkillButton.Hint = string.Format("必中闪：\n被动技能\n射出弓箭可造成额外伤害\n目标越远伤害高\n伤害取决于距离和技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.StraightShot:
                    SkillButton.Hint = string.Format("天日闪：\n主动技能\n射出的箭可造成强力伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.DoubleShot:
                    SkillButton.Hint = string.Format("无我闪：\n主动技能\n一次射出两支箭矢\n目标被击中后有几率后仰两次", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ExplosiveTrap:
                    SkillButton.Hint = string.Format("爆阱：\n主动技能\n设置一个持续15秒的陷阱\n再次使用技能时触发爆炸\n未触发则到时自动爆炸", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.DelayedExplosion:
                    SkillButton.Hint = string.Format("爆闪：\n主动技能\n射出火球吸附在目标身上\n一定时间后自动爆炸造成伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Meditation:
                    SkillButton.Hint = string.Format("气功术：\n被动技能\n攻击时有几率生成气\n在击中或切换目标时获得\n持续时间取决于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ElementalShot:
                    SkillButton.Hint = string.Format("万金闪：\n主动技能\n聚气在身使用技能可推开目标\n没有气息时可生成气息\n造成的伤害取决于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Concentration:
                    SkillButton.Hint = string.Format("气流术：\n主动技能\n激活时不移动可获得气\n获得气的时间取决与技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.ElementalBarrier:
                    SkillButton.Hint = string.Format("金刚术：\n主动技能\n无气时生成气\n有气时将气转换为保护盾\n伤害减低率取决于气的数量及技能等级\n持续时间取决于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.BackStep:
                    SkillButton.Hint = string.Format("风弹步：\n主动技能\n保持方向向后跳跃\n跳跃距离取决于技能等级", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.BindingShot:
                    SkillButton.Hint = string.Format("困兽笼：\n主动技能\n射出一只精神箭\n将目标困在精神牢笼中\n目标受到攻击时失效", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.Stonetrap:
                    SkillButton.Hint = string.Format("地柱钉：\n主动技能\n生成地柱占位阻挡目标\n使用次数与被击中次数取决于技能等级\n与施法者距离较远自动消失", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SummonVampire:
                    SkillButton.Hint = string.Format("吸血地精：\n主动技能\n召唤一只蜘蛛怪\n爆炸时为技能使用者恢复生命值\n超出距离会自动死亡", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.VampireShot:
                    SkillButton.Hint = string.Format("吸血地闪：\n主动技能\n攻击目标同时叠加特效\n再次使用技能触发恢复生命特效", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SummonToad:
                    SkillButton.Hint = string.Format("痹魔阱：\n主动技能\n召唤一只蛙怪\n蛙怪攻击时有几率麻痹目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.PoisonShot:
                    SkillButton.Hint = string.Format("毒魔闪：\n主动技能\n射出的弓箭有几率使目标中毒", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.CrippleShot:
                    SkillButton.Hint = string.Format("邪爆闪：\n主动技能\n对目标造成伤害并触发特效\n几率触发吸血闪、毒魔闪特效", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.SummonSnakes:
                    SkillButton.Hint = string.Format("蛇柱阱：\n主动技能\n生成一个蛇柱图腾\n召唤蛇怪来攻击目标\n蛇怪攻击时有几率沉默目标", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.NapalmShot:
                    SkillButton.Hint = string.Format("血龙闪：\n主动技能\n射出一只龙箭\n造成大范围的高伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.OneWithNature:
                    SkillButton.Hint = string.Format("血龙闪-秘籍：\n主动技能\n射出一只龙箭\n造成大范围的高伤害并造成额外伤害", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;
                case Spell.MentalState:
                    SkillButton.Hint = string.Format("精神状态：\n主动技能(需开启)\n可以调整弓箭的攻击模式", Magic.Level, Magic.Level == 0 ? Magic.Level1 : Magic.Level == 1 ? Magic.Level2 : Magic.Level == 2 ? Magic.Level3 : 0, Magic.BaseCost);
                    break;

                default:

                    break;
            }
            

            SkillButton.Index = Magic.Icon * 2;
            SkillButton.PressedIndex = Magic.Icon * 2 + 1;

            SetDelay();
        }

        public void SetDelay()
        {
            if (Magic == null) return;

            int totalFrames = 34;

            long timeLeft = Magic.CastTime + Magic.Delay - CMain.Time;

            if (timeLeft < 100)
            {
                CoolDown.Visible = false;
                return;
            }

            int delayPerFrame = (int)(Magic.Delay / totalFrames);
            int startFrame = totalFrames - (int)(timeLeft / delayPerFrame);

            if ((CMain.Time <= Magic.CastTime + Magic.Delay))
            {
                CoolDown.Visible = true;
                CoolDown.Index = 1290 + startFrame;
            }
        }
    }
    public sealed class AssignKeyPanel : MirImageControl
    {
        public MirButton SaveButton, NoneButton;
        public UserObject Actor;
        public MirLabel TitleLabel;
        public MirImageControl MagicImage;
        public MirButton[] FKeys;

        public ClientMagic Magic;
        public byte Key;
        public byte KeyOffset;

        public AssignKeyPanel(ClientMagic magic, byte keyOffset, string[] keyStrings)
        {
            Magic = magic;
            Key = magic.Key;
            KeyOffset = keyOffset;

            Modal = true;
            Index = 710;
            Library = Libraries.Prguse;
            Location = Center;
            Parent = GameScene.Scene;
            Visible = true;

            MagicImage = new MirImageControl
            {
                Location = new Point(16, 16),
                Index = magic.Icon * 2,
                Library = Libraries.MagIcon2,
                Parent = this,
            };

            TitleLabel = new MirLabel
            {
                Location = new Point(49, 17),
                Parent = this,
                Size = new Size(230, 32),
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak,
                Text = string.Format(GameLanguage.SelectKey, magic.Name)
            };

            NoneButton = new MirButton
            {
                Index = 287, //154
                HoverIndex = 288,
                PressedIndex = 289,
                Library = Libraries.Title,
                Parent = this,
                Location = new Point(284, 64),
            };
            NoneButton.Click += (o, e) => Key = 0;

            SaveButton = new MirButton
            {
                Library = Libraries.Title,
                Parent = this,
                Location = new Point(284, 101),
                Index = 156,
                HoverIndex = 157,
                PressedIndex = 158,
            };
            SaveButton.Click += (o, e) =>
            {
                for (int i = 0; i < Actor.Magics.Count; i++)
                {
                    if (Actor.Magics[i].Key == Key)
                        Actor.Magics[i].Key = 0;
                }

                Network.Enqueue(new C.MagicKey { Spell = Magic.Spell, Key = Key, OldKey = Magic.Key });
                Magic.Key = Key;
                foreach (SkillBarDialog Bar in GameScene.Scene.SkillBarDialogs)
                    Bar.Update();

                Dispose();
            };

            FKeys = new MirButton[keyStrings.Length];

            for (byte i = 0; i < FKeys.Length; i++)
            {
                FKeys[i] = new MirButton
                {
                    Index = 0,
                    PressedIndex = 1,
                    Library = Libraries.Prguse,
                    Parent = this,
                    Location = new Point(17 + 32 * (i % 8) + 5 * (i % 8 / 4), 58 + 37 * (i / 8)),
                    Sound = SoundList.ButtonA,
                    Text = keyStrings[i]
                };
                int num = i + keyOffset;
                FKeys[i].Click += (o, e) =>
                {
                    Key = (byte)num;
                };
            }

            BeforeDraw += AssignKeyPanel_BeforeDraw;
        }

        private void AssignKeyPanel_BeforeDraw(object sender, EventArgs e)
        {
            for (int i = 0; i < FKeys.Length; i++)
            {
                FKeys[i].Index = 1656;
                FKeys[i].HoverIndex = 1657;
                FKeys[i].PressedIndex = 1658;
                FKeys[i].Visible = true;
            }

            int key = Key - KeyOffset;
            if (key < 0 || key > FKeys.Length) return;

            FKeys[key].Index = 1658;
            FKeys[key].HoverIndex = 1658;
            FKeys[key].PressedIndex = 1658;
        }
    }
    public sealed class DuraStatusDialog : MirImageControl
    {
        public MirButton Character;

        public DuraStatusDialog()
        {
            Size = new Size(30, 30);
            Location = new Point((GameScene.Scene.MiniMapDialog.Location.X + 83), GameScene.Scene.MiniMapDialog.Size.Height);

            Character = new MirButton()
            {
                Index = 2113,
                Library = Libraries.Prguse,
                Parent = this,
                HoverIndex = 2111,
                PressedIndex = 2112,
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.DuraPanel
            };
            Character.Click += (o, e) =>
            {
                if (GameScene.Scene.CharacterDuraPanel.Visible == true)
                {
                    GameScene.Scene.CharacterDuraPanel.Hide();
                    Settings.DuraView = false;
                }
                else
                {
                    GameScene.Scene.CharacterDuraPanel.Show();
                    Settings.DuraView = true;
                }
            };
        }

    }
    public sealed class CharacterDuraPanel : MirImageControl
    {
        public MirImageControl GrayBackground, Background, Helmet, Armour, Belt, Boots, Weapon, Necklace, RightBracelet, LeftBracelet, RightRing, LeftRing, Torch, Stone, Amulet, Mount, Item1, Item2;

        public CharacterDuraPanel()
        {
            Index = 2105;
            Library = Libraries.Prguse;
            Movable = false;
            Location = new Point(Settings.ScreenWidth - 61, 200);

            GrayBackground = new MirImageControl()
            {
                Index = 2161,
                Library = Libraries.Prguse,
                Parent = this,
                Size = new Size(56, 80),
                Location = new Point(3, 3),
                Opacity = 0.4F
            };
            Background = new MirImageControl()
            {
                Index = 2162,
                Library = Libraries.Prguse,
                Parent = this,
                Size = new Size(56, 80),
                Location = new Point(3, 3),
            };

            #region Pieces

            Helmet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(24, 3) };
            Belt = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 7), Location = new Point(23, 23) };
            Armour = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(28, 32), Location = new Point(16, 11) };
            Boots = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(24, 9), Location = new Point(17, 43) };
            Weapon = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 33), Location = new Point(4, 5) };
            Necklace = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(3, 67) };
            LeftBracelet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 8), Location = new Point(3, 43) };
            RightBracelet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 8), Location = new Point(43, 43) };
            LeftRing = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(3, 54) };
            RightRing = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(43, 54) };
            Torch = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(8, 32), Location = new Point(44, 5) };
            Stone = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(30, 54) };
            Amulet = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(16, 54) };
            Mount = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(12, 12), Location = new Point(43, 68) };
            Item1 = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(8, 12), Location = new Point(19, 67) };
            Item2 = new MirImageControl() { Index = -1, Library = Libraries.Prguse, Parent = Background, Size = new Size(8, 12), Location = new Point(31, 67) };

            #endregion
        }

        public void GetCharacterDura()
        {
            if (GameScene.Scene.CharacterDialog.Grid[0].Item == null) { Weapon.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[1].Item == null) { Armour.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[2].Item == null) { Helmet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[3].Item == null) { Torch.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[4].Item == null) { Necklace.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[5].Item == null) { LeftBracelet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[6].Item == null) { RightBracelet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[7].Item == null) { LeftRing.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[8].Item == null) { RightRing.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[9].Item == null) { Amulet.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[10].Item == null) { Belt.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[11].Item == null) { Boots.Index = -1; }
            if (GameScene.Scene.CharacterDialog.Grid[12].Item == null) { Stone.Index = -1; }

            for (int i = 0; i < MapObject.User.Equipment.Length; i++)
            {
                if (MapObject.User.Equipment[i] == null) continue;
                UpdateCharacterDura(MapObject.User.Equipment[i]);
            }
        }
        public void UpdateCharacterDura(UserItem item)
        {
            int Warning = item.MaxDura / 2;
            int Danger = item.MaxDura / 5;
            ushort AmuletWarning = (ushort)(item.Info.StackSize / 2);
            ushort AmuletDanger = (ushort)(item.Info.StackSize / 5);

            switch (item.Info.Type)
            {
                case ItemType.护身符: //Based on stacks of 5000
                    if (item.Count > AmuletWarning)
                        Amulet.Index = 2134;
                    if (item.Count <= AmuletWarning)
                        Amulet.Index = 2135;
                    if (item.Count <= AmuletDanger)
                        Amulet.Index = 2136;
                    if (item.Count == 0)
                        Amulet.Index = -1;
                    break;
                case ItemType.盔甲:
                    if (item.CurrentDura > Warning)
                        Armour.Index = 2149;
                    if (item.CurrentDura <= Warning)
                        Armour.Index = 2150;
                    if (item.CurrentDura <= Danger)
                        Armour.Index = 2151;
                    if (item.CurrentDura == 0)
                        Armour.Index = -1;
                    break;
                case ItemType.腰带:
                    if (item.CurrentDura > Warning)
                        Belt.Index = 2158;
                    if (item.CurrentDura <= Warning)
                        Belt.Index = 2159;
                    if (item.CurrentDura <= Danger)
                        Belt.Index = 2160;
                    if (item.CurrentDura == 0)
                        Belt.Index = -1;
                    break;
                case ItemType.靴子:
                    if (item.CurrentDura > Warning)
                        Boots.Index = 2152;
                    if (item.CurrentDura <= Warning)
                        Boots.Index = 2153;
                    if (item.CurrentDura <= Danger)
                        Boots.Index = 2154;
                    if (item.CurrentDura == 0)
                        Boots.Index = -1;
                    break;
                case ItemType.手镯:
                    if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.右手镯].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.右手镯].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            RightBracelet.Index = 2143;
                        if (item.CurrentDura <= Warning)
                            RightBracelet.Index = 2144;
                        if (item.CurrentDura <= Danger)
                            RightBracelet.Index = 2145;
                        if (item.CurrentDura == 0)
                            RightBracelet.Index = -1;
                    }
                    else if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.左手镯].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.左手镯].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            LeftBracelet.Index = 2143;
                        if (item.CurrentDura <= Warning)
                            LeftBracelet.Index = 2144;
                        if (item.CurrentDura <= Danger)
                            LeftBracelet.Index = 2145;
                        if (item.CurrentDura == 0)
                            LeftBracelet.Index = -1;
                    }
                    break;
                case ItemType.头盔:
                    if (item.CurrentDura > Warning)
                        Helmet.Index = 2155;
                    if (item.CurrentDura <= Warning)
                        Helmet.Index = 2156;
                    if (item.CurrentDura <= Danger)
                        Helmet.Index = 2157;
                    if (item.CurrentDura == 0)
                        Helmet.Index = -1;
                    break;
                case ItemType.项链:
                    if (item.CurrentDura > Warning)
                        Necklace.Index = 2122;
                    if (item.CurrentDura <= Warning)
                        Necklace.Index = 2123;
                    if (item.CurrentDura <= Danger)
                        Necklace.Index = 2124;
                    if (item.CurrentDura == 0)
                        Necklace.Index = -1;
                    break;
                case ItemType.戒指:
                    if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.右戒指].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.右戒指].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            RightRing.Index = 2131;
                        if (item.CurrentDura <= Warning)
                            RightRing.Index = 2132;
                        if (item.CurrentDura <= Danger)
                            RightRing.Index = 2133;
                        if (item.CurrentDura == 0)
                            RightRing.Index = -1;
                    }
                    else if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.左戒指].Item != null && item.UniqueID == GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.左戒指].Item.UniqueID)
                    {
                        if (item.CurrentDura > Warning)
                            LeftRing.Index = 2131;
                        if (item.CurrentDura <= Warning)
                            LeftRing.Index = 2132;
                        if (item.CurrentDura <= Danger)
                            LeftRing.Index = 2133;
                        if (item.CurrentDura == 0)
                            LeftRing.Index = -1;
                    }
                    break;
                case ItemType.守护石:
                    if (item.CurrentDura == 0)
                        Stone.Index = 2137;
                    break;
                case ItemType.坐骑:
                    if (item.CurrentDura > Warning)
                        Mount.Index = 2140;
                    if (item.CurrentDura <= Warning)
                        Mount.Index = 2141;
                    if (item.CurrentDura <= Danger)
                        Mount.Index = 2142;
                    if (item.CurrentDura == 0)
                        Mount.Index = -1;
                    break;
                case ItemType.照明物:
                    if (item.CurrentDura > Warning)
                        Torch.Index = 2146;
                    if (item.CurrentDura <= Warning)
                        Torch.Index = 2147;
                    if (item.CurrentDura <= Danger)
                        Torch.Index = 2148;
                    if (item.CurrentDura == 0)
                        Torch.Index = -1;
                    break;
                case ItemType.武器:
                    if (item.CurrentDura > Warning)
                        Weapon.Index = 2125;
                    if (item.CurrentDura <= Warning)
                        Weapon.Index = 2126;
                    if (item.CurrentDura <= Danger)
                        Weapon.Index = 2127;
                    if (item.CurrentDura == 0)
                        Weapon.Index = -1;
                    break;
            }
        }

        public override void Hide()
        {
            if (!Visible) return;
            Visible = false;
            GameScene.Scene.DuraStatusPanel.Character.Index = 2113;
        }
        public override void Show()
        {
            if (Visible) return;
            Visible = true;
            GameScene.Scene.DuraStatusPanel.Character.Index = 2110;

            GetCharacterDura();
        }
    }
    public sealed class GroupStatusDialog : MirImageControl
    {
        public MirButton GroupStatusSwitch;
        public GroupStatusDialog()
        {
            Size = new Size(30, 30);
            Location = new Point((GameScene.Scene.MiniMapDialog.Location.X + 63), GameScene.Scene.MiniMapDialog.Size.Height);

            GroupStatusSwitch = new MirButton()
            {
                Index = 1335,
                Library = Libraries.Prguse2,
                Parent = this,
                HoverIndex = 1333,
                PressedIndex = 1334,
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.GroupHealthPanel
            };
            GroupStatusSwitch.Click += (o, e) =>
            {
                if (GameScene.Scene.GroupHealthPanel.Visible == true)
                {
                    GameScene.Scene.GroupHealthPanel.Hide();
                }
                else
                {
                    GameScene.Scene.GroupHealthPanel.Show();
                }
            };
        }

    }

    public class GroupHealthPanel : MirImageControl
    {
        private const int MaxMembers = 7;
        private const int PanelWidth = 160;
        private const int NameYOffset = 200;
        private const int HealthYOffset = 220;
        private const int HealthBarHeight = 8;

        private readonly List<PlayerUI> playerUIList;
        private readonly int clientWidth = Program.Form.ClientSize.Width;

        public GroupHealthPanel()
        {
            Size = new Size(PanelWidth, 160);

            playerUIList = new List<PlayerUI>();

            for (int i = 0; i < MaxMembers; i++)
            {
                PlayerUI playerUI = new PlayerUI
                {
                    NameLabel = new MirLabel
                    {
                        Location = new Point(clientWidth - 140, NameYOffset + i * 30),
                        AutoSize = true,
                        Parent = this,
                        NotControl = true,
                        OutLineColour = Color.Black,
                        OutLine = true,
                        DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                        Visible = false
                    },
                    HealthBar = new MirImageControl
                    {
                        Index = 1331,
                        Library = Libraries.Prguse2,
                        Location = new Point(clientWidth - 140, HealthYOffset + i * 30),
                        NotControl = true,
                        Parent = this,
                        DrawImage = false,
                        Visible = false
                    }
                };

                playerUI.HealthBar.BeforeDraw += Health_BeforeDraw;
                playerUIList.Add(playerUI);
            }
        }

        protected internal override void DrawControl()
        {
            base.DrawControl();

            if (!Visible) return;

            if (GroupDialog.GroupList == null || GroupDialog.GroupList.Count <= 1)
            {
                Hide();
                return;
            }

            Show();

            Size = new Size(PanelWidth, Math.Min(GroupDialog.GroupList.Count, MaxMembers) * 30);

            int playerIndex = GroupDialog.GroupList.IndexOf(GameScene.User.Name);

            for (int i = 0; i < GroupDialog.GroupList.Count; i++)
            {
                int memberIndex = (playerIndex + i) % GroupDialog.GroupList.Count;
                string memberName = GroupDialog.GroupList[memberIndex];
                PlayerObject player = MapControl.Objects.OfType<PlayerObject>().FirstOrDefault(p => p.Name == memberName);

                double healthPercent = player != null ? player.PercentHealth : 0;

                PlayerUI playerUI = playerUIList[i];

                playerUI.NameLabel.Text = memberName;
                playerUI.NameLabel.Visible = true;

                if (player == null)
                {
                    playerUI.NameLabel.ForeColour = Color.OrangeRed;
                    healthPercent = 1;
                    UpdatePlayerHealth(playerUI.HealthBar, healthPercent);                    
                    playerUI.HealthBar.Visible = true;
                    continue;
                }
                else if (healthPercent <= 0)
                {
                    playerUI.NameLabel.ForeColour = Color.Gray;
                }
                else
                {
                    playerUI.NameLabel.ForeColour = Color.White;
                }

                UpdatePlayerHealth(playerUI.HealthBar, healthPercent);
            }

            for (int i = GroupDialog.GroupList.Count; i < MaxMembers; ++i)
            {
                playerUIList[i].NameLabel.Visible = false;
                playerUIList[i].HealthBar.Visible = false;
                if (playerUIList[i].LeaderIcon != null) playerUIList[i].LeaderIcon.Visible = false;
            }
        }

        private void UpdatePlayerHealth(MirImageControl healthBar, double percent)
        {
            int healthBarLength = (int)(PanelWidth * Math.Clamp(percent, 0, 1));
            healthBar.Size = new Size(healthBarLength, HealthBarHeight);
            healthBar.Visible = true;
        }

        private void Health_BeforeDraw(object sender, EventArgs e)
        {
            MirImageControl healthControl = (MirImageControl)sender;

            if (healthControl.Library == null) return;

            PlayerUI playerUI = playerUIList.FirstOrDefault(ui => ui.HealthBar == healthControl);
            if (playerUI == null) return;

            string playerName = playerUI.NameLabel.Text;
            if (string.IsNullOrEmpty(playerName)) return;

            PlayerObject player = MapControl.Objects.OfType<PlayerObject>().FirstOrDefault(p => p.Name == playerName);
            if (player == null) return;

            double percent = player.PercentHealth / 100f;
            if (percent > 1) percent = 1;
            if (percent <= 0) return;

            Rectangle section = new Rectangle
            {
                Size = new Size((int)(healthControl.Size.Width * percent), healthControl.Size.Height)
            };

            healthControl.Library.Draw(healthControl.Index, section, healthControl.DisplayLocation, Color.White, false);
        }

        public override void Show()
        {
            if (Visible) return;
            Visible = true;
            GameScene.Scene.GroupStatusPanel.GroupStatusSwitch.Index = 1332;
        }

        public override void Hide()
        {
            if (!Visible) return;

            Visible = false;
            GameScene.Scene.GroupStatusPanel.GroupStatusSwitch.Index = 1335;
        }
        public class PlayerUI
        {
            public MirLabel NameLabel { get; set; }
            public MirImageControl HealthBar { get; set; }
            public MirImageControl LeaderIcon { get; set; }
        }
    }
}
