using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon544S : MonsterObject
    {
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

        protected internal Mon544S(MonsterInfo info) : base(info)
        {
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

        protected override void ProcessAI()
        {
            if (!Dead && Envir.Time > ActionTime)
            {
                bool stoned = !FindNearby(2);

                if (Stoned && !stoned)
                {
                    Wake();
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
            if (CurrentMap.Players.Count == 0 || Target == null)
                return;

            Point targetLocation = Target.CurrentLocation;
            if (Envir.Time >= ShockTime)
            {
                if (Functions.InRange(CurrentLocation, targetLocation, 1))
                {
                    if (CanAttack)
                        Attack();

                    return;
                }

                MoveTo(targetLocation);
            }
            else
                Target = null;
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

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            switch (Envir.Random.Next(6))
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
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                        if (damage == 0) return;

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 2:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                        if (damage == 0) return;

                        PoisonTarget(Target, 3, 5, PoisonType.Bleeding, 1000);
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 3:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;
                        LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 1);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 4:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;
                        LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 2);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                        ActionList.Add(action);
                    }
                    break;
                case 5:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                        if (damage == 0) return;
                        LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 2);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.ACAgility);
                        ActionList.Add(action);
                    }
                    break;
            }
        }
    }
}
    