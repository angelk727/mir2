using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon590N : MonsterObject
    {
        protected internal Mon590N(MonsterInfo info)
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

            if (!CanAttack)
            {
                return;
            }

            ShockTime = 0;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            int distance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;

            if (distance > 0 && distance < 2 && IsAligned(CurrentLocation, Target.CurrentLocation))
            {
                PerformAttack(0, 1);
            }
            else if (distance > 1 && distance <= 3 && IsAligned(CurrentLocation, Target.CurrentLocation))
            {
                PerformAttack(1, 3);
            }
            else if (distance > 3 && distance <= 5 && IsAligned(CurrentLocation, Target.CurrentLocation))
            {
                PerformAttack(2, 5);
            }
            else if (distance > 5 && distance <= 7 && IsAligned(CurrentLocation, Target.CurrentLocation))
            {
                PerformAttack(3, 7);
            }
        }

        private void PerformAttack(int attackType, int range)
        {
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = (byte)attackType });

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            if (damage == 0) return;

            LineAttack(damage, range, 300, DefenceType.ACAgility, false);
            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
            ActionList.Add(action);
        }

        private static bool IsAligned(Point a, Point b)
        {
            return a.X == b.X || a.Y == b.Y;
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
            MoveTo(Target.Front);
        }

        public override void Die()
        {
            base.Die();
        }
    }
}
