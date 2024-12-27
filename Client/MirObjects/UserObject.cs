using Client.MirScenes;
using Client.MirScenes.Dialogs;
using S = ServerPackets;

namespace Client.MirObjects
{
    public class UserObject : PlayerObject
    {
        public uint Id;

        public int HP, MP;

        public int AttackSpeed;

        public Stats Stats;

        public int CurrentHandWeight,
                      CurrentWearWeight,
                      CurrentBagWeight;

        public long Experience, MaxExperience;

        public bool TradeLocked;
        public uint TradeGoldAmount;
        public bool AllowTrade;

        public bool RentalGoldLocked;
        public bool RentalItemLocked;
        public uint RentalGoldAmount;

        public SpecialItemMode ItemMode;

        public BaseStats CoreStats = new BaseStats(0);

        public virtual BuffDialog GetBuffDialog => GameScene.Scene.BuffsDialog;

        public UserItem[] Inventory = new UserItem[46], Equipment = new UserItem[14], Trade = new UserItem[10], QuestInventory = new UserItem[40];
        public int BeltIdx = 6, HeroBeltIdx = 2;
        public bool HasExpandedStorage = false;
        public DateTime ExpandedStorageExpiryTime;

        public List<ClientMagic> Magics = new List<ClientMagic>();
        public List<ItemSets> ItemSets = new List<ItemSets>();
        public List<EquipmentSlot> MirSet = new List<EquipmentSlot>();

        public List<ClientIntelligentCreature> IntelligentCreatures = new List<ClientIntelligentCreature>();
        public IntelligentCreatureType SummonedCreatureType = IntelligentCreatureType.None;
        public bool CreatureSummoned;
        public int PearlCount = 0;

        public List<ClientQuestProgress> CurrentQuests = new List<ClientQuestProgress>();
        public List<int> CompletedQuests = new List<int>();
        public List<ClientMail> Mail = new List<ClientMail>();

        public bool Slaying, Thrusting, HalfMoon, CrossHalfMoon, DoubleSlash, TwinDrakeBlade, FlamingSword;
        public ClientMagic NextMagic;
        public Point NextMagicLocation;
        public MapObject NextMagicObject;
        public MirDirection NextMagicDirection;
        public QueuedAction QueuedAction;

        public UserObject() { }
        public UserObject(uint objectID) : base(objectID)
        {
            Stats = new Stats();
        }

        public virtual void Load(S.UserInformation info)
        {
            Id = info.RealId;
            Name = info.Name;
            Settings.LoadTrackedQuests(info.Name);
            NameColour = info.NameColour;
            GuildName = info.GuildName;
            GuildRankName = info.GuildRank;
            Class = info.Class;
            Gender = info.Gender;
            Level = info.Level;

            CurrentLocation = info.Location;
            MapLocation = info.Location;
            GameScene.Scene.MapControl.AddObject(this);

            Direction = info.Direction;
            Hair = info.Hair;

            HP = info.HP;
            MP = info.MP;

            Experience = info.Experience;
            MaxExperience = info.MaxExperience;

            LevelEffects = info.LevelEffects;

            Inventory = info.Inventory;
            Equipment = info.Equipment;
            QuestInventory = info.QuestInventory;

            HasExpandedStorage = info.HasExpandedStorage;
            ExpandedStorageExpiryTime = info.ExpandedStorageExpiryTime;

            Magics = info.Magics;
            for (int i = 0; i < Magics.Count; i++ )
            {
                Magics[i].CastTime += CMain.Time;
            }

            IntelligentCreatures = info.IntelligentCreatures;
            SummonedCreatureType = info.SummonedCreatureType;
            CreatureSummoned = info.CreatureSummoned;

            BindAllItems();

            RefreshStats();

            SetAction();
        }

        public void SetSlots(S.UserSlotsRefresh p)
        {
            Inventory = p.Inventory;
            Equipment = p.Equipment;

            BindAllItems();
            RefreshStats();
        }

        public override void SetLibraries()
        {
            base.SetLibraries();
        }

        public override void SetEffects()
        {
            base.SetEffects();
        }

