public class GameLanguage
{
    //Client
    public static string PetMode_Both = "[自由攻击]",
                         PetMode_MoveOnly = "[跟随模式]",
                         PetMode_AttackOnly = "[守护模式]",
                         PetMode_None = "[休息模式]",
                         PetMode_FocusMasterTarget = "[指定模式]",

                         AttackMode_Peace = "[和平模式]",
                         AttackMode_Group = "[组队模式]",
                         AttackMode_Guild = "[行会模式]",
                         AttackMode_EnemyGuild = "[行会战争]",
                         AttackMode_RedBrown = "[红名模式]",
                         AttackMode_All = "[全体模式]",

                         LogOutTip = "是否要返回到人物选择界面",
                         ExitTip = "是否要退出游戏",
                         DiedTip = "选择在安全区复活",
                         DropTip = "是否要丢弃 {0}?",

                         Inventory = "背包 ({0})",
                         Character = "属性 ({0})",
                         Skills = "技能 ({0})",
                         Quests = "任务 ({0})",
                         Options = "选项 ({0})",
                         Menu = "菜单",
                         GameShop = "商城 ({0})",
                         BigMap = "大地图 ({0})",
                         MiniMap = "小地图 ({0})",
                         DuraPanel = "装备持久度",
                         Mail = "邮件",
                         Exit = "退出 ({0})",
                         LogOut = "小退 ({0})",
                         Help = "帮助 ({0})",
                         Keybinds = "键盘 ({0})",
                         Ranking = "排名 ({0})",
                         Creatures = "宠物 ({0})",
                         Mount = "坐骑 ({0})",
                         Fishing = "钓鱼 ({0})",
                         Friends = "好友 ({0})",
                         Mentor = "师徒 ({0})",
                         Relationship = "夫妻 ({0})",
                         Groups = "组队 ({0})",
                         Guild = "行会 ({0})",
                         Expire = "失效期: {0}",
                         ExpireNever = "永不过期",
                         ExpirePaused = "有效期: 安全区暂停计时",
                         Never = "永不",
                         Trade = "交易 ({0})",
                         Size = "窗口调整",
                         ChatSettings = "聊天设置",
                         Rotate = "转动",
                         Close = "关闭 ({0})",
                         GameMaster = "管理员",
                         Report = "公告",
                         GroupHealthPanel = "组队状态",

                         PatchErr = "无法获取补丁程序",
                         LastOnline = "上次连线",

                         Gold = "金币",
                         Credit = "信用币",

                         YouGained = "你获得了 {0}",

                         YouGained2 = "你获得了 {0:###,###,###} {1}",

                         ExperienceGained = "获得经验 {0}",

                         HeroInventory = "英雄背包 ({0})",
                         HeroCharacter = "英雄装备 ({0})",
                         HeroSkills = "英雄技能 ({0})",
                         HeroSummon = "英雄(召唤/收起)",
                         HeroExperienceGained = "英雄获得经验 {0}",

                         ItemDescription = "物品说明",
                         RequiredLevel = "需要等级: {0}",
                         RequiredDC = "需要物理攻击: {0}",
                         RequiredMC = "需要魔法攻击: {0}",
                         RequiredSC = "需要道术攻击: {0}",
                         ClassRequired = "需要职业: {0}",

                         Holy = "神圣: + {0} (+{1})",
                         Holy2 = "神圣: + {0}",
                         Accuracy = "准确: + {0} (+{1})",
                         Accuracy2 = "准确: + {0}",
                         Agility = "敏捷: + {0} (+{1})",
                         Agility2 = "敏捷: + {0}",
                         DC = "物理攻击 + {0}~{1} (+{2})",
                         DC2 = "物理攻击 + {0}~{1}",
                         MC = "魔法攻击 + {0}~{1} (+{2})",
                         MC2 = "魔法攻击 + {0}~{1}",
                         SC = "道术攻击 + {0}~{1} (+{2})",
                         SC2 = "道术攻击 + {0}~{1}",
                         Durability = "持久度:",
                         Weight = "重量:",
                         AC = "物理防御 + {0}~{1} (+{2})",
                         AC2 = "物理防御 + {0}~{1}",
                         MAC = "魔法防御 + {0}~{1} (+{2})",
                         MAC2 = "魔法防御 + {0}~{1}",
                         Luck = "幸运 + {0}",

                         DeleteCharacter = "确定要删除角色 {0}",
                         CharacterDeleted = "角色删除成功",
                         CharacterCreated = "角色创建成功",

                         Resolution = "显示设置",
                         Autostart = "自动登录",
                         Usrname = "账号名",
                         Password = "密码",

                         ShuttingDown = "断开连接：服务器正在关闭",
                         MaxCombine = "叠加数量 {0}{1}按Shift+鼠标左键拆分",
                         Count = " 计数 {0}",
                         ExtraSlots8 = "你确定用1000000金币购买8个额外背包空间" +
                         "继续购买你可以解锁4个背包空间，最购买40个空间",
                         ExtraSlots4 = "确定要解锁4个额外的背包吗? 费用: {0:###,###}",

                         Chat_All = "全部",
                         Chat_Short = "喊话",
                         Chat_Whisper = "私聊",
                         Chat_Lover = "夫妻",
                         Chat_Mentor = "师徒",
                         Chat_Group = "组队",
                         Chat_Guild = "公会",
                         ExpandedStorageLocked = "扩展仓库锁定",
                         ExtraStorage = "你想租用10天的扩展仓库？ 1,000,000 金币",
                         ExtendYourRentalPeriod = "你愿意以1000000金币延长10天租期？",

                         CannotLeaveGame = "游戏退出倒计时 {0} 秒",
                         SelectKey = "快捷键 {0}",

                         WeaponSpiritFire = "你的武器已经被火精灵灼热",
                         SpiritsFireDisappeared = "火精灵消失",
                         WeddingRing = "结婚戒指",
                         ItemTextFormat = "{0}{1}{2} {3}",
                         DropAmount = "丢弃数量",
                         LowMana = "法力值不足",
                         NoCreatures = "没有宠物",
                         NoMount = "没有坐骑",
                         NoFishingRod = "没有鱼竿",
                         AttemptingConnect = "正在连接到服务器{0}连接次数:{1}",

                         CreatingCharactersDisabled = "服务器当前禁止创建新账户",
                         InvalidCharacterName = "角色名不可用",
                         NoClass = "职业未开放，请联系管理员",
                         ToManyCharacters = "不能再创建角色 {0}",
                         CharacterNameExists = "角色名已存在",
                         WarriorsDes = "以强有力的体格为基础，特殊之处在于用剑法及刀法等技术。即便穿戴沉重的武器" +
                                        "及对打猎、战斗比较适用。 体力强的战士能带许多东西，铠甲也可以" +
                                        "自由活动。 但战士所戴的铠甲对魔法的防御能力相对较弱。",
                         WizardDes = "以长时间锻炼的内功为基础，能发挥强大的攻击型魔法。魔法攻击力卓越，但体力较弱。对体力" +
                                        "上直接受到攻击的防御能力较低，另外，发挥高水平的魔法时需要较长时间，此时" +
                                        "可能受到对方的快速攻击。 魔法师的魔法比任何攻击能力都强大，能有效的威胁对方。",
                         TaoistDes = "以强大的精神力作为基础，可以使用治疗术帮助别人。 对" +
                                        "自然很熟悉，在用毒方面的能力最强。 博学多知，能使用剑术和魔法" +
                                        "，所以每时每刻都能发挥多样的法术，随机应变性强。",
                         AssassinDes = "以敏捷快速的攻击为基础，矫健的刺客还拥有超强的爆" +
                                        "发性他们熟悉各种技能 尤其擅长瞬移、潜行技能！ 他们是暗夜" +
                                        "的主人，是绝对的伤害高、攻击高、爆发型的职业。",
                         ArcherDes = "强大的远程输出：作为一个名副其实的远程物理输出职业，弓箭最擅长在敌人攻击范围之外" +
                                        "对敌人造成致命打击。 多变：弓箭手永远是战场上的未知数，就必须练就准确的判断力，熟练掌握其操作技巧" +
                                        " 华丽：鲜艳的服装、优雅的射击动作和绚美的特效，非弓箭手莫属!",
                         DateSent = "发送日期: {0}",
                         Send = "发送",
                         Reply = "回复",
                         Read = "阅读",
                         Delete = "删除",
                         BlockList = "阻止列表",
                         EnterMailToName = "输入要邮寄的人的姓名",
                         AddFriend = "添加好友",
                         RemoveFriend = "删除好友",
                         FriendMemo = "好友名单",
                         FriendMail = "好友邮箱",
                         FriendWhisper = "私聊好友",
                         FriendEnterAddName = "输入要添加好友名字",
                         FriendEnterBlockName = "输入要屏蔽的好友名字",
                         AddMentor = "拜师",
                         RemoveMentorMentee = "解除师徒关系",
                         MentorRequests = "允许/拒绝拜师",
                         MentorEnterName = "输入想拜师的名字",
                         RestedBuff = "精力充沛{0}增加经验值{1}%{2}",

