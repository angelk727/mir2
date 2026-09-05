using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class CannibalTentacles : MonsterObject
    {
        protected internal CannibalTentacles(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target == null || CurrentMap == null || Target.CurrentMap != CurrentMap)
                return false;

            return Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
        }

        protected override void Attack()
        {
            if (Target == null || CurrentMap == null || !Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            bool ranged = CurrentLocation == Target.CurrentLocation ||
                          !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged)
            {
                if (Envir.Random.Next(5) > 0)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage <= 0)
                        return;

                    ActionList.Add(new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC, false));
                }
                else
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                    HalfmoonAttack(500);
                }
            }
            else
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                if (damage <= 0)
                    return;

                int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500;

                ActionList.Add(new DelayedAction(DelayedType.RangeDamage, Envir.Time + delay, Target, damage, DefenceType.MACAgility, false));
            }
        }

        protected override void CompleteAttack(IList<object> data)
        {
            if (data == null || data.Count < 4)
                return;

            MapObject target = data[0] as MapObject;
            if (target == null || target.CurrentMap != CurrentMap || target.Node == null || !target.IsAttackTarget(this))
                return;

            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            bool poison = (bool)data[3];

            if (target.Attacked(this, damage, defence) <= 0)
                return;

            if (poison)
                PoisonTarget(target, 1, 5, PoisonType.Green, 1000);
        }

        protected override void ProcessTarget()
        {
            if (Target == null || CurrentMap == null)
                return;

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