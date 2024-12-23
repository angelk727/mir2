public enum MouseCursor : byte
{
    None,
    Default,
    Attack,
    AttackRed,
    NPCTalk,
    TextPrompt,
    Trash,
    Upgrade
}
//[Flags]
public enum WeatherSetting : ushort
{
    无效果 = 0,
    雾天 = 1,
    红色余烬 = 2,
    白色余烬 = 4,
    黄色余烬 = 8,
    落雪 = 16,
    飘雪 = 32,
    雨天 = 64,
    黄色花瓣 = 128,
    红色花瓣 = 256,
    粉色花瓣 = 512,
    沙尘 = 1024,
    沙雾 = 2048,
}
public enum PanelType : byte
{
    Buy,
    BuySub,
    Craft,

    Sell,
    Repair,
    SpecialRepair,
    Consign,
    Refine,
    CheckRefine,
    Disassemble,
    Downgrade,
    Reset,
    CollectRefine,
    ReplaceWedRing,
}

public enum MarketItemType : byte
{
    Consign,
    Auction,
    GameShop
}

public enum MarketPanelType : byte
{
    Market,
    Consign,
    Auction,
    GameShop
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
    HpRegen = 3,
    Poisoning = 4
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
    物理攻击,
    魔法攻击,
    道术攻击,
    物理防御,
    魔法防御,
    生命法力值,
}

