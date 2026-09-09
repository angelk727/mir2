using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Server.MirDatabase;
using Server.MirEnvir;
using Server.MirObjects;
using C = ClientPackets;
using S = ServerPackets;
using Server.Utils;

namespace Server.MirNetwork
{
    public enum GameStage { None, Login, Select, Game, Observer, Disconnected }

    public class MirConnection
    {
        protected static Envir Envir
        {
            get { return Envir.Main; }
        }

        protected static MessageQueue MessageQueue
        {
            get { return MessageQueue.Instance; }
        }

        private enum ConnectionState
        {
            Connected,
            Disconnecting,
            Disconnected
        }

        public enum NetworkHealth
        {
            Excellent,
            Good,
            Normal,
            Poor,
            Critical
        }

        private sealed class SendState
        {
            public TcpClient Client { get; }
            public byte[] Data { get; }
            public int Offset { get; set; }

            public SendState(TcpClient client, byte[] data)
            {
                Client = client;
                Data = data;
                Offset = 0;
            }
        }

        private readonly object _sendLock = new object();
        private readonly object _connectionLock = new object();
        private readonly object _sendPumpLock = new object();

        private SendState _sendState;

        private ConnectionState _connectionState = ConnectionState.Connected;

        private byte _disconnectReason;
        private long _disconnectDeadline;

        private NetworkHealth _networkHealth = NetworkHealth.Excellent;

        private long _ping;
        private long _averagePing;
        private long _minPing = long.MaxValue;
        private long _maxPing;

        private long _packetsReceived;
        private long _packetsSent;
        private long _bytesReceived;
        private long _bytesSent;

        private long _packetRateStart;
        private int _packetRateCount;

        private long _byteRateStart;
        private long _byteRateCount;

        private int _receiveQueueCount;
        private int _sendQueueCount;

        private int _invalidPacketCount;
        private int _disconnectCount;

        private const int MaxReceiveQueue = 5000;
        private const int MaxSendQueue = 10000;
        private const int MaxProcessPacketsPerTick = 500;
        private const int MaxPacketRatePer5Seconds = 5000;
        private const long MaxSendBatchBytes = 1024 * 1024;

        public NetworkHealth Health
        {
            get { return _networkHealth; }
        }

        public long Ping
        {
            get { return _ping; }
        }

        public long AveragePing
        {
            get { return _averagePing; }
        }

        public long PacketsReceived
        {
            get { return _packetsReceived; }
        }

        public long PacketsSent
        {
            get { return _packetsSent; }
        }

        public long BytesReceived
        {
            get { return _bytesReceived; }
        }

        public long BytesSent
        {
            get { return _bytesSent; }
        }

        public int ReceiveQueueCount
        {
            get { return _receiveQueueCount; }
        }

        public int SendQueueCount
        {
            get { return _sendQueueCount; }
        }

        public int InvalidPacketCount
        {
            get { return _invalidPacketCount; }
        }

        public readonly int SessionID;
        public readonly string IPAddress;

        public GameStage Stage;

        private TcpClient _client;

        private readonly ConcurrentQueue<Packet> _receiveList;
        private readonly ConcurrentQueue<Packet> _sendList;
        private readonly Queue<Packet> _retryList;

        public bool Connected
        {
            get
            {
                return _connectionState != ConnectionState.Disconnected;
            }
        }

        public bool Disconnecting
        {
            get
            {
                return _connectionState == ConnectionState.Disconnecting;
            }
        }
        private long _lastReceiveTime;
        private long _lastSendTime;
        private long _lastKeepAliveTime;
        private long _keepAliveSentTime;
        private bool _keepAlivePending;
        private long _keepAliveToken;
        private long _sendBytes;
        private long _receiveBytes;

        private const long KeepAliveInterval = 10000;
        private const long ConnectionTimeout = 30000;
        private const long DisconnectGracePeriod = 500;
        public readonly long TimeConnected;
        public long TimeDisconnected, TimeOutTime;

        private byte[] _receiveBuffer = new byte[ushort.MaxValue];
        private int _receiveCount;

        public AccountInfo Account;
        public PlayerObject Player;

        public List<MirConnection> Observers = new List<MirConnection>();
        public MirConnection Observing;

        public List<ItemInfo> SentItemInfo = new List<ItemInfo>();
        public List<MonsterInfo> SentMonsterInfo = new List<MonsterInfo>();
        public List<NPCInfo> SentNPCInfo = new List<NPCInfo>();
        public List<QuestInfo> SentQuestInfo = new List<QuestInfo>();
        public List<RecipeInfo> SentRecipeInfo = new List<RecipeInfo>();
        public List<UserItem> SentChatItem = new List<UserItem>(); //TODO - Add Expiry time
        public List<MapInfo> SentMapInfo = new List<MapInfo>();
        public List<ulong> SentHeroInfo = new List<ulong>();
        public bool WorldMapSetupSent;
        public bool StorageSent;
        public bool HeroStorageSent;
        public Dictionary<long, DateTime> SentRankings = new Dictionary<long, DateTime>();

        private DateTime _dataCounterReset;
        private int _dataCounter;
        private FixedSizedQueue<Packet> _lastPackets;

        public MirConnection(int sessionID, TcpClient client)
        {
            SessionID = sessionID;
            IPAddress = client.Client.RemoteEndPoint.ToString().Split(':')[0];

            Envir.UpdateIPBlock(IPAddress, TimeSpan.FromSeconds(Settings.IPBlockSeconds));

            MessageQueue.Enqueue(GameLanguage.ServerTextMap.GetLocalization((ServerTextKeys.IPAddressConnected), IPAddress));

            _client = client;
            _client.NoDelay = true;

            _lastPackets = new FixedSizedQueue<Packet>(10);

            _receiveList = new ConcurrentQueue<Packet>();
            _sendList = new ConcurrentQueue<Packet>();
            _sendList.Enqueue(new S.Connected());
            _retryList = new Queue<Packet>();

            TimeConnected = Envir.Time;

            _lastReceiveTime = Envir.Time;
            _lastSendTime = Envir.Time;
            _lastKeepAliveTime = Envir.Time;

            _packetRateStart = Envir.Time;
            _byteRateStart = Envir.Time;

            _ping = 0;
            _averagePing = 0;
            _minPing = long.MaxValue;
            _maxPing = 0;

            _packetsReceived = 0;
            _packetsSent = 0;
            _bytesReceived = 0;
            _bytesSent = 0;

            _receiveQueueCount = 0;
            _sendQueueCount = 0;

            _invalidPacketCount = 0;
            _disconnectCount = 0;

            _networkHealth = NetworkHealth.Excellent;

            _keepAliveSentTime = 0;
            _keepAlivePending = false;
            _keepAliveToken = 0;

            TimeOutTime = Envir.Time + ConnectionTimeout;

            _connectionState = ConnectionState.Connected;

            BeginReceive();
        }

        public void AddObserver(MirConnection c)
        {
            if (c == null || c == this)
                return;

            Observers.Add(c);

            if (c.Observing != null)
                c.Observing.Observers.Remove(c);

            c.Observing = this;
            c.Stage = GameStage.Observer;
        }

        private void BeginReceive()
        {
            if (!Connected || _client == null || _receiveBuffer == null)
                return;

            try
            {
                _client.Client.BeginReceive(
                    _receiveBuffer,
                    _receiveCount,
                    _receiveBuffer.Length - _receiveCount,
                    SocketFlags.None,
                    ReceiveData,
                    _client);
            }
            catch
            {
                BeginDisconnect(20);
            }
        }

        private void ReceiveData(IAsyncResult result)
        {
            TcpClient client = result.AsyncState as TcpClient;

            if (!Connected || client == null || client != _client)
                return;

            int dataRead;

            try
            {
                dataRead = client.Client.EndReceive(result);
            }
            catch
            {
                BeginDisconnect(20);
                return;
            }

            if (dataRead <= 0)
            {
                BeginDisconnect(20);
                return;
            }

            if (_receiveBuffer == null)
                return;

            _receiveCount += dataRead;

            if (_dataCounterReset < Envir.Now)
            {
                _dataCounterReset = Envir.Now.AddSeconds(5);
                _dataCounter = 0;
            }

            try
            {
                int offset = 0;

                while (offset < _receiveCount)
                {
                    Packet p = Packet.ReceivePacket(_receiveBuffer, offset, _receiveCount - offset, out int consumed);

                    if (p == null)
                        break;

                    if (consumed <= 0)
                        throw new InvalidDataException("无效的数据包长度");

                    offset += consumed;

                    if (_receiveQueueCount >= MaxReceiveQueue)
                    {
                        _invalidPacketCount++;
                        BeginDisconnect(20);
                        return;
                    }

                    _receiveList.Enqueue(p);
                    _receiveQueueCount++;

                    _lastPackets.Enqueue(p);

                    _dataCounter++;
                    _packetsReceived++;
                    _bytesReceived += consumed;

                    _packetRateCount++;
                    _byteRateCount += consumed;

                    _lastReceiveTime = Envir.Time;
                    TimeOutTime = _lastReceiveTime + ConnectionTimeout;
                }

                if (offset > 0)
                {
                    int remaining = _receiveCount - offset;

                    if (remaining > 0)
                        Buffer.BlockCopy(
                            _receiveBuffer,
                            offset,
                            _receiveBuffer,
                            0,
                            remaining);

                    _receiveCount = remaining;
                }
            }
            catch
            {
                Envir.UpdateIPBlock(IPAddress, TimeSpan.FromHours(24));

                MessageQueue.Enqueue(
                    GameLanguage.ServerTextMap.GetLocalization(
                        (ServerTextKeys.IPAddressDisconnectedInvalidPacket),
                        IPAddress));

                BeginDisconnect(20);
                return;
            }

            if (_dataCounter > Settings.MaxPacket)
            {
                Envir.UpdateIPBlock(IPAddress, TimeSpan.FromHours(24));

                List<string> packetList = new List<string>();

                while (_lastPackets.Count > 0)
                {
                    _lastPackets.TryDequeue(out Packet pkt);

                    Enum.TryParse<ClientPacketIds>(
                        (pkt?.Index ?? 0).ToString(),
                        out ClientPacketIds cPacket);

                    packetList.Add(cPacket.ToString());
                }

                MessageQueue.Enqueue(
                    GameLanguage.ServerTextMap.GetLocalization(
                        (ServerTextKeys.IPAddressDisconnectedLargePackets),
                        IPAddress,
                        String.Join(",", packetList.Distinct())));

                BeginDisconnect(20);
                return;
            }

            BeginReceive();
        }

