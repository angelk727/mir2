using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;

namespace Server.MirObjects
{
    public class ArcherHero : HeroObject
    {
        public ArcherHero(CharacterInfo info, PlayerObject owner) : base(info, owner) { }

        private bool HasRangedSpell => Info.Magics.Select(x => x.Spell).Intersect(Globals.RangedSpells).Any();

        public bool HasWeapon => Info.Equipment[(int)EquipmentSlot.武器] != null && Info.Equipment[(int)EquipmentSlot.武器].CurrentDura > 0;

        public bool HasClassWeapon
        {
            get
            {
                var classweapon = Info.Equipment[(int)EquipmentSlot.武器];
                return classweapon != null && classweapon.Info.RequiredClass == RequiredClass.弓箭 && classweapon.CurrentDura > 0;
            }
        }

        private static Point GetRandomPointAround(int distance, Point center)
        {
            int randomDistance = new Random().Next(1, distance);
            double randomAngle = new Random().NextDouble() * 2 * Math.PI;
            int targetX = (int)(center.X + (1 + randomDistance) * Math.Cos(randomAngle));
            int targetY = (int)(center.Y + (1 + randomDistance) * Math.Sin(randomAngle));
            return new Point(targetX, targetY);
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;

            if (HasRangedSpell)
                return TargetDistance <= ViewRange;

            return Target.CurrentLocation != CurrentLocation && Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
        }

        protected override void ProcessFriend()
        {
            if (!HasBuff(BuffType.气流术))
            {
                UserMagic magic = GetMagic(Spell.Concentration);
                if (CanUseMagic(magic))
                {
                    BeginMagic(magic.Spell, Direction, ObjectID, CurrentLocation);
                    return;
                }
            }
        }

        protected override void ProcessAttack()
        {
            if (Target == null || Target.Dead) return;
            TargetDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            switch (Envir.Random.Next(4))
            {
                case 0:
                    UserMagic magic = GetMagic(Spell.PoisonShot);//毒魔闪
                    if (CanUseMagic(magic))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                    break;
                case 1:
                    magic = GetMagic(Spell.CrippleShot);//邪暴闪
                    if (CanUseMagic(magic))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                    break;
                case 2:
                    magic = GetMagic(Spell.StraightShot);//天日闪
                    if (CanUseMagic(magic))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                    break;
                case 3:
                    magic = GetMagic(Spell.ElementalShot);//万金闪
                    if (CanUseMagic(magic))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                    break;
            }
        }

        protected override void ProcessTarget()
        {

            if (HasClassWeapon && CanCast && NextMagicSpell != Spell.None)
            {
                Magic(NextMagicSpell, NextMagicDirection, NextMagicTargetID, NextMagicLocation);
                NextMagicSpell = Spell.None;
            }

            if (Target == null || !CanAttack) return;

            if ((!HasWeapon || (HasWeapon && !HasClassWeapon)))
            {
                if (TargetDistance >= 1 && InAttackRange())
                {
                    Attack();

                    if (Target.Dead)
                    {
                        FindTarget();
                        return;
                    }
                }
                else
                {
                    MoveTo(Target.CurrentLocation);
                    return;
                }
            }

            if (HasClassWeapon && TargetDistance < 2)
            {
                Point randomPoint = GetRandomPointAround(6, CurrentLocation);
                MoveTo(randomPoint);
            }

            if (Target != null && HasClassWeapon && !HasRangedSpell)
            {
                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                RangeAttack(Direction, Target.CurrentLocation, Target.ObjectID);

                if (Target.Dead)
                {
                    FindTarget();
                }
                return;
            }

            AttackTime = Envir.Time + AttackSpeed;
            ActionTime = Envir.Time + 1200;
            RegenTime = Envir.Time + RegenDelay;
        }
    }
}