[Flags]
public enum LevelEffects : ushort
{
    None = 0,
    Mist = 1,
    RedDragon = 2,
    BlueDragon = 4,
    Rebirth1 = 8,
    Rebirth2 = 16,
    Rebirth3 = 32,
    NewBlue = 64,
    YellowDragon = 128,
    Phoenix = 256
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
    普通 = 1,
    宝物 = 2,
    圣物 = 3,
    神物 = 4,
    英雄 = 5,
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
    一般 = 0,
    每日 = 1,
    重复 = 2,
    主线 = 3
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

public enum QuestAction : byte
{
    TimeExpired
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
    Client
}

public enum IntelligentCreatureType : byte
{
    None = 99,
    小猪 = 0,
    小鸡 = 1,
    小猫 = 2,
    精灵骷髅 = 3,
    白猪 = 4,
    纸片人 = 5,
    黑猫 = 6,
    龙蛋 = 7,
    火娃 = 8,
    雪人 = 9,
    青蛙 = 10,
    红猴 = 11,
    愤怒的小鸟 = 12,
    阿福 = 13,
    治疗拉拉 = 14,
    猫咪超人 = 15,
    龙宝宝 = 16,
}

//2 blank mob files
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
    WoomaTaurus = 34, //BOSS
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
    EvilSnake = 49,
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
    RedMoonEvil = 62,
    BigRat = 63,
    ZumaArcher = 64,
    ZumaStatue = 65,
    ZumaGuardian = 66,
    RedThunderZuma = 67,
    ZumaTaurus = 68, //BOSS
    DigOutZombie = 69,
    ClZombie = 70,
    NdZombie = 71,
    CrawlerZombie = 72,
    ShamanZombie = 73,
    Ghoul = 74,
    KingScorpion = 75,
    KingHog = 76,
    DarkDevil = 77,
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
    BoneLord = 93, //BOSS
    Minotaur = 94,
    IceMinotaur = 95,
    ElectricMinotaur = 96,
    WindMinotaur = 97,
    FireMinotaur = 98,
    RightGuard = 99,
    LeftGuard = 100,
    MinotaurKing = 101, //BOSS
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
    Yimoogi = 113,
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
    OmaKing = 126, //BOSS
    BlackFoxman = 127,
    RedFoxman = 128,
    WhiteFoxman = 129,
    TrapRock = 130,
    GuardianRock = 131,
    ThunderElement = 132,
    CloudElement = 133,
    GreatFoxSpirit = 134, //BOSS
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
    Behemoth = 158, //BOSS
    DarkDevourer = 159,
    PoisonHugger = 160,
    Hugger = 161,
    MutatedHugger = 162,
    DreamDevourer = 163,
    Treasurebox = 164,
    SnowPile = 165,
    Snowman = 166,
    SnowTree = 167,
    GiantEgg = 168,
    RedTurtle = 169,
    GreenTurtle = 170,
    BlueTurtle = 171,
    Catapult1 = 172, //SPECIAL TODO
    Catapult2 = 173, //SPECIAL TODO
    OldSpittingSpider = 174,
    SiegeRepairman = 175, //SPECIAL TODO
    BlueSanta = 176,
    BattleStandard = 177,
    Blank1 = 178,
    RedYimoogi = 179,
    LionRiderMale = 180, //Not Monster - Skin / Transform
    LionRiderFemale = 181, //Not Monster - Skin / Transform
    Tornado = 182,
    FlameTiger = 183,
    WingedTigerLord = 184, //BOSS
    TowerTurtle = 185,
    FinialTurtle = 186,
    TurtleKing = 187, //BOSS
    DarkTurtle = 188,
    LightTurtle = 189,
    DarkSwordOma = 190,
    DarkAxeOma = 191,
    DarkCrossbowOma = 192,
    DarkWingedOma = 193,
    BoneWhoo = 194,
    DarkSpider = 195, //AI 8
    ViscusWorm = 196,
    ViscusCrawler = 197,
    CrawlerLave = 198,
    DarkYob = 199,
    FlamingMutant = 200,
    StoningStatue = 201, //BOSS
    FlyingStatue = 202,
    ValeBat = 203,
    Weaver = 204,
    VenomWeaver = 205,
    CrackingWeaver = 206,
    ArmingWeaver = 207,
    CrystalWeaver = 208,
    FrozenZumaStatue = 209,
    FrozenZumaGuardian = 210,
    FrozenRedZuma = 211,
    GreaterWeaver = 212,
    SpiderWarrior = 213,
    SpiderBarbarian = 214,
    HellSlasher = 215,
    HellPirate = 216,
    HellCannibal = 217,
    HellKeeper = 218, //BOSS
    HellBolt = 219,
    WitchDoctor = 220,
    ManectricHammer = 221,
    ManectricClub = 222,
    ManectricClaw = 223,
    ManectricStaff = 224,
    NamelessGhost = 225,
    DarkGhost = 226,
    ChaosGhost = 227,
    ManectricBlest = 228,
    ManectricKing = 229,
    Blank2 = 230,
    IcePillar = 231,
    FrostYeti = 232,
    ManectricSlave = 233,
    TrollHammer = 234,
    TrollBomber = 235,
    TrollStoner = 236,
    TrollKing = 237, //BOSS
    FlameSpear = 238,
    FlameMage = 239,
    FlameScythe = 240,
    FlameAssassin = 241,
    FlameQueen = 242, //BOSS
    HellKnight1 = 243,
    HellKnight2 = 244,
    HellKnight3 = 245,
    HellKnight4 = 246,
    HellLord = 247, //BOSS
    WaterGuard = 248,
    IceGuard = 249,
    ElementGuard = 250,
    DemonGuard = 251,
    KingGuard = 252,
    Snake10 = 253,
    Snake11 = 254,
    Snake12 = 255,
    Snake13 = 256,
    Snake14 = 257,
    Snake15 = 258,
    Snake16 = 259,
    Snake17 = 260,
    DeathCrawler = 261,
    BurningZombie = 262,
    MudZombie = 263,
    FrozenZombie = 264,
    UndeadWolf = 265,
    DemonWolf = 266,
    WhiteMammoth = 267,
    DarkBeast = 268,
    LightBeast = 269,//AI 112
    BloodBaboon = 270, //AI 112
    HardenRhino = 271,
    AncientBringer = 272,
    FightingCat = 273,
    FireCat = 274, //AI 44
    CatWidow = 275, //AI 112
    StainHammerCat = 276,
    BlackHammerCat = 277,
    StrayCat = 278,
    CatShaman = 279,
    Jar1 = 280,
    Jar2 = 281,
    SeedingsGeneral = 282,
    RestlessJar = 283,
    GeneralMeowMeow = 284, //BOSS
    Bunny = 285,
    Tucson = 286,
    TucsonFighter = 287, //AI 44
    TucsonMage = 288,
    TucsonWarrior = 289,
    Armadillo = 290,
    ArmadilloElder = 291,
    TucsonEgg = 292, //EFFECT 0/1
    PlaguedTucson = 293,
    SandSnail = 294,
    CannibalTentacles = 295,
    TucsonGeneral = 296, //BOSS
    GasToad = 297,
    Mantis = 298,
    SwampWarrior = 299,

