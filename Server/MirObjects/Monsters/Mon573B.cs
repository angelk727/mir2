using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon573B : MonsterObject
    {
        public long _BigCobwebTime;
        public long _Mon573BBuffTime;

        protected internal Mon573B(MonsterInfo info)
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 2);

            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged && Envir.Random.Next(2) > 0)
            {
                if (Envir.Random.Next(4) > 0)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;
                    TriangleAttack(damage, 3, 1, 800);

                    DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                    ActionList.Add(action);
                }
                else
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                    if (damage == 0) return;
                    TriangleAttack(damage, 3, 2, 500, DefenceType.ACAgility, false);

                    DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                    ActionList.Add(action);
                }

            }
            else
                switch (Envir.Random.Next(7))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                            AttackTime = Envir.Time + AttackSpeed + 500;
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MAC, false);
                            ActionList.Add(action);

                        }
                        break;
                    case 1:
                        if (Envir.Time >= _BigCobwebTime)
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC] * 2);
                            if (damage == 0) return;

                            BigCobweb();
                            _BigCobwebTime = Envir.Time + 10000;

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
                            if (targets.Count == 0) return;

                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                            for (int i = 0; i < targets.Count; i++)
                            {
                                Target = targets[i];
                                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                                if (damage == 0) return;

                                DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, false);
                                ActionList.Add(action);

                                Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.Mon573BCobweb });
                                PoisonTarget(targets[i], 5, 5, PoisonType.Slow, 1000);
                            }
                        }
                        break;
                    case 4:
                        if (Envir.Time >= _Mon573BBuffTime)
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            if (Dead) return;

                            if (Envir.Time >= _Mon573BBuffTime)
                            {
                                var maxAC = 35;
                                var maxMAC = 35;

                                var stats = new Stats
                                {
                                    [Stat.MaxAC] = maxAC * -1,
                                    [Stat.MaxMAC] = maxMAC * -1
                                };
                                Target.AddBuff(BuffType.防御诅咒, this, Settings.Second * 10, stats);
                            }
                            _Mon573BBuffTime = Envir.Time + 30000;
                        }
                        break;
                    case 5:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            WakeAll(11);
                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 6:
                        if (HealthPercent <= 80 && SlaveList.Count < 3)
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                            SpawnSlaves();
                        }
                        break;
                }
        }
        private void BigCobweb()
        {
            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
            int count = targets.Count;

            if (count == 0) return;

            MapObject target = targets[Envir.Random.Next(count)];
            Point location = target.CurrentLocation;

            for (int y = location.Y - 2; y <= location.Y + 2; y++)
            {
                if (y < 0 || y >= CurrentMap.Height) continue;

                for (int x = location.X - 2; x <= location.X + 2; x++)
                {
                    if (x < 0 || x >= CurrentMap.Width) continue;

                    if (x == CurrentLocation.X && y == CurrentLocation.Y) continue;

                    var cell = CurrentMap.GetCell(x, y);
                    if (!cell.Valid) continue;

                    int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                    var start = 500;
                    var time = Settings.Second * 15;

                    SpellObject ob = new()
                    {
                        Spell = Spell.Mon573BBigCobweb,
                        Value = damage,
                        ExpireTime = Envir.Time + time + start,
                        TickSpeed = 3000,
                        CurrentLocation = new Point(x, y),
                        CastLocation = location,
                        Show = location.X == x && location.Y == y,
                        CurrentMap = CurrentMap,
                        Caster = this
                    };

                    PoisonTarget(Target, 0, 3, PoisonType.Slow, 1000);
                    DelayedAction action = new(DelayedType.Spawn, Envir.Time + start, ob);
                    CurrentMap.ActionList.Add(action);
                }
            }
        }

        private void SpawnSlaves()
        {
            const int maxSpawnCount = 3;
            const int spawnRange = 4;

            if (CurrentMap == null)
                return;

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

                    Cell cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid)
                        continue;

                    validLocations.Add(new Point(x, y));
                }
            }

            if (validLocations.Count == 0)
                return;

            int actualSpawnCount = Math.Min(spawnCount, validLocations.Count);

            for (int i = 0; i < actualSpawnCount; i++)
            {
                int index = Envir.Random.Next(validLocations.Count);
                Point location = validLocations[index];

                validLocations[index] = validLocations[validLocations.Count - 1];
                validLocations.RemoveAt(validLocations.Count - 1);

                MonsterInfo info = Envir.GetMonsterInfo(Settings.Mon573BMob);

                if (info == null)
                    continue;

                MonsterObject mob = GetMonster(info);

                if (mob == null)
                    continue;

                mob.Spawn(CurrentMap, location);
                mob.ActionTime = Envir.Time + 2000;
                SlaveList.Add(mob);
            }
        }

        public void WakeAll(int dist)
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
                        Mon575S target = cell.Objects[i] as Mon575S;
                        if (target == null || !target.Stoned) continue;
                        target.Wake();
                        target.Target = Target;
                    }
                }
            }

        }
        public override void Die()
        {
            foreach (var slave in SlaveList)
            {
                slave.Die();
            }

            base.Die();
        }
    }
}
