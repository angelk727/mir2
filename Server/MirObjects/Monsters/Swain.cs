using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Swain : MonsterObject
    {
        protected virtual byte AttackRange => Info.ViewRange;

        public bool HenshinMode = false;
        public long skeletonBombTime;

        protected internal Swain(MonsterInfo info)
            : base(info)
        {
        }

        private bool IsValidTarget(MapObject target)
        {
            return target != null && target.IsAttackTarget(this) && target.CurrentMap == CurrentMap && target.Node != null;
        }

        private void MoveToTarget(Point target)
        {
            if (Functions.InRange(CurrentLocation, target, AttackRange))
            {
                if (HealthPercent <= 45 && HenshinMode == false)
                {
                    HenshinMode = true;
                    Broadcast(new S.ObjectShow { ObjectID = ObjectID });

                    ActionTime = Envir.Time + 1000;
                }

                if (CanAttack)
                {
                    Attack();
                }
                return;

            }

            MoveTo(target);
        }

        public override Packet GetInfo()
        {
            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = HenshinMode ? Monster.Swain1 : Monster.Swain,
                Direction = Direction,
                Effect = Info.Effect,
                AI = Info.AI,
                Light = Info.Light,
                Dead = Dead,
            };
        }

        protected override void ProcessTarget()
        {
            if (CurrentMap.Players.Count == 0 || Target == null)
            {
                return;
            }

            Point targetLocation = Target.CurrentLocation;

            if (Envir.Time >= ShockTime)
            {
                MoveToTarget(targetLocation);
            }
            else
            {
                Target = null;
            }
        }

        public override void Die()
        {
            base.Die();
        }

        protected override bool InAttackRange()
        {
            return IsValidTarget(Target) && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        protected override void Attack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = !Functions.InRange(CurrentLocation, Target.CurrentLocation, 2)
                || (Functions.InRange(CurrentLocation, Target.CurrentLocation, 2) && Envir.Random.Next(10) < 3);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (ranged)
            {
                if (Envir.Random.Next(2) == 0)
                {
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                    int damage = GetAttackPower(Stats[Stat.MinMC], HenshinMode ? (Stats[Stat.MaxMC] * 2) : Stats[Stat.MaxMC]);
                    if (damage == 0) return;

                    DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 1200, Target, damage, DefenceType.MAC);
                    ActionList.Add(action);
                }
                else
                {
                    if (Envir.Time >= skeletonBombTime)
                    {
                        SkeletonBomb();
                    }                       
                }
            }
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                if (HenshinMode == false)
                {
                    LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 1);
                }
                else
                {
                    FullmoonAttack(damage, 600, DefenceType.ACAgility, 1, 2);
                }

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                ActionList.Add(action);
            }
        }

        private void SkeletonBomb()
        {
            const int defaultXY = 2;

            int explodeRange = HenshinMode ? 3 : defaultXY;
            int bombX = HenshinMode ? 3 : defaultXY;
            int bombY = HenshinMode ? 3 : defaultXY;

            if (Target == null) return;

            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

            int minX = Math.Max(0, CurrentLocation.X - 5);
            int maxX = Math.Min(CurrentMap.Width - 1, CurrentLocation.X + 5);
            int minY = Math.Max(0, CurrentLocation.Y - 5);
            int maxY = Math.Min(CurrentMap.Height - 1, CurrentLocation.Y + 5);

            List<Point> validLocations = new List<Point>();
            Random rand = new Random();

            while (validLocations.Count < 4)
            {
                int x = rand.Next(minX, maxX + 1);
                int y = rand.Next(minY, maxY + 1);
                Point newLocation = new Point(x, y);

                if (CurrentMap.GetCell(x, y).Valid && (x != CurrentLocation.X || y != CurrentLocation.Y) &&
                    !validLocations.Any(loc => Math.Abs(loc.X - x) < 3 && Math.Abs(loc.Y - y) < 3))
                {
                    validLocations.Add(newLocation);
                }
            }

            foreach (var location in validLocations)
            {
                for (int y = location.Y - explodeRange; y <= location.Y + explodeRange; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;

                    for (int x = location.X - explodeRange; x <= location.X + explodeRange; x++)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;

                        if ((x == bombX && y == bombY) || (x == CurrentLocation.X && y == CurrentLocation.Y)) continue;

                        var cell = CurrentMap.GetCell(x, y);
                        if (!cell.Valid) continue;

                        int damage = 0;
                        var start = 0;
                        var time = 0;
                        if (HenshinMode)
                        {
                            damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            start = 1300;
                            time = 2300;
                        }
                        else
                        {
                            damage = (int)(GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]) * (HenshinMode ? 2 : 1.5));
                            start = 1800;
                            time = 2800;
                        }

                        SpellObject ob = new SpellObject
                        {
                            Spell = HenshinMode ? Spell.FlameExplosion : Spell.SkeletonBomb,
                            Value = damage,
                            ExpireTime = Envir.Time + time + start,
                            TickSpeed = 1300,
                            Direction = Direction,
                            CurrentLocation = new Point(x, y),
                            CastLocation = location,
                            Show = (location.X == x && location.Y == y),
                            CurrentMap = CurrentMap,
                            Owner = this,
                            Caster = this
                        };

                        DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + start, ob);
                        CurrentMap.ActionList.Add(action);
                    }
                }
                skeletonBombTime = Envir.Time + 5000;
            }
        }
    }
}
    