    AssassinBird = 300,
    RhinoWarrior = 301,
    RhinoPriest = 302,
    ElephantMan = 303,
    StoneGolem = 304,
    EarthGolem = 305,
    TreeGuardian = 306,
    TreeQueen = 307,
    PeacockSpider = 308,
    DarkBaboon = 309, //AI 112
    TwinHeadBeast = 310, //AI 112
    OmaCannibal = 311,
    OmaBlest = 312,
    OmaSlasher = 313,
    OmaAssassin = 314,
    OmaMage = 315,
    OmaWitchDoctor = 316,
    LightningBead = 317, //Effect 0, AI 149
    HealingBead = 318, //Effect 1, AI 149
    PowerUpBead = 319, //Effect 2, AI 14
    DarkOmaKing = 320, //BOSS
    CaveStatue = 321,
    Mandrill = 322,
    PlagueCrab = 323,
    CreeperPlant = 324,
    FloatingWraith = 325, //AI 8
    ArmedPlant = 326,
    AvengerPlant = 327,
    Nadz = 328,
    AvengingSpirit = 329,
    AvengingWarrior = 330,
    AxePlant = 331,
    WoodBox = 332,
    ClawBeast = 333, //AI 8
    DarkCaptain = 334, //BOSS
    SackWarrior = 335,
    WereTiger = 336, //AI 112
    KingHydrax = 337,
    Hydrax = 338,
    HornedMage = 339,
    BlueSoul = 340,
    HornedArcher = 341,
    ColdArcher = 342,
    HornedWarrior = 343,
    FloatingRock = 344,
    ScalyBeast = 345,
    HornedSorceror = 346,
    BoulderSpirit = 347,
    HornedCommander = 348, //BOSS

    MoonStone = 349,
    SunStone = 350,
    LightningStone = 351,
    Turtlegrass = 352,
    ManTree = 353,
    Bear = 354,  //Effect 1, AI 112
    Leopard = 355,
    ChieftainArcher = 356,
    ChieftainSword = 357, //BOSS TODO
    StoningSpider = 358, //Archer Spell mob (not yet coded)
    VampireSpider = 359, //Archer Spell mob
    SpittingToad = 360, //Archer Spell mob
    SnakeTotem = 361, //Archer Spell mob
    CharmedSnake = 362, //Archer Spell mob
    FrozenSoldier = 363,
    FrozenFighter = 364, //AI 44
    FrozenArcher = 365, //AI 8
    FrozenKnight = 366,
    FrozenGolem = 367,
    IcePhantom = 368, //TODO - AI needs revisiting (blue explosion and snakes)
    SnowWolf = 369,
    SnowWolfKing = 370, //BOSS
    WaterDragon = 371,
    BlackTortoise = 372,
    Manticore = 373, //TODO
    DragonWarrior = 374, //Done (DG)
    DragonArcher = 375, //TODO - Wind Arrow spell
    Kirin = 376, // Done (jxtulong)
    Guard3 = 377,
    ArcherGuard3 = 378,
    Bunny2 = 379,
    FrozenMiner = 380, // Done (jxtulong)
    FrozenAxeman = 381, // Done (jxtulong)
    FrozenMagician = 382, // Done (jxtulong)
    SnowYeti = 383, // Done (jxtulong)
    IceCrystalSoldier = 384, // Done (jxtulong)
    DarkWraith = 385, // Done (jxtulong)
    DarkSpirit = 386, // Use AI 8 (AxeSkeleton)
    CrystalBeast = 387,
    RedOrb = 388,
    BlueOrb = 389,
    YellowOrb = 390,
    GreenOrb = 391,
    WhiteOrb = 392,
    FatalLotus = 393,
    AntCommander = 394,
    CargoBoxwithlogo = 395, // Done - Use CargoBox AI.
    Doe = 396, // TELEPORT = EFFECT 9
    Reindeer = 397, //frames not added
    AngryReindeer = 398,
    CargoBox = 399, // Done - Basically a Pinata.

    Ram1 = 400,
    Ram2 = 401,
    Kite = 402,
    PurpleFaeFlower = 403,
    Furball = 404,
    GlacierSnail = 405,
    FurbolgWarrior = 406,
    FurbolgArcher = 407,
    FurbolgCommander = 408,
    RedFaeFlower = 409,
    FurbolgGuard = 410,
    GlacierBeast = 411,
    GlacierWarrior = 412,
    ShardGuardian = 413,
    WarriorScroll = 414, // Use AI "HoodedSummonerScrolls" - Info.Effect = 0
    TaoistScroll = 415, // Use AI "HoodedSummonerScrolls" - Info.Effect = 1
    WizardScroll = 416, // Use AI "HoodedSummonerScrolls" - Info.Effect = 2
    AssassinScroll = 417, // Use AI "HoodedSummonerScrolls" - Info.Effect = 3
    HoodedSummoner = 418, //Summons Scrolls
    HoodedIceMage = 419,
    HoodedPriest = 420,
    ShardMaiden = 421,
    KingKong = 422,
    WarBear = 423,
    ReaperPriest = 424,
    ReaperWizard = 425,
    ReaperAssassin = 426,
    LivingVines = 427,
    BlueMonk = 428,
    MutantBeserker = 429,
    MutantGuardian = 430,
    MutantHighPriest = 431,
    MysteriousMage = 432,
    FeatheredWolf = 433,
    MysteriousAssassin = 434,
    MysteriousMonk = 435,
    ManEatingPlant = 436,
    HammerDwarf = 437,
    ArcherDwarf = 438,
    NobleWarrior = 439,
    NobleArcher = 440,
    NoblePriest = 441,
    NobleAssassin = 442,
    Swain = 443,
    RedMutantPlant = 444,
    BlueMutantPlant = 445,
    UndeadHammerDwarf = 446,
    UndeadDwarfArcher = 447,
    AncientStoneGolem = 448,
    Serpentirian = 449,

