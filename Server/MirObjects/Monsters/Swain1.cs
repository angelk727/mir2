using System.Collections.Generic;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Swain1 : MonsterObject
    {
        private const byte AttackRange = 8;
        public bool Mode = false;
        public long _FlamingSeaTime;

        protected internal Swain1(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
        }

        protected bool InRangeAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        protected override void ProcessTarget()
        {
            if (Target == null || Dead) return;

            if (!InAttackRange())
            {
                if (CanAttack)
                {
                    if (Envir.Random.Next(2) == 0)
                        RangeAttack();
                }
                if (CurrentLocation == Target.CurrentLocation)
                {
                    MirDirection direction = (MirDirection)Envir.Random.Next(8);
                    int rotation = Envir.Random.Next(2) == 0 ? 1 : -1;

                    for (int d = 0; d < 8; d++)
                    {
                        if (Walk(direction)) break;

                        direction = Functions.ShiftDirection(direction, rotation);
                    }
                }
                else
                    MoveTo(Target.CurrentLocation);
            }

            if (!CanAttack) return;

            if (Envir.Random.Next(3) > 0)
            {
                if (InAttackRange())
                    Attack();
            }
            else RangeAttack();

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }
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


            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            if (damage == 0) return;

            FullmoonAttack(damage, 600, DefenceType.ACAgility, 1, 2);
            FullmoonAttack(damage, 1200, DefenceType.ACAgility, 1, 2);
            FullmoonAttack(damage, 2400, DefenceType.ACAgility, 1, 2);

            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage, DefenceType.ACAgility, false);
            ActionList.Add(action);

        }

        public void RangeAttack()
        {
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            ShockTime = 0;
            ActionTime = Envir.Time + 1500;
            AttackTime = Envir.Time + AttackSpeed + 1000;
            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
            if (damage == 0) return;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step

            if (Envir.Random.Next(3) > 0)
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                if (damage == 0) return;
                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 100 + delay, Target, damage, DefenceType.MACAgility);
                ActionList.Add(action);
            }
            else
            {
                FlameExplosion();
                //Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
                //if (damage == 0) return;
                //DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 250 + delay, Target, damage * 3 / 2, DefenceType.MACAgility);
                //ActionList.Add(action);
            }
        }
        private void FlameExplosion() //火焰爆炸
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

                    var start = 1300;
                    var time = 2300;

                    SpellObject ob = new SpellObject
                    {
                        Spell = Spell.FlameExplosion,
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
        }
    }
}