using System.Collections.Generic;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class HoodedSummoner : MonsterObject
    {
        public long _FrozenTrapTime;
        protected virtual byte AttackRange
        {
            get
            {
                return 6;
            }
        }

        protected internal HoodedSummoner(MonsterInfo info)
            : base(info)
        {
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
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            switch (Envir.Random.Next(2))
            {
                case 0:
                    {
                        Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                        if (damage == 0) return;
                        PoisonTarget(Target, 6, 5, PoisonType.Slow, 2000);

                        DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility);
                        ActionList.Add(action);
                    }
                    break;
                case 1:
                    switch (Envir.Random.Next(3))
                    {
                        case 0:
                            {
                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;

                                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                                if (damage == 0) return;
                                PoisonTarget(Target, 0, 5, PoisonType.Dazed, 2000);

                                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, false);
                                ActionList.Add(action);
                            }
                            break;
                        case 1:
                            {
                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;

                                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                                if (damage == 0) return;
                                PoisonTarget(Target, 6, 5, PoisonType.Slow, 2000);
                                PoisonTarget(Target, 9, 5, PoisonType.Frozen, 2000);

                                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, true);
                                ActionList.Add(action);
                            }
                            break;
                        case 2:
                            {
                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;

                                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                                if (damage == 0) return;
                                PoisonTarget(Target, 0, 5, PoisonType.Slow, 1000);

                                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, true);
                                ActionList.Add(action);
                            }
                            break;
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

