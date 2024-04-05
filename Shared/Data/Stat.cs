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
    背包负重 = 16,
    腕力负重 = 17,
    装备负重 = 18,
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
    暴击倍率 = 35,
    暴击伤害 = 36,

    最大防御数率 = 40,
    最大魔御数率 = 41,
    最大物理攻击数率 = 42,
    最大魔法攻击数率 = 43,
    最大道术攻击数率 = 44,
    攻击速度数率 = 45,
    生命值数率 = 46,
    法力值数率 = 47,
    吸血数率 = 48,

    经验增长数率 = 100,
    物品掉落数率 = 101,
    金币收益数率 = 102,
    采矿出矿数率 = 103,
    宝石成功数率 = 104,
    钓鱼成功数率 = 105,
    大师概率数率 = 106,
    技能熟练度倍率 = 107,
    攻击增伤 = 108,

    伴侣专享经验数率 = 120,
    师徒专享伤害数率 = 121,
    师徒专享经验数率 = 123,
    伤害降低数率 = 124,
    气功盾恢复数率 = 125,
    气功盾恢复生命值 = 126,
    法力值消耗数率 = 127,
    传送技法力消耗数率 = 128,
    Hero = 129,

    Unknown = 255
}