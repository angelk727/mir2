using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using C = ClientPackets;
using S = ServerPackets;
using System.Linq;

public enum PanelType : byte
{
    Buy = 0,
    Sell,
    Repair,
    SpecialRepair,
    Consign,
    Craft,
    Refine,
    CheckRefine,
    Disassemble,
    Downgrade,
    Reset,
    CollectRefine,
    ReplaceWedRing,
}

public enum BlendMode : sbyte
{
    NONE = -1,
    NORMAL = 0,
    LIGHT = 1,
    LIGHTINV = 2,
    INVNORMAL = 3,
    INVLIGHT = 4,
    INVLIGHTINV = 5,
    INVCOLOR = 6,
    INVBACKGROUND = 7
}

public enum DamageType : byte
{
    Hit = 0,
    Miss = 1,
    Critical = 2,
    Invalid = 3,
    Heal = 4,
}

[Flags]
public enum GMOptions : byte
{
    None = 0,
    GameMaster = 0x0001,
    Observer = 0x0002,
    Superman = 0x0004
}

public enum AwakeType : byte
{
    None = 0,
    DC,
    MC,
    SC,
    AC,
    MAC,
    HPMP,
}

[Flags]
public enum LevelEffects : byte
{
    None = 0,
    Mist = 0x0001,
    RedDragon = 0x0002,
    BlueDragon = 0x0004
}

public enum OutputMessageType : byte
{
    Normal, 
    Quest,
    Guild
}

public enum ItemGrade : byte
{
    None = 0,
    Common = 1,
    Rare = 2,
    Legendary = 3,
    Mythical = 4,
}
public enum StatType : byte
{
    AC = 0,
    MAC = 1,
    DC = 2,
    MC = 3,
    SC = 4,
    HP = 5,
    MP = 6,
    HP_Percent = 7,
    MP_Percent = 8,
    HP_Regen = 9,
    MP_Regen = 10,
    ASpeed = 11,
    Luck = 12,
    Strong = 13,
    Accuracy = 14,
    Agility = 15,
    MagicResist = 16,
    PoisonResist = 17,
    PoisonAttack = 18,
    PoisonRegen = 19,
    Freezing = 20,
    Holy = 21,
    Durability = 22,
    Unknown = 23
}
public enum RefinedValue : byte
{
    None = 0,
    DC = 1,
    MC = 2,
    SC = 3,
}

public enum QuestType : byte
{
    General = 0,
    Daily = 1,
    Repeatable = 2,
    Story = 3
}

public enum QuestIcon : byte
{
    None = 0,
    QuestionWhite = 1,
    ExclamationYellow = 2,
    QuestionYellow = 3,
    ExclamationBlue = 5,
    QuestionBlue = 6,
    ExclamationGreen = 52,
    QuestionGreen = 53
}

public enum QuestState : byte
{
    Add,
    Update,
    Remove
}

public enum DefaultNPCType : byte
{
    Login,
    LevelUp,
    UseItem,
    MapCoord,
    MapEnter,
    Die,
    Trigger,
    CustomCommand,
    OnAcceptQuest,
    OnFinishQuest,
    Daily,
    TalkMonster,
    KillMonster,
}

public enum IntelligentCreatureType : byte
{
    None = 99,
    BabyPig = 0,
    Chick = 1,
    Kitten = 2,
    BabySkeleton = 3,
    Baekdon = 4,
    Wimaen = 5,
    BlackKitten = 6,
    BabyDragon = 7,
    OlympicFlame = 8,
    BabySnowMan = 9,
    Frog = 10,
    BabyMonkey = 11,
    AngryBird = 12,
    Foxey = 13,
}

public enum SpecialItemShape : short
{
    TeleportEscape = 0,
    Teleport = 1,
    TeleportRandom = 2,
    BenedictionOil = 3,
    RepairOil = 4,
    WarGodOil = 5,
    ResurrectionScroll = 6,
    CreditScroll = 7,
    MapShoutScroll = 8,
    ServerShoutScroll = 9,
    GuildSkillScroll = 10,
    HomeTeleport = 11,
    LotteryTicket = 12,
    RepairAllOil = 13,
    SuperBenedictionOil = 14,
    AwakeMaterial = 100,
    SuccessRateUpItem = 200,
}

public enum MonsterType : byte
{
    Noraml,
    Elite,
    Boss
}

//1 blank mob files
//7 mob frames not added
//2 blank frame sets (92, 173)
//4 mob frames duplicate of other frame sets

//TODO: add 2 missing frames in to blank frames, remove 2 duplicate frames (leaving no blanks and 2 duplicates)
public enum Monster : ushort
{
    Guard = 0,
    TaoistGuard = 1,
    Guard2 = 2,
    Hen = 3,
    Deer = 4,
    Scarecrow = 5,
    HookingCat = 6,
    RakingCat = 7,
    Yob = 8,
    Oma = 9,
    CannibalPlant = 10,
    ForestYeti = 11,
    SpittingSpider = 12,
    ChestnutTree = 13,
    EbonyTree = 14,
    LargeMushroom = 15,
    CherryTree = 16,
    OmaFighter = 17,
    OmaWarrior = 18,
    CaveBat = 19,
    CaveMaggot = 20,
    Scorpion = 21,
    Skeleton = 22,
    BoneFighter = 23,
    AxeSkeleton = 24,
    BoneWarrior = 25,
    BoneElite = 26,
    Dung = 27,
    Dark = 28,
    WoomaSoldier = 29,
    WoomaFighter = 30,
    WoomaWarrior = 31,
    FlamingWooma = 32,
    WoomaGuardian = 33,
    WoomaTaurus = 34,
    WhimperingBee = 35,
    GiantWorm = 36,
    Centipede = 37,
    BlackMaggot = 38,
    Tongs = 39,
    EvilTongs = 40,
    EvilCentipede = 41,
    BugBat = 42,
    BugBatMaggot = 43,
    WedgeMoth = 44,
    RedBoar = 45,
    BlackBoar = 46,
    SnakeScorpion = 47,
    WhiteBoar = 48,
    EvilSnake = 49,     // 邪恶毒蛇

    BombSpider = 50,
    RootSpider = 51,
    SpiderBat = 52,
    VenomSpider = 53,
    GangSpider = 54,
    GreatSpider = 55,
    LureSpider = 56,
    BigApe = 57,
    EvilApe = 58,
    GrayEvilApe = 59,
    RedEvilApe = 60,
    CrystalSpider = 61,
    RedMoonEvil = 62,   // 赤月恶魔

    BigRat = 63,
    ZumaArcher = 64,
    ZumaStatue = 65,
    ZumaGuardian = 66,
    RedThunderZuma = 67,
    ZumaTaurus = 68,   // 祖玛僵住

    DigOutZombie = 69,
    ClZombie = 70,
    NdZombie = 71,
    CrawlerZombie = 72,
    ShamanZombie = 73,
    Ghoul = 74,
    KingScorpion = 75,
    KingHog = 76,
    DarkDevil = 77,     // 黑天魔王

    BoneFamiliar = 78,
    Shinsu = 79,
    Shinsu1 = 80,
    SpiderFrog = 81,
    HoroBlaster = 82,
    BlueHoroBlaster = 83,
    KekTal = 84,
    VioletKekTal = 85,
    Khazard = 86,
    RoninGhoul = 87,
    ToxicGhoul = 88,
    BoneCaptain = 89,
    BoneSpearman = 90,
    BoneBlademan = 91,
    BoneArcher = 92,
    BoneLord = 93,      // 黄泉教主

    Minotaur = 94,
    IceMinotaur = 95,
    ElectricMinotaur = 96,
    WindMinotaur = 97,
    FireMinotaur = 98,
    RightGuard = 99,
    LeftGuard = 100,
    MinotaurKing = 101, // 牛魔王

    // 龙蛇川
    FrostTiger = 102,
    Sheep = 103,
    Wolf = 104,
    ShellNipper = 105,
    Keratoid = 106,
    GiantKeratoid = 107,
    SkyStinger = 108,
    SandWorm = 109,
    VisceralWorm = 110,
    RedSnake = 111,
    TigerSnake = 112,
    Yimoogi = 113,   // 蛇

    // 魔龙
    GiantWhiteSnake = 114,
    BlueSnake = 115,
    YellowSnake = 116,
    HolyDeva = 117,
    AxeOma = 118,
    SwordOma = 119,
    CrossbowOma = 120,
    WingedOma = 121,
    FlailOma = 122,
    OmaGuard = 123,
    YinDevilNode = 124,
    YangDevilNode = 125,
    OmaKing = 126,      // 破凰魔神

    // 狐狸
    BlackFoxman = 127,
    RedFoxman = 128,
    WhiteFoxman = 129,
    TrapRock = 130,
    GuardianRock = 131,
    ThunderElement = 132,
    CloudElement = 133,
    GreatFoxSpirit = 134,
    HedgeKekTal = 135,
    BigHedgeKekTal = 136,
    RedFrogSpider = 137,
    BrownFrogSpider = 138,
    ArcherGuard = 139,
    KatanaGuard = 140,
    ArcherGuard2 = 141,
    Pig = 142,
    Bull = 143,
    Bush = 144,

    ChristmasTree = 145,
    HighAssassin = 146,
    // 红洞
    DarkDustPile = 147,
    DarkBrownWolf = 148,
    Football = 149, 
    GingerBreadman = 150,
    HalloweenScythe = 151,
    GhastlyLeecher = 152,
    CyanoGhast = 153,
    MutatedManworm = 154,
    CrazyManworm = 155,
    MudPile = 156,
    TailedLion = 157,
    Behemoth = 158,//done BOSS  怨恶
    DarkDevourer = 159,//done


    PoisonHugger = 160,//done
    Hugger = 161,//done
    MutatedHugger = 162,//done
    DreamDevourer = 163,//done
    Treasurebox = 164,//done
    SnowPile = 165,//done
    Snowman = 166,//done
    SnowTree = 167,//done
    GiantEgg = 168,//done
    RedTurtle = 169,//done
    GreenTurtle = 170,//done
    BlueTurtle = 171,//done
    Catapult = 172, //not added frames //special 3 states in 1 
    SabukWallSection = 173, //not added frames

    // 南蛮
    NammandWallSection = 174, //not added frames
    SiegeRepairman = 175, //not added frames
    BlueSanta = 176,//done
    BattleStandard = 177,//done
    //ArcherGuard2 = 178,//done
    RedYimoogi = 179,//done
    LionRiderMale = 180, //frames not added
    LionRiderFemale = 181, //frames not added
    Tornado = 182,//done
    FlameTiger = 183,//done
    WingedTigerLord = 184,//done BOSS  野兽王

    // 乌龟洞
    TowerTurtle = 185,//done
    FinialTurtle = 186,//done
    TurtleKing = 187,//done BOSS
    DarkTurtle = 188,//done
    LightTurtle = 189,//done  

    // 魔龙？
    DarkSwordOma = 190,//done
    DarkAxeOma = 191,//done
    DarkCrossbowOma = 192,//done
    DarkWingedOma = 193,//done
    BoneWhoo = 194,//done  骷髅精灵
    DarkSpider = 195,//done
    ViscusWorm = 196,//done
    ViscusCrawler = 197,//done
    CrawlerLave = 198,//done
    DarkYob = 199,//done

    // 月氏
    FlamingMutant = 200,//FINISH
    StoningStatue = 201,//FINISH BOSS
    FlyingStatue = 202,//FINISH
    ValeBat = 203,//done
    Weaver = 204,//done
    VenomWeaver = 205,//done
    CrackingWeaver = 206,//done
    ArmingWeaver = 207,//done
    CrystalWeaver = 208,//done
    FrozenZumaStatue = 209,//done
    FrozenZumaGuardian = 210,//done
    FrozenRedZuma = 211,//done
    GreaterWeaver = 212,//done
    SpiderWarrior = 213,//done
    SpiderBarbarian = 214,//done

    // 黄泉路
    HellSlasher = 215,//done
    HellPirate = 216,//done
    HellCannibal = 217,//done
    HellKeeper = 218, //done BOSS
    HellBolt = 219, //done
    WitchDoctor = 220,//done
    // 冰狱
    ManectricHammer = 221,//done
    ManectricClub = 222,//done
    ManectricClaw = 223,//done
    ManectricStaff = 224,//done
    NamelessGhost = 225,//done
    DarkGhost = 226,//done
    ChaosGhost = 227,//done
    ManectricBlest = 228,//done
    ManectricKing = 229,//done
    FrozenDoor = 230,//done
    IcePillar = 231,//done
    FrostYeti = 232,//done
    ManectricSlave = 233,//done

    // 业火
    TrollHammer = 234,//done
    TrollBomber = 235,//done
    TrollStoner = 236,//done
    TrollKing = 237,//done BOSS
    FlameSpear = 238,//done
    FlameMage = 239,//done
    FlameScythe = 240,//done
    FlameAssassin = 241,//done
    FlameQueen = 242, //finish BOSS
    HellKnight1 = 243,//done
    HellKnight2 = 244,//done
    HellKnight3 = 245,//done
    HellKnight4 = 246,//done
    HellLord = 247,//done BOSS
    WaterGuard = 248,//done
    IceGuard = 249,
    ElementGuard = 250,
    DemonGuard = 251,
    KingGuard = 252,
    Snake10 = 253,//done
    Snake11 = 254,//done
    Snake12 = 255,//done
    Snake13 = 256,//done
    Snake14 = 257,//done
    Snake15 = 258,//done
    Snake16 = 259,//done
    Snake17 = 260,//done

    // 南蛮
    DeathCrawler = 261,
    BurningZombie = 262,
    MudZombie = 263,
    FrozenZombie = 264,
    UndeadWolf = 265,
    Demonwolf = 266,
    WhiteMammoth = 267,
    DarkBeast = 268,
    LightBeast = 269,
    BloodBaboon = 270,
    HardenRhino = 271,
    AncientBringer = 272,   // 丹墨

    // 猫洞
    FightingCat = 273,
    FireCat = 274,
    CatWidow = 275,
    StainHammerCat = 276,
    BlackHammerCat = 277,
    StrayCat = 278,
    CatShaman = 279,
    Jar1 = 280,
    Jar2 = 281,
    SeedingsGeneral = 282,
    RestlessJar = 283,
    GeneralJinmYo = 284,

    Bunny = 285,    // 兔子

    // 异密洞
    Tucson = 286, // done
    TucsonFighter = 287, // done
    TucsonMage = 288,
    TucsonWarrior = 289,
    Armadillo = 290,
    ArmadilloElder = 291,
    TucsonEgg = 292, // done
    TucsonPlagued = 293, // done        
    SandSnail = 294,
    CannibalTentacles = 295,
    TucsonGeneral = 296, // done

    // 如何
    GasToad = 297, // done
    Mantis = 298, // done
    SwampWarrior = 299, // done
    AssassinBird = 300,
    RhinoWarrior = 301,
    RhinoPriest = 302,
    SwampSlime = 303,
    RockGuard = 304,    // c
    MudWarrior = 305,   // c
    SmallPot = 306,     // c
    TreeQueen = 307,    // c
    ShellFighter = 308, // c

    // 灰色洞穴
    DarkBaboon = 309,
    TwinHeadBeast = 310,
    OmaCannibal = 311,
    OmaBlest = 312,
    OmaSlasher = 313,
    OmaAssassin = 314,
    OmaMage = 315,
    OmaWitchDoctor = 316,
    LightningBead = 317,
    HealingBead = 318,
    PowerUpBead = 319,
    DarkOmaKing = 320,

    CaveMage = 321,

    // 片木岛
    Mandrill = 322,
    PlagueCrab = 323, //변이갑충
    CreeperPlant = 324,
    FloatingWraith = 325,
    ArmedPlant = 326,
    AvengerPlant = 327,
    Nadz = 328,
    AvengingSpirit = 329,
    AvengingWarrior = 330,
    AxePlant = 331,
    WoodBox = 332,
    ClawBeast = 333,
    KillerPlant = 334, // done

    // 沙遗址 
    SackWarrior = 335,
    WereTiger = 336,
    KingHydrax = 337,
    Hydrax = 338,
    HornedMage = 339,   
    Basiloid = 340, // 不是怪物 应该和339合并
    HornedArcher = 341, 
    ColdArcher = 342,
    HornedWarrior = 343, // no done
    FloatingRock = 344,
    ScalyBeast = 345,
    HornedSorceror = 346,       // boss
    BoulderSpirit = 347,
    HornedCommander = 348,      // bigboss

    // 玄月林
    MoonStone = 349,
    SunStone = 350,
    LightningStone = 351,
    Turtlegrass = 352,
    Mantree = 353,
    Bear = 354,
    Leopard = 355,
    ChieftainArcher = 356,
    ChieftainSword = 357,
    StoningSpider = 358, //Archer Spell mob (not yet coded)
    VampireSpider = 359, //Archer Spell mob
    SpittingToad = 360, //Archer Spell mob
    SnakeTotem = 361, //Archer Spell mob
    CharmedSnake = 362, //Archer Spell mob

    // 雪原
    FrozenSoldier = 363,    
    FrozenFighter = 364,
    FrozenArcher = 365,
    FrozenKnight = 366,
    FrozenGolem = 367,
    IcePhantom = 368,
    SnowWolf = 369,
    SnowWolfKing = 370,

    // 神鬼
    WaterDragon = 371,
    BlackTortoise = 372,
    Manticore = 373,
    DragonWarrior = 374,
    DragonArcher = 375,
    Kirin = 376,

    Guard3 = 377,
    ArcherGuard3 = 378,
    Bunny2 = 379,

    // 冰川
    FrozenMiner = 380,
    FrozenAxeman = 381,
    FrozenMagician = 382,
    SnowYeti = 383, 
    IceCrystalSoldier = 384,
    DarkWraith = 385,
    DarkSpirit = 386,
    CrystalBeast = 387, // done

    //
    RedOrb = 388,
    BlueOrb = 389,
    YellowOrb = 390,
    GreenOrb = 391,
    WhiteOrb = 392,
    FatalLotus = 393,
    AntCommander = 394,
    CargoBoxwithlogo = 395,
    Doe = 396,
    Reindeer = 397, //frames not added
    AngryReindeer = 398,
    CargoBox = 399,
    
    Ram1 = 400,
    Ram2 = 401,
    Kite = 402,

    // 冰原
    SnowFlower = 403,   // 冰川花
    SnowMouse = 404,    // 冰川
    SnowSnail = 405,
    SnowWarrior = 406,
    SnowArchor = 407,
    SnowAssassin = 408,
    SnowFlowerQueen = 409,  // done

    // 地宫
    SecretWarrior2 = 410, // 枪兵
    SecretWarrior3 = 411,//神兽
    SecretWarrior4 = 412, 
    SecretKnight = 413,//骑士
    SecretPaper1 = 414,
    SecretPaper2 = 415,
    SecretPaper3 = 416,
    SecretPaper4 = 417,
    SecretJudge = 418,
    SecretWizard = 419,
    SecretWarrior8 = 420,
    SecretQueen = 421,

    // 昆仑
    KunLun0 = 480,
    KunLun1 = 481,
    KunLun2 = 482,
    KunLun3 = 483,
    KunLun4 = 484,
    KunLun5 = 485,
    KunLun6 = 486,
    KunLun7 = 487,
    KunLun8 = 488,
    KunLun9 = 489,
    KunLun10 = 490,
    KunLun11 = 491,
    KunLun12 = 492,
    KunLun13 = 493,
    KunLun14 = 494,

    // 毒药林
    DogYoLin0 = 495,
    DogYoLin1 = 496,
    DogYoLin2 = 497,// None
    DogYoLin3 = 498,// None
    DogYoLin4 = 499,
    DogYoLin5 = 500,
    DogYoLin6 = 501,
    DogYoLin7 = 502,
    DogYoLin8 = 503,
    MoreaWind = 504,
    DogYoLinDoor = 505,


    EvilMir = 900,
    EvilMirBody = 901,
    DragonStatue = 902,
    HellBomb1 = 903,
    HellBomb2 = 904,
    HellBomb3 = 905,

    SabukGate = 950,
    PalaceWallLeft = 951,
    PalaceWall1 = 952,
    PalaceWall2 = 953,
    GiGateSouth = 954,
    GiGateEast = 955,
    GiGateWest = 956,
    SSabukWall1 = 957,
    SSabukWall2 = 958,
    SSabukWall3 = 959,

    BabyPig = 10000,//Permanent
    Chick = 10001,//Special
    Kitten = 10002,//Permanent
    BabySkeleton = 10003,//Special
    Baekdon = 10004,//Special
    Wimaen = 10005,//Event
    BlackKitten = 10006,//unknown
    BabyDragon = 10007,//unknown
    OlympicFlame = 10008,//unknown
    BabySnowMan = 10009,//unknown
    Frog = 10010,//unknown
    BabyMonkey = 10011,//unknown
    AngryBird = 10012,
    Foxey = 10013,
}

public enum MirAction : byte
{
    Standing,
    Walking,
    Running,
    Pushed,
    DashL,
    DashR,
    DashFail,
    Stance,
    Stance2,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Attack5,
    AttackRange1,
    AttackRange2,
    AttackRange3,
    AttackRange4,
    AttackRange5,
    AttackRange6,
    AttackRange7,
    AttackRange8,
    AttackRange9,
    Special,
    Struck,
    Harvest,
    Spell,
    Die,
    Dead,
    Skeleton,
    Show,
    Hide,
    Stoned,
    Appear,
    Revive,
    SitDown,
    Mine,
    Sneek,
    DashAttack,
    Lunge,

    WalkingBow,
    RunningBow,
    Jump,

    MountStanding,
    MountWalking,
    MountRunning,
    MountStruck,
    MountAttack,

    FishingCast,
    FishingWait,
    FishingReel,

    JumpDown,
    PushPalm,

}

public enum CellAttribute : byte
{
    Walk = 0,
    HighWall = 1,
    LowWall = 2,
}

public enum LightSetting : byte
{
    Normal = 0,
    Dawn = 1,
    Day = 2,
    Evening = 3,
    Night = 4
}
public enum MirGender : byte
{
    Male = 0,
    Female = 1
}

public enum MirClass : byte
{
    Warrior = 0,
    Wizard = 1,
    Taoist = 2,
    Assassin = 3,
    Archer = 4,
    Monk = 5, //Monk
}

public enum MirDirection : byte
{
    Up = 0,
    UpRight = 1,
    Right = 2,
    DownRight = 3,
    Down = 4,
    DownLeft = 5,
    Left = 6,
    UpLeft = 7
}

public enum ObjectType : byte
{
    None= 0,
    Player = 1,
    Item = 2,
    Merchant = 3,
    Spell = 4,
    Monster = 5,
    Deco = 6,
    Creature = 7
}

public enum ChatType : byte
{
    Normal = 0,
    Shout = 1,
    System = 2,
    Hint = 3,
    Announcement = 4,
    Group = 5,
    WhisperIn = 6,
    WhisperOut = 7,
    Guild = 8,
    Trainer = 9,
    LevelUp = 10,
    System2 = 11,
    Relationship = 12,
    Mentor = 13,
    Shout2 = 14,
    Shout3 = 15
}

public enum ItemType : byte
{
    Nothing = 0,
    Weapon = 1,
    Armour = 2,
    Helmet = 4,
    Necklace = 5,
    Bracelet = 6,
    Ring = 7,
    Amulet = 8,
    Belt = 9,
    Boots = 10,
    Stone = 11,
    Torch = 12,
    Potion = 13,
    Ore = 14,
    Meat = 15,
    CraftingMaterial = 16,
    Scroll = 17,
    Gem = 18,
    Mount = 19,
    Book = 20,
    Script = 21,
    Reins = 22,
    Bells = 23,
    Saddle = 24,
    Ribbon = 25,
    Mask = 26,
    Food = 27,
    Hook = 28,
    Float = 29,
    Bait = 30,
    Finder = 31,
    Reel = 32,
    Fish = 33,
    Quest = 34,
	Awakening = 35,
    Pets = 36,
    Transform = 37,
    RuneStone = 38,
}

public enum MirGridType : byte
{
    None = 0,
    Inventory = 1,
    Equipment = 2,
    Trade = 3,
    Storage = 4,
    BuyBack = 5,
    DropPanel = 6,
    Inspect = 7,
    TrustMerchant = 8,
    GuildStorage = 9,
    GuestTrade = 10,
    Mount = 11,
    Fishing = 12,
    QuestInventory = 13,
    AwakenItem = 14,
    Mail = 15,
    Refine = 16,
    Renting = 17,
    GuestRenting = 18,
    Craft = 19,
    SocketItem,
    SockeRune,
}

public enum EquipmentSlot : byte
{
    Weapon = 0,
    Armour = 1,
    Helmet = 2,
    Torch = 3,
    Necklace = 4,
    BraceletL = 5,
    BraceletR = 6,
    RingL = 7,
    RingR = 8,
    Amulet = 9,
    Belt = 10,
    Boots = 11,
    Stone = 12,
    Mount = 13,
    Transform = 14,
    AmuletUp = 15,
}

public enum MountSlot : byte
{
    Reins = 0,
    Bells = 1,
    Saddle = 2,
    Ribbon = 3,
    Mask = 4
}

public enum FishingSlot : byte
{
    Hook = 0,
    Float = 1,
    Bait = 2,
    Finder = 3,
    Reel = 4
}

public enum AttackMode : byte
{
    Peace = 0,
    Group = 1,
    Guild = 2,
    EnemyGuild = 3,
    RedBrown = 4,
    All = 5
}

public enum PetMode : byte
{
    Both = 0,
    MoveOnly = 1,
    AttackOnly = 2,
    None = 3,
}

[Flags]
public enum PoisonType : ushort
{
    None = 0,
    Green = 1,
    Red = 2,
    Slow = 4,
    Frozen = 8,
    Stun = 16,
    Paralysis = 32,
    DelayedExplosion = 64,
    Bleeding = 128,
    LRParalysis = 256,
    Morea = 512,
    DelayedExplosion2 = 1024,
}

[Flags]
public enum BindMode : short
{
    none = 0,
    DontDeathdrop = 1,//0x0001
    DontDrop = 2,//0x0002
    DontSell = 4,//0x0004
    DontStore = 8,//0x0008
    DontTrade = 16,//0x0010
    DontRepair = 32,//0x0020
    DontUpgrade = 64,//0x0040
    DestroyOnDrop = 128,//0x0080
    BreakOnDeath = 256,//0x0100
    BindOnEquip = 512,//0x0200
    NoSRepair = 1024,//0x0400
    NoWeddingRing = 2048,//0x0800
    UnableToRent = 4096,
    UnableToDisassemble = 8192
}

