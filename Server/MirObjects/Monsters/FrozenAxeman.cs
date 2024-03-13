using System;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class FrozenAxeman : MonsterObject
    {
        public long PushTime;
        protected internal FrozenAxeman(MonsterInfo info)
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

            bool range = !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            if (!range && Envir.Random.Next(5) > 0)
            {
                if (Envir.Time >= PushTime)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });
                    PushTime = Envir.Time + 30000;
                    AttackTime = Envir.Time + AttackSpeed;
                    ActionTime = Envir.Time + 300;

                    Target.Pushed(this, Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation), 2 + Envir.Random.Next(3));
                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC);
                    ActionList.Add(action);

                }
                else
                {
                    base.Attack();
                }
            }
            else
                switch (Envir.Random.Next(7))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            LineAttack(damage, 2);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                            LineAttack(damage, 2);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });
                            HalfmoonAttack(damage);
                            PoisonTarget(Target, 8, 3, PoisonType.Dazed, 800);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 3:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });
                            HalfmoonAttack(damage);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage * 2, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 4:
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

                                    LineAttack(damage, 2);

                                {
                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage * 2, DefenceType.AC);
                                    CurrentMap.ActionList.Add(action);
                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                            Point location;

                            for (int i = 0; i < 1; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);
                                if (!CurrentMap.ValidPoint(location)) return;
                            }

                            for (int i = 0; i < 2; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);

                                CurrentMap.GetCell(CurrentLocation).Remove(this);
                                RemoveObjects(jumpDir, 1);
                                CurrentLocation = location;
                                CurrentMap.GetCell(CurrentLocation).Add(this);
                                AddObjects(jumpDir, 1);

                                if (damage > 0)

                                    LineAttack(damage, 2);

                                {
                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 2400, Target, damage * 3, DefenceType.AC);
                                    CurrentMap.ActionList.Add(action);
                                }
                            }
                            break;
                        }
                    case 6:
                        if (HealthPercent < 100)
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 3 });
                            CallForHelpAll(16);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 3000, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                }

            ActionTime = Envir.Time + 1200;
            AttackTime = Envir.Time + AttackSpeed;
            ShockTime = 0;
        }

        public void CallForHelpAll(int dist)
        {
            for (int y = CurrentLocation.Y - dist; y <= CurrentLocation.Y + dist; y++)
            {
                if (y < 0) continue;
                if (y >= CurrentMap.Height) break;

                for (int x = CurrentLocation.X - dist; x <= CurrentLocation.X + dist; x++)
                {
                    if (x < 0) continue;
                    if (x >= CurrentMap.Width) break;

                    Cell cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid || cell.Objects == null) continue;

                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject target = cell.Objects[i] as MonsterObject;
                        if (target == null) continue;
                        target.Target = Target;
                    }
                }
            }

        }
    }
}