using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirScenes;
using Client.MirSounds;
using Client.MirScenes.Dialogs;
using C = ClientPackets;

namespace Client.MirControls
{
    public sealed class MirItemCell : MirImageControl
    {
        public UserItem Item
        {
            get
            {
                if (GridType == MirGridType.DropPanel)
                    return NPCDropDialog.TargetItem;

                if (GridType == MirGridType.TrustMerchant)
                    return TrustMerchantDialog.SellItemSlot;

                if (GridType == MirGridType.Renting)
                    return ItemRentingDialog.RentalItem;

                if (GridType == MirGridType.GuestRenting)
                    return GuestItemRentingDialog.GuestLoanItem;

                if (ItemArray != null && _itemSlot >= 0 && _itemSlot < ItemArray.Length)
                    return ItemArray[_itemSlot];
                return null;
            }
            set
            {
                if (GridType == MirGridType.DropPanel)
                    NPCDropDialog.TargetItem = value;
                else if (GridType == MirGridType.Renting)
                    ItemRentingDialog.RentalItem = value;
                else if (GridType == MirGridType.TrustMerchant)
                    TrustMerchantDialog.SellItemSlot = value;
                else if (GridType == MirGridType.GuestRenting)
                    GuestItemRentingDialog.GuestLoanItem = value;
                else if (ItemArray != null && _itemSlot >= 0 && _itemSlot < ItemArray.Length)
                    ItemArray[_itemSlot] = value;

                SetEffect();
                Redraw();
            }
        }

        public UserItem ShadowItem
        {
            get
            {
                if ((GridType == MirGridType.Craft) && _itemSlot >= 0 && _itemSlot < ItemArray.Length)
                    return CraftDialog.ShadowItems[_itemSlot];

                return null;
            }
        }

        public UserItem[] ItemArray
        {
            get
            {
                switch (GridType)
                {
                    case MirGridType.Inventory:
                        return MapObject.User.Inventory;
                    case MirGridType.Equipment:
                        return MapObject.User.Equipment;
                    case MirGridType.Storage:
                        return GameScene.Storage;
                    case MirGridType.Inspect:
                        return InspectDialog.Items;
                    case MirGridType.GuildStorage:
                        return GameScene.GuildStorage;
                    case MirGridType.Trade:
                        return GameScene.User.Trade;
                    case MirGridType.GuestTrade:
                        return GuestTradeDialog.GuestItems;
                    case MirGridType.Mount:
                        return MapObject.User.Equipment[(int)EquipmentSlot.坐骑].Slots;
                    case MirGridType.Fishing:
                        return MapObject.User.Equipment[(int)EquipmentSlot.武器].Slots;
                    case MirGridType.QuestInventory:
                        return MapObject.User.QuestInventory;
                    case MirGridType.AwakenItem:
                        return NPCAwakeDialog.Items;
                    case MirGridType.Mail:
                        return MailComposeParcelDialog.Items;
                    case MirGridType.Refine:
                        return GameScene.Refine;
                    case MirGridType.Craft:
                        return CraftDialog.Slots;
                    case MirGridType.Socket:
                        return GameScene.SelectedItem?.Slots;
                    case MirGridType.HeroEquipment:
                        return MapObject.Hero.Equipment;
                    case MirGridType.HeroInventory:
                        return MapObject.Hero.Inventory;
                    case MirGridType.HeroHPItem:
                        return MapObject.Hero.HPItem;
                    case MirGridType.HeroMPItem:
                        return MapObject.Hero.MPItem;

                    default:
                        throw new NotImplementedException();
                }

            }
        }

        public override bool Border
        {
            get { return (GameScene.SelectedCell == this || MouseControl == this || Locked) && !(GridType == MirGridType.DropPanel || GridType == MirGridType.Craft); }
        }

        private bool _locked;

        public bool Locked
        {
            get { return _locked; }
            set
            {
                if (_locked == value) return;
                _locked = value;
                Redraw();
            }
        }



        #region GridType

        private MirGridType _gridType;
        public event EventHandler GridTypeChanged;
        public MirGridType GridType
        {
            get { return _gridType; }
            set
            {
                if (_gridType == value) return;
                _gridType = value;
                OnGridTypeChanged();
            }
        }

        private void OnGridTypeChanged()
        {
            if (GridTypeChanged != null)
                GridTypeChanged.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region ItemSlot

        private int _itemSlot;
        public event EventHandler ItemSlotChanged;
        public int ItemSlot
        {
            get { return _itemSlot; }
            set
            {
                if (_itemSlot == value) return;
                _itemSlot = value;
                OnItemSlotChanged();
            }
        }

        private void OnItemSlotChanged()
        {
            if (ItemSlotChanged != null)
                ItemSlotChanged.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Count Label

        private MirLabel CountLabel { get; set; }

        #endregion

        public MirItemCell()
        {
            Size = new Size(36, 32);
            GridType = MirGridType.None;
            DrawImage = false;

            BorderColour = Color.Lime;

            BackColour = Color.FromArgb(255, 255, 125, 125);
            Opacity = 0.5F;
            DrawControlTexture = true;
            Library = Libraries.Items;
        }

        public void SetEffect()
        {
            //put effect stuff here??
        }


        public override void OnMouseClick(MouseEventArgs e)
        {
            if (Locked || GameScene.Observing) return;

            if (GameScene.PickedUpGold || GridType == MirGridType.Inspect || GridType == MirGridType.QuestInventory) return;

            if (GameScene.SelectedCell == null && (GridType == MirGridType.Mail)) return;

            base.OnMouseClick(e);
            
            Redraw();

            switch (e.Button)
            {
                case MouseButtons.Right:
                    if (CMain.Ctrl)
                    {
                        if (Item != null)
                        {
                            OpenItem();
                        }
                        break;
                    }

                    if (CMain.Shift)
                    {
                        if (Item != null)
                        {
                            string text = string.Format("<{0}> ", Item.FriendlyName);

                            if (GameScene.Scene.ChatDialog.ChatTextBox.Text.Length + text.Length > Globals.MaxChatLength)
                            {
                                GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.UnableLinkItemMessageTooLong), ChatType.System);
                                return;
                            }

                            GameScene.Scene.ChatDialog.LinkedItems.Add(new ChatItem { UniqueID = Item.UniqueID, Title = Item.FriendlyName, Grid = GridType });
                            GameScene.Scene.ChatDialog.SetChatText(text);
                        }

                        break;
                    }

                    UseItem();
                    break;
                case MouseButtons.Left:
                    if (Item != null && GameScene.SelectedCell == null)
                        PlayItemSound();

                    if (CMain.Shift)
                    {
                        if (GridType == MirGridType.Inventory || GridType == MirGridType.Storage)
                        {
                            if (GameScene.SelectedCell == null && Item != null)
                            {
                                if (FreeSpace() == 0)
                                {
                                    GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.NoRoomToSplitStack), ChatType.System);
                                    return;
                                }

                                if (Item.Count > 1)
                                {
                                    MirAmountBox amountBox = new MirAmountBox(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.SplitAmount), Item.Image, (uint)(Item.Count - 1));

                                    amountBox.OKButton.Click += (o, a) =>
                                    {
                                        if (amountBox.Amount == 0 || amountBox.Amount >= Item.Count) return;
                                        Network.Enqueue(new C.SplitItem { Grid = GridType, UniqueID = Item.UniqueID, Count = (ushort)amountBox.Amount });
                                        Locked = true;
                                    };

                                    amountBox.Show();
                                }
                            }
                        }
                    }
                    
                    //Add support for ALT + click to sell quickly
                    else if (CMain.Alt && GameScene.Scene.NPCDropDialog.Visible && GridType == MirGridType.Inventory) // alt sell/repair
                    {
                        MoveItem(); // pickup item
                        GameScene.Scene.NPCDropDialog.ItemCell.OnMouseClick(e); // emulate click to drop control
                        GameScene.Scene.NPCDropDialog.ConfirmButton.OnMouseClick(e); //emulate OK to confirm trade
                    }
                    //Add support for ALT + click to sell quickly

                    else if ((GridType == MirGridType.HeroHPItem || GridType == MirGridType.HeroMPItem) && GameScene.SelectedCell == null && Item != null)
                        Network.Enqueue(new C.SetAutoPotItem { Grid = GridType, ItemIndex = 0 });

