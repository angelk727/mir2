using Server.MirDatabase;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon580B : MonsterObject
    {
        private long _mon580BShieldTime;
        private const int MaxSpawnCount = 1;
        private const int SpellStartDelay = 500;
        private const int SpellDuration = Settings.Second * 7;

        protected internal Mon580B(MonsterInfo info)
            : base(info)
        {
            NameColour = Color.OrangeRed;
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

            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;

            if (CurrentMap.Info.FileName == "icedragon_hide_5" && SlaveList.Count < 1)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });
                SpawnSlaves();
            }

            if (HealthPercent < 80)
            {
                Mon580BShield();
            }

            if (Envir.Random.Next(2) > 0)
            {
                if (Envir.Random.Next(4) > 0)
                {
                    List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
                    if (targets.Count == 0) return;

                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                    foreach (var target in targets)
                    {
                        Target = target;
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        PoisonTarget(target, chanceToPoison: 15, poisonDuration: 10, PoisonType.Blindness,
                            poisonTickSpeed: 1000);
                        DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, target, damage,
                            DefenceType.ACAgility, false);
                        ActionList.Add(action);

                        Broadcast(new S.ObjectEffect { ObjectID = target.ObjectID, Effect = SpellEffect.Mon580BSpikeTrap });
                    }
                }
                else
                {
                    List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
                    if (targets.Count == 0) return;

                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                    foreach (var target in targets)
                    {
                        Target = target;
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, target, damage,
                            DefenceType.ACAgility, false);
                        ActionList.Add(action);

                        Broadcast(new S.ObjectEffect { ObjectID = target.ObjectID, Effect = SpellEffect.Mon580BSpikeTrap });
                    }
                }
            }
            else
                switch (Envir.Random.Next(3))
                {
                    case 0:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 3 });

                        RootAttack();
                    }
                        break;
                    case 1:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 4 });

                        Mon580BSpells();
                    }
                        break;
                    case 2:
                    {
                        List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
                        if (targets.Count == 0) return;

                        Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                        foreach (var target in targets)
                        {
                            Target = target;
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, target, damage,
                                DefenceType.MACAgility, false);
                            ActionList.Add(action);

                            Broadcast(new S.ObjectEffect { ObjectID = target.ObjectID, Effect = SpellEffect.Mon580BLightning });
                        }
                    }
                        break;
                }
        }

        private void RootAttack()
        {
            int[] possibleEdgeLengths = [7, 9, 11, 13, 15, 17];
            int firstEdgeIndex = Envir.Random.Next(0, possibleEdgeLengths.Length - 1);
            int secondEdgeIndex = firstEdgeIndex + 1;

            bool displaySingleEdge = Envir.Random.Next(2) == 0;

            int[] edgeLengthsToUse = displaySingleEdge
                ? new[] { possibleEdgeLengths[firstEdgeIndex] }
                : new[] { possibleEdgeLengths[firstEdgeIndex], possibleEdgeLengths[secondEdgeIndex] };

            foreach (int edgeLength in edgeLengthsToUse)
            {
                int halfLength = edgeLength / 2;

                for (int y = CurrentLocation.Y - halfLength; y <= CurrentLocation.Y + halfLength; y++)
                {
                    for (int x = CurrentLocation.X - halfLength; x <= CurrentLocation.X + halfLength; x++)
                    {
                        if (y != CurrentLocation.Y - halfLength && y != CurrentLocation.Y + halfLength &&
                            x != CurrentLocation.X - halfLength && x != CurrentLocation.X + halfLength)
                        {
                            continue;
                        }

                        if (!CurrentMap.ValidPoint(new Point(x, y))) continue;

                        SpellObject spell = new()
                        {
                            Spell = Spell.Mon580BRoot,
                            Value = Envir.Random.Next(Stats[Stat.MinMC], Stats[Stat.MaxMC]),
                            TickSpeed = 500,
                            ExpireTime = Envir.Time + 2000,
                            CurrentLocation = new Point(x, y),
                            CurrentMap = CurrentMap,
                            Caster = this,
                        };

                        CurrentMap.GetCell(x, y).Add(spell);
                        spell.Spawned();
                    }
                }
            }
        }

        private void Mon580BSpells()
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

                    Spell spellType = Envir.Random.Next(2) == 0 ? Spell.Mon580BDenseFog : Spell.Mon580BPoisonousMist;

                    SpellObject ob = new()
                    {
                        Spell = spellType,
                        Value = damage,
                        ExpireTime = Envir.Time + SpellDuration + SpellStartDelay,
                        TickSpeed = 3000,
                        CurrentLocation = new Point(x, y),
                        CastLocation = location,
                        Show = location.X == x && location.Y == y,
                        CurrentMap = CurrentMap,
                        Caster = this
                    };

                    DelayedAction action = new(DelayedType.Spawn, Envir.Time + SpellStartDelay, ob);
                    CurrentMap.ActionList.Add(action);

                    if (spellType == Spell.Mon580BPoisonousMist)
                    {
                        PoisonTarget(target, 5, 5, PoisonType.Green, 2000);
                    }
                }
            }
        }

        private void Mon580BShield()
        {
            if (Envir.Time < _mon580BShieldTime + 90 * 1000) return;

            var stats = new Stats();
            int shieldTime;
            switch (HealthPercent)
            {
                case > 60 and <= 80:
                    stats[Stat.MaxAC] = 30;
                    stats[Stat.MinAC] = 30;
                    shieldTime = 30000;
                    break;
                case > 30 and <= 50:
                    stats[Stat.MaxAC] = 60;
                    stats[Stat.MinAC] = 60;
                    shieldTime = 45000;
                    break;
                case >= 20 and <= 30:
                    stats[Stat.MaxAC] = 90;
                    stats[Stat.MinAC] = 90;
                    shieldTime = 60000;
                    break;
                default:
                    return;
            }

            if (Target == null) return;

            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });
            AddBuff(BuffType.Mon580BShield, this, shieldTime, stats);
            _mon580BShieldTime = Envir.Time;
        }

        private void SpawnSlaves()
        {

            if (CurrentMap.Info.FileName != "icedragon_hide_5") return;

            int currentSpawnCount = SlaveList.Count;
            int spawnCount = Math.Min(MaxSpawnCount - currentSpawnCount, MaxSpawnCount);

            if (spawnCount <= 0) return;

            Point spawnLocation = new Point(121, 83);

            for (int i = 0; i < spawnCount; i++)
            {
                MonsterObject mob = GetMonster(Envir.GetMonsterInfo(Settings.Mon580BMob));
                if (mob == null) continue;

                mob.Spawn(CurrentMap, spawnLocation);
                mob.ActionTime = Envir.Time + 2000;
                SlaveList.Add(mob);
            }
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

            KillSlaves();
        }
    }
}