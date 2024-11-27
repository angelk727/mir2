using Client.MirControls;
using Client.MirGraphics;
using Client.MirScenes;
using Client.MirSounds;
using S = ServerPackets;

namespace Client.MirObjects
{
    class MonsterObject : MapObject
    {
        public override ObjectType Race
        {
            get { return ObjectType.Monster; }
        }

        public override bool Blocking
        {
            get { return AI == 970 || (AI == 950 && Direction == (MirDirection)6) ? false : !Dead; }
        }

        public Point ManualLocationOffset
        {
            get
            {
                switch (BaseImage)
                {
                    case Monster.EvilMir:
                        return new Point(-21, -15);
                    case Monster.PalaceWall2:
                    case Monster.PalaceWallLeft:
                    case Monster.PalaceWall1:
                    case Monster.GiGateSouth:
                    case Monster.GiGateWest:
                    case Monster.SSabukWall1:
                    case Monster.SSabukWall2:
                    case Monster.SSabukWall3:
                        return new Point(-10, 0);
                    case Monster.GiGateEast:
                        return new Point(-45, 7);
                    default:
                        return new Point(0, 0);
                }
            }
        }

        public Monster BaseImage;
        public byte Effect;
        public bool Skeleton;

        public FrameSet Frames = new FrameSet();
        public Frame Frame;
        public int FrameIndex, FrameInterval, EffectFrameIndex;

        public uint TargetID;
        public Point TargetPoint;

        public bool Stoned;
        public byte Stage;
        public int BaseSound;

        public long ShockTime;
        public bool BindingShotCenter;

        public Color OldNameColor;

        public SpellEffect CurrentEffect;

        public MonsterObject(uint objectID) : base(objectID) { }