        public void RefreshStats()
        {
            Stats.Clear();

            RefreshLevelStats();
            RefreshBagWeight();
            RefreshEquipmentStats();
            RefreshItemSetStats();
            RefreshMirSetStats();
            RefreshSkills();
            RefreshBuffs();
            RefreshGuildBuffs();

            SetLibraries();
            SetEffects();

            Stats[Stat.HP] += (Stats[Stat.HP] * Stats[Stat.生命值数率]) / 100;
            Stats[Stat.MP] += (Stats[Stat.MP] * Stats[Stat.法力值数率]) / 100;
            Stats[Stat.MaxAC] += (Stats[Stat.MaxAC] * Stats[Stat.最大防御数率]) / 100;
            Stats[Stat.MaxMAC] += (Stats[Stat.MaxMAC] * Stats[Stat.最大魔御数率]) / 100;

            Stats[Stat.MaxDC] += (Stats[Stat.MaxDC] * Stats[Stat.最大物理攻击数率]) / 100;
            Stats[Stat.MaxMC] += (Stats[Stat.MaxMC] * Stats[Stat.最大魔法攻击数率]) / 100;
            Stats[Stat.MaxSC] += (Stats[Stat.MaxSC] * Stats[Stat.最大道术攻击数率]) / 100;
            Stats[Stat.攻击速度] += (Stats[Stat.攻击速度] * Stats[Stat.攻击速度数率]) / 100;

            RefreshStatCaps();

            if (this == User && Light < 3) Light = 3;
            AttackSpeed = 1400 - ((Stats[Stat.攻击速度] * 60) + Math.Min(370, (Level * 14)));
            if (AttackSpeed < 550) AttackSpeed = 550;

            PercentHealth = (byte)(HP / (float)Stats[Stat.HP] * 100);

            GameScene.Scene.Redraw();
        }

        private void RefreshLevelStats()
        {
            Light = 0;

            foreach (var stat in CoreStats.Stats)
            {
                Stats[stat.Type] = stat.Calculate(Class, Level);
            }
        }

        private void RefreshBagWeight()
        {
            CurrentBagWeight = 0;

            for (int i = 0; i < Inventory.Length; i++)
            {
                UserItem item = Inventory[i];
                if (item != null)
                {
                    CurrentBagWeight += item.Weight;
                }
            }
        }

        private void RefreshEquipmentStats()
        {
            Weapon = -1;
            WeaponEffect = 0;
            Armour = 0;
            WingEffect = 0;
            MountType = -1;

            CurrentWearWeight = 0;
            CurrentHandWeight = 0;

            ItemMode = SpecialItemMode.None;
            FastRun = false;

            ItemSets.Clear();
            MirSet.Clear();

            for (int i = 0; i < Equipment.Length; i++)
            {
                UserItem temp = Equipment[i];
                if (temp == null) continue;

                ItemInfo realItem = Functions.GetRealItem(temp.Info, Level, Class, GameScene.ItemInfoList);

                if (realItem.Type == ItemType.武器 || realItem.Type == ItemType.照明物)
                    CurrentHandWeight = (int)Math.Min(int.MaxValue, CurrentHandWeight + temp.Weight);
                else
                    CurrentWearWeight = (int)Math.Min(int.MaxValue, CurrentWearWeight + temp.Weight);

                if (temp.CurrentDura == 0 && realItem.Durability > 0) continue;

                if (realItem.Type == ItemType.盔甲)
                {
                    Armour = realItem.Shape;
                    WingEffect = realItem.Effect;
                }
                if (realItem.Type == ItemType.武器)
                {
                    Weapon = realItem.Shape;
                    WeaponEffect = realItem.Effect;
                }

                if (realItem.Type == ItemType.坐骑)
                {
                    MountType = realItem.Shape;
                }

                if (temp.Info.IsFishingRod) continue;

                Stats.Add(realItem.Stats);
                Stats.Add(temp.AddedStats);

                Stats[Stat.MinAC] += temp.Awake.GetAC();
                Stats[Stat.MaxAC] += temp.Awake.GetAC();
                Stats[Stat.MinMAC] += temp.Awake.GetMAC();
                Stats[Stat.MaxMAC] += temp.Awake.GetMAC();

                Stats[Stat.MinDC] += temp.Awake.GetDC();
                Stats[Stat.MaxDC] += temp.Awake.GetDC();
                Stats[Stat.MinMC] += temp.Awake.GetMC();
                Stats[Stat.MaxMC] += temp.Awake.GetMC();
                Stats[Stat.MinSC] += temp.Awake.GetSC();
                Stats[Stat.MaxSC] += temp.Awake.GetSC();

                Stats[Stat.HP] += temp.Awake.GetHPMP();
                Stats[Stat.MP] += temp.Awake.GetHPMP();

                if (realItem.Light > Light) Light = realItem.Light;
                if (realItem.Unique != SpecialItemMode.None)
                {
                    ItemMode |= realItem.Unique;
                }

                if (realItem.CanFastRun)
                {
                    FastRun = true;
                }

                RefreshSocketStats(temp);

                if (realItem.Set == ItemSet.非套装) continue;

                ItemSets itemSet = ItemSets.Where(set => set.Set == realItem.Set && !set.Type.Contains(realItem.Type) && !set.SetComplete).FirstOrDefault();

                if (itemSet != null)
                {
                    itemSet.Type.Add(realItem.Type);
                    itemSet.Count++;
                }
                else
                {
                    ItemSets.Add(new ItemSets { Count = 1, Set = realItem.Set, Type = new List<ItemType> { realItem.Type } });
                }

                //Mir Set
                if (realItem.Set == ItemSet.天龙套装)
                {
                    if (!MirSet.Contains((EquipmentSlot)i))
                        MirSet.Add((EquipmentSlot)i);
                }
            }

            if (ItemMode.HasFlag(SpecialItemMode.Muscle))
            {
                Stats[Stat.背包负重] = Stats[Stat.背包负重] * 2;
                Stats[Stat.装备负重] = Stats[Stat.装备负重] * 2;
                Stats[Stat.腕力负重] = Stats[Stat.腕力负重] * 2;
            }
        }


