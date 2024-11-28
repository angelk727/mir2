using Client.MirNetwork;
using Client.MirScenes;
using Client.MirScenes.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using C = ClientPackets;

namespace Client.MirObjects
{
    public class AutoSkill
    {
        public UserObject User
        {
            get
            {
                return GameScene.User;
            }
        }
        public long NextSpellTime;
        public int Invertal;
        public Spell Spell;

        public AutoSkill(Spell spell, int invertal)
        {
            Spell = spell;
            Invertal = invertal;
        }

        public bool IsToggle()
        {
            switch (Spell)
            {
                case Spell.MagicShield:
                    return Settings.SmartSheild;

                case Spell.FlamingSword:
                    return Settings.SmartFireHit;
                
                case Spell.TwinDrakeBlade:
                    return Settings.自动双龙斩;


                case Spell.ElementalBarrier:
                    return Settings.SmartElementalBarrier;

            }

            return false;
        }

        public virtual bool CheckState()
        {
            switch(Spell)
            {
                case Spell.MagicShield:
                    return User.MagicShield;

                case Spell.ElementalBarrier:
                    return User.ElementalBarrier;

            }

            return false;
        }

        public bool Process()
        {
            if (!IsToggle())
                return false;

            if (CMain.Time < NextSpellTime)
                return false;

            if (CheckState())
                return false;

            UserObject User = GameScene.User;
            ClientMagic magic = User.GetMagic(Spell);
            if (magic == null)
                return false;

            if (User.IsMagicInCD(Spell) || !User.CheckMagicMP(magic))
                return false;

            NextSpellTime = CMain.Time + Invertal;
            GameScene.Scene.UseSpell(Spell);
            return true;
        }
    }

    public class AssistHelper
    {
        //private long lastUseItemTick;
        private long[] UseItemTime = new long[3];

        private byte useAmuletShape = 1;

        public bool AutoAttack;
        private MapObject LastTargetObject;
        private MapObject TargetObject;
        private MirDirection FindDirection;
        private long FindTargetTime;
        private long RandomDirectionTime;
        private long MaxAttackDist = 20;
        private string[] IgnoreMonsterName = { "变异骷髅", "大刀", "弓箭手", "神兽", "月灵", "带刀" };
        private List<Node> CurrentPath;
        private long NextActionTime;
        private long PickItemTime;
        private HashSet<MapObject> BlackObject = new HashSet<MapObject>();
        private int KillCount;
        private long BeginTime;

        private Dictionary<string, ItemFilter> itemFilterList = new Dictionary<string, ItemFilter>();

        public List<AutoSkill> AutoList = new List<AutoSkill>();

        internal Dictionary<string, ItemFilter> ItemFilterList
        {
            get
            {
                return itemFilterList;
            }

            set
            {
                itemFilterList = value;
            }
        }

        public AssistHelper()
        {
            AutoList.Add(new AutoSkill(Spell.FlamingSword, 500));
            AutoList.Add(new AutoSkill(Spell.TwinDrakeBlade, 3000));
            AutoList.Add(new AutoSkill(Spell.MagicShield, 500));
            AutoList.Add(new AutoSkill(Spell.ElementalBarrier, 1000));
        }

        public void Process()
        {
            for (int i=0; i<AutoList.Count; ++i)
            {
                if (AutoList[i].Process())
                    break;
            }

            AutoUseItem();
        }

        private void ClearBlackObjects()
        {
            BlackObject.RemoveWhere(o => { return GameScene.Scene.MapControl.FindObject(o.ObjectID, o.CurrentLocation.X, o.CurrentLocation.Y) == null; });
        }

