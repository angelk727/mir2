using Client.MirControls;
using Client.MirGraphics;
using Client.MirObjects;
using Client.MirSounds;

namespace Client.MirScenes.Dialogs
{
    public sealed class CharacterDialog : MirImageControl
    {
        public MirButton CloseButton, CharacterButton, StatusButton, StateButton, SkillButton;
        public MirImageControl CharacterPage, StatusPage, StatePage, SkillPage, ClassImage;

        public MirLabel NameLabel, GuildLabel, LoverLabel;
        public MirLabel ACLabel, MACLabel, DCLabel, MCLabel, SCLabel, HealthLabel, ManaLabel;
        public MirLabel CritRLabel, CritDLabel, LuckLabel, AttkSpdLabel, AccLabel, AgilLabel;
        public MirLabel ExpPLabel, BagWLabel, WearWLabel, HandWLabel, MagicRLabel, PoisonRecLabel, HealthRLabel, ManaRLabel, PoisonResLabel, HolyTLabel, FreezeLabel, PoisonAtkLabel, ReflectAtkLabel, HPDrainRatePercentLabel;
        public MirLabel HeadingLabel, StatLabel;
        public MirButton NextButton, BackButton;

        public MirItemCell[] Grid;
        private MirGridType GridType;
        public MagicButton[] Magics;

        public int StartIndex;
        private UserObject Actor;