[Flags]
public enum SpecialItemMode : short
{
    None = 0,
    Paralize = 0x0001,
    Teleport = 0x0002,
    Clearring = 0x0004,
    Protection = 0x0008,
    Revival = 0x0010,
    Muscle = 0x0020,
    Flame = 0x0040,
    Healing = 0x0080,
    Probe = 0x0100,
    Skill = 0x0200,
    NoDuraLoss = 0x0400,
    Blink = 0x800,
}

[Flags]
public enum RequiredClass : byte
{
    NoLimit = 0,
    Warrior = 1,
    Wizard = 2,
    Taoist = 4,
    Assassin = 8,
    Archer = 16,
    Monk = 32, //Monk

    WarWizTao = Warrior | Wizard | Taoist,
    None = WarWizTao | Assassin | Archer | Monk
}
[Flags]
public enum RequiredGender : byte
{
    Male = 1,
    Female = 2,
    None = Male | Female
}
public enum RequiredType : byte
{
    Level = 0,
    MaxAC = 1,
    MaxMAC = 2,
    MaxDC = 3,
    MaxMC = 4,
    MaxSC = 5,
    MaxLevel = 6,
    MinAC = 7,
    MinMAC = 8,
    MinDC = 9,
    MinMC = 10,
    MinSC = 11,
}

public enum ItemSet : byte
{
    None = 0,
    Spirit = 1,
    Recall = 2,
    RedOrchid = 3,
    RedFlower = 4,
    Smash = 5,
    HwanDevil = 6,
    Purity = 7,
    FiveString = 8,
    Mundane = 9,
    NokChi = 10,
    TaoProtect = 11,
    Mir = 12,
    Bone = 13,
    Bug = 14,
    WhiteGold = 15,
    WhiteGoldH = 16,
    RedJade = 17,
    RedJadeH = 18,
    Nephrite = 19,
    NephriteH = 20,
    Whisker1 = 21,
    Whisker2 = 22,
    Whisker3 = 23,
    Whisker4 = 24,
    Whisker5 = 25,
    Hyeolryong = 26,
    Monitor = 27,
    Oppressive = 28,
    Paeok = 29,
    Sulgwan = 30
}

public enum Spell : byte
{
    None = 0,

    //Warrior 
    Fencing = 1,                // 基本剑法
    Slaying = 2,                // 攻杀剑法
    Thrusting = 3,              // 刺杀剑法
    HalfMoon = 4,               // 半月弯刀
    ShoulderDash = 5,           // 野蛮冲撞
    TwinDrakeBlade = 6,         // 雷霆剑法 - 原:双龙斩
    Entrapment = 7,             // 擒龙手 - 原:捕绳剑
    FlamingSword = 8,           // 烈火剑法
    LionRoar = 9,               // 狮子吼
    CrossHalfMoon = 10,         // 狂风斩
    BladeAvalanche = 11,        // 空破闪
    ProtectionField = 12,       // 护身气幕 铁布衫
    Rage = 13,                  // 剑气爆
    CounterAttack = 14,         // 罗汉金身
    SlashingBurst = 15,         // 日闪
    Fury = 16,                  // 血龙剑法
    ImmortalSkin = 17,          // 金刚不坏
    CounterAttack1 = 18,        // 罗汉金身1
    ProtectionField1 = 19,      // 护身气幕
    EntrapSwordSecret = 20,     // 捕绳剑 秘籍
    ImmortalSkin1 = 21,         // 金刚不坏 秘籍

    //Wizard
    FireBall = 31,              // 火球术
    Repulsion = 32,             // 抗拒火环
    ElectricShock = 33,         // 诱惑之光
    GreatFireBall = 34,         // 大火球
    HellFire = 35,              // 地狱火
    ThunderBolt = 36,           // 雷电术
    Teleport = 37,              // 瞬息移动
    FireBang = 38,              // 爆裂火焰
    FireWall = 39,              // 火墙
    Lightning = 40,             // 疾光电影
    FrostCrunch = 41,           // 寒冰掌
    ThunderStorm = 42,          // 地狱雷光
    MagicShield = 43,           // 魔法盾
    TurnUndead = 44,            // 圣言术
    Vampirism = 45,             // 噬血术
    IceStorm = 46,              // 冰咆哮
    FlameDisruptor = 47,        // 灭天火
    Mirroring = 48,             // 分身术
    FlameField = 49,            // 火龙气焰
    Blizzard = 50,              // 天霜冰环
    MagicBooster = 51,          // 深延术
    MeteorStrike = 52,          // 流星火雨
    IceThrust = 53,             // 冰焰术
    FastMove = 54,
    StormEscape = 55,           // 雷仙风
    GreateFireBallSecret = 56,  // 大火球秘籍
    Bisul = 57,                 // 天上秘术
    StormEscape1 = 58,          // 雷仙风秘籍


    //Taoist
    Healing = 61,               //治愈术
    SpiritSword = 62,           //精神力战法
    Poisoning = 63,             //施毒术
    SoulFireBall = 64,          //灵魂火符
    SummonSkeleton = 65,        //召唤骷髅
    Hiding = 67,                //隐身术
    MassHiding = 68,            //集体隐身术
    SoulShield = 69,            //幽灵盾
    Revelation = 70,            //心灵启示
    BlessedArmour = 71,         //神圣战甲术
    EnergyRepulsor = 72,        //气功波
    TrapHexagon = 73,           //困魔咒
    Purification = 74,          //净化术
    MassHealing = 75,           //群体治疗术
    Hallucination = 76,         //迷魂术
    UltimateEnhancer = 77,      //无极真气
    SummonShinsu = 78,          //召唤神兽
    Reincarnation = 79,         //苏生术
    SummonHolyDeva = 80,        //召唤月灵
    Curse = 81,                 //诅咒术
    Plague = 82,                //瘟疫
    PoisonCloud = 83,           //毒云
    EnergyShield = 84,          //先天气功
    PetEnhancer = 85,           //血龙水
    HealingCircle = 86,         //阴阳五行阵
    HealingCircle2 = 87,        //阴阳五行阵-秘籍
    Healing2 = 88,              //治愈术-秘籍

    //Assassin
    FatalSword = 91,            //绝命剑法
    DoubleSlash = 92,           //风剑术
    Haste = 93,                 //体迅风
    FlashDash = 94,             //拔刀术
    LightBody = 95,             //风身术
    HeavenlySword = 96,         //炎龙波 - 原:迁移剑
    FireBurst = 97,             //旋风腿 - 原:烈风击
    Trap = 98,                  //灵魂陷阱 - 原:捕缚术
    PoisonSword = 99,           //致残毒药
    MoonLight = 100,            //潜行 - 原:月影术
    MPEater = 101,              //吸气
    SwiftFeet = 102,            //轻身步
    DarkBody = 103,             //烈火身
    Hemorrhage = 104,           //血风击
    CrescentSlash = 105,        //火镰狂舞 - 原:月华乱舞
    MoonMist = 106,             //月影雾
    CatTongue = 107,            //MAO
    FlashDash2 = 108,           //拔刀术-秘籍
    MoonMist2 = 109,            //月影雾-秘籍

    //Archer
    Focus = 121,                //基本箭法 - 原:必中闪
    StraightShot = 122,         //落日箭法 - 原:天日闪
    DoubleShot = 123,           //连珠箭法 - 原:无我闪
    ExplosiveTrap = 124,        //烈火陷阱 - 原:爆阱
    DelayedExplosion = 125,     //爆裂箭法 - 原:爆闪
    Meditation = 126,
    BackStep = 127,             //风弹步
    ElementalShot = 128,        //冲击箭法 - 原:万斤闪
    Concentration = 129, 
    Stonetrap = 130,            //地柱钉
    ElementalBarrier = 131,     //易筋经
    SummonVampire = 132,        //吸血蜘蛛
    VampireShot = 133,          //噬血箭法 - 原:吸血地闪
    SummonToad = 134,           //召唤蛤蟆 - 原:痹魔阱
    PoisonShot = 135,           //剧毒箭法 - 原:毒魔闪 
    CrippleShot = 136,          //邪爆闪
    SummonSnakes = 137,         //召唤图腾 - 原:蛇柱阱
    NapalmShot = 138,           //火龙箭法 - 原:血龙闪
    OneWithNature = 139,
    BindingShot = 140,
    MentalState = 141,
    ElementalBarrier1 = 142,    // 金刚术
    DelayedExplosion2 = 143,    // 爆闪-秘笈
    NapalmShot2 = 144,          // 血龙闪-秘笈

    // Monk
    JiBenGunFa = 161,
    LuoHanGunFa = 162,          // 刺杀
    JinGangGunFa = 163,         // 半月
    DaMoGunFa = 164,            // 类似烈火
    XiangLongGunFa = 165,
    Taunt = 166,                // 嘲讽
    TianLeiZhen = 167,          // 天雷阵
    ShiBuYiSha = 168,           // 天雷一击 
    LuoHanZhen = 169,           // 罗汉阵

    //Custom
    Blink = 151,
    Portal = 152,
    BattleCry = 153,
    Strong = 154, 

    //Map Events
    DigOutZombie = 200,
    Rubble = 201,
    MapLightning = 202,
    MapLava = 203,
    MapQuake1 = 204,
    MapQuake2 = 205,
    MapFire = 206,
    BloodBoom = 207,
    SandStorm = 208,
    StunderBall = 209,
    KunLun5 = 210,
    KunLun5Shield = 211,
    KunLun11 = 212,
    KunLun12 = 213,
    KunLun14 = 214,
    KunLun141 = 215,
    DogYoLinPosionCloud = 216,
    DogYoLinPosionRain = 217,
    DogYoLin6 = 218,
    MoreaKing = 219,
}

public enum SpellEffect : byte
{
    None,
    FatalSword,
    Teleport,
    Healing,
    RedMoonEvil,
    TwinDrakeBlade,
    MagicShieldUp,
    MagicShieldDown,
    GreatFoxSpirit,
    Entrapment,
    Reflect,
    Critical,
    Mine,
    ElementalBarrierUp,
    ElementalBarrierDown,
    DelayedExplosion,
    MPEater,
    Hemorrhage,
    Bleeding,
    AwakeningSuccess,
    AwakeningFail,
    AwakeningMiss,
    AwakeningHit,
    StormEscape,
    TurtleKing,
    Behemoth,
    Stunned,
    IcePillar,
    TianLeiZhen,
    ElementalBarrierUp1,
    ElementalBarrierDown1,

    // edens
    CrystalBeastSpin,
    CrystalBeastExplosion,
    CrystalBeastSplash,
    CrystalBeastBlast,
    GFBHit,
    PsnHit,
    SFBHit,
    Shocking,
    UEnhance,
    TDBEff0,
    TDBEff1,
    Cake,
    CircleEff,
    PurpleBlastEff,
    BubbleEff,
    ExplosionEff,
    RainbowEff,
    GoldenBubble,
    PurpleExplosion,
    GreenExplosion,
    Swirling,
    EyeEff,
    PhoenixEff,
    ElectricExplosion,
    TrapEff,
    CastingBlizzard,
    BlizzardCast,
    CHM,
    MutantWeb,
    MutantWebHold,
    FlyingStatueCast,
    FlyingStatuePurple,
    FlyingStatueGreen,
    KingGuardHold,
    KingGuardSmash,
    DragonFlamesEffect,
    BrokenSoulCutEffect,
    ThunderClapEffect,
    LastJudgementEffect,
    ChopChopStarEffect,
    SoulEaterSwampEffect,
    HandOfGodEffect,
    LavaBurning,
    RestlessJarSpawn,
    RestlessJarSpawnOnPlayer,
    MoonMist,
    FalconShield,
    BearFlameSpin,
    StoningStatusFire,

    CatBigBossThunder,
    CatBigBossShield,
    TreeQueen,
    KillerPlant,
    HornedCommander,
    SnowWolfKing,
    SecretQueen,
    CatShaman,

    HumUpEffect,//stupple
    CreeperPlant,
    AvengerPlant,
    ClawBeast,
    Hydrax,
    Basiloid,
    FloatingRock,
    FrozenKnight,
    IcePhantom,
    BlackTortoise,
    DragonWarrior,
    DragonArcher,
    FrozenMiner,
    DarkWraith,
    SecretWarrior3,
    SecretWarrior4,
    SecretKnight,
    SecretWarrior8,
    SecretJudge,
    HornedMage,
    MoreaShield1Up,
    MoreaShield1Down,
    MoreaShield2Up,
    MoreaShield2Down,
    MoreaShield3Up,
    MoreaShield3Down,
    FrozenAxeman,
    FrozenMagician,
    SnowWarrior,
    SnowAssassin,
    SnowMouse,
    SecretWarrior2,
    SecretWizard,
    ShiBuYiSha,
    HealingCircle,
    HealingCircle2,
    KunLun1,
    KunLun2,
    KunLun3,
    KunLun5,
    IceCrystalSoldier,
    SecretPage4,
    KunLun11,
    DogYoLin7,
    ExplosiveTrap,
    Bear,
}

public enum BuffType : byte
{
    None = 0,

    //magics
    TemporalFlux,
    Hiding,
    Haste,
    SwiftFeet,
    Fury,
    SoulShield,
    BlessedArmour,
    LightBody,
    UltimateEnhancer,
    ProtectionField,
    Rage,
    Curse,
    MoonLight,
    DarkBody,
    Concentration,
    VampireShot,
    PoisonShot,
    CounterAttack,
    MentalState,
    EnergyShield,
    MagicBooster,
    PetEnhancer,
    ImmortalSkin,
    MagicShield,
    HumUp, //stupple
    CounterAttack1,
    ProtectionField1,
    Bisul,
    MoonMist,
    Healing2,
    HealingCircle2,
    BigFireBall,
    StormEscape,

    //special
    GameMaster = 100,
    General,
    Exp,
    Drop,
    Gold,
    BagWeight,
    //Transform,
    RelationshipEXP = 107,
    Mentee,
    Mentor,
    Guild,
    Prison,
    Rested,
    Luck,

    //stats
    Impact = 200,
    Magic,
    Taoist,
    Storm,
    HealthAid,
    ManaAid,
    Defence,
    MagicDefence,
    WonderDrug,
    Knapsack,

    // New
    DamageHalo,
    Exp1,
    KunLun8,
    MoreaBlood,
    MoreaBlood2, // bufficon92
    MoreaBlood3,
    MoreaBlood8,
    SecretKnight,
    KunLun14,
    DogYoLin5,
    DogYoLin6,
    DogYoLin7,
    DogYoLinShield,
}

public enum DefenceType : byte
{
    ACAgility,
    AC,
    MACAgility,
    MAC,
    Agility,
    Repulsion,
    None
}

public enum ServerPacketIds : short
{
    ServerList,
    Connected,
    ClientVersion,
    Disconnect,
    KeepAlive,
    NewAccount,
    ChangePassword,
    ChangePasswordBanned,
    Login,
    LoginBanned,
    LoginSuccess,
    NewCharacter,
    NewCharacterSuccess,
    DeleteCharacter,
    DeleteCharacterSuccess,
    StartGame,
    StartGameBanned,
    StartGameDelay,
    MapInformation,
    UserInformation,
    UserLocation,
    ObjectPlayer,
    ObjectRemove,
    ObjectTurn,
    ObjectWalk,
    ObjectRun,
    Chat,
    ObjectChat,
    NewItemInfo,
    MoveItem,
    EquipItem,
    MergeItem,
    RemoveItem,
    RemoveSlotItem,
    TakeBackItem,
    StoreItem,
    SplitItem,
    SplitItem1,
    DepositRefineItem,
    RetrieveRefineItem,
    RefineCancel,
    RefineItem,
    DepositTradeItem,
    RetrieveTradeItem,
    UseItem,
    DropItem,
    PlayerUpdate,
    PlayerInspect,
    LogOutSuccess,
    LogOutFailed,
    TimeOfDay,
    ChangeAMode,
    ChangePMode,
    ObjectItem,
    ObjectGold,
    GainedItem,
    GainedGold,
    LoseGold,
    GainedCredit,
    LoseCredit,
    ObjectMonster,
    ObjectAttack,
    Struck,
    ObjectStruck,
    DamageIndicator,
    DuraChanged,
    HealthChanged,
    DeleteItem,
    Death,
    ObjectDied,
    ColourChanged,
    ObjectColourChanged,
    ObjectGuildNameChanged,
    GainExperience,
    LevelChanged,
    ObjectLeveled,
    ObjectHarvest,
    ObjectHarvested,
    ObjectNpc,
    NPCResponse,
    ObjectHide,
    ObjectShow,
    Poisoned,
    ObjectPoisoned,
    MapChanged,
    ObjectTeleportOut,
    ObjectTeleportIn,
    TeleportIn,
    NPCGoods,
    NPCSell,
    NPCRepair,
    NPCSRepair,
    NPCRefine,
    NPCCheckRefine,
    NPCCollectRefine,
    NPCReplaceWedRing,
    NPCStorage,
    SellItem,
    CraftItem,
    RepairItem,
    ItemRepaired,
    NewMagic,
    RemoveMagic,
    MagicLeveled,
    Magic,
    MagicDelay,
    MagicCast,
    ObjectMagic,
    ObjectEffect,
    RangeAttack,
    Pushed,
    ObjectPushed,
    ObjectName,
    UserStorage,
    SwitchGroup,
    DeleteGroup,
    DeleteMember,
    GroupInvite,
    AddMember,
    Revived,
    ObjectRevived,
    SpellToggle,
    ObjectHealth,
    MapEffect,
    ObjectRangeAttack,
    AddBuff,
    RemoveBuff,
    ObjectHidden,
    RefreshItem,
    ObjectSpell,
    UserDash,
    ObjectDash,
    UserDashFail,
    ObjectDashFail,
    NPCConsign,
    NPCMarket,
    NPCMarketPage,
    ConsignItem,
    MarketFail,
    MarketSuccess,
    ObjectSitDown,
    InTrapRock,
    BaseStatsInfo,
    UserName,
    ChatItemStats,
    GuildNoticeChange,
    GuildMemberChange,
    GuildStatus,
    GuildInvite,
    GuildExpGain,
    GuildNameRequest,
    GuildStorageGoldChange,
    GuildStorageItemChange,
    GuildStorageList,
    GuildRequestWar,
    DefaultNPC,
    NPCUpdate,
    NPCImageUpdate,
    MarriageRequest,
    DivorceRequest,
    MentorRequest,
    TradeRequest,
    TradeAccept,
    TradeGold,
    TradeItem,
    TradeConfirm,
    TradeCancel,
    MountUpdate,
    EquipSlotItem,
    FishingUpdate,
    ChangeQuest,
    CompleteQuest,
    ShareQuest,
    NewQuestInfo,
    GainedQuestItem,
    DeleteQuestItem,
    CancelReincarnation,
    RequestReincarnation,
    UserBackStep,
    ObjectBackStep,
    UserDashAttack,
    ObjectDashAttack,
    UserAttackMove,
    CombineItem,
    ItemUpgraded,
    SetConcentration,
    SetObjectConcentration,
    SetElemental,
    SetObjectElemental,
    RemoveDelayedExplosion,
    ObjectDeco,
    ObjectSneaking,
    ObjectLevelEffects,
    SetBindingShot,
    SendOutputMessage,

    NPCAwakening,
    NPCDisassemble,
    NPCDowngrade,
    NPCReset,
    AwakeningNeedMaterials,
    AwakeningLockedItem,
    Awakening,

    ReceiveMail,
    MailLockedItem,
    MailSendRequest,
    MailSent,
    ParcelCollected,
    MailCost,
	ResizeInventory,
    ResizeStorage,
    NewIntelligentCreature,
    UpdateIntelligentCreatureList,
    IntelligentCreatureEnableRename,
    IntelligentCreaturePickup,
    NPCPearlGoods,

    TransformUpdate,
    FriendUpdate,
    LoverUpdate,
    MentorUpdate,
    GuildBuffList,
    NPCRequestInput,
    GameShopInfo,
    GameShopStock,
    Rankings,
    Opendoor,

    GetRentedItems,
    ItemRentalRequest,
    ItemRentalFee,
    ItemRentalPeriod,
    DepositRentalItem,
    RetrieveRentalItem,
    UpdateRentalItem,
    CancelItemRental,
    ItemRentalLock,
    ItemRentalPartnerLock,
    CanConfirmItemRental,
    ConfirmItemRental,
    NewRecipeInfo,

    OnlinePlayers,
    RearrangeInventory,
    EERemoveRune,
    EERuneSocketResult,
    EEOpenSocketDialog,
    DownloadFile,

    HumUpPlayer,//stupple
    ChangeNameRequest,
}

public enum ClientPacketIds : short
{
    ServerList,
    ClientVersion,
    Disconnect,
    KeepAlive,
    NewAccount,
    ChangePassword,
    Login,
    NewCharacter,
    DeleteCharacter,
    StartGame,
    LogOut,
    Turn,
    Walk,
    Run,
    Chat,
    MoveItem,
    StoreItem,
    TakeBackItem,
    MergeItem,
    EquipItem,
    RemoveItem,
    RemoveSlotItem,
    SplitItem,
    UseItem,
    DropItem,
    DepositRefineItem,
    RetrieveRefineItem,
    RefineCancel,
    RefineItem,
    CheckRefine,
    ReplaceWedRing,
    DepositTradeItem,
    RetrieveTradeItem,
    DropGold,
    PickUp,
    Inspect,
    ChangeAMode,
    ChangePMode,
    ChangeTrade,
    Attack,
    RangeAttack,
    Harvest,
    CallNPC,
    TalkMonsterNPC,
    BuyItem,
    SellItem,
    CraftItem,
    RepairItem,
    BuyItemBack,
    SRepairItem,
    MagicKey,
    Magic,
    SwitchGroup,
    AddMember,
    DellMember,
    GroupInvite,
    TownRevive,
    SpellToggle,
    ConsignItem,
    MarketSearch,
    MarketRefresh,
    MarketPage,
    MarketBuy,
    MarketGetBack,
    RequestUserName,
    RequestChatItem,
    EditGuildMember,
    EditGuildNotice,
    GuildInvite,
    GuildNameReturn,
    RequestGuildInfo,
    GuildStorageGoldChange,
    GuildStorageItemChange,
    GuildWarReturn,
    MarriageRequest,
    MarriageReply,
    ChangeMarriage,
    DivorceRequest,
    DivorceReply,
    AddMentor,
    MentorReply,
    AllowMentor,
    CancelMentor,
    TradeRequest,
    TradeReply,
    TradeGold,
    TradeConfirm,
    TradeCancel,
    EquipSlotItem,
    FishingCast,
    FishingChangeAutocast,
    AcceptQuest,
    FinishQuest,
    AbandonQuest,
    ShareQuest,

    AcceptReincarnation,
    CancelReincarnation,
    CombineItem,

    SetConcentration,
    AwakeningNeedMaterials,
    AwakeningLockedItem,
    Awakening,
    DisassembleItem,
    DowngradeAwakening,
    ResetAddedItem,

    SendMail,
    ReadMail,
    CollectParcel,
    DeleteMail,
    LockMail,
    MailLockedItem,
    MailCost,

    UpdateIntelligentCreature,
    IntelligentCreaturePickup,

    AddFriend,
    RemoveFriend,
    RefreshFriends,
    AddMemo,
    GuildBuffUpdate,
    NPCConfirmInput,
    GameshopBuy,

    ReportIssue,
    GetRanking,
    Opendoor,

    GetRentedItems,
    ItemRentalRequest,
    ItemRentalFee,
    ItemRentalPeriod,
    DepositRentalItem,
    RetrieveRentalItem,
    CancelItemRental,
    ItemRentalLockFee,
    ItemRentalLockItem,
    ConfirmItemRental,

    RearrangeInventory,
    EESocketItem,
    EERemoveRune,

    DownloadFile,

    ChangeNameReturn
}

public enum ConquestType : byte
{
    Request = 0,
    Auto = 1,
    Forced = 2,
}

public enum ConquestGame : byte
{
    CapturePalace = 0,
    KingOfHill = 1,         // 
    Random = 2,             // 随机
    Classic = 3,            // 经典
    ControlPoints = 4       // 控制点
}

public class InIReader
{
    #region Fields
    private readonly List<string> _contents;
    private readonly string _fileName;
    #endregion

    #region Constructor
    public InIReader(string fileName)
    {
        _fileName = fileName;

        _contents = new List<string>();
        try
        {
            if (File.Exists(_fileName))
                _contents.AddRange(File.ReadAllLines(_fileName));
        }
        catch
        {
        }
    }
    #endregion

    #region Functions

    public List<string> GetSections()
    {
        List<string> sections = new List<string>();
        for (int a = 0; a < _contents.Count; a++)
            if (_contents[a].StartsWith("[") && _contents[a].EndsWith("]"))
                sections.Add(_contents[a].Substring(1, _contents[a].Length-2));

        return sections;
    }

    public string FindValue(string section, string key)
    {
        for (int a = 0; a < _contents.Count; a++)
            if (String.CompareOrdinal(_contents[a], "[" + section + "]") == 0)
                for (int b = a + 1; b < _contents.Count; b++)
                    if (String.CompareOrdinal(_contents[b].Split('=')[0], key) == 0)
                        return _contents[b].Split('=')[1];
                    else if (_contents[b].StartsWith("[") && _contents[b].EndsWith("]"))
                        return null;
        return null;
    }

    public List<string> getContents()
    {
        return _contents;
    }

    private int FindIndex(string section, string key)
    {
        for (int a = 0; a < _contents.Count; a++)
            if (String.CompareOrdinal(_contents[a], "[" + section + "]") == 0)
                for (int b = a + 1; b < _contents.Count; b++)
                    if (String.CompareOrdinal(_contents[b].Split('=')[0], key) == 0)
                        return b;
                    else if (_contents[b].StartsWith("[") && _contents[b].EndsWith("]"))
                    {
                        _contents.Insert(b - 1, key + "=");
                        return b - 1;
                    }
                    else if (_contents.Count - 1 == b)
                    {
                        _contents.Add(key + "=");
                        return _contents.Count - 1;
                    }
        if (_contents.Count > 0)
            _contents.Add("");

        _contents.Add("[" + section + "]");
        _contents.Add(key + "=");
        return _contents.Count - 1;
    }

    public void Save()
    {
        try
        {
             File.WriteAllLines(_fileName, _contents);
        }
        catch
        {
        }
    }
    #endregion

