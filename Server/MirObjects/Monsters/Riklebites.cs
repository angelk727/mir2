using System;
using System.Collections.Generic;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Riklebites : MonsterObject
    {
        protected virtual byte AttackRange => Info.ViewRange;

        public int EnergyShieldTime { get; set; } = Settings.Second * 30;

        public long RiklebitesBlastTime { get; set; }

        public long ShapedLightningTime { get; set; }

        public long KillBlowLastTime { get; set; }

        protected internal Riklebites(MonsterInfo info)
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
                if (CanAttack)
                {
                    Attack();
                }
                return;
            }

            MoveTo(target);
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
            bool ranged = !Functions.InRange(CurrentLocation, Target.CurrentLocation, 3)
                || (Functions.InRange(CurrentLocation, Target.CurrentLocation, 3) && Envir.Random.Next(10) < 3);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (ranged)
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            PoisonTarget(Target, 6, 6, PoisonType.Dazed, 1000);
                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 1200, Target, damage, DefenceType.MAC);
                            ActionList.Add(action);
                        }                       
                        break;

                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            RiklebitesBlast();
                        }
                        break;

                    case 2:
                        {
                            var hpPercent = (HP * 100) / Stats[Stat.HP];

                            if ((Envir.Time > KillBlowLastTime) && hpPercent <= 80 && Envir.Random.Next(3) == 0)
                            {
                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                                KillBlowLastTime = Envir.Time + 60000;
                                KillBlowAttack();
                            }

                        }
                        break;
                }
            }
            else
            {
                switch (Envir.Random.Next(4))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;

                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            if (Functions.InRange(Target.CurrentLocation, CurrentLocation, 1))
                            {
                                SinglePushAttack(damage, DefenceType.AC, 500, 2);
                                Target.ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = 3, TickSpeed = 1500 }, this);
                            }

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;

                    case 2:
                        {
                            EnergyShieldAttack();
                        }
                        break;

                    case 3:
                        {
                            ShapedLightningAttack();
                        }
                        break;
                }
            }
        }

        public void ShapedLightningAttack()
        {
            if (Envir.Time < ShapedLightningTime)
                return;

            ShapedLightningTime = Envir.Time + 20000;

            if (Target == null)
                return;

            byte SLDR = (byte)Envir.Random.Next(5, 10);

            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);

            if (damage == 0)
                return;

            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2, Level = SLDR });
            ActionTime = Envir.Time + 1000;
            AttackTime = ActionTime + AttackSpeed;

            for (int i = 0; i < SLDR; i++)
            {
                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 900 + i * 300, Target, damage, DefenceType.None);
                ActionList.Add(action);
            }
        }

        public void KillBlowAttack()
        {
            List<MapObject> targets = FindAllTargets(AttackRange, Target.CurrentLocation).FindAll(x => x.Race == ObjectType.Player && ((PlayerObject)x).IsAttackTarget(this));

            if (targets.Count == 0) return;

            var target = (PlayerObject)targets[Envir.Random.Next(targets.Count)];

            int damage = (int)(Target.MaxHealth * 1f);

            var KillBlowOb = new SpellObject
            {
                Spell = Spell.RiklebitesRollCall,
                Value = damage,
                ExpireTime = Envir.Time,
                TickSpeed = 500,
                Caster = this,
                CurrentLocation = target.CurrentLocation,
                CurrentMap = CurrentMap,
                Direction = MirDirection.Up
            };

            target.Attacked(this, KillBlowOb.Value, DefenceType.None);

            DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time, KillBlowOb);
            CurrentMap.ActionList.Add(action);

        }

        public void EnergyShieldAttack()
        {
            var hpPercent = (HP * 100) / Stats[Stat.HP];
            bool stage1Bubble = hpPercent >= 70 && hpPercent <= 80;
            bool stage2Bubble = hpPercent >= 40 && hpPercent <= 50;
            bool stage3Bubble = hpPercent <= 20;

            if (stage1Bubble == true || stage2Bubble == true || stage3Bubble == true)
            {
                if (Target != null)
                {
                    var stats = new Stats
                    {
                        [Stat.MaxAC] = 100,
                        [Stat.MinAC] = 100
                    };

                    AddBuff(BuffType.RiklebitesShield, this, EnergyShieldTime, stats);

                    if (Envir.Time > RiklebitesBlastTime)
                    {
                        RiklebitesBlast();
                    }

                    OperateTime = 0;
                }
            }
        }

        public void RiklebitesBlast()
        {
            if (Envir.Time > RiklebitesBlastTime)
            {
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

                        var start = 1200;
                        var time = 2300;

                        SpellObject ob = new SpellObject
                        {
                            Spell = Spell.RiklebitesBlast,
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

                        DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + start, ob);
                        CurrentMap.ActionList.Add(action);
                    }
                }
                RiklebitesBlastTime = Envir.Time + Math.Max(Envir.Random.Next(2000), Envir.Random.Next(3000));
            }
        }
    }
}


