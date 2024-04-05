using System.Text.RegularExpressions;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
namespace Client.MirScenes.Dialogs
{
    public sealed class NewCharacterDialog : MirImageControl
    {
        private static readonly Regex Reg = new Regex(@"^[A-Za-z0-9\u4E00-\u9FA5]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");

        public MirImageControl TitleLabel;
        public MirAnimatedControl CharacterDisplay;

        public MirButton OKButton,
                         CancelButton,
                         WarriorButton,
                         WizardButton,
                         TaoistButton,
                         AssassinButton,
                         ArcherButton,
                         MaleButton,
                         FemaleButton;

        public MirTextBox NameTextBox;

        public MirLabel Description;

        public MirClass Class;
        public MirGender Gender;

        #region Descriptions
            public const string WarriorDescription =
                "以强有力的体格为基础，特殊之处在于用剑法及刀法等技术。即便穿戴沉重的武器" +
                "及对打猎、战斗比较适用。 体力强的战士能带许多东西，铠甲也可以" +
                "自由活动。 但战士所戴的铠甲对魔法的防御能力相对较弱。";

            public const string WizardDescription =
                "以长时间锻炼的内功为基础，能发挥强大的攻击型魔法。魔法攻击力卓越，但体力较弱。对体力" +
                "上直接受到攻击的防御能力较低，另外，发挥高水平的魔法时需要较长时间，此时" +
                "可能受到对方的快速攻击。 魔法师的魔法比任何攻击能力都强大，能有效的威胁对方。";

            public const string TaoistDescription =
                "以强大的精神力作为基础，可以使用治疗术帮助别人。 对" +
                "自然很熟悉，在用毒方面的能力最强。 博学多知，能使用剑术和魔法" +
                "，所以每时每刻都能发挥多样的法术，随机应变性强。";

            public const string AssassinDescription =
                "以敏捷快速的攻击为基础，矫健的刺客还拥有超强的爆" +
                "发性他们熟悉各种技能 尤其擅长瞬移、潜行技能！ 他们是暗夜" +
                "的主人，是绝对的伤害高、攻击高、爆发型的职业。";

            public const string ArcherDescription =
                "强大的远程输出：作为一个名副其实的远程物理输出职业，弓箭最擅长在敌人攻击范围之外" +
                "对敌人造成致命打击。 多变：弓箭手永远是战场上的未知数，就必须练就准确的判断力，熟练掌握其操作技巧" +
                " 华丽：鲜艳的服装、优雅的射击动作和绚美的特效，非弓箭手莫属!";

        #endregion

        public NewCharacterDialog()
        {
            Index = 73;
            Library = Libraries.Prguse;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);
            Modal = true;

            TitleLabel = new MirImageControl
            {
                Index = 20,
                Library = Libraries.Title,
                Location = new Point(206, 11),
                Parent = this,
            };

            CancelButton = new MirButton
            {
                HoverIndex = 281,
                Index = 280,
                Library = Libraries.Title,
                Location = new Point(425, 425),
                Parent = this,
                PressedIndex = 282
            };
            CancelButton.Click += (o, e) => Hide();

            OKButton = new MirButton
            {
                Enabled = false,
                HoverIndex = 361,
                Index = 360,
                Library = Libraries.Title,
                Location = new Point(160, 425),
                Parent = this,
                PressedIndex = 362,
            };
            OKButton.Click += (o, e) => CreateCharacter();

            NameTextBox = new MirTextBox
            {
                Location = new Point(325, 268),
                Parent = this,
                Size = new Size(240, 20),
                MaxLength = Globals.MaxCharacterNameLength
            };
            NameTextBox.TextBox.KeyPress += TextBox_KeyPress;
            NameTextBox.TextBox.TextChanged += CharacterNameTextBox_TextChanged;
            NameTextBox.SetFocus();

            CharacterDisplay = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 250,
                Index = 20,
                Library = Libraries.ChrSel,
                Location = new Point(120, 250),
                Parent = this,
                UseOffSet = true,
            };
            CharacterDisplay.AfterDraw += (o, e) =>
            {
                if (Class == MirClass.法师)
                    Libraries.ChrSel.DrawBlend(CharacterDisplay.Index + 560, CharacterDisplay.DisplayLocationWithoutOffSet, Color.White, true);
            };


            WarriorButton = new MirButton
            {
                HoverIndex = 2427,
                Index = 2427,
                Library = Libraries.Prguse,
                Location = new Point(323, 296),
                Parent = this,
                PressedIndex = 2428,
                Sound = SoundList.ButtonA,
            };
            WarriorButton.Click += (o, e) =>
            {
                Class = MirClass.战士;
                UpdateInterface();
            };


            WizardButton = new MirButton
            {
                HoverIndex = 2430,
                Index = 2429,
                Library = Libraries.Prguse,
                Location = new Point(373, 296),
                Parent = this,
                PressedIndex = 2431,
                Sound = SoundList.ButtonA,
            };
            WizardButton.Click += (o, e) =>
            {
                Class = MirClass.法师;
                UpdateInterface();
            };