    #region Read
    public bool ReadBoolean(string section, string key, bool Default)
    {
        bool result;

        if (!bool.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public byte ReadByte(string section, string key, byte Default)
    {
        byte result;

        if (!byte.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }


        return result;
    }

    public sbyte ReadSByte(string section, string key, sbyte Default)
    {
        sbyte result;

        if (!sbyte.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }


        return result;
    }

    public ushort ReadUInt16(string section, string key, ushort Default)
    {
        ushort result;

        if (!ushort.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }


        return result;
    }

    public short ReadInt16(string section, string key, short Default)
    {
        short result;

        if (!short.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }


        return result;
    }

    public uint ReadUInt32(string section, string key, uint Default)
    {
        uint result;

        if (!uint.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public int ReadInt32(string section, string key, int Default)
    {
        int result;

        if (!int.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public ulong ReadUInt64(string section, string key, ulong Default)
    {
        ulong result;

        if (!ulong.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public long ReadInt64(string section, string key, long Default)
    {
        long result;

        if (!long.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }


        return result;
    }

    public float ReadSingle(string section, string key, float Default)
    {
        float result;

        if (!float.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public double ReadDouble(string section, string key, double Default)
    {
        double result;

        if (!double.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public decimal ReadDecimal(string section, string key, decimal Default)
    {
        decimal result;

        if (!decimal.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public string ReadString(string section, string key, string Default)
    {
        string result = FindValue(section, key);

        if (string.IsNullOrEmpty(result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public char ReadChar(string section, string key, char Default)
    {
        char result;

        if (!char.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }

    public Point ReadPoint(string section, string key, Point Default)
    {
        string temp = FindValue(section, key);
        int tempX, tempY;
        if (temp == null || !int.TryParse(temp.Split(',')[0], out tempX))
        {
            Write(section, key, Default);
            return Default;
        }
        if (!int.TryParse(temp.Split(',')[1], out tempY))
        {
            Write(section, key, Default);
            return Default;
        }

        return new Point(tempX, tempY);
    }

    public Size ReadSize(string section, string key, Size Default)
    {
        string temp = FindValue(section, key);
        int tempX, tempY;
        if (!int.TryParse(temp.Split(',')[0], out tempX))
        {
            Write(section, key, Default);
            return Default;
        }
        if (!int.TryParse(temp.Split(',')[1], out tempY))
        {
            Write(section, key, Default);
            return Default;
        }

        return new Size(tempX, tempY);
    }

    public TimeSpan ReadTimeSpan(string section, string key, TimeSpan Default)
    {
        TimeSpan result;

        if (!TimeSpan.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }


        return result;
    }

    public float ReadFloat(string section, string key, float Default)
    {
        float result;

        if (!float.TryParse(FindValue(section, key), out result))
        {
            result = Default;
            Write(section, key, Default);
        }

        return result;
    }
    #endregion

    #region Write
    public void Write(string section, string key, bool value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, byte value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, sbyte value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, ushort value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, short value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, uint value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, int value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, ulong value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, long value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, float value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, double value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, decimal value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, string value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, char value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    public void Write(string section, string key, Point value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value.X + "," + value.Y;
    }

    public void Write(string section, string key, Size value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value.Width + "," + value.Height;
    }

    public void Write(string section, string key, TimeSpan value)
    {
        _contents[FindIndex(section, key)] = key + "=" + value;
    }

    #endregion
}

public static class Globals
{
    public const int
        MinAccountIDLength = 3,
        MaxAccountIDLength = 15,

        MinPasswordLength = 5,
        MaxPasswordLength = 15,

        MinCharacterNameLength = 3,
        MaxCharacterNameLength = 15,
        MaxCharacterCount = 6,

        MaxChatLength = 80,

        MinGuildNameLength = 3,
        MaxGuildNameLength = 15,

        MaxGroup = 15,
        
        MaxAttackRange = 9,

        MaxDragonLevel = 13,

        FlagIndexCount = 1999,

        MaxConcurrentQuests = 20,

        LogDelay = 10000,

        DataRange = 16;//Was 24

    public static float Commission = 0.05F;

    public const uint SearchDelay = 500,
                      ConsignmentLength = 7,
                      ConsignmentCost = 1000,
                      MinConsignment = 10,
                      MaxConsignment = 50000000;

    public const uint BlizzardCastTime = 500;
    public const uint BlizzardGuideTime = 4500;

}

public static class Functions
{
    public static bool CompareBytes(byte[] a, byte[] b)
    {
        if (a == b) return true;

        if (a == null || b == null || a.Length != b.Length) return false;

        for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;

        return true;
    }

    public static bool TryParse(string s, out Point temp)
    {
        temp = Point.Empty;
        int tempX, tempY;
        if (String.IsNullOrWhiteSpace(s)) return false;

        string[] data = s.Split(',');
        if (data.Length <= 1) return false;

        if (!Int32.TryParse(data[0], out tempX))
            return false;

        if (!Int32.TryParse(data[1], out tempY))
            return false;

        temp = new Point(tempX, tempY);
        return true;
    }
    public static Point Subtract(this Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y);
    }
    public static Point Subtract(this Point p1, int x, int y)
    {
        return new Point(p1.X - x, p1.Y - y);
    }
    public static Point Add(this Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }
    public static Point Add(this Point p1, int x, int y)
    {
        return new Point(p1.X + x, p1.Y + y);
    }
    public static string PointToString(Point p)
    {
        return String.Format("{0}, {1}", p.X, p.Y);
    }
    public static bool InRange(Point a, Point b, int i)
    {
        return Math.Abs(a.X - b.X) <= i && Math.Abs(a.Y - b.Y) <= i;
    }

    public static bool FacingEachOther(MirDirection dirA, Point pointA, MirDirection dirB, Point pointB)
    {
        if (dirA == DirectionFromPoint(pointA, pointB) && dirB == DirectionFromPoint(pointB, pointA))
        {
            return true;
        }

        return false;
    }

    public static string PrintTimeSpanFromSeconds(double secs, bool accurate = true)
    {
        TimeSpan t = TimeSpan.FromSeconds(secs);
        string answer;
        if (t.TotalMinutes < 1.0)
        {
            answer = string.Format("{0}s", t.Seconds);
        }
        else if (t.TotalHours < 1.0)
        {
            answer = accurate ? string.Format("{0}m {1:D2}s", t.Minutes, t.Seconds) : string.Format("{0}m", t.Minutes);
        }
        else if (t.TotalDays < 1.0)
        {
            answer = accurate ? string.Format("{0}h {1:D2}m {2:D2}s", (int)t.Hours, t.Minutes, t.Seconds) : string.Format("{0}h {1:D2}m", (int)t.TotalHours, t.Minutes);
        }
        else // more than 1 day
        {
            answer = accurate ? string.Format("{0}d {1:D2}h {2:D2}m {3:D2}s", (int)t.Days, (int)t.Hours, t.Minutes, t.Seconds) : string.Format("{0}d {1}h {2:D2}m", (int)t.TotalDays, (int)t.Hours, t.Minutes);
        }

        return answer;
    }

    public static string PrintTimeSpanFromMilliSeconds(double milliSeconds)
    {
        TimeSpan t = TimeSpan.FromMilliseconds(milliSeconds);
        string answer;
        if (t.TotalMinutes < 1.0)
        {
            answer = string.Format("{0}.{1}s", t.Seconds, (decimal)(t.Milliseconds / 100));
        }
        else if (t.TotalHours < 1.0)
        {
            answer = string.Format("{0}m {1:D2}s", t.TotalMinutes, t.Seconds);
        }
        else if (t.TotalDays < 1.0)
        {
            answer = string.Format("{0}h {1:D2}m {2:D2}s", (int)t.TotalHours, t.Minutes, t.Seconds);
        }
        else
        {
            answer = string.Format("{0}d {1}h {2:D2}m {3:D2}s", (int)t.Days, (int)t.Hours, t.Minutes, t.Seconds);
        }

        return answer;
    }

    public static MirDirection PreviousDir(MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                return MirDirection.UpLeft;
            case MirDirection.UpRight:
                return MirDirection.Up;
            case MirDirection.Right:
                return MirDirection.UpRight;
            case MirDirection.DownRight:
                return MirDirection.Right;
            case MirDirection.Down:
                return MirDirection.DownRight;
            case MirDirection.DownLeft:
                return MirDirection.Down;
            case MirDirection.Left:
                return MirDirection.DownLeft;
            case MirDirection.UpLeft:
                return MirDirection.Left;
            default: return d;
        }
    }
    public static MirDirection NextDir(MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                return MirDirection.UpRight;
            case MirDirection.UpRight:
                return MirDirection.Right;
            case MirDirection.Right:
                return MirDirection.DownRight;
            case MirDirection.DownRight:
                return MirDirection.Down;
            case MirDirection.Down:
                return MirDirection.DownLeft;
            case MirDirection.DownLeft:
                return MirDirection.Left;
            case MirDirection.Left:
                return MirDirection.UpLeft;
            case MirDirection.UpLeft:
                return MirDirection.Up;
            default: return d;
        }
    }

    public static bool InTheLine(Point source, Point dest, int range)
    {
        if (source.X == dest.X)
            return Math.Abs(source.Y - dest.Y) <= range;
        else if (source.Y == dest.Y)
            return Math.Abs(source.X - dest.X) <= range;
        else if (Math.Abs(source.X - dest.X) == Math.Abs(source.Y - dest.Y))
            return Math.Abs(source.X - dest.X) <= range;

        return false;
    }

    public static MirDirection DirectionFromPoint(Point source, Point dest)
    {
        if (source.X < dest.X)
        {
            if (source.Y < dest.Y)
                return MirDirection.DownRight;
            if (source.Y > dest.Y)
                return MirDirection.UpRight;
            return MirDirection.Right;
        }

        if (source.X > dest.X)
        {
            if (source.Y < dest.Y)
                return MirDirection.DownLeft;
            if (source.Y > dest.Y)
                return MirDirection.UpLeft;
            return MirDirection.Left;
        }

        return source.Y < dest.Y ? MirDirection.Down : MirDirection.Up;
    }



    public static Size Add(this Size p1, Size p2)
    {
        return new Size(p1.Width + p2.Width, p1.Height + p2.Height);
    }
    public static Size Add(this Size p1, int width, int height)
    {
        return new Size(p1.Width + width, p1.Height + height);
    }

    public static Point PointMove(Point p, MirDirection d, int i)
    {
        switch (d)
        {
            case MirDirection.Up:
                p.Offset(0, -i);
                break;
            case MirDirection.UpRight:
                p.Offset(i, -i);
                break;
            case MirDirection.Right:
                p.Offset(i, 0);
                break;
            case MirDirection.DownRight:
                p.Offset(i, i);
                break;
            case MirDirection.Down:
                p.Offset(0, i);
                break;
            case MirDirection.DownLeft:
                p.Offset(-i, i);
                break;
            case MirDirection.Left:
                p.Offset(-i, 0);
                break;
            case MirDirection.UpLeft:
                p.Offset(-i, -i);
                break;
        }
        return p;
    }
    public static Point Left(Point p, MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                p.Offset(-1, 0);
                break;
            case MirDirection.UpRight:
                p.Offset(-1, -1);
                break;
            case MirDirection.Right:
                p.Offset(0, -1);
                break;
            case MirDirection.DownRight:
                p.Offset(1, -1);
                break;
            case MirDirection.Down:
                p.Offset(1, 0);
                break;
            case MirDirection.DownLeft:
                p.Offset(1, 1);
                break;
            case MirDirection.Left:
                p.Offset(0, 1);
                break;
            case MirDirection.UpLeft:
                p.Offset(-1, 1);
                break;
        }
        return p;
    }

    public static Point Right(Point p, MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                p.Offset(1, 0);
                break;
            case MirDirection.UpRight:
                p.Offset(1, 1);
                break;
            case MirDirection.Right:
                p.Offset(0, 1);
                break;
            case MirDirection.DownRight:
                p.Offset(-1, 1);
                break;
            case MirDirection.Down:
                p.Offset(-1, 0);
                break;
            case MirDirection.DownLeft:
                p.Offset(-1,-1);
                break;
            case MirDirection.Left:
                p.Offset(0, -1);
                break;
            case MirDirection.UpLeft:
                p.Offset(1, -1);
                break;
        }
        return p;
    }

    public static int MaxDistance(Point p1, Point p2)
    {
        return Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

    }

    public static MirDirection ReverseDirection(MirDirection dir)
    {
        switch (dir)
        {
            case MirDirection.Up:
                return MirDirection.Down;
            case MirDirection.UpRight:
                return MirDirection.DownLeft;
            case MirDirection.Right:
                return MirDirection.Left;
            case MirDirection.DownRight:
                return MirDirection.UpLeft;
            case MirDirection.Down:
                return MirDirection.Up;
            case MirDirection.DownLeft:
                return MirDirection.UpRight;
            case MirDirection.Left:
                return MirDirection.Right;
            case MirDirection.UpLeft:
                return MirDirection.DownRight;
            default:
                return dir;
        }
    }
    public static ItemInfo GetRealItem(ItemInfo Origin, ushort Level, MirClass job, Dictionary<int, ItemInfo> ItemMap)
    {
        if (Origin.ClassBased && Origin.LevelBased)
            return GetClassAndLevelBasedItem(Origin, job, Level, ItemMap);
        if (Origin.ClassBased)
            return GetClassBasedItem(Origin, job, ItemMap);
        if (Origin.LevelBased)
            return GetLevelBasedItem(Origin, Level, ItemMap);
        return Origin;
    }
    public static ItemInfo GetLevelBasedItem(ItemInfo Origin, ushort level, Dictionary<int, ItemInfo> ItemMap)
    {
        ItemInfo output = Origin;
        foreach (var item in ItemMap)
        {
            ItemInfo info = item.Value; ;
            if (info.Name.StartsWith(Origin.Name))
                if ((info.RequiredType == RequiredType.Level) && (info.RequiredAmount <= level) && (output.RequiredAmount < info.RequiredAmount) && (Origin.RequiredGender == info.RequiredGender))
                    output = info;
        }
        return output;
    }
    public static ItemInfo GetClassBasedItem(ItemInfo Origin, MirClass job, Dictionary<int, ItemInfo> ItemMap)
    {
        foreach (var item in ItemMap)
        {
            ItemInfo info = item.Value;
            if (info.Name.StartsWith(Origin.Name))
                if (((byte)info.RequiredClass == (1 << (byte)job)) && (Origin.RequiredGender == info.RequiredGender))
                    return info;
        }
        return Origin;
    }

    public static ItemInfo GetClassAndLevelBasedItem(ItemInfo Origin, MirClass job, ushort level, Dictionary<int, ItemInfo> ItemMap)
    {
        ItemInfo output = Origin;
        foreach(var item in ItemMap)
        {
            ItemInfo info = item.Value;
            if (info.Name.StartsWith(Origin.Name))
                if ((byte)info.RequiredClass == (1 << (byte)job))
                    if ((info.RequiredType == RequiredType.Level) && (info.RequiredAmount <= level) && (output.RequiredAmount <= info.RequiredAmount) && (Origin.RequiredGender == info.RequiredGender))
                        output = info;
        }
        return output;
    }

    public static bool CanWearItem(EquipmentSlot slot, ItemType itemType)
    {
        switch (slot)
        {
            case EquipmentSlot.Weapon:
                if (itemType != ItemType.Weapon)
                    return false;
                break;
            case EquipmentSlot.Armour:
                if (itemType != ItemType.Armour)
                    return false;
                break;
            case EquipmentSlot.Helmet:
                if (itemType != ItemType.Helmet)
                    return false;
                break;
            case EquipmentSlot.Torch:
                if (itemType != ItemType.Torch)
                    return false;
                break;
            case EquipmentSlot.Necklace:
                if (itemType != ItemType.Necklace)
                    return false;
                break;
            case EquipmentSlot.BraceletL:
                if (itemType != ItemType.Bracelet)
                    return false;
                break;
            case EquipmentSlot.BraceletR:
                if (itemType != ItemType.Bracelet && itemType != ItemType.Amulet)
                    return false;
                break;
            case EquipmentSlot.RingL:
            case EquipmentSlot.RingR:
                if (itemType != ItemType.Ring)
                    return false;
                break;
            case EquipmentSlot.Amulet:
            case EquipmentSlot.AmuletUp:
                if (itemType != ItemType.Amulet)// || item.Info.Shape == 0
                    return false;
                break;
            case EquipmentSlot.Boots:
                if (itemType != ItemType.Boots)
                    return false;
                break;
            case EquipmentSlot.Belt:
                if (itemType != ItemType.Belt)
                    return false;
                break;
            case EquipmentSlot.Stone:
                if (itemType != ItemType.Stone)
                    return false;
                break;
            case EquipmentSlot.Mount:
                if (itemType != ItemType.Mount)
                    return false;
                break;
            case EquipmentSlot.Transform:
                if (itemType != ItemType.Transform)
                    return false;
                break;
            default:
                return false;
        }

        return true;
    }

    public static string StringOverLines(string line, int maxWordsPerLine, int maxLettersPerLine)
    {
        string newString = string.Empty;

        string[] words = line.Split(' ');

        int lineLength = 0;

        for (int i = 0; i < words.Length; i++)
        {
            lineLength += words[i].Length + 1;

            newString += words[i] + " ";
            if (i > 0 && i % maxWordsPerLine == 0 && lineLength > maxLettersPerLine)
            {
                lineLength = 0;
                newString += "\r\n";
            }
        }

        return newString;
    }

    public static byte[] ImageToByteArray(Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, ImageFormat.Gif);
        return ms.ToArray();
    }

    public static Image ByteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Image returnImage = Image.FromStream(ms);
        return returnImage;
    }

    public static IEnumerable<byte[]> SplitArray(byte[] value, int bufferLength)
    {
        int countOfArray = value.Length / bufferLength;
        if (value.Length % bufferLength > 0)
            countOfArray++;
        for (int i = 0; i < countOfArray; i++)
        {
            yield return value.Skip(i * bufferLength).Take(bufferLength).ToArray();
        }
    }

    public static byte[] CombineArray(List<byte[]> arrays)
    {
        byte[] rv = new byte[arrays.Sum(x => x.Length)];
        int offset = 0;
        foreach (byte[] array in arrays)
        {
            System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
            offset += array.Length;
        }
        return rv;
    }

    public static int Distance(Point p1, Point p2)
    {
        return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
    }

    public const int UpWarriorWeaponNum = 4;
    public const int UpWizardWeaponNum = 5;
    public const int UpTaoistWeaponNum = 6;
    public const int UpAssassinWeaponNum = 7;
    public const int UpArcherWeaponNum = 8;

    public static bool HasClassWeapon(MirClass Class, int weapon)
    {
        switch (weapon / 100)
        {
            default:
                return Class == MirClass.Wizard || Class == MirClass.Warrior || Class == MirClass.Taoist;
            case 1:
                return Class == MirClass.Assassin;
            case 2:
                return Class == MirClass.Archer;
            case 3: //Monk
                return Class == MirClass.Monk;

            case UpWarriorWeaponNum:
                return Class == MirClass.Warrior;

            case UpWizardWeaponNum:
                return Class == MirClass.Wizard;

            case UpTaoistWeaponNum:
                return Class == MirClass.Taoist;

            case UpAssassinWeaponNum:
                return Class == MirClass.Assassin;

            case UpArcherWeaponNum:
                return Class == MirClass.Archer;
        }
    }

    public static int CurrentSecond()
    {
        var utcNow = DateTime.UtcNow;
        var timeSpan = utcNow - new DateTime(1970, 1, 1, 0, 0, 0);
        return (int)timeSpan.TotalSeconds;
    }

}

public class SelectInfo
{
    public int Index;
    public string Name = string.Empty;
    public ushort Level;
    public MirClass Class;
    public MirGender Gender;
    public DateTime LastAccess;

    public SelectInfo()
    { }
    public SelectInfo(BinaryReader reader)
    {
        Index = reader.ReadInt32();
        Name = reader.ReadString();
        Level = reader.ReadUInt16();
        Class = (MirClass)reader.ReadByte();
        Gender = (MirGender)reader.ReadByte();
        LastAccess = DateTime.FromBinary(reader.ReadInt64());
    }
    public void Save(BinaryWriter writer)
    {
        writer.Write(Index);
        writer.Write(Name);
        writer.Write(Level);
        writer.Write((byte)Class);
        writer.Write((byte)Gender);
        writer.Write(LastAccess.ToBinary());
    }

    override
    public string ToString()
    {
        return string.Format("Index:{0} Name:{1} Level:{2} LastAccess:{3}", Index, Name, Level, LastAccess.ToString());
    }
}

public class ItemInfo
{
    public int Index;
    public string Name = string.Empty;
    public ItemType Type;
    public ItemGrade Grade;
    public RequiredType RequiredType = RequiredType.Level;
    public RequiredClass RequiredClass = RequiredClass.None;
    public RequiredGender RequiredGender = RequiredGender.None;
    public ItemSet Set;

    public short Shape;
    public byte Weight, Light, RequiredAmount;

    public ushort Image, Durability;

    public uint Price, StackSize = 1;

    public byte MinAC, MaxAC, MinMAC, MaxMAC, MinDC, MaxDC, MinMC, MaxMC, MinSC, MaxSC, Accuracy, Agility;
    public ushort HP, MP;
    public sbyte AttackSpeed, Luck;
    public byte BagWeight, HandWeight, WearWeight;

    public bool StartItem;
    public byte Effect;

    public byte Strong;
    public byte MagicResist, PoisonResist, HealthRecovery, SpellRecovery, PoisonRecovery, HPrate, MPrate;
    public byte CriticalRate, CriticalDamage;
    public bool NeedIdentify, ShowGroupPickup, GlobalDropNotify;
    public bool ClassBased;
    public bool LevelBased;
    public bool CanMine;
    public bool CanFastRun;
    public bool CanAwakening;
    public byte MaxAcRate, MaxMacRate, Holy, Freezing, PoisonAttack, HpDrainRate;
    
    public BindMode Bind = BindMode.none;
    public byte Reflect;
    public SpecialItemMode Unique = SpecialItemMode.None;
    public byte RandomStatsId;
    public RandomItemStat RandomStats;
    public string ToolTip = string.Empty;

    public KnownColor NameColor;
    public bool NeedHumUp;

    public byte MinSocket = 0, MaxSocket = 0;

    public bool IsConsumable
    {
        get { return Type == ItemType.Potion || Type == ItemType.Scroll || Type == ItemType.Food || Type == ItemType.Transform || Type == ItemType.Script; }
    }

    public string FriendlyName
    {
        get 
        {
            string temp = Name;
            temp = Regex.Replace(temp, @"\d+$", string.Empty); //hides end numbers
            temp = Regex.Replace(temp, @"\[[^]]*\]", string.Empty); //hides square brackets

            return temp;
        }
    }
    
    public ItemInfo()
    {
    }
    public ItemInfo(BinaryReader reader, int version = int.MaxValue, int Customversion = int.MaxValue)
    {
        Index = reader.ReadInt32();
        Name = reader.ReadString();
        Type = (ItemType) reader.ReadByte();
        if (version >= 40) Grade = (ItemGrade)reader.ReadByte();
        RequiredType = (RequiredType) reader.ReadByte();
        RequiredClass = (RequiredClass) reader.ReadByte();
        RequiredGender = (RequiredGender) reader.ReadByte();
        if(version >= 17) Set = (ItemSet)reader.ReadByte();

        Shape = version >= 30 ? reader.ReadInt16() : reader.ReadSByte();
        Weight = reader.ReadByte();
        Light = reader.ReadByte();
        RequiredAmount = reader.ReadByte();

        Image = reader.ReadUInt16();
        Durability = reader.ReadUInt16();

        StackSize = reader.ReadUInt32();
        Price = reader.ReadUInt32();

        MinAC = reader.ReadByte();
        MaxAC = reader.ReadByte();
        MinMAC = reader.ReadByte();
        MaxMAC = reader.ReadByte();
        MinDC = reader.ReadByte();
        MaxDC = reader.ReadByte();
        MinMC = reader.ReadByte();
        MaxMC = reader.ReadByte();
        MinSC = reader.ReadByte();
        MaxSC = reader.ReadByte();
        if (version < 25)
        {
            HP = reader.ReadByte();
            MP = reader.ReadByte();
        }
        else
        {
            HP = reader.ReadUInt16();
            MP = reader.ReadUInt16();
        }
        Accuracy = reader.ReadByte();
        Agility = reader.ReadByte();

        Luck = reader.ReadSByte();
        AttackSpeed = reader.ReadSByte();

        StartItem = reader.ReadBoolean();

        BagWeight = reader.ReadByte();
        HandWeight = reader.ReadByte();
        WearWeight = reader.ReadByte();

        if (version >= 9) Effect = reader.ReadByte();
        if (version >= 20)
        {
            Strong = reader.ReadByte();
            MagicResist = reader.ReadByte();
            PoisonResist = reader.ReadByte();
            HealthRecovery = reader.ReadByte();
            SpellRecovery = reader.ReadByte();
            PoisonRecovery = reader.ReadByte();
            HPrate = reader.ReadByte();
            MPrate = reader.ReadByte();
            CriticalRate = reader.ReadByte();
            CriticalDamage = reader.ReadByte();
            byte bools = reader.ReadByte();
            NeedIdentify = (bools & 0x01) == 0x01;
            ShowGroupPickup = (bools & 0x02) == 0x02;
            ClassBased = (bools & 0x04) == 0x04;
            LevelBased = (bools & 0x08) == 0x08;
            CanMine = (bools & 0x10) == 0x10;

            if (version >= 77)
                GlobalDropNotify = (bools & 0x20) == 0x20;

            NeedHumUp = (bools & 0x40) == 0x40;
            
            MaxAcRate = reader.ReadByte();
            MaxMacRate = reader.ReadByte();
            Holy = reader.ReadByte();
            Freezing = reader.ReadByte();
            PoisonAttack = reader.ReadByte();
            if (version < 55)
            {
                Bind = (BindMode)reader.ReadByte();
            }
            else
            {
                Bind = (BindMode)reader.ReadInt16();
            }
            
        }
        if (version >= 21)
        {
            Reflect = reader.ReadByte();
            HpDrainRate = reader.ReadByte();
            Unique = (SpecialItemMode)reader.ReadInt16();
        }
        if (version >= 24)
        {
            RandomStatsId = reader.ReadByte();
        }
        else
        {
            RandomStatsId = 255;
            if ((Type == ItemType.Weapon) || (Type == ItemType.Armour) || (Type == ItemType.Helmet) || (Type == ItemType.Necklace) || (Type == ItemType.Bracelet) || (Type == ItemType.Ring) || (Type == ItemType.Mount))
                RandomStatsId = (byte)Type;
            if ((Type == ItemType.Belt) || (Type == ItemType.Boots))
                RandomStatsId = 7;
        }

        if (version >= 40) CanFastRun = reader.ReadBoolean();

        if (version >= 41)
        {
            CanAwakening = reader.ReadBoolean();
            bool isTooltip = reader.ReadBoolean();
            if (isTooltip)
                ToolTip = reader.ReadString();
        }
        if (version < 70) //before db version 70 all specialitems had wedding rings disabled, after that it became a server option
        {
            if ((Type == ItemType.Ring) &&  (Unique != SpecialItemMode.None))
                Bind |= BindMode.NoWeddingRing;
        }
        if (version >= 78)
        {
            byte color = reader.ReadByte();
            NameColor = color == 0 ? KnownColor.White : (KnownColor)color;
        }

        if (version > 84)
        {
            MinSocket = reader.ReadByte();
            MaxSocket = reader.ReadByte();
        }
    }



    public void Save(BinaryWriter writer)
    {
        writer.Write(Index);
        writer.Write(Name);
        writer.Write((byte) Type);
        writer.Write((byte) Grade);
        writer.Write((byte) RequiredType);
        writer.Write((byte) RequiredClass);
        writer.Write((byte) RequiredGender);
        writer.Write((byte) Set);

        writer.Write(Shape);
        writer.Write(Weight);
        writer.Write(Light);
        writer.Write(RequiredAmount);     

        writer.Write(Image);
        writer.Write(Durability);

        writer.Write(StackSize);
        writer.Write(Price);

        writer.Write(MinAC);
        writer.Write(MaxAC);
        writer.Write(MinMAC);
        writer.Write(MaxMAC);
        writer.Write(MinDC);
        writer.Write(MaxDC);
        writer.Write(MinMC);
        writer.Write(MaxMC);
        writer.Write(MinSC);
        writer.Write(MaxSC);
        writer.Write(HP);
        writer.Write(MP);
        writer.Write(Accuracy);
        writer.Write(Agility);

        writer.Write(Luck);
        writer.Write(AttackSpeed);

        writer.Write(StartItem);

        writer.Write(BagWeight);
        writer.Write(HandWeight);
        writer.Write(WearWeight);

        writer.Write(Effect);
        writer.Write(Strong);
        writer.Write(MagicResist);
        writer.Write(PoisonResist);
        writer.Write(HealthRecovery);
        writer.Write(SpellRecovery);
        writer.Write(PoisonRecovery);
        writer.Write(HPrate);
        writer.Write(MPrate);
        writer.Write(CriticalRate);
        writer.Write(CriticalDamage);
        byte bools = 0;
        if (NeedIdentify) bools |= 0x01;
        if (ShowGroupPickup) bools |= 0x02;
        if (ClassBased) bools |= 0x04;
        if (LevelBased) bools |= 0x08;
        if (CanMine) bools |= 0x10;
        if (GlobalDropNotify) bools |= 0x20;
        if (NeedHumUp) bools |= 0x40;
        writer.Write(bools);
        writer.Write(MaxAcRate);
        writer.Write(MaxMacRate);
        writer.Write(Holy);
        writer.Write(Freezing);
        writer.Write(PoisonAttack);
        writer.Write((short)Bind);
        writer.Write(Reflect);
        writer.Write(HpDrainRate);
        writer.Write((short)Unique);
        writer.Write(RandomStatsId);
        writer.Write(CanFastRun);
		writer.Write(CanAwakening);
        writer.Write(ToolTip != null);
        if (ToolTip != null)
            writer.Write(ToolTip);

        writer.Write((byte)NameColor);

        writer.Write(MinSocket);
        writer.Write(MaxSocket);
    }

    public static ItemInfo FromText(string text)
    {
        string[] data = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        if (data.Length < 33) return null;

        ItemInfo info = new ItemInfo { Name = data[0] };

        if (!Enum.TryParse(data[1], out info.Type)) return null;
        if (!Enum.TryParse(data[2], out info.Grade)) return null;
        if (!Enum.TryParse(data[3], out info.RequiredType)) return null;
        if (!Enum.TryParse(data[4], out info.RequiredClass)) return null;
        if (!Enum.TryParse(data[5], out info.RequiredGender)) return null;
        if (!Enum.TryParse(data[6], out info.Set)) return null;
        if (!short.TryParse(data[7], out info.Shape)) return null;

        if (!byte.TryParse(data[8], out info.Weight)) return null;
        if (!byte.TryParse(data[9], out info.Light)) return null;
        if (!byte.TryParse(data[10], out info.RequiredAmount)) return null;

        if (!byte.TryParse(data[11], out info.MinAC)) return null;
        if (!byte.TryParse(data[12], out info.MaxAC)) return null;
        if (!byte.TryParse(data[13], out info.MinMAC)) return null;
        if (!byte.TryParse(data[14], out info.MaxMAC)) return null;
        if (!byte.TryParse(data[15], out info.MinDC)) return null;
        if (!byte.TryParse(data[16], out info.MaxDC)) return null;
        if (!byte.TryParse(data[17], out info.MinMC)) return null;
        if (!byte.TryParse(data[18], out info.MaxMC)) return null;
        if (!byte.TryParse(data[19], out info.MinSC)) return null;
        if (!byte.TryParse(data[20], out info.MaxSC)) return null;
        if (!byte.TryParse(data[21], out info.Accuracy)) return null;
        if (!byte.TryParse(data[22], out info.Agility)) return null;
        if (!ushort.TryParse(data[23], out info.HP)) return null;
        if (!ushort.TryParse(data[24], out info.MP)) return null;

        if (!sbyte.TryParse(data[25], out info.AttackSpeed)) return null;
        if (!sbyte.TryParse(data[26], out info.Luck)) return null;

        if (!byte.TryParse(data[27], out info.BagWeight)) return null;

        if (!byte.TryParse(data[28], out info.HandWeight)) return null;
        if (!byte.TryParse(data[29], out info.WearWeight)) return null;

        if (!bool.TryParse(data[30], out info.StartItem)) return null;

        if (!ushort.TryParse(data[31], out info.Image)) return null;
        if (!ushort.TryParse(data[32], out info.Durability)) return null;
        if (!uint.TryParse(data[33], out info.Price)) return null;
        if (!uint.TryParse(data[34], out info.StackSize)) return null;
        if (!byte.TryParse(data[35], out info.Effect)) return null;

        if (!byte.TryParse(data[36], out info.Strong)) return null;
        if (!byte.TryParse(data[37], out info.MagicResist)) return null;
        if (!byte.TryParse(data[38], out info.PoisonResist)) return null;
        if (!byte.TryParse(data[39], out info.HealthRecovery)) return null;
        if (!byte.TryParse(data[40], out info.SpellRecovery)) return null;
        if (!byte.TryParse(data[41], out info.PoisonRecovery)) return null;
        if (!byte.TryParse(data[42], out info.HPrate)) return null;
        if (!byte.TryParse(data[43], out info.MPrate)) return null;
        if (!byte.TryParse(data[44], out info.CriticalRate)) return null;
        if (!byte.TryParse(data[45], out info.CriticalDamage)) return null;
        if (!bool.TryParse(data[46], out info.NeedIdentify)) return null;
        if (!bool.TryParse(data[47], out info.ShowGroupPickup)) return null;
        if (!byte.TryParse(data[48], out info.MaxAcRate)) return null;
        if (!byte.TryParse(data[49], out info.MaxMacRate)) return null;
        if (!byte.TryParse(data[50], out info.Holy)) return null;
        if (!byte.TryParse(data[51], out info.Freezing)) return null;
        if (!byte.TryParse(data[52], out info.PoisonAttack)) return null;
        if (!bool.TryParse(data[53], out info.ClassBased)) return null;
        if (!bool.TryParse(data[54], out info.LevelBased)) return null;
        if (!Enum.TryParse(data[55], out info.Bind)) return null;
        if (!byte.TryParse(data[56], out info.Reflect)) return null;
        if (!byte.TryParse(data[57], out info.HpDrainRate)) return null;
        if (!Enum.TryParse(data[58], out info.Unique)) return null;
        if (!byte.TryParse(data[59], out info.RandomStatsId)) return null;
        if (!bool.TryParse(data[60], out info.CanMine)) return null;
        if (!bool.TryParse(data[61], out info.CanFastRun)) return null;
		if (!bool.TryParse(data[62], out info.CanAwakening)) return null;
        if (data[63] == "-")
            info.ToolTip = "";
        else
        {
            info.ToolTip = data[63];
            info.ToolTip = info.ToolTip.Replace("&^&", "\r\n");
        }
        if (!Enum.TryParse(data[64], out info.NameColor)) return null;
        if (!bool.TryParse(data[65], out info.GlobalDropNotify)) return null;
        if (!bool.TryParse(data[66], out info.NeedHumUp)) return null;

        return info;

    }

    public static string ToHeader()
    {
        return "Name, (byte)Type, (byte)Grade, (byte)RequiredType, (byte)RequiredClass, (byte)RequiredGender, (byte)Set, Shape, Weight, Light, RequiredAmount, MinAC, MaxAC, MinMAC, MaxMAC, MinDC, MaxDC,"
            +"MinMC, MaxMC, MinSC, MaxSC, Accuracy, Agility, HP, MP, AttackSpeed, Luck, BagWeight, HandWeight, WearWeight, StartItem, Image, Durability, Price,"
            +"StackSize, Effect, Strong, MagicResist, PoisonResist, HealthRecovery, SpellRecovery, PoisonRecovery, HPrate, MPrate, CriticalRate, CriticalDamage, NeedIdentify,"
            +"ShowGroupPickup, MaxAcRate, MaxMacRate, Holy, Freezing, PoisonAttack, ClassBased, LevelBased, (short)Bind, Reflect, HpDrainRate, (short)Unique,"
            + "RandomStatsId, CanMine, CanFastRun, CanAwakening, TransToolTip, NameColor, GlobalDropNotify, NeedHumUp";
    }

    public string ToText()
    {
        string TransToolTip = ToolTip;
        int length = TransToolTip.Length;

        if (TransToolTip == null || TransToolTip.Length == 0)
        {
            TransToolTip = "-";
        }
        else
        {
            TransToolTip = TransToolTip.Replace("\r\n", "&^&");
        }

        return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26}," +
                             "{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51}," +
                             "{52},{53},{54},{55},{56},{57},{58},{59},{60},{61},{62},{63},{64},{65},{66}",
            Name, (byte)Type, (byte)Grade, (byte)RequiredType, (byte)RequiredClass, (byte)RequiredGender, (byte)Set, Shape, Weight, Light, RequiredAmount, MinAC, MaxAC, MinMAC, MaxMAC, MinDC, MaxDC,
            MinMC, MaxMC, MinSC, MaxSC, Accuracy, Agility, HP, MP, AttackSpeed, Luck, BagWeight, HandWeight, WearWeight, StartItem, Image, Durability, Price,
            StackSize, Effect, Strong, MagicResist, PoisonResist, HealthRecovery, SpellRecovery, PoisonRecovery, HPrate, MPrate, CriticalRate, CriticalDamage, NeedIdentify,
            ShowGroupPickup, MaxAcRate, MaxMacRate, Holy, Freezing, PoisonAttack, ClassBased, LevelBased, (short)Bind, Reflect, HpDrainRate, (short)Unique,
            RandomStatsId, CanMine, CanFastRun, CanAwakening, TransToolTip, (byte)NameColor,GlobalDropNotify,NeedHumUp);
    }

    public override string ToString()
    {
        return string.Format("{0}: {1}", Index, Name);
    }

    public void CopyFrom(ItemInfo newInfo)
    {
        if (!Name.Equals(newInfo.Name))
            Name = newInfo.Name;

        if (Type != newInfo.Type)
            Type = newInfo.Type;

        if (Grade != newInfo.Grade)
            Grade = newInfo.Grade;

        if (RequiredClass != newInfo.RequiredClass)
            RequiredClass = newInfo.RequiredClass;

        if (RequiredType != newInfo.RequiredType)
            RequiredType = newInfo.RequiredType;

        if (RequiredGender != newInfo.RequiredGender)
            RequiredGender = newInfo.RequiredGender;

        if (Set != newInfo.Set)
            Set = newInfo.Set;

        Shape = newInfo.Shape;
        Weight = newInfo.Weight;
        Light = newInfo.Light;
        RequiredAmount = newInfo.RequiredAmount;

        Image = newInfo.Image;
        Durability = newInfo.Durability;

        StackSize = newInfo.StackSize;
        Price = newInfo.Price;

        MinAC = newInfo.MinAC;
        MaxAC = newInfo.MaxAC;
        MinMAC = newInfo.MinMAC;
        MaxMAC = newInfo.MaxMAC;
        MinDC = newInfo.MinDC;
        MaxDC = newInfo.MaxDC;
        MinMC = newInfo.MinMC;
        MaxMC = newInfo.MaxMC;
        
        MaxMC=newInfo.MaxMC;
        MinSC=newInfo.MinSC;
        MaxSC=newInfo.MaxSC;
        HP=newInfo.HP;
        MP=newInfo.MP;
        Accuracy=newInfo.Accuracy;
        Agility=newInfo.Agility;

        Luck=newInfo.Luck;
        AttackSpeed=newInfo.AttackSpeed;

        StartItem=newInfo.StartItem;

        BagWeight=newInfo.BagWeight;
        HandWeight=newInfo.HandWeight;
        WearWeight=newInfo.WearWeight;

        Effect=newInfo.Effect;
        Strong=newInfo.Strong;
        MagicResist=newInfo.MagicResist;
        PoisonResist=newInfo.PoisonResist;
        HealthRecovery=newInfo.HealthRecovery;
        SpellRecovery=newInfo.SpellRecovery;
        PoisonRecovery=newInfo.PoisonRecovery;
        HPrate=newInfo.HPrate;
        MPrate=newInfo.MPrate;
        CriticalRate=newInfo.CriticalRate;
        CriticalDamage=newInfo.CriticalDamage;

        NeedIdentify = newInfo.NeedIdentify;
        ShowGroupPickup = newInfo.ShowGroupPickup;
        ClassBased = newInfo.ClassBased;
        LevelBased = newInfo.LevelBased;
        CanMine = newInfo.CanMine;
        GlobalDropNotify = newInfo.GlobalDropNotify;

        MaxAcRate=newInfo.MaxAcRate;
        MaxMacRate=newInfo.MaxMacRate;
        Holy=newInfo.Holy;
        Freezing=newInfo.Freezing;
        PoisonAttack=newInfo.PoisonAttack;
        Bind=newInfo.Bind;
        Reflect=newInfo.Reflect;
        HpDrainRate=newInfo.HpDrainRate;
        Unique=newInfo.Unique;
        RandomStatsId=newInfo.RandomStatsId;
        CanFastRun=newInfo.CanFastRun;
        CanAwakening=newInfo.CanAwakening;
        ToolTip=newInfo.ToolTip;

        NameColor=newInfo.NameColor;

        MinSocket=newInfo.MinSocket;
        MaxSocket=newInfo.MaxSocket;
        NeedHumUp = newInfo.NeedHumUp;
    }

}

#region Socket System Pete107 Edens-Elite Network
public enum SocketType : byte
{
    //  % Based
    DamageReductionPvE = 0,
    DamageReductionPvP = 1,
    DamageIncreasePvE = 2,
    DamageIncreasePvP = 3,
    MeleeDamageBonus = 4,
    MagicDamageBonus = 5,
    SpiritualBonus = 6,

    //  Flat bonus
    HealthBonus = 7,
    ManaBonus = 8,

    //  % Based
    HealthRegenBonus = 9,
    ManaRegenBonus = 10,

    //  Flat bonus
    DestructionBonus = 11,
    MagicBonus = 12,
    SpiritBonus = 13,

    //  Small Accuracy Boost
    PinPoint = 30,

    //  Small Agility Boost
    Evasive = 40,

    //  Small Attack Speed & Crit bonus.
    Enrage = 50,

    //  Decent Defense.
    IronWall = 60,

    //  Speedy magician
    SpeedyMagician = 70,
    NONE = 255,
}

public class Sockets
{
    //  Depends on how many Socket Slots an item can have.
    public byte Slot;

    //  We'll have a few types of socket items
    public bool CanRemove;

    //  The Item used within the socket.
    public UserItem SocketedItem;

    public SocketType SocketItemType;

    public Sockets()
    {

    }

    public Sockets(BinaryReader reader, int version = int.MaxValue)
    {
        Slot = reader.ReadByte();
        bool validItem = reader.ReadBoolean();
        if (validItem)
        {
            SocketedItem = new UserItem(reader, version);
            CanRemove = reader.ReadBoolean();
            SocketItemType = (SocketType)reader.ReadByte();
        }
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Slot);
        bool validItem = SocketedItem == null ? false : true;
        writer.Write(validItem);
        if (validItem)
        {
            SocketedItem.Save(writer);
            writer.Write(CanRemove);
            writer.Write((byte)SocketItemType);
        }
    }
}
#endregion

public class UserItem
{
    public ulong UniqueID;
    public int ItemIndex;

    public ItemInfo Info;
    public ushort CurrentDura, MaxDura;
    public uint Count = 1, GemCount = 0;

    public byte AC, MAC, DC, MC, SC, Accuracy, Agility, HP, MP, Strong, MagicResist, PoisonResist, HealthRecovery, ManaRecovery, PoisonRecovery, CriticalRate, CriticalDamage, Freezing, PoisonAttack;
    public sbyte AttackSpeed, Luck;

    public RefinedValue RefinedValue = RefinedValue.None;
    public byte RefineAdded = 0;

    public bool DuraChanged;
    public int SoulBoundId = -1;
    public bool Identified = false;
    public bool Cursed = false;

    public int WeddingRing = -1;

    public UserItem[] Slots = new UserItem[5];

    public DateTime BuybackExpiryDate;

    public ExpireInfo ExpireInfo;
    public RentalInformation RentalInformation;

	public Awake Awake = new Awake();

    public Sockets[] RuneSlots = new Sockets[0];

    public short Transform;
    public ushort TransformImage;

    public bool IsAdded
    {
        get
        {
            return AC != 0 || MAC != 0 || DC != 0 || MC != 0 || SC != 0 || Accuracy != 0 || Agility != 0 || HP != 0 || MP != 0 || AttackSpeed != 0 || Luck != 0 || Strong != 0 || MagicResist != 0 || PoisonResist != 0 ||
                HealthRecovery != 0 || ManaRecovery != 0 || PoisonRecovery != 0 || CriticalRate != 0 || CriticalDamage != 0 || Freezing != 0 || PoisonAttack != 0;
        }
    }

    public uint Weight
    {
        get { return Info.Type == ItemType.Amulet ? Info.Weight : Info.Weight*Count; }
    }

    public string Name
    {
        get { return Count > 1 ? string.Format("{0} ({1})", Info.Name, Count) : Info.Name; }   
    }
    
    public string FriendlyName
    {
        get { return Count > 1 ? string.Format("{0} ({1})", Info.FriendlyName, Count) : Info.FriendlyName; }
    }

    public UserItem(ItemInfo info)
    {
        SoulBoundId = -1;
        ItemIndex = info.Index;
        Info = info;

        SetSlotSize();
    }
    public UserItem(BinaryReader reader, int version = int.MaxValue, int Customversion = int.MaxValue)
    {
        UniqueID = reader.ReadUInt64();
        ItemIndex = reader.ReadInt32();

        CurrentDura = reader.ReadUInt16();
        MaxDura = reader.ReadUInt16();

        Count = reader.ReadUInt32();

        AC = reader.ReadByte();
        MAC = reader.ReadByte();
        DC = reader.ReadByte();
        MC = reader.ReadByte();
        SC = reader.ReadByte();

        Accuracy = reader.ReadByte();
        Agility = reader.ReadByte();
        HP = reader.ReadByte();
        MP = reader.ReadByte();

        AttackSpeed = reader.ReadSByte();
        Luck = reader.ReadSByte();

        if (version <= 19) return;
        SoulBoundId = reader.ReadInt32();
        byte Bools = reader.ReadByte();        
        Identified = (Bools & 0x01) == 0x01;
        Cursed = (Bools & 0x02) == 0x02;
        Strong = reader.ReadByte();
        MagicResist = reader.ReadByte();
        PoisonResist = reader.ReadByte();
        HealthRecovery = reader.ReadByte();
        ManaRecovery = reader.ReadByte();
        PoisonRecovery = reader.ReadByte();
        CriticalRate = reader.ReadByte();
        CriticalDamage = reader.ReadByte();
        Freezing = reader.ReadByte();
        PoisonAttack = reader.ReadByte();
        

        if (version <= 31) return;

        int count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            if (reader.ReadBoolean()) continue;
            UserItem item = new UserItem(reader, version, Customversion);
            Slots[i] = item;
        }

        if (version <= 38) return;

        GemCount = reader.ReadUInt32();

        if (version <= 40) return;

        Awake = new Awake(reader);

        if (version <= 56) return;

        RefinedValue = (RefinedValue)reader.ReadByte();
        RefineAdded = reader.ReadByte();
        if (version < 60) return;
        WeddingRing = reader.ReadInt32();

        if (version < 65) return;

        if (reader.ReadBoolean())
            ExpireInfo = new ExpireInfo(reader, version, Customversion);

        if (version < 76)
            return;

        if (reader.ReadBoolean())
            RentalInformation = new RentalInformation(reader, version, Customversion);

        if (version > 85)
        {
            count = reader.ReadInt32();
            RuneSlots = new Sockets[count];
            for (int i = 0; i < count; i++)
            {
                if (!reader.ReadBoolean()) continue;
                Sockets socket = new Sockets(reader, version);
                RuneSlots[i] = socket;
            }
        }

        if (version > 86)
        {
            Transform = reader.ReadInt16();
        }

        if (version > 87)
        {
            TransformImage = reader.ReadUInt16();
        }
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(UniqueID);
        writer.Write(ItemIndex);

        writer.Write(CurrentDura);
        writer.Write(MaxDura);

        writer.Write(Count);

        writer.Write(AC);
        writer.Write(MAC);
        writer.Write(DC);
        writer.Write(MC);
        writer.Write(SC);

        writer.Write(Accuracy);
        writer.Write(Agility);
        writer.Write(HP);
        writer.Write(MP);

        writer.Write(AttackSpeed);
        writer.Write(Luck);
        writer.Write(SoulBoundId);
        byte Bools=0;        
        if (Identified) Bools |= 0x01;
        if (Cursed) Bools |= 0x02;
        writer.Write(Bools);
        writer.Write(Strong);
        writer.Write(MagicResist);
        writer.Write(PoisonResist);
        writer.Write(HealthRecovery);
        writer.Write(ManaRecovery);
        writer.Write(PoisonRecovery);
        writer.Write(CriticalRate);
        writer.Write(CriticalDamage);
        writer.Write(Freezing);
        writer.Write(PoisonAttack);

        writer.Write(Slots.Length);
        for (int i = 0; i < Slots.Length; i++)
        {
            writer.Write(Slots[i] == null);
            if (Slots[i] != null)
                Slots[i].Save(writer);
        }

        writer.Write(GemCount);

       
        Awake.Save(writer);

        writer.Write((byte)RefinedValue);
        writer.Write(RefineAdded);

        writer.Write(WeddingRing);

        writer.Write(ExpireInfo != null);
        ExpireInfo?.Save(writer);

        writer.Write(RentalInformation != null);
        RentalInformation?.Save(writer);

        writer.Write(RuneSlots.Length);
        for (int i = 0; i < RuneSlots.Length; i++)
        {
            writer.Write(RuneSlots[i] != null);
            if (RuneSlots[i] != null)
                RuneSlots[i].Save(writer);
        }

        writer.Write(Transform);
        writer.Write(TransformImage); 
    }

    public uint Price(uint count)
    {
        if (Info == null) return 0;

        uint p = Info.Price;


        if (Info.Durability > 0)
        {
            float r = ((Info.Price / 2F) / Info.Durability);

            p = (uint)(MaxDura * r);

            if (MaxDura > 0)
                r = CurrentDura / (float)MaxDura;
            else
                r = 0;

            p = (uint)Math.Floor(p / 2F + ((p / 2F) * r) + Info.Price / 2F);
        }

        p = (uint)(p * ((AC + MAC + DC + MC + SC + Accuracy + Agility + HP + MP + AttackSpeed + Luck + Strong + MagicResist + PoisonResist + HealthRecovery + ManaRecovery + PoisonRecovery + CriticalRate + CriticalDamage + Freezing + PoisonAttack) * 0.1F + 1F));

        return p * count;
    }

    public uint Price()
    {
        return Price(Count);
    }
    public uint RepairPrice()
    {
        if (Info == null || Info.Durability == 0)
            return 0;

        var p = Info.Price;

        if (Info.Durability > 0)
        {
            p = (uint)Math.Floor(MaxDura * ((Info.Price / 2F) / Info.Durability) + Info.Price / 2F);
            p = (uint)(p * ((AC + MAC + DC + MC + SC + Accuracy + Agility + HP + MP + AttackSpeed + Luck + Strong + MagicResist + PoisonResist + HealthRecovery + ManaRecovery + PoisonRecovery + CriticalRate + CriticalDamage + Freezing + PoisonAttack) * 0.1F + 1F));

        }

        var cost = p * Count - Price();

        if (RentalInformation == null)
            return cost;

        return cost * 2;
    }

    public uint Quality()
    {
        uint q = (uint)(AC + MAC + DC + MC + SC + Accuracy + Agility + HP + MP + AttackSpeed + Luck + Strong + MagicResist + PoisonResist + HealthRecovery + ManaRecovery + PoisonRecovery + CriticalRate + CriticalDamage + Freezing + PoisonAttack + Awake.getAwakeLevel() + 1);

        return q;
    }

    public uint AwakeningPrice()
    {
        if (Info == null) return 0;

        uint p = 1500;

        p = (uint)((p * (1 + Awake.getAwakeLevel() * 2)) * (uint)Info.Grade);
       
        return p;
    }

    public uint DisassemblePrice()
    {
        if (Info == null) return 0;

        uint p = 1500 * (uint)Info.Grade;

        p = (uint)(p * ((AC + MAC + DC + MC + SC + Accuracy + Agility + HP + MP + AttackSpeed + Luck + Strong + MagicResist + PoisonResist + HealthRecovery + ManaRecovery + PoisonRecovery + CriticalRate + CriticalDamage + Freezing + PoisonAttack + Awake.getAwakeLevel()) * 0.1F + 1F));

        return p;
    }

    public uint DowngradePrice()
    {
        if (Info == null) return 0;

        uint p = 3000;

        p = (uint)((p * (1 + (Awake.getAwakeLevel() + 1) * 2)) * (uint)Info.Grade);

        return p;
    }

    public uint ResetPrice()
    {
        if (Info == null) return 0;

        uint p = 3000 * (uint)Info.Grade;

        p = (uint)(p * ((AC + MAC + DC + MC + SC + Accuracy + Agility + HP + MP + AttackSpeed + Luck + Strong + MagicResist + PoisonResist + HealthRecovery + ManaRecovery + PoisonRecovery + CriticalRate + CriticalDamage + Freezing + PoisonAttack) * 0.2F + 1F));

        return p;
    }
    public void SetSlotSize() //set slot size in db?
    {
        int amount = 0;

        switch (Info.Type)
        {
            case ItemType.Mount:
                if (Info.Shape < 7)
                    amount = 4;
                else if (Info.Shape < 12)
                    amount = 5;
                break;
            case ItemType.Weapon:
                if (Info.Shape == 49 || Info.Shape == 50)
                    amount = 5;
                break;
        }

        if (amount == Slots.Length) return;

        Array.Resize(ref Slots, amount);
    }

    public bool LookHumUp()
    {
        return Info != null && (Info.Type == ItemType.Armour || Info.Type == ItemType.Weapon) && (Info.NeedHumUp || Transform > 0);
    }

    public ushort Image
    {
        get
        {
            if (Info == null)
                return 0;

            switch (Info.Type)
            {
                #region Amulet and Poison Stack Image changes
                case ItemType.Amulet:
                    if (Info.StackSize > 0)
                    {
                        switch (Info.Shape)
                        {
                            case 0: //Amulet
                                if (Count >= 300) return 3662;
                                if (Count >= 200) return 3661;
                                if (Count >= 100) return 3660;
                                return 3660;
                            case 1: //Grey Poison
                                if (Count >= 150) return 3675;
                                if (Count >= 100) return 2960;
                                if (Count >= 50) return 3674;
                                return 3673;
                            case 2: //Yellow Poison
                                if (Count >= 150) return 3672;
                                if (Count >= 100) return 2961;
                                if (Count >= 50) return 3671;
                                return 3670;
                        }
                    }
                    break;
            }

            #endregion

            if (TransformImage > 0)
                return TransformImage;
            return Info.Image;
        }
    }

    public UserItem Clone()
    {
        UserItem item = new UserItem(Info)
        {
            UniqueID = UniqueID,
            CurrentDura = CurrentDura,
            MaxDura = MaxDura,
            Count = Count,

            AC = AC,
            MAC = MAC,
            DC = DC,
            MC = MC,
            SC = SC,
            Accuracy = Accuracy,
            Agility = Agility,
            HP = HP,
            MP = MP,

            AttackSpeed = AttackSpeed,
            Luck = Luck,

            DuraChanged = DuraChanged,
            SoulBoundId = SoulBoundId,
            Identified = Identified,
            Cursed = Cursed,
            Strong = Strong,
            MagicResist = MagicResist,
            PoisonResist = PoisonResist,
            HealthRecovery = HealthRecovery,
            ManaRecovery = ManaRecovery,
            PoisonRecovery = PoisonRecovery,
            CriticalRate = CriticalRate,
            CriticalDamage = CriticalDamage,
            Freezing = Freezing,
            PoisonAttack = PoisonAttack,

            Slots = Slots,
            Awake = Awake,

            RefinedValue = RefinedValue,
            RefineAdded = RefineAdded,

            ExpireInfo = ExpireInfo,
            RentalInformation = RentalInformation,
        };

        return item;
    }

    internal void InheritItem(UserItem item)
    {
        AC = item.AC;
        MAC = item.MAC;
        DC = item.DC;
        MC = item.MC;
        SC = item.SC;
        Accuracy = item.Accuracy;
        Agility = item.Agility;
        HP = item.HP;
        MP = item.MP;

        AttackSpeed = item.AttackSpeed;
        Luck = item.Luck;

        DuraChanged = item.DuraChanged;
        SoulBoundId = item.SoulBoundId;
        Identified = item.Identified;
        Cursed = item.Cursed;
        Strong = item.Strong;
        MagicResist = item.MagicResist;
        PoisonResist = item.PoisonResist;
        HealthRecovery = item.HealthRecovery;
        ManaRecovery = item.ManaRecovery;
        PoisonRecovery = item.PoisonRecovery;
        CriticalRate = item.CriticalRate;
        CriticalDamage = item.CriticalDamage;
        Freezing = item.Freezing;
        PoisonAttack = item.PoisonAttack;

        RefinedValue = item.RefinedValue;
        RefineAdded = item.RefineAdded;
    }
}

public class ExpireInfo
{
    public DateTime ExpiryDate;

    public ExpireInfo()
    {

    }

    public ExpireInfo(BinaryReader reader, int version = int.MaxValue, int Customversion = int.MaxValue)
    {
        ExpiryDate = DateTime.FromBinary(reader.ReadInt64());
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(ExpiryDate.ToBinary());
    }
}

public class RentalInformation
{
    public string OwnerName;
    public BindMode BindingFlags = BindMode.none;
    public DateTime ExpiryDate;
    public bool RentalLocked;

    public RentalInformation()
    { }

    public RentalInformation(BinaryReader reader, int version = int.MaxValue, int CustomVersion = int.MaxValue)
    {
        OwnerName = reader.ReadString();
        BindingFlags = (BindMode)reader.ReadInt16();
        ExpiryDate = DateTime.FromBinary(reader.ReadInt64());
        RentalLocked = reader.ReadBoolean();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(OwnerName);
        writer.Write((short)BindingFlags);
        writer.Write(ExpiryDate.ToBinary());
        writer.Write(RentalLocked);
    }
}

public class GameShopItem
{
    public int ItemIndex;
    public int GIndex;
    public ItemInfo Info;
    public uint GoldPrice = 0;
    public uint CreditPrice = 0;
    public uint Count = 1;
    public string Class = "";
    public string Category = "";
    public int Stock = 0;
    public bool iStock = false;
    public bool Deal = false;
    public bool TopItem = false;
    public DateTime Date;
    
    public GameShopItem()
    {
    }

    public GameShopItem(BinaryReader reader, int version = int.MaxValue, int Customversion = int.MaxValue)
    {
        ItemIndex = reader.ReadInt32();
        GIndex = reader.ReadInt32();
        GoldPrice = reader.ReadUInt32();
        CreditPrice = reader.ReadUInt32();
        Count = reader.ReadUInt32();
        Class = reader.ReadString();
        Category = reader.ReadString();
        Stock = reader.ReadInt32();
        iStock = reader.ReadBoolean();
        Deal = reader.ReadBoolean();
        TopItem = reader.ReadBoolean();
        Date = DateTime.FromBinary(reader.ReadInt64());
    }

    public GameShopItem(BinaryReader reader, bool packet = false)
    {
        ItemIndex = reader.ReadInt32();
        GIndex = reader.ReadInt32();
        Info = new ItemInfo(reader);
        GoldPrice = reader.ReadUInt32();
        CreditPrice = reader.ReadUInt32();
        Count = reader.ReadUInt32();
        Class = reader.ReadString();
        Category = reader.ReadString();
        Stock = reader.ReadInt32();
        iStock = reader.ReadBoolean();
        Deal = reader.ReadBoolean();
        TopItem = reader.ReadBoolean();
        Date = DateTime.FromBinary(reader.ReadInt64());
    }

    public void Save(BinaryWriter writer, bool packet = false)
    {
        writer.Write(ItemIndex);
        writer.Write(GIndex);
        if (packet) Info.Save(writer);
        writer.Write(GoldPrice);
        writer.Write(CreditPrice);
        writer.Write(Count);
        writer.Write(Class);
        writer.Write(Category);
        writer.Write(Stock);
        writer.Write(iStock);
        writer.Write(Deal);
        writer.Write(TopItem);
        writer.Write(Date.ToBinary());
    }

    public override string ToString()
    {
        return string.Format("{0}: {1}", GIndex, Info.Name);
    }

    public string ToText()
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
        ItemIndex, GIndex, GoldPrice, CreditPrice, Count, Class, Category, Stock, iStock, Deal, TopItem, Date);
    }

    public static GameShopItem FromText(string text)
    {
        string[] data = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        if (data.Length < 11) return null;

        GameShopItem info = new GameShopItem();

        if (!int.TryParse(data[0], out info.ItemIndex)) return null;
        if (!int.TryParse(data[1], out info.GIndex)) return null;
        if (!uint.TryParse(data[2], out info.GoldPrice)) return null;
        if (!uint.TryParse(data[3], out info.CreditPrice)) return null;
        if (!uint.TryParse(data[4], out info.Count)) return null;
        info.Class = data[5];
        info.Category = data[6];
        if (!int.TryParse(data[7], out info.Stock)) return null;
        if (!bool.TryParse(data[8], out info.iStock)) return null;
        if (!bool.TryParse(data[9], out info.Deal)) return null;
        if (!bool.TryParse(data[10], out info.TopItem)) return null;
        if (!DateTime.TryParse(data[11], out info.Date)) return null;

        return info;
    }

    public static string ToHeader()
    {
        return "ItemIndex,GIndex,GoldPrice,CreditPrice,Count,Class,Category,Stock,Deal,TopItem,Date";
    }
}

public class Awake
{
    //Awake Option
    public static byte AwakeSuccessRate = 70;
    public static byte AwakeHitRate = 70;
    public static int MaxAwakeLevel = 5;
    public static byte Awake_WeaponRate = 1;
    public static byte Awake_HelmetRate = 1;
    public static byte Awake_ArmorRate = 5;
    public static byte AwakeChanceMin = 1;
    public static float[] AwakeMaterialRate = new float[4] { 1.0F, 1.0F, 1.0F, 1.0F };
    public static byte[] AwakeChanceMax = new byte[4] { 1, 2, 3, 4 };
    public static List<List<byte>[]> AwakeMaterials = new List<List<byte>[]>();

    public AwakeType type;
    List<byte> listAwake = new List<byte>();

    public int extraSuccessRate;

    public Awake(BinaryReader reader)
    {
        type = (AwakeType)reader.ReadByte();
        int count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            listAwake.Add(reader.ReadByte());
        }
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write((byte)type);
        writer.Write(listAwake.Count);
        foreach (byte value in listAwake)
        {
            writer.Write(value);
        }
    }

    public Awake()
    {
        type = AwakeType.None;
    }

    public bool IsMaxLevel() { return listAwake.Count == Awake.MaxAwakeLevel; }

    public int getAwakeLevel() { return listAwake.Count; }

    public byte getAwakeValue()
    {
        byte total = 0;

        foreach (byte value in listAwake)
        {
            total += value;
        }

        return total;
    }

    public bool CheckAwakening(UserItem item, AwakeType type)
    {
        if (item.Info.Bind.HasFlag(BindMode.DontUpgrade))
            return false;

        if (item.Info.CanAwakening != true)
            return false;

        if (item.Info.Grade == ItemGrade.None)
            return false;

        if (IsMaxLevel()) return false;

        if (this.type == AwakeType.None)
        {
            if (item.Info.Type == ItemType.Weapon)
            {
                if (type == AwakeType.DC ||
                    type == AwakeType.MC ||
                    type == AwakeType.SC)
                {
                    this.type = type;
                    return true;
                }
                else
                    return false;
            }
            else if (item.Info.Type == ItemType.Helmet)
            {
                if (type == AwakeType.AC ||
                    type == AwakeType.MAC)
                {
                    this.type = type;
                    return true;
                }
                else
                    return false;
            }
            else if (item.Info.Type == ItemType.Armour)
            {
                if (type == AwakeType.HPMP)
                {
                    this.type = type;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        else
        {
            if (this.type == type)
                return true;
            else
                return false;
        }
    }

    public int UpgradeAwake(UserItem item, AwakeType type, out bool[] isHit)
    {
        //return -1 condition error, -1 = dont upgrade, 0 = failed, 1 = Succeed,  
        isHit = null;
        if (!CheckAwakening(item, type))
            return -1;

        Random rand = new Random(DateTime.Now.Millisecond);

        if (rand.Next(0, 100) <= AwakeSuccessRate + extraSuccessRate)
        {
            isHit = Awakening(item);
            return 1;
        }
        else
        {
            int idx;
            isHit = makeHit(1, out idx);
            return 0;
        }
    }

    public int RemoveAwake()
    {
        if (listAwake.Count > 0)
        {
            listAwake.Remove(listAwake[listAwake.Count - 1]);

            if (listAwake.Count == 0)
                type = AwakeType.None;

            return 1;
        }
        else
        {
            type = AwakeType.None;
            return 0;
        }
    }

    public int getAwakeLevelValue(int i) { return listAwake[i]; }

    public byte getDC() { return (type == AwakeType.DC ? getAwakeValue() : (byte)0); }
    public byte getMC() { return (type == AwakeType.MC ? getAwakeValue() : (byte)0); }
    public byte getSC() { return (type == AwakeType.SC ? getAwakeValue() : (byte)0); }
    public byte getAC() { return (type == AwakeType.AC ? getAwakeValue() : (byte)0); }
    public byte getMAC() { return (type == AwakeType.MAC ? getAwakeValue() : (byte)0); }
    public byte getHPMP() { return (type == AwakeType.HPMP ? getAwakeValue() : (byte)0); }

    private bool[] makeHit(int maxValue, out int makeValue)
    {
        float stepValue = (float)maxValue / 5.0f;
        float totalValue = 0.0f;
        bool[] isHit = new bool[5];
        Random rand = new Random(DateTime.Now.Millisecond);

        for (int i = 0; i < 5; i++)
        {
            if (rand.Next(0, 100) < AwakeHitRate)
            {
                totalValue += stepValue;
                isHit[i] = true;
            }
            else
            {
                isHit[i] = false;
            }
        }

        makeValue = totalValue <= 1.0f ? 1 : (int)totalValue;
        return isHit;
    }

    private bool[] Awakening(UserItem item)
    {
        int minValue = AwakeChanceMin;
        int maxValue = (AwakeChanceMax[(int)item.Info.Grade - 1] < AwakeChanceMin) ? AwakeChanceMin : AwakeChanceMax[(int)item.Info.Grade - 1];

        int result;

        bool[] returnValue = makeHit(maxValue, out result);

        switch (item.Info.Type)
        {
            case ItemType.Weapon:
                result *= (int)Awake_WeaponRate;
                break;
            case ItemType.Armour:
                result *= (int)Awake_ArmorRate;
                break;
            case ItemType.Helmet:
                result *= (int)Awake_HelmetRate;
                break;
            default:
                result = 0;
                break;
        }

        listAwake.Add((byte)result);

        return returnValue;
    }
}

public class ClientMagic
{
    public Spell Spell;
    public byte BaseCost, LevelCost;
    public ushort Icon;
    public byte Level1, Level2, Level3, Level4;
    public ushort Need1, Need2, Need3, Need4;

    public byte Level, Key, Range;
    public ushort Experience;

    public bool IsTempSpell;
    public long CastTime, Delay;
    public string Name;


    public bool Toggle;

    public ClientMagic()
    {
    }

    public ClientMagic(BinaryReader reader)
    {
        Spell = (Spell)reader.ReadByte();

        BaseCost = reader.ReadByte();
        LevelCost = reader.ReadByte();
        Icon = reader.ReadUInt16();
        Level1 = reader.ReadByte();
        Level2 = reader.ReadByte();
        Level3 = reader.ReadByte();
        Level4 = reader.ReadByte();
        Need1 = reader.ReadUInt16();
        Need2 = reader.ReadUInt16();
        Need3 = reader.ReadUInt16();
        Need4 = reader.ReadUInt16();

        Level = reader.ReadByte();
        Key = reader.ReadByte();
        Experience = reader.ReadUInt16();

        Delay = reader.ReadInt64();

        Range = reader.ReadByte();
        CastTime = reader.ReadInt64();
        Name = reader.ReadString();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write((byte)Spell);

        writer.Write(BaseCost);
        writer.Write(LevelCost);
        writer.Write(Icon);
        writer.Write(Level1);
        writer.Write(Level2);
        writer.Write(Level3);
        writer.Write(Level4);
        writer.Write(Need1);
        writer.Write(Need2);
        writer.Write(Need3);
        writer.Write(Need4);

        writer.Write(Level);
        writer.Write(Key);
        writer.Write(Experience);

        writer.Write(Delay);

        writer.Write(Range);
        writer.Write(CastTime);
        writer.Write(Name);
    }
   
}

public class ClientAuction
{
    public ulong AuctionID;
    public UserItem Item;
    public string Seller = string.Empty;
    public uint Price;
    public DateTime ConsignmentDate;
    public byte CurrencyType;

    public ClientAuction()
    {
        
    }
    public ClientAuction(BinaryReader reader)
    {
        AuctionID = reader.ReadUInt64();
        Item = new UserItem(reader);
        Seller = reader.ReadString();
        Price = reader.ReadUInt32();
        ConsignmentDate = DateTime.FromBinary(reader.ReadInt64());
        CurrencyType = reader.ReadByte();
    }
    public void Save(BinaryWriter writer)
    {
        writer.Write(AuctionID);
        Item.Save(writer);
        writer.Write(Seller);
        writer.Write(Price);
        writer.Write(ConsignmentDate.ToBinary());
        writer.Write(CurrencyType);
    }
}

public class ClientQuestInfo
{
    public int Index;

    public uint NPCIndex;

    public string Name, Group;
    public List<string> Description = new List<string>();
    public List<string> TaskDescription = new List<string>();
    public List<string> CompletionDescription = new List<string>(); 

    public int MinLevelNeeded, MaxLevelNeeded;
    public int QuestNeeded;
    public RequiredClass ClassNeeded;

    public QuestType Type;

    public uint RewardGold;
    public uint RewardExp;
    public uint RewardCredit;
    public List<QuestItemReward> RewardsFixedItem = new List<QuestItemReward>();
    public List<QuestItemReward> RewardsSelectItem = new List<QuestItemReward>();

    public uint FinishNPCIndex;

    public bool SameFinishNPC
    {
        get { return NPCIndex == FinishNPCIndex; }
    }

    public ClientQuestInfo() { }

    public ClientQuestInfo(BinaryReader reader)
    {
        Index = reader.ReadInt32();
        NPCIndex = reader.ReadUInt32();
        Name = reader.ReadString();
        Group = reader.ReadString();

        int count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
            Description.Add(reader.ReadString());

        count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
            TaskDescription.Add(reader.ReadString());

        count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
            CompletionDescription.Add(reader.ReadString());

        MinLevelNeeded = reader.ReadInt32();
        MaxLevelNeeded = reader.ReadInt32();
        QuestNeeded = reader.ReadInt32();
        ClassNeeded = (RequiredClass)reader.ReadByte();
        Type = (QuestType)reader.ReadByte();
        RewardGold = reader.ReadUInt32();
        RewardExp = reader.ReadUInt32();
        RewardCredit = reader.ReadUInt32();

        count = reader.ReadInt32();

        for (int i = 0; i < count; i++ )
            RewardsFixedItem.Add(new QuestItemReward(reader));

        count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
            RewardsSelectItem.Add(new QuestItemReward(reader));

        FinishNPCIndex = reader.ReadUInt32();
    }
    public void Save(BinaryWriter writer)
    {
        writer.Write(Index);
        writer.Write(NPCIndex);
        writer.Write(Name);
        writer.Write(Group);

        writer.Write(Description.Count);
        for (int i = 0; i < Description.Count; i++)
            writer.Write(Description[i]);

        writer.Write(TaskDescription.Count);
        for (int i = 0; i < TaskDescription.Count; i++)
            writer.Write(TaskDescription[i]);

        writer.Write(CompletionDescription.Count);
        for (int i = 0; i < CompletionDescription.Count; i++)
            writer.Write(CompletionDescription[i]);

        writer.Write(MinLevelNeeded);
        writer.Write(MaxLevelNeeded);
        writer.Write(QuestNeeded);
        writer.Write((byte)ClassNeeded);
        writer.Write((byte)Type);
        writer.Write(RewardGold);
        writer.Write(RewardExp);
        writer.Write(RewardCredit);

        writer.Write(RewardsFixedItem.Count);

        for (int i = 0; i < RewardsFixedItem.Count; i++)
            RewardsFixedItem[i].Save(writer);

        writer.Write(RewardsSelectItem.Count);

        for (int i = 0; i < RewardsSelectItem.Count; i++)
            RewardsSelectItem[i].Save(writer);

        writer.Write(FinishNPCIndex);
    }

    public QuestIcon GetQuestIcon(bool taken = false, bool completed = false)
    {
        QuestIcon icon = QuestIcon.None;

        switch (Type)
        {
            case QuestType.General:
            case QuestType.Repeatable:
                if (completed)
                    icon = QuestIcon.QuestionYellow;
                else if (taken)
                    icon = QuestIcon.QuestionWhite;
                else
                    icon = QuestIcon.ExclamationYellow;
                break;
            case QuestType.Daily:
                if (completed)
                    icon = QuestIcon.QuestionBlue;
                else if (taken)
                    icon = QuestIcon.QuestionWhite;
                else
                    icon = QuestIcon.ExclamationBlue;
                break;
            case QuestType.Story:
                if (completed)
                    icon = QuestIcon.QuestionGreen;
                else if (taken)
                    icon = QuestIcon.QuestionWhite;
                else
                    icon = QuestIcon.ExclamationGreen;
                break;
        }

        return icon;
    }
}

public class ClientQuestProgress
{
    public int Id;

    public ClientQuestInfo QuestInfo;

    public List<string> TaskList = new List<string>();

    public bool Taken;
    public bool Completed;
    public bool New;

    public QuestIcon Icon
    {
        get 
        {
            return QuestInfo.GetQuestIcon(Taken, Completed); 
        }
    }

    public ClientQuestProgress(){ }

    public ClientQuestProgress(BinaryReader reader)
    {
        Id = reader.ReadInt32();

        int count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
            TaskList.Add(reader.ReadString());

        Taken = reader.ReadBoolean();
        Completed = reader.ReadBoolean();
        New = reader.ReadBoolean();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Id);

        writer.Write(TaskList.Count);

        for (int i = 0; i < TaskList.Count; i++)
            writer.Write(TaskList[i]);

        writer.Write(Taken);
        writer.Write(Completed);
        writer.Write(New);
    }
}

public class QuestItemReward
{
    public ItemInfo Item;
    public uint Count = 1;

    public QuestItemReward() { }

    public QuestItemReward(BinaryReader reader)
    {
        Item = new ItemInfo(reader);
        Count = reader.ReadUInt32();
    }

    public void Save(BinaryWriter writer)
    {
        Item.Save(writer);
        writer.Write(Count);
    }
}

public class ClientMail
{
    public ulong MailID;
    public string SenderName;
    public string Message;
    public bool Opened, Locked, CanReply, Collected;

    public DateTime DateSent;

    public uint Gold;
    public List<UserItem> Items = new List<UserItem>();

    public ClientMail() { }

    public ClientMail(BinaryReader reader)
    {
        MailID = reader.ReadUInt64();
        SenderName = reader.ReadString();
        Message = reader.ReadString();
        Opened = reader.ReadBoolean();
        Locked = reader.ReadBoolean();
        CanReply = reader.ReadBoolean();
        Collected = reader.ReadBoolean();

        DateSent = DateTime.FromBinary(reader.ReadInt64());

        Gold = reader.ReadUInt32();
        int count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
            Items.Add(new UserItem(reader));
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(MailID);
        writer.Write(SenderName);
        writer.Write(Message);
        writer.Write(Opened);
        writer.Write(Locked);
        writer.Write(CanReply);
        writer.Write(Collected);

        writer.Write(DateSent.ToBinary());

        writer.Write(Gold);
        writer.Write(Items.Count);

        for (int i = 0; i < Items.Count; i++)
            Items[i].Save(writer);
    }
}

public class ClientFriend
{
    public int Index;
    public string Name;
    public string Memo = "";
    public bool Blocked;

    public bool Online;

    public ClientFriend() { }

    public ClientFriend(BinaryReader reader)
    {
        Index = reader.ReadInt32();
        Name = reader.ReadString();
        Memo = reader.ReadString();
        Blocked = reader.ReadBoolean();

        Online = reader.ReadBoolean();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Index);
        writer.Write(Name);
        writer.Write(Memo);
        writer.Write(Blocked);

        writer.Write(Online);
    }
}


public enum IntelligentCreaturePickupMode : byte
{
    Automatic = 0,
    SemiAutomatic = 1,
}

public class IntelligentCreatureRules
{
    public int MinimalFullness = 1;

    public bool MousePickupEnabled = false;
    public int MousePickupRange = 0;
    public bool AutoPickupEnabled = false;
    public int AutoPickupRange = 0;
    public bool SemiAutoPickupEnabled = false;
    public int SemiAutoPickupRange = 0;

    public bool CanProduceBlackStone = false;

    public string Info = "";
    public string Info1 = "";
    public string Info2 = "";

    public IntelligentCreatureRules()
    {
    }

    public IntelligentCreatureRules(BinaryReader reader)
    {
        MinimalFullness = reader.ReadInt32();
        MousePickupEnabled = reader.ReadBoolean();
        MousePickupRange = reader.ReadInt32();
        AutoPickupEnabled = reader.ReadBoolean();
        AutoPickupRange = reader.ReadInt32();
        SemiAutoPickupEnabled = reader.ReadBoolean();
        SemiAutoPickupRange = reader.ReadInt32();

        CanProduceBlackStone = reader.ReadBoolean();

        Info = reader.ReadString();
        Info1 = reader.ReadString();
        Info2 = reader.ReadString();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(MinimalFullness);
        writer.Write(MousePickupEnabled);
        writer.Write(MousePickupRange);
        writer.Write(AutoPickupEnabled);
        writer.Write(AutoPickupRange);
        writer.Write(SemiAutoPickupEnabled);
        writer.Write(SemiAutoPickupRange);

        writer.Write(CanProduceBlackStone);

        writer.Write(Info);
        writer.Write(Info1);
        writer.Write(Info2);
    }
}

public class IntelligentCreatureItemFilter
{
    public bool PetPickupAll = true;
    public bool PetPickupGold = false;
    public bool PetPickupWeapons = false;
    public bool PetPickupArmours = false;
    public bool PetPickupHelmets = false;
    public bool PetPickupBoots = false;
    public bool PetPickupBelts = false;
    public bool PetPickupAccessories = false;
    public bool PetPickupOthers = false;

    public ItemGrade PickupGrade = ItemGrade.None;

    public IntelligentCreatureItemFilter()
    {
    }

    public void SetItemFilter(int idx)
    {
        switch (idx)
        {
            case 0://all items
                PetPickupAll = true;
                PetPickupGold = false;
                PetPickupWeapons = false;
                PetPickupArmours = false;
                PetPickupHelmets = false;
                PetPickupBoots = false;
                PetPickupBelts = false;
                PetPickupAccessories = false;
                PetPickupOthers = false;
                break;
            case 1://gold
                PetPickupAll = false;
                PetPickupGold = !PetPickupGold;
                break;
            case 2://weapons
                PetPickupAll = false;
                PetPickupWeapons = !PetPickupWeapons;
                break;
            case 3://armours
                PetPickupAll = false;
                PetPickupArmours = !PetPickupArmours;
                break;
            case 4://helmets
                PetPickupAll = false;
                PetPickupHelmets = !PetPickupHelmets;
                break;
            case 5://boots
                PetPickupAll = false;
                PetPickupBoots = !PetPickupBoots;
                break;
            case 6://belts
                PetPickupAll = false;
                PetPickupBelts = !PetPickupBelts;
                break;
            case 7://jewelry
                PetPickupAll = false;
                PetPickupAccessories = !PetPickupAccessories;
                break;
            case 8://others
                PetPickupAll = false;
                PetPickupOthers = !PetPickupOthers;
                break;
        }
        if (PetPickupGold && PetPickupWeapons && PetPickupArmours && PetPickupHelmets && PetPickupBoots && PetPickupBelts && PetPickupAccessories && PetPickupOthers)
        {
            PetPickupAll = true;
            PetPickupGold = false;
            PetPickupWeapons = false;
            PetPickupArmours = false;
            PetPickupHelmets = false;
            PetPickupBoots = false;
            PetPickupBelts = false;
            PetPickupAccessories = false;
            PetPickupOthers = false;
        }
        else
            if (!PetPickupGold && !PetPickupWeapons && !PetPickupArmours && !PetPickupHelmets && !PetPickupBoots && !PetPickupBelts && !PetPickupAccessories && !PetPickupOthers)
            {
                PetPickupAll = true;
            }
    }

    public IntelligentCreatureItemFilter(BinaryReader reader)
    {
        PetPickupAll = reader.ReadBoolean();
        PetPickupGold = reader.ReadBoolean();
        PetPickupWeapons = reader.ReadBoolean();
        PetPickupArmours = reader.ReadBoolean();
        PetPickupHelmets = reader.ReadBoolean();
        PetPickupBoots = reader.ReadBoolean();
        PetPickupBelts = reader.ReadBoolean();
        PetPickupAccessories = reader.ReadBoolean();
        PetPickupOthers = reader.ReadBoolean();
        //PickupGrade = (ItemGrade)reader.ReadByte();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(PetPickupAll);
        writer.Write(PetPickupGold);
        writer.Write(PetPickupWeapons);
        writer.Write(PetPickupArmours);
        writer.Write(PetPickupHelmets);
        writer.Write(PetPickupBoots);
        writer.Write(PetPickupBelts);
        writer.Write(PetPickupAccessories);
        writer.Write(PetPickupOthers);
        //writer.Write((byte)PickupGrade);
    }
}

public class ClientIntelligentCreature
{
    public IntelligentCreatureType PetType;
    public int Icon;

    public string CustomName;
    public int Fullness;
    public int SlotIndex;
    public long ExpireTime;//in days
    public long BlackstoneTime;
    public long MaintainFoodTime;

    public IntelligentCreaturePickupMode petMode = IntelligentCreaturePickupMode.SemiAutomatic;

    public IntelligentCreatureRules CreatureRules;
    public IntelligentCreatureItemFilter Filter;


    public ClientIntelligentCreature()
    {
    }

    public ClientIntelligentCreature(BinaryReader reader)
    {
        PetType = (IntelligentCreatureType)reader.ReadByte();
        Icon = reader.ReadInt32();

        CustomName = reader.ReadString();
        Fullness = reader.ReadInt32();
        SlotIndex = reader.ReadInt32();
        ExpireTime = reader.ReadInt64();
        BlackstoneTime = reader.ReadInt64();

        petMode = (IntelligentCreaturePickupMode)reader.ReadByte();

        CreatureRules = new IntelligentCreatureRules(reader);
        Filter = new IntelligentCreatureItemFilter(reader);
        Filter.PickupGrade = (ItemGrade)reader.ReadByte();
        MaintainFoodTime = reader.ReadInt64();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write((byte)PetType);
        writer.Write(Icon);

        writer.Write(CustomName);
        writer.Write(Fullness);
        writer.Write(SlotIndex);
        writer.Write(ExpireTime);
        writer.Write(BlackstoneTime);

        writer.Write((byte)petMode);

        CreatureRules.Save(writer);
        Filter.Save(writer);
        writer.Write((byte)Filter.PickupGrade);
        writer.Write(MaintainFoodTime);
    }
}


public abstract class Packet
{
    public static bool IsServer;

    public abstract short Index { get; }

    public static Packet ReceivePacket(byte[] rawBytes, out byte[] extra, ref int result)
    {
        extra = rawBytes;

        Packet p;

        if (rawBytes.Length < 4) return null; //| 2Bytes: Packet Size | 2Bytes: Packet ID |

        int length = (rawBytes[1] << 8) + rawBytes[0];

        if (length > rawBytes.Length || length < 4) return null;

        short id = 0;
        using (MemoryStream stream = new MemoryStream(rawBytes, 2, length - 2))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            try
            {
                id = reader.ReadInt16();

                p = IsServer ? GetClientPacket(id) : GetServerPacket(id);
                if (p == null)
                {
                    result = -1;
                    return null;
                }
                p.ReadPacket(reader);
            }
            catch(Exception ex)
            {
                File.AppendAllText(@".\Error.txt",
                                      string.Format("[{0}] {1} {2}{3}", DateTime.Now, id, ex, Environment.NewLine));
                return null;
                //return new C.Disconnect();
            }
        }

        extra = new byte[rawBytes.Length - length];
        Buffer.BlockCopy(rawBytes, length, extra, 0, rawBytes.Length - length);

        return p;
    }
    public static Packet ReceivePacket(byte[] rawBytes, out byte[] extra)
    {
        extra = rawBytes;

        Packet p;

        if (rawBytes.Length < 4) return null; //| 2Bytes: Packet Size | 2Bytes: Packet ID |

        int length = (rawBytes[1] << 8) + rawBytes[0];

        if (length > rawBytes.Length || length < 4) return null;

        short id = 0;
        using (MemoryStream stream = new MemoryStream(rawBytes, 2, length - 2))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            try
            {
                id = reader.ReadInt16();

                p = IsServer ? GetClientPacket(id) : GetServerPacket(id);
                if (p == null)
                {
                    return null;
                }
                p.ReadPacket(reader);
            }
            catch (Exception ex)
            {
                File.AppendAllText(@".\Error.txt",
                                      string.Format("[{0}] {1} {2}{3}", DateTime.Now, id, ex, Environment.NewLine));
                return null;
                //return new C.Disconnect();
            }
        }

        extra = new byte[rawBytes.Length - length];
        Buffer.BlockCopy(rawBytes, length, extra, 0, rawBytes.Length - length);

        return p;
    }
    public IEnumerable<byte> GetPacketBytes()
    {
        if (Index < 0) return new byte[0];

        byte[] data;

        using (MemoryStream stream = new MemoryStream())
        {
            stream.SetLength(2);
            stream.Seek(2, SeekOrigin.Begin);
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Index);
                WritePacket(writer);
                stream.Seek(0, SeekOrigin.Begin);
                writer.Write((short)stream.Length);
                stream.Seek(0, SeekOrigin.Begin);

                data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
            }
        }

        return data;
    }

    protected abstract void ReadPacket(BinaryReader reader);
    protected abstract void WritePacket(BinaryWriter writer);

    private static Packet GetClientPacket(short index)
    {
        switch (index)
        {
            case (short)ClientPacketIds.ServerList:
                return new C.ServerList();
            case (short)ClientPacketIds.ClientVersion:
                return new C.ClientVersion();
            case (short)ClientPacketIds.Disconnect:
                return new C.Disconnect();
            case (short)ClientPacketIds.KeepAlive:
                return new C.KeepAlive();
            case (short)ClientPacketIds.NewAccount:
                return new C.NewAccount();
            case (short)ClientPacketIds.ChangePassword:
                return new C.ChangePassword();
            case (short)ClientPacketIds.Login:
                return new C.Login();
            case (short)ClientPacketIds.NewCharacter:
                return new C.NewCharacter();
            case (short)ClientPacketIds.DeleteCharacter:
                return new C.DeleteCharacter();
            case (short)ClientPacketIds.StartGame:
                return new C.StartGame();
            case (short)ClientPacketIds.LogOut:
                return new C.LogOut();
            case (short)ClientPacketIds.Turn:
                return new C.Turn();
            case (short)ClientPacketIds.Walk:
                return new C.Walk();
            case (short)ClientPacketIds.Run:
                return new C.Run();
            case (short)ClientPacketIds.Chat:
                return new C.Chat();
            case (short)ClientPacketIds.MoveItem:
                return new C.MoveItem();
            case (short)ClientPacketIds.StoreItem:
                return new C.StoreItem();
            case (short)ClientPacketIds.TakeBackItem:
                return new C.TakeBackItem();
            case (short)ClientPacketIds.MergeItem:
                return new C.MergeItem();
            case (short)ClientPacketIds.EquipItem:
                return new C.EquipItem();
            case (short)ClientPacketIds.RemoveItem:
                return new C.RemoveItem();
            case (short)ClientPacketIds.RemoveSlotItem:
                return new C.RemoveSlotItem();
            case (short)ClientPacketIds.SplitItem:
                return new C.SplitItem();
            case (short)ClientPacketIds.UseItem:
                return new C.UseItem();
            case (short)ClientPacketIds.DropItem:
                return new C.DropItem();
            case (short)ClientPacketIds.DepositRefineItem:
                return new C.DepositRefineItem();
            case (short)ClientPacketIds.RetrieveRefineItem:
                return new C.RetrieveRefineItem();
            case (short)ClientPacketIds.RefineCancel:
                return new C.RefineCancel();
            case (short)ClientPacketIds.RefineItem:
                return new C.RefineItem();
            case (short)ClientPacketIds.CheckRefine:
                return new C.CheckRefine();
            case (short)ClientPacketIds.ReplaceWedRing:
                return new C.ReplaceWedRing();
            case (short)ClientPacketIds.DepositTradeItem:
                return new C.DepositTradeItem();
            case (short)ClientPacketIds.RetrieveTradeItem:
                return new C.RetrieveTradeItem();
            case (short)ClientPacketIds.DropGold:
                return new C.DropGold();
            case (short)ClientPacketIds.PickUp:
                return new C.PickUp();
            case (short)ClientPacketIds.Inspect:
                return new C.Inspect();
            case (short)ClientPacketIds.ChangeAMode:
                return new C.ChangeAMode();
            case (short)ClientPacketIds.ChangePMode:
                return new C.ChangePMode();
            case (short)ClientPacketIds.ChangeTrade:
                return new C.ChangeTrade();
            case (short)ClientPacketIds.Attack:
                return new C.Attack();
            case (short)ClientPacketIds.RangeAttack:
                return new C.RangeAttack();
            case (short)ClientPacketIds.Harvest:
                return new C.Harvest();
            case (short)ClientPacketIds.CallNPC:
                return new C.CallNPC();
            case (short)ClientPacketIds.TalkMonsterNPC:
                return new C.TalkMonsterNPC();
            case (short)ClientPacketIds.BuyItem:
                return new C.BuyItem();
            case (short)ClientPacketIds.SellItem:
                return new C.SellItem();
            case (short)ClientPacketIds.CraftItem:
                return new C.CraftItem();
            case (short)ClientPacketIds.RepairItem:
                return new C.RepairItem();
            case (short)ClientPacketIds.BuyItemBack:
                return new C.BuyItemBack();
            case (short)ClientPacketIds.SRepairItem:
                return new C.SRepairItem();
            case (short)ClientPacketIds.MagicKey:
                return new C.MagicKey();
            case (short)ClientPacketIds.Magic:
                return new C.Magic();
            case (short)ClientPacketIds.SwitchGroup:
                return new C.SwitchGroup();
            case (short)ClientPacketIds.AddMember:
                return new C.AddMember();
            case (short)ClientPacketIds.DellMember:
                return new C.DelMember();
            case (short)ClientPacketIds.GroupInvite:
                return new C.GroupInvite();
            case (short)ClientPacketIds.TownRevive:
                return new C.TownRevive();
            case (short)ClientPacketIds.SpellToggle:
                return new C.SpellToggle();
            case (short)ClientPacketIds.ConsignItem:
                return new C.ConsignItem();
            case (short)ClientPacketIds.MarketSearch:
                return new C.MarketSearch();
            case (short)ClientPacketIds.MarketRefresh:
                return new C.MarketRefresh();
            case (short)ClientPacketIds.MarketPage:
                return new C.MarketPage();
            case (short)ClientPacketIds.MarketBuy:
                return new C.MarketBuy();
            case (short)ClientPacketIds.MarketGetBack:
                return new C.MarketGetBack();
            case (short)ClientPacketIds.RequestUserName:
                return new C.RequestUserName();
            case (short)ClientPacketIds.RequestChatItem:
                return new C.RequestChatItem();
            case (short)ClientPacketIds.EditGuildMember:
                return new C.EditGuildMember();
            case (short)ClientPacketIds.EditGuildNotice:
                return new C.EditGuildNotice();
            case (short)ClientPacketIds.GuildInvite:
                return new C.GuildInvite();
            case (short)ClientPacketIds.GuildNameReturn:
                return new C.GuildNameReturn();
            case (short)ClientPacketIds.RequestGuildInfo:
                return new C.RequestGuildInfo();
            case (short)ClientPacketIds.GuildStorageGoldChange:
                return new C.GuildStorageGoldChange();
            case (short)ClientPacketIds.GuildStorageItemChange:
                return new C.GuildStorageItemChange();
            case (short)ClientPacketIds.GuildWarReturn:
                return new C.GuildWarReturn();
            case (short)ClientPacketIds.MarriageRequest:
                return new C.MarriageRequest();
            case (short)ClientPacketIds.MarriageReply:
                return new C.MarriageReply();
            case (short)ClientPacketIds.ChangeMarriage:
                return new C.ChangeMarriage();
            case (short)ClientPacketIds.DivorceRequest:
                return new C.DivorceRequest();
            case (short)ClientPacketIds.DivorceReply:
                return new C.DivorceReply();
            case (short)ClientPacketIds.AddMentor:
                return new C.AddMentor();
            case (short)ClientPacketIds.MentorReply:
                return new C.MentorReply();
            case (short)ClientPacketIds.AllowMentor:
                return new C.AllowMentor();
            case (short)ClientPacketIds.CancelMentor:
                return new C.CancelMentor();
            case (short)ClientPacketIds.TradeRequest:
                return new C.TradeRequest();
            case (short)ClientPacketIds.TradeReply:
                return new C.TradeReply();
            case (short)ClientPacketIds.TradeGold:
                return new C.TradeGold();
            case (short)ClientPacketIds.TradeConfirm:
                return new C.TradeConfirm();
            case (short)ClientPacketIds.TradeCancel:
                return new C.TradeCancel();
            case (short)ClientPacketIds.EquipSlotItem:
                return new C.EquipSlotItem();
            case (short)ClientPacketIds.FishingCast:
                return new C.FishingCast();
            case (short)ClientPacketIds.FishingChangeAutocast:
                return new C.FishingChangeAutocast();
            case (short)ClientPacketIds.AcceptQuest:
                return new C.AcceptQuest();
            case (short)ClientPacketIds.FinishQuest:
                return new C.FinishQuest();
            case (short)ClientPacketIds.AbandonQuest:
                return new C.AbandonQuest();
            case (short)ClientPacketIds.ShareQuest:
                return new C.ShareQuest();
            case (short)ClientPacketIds.AcceptReincarnation:
                return new C.AcceptReincarnation();
            case (short)ClientPacketIds.CancelReincarnation:
                return new C.CancelReincarnation();
            case (short)ClientPacketIds.CombineItem:
                return new C.CombineItem();
            case (short)ClientPacketIds.SetConcentration:
                return new C.SetConcentration();
			case (short)ClientPacketIds.AwakeningNeedMaterials:
                return new C.AwakeningNeedMaterials();
            case (short)ClientPacketIds.AwakeningLockedItem:
                return new C.AwakeningLockedItem();
            case (short)ClientPacketIds.Awakening:
                return new C.Awakening();
            case (short)ClientPacketIds.DisassembleItem:
                return new C.DisassembleItem();
            case (short)ClientPacketIds.DowngradeAwakening:
                return new C.DowngradeAwakening();
            case (short)ClientPacketIds.ResetAddedItem:
                return new C.ResetAddedItem();
            case (short)ClientPacketIds.SendMail:
                return new C.SendMail();
            case (short)ClientPacketIds.ReadMail:
                return new C.ReadMail();
            case (short)ClientPacketIds.CollectParcel:
                return new C.CollectParcel();
            case (short)ClientPacketIds.DeleteMail:
                return new C.DeleteMail();
            case (short)ClientPacketIds.LockMail:
                return new C.LockMail();
            case (short)ClientPacketIds.MailLockedItem:
                return new C.MailLockedItem();
            case (short)ClientPacketIds.MailCost:
                return new C.MailCost();
            case (short)ClientPacketIds.UpdateIntelligentCreature://IntelligentCreature
                return new C.UpdateIntelligentCreature();
            case (short)ClientPacketIds.IntelligentCreaturePickup://IntelligentCreature
                return new C.IntelligentCreaturePickup();
            case (short)ClientPacketIds.AddFriend:
                return new C.AddFriend();
            case (short)ClientPacketIds.RemoveFriend:
                return new C.RemoveFriend();
            case (short)ClientPacketIds.RefreshFriends:
                return new C.RefreshFriends();
            case (short)ClientPacketIds.AddMemo:
                return new C.AddMemo();
            case (short)ClientPacketIds.GuildBuffUpdate:
                return new C.GuildBuffUpdate();
            case (short)ClientPacketIds.GameshopBuy:
                return new C.GameshopBuy();
            case (short)ClientPacketIds.NPCConfirmInput:
                return new C.NPCConfirmInput();
            case (short)ClientPacketIds.ReportIssue:
                return new C.ReportIssue();
            case (short)ClientPacketIds.GetRanking:
                return new C.GetRanking();
            case (short)ClientPacketIds.Opendoor:
                return new C.Opendoor();
            case (short)ClientPacketIds.GetRentedItems:
                return new C.GetRentedItems();
            case (short)ClientPacketIds.ItemRentalRequest:
                return new C.ItemRentalRequest();
            case (short)ClientPacketIds.ItemRentalFee:
                return new C.ItemRentalFee();
            case (short)ClientPacketIds.ItemRentalPeriod:
                return new C.ItemRentalPeriod();
            case (short)ClientPacketIds.DepositRentalItem:
                return new C.DepositRentalItem();
            case (short)ClientPacketIds.RetrieveRentalItem:
                return new C.RetrieveRentalItem();
            case (short)ClientPacketIds.CancelItemRental:
                return new C.CancelItemRental();
            case (short)ClientPacketIds.ItemRentalLockFee:
                return new C.ItemRentalLockFee();
            case (short)ClientPacketIds.ItemRentalLockItem:
                return new C.ItemRentalLockItem();
            case (short)ClientPacketIds.ConfirmItemRental:
                return new C.ConfirmItemRental();
            case (short)ClientPacketIds.RearrangeInventory:
                return new C.RearrangeInventory();
            case (short)ClientPacketIds.EESocketItem:
                return new C.SocketRuneStone();
            case (short)ClientPacketIds.EERemoveRune:
                return new C.RemoveRune();
            case (short)ClientPacketIds.DownloadFile:
                return new C.DownloadFile();
            case (short)ClientPacketIds.ChangeNameReturn:
                return new C.ChangeNameReturn();
            default:
                return null;
        }

    }
    public static Packet GetServerPacket(short index)
    {
        switch (index)
        {
            case (short)ServerPacketIds.ServerList:
                return new S.ServerList();
            case (short)ServerPacketIds.Connected:
                return new S.Connected();
            case (short)ServerPacketIds.ClientVersion:
                return new S.ClientVersion();
            case (short)ServerPacketIds.Disconnect:
                return new S.Disconnect();
            case (short)ServerPacketIds.KeepAlive:
                return new S.KeepAlive();
            case (short)ServerPacketIds.NewAccount:
                return new S.NewAccount();
            case (short)ServerPacketIds.ChangePassword:
                return new S.ChangePassword();
            case (short)ServerPacketIds.ChangePasswordBanned:
                return new S.ChangePasswordBanned();
            case (short)ServerPacketIds.Login:
                return new S.Login();
            case (short)ServerPacketIds.LoginBanned:
                return new S.LoginBanned();
            case (short)ServerPacketIds.LoginSuccess:
                return new S.LoginSuccess();
            case (short)ServerPacketIds.NewCharacter:
                return new S.NewCharacter();
            case (short)ServerPacketIds.NewCharacterSuccess:
                return new S.NewCharacterSuccess();
            case (short)ServerPacketIds.DeleteCharacter:
                return new S.DeleteCharacter();
            case (short)ServerPacketIds.DeleteCharacterSuccess:
                return new S.DeleteCharacterSuccess();
            case (short)ServerPacketIds.StartGame:
                return new S.StartGame();
            case (short)ServerPacketIds.StartGameBanned:
                return new S.StartGameBanned();
            case (short)ServerPacketIds.StartGameDelay:
                return new S.StartGameDelay();       
            case (short)ServerPacketIds.MapInformation:
                return new S.MapInformation();
            case (short)ServerPacketIds.UserInformation:
                return new S.UserInformation();
            case (short)ServerPacketIds.UserLocation:
                return new S.UserLocation();
            case (short)ServerPacketIds.ObjectPlayer:
                return new S.ObjectPlayer();
            case (short)ServerPacketIds.ObjectRemove:
                return new S.ObjectRemove();
            case (short)ServerPacketIds.ObjectTurn:
                return new S.ObjectTurn();
            case (short)ServerPacketIds.ObjectWalk:
                return new S.ObjectWalk();
            case (short)ServerPacketIds.ObjectRun:
                return new S.ObjectRun();
            case (short)ServerPacketIds.Chat:
                return new S.Chat();
            case (short)ServerPacketIds.ObjectChat:
                return new S.ObjectChat();
            case (short)ServerPacketIds.NewItemInfo:
                return new S.NewItemInfo();
            case (short)ServerPacketIds.MoveItem:
                return new S.MoveItem();
            case (short)ServerPacketIds.EquipItem:
                return new S.EquipItem();
            case (short)ServerPacketIds.MergeItem:
                return new S.MergeItem();
            case (short)ServerPacketIds.RemoveItem:
                return new S.RemoveItem();
            case (short)ServerPacketIds.RemoveSlotItem:
                return new S.RemoveSlotItem();
            case (short)ServerPacketIds.TakeBackItem:
                return new S.TakeBackItem();
            case (short)ServerPacketIds.StoreItem:
                return new S.StoreItem();
            case (short)ServerPacketIds.DepositRefineItem:
                return new S.DepositRefineItem();
            case (short)ServerPacketIds.RetrieveRefineItem:
                return new S.RetrieveRefineItem();
            case (short)ServerPacketIds.RefineItem:
                return new S.RefineItem();
            case (short)ServerPacketIds.DepositTradeItem:
                return new S.DepositTradeItem();
            case (short)ServerPacketIds.RetrieveTradeItem:
                return new S.RetrieveTradeItem();
            case (short)ServerPacketIds.SplitItem:
                return new S.SplitItem();
            case (short)ServerPacketIds.SplitItem1:
                return new S.SplitItem1();
            case (short)ServerPacketIds.UseItem:
                return new S.UseItem();
            case (short)ServerPacketIds.DropItem:
                return new S.DropItem();
            case (short)ServerPacketIds.PlayerUpdate:
                return new S.PlayerUpdate();
            case (short)ServerPacketIds.PlayerInspect:
                return new S.PlayerInspect();
            case (short)ServerPacketIds.LogOutSuccess:
                return new S.LogOutSuccess();
            case (short)ServerPacketIds.LogOutFailed:
                return new S.LogOutFailed();
            case (short)ServerPacketIds.TimeOfDay:
                return new S.TimeOfDay();
            case (short)ServerPacketIds.ChangeAMode:
                return new S.ChangeAMode();
            case (short)ServerPacketIds.ChangePMode:
                return new S.ChangePMode();
            case (short)ServerPacketIds.ObjectItem:
                return new S.ObjectItem();
            case (short)ServerPacketIds.ObjectGold:
                return new S.ObjectGold();
            case (short)ServerPacketIds.GainedItem:
                return new S.GainedItem();
            case (short)ServerPacketIds.GainedGold:
                return new S.GainedGold();
            case (short)ServerPacketIds.LoseGold:
                return new S.LoseGold();
            case (short)ServerPacketIds.GainedCredit:
                return new S.GainedCredit();
            case (short)ServerPacketIds.LoseCredit:
                return new S.LoseCredit();
            case (short)ServerPacketIds.ObjectMonster:
                return new S.ObjectMonster();
            case (short)ServerPacketIds.ObjectAttack:
                return new S.ObjectAttack();
            case (short)ServerPacketIds.Struck:
                return new S.Struck();
            case (short)ServerPacketIds.DamageIndicator:
                return new S.DamageIndicator();
            case (short)ServerPacketIds.ObjectStruck:
                return new S.ObjectStruck();
            case (short)ServerPacketIds.DuraChanged:
                return new S.DuraChanged();
            case (short)ServerPacketIds.HealthChanged:
                return new S.HealthChanged();
            case (short)ServerPacketIds.DeleteItem:
                return new S.DeleteItem();
            case (short)ServerPacketIds.Death:
                return new S.Death();
            case (short)ServerPacketIds.ObjectDied:
                return new S.ObjectDied();
            case (short)ServerPacketIds.ColourChanged:
                return new S.ColourChanged();
            case (short)ServerPacketIds.ObjectColourChanged:
                return new S.ObjectColourChanged();
            case (short)ServerPacketIds.ObjectGuildNameChanged:
                return new S.ObjectGuildNameChanged();
            case (short)ServerPacketIds.GainExperience:
                return new S.GainExperience();
            case (short)ServerPacketIds.LevelChanged:
                return new S.LevelChanged();
            case (short)ServerPacketIds.ObjectLeveled:
                return new S.ObjectLeveled();
            case (short)ServerPacketIds.ObjectHarvest:
                return new S.ObjectHarvest();
            case (short)ServerPacketIds.ObjectHarvested:
                return new S.ObjectHarvested();
            case (short)ServerPacketIds.ObjectNpc:
                return new S.ObjectNPC();
            case (short)ServerPacketIds.NPCResponse:
                return new S.NPCResponse();
            case (short)ServerPacketIds.ObjectHide:
                return new S.ObjectHide();
            case (short)ServerPacketIds.ObjectShow:
                return new S.ObjectShow();
            case (short)ServerPacketIds.Poisoned:
                return new S.Poisoned();
            case (short)ServerPacketIds.ObjectPoisoned:
                return new S.ObjectPoisoned();
            case (short)ServerPacketIds.MapChanged:
                return new S.MapChanged();
            case (short)ServerPacketIds.ObjectTeleportOut:
                return new S.ObjectTeleportOut();
            case (short)ServerPacketIds.ObjectTeleportIn:
                return new S.ObjectTeleportIn();
            case (short)ServerPacketIds.TeleportIn:
                return new S.TeleportIn();
            case (short)ServerPacketIds.NPCGoods:
                return new S.NPCGoods();
            case (short)ServerPacketIds.NPCSell:
                return new S.NPCSell();
            case (short)ServerPacketIds.NPCRepair:
                return new S.NPCRepair();
            case (short)ServerPacketIds.NPCSRepair: 
                return new S.NPCSRepair();
            case (short)ServerPacketIds.NPCRefine:
                return new S.NPCRefine();
            case (short)ServerPacketIds.NPCCheckRefine:
                return new S.NPCCheckRefine();
            case (short)ServerPacketIds.NPCCollectRefine:
                return new S.NPCCollectRefine();
            case (short)ServerPacketIds.NPCReplaceWedRing:
                return new S.NPCReplaceWedRing();
            case (short)ServerPacketIds.NPCStorage:
                return new S.NPCStorage();
            case (short)ServerPacketIds.SellItem:
                return new S.SellItem();
            case (short)ServerPacketIds.CraftItem:
                return new S.CraftItem();
            case (short)ServerPacketIds.RepairItem:
                return new S.RepairItem();
            case (short)ServerPacketIds.ItemRepaired:
                return new S.ItemRepaired();
            case (short)ServerPacketIds.NewMagic:
                return new S.NewMagic();
            case (short)ServerPacketIds.MagicLeveled:
                return new S.MagicLeveled();
            case (short)ServerPacketIds.Magic:
                return new S.Magic();
            case (short)ServerPacketIds.MagicDelay:
                return new S.MagicDelay();
            case (short)ServerPacketIds.MagicCast:
                return new S.MagicCast();
            case (short)ServerPacketIds.ObjectMagic:
                return new S.ObjectMagic();
            case (short)ServerPacketIds.ObjectEffect:
                return new S.ObjectEffect();
            case (short)ServerPacketIds.RangeAttack:
                return new S.RangeAttack();
            case (short)ServerPacketIds.Pushed:
                return new S.Pushed();
            case (short)ServerPacketIds.ObjectPushed:
                return new S.ObjectPushed();
            case (short)ServerPacketIds.ObjectName:
                return new S.ObjectName();
            case (short)ServerPacketIds.UserStorage:
                return new S.UserStorage();
            case (short)ServerPacketIds.SwitchGroup:
                return new S.SwitchGroup();
            case (short)ServerPacketIds.DeleteGroup:
                return new S.DeleteGroup();
            case (short)ServerPacketIds.DeleteMember:
                return new S.DeleteMember();
            case (short)ServerPacketIds.GroupInvite:
                return new S.GroupInvite();
            case (short)ServerPacketIds.AddMember:
                return new S.AddMember();
            case (short)ServerPacketIds.Revived:
                return new S.Revived();
            case (short)ServerPacketIds.ObjectRevived:
                return new S.ObjectRevived();
            case (short)ServerPacketIds.SpellToggle:
                return new S.SpellToggle();
            case (short)ServerPacketIds.ObjectHealth:
                return new S.ObjectHealth();
            case (short)ServerPacketIds.MapEffect:
                return new S.MapEffect();
            case (short)ServerPacketIds.ObjectRangeAttack:
                return new S.ObjectRangeAttack();
            case (short)ServerPacketIds.AddBuff:
                return new S.AddBuff();
            case (short)ServerPacketIds.RemoveBuff:
                return new S.RemoveBuff();
            case (short)ServerPacketIds.ObjectHidden:
                return new S.ObjectHidden();
            case (short)ServerPacketIds.RefreshItem:
                return new S.RefreshItem();
            case (short)ServerPacketIds.ObjectSpell:
                return new S.ObjectSpell();
            case (short)ServerPacketIds.UserDash:
                return new S.UserDash();
            case (short)ServerPacketIds.ObjectDash:
                return new S.ObjectDash();
            case (short)ServerPacketIds.UserDashFail:
                return new S.UserDashFail();
            case (short)ServerPacketIds.ObjectDashFail:
                return new S.ObjectDashFail();
            case (short)ServerPacketIds.NPCConsign:
                return new S.NPCConsign();
            case (short)ServerPacketIds.NPCMarket:
                return new S.NPCMarket();
            case (short)ServerPacketIds.NPCMarketPage:
                return new S.NPCMarketPage();
            case (short)ServerPacketIds.ConsignItem:
                return new S.ConsignItem();
            case (short)ServerPacketIds.MarketFail:
                return new S.MarketFail();
            case (short)ServerPacketIds.MarketSuccess:
                return new S.MarketSuccess();
            case (short)ServerPacketIds.ObjectSitDown:
                return new S.ObjectSitDown();
            case (short)ServerPacketIds.InTrapRock:
                return new S.InTrapRock();
            case (short)ServerPacketIds.RemoveMagic:
                return new S.RemoveMagic();
            case (short)ServerPacketIds.BaseStatsInfo:
                return new S.BaseStatsInfo();
            case (short)ServerPacketIds.UserName:
                return new S.UserName();
            case (short)ServerPacketIds.ChatItemStats:
                return new S.ChatItemStats();
            case (short)ServerPacketIds.GuildMemberChange:
                return new S.GuildMemberChange();
            case (short)ServerPacketIds.GuildNoticeChange:
                return new S.GuildNoticeChange();
            case (short)ServerPacketIds.GuildStatus:
                return new S.GuildStatus();
            case (short)ServerPacketIds.GuildInvite:
                return new S.GuildInvite();
            case (short)ServerPacketIds.GuildExpGain:
                return new S.GuildExpGain();
            case (short)ServerPacketIds.GuildNameRequest:
                return new S.GuildNameRequest();
            case (short)ServerPacketIds.GuildStorageGoldChange:
                return new S.GuildStorageGoldChange();
            case (short)ServerPacketIds.GuildStorageItemChange:
                return new S.GuildStorageItemChange();
            case (short)ServerPacketIds.GuildStorageList:
                return new S.GuildStorageList();
            case (short)ServerPacketIds.GuildRequestWar:
                return new S.GuildRequestWar();
            case (short)ServerPacketIds.DefaultNPC:
                return new S.DefaultNPC();
            case (short)ServerPacketIds.NPCUpdate:
                return new S.NPCUpdate();
            case (short)ServerPacketIds.NPCImageUpdate:
                return new S.NPCImageUpdate();
            case (short)ServerPacketIds.MarriageRequest:
                return new S.MarriageRequest();
            case (short)ServerPacketIds.DivorceRequest:
                return new S.DivorceRequest();
            case (short)ServerPacketIds.MentorRequest:
                return new S.MentorRequest();
            case (short)ServerPacketIds.TradeRequest:
                return new S.TradeRequest();
            case (short)ServerPacketIds.TradeAccept:
                return new S.TradeAccept();
            case (short)ServerPacketIds.TradeGold:
                return new S.TradeGold();
            case (short)ServerPacketIds.TradeItem:
                return new S.TradeItem();
            case (short)ServerPacketIds.TradeConfirm:
                return new S.TradeConfirm();
            case (short)ServerPacketIds.TradeCancel:
                return new S.TradeCancel();
            case (short)ServerPacketIds.MountUpdate:
                return new S.MountUpdate();
            case (short)ServerPacketIds.TransformUpdate:
                return new S.TransformUpdate();
            case (short)ServerPacketIds.EquipSlotItem:
                return new S.EquipSlotItem();
            case (short)ServerPacketIds.FishingUpdate:
                return new S.FishingUpdate();
            case (short)ServerPacketIds.ChangeQuest:
                return new S.ChangeQuest();
            case (short)ServerPacketIds.CompleteQuest:
                return new S.CompleteQuest();
            case (short)ServerPacketIds.ShareQuest:
                return new S.ShareQuest();
            case (short)ServerPacketIds.NewQuestInfo:
                return new S.NewQuestInfo();
            case (short)ServerPacketIds.GainedQuestItem:
                return new S.GainedQuestItem();
            case (short)ServerPacketIds.DeleteQuestItem:
                return new S.DeleteQuestItem();
            case (short)ServerPacketIds.CancelReincarnation:
                return new S.CancelReincarnation();
            case (short)ServerPacketIds.RequestReincarnation:
                return new S.RequestReincarnation();
            case (short)ServerPacketIds.UserBackStep:
                return new S.UserBackStep();
            case (short)ServerPacketIds.ObjectBackStep:
                return new S.ObjectBackStep();
            case (short)ServerPacketIds.UserDashAttack:
                return new S.UserDashAttack();
            case (short)ServerPacketIds.ObjectDashAttack:
                return new S.ObjectDashAttack();
            case (short)ServerPacketIds.UserAttackMove://Warrior Skill - SlashingBurst
                return new S.UserAttackMove();
            case (short)ServerPacketIds.CombineItem:
                return new S.CombineItem();
            case (short)ServerPacketIds.ItemUpgraded:
                return new S.ItemUpgraded();
            case (short)ServerPacketIds.SetConcentration:
                return new S.SetConcentration();
            case (short)ServerPacketIds.SetObjectConcentration:
                return new S.SetObjectConcentration();
            case (short)ServerPacketIds.SetElemental:
                return new S.SetElemental();
            case (short)ServerPacketIds.SetObjectElemental:
                return new S.SetObjectElemental();
            case (short)ServerPacketIds.RemoveDelayedExplosion:
                return new S.RemoveDelayedExplosion();
            case (short)ServerPacketIds.ObjectDeco:
                return new S.ObjectDeco();
            case (short)ServerPacketIds.ObjectSneaking:
                return new S.ObjectSneaking();
            case (short)ServerPacketIds.ObjectLevelEffects:
                return new S.ObjectLevelEffects();
            case (short)ServerPacketIds.SetBindingShot:
                return new S.SetBindingShot();
            case (short)ServerPacketIds.SendOutputMessage:
                return new S.SendOutputMessage();
			case (short)ServerPacketIds.NPCAwakening:
                return new S.NPCAwakening();
            case (short)ServerPacketIds.NPCDisassemble:
                return new S.NPCDisassemble();
            case (short)ServerPacketIds.NPCDowngrade:
                return new S.NPCDowngrade();
            case (short)ServerPacketIds.NPCReset:
                return new S.NPCReset();
            case (short)ServerPacketIds.AwakeningNeedMaterials:
                return new S.AwakeningNeedMaterials();
            case (short)ServerPacketIds.AwakeningLockedItem:
                return new S.AwakeningLockedItem();
            case (short)ServerPacketIds.Awakening:
                return new S.Awakening();
            case (short)ServerPacketIds.ReceiveMail:
                return new S.ReceiveMail();
            case (short)ServerPacketIds.MailLockedItem:
                return new S.MailLockedItem();
            case (short)ServerPacketIds.MailSent:
                return new S.MailSent();
            case (short)ServerPacketIds.MailSendRequest:
                return new S.MailSendRequest();
            case (short)ServerPacketIds.ParcelCollected:
                return new S.ParcelCollected();
            case (short)ServerPacketIds.MailCost:
                return new S.MailCost();
			case (short)ServerPacketIds.ResizeInventory:
                return new S.ResizeInventory();
            case (short)ServerPacketIds.ResizeStorage:
                return new S.ResizeStorage();
            case (short)ServerPacketIds.NewIntelligentCreature:
                return new S.NewIntelligentCreature();
            case (short)ServerPacketIds.UpdateIntelligentCreatureList:
                return new S.UpdateIntelligentCreatureList();
            case (short)ServerPacketIds.IntelligentCreatureEnableRename:
                return new S.IntelligentCreatureEnableRename();
            case (short)ServerPacketIds.IntelligentCreaturePickup:
                return new S.IntelligentCreaturePickup();
            case (short)ServerPacketIds.NPCPearlGoods:
                return new S.NPCPearlGoods();
            case (short)ServerPacketIds.FriendUpdate:
                return new S.FriendUpdate();
            case (short)ServerPacketIds.LoverUpdate:
                return new S.LoverUpdate();
            case (short)ServerPacketIds.MentorUpdate:
                return new S.MentorUpdate();
            case (short)ServerPacketIds.GuildBuffList:
                return new S.GuildBuffList();
            case (short)ServerPacketIds.GameShopInfo:
                return new S.GameShopInfo();
            case (short)ServerPacketIds.GameShopStock:
                return new S.GameShopStock();
            case (short)ServerPacketIds.NPCRequestInput:
                return new S.NPCRequestInput();
            case (short)ServerPacketIds.Rankings:
                return new S.Rankings();
            case (short)ServerPacketIds.Opendoor:
                return new S.Opendoor();
            case (short)ServerPacketIds.GetRentedItems:
                return new S.GetRentedItems();
            case (short)ServerPacketIds.ItemRentalRequest:
                return new S.ItemRentalRequest();
            case (short)ServerPacketIds.ItemRentalFee:
                return new S.ItemRentalFee();
            case (short)ServerPacketIds.ItemRentalPeriod:
                return new S.ItemRentalPeriod();
            case (short)ServerPacketIds.DepositRentalItem:
                return new S.DepositRentalItem();
            case (short)ServerPacketIds.RetrieveRentalItem:
                return new S.RetrieveRentalItem();
            case (short)ServerPacketIds.UpdateRentalItem:
                return new S.UpdateRentalItem();
            case (short)ServerPacketIds.CancelItemRental:
                return new S.CancelItemRental();
            case (short)ServerPacketIds.ItemRentalLock:
                return new S.ItemRentalLock();
            case (short)ServerPacketIds.ItemRentalPartnerLock:
                return new S.ItemRentalPartnerLock();
            case (short)ServerPacketIds.CanConfirmItemRental:
                return new S.CanConfirmItemRental();
            case (short)ServerPacketIds.ConfirmItemRental:
                return new S.ConfirmItemRental();
            case (short)ServerPacketIds.NewRecipeInfo:
                return new S.NewRecipeInfo();
            case (short)ServerPacketIds.OnlinePlayers:
                return new S.OnlinePlayers();
            case (short)ServerPacketIds.RearrangeInventory:
                return new S.RearrangeInventory();

            case (short)ServerPacketIds.EEOpenSocketDialog:
                return new S.OpenSocketDialog();
            case (short)ServerPacketIds.EERuneSocketResult:
                return new S.AddRuneResult();
            case (short)ServerPacketIds.EERemoveRune:
                return new S.RemoveRune();
            case (short)ServerPacketIds.DownloadFile:
                return new S.DownloadFile();

            case (short)ServerPacketIds.HumUpPlayer://stupple
                return new S.HumUpPlayer();
            case (short)ServerPacketIds.ChangeNameRequest:
                return new S.ChangeNameRequest();
            default:
                return null;
        }
    }
}

public class BaseStats
{
    public float HpGain, HpGainRate, MpGainRate, BagWeightGain, WearWeightGain, HandWeightGain;
    public short MinAc, MaxAc, MinMac, MaxMac, MinDc, MaxDc, MinMc, MaxMc, MinSc, MaxSc, StartAgility, StartAccuracy, StartCriticalRate, StartCriticalDamage, CritialRateGain, CriticalDamageGain;

    public BaseStats(MirClass Job)
    {
        switch (Job)
        {
            case MirClass.Warrior:
                HpGain = 4F;
                HpGainRate = 4.5F;
                MpGainRate = 0;
                BagWeightGain = 3F;
                WearWeightGain = 20F;
                HandWeightGain = 13F;
                MinAc = 0;
                MaxAc = 7;
                MinMac = 0;
                MaxMac = 0;
                MinDc = 5;
                MaxDc = 5;
                MinMc = 0;
                MaxMc = 0;
                MinSc = 0;
                MaxSc = 0;
                StartAgility = 15;
                StartAccuracy = 5;
                StartCriticalRate = 0;
                StartCriticalDamage = 0;
                CritialRateGain = 0;
                CriticalDamageGain = 0;
                break;
            case MirClass.Wizard:
                HpGain = 15F;
                HpGainRate = 1.8F;
                MpGainRate = 0;
                BagWeightGain = 5F;
                WearWeightGain = 100F;
                HandWeightGain = 90F;
                MinAc = 0;
                MaxAc = 0;
                MinMac = 0;
                MaxMac = 0;
                MinDc = 7;
                MaxDc = 7;
                MinMc = 7;
                MaxMc = 7;
                MinSc = 0;
                MaxSc = 0;
                StartAgility = 15;
                StartAccuracy = 5;
                StartCriticalRate = 0;
                StartCriticalDamage = 0;
                CritialRateGain = 0;
                CriticalDamageGain = 0;
                break;
            case MirClass.Taoist:
                HpGain = 6F;
                HpGainRate = 2.5F;
                MpGainRate = 0;
                BagWeightGain = 4F;
                WearWeightGain = 50F;
                HandWeightGain = 42F;
                MinAc = 0;
                MaxAc = 0;
                MinMac = 12;
                MaxMac = 6;
                MinDc = 7;
                MaxDc = 7;
                MinMc = 0;
                MaxMc = 0;
                MinSc = 7;
                MaxSc = 7;
                StartAgility = 18;
                StartAccuracy = 5;
                StartCriticalRate = 0;
                StartCriticalDamage = 0;
                CritialRateGain = 0;
                CriticalDamageGain = 0;
                break;
            case MirClass.Assassin:
                HpGain = 4F;
                HpGainRate = 3.25F;
                MpGainRate = 0;
                BagWeightGain = 3.5F;
                WearWeightGain = 33F;
                HandWeightGain = 30F;
                MinAc = 0;
                MaxAc = 0;
                MinMac = 0;
                MaxMac = 0;
                MinDc = 8;
                MaxDc = 8;
                MinMc = 0;
                MaxMc = 0;
                MinSc = 0;
                MaxSc = 0;
                StartAgility = 20;
                StartAccuracy = 5;
                StartCriticalRate = 0;
                StartCriticalDamage = 0;
                CritialRateGain = 0;
                CriticalDamageGain = 0;
                break;
            case MirClass.Archer:
                HpGain = 4F;
                HpGainRate = 3.25F;
                MpGainRate = 0;
                BagWeightGain = 4F; //done
                WearWeightGain = 33F;
                HandWeightGain = 30F;
                MinAc = 0;
                MaxAc = 0;
                MinMac = 0;
                MaxMac = 0;
                MinDc = 8;
                MaxDc = 8;
                MinMc = 8;
                MaxMc = 8;
                MinSc = 0;
                MaxSc = 0;
                StartAgility = 15;
                StartAccuracy = 8;
                StartCriticalRate = 0;
                StartCriticalDamage = 0;
                CritialRateGain = 0;
                CriticalDamageGain = 0;
                break;
            case MirClass.Monk: //Monk
                HpGain = 6F;
                HpGainRate = 2.5F;
                MpGainRate = 0;
                BagWeightGain = 4F;
                WearWeightGain = 50F;
                HandWeightGain = 42F;
                MinAc = 0;
                MaxAc = 0;
                MinMac = 12;
                MaxMac = 6;
                MinDc = 7;
                MaxDc = 7;
                MinMc = 0;
                MaxMc = 0;
                MinSc = 7;
                MaxSc = 7;
                StartAgility = 18;
                StartAccuracy = 5;
                StartCriticalRate = 0;
                StartCriticalDamage = 0;
                CritialRateGain = 0;
                CriticalDamageGain = 0;
                break;
        }
    }
    public BaseStats(BinaryReader reader)
    {
        HpGain = reader.ReadSingle();
        HpGainRate = reader.ReadSingle();
        MpGainRate = reader.ReadSingle();
        MinAc = reader.ReadInt16();
        MaxAc = reader.ReadInt16();
        MinMac = reader.ReadInt16();
        MaxMac = reader.ReadInt16();
        MinDc = reader.ReadInt16();
        MaxDc = reader.ReadInt16();
        MinMc = reader.ReadInt16();
        MaxMc = reader.ReadInt16();
        MinSc = reader.ReadInt16();
        MaxSc = reader.ReadInt16();
        StartAccuracy = reader.ReadInt16();
        StartAgility = reader.ReadInt16();
        StartCriticalRate = reader.ReadInt16();
        StartCriticalDamage = reader.ReadInt16();
        CritialRateGain = reader.ReadInt16();
        CriticalDamageGain = reader.ReadInt16();
        BagWeightGain = reader.ReadSingle();
        WearWeightGain = reader.ReadSingle();
        HandWeightGain = reader.ReadSingle();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(HpGain);
        writer.Write(HpGainRate);
        writer.Write(MpGainRate);
        writer.Write(MinAc);
        writer.Write(MaxAc);
        writer.Write(MinMac);
        writer.Write(MaxMac);
        writer.Write(MinDc);
        writer.Write(MaxDc);
        writer.Write(MinMc);
        writer.Write(MaxMc);
        writer.Write(MinSc);
        writer.Write(MaxSc);
        writer.Write(StartAccuracy);
        writer.Write(StartAgility);
        writer.Write(StartCriticalRate);
        writer.Write(StartCriticalDamage);
        writer.Write(CritialRateGain);
        writer.Write(CriticalDamageGain);
        writer.Write(BagWeightGain);
        writer.Write(WearWeightGain);
        writer.Write(HandWeightGain);
    }
}
public class RandomItemStat
{
    public byte MaxDuraChance, MaxDuraStatChance, MaxDuraMaxStat;
    public byte MaxAcChance, MaxAcStatChance, MaxAcMaxStat, MaxMacChance, MaxMacStatChance, MaxMacMaxStat, MaxDcChance, MaxDcStatChance, MaxDcMaxStat, MaxMcChance, MaxMcStatChance, MaxMcMaxStat, MaxScChance, MaxScStatChance, MaxScMaxStat;
    public byte AccuracyChance, AccuracyStatChance, AccuracyMaxStat, AgilityChance, AgilityStatChance, AgilityMaxStat, HpChance, HpStatChance, HpMaxStat, MpChance, MpStatChance, MpMaxStat, StrongChance, StrongStatChance, StrongMaxStat;
    public byte MagicResistChance, MagicResistStatChance, MagicResistMaxStat, PoisonResistChance, PoisonResistStatChance, PoisonResistMaxStat;
    public byte HpRecovChance, HpRecovStatChance, HpRecovMaxStat, MpRecovChance, MpRecovStatChance, MpRecovMaxStat, PoisonRecovChance, PoisonRecovStatChance, PoisonRecovMaxStat;
    public byte CriticalRateChance, CriticalRateStatChance, CriticalRateMaxStat, CriticalDamageChance, CriticalDamageStatChance, CriticalDamageMaxStat;
    public byte FreezeChance, FreezeStatChance, FreezeMaxStat, PoisonAttackChance, PoisonAttackStatChance, PoisonAttackMaxStat;
    public byte AttackSpeedChance, AttackSpeedStatChance, AttackSpeedMaxStat, LuckChance, LuckStatChance, LuckMaxStat;
    public byte CurseChance;

    public RandomItemStat(ItemType Type = ItemType.Book)
    {
        switch (Type)
        {
            case ItemType.Weapon:
                SetWeapon();
                break;
            case ItemType.Armour:
                SetArmour();
                break;
            case ItemType.Helmet:
                SetHelmet();
                break;
            case ItemType.Belt:
            case ItemType.Boots:
                SetBeltBoots();
                break;
            case ItemType.Necklace:
                SetNecklace();
                break;
            case ItemType.Bracelet:
                SetBracelet();
                break;
            case ItemType.Ring:
                SetRing();
                break; 
            case ItemType.Mount:
                SetMount();
                break;
        }
    }

    public void SetWeapon()
    {
        MaxDuraChance = 2;
        MaxDuraStatChance = 13;
        MaxDuraMaxStat = 13;
        
        MaxDcChance = 15;
        MaxDcStatChance = 15;
        MaxDcMaxStat = 13;

        MaxMcChance = 20;
        MaxMcStatChance = 15;
        MaxMcMaxStat = 13;

        MaxScChance = 20;
        MaxScStatChance = 15;
        MaxScMaxStat = 13;

        AttackSpeedChance = 60;
        AttackSpeedStatChance = 30;
        AttackSpeedMaxStat = 3;
        
        StrongChance = 24;
        StrongStatChance = 20;
        StrongMaxStat = 2;

        AccuracyChance = 30;
        AccuracyStatChance = 20;
        AccuracyMaxStat = 2;
    }
    public void SetArmour()
    {
        MaxDuraChance = 2;
        MaxDuraStatChance = 10;
        MaxDuraMaxStat = 3;

        MaxAcChance = 30;
        MaxAcStatChance = 15;
        MaxAcMaxStat = 7;

        MaxMacChance = 30;
        MaxMacStatChance = 15;
        MaxMacMaxStat = 7;

        MaxDcChance = 40;
        MaxDcStatChance = 20;
        MaxDcMaxStat = 7;

        MaxMcChance = 40;
        MaxMcStatChance = 20;
        MaxMcMaxStat = 7;

        MaxScChance = 40;
        MaxScStatChance = 20;
        MaxScMaxStat = 7;

    }
    public void SetHelmet()
    {
        MaxDuraChance = 2;
        MaxDuraStatChance = 10;
        MaxDuraMaxStat = 3;

        MaxAcChance = 30;
        MaxAcStatChance = 15;
        MaxAcMaxStat = 7;

        MaxMacChance = 30;
        MaxMacStatChance = 15;
        MaxMacMaxStat = 7;

        MaxDcChance = 40;
        MaxDcStatChance = 20;
        MaxDcMaxStat = 7;

        MaxMcChance = 40;
        MaxMcStatChance = 20;
        MaxMcMaxStat = 7;

        MaxScChance = 40;
        MaxScStatChance = 20;
        MaxScMaxStat = 7;
    }
    public void SetBeltBoots()
    {
        MaxDuraChance = 2;
        MaxDuraStatChance = 10;
        MaxDuraMaxStat = 3;

        MaxAcChance = 30;
        MaxAcStatChance = 30;
        MaxAcMaxStat = 3;

        MaxMacChance = 30;
        MaxMacStatChance = 30;
        MaxMacMaxStat = 3;

        MaxDcChance = 30;
        MaxDcStatChance = 30;
        MaxDcMaxStat = 3;

        MaxMcChance = 30;
        MaxMcStatChance = 30;
        MaxMcMaxStat = 3;

        MaxScChance = 30;
        MaxScStatChance = 30;
        MaxScMaxStat = 3;

        AgilityChance = 60;
        AgilityStatChance = 30;
        AgilityMaxStat = 3;
    }
    public void SetNecklace()
    {
        MaxDuraChance = 2;
        MaxDuraStatChance = 10;
        MaxDuraMaxStat = 3;

        MaxDcChance = 15;
        MaxDcStatChance = 30;
        MaxDcMaxStat = 7;

        MaxMcChance = 15;
        MaxMcStatChance = 30;
        MaxMcMaxStat = 7;

        MaxScChance = 15;
        MaxScStatChance = 30;
        MaxScMaxStat = 7;

        AccuracyChance = 60;
        AccuracyStatChance = 30;
        AccuracyMaxStat = 7;

        AgilityChance = 60;
        AgilityStatChance = 30;
        AgilityMaxStat = 7;
    }
    public void SetBracelet()
    {
        MaxDuraChance = 2;
        MaxDuraStatChance = 10;
        MaxDuraMaxStat = 3;

        MaxAcChance = 20;
        MaxAcStatChance = 30;
        MaxAcMaxStat = 6;

        MaxMacChance = 20;
        MaxMacStatChance = 30;
        MaxMacMaxStat = 6;

        MaxDcChance = 30;
        MaxDcStatChance = 30;
        MaxDcMaxStat = 6;

        MaxMcChance = 30;
        MaxMcStatChance = 30;
        MaxMcMaxStat = 6;

        MaxScChance = 30;
        MaxScStatChance = 30;
        MaxScMaxStat = 6;
    }
    public void SetRing()
    {
        MaxDuraChance = 2;
        MaxDuraStatChance = 10;
        MaxDuraMaxStat = 3;

        MaxAcChance = 25;
        MaxAcStatChance = 20;
        MaxAcMaxStat = 6;

        MaxMacChance = 25;
        MaxMacStatChance = 20;
        MaxMacMaxStat = 6;

        MaxDcChance = 15;
        MaxDcStatChance = 30;
        MaxDcMaxStat = 6;

        MaxMcChance = 15;
        MaxMcStatChance = 30;
        MaxMcMaxStat = 6;

        MaxScChance = 15;
        MaxScStatChance = 30;
        MaxScMaxStat = 6;
    }

    public void SetMount()
    {
        SetRing();
    }
}

public class ChatItem
{
    public long RecievedTick = 0;
    public ulong ID = 0;
    public UserItem ItemStats;
}

public class UserId
{
    public long Id = 0;
    public string UserName = "";
}

#region ItemSets

public class ItemSets
{
    public ItemSet Set;
    public List<ItemType> Type;
    private byte Amount
    {
        get
        {
            switch (Set)
            {
                case ItemSet.Mundane:
                case ItemSet.NokChi:
                case ItemSet.TaoProtect:
                case ItemSet.Whisker1:
                case ItemSet.Whisker2:
                case ItemSet.Whisker3:
                case ItemSet.Whisker4:
                case ItemSet.Whisker5:
                    return 2;
                case ItemSet.RedOrchid:
                case ItemSet.RedFlower:
                case ItemSet.Smash:
                case ItemSet.HwanDevil:
                case ItemSet.Purity:
                case ItemSet.FiveString:
                case ItemSet.Bone:
                case ItemSet.Bug:
                    return 3;
                case ItemSet.Recall:
                    return 4;
                case ItemSet.Spirit:
                case ItemSet.WhiteGold:
                case ItemSet.WhiteGoldH:
                case ItemSet.RedJade:
                case ItemSet.RedJadeH:
                case ItemSet.Nephrite:
                case ItemSet.NephriteH:
                case ItemSet.Hyeolryong:
                case ItemSet.Monitor:
                case ItemSet.Oppressive:
                case ItemSet.Paeok:
                case ItemSet.Sulgwan:
                    return 5;
                default:
                    return 0;
            }
        }
    }
    public byte Count;
    public bool SetComplete
    {
        get
        {
            return Count == Amount;
        }
    }
}

#endregion

#region "Mine Related"
public class MineSet
{
    public string Name = string.Empty;
    public byte SpotRegenRate = 5;
    public byte MaxStones = 80;
    public byte HitRate = 25;
    public byte DropRate = 10;
    public byte TotalSlots = 100;
    public List<MineDrop> Drops = new List<MineDrop>();
    public bool DropsSet = false;

    public MineSet(byte MineType = 0)
    {
        switch (MineType)
        {
            case 1:
                TotalSlots = 120;
                Drops.Add(new MineDrop(){ItemName = "GoldOre", MinSlot = 1, MaxSlot = 2, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10});
                Drops.Add(new MineDrop() { ItemName = "SilverOre", MinSlot = 3, MaxSlot = 20, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10 });
                Drops.Add(new MineDrop() { ItemName = "CopperOre", MinSlot = 21, MaxSlot = 45, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10 });
                Drops.Add(new MineDrop() { ItemName = "BlackIronOre", MinSlot = 46, MaxSlot = 56, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10 });
                break;
            case 2:
                TotalSlots = 100;
                Drops.Add(new MineDrop(){ItemName = "PlatinumOre", MinSlot = 1, MaxSlot = 2, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10});
                Drops.Add(new MineDrop() { ItemName = "RubyOre", MinSlot = 3, MaxSlot = 20, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10 });
                Drops.Add(new MineDrop() { ItemName = "NephriteOre", MinSlot = 21, MaxSlot = 45, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10 });
                Drops.Add(new MineDrop() { ItemName = "AmethystOre", MinSlot = 46, MaxSlot = 56, MinDura = 3, MaxDura = 16, BonusChance = 20, MaxBonusDura = 10 });
                break;
        }
    }

    public void SetDrops(Dictionary<int, ItemInfo> items)
    {
        if (DropsSet) return;
        for (int i = 0; i < Drops.Count; i++)
        {
            foreach(var info in items.Values)
            {
                if (String.Compare(info.Name.Replace(" ", ""), Drops[i].ItemName, StringComparison.OrdinalIgnoreCase) != 0) continue;
                Drops[i].Item = info;
                break;
            }
        }
        DropsSet = true;
    }
}

public class MineSpot
{
 }

public class MineDrop
{
    public string ItemName;
    public ItemInfo Item;
    public byte MinSlot = 0;
    public byte MaxSlot = 0;
    public byte MinDura = 1;
    public byte MaxDura = 1;
    public byte BonusChance = 0;
    public byte MaxBonusDura = 1;
}

public class MineZone
{
    public byte Mine;
    public Point Location;
    public ushort Size;

    public MineZone()
    {
    }

    public MineZone(BinaryReader reader)
    {
        Location = new Point(reader.ReadInt32(), reader.ReadInt32());
        Size = reader.ReadUInt16();
        Mine = reader.ReadByte();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Location.X);
        writer.Write(Location.Y);
        writer.Write(Size);
        writer.Write(Mine);
    }
    public override string ToString()
    {
        return string.Format("Mine: {0}- {1}", Functions.PointToString(Location), Mine);
    }
}
#endregion

#region "Guild Related"
public class ItemVolume
{
    public ItemInfo Item;
    public string ItemName;
    public uint Amount;
}

public class Rank
{
    public List<GuildMember> Members = new List<GuildMember>();
    public string Name = "";
    public int Index = 0;
    public RankOptions Options = (RankOptions)0;
    public Rank() 
    {
    }
    public Rank(BinaryReader reader, bool Offline = false)
    {
        Name = reader.ReadString();
        Options = (RankOptions)reader.ReadByte();
        if (!Offline)
            Index = reader.ReadInt32();
        int Membercount = reader.ReadInt32();
        for (int j = 0; j < Membercount; j++)
            Members.Add(new GuildMember(reader, Offline));
    }
    public void Save(BinaryWriter writer, bool Save = false)
    {
        writer.Write(Name);
        writer.Write((byte)Options);
        if (!Save)
            writer.Write(Index);
        writer.Write(Members.Count);
        for (int j = 0; j < Members.Count; j++)
            Members[j].save(writer);
    }
}

public class GuildStorageItem
{
    public UserItem Item;
    public long UserId = 0;
    public GuildStorageItem()
    {
    }
    public GuildStorageItem(BinaryReader reader)
    {
        Item = new UserItem(reader);
        UserId = reader.ReadInt64();
    }
    public void save(BinaryWriter writer)
    {
        Item.Save(writer);
        writer.Write(UserId);
    }
}

public class GuildMember
{
    public string name = "";
    public int Id;
    public object Player;
    public DateTime LastLogin;
    public bool hasvoted;
    public bool Online;

