using Client.MirGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Client.MirControls
{
    public class MirImageBar : MirImageControl
    {
        public double Percent;

        protected internal override void DrawControl()
        {
            base.DrawControl();

            if (DrawImage && Library != null)
            {
                Rectangle section = new Rectangle
                {
                    Size = new Size((int)((Size.Width - 3) * Percent), Size.Height)
                };

                Library.Draw(Index, section, DisplayLocation, Color.White, false);
            }
        }
    }
}