        private void BeginSend(List<byte> data)
        {
            if (data == null || data.Count == 0)
                return;

            SendState state;

            lock (_sendLock)
            {
                if (!Connected || _client == null || _sendState != null)
                    return;

                byte[] bytes = data.ToArray();

                if (bytes.Length == 0)
                    return;

                state = new SendState(_client, bytes);
                _sendState = state;
            }

            BeginSend(state);
        }

        private void BeginSend(SendState state)
        {
            if (state == null)
                return;

            TcpClient client = state.Client;

            lock (_sendLock)
            {
                if (!Connected || _client != client || _sendState != state)
                    return;
            }

            int remaining = state.Data.Length - state.Offset;

            if (remaining <= 0)
            {
                SendState completedState = null;

                lock (_sendLock)
                {
                    if (_sendState == state)
                    {
                        _sendState = null;
                        completedState = state;
                    }
                }

                if (completedState != null)
                    SendCompleted(completedState);

                return;
            }

            try
            {
                client.Client.BeginSend(
                    state.Data,
                    state.Offset,
                    remaining,
                    SocketFlags.None,
                    SendData,
                    state);
            }
            catch
            {
                bool owned;

                lock (_sendLock)
                {
                    owned = _sendState == state;

                    if (owned)
                        _sendState = null;
                }

                if (owned)
                    BeginDisconnect(20);
            }
        }

        private void SendData(IAsyncResult result)
        {
            SendState state = result.AsyncState as SendState;

            if (state == null)
                return;

            TcpClient client = state.Client;

            int sent;

            try
            {
                sent = client.Client.EndSend(result);
            }
            catch
            {
                bool owned;

                lock (_sendLock)
                {
                    owned = _sendState == state;

                    if (owned)
                        _sendState = null;
                }

                if (owned)
                    BeginDisconnect(20);

                return;
            }

            if (sent <= 0)
            {
                bool owned;

                lock (_sendLock)
                {
                    owned = _sendState == state;

                    if (owned)
                        _sendState = null;
                }

                if (owned)
                    BeginDisconnect(20);

                return;
            }

            bool continueSending;

            lock (_sendLock)
            {
                if (_sendState != state)
                    return;

                state.Offset += sent;

                continueSending = state.Offset < state.Data.Length;

                if (!continueSending)
                {
                    _sendState = null;
                    _lastSendTime = Envir.Time;
                    _sendBytes += state.Data.Length;
                }
            }

            if (continueSending)
            {
                BeginSend(state);
                return;
            }

            SendCompleted(state);
        }

        private void SendCompleted(SendState state)
        {
            if (!Connected)
                return;

            if (Disconnecting)
            {
                if (_sendList.IsEmpty)
                {
                    Disconnect(_disconnectReason);
                    return;
                }

                TryBeginSend();
                return;
            }

            TryBeginSend();
        }

        private void TryBeginSend()
        {
            List<byte> data;

            lock (_sendPumpLock)
            {
                if (!Connected || _client == null)
                    return;

                lock (_sendLock)
                {
                    if (!Connected || _client == null || _sendState != null)
                        return;
                }

                if (_sendList.IsEmpty)
                    return;

                data = new List<byte>();

                while (_sendList.TryDequeue(out Packet p))
                {
                    _sendQueueCount--;

                    IEnumerable<byte> packetBytes = p.GetPacketBytes();

                    if (packetBytes == null)
                        continue;

                    int packetLength = packetBytes.Count();

                    if (packetLength == 0)
                        continue;

                    if (data.Count + packetLength > MaxSendBatchBytes)
                    {
                        _sendList.Enqueue(p);
                        _sendQueueCount++;
                        break;
                    }

                    data.AddRange(packetBytes);
                }

                if (data.Count == 0)
                    return;
            }

            BeginSend(data);
        }

        public void Enqueue(Packet p)
        {
            if (p == null)
                return;

            lock (_connectionLock)
            {
                if (_connectionState != ConnectionState.Connected)
                    return;

                if (_sendQueueCount >= MaxSendQueue)
                {
                    BeginDisconnect(20);
                    return;
                }

                _sendList.Enqueue(p);
                _sendQueueCount++;
            }

            if (!p.Observable)
                return;

            foreach (MirConnection c in Observers)
            {
                if (c == null || !c.Connected)
                    continue;

                c.Enqueue(p);
            }
        }

        public void Process()
        {
            //=========================================================
            // 1. Connection state check
            //=========================================================

            if (_connectionState == ConnectionState.Disconnected)
                return;


            //=========================================================
            // 2. Socket connection check
            //=========================================================

            if (_client == null || !_client.Connected)
            {
                Disconnect(20);
                return;
            }


            //=========================================================
            // 3. Connected state
            //=========================================================

            if (_connectionState == ConnectionState.Connected)
            {
                // ----------------------------------------------------
                // 3.1 Process received packets
                // ----------------------------------------------------

                while (_receiveList.TryDequeue(out Packet p))
                {
                    if (p == null)
                        continue;

                    ProcessPacket(p);

                    // ProcessPacket() may change the connection state.
                    if (_connectionState != ConnectionState.Connected)
                        break;
                }


                // ----------------------------------------------------
                // 3.2 Process retry packets
                // ----------------------------------------------------

                if (_connectionState == ConnectionState.Connected)
                {
                    while (_retryList.Count > 0)
                    {
                        Packet retryPacket = _retryList.Dequeue();

                        if (retryPacket == null)
                            continue;

                        _receiveList.Enqueue(retryPacket);
                    }
                }


                // ----------------------------------------------------
                // 3.3 Connection timeout
                // ----------------------------------------------------

                if (_connectionState == ConnectionState.Connected)
                {
                    if (Envir.Time >= TimeOutTime)
                    {
                        Disconnect(21);
                        return;
                    }
                }


                // ----------------------------------------------------
                // 3.4 KeepAlive
                // ----------------------------------------------------

                if (_connectionState == ConnectionState.Connected)
                {
                    if (!_keepAlivePending &&
                        Envir.Time - _lastKeepAliveTime >= KeepAliveInterval)
                    {
                        _keepAliveToken = Envir.Time;
                        _keepAliveSentTime = Envir.Time;
                        _lastKeepAliveTime = Envir.Time;
                        _keepAlivePending = true;

                        Enqueue(new S.KeepAlive
                        {
                            Time = _keepAliveToken
                        });
                    }
                }
            }


            //=========================================================
            // 4. Disconnecting state
            //=========================================================

            if (_connectionState == ConnectionState.Disconnecting)
            {
                if (Envir.Time >= _disconnectDeadline)
                {
                    Disconnect(_disconnectReason);
                    return;
                }
            }


            //=========================================================
            // 5. Send queued packets
            //=========================================================

            TryBeginSend();


            //=========================================================
            // 6. Network statistics
            //=========================================================

            UpdateNetworkStatistics();
        }

