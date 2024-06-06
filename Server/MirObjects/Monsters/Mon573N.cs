using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon573N : MonsterObject
    {
        public long _BigCobwebTime;
        public long _Mon573NBuffTime;

        protected internal Mon573N(MonsterInfo info)
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

                                Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.Mon573NCobweb });
                                PoisonTarget(targets[i], 5, 5, PoisonType.Slow, 1000);
                            }
                        }
                        break;
                    case 4:
                        if (Envir.Time >= _Mon573NBuffTime)
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            if (Dead) return;

                            if (Envir.Time >= _Mon573NBuffTime)
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
                            _Mon573NBuffTime = Envir.Time + 30000;
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
                        if (HealthPercent <= 80)
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
                        Spell = Spell.Mon573NBigCobweb,
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
            int maxSpawnCount = 3;
            int currentSpawnCount = SlaveList.Count;
            int spawnCount = Math.Min(maxSpawnCount - currentSpawnCount, maxSpawnCount);

            if (spawnCount <= 0) return;

            Random rand = new Random();
            List<Point> validLocations = new List<Point>();

            int minX = Math.Max(0, CurrentLocation.X - 5);
            int maxX = Math.Min(CurrentMap.Width - 1, CurrentLocation.X + 5);
            int minY = Math.Max(0, CurrentLocation.Y - 5);
            int maxY = Math.Min(CurrentMap.Height - 1, CurrentLocation.Y + 5);

            while (validLocations.Count < spawnCount)
            {
                int x = rand.Next(minX, maxX + 1);
                int y = rand.Next(minY, maxY + 1);
                Point newLocation = new Point(x, y);

                if (CurrentMap.GetCell(x, y).Valid && (x != CurrentLocation.X || y != CurrentLocation.Y) &&
                    !validLocations.Any(loc => loc.X == x && loc.Y == y))
                {
                    validLocations.Add(newLocation);
                }
            }

            foreach (var location in validLocations)
            {
                MonsterObject mob = GetMonster(Envir.GetMonsterInfo(Settings.Mon573NMob));
                if (mob == null) continue;

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