        private void RefreshSocketStats(UserItem equipItem)
        {
            if (equipItem == null) return;

            if (equipItem.Info.Type == ItemType.武器 && equipItem.Info.IsFishingRod)
            {
                return;
            }

            if (equipItem.Info.Type == ItemType.坐骑 && !RidingMount)
            {
                return;
            }

            for (int i = 0; i < equipItem.Slots.Length; i++)
            {
                UserItem temp = equipItem.Slots[i];

                if (temp == null) continue;
                ItemInfo realItem = Functions.GetRealItem(temp.Info, Level, Class, GameScene.ItemInfoList);

                if (realItem.Type == ItemType.武器 || realItem.Type == ItemType.照明物)
                    CurrentHandWeight = (int)Math.Min(int.MaxValue, CurrentHandWeight + temp.Weight);
                else
                    CurrentWearWeight = (int)Math.Min(int.MaxValue, CurrentWearWeight + temp.Weight);

                if (temp.CurrentDura == 0 && realItem.Durability > 0) continue;

                Stats.Add(realItem.Stats);
                Stats.Add(temp.AddedStats);
        
                if (realItem.Light > Light) Light = realItem.Light;
                if (realItem.Unique != SpecialItemMode.None)
                {
                    ItemMode |= realItem.Unique;
                }
            }
        }