    public GuildMember()
    {}
    public GuildMember(BinaryReader reader, bool Offline = false)
    {
        name = reader.ReadString();
        Id = reader.ReadInt32();
        LastLogin = DateTime.FromBinary(reader.ReadInt64());
        hasvoted = reader.ReadBoolean();
        Online = reader.ReadBoolean();
        Online = Offline ? false: Online;
    }
    public void save(BinaryWriter writer)
    {
        writer.Write(name);
        writer.Write(Id);
        writer.Write(LastLogin.ToBinary());
        writer.Write(hasvoted);
        writer.Write(Online);
    }
}

[Flags]
public enum RankOptions : byte
{
    CanChangeRank = 1,
    CanRecruit = 2,
    CanKick = 4,
    CanStoreItem = 8,
    CanRetrieveItem = 16,
    CanAlterAlliance = 32,
    CanChangeNotice = 64,
    CanActivateBuff = 128
}

public class GuildBuffInfo
{
    public int Id;
    public int Icon = 0;
    public string name = "";
    public byte LevelRequirement;
    public byte PointsRequirement = 1;
    public int TimeLimit;
    public int ActivationCost;
    public byte BuffAc;
    public byte BuffMac;
    public byte BuffDc;
    public byte BuffMc;
    public byte BuffSc;
    public byte BuffAttack;
    public int  BuffMaxHp;
    public int  BuffMaxMp;
    public byte BuffMineRate;
    public byte BuffGemRate;
    public byte BuffFishRate;
    public byte BuffExpRate;
    public byte BuffCraftRate;
    public byte BuffSkillRate;
    public byte BuffHpRegen;
    public byte BuffMPRegen;
    
