using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon614B : MonsterObject
    {
        protected internal Mon614B(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            return CurrentMap == Target.CurrentMap &&
                   Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            if (!ranged)
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                        DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;

                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        DelayedAction action = new(DelayedType.Damage, Envir.Time + 600, Target, damage, defenceType, false);
                        ActionList.Add(action);
                        break;
                        }
                    case 1:
                        {
                        DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;
                        int pushDistance = Envir.Random.Next(2) == 0 ? -1 : 1;

                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        DelayedAction action = new(DelayedType.Damage, Envir.Time + 800, Target, damage, defenceType, false);
                        ActionList.Add(action);
                        break;
                        }
                    case 2:
                        {
                            DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;

                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new(DelayedType.Damage, Envir.Time + 600, Target, damage, defenceType, false);
                            ActionList.Add(action);
                            break;
                        }
                }
            }
            else
            {
                if (Envir.Random.Next(8) != 7)
                {
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 0 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;

                    DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 800, Target, damage, DefenceType.MACAgility, false);
                    ActionList.Add(action);
                }
                else
                {
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                    if (damage == 0) return;

                    PoisonTarget(target: Target, chanceToPoison: 7, poisonDuration: 5, poison: PoisonType.Green, poisonTickSpeed: 1000);
                    DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 800, Target, damage, DefenceType.MACAgility, false);
                    ActionList.Add(action);
                }
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
    }
}
