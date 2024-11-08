using Server.MirDatabase;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon579B : MonsterObject
    {
        private long _mon579BShieldTime;
        Point _location;

        protected internal Mon579B(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            return CurrentMap == Target.CurrentMap &&
                   Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
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
            int nearbyTargetsCount = FindAllTargets(dist: 1, location: CurrentLocation, needSight: true).Count;

            if (HealthPercent < 80)
            {
                Mon579BShield();
            }

            if (!ranged)
            {
                switch (nearbyTargetsCount)
                {
                    case <= 1:
                    {
                        DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;

                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        DelayedAction action = new(DelayedType.Damage, Envir.Time + 600, Target, damage, defenceType, false);
                        ActionList.Add(action);
                        break;
                    }
                    case >= 2:
                    {
                        DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;
                        int pushDistance = Envir.Random.Next(2) == 0 ? -1 : 1;

                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        FullmoonAttack(damage, 800, defenceType, pushDistance, 2);
                        DelayedAction action = new(DelayedType.Damage, Envir.Time + 800, Target, damage, defenceType, false);
                        ActionList.Add(action);
                        break;
                    }
                }
            }
            else
            {
                if (Envir.Random.Next(8) != 7)
                {
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;

                    PoisonTarget(target: Target, chanceToPoison: 7, poisonDuration: 5, poison: PoisonType.Slow, poisonTickSpeed: 1000);
                    DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 800, Target, damage, DefenceType.MACAgility, false);
                    ActionList.Add(action);
                }
                else
                {
                    Mon579BStab();
                }
            }
        }

        private void Mon579BStab()
        {
            int jumpDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            for (int i = 0; i < jumpDistance; i++)
            {
                _location = Functions.PointMove(CurrentLocation, jumpDir, 1);

                if (!CurrentMap.ValidPoint(_location)) return;

                CurrentMap.GetCell(CurrentLocation).Remove(this);
                RemoveObjects(jumpDir, 1);

                CurrentLocation = _location;

                CurrentMap.GetCell(CurrentLocation).Add(this);
                AddObjects(jumpDir, 1);

                int damage = Stats[Stat.MaxDC];
                if (damage == 0) return;
                {
                    FullmoonAttack(damage, 800, DefenceType.ACAgility, -1, 4);
                    int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500;
                    DelayedAction action = new(DelayedType.Damage, Envir.Time + delay, _location, damage, DefenceType.AC);
                    CurrentMap.ActionList.Add(action);
                }
            }
            Broadcast(new S.ObjectDashAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Distance = 1 });
        }

        private void Mon579BShield()
        {
            if (Envir.Time < _mon579BShieldTime + 90 * 1000) return;

            var stats = new Stats();
            int shieldTime;
            switch (HealthPercent)
            {
                case > 60 and <= 80:
                    stats[Stat.MaxAC] = 30;
                    stats[Stat.MinAC] = 30;
                    shieldTime = 30000;
                    break;
                case > 30 and <= 50:
                    stats[Stat.MaxAC] = 60;
                    stats[Stat.MinAC] = 60;
                    shieldTime = 35000;
                    break;
                case >= 20 and <= 30:
                    stats[Stat.MaxAC] = 90;
                    stats[Stat.MinAC] = 90;
                    shieldTime = 40000;
                    break;
                default:
                    return;
            }

            if (Target == null) return;

            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
            AddBuff(BuffType.Mon579BShield, this, shieldTime, stats);
            _mon579BShieldTime = Envir.Time;
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
    }
}
