using Server.MirDatabase;

namespace Server.MirObjects.Monsters
{
    public class Mon380P : MonsterObject
    {
        protected override bool CanMove { get { return false; } }
        protected override bool CanAttack { get { return false; } }
        protected override bool CanRegen { get { return false; } }

        protected internal Mon380P(MonsterInfo info)
            : base(info)
        {
            if (info.Effect == 1)
            {
                Direction = MirDirection.UpRight;
            }
            else
            {
                Direction = MirDirection.Up;
            }
        }

        public override bool IsAttackTarget(MonsterObject attacker)
        {
            return false;
        }

        public override void Turn(MirDirection dir) { }

        public override bool Walk(MirDirection dir) { return false; }

        protected override void ProcessRoam() { }

        protected override void ProcessTarget() { }

        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true) { }

        public override int Attacked(HumanObject attacker, int damage, DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
        {
            int armour = 0;
            switch (type)
            {
                case DefenceType.ACAgility when Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]:
                    return 0;
                case DefenceType.ACAgility:
                case DefenceType.AC:
                    armour = GetDefencePower(Stats[Stat.MinAC], Stats[Stat.MaxAC]);
                    break;
                case DefenceType.MACAgility when Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]:
                    return 0;
                case DefenceType.MACAgility:
                case DefenceType.MAC:
                    armour = GetDefencePower(Stats[Stat.MinMAC], Stats[Stat.MaxMAC]);
                    break;
                case DefenceType.Agility when Envir.Random.Next(Stats[Stat.敏捷] + 1) > attacker.Stats[Stat.准确]:
                    return 0;
            }

            if (armour >= damage) return 0;

            if (damageWeapon)
                attacker.DamageWeapon();

            ShockTime = 0;

            int actualDamage = Math.Max(0, damage - armour);
            ChangeHP(-actualDamage);

            return actualDamage;
        }
    }
}
