using System.Collections.Generic;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class AncientStoneGolem : MonsterObject
    {

        protected internal AncientStoneGolem(MonsterInfo info)
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
            //穿刺攻击
            if (HealthPercent < 90  && Envir.Random.Next(3) == 0)
            {
                AxeThump();
                return;
            }
            //飞石攻击
            if ( Envir.Random.Next(3) == 0)
            {
                FlyStone();
                return;
            }

            if (!ranged)

                if (Target == null) return;

            if (InAttackRange() && CanAttack)
            {
                Attack();
                return;
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

        private void AxeThump() //穿刺攻击
        {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation});

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                LineAttack(damage, 4, 300, DefenceType.MACAgility);
         }

        private void FlyStone() //飞石攻击
        {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });
                AttackTime = Envir.Time + AttackSpeed + 500;
       
                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;

                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.AC);
                PoisonTarget(Target, 3, 5, PoisonType.Dazed);
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
                FullmoonAttack(damage);
                {
                    DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, location, damage, DefenceType.AC);
                    ActionList.Add(action);
                }
            }

            Broadcast(new S.ObjectDashAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Distance = 1 });
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