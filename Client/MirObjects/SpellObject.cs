using Client.MirGraphics;
using Client.MirScenes;
using Client.MirSounds;
using S = ServerPackets;

namespace Client.MirObjects
{
    class SpellObject : MapObject
    {
        public override ObjectType Race
        {
            get { return ObjectType.Spell; }
        }

        public override bool Blocking
        {
            get { return false; }
        }

        public Spell Spell;
        public Point AnimationOffset = new(0, 0);
        public int FrameCount, FrameInterval, FrameIndex;
        public bool Repeat, Ended, DrawBehind;


        public SpellObject(uint objectID) : base(objectID)
        {
        }

        public void Load(S.ObjectSpell info)
        {
            CurrentLocation = info.Location;
            MapLocation = info.Location;
            GameScene.Scene.MapControl.AddObject(this);
            Spell = info.Spell;
            Direction = info.Direction;
            Repeat = true;
            Ended = false;
            DrawBehind = false;

            switch (Spell)
            {
                case Spell.TrapHexagon:
                    BodyLibrary = Libraries.Magic;
                    DrawFrame = 1390;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Blend = true;
                    break;
                case Spell.FireWall:
                    BodyLibrary = Libraries.Magic;
                    DrawFrame = 1630;
                    FrameInterval = 120;
                    FrameCount = 6;
                    Light = 3;
                    Blend = true;
                    break;
                case Spell.PoisonCloud:
                    BodyLibrary = Libraries.Magic2;
                    DrawFrame = 1650;
                    FrameInterval = 120;
                    FrameCount = 20;
                    Light = 3;
                    Blend = true;
                    break;
                case Spell.DigOutZombie:
                    BodyLibrary = (ushort)Monster.DigOutZombie < Libraries.Monsters.Count() ? Libraries.Monsters[(ushort)Monster.DigOutZombie] : Libraries.Magic;
                    DrawFrame = 304 + (byte) Direction;
                    FrameCount = 0;
                    Blend = false;
                    break;
                case Spell.Blizzard:
                    AnimationOffset = new Point(0, -20);
                    BodyLibrary = Libraries.Magic2;
                    DrawFrame = 1550;
                    FrameInterval = 100;
                    FrameCount = 30;
                    Light = 3;
                    Blend = true;
                    Repeat = false;
                    break;
                case Spell.MeteorStrike:
                    AnimationOffset = new Point(0, -20);
                    MapControl.Effects.Add(new Effect(Libraries.Magic2, 1600, 10, 800, CurrentLocation) { DrawBehind = true, Repeat = true, RepeatUntil = CMain.Time + 3000 });
                    BodyLibrary = Libraries.Magic2;
                    DrawFrame = 1610;
                    FrameInterval = 100;
                    FrameCount = 30;
                    Light = 3;
                    Blend = true;
                    Repeat = false;
                    break;
                case Spell.HealingcircleRare:
                    DrawColour = Color.FromArgb(100, 255, 200, 150); 
                    BodyLibrary = Libraries.Magic3;
                    DrawFrame = 630;
                    FrameInterval = 100;
                    FrameCount = 11;
                    Light = 5;
                    Blend = true;
                    Repeat = true;
                    DrawBehind = true;
                    break;
                case Spell.Rubble:
                    if (Direction == 0)
                        BodyLibrary = null;
                    else
                    {
                        BodyLibrary = Libraries.Effect;
                        DrawFrame = 64 + Math.Min(4, (int)(Direction - 1));
                        FrameCount = 1;
                        FrameInterval = 10000;
                    }
                    break;
                case Spell.Reincarnation:
                    BodyLibrary = Libraries.Magic;
                    DrawFrame = 4020;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Light = 1;
                    Blend = true;
                    Repeat = true;
                    break;
                case Spell.ExplosiveTrap:
                    BodyLibrary = Libraries.Magic3;
                    if (info.Param)
                    {
                        DrawFrame = 1570;
                        FrameInterval = 100;
                        FrameCount = 9;
                        Repeat = false;
                        SoundManager.PlaySound(20000 + 124 * 10 + 5);//Boom for all players in range
                    }
                    else
                    {
                        DrawFrame = 1560;
                        FrameInterval = 100;
                        FrameCount = 10;
                        Repeat = true;
                    }
                    //Light = 1;
                    Blend = true;
                    break;
                case Spell.Trap:
                    BodyLibrary = Libraries.Magic2;
                    DrawFrame = 2360;
                    FrameInterval = 100;
                    FrameCount = 8;
                    Blend = true;
                    break;
                case Spell.MapLightning:
                    MapControl.Effects.Add(new Effect(Libraries.Dragon, 400 + (CMain.Random.Next(3) * 10), 5, 600, CurrentLocation));
                    SoundManager.PlaySound(8301);
                    break;
                case Spell.MapLava:
                    MapControl.Effects.Add(new Effect(Libraries.Dragon, 440, 20, 1600, CurrentLocation) { Blend = false });
                    MapControl.Effects.Add(new Effect(Libraries.Dragon, 470, 10, 800, CurrentLocation));
                    SoundManager.PlaySound(8302);
                    break;
                case Spell.MapQuake1:
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 27, 12, 1200, CurrentLocation) { Blend = false });
                    SoundManager.PlaySound(8304);
                    break;
                case Spell.MapQuake2:
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 39, 13, 1300, CurrentLocation) { Blend = false });
                    SoundManager.PlaySound(8304);
                    break;
                case Spell.DigOutArmadillo:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Armadillo];
                    DrawFrame = 472 + (byte)Direction;
                    FrameCount = 0;
                    Blend = false;
                    break;
                case Spell.GeneralMeowMeowThunder:                
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GeneralMeowMeow], 562, 7, 700, CurrentLocation) { Blend = true });
                    SoundManager.PlaySound(8321);
                    break;
                case Spell.StoneGolemQuake:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.StoneGolem];
                    DrawFrame = 368 + (int)Direction * 8;
                    FrameInterval = 100;
                    FrameCount = 8;
                    Light = 0;
                    Blend = false;
                    Repeat = false;
                    SoundManager.PlaySound(8304);
                    break;
                case Spell.EarthGolemPile:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.EarthGolem];
                    DrawFrame = 441;
                    FrameInterval = 100;
                    FrameCount = 12;
                    Light = 0;
                    Blend = false;
                    Repeat = false;
                    SoundManager.PlaySound(8331);
                    break;
                case Spell.TreeQueenMassRoots:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.TreeQueen];
                    DrawFrame = 82;
                    FrameInterval = 100;
                    FrameCount = 15;
                    Blend = false;
                    Repeat = false;
                    DrawBehind = true;
                    SoundManager.PlaySound(8341);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeQueen], 97, 15, 1400, CurrentLocation) { Blend = true });
                    break;
                case Spell.TreeQueenGroundRoots:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.TreeQueen];
                    DrawFrame = 48;
                    FrameInterval = 100;
                    FrameCount = 9;
                    Blend = false;
                    Repeat = false;
                    DrawBehind = true;
                    SoundManager.PlaySound(8342);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeQueen], 57, 9, 900, CurrentLocation) { Blend = true });
                    break;
                case Spell.TreeQueenRoot:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.TreeQueen];
                    DrawFrame = 112;
                    FrameInterval = 100;
                    FrameCount = 15;
                    Blend = false;
                    Repeat = false;
                    DrawBehind = true;
                    SoundManager.PlaySound(8343);
                    break;
                case Spell.TucsonGeneralRock:
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonGeneral], 552, 20, 2000, CurrentLocation) { Repeat = false, Blend = false });
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.TucsonGeneral];
                    DrawFrame = 572;
                    FrameInterval = 100;
                    FrameCount = 20;
                    Light = 1;
                    Blend = true;
                    Repeat = false;
                    break;
                case Spell.Portal:
                    BodyLibrary = Libraries.Magic2;
                    DrawFrame = 2360;
                    FrameInterval = 100;
                    FrameCount = 8;
                    Blend = true;
                    break;
                case Spell.HealingCircle:
                    DrawColour = Color.FromArgb(128, 255, 255, 100); 
                    BodyLibrary = Libraries.Magic3;
                    DrawFrame = 630;
                    FrameInterval = 80;
                    FrameCount = 11;
                    Light = 3;
                    Blend = true;
                    break;
                case Spell.FlyingStatueIceTornado:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.FlyingStatue];
                    DrawFrame = 314;
                    FrameInterval = 100;
                    FrameCount = 20;
                    Blend = true;
                    Repeat = false;
                    SoundManager.PlaySound(8303);
                    break;
                case Spell.DarkOmaKingNuke:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.DarkOmaKing];
                    DrawFrame = 1670 + (int)Direction * 9;
                    FrameInterval = 100;
                    FrameCount = 9;
                    Blend = true;
                    Repeat = false;
                    SoundManager.PlaySound(((ushort)Monster.DarkOmaKing * 10) + 9);
                    break;
                case Spell.HornedSorcererDustTornado:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.HornedSorceror];
                    DrawFrame = 634;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(8306);
                    break;
                case Spell.Mon409BRockFall:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon409B];
                    DrawFrame = 1178;
                    FrameInterval = 100;
                    FrameCount = 12;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(4089);
                    break;
                case Spell.Mon409BRockSpike:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon409B];
                    DrawFrame = 1358;
                    FrameInterval = 100;
                    FrameCount = 9;
                    Blend = false;
                    Repeat = true;
                    SoundManager.PlaySound(8457);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon409B], 1367, 9, 900, CurrentLocation) { Blend = true });
                    break;
                case Spell.Mon409BShield:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon409B];
                    DrawColour = Color.White;
                    DrawFrame = 1341;
                    FrameInterval = 100;
                    FrameCount = 17;
                    Light = 5;
                    Blend = true;
                    Repeat = true;
                    break;
                case Spell.YangDragonFlame:
                    DrawColour = Color.FromArgb(200, 200, 200, 200);
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.ChieftainSword];                    
                    DrawFrame = 1088;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Light = 3;
                    Blend = true;
                    Repeat = false;
                    break;
                case Spell.YangDragonIcyBurst:
                    DrawColour = Color.FromArgb(180, 200, 200, 180);
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.ChieftainSword];
                    DrawFrame = 1314;
                    FrameInterval = 200;
                    FrameCount = 5;
                    Light = 3;
                    Blend = true;
                    Repeat = false;
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainSword], 1319, 12, 1200, CurrentLocation, CMain.Time + 1000) { Blend = true });
                    break;               
                case Spell.ShardGuardianIceBomb:
                    DrawColour = Color.FromArgb(158, 158, 158, 255);
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.ShardGuardian];
                    DrawFrame = 502;
                    FrameInterval = 100;
                    FrameCount = 15;
                    Light = 2;
                    Blend = true;
                    Repeat = false;
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShardGuardian], 536, 8, 800, CurrentLocation, CMain.Time + 1500) { Blend = true, DrawBehind = true });
                    break;
                case Spell.GroundFissure:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.MysteriousMonk];
                    DrawFrame = 744 + (int)Direction * 10;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Light = 2;
                    Blend = true;
                    Repeat = false;
                    DrawBehind = true;
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MysteriousMonk], 824 + (int)Direction * 10, 10, 1000, CurrentLocation) { Blend = true });
                    break;
                case Spell.SkeletonBomb:
                    DrawColour = Color.FromArgb(158, 158, 158, 255);
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Swain];
                    DrawFrame = 528;
                    FrameInterval = 100;
                    FrameCount = 13;
                    Light = 2;
                    Blend = true;
                    Repeat = false;
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Swain], 541, 10, 1000, CurrentLocation, CMain.Time + 1300) { Blend = true, DrawBehind = true });
                    break;
                case Spell.FlameExplosion:
                    DrawColour = Color.FromArgb(180, 180, 180, 255);
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Swain1];
                    DrawFrame = 699;
                    FrameInterval = 200;
                    FrameCount = 9;
                    Light = 3;
                    Blend = true;
                    Repeat = false;
                    DrawBehind = true;
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Swain1], 708, 5, 1000, CurrentLocation, CMain.Time + 1800) { Blend = true});
                    break;
                case Spell.ButcherFlyAxe:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Butcher];
                    DrawFrame = 685;
                    FrameInterval = 100;
                    FrameCount = 5;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(8306);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Butcher], 680, 5, 1000, CurrentLocation) { Blend = true });
                    break;
                case Spell.RiklebitesRollCall:           
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Riklebites], 1102, 10, 2000, CurrentLocation) { Blend = true, DrawBehind = true });
                    break;
                case Spell.RiklebitesBlast:
                    DrawColour = Color.FromArgb(200, 200, 200, 200);
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Riklebites];
                    DrawFrame = 1067;
                    FrameInterval = 200;
                    FrameCount = 6;
                    Light = 3;
                    Blend = true;
                    Repeat = false;
                    DrawBehind = false;
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Riklebites], 1073, 11, 1100, CurrentLocation) { Blend = true, DrawBehind = true });
                    break;
                case Spell.SwordFormation:
                    DrawColour = Color.FromArgb(180, 200, 200, 160);
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon550N];
                    DrawFrame = 450;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Light = 3;
                    Blend = true;
                    Repeat = false;
                    DrawBehind = true;
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon550N], 460, 8, 800, CurrentLocation, CMain.Time + 1000) { Blend = true});
                    break;
                case Spell.Mon564NWhirlwind:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon564N];
                    DrawColour = Color.FromArgb(180, 255, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 916;
                    FrameInterval = 100;
                    FrameCount = 20;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(8306);
                    break;
                case Spell.Mon570BRupture:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon570B];
                    DrawColour = Color.FromArgb(180, 255, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 573 + (int)Direction * 10;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Blend = true;
                    Repeat = false;
                    DrawBehind = true;
                    SoundManager.PlaySound(8306);
                    break;
                case Spell.Mon570BLightningCloud:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon570B];
                    DrawColour = Color.FromArgb(180, 200, 200, 180);
                    LightColour = Color.White;
                    Light = 1;
                    DrawFrame = 544;
                    FrameInterval = 100;
                    FrameCount = 16;
                    Blend = true;
                    Repeat = false;
                    DrawBehind = true;
                    SoundManager.PlaySound(8306);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon570B], 566, 8, 300, CurrentLocation, CMain.Time + 1000) { Blend = true });
                    break;
                case Spell.Mon571BFireBomb:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon571B];
                    DrawColour = Color.FromArgb(180, 200, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 574;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(8306);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon571B], 584, 6, 600, CurrentLocation) { Blend = true });
                    break;
                case Spell.Mon572BFlame:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon572B];
                    DrawColour = Color.FromArgb(180, 200, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 475;
                    FrameInterval = 100;
                    FrameCount = 13;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(8306);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon572B], 488, 4, 600, CurrentLocation) { Blend = true });
                    break;
                case Spell.Mon572BDarkVortex:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon572B];
                    DrawColour = Color.FromArgb(180, 200, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 492;
                    FrameInterval = 100;
                    FrameCount = 24;
                    Blend = false;
                    Repeat = true;
                    DrawBehind = true;
                    SoundManager.PlaySound(8306);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon572B], 516, 7, 600, CurrentLocation) { Blend = false });
                    break;
                case Spell.Mon573BBigCobweb:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon573B];
                    DrawColour = Color.FromArgb(180, 255, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 535;
                    FrameInterval = 100;
                    FrameCount = 5;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(8306);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon573B], 539, 4, 1000, CurrentLocation) { Blend = true });
                    break;
                case Spell.Mon580BPoisonousMist:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon580B];
                    DrawColour = Color.FromArgb(180, 255, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 783;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Blend = true;
                    Repeat = true;
                    DrawBehind = true;
                    SoundManager.PlaySound(8306);
                    break;
                case Spell.Mon580BDenseFog:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon580B];
                    DrawColour = Color.FromArgb(180, 255, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 793;
                    FrameInterval = 100;
                    FrameCount = 10;
                    Blend = true;
                    Repeat = true;
                    DrawBehind = true;
                    SoundManager.PlaySound(8306);
                    break;
                case Spell.Mon580BRoot:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon580B];
                    DrawFrame = 803;
                    FrameInterval = 100;
                    FrameCount = 14;
                    Blend = false;
                    Repeat = false;
                    DrawBehind = true;
                    SoundManager.PlaySound(8343);
                    break;
                case Spell.Mon609NBomb:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon609N];
                    DrawColour = Color.FromArgb(180, 255, 200, 180);
                    LightColour = Color.White;
                    Light = 3;
                    DrawFrame = 584;
                    FrameInterval = 100;
                    FrameCount = 13;
                    Blend = true;
                    Repeat = true;
                    SoundManager.PlaySound(8306);
                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon609N], 597, 8, 1000, CurrentLocation) { Blend = true });
                    break;
            }

            NextMotion = CMain.Time + FrameInterval;
            NextMotion -= NextMotion % 100;
        }

        public override void Process()
        {
            if (CMain.Time >= NextMotion)
            {
                if (++FrameIndex >= FrameCount && Repeat)
                {
                    FrameIndex = 0;
                    Ended = true;
                }

                NextMotion = CMain.Time + FrameInterval;

                switch (Spell)
                {
                    case Spell.TucsonGeneralRock:
                        if (FrameIndex == 10) SoundManager.PlaySound(8305);
                        break;
                    case Spell.HornedSorcererDustTornado:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.ButcherFlyAxe:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon409BRockSpike:
                        if (Ended)
                        {
                            DrawFrame = 1366;
                            FrameCount = 1;
                            FrameIndex = 0;
                        }
                        break;
                    case Spell.Mon564NWhirlwind:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon570BRupture:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon570BLightningCloud:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon571BFireBomb:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon572BFlame:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon572BDarkVortex:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon573BBigCobweb:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon580BPoisonousMist:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon580BDenseFog:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                    case Spell.Mon603BWhirlPool:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(6237);
                        break;
                    case Spell.Mon609NBomb:
                        if (FrameIndex == 0 && CMain.Random.Next(3) == 0) SoundManager.PlaySound(8306);
                        break;
                }
            }

            DrawLocation = new Point((CurrentLocation.X - User.Movement.X + MapControl.OffSetX) * MapControl.CellWidth, (CurrentLocation.Y - User.Movement.Y + MapControl.OffSetY) * MapControl.CellHeight);
            DrawLocation.Offset(GlobalDisplayLocationOffset);
            DrawLocation.Offset(User.OffSetMove);
        }

        public override void Draw()
        {
            if (FrameIndex >= FrameCount && !Repeat) return;
            if (BodyLibrary == null) return;

            if (Blend)
            {
                BodyLibrary.DrawBlend(
                    DrawFrame + FrameIndex,
                    AnimationOffset == default ? DrawLocation : GetDrawWithOffset(),
                    DrawColour, true,
                    0.8F);
            }
            else
            {
                BodyLibrary.Draw(DrawFrame + FrameIndex,
                    AnimationOffset == default ? DrawLocation : GetDrawWithOffset(),
                    DrawColour,
                    true);
            }
        }

        public override bool MouseOver(Point p)
        {
            return false;
        }

        public override void DrawBehindEffects(bool effectsEnabled)
        {
        }

        public override void DrawEffects(bool effectsEnabled)
        { 
        }

        private Point GetDrawWithOffset()
        {
            Point newDrawLocation = new (
                (CurrentLocation.X + AnimationOffset.X - User.Movement.X + MapControl.OffSetX) * MapControl.CellWidth,
                (CurrentLocation.Y + AnimationOffset.Y - User.Movement.Y + MapControl.OffSetY) * MapControl.CellHeight);

            newDrawLocation.Offset(GlobalDisplayLocationOffset);
            newDrawLocation.Offset(User.OffSetMove);

            return newDrawLocation;
        }
    }
}