                         ItemTypeNothing = "物品",
                         ItemTypeWeapon = "武器",
                         ItemTypeArmour = "盔甲",
                         ItemTypeHelmet = "头盔",
                         ItemTypeNecklace = "项链",
                         ItemTypeBracelet = "手镯",
                         ItemTypeRing = "戒指",
                         ItemTypeAmulet = "护身符",
                         ItemTypeBelt = "腰带",
                         ItemTypeBoots = "靴子",
                         ItemTypeStone = "守护石",
                         ItemTypeTorch = "照明物",
                         ItemTypePotion = "药水",
                         ItemTypeOre = "矿石",
                         ItemTypeMeat = "肉类",
                         ItemTypeCraftingMaterial = "工艺材料",
                         ItemTypeScroll = "卷轴",
                         ItemTypeGem = "宝玉神珠",
                         ItemTypeMount = "坐骑",
                         ItemTypeBook = "技能书",
                         ItemTypeScript = "特殊消耗品",
                         ItemTypeReins = "缰绳",
                         ItemTypeBells = "铃铛",
                         ItemTypeSaddle = "马鞍",
                         ItemTypeRibbon = "蝴蝶结",
                         ItemTypeMask = "面甲",
                         ItemTypeFood = "坐骑食物",
                         ItemTypeHook = "鱼钩",
                         ItemTypeFloat = "鱼漂",
                         ItemTypeBait = "鱼饵",
                         ItemTypeFinder = "探鱼器",
                         ItemTypeReel = "摇轮",
                         ItemTypeFish = "鱼",
                         ItemTypeQuest = "任务",
                         ItemTypeAwakening = "觉醒",
                         ItemTypePets = "宠物",
                         ItemTypeTransform = "外形变幻",
                         ItemTypeDeco = "装饰",
                         ItemTypeMonsterSpawn = "怪物蛋",
                         ItemTypeSealedHero = "封印英雄",

                         ItemGradeCommon = "普通",
                         ItemGradeRare = "宝物",
                         ItemGradeLegendary = "圣物",
                         ItemGradeMythical = "神物",
						 ItemGradeHeroic = "英雄",
                         NoAccountID = "帐户不存在",
                         IncorrectPasswordAccountID = "帐户密码不正确",
                         GroupSwitch = "允许/禁止组队",
                         GroupAdd = "组队添加",
                         GroupRemove = "组队删除",
                         GroupAddEnterName = "输入要组队玩家的名称",
                         GroupRemoveEnterName = "输入要退组玩家的名称",
                         TooHeavyToHold = "腕力不足",
                         SwitchMarriage = "允许/禁止结婚",
                         RequestMarriage = "结婚请求",
                         RequestDivorce = "离婚请求",
                         MailLover = "夫妻邮件",
                         WhisperLover = "夫妻私聊";

    //Server
    public static string Welcome = "欢迎来到 {0} 服务器",
                         OnlinePlayers = "在线玩家 {0}",
                         WeaponLuck = "武器获得幸运",
                         WeaponCurse = "武器受到诅咒",
                         WeaponNoEffect = "无效",
                         InventoryIncreased = "背包容量增加",
                         FaceToTrade = "必须面对玩家交易",
                         NoTownTeleport = "回城禁用",
                         CanNotRandom = "随机禁用",
                         CanNotDungeon = "地牢禁用",
                         CannotResurrection = "当前状态下复活禁用",
                         CanNotDrop = "丢弃禁止",
                         NewMail = "新邮件到达",
                         CouldNotFindPlayer = "未发现玩家{0}",
                         BeenPoisoned = "你中毒了",
                         AllowingMentorRequests = "拜师请求被认可",
                         BlockingMentorRequests = "拜师请求被拒绝";

    //common
    public static string LowLevel = "等级不足",
                         LowGold = "金币不足",
                         LevelUp = "升级！！！",
                         HeroLevelUp = "英雄等级提升！",
                         LowDC = "物理攻击不足",
                         LowMC = "魔法攻击不足",
                         LowSC = "道术攻击不足",
                         GameName = "Legend of Mir 2",
                         ExpandedStorageExpiresOn = "扩展仓库过期",

                         NotFemale = "非女性",
                         NotMale = "非男性",
                         NotInGuild = "未加入公会",
                         NoMentorship = "未有可解除的师徒关系",
                         NoBagSpace = "背包空间不足";


