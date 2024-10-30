using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    class Mon472N : MonsterObject
    {
        protected internal Mon472N(MonsterInfo info) : base(info) { }

        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && CanFly(Target.CurrentLocation) && Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
        }

        private List<MapObject> GetNearbyTargets()
        {
            List<MapObject> nearbyTargets = new List<MapObject>();

            int x = CurrentLocation.X;
            int y = CurrentLocation.Y;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    var cell = CurrentMap.GetCell(x + dx, y + dy);
                    if (cell?.Objects == null) continue;

                    foreach (var obj in cell.Objects)
                    {
                        if (obj == this) continue;

                        if (obj is MonsterObject monsterObj && monsterObj.IsAttackTarget(this))
                        {
                            nearbyTargets.Add(monsterObj);
                        }
                        else if ((obj.Race == ObjectType.Player ||
                                  obj.Race == ObjectType.Hero ||
                                  obj.Race == ObjectType.Monster) &&
                                  obj.IsAttackTarget(this))
                        {
                            nearbyTargets.Add(obj);
                        }
                    }
                }
            }
            return nearbyTargets;
        }
        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            int targetDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            var nearbyTargets = GetNearbyTargets();
            bool multipleTargetsNearby = nearbyTargets.Count > 1;

            if (targetDistance == 1)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                if (damage > 0)
                {
                    ActionList.Add(new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC));
                }
            }
            else if (multipleTargetsNearby)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                if (damage > 0)
                {
                    FullmoonAttack(damage, 600, DefenceType.ACAgility, -1, 2);
                }
            }
            else if (targetDistance > 1)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                AttackTime = Envir.Time + AttackSpeed + 500;
                if (damage > 0)
                {
                    PoisonTarget(Target, 5, 5, PoisonType.Frozen, 1000);
                    int delay = targetDistance * 50 + 500;
                    ActionList.Add(new DelayedAction(DelayedType.Damage, Envir.Time + delay, Target, damage, DefenceType.MAC));
                }
            }
        }
    }
}
