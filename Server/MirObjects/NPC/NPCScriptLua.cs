using System.Drawing;
using Server.MirDatabase;
using Server.MirEnvir;
using System.Text.RegularExpressions;
using S = ServerPackets;
using NLua;

namespace Server.MirObjects
{

    public partial class NPCScript
    {
        public string LuaFileName = null;

        Lua lua = new Lua();
        public void LoadLua()
        {
            lua.RegisterFunction("GIVEGOLD", this, typeof(NPCScript).GetMethod("GIVEGOLD"));
            lua.RegisterFunction("MOVE", this, typeof(NPCScript).GetMethod("MOVE"));
            try
            {
                lua.DoFile(LuaFileName);
            }
            catch (Exception)
            {

                throw;
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
                catch (Exception)
                {
                    throw;
                }

            else
                try
                {
                    lua.DoString(key + "()");
                }
                catch (Exception)
                {
                    throw;
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

        public void GIVEGOLD(uint gold)
        {
            var player = lua["player"] as PlayerObject;
            player.GainGold(gold);
        }
        public void MOVE(string MapNumber, string X_Coord, string Y_Coord)
        {
            //Map map = Envir.GetMapByNameAndInstance(MapNumber);
            //if (map == null) return;

            //if (!int.TryParse(X_Coord, out int x)) return;
            //if (!int.TryParse(Y_Coord, out int y)) return;

            //var coords = new Point(x, y);

            //if (coords.X > 0 && coords.Y > 0) player.Teleport(map, coords);
            //else player.TeleportRandom(200, 0, map);
        }
    }
}