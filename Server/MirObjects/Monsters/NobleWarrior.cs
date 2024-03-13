using System.Collections.Generic;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class NobleWarrior : MonsterObject
    {
        public long BuffTime;
        protected internal NobleWarrior(MonsterInfo info)
            : base(info)
        {
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

            switch (Envir.Random.Next(4))
            {
                case 0:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 1:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 350, Target, damage * 2, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 2:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        LineAttack(damage, 3);
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                        ActionList.Add(action);
                    }
                    break;
                case 3:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        FullmoonAttack(damage);
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.ACAgility);
                        ActionList.Add(action);
                    }
                    break;
            }
        }

        protected override void ProcessTarget()
        {
            if (Envir.Time > BuffTime)
            {
                var friends = FindAllFriends(Info.ViewRange, CurrentLocation);

                if (friends.Count > 0)
                {
                    var friend = friends[Envir.Random.Next(friends.Count)];

                    int delay = Functions.MaxDistance(CurrentLocation, friend.CurrentLocation) * 50 + 500;

                    Direction = Functions.DirectionFromPoint(CurrentLocation, friend.CurrentLocation);

                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, TargetID = friend.ObjectID, Type = 0 });

                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + delay, friend, 0, DefenceType.MACAgility);

                    ActionList.Add(action);

                    BuffTime = Envir.Time + 20000;
                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;
                    ShockTime = 0;
                    return;
                }

                BuffTime = Envir.Time + 30000;
            }

            base.ProcessTarget();
        }

        protected override void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || target.CurrentMap != CurrentMap || target.Node == null) return;

            if (target.IsFriendlyTarget(this))
            {
                var friends = FindAllFriends(4, target.CurrentLocation);

                var min = Stats[Stat.MinMC];
                var max = Stats[Stat.MaxMC];

                for (int i = 0; i < friends.Count; i++)
                {
                    if (Info.Effect == 0)
                    {
                        var stats = new Stats { [Stat.MinDC] = min, [Stat.MaxDC] = max, [Stat.MinMC] = min, [Stat.MaxMC] = max };
                        friends[i].AddBuff(BuffType.至尊威严, this, Settings.Second * 10, stats);
                    }
                    else if (Info.Effect == 1)
                    {
                        var stats = new Stats { [Stat.MinAC] = min, [Stat.MaxAC] = max, [Stat.MinMAC] = min, [Stat.MaxMAC] = max };
                        friends[i].AddBuff(BuffType.寒冰护甲, this, Settings.Second * 10, stats);
                    }

                    friends[i].OperateTime = 0;
                }
            }
            else if (target.IsAttackTarget(this))
            {
                target.Attacked(this, damage, defence);
            }
        }
    }
}