        private void UpdateNetworkStatistics()
        {
            long now = Envir.Time;

            if (now - _packetRateStart >= 5000)
            {
                _packetRateStart = now;
                _packetRateCount = 0;
            }

            if (now - _byteRateStart >= 5000)
            {
                _byteRateStart = now;
                _byteRateCount = 0;
            }

            UpdateNetworkHealth();
        }
        private void ProcessPacket(Packet p)
        {
            if (p == null || _connectionState != ConnectionState.Connected)
                return;

            switch (p.Index)
            {
                case (short)ClientPacketIds.ClientVersion:
                    ClientVersion((C.ClientVersion)p);
                    break;

                case (short)ClientPacketIds.Disconnect:
                    Disconnect(22);
                    break;

                case (short)ClientPacketIds.KeepAlive:
                    ClientKeepAlive((C.KeepAlive)p);
                    break;

                case (short)ClientPacketIds.NewAccount:
                    NewAccount((C.NewAccount)p);
                    break;

                case (short)ClientPacketIds.ChangePassword:
                    ChangePassword((C.ChangePassword)p);
                    break;

                case (short)ClientPacketIds.UnlockStorage:
                    UnlockStorage((C.UnlockStorage)p);
                    break;

                case (short)ClientPacketIds.SetStoragePassword:
                    SetStoragePassword((C.SetStoragePassword)p);
                    break;

                case (short)ClientPacketIds.RemoveStoragePassword:
                    RemoveStoragePassword((C.RemoveStoragePassword)p);
                    break;

                case (short)ClientPacketIds.Login:
                    Login((C.Login)p);
                    break;

                case (short)ClientPacketIds.NewCharacter:
                    NewCharacter((C.NewCharacter)p);
                    break;

                case (short)ClientPacketIds.DeleteCharacter:
                    DeleteCharacter((C.DeleteCharacter)p);
                    break;

                case (short)ClientPacketIds.StartGame:
                    StartGame((C.StartGame)p);
                    break;

                case (short)ClientPacketIds.LogOut:
                    LogOut();
                    break;

                case (short)ClientPacketIds.Turn:
                    Turn((C.Turn)p);
                    break;

                case (short)ClientPacketIds.Walk:
                    Walk((C.Walk)p);
                    break;

                case (short)ClientPacketIds.Run:
                    Run((C.Run)p);
                    break;

                case (short)ClientPacketIds.Chat:
                    Chat((C.Chat)p);
                    break;

                case (short)ClientPacketIds.MoveItem:
                    MoveItem((C.MoveItem)p);
                    break;

                case (short)ClientPacketIds.StoreItem:
                    StoreItem((C.StoreItem)p);
                    break;

                case (short)ClientPacketIds.DepositRefineItem:
                    DepositRefineItem((C.DepositRefineItem)p);
                    break;

                case (short)ClientPacketIds.RetrieveRefineItem:
                    RetrieveRefineItem((C.RetrieveRefineItem)p);
                    break;

                case (short)ClientPacketIds.RefineCancel:
                    RefineCancel((C.RefineCancel)p);
                    break;

                case (short)ClientPacketIds.RefineItem:
                    RefineItem((C.RefineItem)p);
                    break;

                case (short)ClientPacketIds.CheckRefine:
                    CheckRefine((C.CheckRefine)p);
                    break;

                case (short)ClientPacketIds.ReplaceWedRing:
                    ReplaceWedRing((C.ReplaceWedRing)p);
                    break;

                case (short)ClientPacketIds.DepositTradeItem:
                    DepositTradeItem((C.DepositTradeItem)p);
                    break;

                case (short)ClientPacketIds.RetrieveTradeItem:
                    RetrieveTradeItem((C.RetrieveTradeItem)p);
                    break;

                case (short)ClientPacketIds.TakeBackItem:
                    TakeBackItem((C.TakeBackItem)p);
                    break;

                case (short)ClientPacketIds.MergeItem:
                    MergeItem((C.MergeItem)p);
                    break;

                case (short)ClientPacketIds.EquipItem:
                    EquipItem((C.EquipItem)p);
                    break;

                case (short)ClientPacketIds.RemoveItem:
                    RemoveItem((C.RemoveItem)p);
                    break;

                case (short)ClientPacketIds.RemoveSlotItem:
                    RemoveSlotItem((C.RemoveSlotItem)p);
                    break;

                case (short)ClientPacketIds.SplitItem:
                    SplitItem((C.SplitItem)p);
                    break;

                case (short)ClientPacketIds.UseItem:
                    UseItem((C.UseItem)p);
                    break;

                case (short)ClientPacketIds.DropItem:
                    DropItem((C.DropItem)p);
                    break;

                case (short)ClientPacketIds.TakeBackHeroItem:
                    TakeBackHeroItem((C.TakeBackHeroItem)p);
                    break;

                case (short)ClientPacketIds.TransferHeroItem:
                    TransferHeroItem((C.TransferHeroItem)p);
                    break;

                case (short)ClientPacketIds.DropGold:
                    DropGold((C.DropGold)p);
                    break;

                case (short)ClientPacketIds.PickUp:
                    PickUp();
                    break;

                case (short)ClientPacketIds.RequestMapInfo:
                    RequestMapInfo((C.RequestMapInfo)p);
                    break;

                case (short)ClientPacketIds.RequestMonsterInfo:
                    RequestMonsterInfo((C.RequestMonsterInfo)p);
                    break;

                case (short)ClientPacketIds.RequestNPCInfo:
                    RequestNPCInfo((C.RequestNPCInfo)p);
                    break;

                case (short)ClientPacketIds.RequestItemInfo:
                    RequestItemInfo((C.RequestItemInfo)p);
                    break;

                case (short)ClientPacketIds.TeleportToNPC:
                    TeleportToNPC((C.TeleportToNPC)p);
                    break;

                case (short)ClientPacketIds.SearchMap:
                    SearchMap((C.SearchMap)p);
                    break;

                case (short)ClientPacketIds.Inspect:
                    Inspect((C.Inspect)p);
                    break;

                case (short)ClientPacketIds.Observe:
                    Observe((C.Observe)p);
                    break;

                case (short)ClientPacketIds.ChangeAMode:
                    ChangeAMode((C.ChangeAMode)p);
                    break;

                case (short)ClientPacketIds.ChangePMode:
                    ChangePMode((C.ChangePMode)p);
                    break;

                case (short)ClientPacketIds.ChangeTrade:
                    ChangeTrade((C.ChangeTrade)p);
                    break;

                case (short)ClientPacketIds.Attack:
                    Attack((C.Attack)p);
                    break;

                case (short)ClientPacketIds.RangeAttack:
                    RangeAttack((C.RangeAttack)p);
                    break;

                case (short)ClientPacketIds.Harvest:
                    Harvest((C.Harvest)p);
                    break;

                case (short)ClientPacketIds.CallNPC:
                    CallNPC((C.CallNPC)p);
                    break;

                case (short)ClientPacketIds.BuyItem:
                    BuyItem((C.BuyItem)p);
                    break;

                case (short)ClientPacketIds.CraftItem:
                    CraftItem((C.CraftItem)p);
                    break;

                case (short)ClientPacketIds.SellItem:
                    SellItem((C.SellItem)p);
                    break;

                case (short)ClientPacketIds.RepairItem:
                    RepairItem((C.RepairItem)p);
                    break;

                case (short)ClientPacketIds.BuyItemBack:
                    BuyItemBack((C.BuyItemBack)p);
                    break;

                case (short)ClientPacketIds.SRepairItem:
                    SRepairItem((C.SRepairItem)p);
                    break;

                case (short)ClientPacketIds.MagicKey:
                    MagicKey((C.MagicKey)p);
                    break;

                case (short)ClientPacketIds.Magic:
                    Magic((C.Magic)p);
                    break;

                case (short)ClientPacketIds.SwitchGroup:
                    SwitchGroup((C.SwitchGroup)p);
                    return;

                case (short)ClientPacketIds.AddMember:
                    AddMember((C.AddMember)p);
                    return;

                case (short)ClientPacketIds.DelMember:
                    DelMember((C.DelMember)p);
                    return;

                case (short)ClientPacketIds.GroupInvite:
                    GroupInvite((C.GroupInvite)p);
                    return;

                case (short)ClientPacketIds.NewHero:
                    NewHero((C.NewHero)p);
                    break;

                case (short)ClientPacketIds.SetAutoPotValue:
                    SetAutoPotValue((C.SetAutoPotValue)p);
                    break;

                case (short)ClientPacketIds.SetAutoPotItem:
                    SetAutoPotItem((C.SetAutoPotItem)p);
                    break;

                case (short)ClientPacketIds.SetHeroBehaviour:
                    SetHeroBehaviour((C.SetHeroBehaviour)p);
                    break;

                case (short)ClientPacketIds.ChangeHero:
                    ChangeHero((C.ChangeHero)p);
                    break;

                case (short)ClientPacketIds.TownRevive:
                    TownRevive();
                    return;

                case (short)ClientPacketIds.SpellToggle:
                    SpellToggle((C.SpellToggle)p);
                    return;

                case (short)ClientPacketIds.ConsignItem:
                    ConsignItem((C.ConsignItem)p);
                    return;

                case (short)ClientPacketIds.MarketSearch:
                    MarketSearch((C.MarketSearch)p);
                    return;

                case (short)ClientPacketIds.MarketRefresh:
                    MarketRefresh();
                    return;

                case (short)ClientPacketIds.MarketPage:
                    MarketPage((C.MarketPage)p);
                    return;

                case (short)ClientPacketIds.MarketBuy:
                    MarketBuy((C.MarketBuy)p);
                    return;

                case (short)ClientPacketIds.MarketGetBack:
                    MarketGetBack((C.MarketGetBack)p);
                    return;

                case (short)ClientPacketIds.MarketSellNow:
                    MarketSellNow((C.MarketSellNow)p);
                    return;

                case (short)ClientPacketIds.RequestUserName:
                    RequestUserName((C.RequestUserName)p);
                    return;

                case (short)ClientPacketIds.RequestChatItem:
                    RequestChatItem((C.RequestChatItem)p);
                    return;

                case (short)ClientPacketIds.EditGuildMember:
                    EditGuildMember((C.EditGuildMember)p);
                    return;

                case (short)ClientPacketIds.EditGuildNotice:
                    EditGuildNotice((C.EditGuildNotice)p);
                    return;

                case (short)ClientPacketIds.GuildInvite:
                    GuildInvite((C.GuildInvite)p);
                    return;

                case (short)ClientPacketIds.RequestGuildInfo:
                    RequestGuildInfo((C.RequestGuildInfo)p);
                    return;

                case (short)ClientPacketIds.GuildNameReturn:
                    GuildNameReturn((C.GuildNameReturn)p);
                    return;

                case (short)ClientPacketIds.GuildStorageGoldChange:
                    GuildStorageGoldChange((C.GuildStorageGoldChange)p);
                    return;

                case (short)ClientPacketIds.GuildStorageItemChange:
                    GuildStorageItemChange((C.GuildStorageItemChange)p);
                    return;

                case (short)ClientPacketIds.GuildWarReturn:
                    GuildWarReturn((C.GuildWarReturn)p);
                    return;

                case (short)ClientPacketIds.MarriageRequest:
                    MarriageRequest((C.MarriageRequest)p);
                    return;

                case (short)ClientPacketIds.MarriageReply:
                    MarriageReply((C.MarriageReply)p);
                    return;

                case (short)ClientPacketIds.ChangeMarriage:
                    ChangeMarriage((C.ChangeMarriage)p);
                    return;

                case (short)ClientPacketIds.DivorceRequest:
                    DivorceRequest((C.DivorceRequest)p);
                    return;

                case (short)ClientPacketIds.DivorceReply:
                    DivorceReply((C.DivorceReply)p);
                    return;

                case (short)ClientPacketIds.AddMentor:
                    AddMentor((C.AddMentor)p);
                    return;

                case (short)ClientPacketIds.MentorReply:
                    MentorReply((C.MentorReply)p);
                    return;

                case (short)ClientPacketIds.AllowMentor:
                    AllowMentor((C.AllowMentor)p);
                    return;

                case (short)ClientPacketIds.CancelMentor:
                    CancelMentor((C.CancelMentor)p);
                    return;

                case (short)ClientPacketIds.TradeRequest:
                    TradeRequest((C.TradeRequest)p);
                    return;

                case (short)ClientPacketIds.TradeGold:
                    TradeGold((C.TradeGold)p);
                    return;

                case (short)ClientPacketIds.TradeReply:
                    TradeReply((C.TradeReply)p);
                    return;

                case (short)ClientPacketIds.TradeConfirm:
                    TradeConfirm((C.TradeConfirm)p);
                    return;

                case (short)ClientPacketIds.TradeCancel:
                    TradeCancel((C.TradeCancel)p);
                    return;

                case (short)ClientPacketIds.EquipSlotItem:
                    EquipSlotItem((C.EquipSlotItem)p);
                    break;

                case (short)ClientPacketIds.FishingCast:
                    FishingCast((C.FishingCast)p);
                    break;

                case (short)ClientPacketIds.FishingChangeAutocast:
                    FishingChangeAutocast((C.FishingChangeAutocast)p);
                    break;

                case (short)ClientPacketIds.AcceptQuest:
                    AcceptQuest((C.AcceptQuest)p);
                    break;

                case (short)ClientPacketIds.FinishQuest:
                    FinishQuest((C.FinishQuest)p);
                    break;

                case (short)ClientPacketIds.AbandonQuest:
                    AbandonQuest((C.AbandonQuest)p);
                    break;

                case (short)ClientPacketIds.ShareQuest:
                    ShareQuest((C.ShareQuest)p);
                    break;

                case (short)ClientPacketIds.AcceptReincarnation:
                    AcceptReincarnation();
                    break;

                case (short)ClientPacketIds.CancelReincarnation:
                    CancelReincarnation();
                    break;

                case (short)ClientPacketIds.CombineItem:
                    CombineItem((C.CombineItem)p);
                    break;

                case (short)ClientPacketIds.AwakeningNeedMaterials:
                    AwakeningNeedMaterials((C.AwakeningNeedMaterials)p);
                    break;

                case (short)ClientPacketIds.AwakeningLockedItem:
                    Enqueue(new S.AwakeningLockedItem
                    {
                        UniqueID = ((C.AwakeningLockedItem)p).UniqueID,
                        Locked = ((C.AwakeningLockedItem)p).Locked
                    });
                    break;

                case (short)ClientPacketIds.Awakening:
                    Awakening((C.Awakening)p);
                    break;

                case (short)ClientPacketIds.DisassembleItem:
                    DisassembleItem((C.DisassembleItem)p);
                    break;

                case (short)ClientPacketIds.DowngradeAwakening:
                    DowngradeAwakening((C.DowngradeAwakening)p);
                    break;

                case (short)ClientPacketIds.ResetAddedItem:
                    ResetAddedItem((C.ResetAddedItem)p);
                    break;

                case (short)ClientPacketIds.SendMail:
                    SendMail((C.SendMail)p);
                    break;

                case (short)ClientPacketIds.ReadMail:
                    ReadMail((C.ReadMail)p);
                    break;

                case (short)ClientPacketIds.CollectParcel:
                    CollectParcel((C.CollectParcel)p);
                    break;

                case (short)ClientPacketIds.DeleteMail:
                    DeleteMail((C.DeleteMail)p);
                    break;

                case (short)ClientPacketIds.LockMail:
                    LockMail((C.LockMail)p);
                    break;

                case (short)ClientPacketIds.MailLockedItem:
                    Enqueue(new S.MailLockedItem
                    {
                        UniqueID = ((C.MailLockedItem)p).UniqueID,
                        Locked = ((C.MailLockedItem)p).Locked
                    });
                    break;

                case (short)ClientPacketIds.MailCost:
                    MailCost((C.MailCost)p);
                    break;

                case (short)ClientPacketIds.RequestIntelligentCreatureUpdates:
                    RequestIntelligentCreatureUpdates((C.RequestIntelligentCreatureUpdates)p);
                    break;

                case (short)ClientPacketIds.UpdateIntelligentCreature:
                    UpdateIntelligentCreature((C.UpdateIntelligentCreature)p);
                    break;

                case (short)ClientPacketIds.IntelligentCreaturePickup:
                    IntelligentCreaturePickup((C.IntelligentCreaturePickup)p);
                    break;

                case (short)ClientPacketIds.AddFriend:
                    AddFriend((C.AddFriend)p);
                    break;

                case (short)ClientPacketIds.RemoveFriend:
                    RemoveFriend((C.RemoveFriend)p);
                    break;

                case (short)ClientPacketIds.RefreshFriends:
                    {
                        if (Stage != GameStage.Game)
                            return;

                        Player.GetFriends();
                        break;
                    }

                case (short)ClientPacketIds.AddMemo:
                    AddMemo((C.AddMemo)p);
                    break;

                case (short)ClientPacketIds.GuildBuffUpdate:
                    GuildBuffUpdate((C.GuildBuffUpdate)p);
                    break;

                case (short)ClientPacketIds.GameshopBuy:
                    GameshopBuy((C.GameshopBuy)p);
                    return;

                case (short)ClientPacketIds.NPCConfirmInput:
                    NPCConfirmInput((C.NPCConfirmInput)p);
                    break;

                case (short)ClientPacketIds.ReportIssue:
                    ReportIssue((C.ReportIssue)p);
                    break;

                case (short)ClientPacketIds.GetRanking:
                    GetRanking((C.GetRanking)p);
                    break;

                case (short)ClientPacketIds.Opendoor:
                    Opendoor((C.Opendoor)p);
                    break;

                case (short)ClientPacketIds.GetRentedItems:
                    GetRentedItems();
                    break;

                case (short)ClientPacketIds.ItemRentalRequest:
                    ItemRentalRequest();
                    break;

                case (short)ClientPacketIds.ItemRentalFee:
                    ItemRentalFee((C.ItemRentalFee)p);
                    break;

                case (short)ClientPacketIds.ItemRentalPeriod:
                    ItemRentalPeriod((C.ItemRentalPeriod)p);
                    break;

                case (short)ClientPacketIds.DepositRentalItem:
                    DepositRentalItem((C.DepositRentalItem)p);
                    break;

                case (short)ClientPacketIds.RetrieveRentalItem:
                    RetrieveRentalItem((C.RetrieveRentalItem)p);
                    break;

                case (short)ClientPacketIds.CancelItemRental:
                    CancelItemRental();
                    break;

                case (short)ClientPacketIds.ItemRentalLockFee:
                    ItemRentalLockFee();
                    break;

                case (short)ClientPacketIds.ItemRentalLockItem:
                    ItemRentalLockItem();
                    break;

                case (short)ClientPacketIds.ConfirmItemRental:
                    ConfirmItemRental();
                    break;

                case (short)ClientPacketIds.GuildTerritoryPage:
                    GuildTerritoryPage((C.GuildTerritoryPage)p);
                    return;

                case (short)ClientPacketIds.PurchaseGuildTerritory:
                    PurchaseGuildTerritory((C.PurchaseGuildTerritory)p);
                    return;

                case (short)ClientPacketIds.DeleteItem:
                    DeleteItem((C.DeleteItem)p);
                    break;

                default:
                    MessageQueue.Enqueue(
                        GameLanguage.ServerTextMap.GetLocalization(
                            (ServerTextKeys.InvalidPacketReceived),
                            p.Index));
                    break;
            }
        }