        private void RefreshItemSetStats()
        {
            foreach (var s in ItemSets)
            {
                if (s.Set == ItemSet.破碎套装)
                {
                    if (s.Type.Contains(ItemType.项链) && s.Type.Contains(ItemType.戒指) && s.Type.Contains(ItemType.手镯))
                    {
                        Stats[Stat.MinDC] += 1;
                        Stats[Stat.MaxDC] += 3;
                    }
                    if (s.Type.Contains(ItemType.戒指) && s.Type.Contains(ItemType.手镯))
                    {
                        Stats[Stat.攻击速度] += 2;
                        return;
                    }
                }

                if ((s.Set == ItemSet.灵玉套装) && (s.Type.Contains(ItemType.戒指)) && (s.Type.Contains(ItemType.手镯)))
                {
                    Stats[Stat.神圣] += 3;
                }

                if ((s.Set == ItemSet.幻魔石套) && (s.Type.Contains(ItemType.戒指)) && (s.Type.Contains(ItemType.手镯)))
                {
                    Stats[Stat.装备负重] += 5;
                    Stats[Stat.背包负重] += 20;
                }

                if ((s.Set == ItemSet.鏃未套装) && (s.Type.Contains(ItemType.项链)) && (s.Type.Contains(ItemType.手镯)))
                {
                    Stats[Stat.HP] += 25;
                }

                if (s.Set == ItemSet.圣龙套装)
                {
                    if (Equipment[(int)EquipmentSlot.左戒指] != null && Equipment[(int)EquipmentSlot.右戒指] != null)
                    {
                        bool activateRing = false;

                        if (Equipment[(int)EquipmentSlot.左戒指].Info.Name.StartsWith("双花") &&
                            Equipment[(int)EquipmentSlot.左戒指].Info.Set == ItemSet.圣龙套装 &&
                            Equipment[(int)EquipmentSlot.右戒指].Info.Name.StartsWith("双绿") &&
                            Equipment[(int)EquipmentSlot.右戒指].Info.Set == ItemSet.圣龙套装)
                        {
                            activateRing = true;
                        }

                        if (Equipment[(int)EquipmentSlot.右戒指].Info.Name.StartsWith("双花") &&
                            Equipment[(int)EquipmentSlot.右戒指].Info.Set == ItemSet.圣龙套装 &&
                            Equipment[(int)EquipmentSlot.左戒指].Info.Name.StartsWith("双绿") &&
                            Equipment[(int)EquipmentSlot.左戒指].Info.Set == ItemSet.圣龙套装)
                        {
                            activateRing = true;
                        }

                        if (activateRing)
                        {
                            Stats[Stat.MaxDC] += 5;
                            Stats[Stat.MaxMC] += 5;
                            Stats[Stat.MaxSC] += 5;
                            return;
                        }
                    }
                }

                if (s.Set == ItemSet.神龙套装) //需在ItemData.cs中设置套装件数
                {
                    if (s.Type.Contains(ItemType.戒指) && s.Type.Contains(ItemType.项链))
                    {
                        Stats[Stat.MaxDC] += 8;
                        Stats[Stat.MaxMC] += 8;
                        Stats[Stat.MaxSC] += 8;
                    }
                    if (s.Type.Contains(ItemType.盔甲) && s.Type.Contains(ItemType.戒指) && s.Type.Contains(ItemType.手镯) && s.Type.Contains(ItemType.项链))
                    {
                        Stats[Stat.最大防御数率] += 20;
                    }
                }

                if (!s.SetComplete) continue;

                switch (s.Set)
                {
                    case ItemSet.世轮套装:
                        Stats[Stat.HP] += 50;
                        break;
                    case ItemSet.绿翠套装:
                        Stats[Stat.MP] += 50;
                        break;
                    case ItemSet.道护套装:
                        Stats[Stat.HP] += 30;
                        Stats[Stat.MP] += 30;
                        break;
                    case ItemSet.赤兰套装:
                        Stats[Stat.准确] += 2;
                        Stats[Stat.吸血数率] += 10;
                        break;
                    case ItemSet.密火套装:
                        Stats[Stat.HP] += 50;
                        Stats[Stat.MP] -= 50;
                        break;
                    case ItemSet.幻魔石套:
                        Stats[Stat.MinMC] += 1;
                        Stats[Stat.MaxMC] += 2;
                        break;
                    case ItemSet.灵玉套装:
                        Stats[Stat.MinSC] += 1;
                        Stats[Stat.MaxSC] += 2;
                        break;
                    case ItemSet.五玄套装:
                        Stats[Stat.HP] += (int)(((double)Stats[Stat.HP] / 100) * 30);
                        Stats[Stat.MinAC] += 2;
                        Stats[Stat.MaxAC] += 2;
                        break;
                    case ItemSet.祈祷套装:
                        Stats[Stat.MinDC] += 2;
                        Stats[Stat.MaxDC] += 5;
                        Stats[Stat.攻击速度] += 2;
                        break;
                    case ItemSet.白骨套装:
                        Stats[Stat.MaxAC] += 2;
                        Stats[Stat.MaxMC] += 1;
                        Stats[Stat.MaxSC] += 1;
                        break;
                    case ItemSet.虫血套装:
                        Stats[Stat.MaxDC] += 1;
                        Stats[Stat.MaxMC] += 1;
                        Stats[Stat.MaxSC] += 1;
                        Stats[Stat.MaxMAC] += 1;
                        Stats[Stat.毒物躲避] += 1;
                        break;
                    case ItemSet.白金套装:
                        Stats[Stat.MaxDC] += 2;
                        Stats[Stat.MaxAC] += 2;
                        break;
                    case ItemSet.强白金套:
                        Stats[Stat.MaxDC] += 3;
                        Stats[Stat.HP] += 30;
                        Stats[Stat.攻击速度] += 2;
                        break;
                    case ItemSet.红玉套装:
                        Stats[Stat.MaxMC] += 2;
                        Stats[Stat.MaxMAC] += 2;
                        break;
                    case ItemSet.强红玉套:
                        Stats[Stat.MaxMC] += 2;
                        Stats[Stat.MP] += 40;
                        Stats[Stat.敏捷] += 2;
                        break;
                    case ItemSet.软玉套装:
                        Stats[Stat.MaxSC] += 2;
                        Stats[Stat.MaxAC] += 1;
                        Stats[Stat.MaxMAC] += 1;
                        break;
                    case ItemSet.强软玉套:
                        Stats[Stat.MaxSC] += 2;
                        Stats[Stat.HP] += 15;
                        Stats[Stat.MP] += 20;
                        Stats[Stat.神圣] += 1;
                        Stats[Stat.准确] += 1;
                        break;
                    case ItemSet.贵人战套:
                        Stats[Stat.MaxDC] += 1;
                        Stats[Stat.背包负重] += 25;
                        break;
                    case ItemSet.贵人法套:
                        Stats[Stat.MaxMC] += 1;
                        Stats[Stat.背包负重] += 17;
                        break;
                    case ItemSet.贵人道套:
                        Stats[Stat.MaxSC] += 1;
                        Stats[Stat.背包负重] += 17;
                        break;
                    case ItemSet.贵人刺套:
                        Stats[Stat.MaxDC] += 1;
                        Stats[Stat.背包负重] += 20;
                        break;
                    case ItemSet.贵人弓套:
                        Stats[Stat.MaxDC] += 1;
                        Stats[Stat.背包负重] += 17;
                        break;
                    case ItemSet.龙血套装:
                        Stats[Stat.MaxSC] += 2;
                        Stats[Stat.HP] += 15;
                        Stats[Stat.MP] += 20;
                        Stats[Stat.神圣] += 1;
                        Stats[Stat.准确] += 1;
                        break;
                    case ItemSet.监视套装:
                        Stats[Stat.魔法躲避] += 1;
                        Stats[Stat.毒物躲避] += 1;
                        break;
                    case ItemSet.暴压套装:
                        Stats[Stat.MaxAC] += 1;
                        Stats[Stat.敏捷] += 1;
                        break;
                    case ItemSet.青玉套装:
                        Stats[Stat.MinDC] += 1;
                        Stats[Stat.MaxDC] += 1;
                        Stats[Stat.MinMC] += 1;
                        Stats[Stat.MaxMC] += 1;
                        Stats[Stat.腕力负重] += 1;
                        Stats[Stat.装备负重] += 2;
                        break;
                    case ItemSet.强青玉套:
                        Stats[Stat.MinDC] += 1;
                        Stats[Stat.MaxDC] += 2;
                        Stats[Stat.MaxMC] += 2;
                        Stats[Stat.准确] += 1;
                        Stats[Stat.HP] += 50;
                        break;
                    case ItemSet.鏃未套装:
                        Stats[Stat.MP] += 25;
                        Stats[Stat.攻击速度] += 2;
                        break;
                }
            }
        }

