using Server.MirEnvir;
using Server.MirObjects;

namespace Server.MirDatabase
{
    public class BuffInfo
    {
        public BuffType Type { get; set; }
        public BuffStackType StackType { get; set; } = BuffStackType.None;
        public BuffProperty Properties { get; set; } = BuffProperty.None;
        public int Icon { get; set; }
        public bool Visible { get; set; }

        public static List<BuffInfo> Load()
        {
            List<BuffInfo> info = new List<BuffInfo>
            {
                // Magics
                new BuffInfo { Type = BuffType.时间之殇, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.隐身术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.体迅风, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.轻身步, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.血龙剑法, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.幽灵盾, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.神圣战甲术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.风身术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.无极真气, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.护身气幕, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.剑气爆, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo
                {
                    Type = BuffType.诅咒术,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.Debuff,
                    StackType = BuffStackType.ResetDuration,
                    Visible = true
                },
                new BuffInfo { Type = BuffType.月影术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.烈火身, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.气流术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.吸血地闪, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.毒魔闪, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.天务, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.精神状态, StackType = BuffStackType.Infinite, Visible = true },
                new BuffInfo { Type = BuffType.先天气功, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.深延术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.血龙兽, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.金刚不坏, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.金刚不坏秘籍, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.天上秘术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.魔法盾, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.金刚术, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.万效符, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo
                {
                    Type = BuffType.万效符秘籍,
                    Properties = BuffProperty.RemoveOnDeath,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                //MagicsDebuff
                new BuffInfo
                {
                    Type = BuffType.万效符杀,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit | BuffProperty.Debuff,
                    StackType = BuffStackType.ResetDuration,
                    Visible = true
                },
                
                // Monsters
                new BuffInfo { Type = BuffType.HornedArcherBuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.ColdArcherBuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.HornedColdArcherBuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.GeneralMeowMeowShield, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.惩戒真言, Properties = BuffProperty.Debuff, StackType = BuffStackType.ResetDuration },
                new BuffInfo { Type = BuffType.御体之力, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.HornedWarriorShield, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.ChieftainSwordBuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.Mon409BShieldBuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.失明状态, Properties = BuffProperty.RemoveOnDeath | BuffProperty.Debuff, StackType = BuffStackType.ResetDuration },
                new BuffInfo { Type = BuffType.寒冰护甲, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.ReaperPriestBuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.至尊威严, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.伤口加深, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.死亡印记, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.RiklebitesShield, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.麻痹状态, Properties = BuffProperty.Debuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.绝对封锁, Properties = BuffProperty.Debuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.Mon564NSealing, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.防御诅咒, Properties = BuffProperty.Debuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.烈火焚烧, Properties = BuffProperty.Debuff, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.Mon579BShield, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.Mon580BShield, StackType = BuffStackType.ResetDuration, Visible = true },
                new BuffInfo { Type = BuffType.Mon615BShield, StackType = BuffStackType.ResetDuration, Visible = true },

                // Special
                new BuffInfo
                {
                    Type = BuffType.游戏管理,
                    StackType = BuffStackType.Infinite,
                    Visible = Settings.GameMasterEffect
                },
                new BuffInfo 
                { 
                    Type = BuffType.General, 
                    Visible = true 
                },
                new BuffInfo
                {
                    Type = BuffType.变形效果,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.组队加成,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.公会特效,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.监禁,
                    Properties = BuffProperty.Debuff,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.精力充沛,
                    StackType = BuffStackType.ResetDuration,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.公会成员,
                    Properties = BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.英雄灵气,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },

                // Stats
                new BuffInfo
                {
                    Type = BuffType.防御提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.魔法防御提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.攻击力提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.魔法力提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.道术力提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.准确提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.敏捷提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.生命值提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.法力值提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.攻击速度提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.幸运提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.背包重量提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.腕力提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.负重提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.反弹伤害提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.强度提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.神圣提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.冰冻伤害提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.毒素伤害提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.魔法躲避提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.毒物躲避提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.生命恢复提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.法力恢复提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.中毒恢复提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.暴击率提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.暴击伤害提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.防御强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.魔法防御强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.攻击强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.魔法强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.道术强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.攻速强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.生命值强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.法力值强化提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.生命偷取提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.额外伤害提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.经验收益提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.经验收益固定,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.物品掉落提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.金币收益提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.采矿收益提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.宝石收益提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.钓鱼收益提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.大师收益提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.技能熟练提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.武器增伤提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.伴侣经验提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.师徒经验提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.师徒增伤提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                new BuffInfo
                {
                    Type = BuffType.伤害减免提升,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },

                // ItemsEffect 
                new BuffInfo
                {
                    Type = BuffType.奇异药水,
                    Properties = BuffProperty.PauseInSafeZone,
                    StackType = BuffStackType.StackDuration,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.技巧项链,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.隐身戒指,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.强化队伍,
                    Properties = BuffProperty.RemoveOnDeath,
                    StackType = BuffStackType.AttrStackStat,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.攻击型绝技,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.防御型绝技,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.技能型绝技,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.共用型绝技,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.华丽雨光,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.昆仑攻击,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.昆仑暴击,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.昆仑防御,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.天灵水,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.玉清水,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.甜筒HP,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.甜筒MP,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.内尔族的灵药,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.摩鲁的赤色药剂,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.摩鲁的青色药剂,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.摩鲁的黄色药剂,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.古代宗师祝福,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.黄金宗师祝福,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.破天的核心,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.安息之气,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.远古气息,
                    Properties = BuffProperty.RemoveOnDeath,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.暗影侵袭,
                    Properties = BuffProperty.RemoveOnExit | BuffProperty.RemoveOnMapChange | BuffProperty.Debuff,
                    StackType = BuffStackType.Infinite,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.龙之特效,
                    Properties = BuffProperty.RemoveOnDeath,
                    StackType = BuffStackType.AttrStackStat,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.龙的特效,
                    Properties = BuffProperty.RemoveOnDeath,
                    StackType = BuffStackType.AttrStackStat,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.龍之祝福,
                    Properties = BuffProperty.PauseInSafeZone,
                    StackType = BuffStackType.StackDuration,
                    Visible = true
                },
                new BuffInfo
                {
                    Type = BuffType.白龙祝福,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.AttrStackStatAndDuration,
                    Visible = true
                },

                // Battlefield
                new BuffInfo
                {
                    Type = BuffType.荣誉战场,
                    Properties = BuffProperty.RemoveOnDeath | BuffProperty.RemoveOnExit,
                    StackType = BuffStackType.ResetStatAndDuration,
                    Visible = true
                },
            };

            return info;
        }
    }

    public class Buff
    {
        protected static Envir Envir
        {
            get { return Envir.Main; }
        }

        public Dictionary<string, object> Data { get; set; }

        public BuffInfo Info;
        public MapObject Caster;
        public uint ObjectID;
        public long ExpireTime;

        public long LastTime, NextTime;

        public Stats Stats;

        public int[] Values;

        public bool FlagForRemoval;
        public bool Paused;

        public BuffType Type
        {
            get { return Info.Type; }
        }

        public BuffStackType StackType
        {
            get { return Info.StackType; }
        }

        public BuffProperty Properties
        {
            get { return Info.Properties; }
        }

        public Buff(BuffType type)
        {
            Info = Envir.GetBuffInfo(type);
            Stats = new Stats();
            Data = new Dictionary<string, object>();
        }

        public Buff(BinaryReader reader, int version, int customVersion)
        {
            var type = (BuffType)reader.ReadUInt16();

            Info = Envir.GetBuffInfo(type);

            Caster = null;

            if (version < 88)
            {
                var visible = reader.ReadBoolean();
            }

            ObjectID = reader.ReadUInt32();
            ExpireTime = reader.ReadInt64();

            if (version <= 84)
            {
                Values = new int[reader.ReadInt32()];

                for (int i = 0; i < Values.Length; i++)
                {
                    Values[i] = reader.ReadInt32();
                }

                if (version < 88)
                {
                    var infinite = reader.ReadBoolean();
                }

                Stats = new Stats();
                Data = new Dictionary<string, object>();
            }
            else
            {
                if (version < 88)
                {
                    var stackable = reader.ReadBoolean();
                }

                Values = new int[0];
                Stats = new Stats(reader, version, customVersion);
                Data = new Dictionary<string, object>();

                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    var key = reader.ReadString();
                    var length = reader.ReadInt32();

                    var array = new byte[length];

                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = reader.ReadByte();
                    }

                    Data[key] = Functions.DeserializeFromBytes(array);
                }

                if (version > 86)
                {
                    count = reader.ReadInt32();

                    Values = new int[count];

                    for (int i = 0; i < count; i++)
                    {
                        Values[i] = reader.ReadInt32();
                    }
                }
            }
        }

        public void Save(BinaryWriter writer)
        {
            if (Stats == null)
            {
                MessageQueue.Instance.Enqueue($"Buff保存失败：属性数据为空，类型={Type}，对象ID={ObjectID}");
                return;
            }

            if (Data == null)
            {
                MessageQueue.Instance.Enqueue($"Buff保存失败：附加数据为空，类型={Type}，对象ID={ObjectID}");
                return;
            }

            if (Values == null)
            {
                MessageQueue.Instance.Enqueue($"Buff保存失败：数值数据为空，类型={Type}，对象ID={ObjectID}");
                return;
            }

            foreach (string key in Data.Where(x => x.Value == null).Select(x => x.Key).ToList())
            {
                MessageQueue.Instance.Enqueue($"发现Buff空数据，已自动删除：类型={Type}，Key={key}，对象ID={ObjectID}");
                Data.Remove(key);
            }

            writer.Write((ushort)Type);
            writer.Write(ObjectID);
            writer.Write(ExpireTime);

            Stats.Save(writer);

            writer.Write(Data.Count);

            foreach (KeyValuePair<string, object> pair in Data)
            {
                var bytes = Functions.SerializeToBytes(pair.Value);

                writer.Write(pair.Key);
                writer.Write(bytes.Length);

                for (int i = 0; i < bytes.Length; i++)
                    writer.Write(bytes[i]);
            }

            writer.Write(Values.Length);

            for (int i = 0; i < Values.Length; i++)
                writer.Write(Values[i]);
        }

        public T Get<T>(string key)
        {
            if (!Data.TryGetValue(key, out object result))
            {
                return default;
            }

            return (T)result;
        }

        public void Set(string key, object val)
        {
            Data[key] = val;
        }

        public ClientBuff ToClientBuff()
        {
            return new ClientBuff
            {
                Type = Type,
                Caster = Caster?.Name ?? "",
                ObjectID = ObjectID,
                Visible = Info.Visible,
                Infinite = StackType == BuffStackType.Infinite,
                Paused = Paused,
                ExpireTime = ExpireTime,
                Stats = new Stats(Stats),
                Values = Values
            };
        }
    }
}