        private void BeginDisconnect(byte reason)
        {
            lock (_connectionLock)
            {
                if (_connectionState != ConnectionState.Connected)
                    return;

                _disconnectReason = reason;
                _disconnectDeadline = Envir.Time + DisconnectGracePeriod;
                _connectionState = ConnectionState.Disconnecting;
            }

            SoftDisconnect(reason);
            TryBeginSend();
        }

        public void SoftDisconnect(byte reason)
        {
            if (Stage == GameStage.Disconnected && Player == null && Account == null)
                return;

            Stage = GameStage.Disconnected;
            TimeDisconnected = Envir.Time;

            lock (Envir.AccountLock)
            {
                if (Player != null)
                    Player.StopGame(reason);

                if (Account != null && Account.Connection == this)
                    Account.Connection = null;
            }

            Player = null;
            Account = null;
        }

        public void Disconnect(byte reason)
        {
            lock (_connectionLock)
            {
                if (_connectionState == ConnectionState.Disconnected)
                    return;

                _connectionState = ConnectionState.Disconnected;
                _disconnectReason = reason;
                TimeDisconnected = Envir.Time;
                Stage = GameStage.Disconnected;
                _disconnectCount++;
            }

            lock (Envir.Connections)
                Envir.Connections.Remove(this);

            lock (Envir.AccountLock)
            {
                if (Player != null)
                    Player.StopGame(reason);

                if (Account != null && Account.Connection == this)
                    Account.Connection = null;
            }

            if (Observing != null)
            {
                Observing.Observers.Remove(this);
                Observing = null;
            }

            CleanObservers();

            Account = null;
            Player = null;

            lock (_sendLock)
            {
                _sendState = null;
            }

            _receiveCount = 0;
            _receiveQueueCount = 0;
            _sendQueueCount = 0;

            TcpClient client = _client;
            _client = null;

            if (client != null)
            {
                try
                {
                    client.Client.Shutdown(SocketShutdown.Both);
                }
                catch
                {
                }

                try
                {
                    client.Client.Dispose();
                }
                catch
                {
                }

                try
                {
                    client.Dispose();
                }
                catch
                {
                }
            }
        }

        public void SendDisconnect(byte reason)
        {
            lock (_connectionLock)
            {
                if (_connectionState != ConnectionState.Connected)
                    return;

                _disconnectReason = reason;
                _disconnectDeadline = Envir.Time + DisconnectGracePeriod;
                _connectionState = ConnectionState.Disconnecting;

                _sendList.Enqueue(new S.Disconnect
                {
                    Reason = reason
                });
            }

            SoftDisconnect(reason);

            TryBeginSend();
        }