        private void RefreshMirSetStats()
        {
            if (MirSet.Contains(EquipmentSlot.武器) && MirSet.Contains(EquipmentSlot.盔甲))
            {
                Stats[Stat.武器增伤] += 15;
            }
            if (MirSet.Contains(EquipmentSlot.头盔) && MirSet.Contains(EquipmentSlot.靴子) && MirSet.Contains(EquipmentSlot.腰带))
            {
                Stats[Stat.MaxDC] += 3;
                Stats[Stat.MaxMC] += 3;
                Stats[Stat.MaxSC] += 3;
                Stats[Stat.腕力负重] += 20;
            }
            if (MirSet.Contains(EquipmentSlot.项链) &&
               (MirSet.Contains(EquipmentSlot.左手镯) || MirSet.Contains(EquipmentSlot.右手镯)) &&
               (MirSet.Contains(EquipmentSlot.左戒指) || MirSet.Contains(EquipmentSlot.右戒指)))
            {
                Stats[Stat.MinDC] += 2;
                Stats[Stat.MaxDC] += 6;
                Stats[Stat.MinMC] += 2;
                Stats[Stat.MaxMC] += 6;
                Stats[Stat.MinSC] += 2;
                Stats[Stat.MaxSC] += 6;
                Stats[Stat.攻击速度] += 2;
                Stats[Stat.背包负重] += 60;
                Stats[Stat.装备负重] += 30;
                Stats[Stat.腕力负重] += 30;
            }
            if (MirSet.Contains(EquipmentSlot.盔甲) &&
                MirSet.Contains(EquipmentSlot.武器) &&
                MirSet.Contains(EquipmentSlot.头盔) &&
                MirSet.Contains(EquipmentSlot.靴子) &&
                MirSet.Contains(EquipmentSlot.腰带) &&
                MirSet.Contains(EquipmentSlot.项链) &&
               (MirSet.Contains(EquipmentSlot.左手镯) || MirSet.Contains(EquipmentSlot.右手镯)) &&
               (MirSet.Contains(EquipmentSlot.左戒指) || MirSet.Contains(EquipmentSlot.右戒指)))
            {
                Stats[Stat.MinAC] += 2;
                Stats[Stat.MaxAC] += 6;
                Stats[Stat.MinMAC] += 1;
                Stats[Stat.MaxMAC] += 4;
                Stats[Stat.幸运] += 2;
                Stats[Stat.HP] += 100;
                Stats[Stat.MP] += 100;
                Stats[Stat.中毒恢复] += 2;
            }
        }

