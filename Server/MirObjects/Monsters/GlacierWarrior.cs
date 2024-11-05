using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class GlacierWarrior : MonsterObject
    {

        protected internal GlacierWarrior(MonsterInfo info)
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
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            switch (Envir.Random.Next(2))
            {
                case 0:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        ThreeQuarterMoonAttack(damage);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                        ActionList.Add(action);
                    }
                    break;
                case 1:
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        Thrust(Target);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                        ActionList.Add(action);
                    }
                    break;
            }
        }

        private void Thrust(MapObject target)
        {
            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
            Point location;

            for (int i = 0; i < 2; i++)
            {
                location = Functions.PointMove(CurrentLocation, jumpDir, 1);
                if (!CurrentMap.ValidPoint(location)) return;

                CurrentMap.GetCell(CurrentLocation).Remove(this);
                RemoveObjects(jumpDir, 1);
                CurrentLocation = location;
                CurrentMap.GetCell(CurrentLocation).Add(this);
                AddObjects(jumpDir, 1);

                int damage = Stats[Stat.MaxDC];

                if (damage > 0)
                    LineAttack(damage, 3, 300);
                {
                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, location, damage, DefenceType.AC);
                    CurrentMap.ActionList.Add(action);
                }
            }

            Broadcast(new S.ObjectDashAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Distance = 1 });
        }
    }
}