    public static void LoadClientLanguage(string languageIniPath)
    {
        if (!File.Exists(languageIniPath))
        {
            SaveClientLanguage(languageIniPath);
            return;
        }

        InIReader reader = new InIReader(languageIniPath);
        GameLanguage.PetMode_Both = reader.ReadString("Language", "PetMode_Both", GameLanguage.PetMode_Both);
        GameLanguage.PetMode_MoveOnly = reader.ReadString("Language", "PetMode_MoveOnly", GameLanguage.PetMode_MoveOnly);
        GameLanguage.PetMode_AttackOnly = reader.ReadString("Language", "PetMode_AttackOnly", GameLanguage.PetMode_AttackOnly);
        GameLanguage.PetMode_None = reader.ReadString("Language", "PetMode_None", GameLanguage.PetMode_None);
        GameLanguage.PetMode_FocusMasterTarget = reader.ReadString("Language", "PetMode_FocusMasterTarget", GameLanguage.PetMode_FocusMasterTarget);

        GameLanguage.AttackMode_Peace = reader.ReadString("Language", "AttackMode_Peace", GameLanguage.AttackMode_Peace);
        GameLanguage.AttackMode_Group = reader.ReadString("Language", "AttackMode_Group", GameLanguage.AttackMode_Group);
        GameLanguage.AttackMode_Guild = reader.ReadString("Language", "AttackMode_Guild", GameLanguage.AttackMode_Guild);
        GameLanguage.AttackMode_EnemyGuild = reader.ReadString("Language", "AttackMode_EnemyGuild", GameLanguage.AttackMode_EnemyGuild);
        GameLanguage.AttackMode_RedBrown = reader.ReadString("Language", "AttackMode_RedBrown", GameLanguage.AttackMode_RedBrown);
        GameLanguage.AttackMode_All = reader.ReadString("Language", "AttackMode_All", GameLanguage.AttackMode_All);

        GameLanguage.LogOutTip = reader.ReadString("Language", "LogOutTip", GameLanguage.LogOutTip);
        GameLanguage.ExitTip = reader.ReadString("Language", "ExitTip", GameLanguage.ExitTip);
        GameLanguage.DiedTip = reader.ReadString("Language", "DiedTip", GameLanguage.DiedTip);
        GameLanguage.DropTip = reader.ReadString("Language", "DropTip", GameLanguage.DropTip);

        GameLanguage.Inventory = reader.ReadString("Language", "Inventory", GameLanguage.Inventory);
        GameLanguage.Character = reader.ReadString("Language", "Character", GameLanguage.Character);
        GameLanguage.Skills = reader.ReadString("Language", "Skills", GameLanguage.Skills);
        GameLanguage.Quests = reader.ReadString("Language", "Quests", GameLanguage.Quests);
        GameLanguage.Options = reader.ReadString("Language", "Options", GameLanguage.Options);
        GameLanguage.Menu = reader.ReadString("Language", "Menu", GameLanguage.Menu);
        GameLanguage.GameShop = reader.ReadString("Language", "GameShop", GameLanguage.GameShop);
        GameLanguage.BigMap = reader.ReadString("Language", "BigMap", GameLanguage.BigMap);
        GameLanguage.MiniMap = reader.ReadString("Language", "MiniMap", GameLanguage.MiniMap);
        GameLanguage.DuraPanel = reader.ReadString("Language", "DuraPanel", GameLanguage.DuraPanel);
        GameLanguage.Mail = reader.ReadString("Language", "Mail", GameLanguage.Mail);
        GameLanguage.Exit = reader.ReadString("Language", "Exit", GameLanguage.Exit);
        GameLanguage.LogOut = reader.ReadString("Language", "LogOut", GameLanguage.LogOut);
        GameLanguage.Help = reader.ReadString("Language", "Help", GameLanguage.Help);
        GameLanguage.Keybinds = reader.ReadString("Language", "Keybinds", GameLanguage.Keybinds);
        GameLanguage.Ranking = reader.ReadString("Language", "Ranking", GameLanguage.Ranking);
        GameLanguage.Creatures = reader.ReadString("Language", "Creatures", GameLanguage.Creatures);
        GameLanguage.Mount = reader.ReadString("Language", "Mount", GameLanguage.Mount);
        GameLanguage.Fishing = reader.ReadString("Language", "Fishing", GameLanguage.Fishing);
        GameLanguage.Friends = reader.ReadString("Language", "Friends", GameLanguage.Friends);
        GameLanguage.Mentor = reader.ReadString("Language", "Mentor", GameLanguage.Mentor);
        GameLanguage.Relationship = reader.ReadString("Language", "Relationship", GameLanguage.Relationship);
        GameLanguage.Groups = reader.ReadString("Language", "Groups", GameLanguage.Groups);
        GameLanguage.Guild = reader.ReadString("Language", "Guild", GameLanguage.Guild);
        GameLanguage.Trade = reader.ReadString("Language", "Trade", GameLanguage.Trade);
        GameLanguage.Size = reader.ReadString("Language", "Size", GameLanguage.Size);
        GameLanguage.ChatSettings = reader.ReadString("Language", "ChatSettings", GameLanguage.ChatSettings);
        GameLanguage.Rotate = reader.ReadString("Language", "Rotate", GameLanguage.Rotate);
        GameLanguage.Close = reader.ReadString("Language", "Close", GameLanguage.Close);
        GameLanguage.GameMaster = reader.ReadString("Language", "GameMaster", GameLanguage.GameMaster);
        GameLanguage.Report = reader.ReadString("Language", "Report", GameLanguage.Report);
        GameLanguage.Expire = reader.ReadString("Language", "Expire", GameLanguage.Expire);
        GameLanguage.ExpireNever = reader.ReadString("Language", "ExpireNever", GameLanguage.ExpireNever);
        GameLanguage.ExpirePaused = reader.ReadString("Language", "ExpirePaused", GameLanguage.ExpirePaused);
        GameLanguage.Never = reader.ReadString("Language", "Never", GameLanguage.Never);
        GameLanguage.GroupHealthPanel = reader.ReadString("Language", "GroupHealthPanel", GameLanguage.GroupHealthPanel);

        GameLanguage.PatchErr = reader.ReadString("Language", "PatchErr", GameLanguage.PatchErr);
        GameLanguage.LastOnline = reader.ReadString("Language", "LastOnline", GameLanguage.LastOnline);

        GameLanguage.LowLevel = reader.ReadString("Language", "LowLevel", GameLanguage.LowLevel);
        GameLanguage.LowGold = reader.ReadString("Language", "LowGold", GameLanguage.LowGold);
        GameLanguage.LowDC = reader.ReadString("Language", "LowDC", GameLanguage.LowDC);
        GameLanguage.LowMC = reader.ReadString("Language", "LowMC", GameLanguage.LowMC);
        GameLanguage.LowSC = reader.ReadString("Language", "LowSC", GameLanguage.LowSC);

        GameLanguage.Gold = reader.ReadString("Language", "Gold", GameLanguage.Gold);
        GameLanguage.Credit = reader.ReadString("Language", "Credit", GameLanguage.Credit);

        GameLanguage.YouGained = reader.ReadString("Language", "YouGained", GameLanguage.YouGained);
        GameLanguage.YouGained2 = reader.ReadString("Language", "YouGained2", GameLanguage.YouGained2);
        GameLanguage.ExperienceGained = reader.ReadString("Language", "ExperienceGained", GameLanguage.ExperienceGained);        
        GameLanguage.LevelUp = reader.ReadString("Language", "LevelUp", GameLanguage.LevelUp);
        GameLanguage.HeroLevelUp = reader.ReadString("Language", "HeroLevelUp", GameLanguage.HeroLevelUp);

        GameLanguage.HeroInventory = reader.ReadString("Language", "HeroInventory", GameLanguage.HeroInventory);
        GameLanguage.HeroCharacter = reader.ReadString("Language", "HeroCharacter", GameLanguage.HeroCharacter);
        GameLanguage.HeroSkills = reader.ReadString("Language", "HeroSkills", GameLanguage.HeroSkills);
        GameLanguage.HeroSummon = reader.ReadString("Language", "HeroSummon", GameLanguage.HeroSummon);
        GameLanguage.HeroExperienceGained = reader.ReadString("Language", "HeroExperienceGained", GameLanguage.HeroExperienceGained);

        GameLanguage.ItemDescription = reader.ReadString("Language", "ItemDescription", GameLanguage.ItemDescription);
        GameLanguage.RequiredLevel = reader.ReadString("Language", "RequiredLevel", GameLanguage.RequiredLevel);
        GameLanguage.RequiredDC = reader.ReadString("Language", "RequiredDC", GameLanguage.RequiredDC);
        GameLanguage.RequiredMC = reader.ReadString("Language", "RequiredMC", GameLanguage.RequiredMC);
        GameLanguage.RequiredSC = reader.ReadString("Language", "RequiredSC", GameLanguage.RequiredSC);
        GameLanguage.ClassRequired = reader.ReadString("Language", "ClassRequired", GameLanguage.ClassRequired);
        GameLanguage.Holy = reader.ReadString("Language", "Holy", GameLanguage.Holy);
        GameLanguage.Holy2 = reader.ReadString("Language", "Holy2", GameLanguage.Holy2);
        GameLanguage.Accuracy = reader.ReadString("Language", "Accuracy", GameLanguage.Accuracy);
        GameLanguage.Accuracy2 = reader.ReadString("Language", "Accuracy2", GameLanguage.Accuracy2);
        GameLanguage.Agility = reader.ReadString("Language", "Agility", GameLanguage.Agility);
        GameLanguage.Agility2 = reader.ReadString("Language", "Agility2", GameLanguage.Agility2);
        GameLanguage.DC = reader.ReadString("Language", "DC", GameLanguage.DC);
        GameLanguage.DC2 = reader.ReadString("Language", "DC2", GameLanguage.DC2);
        GameLanguage.MC = reader.ReadString("Language", "MC", GameLanguage.MC);
        GameLanguage.MC2 = reader.ReadString("Language", "MC2", GameLanguage.MC2);
        GameLanguage.SC = reader.ReadString("Language", "SC", GameLanguage.SC);
        GameLanguage.SC2 = reader.ReadString("Language", "SC2", GameLanguage.SC2);
        GameLanguage.Durability = reader.ReadString("Language", "Durability", GameLanguage.Durability);
        GameLanguage.Weight = reader.ReadString("Language", "Weight", GameLanguage.Weight);
        GameLanguage.AC = reader.ReadString("Language", "AC", GameLanguage.AC);
        GameLanguage.AC2 = reader.ReadString("Language", "AC2", GameLanguage.AC2);
        GameLanguage.MAC = reader.ReadString("Language", "MAC", GameLanguage.MAC);
        GameLanguage.MAC2 = reader.ReadString("Language", "MAC2", GameLanguage.MAC2);
        GameLanguage.Luck = reader.ReadString("Language", "Luck", GameLanguage.Luck);

        GameLanguage.DeleteCharacter = reader.ReadString("Language", "DeleteCharacter", GameLanguage.DeleteCharacter);
        GameLanguage.CharacterDeleted = reader.ReadString("Language", "CharacterDeleted", GameLanguage.CharacterDeleted);
        GameLanguage.CharacterCreated = reader.ReadString("Language", "CharacterCreated", GameLanguage.CharacterCreated);

        GameLanguage.Resolution = reader.ReadString("Language", "Resolution", GameLanguage.Resolution);
        GameLanguage.Autostart = reader.ReadString("Language", "Autostart", GameLanguage.Autostart);
        GameLanguage.Usrname = reader.ReadString("Language", "Usrname", GameLanguage.Usrname);
        GameLanguage.Password = reader.ReadString("Language", "Password", GameLanguage.Password);

        GameLanguage.ShuttingDown = reader.ReadString("Language", "ShuttingDown", GameLanguage.ShuttingDown);

        GameLanguage.MaxCombine = reader.ReadString("Language", "MaxCombine", GameLanguage.MaxCombine);
        GameLanguage.Count = reader.ReadString("Language", "Count", GameLanguage.Count);
        GameLanguage.ExtraSlots8 = reader.ReadString("Language", "ExtraSlots8", GameLanguage.ExtraSlots8);
        GameLanguage.ExtraSlots4 = reader.ReadString("Language", "ExtraSlots4", GameLanguage.ExtraSlots4);

        GameLanguage.Chat_All = reader.ReadString("Language", "Chat_All", GameLanguage.Chat_All);
        GameLanguage.Chat_Short = reader.ReadString("Language", "Chat_Short", GameLanguage.Chat_Short);
        GameLanguage.Chat_Whisper = reader.ReadString("Language", "Chat_Whisper", GameLanguage.Chat_Whisper);
        GameLanguage.Chat_Lover = reader.ReadString("Language", "Chat_Lover", GameLanguage.Chat_Lover);
        GameLanguage.Chat_Mentor = reader.ReadString("Language", "Chat_Mentor", GameLanguage.Chat_Mentor);
        GameLanguage.Chat_Group = reader.ReadString("Language", "Chat_Group", GameLanguage.Chat_Group);
        GameLanguage.Chat_Guild = reader.ReadString("Language", "Chat_Guild", GameLanguage.Chat_Guild);
        GameLanguage.ExpandedStorageLocked = reader.ReadString("Language", "ExpandedStorageLocked", GameLanguage.ExpandedStorageLocked);
        GameLanguage.ExtraStorage = reader.ReadString("Language", "ExtraStorage", GameLanguage.ExtraStorage);
        GameLanguage.ExtendYourRentalPeriod = reader.ReadString("Language", "ExtendYourRentalPeriod", GameLanguage.ExtendYourRentalPeriod);
        GameLanguage.ExpandedStorageExpiresOn = reader.ReadString("Language", "ExpandedStorageExpiresOn", GameLanguage.ExpandedStorageExpiresOn);
        GameLanguage.GameName = reader.ReadString("Language", "GameName", GameLanguage.GameName);
        GameLanguage.CannotLeaveGame = reader.ReadString("Language", "CannotLeaveGame", GameLanguage.CannotLeaveGame);
        GameLanguage.SelectKey = reader.ReadString("Language", "SelectKey", GameLanguage.SelectKey);
        GameLanguage.WeaponSpiritFire = reader.ReadString("Language", "WeaponSpiritFire", GameLanguage.WeaponSpiritFire);
        GameLanguage.SpiritsFireDisappeared = reader.ReadString("Language", "SpiritsFireDisappeared", GameLanguage.SpiritsFireDisappeared);
        GameLanguage.WeddingRing = reader.ReadString("Language", "WeddingRing", GameLanguage.WeddingRing);
        GameLanguage.ItemTextFormat = reader.ReadString("Language", "ItemTextFormat", GameLanguage.ItemTextFormat);
        GameLanguage.DropAmount = reader.ReadString("Language", "DropAmount", GameLanguage.DropAmount);
        GameLanguage.LowMana = reader.ReadString("Language", "LowMana", GameLanguage.LowMana);

        GameLanguage.NotFemale = reader.ReadString("Language", "NotFemale", GameLanguage.NotFemale);
        GameLanguage.NotMale = reader.ReadString("Language", "NotMale", GameLanguage.NotMale);
        GameLanguage.NoCreatures = reader.ReadString("Language", "NoCreatures", GameLanguage.NoCreatures);
        GameLanguage.NoMount = reader.ReadString("Language", "NoMount", GameLanguage.NoMount);
        GameLanguage.NoFishingRod = reader.ReadString("Language", "NoFishingRod", GameLanguage.NoFishingRod);
        GameLanguage.NotInGuild = reader.ReadString("Language", "NotInGuild", GameLanguage.NotInGuild);
        GameLanguage.NoBagSpace = reader.ReadString("Language", "NoBagSpace", GameLanguage.NoBagSpace);
        GameLanguage.AttemptingConnect = reader.ReadString("Language", "AttemptingConnect", GameLanguage.AttemptingConnect);

        GameLanguage.CreatingCharactersDisabled = reader.ReadString("Language", "CreatingCharactersDisabled", GameLanguage.CreatingCharactersDisabled);
        GameLanguage.InvalidCharacterName = reader.ReadString("Language", "InvalidCharacterName", GameLanguage.InvalidCharacterName);
        GameLanguage.NoClass = reader.ReadString("Language", "NoClass", GameLanguage.NoClass);
        GameLanguage.ToManyCharacters = reader.ReadString("Language", "ToManyCharacters", GameLanguage.ToManyCharacters);
        GameLanguage.CharacterNameExists = reader.ReadString("Language", "CharacterNameExists", GameLanguage.CharacterNameExists);

        GameLanguage.WarriorsDes = reader.ReadString("Language", "WarriorsDes", GameLanguage.WarriorsDes);
        GameLanguage.WizardDes = reader.ReadString("Language", "WizardDes", GameLanguage.WizardDes);
        GameLanguage.TaoistDes = reader.ReadString("Language", "TaoistDes", GameLanguage.TaoistDes);
        GameLanguage.AssassinDes = reader.ReadString("Language", "AssassinDes", GameLanguage.AssassinDes);
        GameLanguage.ArcherDes = reader.ReadString("Language", "ArcherDes", GameLanguage.ArcherDes);

        GameLanguage.DateSent = reader.ReadString("Language", "DateSent", GameLanguage.DateSent);
        GameLanguage.Send = reader.ReadString("Language", "Send", GameLanguage.Send);
        GameLanguage.Reply = reader.ReadString("Language", "Reply", GameLanguage.Reply);
        GameLanguage.Read = reader.ReadString("Language", "Read", GameLanguage.Read);
        GameLanguage.Delete = reader.ReadString("Language", "Delete", GameLanguage.Delete);
        GameLanguage.BlockList = reader.ReadString("Language", "BlockList", GameLanguage.BlockList);
        GameLanguage.EnterMailToName = reader.ReadString("Language", "EnterMailToName", GameLanguage.EnterMailToName);
        GameLanguage.BeenPoisoned = reader.ReadString("Language", "BeenPoisoned", GameLanguage.BeenPoisoned);
        GameLanguage.AddFriend = reader.ReadString("Language", "AddFriend", GameLanguage.AddFriend);
        GameLanguage.RemoveFriend = reader.ReadString("Language", "RemoveFriend", GameLanguage.RemoveFriend);
        GameLanguage.FriendMemo = reader.ReadString("Language", "FriendMemo", GameLanguage.FriendMemo);
        GameLanguage.FriendMail = reader.ReadString("Language", "FriendMail", GameLanguage.FriendMail);
        GameLanguage.FriendWhisper = reader.ReadString("Language", "FriendWhisper", GameLanguage.FriendWhisper);
        GameLanguage.FriendEnterAddName = reader.ReadString("Language", "FriendEnterAddName", GameLanguage.FriendEnterAddName);
        GameLanguage.FriendEnterBlockName = reader.ReadString("Language", "FriendEnterBlockName", GameLanguage.FriendEnterBlockName);
        GameLanguage.AddMentor = reader.ReadString("Language", "AddMentor", GameLanguage.AddMentor);
        GameLanguage.RemoveMentorMentee = reader.ReadString("Language", "RemoveMentorMentee", GameLanguage.RemoveMentorMentee);
        GameLanguage.MentorRequests = reader.ReadString("Language", "MentorRequests", GameLanguage.MentorRequests);
        GameLanguage.MentorEnterName = reader.ReadString("Language", "MentorEnterName", GameLanguage.MentorEnterName);
        GameLanguage.NoMentorship = reader.ReadString("Language", "NoMentorship", GameLanguage.NoMentorship);
        GameLanguage.RestedBuff = reader.ReadString("Language", "RestedBuff", GameLanguage.RestedBuff);

        GameLanguage.ItemTypeNothing = reader.ReadString("Language", "ItemTypeNothing", GameLanguage.ItemTypeNothing);
        GameLanguage.ItemTypeWeapon = reader.ReadString("Language", "ItemTypeWeapon", GameLanguage.ItemTypeWeapon);
        GameLanguage.ItemTypeArmour = reader.ReadString("Language", "ItemTypeArmour", GameLanguage.ItemTypeArmour);
        GameLanguage.ItemTypeHelmet = reader.ReadString("Language", "ItemTypeHelmet", GameLanguage.ItemTypeHelmet);
        GameLanguage.ItemTypeNecklace = reader.ReadString("Language", "ItemTypeNecklace", GameLanguage.ItemTypeNecklace);
        GameLanguage.ItemTypeBracelet = reader.ReadString("Language", "ItemTypeBracelet", GameLanguage.ItemTypeBracelet);
        GameLanguage.ItemTypeRing = reader.ReadString("Language", "ItemTypeRing", GameLanguage.ItemTypeRing);
        GameLanguage.ItemTypeAmulet = reader.ReadString("Language", "ItemTypeAmulet", GameLanguage.ItemTypeAmulet);
        GameLanguage.ItemTypeBelt = reader.ReadString("Language", "ItemTypeBelt", GameLanguage.ItemTypeBelt);
        GameLanguage.ItemTypeBoots = reader.ReadString("Language", "ItemTypeBoots", GameLanguage.ItemTypeBoots);
        GameLanguage.ItemTypeStone = reader.ReadString("Language", "ItemTypeStone", GameLanguage.ItemTypeStone);
        GameLanguage.ItemTypeTorch = reader.ReadString("Language", "ItemTypeTorch", GameLanguage.ItemTypeTorch);
        GameLanguage.ItemTypePotion = reader.ReadString("Language", "ItemTypePotion", GameLanguage.ItemTypePotion);
        GameLanguage.ItemTypeOre = reader.ReadString("Language", "ItemTypeOre", GameLanguage.ItemTypeOre);
        GameLanguage.ItemTypeMeat = reader.ReadString("Language", "ItemTypeMeat", GameLanguage.ItemTypeMeat);
        GameLanguage.ItemTypeCraftingMaterial = reader.ReadString("Language", "ItemTypeCraftingMaterial", GameLanguage.ItemTypeCraftingMaterial);
        GameLanguage.ItemTypeScroll = reader.ReadString("Language", "ItemTypeScroll", GameLanguage.ItemTypeScroll);
        GameLanguage.ItemTypeGem = reader.ReadString("Language", "ItemTypeGem", GameLanguage.ItemTypeGem);
        GameLanguage.ItemTypeMount = reader.ReadString("Language", "ItemTypeMount", GameLanguage.ItemTypeMount);
        GameLanguage.ItemTypeBook = reader.ReadString("Language", "ItemTypeBook", GameLanguage.ItemTypeBook);
        GameLanguage.ItemTypeScript = reader.ReadString("Language", "ItemTypeScript", GameLanguage.ItemTypeScript);
        GameLanguage.ItemTypeReins = reader.ReadString("Language", "ItemTypeReins", GameLanguage.ItemTypeReins);
        GameLanguage.ItemTypeBells = reader.ReadString("Language", "ItemTypeBells", GameLanguage.ItemTypeBells);
        GameLanguage.ItemTypeSaddle = reader.ReadString("Language", "ItemTypeSaddle", GameLanguage.ItemTypeSaddle);
        GameLanguage.ItemTypeRibbon = reader.ReadString("Language", "ItemTypeRibbon", GameLanguage.ItemTypeRibbon);
        GameLanguage.ItemTypeMask = reader.ReadString("Language", "ItemTypeMask", GameLanguage.ItemTypeMask);
        GameLanguage.ItemTypeFood = reader.ReadString("Language", "ItemTypeFood", GameLanguage.ItemTypeFood);
        GameLanguage.ItemTypeHook = reader.ReadString("Language", "ItemTypeHook", GameLanguage.ItemTypeHook);
        GameLanguage.ItemTypeFloat = reader.ReadString("Language", "ItemTypeFloat", GameLanguage.ItemTypeFloat);
        GameLanguage.ItemTypeBait = reader.ReadString("Language", "ItemTypeBait", GameLanguage.ItemTypeBait);
        GameLanguage.ItemTypeFinder = reader.ReadString("Language", "ItemTypeFinder", GameLanguage.ItemTypeFinder);
        GameLanguage.ItemTypeReel = reader.ReadString("Language", "ItemTypeReel", GameLanguage.ItemTypeReel);
        GameLanguage.ItemTypeFish = reader.ReadString("Language", "ItemTypeFish", GameLanguage.ItemTypeFish);
        GameLanguage.ItemTypeQuest = reader.ReadString("Language", "ItemTypeQuest", GameLanguage.ItemTypeQuest);
        GameLanguage.ItemTypeAwakening = reader.ReadString("Language", "ItemTypeAwakening", GameLanguage.ItemTypeAwakening);
        GameLanguage.ItemTypePets = reader.ReadString("Language", "ItemTypePets", GameLanguage.ItemTypePets);
        GameLanguage.ItemTypeTransform = reader.ReadString("Language", "ItemTypeTransform", GameLanguage.ItemTypeTransform);
        GameLanguage.ItemTypeSealedHero = reader.ReadString("Language", "ItemTypeSealedHero", GameLanguage.ItemTypeSealedHero);

        GameLanguage.ItemGradeCommon = reader.ReadString("Language", "ItemGradeCommon", GameLanguage.ItemGradeCommon);
        GameLanguage.ItemGradeRare = reader.ReadString("Language", "ItemGradeRare", GameLanguage.ItemGradeRare);
        GameLanguage.ItemGradeLegendary = reader.ReadString("Language", "ItemGradeLegendary", GameLanguage.ItemGradeLegendary);
        GameLanguage.ItemGradeMythical = reader.ReadString("Language", "ItemGradeMythical", GameLanguage.ItemGradeMythical);
        GameLanguage.ItemGradeHeroic = reader.ReadString("Language", "ItemGradeHeroic", GameLanguage.ItemGradeHeroic);

        GameLanguage.NoAccountID = reader.ReadString("Language", "NoAccountID", GameLanguage.NoAccountID);
        GameLanguage.IncorrectPasswordAccountID = reader.ReadString("Language", "IncorrectPasswordAccountID", GameLanguage.IncorrectPasswordAccountID);
        GameLanguage.GroupSwitch = reader.ReadString("Language", "GroupSwitch", GameLanguage.GroupSwitch);
        GameLanguage.GroupAdd = reader.ReadString("Language", "GroupAdd", GameLanguage.GroupAdd);
        GameLanguage.GroupRemove = reader.ReadString("Language", "GroupRemove", GameLanguage.GroupRemove);
        GameLanguage.GroupAddEnterName = reader.ReadString("Language", "GroupAddEnterName", GameLanguage.GroupAddEnterName);
        GameLanguage.GroupRemoveEnterName = reader.ReadString("Language", "GroupRemoveEnterName", GameLanguage.GroupRemoveEnterName);
        GameLanguage.TooHeavyToHold = reader.ReadString("Language", "TooHeavyToHold", GameLanguage.TooHeavyToHold);
        GameLanguage.SwitchMarriage = reader.ReadString("Language", "SwitchMarriage", GameLanguage.SwitchMarriage);
        GameLanguage.RequestMarriage = reader.ReadString("Language", "RequestMarriage", GameLanguage.RequestMarriage);
        GameLanguage.RequestDivorce = reader.ReadString("Language", "RequestDivorce", GameLanguage.RequestDivorce);
        GameLanguage.MailLover = reader.ReadString("Language", "MailLover", GameLanguage.MailLover);
        GameLanguage.WhisperLover = reader.ReadString("Language", "WhisperLover", GameLanguage.WhisperLover);
    }


