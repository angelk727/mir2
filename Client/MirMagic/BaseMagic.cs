using Client.MirObjects;
using Client.MirScenes.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effect = Client.MirObjects.Effect;
using Client.MirGraphics;
using System.Drawing;

namespace Client.MirMagic
{
    public class BaseMagic
    {
        public virtual void OnMagicBegin(PlayerObject player)
        {

        }

        public virtual void OnDrawEffect(PlayerObject player, MirAction action)
        {

        }
    }
}
