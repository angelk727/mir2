namespace Client.MirObjects
{
    public class FrameSet : Dictionary<MirAction, Frame>
    {
        public static FrameSet Player;
        public static FrameSet DefaultNPC, DefaultMonster;
        public static List<FrameSet> DragonStatue, GreatFoxSpirit, HellBomb, Mon380P, Gates;

        static FrameSet()
        {
            FrameSet frame;

            Player = new FrameSet();

            //Default NPC
            DefaultNPC = new FrameSet
            {
                { MirAction.站立动作, new Frame(0, 4, 0, 500) },
                { MirAction.挖矿展示, new Frame(12, 10, 0, 200) }
            };

            //Default Monster
            DefaultMonster = new FrameSet
            {
                { MirAction.站立动作, new Frame(0, 4, 0, 500) },
                { MirAction.行走动作, new Frame(32, 6, 0, 200) },
                { MirAction.近距攻击1, new Frame(80, 6, 0, 200) },
                { MirAction.被击动作, new Frame(128, 2, 0, 200) },
                { MirAction.死亡动作, new Frame(144, 10, 0, 200) },
                { MirAction.死后尸体, new Frame(153, 1, 9, 2000) },
                { MirAction.复活动作, new Frame(144, 10, 0, 200) { Reverse = true } }
            };

            #region DragonStatue
            //DragonStatue 1
            DragonStatue = new List<FrameSet> { (frame = new FrameSet()) };
            frame.Add(MirAction.站立动作, new Frame(301, 1, -1, 500));
            frame.Add(MirAction.远程攻击1, new Frame(301, 1, -1, 200));
            frame.Add(MirAction.被击动作, new Frame(302, 1, -1, 200));
            frame.Add(MirAction.死后尸体, new Frame(300, 1, -1, 2000));

            //DragonStatue 2
            DragonStatue.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(301, 1, -1, 500));
            frame.Add(MirAction.远程攻击1, new Frame(301, 1, -1, 200));
            frame.Add(MirAction.被击动作, new Frame(302, 1, -1, 200));
            frame.Add(MirAction.死后尸体, new Frame(300, 1, -1, 2000));
            
            //DragonStatue 3
            DragonStatue.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(301, 1, -1, 500));
            frame.Add(MirAction.远程攻击1, new Frame(301, 1, -1, 200));
            frame.Add(MirAction.被击动作, new Frame(302, 1, -1, 200));
            frame.Add(MirAction.死后尸体, new Frame(300, 1, -1, 2000));

            //DragonStatue 4
            DragonStatue.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(322, 1, -1, 500));
            frame.Add(MirAction.远程攻击1, new Frame(322, 1, -1, 200));
            frame.Add(MirAction.被击动作, new Frame(320, 1, -1, 200));
            frame.Add(MirAction.死后尸体, new Frame(321, 1, -1, 2000));

            //DragonStatue 5
            DragonStatue.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(322, 1, -1, 500));
            frame.Add(MirAction.远程攻击1, new Frame(322, 1, -1, 200));
            frame.Add(MirAction.被击动作, new Frame(320, 1, -1, 200));
            frame.Add(MirAction.死后尸体, new Frame(321, 1, -1, 2000));

            //DragonStatue 6
            DragonStatue.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(322, 1, -1, 500));
            frame.Add(MirAction.远程攻击1, new Frame(322, 1, -1, 200));
            frame.Add(MirAction.被击动作, new Frame(320, 1, -1, 200));
            frame.Add(MirAction.死后尸体, new Frame(321, 1, -1, 2000));
            #endregion

            #region GreatFoxSpirit
            //GreatFoxSpirit level 0
            GreatFoxSpirit = new List<FrameSet> { (frame = new FrameSet()) };
            frame.Add(MirAction.站立动作, new Frame(0, 20, -20, 100));
            frame.Add(MirAction.近距攻击1, new Frame(22, 8, -8, 120));
            frame.Add(MirAction.被击动作, new Frame(20, 2, -2, 200));
            frame.Add(MirAction.死亡动作, new Frame(300, 18, -18, 120));
            frame.Add(MirAction.死后尸体, new Frame(317, 1, -1, 1000));
            frame.Add(MirAction.复活动作, new Frame(300, 18, -18, 150) { Reverse = true });

            //GreatFoxSpirit level 1
            GreatFoxSpirit.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(60, 20, -20, 100));
            frame.Add(MirAction.近距攻击1, new Frame(82, 8, -8, 120));
            frame.Add(MirAction.被击动作, new Frame(80, 2, -2, 200));
            frame.Add(MirAction.死亡动作, new Frame(300, 18, -18, 120));
            frame.Add(MirAction.死后尸体, new Frame(317, 1, -1, 1000));
            frame.Add(MirAction.复活动作, new Frame(300, 18, -18, 150) { Reverse = true });

            //GreatFoxSpirit level 2
            GreatFoxSpirit.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(120, 20, -20, 100));
            frame.Add(MirAction.近距攻击1, new Frame(142, 8, -8, 120));
            frame.Add(MirAction.被击动作, new Frame(140, 2, -2, 200));
            frame.Add(MirAction.死亡动作, new Frame(300, 18, -18, 120));
            frame.Add(MirAction.死后尸体, new Frame(317, 1, -1, 1000));
            frame.Add(MirAction.复活动作, new Frame(300, 18, -18, 150) { Reverse = true });

            //GreatFoxSpirit level 3
            GreatFoxSpirit.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(180, 20, -20, 100));
            frame.Add(MirAction.近距攻击1, new Frame(202, 8, -8, 120));
            frame.Add(MirAction.被击动作, new Frame(200, 2, -2, 200));
            frame.Add(MirAction.死亡动作, new Frame(300, 18, -18, 120));
            frame.Add(MirAction.死后尸体, new Frame(317, 1, -1, 1000));
            frame.Add(MirAction.复活动作, new Frame(300, 18, -18, 150) { Reverse = true });

            //GreatFoxSpirit level 4
            GreatFoxSpirit.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(240, 20, -20, 100));
            frame.Add(MirAction.近距攻击1, new Frame(262, 8, -8, 120));
            frame.Add(MirAction.被击动作, new Frame(260, 2, -2, 200));
            frame.Add(MirAction.死亡动作, new Frame(300, 18, -18, 120));
            frame.Add(MirAction.死后尸体, new Frame(317, 1, -1, 1000));
            frame.Add(MirAction.复活动作, new Frame(300, 18, -18, 150) { Reverse = true });
            #endregion

            #region HellBombs
            //HellBomb1
            HellBomb = new List<FrameSet> { (frame = new FrameSet()) };
            frame.Add(MirAction.站立动作, new Frame(52, 9, -9, 100) { Blend = true });
            frame.Add(MirAction.近距攻击1, new Frame(999, 1, -1, 120) { Blend = true });
            frame.Add(MirAction.被击动作, new Frame(52, 9, -9, 100) { Blend = true });

            //HellBomb2
            HellBomb.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(70, 9, -9, 100) { Blend = true });
            frame.Add(MirAction.近距攻击1, new Frame(999, 1, -1, 120) { Blend = true });
            frame.Add(MirAction.被击动作, new Frame(70, 9, -9, 100) { Blend = true });

            //HellBomb3
            HellBomb.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(88, 9, -9, 100) { Blend = true });
            frame.Add(MirAction.近距攻击1, new Frame(999, 1, -1, 120) { Blend = true });
            frame.Add(MirAction.被击动作, new Frame(88, 9, -9, 100) { Blend = true });
            #endregion

            #region Mon380P
            //Mon380P1
            Mon380P = new List<FrameSet> { (frame = new FrameSet()) };
            frame.Add(MirAction.站立动作, new Frame(0, 1, -1, 100) { Blend = false });
            frame.Add(MirAction.被击动作, new Frame(0, 1, -1, 100) { Blend = false });
            frame.Add(MirAction.死亡动作, new Frame(2, 8, -8, 100) { Blend = false });
            frame.Add(MirAction.死后尸体, new Frame(9, 1, -1, 100) { Blend = false });

            //Mon380P2
            Mon380P.Add(frame = new FrameSet());
            frame.Add(MirAction.站立动作, new Frame(18, 1, -1, 100) { Blend = false });
            frame.Add(MirAction.被击动作, new Frame(18, 1, -1, 100) { Blend = false });
            frame.Add(MirAction.死亡动作, new Frame(20, 8, -8, 100) { Blend = false });
            frame.Add(MirAction.死后尸体, new Frame(27, 1, -1, 100) { Blend = false });
            #endregion

            #region Gates
            //SabukGate
            Gates = new List<FrameSet> { (frame = new FrameSet()) };
            frame.Add(MirAction.站立动作, new Frame(0, 1, 7, 500) { Blend = false });
            frame.Add(MirAction.被击动作, new Frame(1, 3, 5, 200) { Blend = false });
            frame.Add(MirAction.死亡动作, new Frame(24, 10, -10, 200) { Blend = false });
            frame.Add(MirAction.死后尸体, new Frame(33, 1, -1, 2000) { Blend = false });
            frame.Add(MirAction.近距攻击1, new Frame(56, 6, -6, 200) { Blend = false });
            frame.Add(MirAction.近距攻击2, new Frame(64, 6, -6, 200) { Blend = false });
            #endregion

            #region Player
            //Common
            Player.Add(MirAction.站立动作, new Frame(0, 4, 0, 500, 0, 8, 0, 250));
            Player.Add(MirAction.行走动作, new Frame(32, 6, 0, 100, 64, 6, 0, 100));
            Player.Add(MirAction.跑步动作, new Frame(80, 6, 0, 100, 112, 6, 0, 100));
            Player.Add(MirAction.站立姿势, new Frame(128, 1, 0, 1000, 160, 1, 0, 1000));
            Player.Add(MirAction.站立姿势2, new Frame(300, 1, 5, 1000, 332, 1, 5, 1000));
            Player.Add(MirAction.近距攻击1, new Frame(136, 6, 0, 100, 168, 6, 0, 100));
            Player.Add(MirAction.近距攻击2, new Frame(184, 6, 0, 100, 216, 6, 0, 100));
            Player.Add(MirAction.近距攻击3, new Frame(232, 8, 0, 100, 264, 8, 0, 100));
            Player.Add(MirAction.近距攻击4, new Frame(416, 6, 0, 100, 448, 6, 0, 100));
            Player.Add(MirAction.施法动作, new Frame(296, 6, 0, 100, 328, 6, 0, 100));
            Player.Add(MirAction.挖矿展示, new Frame(344, 2, 0, 300, 376, 2, 0, 300));
            Player.Add(MirAction.被击动作, new Frame(360, 3, 0, 100, 392, 3, 0, 100));
            Player.Add(MirAction.死亡动作, new Frame(384, 4, 0, 100, 416, 4, 0, 100));
            Player.Add(MirAction.死后尸体, new Frame(387, 1, 3, 1000, 419, 1, 3, 1000));
            Player.Add(MirAction.复活动作, new Frame(384, 4, 0, 100, 416, 4, 0, 100) { Reverse = true });
            Player.Add(MirAction.挖矿动作, new Frame(184, 6, 0, 100, 216, 6, 0, 100));
            Player.Add(MirAction.刺客步刺, new Frame(139, 1, 5, 1000, 300, 1, 5, 1000));

            //刺客
            Player.Add(MirAction.刺客潜行, new Frame(464, 6, 0, 100, 496, 6, 0, 100));
            Player.Add(MirAction.刺客冲击, new Frame(80, 3, 3, 100, 112, 3, 3, 100));

            //弓箭
            Player.Add(MirAction.弓箭行走, new Frame(0, 6, 0, 100, 0, 6, 0, 100));
            Player.Add(MirAction.弓箭奔跑, new Frame(48, 6, 0, 100, 48, 6, 0, 100));
            Player.Add(MirAction.远程攻击1, new Frame(96, 8, 0, 100, 96, 8, 0, 100));
            Player.Add(MirAction.远程攻击2, new Frame(160, 8, 0, 100, 160, 8, 0, 100));
            Player.Add(MirAction.远程攻击3, new Frame(224, 8, 0, 100, 224, 8, 0, 100));
            Player.Add(MirAction.弓箭跳跃, new Frame(288, 8, 0, 100, 288, 8, 0, 100));

            //骑马动作
            Player.Add(MirAction.坐骑站立, new Frame(416, 4, 0, 500, 448, 4, 0, 500));
            Player.Add(MirAction.坐骑行走, new Frame(448, 8, 0, 100, 480, 8, 0, 500));
            Player.Add(MirAction.坐骑奔跑, new Frame(512, 6, 0, 100, 544, 6, 0, 100));
            Player.Add(MirAction.坐骑被击, new Frame(560, 3, 0, 100, 592, 3, 0, 100));
            Player.Add(MirAction.坐骑攻击, new Frame(584, 6, 0, 100, 616, 6, 0, 100));

            //钓鱼动作
            Player.Add(MirAction.钓鱼抛竿, new Frame(632, 8, 0, 100));
            Player.Add(MirAction.钓鱼等待, new Frame(696, 6, 0, 120));
            Player.Add(MirAction.钓鱼收线, new Frame(744, 8, 0, 100));

            #endregion
        }
    }

    public class Frame
    {
        public int Start, Count, Skip, EffectStart, EffectCount, EffectSkip;
        public int Interval, EffectInterval;
        public bool Reverse, Blend;

        public int OffSet
        {
            get { return Count + Skip; }
        }

        public int EffectOffSet
        {
            get { return EffectCount + EffectSkip; }
        }

        public Frame(int start, int count, int skip, int interval, int effectstart = 0, int effectcount = 0, int effectskip = 0, int effectinterval = 0)
        {
            Start = start;
            Count = count;
            Skip = skip;
            Interval = interval;
            EffectStart = effectstart;
            EffectCount = effectcount;
            EffectSkip = effectskip;
            EffectInterval = effectinterval;
        }

        public Frame(BinaryReader reader)
        {
            Start = reader.ReadInt32();
            Count = reader.ReadInt32();
            Skip = reader.ReadInt32();
            Interval = reader.ReadInt32();
            EffectStart = reader.ReadInt32();
            EffectCount = reader.ReadInt32();
            EffectSkip = reader.ReadInt32();
            EffectInterval = reader.ReadInt32();
            Reverse = reader.ReadBoolean();
            Blend = reader.ReadBoolean();
        }
    }

}