    Butcher = 450,
    Riklebites = 451,
    FeralTundraFurbolg = 452,
    FeralFlameFurbolg = 453,
    ArcaneTotem = 454,
    SpectralWraith = 455,
    BabyMagmaDragon = 456,
    BloodLord = 457,
    SerpentLord = 458,
    MirEmperor = 459,
    MutantManEatingPlant = 460,
    MutantWarg = 461,
    GrassElemental = 462,
    RockElemental = 463,

    Mon472N = 472,

    CallScroll = 477,
    PoisonScroll = 478,
    FireballScroll = 479,
    LightningScroll = 480,  //Mon49.wil
    //ExplosiveSkull = 506,
    //SeaFire = 507,
    Swain1 = 509,

    Mon517P = 517,
    Mon519D = 519,
    Mon520P = 520,  //Mon53.wil

    EnhanceFlameFurbolg = 523,
    BlueArcaneTotem = 525,
    GreenArcaneTotem = 526,
    RedArcaneTotem = 527,
    YellowArcaneTotem = 528,
    WarpGate = 529,
    Mon540N = 540,  //Mon55.wil
    Mon541N = 541,
    Mon542N = 542,
    Mon543N = 543,
    Mon544S = 544,
    Mon545N = 545,
    Mon546T = 546,
    Mon547N = 547,
    Mon548N = 548,
    Mon549N = 549,
    Mon550N = 550,  //Mon56.wil
    Mon551N = 551,
    Mon552N = 552,
    Mon553N = 553,
    Mon554N = 554,
    Mon555N = 555,
    Mon556B = 556,
    Mon557B = 557,
    Mon558B = 558,
    Mon559B = 559,
    Mon560N = 560,  //Mon57.wil
    Mon561N = 561,
    Mon562N = 562,
    Mon563N = 563,
    Mon564N = 564,
    Mon565T = 565,
    Mon570B = 570,  //Mon58.wil
    Mon571B = 571,
    Mon572B = 572,
    Mon573B = 573,
    Mon574T = 574,
    Mon575S = 575,
    Mon576T = 576,
    Mon577N = 577,
    Mon578N = 578,
    Mon579B = 579,
    Mon580B = 580,  //Mon59.wil
    Mon581D = 581,
    Mon582D = 582,
    Mon583N = 583,
    Mon584D = 584,
    Mon585D = 585,
    Mon586N = 586,
    Mon587N = 587,
    Mon588N = 588,
    Mon589N = 589,
    Mon590N = 590,  //Mon60.wil
    Mon591N = 591,
    Mon592N = 592,
    Mon593N = 593,
    Mon594N = 594,
    Mon595N = 595,
    Mon596N = 596,
    Mon597N = 597,
    Mon598N = 598,
    Mon599N = 599,
    Mon600N = 600,  //Mon61.wil
    Mon601N = 601,
    Mon602N = 602,
    Mon603B = 603,
    Mon604N = 604,
    Mon605N = 605,
    Mon606N = 606,
    Mon607N = 607,
    Mon608N = 608,
    Mon609N = 609,
    Mon610B = 610,
    //B=Boss D=Door N=Normal P=Peculiar S=Stoned T=Tree

    //Special
    EvilMir = 900,
    EvilMirBody = 901,
    DragonStatue = 902,
    HellBomb1 = 903,
    HellBomb2 = 904,
    HellBomb3 = 905,

    //Siege
    Catapult = 940,
    ChariotBallista = 941,
    Ballista = 942,
    Trebuchet = 943,
    CanonTrebuchet = 944,

    //Gates
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
    NammandGate1 = 960,
    NammandGate2 = 961,
    SabukWallSection = 962,
    NammandWallSection = 963,
    FrozenDoor = 964,
    GonRyunDoor = 965,
    UnderPassDoor1 = 966,
    UnderPassDoor2 = 967,
    InDunFences = 968,

    //Flags 1000 ~ 1100

    //Creatures
    小猪 = 10000,//Permanent
    小鸡 = 10001,//Special
    小猫 = 10002,//Permanent
    精灵骷髅 = 10003,//Special
    白猪 = 10004,//Special
    纸片人 = 10005,//Event
    黑猫 = 10006,//unknown
    龙蛋 = 10007,//unknown
    火娃 = 10008,//unknown
    雪人 = 10009,//unknown
    青蛙 = 10010,//unknown
    红猴 = 10011,//unknown
    愤怒的小鸟 = 10012,
    阿福 = 10013,
    治疗拉拉 = 10014,
    猫咪超人 = 10015,
    龙宝宝 = 10016,
}