    public byte BuffDropRate;
    public byte BuffGoldRate;

    public GuildBuffInfo()
    {

    }

    public GuildBuffInfo(BinaryReader reader)
    {
        Id = reader.ReadInt32();
        Icon = reader.ReadInt32();
        name = reader.ReadString();
        LevelRequirement = reader.ReadByte();
        PointsRequirement = reader.ReadByte();
        TimeLimit = reader.ReadInt32();
        ActivationCost = reader.ReadInt32();
        BuffAc = reader.ReadByte();
        BuffMac = reader.ReadByte();
        BuffDc = reader.ReadByte();
        BuffMc = reader.ReadByte();
        BuffSc = reader.ReadByte();
        BuffMaxHp = reader.ReadInt32();
        BuffMaxMp = reader.ReadInt32();
        BuffMineRate = reader.ReadByte();
        BuffGemRate = reader.ReadByte();
        BuffFishRate = reader.ReadByte();
        BuffExpRate = reader.ReadByte();
        BuffCraftRate = reader.ReadByte();
        BuffSkillRate = reader.ReadByte();
        BuffHpRegen = reader.ReadByte();
        BuffMPRegen = reader.ReadByte();
        BuffAttack = reader.ReadByte();
        BuffDropRate = reader.ReadByte();
        BuffGoldRate = reader.ReadByte();
    }

