﻿using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon409B : MonsterObject
    {
        protected internal Mon409B(MonsterInfo info)
            : base(info)
        {
        }
        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, 2);
        }

        private bool _StartAdvanced;
        private bool _Immune;

        private Point MapCentre
        {
            get
            {
                if (CurrentMap == null) return Point.Empty;

                int x = Math.Abs(CurrentLocation.X);
                int y = Math.Abs(CurrentLocation.Y);

                return new Point(x + 2, y);
            }
        }

        private readonly byte _BoulderHealthPercent = 80;
        private bool _CalledBoulders, _StartedBoulderWalk;

        private readonly byte _RockHealthPercent = 50;
        private bool _CalledRockSpikes;
        private long _RockSpikeTime;
        private readonly Point[,] _RockSpikeArea = new Point[5, 5];
        private readonly List<SpellObject> _RockSpikeEffects = new List<SpellObject>();

        private readonly byte _ShieldHealthPercent = 20;
        private bool _CalledShield;
        private readonly byte _ShieldSeconds = 20;

        public override bool IsAttackTarget(MonsterObject attacker)
        {
            return !_Immune && base.IsAttackTarget(attacker);
        }

        public override bool IsAttackTarget(HumanObject attacker)
        {
            return !_Immune && base.IsAttackTarget(attacker);
        }

        protected override void ProcessAI()
        {
            if ((HealthPercent < _BoulderHealthPercent || _StartedBoulderWalk) && !_CalledBoulders)
            {
                _StartedBoulderWalk = true;

                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                SpawnBoulder();

                DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + 3000);
                ActionList.Add(action);
            }

            if (_Immune) return;

            if (HealthPercent < _ShieldHealthPercent && !_CalledShield && (HP > 0))
            {
                KillRockSpikes();
                KillSlaves();

                var stats = new Stats
                {
                };
                _CalledShield = true;
                _Immune = true;

                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2, Level = _ShieldSeconds });

                int x = Math.Abs(CurrentLocation.X);
                int y = Math.Abs(CurrentLocation.Y);

                var spellObj = new SpellObject
                {

                    Spell = Spell.Mon409BShield,
                    ExpireTime = Envir.Time + Settings.Second * _ShieldSeconds,
                    TickSpeed = 1000,
                    Caster = this,
                    CurrentLocation = new Point(x, y),
                    CurrentMap = CurrentMap,
                    Direction = MirDirection.Up
                };

                DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + 2000, spellObj);
                CurrentMap.ActionList.Add(action);

                AddBuff(BuffType.HornedCommanderShield, this, Settings.Second * 12, stats);
                SpawnSlave();
                return;
            }

            if (HealthPercent < _RockHealthPercent && HealthPercent >= _ShieldHealthPercent)
            {
                if (!_CalledRockSpikes)
                {
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                    SetupRockSpike();
                    _CalledRockSpikes = true;
                }
            }

            if (_CalledRockSpikes && Envir.Time > _RockSpikeTime)
            {
                var spawned = SpawnRockSpikes();
                _RockSpikeTime = Envir.Time + 5000;

                if (!spawned)
                {
                    _RockSpikeTime = long.MaxValue;
                }
            }

            if (HealthPercent < 100 && !_StartAdvanced)
            {
                _StartAdvanced = true;
            }
            else if (HealthPercent == 100 && _StartAdvanced)
            {
                Reset();
            }

            base.ProcessAI();
        }

        private void Reset()
        {
            _StartAdvanced = false;
            _StartedBoulderWalk = false;
            _CalledBoulders = false;
            _CalledRockSpikes = false;
            _CalledShield = false;
            _RockSpikeTime = 0;

            KillRockSpikes();
            KillSlaves();
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (InAttackRange())
            {
                Attack();

                if (Target != null && Target.Dead)
                {
                    FindTarget();
                }

                return;
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            MoveTo(Target.CurrentLocation);
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
            ShockTime = 0;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            switch (Envir.Random.Next(3))
            {
                case 0:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        HalfmoonAttack(damage);
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 6000, Target, damage, DefenceType.ACAgility, false);
                        ActionList.Add(action);
                    }
                    break;
                case 1:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        TriangleAttack(damage, 3, 2, 500, DefenceType.ACAgility, false);
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 6000, Target, damage, DefenceType.ACAgility, false);
                        ActionList.Add(action);
                    }
                    break;
                case 2:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 3 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        WideLineAttack(damage, 3, 500, DefenceType.ACAgility, false, 3);
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 30000, Target, damage, DefenceType.ACAgility, false);
                        ActionList.Add(action);
                    }
                    break;
            }

            if (_StartAdvanced && Envir.Random.Next(45) == 0)
            {
                byte spinLoops = (byte)Envir.Random.Next(6, 8);
                int spinDuration = spinLoops * 500;
                _Immune = true;
                ActionTime = Envir.Time + spinDuration + 500;
                AttackTime = Envir.Time + spinDuration + 500 + AttackSpeed;

                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2, Level = (byte)(spinLoops - 6) });

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]) * spinLoops;
                if (damage == 0) return;

                FullmoonAttack(damage, 600, DefenceType.ACAgility, -1, 2);
                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + (spinDuration) + 20000, Target, damage, DefenceType.AC, false);
                ActionList.Add(action);

            }

            if (_StartAdvanced && Envir.Random.Next(40) == 0)
            {
                byte HammerLoops = (byte)Envir.Random.Next(10, 12);
                int HammerDuration = HammerLoops * 300;
                _Immune = true;
                ActionTime = Envir.Time + HammerDuration + 500;
                AttackTime = Envir.Time + HammerDuration + 500 + AttackSpeed;

                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 4, Level = (byte)(HammerLoops - 10) });

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]) * HammerLoops;
                if (damage == 0) return;

                WideLineAttack(damage, 4, 500, DefenceType.ACAgility, false, 4);
                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + (HammerDuration) + 20000, Target, damage, DefenceType.ACAgility, false);
                ActionList.Add(action);
            }

            if (_StartAdvanced && Envir.Random.Next(20) == 0)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                if (SlaveList.Count < 3)
                {
                    var mob = GetMonster(Envir.GetMonsterInfo(Settings.HornedCommanderMob));
                    if (mob != null)
                    {
                        Point back = Functions.PointMove(CurrentLocation, Functions.ReverseDirection(Direction), 1);
                        if (!mob.Spawn(CurrentMap, back))
                        {
                            mob.Spawn(CurrentMap, CurrentLocation);
                        }
                        mob.Target = Target;
                        mob.ActionTime = Envir.Time;
                        SlaveList.Add(mob);
                    }
                }
            }
        }

        protected override void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            bool aoe = data.Count >= 4 && (bool)data[3];

            _Immune = false;

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
        protected override void CompleteRangeAttack(IList<object> data)
        {
            _Immune = false;

            if (data.Count > 0)
            {
                MapObject target = (MapObject)data[0];
                int damage = (int)data[1];
                DefenceType defence = (DefenceType)data[2];
                int aoeSize = (int)data[3];

                if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

                var front = Functions.PointMove(CurrentLocation, Direction, 2);
                var targets = FindAllTargets(aoeSize, front, false);

                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].Attacked(this, damage, defence);
                }
            }
            else
            {
                TeleportRandom(10, 10);
                Target = null;
            }
        }
        public override bool TeleportRandom(int attempts, int distance, Map temp = null)
        {
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
            DelayedAction action = new DelayedAction(DelayedType.MapMovement, Envir.Time + 500, this);
            ActionList.Add(action);

            for (int i = 0; i < attempts; i++)
            {
                Point location;

                if (distance <= 0)
                    location = new Point(Envir.Random.Next(CurrentMap.Width), Envir.Random.Next(CurrentMap.Height));
                else
                    location = new Point(CurrentLocation.X + Envir.Random.Next(-distance, distance + 1),
                                         CurrentLocation.Y + Envir.Random.Next(-distance, distance + 1));

                if (Teleport(CurrentMap, location, true, 10)) return true;
            }

            return false;
        }
        private void SetupRockSpike()
        {
            var xLength = _RockSpikeArea.GetLength(0);
            var yLength = _RockSpikeArea.GetLength(1);

            var midX = ((int)Math.Ceiling((decimal)xLength / 2) - 1) * -1;
            var midY = ((int)Math.Ceiling((decimal)yLength / 2) - 1) * -1;

            var actualX = midX;
            var actualY = midY;

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    var point = new Point(MapCentre.X + (actualX * 5), MapCentre.Y + (actualY * 5));

                    _RockSpikeArea[x, y] = point;

                    actualY++;
                }

                actualY = midY;
                actualX++;
            }
        }
        private bool SpawnRockSpikes()
        {
            var spawned = false;

            var xLength = _RockSpikeArea.GetLength(0);
            var yLength = _RockSpikeArea.GetLength(1);

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    var point = _RockSpikeArea[x, y];

                    var existing = _RockSpikeEffects.Any(x => x.CastLocation == point);

                    if (existing) continue;

                    spawned = SpawnRockSpike(point);

                    if (spawned)
                    {
                        return true;
                    }
                }

                if (spawned)
                {
                    return true;
                }
            }

            return false;
        }
        private bool SpawnRockSpike(Point location)
        {
            var spawned = false;

            for (int y = location.Y - 2; y <= location.Y + 2; y++)
            {
                if (y < 0) continue;
                if (y >= CurrentMap.Height) break;

                for (int x = location.X - 2; x <= location.X + 2; x++)
                {
                    if (x < 0) continue;
                    if (x >= CurrentMap.Width) break;

                    var cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid) continue;

                    if (location.X == x && location.Y == y)
                    {
                        spawned = true;
                    }

                    int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);

                    var start = 500;

                    SpellObject ob = new SpellObject
                    {
                        Spell = Spell.HornedCommanderRockSpike,
                        Value = damage,
                        ExpireTime = Envir.Time + start + (Settings.Minute * 10),
                        TickSpeed = 1000,
                        CurrentLocation = new Point(x, y),
                        CastLocation = location,
                        Show = location.X == x && location.Y == y,
                        CurrentMap = CurrentMap,
                        Caster = this
                    };

                    DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + start, ob);
                    CurrentMap.ActionList.Add(action);

                    _RockSpikeEffects.Add(ob);
                }
            }

            return spawned;
        }
        private void SpawnBoulder()
        {
            if (Functions.InRange(CurrentLocation, MapCentre, 20))
            {
                Teleport(CurrentMap, MapCentre, true, 10);
            }

            _CalledBoulders = true;

            for (int i = 0; i < 8; i++)
            {
                var mob = GetMonster(Envir.GetMonsterInfo(Settings.HornedCommanderBombMob));

                var odd = i % 2 != 0;

                var point = Functions.PointMove(CurrentLocation, (MirDirection)i, odd ? 7 : 9);

                if (mob == null) continue;

                mob.Direction = Functions.DirectionFromPoint(point, CurrentLocation);

                if (mob.Spawn(CurrentMap, point))
                {
                    mob.Target = Target;
                    mob.ActionTime = Envir.Time;
                    SlaveList.Add(mob);
                }
            }
        }
        private void SpawnSlave()
        {
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            var mob = GetMonster(Envir.GetMonsterInfo(Settings.Mon409BMob2));

            if (mob == null) return;

            if (!mob.Spawn(CurrentMap, Front))
                mob.Spawn(CurrentMap, CurrentLocation);

            mob.Target = Target;
            mob.ActionTime = Envir.Time;
            SlaveList.Add(mob);
        }
        private void KillRockSpikes()
        {
            _RockSpikeTime = long.MaxValue;

            foreach (var effect in _RockSpikeEffects)
            {
                effect.ExpireTime = Envir.Time;
            }

            _RockSpikeEffects.Clear();
        }
        private void KillSlaves()
        {
            for (int i = SlaveList.Count - 1; i >= 0; i--)
            {
                if (!SlaveList[i].Dead && SlaveList[i].Node != null)
                {
                    SlaveList[i].Die();
                }
            }
        }
        public override void Die()
        {
            base.Die();

            KillRockSpikes();
            KillSlaves();
        }
    }
}

