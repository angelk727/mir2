using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class AssistDialog : MirImageControl
    {
        public MirCheckBox[] TabPageButton;
        public static string[] Pages = { "基本设置", "职业设置", "保护设置" };

        public static int BASE = 0;
        public static int CLASS = 1;
        public static int PROTECT = 2;
        public static int ITEM = 3;

        public static int PageSize = 10;

        public MirButton CloseButton;
        public MirCheckBox CheckBoxFreeShift, CheckBoxSmartFire, CheckBoxShowGroupInfo, CheckBoxHideDead, CheckBoxShowMonsterName, CheckBoxShowNPCName;
        public MirCheckBox CheckBoxSmartDaMo, CheckBoxSmartYiJinJin;
        public MirCheckBox CheckBoxJinGang;
        public MirCheckBox CheckBoxSmartSheild, CheckBoxAutoPick, CheckBoxChangePoison, CheckBoxSpaceThrusting;
        public MirCheckBox CheckBoxShowLevel, CheckBoxShowTransform, CheckBoxShowGuildName;
        public MirCheckBox CheckBoxShowPing, CheckBoxShowHealth, CheckBoxShowDamage, CheckBoxShowHeal, CheckBoxHideSystem2;

        public MirCheckBox CheckBoxProtect;
        public MirLabel[] LabelProtect;
        public MirLabel[] LabelUse;
        public MirLabel[] LabelTip;
        public MirTextBox[] TextBoxProtectPercent;
        public MirTextBox[] TextBoxProtectItem;
        public MirTextBox TextBoxSpell;

        public MirCheckBox[] CheckBoxItemFilter;
        public MirButton PreviousButton, NextButton;
        public MirLabel PageNumberLabel;

        private int Page = 0;
        private int StartIndex = 0;
        private int maxPage
        {
            get
            {
                return (int)Math.Ceiling((double)GameScene.Scene.AssistHelper.ItemFilterList.Count / PageSize);
            }
        }


        public AssistDialog()
        {
            Index = 33;
            Library = Libraries.Prguse3;

            Movable = true;
            Sort = true;
            Location = Center;


            TabPageButton = new MirCheckBox[Pages.Length];
            for (int i = 0; i < TabPageButton.Length; ++i)
            {
                int j = i;
                TabPageButton[i] = new MirCheckBox
                {
                    Index = 13 + i * 2,
                    UnTickedIndex = 13 + i * 2,
                    TickedIndex = 14 + i * 2,
                    Parent = this,
                    Location = new Point(22 + i * 94, 14),
                    Library = Libraries.Prguse3
                };
                TabPageButton[i].Click += (o, e) => SwitchTab(j);
            }


            #region 基本
            CheckBoxFreeShift = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 70), Library = Libraries.Prguse };
            CheckBoxFreeShift.Checked = Settings.FreeShift;
            CheckBoxFreeShift.LabelText = "免shift";
            CheckBoxFreeShift.Click += CheckBoxFreeShiftClick;

            CheckBoxShowLevel = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 95), Library = Libraries.Prguse };
            CheckBoxShowLevel.LabelText = "显示等级";
            CheckBoxShowLevel.Checked = Settings.ShowLevel;
            CheckBoxShowLevel.Click += CheckBoxShowLevelClick;

            CheckBoxShowTransform = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 120), Library = Libraries.Prguse };
            CheckBoxShowTransform.LabelText = "显示时装";
            CheckBoxShowTransform.Checked = Settings.ShowTransform;
            CheckBoxShowTransform.Click += CheckBoxShowTransformClick;

            CheckBoxShowGuildName = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(150, 70), Library = Libraries.Prguse };
            CheckBoxShowGuildName.LabelText = "显示公会名";
            CheckBoxShowGuildName.Checked = Settings.ShowGuildName;
            CheckBoxShowGuildName.Click += (o, e) =>
            {
                Settings.ShowGuildName = !Settings.ShowGuildName;
            };

            CheckBoxShowGroupInfo = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(150, 95), Library = Libraries.Prguse };
            CheckBoxShowGroupInfo.LabelText = "显示组队信息";
            CheckBoxShowGroupInfo.Checked = Settings.ShowGroupInfo;
            CheckBoxShowGroupInfo.Click += (o, e) =>
            {
                Settings.ShowGroupInfo = !Settings.ShowGroupInfo;
            };

            CheckBoxShowDamage = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(150, 120), Library = Libraries.Prguse };
            CheckBoxShowDamage.LabelText = "显示伤害";
            CheckBoxShowDamage.Checked = Settings.DisplayDamage;
            CheckBoxShowDamage.Click += (o, e) =>
            {
                Settings.DisplayDamage = !Settings.DisplayDamage;
            };

            CheckBoxShowHeal = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(150, 145), Library = Libraries.Prguse };
            CheckBoxShowHeal.LabelText = "显示恢复";
            CheckBoxShowHeal.Checked = Settings.ShowHeal;
            CheckBoxShowHeal.Click += (o, e) =>
            {
                Settings.ShowHeal = !Settings.ShowHeal;
            };


            CheckBoxHideDead = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(150, 170), Library = Libraries.Prguse };
            CheckBoxHideDead.LabelText = "隐藏尸体";
            CheckBoxHideDead.Checked = Settings.HideDead;
            CheckBoxHideDead.Click += (o, e) =>
            {
                Settings.HideDead = !Settings.HideDead;
            };

            CheckBoxShowMonsterName = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(282, 70), Library = Libraries.Prguse };
            CheckBoxShowMonsterName.LabelText = "怪物显名";
            CheckBoxShowMonsterName.Checked = Settings.ShowMonsterName;
            CheckBoxShowMonsterName.Click += (o, e) =>
            {
                Settings.ShowMonsterName = !Settings.ShowMonsterName;
            };

            CheckBoxHideSystem2 = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(282, 95), Library = Libraries.Prguse };
            CheckBoxHideSystem2.LabelText = "隐藏掉落通知";
            CheckBoxHideSystem2.Checked = Settings.HideSystem2;
            CheckBoxHideSystem2.Click += (o, e) =>
            {
                Settings.HideSystem2 = !Settings.HideSystem2;
            };

            CheckBoxShowNPCName = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(282, 120), Library = Libraries.Prguse };
            CheckBoxShowNPCName.LabelText = "NPC显名";
            CheckBoxShowNPCName.Checked = Settings.ShowNPCName;
            CheckBoxShowNPCName.Click += (o, e) =>
            {
                Settings.ShowNPCName = !Settings.ShowNPCName;
            };

            CheckBoxShowPing = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 145), Library = Libraries.Prguse };
            CheckBoxShowPing.LabelText = "显示Ping";
            CheckBoxShowPing.Checked = Settings.ShowPing;
            CheckBoxShowPing.Click += (o, e) =>
            {
                Settings.ShowPing = CheckBoxShowPing.Checked;
            };

            CheckBoxShowHealth = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 170), Library = Libraries.Prguse };
            CheckBoxShowHealth.LabelText = "显示血量";
            CheckBoxShowHealth.Checked = Settings.ShowHealth;
            CheckBoxShowHealth.Click += (o, e) => { Settings.ShowHealth = CheckBoxShowHealth.Checked; };

            #endregion

            #region 职业

            CheckBoxSmartFire = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 70), Library = Libraries.Prguse };
            CheckBoxSmartFire.LabelText = "自动烈火";
            CheckBoxSmartFire.Checked = Settings.SmartFireHit;
            CheckBoxSmartFire.Click += CheckBoxFireClick;

            CheckBoxSpaceThrusting = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 95), Library = Libraries.Prguse };
            CheckBoxSpaceThrusting.LabelText = "自动双龙";
            CheckBoxSpaceThrusting.Checked = Settings.自动双龙斩;
            CheckBoxSpaceThrusting.Click += CheckBoxSpaceThrustingClick;

            CheckBoxSmartSheild = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(150, 70), Library = Libraries.Prguse };
            CheckBoxSmartSheild.LabelText = "自动开盾";
            CheckBoxSmartSheild.Checked = Settings.SmartSheild;
            CheckBoxSmartSheild.Click += CheckBoxSheildClick;

            CheckBoxChangePoison = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(301, 70), Library = Libraries.Prguse };
            CheckBoxChangePoison.LabelText = "自动毒符";
            CheckBoxChangePoison.Checked = Settings.SmartChangePoison;
            CheckBoxChangePoison.Click += CheckBoxChangePoisonClick;

            CheckBoxJinGang = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(301, 145), Library = Libraries.Prguse };
            CheckBoxJinGang.LabelText = "自动金刚术";
            CheckBoxJinGang.Checked = Settings.SmartElementalBarrier;
            CheckBoxJinGang.Click += (o, e) => Settings.SmartElementalBarrier = CheckBoxJinGang.Checked;
            #endregion

            #region 保护
            CheckBoxProtect = new MirCheckBox { Index = 2086, UnTickedIndex = 2086, TickedIndex = 2087, Parent = this, Location = new Point(26, 70), Library = Libraries.Prguse };
            CheckBoxProtect.LabelText = "开启保护";
            CheckBoxProtect.Checked = Settings.开启保护;
            CheckBoxProtect.Click += CheckBoxProtectClick;

            LabelProtect = new MirLabel[3];
            LabelUse = new MirLabel[3];
            TextBoxProtectPercent = new MirTextBox[3];
            TextBoxProtectItem = new MirTextBox[3];
            TextBoxSpell = new MirTextBox();
            LabelTip = new MirLabel[3];

            CreateProtectControls(0, 0);
            CreateProtectControls(1, 1);
            CreateProtectControls(2, 0);
            #endregion

            #region 物品
            CheckBoxAutoPick = new MirCheckBox
            {
                Index = 2086,
                UnTickedIndex = 2086,
                TickedIndex = 2087,
                Parent = this,
                Location = new Point(26, 69),
                Library = Libraries.Prguse
            };
            CheckBoxAutoPick.LabelText = "自动拾取";
            CheckBoxAutoPick.Checked = Settings.AutoPick;
            CheckBoxAutoPick.Click += CheckBoxAutoPickClick;

            CheckBoxItemFilter = new MirCheckBox[PageSize];
            for (int i = 0; i < PageSize; i++)
            {
                int x = i < PageSize / 2 ? 150 : 300;
                int y = i < PageSize / 2 ? 70 + 20 * i : 70 + 20 * (i - 5);
                int j = i;
                CheckBoxItemFilter[i] = new MirCheckBox
                {
                    Index = 2086,
                    UnTickedIndex = 2086,
                    TickedIndex = 2087,
                    Parent = this,
                    Location = new Point(x, y),
                    Library = Libraries.Prguse
                };
                CheckBoxItemFilter[i].Click += (o, e) => CheckBoxItemFilterClick(j);
            }

            PageNumberLabel = new MirLabel
            {
                Text = "",
                Parent = this,
                Size = new Size(83, 17),
                Location = new Point(217, 170),
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Font = new Font(Settings.FontName, 7F),
            };

            PreviousButton = new MirButton
            {
                Index = 240,
                HoverIndex = 241,
                PressedIndex = 242,
                Library = Libraries.Prguse2,
                Parent = this,
                Location = new Point(220, 172),
                Sound = SoundList.ButtonA,
            };
            PreviousButton.Click += (o, e) =>
            {
                Page--;
                if (Page < 0) Page = 0;
                StartIndex = PageSize * Page;

                UpdateItemFilters();
            };

            NextButton = new MirButton
            {
                Index = 243,
                HoverIndex = 244,
                PressedIndex = 245,
                Library = Libraries.Prguse2,
                Parent = this,
                Location = new Point(280, 172),
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                Page++;
                if ((Page + 1) > maxPage) Page--;
                StartIndex = PageSize * Page;
                UpdateItemFilters();
            };

            #endregion

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(428, 1),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();
        }

        private void CheckBoxItemFilterClick(int j)
        {
            string name = CheckBoxItemFilter[j].LabelText;
            if (GameScene.Scene.AssistHelper.ItemFilterList.ContainsKey(name))
                GameScene.Scene.AssistHelper.ItemFilterList[name].Pick = CheckBoxItemFilter[j].Checked;
        }

        private void UpdateItemFilters()
        {
            ItemFilter[] items = GameScene.Scene.AssistHelper.ItemFilterList.Values.ToArray();
            for (int i = 0; i < PageSize; ++i)
            {
                if (StartIndex + i < items.Length)
                {
                    CheckBoxItemFilter[i].Visible = true;
                    CheckBoxItemFilter[i].LabelText = items[StartIndex + i].Name;
                    CheckBoxItemFilter[i].Checked = items[StartIndex + i].Pick;
                }
                else
                {
                    CheckBoxItemFilter[i].Visible = false;
                }
            }

            PageNumberLabel.Text = (Page + 1) + " / " + maxPage;
        }

        private void CreateProtectControls(int index, int type)
        {
            int height = 95 + 25 * index;
            LabelProtect[index] = new MirLabel { AutoSize = true, Location = new Point(26, height), Parent = this, Text = type == 0 ? "生命百分比低于" : "魔法百分比低于" };

            TextBoxProtectPercent[index] = new MirTextBox
            {
                Location = new Point(120, height),
                Parent = this,
                Size = new Size(30, 15),
                MaxLength = Globals.MaxPasswordLength,
                OnlyNumber = true,
                CanLoseFocus = true,
                FocusWhenVisible = false,
                Font = new Font(Settings.FontName, 8F),
                BackColour = Color.White,
                ForeColour = Color.Black
            };
            TextBoxProtectPercent[index].TextBox.TextChanged += (o, e) => PercentPercentTextBox_changed(index);
            TextBoxProtectPercent[index].Text = String.Format("{0}", GameScene.Scene.AssistHelper.GetProtectPercent(index));

            LabelUse[index] = new MirLabel { AutoSize = true, Location = new Point(155, height), Parent = this, Text = "使用" };

            TextBoxProtectItem[index] = new MirTextBox
            {
                Location = new Point(200, height),
                Parent = this,
                Size = new Size(80, 15),
                MaxLength = Globals.MaxPasswordLength,
                CanLoseFocus = true,
                FocusWhenVisible = false,
                Font = new Font(Settings.FontName, 8F),
                BackColour = Color.White,
                ForeColour = Color.Black
            };
            TextBoxProtectItem[index].TextBox.TextChanged += (o, e) => PercentItemTextBox_changed(index);
            TextBoxProtectItem[index].Text = GameScene.Scene.AssistHelper.GetProtectItemName(index);
            LabelTip[index] = new MirLabel { AutoSize = true, Location = new Point(285, height), Parent = this, Text = "填关键字" };
        }

        private void PercentItemTextBox_changed(int i)
        {
            GameScene.Scene.AssistHelper.SetProtectItemName(i, TextBoxProtectItem[i].Text);
        }

        private void PercentPercentTextBox_changed(int i)
        {
            int temp = 0;
            int.TryParse(TextBoxProtectPercent[i].Text, out temp);
            GameScene.Scene.AssistHelper.SetProtectPercent(i, temp);
        }

        private void SwitchTab(int j)
        {
            for (int i = 0; i < TabPageButton.Length; ++i)
                TabPageButton[i].Checked = i == j;

            CheckBoxFreeShift.Visible = j == BASE;
            CheckBoxShowLevel.Visible = j == BASE;
            CheckBoxShowTransform.Visible = j == BASE;
            CheckBoxShowPing.Visible = j == BASE;
            CheckBoxShowGuildName.Visible = j == BASE;
            CheckBoxShowGroupInfo.Visible = j == BASE;
            CheckBoxShowHealth.Visible = j == BASE;
            CheckBoxShowDamage.Visible = j == BASE;
            CheckBoxShowHeal.Visible = j == BASE;
            CheckBoxHideDead.Visible = j == BASE;
            CheckBoxShowMonsterName.Visible = j == BASE;
            CheckBoxShowNPCName.Visible = j == BASE;
            CheckBoxHideSystem2.Visible = j == BASE;

            CheckBoxSmartFire.Visible = j == CLASS;
            //CheckBoxSmartDaMo.Visible = j == CLASS;
            CheckBoxChangePoison.Visible = j == CLASS;
            CheckBoxJinGang.Visible = j == CLASS;

            //CheckBoxSmartYiJinJin.Visible = j==CLASS;

            CheckBoxProtect.Visible = j == PROTECT;

            for (int i = 0; i < 3; i++)
            {
                LabelProtect[i].Visible = j == PROTECT;
                LabelUse[i].Visible = j == PROTECT;
                TextBoxProtectPercent[i].Visible = j == PROTECT;
                TextBoxProtectItem[i].Visible = j == PROTECT;
                LabelTip[i].Visible = j == PROTECT;
            }
            TextBoxSpell.Visible = j == BASE;


            CheckBoxSmartSheild.Visible = j == CLASS;
            CheckBoxChangePoison.Visible = j == CLASS;
            CheckBoxSpaceThrusting.Visible = j == CLASS;

            CheckBoxAutoPick.Visible = j == ITEM;
            PreviousButton.Visible = j == ITEM;
            NextButton.Visible = j == ITEM;
            PageNumberLabel.Visible = j == ITEM;
            for (int i = 0; i < PageSize; i++)
                CheckBoxItemFilter[i].Visible = j == ITEM;

            if (j == ITEM)
                UpdateItemFilters();
        }

        private void AutoAttackClick()
        {
            GameScene.Scene.AssistHelper.AutoAttack = !GameScene.Scene.AssistHelper.AutoAttack;
            GameScene.Scene.AssistHelper.ClearAttack();
            if (GameScene.Scene.AssistHelper.AutoAttack)
                GameScene.Scene.ChatDialog.ReceiveChat("开始自动攻击", ChatType.System);
            else
                GameScene.Scene.ChatDialog.ReceiveChat("结束自动攻击", ChatType.System);
        }

        private void CheckBoxChangePoisonClick(object sender, EventArgs e)
        {
            Settings.SmartChangePoison = CheckBoxChangePoison.Checked;
        }

        private void CheckBoxAutoPickClick(object sender, EventArgs e)
        {
            Settings.AutoPick = CheckBoxAutoPick.Checked;
        }

        private void CheckBoxShowTransformClick(object sender, EventArgs e)
        {
            Settings.ShowTransform = CheckBoxShowTransform.Checked;
            MapObject.User.SetLibraries();
        }

        private void CheckBoxSheildClick(object sender, EventArgs e)
        {
            Settings.SmartSheild = CheckBoxSmartSheild.Checked;
        }

        private void CheckBoxSpaceThrustingClick(object sender, EventArgs e)
        {
            Settings.自动双龙斩 = CheckBoxSpaceThrusting.Checked;
        }

        private void CheckBoxShowLevelClick(object sender, EventArgs e)
        {
            Settings.ShowLevel = CheckBoxShowLevel.Checked;
        }

        private void CheckBoxProtectClick(object sender, EventArgs e)
        {
            Settings.开启保护 = CheckBoxProtect.Checked;
        }

        private void CheckBoxFireClick(object sender, EventArgs e)
        {
            Settings.SmartFireHit = CheckBoxSmartFire.Checked;
        }

        private void CheckBoxFreeShiftClick(object sender, EventArgs e)
        {
            Settings.FreeShift = CheckBoxFreeShift.Checked;
        }

        public override void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }

        public override void Show()
        {
            if (Visible) return;
            SwitchTab(0);
            Visible = true;
        }
    }
}
