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
    Guard = 0,  //Mon1.wil
    ForestYeti = 1,
    TaoistGuard = 2,
    Football = 3,
    Guard2 = 4,
    CannibalPlant = 10,  //Mon2.wil
    HalloweenScythe = 12,
    GiantEgg = 13,
    Wolf = 14,
    DarkBrownWolf = 15,
    Bull = 17,
    Skeleton = 20,  //Mon3.wil
    AxeSkeleton = 21,
    BoneFighter = 22,
    BoneWarrior = 23,
    CaveMaggot = 24,
    HookingCat = 25,
    RakingCat = 26,
    Scarecrow = 27,
    Dark = 28,
    Dung = 29,
    WoomaSoldier = 30,  //Mon4.wil
    FlamingWooma = 31,
    WoomaFighter = 32,
    WoomaWarrior = 33,
    WoomaTaurus = 34,
    RedSnake = 35,
    BoneFamiliar = 36,
    TigerSnake = 37,
    WedgeMoth = 38,
    ShamanZombie = 40,  //Mon5.wil
    BugBatMaggot = 41,
    BugBat = 42,
    Sheep = 43,
    SkyStinger = 44,
    ShellNipper = 45,
    BigRat = 46,
    ZumaArcher = 47,
    VisceralWorm = 48,
    SandWorm = 49,
    DigOutZombie = 50,  //Mon6.wil
    ClZombie = 51,
    NdZombie = 52,
    CrawlerZombie = 53,
    DarkDevourer = 54,
    PoisonHugger = 55,
    Hugger = 56,
    Behemoth = 57,
    TailedLion = 58,
    HighAssassin = 59,
    ZumaStatue = 61,  //Mon7.wil
    ZumaGuardian = 62,
    ZumaTaurus = 63,
    MudPile = 64,
    DarkDustPile = 65,
    SnowPile = 66,
    MutatedHugger = 67,
    GingerBreadman = 68,
    Bush = 69,
    GreyWolf = 70,  //Mon8.wil
    ArcherGuard = 71,
    KatanaGuard = 72,
    Centipede = 73,
    BlackMaggot = 74,
    CaveBat = 80,  //Mon9.wil
    WhimperingBee = 81,
    GiantWorm = 82,
    Scorpion = 83,
    Keratoid = 90,  //Mon10.wil
    GiantKeratoid = 91,
    RedEvilApe = 92,
    GrayEvilApe = 93,
    Oma = 100,  //Mon11.wil
    OmaFighter = 101,
    OmaWarrior = 102,
    SpiderFrog = 103,
    HoroBlaster = 104,
    BlueHoroBlaster = 105,
    KekTal = 106,
    VioletKekTal = 107,
    RedBoar = 110,  //Mon12.wil
    BlackBoar = 111,
    WhiteBoar = 112,
    SpiderBat = 113,
    GangSpider = 114,
    BigApe = 115,
    EvilApe = 116,
    LureSpider = 117,
    GreatSpider = 118,
    VenomSpider = 119,
    Tongs = 120,  //Mon13.wil
    EvilTongs = 121,
    SnakeScorpion = 130,  //Mon14.wil
    RedMoonEvil = 131,
    RootSpider = 132,
    BombSpider = 133,
    EvilCentipede1 = 134,
    GhastlyLeecher = 135,
    CyanoGhast = 136,
    MutatedManworm = 137,
    CrazyManworm = 138,
    DreamDevourer = 139,
    EvilCentipede = 140,  //Mon15.wil
    ChestnutTree = 141,
    ChristmasTree = 142,
    EbonyTree = 143,
    LargeMushroom = 144,
    Treasurebox = 145,
    SnowTree = 146,
    Snowman = 147,
    CherryTree = 148,
    BoneElite = 150,  //Mon16.wil
    WoomaGuardian = 151,
    Ghoul = 152,
    Hen = 160,  //Mon17.wil
    Deer = 161,
    Yob = 162,
    SpittingSpider = 163,
    EvilSnake = 164,
    Shinsu = 170,  //Mon18.wil
    Shinsu1 = 171,
    HolyDeva = 172,
    KingScorpion = 180,  //Mon19.wil
    KingHog = 181,
    DarkDevil = 182,
    RedTurtle = 183,
    GreenTurtle = 184,
    BlueTurtle = 185,
    RoninGhoul = 190,  //Mon20.wil
    ToxicGhoul = 191,
    BoneCaptain = 192,
    BoneSpearman = 193,
    BoneBlademan = 194,
    BoneArcher = 195,
    BoneLord = 196,
    BlueSanta = 197,
    BattleStandard = 198,
    ArcherGuard2 = 199,
    Minotaur = 200,  //Mon21.wil
    IceMinotaur = 201,
    ElectricMinotaur = 202,
    WindMinotaur = 203,
    FireMinotaur = 204,
    RightGuard = 205,
    LeftGuard = 206,
    MinotaurKing = 207,
    RedYimoogi = 208,
    WingedOma = 210,  //Mon22.wil
    FlailOma = 211,
    OmaGuard = 212,
    SwordOma = 213,
    AxeOma = 214,
    CrossbowOma = 215,
    YangDevilNode = 216,
    YinDevilNode = 217,
    OmaKing = 218,
    Khazard = 220,  //Mon23.wil
    RedThunderZuma = 221,
    FrostTiger = 222,
    CrystalSpider = 223,
    Yimoogi = 224,
    GiantWhiteSnake = 225,
    YellowSnake = 226,
    BlueSnake = 227,
    FlameTiger = 228,
    WingedTigerLord = 229,
    BlackFoxman = 230,  //Mon24.wil
    RedFoxman = 231,
    WhiteFoxman = 232,
    TrapRock = 233,
    GuardianRock = 234,
    ThunderElement = 235,
    CloudElement = 236,
    GreatFoxSpirit = 237,
    HedgeKekTal = 238,
    BigHedgeKekTal = 239,
    RedFrogSpider = 240,  //Mon25.wil
    BrownFrogSpider = 241,
    TowerTurtle = 242,
    FinialTurtle = 243,
    TurtleKing = 244,
    DarkTurtle = 245,
    LightTurtle = 246,
    DarkSwordOma = 247,
    DarkAxeOma = 248,
    DarkCrossbowOma = 249,
    DarkWingedOma = 250,  //Mon26.wil
    BoneWhoo = 251,
    DarkSpider = 252,
    ViscusWorm = 253,
    ViscusCrawler = 254,
    CrawlerLave = 255,
    DarkYob = 256,
    FlamingMutant = 257,
    StoningStatue = 258,
    FlyingStatue = 259,
    ValeBat = 260,  //Mon27.wil
    Weaver = 261,
    VenomWeaver = 262,
    CrackingWeaver = 263,
    ArmingWeaver = 264,
    CrystalWeaver = 265,
    FrozenZumaStatue = 266,
    FrozenZumaGuardian = 267,
    FrozenRedZuma = 268,
    GreaterWeaver = 269,
    SpiderWarrior = 270,  //Mon28.wil
    Mon271N = 271,
    HellSlasher = 272,
    HellPirate = 273,
    HellCannibal = 274,
    HellKeeper = 275,
    HellBolt = 276,
    WitchDoctor = 277,
    ManectricHammer = 278,
    ManectricClub = 279,
    ManectricClaw = 280,  //Mon29.wil
    ManectricStaff = 281,
    NamelessGhost = 282,
    DarkGhost = 283,
    ChaosGhost = 284,
    ManectricBlest = 285,
    ManectricKing = 286,
    Blank3 = 287,
    IcePillar = 288,
    FrostYeti = 289,
    ManectricSlave = 290,  //Mon30.wil
    TrollHammer = 291,
    TrollBomber = 292,
    TrollStoner = 293,
    TrollKing = 294,
    FlameSpear = 295,
    FlameMage = 296,
    FlameScythe = 297,
    FlameAssassin = 298,
    FlameQueen = 299,
    HellKnight1 = 300,  //Mon31.wil
    HellKnight2 = 301,
    HellKnight3 = 302,
    HellKnight4 = 303,
    HellLord = 304,
    WaterGuard = 305,
    IceGuard = 306,
    ElementGuard = 307,
    DemonGuard = 308,
    KingGuard = 309,
    Snake10 = 310,  //Mon32.wil
    Snake11 = 311,
    Snake12 = 312,
    Snake13 = 313,
    Snake14 = 314,
    Snake15 = 315,
    Snake16 = 316,
    Snake17 = 317,
    DeathCrawler = 318,
    BurningZombie = 319,
    MudZombie = 320,  //Mon33.wil
    FrozenZombie = 321,
    UndeadWolf = 322,
    DemonWolf = 323,
    WhiteMammoth = 324,
    DarkBeast = 325,
    LightBeast = 326,
    BloodBaboon = 327,
    HardenRhino = 328,
    AncientBringer = 329,
    FightingCat = 330,  //Mon34.wil
    FireCat = 331,
    CatWidow = 332,
    StainHammerCat = 333,
    BlackHammerCat = 334,
    StrayCat = 335,
    CatShaman = 336,
    Jar1 = 337,
    Jar2 = 338,
    SeedingsGeneral = 339,
    RestlessJar = 340,  //Mon35.wil
    GeneralMeowMeow = 341,
    Bunny = 342,
    Tucson = 343,
    TucsonFighter = 344,
    TucsonMage = 345,
    TucsonWarrior = 346,
    Armadillo = 347,
    ArmadilloElder = 348,
    TucsonEgg = 350,  //Mon36.wil
    PlaguedTucson = 351,
    SandSnail = 352,
    CannibalTentacles = 353,
    TucsonGeneral = 354,
    GasToad = 355,
    Mantis = 356,
    SwampWarrior = 357,
    AssassinBird = 358,
    RhinoWarrior = 359,
    RhinoPriest = 360,  //Mon37.wil
    ElephantMan = 361,
    StoneGolem = 362,
    EarthGolem = 363,
    TreeGuardian = 364,
    TreeQueen = 365,
    PeacockSpider = 366,
    DarkBaboon = 367,
    TwinHeadBeast = 368,
    OmaCannibal = 369,
    OmaBlest = 370,  //Mon38.wil
    OmaSlasher = 371,
    OmaAssassin = 372,
    OmaMage = 373,
    OmaWitchDoctor = 374,
    LightningBead = 375,
    HealingBead = 376,
    PowerUpBead = 377,
    DarkOmaKing = 378,
    Mon380P = 380,  //Mon39.wil
    Mandrill = 381,
    PlagueCrab = 382,
    CreeperPlant = 383,
    FloatingWraith = 384,
    ArmedPlant = 385,
    AvengerPlant = 386,
    Nadz = 387,
    AvengingSpirit = 388,
    AvengingWarrior = 390,  //Mon40.wil
    AxePlant = 391,
    WoodBox = 392,
    ClawBeast = 393,
    WaterSoul = 394,
    DarkCaptain = 395,
    SackWarrior = 396,
    WereTiger = 397,
    KingHydrax = 398,
    Hydrax = 399,
    HornedMage = 400,  //Mon41.wil
    HornedArcher = 401,
    ColdArcher = 402,
    HornedWarrior = 403,
    FloatingRock = 404,
    ScalyBeast = 405,
    HornedSorceror = 406,
    BlueSoul = 407,
    BoulderSpirit = 408,
    Mon409B = 409,
    Turtlegrass = 410, //Mon42.wil
    ManTree = 411,
    Bear = 412,
    Leopard = 413,
    ChieftainSword = 414,
    MoonStone = 415,
    SunStone = 416,
    ChieftainArcher = 417,
    LightningStone = 418,
    StoningSpider = 419,
    VampireSpider = 420,  //Mon43.wil
    SpittingToad = 421,
    SnakeTotem = 422,
    CharmedSnake = 423,
    FrozenSoldier = 424,
    FrozenFighter = 425,
    FrozenArcher = 426,
    FrozenKnight = 427,
    FrozenGolem = 428,
    IcePhantom = 429,
    SnowWolf = 430,  //Mon44.wil
    SnowWolfKing = 431,
    WaterDragon = 432,
    BlackTortoise = 433,
    Manticore = 434,
    DragonWarrior = 435,
    DragonArcher = 436,
    FlameWhirlwind = 437,
    Kirin = 438,
    Guard3 = 439,
    ArcherGuard3 = 440,  //Mon45.wil
    Bunny2 = 441,
    FrozenMiner = 442,
    Mon443N = 443,
    FrozenMagician = 444,
    SnowYeti = 445,
    IceCrystalSoldier = 446,
    DarkWraith = 447,
    DarkSpirit = 448,
    CrystalBeast = 449,
    RedOrb = 450,  //Mon46.wil
    BlueOrb = 451,
    YellowOrb = 452,
    GreenOrb = 453,
    WhiteOrb = 454,
    FatalLotus = 455,
    AntCommander = 456,
    CargoBoxwithlogo = 457,
    Doe = 458,
    Reindeer = 459,
    AngryReindeer = 460,  //Mon47.wil
    CargoBox = 461,
    Ram1 = 462,
    Ram2 = 463,
    Kite = 465,
    PurpleFaeFlower = 466,
    Furball = 467,
    GlacierSnail = 468,
    FurbolgWarrior = 469,
    FurbolgArcher = 470,  //Mon48.wil
    FurbolgCommander = 471,
    Mon472N = 472,
    FurbolgGuard = 473,
    GlacierBeast = 474,
    GlacierWarrior = 475,
    ShardGuardian = 476,
    CallScroll = 477,
    PoisonScroll = 478,
    FireballScroll = 479,
    LightningScroll = 480,  //Mon49.wil
    HoodedSummoner = 481,
    HoodedIceMage = 482,
    HoodedPriest = 483,
    ShardMaiden = 484,
    KingKong = 485,
    WarBear = 486,
    ReaperPriest = 487,
    ReaperWizard = 488,
    ReaperAssassin = 489,
    LivingVines = 490,  //Mon50.wil
    BlueMonk = 491,
    MutantBeserker = 492,
    MutantGuardian = 493,
    MutantHighPriest = 494,
    MysteriousMage = 495,
    FeatheredWolf = 496,
    MysteriousAssassin = 497,
    MysteriousMonk = 498,
    ManEatingPlant = 499,
    HammerDwarf = 500,  //Mon51.wil
    ArcherDwarf = 501,
    NobleWarrior = 502,
    NobleArcher = 503,
    NoblePriest = 504,
    NobleAssassin = 505,
    //ExplosiveSkull = 506,
    //SeaFire = 507,
    Swain = 508,
    Swain1 = 509,
    RedMutantPlant = 510,  //Mon52.wil
    BlueMutantPlant = 511,
    UndeadHammerDwarf = 512,
    UndeadDwarfArcher = 513,
    AncientStoneGolem = 514,
    Serpentirian = 515,
    Butcher = 516,
    Mon517P = 517,
    Riklebites = 518,
    Mon519D = 519,
    Mon520P = 520,  //Mon53.wil
    FeralTundraFurbolg = 521,
    FeralFlameFurbolg = 522,
    EnhanceFlameFurbolg = 523,
    BlueArcaneTotem = 525,
    GreenArcaneTotem = 526,
    RedArcaneTotem = 527,
    YellowArcaneTotem = 528,
    WarpGate = 529,
    SpectralWraith = 530,  //Mon54.wil
    BabyMagmaDragon = 531,
    BloodLord = 532,
    SerpentLord = 533,
    MirEmperor = 534,
    MutantManEatingPlant = 535,
    MutantWarg = 536,
    GrassElemental = 537,
    RockElemental = 538,
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
    坐骑 = 13
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

