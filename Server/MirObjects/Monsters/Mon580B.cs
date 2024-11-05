using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon580B : MonsterObject
    {
        private long LastMon580BShieldTime = 0;
        protected internal Mon580B(MonsterInfo info)
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 3);

            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;

            if (HealthPercent < 80)
                Mon580BShield();

            if (!ranged && Envir.Random.Next(2) > 0)
            {
                if (Envir.Random.Next(4) > 0)
                {
                    List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
                    if (targets.Count == 0) return;

                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });
                    for (int i = 0; i < targets.Count; i++)
                    {
                        Target = targets[i];
                        int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                        if (damage == 0) return;

                        DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, false);
                        ActionList.Add(action);

                        Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.Mon580BSpikeTrap });
                    }
                }
                else
                {
                    List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
                    if (targets.Count == 0) return;

                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                    for (int i = 0; i < targets.Count; i++)
                    {
                        Target = targets[i];
                        int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                        if (damage == 0) return;

                        DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, false);
                        ActionList.Add(action);

                        Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.Mon580BSpikeTrap });
                        PoisonTarget(targets[i], 15, 10, PoisonType.Blindness, 1000);
                    }
                }
            }
            else
                switch (Envir.Random.Next(3))
                {
                    case 0:
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

                                Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.Mon580BLightning });
                                PoisonTarget(targets[i], 25, 10, PoisonType.Blindness, 1000);
                            }
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            Mon580BSpells();
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                            Mon580BShield();
                        }
                        break;
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
                    var start = 500;
                    var time = Settings.Second * 7;

                    SpellObject ob = new()
                    {
                        Spell = Envir.Random.Next(2) == 0 ? Spell.Mon580BDenseFog : Spell.Mon580BPoisonousMist,
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
        public void Mon580BShield()
        {
            if (Envir.Time < LastMon580BShieldTime + 120 * 1000) return;

            var stats = new Stats();
            int shieldTime;
            if (HealthPercent >= 60 && HealthPercent <= 80)
            {
                stats[Stat.MaxAC] = 30;
                stats[Stat.MinAC] = 30;
                shieldTime = 30000;
            }
            else if (HealthPercent >= 30 && HealthPercent <= 50)
            {
                stats[Stat.MaxAC] = 60;
                stats[Stat.MinAC] = 60;
                shieldTime = 45000;
            }
            else if (HealthPercent <= 20)
            {
                stats[Stat.MaxAC] = 90;
                stats[Stat.MinAC] = 90;
                shieldTime = 60000;
            }
            else
            {
                return;
            }

            if (Target != null)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });
                AddBuff(BuffType.Mon580BShield, this, shieldTime, stats);
                LastMon580BShieldTime = Envir.Time;
            }
        }
        //private void SpawnSlave()
        //{
        //    ActionTime = Envir.Time + 300;
        //    AttackTime = Envir.Time + AttackSpeed;

        //    var mob = GetMonster(Envir.GetMonsterInfo(Settings.Mon580BMob)); //新建一个怪物 Mon581N 并设置为召唤怪Mon580BMob

        //    if (mob == null) return;

        //    if (!mob.Spawn(CurrentMap, Front)) //将此改为身后
        //        mob.Spawn(CurrentMap, CurrentLocation);

        //    mob.Target = Target;
        //    mob.ActionTime = Envir.Time;
        //    SlaveList.Add(mob);
        //}

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