        private void RefreshSkills()
        {
            int[] spiritSwordLvPlus = { 0, 3, 5, 8 };
            int[] slayingLvPlus = {5, 6, 7, 8};
            for (int i = 0; i < Magics.Count; i++)
            {
                ClientMagic magic = Magics[i];
                switch (magic.Spell)
                {
                    case Spell.Fencing:
                        Stats[Stat.准确] += magic.Level * 3;
                        //Stats[Stat.MaxAC] += (magic.Level + 1) * 3;
                        break;
                    case Spell.Slaying:
                        // case Spell.FatalSword:
                        Stats[Stat.准确] += magic.Level;
                        Stats[Stat.MaxDC] += slayingLvPlus[magic.Level];
                        break;
                    case Spell.SpiritSword:
                        Stats[Stat.准确] += spiritSwordLvPlus[magic.Level];
                        // Stats[Stat.准确] += magic.Level;
                        // Stats[Stat.MaxDC] += (int)(Stats[Stat.MaxSC] * (magic.Level + 1) * 0.1F);
                        break;
                }
            }
        }

        private void RefreshBuffs()
        {
            TransformType = -1;
            BuffDialog dialog = GetBuffDialog;

            for (int i = 0; i < dialog.Buffs.Count; i++)
            {
                ClientBuff buff = dialog.Buffs[i];

                Stats.Add(buff.Stats);

                switch (buff.Type)
                {
                    case BuffType.轻身步:
                        Sprint = true;
                        break;
                    case BuffType.变形效果:
                        if (buff.Paused) continue;
                        TransformType = (short)buff.Values[0];
                        FastRun = true;
                        break;
                }
            }
        }

        public void RefreshGuildBuffs()
        {
            if (User != this) return;
            if (GameScene.Scene.GuildDialog == null) return;
            for (int i = 0; i < GameScene.Scene.GuildDialog.EnabledBuffs.Count; i++)
            {
                GuildBuff buff = GameScene.Scene.GuildDialog.EnabledBuffs[i];
                if (buff == null) continue;
                if (!buff.Active) continue;

                if (buff.Info == null)
                {
                    buff.Info = GameScene.Scene.GuildDialog.FindGuildBuffInfo(buff.Id);
                }

                if (buff.Info == null) continue;

                Stats.Add(buff.Info.Stats);
            }
        }