public enum MirAction : byte
{
    站立动作,
    行走动作,
    跑步动作,
    推开动作,
    左冲动作,
    右冲动作,
    冲击失败,
    站立姿势,
    站立姿势2,
    近距攻击1,
    近距攻击2,
    近距攻击3,
    近距攻击4,
    近距攻击5,
    远程攻击1,
    远程攻击2,
    远程攻击3,
    特殊攻击,
    被击动作,
    挖矿展示,
    施法动作,
    死亡动作,
    死后尸体,
    挖后尸骸,
    石化苏醒,
    切换LIB,
    石化状态,
    召唤初现,
    复活动作,
    坐下动作,
    挖矿动作,
    刺客潜行,
    刺客冲击,
    刺客步刺,

    弓箭行走,
    弓箭奔跑,
    弓箭跳跃,

    坐骑站立,
    坐骑行走,
    坐骑奔跑,
    坐骑被击,
    坐骑攻击,

    钓鱼抛竿,
    钓鱼等待,
    钓鱼收线
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
    男性 = 0,
    女性 = 1
}

public enum MirClass : byte
{
    战士 = 0,
    法师 = 1,
    道士 = 2,
    刺客 = 3,
    弓箭 = 4
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
    None = 0,
    Player = 1,
    Item = 2,
    Merchant = 3,
    Spell = 4,
    Monster = 5,
    Deco = 6,
    Creature = 7,
    Hero = 8
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
    Shout3 = 15,
    LineMessage = 16,
}

public enum ItemType : byte
{
    杂物 = 0,
    武器 = 1,
    盔甲 = 2,
    头盔 = 4,
    项链 = 5,
    手镯 = 6,
    戒指 = 7,
    护身符 = 8,
    腰带 = 9,
    靴子 = 10,
    守护石 = 11,
    照明物 = 12,
    药水 = 13,
    矿石 = 14,
    肉 = 15,
    工艺材料 = 16,
    卷轴 = 17,
    宝玉神珠 = 18,
    坐骑 = 19,
    技能书 = 20,
    特殊消耗品 = 21,
    缰绳 = 22,
    铃铛 = 23,
    马鞍 = 24,
    蝴蝶结 = 25,
    面甲 = 26,
    坐骑食物 = 27,
    鱼钩 = 28,
    鱼漂 = 29,
    鱼饵 = 30,
    探鱼器 = 31,
    摇轮 = 32,
    鱼 = 33,
    任务物品 = 34,
    觉醒物品 = 35,
    灵物 = 36,
    外形物品 = 37,
    装饰 = 38,
    镶嵌宝石 = 39,
    怪物蛋 = 40,
    攻城弹药 = 41, //TODO
    封印 = 42,
    攻击型绝技 = 43,
    防御型绝技 = 44,
    技能型绝技 = 45,
    绝技材料 = 46
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
    Socket = 20,
    HeroEquipment = 21,
    HeroInventory = 22,
    HeroHPItem = 23,
    HeroMPItem = 24
}

public enum EquipmentSlot : byte
{
    武器 = 0,
    盔甲 = 1,
    头盔 = 2,
    照明物 = 3,
    项链 = 4,
    左手镯 = 5,
    右手镯 = 6,
    左戒指 = 7,
    右戒指 = 8,
    护身符 = 9,
    腰带 = 10,
    靴子 = 11,
    守护石 = 12,
    坐骑 = 13,
    变身 = 14,
    盾牌 = 15
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
    FocusMasterTarget = 4
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
    Blindness = 512,
    Dazed = 1024
}

[Flags]

public enum BindMode : short
{
    None = 0,
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
    UnableToDisassemble = 8192,
    NoMail = 16384,
    NoHero = -32768
}

[Flags]
public enum SpecialItemMode : short
{
    None = 0,
    Paralize = 0x0001,
    Teleport = 0x0002,
    ClearRing = 0x0004,
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
    战士 = 1,
    法师 = 2,
    道士 = 4,
    刺客 = 8,
    弓箭 = 16,
    武僧 = 32,
    战士刺客 = 战士 | 刺客 ,
    战法道 = 战士 | 法师 | 道士,
    全职业 = 战法道 | 刺客 | 弓箭
}

