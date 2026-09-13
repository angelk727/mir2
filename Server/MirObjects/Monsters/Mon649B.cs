using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon649B : MonsterObject
    {
        protected internal Mon649B(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target == null || Target.CurrentMap != CurrentMap) return false;

            return Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) <= 3;
        }

        protected override void Attack()
        {
            if (Target == null || !Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            int distance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (distance > 5) return;

            ShockTime = 0;
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);

            if (damage <= 0) return;

            if (distance <= 1)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;

                    case 1:
                        {
                            HalfmoonAttack(damage, 600, DefenceType.ACAgility);
                        }

                        break;
                }

                return;
            }

            if (distance >= 2 && distance <= 3)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            TriangleAttack(damage, 3, 1, 600, DefenceType.ACAgility, false);
                        }
                        break;

                    case 1:
                        {
                            WideLineAttack(damage, 3, 600, DefenceType.AC, false, 3);
                        }
                        break;

                    case 2:
                        {
                            WideLineAttack(damage, 5, 600, DefenceType.ACAgility, false, 3);
                        }
                        break;
                }
            }
            if (distance >= 2 && distance <= 4)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            TriangleAttack(damage, 3, 1, 600, DefenceType.ACAgility, false);
                        }
                        break;

                    case 1:
                        {
                            WideLineAttack(damage, 3, 600, DefenceType.AC, false, 3);
                        }
                        break;

                    case 2:
                        {
                            WideLineAttack(damage, 5, 600, DefenceType.ACAgility, false, 3);
                        }
                        break;
                }
            }
            if (distance >= 2 && distance <= 5)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            TriangleAttack(damage, 3, 1, 600, DefenceType.ACAgility, false);
                        }
                        break;

                    case 1:
                        {
                            WideLineAttack(damage, 3, 600, DefenceType.AC, false, 3);
                        }
                        break;

                    case 2:
                        {
                            WideLineAttack(damage, 5, 600, DefenceType.ACAgility, false, 3);
                        }
                        break;
                }
            }
        }
    }
}