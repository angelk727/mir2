using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon603B : MonsterObject
    {
        protected internal Mon603B(MonsterInfo info)
            : base(info)
        {
        }
        protected override bool InAttackRange()
        {
            if (Target == null || Target.CurrentMap != CurrentMap) return false;

            return Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
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

            WhirlPool();

            if (HealthPercent <= 80 && SlaveList.Count < 3)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                SpawnSlaves();
            }

            else if (!ranged1)
                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            PoisonTarget(Target, 5, 8, PoisonType.Green, 2000);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.Agility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            LineAttack(damage, 3, 300, DefenceType.AC);
                        }
                        break;
                }
            else if (!ranged2)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                if (damage == 0) return;

                LineAttack(damage, 3, 300, DefenceType.ACAgility);
            }
        }
        private void WhirlPool()
        {
            const int radius = 3;

            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);

            if (damage <= 0)
                return;

            Point center = CurrentLocation;

            for (int y = -radius; y <= radius; y++)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    Point location = new Point(center.X + x, center.Y + y);

                    if (!CurrentMap.ValidPoint(location))
                        continue;

                    var cell = CurrentMap.GetCell(location);

                    if (!cell.Valid || cell.Objects == null)
                        continue;

                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject target = cell.Objects[i];

                        if (!target.IsAttackTarget(this))
                            continue;

                        target.Attacked(this, damage, DefenceType.MAC);
                        break;
                    }
                }
            }
        }
        private void SpawnSlaves()
        {
            const int maxSpawnCount = 3;
            const int spawnRange = 4;

            int spawnCount = maxSpawnCount - SlaveList.Count;

            if (spawnCount <= 0)
                return;

            int minX = Math.Max(0, CurrentLocation.X - spawnRange);
            int maxX = Math.Min(CurrentMap.Width - 1, CurrentLocation.X + spawnRange);
            int minY = Math.Max(0, CurrentLocation.Y - spawnRange);
            int maxY = Math.Min(CurrentMap.Height - 1, CurrentLocation.Y + spawnRange);

            List<Point> validLocations = new List<Point>();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (x == CurrentLocation.X && y == CurrentLocation.Y)
                        continue;

                    var cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid)
                        continue;

                    validLocations.Add(new Point(x, y));
                }
            }

            int actualSpawnCount = Math.Min(spawnCount, validLocations.Count);

            for (int i = 0; i < actualSpawnCount; i++)
            {
                int index = Envir.Random.Next(validLocations.Count);
                Point location = validLocations[index];

                validLocations.RemoveAt(index);

                MonsterObject mob = GetMonster(Envir.GetMonsterInfo(Settings.Mon603BMob));

                if (mob == null)
                    continue;

                mob.Spawn(CurrentMap, location);
                mob.ActionTime = Envir.Time + 2000;
                SlaveList.Add(mob);
            }
        }
        protected override void CompleteRangeAttack(IList<object> data)
        {
            Point location = (Point)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (CurrentMap == null || !CurrentMap.ValidPoint(location))
                return;

            var cell = CurrentMap.GetCell(location);

            if (cell.Objects == null)
                return;

            for (int o = 0; o < cell.Objects.Count; o++)
            {
                MapObject ob = cell.Objects[o];

                if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster)
                    continue;

                if (!ob.IsAttackTarget(this))
                    continue;

                ob.Attacked(this, damage, defence);
                break;
            }
        }
        protected override void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            bool aoe = data.Count >= 4 && (bool)data[3];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            if (aoe)
            {
                var targets = FindAllTargets(2, CurrentLocation, false);

                for (int i = 0; i < targets.Count; i++)
                {
                    MapObject targetObject = targets[i];

                    if (targetObject == null || targetObject.CurrentMap != CurrentMap || targetObject.Node == null)
                        continue;

                    if (!targetObject.IsAttackTarget(this))
                        continue;

                    targetObject.Attacked(this, damage, defence);
                }
            }
            else
            {
                target.Attacked(this, damage, defence);
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
            for (int i = SlaveList.Count - 1; i >= 0; i--)
            {
                SlaveList[i].Die();
            }

            SlaveList.Clear();
            base.Die();
        }
    }
}