                    else MoveItem();
                    break;
            }
        }
        public override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (Locked) return;

            if (GameScene.PickedUpGold || GridType == MirGridType.Inspect || GridType == MirGridType.TrustMerchant || GridType == MirGridType.Craft) return;

            base.OnMouseClick(e);

            Redraw();

            GameScene.SelectedCell = null;
            UseItem();
        }


        private void BuyItem()
        {
            if (Item == null || Item.Price() * GameScene.NPCRate > GameScene.Gold) return;

            MirAmountBox amountBox;
            if (Item.Count > 1)
            {
                amountBox = new MirAmountBox(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.PurchaseAmount), Item.Image, Item.Count);

                amountBox.OKButton.Click += (o, e) =>
                {
                    Network.Enqueue(new C.BuyItemBack { UniqueID = Item.UniqueID, Count = (ushort)amountBox.Amount });
                    Locked = true;
                };
            }
            else
            {
                amountBox = new MirAmountBox(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.Purchase), Item.Image, GameLanguage.ClientTextMap.GetLocalization((ClientTextKeys.ValueGold), Item.Price()));

                amountBox.OKButton.Click += (o, e) =>
                {
                    Network.Enqueue(new C.BuyItemBack { UniqueID = Item.UniqueID, Count = 1 });
                    Locked = true;
                };
            }

            amountBox.Show();
        }

        public void OpenItem()
        {
            if ((GridType != MirGridType.Equipment && GridType != MirGridType.Inventory) || Item == null || GameScene.SelectedCell == this) return;

            GameScene.Scene.SocketDialog.Show(GridType, Item);
        }

        private bool HeroGridType => GridType == MirGridType.HeroInventory || GridType == MirGridType.HeroEquipment;

        public void UseItem()
        {
            if (Locked || GridType == MirGridType.Inspect || GridType == MirGridType.TrustMerchant || GridType == MirGridType.GuildStorage || GridType == MirGridType.Craft) return;

            if (!HeroGridType && MapObject.User.Fishing) return;
            if (MapObject.User.RidingMount && Item.Info.Type != ItemType.卷轴 && Item.Info.Type != ItemType.药水 && Item.Info.Type != ItemType.照明物) return;

            if (GridType == MirGridType.BuyBack)
            {
                BuyItem();
                return;
            }

            if (GridType == MirGridType.Equipment || GridType == MirGridType.Mount || GridType == MirGridType.Fishing || GridType == MirGridType.Socket)
            {
                RemoveItem();
                return;
            }

            if (GridType == MirGridType.HeroEquipment)
            {
                RemoveHeroItem();
                return;
            }

            if ((GridType != MirGridType.Inventory && GridType != MirGridType.Storage && GridType != MirGridType.HeroInventory) || Item == null || !CanUseItem() || GameScene.SelectedCell == this) return;

            CharacterDialog dialog = GameScene.Scene.CharacterDialog;
            UserObject actor = GameScene.User;
            if (HeroGridType)
            {
                dialog = GameScene.Scene.HeroDialog;
                actor = GameScene.Hero;

                if (Item.SoulBoundId != -1 && MapObject.Hero.Id != Item.SoulBoundId)
                    return;
            }
            else
            {
                if (Item.SoulBoundId != -1 && MapObject.User.Id != Item.SoulBoundId)
                    return;
            }
            

            switch (Item.Info.Type)
            {
                case ItemType.武器:
                    if (dialog.Grid[(int)EquipmentSlot.武器].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.武器 });
                        dialog.Grid[(int)EquipmentSlot.武器].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.盔甲:
                    if (dialog.Grid[(int)EquipmentSlot.盔甲].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.盔甲 });
                        dialog.Grid[(int)EquipmentSlot.盔甲].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.头盔:
                    if (dialog.Grid[(int)EquipmentSlot.头盔].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.头盔 });
                        dialog.Grid[(int)EquipmentSlot.头盔].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.项链:
                    if (dialog.Grid[(int)EquipmentSlot.项链].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.项链 });
                        dialog.Grid[(int)EquipmentSlot.项链].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.手镯:
                    if ((dialog.Grid[(int)EquipmentSlot.右手镯].Item == null || dialog.Grid[(int)EquipmentSlot.右手镯].Item.Info.Type == ItemType.护身符) && dialog.Grid[(int)EquipmentSlot.右手镯].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.右手镯 });
                        dialog.Grid[(int)EquipmentSlot.右手镯].Locked = true;
                        Locked = true;
                    }
                    else if (dialog.Grid[(int)EquipmentSlot.左手镯].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.左手镯 });
                        dialog.Grid[(int)EquipmentSlot.左手镯].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.戒指:
                    if (dialog.Grid[(int)EquipmentSlot.右戒指].Item == null && dialog.Grid[(int)EquipmentSlot.右戒指].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.右戒指 });
                        dialog.Grid[(int)EquipmentSlot.右戒指].Locked = true;
                        Locked = true;
                    }
                    else if (dialog.Grid[(int)EquipmentSlot.左戒指].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.左戒指 });
                        dialog.Grid[(int)EquipmentSlot.左戒指].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.护身符:
                    //if (Item.Info.Shape == 0) return;

                    if (dialog.Grid[(int)EquipmentSlot.护身符].Item != null && Item.Info.Type == ItemType.护身符)
                    {
                        if (dialog.Grid[(int)EquipmentSlot.护身符].Item.Info == Item.Info && dialog.Grid[(int)EquipmentSlot.护身符].Item.Count < dialog.Grid[(int)EquipmentSlot.护身符].Item.Info.StackSize)
                        {
                            Network.Enqueue(new C.MergeItem { GridFrom = GridType, GridTo = GridType == MirGridType.HeroInventory ? MirGridType.HeroEquipment : MirGridType.Equipment, IDFrom = Item.UniqueID, IDTo = dialog.Grid[(int)EquipmentSlot.护身符].Item.UniqueID });
                            //Network.Enqueue(new C.MergeItem { GridFrom = GridType, GridTo = MirGridType.Equipment, IDFrom = Item.UniqueID, IDTo = dialog.Grid[(int)EquipmentSlot.护身符].Item.UniqueID });

                            Locked = true;
                            return;
                        }
                    }

                    if (dialog.Grid[(int)EquipmentSlot.护身符].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.护身符 });
                        dialog.Grid[(int)EquipmentSlot.护身符].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.腰带:
                    if (dialog.Grid[(int)EquipmentSlot.腰带].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.腰带 });
                        dialog.Grid[(int)EquipmentSlot.腰带].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.靴子:
                    if (dialog.Grid[(int)EquipmentSlot.靴子].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.靴子 });
                        dialog.Grid[(int)EquipmentSlot.靴子].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.守护石:
                    if (dialog.Grid[(int)EquipmentSlot.守护石].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.守护石 });
                        dialog.Grid[(int)EquipmentSlot.守护石].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.照明物:
                    if (dialog.Grid[(int)EquipmentSlot.照明物].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.照明物 });
                        dialog.Grid[(int)EquipmentSlot.照明物].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.药水:
                case ItemType.卷轴:
                case ItemType.技能书:
                case ItemType.坐骑食物:
                case ItemType.灵物:
                case ItemType.外形物品:
                case ItemType.装饰:
                case ItemType.怪物蛋:
                case ItemType.特殊消耗品:
                case ItemType.封印:
                    if (CanUseItem() && (GridType == MirGridType.Inventory || GridType == MirGridType.HeroInventory))
                    {
                        if (CMain.Time < GameScene.UseItemTime) return;
                        if (Item.Info.Type == ItemType.药水 && Item.Info.Shape == 4)
                        {
                            MirMessageBox messageBox = new MirMessageBox(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.AreYouWantUsePotion), MirMessageBoxButtons.YesNo);
                            messageBox.YesButton.Click += (o, e) =>
                            {
                                Network.Enqueue(new C.UseItem { UniqueID = Item.UniqueID, Grid = GridType });

                                if (Item.Count == 1 && ItemSlot < GameScene.User.BeltIdx)
                                {
                                    for (int i = GameScene.User.BeltIdx; i < GameScene.User.Inventory.Length; i++)
                                        if (ItemArray[i] != null && ItemArray[i].Info == Item.Info)
                                        {
                                            Network.Enqueue(new C.MoveItem { Grid = MirGridType.Inventory, From = i, To = ItemSlot });
                                            GameScene.Scene.InventoryDialog.Grid[i - GameScene.User.BeltIdx].Locked = true;
                                            break;
                                        }
                                }

                                GameScene.UseItemTime = CMain.Time + 100;
                                PlayItemSound();
                            };

                            messageBox.Show();
                            return;
                        }

                        Network.Enqueue(new C.UseItem { UniqueID = Item.UniqueID, Grid = GridType });

                        if (HeroGridType)
                        {
                            if (Item.Count == 1 && ItemSlot < GameScene.User.HeroBeltIdx)
                            {
                                for (int i = GameScene.User.HeroBeltIdx; i < GameScene.Hero.Inventory.Length; i++)
                                    if (ItemArray[i] != null && ItemArray[i].Info == Item.Info)
                                    {
                                        Network.Enqueue(new C.MoveItem { Grid = MirGridType.HeroInventory, From = i, To = ItemSlot });
                                        GameScene.Scene.HeroInventoryDialog.Grid[i - GameScene.User.HeroBeltIdx].Locked = true;
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            if (Item.Count == 1 && ItemSlot < GameScene.User.BeltIdx)
                            {
                                for (int i = GameScene.User.BeltIdx; i < GameScene.User.Inventory.Length; i++)
                                    if (ItemArray[i] != null && ItemArray[i].Info == Item.Info)
                                    {
                                        Network.Enqueue(new C.MoveItem { Grid = MirGridType.Inventory, From = i, To = ItemSlot });
                                        GameScene.Scene.InventoryDialog.Grid[i - GameScene.User.BeltIdx].Locked = true;
                                        break;
                                    }
                            }
                        }                        

                        Locked = true;
                    }
                    break;
                case ItemType.坐骑:
                    if (dialog.Grid[(int)EquipmentSlot.坐骑].CanWearItem(actor, Item))
                    {
                        Network.Enqueue(new C.EquipItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)EquipmentSlot.坐骑 });
                        dialog.Grid[(int)EquipmentSlot.坐骑].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.缰绳:
                case ItemType.铃铛:
                case ItemType.蝴蝶结:
                case ItemType.马鞍:
                case ItemType.面甲:
                case ItemType.鱼钩:
                case ItemType.鱼漂:
                case ItemType.鱼饵:
                case ItemType.探鱼器:
                case ItemType.摇轮:
                case ItemType.镶嵌宝石:
                    UseSlotItem();
                    break;
            }

            GameScene.UseItemTime = CMain.Time + 300;
            PlayItemSound();
        }
        public void UseSlotItem()
        {
            MountDialog mountDialog;
            FishingDialog fishingDialog;

            if (!CanUseItem()) return;

            switch (Item.Info.Type)
            {
                case ItemType.镶嵌宝石:
                    if (GameScene.SelectedItem != null && !GameScene.SelectedItem.Info.IsFishingRod && GameScene.SelectedItem.Info.Type != ItemType.坐骑)
                    {
                        switch (Item.Info.Shape)
                        {
                            case 1:
                                if (GameScene.SelectedItem.Info.Type != ItemType.武器) return;
                                break;
                            case 2:
                                if (GameScene.SelectedItem.Info.Type != ItemType.盔甲) return;
                                break;
                            case 3:
                                if (GameScene.SelectedItem.Info.Type != ItemType.戒指 && GameScene.SelectedItem.Info.Type != ItemType.手镯 && GameScene.SelectedItem.Info.Type != ItemType.项链) return;
                                break;
                        }

                        MirItemCell cell = null;
                        for (int i = 0; i < GameScene.Scene.SocketDialog.Grid.Length; i++)
                        {
                            if (!GameScene.Scene.SocketDialog.Grid[i].Visible || GameScene.Scene.SocketDialog.Grid[i].Item != null) continue;
                            cell = GameScene.Scene.SocketDialog.Grid[i];
                            break;
                        }

                        if (cell != null && cell.CanWearItem(GameScene.User, Item))
                        {
                            Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = cell.ItemSlot, GridTo = MirGridType.Socket, ToUniqueID = GameScene.SelectedItem.UniqueID });
                            cell.Locked = true;
                            Locked = true;
                        }
                    }
                    break;
                case ItemType.缰绳:
                    mountDialog = GameScene.Scene.MountDialog;
                    if (mountDialog.Grid[(int)MountSlot.Reins].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.坐骑];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)MountSlot.Reins, GridTo = MirGridType.Mount, ToUniqueID = toItem.UniqueID });
                        mountDialog.Grid[(int)MountSlot.Reins].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.铃铛:
                    mountDialog = GameScene.Scene.MountDialog;
                    if (mountDialog.Grid[(int)MountSlot.Bells].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.坐骑];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)MountSlot.Bells, GridTo = MirGridType.Mount, ToUniqueID = toItem.UniqueID });
                        mountDialog.Grid[(int)MountSlot.Bells].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.蝴蝶结:
                    mountDialog = GameScene.Scene.MountDialog;
                    if (mountDialog.Grid[(int)MountSlot.Ribbon].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.坐骑];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)MountSlot.Ribbon, GridTo = MirGridType.Mount, ToUniqueID = toItem.UniqueID });
                        mountDialog.Grid[(int)MountSlot.Ribbon].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.马鞍:
                    mountDialog = GameScene.Scene.MountDialog;
                    if (mountDialog.Grid[(int)MountSlot.Saddle].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.坐骑];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)MountSlot.Saddle, GridTo = MirGridType.Mount, ToUniqueID = toItem.UniqueID });
                        mountDialog.Grid[(int)MountSlot.Saddle].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.面甲:
                    mountDialog = GameScene.Scene.MountDialog;
                    if (mountDialog.Grid[(int)MountSlot.Mask].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.坐骑];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)MountSlot.Mask, GridTo = MirGridType.Mount, ToUniqueID = toItem.UniqueID });
                        mountDialog.Grid[(int)MountSlot.Mask].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.鱼钩:
                    fishingDialog = GameScene.Scene.FishingDialog;
                    if (fishingDialog.Grid[(int)FishingSlot.Hook].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.武器];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)FishingSlot.Hook, GridTo = MirGridType.Fishing, ToUniqueID = toItem.UniqueID });
                        fishingDialog.Grid[(int)FishingSlot.Hook].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.鱼漂:
                    fishingDialog = GameScene.Scene.FishingDialog;
                    if (fishingDialog.Grid[(int)FishingSlot.Float].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.武器];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)FishingSlot.Float, GridTo = MirGridType.Fishing, ToUniqueID = toItem.UniqueID });
                        fishingDialog.Grid[(int)FishingSlot.Float].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.鱼饵:
                    fishingDialog = GameScene.Scene.FishingDialog;

                    if (fishingDialog.Grid[(int)FishingSlot.Bait].Item != null && Item.Info.Type == ItemType.鱼饵)
                    {
                        if (fishingDialog.Grid[(int)FishingSlot.Bait].Item.Info == Item.Info && fishingDialog.Grid[(int)FishingSlot.Bait].Item.Count < fishingDialog.Grid[(int)FishingSlot.Bait].Item.Info.StackSize)
                        {
                            Network.Enqueue(new C.MergeItem { GridFrom = GridType, GridTo = MirGridType.Fishing, IDFrom = Item.UniqueID, IDTo = fishingDialog.Grid[(int)FishingSlot.Bait].Item.UniqueID });

                            Locked = true;
                            GameScene.SelectedCell.Locked = true;
                            GameScene.SelectedCell = null;
                            return;
                        }
                    }

                    if (fishingDialog.Grid[(int)FishingSlot.Bait].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.武器];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)FishingSlot.Bait, GridTo = MirGridType.Fishing, ToUniqueID = toItem.UniqueID });
                        fishingDialog.Grid[(int)FishingSlot.Bait].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.探鱼器:
                    fishingDialog = GameScene.Scene.FishingDialog;
                    if (fishingDialog.Grid[(int)FishingSlot.Finder].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.武器];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)FishingSlot.Finder, GridTo = MirGridType.Fishing, ToUniqueID = toItem.UniqueID });
                        fishingDialog.Grid[(int)FishingSlot.Finder].Locked = true;
                        Locked = true;
                    }
                    break;
                case ItemType.摇轮:
                    fishingDialog = GameScene.Scene.FishingDialog;
                    if (fishingDialog.Grid[(int)FishingSlot.Reel].CanWearItem(GameScene.User, Item))
                    {
                        var toItem = MapObject.User.Equipment[(byte)EquipmentSlot.武器];
                        Network.Enqueue(new C.EquipSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = (int)FishingSlot.Reel, GridTo = MirGridType.Fishing, ToUniqueID = toItem.UniqueID });
                        fishingDialog.Grid[(int)FishingSlot.Reel].Locked = true;
                        Locked = true;
                    }
                    break;
            }
        }

        public void RemoveItem()
        {
            int count = 0;

            for (int i = 0; i < GameScene.User.Inventory.Length; i++)
            {
                MirItemCell itemCell = i < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[i] : GameScene.Scene.InventoryDialog.Grid[i - GameScene.User.BeltIdx];

                if (itemCell.Item == null) count++;
            }

            if (Item == null || count < 1 || (MapObject.User.RidingMount && Item.Info.Type != ItemType.照明物)) return;

            if (Item.Info.StackSize > 1)
            {
                UserItem item = null;

                for (int i = 0; i < GameScene.User.Inventory.Length; i++)
                {
                    MirItemCell itemCell = i < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[i] : GameScene.Scene.InventoryDialog.Grid[i - GameScene.User.BeltIdx];

                    if (itemCell.Item == null || itemCell.Item.Info != Item.Info) continue;

                    item = itemCell.Item;
                }

                if (item != null && ((item.Count + Item.Count) <= item.Info.StackSize))
                {
                    //Merge.
                    Network.Enqueue(new C.MergeItem { GridFrom = GridType, GridTo = MirGridType.Inventory, IDFrom = Item.UniqueID, IDTo = item.UniqueID });

                    Locked = true;

                    PlayItemSound();
                    return;
                }
            }

            for (int i = 0; i < GameScene.User.Inventory.Length; i++)
            {
                MirItemCell itemCell;

                if (Item.Info.Type == ItemType.护身符)
                {
                    itemCell = i < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[i] : GameScene.Scene.InventoryDialog.Grid[i - GameScene.User.BeltIdx];
                }
                else
                {
                    itemCell = i < (GameScene.User.Inventory.Length - GameScene.User.BeltIdx) ? GameScene.Scene.InventoryDialog.Grid[i] : GameScene.Scene.BeltDialog.Grid[i - GameScene.User.Inventory.Length];
                }

                if (itemCell.Item != null) continue;

                if (GridType != MirGridType.Equipment)
                {
                    ulong fromID;

                    if (GridType == MirGridType.Fishing)
                    {
                        if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.武器].Item == null) return;

                        fromID = GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.武器].Item.UniqueID;
                    }
                    else if (GridType == MirGridType.Mount)
                    {
                        if (GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.坐骑].Item == null) return;

                        fromID = GameScene.Scene.CharacterDialog.Grid[(byte)EquipmentSlot.坐骑].Item.UniqueID;
                    }
                    else
                    {
                        if (GameScene.SelectedItem == null) return;

                        fromID = GameScene.SelectedItem.UniqueID;
                    }

                    Network.Enqueue(new C.RemoveSlotItem { Grid = GridType, UniqueID = Item.UniqueID, To = itemCell.ItemSlot, GridTo = MirGridType.Inventory, FromUniqueID = fromID });
                }
                else
                {
                    Network.Enqueue(new C.RemoveItem { Grid = MirGridType.Inventory, UniqueID = Item.UniqueID, To = itemCell.ItemSlot });
                }

                Locked = true;

                PlayItemSound();
                break;
            }
        }

        public void RemoveHeroItem()
        {
            int count = 0;
            if (GameScene.Hero == null || GameScene.Hero.Dead) return;

            for (int i = 0; i < GameScene.Hero.Inventory.Length; i++)
            {
                MirItemCell itemCell = i < GameScene.User.HeroBeltIdx ? GameScene.Scene.HeroBeltDialog.Grid[i] : GameScene.Scene.HeroInventoryDialog.Grid[i - GameScene.User.HeroBeltIdx];

                if (itemCell.Item == null) count++;
            }

            if (Item == null || count < 1 || (MapObject.Hero.RidingMount && Item.Info.Type != ItemType.照明物)) return;

            if (Item.Info.StackSize > 1)
            {
                UserItem item = null;

                for (int i = 0; i < GameScene.Hero.Inventory.Length; i++)
                {
                    MirItemCell itemCell = i < GameScene.User.HeroBeltIdx ? GameScene.Scene.HeroBeltDialog.Grid[i] : GameScene.Scene.HeroInventoryDialog.Grid[i - GameScene.User.HeroBeltIdx];

                    if (itemCell.Item == null || itemCell.Item.Info != Item.Info) continue;

                    item = itemCell.Item;
                }

                if (item != null && ((item.Count + Item.Count) <= item.Info.StackSize))
                {
                    //Merge.
                    Network.Enqueue(new C.MergeItem { GridFrom = GridType, GridTo = MirGridType.Inventory, IDFrom = Item.UniqueID, IDTo = item.UniqueID });

                    Locked = true;

                    PlayItemSound();
                    return;
                }
            }

            for (int i = 0; i < GameScene.Hero.Inventory.Length; i++)
            {
                MirItemCell itemCell;

                if (Item.Info.Type == ItemType.护身符)
                {
                    itemCell = i < GameScene.User.HeroBeltIdx ? GameScene.Scene.HeroBeltDialog.Grid[i] : GameScene.Scene.HeroInventoryDialog.Grid[i - GameScene.User.HeroBeltIdx];
                }
                else
                {
                    itemCell = i < (GameScene.Hero.Inventory.Length - GameScene.User.HeroBeltIdx) ? GameScene.Scene.HeroInventoryDialog.Grid[i] : GameScene.Scene.HeroBeltDialog.Grid[i - GameScene.Hero.Inventory.Length];
                }

                if (itemCell.Item != null) continue;

                Network.Enqueue(new C.RemoveItem { Grid = MirGridType.HeroInventory, UniqueID = Item.UniqueID, To = itemCell.ItemSlot });

                Locked = true;

                PlayItemSound();
                break;
            }
        }

        private void MoveItem()
        {
            if (GridType == MirGridType.BuyBack || GridType == MirGridType.DropPanel || GridType == MirGridType.Inspect || GridType == MirGridType.TrustMerchant || GridType == MirGridType.Craft) return;

            if (GameScene.SelectedCell != null)
            {
                if (GameScene.SelectedCell.Item == null || GameScene.SelectedCell == this)
                {
                    GameScene.SelectedCell = null;
                    return;
                }

                switch (GridType)
                {
                    #region To Inventory
                    case MirGridType.Inventory: // To Inventory
                        switch (GameScene.SelectedCell.GridType)
                        {
                            #region From Inventory
                            case MirGridType.Inventory: //From Invenotry
                                if (Item != null)
                                {
                                    if (CMain.Ctrl)
                                    {
                                        MirMessageBox messageBox = new MirMessageBox(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouWantCombineItems), MirMessageBoxButtons.YesNo);
                                        messageBox.YesButton.Click += (o, e) =>
                                        {
                                            //Combine
                                            Network.Enqueue(new C.CombineItem { Grid = GameScene.SelectedCell.GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });
                                            Locked = true;
                                            GameScene.SelectedCell.Locked = true;
                                            GameScene.SelectedCell = null;
                                        };

                                        messageBox.Show();
                                        return;
                                    }

                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        //Merge
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                Network.Enqueue(new C.MoveItem { Grid = GridType, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                            #endregion
                            #region From Equipment
                            case MirGridType.Equipment: //From Equipment
                                if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (!CanRemoveItem(GameScene.SelectedCell.Item))
                                {
                                    GameScene.SelectedCell = null;
                                    return;
                                }
                                if (Item == null)
                                {
                                    Network.Enqueue(new C.RemoveItem { Grid = GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 6; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.RemoveItem { Grid = GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = x });

                                        MirItemCell temp = x < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[x] : GameScene.Scene.InventoryDialog.Grid[x - GameScene.User.BeltIdx];

                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                            #endregion
                            #region From Storage
                            case MirGridType.Storage: //From Storage
                                if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }


                                if (Item == null)
                                {
                                    Network.Enqueue(new C.TakeBackItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 6; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.TakeBackItem { From = GameScene.SelectedCell.ItemSlot, To = x });

                                        MirItemCell temp = x < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[x] : GameScene.Scene.InventoryDialog.Grid[x - GameScene.User.BeltIdx];

                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                            #endregion
                            #region From Guild Storage
                            case MirGridType.GuildStorage:
                                if (Item != null)
                                {
                                    GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouCannotSwapItems), ChatType.System);
                                    return;
                                }
                                if (!GuildDialog.MyOptions.HasFlag(GuildRankOptions.CanRetrieveItem))
                                {
                                    GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.InsufficientRightsRetrieveItems), ChatType.System);
                                    return;
                                }
                                Network.Enqueue(new C.GuildStorageItemChange { Type = 1, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                break;
                            #endregion
                            #region From Trade
                            case MirGridType.Trade: //From Trade
                                if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }


                                if (Item == null)
                                {
                                    Network.Enqueue(new C.RetrieveTradeItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 6; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.RetrieveTradeItem { From = GameScene.SelectedCell.ItemSlot, To = x });

                                        MirItemCell temp = x < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[x] : GameScene.Scene.InventoryDialog.Grid[x - GameScene.User.BeltIdx];

                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                            #endregion
                            #region From AwakenItem
                            case MirGridType.AwakenItem: //From AwakenItem
                                Network.Enqueue(new C.MoveItem { Grid = GridType, From = NPCAwakeDialog.ItemsIdx[GameScene.SelectedCell.ItemSlot], To = NPCAwakeDialog.ItemsIdx[GameScene.SelectedCell.ItemSlot] });
                                GameScene.SelectedCell.Locked = false;
                                GameScene.SelectedCell.Item = null;
                                NPCAwakeDialog.ItemsIdx[GameScene.SelectedCell.ItemSlot] = 0;

                                if (GameScene.SelectedCell.ItemSlot == 0)
                                    GameScene.Scene.NPCAwakeDialog.ItemCell_Click();
                                GameScene.SelectedCell = null;
                                break;
                            #endregion
                            #region From Refine
                            case MirGridType.Refine: //From AwakenItem
                                if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }


                                if (Item == null)
                                {
                                    Network.Enqueue(new C.RetrieveRefineItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 6; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.RetrieveRefineItem { From = GameScene.SelectedCell.ItemSlot, To = x });

                                        MirItemCell temp = x < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[x] : GameScene.Scene.InventoryDialog.Grid[x - GameScene.User.BeltIdx];

                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                            #endregion
                            #region From Item Renting Dialog

                            case MirGridType.Renting:
                                if (GameScene.User.RentalItemLocked)
                                {
                                    GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.UnableRemoveLockedItem), ChatType.System);
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                if (Item == null)
                                {
                                    Network.Enqueue(new C.RetrieveRentalItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                break;
                            #endregion
                            #region From Hero Inventory
                            case MirGridType.HeroInventory:
                                if (GameScene.Hero == null || GameScene.Hero.Dead)
                                    return;
                        if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }


                                if (Item == null)
                                {
                                    Network.Enqueue(new C.TakeBackHeroItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 6; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.TakeBackHeroItem { From = GameScene.SelectedCell.ItemSlot, To = x });

                                        MirItemCell temp = x < GameScene.User.BeltIdx ? GameScene.Scene.BeltDialog.Grid[x] : GameScene.Scene.InventoryDialog.Grid[x - GameScene.User.BeltIdx];

                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                                #endregion
                        }
                        break;
                    #endregion
                    #region To Equipment
                    case MirGridType.Equipment: //To Equipment

                        if (GameScene.SelectedCell.GridType != MirGridType.Inventory && GameScene.SelectedCell.GridType != MirGridType.Storage) return;


                        if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                        {
                            if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                            {
                                Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                            }
                        }

                        if (CorrectSlot(GameScene.SelectedCell.Item, GameScene.SelectedCell.GridType))
                        {
                            if (CanWearItem(GameScene.User, GameScene.SelectedCell.Item))
                            {
                                Network.Enqueue(new C.EquipItem { Grid = GameScene.SelectedCell.GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = ItemSlot });
                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                            }
                            GameScene.SelectedCell = null;
                        }
                        return;
                    #endregion
                    #region To Storage
                    case MirGridType.Storage: //To Storage
                        switch (GameScene.SelectedCell.GridType)
                        {
                            #region From Inventory
                            case MirGridType.Inventory: //From Invenotry
                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }


                                if (ItemArray[ItemSlot] == null)
                                {
                                    Network.Enqueue(new C.StoreItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 0; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.StoreItem { From = GameScene.SelectedCell.ItemSlot, To = x });

                                        MirItemCell temp = GameScene.Scene.StorageDialog.Grid[x];
                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                            #endregion
                            #region From Equipment
                            case MirGridType.Equipment: //From Equipment
                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        //Merge.
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (!CanRemoveItem(GameScene.SelectedCell.Item))
                                {
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                if (Item == null)
                                {
                                    Network.Enqueue(new C.RemoveItem { Grid = GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 0; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.RemoveItem { Grid = GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = x });

                                        MirItemCell temp = GameScene.Scene.StorageDialog.Grid[x];
                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                            #endregion
                            #region From Storage
                            case MirGridType.Storage:
                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        //Merge.
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                Network.Enqueue(new C.MoveItem { Grid = GridType, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                                #endregion

                        }
                        break;

                    #endregion
                    #region To guild storage
                    case MirGridType.GuildStorage: //To Guild Storage
                        switch (GameScene.SelectedCell.GridType)
                        {
                            case MirGridType.GuildStorage: //From Guild Storage
                                if (GameScene.SelectedCell.GridType == MirGridType.GuildStorage)
                                {
                                    if (!GuildDialog.MyOptions.HasFlag(GuildRankOptions.CanStoreItem))
                                    {
                                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.InsufficientRightsStoreItems), ChatType.System);
                                        return;
                                    }

                                    //if (ItemArray[ItemSlot] == null)
                                    //{
                                    Network.Enqueue(new C.GuildStorageItemChange { Type = 2, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                    //}
                                }
                                return;

                            case MirGridType.Inventory:

                                if (GameScene.SelectedCell.GridType == MirGridType.Inventory)
                                {
                                    if (Item != null)
                                    {
                                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouCannotSwapItems), ChatType.System);
                                        return;
                                    }
                                    if (!GuildDialog.MyOptions.HasFlag(GuildRankOptions.CanStoreItem))
                                    {
                                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.InsufficientRightsStoreItems), ChatType.System);
                                        return;
                                    }
                                    if (ItemArray[ItemSlot] == null)
                                    {
                                        Network.Enqueue(new C.GuildStorageItemChange { Type = 0, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }
                                return;
                        }
                        break;
                    #endregion
                    #region To Trade

                    case MirGridType.Trade:
                        if (Item != null && Item.Info.Bind.HasFlag(BindMode.DontTrade)) return;

                        switch (GameScene.SelectedCell.GridType)
                        {
                            #region From Trade
                            case MirGridType.Trade: //From Trade
                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        //Merge.
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                Network.Enqueue(new C.MoveItem { Grid = GridType, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                            #endregion

                            #region From Inventory
                            case MirGridType.Inventory: //From Inventory
                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }


                                if (ItemArray[ItemSlot] == null)
                                {
                                    Network.Enqueue(new C.DepositTradeItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 0; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.DepositTradeItem { From = GameScene.SelectedCell.ItemSlot, To = x });

                                        MirItemCell temp = GameScene.Scene.TradeDialog.Grid[x];
                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                                #endregion
                        }
                        break;

                    #endregion
                    #region To Refine 

                    case MirGridType.Refine:

                        switch (GameScene.SelectedCell.GridType)
                        {
                            #region From Refine
                            case MirGridType.Refine: //From Refine
                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        //Merge.
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                Network.Enqueue(new C.MoveItem { Grid = GridType, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                            #endregion

                            #region From Inventory
                            case MirGridType.Inventory: //From Inventory
                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                Network.Enqueue(new C.DepositRefineItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                                #endregion
                        }
                        break;

                    #endregion
                    #region To Item Renting Dialog

                    case MirGridType.Renting:
                        switch (GameScene.SelectedCell.GridType)
                        {
                            case MirGridType.Inventory:

                                if (Item == null)
                                {
                                    Network.Enqueue(new C.DepositRentalItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });
                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                break;
                        }

                        break;

                    #endregion
                    #region To Awakening
                    case MirGridType.AwakenItem:
                        {
                            int errorCode = 0;

                            if (GameScene.SelectedCell.GridType != MirGridType.Inventory && _itemSlot < 1) return;

                            switch (_itemSlot)
                            {
                                //baseitem
                                case 0:
                                    {
                                        if ((GameScene.SelectedCell.Item.Info.Type == ItemType.武器 ||
                                            GameScene.SelectedCell.Item.Info.Type == ItemType.头盔 ||
                                            GameScene.SelectedCell.Item.Info.Type == ItemType.盔甲) &&
                                            GameScene.SelectedCell.Item.Info.Grade != ItemGrade.None &&
                                            _itemSlot == 0)
                                        {
                                            if (Item == null)
                                            {
                                                Item = GameScene.SelectedCell.Item;
                                                GameScene.SelectedCell.Locked = true;
                                                NPCAwakeDialog.ItemsIdx[_itemSlot] = GameScene.SelectedCell._itemSlot;
                                            }
                                            else
                                            {
                                                Network.Enqueue(new C.AwakeningLockedItem { UniqueID = Item.UniqueID, Locked = false });

                                                Item = GameScene.SelectedCell.Item;
                                                GameScene.SelectedCell.Locked = true;
                                                NPCAwakeDialog.ItemsIdx[_itemSlot] = GameScene.SelectedCell._itemSlot;
                                            }
                                            GameScene.Scene.NPCAwakeDialog.ItemCell_Click();
                                            GameScene.Scene.NPCAwakeDialog.OnAwakeTypeSelect(0);
                                        }
                                        else
                                        {
                                            errorCode = -2;
                                        }
                                    }
                                    break;
                                //view materials
                                case 1:
                                case 2:
                                    break;
                                //materials
                                case 3:
                                case 4:
                                    {
                                        switch (GameScene.SelectedCell.GridType)
                                        {
                                            case MirGridType.Inventory:
                                                {
                                                    if (GameScene.SelectedCell.Item.Info.Type == ItemType.觉醒物品 &&
                                                        GameScene.SelectedCell.Item.Info.Shape < 200 && NPCAwakeDialog.ItemsIdx[_itemSlot] == 0)
                                                    {
                                                        Item = GameScene.SelectedCell.Item;
                                                        GameScene.SelectedCell.Locked = true;
                                                        NPCAwakeDialog.ItemsIdx[_itemSlot] = GameScene.SelectedCell._itemSlot;
                                                    }
                                                    else
                                                    {
                                                        errorCode = -2;
                                                    }
                                                }
                                                break;
                                            case MirGridType.AwakenItem:
                                                {
                                                    if (GameScene.SelectedCell.ItemSlot == ItemSlot || GameScene.SelectedCell.ItemSlot == 0)
                                                    {
                                                        Locked = false;
                                                        GameScene.SelectedCell = null;
                                                    }
                                                    else
                                                    {
                                                        GameScene.SelectedCell.Locked = false;
                                                        Locked = false;

                                                        int beforeIdx = NPCAwakeDialog.ItemsIdx[GameScene.SelectedCell._itemSlot];
                                                        NPCAwakeDialog.ItemsIdx[GameScene.SelectedCell._itemSlot] = NPCAwakeDialog.ItemsIdx[_itemSlot];
                                                        NPCAwakeDialog.ItemsIdx[_itemSlot] = beforeIdx;

                                                        UserItem item = GameScene.SelectedCell.Item;
                                                        GameScene.SelectedCell.Item = Item;
                                                        Item = item;
                                                        GameScene.SelectedCell = null;
                                                    }
                                                }
                                                break;
                                        }

                                    }
                                    break;
                                //SuccessRateUpItem or RandomValueUpItem or CancelDestroyedItem etc.
                                //AllCashItem Korea Server Not Implementation.
                                case 5:
                                case 6:
                                    if (GameScene.SelectedCell.Item.Info.Type == ItemType.觉醒物品 &&
                                            GameScene.SelectedCell.Item.Info.Shape == 200)
                                    {
                                        Item = GameScene.SelectedCell.Item;
                                        GameScene.SelectedCell.Locked = true;
                                        NPCAwakeDialog.ItemsIdx[_itemSlot] = GameScene.SelectedCell._itemSlot;
                                    }
                                    else
                                    {
                                        errorCode = -2;
                                    }
                                    break;
                                default:
                                    break;
                            }

                            GameScene.SelectedCell = null;

                            switch (errorCode)
                            {
                                //case -1:
                                //    messageBox = new MirMessageBox("Item must be in your inventory.", MirMessageBoxButtons.OK);
                                //    messageBox.Show();
                                //    break;
                                case -2:
                                    //messageBox = new MirMessageBox("Cannot awaken this item.", MirMessageBoxButtons.OK);
                                    //messageBox.Show();
                                    break;
                            }
                        }
                        return;
                    #endregion
                    #region To Mail
                    case MirGridType.Mail: //To Mail
                        if (GameScene.SelectedCell.GridType == MirGridType.Inventory)
                        {
                            if (Item != null)
                            {
                                GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouCannotSwapItems), ChatType.System);
                                return;
                            }

                            if (GameScene.SelectedCell.Item.Info.Bind.HasFlag(BindMode.DontTrade))
                            {
                                GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouCannotMailItem), ChatType.System);
                                return;
                            }

                            if (ItemArray[ItemSlot] == null)
                            {
                                Item = GameScene.SelectedCell.Item;
                                GameScene.SelectedCell.Locked = true;
                                MailComposeParcelDialog.ItemsIdx[_itemSlot] = GameScene.SelectedCell.Item.UniqueID;
                                GameScene.SelectedCell = null;
                                GameScene.Scene.MailComposeParcelDialog.CalculatePostage();

                                return;
                            }
                        }
                        break;
                    #endregion
                    #region To Hero Inventory
                    case MirGridType.HeroInventory:
                        if (GameScene.Hero == null || GameScene.Hero.Dead)
                            return;
                        switch (GameScene.SelectedCell.GridType)
                        {
                            #region From Hero Inventory
                            case MirGridType.HeroInventory:
                                if (Item != null)
                                {
                                    if (CMain.Ctrl)
                                    {
                                        MirMessageBox messageBox = new MirMessageBox(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouWantCombineItems), MirMessageBoxButtons.YesNo);
                                        messageBox.YesButton.Click += (o, e) =>
                                        {
                                            //Combine
                                            Network.Enqueue(new C.CombineItem { Grid = GameScene.SelectedCell.GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });
                                            Locked = true;
                                            GameScene.SelectedCell.Locked = true;
                                            GameScene.SelectedCell = null;
                                        };

                                        messageBox.Show();
                                        return;
                                    }

                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        //Merge
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                Network.Enqueue(new C.MoveItem { Grid = GridType, From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                            #endregion
                            #region From Hero Equipment
                            case MirGridType.HeroEquipment:
                                if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (!CanRemoveItem(GameScene.SelectedCell.Item))
                                {
                                    GameScene.SelectedCell = null;
                                    return;
                                }
                                if (Item == null)
                                {
                                    Network.Enqueue(new C.RemoveItem { Grid = GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 2; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.RemoveItem { Grid = GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = x });

                                        MirItemCell temp = x < GameScene.User.HeroBeltIdx ? GameScene.Scene.HeroBeltDialog.Grid[x] : GameScene.Scene.HeroInventoryDialog.Grid[x - GameScene.User.HeroBeltIdx];

                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                            #endregion
                            #region From Inventory
                            case MirGridType.Inventory:
                                if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }

                                if (GameScene.SelectedCell.Item.Weight + MapObject.Hero.CurrentBagWeight > MapObject.Hero.Stats[Stat.背包负重])
                                {
                                    GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.TooHeavyToTransfer), ChatType.System);
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                if (Item != null)
                                {
                                    if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                                    {
                                        Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                        Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                }


                                if (Item == null)
                                {
                                    Network.Enqueue(new C.TransferHeroItem { From = GameScene.SelectedCell.ItemSlot, To = ItemSlot });

                                    Locked = true;
                                    GameScene.SelectedCell.Locked = true;
                                    GameScene.SelectedCell = null;
                                    return;
                                }

                                for (int x = 2; x < ItemArray.Length; x++)
                                    if (ItemArray[x] == null)
                                    {
                                        Network.Enqueue(new C.TransferHeroItem { From = GameScene.SelectedCell.ItemSlot, To = x });

                                        MirItemCell temp = x < GameScene.User.HeroBeltIdx ? GameScene.Scene.HeroBeltDialog.Grid[x] : GameScene.Scene.HeroInventoryDialog.Grid[x - GameScene.User.HeroBeltIdx];

                                        if (temp != null) temp.Locked = true;
                                        GameScene.SelectedCell.Locked = true;
                                        GameScene.SelectedCell = null;
                                        return;
                                    }
                                break;
                                #endregion
                        }
                        break;
                    #endregion
                    #region To Hero Equipment
                    case MirGridType.HeroEquipment:

                        if (GameScene.SelectedCell.GridType != MirGridType.HeroInventory) return;
                        if (GameScene.Hero == null || GameScene.Hero.Dead)
                            return;

                        if (Item != null && GameScene.SelectedCell.Item.Info.Type == ItemType.护身符)
                        {
                            if (GameScene.SelectedCell.Item.Info == Item.Info && Item.Count < Item.Info.StackSize)
                            {
                                Network.Enqueue(new C.MergeItem { GridFrom = GameScene.SelectedCell.GridType, GridTo = GridType, IDFrom = GameScene.SelectedCell.Item.UniqueID, IDTo = Item.UniqueID });

                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                                GameScene.SelectedCell = null;
                                return;
                            }
                        }

                        if (CorrectSlot(GameScene.SelectedCell.Item, GameScene.SelectedCell.GridType))
                        {
                            if (CanWearItem(GameScene.Hero, GameScene.SelectedCell.Item))
                            {
                                Network.Enqueue(new C.EquipItem { Grid = GameScene.SelectedCell.GridType, UniqueID = GameScene.SelectedCell.Item.UniqueID, To = ItemSlot });
                                Locked = true;
                                GameScene.SelectedCell.Locked = true;
                            }
                            GameScene.SelectedCell = null;
                        }
                        return;
                    #endregion
                    #region To Hero AutoPot
                    case MirGridType.HeroHPItem:
                    case MirGridType.HeroMPItem:
                        if (GameScene.SelectedCell.GridType != MirGridType.HeroInventory) return;
                        if (GameScene.Hero == null || GameScene.Hero.Dead)
                            return;
                        if (GameScene.SelectedCell.Item.Info.Type != ItemType.药水 || GameScene.SelectedCell.Item.Info.Shape > 1)
                            return;

                        Network.Enqueue(new C.SetAutoPotItem { Grid = GridType, ItemIndex = GameScene.SelectedCell.Item.Info.Index });
                        GameScene.SelectedCell = null;

                        return;
                        #endregion
                }

                return;
            }

            if (Item != null)
            {
                GameScene.SelectedCell = this;
            }
        }
        private void PlayItemSound()
        {
            if (Item == null) return;

            switch (Item.Info.Type)
            {
                case ItemType.武器:
                    SoundManager.PlaySound(SoundList.ClickWeapon);
                    break;
                case ItemType.盔甲:
                    SoundManager.PlaySound(SoundList.ClickArmour);
                    break;
                case ItemType.头盔:
                    SoundManager.PlaySound(SoundList.ClickHelmet);
                    break;
                case ItemType.项链:
                    SoundManager.PlaySound(SoundList.ClickNecklace);
                    break;
                case ItemType.手镯:
                    SoundManager.PlaySound(SoundList.ClickBracelet);
                    break;
                case ItemType.戒指:
                    SoundManager.PlaySound(SoundList.ClickRing);
                    break;
                case ItemType.靴子:
                    SoundManager.PlaySound(SoundList.ClickBoots);
                    break;
                case ItemType.药水:
                    SoundManager.PlaySound(SoundList.ClickDrug);
                    break;
                default:
                    SoundManager.PlaySound(SoundList.ClickItem);
                    break;
            }
        }

        private int FreeSpace()
        {
            int count = 0;

            for (int i = 0; i < ItemArray.Length; i++)
                if (ItemArray[i] == null) count++;

            return count;
        }


        private bool CanRemoveItem(UserItem i)
        {
            if(MapObject.User.RidingMount && i.Info.Type != ItemType.照明物)
            {
                return false;
            }
            //stuck
            return FreeSpace() > 0;
        }

        private bool CorrectSlot(UserItem i, MirGridType grid)
        {
            ItemType type = i.Info.Type;

            switch (GridType)
            {
                case MirGridType.Equipment:
                    if (grid != MirGridType.Inventory && grid != MirGridType.Storage)
                        return false;
                    break;
                case MirGridType.HeroEquipment:
                    if (grid != MirGridType.HeroInventory)
                        return false;
                    break;                
            }

            switch ((EquipmentSlot)ItemSlot)
            {
                case EquipmentSlot.武器:
                    return type == ItemType.武器;
                case EquipmentSlot.盔甲:
                    return type == ItemType.盔甲;
                case EquipmentSlot.头盔:
                    return type == ItemType.头盔;
                case EquipmentSlot.照明物:
                    return type == ItemType.照明物;
                case EquipmentSlot.项链:
                    return type == ItemType.项链;
                case EquipmentSlot.左手镯:
                    return i.Info.Type == ItemType.手镯;
                case EquipmentSlot.右手镯:
                    return i.Info.Type == ItemType.手镯 || i.Info.Type == ItemType.护身符;
                case EquipmentSlot.左戒指:
                case EquipmentSlot.右戒指:
                    return type == ItemType.戒指;
                case EquipmentSlot.护身符:
                    return type == ItemType.护身符;// && i.Info.Shape > 0;
                case EquipmentSlot.靴子:
                    return type == ItemType.靴子;
                case EquipmentSlot.腰带:
                    return type == ItemType.腰带;
                case EquipmentSlot.守护石:
                    return type == ItemType.守护石;
                case EquipmentSlot.坐骑:
                    return type == ItemType.坐骑;
                default:
                    return false;
            }

        }
        private bool CanUseItem()
        {
            if (Item == null) return false;

            UserObject actor = GameScene.User;
            if (HeroGridType)
                actor = GameScene.Hero;

            switch (actor.Gender)
            {
                case MirGender.男性:
                    if (!Item.Info.RequiredGender.HasFlag(RequiredGender.男性))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.NotFemale), ChatType.System);
                        return false;
                    }
                    break;
                case MirGender.女性:
                    if (!Item.Info.RequiredGender.HasFlag(RequiredGender.女性))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.NotMale), ChatType.System);
                        return false;
                    }
                    break;
            }

            switch (actor.Class)
            {
                case MirClass.战士:
                    if (!Item.Info.RequiredClass.HasFlag(RequiredClass.战士))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.WarriorsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.法师:
                    if (!Item.Info.RequiredClass.HasFlag(RequiredClass.法师))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.WizardsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.道士:
                    if (!Item.Info.RequiredClass.HasFlag(RequiredClass.道士))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.TaoistsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.刺客:
                    if (!Item.Info.RequiredClass.HasFlag(RequiredClass.刺客))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.AssassinsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.弓箭:
                    if (!Item.Info.RequiredClass.HasFlag(RequiredClass.弓箭))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.ArchersCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
            }

            switch (Item.Info.RequiredType)
            {
                case RequiredType.Level:
                    if (actor.Level < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowLevel), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxAC:
                    if (actor.Stats[Stat.MaxAC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowAC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxMAC:
                    if (actor.Stats[Stat.MaxMAC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowMAC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxDC:
                    if (actor.Stats[Stat.MaxDC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowDC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxMC:
                    if (actor.Stats[Stat.MaxMC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowMC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxSC:
                    if (actor.Stats[Stat.MaxSC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowSC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxLevel:
                    if (actor.Level > Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouExceededMaxLevel), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinAC:
                    if (actor.Stats[Stat.MinAC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouDoNotHaveEnoughBaseAC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinMAC:
                    if (actor.Stats[Stat.MinMAC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouDoNotHaveEnoughBaseMac), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinDC:
                    if (actor.Stats[Stat.MinDC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouLackBaseDC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinMC:
                    if (actor.Stats[Stat.MinMC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouLackBaseMC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinSC:
                    if (actor.Stats[Stat.MinSC] < Item.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouLackBaseSC), ChatType.System);
                        return false;
                    }
                    break;
            }

            switch (Item.Info.Type)
            {
                case ItemType.马鞍:
                case ItemType.蝴蝶结:
                case ItemType.铃铛:
                case ItemType.面甲:
                case ItemType.缰绳:
                    if (actor.Equipment[(int)EquipmentSlot.坐骑] == null)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouDoNotHaveMountEquipped), ChatType.System);
                        return false;
                    }
                    break;
                case ItemType.鱼钩:
                case ItemType.鱼漂:
                case ItemType.鱼饵:
                case ItemType.探鱼器:
                case ItemType.摇轮:
                    if (actor.Equipment[(int)EquipmentSlot.武器] == null || !actor.Equipment[(int)EquipmentSlot.武器].Info.IsFishingRod)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouDoNotHaveFishingRodEquipped), ChatType.System);
                        return false;
                    }
                    break;
            }
            return true;
        }

        private bool CanWearItem(UserObject actor, UserItem i)
        {
            if (i == null) return false;

            if (actor == GameScene.Hero && actor.Dead)
                return false;

            //If Can remove;

            switch (actor.Gender)
            {
                case MirGender.男性:
                    if (!i.Info.RequiredGender.HasFlag(RequiredGender.男性))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.NotFemale), ChatType.System);
                        return false;
                    }
                    break;
                case MirGender.女性:
                    if (!i.Info.RequiredGender.HasFlag(RequiredGender.女性))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.NotMale), ChatType.System);
                        return false;
                    }
                    break;
            }

            switch (actor.Class)
            {
                case MirClass.战士:
                    if (!i.Info.RequiredClass.HasFlag(RequiredClass.战士))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.WarriorsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.法师:
                    if (!i.Info.RequiredClass.HasFlag(RequiredClass.法师))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.WizardsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.道士:
                    if (!i.Info.RequiredClass.HasFlag(RequiredClass.道士))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.TaoistsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.刺客:
                    if (!i.Info.RequiredClass.HasFlag(RequiredClass.刺客))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.AssassinsCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.弓箭:
                    if (!i.Info.RequiredClass.HasFlag(RequiredClass.弓箭))
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.ArchersCannotUseItem), ChatType.System);
                        return false;
                    }
                    break;
            }

            switch (i.Info.RequiredType)
            {
                case RequiredType.Level:
                    if (actor.Level < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowLevel), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxAC:
                    if (actor.Stats[Stat.MaxAC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowAC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxMAC:
                    if (actor.Stats[Stat.MaxMAC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowMAC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxDC:
                    if (actor.Stats[Stat.MaxDC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowDC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxMC:
                    if (actor.Stats[Stat.MaxMC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowMC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxSC:
                    if (actor.Stats[Stat.MaxSC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LowSC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MaxLevel:
                    if (actor.Level > i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouExceededMaxLevel), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinAC:
                    if (actor.Stats[Stat.MinAC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouDoNotHaveEnoughBaseAC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinMAC:
                    if (actor.Stats[Stat.MinMAC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouDoNotHaveEnoughBaseMAC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinDC:
                    if (actor.Stats[Stat.MinDC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouNotEnoughBaseDC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinMC:
                    if (actor.Stats[Stat.MinMC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouNotEnoughBaseMC), ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MinSC:
                    if (actor.Stats[Stat.MinSC] < i.Info.RequiredAmount)
                    {
                        GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.YouDoNotHaveEnoughBaseSC), ChatType.System);
                        return false;
                    }
                    break;
            }

            if (i.Info.Type == ItemType.武器 || i.Info.Type == ItemType.照明物)
            {
                if (i.Weight - (Item != null ? Item.Weight : 0) + actor.CurrentHandWeight > actor.Stats[Stat.腕力负重])
                {
                    GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.TooHeavyToHold), ChatType.System);
                    return false;
                }
            }
            else
            {
                if (i.Weight - (Item != null ? Item.Weight : 0) + actor.CurrentWearWeight > actor.Stats[Stat.装备负重])
                {
                    GameScene.Scene.ChatDialog.ReceiveChat(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.ItIsTooHeavyToWear), ChatType.System);
                    return false;
                }
            }

            switch (i.Info.Type)
            {
                case ItemType.鱼饵:
                case ItemType.探鱼器:
                case ItemType.鱼钩:
                case ItemType.摇轮:
                case ItemType.鱼漂:
                    if (!actor.HasFishingRod)
                    {
                        return false;
                    }
                    break;
                case ItemType.铃铛:
                case ItemType.缰绳:
                case ItemType.蝴蝶结:
                case ItemType.马鞍:
                    if (actor.MountType < 0)
                    {
                        return false;
                    }
                    break;
                case ItemType.镶嵌宝石:
                    if (GameScene.SelectedItem == null || GameScene.SelectedItem.Info.Type == ItemType.坐骑 || (GameScene.SelectedItem.Info.Type == ItemType.武器 && GameScene.SelectedItem.Info.IsFishingRod))
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }

        protected internal override void DrawControl()
        {
            if (Item != null && GameScene.SelectedCell != this && Locked != true)
            {
                CreateDisposeLabel();

                if (Library != null)
                {
                    ushort image = Item.Image;

                    Size imgSize = Library.GetTrueSize(image);

                    Point offSet = new Point((Size.Width - imgSize.Width) / 2, (Size.Height - imgSize.Height) / 2);

                    if (GridType == MirGridType.Craft)
                    {
                        Libraries.Prguse.Draw(1121, DisplayLocation.Add(new Point(-2, -1)), Color.White, UseOffSet, 0.8F);
                    }

                    Library.Draw(image, DisplayLocation.Add(offSet), ForeColour, UseOffSet, 1F);

                    if (Item.SealedInfo != null && Item.SealedInfo.ExpiryDate > CMain.Now)
                    {
                        Libraries.StateItems.Draw(3590, DisplayLocation.Add(new Point(2, 2)), Color.White, UseOffSet, 1F);
                    }
                }
            }
            else if (Item != null && (GameScene.SelectedCell == this || Locked))
            {
                CreateDisposeLabel();

                if (Library != null)
                {
                    ushort image = Item.Image;

                    Size imgSize = Library.GetTrueSize(image);

                    Point offSet = new Point((Size.Width - imgSize.Width) / 2, (Size.Height - imgSize.Height) / 2);

                    Library.Draw(image, DisplayLocation.Add(offSet), Color.DimGray, UseOffSet, 0.8F);
                }
            }
            else if (ShadowItem != null)
            {
                CreateDisposeLabel();

                if (Library != null)
                {
                    ushort image = ShadowItem.Info.Image;

                    Size imgSize = Library.GetTrueSize(image);

                    Point offSet = new Point((Size.Width - imgSize.Width) / 2, (Size.Height - imgSize.Height) / 2);

                    if (GridType == MirGridType.Craft)
                    {
                        Libraries.Prguse.Draw(1121, DisplayLocation.Add(new Point(-2, -1)), Color.White, UseOffSet, 0.8F);
                    }

                    Library.Draw(image, DisplayLocation.Add(offSet), Color.DimGray, UseOffSet, 0.8F);
                }
            }
            else
                DisposeCountLabel();
        }

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            if (GridType == MirGridType.Inspect)
                GameScene.Scene.CreateItemLabel(Item, true);
            else
            {
                if (Item != null)
                    GameScene.Scene.CreateItemLabel(Item);
                else if (ShadowItem != null)
                    GameScene.Scene.CreateItemLabel(ShadowItem, false, ShadowItem.CurrentDura == ShadowItem.MaxDura);
            }
        }
        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();
            GameScene.Scene.DisposeItemLabel();
            GameScene.HoverItem = null;
        }

        private void CreateDisposeLabel()
        {
            if (Item == null && ShadowItem == null)
                return;

            if (Item != null && ShadowItem == null && Item.Info.StackSize <= 1)
            {
                DisposeCountLabel();
                return;
            }

            if (CountLabel == null || CountLabel.IsDisposed)
            {
                CountLabel = new MirLabel
                {
                    AutoSize = true,
                    ForeColour = Color.Yellow,
                    NotControl = true,
                    OutLine = false,
                    Parent = this,
                };
            }

            if (ShadowItem != null)
            {
                CountLabel.ForeColour = (Item == null || ShadowItem.Count > Item.Count) ? Color.Red : Color.LimeGreen;
                CountLabel.Text = string.Format("{0}/{1}", Item == null ? 0 : Item.Count, ShadowItem.Count);
            }
            else
            {
                CountLabel.Text = Item.Count.ToString("###0");
            }

            CountLabel.Location = new Point(Size.Width - CountLabel.Size.Width, Size.Height - CountLabel.Size.Height);
        }
        private void DisposeCountLabel()
        {
            if (CountLabel != null && !CountLabel.IsDisposed)
                CountLabel.Dispose();
            CountLabel = null;
        }
    }
}
