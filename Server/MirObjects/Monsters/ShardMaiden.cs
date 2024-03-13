using System;
using System.Collections.Generic;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class ShardMaiden : MonsterObject
    {

        protected internal ShardMaiden(MonsterInfo info)
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

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            bool ranged = CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);

            if (!ranged)
            {
                switch (Envir.Random.Next(3))
                {
                    case 0:
                        switch (Envir.Random.Next(3))
                        {
                            case 0:
                                {
                                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                    if (damage == 0) return;

                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                                    ActionList.Add(action);
                                }
                                break;
                            case 1:
                                {
                                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                    if (damage == 0) return;

                                    PoisonTarget(Target, 8, 6, PoisonType.Dazed, 1000);

                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                                    ActionList.Add(action);
                                }
                                break;
                            case 2:
                                {
                                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 0 });

                                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC] * 2);
                                    if (damage == 0) return;

                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                                    ActionList.Add(action);
                                }
                                break;
                        }
                        break;
                    case 1:
                        switch (Envir.Random.Next(2))
                        {
                            case 0:
                                {
                                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });

                                    int damage = GetAttackPower(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
                                    if (damage == 0) return;

                                    WideLineAttack(damage, 5, 500, DefenceType.ACAgility, false, 3);
                                    PoisonTarget(Target, 9, 5, PoisonType.Frozen, 1000);

                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 300, Target, damage, DefenceType.ACAgility);
                                    ActionList.Add(action);
                                }
                                break;
                            case 1:
                                {
                                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 1 });
                                    SpawnSlaves();
                                }
                                break;
                        }
                        break;
                    case 2:
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Type = 2 });

                            MirDirection jumpDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                            Point location;

                            for (int i = 0; i < 1; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);
                                if (!CurrentMap.ValidPoint(location)) return;
                            }

                            for (int i = 0; i < 2; i++)
                            {
                                location = Functions.PointMove(CurrentLocation, jumpDir, 1);

                                CurrentMap.GetCell(CurrentLocation).Remove(this);
                                RemoveObjects(jumpDir, 1);
                                CurrentLocation = location;
                                CurrentMap.GetCell(CurrentLocation).Add(this);
                                AddObjects(jumpDir, 1);

                                int damage = Stats[Stat.MaxDC];

                                if (damage > 0)

                                    WideLineAttack(damage, 6, 500, DefenceType.ACAgility, false, 2);

                                {
                                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, location, damage, DefenceType.AC);
                                    CurrentMap.ActionList.Add(action);
                                }
                            }
                        }
                        break;
                }
            }
            ShockTime = 0;
            ActionTime = Envir.Time + 500;
            AttackTime = Envir.Time + AttackSpeed;
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

            MoveTo(Target.Front);
        }

        protected override void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            bool aoe = data.Count >= 4 && (bool)data[3];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            if (aoe)
            {
                var targets = FindAllTargets(2, CurrentLocation, false);

                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].Attacked(this, damage, defence);
                }
            }
            else
            {
                target.Attacked(this, damage, defence);
            }
        }

        private void SpawnSlaves()
        {
            var hpPercent = (HP * 100) / Stats[Stat.HP];

            if (hpPercent < 50)
            {
                int count = Math.Min(4, 4 - SlaveList.Count);

                for (int i = 0; i < count; i++)
                {
                    MonsterObject mob = null;

                    switch (Envir.Random.Next(4))
                    {
                        case 0:
                            mob = GetMonster(Envir.GetMonsterInfo(Settings.ShardMaidenMob1));
                            break;
                        case 1:
                            mob = GetMonster(Envir.GetMonsterInfo(Settings.ShardMaidenMob2));
                            break;
                        case 2:
                            mob = GetMonster(Envir.GetMonsterInfo(Settings.ShardMaidenMob3));
                            break;
                        case 3:
                            mob = GetMonster(Envir.GetMonsterInfo(Settings.ShardMaidenMob4));
                            break;
                    }

                    if (mob == null) continue;

                    if (!mob.Spawn(CurrentMap, Front))
                        mob.Spawn(CurrentMap, Target.CurrentLocation);

                    mob.ActionTime = Envir.Time + 2000;
                    SlaveList.Add(mob);
                }
            }
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
