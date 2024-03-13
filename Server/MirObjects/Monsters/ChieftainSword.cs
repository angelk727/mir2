using System.Collections.Generic;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class ChieftainSword : MonsterObject
    {
        private long _BuffTime;
        public bool YangDragonFlameMode = false;
        protected virtual byte AttackRange => Info.ViewRange;

        protected internal ChieftainSword(MonsterInfo info)
            : base(info)
        {
        }

        private bool IsValidTarget(MapObject target)
        {
            return target != null && target.IsAttackTarget(this) && target.CurrentMap == CurrentMap && target.Node != null;
        }

        protected override bool InAttackRange()
        {
            return IsValidTarget(Target) && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
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

        private void MoveToTarget(Point target)
        {
            if (Functions.InRange(CurrentLocation, target, AttackRange))
            {
                if (CanAttack)
                {
                    Attack();
                }
                return;
            }
            MoveTo(target);
        }

        public override void Die()
        {
            base.Die();
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

            if (!ranged)
            {
                switch (Envir.Random.Next(5))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            ThreeQuarterMoonAttack(damage);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            SinglePushAttack(damage);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            BloodthirstySpike();
                        }
                        break;
                    case 3:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 3 });

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

                                int damage = Stats[Stat.MaxDC];

                                if (damage > 0)

                                    TriangleAttack(damage, 3, 2, 500);

                                {
                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, location, damage, DefenceType.AC);
                                    CurrentMap.ActionList.Add(action);
                                }
                            }
                        }
                        break;
                    case 4:
                        {
                            ActionTime = Envir.Time + 500;
                            Thrust(Target);
                        }
                        break;

                }
            }
            else
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            YangDragonFlameMode = false;
                            {
                                YangDragonFlame();
                            }
                        }
                        break;
                    case 1:
                        {
                            YangDragonFlameMode = true;
                            {
                                YangDragonFlame();
                            }
                        }
                        break;
                    case 2:
                        {

                            if (HasBuff(BuffType.ChieftainSwordBuff, out _))
                            {
                                return;
                            }

                            var hpPercent = (HP * 100) / MaxHealth;

                            if (Envir.Time > _BuffTime && hpPercent < 50)
                            {
                                _BuffTime = Envir.Time + 15000 + Envir.Random.Next(0, 5000);

                                var stats = new Stats
                                {
                                    [Stat.MaxDC] = 100,
                                    [Stat.MinMC] = 100
                                };

                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                                AddBuff(BuffType.ChieftainSwordBuff, this, Settings.Second * 10, stats);
                            }
                        }
                        break;
                }
            }
        }

        private void Thrust(MapObject target)
        {
            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);

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

                int damage = Stats[Stat.MaxDC];

                if (damage > 0)
                    LineAttack(damage, 3, 300);
                {
                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, location, damage, DefenceType.AC);
                    CurrentMap.ActionList.Add(action);
                }
            }

            Broadcast(new S.ObjectDashAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Distance = 1 });
        }

        private void BloodthirstySpike()
        {
            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
            if (targets.Count == 0) return;

            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

            for (int i = 0; i < targets.Count; i++)
            {
                Target = targets[i];

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) continue;

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                ActionList.Add(action);

                Broadcast(new S.ObjectEffect { ObjectID = Target.ObjectID, Effect = SpellEffect.BloodthirstySpike });
            }
        }

        private void YangDragonFlame()
        {
            const int defaultXY = 2;
            const int RangeType = 0;

            int explodeRange = YangDragonFlameMode ? 3 : defaultXY;
            byte RangeTypeDate = (byte)(YangDragonFlameMode ? 1 : RangeType);

            int bombX = YangDragonFlameMode ? 3 : defaultXY;
            int bombY = YangDragonFlameMode ? 3 : defaultXY;

            if (Target == null) return;

            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = RangeTypeDate });

            var targets = FindAllTargets(explodeRange, Target.CurrentLocation);
            if (Dead || targets.Count == 0) return;

            Target = targets[Envir.Random.Next(targets.Count)];
            var location = Target.CurrentLocation;

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

                    if (YangDragonFlameMode)
                    {
                        damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                        start = 1000;
                        time = 1600;
                    }
                    else
                    {
                        damage = (int)(GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]) * (YangDragonFlameMode ? 2 : 1.5));
                        start = 1000;
                        time = 2200;
                    }

                    SpellObject ob = new SpellObject
                    {
                        Spell = YangDragonFlameMode ? Spell.YangDragonIcyBurst : Spell.YangDragonFlame,
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
        }
    }
}