[Flags]
public enum RequiredGender : byte
{
    男性 = 1,
    女性 = 2,
    性别不限 = 男性 | 女性
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
    非套装 = 0,
    祈祷套装 = 1,
    记忆套装 = 2,
    赤兰套装 = 3,
    密火套装 = 4,
    破碎套装 = 5,
    幻魔石套 = 6,
    灵玉套装 = 7,
    五玄套装 = 8,
    世轮套装 = 9,
    绿翠套装 = 10,
    道护套装 = 11,
    天龙套装 = 12,
    白骨套装 = 13,
    虫血套装 = 14,
    白金套装 = 15,
    强白金套 = 16,
    红玉套装 = 17,
    强红玉套 = 18,
    软玉套装 = 19,
    强软玉套 = 20,
    贵人战套 = 21,
    贵人法套 = 22,
    贵人道套 = 23,
    贵人刺套 = 24,
    贵人弓套 = 25,
    龙血套装 = 26,
    监视套装 = 27,
    暴压套装 = 28,
    贝玉套装 = 29,
    黑术套装 = 30,
    青玉套装 = 31,
    鏃未套装 = 38,
    强青玉套 = 39,
    圣龙套装 = 40,
    神龙套装 = 41
}

public enum Spell : byte
{
    None = 0,

    //Warrior
    Fencing = 1,
    Slaying = 2,
    Thrusting = 3,
    HalfMoon = 4,
    ShoulderDash = 5,
    TwinDrakeBlade = 6,
    Entrapment = 7,
    FlamingSword = 8,
    LionRoar = 9,
    CrossHalfMoon = 10,
    BladeAvalanche = 11,
    ProtectionField = 12,
    Rage = 13,
    CounterAttack = 14,
    SlashingBurst = 15,
    Fury = 16,
    ImmortalSkin = 17,
    EntrapmentRare = 18,
    ImmortalSkinRare = 19,
    LionRoarRare = 20,
    DimensionalSword = 21,
    DimensionalSwordRare = 22,

    //Wizard
    FireBall = 31,
    Repulsion = 32,
    ElectricShock = 33,
    GreatFireBall = 34,
    HellFire = 35,
    ThunderBolt = 36,
    Teleport = 37,
    FireBang = 38,
    FireWall = 39,
    Lightning = 40,
    FrostCrunch = 41,
    ThunderStorm = 42,
    MagicShield = 43,
    TurnUndead = 44,
    Vampirism = 45,
    IceStorm = 46,
    FlameDisruptor = 47,
    Mirroring = 48,
    FlameField = 49,
    Blizzard = 50,
    MagicBooster = 51,
    MeteorStrike = 52,
    IceThrust = 53,
    FastMove = 54,
    StormEscape = 55,
    HeavenlySecrets = 56,
    GreatFireBallRare = 57,
    StormEscapeRare = 58,

    //Taoist
    Healing = 61,
    SpiritSword = 62,
    Poisoning = 63,
    SoulFireBall = 64,
    SummonSkeleton = 65,
    Hiding = 67,
    MassHiding = 68,
    SoulShield = 69,
    Revelation = 70,
    BlessedArmour = 71,
    EnergyRepulsor = 72,
    TrapHexagon = 73,
    Purification = 74,
    MassHealing = 75,
    Hallucination = 76,
    UltimateEnhancer = 77,
    SummonShinsu = 78,
    Reincarnation = 79,
    SummonHolyDeva = 80,
    Curse = 81,
    Plague = 82,
    PoisonCloud = 83,
    EnergyShield = 84,
    PetEnhancer = 85,
    HealingCircle = 86,
    HealingRare = 87,
    HealingcircleRare = 88,

    //Assassin
    FatalSword = 91,
    DoubleSlash = 92,
    Haste = 93,
    FlashDash = 94,
    LightBody = 95,
    HeavenlySword = 96,
    FireBurst = 97,
    Trap = 98,
    PoisonSword = 99,
    MoonLight = 100,
    MPEater = 101,
    SwiftFeet = 102,
    DarkBody = 103,
    Hemorrhage = 104,
    CrescentSlash = 105,
    MoonMist = 106,
    CatTongue = 107,

    //Archer
    Focus = 121,
    StraightShot = 122,
    DoubleShot = 123,
    ExplosiveTrap = 124,
    DelayedExplosion = 125,
    Meditation = 126,
    BackStep = 127,
    ElementalShot = 128,
    Concentration = 129,
    Stonetrap = 130,
    ElementalBarrier = 131,
    SummonVampire = 132,
    VampireShot = 133,
    SummonToad = 134,
    PoisonShot = 135,
    CrippleShot = 136,
    SummonSnakes = 137,
    NapalmShot = 138,
    OneWithNature = 139,
    BindingShot = 140,
    MentalState = 141,

    //Custom
    Blink = 151,
    Portal = 152,
    BattleCry = 153,
    FireBounce = 154,
    MeteorShower = 155,

