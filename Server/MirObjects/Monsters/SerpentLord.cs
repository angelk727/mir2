using System;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class SerpentLord : MonsterObject
    {
        protected internal SerpentLord(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            if (Target.CurrentLocation == CurrentLocation) return false;

            int x = Math.Abs(Target.CurrentLocation.X - CurrentLocation.X);
            int y = Math.Abs(Target.CurrentLocation.Y - CurrentLocation.Y);

            if (x > 2 || y > 2) return false;


            return (x <= 1 && y <= 1) || (x == y || x % 2 == y % 2);
        }

        protected override void Attack()
        {

            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            if (damage == 0) return;

            switch (Envir.Random.Next(4))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            HalfmoonAttack(damage);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage * 2, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            ThreeQuarterMoonAttack(damage);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage * 3, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                            Point location;

                            for (int i = 0; i < 1; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);
                                if (!CurrentMap.ValidPoint(location)) return;
                            }

                            for (int i = 0; i < 1; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);

                                CurrentMap.GetCell(CurrentLocation).Remove(this);
                                RemoveObjects(jumpDir, 1);
                                CurrentLocation = location;
                                CurrentMap.GetCell(CurrentLocation).Add(this);
                                AddObjects(jumpDir, 1);

                                if (damage > 0)

                                    LineAttack(damage, 3);

                                {
                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                                    CurrentMap.ActionList.Add(action);
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                            Point location;

                            for (int i = 0; i < 1; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);
                                if (!CurrentMap.ValidPoint(location)) return;
                            }

                            for (int i = 0; i < 1; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);

                                CurrentMap.GetCell(CurrentLocation).Remove(this);
                                RemoveObjects(jumpDir, 1);
                                CurrentLocation = location;
                                CurrentMap.GetCell(CurrentLocation).Add(this);
                                AddObjects(jumpDir, 1);

                                if (damage > 0)

                                    LineAttack(damage, 3);

                                {
                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 2400, Target, damage * 2, DefenceType.AC);
                                    CurrentMap.ActionList.Add(action);
                                }
                            }
                            break;
                        }
                }

            ActionTime = Envir.Time + 1200;
            AttackTime = Envir.Time + AttackSpeed;
            ShockTime = 0;
        }
    }
}