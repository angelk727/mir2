using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon354B : MonsterObject
    {
        private long _multiPointAreaAttackTime;

        protected internal Mon354B(MonsterInfo info)
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

            ShockTime = 0;
            ActionTime = Envir.Time + 600;
            AttackTime = Envir.Time + AttackSpeed;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
            bool ranged2 = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 2);

            if (Envir.Time > _multiPointAreaAttackTime)
            {
                _multiPointAreaAttackTime = Envir.Time + 8000;

                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                if (damage == 0) return;

                Point[] locations = MultiPointAreaAttack(3, true, 3, false, 1, false);

                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Locations = locations, Type = 0 });
                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 1200, damage, DefenceType.MAC, locations);
                ActionList.Add(action);
            }

            if (!ranged)
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            HalfmoonAttack(damage, 1200, DefenceType.AC);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage == 0) return;

                            ThreeQuarterMoonAttack(damage, 1200, DefenceType.ACAgility);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage <= 0) return;

                            TargetAreaAttack(Spell.Mon354BRock, damage, 3, 600, 3000, 1500, DefenceType.MAC);
                        }
                        break;

                }
            }
            else if (!ranged2)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                if (damage == 0) return;

                LineCharge(1);
                FullmoonAttack(damage, 600, DefenceType.ACAgility, -1, 3);
            }
            else
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage <= 0) return;

                            int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 60 + 1200;
                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + delay, Target, damage, DefenceType.MACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID, Type = 2 });

                            int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                            if (damage <= 0) return;

                            TargetAreaAttack(Spell.Mon354BRock, damage, 3, 600, 3000, 1500, DefenceType.MAC);

                            DelayedAction action = new(DelayedType.RangeDamage, Envir.Time + 1200, Target, damage, DefenceType.MACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            if (Envir.Time > _multiPointAreaAttackTime)
                            {
                                _multiPointAreaAttackTime = Envir.Time + 8000;

                                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                                if (damage == 0) return;

                                Point[] locations = MultiPointAreaAttack(5, false, 3, false, 0, false);

                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Locations = locations, Type = 0 });
                                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 1200, damage, DefenceType.MAC, locations);
                                ActionList.Add(action);
                            }
                        }
                        break;
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