    public static void SaveClientLanguage(string languageIniPath)
    {
        File.Delete(languageIniPath);
        InIReader reader = new InIReader(languageIniPath);
        reader.Write("Language", "PetMode_Both", GameLanguage.PetMode_Both);
        reader.Write("Language", "PetMode_MoveOnly", GameLanguage.PetMode_MoveOnly);
        reader.Write("Language", "PetMode_AttackOnly", GameLanguage.PetMode_AttackOnly);
        reader.Write("Language", "PetMode_None", GameLanguage.PetMode_None);
        reader.Write("Language", "PetMode_FocusMasterTarget", GameLanguage.PetMode_FocusMasterTarget);

        reader.Write("Language", "AttackMode_Peace", GameLanguage.AttackMode_Peace);
        reader.Write("Language", "AttackMode_Group", GameLanguage.AttackMode_Group);
        reader.Write("Language", "AttackMode_Guild", GameLanguage.AttackMode_Guild);
        reader.Write("Language", "AttackMode_EnemyGuild", GameLanguage.AttackMode_EnemyGuild);
        reader.Write("Language", "AttackMode_RedBrown", GameLanguage.AttackMode_RedBrown);
        reader.Write("Language", "AttackMode_All", GameLanguage.AttackMode_All);

        reader.Write("Language", "LogOutTip", GameLanguage.LogOutTip);
        reader.Write("Language", "ExitTip", GameLanguage.ExitTip);
        reader.Write("Language", "DiedTip", GameLanguage.DiedTip);
        reader.Write("Language", "DropTip", GameLanguage.DropTip);

        reader.Write("Language", "Inventory", GameLanguage.Inventory);
        reader.Write("Language", "Character", GameLanguage.Character);
        reader.Write("Language", "Skills", GameLanguage.Skills);
        reader.Write("Language", "Quests", GameLanguage.Quests);
        reader.Write("Language", "Options", GameLanguage.Options);
        reader.Write("Language", "Menu", GameLanguage.Menu);
        reader.Write("Language", "GameShop", GameLanguage.GameShop);
        reader.Write("Language", "BigMap", GameLanguage.BigMap);
        reader.Write("Language", "MiniMap", GameLanguage.MiniMap);
        reader.Write("Language", "DuraPanel", GameLanguage.DuraPanel);
        reader.Write("Language", "Mail", GameLanguage.Mail);
        reader.Write("Language", "Exit", GameLanguage.Exit);
        reader.Write("Language", "LogOut", GameLanguage.LogOut);
        reader.Write("Language", "Help", GameLanguage.Help);
        reader.Write("Language", "Keybinds", GameLanguage.Keybinds);
        reader.Write("Language", "Ranking", GameLanguage.Ranking);
        reader.Write("Language", "Creatures", GameLanguage.Creatures);
        reader.Write("Language", "Mount", GameLanguage.Mount);
        reader.Write("Language", "Fishing", GameLanguage.Fishing);
        reader.Write("Language", "Friends", GameLanguage.Friends);
        reader.Write("Language", "Mentor", GameLanguage.Mentor);
        reader.Write("Language", "Relationship", GameLanguage.Relationship);
        reader.Write("Language", "Groups", GameLanguage.Groups);
        reader.Write("Language", "Guild", GameLanguage.Guild);
        reader.Write("Language", "Trade", GameLanguage.Trade);
        reader.Write("Language", "Size", GameLanguage.Size);
        reader.Write("Language", "ChatSettings", GameLanguage.ChatSettings);
        reader.Write("Language", "Rotate", GameLanguage.Rotate);
        reader.Write("Language", "Close", GameLanguage.Close);
        reader.Write("Language", "GameMaster", GameLanguage.GameMaster);
        reader.Write("Language", "Report", GameLanguage.Report);
        reader.Write("Language", "GroupHealthPanel", GameLanguage.GroupHealthPanel);


        reader.Write("Language", "Expire", GameLanguage.Expire);
        reader.Write("Language", "ExpireNever", GameLanguage.ExpireNever);
        reader.Write("Language", "ExpirePaused", GameLanguage.ExpirePaused);
        reader.Write("Language", "Never", GameLanguage.Never);
        reader.Write("Language", "PatchErr", GameLanguage.PatchErr);
        reader.Write("Language", "LastOnline", GameLanguage.LastOnline);

        reader.Write("Language", "LowLevel", GameLanguage.LowLevel);
        reader.Write("Language", "LowGold", GameLanguage.LowGold);
        reader.Write("Language", "LowDC", GameLanguage.LowDC);
        reader.Write("Language", "LowMC", GameLanguage.LowMC);
        reader.Write("Language", "LowSC", GameLanguage.LowSC);

        reader.Write("Language", "Gold", GameLanguage.Gold);
        reader.Write("Language", "Credit", GameLanguage.Credit);

        reader.Write("Language", "YouGained", GameLanguage.YouGained);
        reader.Write("Language", "YouGained2", GameLanguage.YouGained2);
        reader.Write("Language", "ExperienceGained", GameLanguage.ExperienceGained);        
        reader.Write("Language", "LevelUp", GameLanguage.LevelUp);
        reader.Write("Language", "HeroLevelUp", GameLanguage.HeroLevelUp);

        reader.Write("Language", "HeroInventory", GameLanguage.Inventory);
        reader.Write("Language", "HeroCharacter", GameLanguage.Character);
        reader.Write("Language", "HeroSkills", GameLanguage.Skills);
        reader.Write("Language", "HeroSummon", GameLanguage.HeroSummon);
        reader.Write("Language", "HeroExperienceGained", GameLanguage.HeroExperienceGained);

        reader.Write("Language", "ItemDescription", GameLanguage.ItemDescription);
        reader.Write("Language", "RequiredLevel", GameLanguage.RequiredLevel);
        reader.Write("Language", "RequiredDC", GameLanguage.RequiredDC);
        reader.Write("Language", "RequiredMC", GameLanguage.RequiredMC);
        reader.Write("Language", "RequiredSC", GameLanguage.RequiredSC);
        reader.Write("Language", "ClassRequired", GameLanguage.ClassRequired);
        reader.Write("Language", "Holy", GameLanguage.Holy);
        reader.Write("Language", "Accuracy", GameLanguage.Accuracy);
        reader.Write("Language", "Agility", GameLanguage.Agility);
        reader.Write("Language", "DC", GameLanguage.DC);
        reader.Write("Language", "MC", GameLanguage.MC);
        reader.Write("Language", "SC", GameLanguage.SC);
        reader.Write("Language", "Durability", GameLanguage.Durability);
        reader.Write("Language", "Weight", GameLanguage.Weight);
        reader.Write("Language", "AC", GameLanguage.AC);
        reader.Write("Language", "MAC", GameLanguage.MAC);
        reader.Write("Language", "Luck", GameLanguage.Luck);

        reader.Write("Language", "DeleteCharacter", GameLanguage.DeleteCharacter);
        reader.Write("Language", "CharacterDeleted", GameLanguage.CharacterDeleted);
        reader.Write("Language", "CharacterCreated", GameLanguage.CharacterCreated);

        reader.Write("Language", "Resolution", GameLanguage.Resolution);
        reader.Write("Language", "Autostart", GameLanguage.Autostart);
        reader.Write("Language", "Usrname", GameLanguage.Usrname);
        reader.Write("Language", "Password", GameLanguage.Password);

        reader.Write("Language", "ShuttingDown", GameLanguage.ShuttingDown);

        reader.Write("Language", "MaxCombine", GameLanguage.MaxCombine);
        reader.Write("Language", "Count", GameLanguage.Count);
        reader.Write("Language", "ExtraSlots8", GameLanguage.ExtraSlots8);
        reader.Write("Language", "ExtraSlots4", GameLanguage.ExtraSlots4);

        reader.Write("Language", "Chat_All", GameLanguage.Chat_All);
        reader.Write("Language", "Chat_Short", GameLanguage.Chat_Short);
        reader.Write("Language", "Chat_Whisper", GameLanguage.Chat_Whisper);
        reader.Write("Language", "Chat_Lover", GameLanguage.Chat_Lover);
        reader.Write("Language", "Chat_Mentor", GameLanguage.Chat_Mentor);
        reader.Write("Language", "Chat_Group", GameLanguage.Chat_Group);
        reader.Write("Language", "Chat_Guild", GameLanguage.Chat_Guild);
        reader.Write("Language", "ExpandedStorageLocked", GameLanguage.ExpandedStorageLocked);
        reader.Write("Language", "ExtraStorage", GameLanguage.ExtraStorage);
        reader.Write("Language", "ExtendYourRentalPeriod", GameLanguage.ExtendYourRentalPeriod);
        reader.Write("Language", "ExpandedStorageExpiresOn", GameLanguage.ExpandedStorageExpiresOn);
        reader.Write("Language", "GameName", GameLanguage.GameName);
        reader.Write("Language", "CannotLeaveGame", GameLanguage.CannotLeaveGame);
        reader.Write("Language", "SelectKey", GameLanguage.SelectKey);
        reader.Write("Language", "WeaponSpiritFire", GameLanguage.WeaponSpiritFire);
        reader.Write("Language", "SpiritsFireDisappeared", GameLanguage.SpiritsFireDisappeared);
        reader.Write("Language", "WeddingRing", GameLanguage.WeddingRing);
        reader.Write("Language", "ItemTextFormat", GameLanguage.ItemTextFormat);
        reader.Write("Language", "DropAmount", GameLanguage.DropAmount);
        reader.Write("Language", "LowMana", GameLanguage.LowMana);

        reader.Write("Language", "NotFemale", GameLanguage.NotFemale);
        reader.Write("Language", "NotMale", GameLanguage.NotMale);
        reader.Write("Language", "NoCreatures", GameLanguage.NoCreatures);
        reader.Write("Language", "NoMount", GameLanguage.NoMount);
        reader.Write("Language", "NoFishingRod", GameLanguage.NoFishingRod);
        reader.Write("Language", "NotInGuild", GameLanguage.NotInGuild);
        reader.Write("Language", "AttemptingConnect", GameLanguage.AttemptingConnect);
        reader.Write("Language", "NoBagSpace", GameLanguage.NoBagSpace);

        reader.Write("Language", "CreatingCharactersDisabled", GameLanguage.CreatingCharactersDisabled);
        reader.Write("Language", "InvalidCharacterName", GameLanguage.InvalidCharacterName);
        reader.Write("Language", "NoClass", GameLanguage.NoClass);
        reader.Write("Language", "ToManyCharacters", GameLanguage.ToManyCharacters);
        reader.Write("Language", "CharacterNameExists", GameLanguage.CharacterNameExists);

        reader.Write("Language", "WarriorsDes", GameLanguage.WarriorsDes);
        reader.Write("Language", "WizardDes", GameLanguage.WizardDes);
        reader.Write("Language", "TaoistDes", GameLanguage.TaoistDes);
        reader.Write("Language", "AssassinDes", GameLanguage.AssassinDes);
        reader.Write("Language", "ArcherDes", GameLanguage.ArcherDes);

        reader.Write("Language", "DateSent", GameLanguage.DateSent);
        reader.Write("Language", "Send", GameLanguage.Send);
        reader.Write("Language", "Reply", GameLanguage.Reply);
        reader.Write("Language", "Read", GameLanguage.Read);
        reader.Write("Language", "Delete", GameLanguage.Delete);
        reader.Write("Language", "BlockList", GameLanguage.BlockList);
        reader.Write("Language", "EnterMailToName", GameLanguage.EnterMailToName);
        reader.Write("Language", "BeenPoisoned", GameLanguage.BeenPoisoned);
        reader.Write("Language", "AddFriend", GameLanguage.AddFriend);
        reader.Write("Language", "RemoveFriend", GameLanguage.RemoveFriend);
        reader.Write("Language", "FriendMemo", GameLanguage.FriendMemo);
        reader.Write("Language", "FriendMail", GameLanguage.FriendMail);
        reader.Write("Language", "FriendWhisper", GameLanguage.FriendWhisper);
        reader.Write("Language", "FriendEnterAddName", GameLanguage.FriendEnterAddName);
        reader.Write("Language", "FriendEnterBlockName", GameLanguage.FriendEnterBlockName);
        reader.Write("Language", "AddMentor", GameLanguage.AddMentor);
        reader.Write("Language", "RemoveMentorMentee", GameLanguage.RemoveMentorMentee);
        reader.Write("Language", "MentorRequests", GameLanguage.MentorRequests);
        reader.Write("Language", "MentorEnterName", GameLanguage.MentorEnterName);
        reader.Write("Language", "NoMentorship", GameLanguage.NoMentorship);
        reader.Write("Language", "RestedBuff", GameLanguage.RestedBuff);

        reader.Write("Language", "ItemTypeNothing", GameLanguage.ItemTypeNothing);
        reader.Write("Language", "ItemTypeWeapon", GameLanguage.ItemTypeWeapon);
        reader.Write("Language", "ItemTypeArmour", GameLanguage.ItemTypeArmour);
        reader.Write("Language", "ItemTypeHelmet", GameLanguage.ItemTypeHelmet);
        reader.Write("Language", "ItemTypeNecklace", GameLanguage.ItemTypeNecklace);
        reader.Write("Language", "ItemTypeBracelet", GameLanguage.ItemTypeBracelet);
        reader.Write("Language", "ItemTypeRing", GameLanguage.ItemTypeRing);
        reader.Write("Language", "ItemTypeAmulet", GameLanguage.ItemTypeAmulet);
        reader.Write("Language", "ItemTypeBelt", GameLanguage.ItemTypeBelt);
        reader.Write("Language", "ItemTypeBoots", GameLanguage.ItemTypeBoots);
        reader.Write("Language", "ItemTypeStone", GameLanguage.ItemTypeStone);
        reader.Write("Language", "ItemTypeTorch", GameLanguage.ItemTypeTorch);
        reader.Write("Language", "ItemTypePotion", GameLanguage.ItemTypePotion);
        reader.Write("Language", "ItemTypeOre", GameLanguage.ItemTypeOre);
        reader.Write("Language", "ItemTypeMeat", GameLanguage.ItemTypeMeat);
        reader.Write("Language", "ItemTypeCraftingMaterial", GameLanguage.ItemTypeCraftingMaterial);
        reader.Write("Language", "ItemTypeScroll", GameLanguage.ItemTypeScroll);
        reader.Write("Language", "ItemTypeGem", GameLanguage.ItemTypeGem);
        reader.Write("Language", "ItemTypeMount", GameLanguage.ItemTypeMount);
        reader.Write("Language", "ItemTypeBook", GameLanguage.ItemTypeBook);
        reader.Write("Language", "ItemTypeScript", GameLanguage.ItemTypeScript);
        reader.Write("Language", "ItemTypeReins", GameLanguage.ItemTypeReins);
        reader.Write("Language", "ItemTypeBells", GameLanguage.ItemTypeBells);
        reader.Write("Language", "ItemTypeSaddle", GameLanguage.ItemTypeSaddle);
        reader.Write("Language", "ItemTypeRibbon", GameLanguage.ItemTypeRibbon);
        reader.Write("Language", "ItemTypeMask", GameLanguage.ItemTypeMask);
        reader.Write("Language", "ItemTypeFood", GameLanguage.ItemTypeFood);
        reader.Write("Language", "ItemTypeHook", GameLanguage.ItemTypeHook);
        reader.Write("Language", "ItemTypeFloat", GameLanguage.ItemTypeFloat);
        reader.Write("Language", "ItemTypeBait", GameLanguage.ItemTypeBait);
        reader.Write("Language", "ItemTypeFinder", GameLanguage.ItemTypeFinder);
        reader.Write("Language", "ItemTypeReel", GameLanguage.ItemTypeReel);
        reader.Write("Language", "ItemTypeFish", GameLanguage.ItemTypeFish);
        reader.Write("Language", "ItemTypeQuest", GameLanguage.ItemTypeQuest);
        reader.Write("Language", "ItemTypeAwakening", GameLanguage.ItemTypeAwakening);
        reader.Write("Language", "ItemTypePets", GameLanguage.ItemTypePets);
        reader.Write("Language", "ItemTypeTransform", GameLanguage.ItemTypeTransform);
        reader.Write("Language", "ItemTypeSealedHero", GameLanguage.ItemTypeSealedHero);

        reader.Write("Language", "ItemGradeCommon", GameLanguage.ItemGradeCommon);
        reader.Write("Language", "ItemGradeRare", GameLanguage.ItemGradeRare);
        reader.Write("Language", "ItemGradeLegendary", GameLanguage.ItemGradeLegendary);
        reader.Write("Language", "ItemGradeMythical", GameLanguage.ItemGradeMythical);
        reader.Write("Language", "ItemGradeHeroic", GameLanguage.ItemGradeHeroic);

        reader.Write("Language", "NoAccountID", GameLanguage.NoAccountID);
        reader.Write("Language", "IncorrectPasswordAccountID", GameLanguage.IncorrectPasswordAccountID);
        reader.Write("Language", "GroupSwitch", GameLanguage.GroupSwitch);
        reader.Write("Language", "GroupAdd", GameLanguage.GroupAdd);
        reader.Write("Language", "GroupRemove", GameLanguage.GroupRemove);
        reader.Write("Language", "GroupAddEnterName", GameLanguage.GroupAddEnterName);
        reader.Write("Language", "GroupRemoveEnterName", GameLanguage.GroupRemoveEnterName);
        reader.Write("Language", "TooHeavyToHold", GameLanguage.TooHeavyToHold);
        reader.Write("Language", "SwitchMarriage", GameLanguage.SwitchMarriage);
        reader.Write("Language", "RequestMarriage", GameLanguage.RequestMarriage);
        reader.Write("Language", "RequestDivorce", GameLanguage.RequestDivorce);
        reader.Write("Language", "MailLover", GameLanguage.MailLover);
        reader.Write("Language", "WhisperLover", GameLanguage.WhisperLover);
    }


