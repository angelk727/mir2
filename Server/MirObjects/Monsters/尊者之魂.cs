using System.Collections.Generic;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class 尊者之魂 : MonsterObject
    {
        public byte AttackRange = 7;
        private long _SeniorTornadoTime;
        public bool ResurrectionMark = false;

        protected internal 尊者之魂(MonsterInfo info)
            : base(info)
        {
        }
        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;

            return Target.CurrentLocation != CurrentLocation && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 2);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            //旋风
            if (Envir.Time > _SeniorTornadoTime && HealthPercent < 90 && Envir.Random.Next(4) == 0)
            {
                _SeniorTornadoTime = Envir.Time + 15000;

                SeniorTornado();
                return;
            }

            if (!ranged && Envir.Random.Next(2) > 0)
            {
                if (Envir.Random.Next(4) > 0)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, false);
                    ActionList.Add(action);
                }
                else
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                    if (damage == 0) return;

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC, true);
                    ActionList.Add(action);
                }

            }
            else

                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                            AttackTime = Envir.Time + AttackSpeed + 500;
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MAC, false);
                            ActionList.Add(action);

                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC] * 2);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MAC, true);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC] * 2);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MAC, true);
                            ActionList.Add(action);
                        }
                        break;
                }
        }

        protected override void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            bool aoe = (bool)data[3];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            if (aoe)
            {
                List<MapObject> targets = FindAllTargets(3, CurrentLocation);
                if (targets.Count == 0) return;

                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].Attacked(this, damage, defence) <= 0) continue;
                }
            }
            else
            {
                if (target.Attacked(this, damage, defence) <= 0) return;
            }
        }

        protected override void CompleteRangeAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            bool aoe = (bool)data[3];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            if (aoe)
            {
                List<MapObject> targets = FindAllTargets(AttackRange, CurrentLocation);
                if (targets.Count == 0) return;

                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].Attacked(this, damage, defence) <= 0) continue;

                    if (Envir.Random.Next(3) >= 0)
                    {
                        Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.KingGuard, EffectType = 0 });
                        PoisonTarget(targets[i], 5, 10, PoisonType.Slow, 1000);
                    }
                    else
                    {
                        Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.KingGuard, EffectType = 1 });
                        PoisonTarget(targets[i], 5, 10, PoisonType.Paralysis, 1000);
                    }
                }
            }
            else
            {
                if (target.Attacked(this, damage, defence) <= 0) return;

                PoisonTarget(target, 10, 5, PoisonType.Green, 1000);
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

        private void SeniorTornado()
        {
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);

            var count = targets.Count;

            if (count == 0) return;

            var target = targets[Envir.Random.Next(count)];

            var location = target.CurrentLocation;

            for (int y = location.Y - 1; y <= location.Y + 1; y++)
            {
                if (y < 0) continue;
                if (y >= CurrentMap.Height) break;

                for (int x = location.X - 1; x <= location.X + 1; x++)
                {
                    if (x < 0) continue;
                    if (x >= CurrentMap.Width) break;

                    if (x == CurrentLocation.X && y == CurrentLocation.Y) continue;

                    var cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid) continue;

                    int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MinMC]);

                    var start = 500;

                    var time = Settings.Second * 15;
                    byte randomdirection = (byte)Envir.Random.Next(8);

                    SpellObject ob = new SpellObject
                    {
                        Spell = Spell.尊者旋风,
                        Value = damage,
                        ExpireTime = Envir.Time + time + start,
                        TickSpeed = 3000,
                        CurrentLocation = new Point(x, y),
                        CastLocation = location,
                        Show = location.X == x && location.Y == y,
                        CurrentMap = CurrentMap,
                        Caster = this
                    };

                    DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + start, ob);
                    CurrentMap.ActionList.Add(action);
                }
            }
        }
         public override void Die()
        {
            if (ResurrectionMark == false)
            {
                ResurrectionMark = true;
                Broadcast(new S.ObjectRevived{ ObjectID = ObjectID });
                SetHP(MaxHealth);
                ActionTime = Envir.Time + 1000;

            }
            else
            {
                base.Die();
            }
        }
    }
}
