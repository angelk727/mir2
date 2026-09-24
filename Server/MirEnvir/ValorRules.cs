namespace Server.MirEnvir
{
    public enum ValorTeam : byte { None, Red, Blue }

    // Pure arithmetic shared with the standalone regression checks.
    public static class ValorRules
    {
        public static int OwnershipPoints(bool sun, bool moon, bool lightning)
        {
            int count = (sun ? 1 : 0) + (moon ? 1 : 0) + (lightning ? 1 : 0);
            return (sun ? 15 : 0) + (moon ? 10 : 0) + (lightning ? 10 : 0)
                   + (count == 3 ? 50 : count == 2 ? 35 : 0);
        }

        public static ValorTeam Winner(int red, int blue)
        {
            return red == blue ? ValorTeam.None : red > blue ? ValorTeam.Red : ValorTeam.Blue;
        }

        public static int HonorAward(int personal, bool completed, bool won, int completion, int victory)
        {
            return (int)System.Math.Min(200000L, System.Math.Max(0L, personal)
                + (completed ? System.Math.Max(0, completion) : 0)
                + (completed && won ? System.Math.Max(0, victory) : 0));
        }

        public static int AddHonor(int current, int award)
        {
            return (int)System.Math.Min(200000L, System.Math.Max(0L, current) + System.Math.Max(0L, award));
        }
    }
}
