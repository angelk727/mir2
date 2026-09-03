public sealed class Stats : IEquatable<Stats>
{
    public SortedDictionary<Stat, int> Values { get; set; } = new SortedDictionary<Stat, int>();
    public int Count => Values.Sum(pair => Math.Abs(pair.Value));

    public int this[Stat stat]
    {
        get
        {
            return !Values.TryGetValue(stat, out int result) ? 0 : result;
        }
        set
        {
            if (value == 0)
            {
                if (Values.ContainsKey(stat))
                {
                    Values.Remove(stat);
                }

                return;
            }

            Values[stat] = value;
        }
    }

    public Stats() { }

    public Stats(Stats stats)
    {
        foreach (KeyValuePair<Stat, int> pair in stats.Values)
            this[pair.Key] += pair.Value;
    }

    public Stats(BinaryReader reader, int version = int.MaxValue, int customVersion = int.MaxValue)
    {
        int count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
            Values[(Stat)reader.ReadByte()] = reader.ReadInt32();
    }

    public void Add(Stats stats)
    {
        foreach (KeyValuePair<Stat, int> pair in stats.Values)
            this[pair.Key] += pair.Value;
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Values.Count);

        foreach (KeyValuePair<Stat, int> pair in Values)
        {
            writer.Write((byte)pair.Key);
            writer.Write(pair.Value);
        }
    }

    public void Clear()
    {
        Values.Clear();
    }

    public bool Equals(Stats other)
    {
        if (Values.Count != other.Values.Count) return false;

        foreach (KeyValuePair<Stat, int> value in Values)
            if (other[value.Key] != value.Value) return false;

        return true;
    }
}

public enum StatFormula : byte
{
    Health,
    Mana,
    Weight,
    Stat
}

public enum Stat : byte
{
    MinAC = 0,
    MaxAC = 1,
    MinMAC = 2,
    MaxMAC = 3,
    MinDC = 4,
    MaxDC = 5,
    MinMC = 6,
    MaxMC = 7,
    MinSC = 8,
    MaxSC = 9,

    准确 = 10,
    敏捷 = 11,
    HP = 12,
    MP = 13,
    攻击速度 = 14,
    幸运 = 15,
    背包重量 = 16,
    腕力 = 17,
    负重 = 18,
    反弹伤害 = 19,
    强度 = 20,
    神圣 = 21,
    冰冻伤害 = 22,
    毒素伤害 = 23,

    魔法躲避 = 30,
    毒物躲避 = 31,
    生命恢复 = 32,
    法力恢复 = 33,
    中毒恢复 = 34, //TODO - Should this be in seconds or milliseconds??？
    暴击率 = 35,
    暴击伤害 = 36,

    防御强化 = 40,
    魔法防御强化 = 41,
    攻击强化 = 42,
    魔法强化 = 43,
    道术强化 = 44,
    攻速强化 = 45,
    生命值强化 = 46,
    法力值强化 = 47,
    生命偷取 = 48,

    经验收益 = 100,
    掉落收益 = 101,
    金币收益 = 102,
    采矿收益 = 103,
    宝石收益 = 104,
    钓鱼收益 = 105,
    大师收益 = 106,
    技能熟练度收益 = 107,
    武器增伤 = 108,

    伴侣经验收益 = 120,
    师徒增伤收益 = 121,
    师徒经验收益 = 123,
    伤害减免 = 124,
    气功盾恢复百分比 = 125,
    气功盾恢复生命值 = 126,
    法力值消耗百分比 = 127,
    传送技能法力值消耗 = 128,
    Hero = 129,

    Unknown = 255
}