        public void CleanObservers()
        {
            foreach (MirConnection c in Observers)
            {
                if (c == null)
                    continue;

                c.Stage = GameStage.Login;
                c.Enqueue(new S.ReturnToLogin());
            }

            Observers.Clear();
        }

        private void ClientVersion(C.ClientVersion p)
        {
            if (Stage != GameStage.None)
                return;

            if (Settings.CheckVersion)
            {
                bool match = false;

                foreach (var hash in Settings.VersionHashes)
                {
                    if (Functions.CompareBytes(hash, p.VersionHash))
                    {
                        match = true;
                        break;
                    }
                }

                if (!match)
                {
                    Enqueue(new S.ClientVersion
                    {
                        Result = 0
                    });

                    MessageQueue.Enqueue(
                        GameLanguage.ServerTextMap.GetLocalization(
                            (ServerTextKeys.PlayerDisconnectedWrongClientVersion),
                            SessionID));

                    SendDisconnect(10);
                    return;
                }
            }

            MessageQueue.Enqueue(
                GameLanguage.ServerTextMap.GetLocalization(
                    (ServerTextKeys.ClientVersionMatched),
                    SessionID,
                    IPAddress));

            Enqueue(new S.ClientVersion
            {
                Result = 1
            });

            Stage = GameStage.Login;
        }

        private void ClientKeepAlive(C.KeepAlive p)
        {
            if (!_keepAlivePending || p.Time != _keepAliveToken)
                return;

            _keepAlivePending = false;

            long rtt = Envir.Time - _keepAliveSentTime;

            if (rtt < 0)
                rtt = 0;

            _ping = rtt;

            if (_minPing == long.MaxValue || rtt < _minPing)
                _minPing = rtt;

            if (rtt > _maxPing)
                _maxPing = rtt;

            if (_averagePing == 0)
                _averagePing = rtt;
            else
                _averagePing = (_averagePing * 3 + rtt) / 4;

            UpdateNetworkHealth();
        }

        private void UpdateNetworkHealth()
        {
            if (_connectionState != ConnectionState.Connected)
            {
                _networkHealth = NetworkHealth.Critical;
                return;
            }

            if (_averagePing <= 80 &&
                _receiveQueueCount <= 20 &&
                _sendQueueCount <= 50)
            {
                _networkHealth = NetworkHealth.Excellent;
                return;
            }

            if (_averagePing <= 150 &&
                _receiveQueueCount <= 100 &&
                _sendQueueCount <= 200)
            {
                _networkHealth = NetworkHealth.Good;
                return;
            }

            if (_averagePing <= 250 &&
                _receiveQueueCount <= 500 &&
                _sendQueueCount <= 1000)
            {
                _networkHealth = NetworkHealth.Normal;
                return;
            }

            if (_averagePing <= 500 &&
                _receiveQueueCount <= 2000 &&
                _sendQueueCount <= 5000)
            {
                _networkHealth = NetworkHealth.Poor;
                return;
            }

            _networkHealth = NetworkHealth.Critical;
        }

        private void NewAccount(C.NewAccount p)
        {
            if (Stage != GameStage.Login)
                return;

            MessageQueue.Enqueue(
                GameLanguage.ServerTextMap.GetLocalization(
                    (ServerTextKeys.NewAccountBeingCreated),
                    SessionID,
                    IPAddress));

            Envir.NewAccount(p, this);
        }

        private void ChangePassword(C.ChangePassword p)
        {
            if (Stage != GameStage.Login)
                return;

            MessageQueue.Enqueue(
                GameLanguage.ServerTextMap.GetLocalization(
                    (ServerTextKeys.PasswordBeingChanged),
                    SessionID,
                    IPAddress));

            Envir.ChangePassword(p, this);
        }

        private void UnlockStorage(C.UnlockStorage p)
        {
            if (Stage != GameStage.Game || Player == null || Account == null)
            {
                Enqueue(new S.StorageUnlockResult
                {
                    Result = 3,
                    HasPassword = Account != null && Account.HasStoragePassword
                });
                return;
            }

            if (!CanAccessStorageNpc())
            {
                Enqueue(new S.StorageUnlockResult
                {
                    Result = 3,
                    HasPassword = Account.HasStoragePassword
                });
                return;
            }

            if (!Account.HasStoragePassword)
            {
                Player.SetStorageUnlocked(true);

                Enqueue(new S.StorageUnlockResult
                {
                    Result = 4,
                    HasPassword = false
                });

                return;
            }

            if (!Envir.IsPasswordValid(p.Password))
            {
                Enqueue(new S.StorageUnlockResult
                {
                    Result = 1,
                    HasPassword = true
                });

                return;
            }

            if (!Account.ValidateStoragePassword(p.Password))
            {
                Enqueue(new S.StorageUnlockResult
                {
                    Result = 2,
                    HasPassword = true
                });

                return;
            }

            Player.SetStorageUnlocked(true);

            Enqueue(new S.StorageUnlockResult
            {
                Result = 0,
                HasPassword = true
            });

            Player.SendStorage();
        }

        private void SetStoragePassword(C.SetStoragePassword p)
        {
            if (Stage != GameStage.Game || Player == null || Account == null)
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 0,
                    Removing = false,
                    HasPassword = Account != null && Account.HasStoragePassword,
                    LastSetTime = Account?.StoragePasswordLastSet ?? DateTime.MinValue
                });

