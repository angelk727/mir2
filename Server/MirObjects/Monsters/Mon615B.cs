using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon615B : MonsterObject
    {
        private long _mon615BShieldTime;
        protected internal Mon615B(MonsterInfo info)
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

                        HalfmoonAttack(damage, 300, defenceType);
                        DelayedAction action = new(DelayedType.Damage, Envir.Time + 600, Target, damage, defenceType, false);
                        ActionList.Add(action);
                        break;
                    }
                    case 1:
                        {
                            if (Envir.Time < _mon615BShieldTime + 90 * 1000) return;

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
                                    shieldTime = 35000;
                                    break;
                                case >= 20 and <= 30:
                                    stats[Stat.MaxAC] = 90;
                                    stats[Stat.MinAC] = 90;
                                    shieldTime = 40000;
                                    break;
                                default:
                                    return;
                            }

                            if (Target == null) return;

                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                            AddBuff(BuffType.Mon615BShield, this, shieldTime, stats);
                            _mon615BShieldTime = Envir.Time;
                            break;
                         }
                    case 2:
                        {
                            DefenceType defenceType = Envir.Random.Next(2) == 0 ? DefenceType.AC : DefenceType.ACAgility;

                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            FullmoonAttack(damage, 1200, defenceType, -1, 2);
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

        //private void Mon615BShield()
        //{
        //    if (Envir.Time < _mon615BShieldTime + 90 * 1000) return;

        //    var stats = new Stats();
        //    int shieldTime;
        //    switch (HealthPercent)
        //    {
        //        case > 60 and <= 80:
        //            stats[Stat.MaxAC] = 30;
        //            stats[Stat.MinAC] = 30;
        //            shieldTime = 30000;
        //            break;
        //        case > 30 and <= 50:
        //            stats[Stat.MaxAC] = 60;
        //            stats[Stat.MinAC] = 60;
        //            shieldTime = 35000;
        //            break;
        //        case >= 20 and <= 30:
        //            stats[Stat.MaxAC] = 90;
        //            stats[Stat.MinAC] = 90;
        //            shieldTime = 40000;
        //            break;
        //        default:
        //            return;
        //    }

        //    if (Target == null) return;

        //    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });
        //    AddBuff(BuffType.Mon615BShield, this, shieldTime, stats);
        //    _mon615BShieldTime = Envir.Time;
        //}
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
