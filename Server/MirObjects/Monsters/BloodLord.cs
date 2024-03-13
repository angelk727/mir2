using System;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class BloodLord : MonsterObject
    {
        protected internal BloodLord(MonsterInfo info)
            : base(info)
        {
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap || Target.CurrentLocation == CurrentLocation)
            {
                return false;
            }

            int xDiff = Math.Abs(Target.CurrentLocation.X - CurrentLocation.X);
            int yDiff = Math.Abs(Target.CurrentLocation.Y - CurrentLocation.Y);
            int manhattanDistance = xDiff + yDiff;

            if (manhattanDistance > 2)
            {
                return false;
            }

            return (xDiff <= 1 && yDiff <= 1) || (xDiff == yDiff || ((xDiff + yDiff) & 1) == 0);
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

            ActionTime = Envir.Time;
            AttackTime = Envir.Time + AttackSpeed;

            switch (Envir.Random.Next(2))
            {
                case 0:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;
                        HalfmoonAttack(damage);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage * 2, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 1:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;
                        TriangleAttack(damage, 3, 2, 500, DefenceType.ACAgility, false);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 600, Target, damage * 2, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
            }
        }
    }
}