public enum Spell : ushort
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
    FireBall = 51,
    Repulsion = 52,
    ElectricShock = 53,
    GreatFireBall = 54,
    HellFire = 55,
    ThunderBolt = 56,
    Teleport = 57,
    FireBang = 58,
    FireWall = 59,
    Lightning = 60,
    FrostCrunch = 61,
    ThunderStorm = 62,
    MagicShield = 63,
    TurnUndead = 64,
    Vampirism = 65,
    IceStorm = 66,
    FlameDisruptor = 67,
    Mirroring = 68,
    FlameField = 69,
    Blizzard = 70,
    MagicBooster = 71,
    MeteorStrike = 72,
    IceThrust = 73,
    FastMove = 74,
    StormEscape = 75,
    HeavenlySecrets = 76,
    GreatFireBallRare = 77,
    StormEscapeRare = 78,

    //Taoist
    Healing = 101,
    SpiritSword = 102,
    Poisoning = 103,
    SoulFireBall = 104,
    SummonSkeleton = 105,
    Hiding = 107,
    MassHiding = 108,
    SoulShield = 109,
    Revelation = 110,
    BlessedArmour = 111,
    EnergyRepulsor = 112,
    TrapHexagon = 113,
    Purification = 114,
    MassHealing = 115,
    Hallucination = 116,
    UltimateEnhancer = 117,
    SummonShinsu = 118,
    Reincarnation = 119,
    SummonHolyDeva = 120,
    Curse = 121,
    Plague = 122,
    PoisonCloud = 123,
    EnergyShield = 124,
    PetEnhancer = 125,
    HealingCircle = 126,
    HealingRare = 127,
    HealingcircleRare = 128,
    PetEnhancerRare = 129,
    MultipleEffects = 130,
    MultipleEffectsRare = 131,

    //Assassin
    FatalSword = 151,
    DoubleSlash = 152,
    Haste = 153,
    FlashDash = 154,
    LightBody = 155,
    HeavenlySword = 156,
    FireBurst = 157,
    Trap = 158,
    PoisonSword = 159,
    MoonLight = 160,
    MPEater = 161,
    SwiftFeet = 162,
    DarkBody = 163,
    Hemorrhage = 164,
    CrescentSlash = 165,
    MoonMist = 166,
    CatTongue = 167,

    //Archer
    Focus = 201,
    StraightShot = 202,
    DoubleShot = 203,
    ExplosiveTrap = 204,
    DelayedExplosion = 205,
    Meditation = 206,
    BackStep = 207,
    ElementalShot = 208,
    Concentration = 209,
    Stonetrap = 210,
    ElementalBarrier = 211,
    SummonVampire = 212,
    VampireShot = 213,
    SummonToad = 214,
    PoisonShot = 215,
    CrippleShot = 216,
    SummonSnakes = 217,
    NapalmShot = 218,
    OneWithNature = 219,
    BindingShot = 220,
    MentalState = 221,

    //Custom
    Blink = 301,
    Portal = 302,
    BattleCry = 303,
    FireBounce = 304,
    MeteorShower = 305,

    //Map Events
    DigOutZombie = 400,
    Rubble = 401,
    MapLightning = 402,
    MapLava = 403,
    MapQuake1 = 404,
    MapQuake2 = 405,
    DigOutArmadillo = 406,
    FlyingStatueIceTornado = 407, //259
    GeneralMeowMeowThunder = 408, //341
    TucsonGeneralRock = 409, //354
    StoneGolemQuake = 410, //362
    EarthGolemPile = 411, //363
    TreeQueenRoot = 412, //365
    TreeQueenMassRoots = 413, //365
    TreeQueenGroundRoots = 414, //365
    DarkOmaKingNuke = 415, //378
    HornedSorcererDustTornado = 416, //406
    Mon409BRockFall = 417,
    Mon409BRockSpike = 418,
    Mon409BShield = 419,
    YangDragonFlame = 420, //414
    YangDragonIcyBurst = 421, //414
    ShardGuardianIceBomb = 422, //476
    GroundFissure = 423, //498
    SkeletonBomb = 424, //508
    FlameExplosion = 425, //509
    ButcherFlyAxe = 426, //516
    RiklebitesBlast = 427, //518
    RiklebitesRollCall = 428, //518
    SwordFormation = 429, //550
    Mon564NWhirlwind = 430,
    Mon570BRupture = 431,
    Mon570BLightningCloud = 432,
    Mon571BFireBomb = 433,
    Mon572BFlame = 434,
    Mon572BDarkVortex = 435,
    Mon573BBigCobweb = 436,
    Mon580BPoisonousMist = 437,
    Mon580BDenseFog = 438,
    Mon580BRoot = 439,
    Mon603BWhirlPool = 440,
    Mon609NBomb = 441
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
    血龙兽,
    金刚不坏,
    金刚不坏秘籍,
    魔法盾,
    金刚术,
    天上秘术,
    万效符,
    万效符秘籍,

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
    万效符爆杀,

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