        public void Load(S.ObjectMonster info, bool update = false)
        {
            Name = info.Name;
            NameColour = info.NameColour;
            BaseImage = info.Image;

            OldNameColor = NameColour;

            CurrentLocation = info.Location;
            MapLocation = info.Location;
            if (!update) GameScene.Scene.MapControl.AddObject(this);

            Effect = info.Effect;
            AI = info.AI;
            Light = info.Light;

            Direction = info.Direction;
            Dead = info.Dead;
            Poison = info.Poison;
            Skeleton = info.Skeleton;
            Hidden = info.Hidden;
            ShockTime = CMain.Time + info.ShockTime;
            BindingShotCenter = info.BindingShotCenter;

            Buffs = info.Buffs;

            if (Stage != info.ExtraByte)
            {
                switch (BaseImage)
                {
                    case Monster.GreatFoxSpirit: //237
                        if (update)
                        {
                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GreatFoxSpirit], 335, 20, 3000, this));
                            SoundManager.PlaySound(BaseSound + 8);
                        }
                        break;
                    case Monster.HellLord:
                        {
                            Effects.Clear();

                            var effects = MapControl.Effects.Where(x => x.Library == Libraries.Monsters[(ushort)Monster.HellLord]);

                            foreach (var effect in effects)
                                effect.Repeat = false;

                            if (info.ExtraByte > 3)
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 21, 6, 600, this) { Repeat = true });
                            else
                            {
                                if (info.ExtraByte > 2)
                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 105, 6, 600, new Point(100, 84)) { Repeat = true });
                                if (info.ExtraByte > 1)
                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 111, 6, 600, new Point(96, 81)) { Repeat = true });
                                if (info.ExtraByte > 0)
                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 123, 6, 600, new Point(93, 84)) { Repeat = true });
                            }

                            SoundManager.PlaySound(BaseSound + 9);
                        }
                        break;
                }
            }

            Stage = info.ExtraByte;

            //Library
            switch (BaseImage)
            {
                case Monster.EvilMir: //900
                case Monster.DragonStatue: //902
                    BodyLibrary = Libraries.Dragon;
                    break;
                case Monster.EvilMirBody: //901
                    break;
                case Monster.Catapult:
                case Monster.ChariotBallista:
                case Monster.Ballista:
                case Monster.Trebuchet:
                case Monster.CanonTrebuchet:
                    BodyLibrary = Libraries.Siege[((ushort)BaseImage) - 940];
                    break;
                case Monster.SabukGate:
                case Monster.PalaceWallLeft:
                case Monster.PalaceWall1:
                case Monster.PalaceWall2:
                case Monster.GiGateSouth:
                case Monster.GiGateEast:
                case Monster.GiGateWest:
                case Monster.SSabukWall1:
                case Monster.SSabukWall2:
                case Monster.SSabukWall3:
                case Monster.NammandGate1:
                case Monster.NammandGate2:
                case Monster.SabukWallSection:
                case Monster.NammandWallSection:
                case Monster.FrozenDoor: //964
                case Monster.GonRyunDoor: //965
                case Monster.UnderPassDoor1: //966
                case Monster.UnderPassDoor2: //967
                case Monster.InDunFences: //968
                    BodyLibrary = Libraries.Gates[((ushort)BaseImage) - 950];
                    break;
                case Monster.小猪:
                case Monster.小鸡:
                case Monster.小猫:
                case Monster.精灵骷髅:
                case Monster.白猪:
                case Monster.纸片人:
                case Monster.黑猫:
                case Monster.龙蛋:
                case Monster.火娃:
                case Monster.雪人:
                case Monster.青蛙:
                case Monster.红猴:
                case Monster.愤怒的小鸟:
                case Monster.阿福:
                case Monster.治疗拉拉:
                case Monster.猫咪超人:
                case Monster.龙宝宝:
                    BodyLibrary = Libraries.Pets[((ushort)BaseImage) - 10000];
                    break;
                case Monster.HellBomb1:
                case Monster.HellBomb2:
                case Monster.HellBomb3:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.HellLord];
                    break;
                case Monster.Mon380P:
                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon380P];
                    break;
                default:
                    BodyLibrary = Libraries.Monsters[(ushort)BaseImage];
                    break;
            }

            if (Skeleton)
                ActionFeed.Add(new QueuedAction { Action = MirAction.挖后尸骸, Direction = Direction, Location = CurrentLocation });
            else if (Dead)
                ActionFeed.Add(new QueuedAction { Action = MirAction.死后尸体, Direction = Direction, Location = CurrentLocation });

            BaseSound = (ushort)BaseImage * 10;

            switch (BaseImage)
            {
                case Monster.BoneFamiliar:
                case Monster.Shinsu:
                case Monster.HolyDeva: //172
                case Monster.HellKnight1:
                case Monster.HellKnight2:
                case Monster.HellKnight3:
                case Monster.HellKnight4:
                case Monster.LightningBead:
                case Monster.HealingBead:
                case Monster.FeatheredWolf: //496
                case Monster.PowerUpBead:
                case Monster.Mon590N:
                    if (!info.Extra) ActionFeed.Add(new QueuedAction { Action = MirAction.召唤初现, Direction = Direction, Location = CurrentLocation });
                    break;
                case Monster.FrostTiger:
                case Monster.FlameTiger:
                case Monster.DragonArcher: //436
                case Monster.Kirin: //438
                case Monster.KingKong: //485
                    SitDown = info.Extra;
                    break;
                case Monster.ZumaStatue:
                case Monster.ZumaGuardian:
                case Monster.RedThunderZuma:
                case Monster.FrozenZumaStatue:
                case Monster.FrozenZumaGuardian:
                case Monster.ZumaTaurus:
                case Monster.DemonGuard:
                case Monster.Turtlegrass:
                case Monster.ManTree:
                case Monster.EarthGolem:
                case Monster.LightningScroll:
                case Monster.CallScroll:
                case Monster.PoisonScroll:
                case Monster.FireballScroll:
                case Monster.PurpleFaeFlower: //466
                case Monster.FeralFlameFurbolg: //522,
                case Monster.Mon544S: //544
                case Monster.Mon552N: //552
                case Monster.Mon575S:
                    Stoned = info.Extra;
                    break;
            }

            //Frames
            switch (BaseImage)
            {
                case Monster.GreatFoxSpirit:
                    Frames = FrameSet.GreatFoxSpirit[Stage];
                    break;
                case Monster.DragonStatue: //902
                    Frames = FrameSet.DragonStatue[(byte)Direction];
                    break;
                case Monster.HellBomb1:
                case Monster.HellBomb2:
                case Monster.HellBomb3:
                    Frames = FrameSet.HellBomb[((ushort)BaseImage) - 903];
                    break;
                case Monster.Mon380P:
                    Frames = FrameSet.Mon380P[(byte)Direction];
                    break;
                case Monster.SabukGate:
                    Frames = FrameSet.Gates[((ushort)BaseImage) - 950];
                    break;
                default:
                    if (BodyLibrary != null)
                    {
                        Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                    }
                    break;
            }

            SetAction();
            SetCurrentEffects();

            if (CurrentAction == MirAction.站立动作)
            {
                PlayAppearSound();

                if (Frame != null)
                {
                    FrameIndex = CMain.Random.Next(Frame.Count);
                }
            }
            else if (CurrentAction == MirAction.坐下动作)
            {
                PlayAppearSound();
            }

            NextMotion -= NextMotion % 100;

            if (Settings.Effect && !Dead)
            {
                switch (BaseImage)
                {
                    case Monster.Weaver:
                    case Monster.VenomWeaver:
                    case Monster.ArmingWeaver:
                    case Monster.ValeBat:
                    case Monster.CrackingWeaver:
                    case Monster.GreaterWeaver:
                        Effects.Add(new Effect(Libraries.Effect, 601, 1, 1 * Frame.Interval, this) { DrawBehind = true, Repeat = true }); // Blue effect                        
                        break;
                    case Monster.CrystalWeaver:
                    case Monster.FrozenZumaGuardian:
                    case Monster.FrozenRedZuma:
                    case Monster.FrozenZumaStatue:
                        Effects.Add(new Effect(Libraries.Effect, 600, 1, 1 * Frame.Interval, this) { DrawBehind = true, Repeat = true }); // Blue effect
                        break;
                    case Monster.Mon380P: //380
                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon380P], 10, 8, 2400, this) { Blend = true, Repeat = true });
                        break;
                    case Monster.Mon572B:
                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon572B], 376, 30, 3000, this) { Blend = true, Repeat = true, DrawBehind = true });
                        break;
                    case Monster.Mon575S:
                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon575S], 420 + (int)Direction * 8, 8, 2000, this) { Blend = true, Repeat = true });
                        break;
                    case Monster.Mon603B:
                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon603B], 439, 12, 3000, this) { Repeat = true, DrawBehind = true });
                        break;
                }
            }

            ProcessBuffs();
        }

        public void ProcessBuffs()
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                AddBuffEffect(Buffs[i]);
            }
        }

        public override bool ShouldDrawHealth()
        {
            string name = string.Empty;
            if (Name.Contains("(")) name = Name.Substring(Name.IndexOf("(") + 1, Name.Length - Name.IndexOf("(") - 2);

            return Name.EndsWith(string.Format("({0})", User.Name)) || MirScenes.Dialogs.GroupDialog.GroupList.Contains(name);
        }

        public override void Process()
        {
            bool update = CMain.Time >= NextMotion || GameScene.CanMove;
            SkipFrames = ActionFeed.Count > 1;

            ProcessFrames();

            if (Frame == null)
            {
                DrawFrame = 0;
                DrawWingFrame = 0;
            }
            else
            {
                DrawFrame = Frame.Start + (Frame.OffSet * (byte)Direction) + FrameIndex;
                DrawWingFrame = Frame.EffectStart + (Frame.EffectOffSet * (byte)Direction) + EffectFrameIndex;
            }


            #region Moving OffSet

            switch (CurrentAction)
            {
                case MirAction.行走动作:
                case MirAction.跑步动作:
                case MirAction.推开动作:
                case MirAction.弓箭跳跃:
                case MirAction.左冲动作:
                case MirAction.右冲动作:
                case MirAction.刺客冲击:
                    if (Frame == null)
                    {
                        OffSetMove = Point.Empty;
                        Movement = CurrentLocation;
                        break;
                    }
                    int i = CurrentAction == MirAction.跑步动作 ? 2 : 1;

                    if (CurrentAction == MirAction.弓箭跳跃) i = -JumpDistance;
                    if (CurrentAction == MirAction.刺客冲击) i = JumpDistance;

                    Movement = Functions.PointMove(CurrentLocation, Direction, CurrentAction == MirAction.推开动作 ? 0 : -i);

                    int count = Frame.Count;
                    int index = FrameIndex;

                    if (CurrentAction == MirAction.右冲动作 || CurrentAction == MirAction.左冲动作)
                    {
                        count = 3;
                        index %= 3;
                    }

                    switch (Direction)
                    {
                        case MirDirection.Up:
                            OffSetMove = new Point(0, (int)((MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.UpRight:
                            OffSetMove = new Point((int)((-MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.Right:
                            OffSetMove = new Point((int)((-MapControl.CellWidth * i / (float)(count)) * (index + 1)), 0);
                            break;
                        case MirDirection.DownRight:
                            OffSetMove = new Point((int)((-MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((-MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.Down:
                            OffSetMove = new Point(0, (int)((-MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.DownLeft:
                            OffSetMove = new Point((int)((MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((-MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.Left:
                            OffSetMove = new Point((int)((MapControl.CellWidth * i / (float)(count)) * (index + 1)), 0);
                            break;
                        case MirDirection.UpLeft:
                            OffSetMove = new Point((int)((MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                    }

                    OffSetMove = new Point(OffSetMove.X % 2 + OffSetMove.X, OffSetMove.Y % 2 + OffSetMove.Y);
                    break;
                default:
                    OffSetMove = Point.Empty;
                    Movement = CurrentLocation;
                    break;
            }

            #endregion

            DrawY = Movement.Y > CurrentLocation.Y ? Movement.Y : CurrentLocation.Y;

            DrawLocation = new Point((Movement.X - User.Movement.X + MapControl.OffSetX) * MapControl.CellWidth, (Movement.Y - User.Movement.Y + MapControl.OffSetY) * MapControl.CellHeight);
            DrawLocation.Offset(-OffSetMove.X, -OffSetMove.Y);
            DrawLocation.Offset(User.OffSetMove);
            DrawLocation = DrawLocation.Add(ManualLocationOffset);
            DrawLocation.Offset(GlobalDisplayLocationOffset);

            if (BodyLibrary != null && update)
            {
                FinalDrawLocation = DrawLocation.Add(BodyLibrary.GetOffSet(DrawFrame));
                DisplayRectangle = new Rectangle(DrawLocation, BodyLibrary.GetTrueSize(DrawFrame));
            }

            for (int i = 0; i < Effects.Count; i++)
                Effects[i].Process();

            Color colour = DrawColour;
            if (Poison == PoisonType.None)
                DrawColour = Color.White;
            if (Poison.HasFlag(PoisonType.Green))
                DrawColour = Color.Green;
            if (Poison.HasFlag(PoisonType.Red))
                DrawColour = Color.Red;
            if (Poison.HasFlag(PoisonType.Bleeding))
                DrawColour = Color.DarkRed;
            if (Poison.HasFlag(PoisonType.Slow))
                DrawColour = Color.Purple;
            if (Poison.HasFlag(PoisonType.Stun) || Poison.HasFlag(PoisonType.Dazed))
                DrawColour = Color.Yellow;
            if (Poison.HasFlag(PoisonType.Blindness))
                DrawColour = Color.MediumVioletRed;
            if (Poison.HasFlag(PoisonType.Frozen))
                DrawColour = Color.Blue;
            if (Poison.HasFlag(PoisonType.Paralysis) || Poison.HasFlag(PoisonType.LRParalysis))
                DrawColour = Color.Gray;
            if (Poison.HasFlag(PoisonType.DelayedExplosion))
                DrawColour = Color.Orange;
            if (colour != DrawColour) GameScene.Scene.MapControl.TextureValid = false;
        }

        public bool SetAction()
        {
            if (NextAction != null && !GameScene.CanMove)
            {
                switch (NextAction.Action)
                {
                    case MirAction.行走动作:
                    case MirAction.跑步动作:
                    case MirAction.推开动作:
                        return false;
                }
            }

            //IntelligentCreature
            switch (BaseImage)
            {
                case Monster.小猪:
                case Monster.小鸡:
                case Monster.小猫:
                case Monster.精灵骷髅:
                case Monster.白猪:
                case Monster.纸片人:
                case Monster.黑猫:
                case Monster.龙蛋:
                case Monster.火娃:
                case Monster.雪人:
                case Monster.青蛙:
                case Monster.红猴:
                case Monster.愤怒的小鸟:
                case Monster.阿福:
                case Monster.治疗拉拉:
                case Monster.猫咪超人:
                case Monster.龙宝宝:
                    BodyLibrary = Libraries.Pets[((ushort)BaseImage) - 10000];
                    break;
            }

            if (ActionFeed.Count == 0)
            {
                CurrentAction = Stoned ? MirAction.石化状态 : MirAction.站立动作;
                if (CurrentAction == MirAction.站立动作) CurrentAction = SitDown ? MirAction.坐下动作 : MirAction.站立动作;

                Frames.TryGetValue(CurrentAction, out Frame);

                if (MapLocation != CurrentLocation)
                {
                    GameScene.Scene.MapControl.RemoveObject(this);
                    MapLocation = CurrentLocation;
                    GameScene.Scene.MapControl.AddObject(this);
                }

                FrameIndex = 0;

                if (Frame == null) return false;

                FrameInterval = Frame.Interval;
            }
            else
            {
                QueuedAction action = ActionFeed[0];
                ActionFeed.RemoveAt(0);

                CurrentActionLevel = 0;
                CurrentAction = action.Action;
                CurrentLocation = action.Location;
                Direction = action.Direction;

                Point temp;
                switch (CurrentAction)
                {
                    case MirAction.行走动作:
                    case MirAction.跑步动作:
                    case MirAction.推开动作:
                        int i = CurrentAction == MirAction.跑步动作 ? 2 : 1;
                        temp = Functions.PointMove(CurrentLocation, Direction, CurrentAction == MirAction.推开动作 ? 0 : -i);
                        break;
                    case MirAction.弓箭跳跃:
                    case MirAction.刺客冲击:
                        temp = Functions.PointMove(CurrentLocation, Direction, JumpDistance);
                        break;
                    default:
                        temp = CurrentLocation;
                        break;
                }

                temp = new Point(action.Location.X, temp.Y > CurrentLocation.Y ? temp.Y : CurrentLocation.Y);

                if (MapLocation != temp)
                {
                    GameScene.Scene.MapControl.RemoveObject(this);
                    MapLocation = temp;
                    GameScene.Scene.MapControl.AddObject(this);
                }


                switch (CurrentAction)
                {
                    case MirAction.推开动作:
                        Frames.TryGetValue(MirAction.行走动作, out Frame);
                        break;
                    case MirAction.弓箭跳跃:
                        Frames.TryGetValue(MirAction.弓箭跳跃, out Frame);
                        break;
                    case MirAction.刺客冲击:
                        Frames.TryGetValue(MirAction.刺客冲击, out Frame);
                        break;
                    case MirAction.远程攻击1:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.近距攻击1, out Frame);
                        break;
                    case MirAction.远程攻击2:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.近距攻击2, out Frame);
                        break;
                    case MirAction.远程攻击3:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.近距攻击3, out Frame);
                        break;
                    case MirAction.特殊攻击:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.近距攻击1, out Frame);
                        break;
                    case MirAction.挖后尸骸:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.死后尸体, out Frame);
                        break;
                    case MirAction.切换LIB:
                        switch (BaseImage)
                        {
                            case Monster.Shinsu1:
                                BodyLibrary = Libraries.Monsters[(ushort)Monster.Shinsu];
                                BaseImage = Monster.Shinsu;
                                BaseSound = (ushort)BaseImage * 10;
                                Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                                Frames.TryGetValue(CurrentAction, out Frame);
                                break;
                            default:
                                Frames.TryGetValue(CurrentAction, out Frame);
                                break;
                        }
                        break;
                    case MirAction.死后尸体:
                        switch (BaseImage)
                        {
                            case Monster.Shinsu:
                            case Monster.Shinsu1:
                            case Monster.HolyDeva: //172
                            case Monster.GuardianRock:
                            case Monster.CharmedSnake://SummonSnakes
                            case Monster.HellKnight1:
                            case Monster.HellKnight2:
                            case Monster.HellKnight3:
                            case Monster.HellKnight4:
                            case Monster.HellBomb1:
                            case Monster.HellBomb2:
                            case Monster.HellBomb3:
                            case Monster.TurtleKing:
                            case Monster.PoisonHugger: //55
                            case Monster.Hugger: //56
                            case Monster.MutatedHugger: //67
                            case Monster.BoulderSpirit: //408
                            case Monster.IcePhantom: //429
                            case Monster.MysteriousMonk: //498
                            case Monster.MysteriousMage: //499
                            case Monster.Swain: //508
                            case Monster.SpectralWraith: //530
                            case Monster.BabyMagmaDragon: //531
                            case Monster.Mon572B:
                            case Monster.Mon575S:
                            case Monster.Mon579B:
                            case Monster.Mon580B:
                            case Monster.Mon581D:
                            case Monster.Mon582D:
                            case Monster.Mon584D:
                            case Monster.Mon585D:
                                Remove();
                                return false;
                            default:
                                Frames.TryGetValue(CurrentAction, out Frame);
                                break;
                        }
                        break;
                    default:
                        Frames.TryGetValue(CurrentAction, out Frame);
                        break;

                }

                FrameIndex = 0;

                if (Frame == null) return false;

                FrameInterval = Frame.Interval;

                Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                switch (CurrentAction)
                {
                    case MirAction.召唤初现:
                        PlaySummonSound();
                        switch (BaseImage)
                        {
                            case Monster.HellKnight1:
                            case Monster.HellKnight2:
                            case Monster.HellKnight3:
                            case Monster.HellKnight4:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 448, 10, 600, this));
                                break;
                            case Monster.Mon590N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 461, 16, 600, this));
                                break;
                        }
                        break;
                    case MirAction.石化苏醒:
                        PlayPopupSound();
                        break;
                    case MirAction.推开动作:
                        FrameIndex = Frame.Count - 1;
                        GameScene.Scene.Redraw();
                        break;
                    case MirAction.弓箭跳跃:
                        PlayJumpSound();
                        switch (BaseImage)
                        {
                            // Sanjian
                            case Monster.FurbolgGuard: //473
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgGuard], 414 + (int)Direction * 6, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Armadillo:
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 600, 12, 800, CurrentLocation, CMain.Time + 500));
                                break;
                        }
                        break;
                    case MirAction.刺客冲击: //刺客冲击动作
                        PlayDashSound();
                        switch (BaseImage)
                        {
                            case Monster.ChieftainSword: //414
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainSword], 1194 + (int)Direction * 9, 9, 1200, CurrentLocation));
                                break;
                            case Monster.Mon579B:
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon579B], 568, 4, 800, CurrentLocation, CMain.Time + 600));
                                break;
                        }
                        break;
                    case MirAction.行走动作:
                        GameScene.Scene.Redraw();
                        break;
                    case MirAction.跑步动作:
                        PlayRunSound();
                        GameScene.Scene.Redraw();
                        break;
                    case MirAction.近距攻击1:
                        PlayAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.FlamingWooma:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingWooma], 224 + (int)Direction * 7, 7, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ZumaTaurus:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ZumaTaurus], 244 + (int)Direction * 8, 8, 8 * FrameInterval, this));
                                break;
                            case Monster.FlamingMutant: //257
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 304, 10, 10 * 20, this) { Blend = true, DrawBehind = true });
                                break;

                            case Monster.DemonWolf: //323
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DemonWolf], 376 + (int)Direction * 9, 9, Frame.Count * Frame.Interval, this));
                                break;

                            case Monster.YinDevilNode: //217
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YinDevilNode], 26, 26, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.YangDevilNode: //216
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YangDevilNode], 26, 26, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.GreatFoxSpirit: //237
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GreatFoxSpirit], 355, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.EvilMir:
                                Effects.Add(new Effect(Libraries.Dragon, 60, 8, 8 * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Dragon, 68, 14, 14 * Frame.Interval, this));
                                byte random = (byte)CMain.Random.Next(7);
                                for (int i = 0; i <= 7 + random; i++)
                                {
                                    Point source = new Point(User.CurrentLocation.X + CMain.Random.Next(-7, 7), User.CurrentLocation.Y + CMain.Random.Next(-7, 7));

                                    MapControl.Effects.Add(new Effect(Libraries.Dragon, 230 + (CMain.Random.Next(5) * 10), 5, 400, source, CMain.Time + CMain.Random.Next(1000)));
                                }
                                break;
                            case Monster.CrawlerLave:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrawlerLave], 224 + (int)Direction * 6, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.HellKeeper: //275
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellKeeper], 32, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.IcePillar: //288
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePillar], 12, 6, 6 * 100, this));
                                break;
                            case Monster.TrollKing: //294
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TrollKing], 288, 6, 6 * Frame.Interval, this) { DrawBehind = true, Blend = true });
                                SoundManager.PlaySound(BaseSound + 6);
                                break;
                            case Monster.HellBomb1:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 61, 7, 7 * Frame.Interval, this));
                                break;
                            case Monster.HellBomb2:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 79, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.HellBomb3:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 97, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.SeedingsGeneral:
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1072 + (int)Direction * 9, 9, 9 * Frame.Interval, front, CMain.Time));
                                break;
                            case Monster.RestlessJar:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 384, 7, 7 * Frame.Interval, this));
                                break;
                            case Monster.AssassinBird: //358
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AssassinBird], 392, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.FlyingStatue:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 334, 10, 10 * 100, this));
                                break;
                            case Monster.Swain1: //509
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Swain1], 590, 26, 26 * 50, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.Serpentirian: //515
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Serpentirian], 400, 7, 7 * 200, this) { Blend = true, DrawBehind = true });
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Serpentirian], 407, 11, 900, this) { Start = CMain.Time + 600 });
                                break;
                            case Monster.SpectralWraith: //530
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SpectralWraith], 592, 9, 9 * 180, this) { Blend = true });
                                break;
                            case Monster.Mon544S: //544
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon544S], 544, 8, 8 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.Mon551N: //551
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon551N], 370, 10, 10 * 200, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.Mon554N: //554
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon554N], 346, 10, 10 * 200, this) { Blend = true });
                                break;
                            case Monster.Mon593N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon593N], 177 + (int)Direction * 10, 10, 10 * 80, this) { Blend = true, Start = CMain.Time + 900 });
                                break;
                            case Monster.Mon601N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon601N], 515 + (int)Direction * 10, 10, Frame.Count * Frame.Interval, this));
                                break;
                        }
                        break;
                    case MirAction.近距攻击2:
                        PlaySecondAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.CrystalSpider: //223
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalSpider], 272 + (int)Direction * 10, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Yimoogi:
                            case Monster.RedYimoogi: //208
                            case Monster.Snake10: //310
                            case Monster.Snake11:
                            case Monster.Snake12:
                            case Monster.Snake13:
                            case Monster.Snake14:
                            case Monster.Snake15:
                            case Monster.Snake16:
                            case Monster.Snake17: //317
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 304, 6, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Magic2, 1280, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ManectricKing: //286
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManectricKing], 648 + (int)Direction * 10, 10, 10 * 100, this));
                                break;
                            case Monster.FireCat: //331
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FireCat], 248 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.RhinoWarrior: //359
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoWarrior], 504, 17, 17 * 100, this) { DrawBehind = true, Blend = true });
                                break;
                            case Monster.DarkCaptain: //395
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkCaptain], 1238, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.FlyingStatue:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 352, 10, 10 * 100, this));
                                break;
                            case Monster.HornedSorceror: //406
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 552 + (int)Direction * 9, 9, 900, this));
                                break;
                            case Monster.Mon472N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon472N], 611, 10, 10 * 100, this){ Blend = true });
                                break;
                            case Monster.MutantBeserker: //492
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantBeserker], 432, 9, 9 * 200, this) { DrawBehind = true, Blend = true });
                                break;
                            case Monster.FeralTundraFurbolg: //521
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeralTundraFurbolg], 632 + (int)Direction * 7, 7, 7 * Frame.Interval, this) { Start = CMain.Time + 1200 });
                                break;
                            case Monster.Mon554N: //554
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon554N], 356 + (int)Direction * 8, 8, 8 * Frame.Interval, this) { DrawBehind = true, Blend = true });
                                break;
                            case Monster.Mon562N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon562N], 658, 8, 8 * 200, this) { Blend = true });
                                break;
                            case Monster.Mon563N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon563N], 664, 13, 13 * 200, this) { Blend = true });
                                break;
                            case Monster.Mon573B:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon573B], 555, 7, 7 * 200, this) { Blend = true });
                                break;
                            case Monster.Mon575S:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon575S], 403, 7, 7 * 200, this) { Blend = true });
                                break;
                            case Monster.Mon579B:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon579B], 558, 5, 5 * 160, this) { Blend = true });
                                break;
                        }

                        if ((ushort)BaseImage >= 10000)
                        {
                            PlayPetSound();
                        }
                        break;
                    case MirAction.近距攻击3:
                        PlayThirdAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.Yimoogi:
                            case Monster.RedYimoogi: //208
                            case Monster.Snake10:
                            case Monster.Snake11:
                            case Monster.Snake12:
                            case Monster.Snake13:
                            case Monster.Snake14:
                            case Monster.Snake15:
                            case Monster.Snake16:
                            case Monster.Snake17:
                                SoundManager.PlaySound(BaseSound + 9);
                                Effects.Add(new Effect(Libraries.Magic2, 1330, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SandSnail:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SandSnail], 448, 9, 900, this) { Blend = true });
                                break;
                            case Monster.PeacockSpider: //366
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PeacockSpider], 755, 21, 21 * Frame.Interval, this) { DrawBehind = true });
                                break;
                            case Monster.ShardMaiden: //484
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 439, 10, 2200, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.FeralTundraFurbolg: //521
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeralTundraFurbolg], 707 + (int)Direction * 5, 5, 5 * Frame.Interval, this) { Blend = true });
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeralTundraFurbolg], 747 + (int)Direction * 6, 6, 6 * Frame.Interval, this) { Start = CMain.Time + 1200 });
                                break;
                            case Monster.Mon571B:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon571B], 590, 13, 600, this) { Blend = true, DrawBehind = true });
                                break;
                        }
                        break;
                    case MirAction.近距攻击4:
                        PlayFourthAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.DarkOmaKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkOmaKing], 1742, 13, 13 * Frame.Interval, this));
                                break;
                            case Monster.Mon443N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon443N], 786, 1, 1  * Frame.Interval, this) { Repeat = true, RepeatUntil = CMain.Time + 3000});
                                break;
                            case Monster.CrystalBeast: //449
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1269, 7, 7 * 200, CurrentLocation));
                                break;
                            case Monster.FeralTundraFurbolg: //521
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeralTundraFurbolg], 795, 9, 1200, this) { Blend = true });
                                break;
                        }
                        break;
                    case MirAction.近距攻击5:
                        PlayFithAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        break;
                    case MirAction.远程攻击1:
                        PlayRangeSound();
                        TargetID = (uint)action.Params[0];
                        CurrentActionLevel = (byte)action.Params[4];
                        switch (BaseImage)
                        {
                            case Monster.Behemoth: //57
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 810, 10, 600, this));
                                break;
                            case Monster.KingScorpion: //180
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingScorpion], 272 + (int)Direction * 8, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.DarkDevil: //182
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkDevil], 272 + (int)Direction * 8, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ShamanZombie:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShamanZombie], 232 + (int)Direction * 12, 6, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShamanZombie], 328, 12, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.GuardianRock: //234
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GuardianRock], 12, 10, Frame.Count * Frame.Interval, this) { DrawBehind = true });
                                break;
                            case Monster.GreatFoxSpirit: //237
                                byte random = (byte)CMain.Random.Next(4);
                                for (int i = 0; i <= 4 + random; i++)
                                {
                                    Point source = new Point(User.CurrentLocation.X + CMain.Random.Next(-7, 7), User.CurrentLocation.Y + CMain.Random.Next(-7, 7));

                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GreatFoxSpirit], 375 + (CMain.Random.Next(3) * 20), 20, 1400, source, CMain.Time + CMain.Random.Next(600)));
                                }
                                break;
                            case Monster.EvilMir:
                                Effects.Add(new Effect(Libraries.Dragon, 90 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.DragonStatue: //902
                                Effects.Add(new Effect(Libraries.Dragon, 310 + ((int)Direction / 3) * 20, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.TurtleKing: //244
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], 946, 10, Frame.Count * Frame.Interval, User));
                                break;
                            case Monster.HellBolt: //276
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellBolt], 304, 11, 11 * 100, this) { DrawBehind = true });
                                break;
                            case Monster.WitchDoctor: //277
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WitchDoctor], 304, 9, 9 * 100, this));
                                break;
                            case Monster.FlyingStatue:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 304, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.NamelessGhost: //282
                                Effects.Add(new Effect(Libraries.Magic, 3840, 10, 10 * 100, this) { DrawBehind = true });
                                SoundManager.PlaySound(20000 + 46 * 10);//sound M46-0
                                break;
                            case Monster.DarkGhost: //283
                                Effects.Add(new Effect(Libraries.Magic2, 130, 6, 6 * 100, this));
                                SoundManager.PlaySound(20000 + 47 * 10);//sound M47-0
                                break;
                            case Monster.ChaosGhost: //284
                                Effects.Add(new Effect(Libraries.Magic2, 990, 10, 10 * 100, this) { DrawBehind = true });
                                SoundManager.PlaySound(20000 + 46 * 10);//sound M46-0
                                break;
                            case Monster.ManectricKing: //286
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManectricKing], 728, 12, 12 * 100, this));
                                break;
                            case Monster.IcePillar: //288
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePillar], 26, 6, 8 * 100, this) { Start = CMain.Time + 750 });
                                break;
                            case Monster.CatShaman: //336
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CatShaman], 786, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SeedingsGeneral:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1192 + (int)Direction * 9, 9, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.RestlessJar:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 471, 5, 500, this));
                                break;
                            case Monster.BurningZombie: //319
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BurningZombie], 368, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.AvengingSpirit: //388
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingSpirit], 352 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                break;
                            case Monster.ClawBeast: //393
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ClawBeast], 512 + (int)Direction * 8, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.DarkCaptain: //395
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkCaptain], 1224, 13, 13 * Frame.Interval, this));
                                break;
                            case Monster.FloatingRock:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 216, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.IcePhantom:  //429
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 712, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.LightningScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LightningScroll], 296, 3, 3 * Frame.Interval, this));
                                break;
                            case Monster.CrystalBeast: //449
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1276, 5, 5 * Frame.Interval, this, CMain.Time + 500));
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 368 + (int)Direction * 2, 2, 2 * Frame.Interval, this, CMain.Time + 2100) { Blend = false });
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 904 + (int)Direction * 2, 2, 2 * Frame.Interval, this, CMain.Time + 2100) { Blend = true });
                                break;
                            case Monster.PurpleFaeFlower: //466
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PurpleFaeFlower], 328, 3, 3 * Frame.Interval, this));
                                break;
                            case Monster.Mon472N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon472N], 464, 3, 3 * Frame.Interval, this));
                                break;
                            case Monster.ShardGuardian: //476
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShardGuardian], 438 + (int)Direction * 8, 8, 8 * 100, this) { Blend = true, DrawBehind = true, Start = CMain.Time + 800 });
                                break;
                            case Monster.CallScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CallScroll], 296, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.FireballScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FireballScroll], 296, 4, 4 * Frame.Interval, this));
                                break;
                            case Monster.HoodedSummoner: //481
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedSummoner], 352, 12, 12 * Frame.Interval, this) { DrawBehind = true });
                                break;
                            case Monster.HoodedIceMage: //482
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 431, 8, 8 * 80, this) { Blend = true });
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 439, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.MutantHighPriest: //494
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantHighPriest], 408, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.MysteriousMage: //495
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MysteriousMage], 450, 5, 5 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.FeralTundraFurbolg: //521
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeralTundraFurbolg], 688, 7, 900, this) { Start = CMain.Time + 600 });
                                break;
                            case Monster.SpectralWraith: //530
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SpectralWraith], 611, 6, 6 * Frame.Interval, this) { Blend = true});
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SpectralWraith], 601, 10, 10 * 100, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.Mon563N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon563N], 751 + (int)Direction * 6, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Mon572B:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon572B], 424, 1, 600, this) { Blend = true });
                                break;
                            case Monster.Mon586N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon586N], 216, 10, 10 * Frame.Interval, this) { Blend = true });
                                break;
                            case Monster.Mon601N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon601N], 344 + (int)Direction * 8, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Mon602N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon602N], 200 + (int)Direction * 8, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Mon603B:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon603B], 451, 15, 15 * 60, this));
                                break;
                        }
                        break;
                    case MirAction.远程攻击2:
                        PlaySecondRangeSound();
                        TargetID = (uint)action.Params[0];
                        CurrentActionLevel = (byte)action.Params[4];
                        switch (BaseImage)
                        {
                            case Monster.Behemoth: //57
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 825, 10, Frame.Count * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.TurtleKing: //244
                                byte random = (byte)CMain.Random.Next(4);
                                for (int i = 0; i <= 4 + random; i++)
                                {
                                    Point source = new Point(User.CurrentLocation.X + CMain.Random.Next(-7, 7), User.CurrentLocation.Y + CMain.Random.Next(-7, 7));

                                    Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], CMain.Random.Next(2) == 0 ? 922 : 934, 12, 1200, source, CMain.Time + CMain.Random.Next(600));
                                    ef.Played += (o, e) => SoundManager.PlaySound(20000 + (ushort)Spell.HellFire * 10 + 1);
                                    MapControl.Effects.Add(ef);
                                }
                                break;
                            case Monster.SeedingsGeneral:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1264, 9, 900, this));
                                break;
                            case Monster.AncientBringer: //329
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientBringer], 786, 10, 1000, this));
                                break;
                            case Monster.PeacockSpider: //BROKEN 366
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PeacockSpider], 776, 30, 30 * Frame.Interval, this));
                                break;
                            case Monster.DarkCaptain: //395
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkCaptain], 1258, 13, 13 * Frame.Interval, this));
                                break;
                            case Monster.IcePhantom: //429
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 712, 10, 10 * Frame.Interval, this) { DrawBehind = true });
                                break;
                            case Monster.ShardGuardian: //476
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShardGuardian], 517, 11, 11 * 100, this) { DrawBehind = true, Blend = true });
                                break;
                            case Monster.HoodedSummoner: //481
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedSummoner], 364, 10, 10 * 100, this) { DrawBehind = true, Blend = true });
                                break;
                            case Monster.HoodedIceMage: //482
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 431, 8, 8 * 80, this) { Blend = true });
                                break;
                            case Monster.MutantGuardian: //493
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantGuardian], 728, 13, 1000, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.MysteriousMage: //495
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MysteriousMage], 473, 5, 5 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.Serpentirian: //515
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Serpentirian], 432, 6, 900, this) { Start = CMain.Time + 600 });
                                break;
                            case Monster.MirEmperor: //534
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MirEmperor], 77, 11, 1000, this) { Blend = true });
                                break;
                            case Monster.Swain1: //509
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Swain1], 622, 5, 5 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.Mon570B:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon570B], 560, 6, 6 * Frame.Interval, this));
                                break;
                            case Monster.Mon572B:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon572B], 436, 23, 1000, this) { Blend = true, DrawBehind = true });
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon572B], 458, 17, 1000, this) { Blend = true });
                                break;
                        }
                        break;
                    case MirAction.远程攻击3:
                        PlayThirdRangeSound();
                        TargetID = (uint)action.Params[0];
                        CurrentActionLevel = (byte)action.Params[4];
                        switch (BaseImage)
                        {
                            case Monster.TurtleKing: //244
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], 946, 10, Frame.Count * Frame.Interval, User));
                                break;
                            case Monster.HoodedSummoner: //481
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShardMaiden], 661 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                break;
                            case Monster.NobleArcher: //503
                            case Monster.Mon557B: //557
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.NobleArcher], 504, 8, 8 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                break;
                            case Monster.Mon564N:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon564N], 801, 35, Frame.Count * FrameInterval, this));
                                break;

                        }
                        break;
                    case MirAction.被击动作:
                        uint attackerID = (uint)action.Params[0];
                        StruckWeapon = -2;
                        for (int i = 0; i < MapControl.Objects.Count; i++)
                        {
                            MapObject ob = MapControl.Objects[i];
                            if (ob.ObjectID != attackerID) continue;
                            if (ob.Race != ObjectType.Player) break;
                            PlayerObject player = ((PlayerObject)ob);
                            StruckWeapon = player.Weapon;
                            if (player.Class != MirClass.刺客 || StruckWeapon == -1) break; //弓箭?
                            StruckWeapon = 1;
                            break;
                        }
                        PlayFlinchSound();
                        PlayStruckSound();
                        // Sanjian
                        switch (BaseImage)
                        {
                            case Monster.GlacierBeast: //474
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierBeast], 304, 6, 400, this));
                                break;
                        }
                        break;
                    case MirAction.死亡动作:
                        switch (BaseImage)
                        {

                            case Monster.DarkDevil: //182
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkDevil], 336, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ShamanZombie:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShamanZombie], 224, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.RoninGhoul:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RoninGhoul], 224, 10, Frame.Count * FrameInterval, this));
                                break;
                            case Monster.BoneCaptain:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BoneCaptain], 224 + (int)Direction * 10, 10, Frame.Count * FrameInterval, this));
                                break;
                            case Monster.RightGuard: //205
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RightGuard], 320, 5, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.LeftGuard: //206
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LeftGuard], 296 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                break;
                            case Monster.FrostTiger: //222
                            case Monster.FlameTiger: //228
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrostTiger], 304, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Yimoogi:
                            case Monster.RedYimoogi: //208
                            case Monster.Snake10:
                            case Monster.Snake11:
                            case Monster.Snake12:
                            case Monster.Snake13:
                            case Monster.Snake14:
                            case Monster.Snake15:
                            case Monster.Snake16:
                            case Monster.Snake17:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Yimoogi], 352, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.YinDevilNode: //217
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YinDevilNode], 52, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.YangDevilNode: //216
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YangDevilNode], 52, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.BlackFoxman: //230
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackFoxman], 224, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.VampireSpider:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.VampireSpider], 296, 5, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.CharmedSnake:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CharmedSnake], 40, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Manticore: //434
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Manticore], 632, 9, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ValeBat:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ValeBat], 224, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SpiderBat:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SpiderBat], 224, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.VenomWeaver:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.VenomWeaver], 224, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.HellBolt: //276
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellBolt], 325, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SabukGate: //950 沙巴克大门
                                Effects.Add(new Effect(Libraries.Gates[(ushort)Monster.SabukGate - 950], 41, 10, Frame.Count * Frame.Interval, this) { Light = -1 });
                                break;
                            case Monster.WingedTigerLord: //229
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WingedTigerLord], 650 + (int)Direction * 5, 5, Frame.Count * FrameInterval, this));
                                break;
                            case Monster.HellKnight1: //300
                            case Monster.HellKnight2: //301
                            case Monster.HellKnight3: //302
                            case Monster.HellKnight4: //303
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 448, 10, 600, this));
                                break;
                            case Monster.IceGuard: //306
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IceGuard], 256, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.DeathCrawler: //318
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DeathCrawler], 376, 9, 900, CurrentLocation, CMain.Time + 900));
                                break;

                            case Monster.EarthGolem: //363
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.EarthGolem], 432, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.CreeperPlant: //383
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 266, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SackWarrior:  //396
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SackWarrior], 480, 10, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SackWarrior], 490, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FrozenSoldier: //424
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenSoldier], 256, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FrozenGolem: //428
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenGolem], 456, 7, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenGolem], 463, 10, Frame.Count * Frame.Interval, this) { DrawBehind = true });
                                break;
                            case Monster.DragonWarrior: //435
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DragonWarrior], 504 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                break;
                            case Monster.FloatingRock:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 152, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.AvengingSpirit: //388
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingSpirit], 450 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.PoisonScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PoisonScroll], 282, 11, 11 * Frame.Interval, this));
                                break;
                        }
                        PlayDieSound();
                        break;
                    case MirAction.死后尸体:
                        GameScene.Scene.Redraw();
                        GameScene.Scene.MapControl.SortObject(this);
                        if (MouseObject == this) MouseObjectID = 0;
                        if (TargetObject == this) TargetObjectID = 0;
                        if (MagicObject == this) MagicObjectID = 0;

                        for (int i = 0; i < Effects.Count; i++)
                            Effects[i].Remove();

                        DeadTime = CMain.Time;

                        break;
                }

            }

            GameScene.Scene.MapControl.TextureValid = false;

            NextMotion = CMain.Time + FrameInterval;

            return true;
        }

        public void SetCurrentEffects()
        {
            //BindingShot
            if (BindingShotCenter && ShockTime > CMain.Time)
            {
                int effectid = TrackableEffect.GetOwnerEffectID(ObjectID);
                if (effectid >= 0)
                    TrackableEffect.effectlist[effectid].RemoveNoComplete();

                TrackableEffect NetDropped = new TrackableEffect(new Effect(Libraries.MagicC, 7, 1, 1000, this) { Repeat = true, RepeatUntil = (ShockTime - 1500) });
                NetDropped.Complete += (o1, e1) =>
                {
                    SoundManager.PlaySound(20000 + 130 * 10 + 6);//sound M130-6
                    Effects.Add(new TrackableEffect(new Effect(Libraries.MagicC, 8, 8, 700, this)));
                };
                Effects.Add(NetDropped);
            }
            else if (BindingShotCenter && ShockTime <= CMain.Time)
            {
                int effectid = TrackableEffect.GetOwnerEffectID(ObjectID);
                if (effectid >= 0)
                    TrackableEffect.effectlist[effectid].Remove();

                //SoundManager.PlaySound(20000 + 130 * 10 + 6);//sound M130-6
                //Effects.Add(new TrackableEffect(new Effect(Libraries.ArcherMagic, 8, 8, 700, this)));

                ShockTime = 0;
                BindingShotCenter = false;
            }

        }


        private void ProcessFrames()
        {
            if (Frame == null) return;

            switch (CurrentAction)
            {
                case MirAction.行走动作:
                    if (!GameScene.CanMove) return;

                    GameScene.Scene.MapControl.TextureValid = false;

                    if (SkipFrames) UpdateFrame();

                    if (UpdateFrame() >= Frame.Count)
                    {
                        FrameIndex = Frame.Count - 1;
                        SetAction();
                    }
                    else
                    {
                        switch (FrameIndex)
                        {
                            case 1:
                                PlayWalkSound(true);
                                break;
                            case 4:
                                PlayWalkSound(false);
                                break;
                        }
                    }
                    break;
                case MirAction.跑步动作:
                    if (!GameScene.CanMove) return;

                    GameScene.Scene.MapControl.TextureValid = false;

                    if (SkipFrames) UpdateFrame();

                    if (UpdateFrame() >= Frame.Count)
                    {
                        FrameIndex = Frame.Count - 1;
                        SetAction();
                    }
                    break;
                case MirAction.推开动作:
                    if (!GameScene.CanMove) return;

                    GameScene.Scene.MapControl.TextureValid = false;

                    FrameIndex -= 2;

                    if (FrameIndex < 0)
                    {
                        FrameIndex = 0;
                        SetAction();
                    }
                    break;
                case MirAction.弓箭跳跃:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.刺客冲击:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.HornedSorceror: //406
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 644 + (int)Direction * 5, 5, 500, CurrentLocation));
                                                break;
                                            case Monster.ChieftainSword: //414
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainSword], 1266 + (int)Direction * 6, 6, 600, front, CMain.Time));
                                                break;
                                            case Monster.AncientStoneGolem: //514
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientStoneGolem], 392, 7, 7 * 200, CurrentLocation));
                                                break;
                                            case Monster.Butcher: //516
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Butcher], 528, 8, 8 * 200, CurrentLocation));
                                                break;
                                            case Monster.Mon571B:
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon571B], 603 + (int)Direction * 8, 8, 500, CurrentLocation));
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon571B], 667 + (int)Direction * 8, 8, 500, CurrentLocation));
                                                break;
                                        }
                                    }
                                    break;
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.石化苏醒:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            switch (BaseImage)
                            {
                                case Monster.ZumaStatue:
                                case Monster.ZumaGuardian:
                                case Monster.RedThunderZuma:
                                case Monster.FrozenRedZuma:
                                case Monster.FrozenZumaStatue:
                                case Monster.FrozenZumaGuardian:
                                case Monster.ZumaTaurus:
                                case Monster.DemonGuard:
                                case Monster.EarthGolem:
                                case Monster.Turtlegrass:
                                case Monster.ManTree:
                                case Monster.LightningScroll:
                                case Monster.CallScroll:
                                case Monster.PoisonScroll:
                                case Monster.FireballScroll:
                                case Monster.PurpleFaeFlower: //466
                                case Monster.FeralFlameFurbolg: //522,
                                case Monster.Mon544S: //544
                                case Monster.Mon552N: //552
                                case Monster.Mon575S:
                                    Stoned = false;
                                    break;
                                case Monster.Shinsu:
                                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Shinsu1];
                                    BaseImage = Monster.Shinsu1;
                                    BaseSound = (ushort)BaseImage * 10;
                                    Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                                    break;
                                case Monster.ShardGuardian: //476
                                    BodyLibrary = Libraries.Monsters[(ushort)Monster.GlacierWarrior];
                                    BaseImage = Monster.GlacierWarrior;
                                    BaseSound = (ushort)BaseImage * 10;
                                    Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                                    break;
                                case Monster.Swain: //508
                                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Swain1];
                                    BaseImage = Monster.Swain1;
                                    BaseSound = (ushort)BaseImage * 10;
                                    SoundManager.PlaySound(5085);
                                    Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                                    break;
                            }

                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.CreeperPlant: //383
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 250, 6, 6 * 100, this));
                                                break;
                                            //case Monster.Mon544S: //544 暂不知做什么的特效
                                            //Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon544S], 552, 8, Frame.Count * Frame.Interval, this) { Blend = true });
                                            //Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon544S], 560, 8, Frame.Count * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            //break;
                                        }
                                        break;
                                    }
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.切换LIB:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            switch (BaseImage)
                            {
                                case Monster.CannibalPlant: //10
                                case Monster.WaterDragon:
                                case Monster.CreeperPlant: //383
                                case Monster.LivingVines: //490
                                case Monster.ManEatingPlant: //490
                                case Monster.EvilCentipede:
                                case Monster.DigOutZombie:
                                case Monster.Armadillo:
                                case Monster.ArmadilloElder:
                                    Remove();
                                    return;
                                case Monster.ZumaStatue:
                                case Monster.ZumaGuardian:
                                case Monster.RedThunderZuma:
                                case Monster.FrozenRedZuma:
                                case Monster.FrozenZumaStatue:
                                case Monster.FrozenZumaGuardian:
                                case Monster.ZumaTaurus:
                                case Monster.DemonGuard:
                                case Monster.EarthGolem:
                                case Monster.Turtlegrass:
                                case Monster.ManTree:
                                case Monster.LightningScroll:
                                case Monster.CallScroll:
                                case Monster.PoisonScroll:
                                case Monster.FireballScroll:
                                case Monster.PurpleFaeFlower: //466
                                case Monster.Mon544S: //544
                                case Monster.Mon552N: //552
                                case Monster.Mon575S:
                                    Stoned = true;
                                    return;
                            }

                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.召唤初现:
                case MirAction.站立动作:
                case MirAction.石化状态:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            if (CurrentAction == MirAction.站立动作)
                            {
                                switch (BaseImage)
                                {
                                    case Monster.SnakeTotem://SummonSnakes Totem
                                        if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "SnakeTotem") < 0)
                                            Effects.Add(new TrackableEffect(new Effect(Libraries.Monsters[(ushort)Monster.SnakeTotem], 2, 10, 1500, this) { Repeat = true }, "SnakeTotem"));
                                        break;
                                    case Monster.StoningSpider://419
                                        if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "StoneTrap") < 0)
                                            Effects.Add(new TrackableEffect(new Effect(Libraries.Monsters[(ushort)Monster.StoningSpider], 128, 10, 1500, this) { Repeat = true }, "StoneTrap"));
                                        break;
                                    case Monster.PalaceWall1:
                                        //Effects.Add(new Effect(Libraries.Effect, 196, 1, 1000, this) { DrawBehind = true, d });
                                        //Libraries.Effect.Draw(196, DrawLocation, Color.White, true);
                                        //Libraries.Effect.DrawBlend(196, DrawLocation, Color.White, true);
                                        break;
                                }
                            }

                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.近距攻击1:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            if (SetAction())
                            {
                                switch (BaseImage)
                                {
                                    case Monster.EvilCentipede:
                                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.EvilCentipede], 42, 10, 600, this));
                                        break;
                                    case Monster.ToxicGhoul:
                                        SoundManager.PlaySound(BaseSound + 3);
                                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ToxicGhoul], 224 + (int)Direction * 6, 6, 600, this));
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.Kirin: //438
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Kirin], 832 + (int)Direction * 5, 5, 500, this));
                                                break;

                                            case Monster.Jar1: //337
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Jar1], 130 + (int)Direction * 3, 3, 300, this));
                                                break;
                                            case Monster.Jar2: //338
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Jar2], 624 + (int)Direction * 8, 8, 800, this));
                                                break;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.ManEatingPlant: //499
                                                Effect ef499 = new Effect(Libraries.Monsters[(ushort)Monster.ManEatingPlant], 456, 5, 200, front, CMain.Time + 200);
                                                MapControl.Effects.Add(ef499);
                                                break;

                                        }
                                    }
                                    break;
                                case 3:
                                    {
                                        PlaySwingSound();
                                        switch (BaseImage)
                                        {

                                            case Monster.Shinsu1:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Shinsu1], 224 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;

                                            case Monster.ArmadilloElder:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ArmadilloElder], 488 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;

                                            case Monster.AvengingSpirit: //388
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingSpirit], 288 + (int)Direction * 8, 8, 8 * Frame.Interval, this));
                                                break;

                                            case Monster.BlackTortoise: //433
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackTortoise], 360, 6, 6 * Frame.Interval, front, CMain.Time));
                                                break;
                                        }
                                        break;
                                    }
                                case 4: // 击中目标动画
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.Behemoth: //57
                                                Point Behemoth1 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 716, 5, 200, Behemoth1, CMain.Time) { Blend = true });
                                                break;
                                            case Monster.Armadillo:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Armadillo], 480 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.StainHammerCat: //333
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StainHammerCat], 296 + (int)Direction * 6, 6, 6 * Frame.Interval, this) { DrawBehind = true, Blend = true });
                                                break;
                                            case Monster.AvengerPlant: //386
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengerPlant], 280, 6, 600, front, CMain.Time));
                                                break;
                                            case Monster.HornedMage: //400
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedMage], 783, 9, 800, this));
                                                break;
                                            case Monster.DarkWraith: //447
                                                Effect darkWraithEffect = new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 784, 6, 600, front, CMain.Time);
                                                MapControl.Effects.Add(darkWraithEffect);
                                                break;

                                            case Monster.FlamingMutant: //257
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 314, 6, 200, front));
                                                break;

                                            case Monster.WaterDragon: //432
                                                Effect waterDragonEffect = new Effect(Libraries.Monsters[(ushort)Monster.WaterDragon], 905, 9, 900, front, CMain.Time + 300);
                                                MapControl.Effects.Add(waterDragonEffect);
                                                break;
                                            case Monster.Mon443N:
                                                Point Point430 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon443N], 608, 6, 60, Point430, CMain.Time + 600));
                                                break;
                                            case Monster.CrystalBeast:  //449
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1112, 6, 300, front));
                                                break;
                                            case Monster.PurpleFaeFlower: //466
                                                Effect purpleFaeFlowerEffect = new Effect(Libraries.Monsters[(ushort)Monster.PurpleFaeFlower], 500, 7, 700, front, CMain.Time + 300);
                                                MapControl.Effects.Add(purpleFaeFlowerEffect);
                                                break;
                                            case Monster.Mon472N:
                                                Effect Mon472NEffect = new Effect(Libraries.Monsters[(ushort)Monster.Mon472N], 604, 7, 700, front, CMain.Time + 300);
                                                MapControl.Effects.Add(Mon472NEffect);
                                                break;
                                            case Monster.GlacierWarrior: //475
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierWarrior], 352, 6, 200, front));
                                                break;
                                            case Monster.ShardGuardian: //476
                                                Point Point476 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShardGuardian], 432, 6, 200, Point476, CMain.Time));
                                                break;
                                            case Monster.HoodedIceMage: //482
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 424, 7, 500, front, CMain.Time));
                                                break;
                                            case Monster.HoodedPriest: //483
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedPriest], 416, 6, 500, front, CMain.Time));
                                                break;
                                            case Monster.KingKong: //485
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingKong], 328, 5, 300, front, CMain.Time));
                                                break;
                                            case Monster.ReaperPriest: //487
                                                MapControl.Effects.Add(new Effect(Libraries.Magic3, 420, 8, 600, front, CMain.Time + 300));
                                                break;
                                            case Monster.Swain1: //509
                                                Effect Swain1Effect = new Effect(Libraries.Monsters[(ushort)Monster.Swain1], 616, 6, 300, front, CMain.Time + 300);
                                                MapControl.Effects.Add(Swain1Effect);
                                                break;
                                            case Monster.UndeadHammerDwarf:  //512
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.UndeadHammerDwarf], 272, 9, 600, front, CMain.Time));
                                                break;
                                            case Monster.Serpentirian: //515
                                                MapObject ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Serpentirian], 418, 3, 600, ob));
                                                break;
                                            case Monster.Mon543N: //543
                                                Point Point543 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon543N], 328, 10, 800, Point543, CMain.Time) { DrawBehind = true, Blend = true });
                                                break;
                                            case Monster.Mon550N: //550
                                                Effect Effect550 = new Effect(Libraries.Monsters[(ushort)Monster.Mon550N], 442, 8, 300, front, CMain.Time + 300);
                                                MapControl.Effects.Add(Effect550);
                                                break;
                                            case Monster.Mon571B:
                                                Effect Effect571 = new(Libraries.Monsters[(ushort)Monster.Mon571B], 500, 10, 300, front, CMain.Time + 300) { DrawBehind = true, Blend = true };
                                                MapControl.Effects.Add(Effect571);
                                                break;
                                            case Monster.Mon573B:
                                                Effect Mon573BEffect = new Effect(Libraries.Monsters[(ushort)Monster.Mon573B], 522, 9, 300, front, CMain.Time + 600);
                                                MapControl.Effects.Add(Mon573BEffect);
                                                break;
                                            case Monster.Mon575S:
                                                Effect Mon575SEffect1 = new Effect(Libraries.Monsters[(ushort)Monster.Mon575S], 394, 9, 300, front, CMain.Time + 300);
                                                MapControl.Effects.Add(Mon575SEffect1);
                                                break;
                                            case Monster.Mon590N:
                                                Effect Mon590NEffect = new Effect(Libraries.Monsters[(ushort)Monster.Mon590N], 456, 6, 300, front, CMain.Time);
                                                MapControl.Effects.Add(Mon590NEffect);
                                                break;
                                        }
                                    }
                                    break;
                                case 5:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FurbolgGuard: //473
                                                MapObject ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgGuard], 384, 7, 600, ob));
                                                break;
                                            case Monster.FlyingStatue:
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 344, 8, 800, front, CMain.Time));
                                                break;

                                            case Monster.PlagueCrab: //382
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PlagueCrab], 496 + (int)Direction * 8, 8, 8 * Frame.Interval, this));
                                                break;

                                            case Monster.HornedSorceror: //406
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 536 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.FrozenMagician://444
                                                Effect frozenMagicianEffect = new Effect(Libraries.Monsters[(ushort)Monster.FrozenMagician], 960, 6, 100, front, CMain.Time + 500);
                                                MapControl.Effects.Add(frozenMagicianEffect);
                                                break;
                                        }
                                        break;
                                    }
                                case 6:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FrozenMiner: //442
                                                Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                                Effect ef4420 = new Effect(Libraries.Monsters[(ushort)Monster.FrozenMiner], 512, 6, 60, source);
                                                MapControl.Effects.Add(ef4420);
                                                break;
                                            case Monster.IceCrystalSoldier: //446
                                                source = Functions.PointMove(CurrentLocation, Direction, 1);
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.IceCrystalSoldier], 464, 6, 600, source, CMain.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                        }
                                        break;
                                    }
                                case 7:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.AxePlant: //391
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AxePlant], 256 + (int)Direction * 10, 10, 10 * Frame.Interval, this) { DrawBehind = true });
                                                break;
                                            case Monster.TreeQueen://Fire Bombardment //365
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeQueen], 35, 13, 1300, this) { Blend = true });
                                                break;
                                        }
                                        break;
                                    }
                                    //case 8:
                                    //    {
                                    //        MapObject ob = MapControl.GetObject(TargetID);
                                    //        switch (BaseImage)
                                    //        {

                                    //        }
                                    //        break;
                                    //    }
                                    //case 9:
                                    //    {
                                    //        switch (BaseImage)
                                    //        {

                                    //        }
                                    //        break;
                                    //    }
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.坐下动作:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.近距攻击2:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.雪人:
                                                if (FrameIndex == 1)
                                                {
                                                    if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "SnowmanSnow") < 0)
                                                        Effects.Add(new TrackableEffect(new Effect(Libraries.Pets[((ushort)BaseImage) - 10000], 208, 11, 1500, this), "SnowmanSnow"));
                                                }
                                                break;
                                            case Monster.CannibalTentacles: //353
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CannibalTentacles], 408 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.DarkOmaKing: //378
                                                SoundManager.PlaySound(BaseSound + 6, false, 800);
                                                SoundManager.PlaySound(BaseSound + 6, false, 1500);
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkOmaKing], 1640, 30, 30 * Frame.Interval, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 2:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.SnowWolfKing: //431
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolfKing], 520, 9, 9 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                                break;
                                        }
                                    }
                                    break;
                                case 3:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.GlacierBeast: //474
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierBeast], 366, 6, 500, front, CMain.Time));
                                                break;
                                            case Monster.FlameQueen: //299
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameQueen], 720, 9, Frame.Count * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                                break;
                                            case Monster.DeathCrawler: //318                                          
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DeathCrawler], 376, 9, 9 * Frame.Interval, front, CMain.Time) { Blend = true, DrawBehind = true });
                                                break;
                                            case Monster.TucsonMage: //345
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonMage], 296 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.Armadillo:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Armadillo], 504 + (int)Direction * 12, 12, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.RhinoWarrior: //359
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoWarrior], 376 + (int)Direction * 8, 8, 100, this) { Blend = true, DrawBehind = true });
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoWarrior], 440 + (int)Direction * 8, 8, 100, this) { Blend = true });
                                                break;
                                            case Monster.TreeQueen: //365
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeQueen], 66, 16, 16 * Frame.Interval, this) { DrawBehind = true });
                                                break;
                                            case Monster.SnowWolf: //430                                            
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolf], 328, 9, Frame.Count * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                                break;
                                            case Monster.BlackTortoise: //433                                               
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackTortoise], 422, 6, 6 * Frame.Interval, front, CMain.Time));
                                                break;
                                            case Monster.FurbolgCommander: //471
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgCommander], 389, 6, 600, front, CMain.Time));
                                                break;
                                            case Monster.GlacierWarrior: //475
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierWarrior], 414, 7, 200, front));
                                                break;
                                            case Monster.BloodLord: //532
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BloodLord], 400, 10, 400, front, CMain.Time + 600) { Blend = true, DrawBehind = true });
                                                break;
                                            case Monster.Mon548N: //544 
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon548N], 536, 8, 200, front, CMain.Time + 800));
                                                break;
                                        }
                                    }
                                    break;
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.GlacierSnail://468
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierSnail], 344 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;

                                            case Monster.TucsonWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonWarrior], 360 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                                break;
                                            case Monster.DarkWraith: //447
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 870, 6, 600, front, CMain.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.HornedWarrior: //403
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedWarrior], 880 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.Turtlegrass: //410
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Turtlegrass], 360 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.ReaperWizard:
                                                Effect ef2 = new Effect(Libraries.Monsters[(ushort)Monster.ReaperWizard], 476, 7, 600, front, CMain.Time);
                                                MapControl.Effects.Add(ef2);
                                                break;
                                            case Monster.Mon443N:
                                                Point FA4431 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon443N], 694, 6, 60, FA4431, CMain.Time + 600));
                                                break;
                                            case Monster.HammerDwarf: //500 
                                                Point HD500 = Functions.PointMove(CurrentLocation, Direction, 0);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HammerDwarf], 520 + (int)Direction * 6, 6, 200, HD500, CMain.Time + 300));
                                                break;
                                            case Monster.Mon575S:
                                                Effect Mon575SEffect2 = new Effect(Libraries.Monsters[(ushort)Monster.Mon575S], 410, 10, 600, front, CMain.Time + 600);
                                                MapControl.Effects.Add(Mon575SEffect2);
                                                break;
                                            case Monster.Mon603B:
                                                Effect Mon603BEffect = new Effect(Libraries.Monsters[(ushort)Monster.Mon603B], 432, 7, 300, front, CMain.Time);
                                                MapControl.Effects.Add(Mon603BEffect);
                                                break;
                                        }
                                    }
                                    break;
                                case 5:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.Behemoth: //57
                                                Point Behemoth2 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 721, 9, 300, Behemoth2, CMain.Time) { Blend = true });
                                                break;
                                            case Monster.GeneralMeowMeow: //341
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GeneralMeowMeow], 496 + (int)Direction * 7, 7, 7 * Frame.Interval, this) { Blend = true });
                                                break;
                                            case Monster.TucsonGeneral: //354
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonGeneral], 544, 8, 8 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                                break;
                                            case Monster.FrozenMiner: //442
                                                Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                                Effect ef4421 = new Effect(Libraries.Monsters[(ushort)Monster.FrozenMiner], 598, 6, 60, source, CMain.Time + 200);
                                                MapControl.Effects.Add(ef4421);
                                                break;
                                            case Monster.IceCrystalSoldier: //446
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.IceCrystalSoldier], 470, 6, 600, front, CMain.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.Bear: //412
                                                Effect bleedEffect = new Effect(Libraries.Monsters[(ushort)Monster.Bear], 312, 9, 900, front, CMain.Time);
                                                MapControl.Effects.Add(bleedEffect);
                                                break;
                                            case Monster.Manticore: //434
                                                Point source2 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Manticore], 569, 7, 800, source2, CMain.Time));
                                                break;
                                            case Monster.FlyingStatue:
                                                Effect flyingStatueEffect1 = new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 362, 8, 800, front, CMain.Time);
                                                MapControl.Effects.Add(flyingStatueEffect1);
                                                break;
                                            case Monster.NobleWarrior: //502
                                            case Monster.Mon556B: //556
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.NobleWarrior], 464, 8, 300, this));
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.NobleWarrior], 478, 12, 300, this));
                                                break;
                                            case Monster.Mon543N: //543
                                                Point sourceMon543N = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon543N], 338, 11, 800, sourceMon543N, CMain.Time) { Blend = true });
                                                break;
                                            case Monster.Mon554N: //554
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon554N], 468, 10, 300, front, CMain.Time + 300));
                                                break;
                                        }
                                    }
                                    break;
                                case 6:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.SeedingsGeneral:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1144 + (int)Direction * 6, 6, 600, this));
                                                break;
                                            case Monster.OmaBlest: //370
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaBlest], 392 + (int)Direction * 5, 5, 500, this) { DrawBehind = true });
                                                break;
                                            case Monster.WereTiger: //397
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WereTiger], 344 + (int)Direction * 6, 6, 600, this));
                                                break;
                                            case Monster.Kirin: //438
                                                Point source2 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.Kirin], 872, 8, 800, source2, CMain.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.MysteriousMonk: //498
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MysteriousMonk], 584 + (int)Direction * 10, 10, 200, front, CMain.Time + 600));
                                                break;
                                        }
                                    }
                                    break;
                                case 7:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.ElephantMan: //361
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ElephantMan], 368, 9, 900, this) { Blend = true, DrawBehind = true });
                                                break;
                                            case Monster.ScalyBeast: //406
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ScalyBeast], 408 + (int)Direction * 3, 3, 3 * Frame.Interval, this) { Blend = false });
                                                break;
                                        }
                                    }
                                    break;
                                case 8:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.StrayCat: //335
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StrayCat], 688 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 9:
                                    {
                                        switch (BaseImage) //406
                                        {
                                            case Monster.ScalyBeast:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ScalyBeast], 429, 3, 300, this) { Blend = true, DrawBehind = true });
                                                break;
                                        }
                                    }
                                    break;
                                case 10:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.StoningStatue:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StoningStatue], 642, 15, 15 * 100, this));
                                                SoundManager.PlaySound(BaseSound + 7);
                                                break;
                                        }
                                    }
                                    break;
                                case 11:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.BlackHammerCat: //334
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackHammerCat], 768 + (int)Direction * 8, 8, 800, this) { Blend = true, DrawBehind = true });
                                                break;
                                        }
                                    }
                                    break;
                                case 19:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.StoningStatue:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StoningStatue], 624, 8, 8 * 100, this));
                                                break;
                                        }
                                    }
                                    break;
                            }
                            if (FrameIndex == 3) PlaySwingSound();
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.近距攻击3:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    switch (BaseImage)
                                    {
                                        case Monster.火娃:
                                            if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "CreatureFlame") < 0)
                                                Effects.Add(new TrackableEffect(new Effect(Libraries.Pets[((ushort)BaseImage) - 10000], 280, 4, 800, this), "CreatureFlame"));
                                            break;
                                        case Monster.GasToad: //355
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GasToad], 440, 9, 9 * Frame.Interval, this));
                                            break;
                                        case Monster.Mon409B:
                                            {
                                                int loops = CurrentActionLevel;
                                                int duration = 7 * FrameInterval;
                                                int totalDuration = loops * duration;

                                                if (FrameLoop == null)

                                                {
                                                    for (int i = 0; i < loops; i++)
                                                    {
                                                        SoundManager.PlaySound(4088, false, 0 + (i * duration));
                                                    }
                                                    LoopFrame(FrameIndex, 3, FrameInterval, totalDuration);
                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon409B], 848 + (int)Direction * 7, 7, duration, this) { Repeat = true, RepeatUntil = CMain.Time + totalDuration });
                                                }

                                                {
                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon409B], 904, 8, 8 * Frame.Interval, this, CMain.Time + totalDuration) { Blend = true });
                                                    SoundManager.PlaySound(4076, false);
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case 2:
                                    switch (BaseImage)
                                    {
                                        case Monster.ReaperAssassin: //489
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ReaperAssassin], 513, 9, 300, front, CMain.Time + 300));
                                            break;
                                        case Monster.MysteriousAssassin: //497
                                            Effect ef497 = new Effect(Libraries.Monsters[(ushort)Monster.MysteriousAssassin], 560, 5, 900, front, CMain.Time);
                                            MapControl.Effects.Add(ef497);
                                            break;
                                        case Monster.Butcher: //516
                                            Point source516 = Functions.PointMove(CurrentLocation, Direction, 1);
                                            Effect ef516 = new Effect(Libraries.Monsters[(ushort)Monster.Butcher], 616 + (int)Direction * 8, 8, 500, source516, CMain.Time + 600);
                                            MapControl.Effects.Add(ef516);
                                            break;
                                    }
                                    break;
                                case 3:
                                    switch (BaseImage)
                                    {
                                        case Monster.WingedTigerLord: //229
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WingedTigerLord], 632, 8, 600, this, 0, true));
                                            break;
                                        case Monster.DarkWraith: //447
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 876 + (int)Direction * 5, 5, 500, this));
                                            break;
                                        case Monster.HornedSorceror: //406
                                            {
                                                int loops = CurrentActionLevel;
                                                int duration = 5 * FrameInterval;
                                                int totalDuration = loops * duration;

                                                if (FrameLoop == null)
                                                {
                                                    for (int i = 0; i < loops; i++)
                                                    {
                                                        SoundManager.PlaySound(BaseSound + 7, false, 0 + (i * duration));
                                                    }
                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 684 + (int)Direction * 5, 5, duration, this) { Repeat = true, RepeatUntil = CMain.Time + totalDuration });
                                                }
                                                LoopFrame(FrameIndex, 1, FrameInterval, totalDuration);
                                            }
                                            break;
                                        case Monster.Riklebites: //518
                                            {
                                                int totalDuration = 2400;

                                                if (FrameLoop == null)
                                                {
                                                    LoopFrame(FrameIndex + 3, 1, FrameInterval, totalDuration);

                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Riklebites], 1112 + (int)Direction * 7, 7, 800, this) { Repeat = true, RepeatUntil = CMain.Time + totalDuration });
                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Riklebites], 1168 + (int)Direction * 6, 6, 800, this) { Repeat = true, RepeatUntil = CMain.Time + totalDuration });
                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Riklebites], 1216 + (int)Direction * 7, 7, 800, this) { Repeat = true, RepeatUntil = CMain.Time + totalDuration });
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case 4:
                                    switch (BaseImage)
                                    {
                                        case Monster.火娃:
                                            if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "CreatureSmoke") < 0)
                                                Effects.Add(new TrackableEffect(new Effect(Libraries.Pets[((ushort)BaseImage) - 10000], 256, 3, 1000, this), "CreatureSmoke"));
                                            break;
                                        case Monster.Kirin: //438
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Kirin], 880 + (int)Direction * 7, 7, 700, this));
                                            break;
                                        case Monster.DragonWarrior: //435
                                            Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                            Effect effect = new Effect(Libraries.Monsters[(ushort)Monster.DragonWarrior], 752, 6, 600, source, CMain.Time + 300);
                                            MapControl.Effects.Add(effect);
                                            break;
                                        case Monster.Mon443N:
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon443N], 780, 6, 60, front));
                                            break;
                                        case Monster.CrystalBeast: //499
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1229 + (int)Direction * 5, 5, 300, this));
                                            break;
                                    }
                                    break;
                                case 5:
                                    switch (BaseImage)
                                    {
                                        case Monster.WhiteMammoth: //324
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WhiteMammoth], 376, 5, Frame.Count * Frame.Interval, this));
                                            SoundManager.PlaySound(BaseSound + 8);
                                            break;
                                        case Monster.ManTree: //411
                                            Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                            Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.ManTree], 696, 8, 800, source, CMain.Time, drawBehind: true);
                                            MapControl.Effects.Add(ef);
                                            break;
                                        case Monster.HornedSorceror: //406
                                            SoundManager.PlaySound(BaseSound + 1);
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 624, 10, 10 * Frame.Interval, this));
                                            break;
                                        case Monster.CrystalBeast:  //449
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1222, 7, 300, front));
                                            break;
                                        case Monster.ShardMaiden: //484
                                            Point source4840 = Functions.PointMove(CurrentLocation, Direction, 0);
                                            Point source4843 = Functions.PointMove(CurrentLocation, Direction, 3);
                                            Point source4846 = Functions.PointMove(CurrentLocation, Direction, 6);
                                            Effect ef4840 = new Effect(Libraries.Monsters[(ushort)Monster.ShardMaiden], 631, 22, 2200, source4840, CMain.Time) { Blend = true, DrawBehind = true };
                                            Effect ef4843 = new Effect(Libraries.Monsters[(ushort)Monster.ShardMaiden], 631, 22, 2200, source4843, CMain.Time + 100) { Blend = true, DrawBehind = true };
                                            Effect ef4846 = new Effect(Libraries.Monsters[(ushort)Monster.ShardMaiden], 631, 22, 2200, source4846, CMain.Time + 200) { Blend = true, DrawBehind = true };
                                            MapControl.Effects.Add(ef4840);
                                            MapControl.Effects.Add(ef4843);
                                            MapControl.Effects.Add(ef4846);
                                            break;
                                    }
                                    break;
                                case 6:
                                    switch (BaseImage)
                                    {
                                        case Monster.RestlessJar:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 512, 7, 700, this));
                                            break;
                                        case Monster.ChieftainSword: //414
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainSword], 1178, 10, 300, front, CMain.Time + 300) { Blend = true, DrawBehind = true });
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (BaseImage)
                                    {
                                        case Monster.StrayCat: //335
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StrayCat], 840 + (int)Direction * 10, 10, 1000, this));
                                            break;
                                    }
                                    break;
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.近距攻击4:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (UpdateFrame() >= Frame.Count)
                        {

                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 3:
                                    switch (BaseImage)
                                    {
                                        case Monster.ChieftainSword: //414
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainSword], 1420 + (int)Direction * 10, 10, 300, front, CMain.Time + 300) { Blend = false });
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainSword], 1500 + (int)Direction * 10, 10, 300, front, CMain.Time + 300));
                                            break;
                                        case Monster.Mon409B:
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon409B], 986 + (int)Direction * 9, 9, 300, front, CMain.Time + 300) { Blend = true, DrawBehind = true });
                                            SoundManager.PlaySound(4089);
                                            break;
                                    }
                                    break;
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.近距攻击5:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    switch (BaseImage)
                                    {
                                        case Monster.Mon409B:
                                            {
                                                int loops = CurrentActionLevel;
                                                int duration = 5 * FrameInterval;
                                                int totalDuration = loops * duration;
                                                Point Mon409BPoint = Functions.PointMove(CurrentLocation, Direction, 4);

                                                if (FrameLoop == null)
                                                {
                                                    for (int i = 0; i < loops; i++)
                                                    {
                                                        SoundManager.PlaySound(4077, false, 0 + (i * duration));
                                                    }

                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon409B], 1138 + (int)Direction * 5, 5, duration, this) { Repeat = true, RepeatUntil = CMain.Time + totalDuration });
                                                }

                                                LoopFrame(FrameIndex, 3, FrameInterval, totalDuration);

                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon409B], 1178, 12, 300, Mon409BPoint, CMain.Time + totalDuration + 900));
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon409B], 1190 + (int)Direction * 8, 8, 300, front, CMain.Time + totalDuration + 1200) { Blend = true, DrawBehind = true });
                                                    SoundManager.PlaySound(4089);
                                                }
                                            }
                                            break;
                                    }
                                    break;
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.远程攻击1:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            switch (BaseImage)
                            {
                                case Monster.DragonStatue: //902
                                    MapObject ob = MapControl.GetObject(TargetID);
                                    if (ob != null)
                                    {
                                        ob.Effects.Add(new Effect(Libraries.Dragon, 350, 35, 1200, ob) { Blend = true, DrawBehind = true });
                                        SoundManager.PlaySound(BaseSound + 6);
                                    }
                                    break;
                            }
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            if (FrameIndex == 2) PlaySwingSound();
                            MapObject ob = null;
                            Missile missile;
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.GuardianRock: //234
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 1410, 10, 400, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.OmaWitchDoctor: //374
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaWitchDoctor], 800 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                                break;
                                            case Monster.FloatingRock:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 159 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                                break;
                                            case Monster.KingHydrax: //398
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 368 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.BlueSoul: //407
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlueSoul], 280, 6, 700, ob));
                                                    SoundManager.PlaySound(BaseSound + 7);
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        switch (BaseImage)
                                        {

                                            case Monster.DarkDevourer: //54
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkDevourer], 480, 7, 7 * Frame.Interval, ob));
                                                }
                                                break;
                                            case Monster.ManectricClaw: //280
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManectricClaw], 304 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.FlameSpear: //295
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameSpear], 544 + (int)Direction * 10, 10, 10 * 100, this));
                                                break;
                                            case Monster.PurpleFaeFlower: //466
                                                missile = CreateProjectile(331, Libraries.Monsters[(ushort)Monster.PurpleFaeFlower], true, 6, 60, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PurpleFaeFlower], 427, 9, 900, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon472N:
                                                missile = CreateProjectile(467, Libraries.Monsters[(ushort)Monster.Mon472N], true, 5, 60, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon472N], 547, 9, 900, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.CrystalBeast: //499
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1281, 10, 10 * 60, this, CMain.Time + 2200) { Blend = true, DrawBehind = true });
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1291, 7, 7 * Frame.Interval, this, CMain.Time + 200) { Blend = true });
                                                break;
                                            case Monster.ArcherDwarf: //501
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(320, Libraries.Monsters[(ushort)Monster.ArcherDwarf], true, 1, 40, 0, direction16: true);
                                                break;
                                            case Monster.RedMutantPlant: //510
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RedMutantPlant], 312, 10, 10 * Frame.Interval, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.BlueMutantPlant: //511
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlueMutantPlant], 312, 10, 10 * Frame.Interval, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.UndeadDwarfArcher: //513
                                                missile = CreateProjectile(240, Libraries.Monsters[(ushort)Monster.UndeadDwarfArcher], true, 3, 40, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.UndeadDwarfArcher], 288, 10, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.AncientStoneGolem: //514
                                                missile = CreateProjectile(486, Libraries.Monsters[(ushort)Monster.AncientStoneGolem], true, 6, 60, -6, direction16: false);
                                                missile = CreateProjectile(399, Libraries.Monsters[(ushort)Monster.AncientStoneGolem], true, 4, 40, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientStoneGolem], 480, 6, 900, missile.Target) { Blend = true });
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientStoneGolem], 492, 6, 900, missile.Target) { Blend = true, DrawBehind = true });
                                                    };
                                                }
                                                break;
                                            case Monster.ReaperWizard: //563
                                                missile = CreateProjectile(456, Libraries.Monsters[(ushort)Monster.ReaperWizard], true, 5, 30, -5, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ReaperWizard], 461, 15, 900, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.FeatheredWolf: //496
                                                missile = CreateProjectile(456, Libraries.Monsters[(ushort)Monster.FeatheredWolf], true, 4, 30, -4, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeatheredWolf], 460, 7, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Serpentirian: //515
                                                missile = CreateProjectile(421, Libraries.Monsters[(ushort)Monster.Serpentirian], true, 7, 30, -7, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Serpentirian], 428, 4, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Riklebites: //518
                                                missile = CreateProjectile(1056, Libraries.Monsters[(ushort)Monster.Riklebites], true, 6, 30, -6, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Riklebites], 1272, 7, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.Jar2: //338
                                                missile = CreateProjectile(688, Libraries.Monsters[(ushort)Monster.Jar2], true, 4, 50, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Jar2], 752, 8, 500, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.HardenRhino: //328
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HardenRhino], 392, 5, 5 * Frame.Interval, this));
                                                break;
                                            case Monster.Mon547N: //547
                                                missile = CreateProjectile(368, Libraries.Monsters[(ushort)Monster.Mon547N], true, 3, 60, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon547N], 416, 10, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon549N: //549
                                                missile = CreateProjectile(200, Libraries.Monsters[(ushort)Monster.Mon549N], true, 4, 60, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon549N], 264, 8, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon572B:
                                                 missile = CreateProjectile(425, Libraries.Monsters[(ushort)Monster.Mon572B], false, 11, 20, -11);

                                                 if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon572B], 516, 7, 300, missile.Target) { Blend = false, DrawBehind = true });
                                                    };
                                                }
                                                 break;
                                            case Monster.Mon575S:
                                                 missile = CreateProjectile(350, Libraries.Monsters[(ushort)Monster.Mon575S], true, 5, 50, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon575S], 390, 4, 300, missile.Target) { Blend = false, DrawBehind = true });
                                                    };
                                                }
                                                 break;
                                            case Monster.Mon577N:
                                                missile = CreateProjectile(408, Libraries.Monsters[(ushort)Monster.Mon577N], true, 1, 30, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon577N], 424, 9, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon586N:
                                                missile = CreateProjectile(226, Libraries.Monsters[(ushort)Monster.Mon586N], true, 6, 30, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon586N], 274, 10, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon601N:
                                                missile = CreateProjectile(408, Libraries.Monsters[(ushort)Monster.Mon601N], true, 6, 30, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon601N], 504, 11, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                             case Monster.Mon602N:
                                                missile = CreateProjectile(264, Libraries.Monsters[(ushort)Monster.Mon602N], true, 6, 30, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon602N], 360, 8, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.FurbolgArcher: //470
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(429, Libraries.Monsters[(ushort)Monster.FurbolgArcher], false, 5, 30, 0);
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.FurbolgGuard: //473
                                                missile = CreateProjectile(391, Libraries.Monsters[(ushort)Monster.FurbolgGuard], false, 1, 30, 0);
                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgGuard], 407, 7, 600, missile.Target));
                                                        SoundManager.PlaySound(BaseSound + 6);
                                                    };
                                                }
                                                break;
                                            case Monster.AxeSkeleton:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(224, Libraries.Monsters[(ushort)Monster.AxeSkeleton], false, 3, 30, 0);
                                                break;
                                            case Monster.Dark:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(224, Libraries.Monsters[(ushort)Monster.Dark], false, 3, 30, 0);
                                                break;
                                            case Monster.ZumaArcher: //47
                                            case Monster.BoneArcher: //195
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.ZumaArcher], false, 1, 30, 6, direction16: true);
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.RedThunderZuma: //221
                                            case Monster.FrozenRedZuma:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Dragon, 400 + CMain.Random.Next(3) * 10, 5, 300, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.BoneLord:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(784, Libraries.Monsters[(ushort)Monster.BoneLord], true, 6, 30, 0, direction16: false);
                                                break;
                                            case Monster.RightGuard: //205
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 10, 5, 300, ob));
                                                }
                                                break;
                                            case Monster.LeftGuard: //206
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(10, Libraries.Magic, true, 6, 30, 4);
                                                }
                                                break;
                                            case Monster.MinotaurKing: //207
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MinotaurKing], 320, 20, 1000, ob) { DrawBehind = true });
                                                }
                                                break;
                                            case Monster.FrostTiger: //222
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(7200, Libraries.Magic3, true, 6, 30, 4);
                                                }
                                                break;
                                            case Monster.FlameTiger: //228
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(2330, Libraries.Magic3, true, 5, 30, 5);
                                                }
                                                break;
                                            case Monster.MysteriousMage: //495
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MysteriousMage], 455, 18, 1000, ob) { DrawBehind = true });
                                                }
                                                break;

                                            case Monster.Yimoogi:
                                            case Monster.RedYimoogi: //208
                                            case Monster.Snake10:
                                            case Monster.Snake11:
                                            case Monster.Snake12:
                                            case Monster.Snake13:
                                            case Monster.Snake14:
                                            case Monster.Snake15:
                                            case Monster.Snake16:
                                            case Monster.Snake17:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 1250, 15, 1000, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.HolyDeva: //172
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 10, 5, 300, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.CrossbowOma: //215
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.CrossbowOma], false, 1, 30, 6);
                                                break;
                                            case Monster.DarkCrossbowOma:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.DarkCrossbowOma], false, 1, 30, 6);
                                                break;
                                            case Monster.WingedOma: //210
                                            case Monster.DarkWingedOma:
                                                missile = CreateProjectile(224, Libraries.Monsters[(ushort)Monster.WingedOma], false, 6, 30, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WingedOma], 272, 2, 150, missile.Target) { Blend = false });
                                                    };
                                                }
                                                break;
                                            case Monster.FlamingMutant: //257
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 320, 10, 1000, ob.CurrentLocation, CMain.Time) { Blend = true });
                                                }
                                                break;
                                            case Monster.PoisonHugger: //55
                                                missile = CreateProjectile(208, Libraries.Monsters[(ushort)Monster.PoisonHugger], true, 2, 30, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PoisonHugger], 224, 5, 150, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.RedFoxman: //231
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RedFoxman], 224, 9, 300, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.WhiteFoxman: //232
                                                missile = CreateProjectile(1160, Libraries.Magic, true, 3, 30, 7);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WhiteFoxman], 352, 10, 300, missile.Target));
                                                        SoundManager.PlaySound(BaseSound + 6);
                                                    };
                                                }
                                                break;
                                            case Monster.TrapRock: //233
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TrapRock], 26, 10, 600, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.HedgeKekTal:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.HedgeKekTal], false, 4, 30, 6);
                                                break;
                                            case Monster.BigHedgeKekTal:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.BigHedgeKekTal], false, 4, 30, 6);
                                                break;
                                            case Monster.EvilMir:
                                                missile = CreateProjectile(60, Libraries.Dragon, true, 10, 10, 0);

                                                if (missile.Direction > 12)
                                                    missile.Direction = 12;
                                                if (missile.Direction < 7)
                                                    missile.Direction = 7;

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Dragon, 200, 20, 600, missile.Target));
                                                    };
                                                }
                                                break;
                                            case Monster.ArcherGuard: //71
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.ArcherGuard], false, 3, 30, 6);
                                                break;
                                            case Monster.SpittingToad:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(280, Libraries.Monsters[(ushort)Monster.SpittingToad], true, 6, 30, 0);
                                                break;
                                            case Monster.ArcherGuard2: //199
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(6, Libraries.Monsters[(ushort)Monster.ArcherGuard2], true, 2, 30, 6, lightDistance: 6, direction16: true, Color.Green, TargetID);
                                                break;
                                            case Monster.ArcherGuard3:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(104, Libraries.Monsters[(ushort)Monster.ArcherGuard3], false, 3, 30, 0);
                                                break;
                                            case Monster.FinialTurtle:
                                                missile = CreateProjectile(272, Libraries.Monsters[(ushort)Monster.FinialTurtle], true, 3, 30, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FinialTurtle], 320, 10, 500, missile.Target) { Blend = true });
                                                        SoundManager.PlaySound(20000 + (ushort)Spell.FrostCrunch * 10 + 2);
                                                    };
                                                }
                                                break;
                                            case Monster.HellBolt: //276
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellBolt], 315, 10, 600, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.NamelessGhost: //282
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic, 3850, 20, 600, ob, CMain.Time + 600, false));
                                                    SoundManager.PlaySound(20000 + 46 * 10 + 1);//sound M46-1
                                                }
                                                break;
                                            case Monster.DarkGhost: //283
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 140, 10, 600, ob));
                                                    SoundManager.PlaySound(20000 + 47 * 10 + 1);//sound M47-1
                                                }
                                                break;
                                            case Monster.ChaosGhost: //284
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 1010, 18, 600, ob));
                                                }
                                                break;
                                            case Monster.WitchDoctor: //277
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    missile = CreateProjectile(313, Libraries.Monsters[(ushort)Monster.WitchDoctor], true, 5, 30, -5, direction16: false);

                                                    if (missile.Target != null)
                                                    {
                                                        missile.Complete += (o, e) =>
                                                        {
                                                            if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                            missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WitchDoctor], 318, 10, 600, missile.Target));
                                                            SoundManager.PlaySound(BaseSound + 6);
                                                        };
                                                    }
                                                }
                                                break;
                                            case Monster.WingedTigerLord: //229
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WingedTigerLord], 640, 10, 800, ob, CMain.Time + 400, false));
                                                }
                                                break;
                                            case Monster.TrollBomber: //292
                                                missile = CreateProjectile(208, Libraries.Monsters[(ushort)Monster.TrollBomber], false, 4, 40, -4, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        SoundManager.PlaySound(BaseSound + 9);
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TrollBomber], 212, 6, 600, missile.Target) { Blend = true, DrawBehind = true });
                                                    };
                                                }
                                                break;
                                            case Monster.TrollStoner: //293
                                                SoundManager.PlaySound(BaseSound + 9);
                                                missile = CreateProjectile(208, Libraries.Monsters[(ushort)Monster.TrollStoner], false, 4, 40, -4, direction16: false);
                                                break;
                                            case Monster.FlameMage: //296
                                                missile = CreateProjectile(544, Libraries.Monsters[(ushort)Monster.FlameMage], true, 3, 20, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameMage], 592, 10, 1000, missile.Target) { Blend = true, DrawBehind = true });
                                                    };
                                                }
                                                break;
                                            case Monster.FlameScythe: //297
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameScythe], 592, 9, 900, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.FlameAssassin: //298
                                                missile = CreateProjectile(592, Libraries.Monsters[(ushort)Monster.FlameAssassin], true, 3, 20, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameAssassin], 640, 6, 600, missile.Target));
                                                    };
                                                }
                                                break;
                                            case Monster.FlameQueen: //299
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameQueen], 729, 10, Frame.Count * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                                break;
                                            case Monster.AncientBringer: //329
                                                missile = CreateProjectile(744, Libraries.Monsters[(ushort)Monster.AncientBringer], true, 4, 50, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientBringer], 776, 10, 800, missile.Target) { Blend = true, DrawBehind = true });
                                                    };
                                                }
                                                break;
                                            case Monster.RestlessJar:
                                                missile = CreateProjectile(476, Libraries.Monsters[(ushort)Monster.RestlessJar], true, 2, 100, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 508, 3, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.IceGuard: //306
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IceGuard], 262, 6, 600, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.ElementGuard: //307
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ElementGuard], 368 + (int)ob.Direction * 7, 7, 7 * Frame.Interval, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.KingGuard: //309
                                                 missile = CreateProjectile(784, Libraries.Monsters[(ushort)Monster.KingGuard], true, 9, 40, -9, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        SoundManager.PlaySound(BaseSound + 9);
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 793, 7, 300, missile.Target) { Blend = true});
                                                    };
                                                }
                                                break;
                                            case Monster.BurningZombie: //319
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BurningZombie], 378, 12, 800, ob));
                                                }
                                                break;
                                            case Monster.FrozenZombie: //321
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenZombie], 368, 8, 800, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.CatShaman: //336
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CatShaman], 768, 12, 1500, ob) { Blend = false });
                                                }
                                                break;
                                            case Monster.GeneralMeowMeow: //341
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GeneralMeowMeow], 552, 10, 1000, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.CannibalTentacles:  //353
                                                if (MapControl.GetObject(TargetID) != null)
                                                    missile = CreateProjectile(488, Libraries.Monsters[(ushort)Monster.CannibalTentacles], true, 8, 100, 0, direction16: false);
                                                break;
                                            case Monster.SwampWarrior: //357
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SwampWarrior], 392, 8, 800, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.PeacockSpider: //366
                                                missile = CreateProjectile(664, Libraries.Monsters[(ushort)Monster.PeacockSpider], true, 5, 100, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PeacockSpider], 744, 11, 1100, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.RhinoPriest: //360
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoPriest], 376, 9, 900, ob));
                                                }
                                                break;
                                            case Monster.TreeGuardian: //364
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeGuardian], 544, 8, 800, ob));
                                                }
                                                break;
                                            case Monster.CreeperPlant: //383
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 250, 6, 600, ob.CurrentLocation, CMain.Time) { Blend = true });
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 256, 10, 1000, ob.CurrentLocation, CMain.Time) { Blend = false });
                                                }
                                                break;
                                            case Monster.FloatingWraith: //384
                                                missile = CreateProjectile(248, Libraries.Monsters[(ushort)Monster.FloatingWraith], true, 2, 20, 0, direction16: true);
                                                break;
                                            case Monster.AvengingSpirit:  //388
                                                missile = CreateProjectile(376, Libraries.Monsters[(ushort)Monster.AvengingSpirit], true, 4, 40, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingSpirit], 440, 10, 1000, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.AvengingWarrior: //390
                                                missile = CreateProjectile(320, Libraries.Monsters[(ushort)Monster.AvengingWarrior], true, 5, 50, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingWarrior], 400, 7, 700, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.LightningBead: //375
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LightningBead], 66, 12, 800, ob));
                                                }
                                                break;
                                            case Monster.HealingBead: //376
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HealingBead], 62, 11, 1100, ob));
                                                }
                                                break;
                                            case Monster.PowerUpBead: //377
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PowerUpBead], 62, 6, 600, ob));
                                                }
                                                break;
                                            case Monster.DarkOmaKing: //378
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkOmaKing], 1755, 13, 1300, ob.CurrentLocation));
                                                }
                                                break;
                                            case Monster.ChieftainArcher: //417
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    if (CurrentActionLevel == 0)
                                                    {
                                                        missile = CreateProjectile(312, Libraries.Monsters[(ushort)Monster.ChieftainArcher], true, 5, 50, 0, direction16: true);

                                                        if (missile.Target != null)
                                                        {
                                                            missile.Complete += (o, e) =>
                                                            {
                                                                if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                                missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainArcher], 392, 6, 200, missile.Target));
                                                            };
                                                        }
                                                    }
                                                    else if (CurrentActionLevel == 1)
                                                    {
                                                        missile = CreateProjectile(398, Libraries.Monsters[(ushort)Monster.ChieftainArcher], true, 5, 50, 0, direction16: true);

                                                        if (missile.Target != null)
                                                        {
                                                            missile.Complete += (o, e) =>
                                                            {
                                                                if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                                missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ChieftainArcher], 478, 6, 60, missile.Target));
                                                            };
                                                        }
                                                    }
                                                    else
                                                    {
                                                        missile = CreateProjectile(312, Libraries.Monsters[(ushort)Monster.ChieftainArcher], true, 5, 50, 0, direction16: true);
                                                        missile = CreateProjectile(484, Libraries.Monsters[(ushort)Monster.ChieftainArcher], true, 5, 50, 0, direction16: true);
                                                    }
                                                }
                                                break;
                                            case Monster.ManTree: //411
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManTree], 696, 8, 1000, ob));
                                                }
                                                break;
                                            case Monster.ClawBeast: //393
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ClawBeast], 576, 7, 700, ob));
                                                }
                                                break;
                                            case Monster.BlackTortoise: //433
                                                Point source = Functions.PointMove(CurrentLocation, Direction, 2);

                                                missile = CreateProjectile(476, Libraries.Monsters[(ushort)Monster.BlackTortoise], true, 6, 60, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackTortoise], 572, 6, 600, missile.Target));
                                                    };
                                                }
                                                break;
                                            case Monster.DragonArcher: //436
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(416, Libraries.Monsters[(ushort)Monster.DragonArcher], true, 5, 50, 0, direction16: true);
                                                break;
                                            case Monster.WaterSoul: //394
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WaterSoul], 240, 10, 300, ob));
                                                }
                                                break;
                                            case Monster.HornedMage: //400
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedMage], 768, 9, 800, ob) { Blend = false });
                                                }
                                                break;
                                            case Monster.HornedArcher: //401
                                                missile = CreateProjectile(416, Libraries.Monsters[(ushort)Monster.HornedArcher], true, 3, 50, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedArcher], 464, 6, 500, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.KingHydrax: //398
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 416, 9, 900, ob) { DrawBehind = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.WaterDragon: //432
                                                missile = CreateProjectile(800, Libraries.Monsters[(ushort)Monster.WaterDragon], true, 6, 60, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WaterDragon], 896, 9, 900, missile.Target) { Blend = true, DrawBehind = true });
                                                    };
                                                }
                                                break;
                                            case Monster.AntCommander: //456
                                                missile = CreateProjectile(432, Libraries.Monsters[(ushort)Monster.AntCommander], true, 3, 100, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AntCommander], 480, 4, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.LightningScroll:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LightningScroll], 299, 8, 800, ob));
                                                }
                                                break;
                                            case Monster.PoisonScroll:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PoisonScroll], 272, 10, 1000, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.CallScroll:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CallScroll], 304, 11, 1100, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.FireballScroll:
                                                missile = CreateProjectile(300, Libraries.Monsters[(ushort)Monster.FireballScroll], true, 5, 50, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FireballScroll], 380, 8, 800, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Catapult:
                                                missile = CreateProjectile(256, Libraries.Siege[(ushort)Monster.Catapult - 940], false, 4, 40, 0);
                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Siege[(ushort)Monster.Catapult - 940], 288, 10, 1000, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.ChariotBallista:
                                                missile = CreateProjectile(38, Libraries.Siege[(ushort)Monster.ChariotBallista - 940], false, 3, 30, 6);
                                                break;
                                            case Monster.Mon271N:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic, 3850, 20, 600, ob));
                                                }
                                                break;
                                            case Monster.HoodedIceMage: //482
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 449, 11, 600, ob.CurrentLocation, CMain.Time) { Blend = true });
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 460, 11, 600, ob.CurrentLocation, CMain.Time) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.HoodedPriest: //483
                                                missile = CreateProjectile(478, Libraries.Monsters[(ushort)Monster.HoodedPriest], true, 5, 50, -5, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedPriest], 483, 6, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.LivingVines: //490
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LivingVines], 384, 6, 600, ob.CurrentLocation, CMain.Time) { Blend = true });
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LivingVines], 390, 6, 600, ob.CurrentLocation, CMain.Time) { Blend = false });
                                                }
                                                break;
                                            case Monster.BlueMonk: //491
                                                missile = CreateProjectile(456, Libraries.Monsters[(ushort)Monster.BlueMonk], true, 4, 100, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlueMonk], 520, 5, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.MutantGuardian: //493
                                                missile = CreateProjectile(496, Libraries.Monsters[(ushort)Monster.MutantGuardian], true, 5, 100, 0, direction16: true);
                                                missile = CreateProjectile(640, Libraries.Monsters[(ushort)Monster.MutantGuardian], true, 5, 100, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantGuardian], 720, 8, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.MutantHighPriest: //494
                                                missile = CreateProjectile(418, Libraries.Monsters[(ushort)Monster.MutantHighPriest], true, 5, 100, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantHighPriest], 422, 9, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.ManEatingPlant: //499
                                                missile = CreateProjectile(461, Libraries.Monsters[(ushort)Monster.ManEatingPlant], true, 6, 100, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManEatingPlant], 557, 7, 500, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.NobleArcher: //503
                                            case Monster.Mon557B: //557
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(400, Libraries.Monsters[(ushort)Monster.NobleArcher], true, 6, 30, 6, direction16: false);
                                                break;
                                            case Monster.NoblePriest: //504
                                            case Monster.Mon558B: //558
                                                missile = CreateProjectile(400, Libraries.Monsters[(ushort)Monster.NoblePriest], true, 5, 30, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.NoblePriest], 440, 14, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Swain1: //509
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Swain1], 504, 14, 600, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.Butcher: //516
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(680, Libraries.Monsters[(ushort)Monster.Butcher], true, 5, 30, 5);
                                                }
                                                break;
                                            case Monster.SpectralWraith: //530
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SpectralWraith], 659, 8, 300, ob, CMain.Time + 300) { Blend = true });
                                                }
                                                break;
                                            case Monster.Mon542N: //542
                                                missile = CreateProjectile(336, Libraries.Monsters[(ushort)Monster.Mon542N], true, 8, 50, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon542N], 400, 15, 800, missile.Target) { Blend = true, DrawBehind = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon545N: //545
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon545N], 752, 14, 600, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.Mon551N: //551
                                                missile = CreateProjectile(380, Libraries.Monsters[(ushort)Monster.Mon551N], true, 10, 50, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon551N], 460, 6, 800, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon555N: //555
                                                missile = CreateProjectile(398, Libraries.Monsters[(ushort)Monster.Mon555N], true, 8, 50, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon555N], 526, 10, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon562N:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon562N], 666, 10, 300, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.Mon563N:
                                                missile = CreateProjectile(677, Libraries.Monsters[(ushort)Monster.Mon563N], true, 7, 50, 0, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon563N], 733, 4, 300, missile.Target) { Blend = true, DrawBehind = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon609N:
                                                missile = CreateProjectile(584, Libraries.Monsters[(ushort)Monster.Mon609N], false, 13, 20, -13);

                                                 if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon609N], 597, 8, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                 break;
                                            case Monster.ReaperPriest: //487
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ReaperPriest], 464, 15, 900, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.Swain: //508
                                                missile = CreateProjectile(472, Libraries.Monsters[(ushort)Monster.Swain], true, 3, 50, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Swain], 520, 8, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.FeralTundraFurbolg: //521
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeralTundraFurbolg], 695, 12, 900, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.Mon579B:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon579B], 563, 5, 800, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.Mon580B:
                                                missile = CreateProjectile(680, Libraries.Monsters[(ushort)Monster.Mon580B], true, 6, 50, 0, direction16: true);
                                                //SoundManager.PlaySound(BaseSound + 6);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon580B], 776, 7, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon591N:
                                                missile = CreateProjectile(336, Libraries.Monsters[(ushort)Monster.Mon591N], true, 8, 50, -8);
                                                SoundManager.PlaySound(BaseSound + 6);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon591N], 328, 8, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;

                                        }
                                        break;
                                    }//end of case 4
                                case 5:
                                    switch (BaseImage)
                                    {
                                        case Monster.OmaCannibal: //369
                                            missile = CreateProjectile(360, Libraries.Monsters[(ushort)Monster.OmaCannibal], true, 6, 60, 0);

                                            if (missile.Target != null)
                                            {
                                                missile.Complete += (o, e) =>
                                                {
                                                    if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                    missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaCannibal], 456, 7, 700, missile.Target) { Blend = true, DrawBehind = true });
                                                };
                                            }
                                            break;
                                        case Monster.OmaMage: //373
                                            missile = CreateProjectile(392, Libraries.Monsters[(ushort)Monster.OmaMage], true, 8, 80, 0, direction16: true);

                                            if (missile.Target != null)
                                            {
                                                missile.Complete += (o, e) =>
                                                {
                                                    if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                    missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaMage], 520, 9, 600, missile.Target) { Blend = true, DrawBehind = true });
                                                };
                                            }
                                            break;
                                        case Monster.OmaWitchDoctor: //374
                                            ob = MapControl.GetObject(TargetID);
                                            if (ob != null)
                                            {
                                                SoundManager.PlaySound(BaseSound + 7);
                                                ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaWitchDoctor], 856, 11, 1100, ob) { Blend = true, DrawBehind = true });
                                            }
                                            break;
                                        case Monster.SnowYeti: //445
                                            missile = CreateProjectile(648, Libraries.Monsters[(ushort)Monster.SnowYeti], true, 6, 20, 0);
                                            break;
                                        case Monster.MudZombie: //320
                                            ob = MapControl.GetObject(TargetID);
                                            if (ob != null)
                                            {
                                                ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MudZombie], 304, 7, 700, ob) { Blend = false });
                                            }
                                            break;
                                        case Monster.DarkSpirit: //448
                                            missile = CreateProjectile(512, Libraries.Monsters[(ushort)Monster.DarkSpirit], true, 6, 60, 0);

                                            if (missile.Target != null)
                                            {
                                                missile.Complete += (o, e) =>
                                                {
                                                    if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                    missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkSpirit], 608, 10, 1000, missile.Target));
                                                };
                                            }
                                            break;
                                    }

                                    break;
                                case 6:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.HornedMage: //400
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedMage], 777, 6, 800, ob) /*{ Blend = false }*/);
                                                }
                                                break;
                                            case Monster.FloatingRock: //405
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 224, 10, 1000, ob) /*{ Blend = false }*/);
                                                }
                                                break;
                                            case Monster.FrozenArcher: //426
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(264, Libraries.Monsters[(ushort)Monster.FrozenArcher], true, 5, 60, 0);
                                                break;
                                            case Monster.FrozenMagician: //444
                                                missile = CreateProjectile(592, Libraries.Monsters[(ushort)Monster.FrozenMagician], true, 6, 30, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenMagician], 688, 6, 300, missile.Target));
                                                    };
                                                }
                                                break;
                                            case Monster.IceCrystalSoldier: //446
                                                Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.IceCrystalSoldier], 476, 8, 800, source, CMain.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.ColdArcher: //402
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ColdArcher], 416 + (int)Direction * 2, 2, 2 * FrameInterval, this));
                                                break;
                                            case Monster.HoodedSummoner: //481
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedSummoner], 374, 13, 1300, ob) { Blend = true });
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 7:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FrozenKnight: //427
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenKnight], 520, 10, 60, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.IcePhantom: //429
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 722, 10, 200, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.FloatingRock: //405
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 234, 10, 1000, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 9:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.ColdArcher: //402
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ColdArcher], 432, 10, 1000, ob));
                                                }
                                                break;

                                        }
                                        break;
                                    }
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.远程攻击2:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            MapObject ob = null;
                            Missile missile;
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.RestlessJar:
                                                var point = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 391 + (int)Direction * 10, 10, 10 * Frame.Interval, point));
                                                break;
                                            case Monster.KingHydrax://398
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 425 + (int)Direction * 6, 6, 600, this));
                                                break;
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FurbolgArcher: //470
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    missile = CreateProjectile(344, Libraries.Monsters[(ushort)Monster.FurbolgArcher], false, 5, 30, 0);

                                                    if (missile.Target != null)
                                                    {
                                                        missile.Complete += (o, e) =>
                                                        {
                                                            if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                            missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgArcher], 424, 5, 500, missile.Target));
                                                            SoundManager.PlaySound(BaseSound + 7);
                                                        };
                                                    }
                                                }
                                                break;
                                            case Monster.RedFoxman: //231
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RedFoxman], 233, 10, 400, ob));
                                                    SoundManager.PlaySound(BaseSound + 7);
                                                }
                                                break;
                                            case Monster.KingGuard: //309
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 820, 10, 600, ob) { Blend = true, DrawBehind = true });
                                                    SoundManager.PlaySound(BaseSound + 7);
                                                }
                                                break;
                                            case Monster.WhiteFoxman: //232
                                                missile = CreateProjectile(1160, Libraries.Magic, true, 3, 30, 7);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WhiteFoxman], 362, 15, 1800, missile.Target) { DrawBehind = true });
                                                        SoundManager.PlaySound(BaseSound + 7);
                                                    };
                                                }
                                                break;
                                            case Monster.TrollKing: //294
                                                missile = CreateProjectile(294, Libraries.Monsters[(ushort)Monster.TrollKing], false, 4, 40, -4, direction16: false);
                                                SoundManager.PlaySound(BaseSound + 7);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TrollKing], 298, 6, 600, missile.Target) { Blend = true });
                                                        SoundManager.PlaySound(BaseSound + 8);
                                                    };
                                                }
                                                break;
                                            case Monster.AncientBringer: //329
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientBringer], 796, 14, 2000, ob) { Blend = true, DrawBehind = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.IceGuard: //306
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IceGuard], 268, 5, 500, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.CatShaman: //336
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CatShaman], 780, 6, 500, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.TucsonGeneral: //354
                                                missile = CreateProjectile(592, Libraries.Monsters[(ushort)Monster.TucsonGeneral], true, 9, 30, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonGeneral], 736, 9, 500, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.RhinoPriest: //360
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoPriest], 448, 7, 700, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.TreeGuardian: //364
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeGuardian], 684 + ((int)Direction * 10), 10, 1000, ob.CurrentLocation));
                                                }
                                                break;
                                            case Monster.OmaWitchDoctor: //374
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaWitchDoctor], 867, 10, 1000, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.KingHydrax: //398
                                                missile = CreateProjectile(473, Libraries.Monsters[(ushort)Monster.KingHydrax], true, 4, 50, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 537, 6, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.IcePhantom: //429
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 812, 7, 700, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.ColdArcher: //402
                                                missile = CreateProjectile(442, Libraries.Monsters[(ushort)Monster.ColdArcher], true, 3, 50, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ColdArcher], 490, 6, 500, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.HornedArcher: //401
                                                missile = CreateProjectile(470, Libraries.Monsters[(ushort)Monster.HornedArcher], true, 3, 50, 0, direction16: true);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedArcher], 518, 6, 500, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;                                           
                                            case Monster.DragonArcher: //436
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(496, Libraries.Monsters[(ushort)Monster.DragonArcher], true, 5, 50, 0, direction16: true);
                                                break;
                                            case Monster.CrystalBeast: //449
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalBeast], 1298, 13, 1300, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.HoodedSummoner: //481
                                                missile = CreateProjectile(717, Libraries.Monsters[(ushort)Monster.ShardMaiden], true, 3, 60, -3, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShardMaiden], 720, 16, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.HoodedIceMage: //482
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 471, 18, 1200, ob.CurrentLocation, CMain.Time) { Blend = true });
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedIceMage], 489, 9, 600, ob.CurrentLocation, CMain.Time + 600) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.HoodedPriest: //483
                                                missile = CreateProjectile(489, Libraries.Monsters[(ushort)Monster.HoodedPriest], true, 6, 60, -6, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedPriest], 495, 10, 500, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.ReaperPriest: //487
                                                missile = CreateProjectile(513, Libraries.Monsters[(ushort)Monster.ReaperPriest], true, 5, 30, -5, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ReaperPriest], 518, 9, 600, missile.Target) { Blend = true, DrawBehind = true });
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ReaperPriest], 527, 9, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.BlueMonk: //491
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlueMonk], 528, 8, 1200, ob) { Blend = true });
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlueMonk], 536, 8, 1200, ob));
                                                }
                                                break;
                                            case Monster.MutantGuardian: //493
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantGuardian], 741, 18, 900, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.MysteriousMage: //495
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MysteriousMage], 478, 18, 1000, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.NobleArcher: //503
                                            case Monster.Mon557B: //557
                                                missile = CreateProjectile(496, Libraries.Monsters[(ushort)Monster.NobleArcher], true, 8, 30, -8, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.NobleArcher], 512, 5, 300, missile.Target, CMain.Time + 300) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.NoblePriest: //504
                                            case Monster.Mon558B: //558
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(454, Libraries.Monsters[(ushort)Monster.NoblePriest], true, 6, 30, 0, direction16: false);
                                                break;
                                            case Monster.Serpentirian: //515
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Serpentirian], 438, 3, 600, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.Riklebites: //518
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(1062, Libraries.Monsters[(ushort)Monster.Riklebites], true, 6, 30, -6, direction16: false);
                                                break;
                                            case Monster.MirEmperor: //534
                                                missile = CreateProjectile(77, Libraries.Monsters[(ushort)Monster.MirEmperor], true, 11, 50, -11, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MirEmperor], 66, 11, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon545N: //545
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon545N], 766, 14, 600, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.Mon564N:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon564N], 1026, 10, 600, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.Mon570B:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon570B], 566, 7, 600, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 8:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FrozenMagician: //444
                                                missile = CreateProjectile(774, Libraries.Monsters[(ushort)Monster.FrozenMagician], true, 6, 30, 0);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenMagician], 870, 10, 300, missile.Target));
                                                    };
                                                }
                                                break;
                                            case Monster.ArcherDwarf: //501
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(416, Libraries.Monsters[(ushort)Monster.ArcherDwarf], true, 5, 40, 0, direction16: true);
                                                break;
                                            case Monster.ArcherGuard2: //199 远程16方向
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(264, Libraries.Monsters[(ushort)Monster.ArcherGuard2], true, 1, 30, 0, lightDistance: 6, direction16: true, Color.Red, TargetID);
                                                break;
                                        }
                                        break;

                                    }
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.远程攻击3:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            MapObject ob = null;
                            Missile missile;
                            switch (FrameIndex)
                            {
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.TucsonGeneral: //354
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonGeneral], 745, 17, 1000, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.HoodedSummoner: //481
                                                missile = CreateProjectile(4700, Libraries.Magic3, true, 4, 30, 6);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        missile.Target.Effects.Add(new Effect(Libraries.Magic3, 4860, 7, 300, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.NobleArcher: //503
                                            case Monster.Mon557B: //557
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.NobleArcher], 517, 9, 600, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.NoblePriest: //504
                                            case Monster.Mon558B: //558
                                                missile = CreateProjectile(502, Libraries.Monsters[(ushort)Monster.NoblePriest], true, 3, 30, -3, direction16: false);

                                                if (missile.Target != null)
                                                {
                                                    missile.Complete += (o, e) =>
                                                    {
                                                        if (missile.Target.CurrentAction == MirAction.死后尸体) return;
                                                        missile.Target.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.NoblePriest], 505, 13, 600, missile.Target) { Blend = true });
                                                    };
                                                }
                                                break;
                                            case Monster.Mon564N:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon564N], 1040, 17, 600, ob) { Blend = true, DrawBehind = true });
                                                }
                                                break;
                                            case Monster.Mon409B:
                                                LoopFrame(FrameIndex, 2, FrameInterval, CurrentActionLevel * 1000);
                                                break;
                                        }
                                    }
                                    break;
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;

                case MirAction.被击动作:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;

                case MirAction.死亡动作:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            ActionFeed.Clear();
                            ActionFeed.Add(new QueuedAction { Action = MirAction.死后尸体, Direction = Direction, Location = CurrentLocation });
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    switch (BaseImage)
                                    {
                                        case Monster.DarkDevourer: //54
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkDevourer], 536, 4, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.Behemoth: //57
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 658, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.FurbolgCommander://471
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgCommander], 320, 5, 5 * Frame.Interval, this) { DrawBehind = true });
                                            break;
                                        case Monster.Furball: //467
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Furball], 312, 8, Frame.Count * Frame.Interval, this));
                                            break;
                                        case Monster.GlacierBeast: //474
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierBeast], 372, 6, 800, this) { Blend = true });
                                            break;
                                        case Monster.HellCannibal: //274
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellCannibal], 304, 6, 800, this) { Blend = true });
                                            break;
                                        case Monster.PoisonHugger: //55
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PoisonHugger], 224, 5, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.Hugger: //56
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Hugger], 256, 8, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.MutatedHugger: //67
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutatedHugger], 128, 7, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.CyanoGhast: //136
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CyanoGhast], 681, 7, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.Hydrax: //338
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Hydrax], 240, 5, 5 * Frame.Interval, this));
                                            break;
                                        case Monster.PoisonScroll: //478
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PoisonScroll], 282, 11, 11 * Frame.Interval, this));
                                            break;
                                        case Monster.BabyMagmaDragon: //531
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BabyMagmaDragon], 272, 7, 7 * Frame.Interval, this));
                                            break;
                                    }
                                    break;
                                case 3:
                                    PlayDeadSound();
                                    switch (BaseImage)
                                    {
                                        case Monster.BoneSpearman:
                                        case Monster.BoneBlademan:
                                        case Monster.BoneArcher:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BoneSpearman], 224, 8, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.WoodBox: //392
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WoodBox], 104, 6, 6 * Frame.Interval, this) { Blend = true });
                                            break;
                                        case Monster.BoulderSpirit:  //408
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BoulderSpirit], 64, 8, 8 * Frame.Interval, this) { Blend = true });
                                            break;
                                        case Monster.StoningSpider:  //419
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StoningSpider], 128, 10, 10 * Frame.Interval, this) { Blend = true });
                                            break;
                                    }
                                    break;

                                case 4:
                                    PlayDeadSound();
                                    switch (BaseImage)
                                    {
                                        case Monster.Mon472N:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon472N], 629, 10, 10 * Frame.Interval, this) { Blend = true });
                                            break;
                                        case Monster.BlueMonk: //491
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlueMonk], 544, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.MutantBeserker: //492
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantBeserker], 441, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.MutantGuardian: //493
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantGuardian], 759, 11, 11 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.MutantHighPriest: //494
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutantHighPriest], 431, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.FeatheredWolf: //496
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FeatheredWolf], 539, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.MysteriousAssassin: //497
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MysteriousAssassin], 565, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break; ;
                                        case Monster.UndeadHammerDwarf: //512
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.UndeadHammerDwarf], 281, 10, 10 * Frame.Interval, this) { Blend = true });
                                            break;
                                        case Monster.BloodLord: //532
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BloodLord], 410, 10, Frame.Count * Frame.Interval, this));
                                            break;
                                        case Monster.Mon541N: //541
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon541N], 325, 9, 9 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon542N: //542
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon542N], 415, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon543N: //543
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon543N], 349, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon546T: //546
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon546T], 96, 8, 8 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon547N: //547
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon547N], 506, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon550N: //550
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon550N], 352, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon551N: //551
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon551N], 360, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon554N: //554
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon554N], 336, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon554N], 420 + (int)Direction * 6, 6, 200 * Frame.Interval, this));
                                            break;
                                        case Monster.Mon555N: //555
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon555N], 328, 6, 6 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon562N:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon562N], 676 + (int)Direction * 9, 9, 8 * Frame.Interval, this, CMain.Time + 300));
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon562N], 748, 30, 30 * Frame.Interval, this, CMain.Time + 600));
                                            break;
                                        case Monster.Mon563N:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon563N], 799, 7, 10 * Frame.Interval, this, CMain.Time + 300) { Blend = true });
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon563N], 806, 20, 30 * Frame.Interval, this, CMain.Time + 600));
                                            break;
                                        case Monster.Mon564N:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon564N], 756, 16, 16 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon565T:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon565T], 88, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon571B:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon571B], 480, 10, 10 * Frame.Interval, this) { Blend = true });
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon571B], 490, 10, 10 * Frame.Interval, this, CMain.Time + 400) { Blend = true });
                                            break;
                                        case Monster.Mon573B:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon573B], 504, 18, 18 * Frame.Interval, this, CMain.Time + 300) { Blend = true });
                                            break;
                                        case Monster.Mon575S:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon575S], 332, 18, 18 * Frame.Interval, this, CMain.Time + 100) { Blend = true });
                                            break;
                                        case Monster.Mon579B:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon579B], 488, 6, 6 * Frame.Interval, this, CMain.Time + 160) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.Mon593N:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon593N], 168, 8, 8 * Frame.Interval, this, CMain.Time + 100) { Blend = true });
                                            break;
                                    }
                                    break;
                                case 5:
                                    switch (BaseImage)
                                    {
                                        case Monster.KingHydrax: //398
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 543 + (int)Direction * 7, 7, 700, this));
                                            break;
                                        case Monster.Bear: //412
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Bear], 393, 9, Frame.Count * Frame.Interval, this));
                                            break;
                                        case Monster.WarBear: //486
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WarBear], 472, 10, Frame.Count * Frame.Interval, this));
                                            break;
                                        case Monster.IcePhantom: //429
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 732 + (int)Direction * 10, 10, 700, this, CMain.Time + 300));
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 812, 7, 300, this) { DrawBehind = true });
                                            break;
                                        case Monster.SnowWolfKing: //431
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolfKing], 669, 10, 10 * Frame.Interval, this) { Blend = true, DrawBehind = true });
                                            break;
                                        case Monster.DragonArcher: //436
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DragonArcher], 576 + (int)Direction * 6, 6, 600, this) { DrawBehind = true });
                                            break;
                                    }
                                    break;
                                    //case 9:
                                    //    switch (BaseImage)
                                    //    {

                                    //    }
                                    //    break;
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.复活动作:
                    if (CMain.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            switch (BaseImage)
                            {
                                case Monster.Mon552N: //552
                                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Mon553N];
                                    BaseImage = Monster.Mon553N;
                                    BaseSound = (ushort)BaseImage * 10;
                                    Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                                    break;
                            }
                            FrameIndex = Frame.Count - 1;
                            ActionFeed.Clear();
                            ActionFeed.Add(new QueuedAction { Action = MirAction.站立动作, Direction = Direction, Location = CurrentLocation });
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    switch (BaseImage)
                                    {
                                        case Monster.Mon564N:
                                           Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mon564N], 624, 30, Frame.Count * FrameInterval, this));
                                            break;
                                    }
                                    break;
                            }
                            if (FrameIndex == 3)
                                PlayReviveSound();
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.死后尸体:
                    break;

            }

            if ((CurrentAction == MirAction.站立动作 || CurrentAction == MirAction.坐下动作) && NextAction != null)
                SetAction();
            else if (CurrentAction == MirAction.死后尸体 && NextAction != null && (NextAction.Action == MirAction.挖后尸骸 || NextAction.Action == MirAction.复活动作))
                SetAction();
        }

        public int UpdateFrame()
        {
            if (Frame == null) return 0;

            if (FrameLoop != null)
            {
                if (FrameLoop.CurrentCount > FrameLoop.Loops)
                {
                    FrameLoop = null;
                }
                else if (FrameIndex >= FrameLoop.End)
                {
                    FrameIndex = FrameLoop.Start - 1;
                    FrameLoop.CurrentCount++;
                }
            }

            if (Frame.Reverse) return Math.Abs(--FrameIndex);

            return ++FrameIndex;
        }

        public override Missile CreateProjectile(int baseIndex, MLibrary library, bool blend, int count, int interval, int skip, int lightDistance = 6, bool direction16 = true, Color? lightColour = null, uint targetID = 0)
        {
            if (targetID == 0)
            {
                targetID = TargetID;
            }

            MapObject ob = MapControl.GetObject(targetID);

            var targetPoint = TargetPoint;

            if (ob != null) targetPoint = ob.CurrentLocation;

            int duration = Functions.MaxDistance(CurrentLocation, targetPoint) * 50;

            Missile missile = new Missile(library, baseIndex, duration / interval, duration, this, targetPoint, direction16)
            {
                Target = ob,
                Interval = interval,
                FrameCount = count,
                Blend = blend,
                Skip = skip,
                Light = lightDistance,
                LightColour = lightColour == null ? Color.White : (Color)lightColour
            };

            Effects.Add(missile);

            return missile;
        }

        private void PlaySummonSound()
        {
            switch (BaseImage)
            {
                case Monster.HellKnight1:
                case Monster.HellKnight2:
                case Monster.HellKnight3:
                case Monster.HellKnight4:
                case Monster.LightningBead:
                case Monster.HealingBead:
                case Monster.PowerUpBead:
                    SoundManager.PlaySound(BaseSound + 0);
                    return;
                case Monster.BoneFamiliar:
                case Monster.Shinsu:
                case Monster.HolyDeva:
                    SoundManager.PlaySound(BaseSound + 1);
                    return;
            }
        }
        private void PlayWalkSound(bool left = true)
        {
            if (left)
            {
                switch (BaseImage)
                {
                    case Monster.WingedTigerLord:
                    case Monster.PoisonHugger:
                    case Monster.SnowWolfKing:
                    case Monster.Catapult:
                    case Monster.ChariotBallista:
                        SoundManager.PlaySound(BaseSound + 8);
                        return;
                }
            }
            else
            {
                switch (BaseImage)
                {
                    case Monster.WingedTigerLord:
                    case Monster.AvengerPlant:
                        SoundManager.PlaySound(BaseSound + 8);
                        return;
                    case Monster.PoisonHugger:
                    case Monster.SnowWolfKing:
                        SoundManager.PlaySound(BaseSound + 9);
                        return;
                }
            }
        }
        public void PlayAppearSound()
        {
            switch (BaseImage)
            {
                case Monster.CannibalPlant:
                case Monster.WaterDragon:
                case Monster.EvilCentipede:
                case Monster.CreeperPlant:
                    return;
                case Monster.ZumaArcher:
                case Monster.ZumaStatue:
                case Monster.ZumaGuardian:
                case Monster.RedThunderZuma:
                case Monster.FrozenRedZuma:
                case Monster.FrozenZumaStatue:
                case Monster.FrozenZumaGuardian:
                case Monster.ZumaTaurus:
                case Monster.DemonGuard:
                case Monster.Turtlegrass:
                case Monster.ManTree:
                case Monster.EarthGolem:
                case Monster.LightningScroll:
                case Monster.CallScroll:
                case Monster.PoisonScroll:
                case Monster.FireballScroll:
                case Monster.PurpleFaeFlower:
                    if (Stoned) return;
                    break;
                case Monster.DragonStatue:
                    SoundManager.PlaySound(BaseSound + 6);
                    return;
            }

            SoundManager.PlaySound(BaseSound);
        }
        public void PlayPopupSound()
        {
            switch (BaseImage)
            {
                case Monster.ZumaTaurus:
                case Monster.DigOutZombie:
                case Monster.Armadillo:
                case Monster.ArmadilloElder:
                    SoundManager.PlaySound(BaseSound + 1);
                    return;
                case Monster.Shinsu:
                    SoundManager.PlaySound(BaseSound + 6);
                    return;
            }
            SoundManager.PlaySound(BaseSound);
        }

        public void PlayRunSound()
        {
            switch (BaseImage)
            {
                case Monster.HardenRhino:
                    SoundManager.PlaySound(BaseSound + 8);
                    break;
            }
        }

        public void PlayJumpSound()
        {
            switch (BaseImage)
            {
                case Monster.Armadillo:
                case Monster.ArmadilloElder:
                case Monster.ChieftainArcher:
                    SoundManager.PlaySound(BaseSound + 8);
                    break;
            }
        }

        public void PlayDashSound()
        {
            switch (BaseImage)
            {
                case Monster.HornedSorceror:
                    SoundManager.PlaySound(BaseSound + 9);
                    break;
            }
        }

        public void PlayFlinchSound()
        {
            switch (BaseImage)
            {
                default:
                    SoundManager.PlaySound(BaseSound + 4);
                    break;
            }
        }
        public void PlayStruckSound()
        {
            switch(BaseImage)
            {
                case Monster.EvilMir:
                    SoundManager.PlaySound(SoundList.StruckEvilMir);
                    return;
            }

            switch (StruckWeapon)
            {
                case 0:
                case 23:
                case 28:
                case 40:
                    SoundManager.PlaySound(SoundList.StruckWooden);
                    break;
                case 1:
                case 12:
                    SoundManager.PlaySound(SoundList.StruckShort);
                    break;
                case 2:
                case 8:
                case 11:
                case 15:
                case 18:
                case 20:
                case 25:
                case 31:
                case 33:
                case 34:
                case 37:
                case 41:
                    SoundManager.PlaySound(SoundList.StruckSword);
                    break;
                case 3:
                case 5:
                case 7:
                case 9:
                case 13:
                case 19:
                case 24:
                case 26:
                case 29:
                case 32:
                case 35:
                    SoundManager.PlaySound(SoundList.StruckSword2);
                    break;
                case 4:
                case 14:
                case 16:
                case 38:
                    SoundManager.PlaySound(SoundList.StruckAxe);
                    break;
                case 6:
                case 10:
                case 17:
                case 22:
                case 27:
                case 30:
                case 36:
                case 39:
                    SoundManager.PlaySound(SoundList.StruckShort);
                    break;
                case 21:
                    SoundManager.PlaySound(SoundList.StruckClub);
                    break;
            }
        }
        public void PlayAttackSound()
        {
            switch (BaseImage)
            {
                default:
                    SoundManager.PlaySound(BaseSound + 2);
                    break;
            }
        }

        public void PlaySecondAttackSound()
        {
            switch (BaseImage)
            {
                default:
                    SoundManager.PlaySound(BaseSound + 6);
                    break;

            }            
        }

        public void PlayThirdAttackSound()
        {
            switch (BaseImage)
            {
                case Monster.DarkCaptain:
                case Monster.HornedSorceror:
                case Monster.Mon409B:
                    return;
                default:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
            }
        }

        public void PlayFourthAttackSound()
        {
            switch (BaseImage)
            {
                case Monster.Mon409B:
                    return;
                case Monster.SnowWolfKing:
                    SoundManager.PlaySound(BaseSound + 1);
                    return;
                default:
                    SoundManager.PlaySound(BaseSound + 8);
                    return;
            }
        }

        public void PlayFithAttackSound()
        {
            SoundManager.PlaySound(BaseSound + 9);
        }

        public void PlaySwingSound()
        {
            switch (BaseImage)
            {
                case Monster.DarkCaptain:
                case Monster.EvilMir:
                case Monster.DragonStatue:
                    return;
                default:
                    SoundManager.PlaySound(BaseSound + 3);
                    return;
            }
        }
        public void PlayDieSound()
        {
            switch (BaseImage)
            {
                case Monster.RedOrb:
                default:
                    SoundManager.PlaySound(BaseSound + 5);
                    return;
            }
        }

        public void PlayDeadSound()
        {
            switch (BaseImage)
            {
                case Monster.CaveBat:
                case Monster.HellKnight1:
                case Monster.HellKnight2:
                case Monster.HellKnight3:
                case Monster.HellKnight4:
                case Monster.CyanoGhast:
                case Monster.WoodBox:
                    SoundManager.PlaySound(BaseSound + 1);
                    return;
            }
        }
        public void PlayReviveSound()
        {
            switch (BaseImage)
            {
                case Monster.ClZombie:
                case Monster.NdZombie:
                case Monster.CrawlerZombie:
                    SoundManager.PlaySound(SoundList.ZombieRevive);
                    return;
            }
        }
        public void PlayRangeSound()
        {
            switch (BaseImage)
            {
                case Monster.RedThunderZuma:
                case Monster.FrozenRedZuma:
                case Monster.KingScorpion:
                case Monster.DarkDevil:
                case Monster.Khazard:
                case Monster.BoneLord:
                case Monster.LeftGuard:
                case Monster.RightGuard:
                case Monster.FrostTiger:
                case Monster.FlameTiger:
                case Monster.GreatFoxSpirit:
                case Monster.BoneSpearman:
                case Monster.MinotaurKing:
                case Monster.WingedTigerLord:
                case Monster.ManectricClaw:
                case Monster.ManectricKing:
                case Monster.HellBolt:
                case Monster.WitchDoctor:
                case Monster.FlameSpear:
                case Monster.FlameMage:
                case Monster.FlameScythe:
                case Monster.FlameAssassin:
                case Monster.FlameQueen:
                case Monster.DarkDevourer:
                case Monster.DreamDevourer:
                case Monster.FlyingStatue:
                case Monster.IceGuard:
                case Monster.ElementGuard:
                case Monster.KingGuard:
                case Monster.Yimoogi:
                case Monster.RedYimoogi:
                case Monster.Snake10:
                case Monster.Snake11:
                case Monster.Snake12:
                case Monster.Snake13:
                case Monster.Snake14:
                case Monster.Snake15:
                case Monster.Snake16:
                case Monster.Snake17:
                case Monster.BurningZombie:
                case Monster.MudZombie:
                case Monster.FrozenZombie:
                case Monster.UndeadWolf:
                case Monster.CatShaman:
                case Monster.CannibalTentacles:
                case Monster.SwampWarrior:
                case Monster.GeneralMeowMeow:
                case Monster.RhinoPriest:
                case Monster.HardenRhino:
                case Monster.TreeGuardian:
                case Monster.OmaCannibal:
                case Monster.OmaMage:
                case Monster.OmaWitchDoctor:
                case Monster.CreeperPlant:
                case Monster.AvengingSpirit:
                case Monster.AvengingWarrior:
                case Monster.PeacockSpider:
                case Monster.FlamingMutant:
                case Monster.KingHydrax:
                case Monster.DarkCaptain:
                case Monster.DarkOmaKing:
                case Monster.HornedMage:
                case Monster.FrozenKnight:
                case Monster.IcePhantom:
                case Monster.WaterDragon:
                case Monster.BlackTortoise:
                case Monster.EvilMir:
                case Monster.DragonStatue:
                case Monster.RedOrb:
                    SoundManager.PlaySound(BaseSound + 1);
                    return;
                case Monster.AncientBringer:
                case Monster.SeedingsGeneral:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
                case Monster.RestlessJar:
                    SoundManager.PlaySound(BaseSound + 8);
                    return;
                case Monster.TucsonGeneral:
                    return;
                default:
                    PlayAttackSound();
                    return;
            }
        }
        public void PlaySecondRangeSound()
        {
            switch (BaseImage)
            {
                case Monster.TucsonGeneral:
                    SoundManager.PlaySound(BaseSound + 1);
                    return;
                case Monster.TurtleKing:
                    return;
                case Monster.KingGuard:
                case Monster.TreeGuardian:
                case Monster.DarkCaptain:
                case Monster.Mon409B:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
                case Monster.AncientBringer:
                case Monster.SeedingsGeneral:
                    SoundManager.PlaySound(BaseSound + 8);
                    return;
                default:
                    PlaySecondAttackSound();
                    return;
            }
        }

        public void PlayThirdRangeSound()
        {
            switch (BaseImage)
            {
                case Monster.TucsonGeneral:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
                default:
                    PlayThirdAttackSound();
                    return;
            }
        }

        public void PlayPickupSound()
        {
            SoundManager.PlaySound(SoundList.PetPickup);
        }

        public void PlayPetSound()
        {
            int petSound = (ushort)BaseImage - 10000 + 10500;

            switch (BaseImage)
            {
                case Monster.小鸡:
                case Monster.小猪:
                case Monster.小猫:
                case Monster.精灵骷髅:
                case Monster.白猪:
                case Monster.纸片人:
                case Monster.黑猫:
                case Monster.龙蛋:
                case Monster.火娃:
                case Monster.雪人:
                case Monster.青蛙:
                case Monster.红猴:
                case Monster.愤怒的小鸟:
                case Monster.阿福:
                case Monster.治疗拉拉:
                case Monster.猫咪超人:
                case Monster.龙宝宝:
                    SoundManager.PlaySound(petSound);
                    break;
            }
        }
        public override void Draw()
        {
            DrawBehindEffects(Settings.Effect);

            float oldOpacity = DXManager.Opacity;
            if (Hidden && !DXManager.Blending) DXManager.SetOpacity(0.5F);

            if (BodyLibrary == null || Frame == null) return;

            bool oldGrayScale = DXManager.GrayScale;
            Color drawColour = ApplyDrawColour();

            if (!DXManager.Blending && Frame.Blend)
                BodyLibrary.DrawBlend(DrawFrame, DrawLocation, drawColour, true);
            else
                BodyLibrary.Draw(DrawFrame, DrawLocation, drawColour, true);

            DXManager.SetGrayscale(oldGrayScale);
            DXManager.SetOpacity(oldOpacity);
        }


        public override bool MouseOver(Point p)
        {
            return MapControl.MapLocation == CurrentLocation || BodyLibrary != null && BodyLibrary.VisiblePixel(DrawFrame, p.Subtract(FinalDrawLocation), false);
        }

        public override void DrawBehindEffects(bool effectsEnabled)
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                if (!Effects[i].DrawBehind) continue;
                Effects[i].Draw();
            }
        }

        public override void DrawEffects(bool effectsEnabled)
        {
            if (!effectsEnabled) return;

            for (int i = 0; i < Effects.Count; i++)
            {
                if (Effects[i].DrawBehind) continue;
                Effects[i].Draw();
            }

            switch (BaseImage)
            {
                case Monster.Scarecrow:
                    switch (CurrentAction)
                    {
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Scarecrow].DrawBlend(224 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.CaveMaggot:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            if (FrameIndex >= 1)
                                Libraries.Monsters[(ushort)Monster.CaveMaggot].DrawBlend(175 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Skeleton:
                case Monster.BoneFighter:
                case Monster.AxeSkeleton:
                case Monster.BoneWarrior:
                case Monster.BoneElite:
                case Monster.BoneWhoo:
                    switch (CurrentAction)
                    {
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Skeleton].DrawBlend(224 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.WoomaTaurus:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.WoomaTaurus].DrawBlend(224 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Dung:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            if (FrameIndex >= 1)
                                Libraries.Monsters[(ushort)Monster.Dung].DrawBlend(223 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.TaoistGuard: //2
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.TaoistGuard].DrawBlend((80 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.WedgeMoth: //38
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.WedgeMoth].DrawBlend(224 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DarkDevourer: //54
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.DarkDevourer].DrawBlend(272 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.DarkDevourer].DrawBlend(304 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkDevourer].DrawBlend(352 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.DarkDevourer].DrawBlend(400 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.DarkDevourer].DrawBlend(416 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkDevourer].DrawBlend(540 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Behemoth: //57
                    switch (CurrentAction)
                    {
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Behemoth].DrawBlend(464 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Behemoth].DrawBlend(668 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.Behemoth].DrawBlend(512 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.Behemoth].DrawBlend(730 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Behemoth].DrawBlend(592 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    if (CurrentAction != MirAction.死后尸体)
                        Libraries.Monsters[(ushort)Monster.Behemoth].DrawBlend(648 + FrameIndex, DrawLocation, Color.White, true);
                    break;
                case Monster.CyanoGhast: //mob glow effect //136
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.CyanoGhast].DrawBlend(448 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.CyanoGhast].DrawBlend(480 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.CyanoGhast].DrawBlend(528 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.CyanoGhast].DrawBlend(576 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                        case MirAction.复活动作:
                            Libraries.Monsters[(ushort)Monster.CyanoGhast].DrawBlend(592 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MutatedManworm: //137
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MutatedManworm].DrawBlend(285 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.MutatedManworm].DrawBlend(333 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.CrazyManworm: //138
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.CrazyManworm].DrawBlend(272 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DreamDevourer: //139
                    switch (CurrentAction)
                    {
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.DreamDevourer].DrawBlend(264 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.DreamDevourer].DrawBlend(328 + (FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HolyDeva: //172
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.HolyDeva].DrawBlend(0 + FrameIndex + (int)Direction * 4, DrawLocation, Color.Green, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.HolyDeva].DrawBlend(32 + FrameIndex + (int)Direction * 6, DrawLocation, Color.Green, true);
                            break;
                        case MirAction.推开动作:
                            Libraries.Monsters[(ushort)Monster.HolyDeva].DrawBlend(32 + FrameIndex + (int)Direction * 6, DrawLocation, Color.Navy, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.HolyDeva].DrawBlend(80 + FrameIndex + (int)Direction * 6, DrawLocation, Color.Blue, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.HolyDeva].DrawBlend(128 + FrameIndex + (int)Direction * 2, DrawLocation, Color.Tomato, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.HolyDeva].DrawBlend(144 + FrameIndex + (int)Direction * 10, DrawLocation, Color.DarkRed, true);
                            break;
                        case MirAction.召唤初现:
                            Libraries.Monsters[(ushort)Monster.HolyDeva].DrawBlend(224 + FrameIndex * 10, DrawLocation, Color.DarkOrchid, true);
                            break;
                    }
                    break;
                case Monster.KingHog: //181
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.KingHog].DrawBlend(224 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DarkDevil: //182
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkDevil].DrawBlend(342 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BoneLord: //196
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.BoneLord].DrawBlend(400 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                        case MirAction.推开动作:
                            Libraries.Monsters[(ushort)Monster.BoneLord].DrawBlend(432 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.BoneLord].DrawBlend(480 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.BoneLord].DrawBlend(528 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.BoneLord].DrawBlend(576 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.BoneLord].DrawBlend(624 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.BoneLord].DrawBlend(640 + FrameIndex + (int)Direction * 20, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.RightGuard: //205
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.RightGuard].DrawBlend(272 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.LeftGuard: //206
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.LeftGuard].DrawBlend(272 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.LeftGuard].DrawBlend(360 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MinotaurKing: //207
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MinotaurKing].DrawBlend(272 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.YangDevilNode: //216
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.YangDevilNode].DrawBlend(22 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.YinDevilNode: //217
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.YinDevilNode].DrawBlend(22 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.OmaKing: //218
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.OmaKing].DrawBlend(624 + FrameIndex + (int)Direction * 6, DrawLocation, Color.DimGray, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.OmaKing].DrawBlend(672 + FrameIndex, DrawLocation, Color.DimGray, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.OmaKing].DrawBlend(304 + FrameIndex + (int)Direction * 20, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.RedThunderZuma: //221
                case Monster.FrozenRedZuma:
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.RedThunderZuma].DrawBlend(320 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                        case MirAction.推开动作:
                            Libraries.Monsters[(ushort)Monster.RedThunderZuma].DrawBlend(352 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.RedThunderZuma].DrawBlend(400 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.RedThunderZuma].DrawBlend(448 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.RedThunderZuma].DrawBlend(464 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FrostTiger: //222
                    {
                        if (Effect == 1)
                            switch (CurrentAction)
                            {
                                case MirAction.站立动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((314 + FrameIndex + (int)Direction * 4), DrawLocation, Color.Gray, true);
                                    break;
                                case MirAction.行走动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((346 + FrameIndex + (int)Direction * 6), DrawLocation, Color.Gray, true);
                                    break;
                                case MirAction.近距攻击1:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((394 + FrameIndex + (int)Direction * 6), DrawLocation, Color.Gray, true);
                                    break;
                                case MirAction.被击动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((442 + FrameIndex + (int)Direction * 2), DrawLocation, Color.Gray, true);
                                    break;
                                case MirAction.死亡动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((458 + FrameIndex + (int)Direction * 10), DrawLocation, Color.Gray, true);
                                    break;
                                case MirAction.远程攻击1:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((538 + FrameIndex + (int)Direction * 6), DrawLocation, Color.Gray, true);
                                    break;
                                case MirAction.坐下动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((586 + FrameIndex + (int)Direction * 4), DrawLocation, Color.Gray, true);
                                    break;
                            }
                        break;
                    }
                case Monster.FlameTiger: //228
                    {
                        if (Effect == 1)
                            switch (CurrentAction)
                            {
                                case MirAction.站立动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((314 + FrameIndex + (int)Direction * 4), DrawLocation, Color.Brown, true);
                                    break;
                                case MirAction.行走动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((346 + FrameIndex + (int)Direction * 6), DrawLocation, Color.Brown, true);
                                    break;
                                case MirAction.近距攻击1:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((394 + FrameIndex + (int)Direction * 6), DrawLocation, Color.Brown, true);
                                    break;
                                case MirAction.被击动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((442 + FrameIndex + (int)Direction * 2), DrawLocation, Color.Brown, true);
                                    break;
                                case MirAction.死亡动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((458 + FrameIndex + (int)Direction * 10), DrawLocation, Color.Brown, true);
                                    break;
                                case MirAction.远程攻击1:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((538 + FrameIndex + (int)Direction * 6), DrawLocation, Color.Brown, true);
                                    break;
                                case MirAction.坐下动作:
                                    Libraries.Monsters[(ushort)Monster.FrostTiger].DrawBlend((586 + FrameIndex + (int)Direction * 4), DrawLocation, Color.Brown, true);
                                    break;
                            }
                        break;
                    }
                case Monster.WingedTigerLord: //229
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            if (FrameIndex >= 2) Libraries.Monsters[(ushort)Monster.WingedTigerLord].DrawBlend(584 + (FrameIndex + (int)Direction * 6) - 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            if (FrameIndex >= 2) Libraries.Monsters[(ushort)Monster.WingedTigerLord].DrawBlend(560 + (FrameIndex + (int)Direction * 3) - 2, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BlackFoxman: //230
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.BlackFoxman].DrawBlend(234 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.GuardianRock: //234
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.GuardianRock].DrawBlend(8 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ThunderElement: //235
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.ThunderElement].DrawBlend(44 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                        case MirAction.推开动作:
                            Libraries.Monsters[(ushort)Monster.ThunderElement].DrawBlend(54 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ThunderElement].DrawBlend(64 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.ThunderElement].DrawBlend(74 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.ThunderElement].DrawBlend(78 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.CloudElement: //236
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.CloudElement].DrawBlend(44 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                        case MirAction.推开动作:
                            Libraries.Monsters[(ushort)Monster.CloudElement].DrawBlend(54 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.CloudElement].DrawBlend(64 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.CloudElement].DrawBlend(74 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.CloudElement].DrawBlend(78 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.GreatFoxSpirit: //237
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.GreatFoxSpirit].DrawBlend(Frame.Start + 30 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.GreatFoxSpirit].DrawBlend(Frame.Start + 30 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.GreatFoxSpirit].DrawBlend(Frame.Start + 30 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.GreatFoxSpirit].DrawBlend(318 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.TurtleKing: //244
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(456 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(488 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(536 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(616 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                        case MirAction.复活动作:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(632 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(704 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(752 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(800 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击3:
                            Libraries.Monsters[(ushort)Monster.TurtleKing].DrawBlend(848 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HellSlasher: //272
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.HellSlasher].DrawBlend((304 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HellPirate: //273
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.HellPirate].DrawBlend((280 + FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HellKeeper: //275
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            if (FrameIndex >= 2) Libraries.Monsters[(ushort)Monster.HellKeeper].DrawBlend((40 + FrameIndex) - 2, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ManectricStaff: //281
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.ManectricStaff].DrawBlend(296 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ManectricBlest: //285
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.ManectricBlest].DrawBlend(328 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.ManectricBlest].DrawBlend(392 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ManectricKing: //286
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.ManectricKing].DrawBlend(360 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.ManectricKing].DrawBlend(392 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ManectricKing].DrawBlend(440 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.ManectricKing].DrawBlend(584 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.ManectricKing].DrawBlend(488 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.ManectricKing].DrawBlend(504 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FlameSpear: //295
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.FlameSpear].DrawBlend(272 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.FlameSpear].DrawBlend(304 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameSpear].DrawBlend(352 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.FlameSpear].DrawBlend(400 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.FlameSpear].DrawBlend(416 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameSpear].DrawBlend(496 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FlameMage: //296
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.FlameMage].DrawBlend(272 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.FlameMage].DrawBlend(304 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameMage].DrawBlend(352 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.FlameMage].DrawBlend(400 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.FlameMage].DrawBlend(416 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameMage].DrawBlend(496 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FlameScythe: //297
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.FlameScythe].DrawBlend(272 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.FlameScythe].DrawBlend(304 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameScythe].DrawBlend(352 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.FlameScythe].DrawBlend(544 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.FlameScythe].DrawBlend(400 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.FlameScythe].DrawBlend(416 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.FlameScythe].DrawBlend(496 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FlameAssassin: //298
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.FlameAssassin].DrawBlend(272 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.FlameAssassin].DrawBlend(304 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameAssassin].DrawBlend(352 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.FlameAssassin].DrawBlend(544 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.FlameAssassin].DrawBlend(400 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.FlameAssassin].DrawBlend(416 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameAssassin].DrawBlend(496 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FlameQueen: //299
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.FlameQueen].DrawBlend(360 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.FlameQueen].DrawBlend(392 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameQueen].DrawBlend(440 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.FlameQueen].DrawBlend(488 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.FlameQueen].DrawBlend(504 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.FlameQueen].DrawBlend(584 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.FlameQueen].DrawBlend(656 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HellKnight1: //300
                case Monster.HellKnight2: //301
                case Monster.HellKnight3: //302
                case Monster.HellKnight4: //304
                    switch (CurrentAction)
                    {
                        case MirAction.召唤初现:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(224 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(224 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(256 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(304 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(352 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(368 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(400 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HellLord: //304
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                        case MirAction.近距攻击1:
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.HellLord].Draw(15, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.HellLord].Draw(16 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死后尸体:
                            Libraries.Monsters[(ushort)Monster.HellLord].Draw(20, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.WaterGuard: //305
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.WaterGuard].DrawBlend(264 + (FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ElementGuard: //307
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.ElementGuard].DrawBlend(320 + (FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DemonGuard: //308
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DemonGuard].DrawBlend(288 + (FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.KingGuard: //309
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(392 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(424 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(472 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(616 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.推开动作:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(616 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(520 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(664 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(728 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.KingGuard].DrawBlend(536 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DeathCrawler: //318
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.DeathCrawler].DrawBlend(248 + (FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DeathCrawler].DrawBlend(304 + (FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.DeathCrawler].DrawBlend(385 + FrameIndex + (int)Direction * 11, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BurningZombie: //319
                    switch (CurrentAction)
                    {
                        //case MirAction.远程攻击1:
                        //Libraries.Monsters[(ushort)Monster.BurningZombie].DrawBlend(352 + FrameIndex, DrawLocation, Color.White, true);
                        //break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.BurningZombie].DrawBlend(312 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.BurningZombie].DrawBlend(390 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MudZombie: //320
                    switch (CurrentAction)
                    {
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.MudZombie].DrawBlend(304 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FrozenZombie: //321
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenZombie].DrawBlend(312 + (FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.FrozenZombie].DrawBlend(376 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DemonWolf: //323
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DemonWolf].DrawBlend(312 + (FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DarkBeast: //325
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DarkBeast].DrawBlend(296 + (FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.LightBeast: //326
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.LightBeast].DrawBlend(296 + (FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BloodBaboon: //327
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.BloodBaboon].DrawBlend(312 + (FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HardenRhino: //328
                    switch (CurrentAction)
                    {
                        case MirAction.跑步动作:
                            Libraries.Monsters[(ushort)Monster.HardenRhino].DrawBlend(397 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.AncientBringer: //329
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.AncientBringer].DrawBlend(512 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.AncientBringer].DrawBlend(600 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.AncientBringer].DrawBlend((680 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;

                    }
                    break;
                case Monster.FightingCat: //330
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FightingCat].DrawBlend(208 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.CatWidow: //332
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.CatWidow].DrawBlend(256 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.StainHammerCat: //333
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.StainHammerCat].DrawBlend(240 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BlackHammerCat: //334
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.BlackHammerCat].DrawBlend(336 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.BlackHammerCat].DrawBlend(368 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.BlackHammerCat].DrawBlend(416 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.BlackHammerCat].DrawBlend(472 + FrameIndex + (int)Direction * 12, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.BlackHammerCat].DrawBlend(672 + FrameIndex + (int)Direction * 12, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.BlackHammerCat].DrawBlend(568 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.BlackHammerCat].DrawBlend(591 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.StrayCat: //335
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.StrayCat].DrawBlend(528 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.StrayCat].DrawBlend(608 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.StrayCat].DrawBlend(736 + FrameIndex + (int)Direction * 13, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.CatShaman: //336
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(360 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(392 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(472 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(720 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(520 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(576 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(794 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(632 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.CatShaman].DrawBlend(648 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Jar2: //338
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Jar2].DrawBlend(312 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Jar2].DrawBlend(392 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Jar2].DrawBlend(440 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Jar2].DrawBlend(520 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Jar2].DrawBlend(544 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.SeedingsGeneral: //339
                    switch (CurrentAction)
                    {
                        case MirAction.坐下动作:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(536 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(568 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(600 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.跑步动作:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(656 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(704 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(776 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(848 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(912 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(984 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.SeedingsGeneral].DrawBlend(1008 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.GeneralMeowMeow: //341
                    {
                        switch (CurrentAction)
                        {

                            case MirAction.近距攻击1:
                                Libraries.Monsters[(ushort)Monster.GeneralMeowMeow].DrawBlend(416 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                                break;
                        }
                        break;
                    }
                case Monster.TucsonWarrior: //346
                    {
                        switch (CurrentAction)
                        {

                            case MirAction.近距攻击1:
                                Libraries.Monsters[(ushort)Monster.TucsonWarrior].DrawBlend(296 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                                break;
                        }
                        break;
                    }
                case Monster.RhinoWarrior: //359
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.RhinoWarrior].DrawBlend(320 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.TreeGuardian: //364
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.TreeGuardian].DrawBlend(608 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.PeacockSpider: //366
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.PeacockSpider].DrawBlend(592 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DarkBaboon: //367
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DarkBaboon].DrawBlend(296 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.TwinHeadBeast: //368
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.TwinHeadBeast].DrawBlend(352 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.OmaSlasher: //371
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.OmaSlasher].DrawBlend(304 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.OmaAssassin: //372
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.OmaAssassin].DrawBlend(312 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.OmaWitchDoctor:  //374
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.OmaWitchDoctor].DrawBlend(400 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.OmaWitchDoctor].DrawBlend(472 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.OmaWitchDoctor].DrawBlend(520 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.OmaWitchDoctor].DrawBlend(576 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.OmaWitchDoctor].DrawBlend(632 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.OmaWitchDoctor].DrawBlend(704 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.OmaWitchDoctor].DrawBlend(727 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.LightningBead: //375
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.LightningBead].DrawBlend(31 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.LightningBead].DrawBlend(37 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.LightningBead].DrawBlend(44 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.LightningBead].DrawBlend(47 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.召唤初现:
                            Libraries.Monsters[(ushort)Monster.LightningBead].DrawBlend(55 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HealingBead: //376
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.HealingBead].DrawBlend(31 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.HealingBead].DrawBlend(37 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.HealingBead].DrawBlend(44 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.HealingBead].DrawBlend(47 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.召唤初现:
                            Libraries.Monsters[(ushort)Monster.HealingBead].DrawBlend(55 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.PowerUpBead: //377
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.PowerUpBead].DrawBlend(31 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.PowerUpBead].DrawBlend(37 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.PowerUpBead].DrawBlend(44 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.PowerUpBead].DrawBlend(47 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                        case MirAction.召唤初现:
                            Libraries.Monsters[(ushort)Monster.PowerUpBead].DrawBlend(55 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DarkOmaKing:  //378
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((784 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((864 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((912 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((1568 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((984 + FrameIndex + (int)Direction * 34), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((1256 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击4:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((1320 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((1392 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((1464 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.DarkOmaKing].DrawBlend((1488 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mandrill: //381
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mandrill].DrawBlend(264 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.PlagueCrab: //382
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.PlagueCrab].DrawBlend(248 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.PlagueCrab].DrawBlend(280 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.PlagueCrab].DrawBlend(328 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.PlagueCrab].DrawBlend(392 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.PlagueCrab].DrawBlend(416 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ArmedPlant: //385
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ArmedPlant].DrawBlend((256 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.AvengerPlant: //386
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.AvengerPlant].DrawBlend((224 + FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Nadz: //387
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Nadz].DrawBlend((280 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Nadz].DrawBlend((328 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.AvengingWarrior: //390
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.AvengingWarrior].DrawBlend((272 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ClawBeast: //393
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.ClawBeast].DrawBlend(256 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.ClawBeast].DrawBlend(288 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ClawBeast].DrawBlend(336 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.ClawBeast].DrawBlend(416 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.ClawBeast].DrawBlend(440 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DarkCaptain: //395
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(584 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(664 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(728 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(1168 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(784 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(840 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(896 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(952 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击3:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(1008 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(1064 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.DarkCaptain].DrawBlend(1088 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.SackWarrior: //396
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.SackWarrior].DrawBlend(344 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.SackWarrior].DrawBlend(408 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HornedMage: //400
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.HornedMage].DrawBlend((384 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.HornedMage].DrawBlend((416 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.HornedMage].DrawBlend((464 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.HornedMage].DrawBlend((528 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.HornedMage].DrawBlend((600 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.HornedMage].DrawBlend((664 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.HornedMage].DrawBlend((688 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HornedArcher: //401
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.HornedArcher].DrawBlend(336 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ColdArcher: //402
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.ColdArcher].DrawBlend(336 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HornedWarrior: //403
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((376 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((408 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((456 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((752 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((520 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((592 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((656 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.HornedWarrior].DrawBlend((680 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FloatingRock: //405
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:

                            Libraries.Monsters[(ushort)Monster.FloatingRock].DrawBlend((160 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.FloatingRock].DrawBlend((152 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ScalyBeast: //406
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ScalyBeast].DrawBlend((344 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon409B:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon409B].DrawBlend((1254 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon409B].DrawBlend((784 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon409B].DrawBlend((912 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击5:
                            Libraries.Monsters[(ushort)Monster.Mon409B].DrawBlend((1058 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ManTree: //411
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ManTree].DrawBlend((472 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.ManTree].DrawBlend((536 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.ManTree].DrawBlend((616 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ChieftainSword: //414
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ChieftainSword].DrawBlend((792 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.ChieftainSword].DrawBlend((856 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.ChieftainSword].DrawBlend((928 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.ChieftainSword].DrawBlend((1008 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.ChieftainSword].DrawBlend((1098 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击4:
                            Libraries.Monsters[(ushort)Monster.ChieftainSword].DrawBlend((1340 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Bear: //412
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Bear].DrawBlend((321 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.StoningSpider: //419
                    switch (CurrentAction)
                    {
                        case MirAction.召唤初现:
                            Libraries.Monsters[(ushort)Monster.StoningSpider].DrawBlend((16 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FrozenFighter: //425
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenFighter].DrawBlend(336 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.FrozenFighter].DrawBlend(416 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FrozenKnight: //427
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenKnight].DrawBlend(360 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenKnight].DrawBlend(440 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FrozenGolem: //428
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.FrozenGolem].DrawBlend(264 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.FrozenGolem].DrawBlend(296 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenGolem].DrawBlend(344 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.FrozenGolem].DrawBlend(408 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            if (FrameIndex >= 9) Libraries.Monsters[(ushort)Monster.FrozenGolem].DrawBlend((432 + FrameIndex + (int)Direction * 3) - 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.IcePhantom:  //429
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(320 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(352 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(400 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(640 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(472 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(472 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(536 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.IcePhantom].DrawBlend(560 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.SnowWolfKing: //431
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.SnowWolfKing].DrawBlend(456 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.SnowWolfKing].DrawBlend(529 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击4:
                            Libraries.Monsters[(ushort)Monster.SnowWolfKing].DrawBlend(629 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.WaterDragon: //432
                    switch (CurrentAction)
                    {
                        case MirAction.石化苏醒:
                            Libraries.Monsters[(ushort)Monster.WaterDragon].DrawBlend(400 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.WaterDragon].DrawBlend(464 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.WaterDragon].DrawBlend(512 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.WaterDragon].DrawBlend(576 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.WaterDragon].DrawBlend(656 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.WaterDragon].DrawBlend(680 + FrameIndex + (int)Direction * 15, DrawLocation, Color.White, true);
                            break;
                        case MirAction.切换LIB:
                            Libraries.Monsters[(ushort)Monster.WaterDragon].DrawBlend(407 + (FrameIndex * -1) + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BlackTortoise: //433
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.BlackTortoise].DrawBlend(366 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.BlackTortoise].DrawBlend(428 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Manticore: //434
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Manticore].DrawBlend(505 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.Manticore].DrawBlend(576 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DragonWarrior: //435
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.DragonWarrior].DrawBlend(552 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.DragonWarrior].DrawBlend(616 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.DragonWarrior].DrawBlend(696 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Kirin: //438
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((448 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                        case MirAction.坐下动作:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((480 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((552 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((600 + FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((656 + FrameIndex + (int)Direction * 12), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((752 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((800 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Kirin].DrawBlend((936 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FrozenMiner: //442
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenMiner].DrawBlend(432 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.FrozenMiner].DrawBlend(518 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon443N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon443N].DrawBlend(528 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon443N].DrawBlend(614 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.Mon443N].DrawBlend(700 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FrozenMagician://444
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenMagician].DrawBlend(880 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.FrozenMagician].DrawBlend(512 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.FrozenMagician].DrawBlend(694 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.SnowYeti: //445
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.SnowYeti].DrawBlend(504 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.SnowYeti].DrawBlend(576 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.DarkWraith: //447
                    {
                        switch (CurrentAction)
                        {
                            case MirAction.站立动作:
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((360 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                                break;
                            case MirAction.行走动作:
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((392 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                                break;
                            case MirAction.近距攻击1:
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((440 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((720 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                                break;
                            case MirAction.近距攻击2:
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((504 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((790 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                                break;
                            case MirAction.近距攻击3:
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((584 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                                break;
                            case MirAction.被击动作:
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((616 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                                break;
                            case MirAction.死亡动作:
                                Libraries.Monsters[(ushort)Monster.DarkWraith].DrawBlend((640 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                                break;
                        }
                        break;
                    }
                case Monster.DarkSpirit: //448
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.DarkSpirit].DrawBlend((256 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.DarkSpirit].DrawBlend((288 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.DarkSpirit].DrawBlend((336 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.DarkSpirit].DrawBlend((408 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.DarkSpirit].DrawBlend((432 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.CrystalBeast: //449
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(536 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(584 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(632 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(1072 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(672 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(1118 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(712 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(1158 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击4:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(776 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(824 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(920 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(984 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.CrystalBeast].DrawBlend(1008 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.RedOrb: //450
                case Monster.BlueOrb: //451,
                case Monster.YellowOrb: //452,
                case Monster.GreenOrb: //453,
                case Monster.WhiteOrb: //454
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(224 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(272 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(320 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(384 + FrameIndex + (int)Direction * 2, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)BaseImage].DrawBlend(400 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.AntCommander: //456
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.AntCommander].DrawBlend(368 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.AntCommander].DrawBlend(484 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.PurpleFaeFlower: //466
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.PurpleFaeFlower].DrawBlend(436 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Furball: //467
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Furball].DrawBlend(256 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FurbolgWarrior: //469
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FurbolgWarrior].DrawBlend(320 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.FurbolgWarrior].DrawBlend(392 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FurbolgCommander: //471
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.FurbolgCommander].DrawBlend(325 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon472N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon472N].DrawBlend(556 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.GlacierBeast: //474
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.GlacierBeast].DrawBlend(310 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.GlacierWarrior: //475
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.GlacierWarrior].DrawBlend(296 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.刺客冲击:
                            Libraries.Monsters[(ushort)Monster.GlacierWarrior].DrawBlend(358 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.GlacierWarrior].DrawBlend(382 + FrameIndex + (int)Direction * 4, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ShardGuardian: //476
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ShardGuardian].DrawBlend(384 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HoodedIceMage: //482
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.HoodedIceMage].DrawBlend(368 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HoodedPriest: //483
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.HoodedPriest].DrawBlend(360 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.HoodedPriest].DrawBlend(422 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ShardMaiden: //484
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ShardMaiden].DrawBlend(424 + FrameIndex + (int)Direction * 7, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.ShardMaiden].DrawBlend(487 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.ShardMaiden].DrawBlend(567 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.WarBear: //486
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.WarBear].DrawBlend(336 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.WarBear].DrawBlend(400 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ReaperWizard: //488
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ReaperWizard].DrawBlend(392 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ReaperAssassin: //489
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ReaperAssassin].DrawBlend(432 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BlueMonk: //491
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.BlueMonk].DrawBlend(392 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MutantBeserker: //492
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MutantBeserker].DrawBlend((352 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MutantGuardian: //493
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MutantGuardian].DrawBlend((416 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.MutantGuardian].DrawBlend((576 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MutantHighPriest: //494
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MutantHighPriest].DrawBlend((344 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MysteriousMage: //495
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MysteriousMage].DrawBlend((384 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FeatheredWolf: //496
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.FeatheredWolf].DrawBlend((400 + FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.FeatheredWolf].DrawBlend((467 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MysteriousAssassin: //497
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MysteriousAssassin].DrawBlend((416 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.MysteriousAssassin].DrawBlend((480 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MysteriousMonk: //498
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MysteriousMonk].DrawBlend((440 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.MysteriousMonk].DrawBlend((504 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.MysteriousMonk].DrawBlend((664 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ManEatingPlant: //499
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.ManEatingPlant].DrawBlend(384 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.HammerDwarf: //500
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.HammerDwarf].DrawBlend(384 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.HammerDwarf].DrawBlend(448 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.ArcherDwarf: //501
                    switch (CurrentAction)
                    {

                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.ArcherDwarf].DrawBlend(336 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.NobleWarrior: //502
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.NobleWarrior].DrawBlend(400 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.NobleAssassin: //505
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.NobleAssassin].DrawBlend(408 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.NobleAssassin].DrawBlend(472 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Swain: //508
                    switch (CurrentAction)
                    {
                        case MirAction.石化苏醒:
                            Libraries.Monsters[(ushort)Monster.Swain].DrawBlend(549 + FrameIndex + (int)Direction * 19, DrawLocation, Color.White, true);
                            SoundManager.PlaySound(5085);
                            break;
                    }
                    break;
                case Monster.Swain1: //509
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Swain1].DrawBlend(424 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Swain1].DrawBlend(518 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.Swain1].DrawBlend(627 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Swain1].DrawBlend(712 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.AncientStoneGolem: //514
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.AncientStoneGolem].DrawBlend(498 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.AncientStoneGolem].DrawBlend(570 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Butcher: //516
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.Butcher].DrawBlend(536 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Riklebites: //518
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(528 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(576 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(624 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(688 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(760 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(832 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(904 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(968 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Riklebites].DrawBlend(992 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.FeralFlameFurbolg: //522
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.FeralFlameFurbolg].DrawBlend(504 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BlueArcaneTotem: //525
                     switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                        case MirAction.被击动作:
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.BlueArcaneTotem].DrawBlend(10 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.GreenArcaneTotem: //526
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                        case MirAction.被击动作:
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.GreenArcaneTotem].DrawBlend(10 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.RedArcaneTotem: //527
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                        case MirAction.被击动作:
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.RedArcaneTotem].DrawBlend(10 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.YellowArcaneTotem: //528
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                        case MirAction.被击动作:
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.YellowArcaneTotem].DrawBlend(10 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.WarpGate: //529
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                        case MirAction.被击动作:
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.WarpGate].DrawBlend(10 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.SpectralWraith: //530
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.SpectralWraith].DrawBlend(296 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.SpectralWraith].DrawBlend(344 + FrameIndex + (int)Direction * 6, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.SpectralWraith].DrawBlend(392 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.SpectralWraith].DrawBlend(456 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.SpectralWraith].DrawBlend(528 + FrameIndex + (int)Direction * 3, DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.SpectralWraith].DrawBlend(552 + FrameIndex + (int)Direction * 5, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.BloodLord: //532
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.BloodLord].DrawBlend(328 + FrameIndex + (int)Direction * 9, DrawLocation, Color.FromArgb(128, 128, 128, 128), true);
                            break;
                    }
                    break;
                case Monster.SerpentLord: //533
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.SerpentLord].DrawBlend(320 + FrameIndex + (int)Direction * 10, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.SerpentLord].DrawBlend(400 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.MirEmperor: //534
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.MirEmperor].DrawBlend(42 + FrameIndex, DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.MirEmperor].DrawBlend(52 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon540N: //540
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon540N].DrawBlend(328 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon541N: //541
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon541N].DrawBlend(256 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon544S: //544
                    switch (CurrentAction)
                    {

                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon544S].DrawBlend(400 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon544S].DrawBlend(472 + FrameIndex + (int)Direction * 9, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon545N: //545
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Mon545N].DrawBlend((376 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Mon545N].DrawBlend((424 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon545N].DrawBlend((456 + FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon545N].DrawBlend((512 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon545N].DrawBlend((584 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Mon545N].DrawBlend((648 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon545N].DrawBlend((672 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon547N: //547
                    switch (CurrentAction)
                    {
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon547N].DrawBlend((288 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon547N].DrawBlend((426 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon548N: //548
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon548N].DrawBlend((304 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon548N].DrawBlend((376 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.Mon548N].DrawBlend((456 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon550N: //550
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon550N].DrawBlend(362 + FrameIndex + (int)Direction * 10, DrawLocation, Color.FromArgb(135, 255, 200, 135), true);
                            break;
                    }
                    break;
                case Monster.Mon552N: //552
                case Monster.Mon553N: //553
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon552N].DrawBlend(336 + FrameIndex + (int)Direction * 8, DrawLocation, Color.White, true);
                            break;
                        case MirAction.复活动作:
                            Libraries.Monsters[(ushort)Monster.Mon552N].DrawBlend(400 + FrameIndex, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon555N: //555
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon555N].DrawBlend((334 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon556B: //556
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Mon556B].DrawBlend((400 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Mon556B].DrawBlend((448 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon556B].DrawBlend((496 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.NobleWarrior].DrawBlend((400 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon556B].DrawBlend((560 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon556B].DrawBlend((640 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Mon556B].DrawBlend((704 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon556B].DrawBlend((736 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon557B: //557
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Mon557B].DrawBlend((400 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Mon557B].DrawBlend((448 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon557B].DrawBlend((496 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon557B].DrawBlend((568 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击3:
                            Libraries.Monsters[(ushort)Monster.Mon557B].DrawBlend((648 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Mon557B].DrawBlend((712 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon557B].DrawBlend((736 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon558B: //558
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Mon558B].DrawBlend((400 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Mon558B].DrawBlend((448 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon558B].DrawBlend((496 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon558B].DrawBlend((560 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击3:
                            Libraries.Monsters[(ushort)Monster.Mon558B].DrawBlend((624 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Mon558B].DrawBlend((696 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon558B].DrawBlend((720 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon559B: //559
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Mon559B].DrawBlend((408 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Mon559B].DrawBlend((456 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon559B].DrawBlend((504 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.NobleAssassin].DrawBlend((408 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon559B].DrawBlend((568 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.NobleAssassin].DrawBlend((472 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon559B].DrawBlend((640 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Mon559B].DrawBlend((712 + FrameIndex + (int)Direction * 4), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon559B].DrawBlend((744 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon560N:
                    switch (CurrentAction)
                    {
                        case MirAction.站立动作:
                            Libraries.Monsters[(ushort)Monster.Mon560N].DrawBlend((400 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.行走动作:
                            Libraries.Monsters[(ushort)Monster.Mon560N].DrawBlend((448 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon560N].DrawBlend((496 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon560N].DrawBlend((560 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon560N].DrawBlend((632 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                        case MirAction.被击动作:
                            Libraries.Monsters[(ushort)Monster.Mon560N].DrawBlend((696 + FrameIndex + (int)Direction * 3), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon560N].DrawBlend((720 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon561N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon561N].DrawBlend((552 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon562N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon562N].DrawBlend((576 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon563N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon563N].DrawBlend((592 + FrameIndex + (int)Direction * 9), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon564N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon564N].DrawBlend((836 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon564N].DrawBlend((936 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon564N].DrawBlend((676 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon571B:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon571B].DrawBlend((510 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon577N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon577N].DrawBlend((360 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.远程攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon577N].DrawBlend((433 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon577N].DrawBlend((481 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            Libraries.Monsters[(ushort)Monster.Mon577N].DrawBlend((545 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon578N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon578N].DrawBlend((352 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击3:
                            Libraries.Monsters[(ushort)Monster.Mon578N].DrawBlend((400 + FrameIndex + (int)Direction * 11), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon579B:
                    switch (CurrentAction)
                    {
                        case MirAction.刺客冲击:
                            Libraries.Monsters[(ushort)Monster.Mon579B].DrawBlend((494 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon588N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon588N].DrawBlend((408 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon603B:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon603B].DrawBlend((368 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon605N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon605N].DrawBlend((408 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon605N].DrawBlend((456 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon607N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon607N].DrawBlend((480 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击5:
                            Libraries.Monsters[(ushort)Monster.Mon607N].DrawBlend((545 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon608N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon608N].DrawBlend((472 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.死亡动作:
                            Libraries.Monsters[(ushort)Monster.Mon608N].DrawBlend((392 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.Mon609N:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Monsters[(ushort)Monster.Mon609N].DrawBlend((464 + FrameIndex + (int)Direction * 7), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.Mon609N].DrawBlend((520 + FrameIndex + (int)Direction * 8), DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.StoningStatue:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            if (FrameIndex == 4)
                            {
                                SoundManager.PlaySound(BaseSound + 1);
                            }
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Monsters[(ushort)Monster.StoningStatue].DrawBlend(464 + FrameIndex + (int)Direction * 20, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.TucsonGeneral:
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            if (FrameIndex >= 2) Libraries.Monsters[(ushort)Monster.TucsonGeneral].DrawBlend((504 + FrameIndex + (int)Direction * 5) - 2, DrawLocation, Color.White, true);
                            break;
                    }
                    break;
                case Monster.龙宝宝: //Pet16
                    switch (CurrentAction)
                    {
                        case MirAction.近距攻击1:
                            Libraries.Pets[(ushort)Monster.龙宝宝 - 10000].DrawBlend((304 + FrameIndex + (int)Direction * 6), DrawLocation, Color.White, true);
                            break;
                        case MirAction.近距攻击2:
                            Libraries.Pets[(ushort)Monster.龙宝宝 - 10000].DrawBlend((352 + FrameIndex + (int)Direction * 10), DrawLocation, Color.White, true);
                            break;
                    }
                    break;

            } //END OF DRAW EFFECTS
        }

        public override void DrawName()
        {
            if (!Name.Contains("_"))
            {
                base.DrawName();
                return;
            }

            string[] splitName = Name.Split('_');

            //IntelligentCreature
            int yOffset = 0;
            switch (BaseImage)
            {
                case Monster.小鸡:
                    yOffset = -10;
                    break;
                case Monster.小猪:
                case Monster.小猫:
                case Monster.精灵骷髅:
                case Monster.白猪:
                case Monster.纸片人:
                case Monster.黑猫:
                case Monster.龙蛋:
                case Monster.火娃:
                case Monster.雪人:
                case Monster.青蛙:
                case Monster.红猴:
                case Monster.愤怒的小鸟:
                case Monster.阿福:
                case Monster.治疗拉拉:
                case Monster.猫咪超人:
                case Monster.龙宝宝:
                    yOffset = -20;
                    break;
            }

            for (int s = 0; s < splitName.Count(); s++)
            {
                CreateMonsterLabel(splitName[s], s);

                TempLabel.Text = splitName[s];
                TempLabel.Location = new Point(DisplayRectangle.X + (48 - TempLabel.Size.Width) / 2, DisplayRectangle.Y - (32 - TempLabel.Size.Height / 2) + (Dead ? 35 : 8) - (((splitName.Count() - 1) * 10) / 2) + (s * 12) + yOffset);
                TempLabel.Draw();
            }
        }

        public void CreateMonsterLabel(string word, int wordOrder)
        {
            TempLabel = null;

            for (int i = 0; i < LabelList.Count; i++)
            {
                if (LabelList[i].Text != word) continue;
                TempLabel = LabelList[i];
                break;
            }

            if (TempLabel != null && !TempLabel.IsDisposed && NameColour == OldNameColor) return;

            OldNameColor = NameColour;

            TempLabel = new MirLabel
            {
                AutoSize = true,
                BackColour = Color.Transparent,
                ForeColour = NameColour,
                OutLine = true,
                OutLineColour = Color.Black,
                Text = word,
            };

            TempLabel.Disposing += (o, e) => LabelList.Remove(TempLabel);
            LabelList.Add(TempLabel);
        }

        public override void DrawChat()
        {
            if (ChatLabel == null || ChatLabel.IsDisposed) return;

            if (CMain.Time > ChatTime)
            {
                ChatLabel.Dispose();
                ChatLabel = null;
                return;
            }

            //IntelligentCreature
            int yOffset = 0;
            switch (BaseImage)
            {
                case Monster.小鸡:
                    yOffset = 30;
                    break;
                case Monster.小猪:
                case Monster.小猫:
                case Monster.精灵骷髅:
                case Monster.白猪:
                case Monster.纸片人:
                case Monster.黑猫:
                case Monster.龙蛋:
                case Monster.火娃:
                case Monster.雪人:
                case Monster.青蛙:
                case Monster.红猴:
                case Monster.愤怒的小鸟:
                case Monster.阿福:
                case Monster.治疗拉拉:
                case Monster.猫咪超人:
                case Monster.龙宝宝:
                    yOffset = 20;
                    break;
            }

            ChatLabel.ForeColour = Dead ? Color.Gray : Color.White;
            ChatLabel.Location = new Point(DisplayRectangle.X + (48 - ChatLabel.Size.Width) / 2, DisplayRectangle.Y - (60 + ChatLabel.Size.Height) - (Dead ? 35 : 0) + yOffset);
            ChatLabel.Draw();
        }
    }
}