            TaoistButton = new MirButton
            {
                HoverIndex = 2433,
                Index = 2432,
                Library = Libraries.Prguse,
                Location = new Point(423, 296),
                Parent = this,
                PressedIndex = 2434,
                Sound = SoundList.ButtonA,
            };
            TaoistButton.Click += (o, e) =>
            {
                Class = MirClass.道士;
                UpdateInterface();
            };

            AssassinButton = new MirButton
            {
                HoverIndex = 2436,
                Index = 2435,
                Library = Libraries.Prguse,
                Location = new Point(473, 296),
                Parent = this,
                PressedIndex = 2437,
                Sound = SoundList.ButtonA,
            };
            AssassinButton.Click += (o, e) =>
            {
                Class = MirClass.刺客;
                UpdateInterface();
            };

            ArcherButton = new MirButton
            {
                HoverIndex = 2439,
                Index = 2438,
                Library = Libraries.Prguse,
                Location = new Point(523, 296),
                Parent = this,
                PressedIndex = 2440,
                Sound = SoundList.ButtonA,
            };
            ArcherButton.Click += (o, e) =>
            {
                Class = MirClass.弓箭;
                UpdateInterface();
            };


            MaleButton = new MirButton
            {
                HoverIndex = 2421,
                Index = 2421,
                Library = Libraries.Prguse,
                Location = new Point(323, 343),
                Parent = this,
                PressedIndex = 2422,
                Sound = SoundList.ButtonA,
            };
            MaleButton.Click += (o, e) =>
            {
                Gender = MirGender.男性;
                UpdateInterface();
            };

            FemaleButton = new MirButton
            {
                HoverIndex = 2424,
                Index = 2423,
                Library = Libraries.Prguse,
                Location = new Point(373, 343),
                Parent = this,
                PressedIndex = 2425,
                Sound = SoundList.ButtonA,
            };
            FemaleButton.Click += (o, e) =>
            {
                Gender = MirGender.女性;
                UpdateInterface();
            };

            Description = new MirLabel
            {
                Border = true,
                Location = new Point(279, 70),
                Parent = this,
                Size = new Size(278, 170),
                Text = WarriorDescription,
            };
        }

        public override void Show()
        {
            base.Show();

            Class = MirClass.战士;
            Gender = MirGender.男性;
            NameTextBox.Text = string.Empty;

            UpdateInterface();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == null) return;
            if (e.KeyChar != (char)Keys.Enter) return;
            e.Handled = true;

            if (OKButton.Enabled)
                OKButton.InvokeMouseClick(null);
        }
        private void CharacterNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                OKButton.Enabled = false;
                NameTextBox.Border = false;
            }
            else if (!Reg.IsMatch(NameTextBox.Text))
            {
                OKButton.Enabled = false;
                NameTextBox.Border = true;
                NameTextBox.BorderColour = Color.Red;
            }
            else
            {
                OKButton.Enabled = true;
                NameTextBox.Border = true;
                NameTextBox.BorderColour = Color.Green;
            }
        }

        public event EventHandler OnCreateCharacter;
        private void CreateCharacter()
        {
            OKButton.Enabled = false;

            if (OnCreateCharacter != null)
                OnCreateCharacter.Invoke(this, EventArgs.Empty);            
        }

        private void UpdateInterface()
        {
            MaleButton.Index = 2420;
            FemaleButton.Index = 2423;

            WarriorButton.Index = 2426;
            WizardButton.Index = 2429;
            TaoistButton.Index = 2432;
            AssassinButton.Index = 2435;
            ArcherButton.Index = 2438;

            switch (Gender)
            {
                case MirGender.男性:
                    MaleButton.Index = 2421;
                    break;
                case MirGender.女性:
                    FemaleButton.Index = 2424;
                    break;
            }

            switch (Class)
            {
                case MirClass.战士:
                    WarriorButton.Index = 2427;
                    Description.Text = WarriorDescription;
                    CharacterDisplay.Index = (byte)Gender == 0 ? 20 : 300; //220 : 500;
                    break;
                case MirClass.法师:
                    WizardButton.Index = 2430;
                    Description.Text = WizardDescription;
                    CharacterDisplay.Index = (byte)Gender == 0 ? 40 : 320; //240 : 520;
                    break;
                case MirClass.道士:
                    TaoistButton.Index = 2433;
                    Description.Text = TaoistDescription;
                    CharacterDisplay.Index = (byte)Gender == 0 ? 60 : 340; //260 : 540;
                    break;
                case MirClass.刺客:
                    AssassinButton.Index = 2436;
                    Description.Text = AssassinDescription;
                    CharacterDisplay.Index = (byte)Gender == 0 ? 80 : 360; //280 : 560;
                    break;
                case MirClass.弓箭:
                    ArcherButton.Index = 2439;
                    Description.Text = ArcherDescription;
                    CharacterDisplay.Index = (byte)Gender == 0 ? 100 : 140; //160 : 180;
                    break;
            }

            //CharacterDisplay.Index = ((byte)_class + 1) * 20 + (byte)_gender * 280;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            OnCreateCharacter = null;
        }
    }
}
