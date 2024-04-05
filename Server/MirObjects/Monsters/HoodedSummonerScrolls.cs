using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    /// <summary>
    /// Contains AI's for all of the HoodedSummoner's scrolls (CallScroll / PoisonScroll / FireballScroll / LightningScroll).
    /// Set AI for all Scrolls to this AI and set the Effect to case number based on which scroll you want.
    /// 
    /// CallScroll = 0
    /// PoisonScroll = 1
    /// FireballScroll = 2
    /// LightningScroll = 3
    /// 
    /// </summary>

    class HoodedSummonerScrolls : ZumaMonster
    {
        public long FearTime;

        protected internal HoodedSummonerScrolls(MonsterInfo info)
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
            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            if (damage == 0) return;


            switch (Info.Effect)
            {
                case 0: // CallScroll - FireWall attack?
                    switch (Envir.Random.Next(2))
                    {
                        case 0:
                            {
                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                                DelayedAction callScrollAction = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.MACAgility);
                                ActionList.Add(callScrollAction);
                            }
                            break;
                        case 1:
                            {
                                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                                SpawnSlaves();
                            }
                            break;
                    }
                    break;
                case 1: // PoisonScroll - PoisonCloud + PoisonExplosion on death.
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                    DelayedAction poisonScrollAction = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 500, Target, damage, DefenceType.MACAgility, true);
                    ActionList.Add(poisonScrollAction);
                    break;
                case 2: // FireballScroll - Projectile Attack (D16).
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });

                    int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step

                    DelayedAction fireballScrollAction = new DelayedAction(DelayedType.Damage, Envir.Time + delay, Target, damage, DefenceType.MACAgility);
                    ActionList.Add(fireballScrollAction);
                    break;
                case 3: // LightningScroll - ThunderBolt Attack.
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });
                    DelayedAction lightningScrollAction = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.MACAgility);
                    ActionList.Add(lightningScrollAction);
                    break;
                default:
                    base.Attack();
                    break;
            }
        }

        protected override void CompleteRangeAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            bool poisonAttack = (bool)data[3];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            List<MapObject> targets = FindAllTargets(1, target.CurrentLocation);
            if (targets.Count == 0) return;

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Attacked(this, damage, defence);
                PoisonTarget(targets[i], 7, 5, PoisonType.Green);
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

            if (dist >= Info.ViewRange)
                MoveTo(Target.CurrentLocation);
            else
            {
                MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                if (Walk(dir)) return;

                switch (Envir.Random.Next(2)) //No favour
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

        public override void Die()
        {
            switch (Info.Effect)
            {
                case 1:
                    ActionList.Add(new DelayedAction(DelayedType.Die, Envir.Time + 500));
                    break;
                case 0:
                case 2:
                case 3:
                    //default:
                    //base.Die();
                    break;
            }
            base.Die();
        }

        protected override void CompleteDeath(IList<object> data)
        {
            List<MapObject> targets = FindAllTargets(1, CurrentLocation, false);
            if (targets.Count == 0) return;

            for (int i = 0; i < targets.Count; i++)
            {
                int damage = GetAttackPower(Stats[Stat.MinSC], Stats[Stat.MaxSC]);
                if (damage == 0) return;

                if (targets[i].Attacked(this, damage, DefenceType.ACAgility) <= 0) continue;

                PoisonTarget(targets[i], 7, 5, PoisonType.Green, 2000);
            }
        }

        private void SpawnSlaves()
        {
            int count = Math.Min(1, 1 - SlaveList.Count);

            for (int i = 0; i < count; i++)
            {
                MonsterObject mob;

                {
                  mob = GetMonster(Envir.GetMonsterInfo(Settings.CallScrollMob));
                }

                if (mob == null) continue;

                if (!mob.Spawn(CurrentMap, Front))
                    mob.Spawn(CurrentMap, Target.CurrentLocation);

                mob.ActionTime = Envir.Time + 2000;
                SlaveList.Add(mob);
            }
        }
    }
}