        private void FindTarget()
        {
            if (FindTargetTime >= CMain.Time)
                return;

            if (!NeedFindTarget())
                return;

            LastTargetObject = TargetObject;
            TargetObject = null;
            PickItemTime = 0;
            UserObject User = UserObject.User;
            int MinDist = int.MaxValue;
            for (int i = 0; i < MapControl.Objects.Count; ++i)
            {
                MapObject obj = MapControl.Objects[i];

                if (!CanBeTarget(obj))
                    continue;

                int dist = Functions.Distance(User.CurrentLocation, obj.CurrentLocation);
                if (dist < MinDist)
                {
                    TargetObject = obj;
                    FindTargetTime = CMain.Time + 1000;
                    MinDist = dist;
                }
            }

            if (TargetObject != null)
            {
                int dist = Functions.Distance(User.CurrentLocation, TargetObject.CurrentLocation);
                FindTargetTime = CMain.Time + 1000;
                if (CurrentPath != null)
                    CurrentPath.Clear();
                GameScene.Scene.ChatDialog.ReceiveChat(string.Format("发现目标:{0} L:{1},{2} 坐标:{3},{4} 距离:{5}", TargetObject.Name, 
                    User.CurrentLocation.X, User.CurrentLocation.Y, TargetObject.CurrentLocation.X, TargetObject.CurrentLocation.Y, dist), ChatType.System);
                return;
            }

            if (CMain.Time >= RandomDirectionTime)
            {
                MapControl MapControl = GameScene.Scene.MapControl;
                Point pt = Functions.PointMove(User.CurrentLocation, FindDirection, 1);
                if (!MapControl.EmptyCell(pt))
                {
                    FindDirection = (MirDirection)CMain.Random.Next(8);
                    for (int i = 0; i < 8; i++)
                    {
                        if (MapControl.EmptyCell(pt))
                            break;

                        FindDirection = Functions.NextDir(FindDirection);
                        pt = Functions.PointMove(User.CurrentLocation, FindDirection, 1);
                    }
                }

                RandomDirectionTime = CMain.Time + 1000;
                GameScene.Scene.ChatDialog.ReceiveChat("周围没有目标，开始游荡", ChatType.System);
            }

            Move(FindDirection);
        }

        private bool CanBeTarget(MapObject obj)
        {
            if (obj == LastTargetObject)
                return false;

            if (BlackObject.Contains(obj))
                return false;

            if (obj is MonsterObject && !obj.Dead && CheckCanAttack(obj))
                return true;

            if (Settings.AutoPick && obj is ItemObject && NeedPick(obj.Name)) 
                return true;

            return false;
        }

        private bool CheckCanAttack(MapObject obj)
        {
            for (int i=0; i< IgnoreMonsterName.Length; ++i)
            {
                if (obj.Name.Contains(IgnoreMonsterName[i]))
                    return false;
            }

            return true;
        }

        private bool NeedFindTarget()
        {
            if (TargetObject == null)
                return true;

            UserObject User = UserObject.User;
            if (TargetObject.Dead)
            {
                KillCount++;
                int second =(int) (CMain.Time - BeginTime)/60000;
                GameScene.Scene.ChatDialog.ReceiveChat(string.Format("目标死亡,杀敌数:{0} 耗时:{1}分钟 平均:{2}杀/分", KillCount, second, second != 0 ? KillCount / second : 0), ChatType.System);
                return true; ;
            }

            int dist = Functions.MaxDistance(User.CurrentLocation, TargetObject.CurrentLocation);
            if (dist > MaxAttackDist)
            {
                GameScene.Scene.ChatDialog.ReceiveChat("目标超出范围", ChatType.System);
                return true;
            }

            if (GameScene.Scene.MapControl.FindObject(TargetObject.ObjectID, TargetObject.CurrentLocation.X, TargetObject.CurrentLocation.Y) == null)
            {
                GameScene.Scene.ChatDialog.ReceiveChat("找不到目标", ChatType.System);
                return true;
            }

            if (PickItemTime > 0 && CMain.Time >= PickItemTime)
            {
                GameScene.Scene.ChatDialog.ReceiveChat("拾取超时", ChatType.System);
                BlackObject.Add(TargetObject);
                return true;
            }

            if (TargetObject is ItemObject && TargetObject.CurrentLocation != User.CurrentLocation
                && !GameScene.Scene.MapControl.EmptyCell(TargetObject.CurrentLocation))
            {
                GameScene.Scene.ChatDialog.ReceiveChat("无法到达物品位置", ChatType.System);
                return true;
            }
          
            return false;
        }