    public GuildBuffInfo(InIReader reader, int i)
    {
        Id  = reader.ReadInt32("Buff-" + i.ToString(), "Id",0);
        Icon = reader.ReadInt32("Buff-" + i.ToString(), "Icon", 0);
        name = reader.ReadString("Buff-" + i.ToString(), "Name","");
        LevelRequirement = reader.ReadByte("Buff-" + i.ToString(), "LevelReq",0);
        PointsRequirement = reader.ReadByte("Buff-" + i.ToString(), "PointsReq",1);
        TimeLimit = reader.ReadInt32("Buff-" + i.ToString(), "TimeLimit",0);;
        ActivationCost = reader.ReadInt32("Buff-" + i.ToString(), "ActivationCost",0);
        BuffAc = reader.ReadByte("Buff-" + i.ToString(), "BuffAc",0);
        BuffMac = reader.ReadByte("Buff-" + i.ToString(), "BuffMAC",0);
        BuffDc = reader.ReadByte("Buff-" + i.ToString(), "BuffDc",0);
        BuffMc = reader.ReadByte("Buff-" + i.ToString(), "BuffMc",0);
        BuffSc = reader.ReadByte("Buff-" + i.ToString(), "BuffSc",0);
        BuffMaxHp = reader.ReadInt32("Buff-" + i.ToString(), "BuffMaxHp",0);
        BuffMaxMp = reader.ReadInt32("Buff-" + i.ToString(), "BuffMaxMp",0);
        BuffMineRate = reader.ReadByte("Buff-" + i.ToString(), "BuffMineRate",0);
        BuffGemRate = reader.ReadByte("Buff-" + i.ToString(), "BuffGemRate",0);
        BuffFishRate = reader.ReadByte("Buff-" + i.ToString(), "BuffFishRate",0);
        BuffExpRate = reader.ReadByte("Buff-" + i.ToString(), "BuffExpRate",0);
        BuffCraftRate = reader.ReadByte("Buff-" + i.ToString(), "BuffCraftRate",0);
        BuffSkillRate = reader.ReadByte("Buff-" + i.ToString(), "BuffSkillRate",0);
        BuffHpRegen = reader.ReadByte("Buff-" + i.ToString(), "BuffHpRegen",0);
        BuffMPRegen = reader.ReadByte("Buff-" + i.ToString(), "BuffMpRegen",0);
        BuffAttack = reader.ReadByte("Buff-" + i.ToString(), "BuffAttack",0);
        BuffDropRate = reader.ReadByte("Buff-" + i.ToString(), "BuffDropRate",0);
        BuffGoldRate = reader.ReadByte("Buff-" + i.ToString(), "BuffGoldRate",0);
    }

