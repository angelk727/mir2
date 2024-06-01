using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon577N : MonsterObject
    {
        protected internal Mon577N(MonsterInfo info)
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
                return;

            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
            bool ranged2 = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 2);

            ActionTime = Envir.Time + 300;
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

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            if (!ranged2)
            {
                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            WideLineAttack(damage, 5, 500, DefenceType.ACAgility, false, 3);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                            if (damage == 0) return;

                            WideLineAttack(damage, 5, 500, DefenceType.ACAgility, false, 3);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            else
            {
                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 300, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                }
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

            else
            {
                MoveTo(Envir.Random.Next(2) == 0 ? Target.Front : Target.Back);
            }
        }

        public override void Die()
        {
            base.Die();
        }
    }
}
