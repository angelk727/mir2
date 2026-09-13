using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon637B : MonsterObject
    {
        public bool Visible;
        public long VisibleTime;

        protected internal Mon637B(MonsterInfo info)
            : base(info)
        {
            Visible = false;
            VisibleTime = 0;
        }
        private readonly MonsterInfo Mon637BMob = Envir.GetMonsterInfo(Settings.Mon637BMob);
        protected override bool CanMove
        {
            get { return false; }
        }

        public override bool Walk(MirDirection dir)
        {
            return false;
        }

        protected override bool CanAttack
        {
            get
            {
                return Visible && base.CanAttack;
            }
        }

        public override bool Blocking
        {
            get
            {
                return Visible && base.Blocking;
            }
        }

        public override bool IsAttackTarget(MonsterObject attacker)
        {
            return Visible && base.IsAttackTarget(attacker);
        }

        public override bool IsAttackTarget(HumanObject attacker)
        {
            return Visible && base.IsAttackTarget(attacker);
        }

        protected override void ProcessRoam()
        {
        }

        protected override void ProcessSearch()
        {
            if (!Visible)
                return;

            base.ProcessSearch();
        }

        public override Packet GetInfo()
        {
            return Visible ? base.GetInfo() : null;
        }

        protected override void ProcessAI()
        {
            if (!Visible)
                SetHP(Stats[Stat.HP]);

            if (!Dead && Envir.Time > VisibleTime)
            {
                VisibleTime = Envir.Time + 2000;

                bool nearby = FindNearby(Visible ? 7 : 3);

                if (!Visible && nearby)
                {
                    Visible = true;
                    CellTime = Envir.Time + 900;

                    Broadcast(GetInfo());
                    Broadcast(new S.ObjectShow { ObjectID = ObjectID });

                    ActionTime = Envir.Time + 2000;
                }
                else if (Visible && !nearby)
                {
                    Visible = false;
                    VisibleTime = Envir.Time + 3000;

                    Target = null;

                    Broadcast(new S.ObjectHide { ObjectID = ObjectID });

                    SetHP(Stats[Stat.HP]);
                }
            }

            base.ProcessAI();
        }

        public override void Turn(MirDirection dir)
        {
            if (!Visible || Target == null || Target.Dead || Target.CurrentMap != CurrentMap)
                return;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
        }

        protected override void ProcessTarget()
        {
            if (!Visible || Dead || Target == null || !CanAttack)
                return;

            if (Target.Dead || Target.CurrentMap != CurrentMap || !Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            if (!InAttackRange())
            {
                Target = null;
                return;
            }

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            Attack();
        }

        protected override bool InAttackRange()
        {
            return Target != null && !Target.Dead && Target.CurrentMap == CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
        }

        protected override void Attack()
        {
            if (!Visible || Dead || Target == null)
                return;

            if (Target.Dead || Target.CurrentMap != CurrentMap || !Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            if (!CanAttack)
                return;

            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            ActionTime = Envir.Time + 600;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged)
            {
                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            if (Envir.Random.Next(100) < 80 && HalfmoonAttack(damage, 1200, DefenceType.AC)) return;

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            if (Envir.Random.Next(100) < 80 && HalfmoonAttack(damage, 1200, DefenceType.ACAgility))
                            {
                                PoisonTarget(Target, 5, 5, PoisonType.Frozen, 1000, true, false);
                                return;
                            }

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            else
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage <= 0) return;

                            int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 60 + 1200;
                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + delay, Target, damage, DefenceType.MACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage <= 0) return;

                            TargetAreaAttack(Spell.Mon637BAreaBall, damage, 3, 500, Settings.Second * 3, 1500, DefenceType.MAC);
                                int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 60 + 1200;
                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + delay, Target, damage, DefenceType.MACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            if (Envir.Random.Next(100) < 30)
                            {
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                if (damage == 0) return;

                                if (Envir.Random.Next(100) < 80 && SinglePullAttack(damage, DefenceType.AC, 1200, 7, false))

                                    if (Envir.Random.Next(100) < 80 && SpawnSlaves(Mon637BMob, 3, true))
                                    {
                                        PoisonTarget(Target, 1, 5, PoisonType.Frozen, 1000, true, false);
                                        return;
                                    }
                            }
                        }
                        break;
                }
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
