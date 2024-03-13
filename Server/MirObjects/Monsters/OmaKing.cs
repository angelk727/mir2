using System.Collections.Generic;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class OmaKing : MonsterObject
    {
        protected virtual byte AttackRange => Info.ViewRange;

        protected internal OmaKing(MonsterInfo info)
            : base(info)
        {
        }

        private bool IsValidTarget(MapObject target)
        {
            return target != null && target.IsAttackTarget(this) && target.CurrentMap == CurrentMap && target.Node != null;
        }

        protected override bool InAttackRange()
        {
            return IsValidTarget(Target) && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        protected override void ProcessTarget()
        {
            if (CurrentMap.Players.Count == 0 || Target == null)
            {
                return;
            }

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

            base.ProcessTarget();
        }

        public override void Die()
        {
            base.Die();
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
            bool ranged = !Functions.InRange(CurrentLocation, Target.CurrentLocation, 2)
                || (Functions.InRange(CurrentLocation, Target.CurrentLocation, 2) && Envir.Random.Next(10) < 2);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged)
            {
                switch (Envir.Random.Next(2))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            FullmoonAttack(damage, 600, DefenceType.ACAgility, -1, 2);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        if (Envir.Random.Next(10) < 3)
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            FullmoonAttack(damage, 600, DefenceType.ACAgility, 1, 2);
                            LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 2);

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                List<MapObject> targets = FindAllTargets(Info.ViewRange, CurrentLocation);
                if (targets.Count == 0) return;

                int levelgap;
                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                if (damage == 0) return;

                int poisonChance = 30 + (targets.Count - 1) * 5;
                int addDamage = (targets.Count - 1) * 5;

                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].IsAttackTarget(this))
                    {
                        levelgap = 60 - targets[i].Level;
                        if (Envir.Random.Next(20) < 4 + levelgap)
                        {
                            if (Envir.Random.Next(Settings.MagicResistWeight) < targets[i].Stats[Stat.魔法躲避]) continue;
                            {
                                if (Envir.Random.Next(100) < poisonChance)
                                {
                                    PoisonTarget(targets[i], 1, 5, PoisonType.Paralysis, 1000, true);
                                }
                            }
                        }
                    }
                }

                damage += damage * addDamage / 100;
                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.MACAgility);
                ActionList.Add(action);
            }
        }
    }
}

