using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using System.Text.RegularExpressions;
using S = ServerPackets;
using NLua;
using System.Text;

namespace Server.MirObjects
{

    public partial class NPCScript
    {
        public string LuaFileName = null;

        Lua lua = new Lua();

        public void LoadLua()
        {
            lua.State.Encoding = Encoding.UTF8;
            //初始化Lua库
            lua.LoadCLRPackage();
            RegistGlobalLuaFunctions();
            try
            {
                lua.DoFile(LuaFileName);
            }
            catch (NLua.Exceptions.LuaException ex)
            {
                MessageQueue.Enqueue($"脚本错误：{ex.Message}");
            }
        }
        public void CallLua(string key)
        {
            key = key.Remove(0, 3);
            key = key.Remove(key.Length - 1);
            if (key.EndsWith(')'))
                try
                {
                    lua.DoString(key);
                }
                catch (NLua.Exceptions.LuaException ex)
                {
                    MessageQueue.Enqueue($"脚本错误：{ex.Message}");
                }

            else
                try
                {
                    lua.DoString(key + "()");
                }
                catch (NLua.Exceptions.LuaException ex)
                {
                    MessageQueue.Enqueue($"脚本错误：{ex.Message}");
                }

        }
        public void CallLua(MonsterObject monster, string key)
        {
            lua["monster"] = monster;
            CallLua(key);
            lua["monster"] = null;
        }
        public void CallLua(PlayerObject player, uint objectID, string key)
        {
            lua["player"] = player;
            lua["objectID"] = objectID;
            CallLua(key);
            lua["player"] = null;
            lua["objectID"] = null;
        }
        #region 注册全局命令
        public void RegistGlobalLuaFunctions()
        {
            lua.RegisterFunction("GIVEGOLD", this, typeof(NPCScript).GetMethod("GIVEGOLD"));
            lua.RegisterFunction("MOVE", this, typeof(NPCScript).GetMethod("MOVE"));
            lua.RegisterFunction("INSTANCEMOVE", this, typeof(NPCScript).GetMethod("INSTANCEMOVE"));
            lua.RegisterFunction("TAKEGOLD", this, typeof(NPCScript).GetMethod("TAKEGOLD"));
            lua.RegisterFunction("GIVEITEM", this, typeof(NPCScript).GetMethod("GIVEITEM"));
            lua.RegisterFunction("TAKEITEM", this, typeof(NPCScript).GetMethod("TAKEITEM"));
        }
        #endregion

        public void GIVEGOLD(uint gold)
        {
            if (lua["player"] is PlayerObject player)
            {
                if (gold + player.Account.Gold >= uint.MaxValue)
                    gold = uint.MaxValue - player.Account.Gold;
                player.GainGold(gold);
            }

        }
        public void MOVE(string MapNumber, int X_Coord, int Y_Coord)
        {
            var player = lua["player"] as PlayerObject;
            Map map = Envir.GetMapByNameAndInstance(MapNumber);
            if (map == null) return;

            var coords = new Point(X_Coord, Y_Coord);

            if (coords.X > 0 && coords.Y > 0) player.Teleport(map, coords);
            else player.TeleportRandom(200, 0, map);
        }
        public void INSTANCEMOVE(string MapNumber, int InstanceID, int X_Coord, int Y_Coord)
        {
            var player = lua["player"] as PlayerObject;

            var map = Envir.GetMapByNameAndInstance(MapNumber, InstanceID);
            if (map == null) return;
            player.Teleport(map, new Point(X_Coord, Y_Coord));
        }
        public void TAKEGOLD(uint gold)
        {
            var player = lua["player"] as PlayerObject;
            if (gold >= player.Account.Gold) gold = player.Account.Gold;
            player.Account.Gold -= gold;
            player.Enqueue(new S.LoseGold { Gold = gold });
        }
        public void GIVEITEM(string ItemName, ushort Amount)
        {
            var player = lua["player"] as PlayerObject;
            var info = Envir.GetItemInfo(ItemName);

            if (info == null)
            {
                MessageQueue.Enqueue("无法获取物品信息: " + ItemName);
                return;
            }

            while (Amount > 0)
            {
                UserItem item = Envir.CreateFreshItem(info);

                if (item == null)
                {
                    MessageQueue.Enqueue("无法创建用户物品" + ItemName);
                    return;
                }

                if (item.Info.StackSize > Amount)
                {
                    item.Count = Amount;
                    Amount = 0;
                }
                else
                {
                    Amount -= item.Info.StackSize;
                    item.Count = item.Info.StackSize;
                }

                if (player.CanGainItem(item))
                    player.GainItem(item);
            }
        }
        public void TAKEITEM(string ItemName, ushort count, int Dura = -1)
        {
            var player = lua["player"] as PlayerObject;
            var info = Envir.GetItemInfo(ItemName);

            if (info == null)
            {
                MessageQueue.Enqueue("TAKEITEM命令未能获取物品信息" + ItemName);
                return;
            }

            for (int j = 0; j < player.Info.Inventory.Length; j++)
            {
                UserItem item = player.Info.Inventory[j];
                if (item == null) continue;
                if (item.Info != info) continue;
                bool checkDura = Dura != -1;
                if (checkDura)
                {
                    if (item.CurrentDura < (Dura * 1000)) continue;
                }


                if (count > item.Count)
                {
                    player.Enqueue(new S.DeleteItem { UniqueID = item.UniqueID, Count = item.Count });
                    player.Info.Inventory[j] = null;

                    count -= item.Count;
                    continue;
                }

                player.Enqueue(new S.DeleteItem { UniqueID = item.UniqueID, Count = count });
                if (count == item.Count)
                    player.Info.Inventory[j] = null;
                else
                    item.Count -= count;
                break;
            }
            player.RefreshStats();
        }
    }
}