        private bool ProcessTarget()
        {
            if (TargetObject == null)
                return false;

            UserObject User = UserObject.User;
            if (User.Poison.HasFlag(PoisonType.Paralysis) || User.Poison.HasFlag(PoisonType.LRParalysis) || User.Poison.HasFlag(PoisonType.Frozen) || User.Fishing)
                return false;

            MapControl MapControl = GameScene.Scene.MapControl;
            if (Functions.InRange(TargetObject.CurrentLocation, User.CurrentLocation, 1))
            {
                if (TargetObject is MonsterObject)
                {
                    if (CMain.Time > GameScene.AttackTime && User.CanRideAttack() && CMain.Time > NextActionTime)
                    {
                        MapObject.TargetObject = TargetObject;

                        if (ProcessSpell())
                            return true;

                        ProcessAttack();

                        User.QueuedAction = new QueuedAction { Action = MirAction.近距攻击1, Direction = Functions.DirectionFromPoint(User.CurrentLocation, TargetObject.CurrentLocation), Location = User.CurrentLocation };
                        return true;
                    }
                }
                else if (TargetObject is ItemObject)
                {
                    if (User.CurrentLocation == TargetObject.CurrentLocation)
                    {
                        if (PickItemTime == 0)
                            PickItemTime = CMain.Time + 2000;
                    }
                    else
                    {
                        MirDirection direction = Functions.DirectionFromPoint(User.CurrentLocation, TargetObject.CurrentLocation);
                        if (User.CanWalk(direction))
                            User.QueuedAction = new QueuedAction {
                                Action = MirAction.行走动作, Direction = direction,
                                Location = Functions.PointMove(User.CurrentLocation, direction, 1) };
                    }
                }
            }
            else
            {
                if (CurrentPath == null || CurrentPath.Count == 0)
                {
                    if (TargetObject is MonsterObject)
                    {
                        MirDirection direction = Functions.DirectionFromPoint(TargetObject.CurrentLocation, User.CurrentLocation);
                        Point pt = Functions.PointMove(TargetObject.CurrentLocation, direction, 1);
                        if (!MapControl.EmptyCell(pt))
                        {
                            direction = (MirDirection)CMain.Random.Next(8);
                            for (int i = 0; i < 7; i++)
                            {
                                if (MapControl.EmptyCell(pt))
                                    break;

                                direction = Functions.NextDir(direction);
                                pt = Functions.PointMove(TargetObject.CurrentLocation, direction, 1);
                            }
                        }
                        CurrentPath = GameScene.Scene.MapControl.PathFinder.FindPath(User.CurrentLocation, pt);
                    }
                    else
                    {
                        CurrentPath = GameScene.Scene.MapControl.PathFinder.FindPath(User.CurrentLocation, TargetObject.CurrentLocation);
                    }

                    GameScene.Scene.ChatDialog.ReceiveChat("搜索路径...", ChatType.System);
                    if (CurrentPath == null || CurrentPath.Count == 0)
                        GameScene.Scene.ChatDialog.ReceiveChat("搜索路径失败...", ChatType.System);
                }

                if (CurrentPath != null && CurrentPath.Count > 0)
                    Move2();
            }

            return true;
        }

        private void ProcessAttack()
        {
            UserObject User = MapObject.User;
            if (User.GetMagic(Spell.TwinDrakeBlade) != null)
            {
                GameScene.Scene.UseSpell(Spell.TwinDrakeBlade);
                return;
            }

            if (User.GetMagic(Spell.FlashDash) != null && !User.IsMagicInCD(Spell.FlashDash))
            {
                GameScene.Scene.UseSpell(Spell.FlashDash);
                return;
            }
        }

        private bool ProcessSpell()
        {
            UserObject User = MapObject.User;
            ClientMagic magic = User.GetMagic(Spell.PoisonSword);
            if (magic != null && !User.IsMagicInCD(Spell.PoisonSword) && User.CheckMagicMP(magic)
                && TargetObject != null && !TargetObject.Poison.HasFlag(PoisonType.Green) && CMain.Time > MapControl.NextAction)
            {
                User.NextMagicLocation = TargetObject.CurrentLocation;
                User.NextMagicObject = TargetObject;
                User.NextMagicDirection = Functions.DirectionFromPoint(User.CurrentLocation, TargetObject.CurrentLocation);
                NextActionTime += 2500;
                GameScene.Scene.MapControl.UseMagic(magic, User);
                return true;
            }

            if (User.GetMagic(Spell.Haste) != null && !User.IsMagicInCD(Spell.Haste) && !GameScene.Scene.Buffs.Any(e => e.Type == BuffType.体迅风))
            {
                GameScene.Scene.UseSpell(Spell.Haste);
                NextActionTime += 2500;
                return true;
            }

            if (User.GetMagic(Spell.LightBody) != null && !User.IsMagicInCD(Spell.LightBody) && !GameScene.Scene.Buffs.Any(e => e.Type == BuffType.风身术))
            {
                GameScene.Scene.UseSpell(Spell.LightBody);
                NextActionTime += 2500;
                return true;
            }

            return false;
        }

        public void SetProtectItemName(int i, string text)
        {
            switch (i)
            {
                case 0:
                    Settings.PercentItem0 = text;
                    break;

                case 1:
                    Settings.PercentItem1 = text;
                    break;

                case 2:
                    Settings.PercentItem2 = text;
                    break;
            }
        }

        public void SetProtectPercent(int i, int temp)
        {
            switch (i)
            {
                case 0:
                    Settings.ProtectPercent0 = temp;
                    break;

                case 1:
                    Settings.ProtectPercent1 = temp;
                    break;

                case 2:
                    Settings.ProtectPercent2 = temp;
                    break;
            }
        }

