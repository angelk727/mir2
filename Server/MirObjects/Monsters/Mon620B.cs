using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon620B : MonsterObject
    {
        private byte _stage;
        public bool Stoned = true;

        protected override bool CanMove
        {
            get
            {
                return base.CanMove && !Stoned;
            }
        }
        protected override bool CanAttack
        {
            get
            {
                return base.CanAttack && !Stoned;
            }
        }

        protected internal Mon620B(MonsterInfo info) : base(info)
        {
        }
        protected override bool InAttackRange()
        {
            return CurrentMap == Target.CurrentMap && CanFly(Target.CurrentLocation) && Functions.InRange(CurrentLocation, Target.CurrentLocation, Info.ViewRange);
        }

        public override int Pushed(MapObject pusher, MirDirection dir, int distance)
        {
            return Stoned ? 0 : base.Pushed(pusher, dir, distance);
        }

        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true)
        {
            if (Stoned) return;

            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
        }
        public override Buff AddBuff(BuffType type, MapObject owner, int duration, Stats stats, bool refreshStats = true, bool updateOnly = false, params int[] values)
        {
            if (Stoned) return null;

            return base.AddBuff(type, owner, duration, stats, refreshStats, updateOnly, values);
        }

        public override bool IsFriendlyTarget(HumanObject ally)
        {
            if (Stoned) return false;

            return base.IsFriendlyTarget(ally);
        }

        //protected override void ProcessAI()
        //{
        //    if (Dead) return;

        //    if (Stats[Stat.HP] >= 4)
        //    {
        //        byte stage = (byte)(4 - (HP / (Stats[Stat.HP] / 4)));

        //        if (stage > _stage)
        //        {
        //            _stage = stage;
        //            Broadcast(GetInfo());
        //        }
        //    }

        //    base.ProcessAI();
        //}
        //protected override void ProcessAI()
        //{
        //    if (!Dead && Envir.Time > ActionTime)
        //    {
        //        bool stoned = !FindNearby(2);

        //        if (Stoned && !stoned)
        //        {
        //            Wake();
        //        }
        //    }

        //    base.ProcessAI();
        //}
        protected override void ProcessAI()
        {
            if (!Dead && Envir.Time > ActionTime)
            {
                bool stoned = !FindNearby(2);

                if (Stoned && !stoned)
                {
                    Wake();
                }

                if (Stats[Stat.HP] >= 5)
                {
                    byte stage = (byte)(5 - (HP / (Stats[Stat.HP] / 5)));

                    if (stage > _stage)
                    {
                        _stage = stage;
                        Broadcast(GetInfo());
                    }
                }
            }

            base.ProcessAI();
        }
        public void Wake()
        {
            if (!Stoned) return;

            Stoned = false;
            Broadcast(new S.ObjectShow { ObjectID = ObjectID });
            ActionTime = Envir.Time + 1000;
        }

        public override bool IsAttackTarget(MonsterObject attacker)
        {
            return !Stoned && base.IsAttackTarget(attacker);
        }
        public override bool IsAttackTarget(HumanObject attacker)
        {
            return !Stoned && base.IsAttackTarget(attacker);
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

        public override Packet GetInfo()
        {
            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = Info.Image,
                Direction = Direction,
                Effect = Info.Effect,
                AI = Info.AI,
                Light = Info.Light,
                Dead = Dead,
                Skeleton = Harvested,
                Poison = CurrentPoison,
                Hidden = Hidden,
                Extra = Stoned,
                ExtraByte = _stage,
            };
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
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            if (!ranged && Envir.Random.Next(2) > 0)
            {
                switch (Envir.Random.Next(5))
                {
                    case 0:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                            ActionList.Add(action);
                        }
                        break;
                    case 1:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            PoisonTarget(Target, 5, 10, PoisonType.Red, 1000);
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });
                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            FullmoonAttack(damage);
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 3:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 3 });
                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            FullmoonAttack(damage);
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                    case 4:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 4 });
                            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                            if (damage == 0) return;

                            FullmoonAttack(damage);
                            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                            ActionList.Add(action);
                        }
                        break;
                }
            }
            else
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = Target.ObjectID });

                int damage = GetAttackPower(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
                if (damage == 0) return;

                PoisonTarget(Target, 5, 7, PoisonType.Green, 1000);
                DelayedAction action = new DelayedAction(DelayedType.RangeDamage, Envir.Time + 300, Target, damage, DefenceType.MACAgility, false);
                ActionList.Add(action);
            }
        }
    }
}
    