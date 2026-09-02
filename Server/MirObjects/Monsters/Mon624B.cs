using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon624B : MonsterObject
    {
        protected internal Mon624B(MonsterInfo info)
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
            bool ranged1 = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
            bool ranged2 = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 2);

            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged1 && !ranged2)
            {
                switch (Envir.Random.Next(5))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            if (Envir.Random.Next(100) < 20) WideLineAttack(damage, 1, 1200, DefenceType.ACAgility, false, 3);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int maxDC = Stats[Stat.MaxDC];
                            if (Envir.Random.Next(100) < 20) maxDC *= 2;
                            int damage = GetAttackPower(Stats[Stat.MinDC], maxDC);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                            break;
                        }
                    case 3:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (Envir.Random.Next(100) < 20) damage *= 2;
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                            break;
                        }
                    case 4:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            WideLineAttack(damage, 5, 1200, DefenceType.ACAgility, false, 3);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            if (ranged1)
            {
                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            WideLineAttack(damage, 5, 1200, DefenceType.ACAgility, false, 3);
                            PoisonTarget(Target, 7, 7, PoisonType.Green, 1000, true, false);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            WideLineAttack(damage, 5, 1200, DefenceType.ACAgility, false, 3);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            if (ranged2)
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            WideLineAttack(damage, 5, 1200, DefenceType.ACAgility, false, 3);
                            PoisonTarget(Target, 5, 7, PoisonType.Green, 1000, true, false);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            WideLineAttack(damage, 5, 1200, DefenceType.ACAgility, false, 3);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.MAC, false);
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
            MoveTo(Target.Front);
        }

        public override void Die()
        {
            base.Die();
        }
    }
}
