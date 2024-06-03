using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon570N : MonsterObject
    {
        public bool callmob = true;

        protected internal Mon570N(MonsterInfo info)
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

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged && Envir.Random.Next(2) > 0)
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            HalfmoonAttack(damage, 300, DefenceType.ACAgility);
                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            var start = 1000;
                            var time = 2000;
                            if (damage == 0) return;

                            SpellObject ob = new()
                            {
                                Spell = Spell.Mon570NRupture,
                                Value = damage,
                                ExpireTime = Envir.Time + time + start,
                                TickSpeed = 1000,
                                Direction = Direction,
                                CurrentLocation = CurrentLocation,
                                CastLocation = CurrentLocation,
                                Show = true,
                                CurrentMap = CurrentMap,
                                Caster = Owner
                            };
                            WideLineAttack(damage, 5, 500, DefenceType.ACAgility, false, 3);
                            DelayedAction action = new(DelayedType.Spawn, Envir.Time + start, ob);
                            CurrentMap.ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            JumpToTarget(3);
                        }
                        break;
                }
            }
            else
                switch (Envir.Random.Next(5))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;
                            var start = 500;
                            var time = 1500;

                            Point targetLocation = Target.CurrentLocation;

                            for (int y = targetLocation.Y - 1; y <= targetLocation.Y + 1; y++)
                            {
                                if (y < 0 || y >= CurrentMap.Height) continue;

                                for (int x = targetLocation.X - 1; x <= targetLocation.X + 1; x++)
                                {
                                    if (x < 0 || x >= CurrentMap.Width) continue;

                                    var cell = CurrentMap.GetCell(x, y);
                                    if (!cell.Valid) continue;

                                    SpellObject ob = new()
                                    {
                                        Spell = Spell.Mon570NLightningCloud,
                                        Value = damage,
                                        ExpireTime = Envir.Time + time + start,
                                        TickSpeed = 1000,
                                        CurrentLocation = new Point(x, y),
                                        CastLocation = targetLocation,
                                        Show = (x == targetLocation.X && y == targetLocation.Y),
                                        CurrentMap = CurrentMap,
                                        Caster = this
                                    };

                                    DelayedAction action = new(DelayedType.Spawn, Envir.Time + start, ob, damage, DefenceType.MAC);
                                    CurrentMap.ActionList.Add(action);
                                }
                            }
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 300, Target, damage, DefenceType.MACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        if (HealthPercent <= 25)
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                            int dm = (int)(Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 0.6);
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC] * dm);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 300, Target, damage, DefenceType.MACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 3:
                        if (Info.Effect == 0 && HealthPercent <= 50 && callmob == true)
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                            SpawnSlaves();
                        }
                        break;
                    case 4:
                        {
                            JumpToTarget(3);
                        }
                        break;

                }
        }
        protected virtual void JumpToTarget(int distance)
        {
            if (Functions.InRange(CurrentLocation, Target.CurrentLocation, 1)) return;

            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            if (jumpDir != Direction) return;

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
            if (damage == 0) return;

            int targetDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);
            int maxJumpDistance = Math.Min(distance, targetDistance - 1);
            var start = 1000;
            var time = 2000;

            Point location = CurrentLocation;

            for (int i = 0; i < maxJumpDistance; i++)
            {
                location = Functions.PointMove(location, jumpDir, 1);
                if (!CurrentMap.ValidPoint(location))
                {
                    return;
                }

                CurrentMap.GetCell(CurrentLocation).Remove(this);
                RemoveObjects(jumpDir, 1);
                CurrentLocation = location;
                CurrentMap.GetCell(CurrentLocation).Add(this);
                AddObjects(jumpDir, 1);
            }

            Broadcast(new S.ObjectBackStep { ObjectID = ObjectID, Direction = jumpDir, Location = location, Distance = maxJumpDistance });

            SpellObject ob = new()
            {
                Spell = Spell.Mon570NRupture,
                Value = damage,
                ExpireTime = Envir.Time + time + start,
                TickSpeed = 1000,
                Direction = Direction,
                CurrentLocation = CurrentLocation,
                CastLocation = CurrentLocation,
                Show = true,
                CurrentMap = CurrentMap,
                Caster = Owner
            };
            WideLineAttack(damage, 5, 500, DefenceType.ACAgility, false, 3);
            DelayedAction action = new(DelayedType.Spawn, Envir.Time + start, ob);
            CurrentMap.ActionList.Add(action);
        }

        private void SpawnSlaves()
        {
            if (Info.Effect != 0 || callmob == false) return;

            int count = Math.Min(1, 1 - SlaveList.Count);

            for (int i = 0; i < count; i++)
            {
                MonsterObject mob;

                {
                    mob = GetMonster(Envir.GetMonsterInfo(Settings.Mon570NMirrorImageMob));
                }

                if (mob == null) continue;

                if (!mob.Spawn(CurrentMap, Front))
                    mob.Spawn(CurrentMap, Target.CurrentLocation);

                mob.ActionTime = Envir.Time + 2000;
                SlaveList.Add(mob);

                callmob = false;
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
            MoveTo(Target.CurrentLocation);
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