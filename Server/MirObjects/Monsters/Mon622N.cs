using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon622N : MonsterObject
    {
        protected internal Mon622N(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange); ;
        }


        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;
            ActionTime = Envir.Time + 600;
            AttackTime = Envir.Time + AttackSpeed;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            if (Envir.Random.Next(7) == 0)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                if (damage == 0) return;

                DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 300, Target, damage, DefenceType.MAC, true);
                ActionList.Add(action);
            }
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                Broadcast(new S.ObjectEffect { ObjectID = Target.ObjectID, Effect = SpellEffect.Mon622NSpikes });
                DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                ActionList.Add(action);
            }
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (Envir.Time < ShockTime) return;

            int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            const int MinRange = 3;
            const int MaxRange = 6;

            if (dist > MaxRange)
            {
                MoveTo(Target.CurrentLocation);
                return;
            }

            if (dist >= MinRange && dist <= MaxRange)
            {
                if (InAttackRange())
                    Attack(); return;
            }
            MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

            if (Walk(dir)) return;

            bool clockwise = Envir.Random.Next(2) == 0;

            for (int i = 0; i < 7; i++)
            {
                dir = clockwise ? Functions.NextDir(dir) : Functions.PreviousDir(dir);
                if (Walk(dir)) return;
            }
        }
    }
}