    public void Save(InIReader reader, int i)
    {
        reader.Write("Buff-" + i.ToString(), "Id", Id);
        reader.Write("Buff-" + i.ToString(), "Icon", Icon);
        reader.Write("Buff-" + i.ToString(), "Name", name);
        reader.Write("Buff-" + i.ToString(), "LevelReq", LevelRequirement);
        reader.Write("Buff-" + i.ToString(), "PointsReq", PointsRequirement);
        reader.Write("Buff-" + i.ToString(), "TimeLimit", TimeLimit); ;
        reader.Write("Buff-" + i.ToString(), "ActivationCost", ActivationCost); ;
        reader.Write("Buff-" + i.ToString(), "BuffAc", BuffAc); ;
        reader.Write("Buff-" + i.ToString(), "BuffMAC", BuffMac); ;
        reader.Write("Buff-" + i.ToString(), "BuffDc", BuffDc); ;
        reader.Write("Buff-" + i.ToString(), "BuffMc", BuffMc); ;
        reader.Write("Buff-" + i.ToString(), "BuffSc", BuffSc); ;
        reader.Write("Buff-" + i.ToString(), "BuffMaxHp", BuffMaxHp); ;
        reader.Write("Buff-" + i.ToString(), "BuffMaxMp", BuffMaxMp); ;
        reader.Write("Buff-" + i.ToString(), "BuffMineRate", BuffMineRate); ;
        reader.Write("Buff-" + i.ToString(), "BuffGemRate", BuffGemRate); ;
        reader.Write("Buff-" + i.ToString(), "BuffFishRate", BuffFishRate); ;
        reader.Write("Buff-" + i.ToString(), "BuffExpRate", BuffExpRate); ;
        reader.Write("Buff-" + i.ToString(), "BuffCraftRate", BuffCraftRate); ;
        reader.Write("Buff-" + i.ToString(), "BuffSkillRate", BuffSkillRate); ;
        reader.Write("Buff-" + i.ToString(), "BuffHpRegen", BuffHpRegen); ;
        reader.Write("Buff-" + i.ToString(), "BuffMpRegen", BuffMPRegen); ;
        reader.Write("Buff-" + i.ToString(), "BuffAttack", BuffAttack); ;
        reader.Write("Buff-" + i.ToString(), "BuffDropRate", BuffDropRate); ;
        reader.Write("Buff-" + i.ToString(), "BuffGoldRate", BuffGoldRate); ;
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Id);
        writer.Write(Icon);
        writer.Write(name);
        writer.Write(LevelRequirement);
        writer.Write(PointsRequirement);
        writer.Write(TimeLimit);
        writer.Write(ActivationCost);
        writer.Write(BuffAc);
        writer.Write(BuffMac);
        writer.Write(BuffDc);
        writer.Write(BuffMc);
        writer.Write(BuffSc);
        writer.Write(BuffMaxHp);
        writer.Write(BuffMaxMp);
        writer.Write(BuffMineRate);
        writer.Write(BuffGemRate);
        writer.Write(BuffFishRate);
        writer.Write(BuffExpRate);
        writer.Write(BuffCraftRate);
        writer.Write(BuffSkillRate);
        writer.Write(BuffHpRegen);
        writer.Write(BuffMPRegen);
        writer.Write(BuffAttack);
        writer.Write(BuffDropRate);
        writer.Write(BuffGoldRate);
    }

