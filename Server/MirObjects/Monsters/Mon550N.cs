using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon550N : MonsterObject
    {
        protected internal Mon550N(MonsterInfo info)
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

            switch (Envir.Random.Next(4))
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
                    if (Envir.Random.Next(10) < 3) return;
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                        if (damage == 0) return;

                        LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 1);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 2:
                    if (Envir.Random.Next(10) < 7) return;
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });
                        int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                        if (damage == 0) return;

                        LineAttack(damage, Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) + 1);

                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 1200, Target, damage, DefenceType.AC);
                        ActionList.Add(action);
                    }
                    break;
                case 3:
                    if (Envir.Random.Next(10) < 5) return;
                    {
                        SwordFormation();
                    }
                    break;
            }
        }

        private void SwordFormation()
        {
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

            int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MinDC] * 2);

            var start = 1000;
            var time = 1800;

            SpellObject ob = new SpellObject
            {
                Spell = Spell.SwordFormation,
                Value = damage,
                ExpireTime = Envir.Time + time + start,
                TickSpeed = 1000,
                Direction = Direction,
                CurrentMap = CurrentMap,
                CurrentLocation = CurrentLocation,
                CastLocation = CurrentLocation,
                Show = true,
                Caster = Owner
            };

            FullmoonAttack(damage, start, DefenceType.ACAgility, -1, 3);

            DelayedAction action = new DelayedAction(DelayedType.Spawn, Envir.Time + start, ob);
            CurrentMap.ActionList.Add(action);
        }
    }
}
    



    