    //Map Events
    DigOutZombie = 200,
    Rubble = 201,
    MapLightning = 202,
    MapLava = 203,
    MapQuake1 = 204,
    MapQuake2 = 205,
    DigOutArmadillo = 206,
    FlyingStatueIceTornado = 207, //259
    GeneralMeowMeowThunder = 208, //341
    TucsonGeneralRock = 209, //354
    StoneGolemQuake = 210, //362
    EarthGolemPile = 211, //363
    TreeQueenRoot = 212, //365
    TreeQueenMassRoots = 213, //365
    TreeQueenGroundRoots = 214, //365
    DarkOmaKingNuke = 215, //378
    HornedSorcererDustTornado = 216, //406
    Mon409BRockFall = 217,
    Mon409BRockSpike = 218,
    Mon409BShield = 219,
    YangDragonFlame = 220, //414
    YangDragonIcyBurst = 221, //414
    ShardGuardianIceBomb = 222, //476
    GroundFissure = 223, //498
    SkeletonBomb = 224, //508
    FlameExplosion = 225, //509
    ButcherFlyAxe = 226, //516
    RiklebitesBlast = 227, //518
    RiklebitesRollCall = 228, //518
    SwordFormation = 229, //550
    Mon564NWhirlwind = 230,
    Mon570BRupture = 231,
    Mon570BLightningCloud = 232,
    Mon571BFireBomb = 233,
    Mon572BFlame = 234,
    Mon572BDarkVortex = 235,
    Mon573BBigCobweb = 236,
    Mon580BPoisonousMist = 237,
    Mon580BDenseFog = 238,
    Mon580BRoot = 239,
    Mon603BWhirlPool = 240,
    Mon609NBomb = 241
}

public enum SpellEffect : byte
{
    None,
    FatalSword,
    Teleport,
    Healing,
    HealingRare,
    RedMoonEvil,
    TwinDrakeBlade,
    MagicShieldUp,
    MagicShieldDown,
    GreatFoxSpirit,
    Entrapment,
    EntrapmentRare,
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
    StormEscapeRare,
    TurtleKing,
    Behemoth,
    Stunned,
    IcePillar,
    KingGuard,
    KingGuard2,    
    DeathCrawlerBreath,
    FlamingMutantWeb,
    FurbolgWarriorCritical,
    Tester,
	MoonMist,
    HealingcircleRare,
    HealingcircleRare1,
    BloodthirstySpike,
    GroundBurstIce,
    MirEmperor,
    Mon562NLightning,
    Mon563NPoisonCloud,
    Mon564NFlame,
    Mon572BLightning,
    Mon573BCobweb,
    Mon580BLightning,
    Mon580BSpikeTrap
}


public enum BuffType : byte
{
    None = 0,

    //Magics
    时间之殇,
    隐身术,
    体迅风,
    轻身步,
    血龙剑法,
    幽灵盾,
    神圣战甲术,
    风身术,
    无极真气,
    护身气幕,
    剑气爆,
    诅咒术,
    月影术,
    烈火身,
    气流术,
    吸血地闪,
    毒魔闪,
    天务,
    精神状态,
    先天气功,
    深延术,
    血龙水,
    金刚不坏,
    金刚不坏秘籍,
    魔法盾,
    金刚术,
    天上秘术,

    //Monster
    HornedArcherBuff = 50,
    ColdArcherBuff,
    HornedColdArcherBuff,
    GeneralMeowMeowShield,
    惩戒真言,
    御体之力,
    HornedWarriorShield,
    Mon409BShieldBuff,
    失明状态,
    ChieftainSwordBuff,
    寒冰护甲,
    ReaperPriestBuff,
    至尊威严,
    伤口加深,
    死亡印记,
    RiklebitesShield,
    麻痹状态,
    绝对封锁,
    Mon564NSealing,
    烈火焚烧,
    防御诅咒,
    Mon579BShield,
    Mon580BShield,

    //Special
    游戏管理 = 100,
    General,
    获取经验提升,
    物品掉落提升,
    金币辉煌,
    背包负重提升,
    变形效果,
    心心相映,
    衣钵相传,
    火传穷薪,
    公会特效,
    Prison,
    精力充沛,
    技巧项链,
    隐身戒指,
    新人特效,
    技能经验提升,
    英雄灵气,
    暗影侵袭,
    攻击型绝技,
    防御型绝技,
    技能型绝技,
    共用型绝技,

    //Stats
    攻击力提升 = 200,
    魔法力提升,
    道术力提升,
    攻击速度提升,
    生命值提升,
    法力值提升,
    防御提升,
    魔法防御提升,
    奇异药水,
    包容万斤,
    龍之祝福,
    准确命中提升,
    敏捷躲避提升,
    安息之气,
    远古气息,
    华丽雨光,
    龙之特效,
    龙的特效,
    组队加成,
    强化队伍,
    天灵水,
    玉清水,
    甜筒HP,
    甜筒MP,
    内尔族的灵药,
    摩鲁的赤色药剂,
    摩鲁的青色药剂,
    摩鲁的黄色药剂,
    古代宗师祝福,
    黄金宗师祝福,
    DogYoLin7,
    落物纷飞
}