    public static void LoadServerLanguage(string languageIniPath)
    {
        if (!File.Exists(languageIniPath))
        {
            SaveServerLanguage(languageIniPath);
            return;
        }
        InIReader reader = new InIReader(languageIniPath);
        GameLanguage.Welcome = reader.ReadString("Language", "Welcome", GameLanguage.Welcome);
        GameLanguage.OnlinePlayers = reader.ReadString("Language", "OnlinePlayers", GameLanguage.OnlinePlayers);
        GameLanguage.LowLevel = reader.ReadString("Language", "LowLevel", GameLanguage.LowLevel);
        GameLanguage.LowGold = reader.ReadString("Language", "LowGold", GameLanguage.LowGold);
        GameLanguage.LowDC = reader.ReadString("Language", "LowDC", GameLanguage.LowDC);
        GameLanguage.LowMC = reader.ReadString("Language", "LowMC", GameLanguage.LowMC);
        GameLanguage.LowSC = reader.ReadString("Language", "LowSC", GameLanguage.LowSC);

        GameLanguage.LevelUp = reader.ReadString("Language", "LevelUp", GameLanguage.LevelUp);
        GameLanguage.HeroLevelUp = reader.ReadString("Language", "HeroLevelUp", GameLanguage.HeroLevelUp);

        GameLanguage.WeaponLuck = reader.ReadString("Language", "WeaponLuck", GameLanguage.WeaponLuck);
        GameLanguage.WeaponCurse = reader.ReadString("Language", "WeaponCurse", GameLanguage.WeaponCurse);
        GameLanguage.WeaponNoEffect = reader.ReadString("Language", "WeaponNoEffect", GameLanguage.WeaponNoEffect);

        GameLanguage.InventoryIncreased = reader.ReadString("Language", "InventoryIncreased", GameLanguage.InventoryIncreased);
        GameLanguage.ExpandedStorageExpiresOn = reader.ReadString("Language", "ExpandedStorageExpiresOn", GameLanguage.ExpandedStorageExpiresOn);
        GameLanguage.GameName = reader.ReadString("Language", "GameName", GameLanguage.GameName);
        GameLanguage.FaceToTrade = reader.ReadString("Language", "FaceToTrade", GameLanguage.FaceToTrade);
        GameLanguage.NoTownTeleport = reader.ReadString("Language", "NoTownTeleport", GameLanguage.NoTownTeleport);
        GameLanguage.CanNotRandom = reader.ReadString("Language", "CanNotRandom", GameLanguage.CanNotRandom);
        GameLanguage.CanNotDungeon = reader.ReadString("Language", "CanNotDungeon", GameLanguage.CanNotDungeon);
        GameLanguage.CannotResurrection = reader.ReadString("Language", "CannotResurrection", GameLanguage.CannotResurrection);
        GameLanguage.CanNotDrop = reader.ReadString("Language", "CanNotDrop", GameLanguage.CanNotDrop);

        GameLanguage.NotFemale = reader.ReadString("Language", "NotFemale", GameLanguage.NotFemale);
        GameLanguage.NotMale = reader.ReadString("Language", "NotMale", GameLanguage.NotMale);
        GameLanguage.NotInGuild = reader.ReadString("Language", "NotInGuild", GameLanguage.NotInGuild);
        GameLanguage.NewMail = reader.ReadString("Language", "NewMail", GameLanguage.NewMail);
        GameLanguage.CouldNotFindPlayer = reader.ReadString("Language", "CouldNotFindPlayer", GameLanguage.CouldNotFindPlayer);
        GameLanguage.NoMentorship = reader.ReadString("Language", "NoMentorship", GameLanguage.NoMentorship);
        GameLanguage.NoBagSpace = reader.ReadString("Language", "NoBagSpace", GameLanguage.NoBagSpace);
        GameLanguage.AllowingMentorRequests = reader.ReadString("Language", "AllowingMentorRequests", GameLanguage.AllowingMentorRequests);
        GameLanguage.BlockingMentorRequests = reader.ReadString("Language", "BlockingMentorRequests", GameLanguage.BlockingMentorRequests);
    }

