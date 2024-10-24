using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon601N : MonsterObject
    {
        public long FearTime;

        protected virtual byte AttackRange
        {
            get
            {
                return Info.ViewRange;
            }
        }

        protected internal Mon601N(MonsterInfo info)
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

            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            int distance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (distance > 2)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                ProjectileAttack(damage + (damage / 2), DefenceType.AC, 500);
            }
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                WideLineAttack(damage, 3, 500, DefenceType.ACAgility, false, 3);
            }

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed; 
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (InAttackRange() && Envir.Time < FearTime)
            {
                Attack();
                return;
            }

            FearTime = Envir.Time + 5000;

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (dist >= AttackRange)
                MoveTo(Target.CurrentLocation);
            else
            {
                MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                if (Walk(dir)) return;

                switch (Envir.Random.Next(2))
                {
                    case 0:
                        for (int i = 0; i < 3; i++)
                        {
                            dir = (MirDirection)Envir.Random.Next(8);
                            if (Walk(dir)) return;
                        }
                        break;
                    case 1:
                        for (int i = 0; i < 3; i++)
                        {
                            dir = (MirDirection)Envir.Random.Next(8);
                            if (Walk(dir)) return;
                        }
                        break;
                }
            }
        }
    }
}