        public CharacterDialog(MirGridType gridType, UserObject actor)
        {
            Actor = actor;
            GridType = gridType;

            Index = gridType == MirGridType.HeroEquipment ? 505 : 504;
            Library = Libraries.Title;
            Location = new Point(Settings.ScreenWidth - 264, 0);
            Movable = true;
            Sort = true;            

            BeforeDraw += (o, e) => RefreshInterface();

            CharacterPage = new MirImageControl
            {
                Index = 340,
                Parent = this,
                Library = Libraries.Prguse,
                Location = new Point(8, 90),
            };
            CharacterPage.AfterDraw += (o, e) =>
            {
                if (Libraries.StateItems == null) return;
                ItemInfo RealItem = null;
                if (Grid[(int)EquipmentSlot.盔甲].Item != null)
                {
                    RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.盔甲].Item.Info, actor.Level, actor.Class, GameScene.ItemInfoList);
                    Libraries.StateItems.Draw(RealItem.Image, DisplayLocation, Color.White, true, 1F);
                    {
                        if (actor.WingEffect > 0)
                        {
                            int genderOffset = actor.Gender == MirGender.男性 ? 0 : 1;

                            switch (actor.WingEffect)
                            {
                                case 1:
                                    if (actor.WingEffect == 1)
                                        Libraries.Prguse2.DrawBlend(1202 + genderOffset, DisplayLocation, Color.White, true, 1F);
                                    break;
                                case 2:
                                    if (actor.WingEffect == 2)
                                        Libraries.Prguse2.DrawBlend(1204 + genderOffset, DisplayLocation, Color.White, true, 1F);
                                    break;
                                case 58:
                                    if (actor.WingEffect == 58)
                                        Libraries.Prguse2.DrawBlend(1530 + genderOffset, DisplayLocation, Color.White, true, 1F);
                                    break;
                                case 59:
                                    if (actor.WingEffect == 59)
                                        Libraries.Prguse2.DrawBlend(1532 + genderOffset, DisplayLocation, Color.White, true, 1F);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                if (Grid[(int)EquipmentSlot.武器].Item != null)
                {
                    RealItem = Functions.GetRealItem(Grid[(int)EquipmentSlot.武器].Item.Info, actor.Level, actor.Class, GameScene.ItemInfoList);
                    Libraries.StateItems.Draw(RealItem.Image, DisplayLocation, Color.White, true, 1F);

                    if (actor.WeaponEffect > 0)
                    {
                        switch (actor.WeaponEffect)
                        {
                            case 21:
                                if (actor.WeaponEffect == 21)
                                    Libraries.StateitemEffect.DrawBlend(4, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 22:
                                if (actor.WeaponEffect == 22)
                                    Libraries.StateitemEffect.DrawBlend(20, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 23:
                                if (actor.WeaponEffect == 23)
                                    Libraries.StateitemEffect.DrawBlend(0, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 26:
                                if (actor.WeaponEffect == 26)
                                    Libraries.StateitemEffect.DrawBlend(24, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 27:
                                if (actor.WeaponEffect == 27)
                                    Libraries.StateitemEffect.DrawBlend(28, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 28:
                                if (actor.WeaponEffect == 28)
                                    Libraries.StateitemEffect.DrawBlend(32, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 29:
                                if (actor.WeaponEffect == 29)
                                    Libraries.StateitemEffect.DrawBlend(12, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 30:
                                if (actor.WeaponEffect == 30)
                                    Libraries.StateitemEffect.DrawBlend(16, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 31:
                                if (actor.WeaponEffect == 31)
                                    Libraries.StateitemEffect.DrawBlend(8, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 32:
                                if (actor.WeaponEffect == 32)
                                    Libraries.StateitemEffect.DrawBlend(36, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 33:
                                if (actor.WeaponEffect == 33)
                                    Libraries.StateitemEffect.DrawBlend(40, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 34:
                                if (actor.WeaponEffect == 34)
                                    Libraries.StateitemEffect.DrawBlend(44, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 35:
                                if (actor.WeaponEffect == 35)
                                    Libraries.StateitemEffect.DrawBlend(52, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 36:
                                if (actor.WeaponEffect == 36)
                                    Libraries.StateitemEffect.DrawBlend(60, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 37:
                                if (actor.WeaponEffect == 37)
                                    Libraries.StateitemEffect.DrawBlend(56, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 38:
                                if (actor.WeaponEffect == 38)
                                    Libraries.StateitemEffect.DrawBlend(64, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 39:
                                if (actor.WeaponEffect == 39)
                                    Libraries.StateitemEffect.DrawBlend(68, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 40:
                                if (actor.WeaponEffect == 40)
                                    Libraries.StateitemEffect.DrawBlend(72, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 41:
                                if (actor.WeaponEffect == 41)
                                    Libraries.StateitemEffect.DrawBlend(48, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 43:
                                if (actor.WeaponEffect == 43)
                                    Libraries.StateItems.DrawBlend(922, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 45:
                                if (actor.WeaponEffect == 45)
                                    Libraries.StateitemEffect.DrawBlend(76, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 51:
                                if (actor.WeaponEffect == 51)
                                    Libraries.StateitemEffect.DrawBlend(108, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 52:
                                if (actor.WeaponEffect == 52)
                                    Libraries.StateitemEffect.DrawBlend(124, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 53:
                                if (actor.WeaponEffect == 53)
                                    Libraries.StateitemEffect.DrawBlend(104, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 56:
                                if (actor.WeaponEffect == 56)
                                    Libraries.StateitemEffect.DrawBlend(128, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 57:
                                if (actor.WeaponEffect == 57)
                                    Libraries.StateitemEffect.DrawBlend(132, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 58:
                                if (actor.WeaponEffect == 58)
                                    Libraries.StateitemEffect.DrawBlend(136, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 59:
                                if (actor.WeaponEffect == 59)
                                    Libraries.StateitemEffect.DrawBlend(116, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 60:
                                if (actor.WeaponEffect == 60)
                                    Libraries.StateitemEffect.DrawBlend(120, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 61:
                                if (actor.WeaponEffect == 61)
                                    Libraries.StateitemEffect.DrawBlend(112, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 62:
                                if (actor.WeaponEffect == 62)
                                    Libraries.StateitemEffect.DrawBlend(140, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 63:
                                if (actor.WeaponEffect == 63)
                                    Libraries.StateitemEffect.DrawBlend(144, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 64:
                                if (actor.WeaponEffect == 64)
                                    Libraries.StateitemEffect.DrawBlend(148, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 65:
                                if (actor.WeaponEffect == 65)
                                    Libraries.StateitemEffect.DrawBlend(156, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 66:
                                if (actor.WeaponEffect == 66)
                                    Libraries.StateitemEffect.DrawBlend(164, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 67:
                                if (actor.WeaponEffect == 67)
                                    Libraries.StateitemEffect.DrawBlend(160, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 68:
                                if (actor.WeaponEffect == 68)
                                    Libraries.StateitemEffect.DrawBlend(168, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 69:
                                if (actor.WeaponEffect == 69)
                                    Libraries.StateitemEffect.DrawBlend(172, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 70:
                                if (actor.WeaponEffect == 70)
                                    Libraries.StateitemEffect.DrawBlend(176, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 71:
                                if (actor.WeaponEffect == 71)
                                    Libraries.StateitemEffect.DrawBlend(152, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 75:
                                if (actor.WeaponEffect == 75)
                                    Libraries.StateitemEffect.DrawBlend(180, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 104:
                                if (actor.WeaponEffect == 104)
                                    Libraries.StateitemEffect.DrawBlend(80, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 105:
                                if (actor.WeaponEffect == 105)
                                    Libraries.StateitemEffect.DrawBlend(84, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 106:
                                if (actor.WeaponEffect == 106)
                                    Libraries.StateitemEffect.DrawBlend(88, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 107:
                                if (actor.WeaponEffect == 107)
                                    Libraries.StateitemEffect.DrawBlend(92, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 108:
                                if (actor.WeaponEffect == 108)
                                    Libraries.StateitemEffect.DrawBlend(96, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 109:
                                if (actor.WeaponEffect == 109)
                                    Libraries.StateitemEffect.DrawBlend(100, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 114:
                                if (actor.WeaponEffect == 114)
                                    Libraries.StateitemEffect.DrawBlend(184, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 115:
                                if (actor.WeaponEffect == 115)
                                    Libraries.StateitemEffect.DrawBlend(188, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 116:
                                if (actor.WeaponEffect == 116)
                                    Libraries.StateitemEffect.DrawBlend(192, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 117:
                                if (actor.WeaponEffect == 117)
                                    Libraries.StateitemEffect.DrawBlend(196, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 118:
                                if (actor.WeaponEffect == 118)
                                    Libraries.StateitemEffect.DrawBlend(200, DisplayLocation, Color.White, true, 1F);
                                break;
                            case 119:
                                if (actor.WeaponEffect == 119)
                                    Libraries.StateitemEffect.DrawBlend(204, DisplayLocation, Color.White, true, 1F);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (Grid[(int)EquipmentSlot.头盔].Item != null)
                    Libraries.StateItems.Draw(Grid[(int)EquipmentSlot.头盔].Item.Info.Image, DisplayLocation, Color.White, true, 1F);
                else
                {
                    int hair = 441 + actor.Hair + (actor.Class == MirClass.刺客 ? 20 : 0) + (actor.Gender == MirGender.男性 ? 0 : 40);

                    int offSetX = actor.Class == MirClass.刺客 ? (actor.Gender == MirGender.男性 ? 6 : 4) : 0;
                    int offSetY = actor.Class == MirClass.刺客 ? (actor.Gender == MirGender.男性 ? 25 : 18) : 0;

                    Libraries.Prguse.Draw(hair, new Point(DisplayLocation.X + offSetX, DisplayLocation.Y + offSetY), Color.White, true, 1F);
                }
            };

            StatusPage = new MirImageControl
            {
                Index = 506,
                Parent = this,
                Library = Libraries.Title,
                Location = new Point(8, 90),
                Visible = false,
            };
            StatusPage.BeforeDraw += (o, e) =>
            {
                ACLabel.Text = string.Format("{0}-{1}", actor.Stats[Stat.MinAC], actor.Stats[Stat.MaxAC]);
                MACLabel.Text = string.Format("{0}-{1}", actor.Stats[Stat.MinMAC], actor.Stats[Stat.MaxMAC]);
                DCLabel.Text = string.Format("{0}-{1}", actor.Stats[Stat.MinDC], actor.Stats[Stat.MaxDC]);
                MCLabel.Text = string.Format("{0}-{1}", actor.Stats[Stat.MinMC], actor.Stats[Stat.MaxMC]);
                SCLabel.Text = string.Format("{0}-{1}", actor.Stats[Stat.MinSC], actor.Stats[Stat.MaxSC]);
                HealthLabel.Text = string.Format("{0}/{1}", actor.HP, actor.Stats[Stat.HP]);
                ManaLabel.Text = string.Format("{0}/{1}", actor.MP, actor.Stats[Stat.MP]);
                CritRLabel.Text = string.Format("{0}%", actor.Stats[Stat.暴击倍率]);
                CritDLabel.Text = string.Format("{0}", actor.Stats[Stat.暴击伤害]);
                AttkSpdLabel.Text = string.Format("{0}", actor.Stats[Stat.攻击速度]);
                AccLabel.Text = string.Format("+{0}", actor.Stats[Stat.准确]);
                AgilLabel.Text = string.Format("+{0}", actor.Stats[Stat.敏捷]);
                LuckLabel.Text = string.Format("{0}", actor.Stats[Stat.幸运]);
            };

            StatePage = new MirImageControl
            {
                Index = 507,
                Parent = this,
                Library = Libraries.Title,
                Location = new Point(8, 90),
                Visible = false
            };
            StatePage.BeforeDraw += (o, e) =>
            {
                ExpPLabel.Text = string.Format("{0:0.##%}", actor.Experience / (double)actor.MaxExperience);
                BagWLabel.Text = string.Format("{0}/{1}", actor.CurrentBagWeight, actor.Stats[Stat.背包负重]);
                WearWLabel.Text = string.Format("{0}/{1}", actor.CurrentWearWeight, actor.Stats[Stat.装备负重]);
                HandWLabel.Text = string.Format("{0}/{1}", actor.CurrentHandWeight, actor.Stats[Stat.腕力负重]);
                MagicRLabel.Text = string.Format("+{0}", actor.Stats[Stat.魔法躲避]);
                PoisonResLabel.Text = string.Format("+{0}", actor.Stats[Stat.毒物躲避]);
                HealthRLabel.Text = string.Format("+{0}", actor.Stats[Stat.生命恢复]);
                ManaRLabel.Text = string.Format("+{0}", actor.Stats[Stat.法力恢复]);
                PoisonRecLabel.Text = string.Format("+{0}", actor.Stats[Stat.中毒恢复]);
                HolyTLabel.Text = string.Format("+{0}", actor.Stats[Stat.神圣]);
                FreezeLabel.Text = string.Format("+{0}", actor.Stats[Stat.冰冻伤害]);
                PoisonAtkLabel.Text = string.Format("+{0}", actor.Stats[Stat.毒素伤害]);
                ReflectAtkLabel.Text = string.Format("+{0}", actor.Stats[Stat.反弹伤害]);
                HPDrainRatePercentLabel.Text = string.Format("+ {0}%", actor.Stats[Stat.吸血数率]);
            };


            SkillPage = new MirImageControl
            {
                Index = 508,
                Parent = this,
                Library = Libraries.Title,
                Location = new Point(8, 90),
                Visible = false
            };


            CharacterButton = new MirButton
            {
                Index = 500,
                Library = Libraries.Title,
                Location = new Point(8, 70),
                Parent = this,
                PressedIndex = 500,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA,
            };
            CharacterButton.Click += (o, e) => ShowCharacterPage();
            StatusButton = new MirButton
            {
                Library = Libraries.Title,
                Location = new Point(70, 70),
                Parent = this,
                PressedIndex = 501,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA
            };
            StatusButton.Click += (o, e) => ShowStatusPage();

            StateButton = new MirButton
            {
                Library = Libraries.Title,
                Location = new Point(132, 70),
                Parent = this,
                PressedIndex = 502,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA
            };
            StateButton.Click += (o, e) => ShowStatePage();

            SkillButton = new MirButton
            {
                Library = Libraries.Title,
                Location = new Point(194, 70),
                Parent = this,
                PressedIndex = 503,
                Size = new Size(64, 20),
                Sound = SoundList.ButtonA
            };
            SkillButton.Click += (o, e) => ShowSkillPage();

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

            NameLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                Location = new Point(0, 12),
                Size = new Size(264, 20),
                NotControl = true,
            };
            GuildLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                Location = new Point(0, 33),
                Size = new Size(264, 30),
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

            Grid = new MirItemCell[Enum.GetNames(typeof(EquipmentSlot)).Length];

            Grid[(int)EquipmentSlot.武器] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.武器,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(125, 8), //(123, 7)
            };


            Grid[(int)EquipmentSlot.盔甲] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.盔甲,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(165, 8), //(163, 7)
            };


            Grid[(int)EquipmentSlot.头盔] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.头盔,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(205, 8), //(203, 7)
            };



            Grid[(int)EquipmentSlot.照明物] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.照明物,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(205, 135), //203, 134
            };


            Grid[(int)EquipmentSlot.项链] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.项链,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(205, 99), //203, 98
            };


            Grid[(int)EquipmentSlot.左手镯] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.左手镯,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(10, 170), //(8, 170)
            };

            Grid[(int)EquipmentSlot.右手镯] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.右手镯,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(205, 171), //(203, 170)
            };

            Grid[(int)EquipmentSlot.左戒指] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.左戒指,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(10, 207), //(8, 206)
            };

            Grid[(int)EquipmentSlot.右戒指] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.右戒指,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(205, 207), //(203, 206)
            };


            Grid[(int)EquipmentSlot.护身符] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.护身符,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(10, 242), //(8, 242)
            };


            Grid[(int)EquipmentSlot.靴子] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.靴子,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(50, 242), //(48, 242)
            };

            Grid[(int)EquipmentSlot.腰带] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.腰带,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(90, 242), //(88, 242)
            };


            Grid[(int)EquipmentSlot.守护石] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.守护石,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(129, 242), //(130, 242)
            };

            Grid[(int)EquipmentSlot.坐骑] = new MirItemCell
            {
                ItemSlot = (int)EquipmentSlot.坐骑,
                GridType = gridType,
                Parent = CharacterPage,
                Location = new Point(204, 63), //(205, 63)
            };

            // STATS I
            HealthLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 20),
                NotControl = true,
                Text = "0-0",
            };

            ManaLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 38),
                NotControl = true,
                Text = "0-0",
            };

            ACLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 56),
                NotControl = true,
                Text = "0-0",
            };

            MACLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 74),
                NotControl = true,
                Text = "0-0",
            };
            DCLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 92),
                NotControl = true,
                Text = "0-0"
            };
            MCLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 110),
                NotControl = true,
                Text = "0/0"
            };
            SCLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 128),
                NotControl = true,
                Text = "0/0"
            };
            //Breezer - New Labels
            CritRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 146),
                NotControl = true
            };
            CritDLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 164),
                NotControl = true
            };
            AttkSpdLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 182),
                NotControl = true
            };
            AccLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 200),
                NotControl = true
            };
            AgilLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 218),
                NotControl = true
            };
            LuckLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatusPage,
                Location = new Point(126, 236),
                NotControl = true
            };
            // STATS II 
            ExpPLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 20),
                NotControl = true,
                Text = "0-0",
            };

            BagWLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 38),
                NotControl = true,
                Text = "0-0",
            };

            WearWLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 56),
                NotControl = true,
                Text = "0-0",
            };

            HandWLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 74),
                NotControl = true,
                Text = "0-0",
            };
            MagicRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 92),
                NotControl = true,
                Text = "0-0"
            };
            PoisonResLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 110),
                NotControl = true,
                Text = "0/0"
            };
            HealthRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 128),
                NotControl = true,
                Text = "0/0"
            };
            //Breezer
            ManaRLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 146),
                NotControl = true
            };
            PoisonRecLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 164),
                NotControl = true
            };
            HolyTLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 182),
                NotControl = true
            };
            FreezeLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 200),
                NotControl = true
            };
            PoisonAtkLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 218),
                NotControl = true
            };
            ReflectAtkLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 236),
                NotControl = true
            };
            HPDrainRatePercentLabel = new MirLabel
            {
                AutoSize = true,
                Parent = StatePage,
                Location = new Point(126, 254),
                NotControl = true
            };

            Magics = new MagicButton[7];

            for (int i = 0; i < Magics.Length; i++)
                Magics[i] = new MagicButton 
                { 
                    Parent = SkillPage, 
                    Visible = false, 
                    Location = new Point(8, 8 + i * 33),
                    HeroMagic = gridType == MirGridType.HeroEquipment
                };

            NextButton = new MirButton
            {
                Index = 396,
                Location = new Point(140, 250),
                Library = Libraries.Prguse,
                Parent = SkillPage,
                PressedIndex = 397,
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                if (StartIndex + 7 >= actor.Magics.Count) return;

                StartIndex += 7;
                RefreshInterface();
            };

            BackButton = new MirButton
            {
                Index = 398,
                Location = new Point(90, 250),
                Library = Libraries.Prguse,
                Parent = SkillPage,
                PressedIndex = 399,
                Sound = SoundList.ButtonA,
            };
            BackButton.Click += (o, e) =>
            {
                if (StartIndex - 7 < 0) return;

                StartIndex -= 7;
                RefreshInterface();
            };
        }

        public override void Show()
        {
            if (Visible) return;
            Visible = true;
        }

        public override void Hide()
        {
            GameScene.Scene.SocketDialog.Hide();
            base.Hide();
        }

        public void ShowCharacterPage()
        {
            CharacterPage.Visible = true;
            StatusPage.Visible = false;
            StatePage.Visible = false;
            SkillPage.Visible = false;
            CharacterButton.Index = 500;
            StatusButton.Index = -1;
            StateButton.Index = -1;
            SkillButton.Index = -1;
        }

        private void ShowStatusPage()
        {
            CharacterPage.Visible = false;
            StatusPage.Visible = true;
            StatePage.Visible = false;
            SkillPage.Visible = false;
            CharacterButton.Index = -1;
            StatusButton.Index = 501;
            StateButton.Index = -1;
            SkillButton.Index = -1;
        }

        private void ShowStatePage()
        {
            CharacterPage.Visible = false;
            StatusPage.Visible = false;
            StatePage.Visible = true;
            SkillPage.Visible = false;
            CharacterButton.Index = -1;
            StatusButton.Index = -1;
            StateButton.Index = 502;
            SkillButton.Index = -1;
        }

        public void ShowSkillPage()
        {
            CharacterPage.Visible = false;
            StatusPage.Visible = false;
            StatePage.Visible = false;
            SkillPage.Visible = true;
            CharacterButton.Index = -1;
            StatusButton.Index = -1;
            StateButton.Index = -1;
            SkillButton.Index = 503;
            StartIndex = 0;
        }

        private void RefreshInterface()
        {
            int offSet = Actor.Gender == MirGender.男性 ? 0 : 1;

            Index = GridType == MirGridType.HeroEquipment ? 505 : 504;
            CharacterPage.Index = 340 + offSet;

            switch (Actor.Class)
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

            NameLabel.Text = Actor.Name;
            GuildLabel.Text = Actor.GuildName + " " + Actor.GuildRankName;

            for (int i = 0; i < Magics.Length; i++)
            {
                if (i + StartIndex >= Actor.Magics.Count)
                {
                    Magics[i].Visible = false;
                    continue;
                }

                Magics[i].Visible = true;
                Magics[i].Update(Actor.Magics[i + StartIndex]);
            }
        }

        public MirItemCell GetCell(ulong id)
        {

            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i].Item == null || Grid[i].Item.UniqueID != id) continue;
                return Grid[i];
            }
            return null;
        }

    }
}
