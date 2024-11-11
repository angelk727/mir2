using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;

namespace Server.MirObjects.Monsters
{
    public sealed class Mon581D : MonsterObject
    {
        protected override bool CanMove => false;
        public override bool Blocking => true;
        protected override bool CanAttack => false;
        protected override bool CanRegen => false;

        public override bool IsAttackTarget(HumanObject attacker)
        {
            return false;
        }

        public override bool IsAttackTarget(MonsterObject attacker)
        {
            return false;
        }

        public override bool Walk(MirDirection dir)
        {
            return false;
        }

        public override void Turn(MirDirection dir)
        {
        }

        private readonly Point[] _wallOffsets;

        internal Mon581D(MonsterInfo info) : base(info)
        {
            _wallOffsets = info.Effect switch
            {
                1 => new Point[]
                {
                    new Point(-3, -4),
                    new Point(-3, -3),
                    new Point(-3, -2),
                    new Point(-2, -4),
                    new Point(-2, -3),
                    new Point(-2, -2),
                    new Point(-2, -1),
                    new Point(-2, 0),
                    new Point(-1, -2),
                    new Point(-1, -1),
                    new Point(-1, 0),
                    new Point(-1, 1),
                    new Point(0, -1),
                    new Point(0, 1),
                    new Point(0, 2),
                    new Point(1, -1),
                    new Point(1, 0),
                    new Point(1, 1),
                    new Point(1, 2),
                    new Point(2, 0),
                    new Point(2, 1),
                    new Point(2, 2),
                    new Point(2, 3),
                    new Point(3, 1),
                    new Point(3, 2),
                    new Point(3, 3),
                },
                2 => new Point[]
                {
                    new Point(-3, 3),
                    new Point(-3, 2),
                    new Point(-3, 1),
                    new Point(-2, 3),
                    new Point(-2, 2),
                    new Point(-2, 1),
                    new Point(-2, 0),
                    new Point(-1, 2),
                    new Point(-1, 1),
                    new Point(-1, 0),
                    new Point(-1, -1),
                    new Point(0, -2),
                    new Point(0, -1),
                    new Point(0, 1),
                    new Point(1, 1),
                    new Point(1, 0),
                    new Point(1, -1),
                    new Point(1, -2),
                    new Point(1, -3),
                    new Point(2, 0),
                    new Point(2, -1),
                    new Point(2, -2),
                    new Point(2, -3),
                    new Point(2, -4),
                    new Point(3, -1),
                    new Point(3, -2),
                    new Point(3, -3),
                    new Point(3, -4),
                },
                4 => new Point[]
                {
                    new Point(-4, -5),
                    new Point(-4, -4),
                    new Point(-4, -3),
                    new Point(-4, -2),
                    new Point(-4, -1),
                    new Point(-3, -5),
                    new Point(-3, -4),
                    new Point(-3, -3),
                    new Point(-3, -2),
                    new Point(-3, -1),
                    new Point(-3, 0),
                    new Point(-2, -3),
                    new Point(-2, -2),
                    new Point(-2, -1),
                    new Point(-2, 0),
                    new Point(-1, -2),
                    new Point(-1, -1),
                    new Point(-1, 0),
                    new Point(-1, 1),
                    new Point(0, -1),
                    new Point(0, 1),
                    new Point(0, 2),
                    new Point(1, -1),
                    new Point(1, 0),
                    new Point(1, 1),
                    new Point(1, 2),
                    new Point(2, 0),
                    new Point(2, 1),
                    new Point(2, 2),
                    new Point(2, 3),
                    new Point(3, 1),
                    new Point(3, 2),
                    new Point(3, 3),
                    new Point(3, 4),
                    new Point(4, 1),
                    new Point(4, 2),
                    new Point(4, 3),
                    new Point(4, 4),
                    new Point(5, 2),
                    new Point(5, 3),
                    new Point(5, 4),
                },
                5 => new Point[]
                {
                    new Point(-5, 3),
                    new Point(-4, 4),
                    new Point(-4, 3),
                    new Point(-4, 2),
                    new Point(-4, 1),
                    new Point(-3, 3),
                    new Point(-3, 2),
                    new Point(-3, 1),
                    new Point(-2, 3),
                    new Point(-2, 2),
                    new Point(-2, 1),
                    new Point(-2, 0),
                    new Point(-1, 2),
                    new Point(-1, 1),
                    new Point(-1, 0),
                    new Point(-1, -1),
                    new Point(0, -2),
                    new Point(0, -1),
                    new Point(0, 2),
                    new Point(0, 1),
                    new Point(1, 1),
                    new Point(1, 0),
                    new Point(1, -1),
                    new Point(1, -2),
                    new Point(1, -3),
                    new Point(2, 0),
                    new Point(2, -1),
                    new Point(2, -2),
                    new Point(2, -3),
                    new Point(2, -4),
                    new Point(3, 0),
                    new Point(3, -1),
                    new Point(3, -2),
                    new Point(3, -3),
                    new Point(3, -4),
                    new Point(4, -5),
                    new Point(4, -4),
                    new Point(4, -3),
                    new Point(4, -2),
                    new Point(4, -1),
                    new Point(5, -2),
                    new Point(5, -3),
                    new Point(5, -4),
                    new Point(5, -5),
                },
                _ => Array.Empty<Point>()

            };

            Direction = MirDirection.Up;

            NameColour = Color.SteelBlue;
        }

        private void CreateWall(Map map, Point location)
        {
            if (map == null)
            {
                return;
            }

            foreach (var offset in _wallOffsets)
            {
                Point wallLocation = new Point(location.X + offset.X, location.Y + offset.Y);
                WallHelper.CreateWall(map, wallLocation, false);
            }
        }

        private void RemoveWall(Map map, Point location)
        {
            if (map == null)
            {
                return;
            }

            foreach (var offset in _wallOffsets)
            {
                Point wallLocation = new Point(location.X + offset.X, location.Y + offset.Y);
                WallHelper.RemoveWall(map, wallLocation);
            }
        }

        public override void Die()
        {
            base.Die();
            if (CurrentMap != null)
            {
                RemoveWall(CurrentMap, CurrentLocation);
            }
        }

        public override void Despawn()
        {
            base.Despawn();
            if (CurrentMap != null)
            {
                RemoveWall(CurrentMap, CurrentLocation);
            }
        }

        public override void Spawned()
        {
            base.Spawned();
            if (CurrentMap != null)
            {
                CreateWall(CurrentMap, CurrentLocation);
            }
        }

        public class WallHelper
        {
            public static void CreateWall(Map map, Point location, bool isHighWall)
            {
                if (map == null || location.X < 0 || location.Y < 0 || location.X >= map.Width ||
                    location.Y >= map.Height)
                {
                    return;
                }

                Cell cell = map.GetCell(location);
                if (cell == null) return;

                cell.Attribute = isHighWall ? CellAttribute.HighWall : CellAttribute.LowWall;
            }

            public static void RemoveWall(Map map, Point location)
            {
                if (map == null || location.X < 0 || location.Y < 0 || location.X >= map.Width ||
                    location.Y >= map.Height)
                {
                    return;
                }

                Cell cell = map.GetCell(location);
                if (cell == null) return;

                cell.Attribute = CellAttribute.Walk;
            }
        }
    }
}