        public string GetProtectItemName(int i)
        {
            switch (i)
            {
                case 0:
                    return Settings.PercentItem0;

                case 1:
                    return Settings.PercentItem1;

                case 2:
                    return Settings.PercentItem2;
            }
            return "";
        }

        public int GetProtectPercent(int i)
        {
            switch (i)
            {
                case 0:
                    return Settings.ProtectPercent0;

                case 1:
                    return Settings.ProtectPercent1;

                case 2:
                    return Settings.ProtectPercent2;
            }
            return 0;
        }

        private void Move2()
        {
            UserObject User = UserObject.User;
            Node currentNode = CurrentPath.SingleOrDefault(x => User.CurrentLocation == x.Location);
            if (currentNode != null)
            {
                while (true)
                {
                    Node first = CurrentPath.First();
                    CurrentPath.Remove(first);

                    if (first == currentNode)
                        break;
                }
            }

            if (CurrentPath.Count > 0)
            {
                MirDirection dir = Functions.DirectionFromPoint(User.CurrentLocation, CurrentPath.First().Location);
                Node upcomingStep = CurrentPath.SingleOrDefault(x => Functions.PointMove(User.CurrentLocation, dir, 2) == x.Location);

                if (!User.CanWalk(dir))
                {
                    CurrentPath = GameScene.Scene.MapControl.PathFinder.FindPath(MapObject.User.CurrentLocation, CurrentPath.Last().Location);
                    return;
                }

                if (GameScene.CanRun && User.CanRun(dir) && CMain.Time > GameScene.NextRunTime && User.HP >= 10 && CurrentPath.Count > 1 && upcomingStep != null)
                {
                    User.QueuedAction = new QueuedAction { Action = MirAction.跑步动作, Direction = dir, Location = Functions.PointMove(User.CurrentLocation, dir, 2) };
                    return;
                }
                if (User.CanWalk(dir))
                {
                    User.QueuedAction = new QueuedAction { Action = MirAction.行走动作, Direction = dir, Location = Functions.PointMove(User.CurrentLocation, dir, 1) };

                    return;
                }
            }
        }

        public void ClearAttack()
        {
            TargetObject = null;
        }

        public void Init()
        {
            ItemFilterList.Clear();
            string path = Path.Combine("./Configs/", UserObject.User.Name + "_filter.txt");
            if (!File.Exists(path))
                return;

            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; ++i)
            {
                ItemFilter itemFilter = ItemFilter.FromLine(lines[i]);
                ItemFilterList[itemFilter.Name] = itemFilter;
            }
        }

        public void Save()
        {
            string path = Path.Combine("./Configs/", UserObject.User.Name + "_filter.txt");

            var list = ItemFilterList.Values.Select(item => item.ToText()).ToList();
            File.WriteAllLines(path, list);
        }

        private void Move(MirDirection direction)
        {
            UserObject User = UserObject.User;
            MapControl MapControl = GameScene.Scene.MapControl;
            if (GameScene.CanRun && User.CanRun(direction) && CMain.Time > GameScene.NextRunTime && User.HP >= 10 && (!User.Sneaking || (User.Sneaking && User.Sprint)))
            {
                int distance = User.RidingMount || User.Sprint && !User.Sneaking ? 3 : 2;
                bool fail = false;
                for (int i = 1; i <= distance; i++)
                {
                    if (!MapControl.CheckDoorOpen(Functions.PointMove(User.CurrentLocation, direction, i)))
                        fail = true;
                }
                if (!fail)
                {
                    Point location = Functions.PointMove(User.CurrentLocation, direction, distance);
                    User.QueuedAction = new QueuedAction { Action = MirAction.跑步动作, Direction = direction, Location = location};
                }
            }
            else
            {
                Point location = Functions.PointMove(User.CurrentLocation, direction, 1);
                if (User.CanWalk(direction))
                    User.QueuedAction = new QueuedAction { Action = MirAction.行走动作, Direction = direction, Location = location };
            }
        }

        private void Move1()
        {
            UserObject User = UserObject.User;
            MapControl MapControl = GameScene.Scene.MapControl;
            MirDirection direction = Functions.DirectionFromPoint(User.CurrentLocation, TargetObject.CurrentLocation);

            Move(direction);
        }

        private void AutoUseItem()
        {
            if (Settings.开启保护)
            {
                for (int i=0;i <3; i++)
                {
                    AutoProtect(i);
                }
            }
        }

