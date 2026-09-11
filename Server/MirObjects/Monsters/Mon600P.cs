using Server.MirDatabase;
using Server.MirEnvir;
using System.Drawing;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class Mon600P : MonsterObject
    {
        protected override bool CanAttack { get { return false; } }

        private long _mon600PDropTime;

        protected internal Mon600P(MonsterInfo info)
            : base(info)
        {
        }

        protected override void ProcessTarget()
        {
            if (!CanMove || Target == null)
                return;

            MirDirection dir = FindEscapeDirection(Target);

            if (Envir.Random.Next(10) < 4)
            {
                if (Walk(dir))
                {
                    DropRunAwayCoin();
                    return;
                }
            }
            else
            {
                if (Run(dir))
                {
                    DropRunAwayCoin();
                    return;
                }
            }
        }

        private MirDirection FindEscapeDirection(MapObject target)
        {
            MirDirection escapeDir = Functions.DirectionFromPoint(target.CurrentLocation, CurrentLocation);

            MirDirection leftDir = Functions.PreviousDir(escapeDir);
            MirDirection rightDir = Functions.NextDir(escapeDir);

            int centerScore = GetEscapeScore(escapeDir);
            int leftScore = GetEscapeScore(leftDir);
            int rightScore = GetEscapeScore(rightDir);

            if (centerScore > 0 || leftScore > 0 || rightScore > 0)
            {
                if (leftScore > centerScore && leftScore >= rightScore) escapeDir = leftDir;

                else if (rightScore > centerScore && rightScore > leftScore) escapeDir = rightDir;

                return escapeDir;
            }

            MirDirection backDir = Functions.NextDir(Functions.NextDir(escapeDir));
            MirDirection backLeftDir = Functions.PreviousDir(backDir);
            MirDirection backRightDir = Functions.NextDir(backDir);

            int backScore = GetEscapeScore(backDir);
            int backLeftScore = GetEscapeScore(backLeftDir);
            int backRightScore = GetEscapeScore(backRightDir);

            if (backLeftScore > backScore && backLeftScore >= backRightScore) backDir = backLeftDir;

            else if (backRightScore > backScore && backRightScore > backLeftScore) backDir = backRightDir;

            return backDir;
        }

        private int GetEscapeScore(MirDirection dir)
        {
            int score = 0;

            for (int distance = 1; distance <= 5; distance++)
            {
                Point location = Functions.PointMove(CurrentLocation, dir, distance);

                if (!CurrentMap.ValidPoint(location))
                    break;

                score++;
            }

            return score;
        }

        private bool Run(MirDirection dir)
        {
            if (!CanMove)
                return false;

            int distance = 2;

            int chance = Envir.Random.Next(10);

            if (chance >= 5 && chance < 8) distance = 3;

            else if (chance >= 8) distance = 4;

            Point location = CurrentLocation;
            int moved = 0;

            for (int i = 0; i < distance; i++)
            {
                Point nextLocation = Functions.PointMove(location, dir, 1);

                if (!CurrentMap.ValidPoint(nextLocation))
                    break;

                location = nextLocation;
                moved++;
            }

            if (moved == 0)
                return false;

            CurrentMap.GetCell(CurrentLocation).Remove(this);

            Direction = dir;
            RemoveObjects(dir, moved);

            CurrentLocation = location;

            CurrentMap.GetCell(CurrentLocation).Add(this);
            AddObjects(dir, moved);

            Broadcast(new S.ObjectRun { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            return true;
        }

        private void DropRunAwayCoin()
        {
            if (Envir.Time < _mon600PDropTime)
                return;

            uint gold = ApplyGoldModifier((uint)Envir.Random.Next(10, 50));

            DropGold(gold);

            _mon600PDropTime = Envir.Time + Envir.Random.Next(2000, 5000);
        }
    }
}