using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
using Client.MirNetwork;
using Timer = System.Windows.Forms.Timer;
using Font = System.Drawing.Font;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class StuntDialog : MirImageControl
    {
        public MirLabel StuntDialogTitleText, StuntPointsText;
        public MirImageControl StuntPointsImageControl;
        public MirButton CloseButton, HelpButton, ExtractButton, RepairStuntButton, ObtainStuntButton, StuntGridBarLockButton;
        public MirItemCell[] Grid;
        public MirItemCell[] StuntExtractGrid = new MirItemCell[12];

        public static UserItem[] Items = new UserItem[12];
        public static int[] ItemsIdx = new int[12];

        public int StuntPoints;

        private Timer _reEnableTimer;

        private MirGridType GridType;


        public StuntDialog()
        {
            {
                Index = 1180;
                Library = Libraries.Title;
                Movable = true;
                Sort = true;
                Location = new Point(515, 33);

                StuntDialogTitleText = new MirLabel
                {
                    Text = "绝技盒",
                    Parent = this,
                    Font = new Font("微软雅黑", 11F, FontStyle.Bold),
                    ForeColour = Color.Orange,
                    DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                    Size = new Size(50, 20),
                    Location = new Point(35, 5)
                };

                CloseButton = new MirButton
                {
                    HoverIndex = 361,
                    Index = 360,
                    Location = new Point(220, 3),
                    Library = Libraries.Prguse2,
                    Parent = this,
                    PressedIndex = 362,
                    Sound = SoundList.ButtonA,
                };
                CloseButton.Click += (o, e) => Hide();

                HelpButton = new MirButton
                {
                    Index = 257,
                    HoverIndex = 258,
                    PressedIndex = 259,
                    Location = new Point(195, 3),
                    Library = Libraries.Prguse2,
                    Parent = this,
                    Sound = SoundList.ButtonA,
                };
                HelpButton.Click += (o, e) => GameScene.Scene.HelpDialog.DisplayPage("绝技");

                StuntPointsImageControl = new MirImageControl
                {
                    Index = 1261,
                    Library = Libraries.Title,
                    Parent = this,
                    Location = new Point(46, 156),
                };

                StuntPointsText = new MirLabel
                {
                    AutoSize = true,
                    Parent = this,
                    Location = new Point(75, 160),
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    Size = new Size(120, 20),
                    Text = "0",
                    NotControl = true,
                    Visible = true,
                };

                Grid = new MirItemCell[Enum.GetNames(typeof(StuntSlot)).Length];
                InitializeStuntGrid();
                InitializeStuntExtractGrid();

                ExtractButton = new MirButton
                {
                    Index = 1220,
                    HoverIndex = 1221,
                    PressedIndex = 1222,
                    Library = Libraries.Title,
                    Parent = this,
                    Sound = SoundList.ButtonA,
                    Location = new Point(95, 191)
                };
                ExtractButton.Click += (o, e) =>
                {
                    for (int i = 0; i < StuntExtractGrid.Length; i++)
                    {
                        MirItemCell cell = StuntExtractGrid[i];

                        if (cell.Item != null)
                        {
                            Network.Enqueue(new C.DisassembleStuntItems
                            {
                                UniqueID = cell.Item.UniqueID
                            });

                            int inventoryIndex = StuntDialog.ItemsIdx[i];

                            if (inventoryIndex >= 0 && inventoryIndex < GameScene.User.Inventory.Length)
                            {
                                if (inventoryIndex < GameScene.User.BeltIdx)
                                    GameScene.Scene.BeltDialog.Grid[inventoryIndex].Locked = false;
                                else
                                    GameScene.Scene.InventoryDialog.Grid[inventoryIndex - GameScene.User.BeltIdx].Locked = false;
                            }

                            StuntDialog.ItemsIdx[i] = -1;
                            cell.Item = null;
                        }
                    }
                };

                ObtainStuntButton = new MirButton
                {
                    Index = 1190,
                    HoverIndex = 1191,
                    PressedIndex = 1192,
                    Library = Libraries.Title,
                    Parent = this,
                    Sound = SoundList.ButtonA,
                    Location = new Point(35, 315)
                };
                ObtainStuntButton.Click += (o, e) =>
                {
                    GameScene.Scene.StuntDialog.ClearStuntExtractItems();
                    GameScene.Scene.StuntDialog.Hide();
                    GameScene.Scene.StuntObtainDialog.Show();
                };

                RepairStuntButton = new MirButton
                {
                    Index = 1256,
                    HoverIndex = 1257,
                    PressedIndex = 1258,
                    Library = Libraries.Title,
                    Parent = this,
                    Sound = SoundList.ButtonA,
                    Location = new Point(128, 315)
                };
                RepairStuntButton.Click += (o, e) =>
                {
                    Network.Enqueue(new C.RepairStuntItems());
                };

                StuntGridBarLockButton = new MirButton
                {
                    Index = 1186,
                    Library = Libraries.Title,
                    Parent = this,
                    Sound = SoundList.ButtonA,
                    Location = new Point(177, 224),
                    Visible = true,
                };
            }
        }
        private void InitializeStuntGrid()
        {
            Grid[(int)StuntSlot.StuntDestroy] = new MirItemCell
            {
                ItemSlot = (int)StuntSlot.StuntDestroy,
                GridType = MirGridType.Stunt,
                Parent = this,
                Size = new Size(34, 30),
                Location = new Point(32, 226),
                Visible = true,
                Enabled = true

            };

            Grid[(int)StuntSlot.StuntGuard] = new MirItemCell
            {
                ItemSlot = (int)StuntSlot.StuntGuard,
                GridType = MirGridType.Stunt,
                Parent = this,
                Size = new Size(34, 30),
                Location = new Point(81, 226),
                Visible = true,
                Enabled = true
            };

            Grid[(int)StuntSlot.StuntMedicine] = new MirItemCell
            {
                ItemSlot = (int)StuntSlot.StuntMedicine,
                GridType = MirGridType.Stunt,
                Parent = this,
                Size = new Size(34, 30),
                Location = new Point(130, 226),
                Visible = true,
                Enabled = true
            };

            Grid[(int)StuntSlot.StuntAll] = new MirItemCell
            {
                ItemSlot = (int)StuntSlot.StuntAll,
                GridType = MirGridType.Stunt,
                Parent = this,
                Size = new Size(34, 30),
                Location = new Point(179, 226),
                Visible = false,
                Enabled = false
            };

        }

        private void InitializeStuntExtractGrid()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    int idx = 4 * y + x;
                    StuntExtractGrid[idx] = new MirItemCell
                    {
                        ItemSlot = idx,
                        GridType = MirGridType.StuntExtractItem,
                        Library = Libraries.Items,
                        Parent = this,
                        Size = new Size(36, 32),
                        Location = new Point(x * 36 + 49 + x, y * 32 + 59 + y),
                    };
                }
            }
        }
        public override void Show()
        {
            UserItem item = GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.护身符].Item;

            if (item == null || item.Info.Shape != 5)
            {
                Hide();
                return;
            }

            if (Visible) return;

            Visible = true;
        }

        public override void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
        public void RefreshDialog()//暂未使用
        {
            UserItem StuntItem = GameScene.User.Equipment[(int)EquipmentSlot.护身符];
            UserItem[] StuntSlots = null;

            if (StuntItem != null)
            {
                StuntSlots = StuntItem.Slots;
            }

            if (StuntSlots == null) return;
        }

        public MirItemCell GetCell(ulong id)
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i].Item == null || Grid[i].Item.UniqueID != id) continue;
                return Grid[i];
            }
            return null;
        }

        public void ClearStuntExtractItems()
        {
            for (int i = 0; i < StuntExtractGrid.Length; i++)
            {
                if (StuntExtractGrid[i].Item == null)
                    continue;

                int inventoryIndex = ItemsIdx[i];

                if (inventoryIndex >= 0 && inventoryIndex < GameScene.User.Inventory.Length)
                {
                    if (inventoryIndex < GameScene.User.BeltIdx)
                        GameScene.Scene.BeltDialog.Grid[inventoryIndex].Locked = false;
                    else
                        GameScene.Scene.InventoryDialog.Grid[inventoryIndex - GameScene.User.BeltIdx].Locked = false;
                }

                ItemsIdx[i] = -1;
                StuntExtractGrid[i].Item = null;
            }
        }
    }
    public sealed class StuntObtainDialog : MirImageControl
    {
        public MirLabel TitleStuntObtainLabel, StuntPointsText;
        public MirImageControl StuntObtainPage, StuntObtainCountPage;
        public MirButton CloseButton, StuntRaffleButton;
        public MirAnimatedControl StuntObtainDisplay, StuntRaffleDisplay;
        public int StuntPoints;
        public StuntObtainDialog()
        {
            Index = 1182;
            Library = Libraries.Title;
            Movable = true;
            Sort = true;
            Location = new Point(190, 130);

            StuntObtainDisplay = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 30,
                AnimationDelay = 250,
                Index = 1630,
                Library = Libraries.Prguse2,
                Location = new Point(250, 260),
                Parent = this,
                UseOffSet = true,
                Blending = true,
                BlendingRate = 1F,
                Visible = false
            };

            TitleStuntObtainLabel = new MirLabel
            {
                Text = "抽取绝技",
                Parent = this,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColour = Color.Orange,
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Size = new Size(70, 20),
                Location = new Point(40, 6)
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(548, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            StuntObtainCountPage = new MirImageControl
            {
                Index = 1261,
                Library = Libraries.Title,
                Parent = this,
                Location = new Point(290, 358),
            };

            StuntPointsText = new MirLabel
            {
                AutoSize = true,
                Parent = this,
                Location = new Point(320, 363),
                DrawFormat = TextFormatFlags.VerticalCenter,
                Size = new Size(120, 20),
                Text = "0",
                NotControl = true,
            };

            StuntRaffleButton = new MirButton
            {
                Index = 1262,
                HoverIndex = 1263,
                PressedIndex = 1264,
                Library = Libraries.Title,
                Parent = this,
                Sound = SoundList.ButtonA,
                Location = new Point(450, 358)
            };

            StuntRaffleDisplay = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 12,
                AnimationDelay = 250,
                Index = 20,
                Library = Libraries.Effect_32bit,
                Location = new Point(460, 356),
                Parent = this,
                UseOffSet = true,
                Blending = true,
                BlendingRate = 1F,
                Enabled = true,
                NotControl = true,
                Visible = false
            };

            StuntRaffleButton.Click += (sender, e) =>
            {
                GameScene.Scene.StuntlotteryDialog.Show();
                Network.Enqueue(new C.GetStuntlucky());
            };
        }

        public override void Show()
        {
            UserItem item = GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.护身符].Item;

            if (item == null || item.Info.Shape != 5)
            {
                Hide();
                return;
            }

            if (Visible) return;

            Visible = true;
        }

        public override void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
    }

    public sealed class StuntlotteryDialog : MirImageControl
    {
        public MirButton CloseButton, StuntlotteryButton;
        public MirAnimatedControl StuntlotteryDisplay;

        public StuntlotteryDialog()
        {
            Index = 1188;
            Library = Libraries.Title;
            Movable = false;
            Sort = true;
            Location = new Point(200, 120);

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(149, 7),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            StuntlotteryDisplay = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 10,
                AnimationDelay = 250,
                Index = 1670,
                Library = Libraries.Prguse2,
                Location = new Point(67, 47),
                Parent = this,
                UseOffSet = true,
                Blending = true,
                BlendingRate = 1F,
                Visible = false
            };

            StuntlotteryButton = new MirButton
            {
                Index = 1185,
                Library = Libraries.Title,
                Parent = this,
                Sound = SoundList.ButtonA,
                Location = new Point(67, 47)
            };

            StuntlotteryButton.Click += (sender, e) =>
            {
                StuntDialogService.RequestLottery();
                GameScene.Scene.ChatDialog.ReceiveChat("抽奖成功", ChatType.System);
                Visible = false;
            };
        }

        public override void Show()
        {
            if (Visible) return;

            Parent = GameScene.Scene.StuntObtainDialog;
            Visible = true;
        }

        public override void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
    }

    public static class StuntDialogService
    {
        public static void RequestLottery()
        {
            Network.Enqueue(new C.LotteryStuntItems());
        }

        public static void UpdateStuntPoints()

        {
            Network.Enqueue(new C.StuntBox());
        }

        public static void UpdateStuntBoostStatus(bool isBoostActive)
        {
            if (GameScene.Scene.StuntObtainDialog != null)
            {
                GameScene.Scene.StuntObtainDialog.StuntRaffleDisplay.Visible = isBoostActive;
                GameScene.Scene.StuntObtainDialog.StuntObtainDisplay.Visible = isBoostActive;
            }

            if (GameScene.Scene.StuntlotteryDialog != null)
            {
                GameScene.Scene.StuntlotteryDialog.StuntlotteryDisplay.Visible = isBoostActive;
            }
        }
    }
}
