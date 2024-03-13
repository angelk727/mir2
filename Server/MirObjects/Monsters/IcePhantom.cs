using System.Collections.Generic;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class IcePhantom : MonsterObject
    {
        protected virtual byte AttackRange
        {
            get
            {
                return 5;
            }
        }

        protected internal IcePhantom(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged)
            {

                switch (Envir.Random.Next(4))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 350, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 350, Target, damage * 2, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;
                            HalfmoonAttack(damage);
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 350, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 3:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;
                            HalfmoonAttack(damage);
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 350, Target, damage * 2, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            else
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });

                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                if (damage == 0) return;

                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MAC);
                ActionList.Add(action);
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
            ActionList.Add(new DelayedAction(DelayedType.Die, Envir.Time + 2000));
            base.Die();
        }

        protected override void CompleteDeath(IList<object> data)
        {
            List<MapObject> targets = FindAllTargets(1, CurrentLocation, false);
            if (targets.Count == 0) return;

            for (int i = 0; i < targets.Count; i++)
            {
                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                if (targets[i].Attacked(this, damage, DefenceType.ACAgility) <= 0) return;

                PoisonTarget(Target, 9, 5, PoisonType.Frozen, 1000);
            }
        }
    }
}
