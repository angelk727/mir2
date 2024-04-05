using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class ShardGuardian : MonsterObject
    {
        protected virtual byte AttackRange => Info.ViewRange;

        public bool HenshinMode = false;

        private long _BuffTime;

        protected internal ShardGuardian(MonsterInfo info)
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

            if (HealthPercent <= 45 && HenshinMode == false)
            {
                HenshinMode = true;
                Broadcast(new S.ObjectShow { ObjectID = ObjectID });

                ActionTime = Envir.Time + 1000;
            }

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

            MoveTo(Target.CurrentLocation);

            if (Envir.Time > _BuffTime)
            {
                var friends = FindAllFriends(Info.ViewRange, CurrentLocation);

                if (friends.Count > 0)
                {
                    var friend = friends[Envir.Random.Next(friends.Count)];

                    int delay = Functions.MaxDistance(CurrentLocation, friend.CurrentLocation) * 50 + 500;

                    Direction = Functions.DirectionFromPoint(CurrentLocation, friend.CurrentLocation);

                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = friend.ObjectID, Type = 1 });

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + delay, friend, 0, DefenceType.MACAgility);

                    ActionList.Add(action);

                    _BuffTime = Envir.Time + 60000;
                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;
                    ShockTime = 0;
                    return;
                }

                _BuffTime = Envir.Time + 30000;
            }

            base.ProcessTarget();
        }

        public override Packet GetInfo()
        {
            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = HenshinMode ? Monster.GlacierWarrior : Monster.ShardGuardian,
                Direction = Direction,
                Effect = Info.Effect,
                AI = Info.AI,
                Light = Info.Light,
                Dead = Dead,
            };
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

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (HenshinMode == false)
            {
                if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, 2) || (Functions.InRange(CurrentLocation, Target.CurrentLocation, 2) && Envir.Random.Next(10) < 3))
                {
                    switch (Envir.Random.Next(2))
                    {
                        case 0:
                            {
                                GroundBurstIce();
                            }
                            break;
                        case 1:
                            {
                                ShardGuardianIceBomb();
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
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                if (damage == 0) return;

                                HalfmoonAttack(damage);

                                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                                ActionList.Add(action);
                            }
                            break;
                        case 1:
                            {
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                if (damage == 0) return;

                                HalfmoonAttack(damage);
                                PoisonTarget(Target, 5, Envir.Random.Next(1, 4), PoisonType.Bleeding, 1000);

                                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                                ActionList.Add(action);
                            }
                            break;
                        case 2:
                            {
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                if (damage == 0) return;

                                FullmoonAttack(damage, 600, DefenceType.ACAgility, 1, 2);

                                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                                ActionList.Add(action);
                            }
                            break;
                    }
                }
            }
            else if (HenshinMode == true)
            {
                if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, 2))
                {
                    return;
                }
                {
                    switch (Envir.Random.Next(4))
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

                                FullmoonAttack(damage, 600, DefenceType.ACAgility, 1, 2);

                                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                                ActionList.Add(action);
                            }
                            break;
                        case 2:
                            {
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                if (damage == 0) return;

                                Thrust(Target);

                                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                                ActionList.Add(action);
                            }
                            break;
                        case 3:
                            {
                                Thrust(Target);
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                if (damage == 0) return;

                                LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 2, 300, DefenceType.ACAgility, true);

                                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                                ActionList.Add(action);
                            }
                            break;
                    }
                }
            }
        }

        private void ShardGuardianIceBomb()
        {
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);

            if (Dead) return;

            var count = targets.Count;

            if (count == 0) return;

            var target = targets[Envir.Random.Next(count)];

            var location = target.CurrentLocation;

            for (int y = location.Y - 3; y <= location.Y + 3; y++)
            {
                if (y < 0) continue;
                if (y >= CurrentMap.Height) break;

                for (int x = location.X - 3; x <= location.X + 3; x++)
                {
                    if (x < 0) continue;
                    if (x >= CurrentMap.Width) break;

                    if (x == CurrentLocation.X && y == CurrentLocation.Y) continue;

                    var cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid) continue;

                    int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MinMC]);

                    var start = 1500;
                    var time = 2300;

                    SpellObject ob = new SpellObject
                    {
                        Spell = Spell.ShardGuardianIceBomb,
                        Value = damage,
                        ExpireTime = Envir.Time + time + start,
                        TickSpeed = 1000,
                        Direction = Direction,
                        CurrentLocation = new Point(x, y),
                        CastLocation = location,
                        Show = location.X == x && location.Y == y,
                        CurrentMap = CurrentMap,
                        Owner = this,
                        Caster = this
                    };

                    PoisonTarget(Target, 5, Envir.Random.Next(1, 5), PoisonType.Frozen, 1000);
                    DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + start, ob);
                    CurrentMap.ActionList.Add(action);
                }
            }
        }

        private void GroundBurstIce()
        {
            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
            if (targets.Count == 0) return;

            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

            for (int i = 0; i < targets.Count; i++)
            {
                Target = targets[i];

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) continue;

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                ActionList.Add(action);

                Broadcast(new S.ObjectEffect { ObjectID = Target.ObjectID, Effect = SpellEffect.GroundBurstIce });
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

        protected override void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || target.CurrentMap != CurrentMap || target.Node == null) return;

            if (target.IsFriendlyTarget(this))
            {
                var friends = FindAllFriends(4, target.CurrentLocation);

                var min = Stats[Stat.MinMC];
                var max = Stats[Stat.MaxMC];

                for (int i = 0; i < friends.Count; i++)
                {

                    if (Info.Effect == 0)
                    {
                        var stats = new Stats { [Stat.MinAC] = min, [Stat.MaxAC] = max, [Stat.MinMAC] = min, [Stat.MaxMAC] = max };
                        friends[i].AddBuff(BuffType.寒冰护甲, this, Settings.Second * 10, stats);
                    }
                    else if (Info.Effect == 1)
                    {
                        var stats = new Stats { [Stat.MinDC] = min, [Stat.MaxDC] = max, [Stat.MinMC] = min, [Stat.MaxMC] = max };
                        friends[i].AddBuff(BuffType.至尊威严, this, Settings.Second * 10, stats);
                    }

                    friends[i].OperateTime = 0;
                }
            }
            else if (target.IsAttackTarget(this))
            {
                target.Attacked(this, damage, defence);
            }
        }
    }
}