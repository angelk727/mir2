using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon579B : MonsterObject
    {
        private long Mon579BShieldTime = 0;

        protected internal Mon579B(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
        }

        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
            int nearbyTargetsCount = FindAllTargets(1, CurrentLocation, true).Count;

            if (HealthPercent < 80)
            {
                Mon579BShield();
            }

            if (!ranged)
            {
                if (nearbyTargetsCount <= 1)
                {
                    DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;

                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage, defenceType, false);
                    ActionList.Add(action);
                }
                else if (nearbyTargetsCount >= 2)
                {
                    DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;
                    int pushDistance = Envir.Random.Next(2) == 0 ? -1 : 1;

                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;

                    FullmoonAttack(damage, 800, defenceType, pushDistance, 2);
                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 800, Target, damage, defenceType, false);
                    ActionList.Add(action);
                }
            }
            else
            {
                if (Envir.Random.Next(8) != 7)
                {
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;

                    PoisonTarget(Target, 7, 5, PoisonType.Slow, 1000);
                    DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 800, Target, damage, DefenceType.MACAgility, false);
                    ActionList.Add(action);
                }
                else
                {
                    Mon579BStab();
                }
            }
        }

        public void Mon579BStab()
        {
            int jumpDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Point location;

            for (int i = 0; i < jumpDistance; i++)
            {
                location = Functions.PointMove(CurrentLocation, jumpDir, 1);

                if (!CurrentMap.ValidPoint(location)) return;

                CurrentMap.GetCell(CurrentLocation).Remove(this);
                RemoveObjects(jumpDir, 1);

                CurrentLocation = location;

                CurrentMap.GetCell(CurrentLocation).Add(this);
                AddObjects(jumpDir, 1);

                int damage = Stats[Stat.MaxDC];
                if (damage == 0) return;
                {
                    FullmoonAttack(damage, 800, DefenceType.ACAgility, -1, 4);
                    int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500;
                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + delay, location, damage, DefenceType.AC);
                    CurrentMap.ActionList.Add(action);
                }
            }
            Broadcast(new S.ObjectDashAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Distance = 1 });
        }

        public void Mon579BShield()
        {
            if (Envir.Time < Mon579BShieldTime + 90 * 1000) return;

            var stats = new Stats();
            int shieldTime;
            if (HealthPercent > 60 && HealthPercent < 80)
            {
                stats[Stat.MaxAC] = 30;
                stats[Stat.MinAC] = 30;
                shieldTime = 30000;
            }
            else if (HealthPercent > 30 && HealthPercent < 50)
            {
                stats[Stat.MaxAC] = 60;
                stats[Stat.MinAC] = 60;
                shieldTime = 35000;
            }
            else if (HealthPercent < 20)
            {
                stats[Stat.MaxAC] = 90;
                stats[Stat.MinAC] = 90;
                shieldTime = 40000;
            }
            else
            {
                return;
            }

            if (Target != null)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                AddBuff(BuffType.Mon579BShield, this, shieldTime, stats);
                Mon579BShieldTime = Envir.Time;
            }
        }

        protected override void ProcessTarget()
        {
            if (Target == null) return;

            if (InAttackRange() && CanAttack)
            {
                Attack();
                return;
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            MoveTo(Target.CurrentLocation);
        }

        public override void Die()
        {
            base.Die();
        }
    }
}