        private void AutoProtect(int index)
        {
            UserObject User = UserObject.User;
            int value = 0;
            if (index == 0 || index == 2)
                value = User.PercentHealth > 0 ? User.PercentHealth : 100;
            else
                value = User.PercentMana > 0 ? User.PercentMana : 100;

            if (value <= GetProtectPercent(index) && CMain.Time > UseItemTime[index])
            {
                string itemName = GetProtectItemName(index);
                if (string.IsNullOrEmpty(itemName))
                    return;

                UseItemTime[index] = CMain.Time + Settings.UseItemInterval;
                for (int i = 0; i < User.Inventory.Length; i++)
                {
                    UserItem item = User.Inventory[i];
                    
                    if (item != null && item.Info != null && item.Info.Name.Contains(itemName))
                    {
                        Network.Enqueue(new C.UseItem { UniqueID = item.UniqueID, Grid = MirGridType.Inventory });
                        break;
                    }
                }
            }
        }

        private void SellItem()
        {
            UserObject User = UserObject.User;
            for (int i = 0; i < User.Inventory.Length; i++)
            {
                UserItem item = User.Inventory[i];
                if (item == null || item.Info == null || item.Info.Bind.HasFlag(BindMode.DontSell))
                    continue;

                if (GameScene.Gold + item.Price() / 2 <= uint.MaxValue)
                    Network.Enqueue(new C.SellItem { UniqueID = item.UniqueID, Count = item.Count });
            }
        }

        private bool CheckEquipment(int slot, ItemType itemType, int count, int shape)
        {
            UserObject User = UserObject.User;
            UserItem item = User.Equipment[slot];
            return item != null && item.Info.Type == ItemType.护身符 && item.Count >= count && item.Info.Shape == shape;
        }

        public void PrevSendUseMagic(ClientMagic magic)
        {
            UserObject User = UserObject.User;
            switch (magic.Spell)
            {
                case Spell.Poisoning:
                    {
                        if (!Settings.SmartChangePoison)
                            break;

                        bool success = AutoEquipAmulet(useAmuletShape, 1);
                        if (success)
                        {
                            if (++useAmuletShape > 2)
                                useAmuletShape = 1;
                        }
                    }
                    break;
                case Spell.PoisonCloud:
                    {
                        if (!Settings.SmartChangePoison)
                            break;

                        AutoEquipAmulet(1, 1);
                        AutoEquipAmulet(0, 1);
                    }
                    break;

                case Spell.SoulFireBall:
                case Spell.SummonSkeleton:
                case Spell.Hiding:
                case Spell.MassHiding:
                case Spell.SoulShield:
                case Spell.TrapHexagon:
                case Spell.Curse:
                case Spell.Plague:
                case Spell.UltimateEnhancer:
                case Spell.BlessedArmour:
                    {
                        if (!Settings.SmartChangePoison)
                            break;

                        AutoEquipAmulet(0, 1);
                    }
                    break;

                case Spell.SummonHolyDeva:
                    {
                        if (!Settings.SmartChangePoison)
                            break;

                        AutoEquipAmulet(0, 2);
                    }
                    break;

                case Spell.SummonShinsu:
                    {
                        if (!Settings.SmartChangePoison)
                            break;

                        AutoEquipAmulet(0, 5);
                    }
                    break;
            }
        }

        private bool AutoEquipAmulet(byte shape, byte count)
        {
            UserObject User = UserObject.User;
            if (shape != 0)
            {
                if (CheckEquipment((int)EquipmentSlot.盾牌, ItemType.护身符, count, shape))
                    return true;

                UserItem item =  User.GetPoison(count, shape);
                if (item == null)
                    return false;

                Network.Enqueue(new C.EquipItem { Grid = MirGridType.Inventory, UniqueID = item.UniqueID, To = (int)EquipmentSlot.盾牌 });
            }
            else
            {
                if (CheckEquipment((int)EquipmentSlot.护身符, ItemType.护身符, count, shape))
                    return true;

                UserItem item = User.GetAmulet(count);
                if (item == null)
                    return false;

                Network.Enqueue(new C.EquipItem { Grid = MirGridType.Inventory, UniqueID = item.UniqueID, To = (int)EquipmentSlot.护身符 });
            }
          
            return true;
        }

        public bool NeedPick(string name)
        {
            name = Regex.Replace(name, @"\([\d,]+\)", string.Empty).Trim();
            if (!ItemFilterList.ContainsKey(name))
            {
                ItemFilter itemFilter = new ItemFilter() { Name = name, Pick = true, Sell = false };
                ItemFilterList[name] = itemFilter;
                return itemFilter.Pick;
            }
            else
            {
                return ItemFilterList[name].Pick;
            }
        }
    }
}
