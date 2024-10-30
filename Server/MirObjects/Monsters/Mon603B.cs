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
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC, false);
                            ActionList.Add(action);
                        }
                        break;
                }
            else if (!ranged2)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                if (damage == 0) return;

                LineAttack(damage, 3, 300, DefenceType.ACAgility);
                DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                ActionList.Add(action);
            }
        }
        private void WhirlPool()
        {
            List<SpellObject> spellObjects = CurrentMap.GetSpellObjects(Spell.Mon603BWhirlPool, caster: this);

            if (spellObjects.Count > 0) return;

            Point location = Functions.PointMove(CurrentLocation, Direction, 1);

            for (int y = location.Y - 3; y <= location.Y + 3; y++)
            {
                if (y < 0 || y >= CurrentMap.Height) continue;

                for (int x = location.X - 3; x <= location.X + 3; x++)
                {
                    if (x < 0 || x >= CurrentMap.Width) continue;

                    if (x == CurrentLocation.X && y == CurrentLocation.Y) continue;

                    var cell = CurrentMap.GetCell(x, y);
                    if (!cell.Valid) continue;

                    int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                    var start = 500;
                    var time = Settings.Second * 5;

                    SpellObject ob = new()
                    {
                        Spell = Spell.Mon603BWhirlPool,
                        Value = damage,
                        ExpireTime = Envir.Time + time + start,
                        TickSpeed = 3000,
                        CurrentLocation = new Point(x, y),
                        CastLocation = location,
                        Show = location.X == x && location.Y == y,
                        CurrentMap = CurrentMap,
                        Caster = this
                    };

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
                MonsterObject mob = GetMonster(Envir.GetMonsterInfo(Settings.Mon603BMob));
                if (mob == null) continue;

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

            var cell = CurrentMap.GetCell(location);
            if (cell.Objects == null) return;

            for (int o = 0; o < cell.Objects.Count; o++)
            {
                MapObject ob = cell.Objects[o];
                if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) continue;
                if (!ob.IsAttackTarget(this)) continue;

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
                    targets[i].Attacked(this, damage, defence);
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
            foreach (var slave in SlaveList)
            {
                slave.Die();
            }
            base.Die();
        }
    }
}
