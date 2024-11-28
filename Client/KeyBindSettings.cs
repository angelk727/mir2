namespace Client
{

    public enum KeybindOptions : int
    {
        Bar1Skill1 = 0,
        Bar1Skill2,
        Bar1Skill3,
        Bar1Skill4,
        Bar1Skill5,
        Bar1Skill6,
        Bar1Skill7,
        Bar1Skill8,
        Bar2Skill1,
        Bar2Skill2,
        Bar2Skill3,
        Bar2Skill4,
        Bar2Skill5,
        Bar2Skill6,
        Bar2Skill7,
        Bar2Skill8,
        Inventory,
        Inventory2,
        Equipment,
        Equipment2,
        Skills,
        Skills2,
        Creature,
        MountWindow,
        Mount,
        Fishing,
        Skillbar,
        Mentor,
        Relationship,
        Friends,
        Guilds,
        GameShop,
        Quests,
        Closeall,
        Options,
        Options2,
        Group,
        Belt,
        BeltFlip,
        Pickup,
        Belt1,
        Belt1Alt,
        Belt2,
        Belt2Alt,
        Belt3,
        Belt3Alt,
        Belt4,
        Belt4Alt,
        Belt5,
        Belt5Alt,
        Belt6,
        Belt6Alt,
        Belt7,
        Belt7Alt,
        Belt8,
        Belt8Alt,
        Logout,
        Exit,
        CreaturePickup,
        CreatureAutoPickup,
        Minimap,
        Bigmap,
        Trade,
        Rental,
        ChangeAttackmode,
        AttackmodePeace,
        AttackmodeGroup,
        AttackmodeGuild,
        AttackmodeEnemyguild,
        AttackmodeRedbrown,
        AttackmodeAll,
        ChangePetmode,
        PetmodeBoth,
        PetmodeMoveonly,
        PetmodeAttackonly,
        PetmodeNone,
        Help,
        Keybind,
        Autorun,
        Cameramode,
        Screenshot,
        DropView,
        TargetDead,
        Ranking,
        AddGroupMember,
        HeroSkill1,
        HeroSkill2,
        HeroSkill3,
        HeroSkill4,
        HeroSkill5,
        HeroSkill6,
        HeroSkill7,
        HeroSkill8,
        HeroInventory,
        HeroEquipment,
        HeroSkills,
        TargetSpellLockOn,
        PetmodeFocusMasterTarget
    }

    public class KeyBind
    {
        public KeybindOptions function = KeybindOptions.Bar1Skill1;
        public string Group = "", Description = "";
        public Keys Key = 0;

        /// <summary>
        /// Require Options : 0 = Require unpressed key, 1 = Require pressed key, 2 = Don't care
        /// </summary>
        public byte RequireCtrl = 0;
        public byte RequireShift = 0;
        public byte RequireAlt = 0;
        public byte RequireTilde = 0;
    }


    public class KeyBindSettings
    {
        private static InIReader Reader = new InIReader(@".\KeyBinds.ini");
        public List<KeyBind> Keylist = new List<KeyBind>();
        public List<KeyBind> DefaultKeylist = new List<KeyBind>();

        public KeyBindSettings()
        {
            New(Keylist);
            New(DefaultKeylist);

            if (!File.Exists(@".\KeyBinds.ini"))
            {
                Save(DefaultKeylist);
                return;
            }

            Load();
        }

        public void Load()
        {
            foreach (KeyBind Inputkey in Keylist)
            {
                Inputkey.RequireAlt = Reader.ReadByte(Inputkey.function.ToString(), "RequireAlt", Inputkey.RequireAlt);
                Inputkey.RequireShift = Reader.ReadByte(Inputkey.function.ToString(), "RequireShift", Inputkey.RequireShift);
                Inputkey.RequireTilde = Reader.ReadByte(Inputkey.function.ToString(), "RequireTilde", Inputkey.RequireTilde);
                Inputkey.RequireCtrl = Reader.ReadByte(Inputkey.function.ToString(), "RequireCtrl", Inputkey.RequireCtrl);
                string Input = Reader.ReadString(Inputkey.function.ToString(), "RequireKey", Inputkey.Key.ToString());
                Enum.TryParse(Input, out Inputkey.Key);
                
            }
        }

        public void Save(List<KeyBind> keyList)
        {
            Reader.Write("Guide", "01", "需要Alt键,需要Shift键,需要~键,需要Ctrl键");
            Reader.Write("Guide", "02", "有3个选项: 0/1/2");
            Reader.Write("Guide", "03", "0 < 不能按此键使用该功能");
            Reader.Write("Guide", "04", "1 < 必须按下这个键才能使用这个功能");
            Reader.Write("Guide", "05", "2 < 按这个键使用这个功能没关系");
            Reader.Write("Guide", "06", "默认情况下只使用2，除非同一个键上有两个函数");
            Reader.Write("Guide", "07", "示例：修改攻击模式（Ctrl+H）和帮助（H）");
            Reader.Write("Guide", "08", "如果将这两个功能中的任意一个设置为需要Shift键 2次，那么它们将无法同时工作");
            Reader.Write("Guide", "09", "");
            Reader.Write("Guide", "10", "要获取所需密钥的值，请查看:");
            Reader.Write("Guide", "11", "https://msdn.microsoft.com/en-us/library/system.windows.forms.keys(v=vs.110).aspx");
        
            foreach (KeyBind Inputkey in keyList)
            {
                Reader.Write(Inputkey.function.ToString(), "RequireAlt", Inputkey.RequireAlt);
                Reader.Write(Inputkey.function.ToString(), "RequireShift", Inputkey.RequireShift);
                Reader.Write(Inputkey.function.ToString(), "RequireTilde", Inputkey.RequireTilde);
                Reader.Write(Inputkey.function.ToString(), "RequireCtrl", Inputkey.RequireCtrl);
                Reader.Write(Inputkey.function.ToString(), "RequireKey", Inputkey.Key.ToString());
            }
        }

        public void New(List<KeyBind> list)
        {
            KeyBind InputKey;
            InputKey = new KeyBind { Group = "功能框", Description = "背包 开/关", function = KeybindOptions.Inventory, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.F9 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "背包 开/关", function = KeybindOptions.Inventory2, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 0, Key = Keys.I };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "装备栏 开/关", function = KeybindOptions.Equipment, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.F10 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "装备栏 开/关", function = KeybindOptions.Equipment2, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 0, Key = Keys.C };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "技能栏 开/关", function = KeybindOptions.Skills, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.F11 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "技能栏 开/关", function = KeybindOptions.Skills2, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 0, Key = Keys.S };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "英雄背包 开/关", function = KeybindOptions.HeroInventory, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 1, Key = Keys.I };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "英雄装备栏 开/关", function = KeybindOptions.HeroEquipment, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 1, Key = Keys.C };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "英雄技能栏 开/关", function = KeybindOptions.HeroSkills, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 1, Key = Keys.S };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "宠物栏 开/关", function = KeybindOptions.Creature, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.E };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "坐骑 开/关", function = KeybindOptions.MountWindow, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.J };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "钓鱼 开/关", function = KeybindOptions.Fishing, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.N };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "技能悬浮栏 开/关", function = KeybindOptions.Skillbar, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.R };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "师徒 开/关", function = KeybindOptions.Mentor, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "夫妻 开/关", function = KeybindOptions.Relationship, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.L };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "好友 开/关", function = KeybindOptions.Friends, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.F };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "行会 开/关", function = KeybindOptions.Guilds, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 0, Key = Keys.G };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "游戏商城 开/关", function = KeybindOptions.GameShop, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.Y };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "任务栏 开/关", function = KeybindOptions.Quests, RequireAlt = 0, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.Q };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "设置 开/关", function = KeybindOptions.Options, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.F12 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "设置 开/关", function = KeybindOptions.Options2, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.O };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "组队 开/关", function = KeybindOptions.Group, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.P };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "快捷栏 开/关", function = KeybindOptions.Belt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 0, Key = Keys.Z };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "小地图 开/关", function = KeybindOptions.Minimap, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.V };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "大地图 开/关", function = KeybindOptions.Bigmap, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.B };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "排名 开/关", function = KeybindOptions.Ranking, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.K };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "帮助 开/关", function = KeybindOptions.Help, RequireAlt = 2, RequireShift = 0, RequireTilde = 2, RequireCtrl = 0, Key = Keys.H };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "键盘 开/关", function = KeybindOptions.Keybind, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.U };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "功能框", Description = "关闭所有窗口", function = KeybindOptions.Closeall, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.Escape };
            list.Add(InputKey);

            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 1", function = KeybindOptions.Bar1Skill1, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F1 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 2", function = KeybindOptions.Bar1Skill2, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F2 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 3", function = KeybindOptions.Bar1Skill3, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F3 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 4", function = KeybindOptions.Bar1Skill4, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F4 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 5", function = KeybindOptions.Bar1Skill5, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F5 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 6", function = KeybindOptions.Bar1Skill6, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F6 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 7", function = KeybindOptions.Bar1Skill7, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F7 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "快捷键 8", function = KeybindOptions.Bar1Skill8, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F8 };
            list.Add(InputKey);

            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 1", function = KeybindOptions.Bar2Skill1, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F1 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 2", function = KeybindOptions.Bar2Skill2, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F2 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 3", function = KeybindOptions.Bar2Skill3, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F3 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 4", function = KeybindOptions.Bar2Skill4, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F4 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 5", function = KeybindOptions.Bar2Skill5, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F5 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 6", function = KeybindOptions.Bar2Skill6, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F6 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 7", function = KeybindOptions.Bar2Skill7, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F7 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "Alt+组合键 8", function = KeybindOptions.Bar2Skill8, RequireAlt = 2, RequireShift = 0, RequireTilde = 0, RequireCtrl = 1, Key = Keys.F8 };
            list.Add(InputKey);

            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 1", function = KeybindOptions.HeroSkill1, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F1 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 2", function = KeybindOptions.HeroSkill2, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F2 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 3", function = KeybindOptions.HeroSkill3, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F3 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 4", function = KeybindOptions.HeroSkill4, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F4 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 5", function = KeybindOptions.HeroSkill5, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F5 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 6", function = KeybindOptions.HeroSkill6, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F6 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 7", function = KeybindOptions.HeroSkill7, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F7 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "技能快捷键", Description = "英雄技能栏 8", function = KeybindOptions.HeroSkill8, RequireAlt = 2, RequireShift = 1, RequireTilde = 0, RequireCtrl = 0, Key = Keys.F8 };
            list.Add(InputKey);

            InputKey = new KeyBind { Group = "物品快捷栏", Description = "快捷栏转动", function = KeybindOptions.BeltFlip, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 1, Key = Keys.Z };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "快捷键 1", function = KeybindOptions.Belt1, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D1 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "Alt+快捷键 1", function = KeybindOptions.Belt1Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad1 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "快捷键 2", function = KeybindOptions.Belt2, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D2 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "Alt+快捷键 2", function = KeybindOptions.Belt2Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad2 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "快捷键 3", function = KeybindOptions.Belt3, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D3 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "Alt+快捷键 3", function = KeybindOptions.Belt3Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad3 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "快捷键 4", function = KeybindOptions.Belt4, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D4 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "Alt+快捷键 4", function = KeybindOptions.Belt4Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad4 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "快捷键 5", function = KeybindOptions.Belt5, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D5 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "Alt+快捷键 5", function = KeybindOptions.Belt5Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad5 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "快捷键 6", function = KeybindOptions.Belt6, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D6 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "物品快捷栏", Description = "Alt+快捷键 6", function = KeybindOptions.Belt6Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad6 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "英雄物品快捷栏", Description = "快捷键 7", function = KeybindOptions.Belt7, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D7 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "英雄物品快捷栏", Description = "Alt+快捷键 7", function = KeybindOptions.Belt7Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad7 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "英雄物品快捷栏", Description = "快捷键 8", function = KeybindOptions.Belt8, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D8 };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "英雄物品快捷栏", Description = "Alt+快捷键 8", function = KeybindOptions.Belt8Alt, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.NumPad8 };
            list.Add(InputKey);

            InputKey = new KeyBind { Group = "常规", Description = "返回人物选择界面", function = KeybindOptions.Logout, RequireAlt = 1, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.X };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "常规", Description = "退出游戏", function = KeybindOptions.Exit, RequireAlt = 1, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.Q };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "常规", Description = "上下坐骑", function = KeybindOptions.Mount, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.M };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "常规", Description = "切换目标", function = KeybindOptions.Pickup, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.Tab };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "常规", Description = "灵物物品拾取", function = KeybindOptions.CreaturePickup, RequireAlt = 0, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.X };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "常规", Description = "灵物自动拾取", function = KeybindOptions.CreatureAutoPickup, RequireAlt = 1, RequireShift = 2, RequireTilde = 2, RequireCtrl = 0, Key = Keys.A };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "常规", Description = "交易", function = KeybindOptions.Trade, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.T };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "常规", Description = "组队", function = KeybindOptions.AddGroupMember, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 1, Key = Keys.G };
            list.Add(InputKey);

            InputKey = new KeyBind { Group = "攻击模式", Description = "切换攻击模式", function = KeybindOptions.ChangeAttackmode, RequireAlt = 2, RequireShift = 0, RequireTilde = 2, RequireCtrl = 1, Key = Keys.H };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "攻击模式 : 和平", function = KeybindOptions.AttackmodePeace, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "攻击模式 : 组队", function = KeybindOptions.AttackmodeGroup, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "攻击模式 : 行会", function = KeybindOptions.AttackmodeGuild, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "攻击模式 : 行会战", function = KeybindOptions.AttackmodeEnemyguild, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "攻击模式 : 红名", function = KeybindOptions.AttackmodeRedbrown, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "攻击模式 : 全体", function = KeybindOptions.AttackmodeAll, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "切换属下攻击模式", function = KeybindOptions.ChangePetmode, RequireAlt = 0, RequireShift = 2, RequireTilde = 2, RequireCtrl = 1, Key = Keys.A };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "属下攻击模式 : 自由", function = KeybindOptions.PetmodeBoth, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "属下攻击模式 : 跟随", function = KeybindOptions.PetmodeMoveonly, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "属下攻击模式 : 守护", function = KeybindOptions.PetmodeAttackonly, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "属下攻击模式 : 休息", function = KeybindOptions.PetmodeNone, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "切换模式", Description = "设置宠物模式：专注于主要目标", function = KeybindOptions.PetmodeFocusMasterTarget, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "自动行走", function = KeybindOptions.Autorun, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.D };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "切换拍照模式", function = KeybindOptions.Cameramode, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.Insert };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "屏幕截图", function = KeybindOptions.Screenshot, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.PrintScreen };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "攻击模式", Description = "切换全屏视图", function = KeybindOptions.DropView, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.Tab };
            list.Add(InputKey);
            InputKey = new KeyBind { Group = "魔法锁定", Description = "按住启用对目标法术锁定", function = KeybindOptions.TargetSpellLockOn, RequireAlt = 2, RequireShift = 2, RequireTilde = 2, RequireCtrl = 2, Key = Keys.None };
            list.Add(InputKey);
        }

        public string GetKey(KeybindOptions Option, bool defaultKey = false)
        {
            List<KeyBind> lst;

            if (defaultKey) lst = CMain.InputKeys.DefaultKeylist;
            else lst = CMain.InputKeys.Keylist;

            string output = "";
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].function == Option)
                {
                    if (lst[i].Key == Keys.None) return output;
                    if (lst[i].RequireAlt == 1)
                        output = "Alt";
                    if (lst[i].RequireCtrl == 1)
                        output = output != "" ? output + " + Ctrl" : "Ctrl";
                    if (lst[i].RequireShift == 1)
                        output = output != "" ? output + " + Shift" : "Shift";
                    if (lst[i].RequireTilde == 1)
                        output = output != "" ? output + " + ~" : "~";

                    output = output != "" ? output + " + " + lst[i].Key.ToString() : lst[i].Key.ToString();
                    return output;
                }
            }
            return "";
        }
    }

    
}
