using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon593N : MonsterObject
    {
        public long ReloadTime;
        protected override bool CanMove { get { return false; } }

        protected internal Mon593N(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, 3);
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
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            if (damage == 0) return;

            FullmoonAttack(damage, 600, DefenceType.MACAgility, -1, 3);
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (InAttackRange() && Envir.Time < ReloadTime)
            {
                Attack();
                return;
            }

            ReloadTime = Envir.Time + 5000;

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }
        }
    }
}
