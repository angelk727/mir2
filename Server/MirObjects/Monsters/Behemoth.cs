using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Behemoth : MonsterObject
    {
        public long FearTime;
        public byte AttackRange = 10;

        protected internal Behemoth(MonsterInfo info)
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

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            if (!ranged)
            {
                switch (Envir.Random.Next(5))
                {
                    case 0:
                    case 1:
                    case 2:
                        base.Attack(); //重击
                        break;
                    case 3:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility, true);
                            ActionList.Add(action);
                        }
                        break;
                    case 4:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, 0, DefenceType.ACAgility, false);
                            ActionList.Add(action);
                        }
                        break;
                }

                PoisonTarget(Target, 15, 5, PoisonType.Bleeding);
            }
            else
            {
                if (Envir.Random.Next(2) == 0)
                {
                    MoveTo(Target.CurrentLocation);
                }
                else
                {
                    switch (Envir.Random.Next(2))
                    {
                        case 0:
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            SpawnSlaves(); //召唤怪物
                            break;
                        case 1:
                            {
                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                                int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]) * 3;
                                if (damage == 0) return;

                                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.ACAgility);
                                ActionList.Add(action);


                            }
                            break;
                    }
                }

                ShockTime = 0;
                ActionTime = Envir.Time + 300;
                AttackTime = Envir.Time + AttackSpeed;
            }
        }

        protected override void CompleteRangeAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            List<MapObject> targets = FindAllTargets(AttackRange, CurrentLocation);
            if (targets.Count == 0) return;

            for (int i = 0; i < targets.Count; i++)
            {
                Broadcast(new S.ObjectEffect { ObjectID = targets[i].ObjectID, Effect = SpellEffect.Behemoth });

                if (targets[i].Attacked(this, damage, defence) <= 0) continue;

                PoisonTarget(targets[i], 15, 5, PoisonType.Paralysis, 1000);
            }
        }

        protected override void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            {
                List<MapObject> targets = FindAllTargets(1, CurrentLocation);
                if (targets.Count == 0) return;

                for (int i = 0; i < targets.Count; i++)
                        {
                            targets[i].Pushed(this, Functions.DirectionFromPoint(CurrentLocation, targets[i].CurrentLocation), 4);
                            PoisonTarget(targets[i], 3, 5, PoisonType.Dazed, 1000);
                        }
            }
        }

        private void SpawnSlaves()
        {
            List<MapObject> targets = FindAllTargets(10, CurrentLocation);

            int count = Math.Min(3, (targets.Count * 3) - SlaveList.Count);
            
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            for (int i = 0; i < count; i++)
            {
                MonsterObject mob = null;
                switch (Envir.Random.Next(4))
                {
                    case 0:
                        mob = GetMonster(Envir.GetMonsterInfo(Settings.BehemothMonster1));
                        break;
                    case 1:
                        mob = GetMonster(Envir.GetMonsterInfo(Settings.BehemothMonster2));
                        break;
                    case 2:
                        mob = GetMonster(Envir.GetMonsterInfo(Settings.BehemothMonster3));
                        break;
                }

                if (mob == null) continue;

                if (!mob.Spawn(CurrentMap, Front))
                    mob.Spawn(CurrentMap, CurrentLocation);
                
                mob.Target = targets[Envir.Random.Next(targets.Count)];
                mob.ActionTime = Envir.Time + 2000;
                SlaveList.Add(mob);
            }
            if (Envir.Random.Next(5) == 0)
            {
                TeleportRandom(10, AttackRange);
            }
            else
            {
                var hpPercent = (HP * 100) / Stats[Stat.HP];

                if (Envir.Random.Next(3) == 0 && hpPercent < 50)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;

                    ChangeHP(Stats[Stat.HP] / 4);
                }
             }
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (InAttackRange() && Envir.Time < FearTime)
            {
                Attack();
                return;
            }

            FearTime = Envir.Time + 5000;

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (dist >= AttackRange)
                MoveTo(Target.CurrentLocation);
            else
            {
                MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                if (Walk(dir)) return;

                switch (Envir.Random.Next(2)) //不赞同
                {
                    case 0:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.NextDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                    default:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.PreviousDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                }
                
            }
        }

        public override bool TeleportRandom(int attempts, int distance, Map temp = null)
        {
            if (Target == null) return false;

            for (int i = 0; i < attempts; i++)
            {
                Point location;

                location = new Point(Target.CurrentLocation.X + Envir.Random.Next(-distance, distance + 1),
                                          Target.CurrentLocation.Y + Envir.Random.Next(-distance, distance + 1));

                if (Teleport(CurrentMap, location, true, 12)) return true; //12为显示瞬移特效号
            }

            return false;
        }

        public override void Die()
        {
            foreach (var slave in SlaveList)
            {
                slave.Die();
            }

            base.Die();
        }
    }
}