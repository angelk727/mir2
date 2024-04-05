using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class NobleArcher : MonsterObject
    {
        protected internal NobleArcher(MonsterInfo info)
            : base(info)
        {

        }
        protected virtual byte AttackRange
        {
            get
            {
                return 6;
            }
        }
        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
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

            if (Functions.InRange(Target.CurrentLocation, CurrentLocation, 1))
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                ActionTime = Envir.Time + 300;
                AttackTime = Envir.Time + AttackSpeed;

                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                if (damage == 0) return;
                FullmoonAttack(damage);
                SinglePushAttack(damage);

                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 350, Target, damage * 2, DefenceType.AC);
                ActionList.Add(action);
            }
            else
                switch (Envir.Random.Next(4))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 3:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility);
                            ActionList.Add(action);
                        }
                        break;

                }
        }
        protected override void CompleteRangeAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            List<MapObject> targets = FindAllTargets(2, target.CurrentLocation);

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Attacked(this, damage, defence);
            }
        }
    }
}
