using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon244B : MonsterObject
    {
        public byte CloseRange = 2;
        private const int SpawnDelay = 30000;
        private long _nextSpawnTime;
        private byte _stage = 5;
        private int spawnCount = 3;

        protected internal Mon244B(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target == null || Target.CurrentMap != CurrentMap)
                return false;

            return Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
        }

        protected override void ProcessAI()
        {
            if (Dead)
                return;

            if (Stats[Stat.HP] >= 5)
            {
                byte stage = (byte)(HP / (Stats[Stat.HP] / 5));

                if (stage < _stage)
                {
                    int oldStageLimit = (5 - _stage) * spawnCount;
                    int newStageLimit = (5 - stage) * spawnCount;

                    int addCount = newStageLimit - oldStageLimit;

                    string[] turtles = [Settings.Turtle1, Settings.Turtle2, Settings.Turtle3, Settings.Turtle4, Settings.Turtle5];

                    for (int i = 0; i < addCount; i++)
                    {
                        SpawnSlaves(Envir.GetMonsterInfo(turtles[Envir.Random.Next(turtles.Length)]), 1, false);
                    }

                    _stage = stage;
                }

                int stageLimit = (5 - stage) * spawnCount;

                if (ActiveSlaveCount < stageLimit && Envir.Time >= _nextSpawnTime)
                {
                    string[] turtles = [Settings.Turtle1, Settings.Turtle2, Settings.Turtle3, Settings.Turtle4, Settings.Turtle5];

                    SpawnSlaves(Envir.GetMonsterInfo(turtles[Envir.Random.Next(turtles.Length)]), 1, false);

                    _nextSpawnTime = Envir.Time + SpawnDelay;
                }
            }

            base.ProcessAI();
        }
        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, CloseRange);

            if (!ranged)
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            WideLineAttack(damage, 3, 600, DefenceType.ACAgility, false, 3);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            FullmoonAttack(damage, 600, DefenceType.ACAgility, -1, 2);
                        }
                        break;
                    case 2:
                        {
                            Point[] locations = MultiPointAreaAttack(5, true, 3, false, 0, false);
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1, Locations = locations });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC] * Math.Max(1, ActiveSlaveCount));
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 1200, Target, damage, DefenceType.MAC);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            else
            {
                switch (Envir.Random.Next(5))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 600, Target, damage, DefenceType.MAC);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage <= 0) return;

                            TargetAreaAttack(Spell.Mon244BLGWhirlwind, damage, 3, 500, Settings.Second * 3, 1500, DefenceType.MAC);
                            int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 60 + 600;
                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + delay, Target, damage, DefenceType.MACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Point[] locations = MultiPointAreaAttack(3, false, 3, false, 1, false);
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1, Locations = locations });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC] * Math.Max(1, ActiveSlaveCount));
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 600, Target, damage, DefenceType.MAC);
                            ActionList.Add(action);
                        }
                        break;
                    case 3:
                        if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, CloseRange) && Envir.Random.Next(4) == 0)
                        {
                            Point target = Functions.PointMove(CurrentLocation, Direction, 1);
                            Target.Teleport(CurrentMap, target, true, 6);
                        }
                        break;
                    case 4:
                            if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, CloseRange) && Envir.Random.Next(4) == 0)
                            {
                                Point target = Functions.PointMove(Target.CurrentLocation, Target.Direction, 1);
                                Teleport(CurrentMap, target, true, 6);
                            }
                        break;
                }

            }
        }
        private int ActiveSlaveCount
        {
            get
            {
                int count = 0;

                for (int i = SlaveList.Count - 1; i >= 0; i--)
                {
                    MonsterObject mob = SlaveList[i];

                    if (mob == null || mob.Dead || mob.CurrentMap == null)
                    {
                        SlaveList.RemoveAt(i);
                        continue;
                    }
                    count++;
                }

                return count;
            }
        }
        public override void Die()
        {
            foreach (var slave in SlaveList)
            {
                slave.Die();
            }

            base.Die();
        }
    }
}
