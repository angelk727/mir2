using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;

namespace Server.MirObjects
{
    public class ArcherHero : HeroObject
    {
        private static readonly Random Random = new Random();
        private static int GetRandomInt(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

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
            int randomDistance = GetRandomInt(1, distance);
            double randomAngle = GetRandomInt(0, 360) * (Math.PI / 180);
            int targetX = (int)(center.X + (1 + randomDistance) * Math.Cos(randomAngle));
            int targetY = (int)(center.Y + (1 + randomDistance) * Math.Sin(randomAngle));
            return new Point(targetX, targetY);
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;

            if (HasClassWeapon && HasRangedSpell)
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
            if (Target == null || Target.Dead)
            {
                FindTarget();
                if (Target == null || Target.Dead) return;
            }
            TargetDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (!CanCast) return;
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UserMagic magic = null;

            switch (Envir.Random.Next(4))
            {
                case 0:
                    magic = GetMagic(Spell.PoisonShot);
                    if (CanUseMagic(magic) && !Target.PoisonList.Any(p => p.PType == PoisonType.Green) && !HasBuff(BuffType.毒魔闪))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                    break;
                case 1:
                    magic = GetMagic(Spell.CrippleShot);
                    if (CanUseMagic(magic))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                    break;
                case 2:
                    magic = GetMagic(Spell.StraightShot);
                    if (CanUseMagic(magic))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                    break;
                case 3:
                    if (GetElementalOrbCount() < 1 || GetElementalOrbCount() > 3)
                    {
                        magic = GetMagic(Spell.ElementalShot);
                        if (CanUseMagic(magic))
                        {
                            BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                            return;
                        }
                    }
                    break;
            }
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (!HasWeapon || (HasWeapon && !HasClassWeapon))
            {
                MeleeInAttackRange();
                return;
            }

            if (NextMagicSpell != Spell.None)
            {
                RemoteSpellInAttackRange();
                return;
            }

            if (HasClassWeapon && TargetDistance < 2)
            {
                MoveTo(GetRandomPointAround(6, CurrentLocation));
                UpdateTimers();
                return;
            }

            RemoteInAttackRange();
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            AttackTime = Envir.Time + AttackSpeed;
            ActionTime = Envir.Time + 1200;
            RegenTime = Envir.Time + RegenDelay;
        }

        private void MeleeInAttackRange()
        {
            if (InAttackRange())
            {
                Attack();
                if (Target != null && Target.Dead)
                {
                    FindTarget();
                }
            }
            else
            {
                MoveTo(Target.CurrentLocation);
            }
        }

        private void RemoteInAttackRange()
        {
            if (Target == null || !CanAttack) return;

            if (HasClassWeapon && CanCast && NextMagicSpell == Spell.None)
            {
                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                RangeAttack(Direction, Target.CurrentLocation, Target.ObjectID);

                if (Target != null && Target.Dead && TargetDistance > ViewRange)
                {
                    FindTarget();
                }
                else
                {
                    MoveTo(Target.CurrentLocation);
                }
            }
        }

        private void RemoteSpellInAttackRange()
        {
            if (Target == null || !CanAttack) return;

            if (HasClassWeapon && CanCast && NextMagicSpell != Spell.None)
            {
                Magic(NextMagicSpell, NextMagicDirection, NextMagicTargetID, NextMagicLocation);
                NextMagicSpell = Spell.None;

                if (Target != null && Target.Dead && TargetDistance > ViewRange)
                {
                    FindTarget();
                }
                else
                {
                    MoveTo(Target.CurrentLocation);
                }
            }
        }
    }
}