using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirMagic
{
    public class MagicMgr
    {
        private static Dictionary<Spell, BaseMagic> MagicTable = new Dictionary<Spell, BaseMagic>();

        static MagicMgr()
        {
            //MagicTable.Add(Spell.LuoHanGunFa, new LuoHanGunFa());
            //MagicTable.Add(Spell.DaMoGunFa, new DaMoGunFa());
        }

        public static BaseMagic Get(Spell spell)
        {
            if (MagicTable.ContainsKey(spell))
                return MagicTable[spell];

            return null;
        }
    }
}