        public void RefreshStatCaps()
        {
            foreach (var cap in CoreStats.Caps.Values)
            {
                Stats[cap.Key] = Math.Min(cap.Value, Stats[cap.Key]);
            }

            Stats[Stat.HP] = Math.Max(0, Stats[Stat.HP]);
            Stats[Stat.MP] = Math.Max(0, Stats[Stat.MP]);

            Stats[Stat.MinAC] = Math.Max(0, Stats[Stat.MinAC]);
            Stats[Stat.MaxAC] = Math.Max(0, Stats[Stat.MaxAC]);
            Stats[Stat.MinMAC] = Math.Max(0, Stats[Stat.MinMAC]);
            Stats[Stat.MaxMAC] = Math.Max(0, Stats[Stat.MaxMAC]);
            Stats[Stat.MinDC] = Math.Max(0, Stats[Stat.MinDC]);
            Stats[Stat.MaxDC] = Math.Max(0, Stats[Stat.MaxDC]);
            Stats[Stat.MinMC] = Math.Max(0, Stats[Stat.MinMC]);
            Stats[Stat.MaxMC] = Math.Max(0, Stats[Stat.MaxMC]);
            Stats[Stat.MinSC] = Math.Max(0, Stats[Stat.MinSC]);
            Stats[Stat.MaxSC] = Math.Max(0, Stats[Stat.MaxSC]);

            Stats[Stat.MinDC] = Math.Min(Stats[Stat.MinDC], Stats[Stat.MaxDC]);
            Stats[Stat.MinMC] = Math.Min(Stats[Stat.MinMC], Stats[Stat.MaxMC]);
            Stats[Stat.MinSC] = Math.Min(Stats[Stat.MinSC], Stats[Stat.MaxSC]);
        }

        public void BindAllItems()
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == null) continue;
                GameScene.Bind(Inventory[i]);
            }

            for (int i = 0; i < Equipment.Length; i++)
            {
                if (Equipment[i] == null) continue;
                GameScene.Bind(Equipment[i]);
            }

            for (int i = 0; i < QuestInventory.Length; i++)
            {
                if (QuestInventory[i] == null) continue;
                GameScene.Bind(QuestInventory[i]);
            }
        }


        public ClientMagic GetMagic(Spell spell)
        {
            for (int i = 0; i < Magics.Count; i++)
            {
                ClientMagic magic = Magics[i];
                if (magic.Spell != spell) continue;
                return magic;
            }

            return null;
        }


        public void GetMaxGain(UserItem item)
        {
            int freeSpace = FreeSpace(Inventory);

            if (freeSpace > 0)
            {
                return;
            }

            ushort canGain = 0;

            foreach (UserItem inventoryItem in Inventory)
            {
                if (inventoryItem.Info != item.Info)
                {
                    continue;
                }

                int availableStack = inventoryItem.Info.StackSize - inventoryItem.Count;

                if (availableStack == 0)
                {
                    continue;
                }

                canGain += (ushort)availableStack;

                if (canGain >= item.Count)
                {
                    return;
                }
            }

            if (canGain == 0)
            {
                item.Count = 0;
                return;
            }

            item.Count = canGain;
        }
        private int FreeSpace(UserItem[] array)
        {
            int freeSlots = 0;

            foreach (UserItem slot in array)
            {
                if (slot == null)
                {
                    freeSlots++;
                }
            }

            return freeSlots;
        }

        public override void SetAction()
        {
            if (QueuedAction != null && !GameScene.Observing)
            {
                if ((ActionFeed.Count == 0) || (ActionFeed.Count == 1 && NextAction.Action == MirAction.站立姿势))
                {
                    ActionFeed.Clear();
                    ActionFeed.Add(QueuedAction);
                    QueuedAction = null;
                }
            }

            base.SetAction();
        }
        public override void ProcessFrames()
        {
            bool clear = CMain.Time >= NextMotion;

            base.ProcessFrames();

            if (clear) QueuedAction = null;
            if ((CurrentAction == MirAction.站立动作 || CurrentAction == MirAction.坐骑站立 || CurrentAction == MirAction.站立姿势 || CurrentAction == MirAction.站立姿势2 || CurrentAction == MirAction.冲击失败) && (QueuedAction != null || NextAction != null))
                SetAction();
        }

        public void ClearMagic()
        {
            NextMagic = null;
            NextMagicDirection = 0;
            NextMagicLocation = Point.Empty;
            NextMagicObject = null;
        } 
    }
}