    public static void SaveServerLanguage(string languageIniPath)
    {
        File.Delete(languageIniPath);
        InIReader reader = new InIReader(languageIniPath);
        reader.Write("Language", "Welcome", GameLanguage.Welcome);
        reader.Write("Language", "OnlinePlayers", GameLanguage.OnlinePlayers);
        reader.Write("Language", "LowLevel", GameLanguage.LowLevel);
        reader.Write("Language", "LowGold", GameLanguage.LowGold);
        reader.Write("Language", "LowDC", GameLanguage.LowDC);
        reader.Write("Language", "LowMC", GameLanguage.LowMC);
        reader.Write("Language", "LowSC", GameLanguage.LowSC);

        reader.Write("Language", "LevelUp", GameLanguage.LevelUp);
        reader.Write("Language", "HeroLevelUp", GameLanguage.HeroLevelUp);

        reader.Write("Language", "WeaponLuck", GameLanguage.WeaponLuck);
        reader.Write("Language", "WeaponCurse", GameLanguage.WeaponCurse);
        reader.Write("Language", "WeaponNoEffect", GameLanguage.WeaponNoEffect);

        reader.Write("Language", "InventoryIncreased", GameLanguage.InventoryIncreased);
        reader.Write("Language", "ExpandedStorageExpiresOn", GameLanguage.ExpandedStorageExpiresOn);
        reader.Write("Language", "GameName", GameLanguage.GameName);
        reader.Write("Language", "FaceToTrade", GameLanguage.FaceToTrade);
        reader.Write("Language", "NoTownTeleport", GameLanguage.NoTownTeleport);
        reader.Write("Language", "CanNotRandom", GameLanguage.CanNotRandom);
        reader.Write("Language", "CanNotDungeon", GameLanguage.CanNotDungeon);
        reader.Write("Language", "CannotResurrection", GameLanguage.CannotResurrection);
        reader.Write("Language", "CanNotDrop", GameLanguage.CanNotDrop);

        reader.Write("Language", "NotFemale", GameLanguage.NotFemale);
        reader.Write("Language", "NotMale", GameLanguage.NotMale);
        reader.Write("Language", "NotInGuild", GameLanguage.NotInGuild);
        reader.Write("Language", "NewMail", GameLanguage.NewMail);
        reader.Write("Language", "CouldNotFindPlayer", GameLanguage.CouldNotFindPlayer);
        reader.Write("Language", "NoMentorship", GameLanguage.NoMentorship);
        reader.Write("Language", "NoBagSpace", GameLanguage.NoBagSpace);
        reader.Write("Language", "AllowingMentorRequests", GameLanguage.AllowingMentorRequests);
        reader.Write("Language", "BlockingMentorRequests", GameLanguage.BlockingMentorRequests);
    }
}