                return;
            }

            if (!CanAccessStorageNpc())
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 0,
                    Removing = false,
                    HasPassword = Account.HasStoragePassword,
                    LastSetTime = Account.StoragePasswordLastSet
                });

                return;
            }

            if (!Envir.IsPasswordValid(p.NewPassword))
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 3,
                    Removing = false,
                    HasPassword = Account.HasStoragePassword,
                    LastSetTime = Account.StoragePasswordLastSet
                });

                return;
            }

            if (Account.HasStoragePassword)
            {
                if (!Envir.IsPasswordValid(p.CurrentPassword))
                {
                    Enqueue(new S.StoragePasswordResult
                    {
                        Result = 1,
                        Removing = false,
                        HasPassword = true,
                        LastSetTime = Account.StoragePasswordLastSet
                    });

                    return;
                }

                if (!Account.ValidateStoragePassword(p.CurrentPassword))
                {
                    Enqueue(new S.StoragePasswordResult
                    {
                        Result = 2,
                        Removing = false,
                        HasPassword = true,
                        LastSetTime = Account.StoragePasswordLastSet
                    });

                    return;
                }
            }

            Account.StoragePassword = p.NewPassword;
            Account.StoragePasswordLastSet = Envir.Now;

            Player.SetStorageUnlocked(true);

            Enqueue(new S.StoragePasswordResult
            {
                Result = 4,
                Removing = false,
                HasPassword = true,
                LastSetTime = Account.StoragePasswordLastSet
            });
        }

        private void RemoveStoragePassword(C.RemoveStoragePassword p)
        {
            if (Stage != GameStage.Game || Player == null || Account == null)
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 0,
                    Removing = true,
                    HasPassword = Account != null && Account.HasStoragePassword,
                    LastSetTime = Account?.StoragePasswordLastSet ?? DateTime.MinValue
                });

                return;
            }

            if (!CanAccessStorageNpc())
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 0,
                    Removing = true,
                    HasPassword = Account.HasStoragePassword,
                    LastSetTime = Account.StoragePasswordLastSet
                });

                return;
            }

            if (!Account.HasStoragePassword)
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 5,
                    Removing = true,
                    HasPassword = false,
                    LastSetTime = DateTime.MinValue
                });

                return;
            }

            if (!Envir.IsPasswordValid(p.CurrentPassword))
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 1,
                    Removing = true,
                    HasPassword = true,
                    LastSetTime = Account.StoragePasswordLastSet
                });

                return;
            }

            if (!Account.ValidateStoragePassword(p.CurrentPassword))
            {
                Enqueue(new S.StoragePasswordResult
                {
                    Result = 2,
                    Removing = true,
                    HasPassword = true,
                    LastSetTime = Account.StoragePasswordLastSet
                });

                return;
            }

            Account.ClearStoragePassword();
            Player.SetStorageUnlocked(true);

            Enqueue(new S.StoragePasswordResult
            {
                Result = 4,
                Removing = true,
                HasPassword = false,
                LastSetTime = DateTime.MinValue
            });
        }

        private bool CanAccessStorageNpc()
        {
            if (Player == null)
                return false;

            if (Player.NPCPage == null ||
                !String.Equals(
                    Player.NPCPage.Key,
                    NPCScript.StorageKey,
                    StringComparison.CurrentCultureIgnoreCase))
                return false;

            NPCObject ob = null;

            for (int i = 0; i < Player.CurrentMap.NPCs.Count; i++)
            {
                if (Player.CurrentMap.NPCs[i].ObjectID != Player.NPCObjectID)
                    continue;

                ob = Player.CurrentMap.NPCs[i];
                break;
            }

            return ob != null &&
                   Functions.InRange(
                       ob.CurrentLocation,
                       Player.CurrentLocation,
                       Globals.DataRange);
        }

        private void Login(C.Login p)
        {
            if (Stage != GameStage.Login)
                return;

            MessageQueue.Enqueue(
                GameLanguage.ServerTextMap.GetLocalization(
                    (ServerTextKeys.UserLoggingIn),
                    SessionID,
                    IPAddress));

            Envir.Login(p, this);
        }

        private void NewCharacter(C.NewCharacter p)
        {
            if (Stage != GameStage.Select)
                return;

            Envir.NewCharacter(p, this, Account.AdminAccount);
        }

        private void DeleteCharacter(C.DeleteCharacter p)
        {
            if (Stage != GameStage.Select)
                return;

            if (!Settings.AllowDeleteCharacter)
            {
                Enqueue(new S.DeleteCharacter
                {
                    Result = 0
                });

                return;
            }

            CharacterInfo temp = null;

            for (int i = 0; i < Account.Characters.Count; i++)
            {
                if (Account.Characters[i].Index != p.CharacterIndex)
                    continue;

                temp = Account.Characters[i];
                break;
            }

            if (temp == null)
            {
                Enqueue(new S.DeleteCharacter
                {
                    Result = 1
                });

                return;
            }

            temp.Deleted = true;
            temp.DeleteDate = Envir.Now;
            Envir.RemoveRank(temp);

            Enqueue(new S.DeleteCharacterSuccess
            {
                CharacterIndex = temp.Index
            });
        }

        private void StartGame(C.StartGame p)
        {
            if (Stage != GameStage.Select)
                return;

            if (!Settings.AllowStartGame &&
                (Account == null || (Account != null && !Account.AdminAccount)))
            {
                Enqueue(new S.StartGame
                {
                    Result = 0
                });

                return;
            }

            if (Account == null)
            {
                Enqueue(new S.StartGame
                {
                    Result = 1
                });

                return;
            }

            CharacterInfo info = null;

            for (int i = 0; i < Account.Characters.Count; i++)
            {
                if (Account.Characters[i].Index != p.CharacterIndex)
                    continue;

                info = Account.Characters[i];
                break;
            }

            if (info == null)
            {
                Enqueue(new S.StartGame
                {
                    Result = 2
                });

                return;
            }

            if (info.Banned)
            {
                if (info.ExpiryDate > Envir.Now)
                {
                    Enqueue(new S.StartGameBanned
                    {
                        Reason = info.BanReason,
                        ExpiryDate = info.ExpiryDate
                    });

                    return;
                }

                info.Banned = false;
            }

            info.BanReason = string.Empty;
            info.ExpiryDate = DateTime.MinValue;

            Player = new PlayerObject(info, this);
            Player.StartGame();
        }

        public void LogOut()
        {
            if (Stage == GameStage.Game)
            {
                if (Envir.Time < Player.LogTime)
                {
                    Enqueue(new S.LogOutFailed());
                    return;
                }

                Player.StopGame(23);

                Stage = GameStage.Select;
                Player = null;

                Enqueue(new S.LogOutSuccess
                {
                    Characters = Account.GetSelectInfo()
                });
            }
            else if (Stage == GameStage.Observer)
            {
                if (Observing != null)
                    Observing.Observers.Remove(this);

                Observing = null;
                Stage = GameStage.Select;

                Enqueue(new S.LogOutSuccess
                {
                    Characters = Account.GetSelectInfo()
                });
            }
        }

        private void Turn(C.Turn p)
        {
            if (Stage != GameStage.Game)
                return;

            if (Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Turn(p.Direction);
        }

        private void Walk(C.Walk p)
        {
            if (Stage != GameStage.Game)
                return;

            if (Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Walk(p.Direction);
        }

        private void Run(C.Run p)
        {
            if (Stage != GameStage.Game)
                return;

            if (Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Run(p.Direction);
        }

        private void Chat(C.Chat p)
        {
            if (p.Message.Length > Globals.MaxChatLength)
            {
                SendDisconnect(2);
                return;
            }

            if (Stage == GameStage.Game)
            {
                Player.Chat(p.Message, p.LinkedItems);
            }
            else if (Stage == GameStage.Observer)
            {
                if (!p.Message.StartsWith("@"))
                    return;

                string message = p.Message.Remove(0, 1);
                string[] parts = message.Split(
                    new[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0)
                    return;

                if (string.Equals(
                    parts[0],
                    "OBSERVE",
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 2)
                        return;

                    PlayerObject player = Envir.GetPlayer(parts[1]);

                    if (player == null)
                        return;

                    if ((!player.AllowObserve || !Settings.AllowObserve) &&
                        (Account == null || !Account.AdminAccount))
                        return;

                    player.AddObserver(this);
                }
            }
        }

        private void MoveItem(C.MoveItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MoveItem(p.Grid, p.From, p.To);
        }

        private void StoreItem(C.StoreItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.StoreItem(p.From, p.To);
        }

        private void DepositRefineItem(C.DepositRefineItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DepositRefineItem(p.From, p.To);
        }

        private void RetrieveRefineItem(C.RetrieveRefineItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RetrieveRefineItem(p.From, p.To);
        }

        private void RefineCancel(C.RefineCancel p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RefineCancel();
        }

        private void RefineItem(C.RefineItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RefineItem(p.UniqueID);
        }

        private void CheckRefine(C.CheckRefine p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.CheckRefine(p.UniqueID);
        }

        private void ReplaceWedRing(C.ReplaceWedRing p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.ReplaceWeddingRing(p.UniqueID);
        }

        private void DepositTradeItem(C.DepositTradeItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DepositTradeItem(p.From, p.To);
        }

        private void RetrieveTradeItem(C.RetrieveTradeItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RetrieveTradeItem(p.From, p.To);
        }

        private void TakeBackItem(C.TakeBackItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TakeBackItem(p.From, p.To);
        }

        private void MergeItem(C.MergeItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MergeItem(p.GridFrom, p.GridTo, p.IDFrom, p.IDTo);
        }

        private void EquipItem(C.EquipItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.EquipItem(p.Grid, p.UniqueID, p.To);
        }

        private void RemoveItem(C.RemoveItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RemoveItem(p.Grid, p.UniqueID, p.To);
        }

        private void RemoveSlotItem(C.RemoveSlotItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RemoveSlotItem(
                p.Grid,
                p.UniqueID,
                p.To,
                p.GridTo,
                p.FromUniqueID);
        }

        private void SplitItem(C.SplitItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SplitItem(p.Grid, p.UniqueID, p.Count);
        }

        private void UseItem(C.UseItem p)
        {
            if (Stage != GameStage.Game)
                return;

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    Player.UseItem(p.UniqueID);
                    break;

                case MirGridType.HeroInventory:
                    Player.HeroUseItem(p.UniqueID);
                    break;
            }
        }

        private void DropItem(C.DropItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DropItem(p.UniqueID, p.Count, p.HeroInventory);
        }

        private void TakeBackHeroItem(C.TakeBackHeroItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TakeBackHeroItem(p.From, p.To);
        }

        private void TransferHeroItem(C.TransferHeroItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TransferHeroItem(p.From, p.To);
        }

        private void DropGold(C.DropGold p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DropGold(p.Amount);
        }

        private void PickUp()
        {
            if (Stage != GameStage.Game)
                return;

            Player.PickUp();
        }

        private void RequestMapInfo(C.RequestMapInfo p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RequestMapInfo(p.MapIndex);
        }

        private void RequestMonsterInfo(C.RequestMonsterInfo p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RequestMonsterInfo(p.MonsterIndex);
        }

        private void RequestNPCInfo(C.RequestNPCInfo p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RequestNPCInfo(p.NPCIndex);
        }

        private void RequestItemInfo(C.RequestItemInfo p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RequestItemInfo(p.ItemIndex);
        }

        private void TeleportToNPC(C.TeleportToNPC p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TeleportToNPC(p.ObjectID);
        }

        private void SearchMap(C.SearchMap p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SearchMap(p.Text);
        }

        private void Inspect(C.Inspect p)
        {
            if (Stage != GameStage.Game && Stage != GameStage.Observer)
                return;

            if (p.Ranking)
            {
                Envir.Inspect(this, (int)p.ObjectID);
            }
            else if (p.Hero)
            {
                Envir.InspectHero(this, (int)p.ObjectID);
            }
            else
            {
                Envir.Inspect(this, p.ObjectID);
            }
        }

        private void Observe(C.Observe p)
        {
            if (Stage != GameStage.Game && Stage != GameStage.Observer)
                return;

            Envir.Observe(this, p.Name);
        }

        private void ChangeAMode(C.ChangeAMode p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AMode = p.Mode;

            Enqueue(new S.ChangeAMode
            {
                Mode = Player.AMode
            });
        }

        private void ChangePMode(C.ChangePMode p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.PMode = p.Mode;

            Enqueue(new S.ChangePMode
            {
                Mode = Player.PMode
            });
        }

        private void ChangeTrade(C.ChangeTrade p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AllowTrade = p.AllowTrade;
        }

        private void Attack(C.Attack p)
        {
            if (Stage != GameStage.Game)
                return;

            if (!Player.Dead &&
                (Player.ActionTime > Envir.Time ||
                 Player.AttackTime > Envir.Time))
                _retryList.Enqueue(p);
            else
                Player.Attack(p.Direction, p.Spell);
        }

        private void RangeAttack(C.RangeAttack p)
        {
            if (Stage != GameStage.Game)
                return;

            if (!Player.Dead &&
                (Player.ActionTime > Envir.Time ||
                 Player.AttackTime > Envir.Time))
                _retryList.Enqueue(p);
            else
                Player.RangeAttack(
                    p.Direction,
                    p.TargetLocation,
                    p.TargetID);
        }

        private void Harvest(C.Harvest p)
        {
            if (Stage != GameStage.Game)
                return;

            if (!Player.Dead && Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Harvest(p.Direction);
        }

        private void CallNPC(C.CallNPC p)
        {
            if (Stage != GameStage.Game)
                return;

            if (p.Key.Length > 30)
            {
                SendDisconnect(2);
                return;
            }

            if (p.ObjectID == Envir.DefaultNPC.LoadedObjectID &&
                Player.NPCObjectID == Envir.DefaultNPC.LoadedObjectID)
            {
                Player.CallDefaultNPC(p.Key);
                return;
            }

            if (p.ObjectID == uint.MaxValue)
            {
                Player.CallDefaultNPC(DefaultNPCType.Client, null);
                return;
            }

            Player.CallNPC(p.ObjectID, p.Key);
        }

        private void BuyItem(C.BuyItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.BuyItem(p.ItemIndex, p.Count, p.Type);
        }

        private void CraftItem(C.CraftItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.CraftItem(p.UniqueID, p.Count, p.Slots);
        }

        private void SellItem(C.SellItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SellItem(p.UniqueID, p.Count);
        }

        private void RepairItem(C.RepairItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RepairItem(p.UniqueID);
        }

        private void BuyItemBack(C.BuyItemBack p)
        {
            if (Stage != GameStage.Game)
                return;

            // Player.BuyItemBack(p.UniqueID, p.Count);
        }

        private void SRepairItem(C.SRepairItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RepairItem(p.UniqueID, true);
        }

        private void MagicKey(C.MagicKey p)
        {
            if (Stage != GameStage.Game)
                return;

            HumanObject actor = Player;

            if (p.Key > 16 || p.OldKey > 16)
            {
                if (!Player.HeroSpawned || Player.Hero.Dead)
                    return;

                actor = Player.Hero;
            }

            for (int i = 0; i < actor.Info.Magics.Count; i++)
            {
                UserMagic magic = actor.Info.Magics[i];

                if (magic.Spell != p.Spell)
                {
                    if (magic.Key == p.Key)
                        magic.Key = 0;

                    continue;
                }

                magic.Key = p.Key;
            }
        }

        private void Magic(C.Magic p)
        {
            if (Stage != GameStage.Game)
                return;

            HumanObject actor = Player;

            if (Player.HeroSpawned &&
                p.ObjectID == Player.Hero.ObjectID)
                actor = Player.Hero;

            if (actor.Dead)
                return;

            if (!actor.Dead &&
                (actor.ActionTime > Envir.Time ||
                 actor.SpellTime > Envir.Time))
                _retryList.Enqueue(p);
            else
                actor.BeginMagic(
                    p.Spell,
                    p.Direction,
                    p.TargetID,
                    p.Location,
                    p.SpellTargetLock);
        }

        private void SwitchGroup(C.SwitchGroup p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SwitchGroup(p.AllowGroup);
        }

        private void AddMember(C.AddMember p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AddMember(p.Name);
        }

        private void DelMember(C.DelMember p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DelMember(p.Name);
        }

        private void GroupInvite(C.GroupInvite p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GroupInvite(p.AcceptInvite);
        }

        private void NewHero(C.NewHero p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.NewHero(p);
        }

        private void SetAutoPotValue(C.SetAutoPotValue p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SetAutoPotValue(p.Stat, p.Value);
        }

        private void SetAutoPotItem(C.SetAutoPotItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SetAutoPotItem(p.Grid, p.ItemIndex);
        }

        private void SetHeroBehaviour(C.SetHeroBehaviour p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SetHeroBehaviour(p.Behaviour);
        }

        private void ChangeHero(C.ChangeHero p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.ChangeHero(p.ListIndex);
        }

        private void TownRevive()
        {
            if (Stage != GameStage.Game)
                return;

            Player.TownRevive();
        }

        private void SpellToggle(C.SpellToggle p)
        {
            if (Stage != GameStage.Game)
                return;

            if (p.canUse > SpellToggleState.None)
            {
                Player.SpellToggle(p.Spell, p.canUse);
                return;
            }

            if (Player.HeroSpawned)
                Player.Hero.SpellToggle(p.Spell, p.canUse);
        }

        private void ConsignItem(C.ConsignItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.ConsignItem(p.UniqueID, p.Price, p.Type);
        }

        private void GuildTerritoryPage(C.GuildTerritoryPage p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GetGuildTerritories(p.Page);
        }

        private void PurchaseGuildTerritory(C.PurchaseGuildTerritory p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.PurchaseGuildTerritory(p.Owner);
        }

        private void MarketSearch(C.MarketSearch p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.UserMatch = p.Usermode;
            Player.MinShapes = p.MinShape;
            Player.MaxShapes = p.MaxShape;
            Player.MarketPanelType = p.MarketType;

            Player.MarketSearch(p.Match, p.Type);
        }

        private void MarketRefresh()
        {
            if (Stage != GameStage.Game)
                return;

            Player.MarketSearch(string.Empty, Player.MatchType);
        }

        private void MarketPage(C.MarketPage p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MarketPage(p.Page);
        }

        private void MarketBuy(C.MarketBuy p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MarketBuy(p.AuctionID, p.BidPrice);
        }

        private void MarketSellNow(C.MarketSellNow p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MarketSellNow(p.AuctionID);
        }

        private void MarketGetBack(C.MarketGetBack p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MarketGetBack(p.Mode, p.AuctionID);
        }

        private void RequestUserName(C.RequestUserName p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RequestUserName(p.UserID);
        }

        private void RequestChatItem(C.RequestChatItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RequestChatItem(p.ChatItemID);
        }

        private void EditGuildMember(C.EditGuildMember p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.EditGuildMember(
                p.Name,
                p.RankName,
                p.RankIndex,
                p.ChangeType);
        }

        private void EditGuildNotice(C.EditGuildNotice p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.EditGuildNotice(p.notice);
        }

        private void GuildInvite(C.GuildInvite p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GuildInvite(p.AcceptInvite);
        }

        private void RequestGuildInfo(C.RequestGuildInfo p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RequestGuildInfo(p.Type);
        }

        private void GuildNameReturn(C.GuildNameReturn p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GuildNameReturn(p.Name);
        }

        private void GuildStorageGoldChange(C.GuildStorageGoldChange p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GuildStorageGoldChange(p.Type, p.Amount);
        }

        private void GuildStorageItemChange(C.GuildStorageItemChange p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GuildStorageItemChange(
                p.Type,
                p.From,
                p.To);
        }

        private void GuildWarReturn(C.GuildWarReturn p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GuildWarReturn(p.Name);
        }

        private void MarriageRequest(C.MarriageRequest p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MarriageRequest();
        }

        private void MarriageReply(C.MarriageReply p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MarriageReply(p.AcceptInvite);
        }

        private void ChangeMarriage(C.ChangeMarriage p)
        {
            if (Stage != GameStage.Game)
                return;

            if (Player.Info.Married == 0)
            {
                Player.AllowMarriage = !Player.AllowMarriage;

                if (Player.AllowMarriage)
                    Player.ReceiveChat(
                        GameLanguage.ServerTextMap.GetLocalization(
                            ServerTextKeys.YouAllowMarriageRequests),
                        ChatType.Hint);
                else
                    Player.ReceiveChat(
                        GameLanguage.ServerTextMap.GetLocalization(
                            ServerTextKeys.YouBlockMarriageRequests),
                        ChatType.Hint);
            }
            else
            {
                Player.AllowLoverRecall = !Player.AllowLoverRecall;

                if (Player.AllowLoverRecall)
                    Player.ReceiveChat(
                        GameLanguage.ServerTextMap.GetLocalization(
                            ServerTextKeys.YouAllowRecallFromLover),
                        ChatType.Hint);
                else
                    Player.ReceiveChat(
                        GameLanguage.ServerTextMap.GetLocalization(
                            ServerTextKeys.YouBlockRecallFromLover),
                        ChatType.Hint);
            }
        }

        private void DivorceRequest(C.DivorceRequest p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DivorceRequest();
        }

        private void DivorceReply(C.DivorceReply p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DivorceReply(p.AcceptInvite);
        }

        private void AddMentor(C.AddMentor p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AddMentor(p.Name);
        }

        private void MentorReply(C.MentorReply p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MentorReply(p.AcceptInvite);
        }

        private void AllowMentor(C.AllowMentor p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AllowMentor = !Player.AllowMentor;

            if (Player.AllowMentor)
                Player.ReceiveChat(
                    GameLanguage.ServerTextMap.GetLocalization(
                        ServerTextKeys.AllowingMentorRequests),
                    ChatType.Hint);
            else
                Player.ReceiveChat(
                    GameLanguage.ServerTextMap.GetLocalization(
                        ServerTextKeys.BlockingMentorRequests),
                    ChatType.Hint);
        }

        private void CancelMentor(C.CancelMentor p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.MentorBreak(true);
        }

        private void TradeRequest(C.TradeRequest p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TradeRequest();
        }

        private void TradeGold(C.TradeGold p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TradeGold(p.Amount);
        }

        private void TradeReply(C.TradeReply p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TradeReply(p.AcceptInvite);
        }

        private void TradeConfirm(C.TradeConfirm p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TradeConfirm(p.Locked);
        }

        private void TradeCancel(C.TradeCancel p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.TradeCancel();
        }

        private void EquipSlotItem(C.EquipSlotItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.EquipSlotItem(
                p.Grid,
                p.UniqueID,
                p.To,
                p.GridTo,
                p.ToUniqueID);
        }

        private void FishingCast(C.FishingCast p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.FishingCast(p.CastOut, true);
        }

        private void FishingChangeAutocast(C.FishingChangeAutocast p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.FishingChangeAutocast(p.AutoCast);
        }

        private void AcceptQuest(C.AcceptQuest p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AcceptQuest(p.QuestIndex);
        }

        private void FinishQuest(C.FinishQuest p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.FinishQuest(
                p.QuestIndex,
                p.SelectedItemIndex);
        }

        private void AbandonQuest(C.AbandonQuest p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AbandonQuest(p.QuestIndex);
        }

        private void ShareQuest(C.ShareQuest p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.ShareQuest(p.QuestIndex);
        }

        private void AcceptReincarnation()
        {
            if (Stage != GameStage.Game)
                return;

            if (Player.ReincarnationHost != null &&
                Player.ReincarnationHost.ReincarnationReady)
            {
                Player.Revive(
                    Player.Stats[Stat.HP] / 2,
                    true);

                Player.ReincarnationHost = null;
                return;
            }

            Player.ReceiveChat(
                GameLanguage.ServerTextMap.GetLocalization(
                    ServerTextKeys.ReincarnationFailed),
                ChatType.System);
        }

        private void CancelReincarnation()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ReincarnationExpireTime = Envir.Time;
        }

        private void CombineItem(C.CombineItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.CombineItem(
                p.Grid,
                p.IDFrom,
                p.IDTo);
        }

        private void Awakening(C.Awakening p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.Awakening(
                p.UniqueID,
                p.Type);
        }

        private void AwakeningNeedMaterials(C.AwakeningNeedMaterials p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AwakeningNeedMaterials(
                p.UniqueID,
                p.Type);
        }

        private void DisassembleItem(C.DisassembleItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DisassembleItem(p.UniqueID);
        }

        private void DowngradeAwakening(C.DowngradeAwakening p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DowngradeAwakening(p.UniqueID);
        }

        private void ResetAddedItem(C.ResetAddedItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.ResetAddedItem(p.UniqueID);
        }

        public void SendMail(C.SendMail p)
        {
            if (Stage != GameStage.Game)
                return;

            if (p.Gold > 0 || p.ItemsIdx.Length > 0)
                Player.SendMail(
                    p.Name,
                    p.Message,
                    p.Gold,
                    p.ItemsIdx,
                    p.Stamped);
            else
                Player.SendMail(
                    p.Name,
                    p.Message);
        }

        public void ReadMail(C.ReadMail p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.ReadMail(p.MailID);
        }

        public void CollectParcel(C.CollectParcel p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.CollectMail(p.MailID);
        }

        public void DeleteMail(C.DeleteMail p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DeleteMail(p.MailID);
        }

        public void LockMail(C.LockMail p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.LockMail(p.MailID, p.Lock);
        }

        public void MailCost(C.MailCost p)
        {
            if (Stage != GameStage.Game)
                return;

            uint cost = Player.GetMailCost(
                p.ItemsIdx,
                p.Gold,
                p.Stamped);

            Enqueue(new S.MailCost
            {
                Cost = cost
            });
        }

        private void RequestIntelligentCreatureUpdates(C.RequestIntelligentCreatureUpdates p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SendIntelligentCreatureUpdates = p.Update;
        }

        private void UpdateIntelligentCreature(C.UpdateIntelligentCreature p)
        {
            if (Stage != GameStage.Game)
                return;

            ClientIntelligentCreature petUpdate = p.Creature;

            if (petUpdate == null)
                return;

            if (p.ReleaseMe)
            {
                Player.ReleaseIntelligentCreature(petUpdate.PetType);
                return;
            }

            if (p.SummonMe)
            {
                Player.SummonIntelligentCreature(petUpdate.PetType);
                return;
            }

            if (p.UnSummonMe)
            {
                Player.UnSummonIntelligentCreature(petUpdate.PetType);
                return;
            }

            for (int i = 0; i < Player.Info.IntelligentCreatures.Count; i++)
            {
                if (Player.Info.IntelligentCreatures[i].PetType == petUpdate.PetType)
                {
                    var reg = new Regex(
                        @"^[A-Za-z0-9\u4E00-\u9FA5]{" +
                        Globals.MinCharacterNameLength +
                        "," +
                        Globals.MaxCharacterNameLength +
                        "}$");

                    if (reg.IsMatch(petUpdate.CustomName))
                        Player.Info.IntelligentCreatures[i].CustomName = petUpdate.CustomName;

                    Player.Info.IntelligentCreatures[i].SlotIndex = petUpdate.SlotIndex;
                    Player.Info.IntelligentCreatures[i].Filter = petUpdate.Filter;
                    Player.Info.IntelligentCreatures[i].petMode = petUpdate.petMode;
                }
            }

            if (Player.CreatureSummoned &&
                Player.SummonedCreatureType == petUpdate.PetType)
            {
                Player.UpdateSummonedCreature(petUpdate.PetType);
            }
        }

        private void IntelligentCreaturePickup(C.IntelligentCreaturePickup p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.IntelligentCreaturePickup(
                p.MouseMode,
                p.Location);
        }

        private void AddFriend(C.AddFriend p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AddFriend(
                p.Name,
                p.Blocked);
        }

        private void RemoveFriend(C.RemoveFriend p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RemoveFriend(p.CharacterIndex);
        }

        private void AddMemo(C.AddMemo p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.AddMemo(
                p.CharacterIndex,
                p.Memo);
        }

        private void GuildBuffUpdate(C.GuildBuffUpdate p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GuildBuffUpdate(
                p.Action,
                p.Id);
        }

        private void GameshopBuy(C.GameshopBuy p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.GameshopBuy(
                p.GIndex,
                p.Quantity,
                p.PType);
        }

        private void NPCConfirmInput(C.NPCConfirmInput p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.NPCData["NPCInputStr"] = p.Value;

            if (p.NPCID == Envir.DefaultNPC.LoadedObjectID &&
                Player.NPCObjectID == Envir.DefaultNPC.LoadedObjectID)
            {
                Player.CallDefaultNPC(p.PageName);
                return;
            }

            Player.CallNPC(
                Player.NPCObjectID,
                p.PageName);
        }

        public List<byte[]> Image = new List<byte[]>();

        private void ReportIssue(C.ReportIssue p)
        {
            if (Stage != GameStage.Game)
                return;

            return;

            // Image.Add(p.Image);

            // if (p.ImageChunk >= p.ImageSize)
            // {
            //     System.Drawing.Image image = Functions.ByteArrayToImage(Functions.CombineArray(Image));
            //     image.Save("Reported-" + Player.Name + "-" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg");
            //     Image.Clear();
            // }
        }

        private void GetRanking(C.GetRanking p)
        {
            if (Stage != GameStage.Game && Stage != GameStage.Observer)
                return;

            Envir.GetRanking(
                this,
                p.RankType,
                p.RankIndex,
                p.OnlineOnly);
        }

        private void Opendoor(C.Opendoor p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.Opendoor(p.DoorIndex);
        }

        private void GetRentedItems()
        {
            if (Stage != GameStage.Game)
                return;

            Player.GetRentedItems();
        }

        private void ItemRentalRequest()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ItemRentalRequest();
        }

        private void ItemRentalFee(C.ItemRentalFee p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SetItemRentalFee(p.Amount);
        }

        private void ItemRentalPeriod(C.ItemRentalPeriod p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SetItemRentalPeriodLength(p.Days);
        }

        private void DepositRentalItem(C.DepositRentalItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DepositRentalItem(
                p.From,
                p.To);
        }

        private void RetrieveRentalItem(C.RetrieveRentalItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RetrieveRentalItem(
                p.From,
                p.To);
        }

        private void CancelItemRental()
        {
            if (Stage != GameStage.Game)
                return;

            Player.CancelItemRental();
        }

        private void ItemRentalLockFee()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ItemRentalLockFee();
        }

        private void ItemRentalLockItem()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ItemRentalLockItem();
        }

        private void ConfirmItemRental()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ConfirmItemRental();
        }

        public void CheckItemInfo(ItemInfo info, bool dontLoop = false)
        {
            if ((dontLoop == false) &&
                (info.ClassBased | info.LevelBased))
            {
                for (int i = 0; i < Envir.ItemInfoList.Count; i++)
                {
                    if ((Envir.ItemInfoList[i] != info) &&
                        Envir.ItemInfoList[i].Name.StartsWith(info.Name))
                    {
                        CheckItemInfo(
                            Envir.ItemInfoList[i],
                            true);
                    }
                }
            }

            foreach (MirConnection observer in Observers)
                observer.CheckItemInfo(info, dontLoop);

            if (SentItemInfo.Contains(info))
                return;

            Enqueue(new S.NewItemInfo
            {
                Info = info
            });

            SentItemInfo.Add(info);
        }

        public void CheckMonsterInfo(int monsterIndex)
        {
            CheckMonsterInfo(
                Envir.GetMonsterInfo(monsterIndex));
        }

        public void CheckMonsterInfo(MonsterInfo info)
        {
            if (info == null)
                return;

            foreach (MirConnection observer in Observers)
                observer.CheckMonsterInfo(info);

            if (SentMonsterInfo.Contains(info))
                return;

            Enqueue(new S.NewMonsterInfo
            {
                Info = info.ClientInformation
            });

            SentMonsterInfo.Add(info);
        }

        public void CheckNPCInfo(int npcIndex)
        {
            CheckNPCInfo(
                Envir.GetNPCInfo(npcIndex));
        }

        public void CheckNPCInfo(NPCInfo info)
        {
            if (info == null)
                return;

            foreach (MirConnection observer in Observers)
                observer.CheckNPCInfo(info);

            if (SentNPCInfo.Contains(info))
                return;

            Enqueue(new S.NewNPCInfo
            {
                Info = info.ClientInformation
            });

            SentNPCInfo.Add(info);
        }

        public void CheckItem(UserItem item)
        {
            CheckItemInfo(item.Info);

            for (int i = 0; i < item.Slots.Length; i++)
            {
                if (item.Slots[i] == null)
                    continue;

                CheckItemInfo(item.Slots[i].Info);
            }

            CheckHeroInfo(item);
        }

        private void CheckHeroInfo(UserItem item)
        {
            if (item.AddedStats[Stat.Hero] == 0)
                return;

            if (SentHeroInfo.Contains(item.UniqueID))
                return;

            HeroInfo heroInfo = Envir.GetHeroInfo(
                item.AddedStats[Stat.Hero]);

            if (heroInfo == null)
                return;

            Enqueue(new S.NewHeroInfo
            {
                Info = heroInfo.ClientInformation
            });

            SentHeroInfo.Add(item.UniqueID);
        }

        private void DeleteItem(C.DeleteItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DeleteItem(
                p.UniqueID,
                p.Count);
        }
    }

    public class MirConnectionLog
    {
        public string IPAddress = "";
        public List<long> AccountsMade = new List<long>();
        public List<long> CharactersMade = new List<long>();
    }
}
