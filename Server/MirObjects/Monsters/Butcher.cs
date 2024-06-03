using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Butcher : MonsterObject
    {
        public long _FlyAxeTime;
        private long _AxeThumpTime;
        public long _BuffTime;

        protected internal Butcher(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);


            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
            //蓄力重击 Type 2=近3
            if (Envir.Time > _AxeThumpTime && HealthPercent < 90 && Envir.Random.Next(4) == 0)
            {
                AxeThump();
                return;
            }
            //飞斧攻击
            if (Envir.Time > _FlyAxeTime && HealthPercent < 85 && Envir.Random.Next(4) == 0)
            {
                FlyAxe();
                return;
            }
            //增加特效
            if (Envir.Time > _BuffTime && HealthPercent < 80 && Envir.Random.Next(4) == 0)
            {
                ButcherSpellBuff();
                return;
            }

            if (!ranged)

                if (Target == null) return;

            if (InAttackRange() && CanAttack)
            {
                Attack();
                return;
            }

            else
            //普通攻击
            {


                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.ACAgility, false);
                ActionList.Add(action);
            }


            if (Envir.Random.Next(3) == 0)
            {
                ActionTime = Envir.Time + 500;

                Thrust(Target);
            }
            else
            {
                MoveTo(Target.CurrentLocation);
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

        private void AxeThump() //蓄力重击
        {
            byte stompLoops = (byte)Envir.Random.Next(5, 10);
            int stompDuration = stompLoops * 100;

            _AxeThumpTime = Envir.Time + 5000;

            ActionTime = Envir.Time + (stompDuration) + 500;
            AttackTime = Envir.Time + (stompDuration) + 500 + AttackSpeed;

            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2, Level = stompLoops });

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]) * stompLoops;
            if (damage == 0) return;
            LineAttack(damage, 7, 300, DefenceType.MACAgility);

            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + stompDuration + 500, Target, damage, DefenceType.AC, true);
            ActionList.Add(action);

            return;
        }

        private void FlyAxe() //飞斧攻击
        {
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 3 });

            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);

            if (Dead) return;

            if (Envir.Time > _FlyAxeTime)
                _FlyAxeTime = Envir.Time + 10000;

            var count = targets.Count;

            if (count == 0) return;

            var target = targets[Envir.Random.Next(count)];

            var location = target.CurrentLocation;

            Parallel.For(location.Y - 3, location.Y + 4, y =>
            {
                if (y < 0) return;
                if (y >= CurrentMap.Height) return;

                Parallel.For(location.X - 3, location.X + 4, x =>
                {
                    if (x < 0) return;
                    if (x >= CurrentMap.Width) return;

                    if (x == CurrentLocation.X && y == CurrentLocation.Y) return;

                    var cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid) return;

                    int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);

                    var start = 1000;
                    var time = Settings.Second * 15;

                    SpellObject ob = new SpellObject
                    {
                        Spell = Spell.ButcherFlyAxe,
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
                });
            });
        }

        private void Thrust(MapObject target) //旋风冲击
        {
            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);

            Point location;

            for (int i = 0; i < 1; i++) //1为冲击格数
            {
                location = Functions.PointMove(CurrentLocation, jumpDir, 1);
                if (!CurrentMap.ValidPoint(location)) return;
            }

            for (int i = 0; i < 1; i++)
            {
                location = Functions.PointMove(CurrentLocation, jumpDir, 1);

                CurrentMap.GetCell(CurrentLocation).Remove(this);
                RemoveObjects(jumpDir, 1);
                CurrentLocation = location;
                CurrentMap.GetCell(CurrentLocation).Add(this);
                AddObjects(jumpDir, 1);

                int damage = Stats[Stat.MaxDC];

                if (damage > 0)
                    FullmoonAttack(damage);
                {
                    DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, location, damage, DefenceType.AC);
                    ActionList.Add(action);
                }
            }

            Broadcast(new S.ObjectDashAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Distance = 1 });
        }

        private void ButcherSpellBuff() //诅咒特效
        {
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

            List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);

            if (Dead) return;

            if (Envir.Time > _BuffTime)
                _BuffTime = Envir.Time + 30000;

            {

                var min = Stats[Stat.MinAC];
                var max = Stats[Stat.MaxAC];
                var stats = new Stats
                {
                    [Stat.MaxAC] = max * -1,
                    [Stat.MinAC] = min * -1
                };

                Target.AddBuff(BuffType.死亡印记, this, Settings.Second * 10, stats);

            }
            return;
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
    }
}