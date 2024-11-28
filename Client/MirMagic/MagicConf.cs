using Client.MirGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirMagic
{
    public class MagicConf
    {
        public Spell Spell;
        public bool IsPassive;
        public bool CostMP;
        public int ToggleInverval;
        public bool NeedToggle;
        public int CastEffect;
        public int Sound;
        public string CanUsedTips;
        public string CannotUsedTips;
        public int SpellEffect;
        public int SpellEffect2;
    }

    public class EffectConf
    {
        public int Id;
        public MLibrary Lib;
        public int BaseIndex;
        public int Count;
        public int DurationParam1;      // param1 + param2 * FrameCount * FrameInverval + param3 * FrameInverval
        public int DurationParam2;
        public int DurationParam3;
        public int DirectionOffset;
        public bool owner = true;
        public int StartTime;
        public bool Repeat;
        public long RepeatUntil;
        public Dictionary<int, int> FrameIntervals;
    }

    public class MagicConfMgr
    {
        private Dictionary<Spell, MagicConf> spellConfs = new Dictionary<Spell, MagicConf>();
        private Dictionary<int, EffectConf> effectConfs = new Dictionary<int, EffectConf>();

        public MagicConfMgr()
        {
            Init();
        }

        public void Init()
        {
            InitEffectConf();
            InitSpellConf();
        }

        public void InitEffectConf()
        {
            AddEffect(new EffectConf { Id = 1, Lib = Libraries.Magic2, BaseIndex = 210, Count = 6, DurationParam2 = 500 });
            AddEffect(new EffectConf { Id = 101, Lib = Libraries.Magic, BaseIndex = 0, Count = 10, DurationParam2 = 1 });         // 火球
            AddEffect(new EffectConf { Id = 102, Lib = Libraries.Magic, BaseIndex = 200, Count = 10, DurationParam2 = 1 });       // heal
            AddEffect(new EffectConf { Id = 103, Lib = Libraries.Magic, BaseIndex = 900, Count = 6, DurationParam2 = 1 });
            AddEffect(new EffectConf { Id = 104, Lib = Libraries.Magic, BaseIndex = 1560, Count = 10, DurationParam2 = 1 });
            AddEffect(new EffectConf { Id = 105, Lib = Libraries.Magic, BaseIndex = 600, Count = 10, DurationParam2 = 1 });
            AddEffect(new EffectConf { Id = 106, Lib = Libraries.Magic, BaseIndex = 400, Count = 10, DurationParam2 = 1 });
            AddEffect(new EffectConf { Id = 107, Lib = Libraries.Magic, BaseIndex = 920, Count = 10, DurationParam2 = 1 });
            AddEffect(new EffectConf { Id = 108, Lib = Libraries.Magic, BaseIndex = 1500, Count = 10, DurationParam2 = 1 });      // 召唤骷髅
            AddEffect(new EffectConf { Id = 109, Lib = Libraries.Magic3, BaseIndex = 590, Count = 10, DurationParam2 = 1 });
            AddEffect(new EffectConf { Id = 110, Lib = Libraries.Magic, BaseIndex = 1590, Count = 10, DurationParam2 = 1 });      // 传送
            AddEffect(new EffectConf { Id = 111, Lib = Libraries.Magic, BaseIndex = 1520, Count = 10, DurationParam2 = 1 });      // Hiding
            AddEffect(new EffectConf { Id = 113, Lib = Libraries.Magic, BaseIndex = 1650, Count = 10, DurationParam2 = 1 });      // firebang
            AddEffect(new EffectConf { Id = 114, Lib = Libraries.Magic, BaseIndex = 1620, Count = 10, DurationParam2 = 1 });      // firewall
            AddEffect(new EffectConf { Id = 115, Lib = Libraries.Magic, BaseIndex = 1380, Count = 10, DurationParam2 = 1 });      // TrapHexagon
            AddEffect(new EffectConf { Id = 116, Lib = Libraries.Magic2, BaseIndex = 190, Count= 6, DurationParam2 = 1 });       // EnergyRepulsor
            AddEffect(new EffectConf { Id = 117, Lib = Libraries.Magic2, BaseIndex = 2320, Count = 10, DurationParam2 = 1 });     // FireBurst
            AddEffect(new EffectConf { Id = 118, Lib = Libraries.Magic2, BaseIndex = 0, Count = 10, DurationParam2 = 1 });        // SummonShinsu
            AddEffect(new EffectConf { Id = 119, Lib = Libraries.Magic2, BaseIndex = 400, Count = 10, DurationParam2 = 1 });      // FrostCrunch
            AddEffect(new EffectConf { Id = 120, Lib = Libraries.Magic2, BaseIndex = 600, Count = 10, DurationParam2 = 1 });      // Purification
            AddEffect(new EffectConf { Id = 121, Lib = Libraries.Magic2, BaseIndex = 2430, Count = 11, DurationParam2 = 1 });
            AddEffect(new EffectConf { Id = 122, Lib = Libraries.Magic2, BaseIndex = 2380, Count = 10, DurationParam2 = 1 });     // MoonLight
            AddEffect(new EffectConf { Id = 123, Lib = Libraries.Magic2, BaseIndex = 2470, Count = 10, DurationParam2 = 1 });     // LightBody
            AddEffect(new EffectConf { Id = 124, Lib = Libraries.Magic, BaseIndex = 1790, Count = 10, DurationParam2 = 1 });      // MassHealing
            AddEffect(new EffectConf { Id = 125, Lib = Libraries.Magic, BaseIndex = 3840, Count = 10, DurationParam2 = 1 });      // IceStorm
            AddEffect(new EffectConf { Id = 126, Lib = Libraries.Magic, BaseIndex = 3880, Count = 10, DurationParam2 = 1 });      // MagicShield
            AddEffect(new EffectConf { Id = 127, Lib = Libraries.Magic, BaseIndex = 3920, Count = 10, DurationParam2 = 1 });      // TurnUndead
            AddEffect(new EffectConf { Id = 128, Lib = Libraries.Magic2, BaseIndex = 1520, Count = 10, DurationParam2 = 1 });     // ProtectionField
            AddEffect(new EffectConf { Id = 129, Lib = Libraries.Magic2, BaseIndex = 1510, Count = 10, DurationParam2 = 1 });     // Rage
            AddEffect(new EffectConf { Id = 130, Lib = Libraries.Magic2, BaseIndex = 1040, Count= 7, DurationParam2 = 1 });      // Vampirism
            AddEffect(new EffectConf { Id = 131, Lib = Libraries.Magic2, BaseIndex = 990, Count = 10, DurationParam2 = 1 });      // Entrapment
            AddEffect(new EffectConf { Id = 132, Lib = Libraries.Magic2, BaseIndex = 650, Count = 10, DurationParam2 = 1 });      // Mirroring


            AddEffect(new EffectConf { Id = 133, Lib = Libraries.Magic2, BaseIndex = 2140, Count = 6, DirectionOffset = 10 });      // Haste
            AddEffect(new EffectConf { Id = 134, Lib = Libraries.Magic2, BaseIndex = 20, Count = 3, DurationParam1 = 300 });        // ThunderBolt

            AddEffect(new EffectConf { Id = 135, Lib = Libraries.Magic3, BaseIndex = 200, Count = 8, DurationParam3 = 8 });        // Fury
            AddEffect(new EffectConf { Id = 136, Lib = Libraries.Magic3, BaseIndex = 187, Count = 10, DurationParam3 = 10});        // Fury

            AddEffect(new EffectConf { Id = 137, Lib = Libraries.Magic3, BaseIndex = 550, Count = 17, DurationParam2 = 4});         // ImmortalSkin
            AddEffect(new EffectConf { Id = 138, Lib = Libraries.Magic3, BaseIndex = 570, Count = 5, DurationParam2 = 1});          // ImmortalSkin
            AddEffect(new EffectConf { Id = 139, Lib = Libraries.Magic2, BaseIndex = 130, Count = 6, DurationParam2 = 1 });         // FlameDisruptor

            AddEffect(new EffectConf { Id = 140, Lib = Libraries.Magic2, BaseIndex = 910, Count = 23, DurationParam1 = 1800, owner = false });     // FlameField  ? 火龙气焰
            AddEffect(new EffectConf { Id = 141, Lib = Libraries.Magic2, BaseIndex = 2340, Count = 11, DurationParam3 = 11 });     // Trap 
            AddEffect(new EffectConf { Id = 142, Lib = Libraries.Magic2, BaseIndex = 2440, Count = 16, DurationParam3 = 16 });     // SwiftFeet  ?
            AddEffect(new EffectConf { Id = 143, Lib = Libraries.Magic2, BaseIndex = 2490, Count = 10, DirectionOffset = 10, DurationParam1 = 500, DurationParam2 = 1 });     // PoisonSword 
            AddEffect(new EffectConf { Id = 144, Lib = Libraries.Magic2, BaseIndex = 2580, Count = 10, DurationParam3 = 10 });     // DarkBody
            AddEffect(new EffectConf { Id = 145, Lib = Libraries.Magic, BaseIndex = 1680, Count = 10, DurationParam2 = 1, owner = false });     // ThunderStorm? 地狱雷光
            AddEffect(new EffectConf { Id = 146, Lib = Libraries.Magic3, BaseIndex = 80, Count = 9, DurationParam3 = 9 });     // MagicBooster
            AddEffect(new EffectConf { Id = 147, Lib = Libraries.Magic3, BaseIndex = 200, Count = 8, DurationParam3 = 8 });     // PetEnhancer
            AddEffect(new EffectConf { Id = 148, Lib = Libraries.Magic, BaseIndex = 3960, Count = 20, DurationParam1 = 1200 });     // Revelation
            AddEffect(new EffectConf { Id = 149, Lib = Libraries.Magic2, BaseIndex = 710, Count = 20, DurationParam1 = 1200 });     // LionRoar BattleCry
            AddEffect(new EffectConf { Id = 150, Lib = Libraries.Magic2, BaseIndex = 210, Count = 6, DurationParam1 = 500 });     // TwinDrakeBlade
            AddEffect(new EffectConf { Id = 151, Lib = Libraries.Magic2, BaseIndex = 740, Count = 15, DirectionOffset = 20, DurationParam3 = 15 });     // BladeAvalanche
            AddEffect(new EffectConf { Id = 152, Lib = Libraries.Magic2, BaseIndex = 1700, DirectionOffset = 10, Count = 9, DurationParam3 = 9, owner = false });     // SlashingBurst 日闪 
            AddEffect(new EffectConf { Id = 154, Lib = Libraries.Magic, BaseIndex = 3480, DirectionOffset = 10, Count = 10, DurationParam3 = 10 });     // CounterAttack
            AddEffect(new EffectConf { Id = 155, Lib = Libraries.Magic2, BaseIndex = 2620, DirectionOffset = 20, Count = 20, DurationParam3 = 20 });     // CrescentSlash
            AddEffect(new EffectConf { Id = 157, Lib = Libraries.Magic2, BaseIndex = 1540, Count = 8, DurationParam2 = 1 });     // Blizzard
            AddEffect(new EffectConf { Id = 158, Lib = Libraries.Magic2, BaseIndex = 1590, Count = 10, DurationParam2 = 1 });     // MeteorStrike
            AddEffect(new EffectConf { Id = 160, Lib = Libraries.Magic2, BaseIndex = 2230, Count = 8, DirectionOffset = 10, DurationParam1 = 800 });     // HeavenlySword
            //AddEffect(new EffectConf { Id = 161, Lib = Libraries.Magic3, BaseIndex = 1880, Count = 8, DurationParam2 = 1 });     // ElementalBarrier
            AddEffect(new EffectConf { Id = 162, Lib = Libraries.Magic3, BaseIndex = 2300, Count = 8, DurationParam1 = 1000 });     // PoisonShot
            AddEffect(new EffectConf { Id = 163, Lib = Libraries.Magic3, BaseIndex = 2710, Count = 8, DurationParam1 = 1200 });     // OneWithNature

            AddEffect(new EffectConf { Id = 164, Lib = Libraries.Magic3, BaseIndex = 140, Count = 2, DurationParam3 = 2 });     // CounterAttack

            AddEffect(new EffectConf { Id = 165, Lib = Libraries.Magic2, BaseIndex = 160, Count = 15, DurationParam1 = 1000 });     // CounterAttack



            AddEffect(new EffectConf { Id = 168, Lib = Libraries.Magic3, BaseIndex = 620, Count = 10, DurationParam2 = 1 });     // HealCircle
            AddEffect(new EffectConf { Id = 169, Lib = Libraries.Magic3, BaseIndex = 250, Count = 10, DurationParam2 = 1 });     // CatPongue
            EffectConf eff = new EffectConf { Id = 170, Lib = Libraries.Magic3, BaseIndex = 4420, Count = 10, DurationParam1 = 300, DurationParam2 = 1, StartTime = 200 };     // CreateBigFireSecret
            AddEffect(eff);     // CreateBigFireSecret
            AddEffect(new EffectConf { Id = 171, Lib = Libraries.Magic3, BaseIndex = 200, Count = 8, DurationParam3 = 5 });     // Bisul

            AddEffect(new EffectConf { Id = 172, Lib = Libraries.Magic3, BaseIndex = 6120, Count = 5, DurationParam2 = 1, DirectionOffset=10 });     // 爆闪 秘笈
            AddEffect(new EffectConf { Id = 173, Lib = Libraries.Magic3, BaseIndex = 6210, Count = 6,
               DurationParam2 = 1, DirectionOffset=10, StartTime = 500,
                Repeat = true, RepeatUntil = 2110});     // 爆闪 秘笈

            AddEffect(new EffectConf { Id = 174, Lib = Libraries.Magic3, BaseIndex = 2710, Count = 8, DurationParam2 = 1 });     // 爆闪 秘笈
        }

        private void AddEffect(EffectConf effectConf)
        {
            effectConfs.Add(effectConf.Id, effectConf);
        }

        public void InitSpellConf()
        {
            spellConfs.Add(Spell.Fencing, new MagicConf { Spell = Spell.Fencing, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false, });
            spellConfs.Add(Spell.Slaying, new MagicConf { Spell = Spell.Slaying, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.Thrusting, new MagicConf { Spell = Spell.Thrusting, IsPassive = false, CostMP = false, ToggleInverval = 1000, NeedToggle = true, CanUsedTips = "Use Thrusting.", CannotUsedTips = "Do not use Thrusting"});
            spellConfs.Add(Spell.HalfMoon, new MagicConf { Spell = Spell.HalfMoon, IsPassive = false, CostMP = false, ToggleInverval = 1000, NeedToggle = true, CanUsedTips = "Use HalfMoon.", CannotUsedTips = "Do not use HalfMoon."});
            spellConfs.Add(Spell.ShoulderDash, new MagicConf { Spell = Spell.ShoulderDash, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = ""});
            spellConfs.Add(Spell.TwinDrakeBlade, new MagicConf { Spell = Spell.TwinDrakeBlade, IsPassive = false, CostMP = true, ToggleInverval = 500, NeedToggle = true, CastEffect = 1 , SpellEffect = 150});
            spellConfs.Add(Spell.Entrapment, new MagicConf { Spell = Spell.Entrapment, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect=131});
            spellConfs.Add(Spell.FlamingSword, new MagicConf { Spell = Spell.FlamingSword, IsPassive = false, CostMP = true, ToggleInverval = 500, NeedToggle = true,
            CanUsedTips = "Your weapon is glowed by spirit of fire.",
            CannotUsedTips = "The spirits of fire disappeared."
            });
            spellConfs.Add(Spell.LionRoar, new MagicConf { Spell = Spell.LionRoar, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 149});
            spellConfs.Add(Spell.CrossHalfMoon, new MagicConf { Spell = Spell.CrossHalfMoon, IsPassive = false, CostMP = false, ToggleInverval = 1000, NeedToggle = true });
            spellConfs.Add(Spell.BladeAvalanche, new MagicConf { Spell = Spell.BladeAvalanche, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 151});
            spellConfs.Add(Spell.ProtectionField, new MagicConf { Spell = Spell.ProtectionField, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 128});


            spellConfs.Add(Spell.Rage, new MagicConf { Spell = Spell.Rage, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 129});
            spellConfs.Add(Spell.CounterAttack, new MagicConf { Spell = Spell.CounterAttack, IsPassive = false, CostMP = true, ToggleInverval = 0, NeedToggle = false, Sound = 20000 + (ushort)Spell.CounterAttack * 10 , SpellEffect = 154});


            spellConfs.Add(Spell.SlashingBurst, new MagicConf { Spell = Spell.SlashingBurst, IsPassive = false, CostMP = true, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 152});
            spellConfs.Add(Spell.Fury, new MagicConf { Spell = Spell.Fury, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 134, SpellEffect2 = 135});
            spellConfs.Add(Spell.ImmortalSkin, new MagicConf { Spell = Spell.ImmortalSkin, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "",  SpellEffect = 137, SpellEffect2 = 138});


            spellConfs.Add(Spell.FireBall, new MagicConf { Spell = Spell.FireBall, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 101});
            spellConfs.Add(Spell.Repulsion, new MagicConf { Spell = Spell.Repulsion, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 103});
            spellConfs.Add(Spell.ElectricShock, new MagicConf { Spell = Spell.ElectricShock, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 104});
            spellConfs.Add(Spell.GreatFireBall, new MagicConf { Spell = Spell.GreatFireBall, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 106});
            spellConfs.Add(Spell.HellFire, new MagicConf { Spell = Spell.HellFire, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 107});
            spellConfs.Add(Spell.ThunderBolt, new MagicConf { Spell = Spell.ThunderBolt, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 134});
            spellConfs.Add(Spell.Teleport, new MagicConf { Spell = Spell.Teleport, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 110});
            spellConfs.Add(Spell.FireBang, new MagicConf { Spell = Spell.FireBang, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 113});
            spellConfs.Add(Spell.FireWall, new MagicConf { Spell = Spell.FireWall, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 114});
            spellConfs.Add(Spell.Lightning, new MagicConf { Spell = Spell.Lightning, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = ""});
            spellConfs.Add(Spell.FrostCrunch, new MagicConf { Spell = Spell.FrostCrunch, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 119});
            spellConfs.Add(Spell.ThunderStorm, new MagicConf { Spell = Spell.ThunderStorm, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 145});
            spellConfs.Add(Spell.MagicShield, new MagicConf { Spell = Spell.MagicShield, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 126});
            spellConfs.Add(Spell.TurnUndead, new MagicConf { Spell = Spell.TurnUndead, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 127});
            spellConfs.Add(Spell.Vampirism, new MagicConf { Spell = Spell.Vampirism, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 130});
            spellConfs.Add(Spell.IceStorm, new MagicConf { Spell = Spell.IceStorm, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 125});
            spellConfs.Add(Spell.FlameDisruptor, new MagicConf { Spell = Spell.FlameDisruptor, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 139});
            spellConfs.Add(Spell.Mirroring, new MagicConf { Spell = Spell.Mirroring, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 132});
            spellConfs.Add(Spell.FlameField, new MagicConf { Spell = Spell.FlameField, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 140});
            spellConfs.Add(Spell.Blizzard, new MagicConf { Spell = Spell.Blizzard, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 157});
            spellConfs.Add(Spell.MagicBooster, new MagicConf { Spell = Spell.MagicBooster, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 146});
            spellConfs.Add(Spell.MeteorStrike, new MagicConf { Spell = Spell.MeteorStrike, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 158});
            spellConfs.Add(Spell.IceThrust, new MagicConf { Spell = Spell.IceThrust, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = ""});
            spellConfs.Add(Spell.FastMove, new MagicConf { Spell = Spell.FastMove, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = ""});
            spellConfs.Add(Spell.StormEscape, new MagicConf { Spell = Spell.StormEscape, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 109});

            spellConfs.Add(Spell.Healing, new MagicConf { Spell = Spell.Healing, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 102 });
            spellConfs.Add(Spell.SpiritSword, new MagicConf { Spell = Spell.SpiritSword, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.Poisoning, new MagicConf { Spell = Spell.Poisoning, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 105 });
            spellConfs.Add(Spell.SoulFireBall, new MagicConf { Spell = Spell.SoulFireBall, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.SummonSkeleton, new MagicConf { Spell = Spell.SummonSkeleton, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 108 });
            spellConfs.Add(Spell.Hiding, new MagicConf { Spell = Spell.Hiding, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 111 });
            spellConfs.Add(Spell.MassHiding, new MagicConf { Spell = Spell.MassHiding, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.SoulShield, new MagicConf { Spell = Spell.SoulShield, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.Revelation, new MagicConf { Spell = Spell.Revelation, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 148 });
            spellConfs.Add(Spell.BlessedArmour, new MagicConf { Spell = Spell.BlessedArmour, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.EnergyRepulsor, new MagicConf { Spell = Spell.EnergyRepulsor, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 116 });
            spellConfs.Add(Spell.TrapHexagon, new MagicConf { Spell = Spell.TrapHexagon, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 115 });
            spellConfs.Add(Spell.Purification, new MagicConf { Spell = Spell.Purification, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 120});
            spellConfs.Add(Spell.MassHealing, new MagicConf { Spell = Spell.MassHealing, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 124 });
            spellConfs.Add(Spell.Hallucination, new MagicConf { Spell = Spell.Hallucination, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.UltimateEnhancer, new MagicConf { Spell = Spell.UltimateEnhancer, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 165 });
            spellConfs.Add(Spell.SummonShinsu, new MagicConf { Spell = Spell.SummonShinsu, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 118 });
            spellConfs.Add(Spell.Reincarnation, new MagicConf { Spell = Spell.Reincarnation, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.SummonHolyDeva, new MagicConf { Spell = Spell.SummonHolyDeva, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.Curse, new MagicConf { Spell = Spell.Curse, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.Plague, new MagicConf { Spell = Spell.Plague, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.PoisonCloud, new MagicConf { Spell = Spell.PoisonCloud, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.EnergyShield, new MagicConf { Spell = Spell.EnergyShield, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });
            spellConfs.Add(Spell.PetEnhancer, new MagicConf { Spell = Spell.PetEnhancer, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 147 });
            spellConfs.Add(Spell.HealingCircle, new MagicConf { Spell = Spell.HealingCircle, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "" });

            spellConfs.Add(Spell.FatalSword, new MagicConf { Spell = Spell.FatalSword, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false, });
            spellConfs.Add(Spell.DoubleSlash, new MagicConf { Spell = Spell.DoubleSlash, IsPassive = false, CostMP = false, ToggleInverval = 1000, NeedToggle = true });
            spellConfs.Add(Spell.Haste, new MagicConf { Spell = Spell.Haste, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 133 });
            spellConfs.Add(Spell.FlashDash, new MagicConf { Spell = Spell.FlashDash, IsPassive = false, CostMP = true, ToggleInverval = 500, NeedToggle = false, });
            spellConfs.Add(Spell.LightBody, new MagicConf { Spell = Spell.LightBody, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 123 }); 
            spellConfs.Add(Spell.HeavenlySword, new MagicConf { Spell = Spell.HeavenlySword, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 160 });
            spellConfs.Add(Spell.FireBurst, new MagicConf { Spell = Spell.FireBurst, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 117 });
            spellConfs.Add(Spell.Trap, new MagicConf { Spell = Spell.Trap, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 141 });
            spellConfs.Add(Spell.PoisonSword, new MagicConf { Spell = Spell.PoisonSword, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 143 }); 
            spellConfs.Add(Spell.MoonLight, new MagicConf { Spell = Spell.MoonLight, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 122}); 
            spellConfs.Add(Spell.SwiftFeet, new MagicConf { Spell = Spell.SwiftFeet, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 142 });
            spellConfs.Add(Spell.DarkBody, new MagicConf { Spell = Spell.DarkBody, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 144});
            spellConfs.Add(Spell.Hemorrhage, new MagicConf { Spell = Spell.Hemorrhage, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.CrescentSlash, new MagicConf { Spell = Spell.CrescentSlash, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 155 });
            spellConfs.Add(Spell.MoonMist, new MagicConf { Spell = Spell.MoonMist, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.MPEater, new MagicConf { Spell = Spell.MPEater, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.CatTongue, new MagicConf { Spell = Spell.CatTongue, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, CanUsedTips = "", CannotUsedTips = "", SpellEffect = 169 });

            spellConfs.Add(Spell.Focus, new MagicConf { Spell = Spell.Focus, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.StraightShot, new MagicConf { Spell = Spell.StraightShot, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, });
            spellConfs.Add(Spell.DoubleShot, new MagicConf { Spell = Spell.DoubleShot, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, });
            spellConfs.Add(Spell.ExplosiveTrap, new MagicConf { Spell = Spell.ExplosiveTrap, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, });
            spellConfs.Add(Spell.DelayedExplosion, new MagicConf { Spell = Spell.DelayedExplosion, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, });
            spellConfs.Add(Spell.Meditation, new MagicConf { Spell = Spell.Meditation, IsPassive = true, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.BackStep, new MagicConf { Spell = Spell.BackStep, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.ElementalShot, new MagicConf { Spell = Spell.ElementalShot, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.Concentration, new MagicConf { Spell = Spell.Concentration, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.ElementalBarrier, new MagicConf { Spell = Spell.ElementalBarrier, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 161 });
            spellConfs.Add(Spell.SummonVampire, new MagicConf { Spell = Spell.SummonVampire, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.VampireShot, new MagicConf { Spell = Spell.VampireShot, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.SummonToad, new MagicConf { Spell = Spell.SummonToad, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.PoisonShot, new MagicConf { Spell = Spell.PoisonShot, IsPassive = false, CostMP = false, ToggleInverval = 500, NeedToggle = true, SpellEffect = 162 });
            spellConfs.Add(Spell.CrippleShot, new MagicConf { Spell = Spell.CrippleShot, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.SummonSnakes, new MagicConf { Spell = Spell.SummonSnakes, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.NapalmShot, new MagicConf { Spell = Spell.NapalmShot, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 174 });
            spellConfs.Add(Spell.OneWithNature, new MagicConf { Spell = Spell.OneWithNature, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 163 });
            spellConfs.Add(Spell.BindingShot, new MagicConf { Spell = Spell.BindingShot, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.MentalState, new MagicConf { Spell = Spell.MentalState, IsPassive = false, CostMP = false, ToggleInverval = 500, NeedToggle = true });
 
            spellConfs.Add(Spell.Blink, new MagicConf { Spell = Spell.Blink, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 110 });
            spellConfs.Add(Spell.Portal, new MagicConf { Spell = Spell.Portal, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.BattleCry, new MagicConf { Spell = Spell.BattleCry, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false, SpellEffect = 149 });
            spellConfs.Add(Spell.DigOutZombie, new MagicConf { Spell = Spell.DigOutZombie, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.Rubble, new MagicConf { Spell = Spell.Rubble, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.MapLightning, new MagicConf { Spell = Spell.MapLightning, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.MapLava, new MagicConf { Spell = Spell.MapLava, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.MapQuake1, new MagicConf { Spell = Spell.MapQuake1, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
            spellConfs.Add(Spell.MapQuake2, new MagicConf { Spell = Spell.MapQuake2, IsPassive = false, CostMP = false, ToggleInverval = 0, NeedToggle = false });
        }

        public MagicConf GetConf(Spell spell)
        {
            if (spellConfs.ContainsKey(spell))
                return spellConfs[spell];

            File.AppendAllText(@".\Error.txt",
                        string.Format("[{0}] spell config is null {1}{2}", DateTime.Now, spell.ToString(), Environment.NewLine));
            return null;
        }

        public EffectConf GetEffectConf(int effectId)
        {
            if (effectConfs.ContainsKey(effectId))
                return effectConfs[effectId];

            return null;
        }
    }
}