[Flags]
public enum BuffProperty : byte
{
    None = 0,
    RemoveOnDeath = 1,
    RemoveOnExit = 2,
    Debuff = 4,
    PauseInSafeZone = 8
}

public enum BuffStackType : byte
{
    None,
    ResetDuration,
    StackDuration,
    StackStat,
    StackStatAndDuration,
    Infinite,
    ResetStat,
    ResetStatAndDuration,
    AttrStackStat,
    AttrStackStatAndDuration
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
    NewMapInfo,
    WorldMapSetup,
    SearchMapResult,
    UserInformation,
    UserSlotsRefresh,
    UserLocation,
    ObjectPlayer,
    ObjectHero,
    ObjectRemove,
    ObjectTurn,
    ObjectWalk,
    ObjectRun,
    Chat,
    ObjectChat,
    NewItemInfo,
    NewHeroInfo,
    NewChatItem,
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
    TakeBackHeroItem,
    TransferHeroItem,
    PlayerUpdate,
    PlayerInspect,
    LogOutSuccess,
    LogOutFailed,
    ReturnToLogin,
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
    HeroHealthChanged,
    DeleteItem,
    Death,
    ObjectDied,
    ColourChanged,
    ObjectColourChanged,
    ObjectGuildNameChanged,
    GainExperience,
    GainHeroExperience,
    LevelChanged,
    HeroLevelChanged,
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
    ItemSlotSizeChanged,
    ItemSealChanged,
    NewMagic,
    RemoveMagic,
    MagicLeveled,
    Magic,
    MagicDelay,
    MagicCast,
    ObjectMagic,
    ObjectEffect,
    ObjectProjectile,
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
    ObjectMana,
    MapEffect,
    AllowObserve,
    ObjectRangeAttack,
    AddBuff,
    RemoveBuff,
    PauseBuff,
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
    HeroBaseStatsInfo,
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
    HeroCreateRequest,
    NewHero,
    HeroInformation,
    UpdateHeroSpawnState,
    UnlockHeroAutoPot,
    SetAutoPotValue,
    SetAutoPotItem,
    SetHeroBehaviour,
    ManageHeroes,
    ChangeHero,
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
    SetElemental,
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
    OpenBrowser,
    PlaySound,
    SetTimer,
    ExpireTimer,
    UpdateNotice,
    Roll,
    SetCompass,
    GroupMembersMap,
    SendMemberLocation,
    InventoryCollating,
    StorageCollating,
}

public enum ClientPacketIds : short
{
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
    TakeBackHeroItem,
    TransferHeroItem,
    DropGold,
    PickUp,
    RequestMapInfo,
    TeleportToNPC,
    SearchMap,
    Inspect,
    Observe,
    ChangeAMode,
    ChangePMode,
    ChangeTrade,
    Attack,
    RangeAttack,
    Harvest,
    CallNPC,
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
    NewHero,
    SetAutoPotValue,
    SetAutoPotItem,
    SetHeroBehaviour,
    ChangeHero,
    TownRevive,
    SpellToggle,
    ConsignItem,
    MarketSearch,
    MarketRefresh,
    MarketPage,
    MarketBuy,
    MarketGetBack,
    MarketSellNow,
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
    RequestIntelligentCreatureUpdates,

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
    ConfirmItemRental
}

public enum ConquestType : byte
{
    申请启动 = 0,
    Auto = 1,
    强制启动 = 2,
}

public enum ConquestGame : byte
{
    占领皇宫 = 0,
    争夺国王 = 1,
    随机模式 = 2,
    经典模式 = 3,
    征服模式 = 4
}

[Flags]
public enum GuildRankOptions : byte
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

public enum DoorState : byte
{
    Closed = 0,
    Opening = 1,
    Open = 2,
    Closing = 3
}

public enum IntelligentCreaturePickupMode : byte
{
    Automatic = 0,
    SemiAutomatic = 1,
}

public enum HeroSpawnState : byte
{
    None = 0,
    Unsummoned = 1,
    Summoned = 2,
    Dead = 3
}

public enum HeroBehaviour : byte
{
    攻击 = 0,
    反击 = 1,
    跟随 = 2,
    自定 = 3,
    守护 = 4,
    跑回 = 5,
    瞬回 = 6
}

public enum SpellToggleState: sbyte
{
    None = -1,
    False = 0,
    True = 1
}

public enum MarketCollectionMode : byte
{
    Any = 0,
    Sold = 1,
    Expired = 2
}