    public override string ToString()
    {
        return string.Format("{0}: {1}", Id, name);
    }

 
}

public class GuildBuff
{
    public int Id;
    public GuildBuffInfo Info;
    public bool Active = false;
    public int ActiveTimeRemaining;

    public bool UsingGuildSkillIcon
    {
        get { return Info != null && Info.Icon < 1000; }
    }

    public GuildBuff()
    {
    }

    public GuildBuff(BinaryReader reader)
    {
        Id = reader.ReadInt32();
        Active = reader.ReadBoolean();
        ActiveTimeRemaining = reader.ReadInt32();
    }
    public void Save(BinaryWriter writer)
    {
        writer.Write(Id);
        writer.Write(Active);
        writer.Write(ActiveTimeRemaining);
    }

    public string PrintTimeSpan(double secs)
    {
        TimeSpan t = TimeSpan.FromMinutes(secs);
        string answer;
        if (t.TotalMinutes < 1.0)
        {
            answer = string.Format("{0}s", t.Seconds);
        }
        else if (t.TotalHours < 1.0)
        {
            answer = string.Format("{0}ms", t.Minutes);
        }
        else // more than 1 hour
        {
            answer = string.Format("{0}h {1:D2}m ", (int)t.TotalHours, t.Minutes);
        }

        return answer;
    }
    
}

//outdated but cant delete it or old db's wont load
public class GuildBuffOld
{
    public GuildBuffOld()
    { }
    public GuildBuffOld(BinaryReader reader)
    {
        reader.ReadByte();
        reader.ReadInt64();
    }
}

#endregion

#region Ranking Pete107|Petesn00beh 15/1/2016
public class Rank_Character_Info
{
    public long PlayerId;
    public string Name;
    public MirClass Class;
    public int level;
    //public int rank;
    public long Experience;//clients shouldnt care about this only server
    public object info;//again only keep this on server!

    public Rank_Character_Info()
    {

    }
    public Rank_Character_Info(BinaryReader reader)
    {
        //rank = reader.ReadInt32();
        PlayerId = reader.ReadInt64();
        Name = reader.ReadString();
        level = reader.ReadInt32();
        Class = (MirClass)reader.ReadByte();

    }
    public void Save(BinaryWriter writer)
    {
        //writer.Write(rank);
        writer.Write(PlayerId);
        writer.Write(Name);
        writer.Write(level);
        writer.Write((byte)Class);
    }
}
#endregion

public class Door
{
    public byte index;
    public byte DoorState;//0: closed, 1: opening, 2: open, 3: closing
    public byte ImageIndex;
    public long LastTick;
    public int X;
    public int Y;
}

public class ItemRentalInformation
{
    public ulong ItemId;
    public string ItemName;
    public string RentingPlayerName;
    public DateTime ItemReturnDate;

    public ItemRentalInformation()
    { }

    public ItemRentalInformation(BinaryReader reader)
    {
        ItemId = reader.ReadUInt64();
        ItemName = reader.ReadString();
        RentingPlayerName = reader.ReadString();
        ItemReturnDate = DateTime.FromBinary(reader.ReadInt64());
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(ItemId);
        writer.Write(ItemName);
        writer.Write(RentingPlayerName);
        writer.Write(ItemReturnDate.ToBinary());
    }
}

public class ClientRecipeInfo
{
    public UserItem Item;
    public List<UserItem> Ingredients = new List<UserItem>();
    public int SuccessRate;

    public ClientRecipeInfo()
    {

    }

    public ClientRecipeInfo(BinaryReader reader)
    {
        Item = new UserItem(reader);

        int count = reader.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            Ingredients.Add(new UserItem(reader));
        }
        SuccessRate = reader.ReadInt32();
    }

    public void Save(BinaryWriter writer)
    {
        Item.Save(writer);

        writer.Write(Ingredients.Count);
        foreach (var ingredient in Ingredients)
        {
            ingredient.Save(writer);
        }
        writer.Write(SuccessRate);
    }
}

public class GameServerInfo
{
    public string Name;
    public byte RegionID;
    public string Ip;
    public int Port;
    public bool IsOpen;

    public GameServerInfo()
    {

    }

    public GameServerInfo(BinaryReader reader)
    {
        Name = reader.ReadString();
        RegionID = reader.ReadByte();
        Ip = reader.ReadString();
        Port = reader.ReadInt32();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Name);
        writer.Write(RegionID);
        writer.Write(Ip);
        writer.Write(Port);
    }
}

public class NameChecker
{
    private static readonly Regex Reg = new Regex(@"^[A-Za-z0-9-\u4E00-\u9FA5]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");//sldz:角色名允许中文

    private static readonly Regex ChineseReg = new Regex(@"[\u4E00-\u9FA5]");

    private static bool CheckLength(string text, int minLen, int maxLen)
    {
        int len = 0;
        for (int i = 0; i < text.Length; i++)
        {
            string temp = text.Substring(i, 1);
            if (ChineseReg.IsMatch(temp))
                len += 2;
            else
                len++;
        }

        return len <= maxLen && len >= minLen;
    }

    public static bool CheckCharacterName(string text)
    {
        return Reg.IsMatch(text) && CheckLength(text, Globals.MinCharacterNameLength, Globals.MaxCharacterNameLength);
    }

    public static bool CheckGuildName(string text)
    {
        return Reg.IsMatch(text) && CheckLength(text, Globals.MinGuildNameLength, Globals.MaxGuildNameLength);
    }
}


class InIAttribute : Attribute
{
    public string Section;
    public bool SaveToFile;
    public InIAttribute(string section, bool bSave = true)
    {
        this.Section = section;
        this.SaveToFile = bSave;
    }

    public static void SetIni<T>(string key, string value) where T : class, new()
    {
        key = key.Substring(1);
        Type objType = typeof(T);
        //取属性上的自定义特性
        foreach (FieldInfo fieldInfo in objType.GetFields())
        {
            object[] objAttrs = fieldInfo.GetCustomAttributes(typeof(InIAttribute), true);
            if (objAttrs.Length <= 0)
                continue;

            InIAttribute attr = objAttrs[0] as InIAttribute;
            if (attr == null)
                continue;

            if (string.Compare(fieldInfo.Name, key, StringComparison.OrdinalIgnoreCase) != 0)
                continue;

            if (fieldInfo.FieldType == typeof(bool))
            {
                bool result;
                bool.TryParse(value, out result);
                fieldInfo.SetValue(null, result);
            }
            else if (fieldInfo.FieldType == typeof(int))
            {
                int result;
                int.TryParse(value, out result);
                fieldInfo.SetValue(null, value);
            }
            else if (fieldInfo.FieldType == typeof(string))
            {
                fieldInfo.SetValue(null, value);
            }
        }
    }


    public static void ReadInI<T>(InIReader reader) where T : class, new()
    {
        Type objType = typeof(T);
        //取属性上的自定义特性
        foreach (FieldInfo fieldInfo in objType.GetFields())
        {
            object[] objAttrs = fieldInfo.GetCustomAttributes(typeof(InIAttribute), true);
            if (objAttrs.Length <= 0)
                continue;

            InIAttribute attr = objAttrs[0] as InIAttribute;
            if (attr == null)
                continue;

            if (reader.FindValue(attr.Section, fieldInfo.Name) == null)
                continue;

            if (fieldInfo.FieldType == typeof(bool))
            {
                bool value = reader.ReadBoolean(attr.Section, fieldInfo.Name, false);
                fieldInfo.SetValue(null, value);
            }
            else if (fieldInfo.FieldType == typeof(int))
            {
                int value = reader.ReadInt32(attr.Section, fieldInfo.Name, 0);
                fieldInfo.SetValue(null, value);
            }
            else if (fieldInfo.FieldType == typeof(string))
            {
                string value = reader.ReadString(attr.Section, fieldInfo.Name, "");
                fieldInfo.SetValue(null, value);
            }
            else if (fieldInfo.FieldType == typeof(ushort))
            {
                ushort value = reader.ReadUInt16(attr.Section, fieldInfo.Name, 0);
                fieldInfo.SetValue(null, value);
            }
            else if (fieldInfo.FieldType == typeof(byte))
            {
                byte value = reader.ReadByte(attr.Section, fieldInfo.Name, 0);
                fieldInfo.SetValue(null, value);
            }
            else if (fieldInfo.FieldType == typeof(short))
            {
                short value = reader.ReadInt16(attr.Section, fieldInfo.Name, 0);
                fieldInfo.SetValue(null, value);
            }
            else
                throw new Exception("Un support exception." + fieldInfo.FieldType);
        }
    }

    public static void WriteInI<T>(InIReader reader) where T : class, new()
    {
        Type objType = typeof(T);
        //取属性上的自定义特性
        foreach (FieldInfo fieldInfo in objType.GetFields())
        {
            object[] objAttrs = fieldInfo.GetCustomAttributes(typeof(InIAttribute), true);

            if (objAttrs.Length <= 0)
                continue;

            InIAttribute attr = objAttrs[0] as InIAttribute;
            if (attr == null)
                continue;

            if (!attr.SaveToFile)
                continue;

            if (fieldInfo.FieldType == typeof(bool))
                reader.Write(attr.Section, fieldInfo.Name, (bool)fieldInfo.GetValue(null));
            else if (fieldInfo.FieldType == typeof(int))
                reader.Write(attr.Section, fieldInfo.Name, (int)fieldInfo.GetValue(null));
            else if (fieldInfo.FieldType == typeof(string))
                reader.Write(attr.Section, fieldInfo.Name, (string)fieldInfo.GetValue(null));
            else if (fieldInfo.FieldType == typeof(ushort))
                reader.Write(attr.Section, fieldInfo.Name, (ushort)fieldInfo.GetValue(null));
            else if (fieldInfo.FieldType == typeof(byte))
                reader.Write(attr.Section, fieldInfo.Name, (byte)fieldInfo.GetValue(null));
            else if (fieldInfo.FieldType == typeof(short))
                reader.Write(attr.Section, fieldInfo.Name, (short)fieldInfo.GetValue(null));
            else
                throw new Exception("Un support exception." + fieldInfo.FieldType);
        }
    }
}

public class HumanMonsterSettings
{
    public string HumansName = string.Empty;
    public MirClass MobsClass;
    public MirGender MobsGender;
    public short Weapon, Armour;
    public byte Wing, Hair, Light;

    public HumanMonsterSettings() { }
}

