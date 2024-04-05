using System.Collections.Generic;
using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class CityGate : MonsterObject
    {
        protected override bool CanMove { get { return false; } }
        protected override bool CanAttack { get { return false; } }
        protected override bool CanRegen { get { return false; } }

        protected List<BlockingObject> BlockingObjects = new List<BlockingObject>();

        public Point[] BlockArray;

        protected internal CityGate(MonsterInfo info)
            : base(info)
        {
            switch (info.Effect)
            {
                case 2: //燃烧昆仑大门
                    BlockArray = new Point[]
                    {
                        new Point(0, -1),
                        new Point(2, -2),
                        new Point(2, -3),
                        new Point(3, -3),
                        new Point(3, -4),
                        new Point(4, -4),
                        new Point(4, -5),
                        new Point(5, -4),
                        new Point(-2, 2),
                        new Point(-3, 2),
                        new Point(-3, 3),
                        new Point(-4, 3),
                        new Point(-4, 4)
                    };
                    break;
                case 3: //地下通道门1
                    BlockArray = new Point[]
                    {
                        new Point(1, 1),
                        new Point(1, 0),
                        new Point(0, -1),
                        new Point(-1, -1),
                        new Point(-1, -2),
                    };
                    break;
                case 4: //地下通道门2
                    BlockArray = new Point[]
                    {
                        new Point(1, -1),
                        new Point(1, -2),
                        new Point(0, -1),
                        new Point(-1, 1),
                        new Point(-1, 0),
                        new Point(-1, 2),
                    };
                    break;
            }

            Direction = MirDirection.Up;
        }

        protected override void Attack() { }
        protected override void FindTarget() { }
        public override void Spawned()
        {
            base.Spawned();

            if (BlockArray == null) return;

            MonsterInfo bInfo = new MonsterInfo
            {
                Image = Monster.EvilMirBody,
                CanTame = false,
                CanPush = false,
                AutoRev = false
            };

            bInfo.Stats[Stat.HP] = this.Stats[Stat.HP];

            foreach (var block in BlockArray)
            {
                BlockingObject b = new BlockingObject(this, bInfo);
                BlockingObjects.Add(b);

                if (!b.Spawn(this.CurrentMap, new Point(this.CurrentLocation.X + block.X, this.CurrentLocation.Y + block.Y)))
                {
                    MessageQueue.EnqueueDebugging(string.Format("{3} 挡住怪物不能刷在 {0} {1}:{2}", CurrentMap.Info.FileName, block.X, block.Y, Info.Name));
                }
            }
        }
        public override void Turn(MirDirection dir)
        {
        }
        public override bool Walk(MirDirection dir)
        {
            return false;
        }

        protected override void ProcessTarget() { }

        protected override void ProcessRegen() { }
        protected override void ProcessSearch() { }
        protected override void ProcessRoam() { }

        public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility)
        {
            int armour = 0;

            switch (type)
            {
                case DefenceType.ACAgility:
                    if (Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]) return 0;
                    armour = GetAttackPower(Stats[Stat.MinAC], Stats[Stat.MaxAC]);
                    break;
                case DefenceType.AC:
                    armour = GetAttackPower(Stats[Stat.MinAC], Stats[Stat.MaxAC]);
                    break;
                case DefenceType.MACAgility:
                    if (Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]) return 0;
                    armour = GetAttackPower(Stats[Stat.MinMAC], Stats[Stat.MaxMAC]);
                    break;
                case DefenceType.MAC:
                    armour = GetAttackPower(Stats[Stat.MinMAC], Stats[Stat.MaxMAC]);
                    break;
                case DefenceType.Agility:
                    if (Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]) return 0;
                    break;
            }

            if (armour >= damage) return 0;

            ShockTime = 0;

            if (attacker.Info.AI == 980)
                EXPOwner = null;
            else if (attacker.Master != null)
            {
                if (EXPOwner == null || EXPOwner.Dead)
                    EXPOwner = attacker.Master switch
                    {
                        HeroObject hero => hero.Owner,
                        _ => attacker.Master
                    };

                if (EXPOwner == attacker.Master)
                    EXPOwnerTime = Envir.Time + EXPOwnerDelay;

            }

            Broadcast(new S.ObjectStruck { ObjectID = ObjectID, AttackerID = attacker.ObjectID, Direction = Direction, Location = CurrentLocation });

            ChangeHP(-1);
            return 1;

        }

        public override int Struck(int damage, DefenceType type = DefenceType.ACAgility)
        {
            return 0;
        }

        public override int Attacked(HumanObject attacker, int damage, DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
        {
            int armour = 0;

            switch (type)
            {
                case DefenceType.ACAgility:
                    if (Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]) return 0;
                    armour = GetAttackPower(Stats[Stat.MinAC], Stats[Stat.MaxAC]);
                    break;
                case DefenceType.AC:
                    armour = GetAttackPower(Stats[Stat.MinAC], Stats[Stat.MaxAC]);
                    break;
                case DefenceType.MACAgility:
                    if (Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]) return 0;
                    armour = GetAttackPower(Stats[Stat.MinMAC], Stats[Stat.MaxMAC]);
                    break;
                case DefenceType.MAC:
                    armour = GetAttackPower(Stats[Stat.MinMAC], Stats[Stat.MaxMAC]);
                    break;
                case DefenceType.Agility:
                    if (Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]) return 0;
                    break;
            }

            if (armour >= damage) return 0;

            if (damageWeapon)
                attacker.DamageWeapon();

            ShockTime = 0;

            if (Master != null && Master != attacker)
                if (Envir.Time > Master.BrownTime && Master.PKPoints < 200)
                    attacker.BrownTime = Envir.Time + Settings.Minute;

            if (EXPOwner == null || EXPOwner.Dead)
                EXPOwner = GetAttacker(attacker);

            if (EXPOwner == attacker)
                EXPOwnerTime = Envir.Time + EXPOwnerDelay;

            Broadcast(new S.ObjectStruck { ObjectID = ObjectID, AttackerID = attacker.ObjectID, Direction = Direction, Location = CurrentLocation });
            attacker.GatherElement();
            ChangeHP(-1);

            return 1;
        }

        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true) { }

        protected virtual void ActiveDoorWall(bool closed)
        {
            foreach (var obj in BlockingObjects)
            {
                if (obj == null) continue;
                if (closed)
                    obj.Show();
                else
                    obj.Hide();
            }
        }
        public override void Die()
        {
            ActiveDoorWall(false);
            base.Die();
        }
    }
}
