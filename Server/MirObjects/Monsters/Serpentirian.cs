using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Serpentirian : MonsterObject
    {
        protected virtual byte AttackRange => Info.ViewRange;

        protected internal Serpentirian(MonsterInfo info)
            : base(info)
        {
        }

        private bool IsValidTarget(MapObject target)
        {
            return target != null && target.IsAttackTarget(this) && target.CurrentMap == CurrentMap && target.Node != null;
        }

        private void MoveToTarget(Point target)
        {
            if (Functions.InRange(CurrentLocation, target, AttackRange))
            {
                if (CanAttack)
                {
                    Attack();
                }
                return;
            }

            MoveTo(target);
        }

        protected override void ProcessTarget()
        {
            if (CurrentMap.Players.Count == 0 || Target == null)
            {
                return;
            }

            Point targetLocation = Target.CurrentLocation;

            if (Envir.Time >= ShockTime)
            {
                MoveToTarget(targetLocation);
            }
            else
            {
                Target = null;
            }
        }

        public override void Die()
        {
            base.Die();
        }

        protected override bool InAttackRange()
        {
            return IsValidTarget(Target) && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1)
                || (Functions.InRange(CurrentLocation, Target.CurrentLocation, 1) && Envir.Random.Next(10) < 3);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (ranged)
            {
                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            PoisonTarget(Target, 5, 5, PoisonType.Green, 1000);

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 1200, Target, damage, DefenceType.MAC);
                            ActionList.Add(action);
                        }
                        break;

                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            PoisonTarget(Target, 5, 7, PoisonType.Slow, 1000);

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 1200, Target, damage, DefenceType.MAC);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                FullmoonAttack(damage);
                PoisonTarget(Target, 7, 3, PoisonType.Green, 1000);

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage, DefenceType.ACAgility);
                ActionList.Add(action);
            }
        }
    }
}


