public class BaseStats
{
    public MirClass Job;
    public List<BaseStat> Stats = new List<BaseStat>();

    public Stats Caps = new Stats();

    public BaseStats(MirClass job)
    {
        Job = job;

        switch (job)
        {
            #region 战士
            case MirClass.战士:
                Stats.Add(new BaseStat(Stat.HP) { FormulaType = StatFormula.Health, Base = 14, Gain = 4F, GainRate = 4.5F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MP) { FormulaType = StatFormula.Mana, Base = 11, Gain = 3.5F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.背包负重) { FormulaType = StatFormula.Weight, Base = 50, Gain = 3F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.装备负重) { FormulaType = StatFormula.Weight, Base = 15, Gain = 20F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.腕力负重) { FormulaType = StatFormula.Weight, Base = 12, Gain = 13F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinAC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 0, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxAC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 5, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 5, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.敏捷) { FormulaType = StatFormula.Stat, Base = 15, Gain = 0, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.准确) { FormulaType = StatFormula.Stat, Base = 5, Gain = 0, GainRate = 0F, Max = 0 });
                break;
            #endregion
            #region 法师
            case MirClass.法师:
                Stats.Add(new BaseStat(Stat.HP) { FormulaType = StatFormula.Health, Base = 14, Gain = 15F, GainRate =1.8F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MP) { FormulaType = StatFormula.Mana, Base = 13, Gain = 5F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.背包负重) { FormulaType = StatFormula.Weight, Base = 50, Gain = 5F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.装备负重) { FormulaType = StatFormula.Weight, Base = 15, Gain = 100F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.腕力负重) { FormulaType = StatFormula.Weight, Base = 12, Gain = 90F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinMC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxMC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.敏捷) { FormulaType = StatFormula.Stat, Base = 15, Gain = 0, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.准确) { FormulaType = StatFormula.Stat, Base = 5, Gain = 0, GainRate = 0F, Max = 0 });
                break;
            #endregion
            #region 道士
            case MirClass.道士:
                Stats.Add(new BaseStat(Stat.HP) { FormulaType = StatFormula.Health, Base = 14, Gain = 6F, GainRate = 2.5F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MP) { FormulaType = StatFormula.Mana, Base = 13, Gain = 8F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.背包负重) { FormulaType = StatFormula.Weight, Base = 50, Gain = 4F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.装备负重) { FormulaType = StatFormula.Weight, Base = 15, Gain = 50F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.腕力负重) { FormulaType = StatFormula.Weight, Base = 12, Gain = 42F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinMAC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 12, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxMAC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 6, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinSC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxSC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 7, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.敏捷) { FormulaType = StatFormula.Stat, Base = 18, Gain = 0, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.准确) { FormulaType = StatFormula.Stat, Base = 5, Gain = 0, GainRate = 0F, Max = 0 });
                break;
            #endregion
            #region 刺客
            case MirClass.刺客:
                Stats.Add(new BaseStat(Stat.HP) { FormulaType = StatFormula.Health, Base = 14, Gain = 4F, GainRate = 3.25F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MP) { FormulaType = StatFormula.Mana, Base = 11, Gain = 5F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.背包负重) { FormulaType = StatFormula.Weight, Base = 50, Gain = 3.5F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.装备负重) { FormulaType = StatFormula.Weight, Base = 15, Gain = 33F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.腕力负重) { FormulaType = StatFormula.Weight, Base = 12, Gain = 30F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 8, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 8, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.敏捷) { FormulaType = StatFormula.Stat, Base = 20, Gain = 0, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.准确) { FormulaType = StatFormula.Stat, Base = 5, Gain = 0, GainRate = 0F, Max = 0 });
                break;
            #endregion
            #region 弓箭
            case MirClass.弓箭:
                Stats.Add(new BaseStat(Stat.HP) { FormulaType = StatFormula.Health, Base = 14, Gain = 4F, GainRate = 3.25F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MP) { FormulaType = StatFormula.Mana, Base = 11, Gain = 4F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.背包负重) { FormulaType = StatFormula.Weight, Base = 50, Gain = 4F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.装备负重) { FormulaType = StatFormula.Weight, Base = 15, Gain = 33F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.腕力负重) { FormulaType = StatFormula.Weight, Base = 12, Gain = 30F, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 8, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxDC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 8, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MinMC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 8, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.MaxMC) { FormulaType = StatFormula.Stat, Base = 0, Gain = 8, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.敏捷) { FormulaType = StatFormula.Stat, Base = 15, Gain = 0, GainRate = 0F, Max = 0 });
                Stats.Add(new BaseStat(Stat.准确) { FormulaType = StatFormula.Stat, Base = 8, Gain = 0, GainRate = 0F, Max = 0 });
                break;
                #endregion
        }

        Caps[Stat.魔法躲避] = 2;
        Caps[Stat.毒物躲避] = 6;
        Caps[Stat.暴击倍率] = 18;
        Caps[Stat.暴击伤害] = 10;
        Caps[Stat.冰冻伤害] = 6;
        Caps[Stat.毒素伤害] = 6;
        Caps[Stat.生命恢复] = 8;
        Caps[Stat.法力恢复] = 8;
        Caps[Stat.中毒恢复] = 6;
    }

    public BaseStats(BinaryReader reader)
    {
        var count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
        {
            Stats.Add(new BaseStat((Stat)reader.ReadByte())
            {
                FormulaType = (StatFormula)reader.ReadByte(),
                Base = reader.ReadInt32(),
                Gain = reader.ReadSingle(),
                GainRate = reader.ReadSingle(),
                Max = reader.ReadInt32()
            });
        }

        Caps = new Stats(reader);
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Stats.Count);

        foreach (var stat in Stats)
        {
            writer.Write((byte)stat.Type);
            writer.Write((byte)stat.FormulaType);
            writer.Write(stat.Base);
            writer.Write(stat.Gain);
            writer.Write(stat.GainRate);
            writer.Write(stat.Max);
        }

        Caps.Save(writer);
    }
}

public class BaseStat
{
    public StatFormula FormulaType;
    public Stat Type;
    public int Base;
    public float Gain;
    public float GainRate;
    public int Max;

    public BaseStat(Stat type)
    {
        Type = type;
    }

    public int Calculate(MirClass job, int level)
    {
        if (Gain == 0) return Base;

        if (FormulaType == StatFormula.Health)
        {
            return job switch
            {
                MirClass.战士 => (int)Math.Min(Max > 0 ? Max : int.MaxValue, Base + (level / Gain + GainRate + level / 20F) * level),
                _ => (int)Math.Min(Max > 0 ? Max : int.MaxValue, Base + (level / Gain + GainRate) * level),
            };
        }
        else if (FormulaType == StatFormula.Mana)
        {
            return job switch
            {
                MirClass.法师 => (int)Math.Min(Max > 0 ? Max : int.MaxValue, Base + ((level / Gain + 2F) * 2.2F * level) + (level * GainRate)),
                MirClass.道士 => (int)Math.Min(Max > 0 ? Max : int.MaxValue, (Base + level / Gain * 2.2F * level) + (level * GainRate)),
                _ => (int)Math.Min(Max > 0 ? Max : int.MaxValue, Base + (level * Gain) + (level * GainRate)),
            };
        }
        else
        {
            return FormulaType switch
            {
                StatFormula.Weight => (int)Math.Min(Max > 0 ? Max : int.MaxValue, Base + ((level / Gain) * level)),
                _ => (int)Math.Min(Max > 0 ? Max : int.MaxValue, Base + (level / Gain)),
            };
        }  
    }
}