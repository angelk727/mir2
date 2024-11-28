namespace Client.MirScenes.Dialogs
{
    using Client;
    using Client.MirControls;
    using Client.MirGraphics;
    using Client.MirObjects;
    using Client.MirScenes;
    using Client.MirScenes.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class GroupInfoDialog : MirImageControl
    {
        public static MirLabel[] Name;
        public static MirImageBar[] Health;

        public GroupInfoDialog()
        {
            this.Size = new Size(160, 600);
            Movable = true;
            base.Location = new Point((Settings.ScreenWidth - Settings.ScreenWidth) + 10, (Settings.ScreenHeight - Settings.ScreenHeight) + 50);
            Name = new MirLabel[15];
            Health = new MirImageBar[15];
            for (int i = 0; i < 15; i++)
            {
                Health[i] = new MirImageBar
                {
                    Index = 1331,
                    Library = Libraries.Prguse2,
                    Location = new Point(42, 20 + 30*i),
                    Parent = this,
                    NotControl = true,
                };

                // 1349

                MirLabel label1 = new MirLabel();
                label1.DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                label1.AutoSize = true;
                label1.Parent = this;
                label1.Visible = false;
                label1.NotControl = true;
                label1.Location = new Point(42, 3 + i * 30);
                label1.OutLineColour = Color.Black;
                label1.OutLine = true;
                Name[i] = label1;
            }
        }

        public void Hide()
        {
            if (this.Visible)
            {
                this.Visible = false;
            }
        }

        public void Show()
        {
            if (!this.Visible)
            {
                this.Visible = true;
            }
        }

        public void Process()
        {
            if (GroupDialog.GroupList.Count <= 1 || !Settings.ShowGroupInfo)
            {
                this.Hide();
                return;
            }

            Show();

            this.Size = new Size(160, 40 * GroupDialog.GroupList.Count);

            int num = 0;
            UpdatePlayerObj(0, GameScene.User.Name, Color.White, (100f * GameScene.User.PercentHealth) / 100f);

            int index = 1;
            foreach (string name in GroupDialog.GroupList)
            {
                if (name == GameScene.User.Name)
                    continue;

                PlayerObject ob = null;
                foreach (MapObject mapObject in MapControl.Objects)
                {
                    if (!(mapObject is PlayerObject))
                        continue;

                    if (name != mapObject.Name)
                        continue;

                    ob = (PlayerObject)mapObject;
                    break;
                }

                if (ob != null)
                    UpdatePlayerObj(index, name, Color.White,  (100f * ob.PercentHealth) / 100f);
                else
                    UpdatePlayerObj(index, name, Color.Gray,  -1);
                ++index;
            }

            for (int i = index; i < 15; ++i)
            {
                Name[i].Visible = false;
                Health[i].Visible = false;
            }
        }

        private void UpdatePlayerObj(int index, string name, Color color,  double percent)
        {
            Name[index].Text = name;
            Name[index].Visible = true;
            Name[index].ForeColour = color;
            if (percent < 0)
            {
                Health[index].Visible = false;
            }
            else
            {
                Health[index].Percent = (100f * percent) / 100f;
                Health[index].Visible = true;
            }
        }
    }
}

