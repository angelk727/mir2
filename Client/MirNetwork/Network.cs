using System.Collections.Concurrent;
using System.Net.Sockets;
using Client.MirControls;
using C = ClientPackets;

namespace Client.MirNetwork
{
    static class Network
    {
        private enum ConnectionState
        {
            Disconnected,
            Connecting,
            Connected,
            Reconnecting,
            Failed
        }

        private static TcpClient _client;

        public static int ConnectAttempt = 0;
        public static int MaxAttempts = 20;
        public static bool ErrorShown;
        public static bool Connected;

        public static long TimeOutTime;
        public static long TimeConnected;
        public static long RetryTime = CMain.Time + 5000;

        private static ConcurrentQueue<ReceivedPacket> _receiveList;
        private static ConcurrentQueue<Packet> _sendList;

        private static ReceiveState _receiveState;
        private static SendState _sendState;

        private static int _sendPumpRunning;

        private static System.Threading.Timer _keepAliveTimer;
        private static System.Threading.Timer _reconnectTimer;
        private static System.Threading.Timer _networkWatchdogTimer;

        private static long _lastReceiveTime;
        private static long _lastKeepAliveTime;

        private const int KeepAliveInterval = 10000;
        private const int ConnectionTimeout = 30000;
        private const int ReconnectDelay = 5000;

        private static readonly object _connectionLock = new object();

        private static long _connectionGeneration;
        private static int _connectionLostNotification;

        private static ConnectionState _connectionState = ConnectionState.Disconnected;
        private static bool _hasConnected;

        public static void Connect()
        {
            TcpClient oldClient = null;
            TcpClient client = null;
            long generation = 0;
            bool attemptsExceeded = false;

            lock (_connectionLock)
            {
                if (_connectionState == ConnectionState.Connected && Connected && _client != null)
                    return;

                if (_connectionState == ConnectionState.Connecting)
                    return;

                if (_connectionState == ConnectionState.Failed && ConnectAttempt >= MaxAttempts)
                {
                    attemptsExceeded = true;
                }
                else if (ConnectAttempt >= MaxAttempts)
                {
                    attemptsExceeded = true;
                }
                else
                {
                    oldClient = _client;
                    _client = null;
                    Connected = false;
                    TimeConnected = 0;

                    _receiveList = null;
                    _sendList = null;
                    _receiveState = null;
                    _sendState = null;

                    Interlocked.Exchange(ref _sendPumpRunning, 0);

                    StopKeepAliveTimer();
                    StopNetworkWatchdog();

                    ConnectAttempt++;

                    _connectionState = _hasConnected ? ConnectionState.Reconnecting : ConnectionState.Connecting;

                    generation = Interlocked.Increment(ref _connectionGeneration);

                    try
                    {
                        client = new TcpClient { NoDelay = true };
                        _client = client;
                    }
                    catch (Exception ex)
                    {
                        if (Settings.LogErrors)
                            CMain.SaveError(ex.ToString());

                        _client = null;
                        _connectionState = ConnectionState.Reconnecting;
                        client = null;
                    }
                }
            }

            CloseClient(oldClient);

            if (attemptsExceeded)
            {
                HandleConnectionAttemptsExceeded();
                return;
            }

            if (client == null)
            {
                ScheduleReconnect();
                return;
            }

            try
            {
                client.BeginConnect(Settings.IPAddress, Settings.Port, Connection, new ConnectionAttempt(client, generation));
            }
            catch (ObjectDisposedException ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                HandleConnectFailure(client, generation);
            }
            catch (SocketException ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                HandleConnectFailure(client, generation);
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                HandleConnectFailure(client, generation);
            }
        }

        private static void Connection(IAsyncResult result)
        {
            ConnectionAttempt attempt = result.AsyncState as ConnectionAttempt;

            if (attempt == null)
                return;

            TcpClient client = attempt.Client;
            long generation = attempt.Generation;

            if (client == null)
                return;

            if (!IsCurrentConnection(client, generation))
            {
                CloseClient(client);
                return;
            }

            try
            {
                client.EndConnect(result);

                if (!client.Connected)
                {
                    HandleConnectFailure(client, generation);
                    return;
                }

                lock (_connectionLock)
                {
                    if (!IsCurrentConnection(client, generation))
                    {
                        CloseClient(client);
                        return;
                    }

                    _receiveList = new ConcurrentQueue<ReceivedPacket>();
                    _sendList = new ConcurrentQueue<Packet>();

                    _receiveState = new ReceiveState(client, generation, _receiveList);
                    _sendState = null;

                    Interlocked.Exchange(ref _sendPumpRunning, 0);

                    Connected = true;
                    _connectionState = ConnectionState.Connected;
                    _lastReceiveTime = CMain.Time;
                    _lastKeepAliveTime = CMain.Time;
                    TimeOutTime = CMain.Time + ConnectionTimeout;
                    TimeConnected = CMain.Time;
                    RetryTime = CMain.Time + ReconnectDelay;

                    ConnectAttempt = 0;
                    ErrorShown = false;
                    _hasConnected = true;

                    Interlocked.Exchange(ref _connectionLostNotification, 0);
                }

                StopReconnectTimer();
                StartKeepAliveTimer();
                StartNetworkWatchdog();
                BeginReceive(_receiveState);
            }
            catch (SocketException ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                HandleConnectFailure(client, generation);
            }
            catch (ObjectDisposedException ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                HandleConnectFailure(client, generation);
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                HandleConnectFailure(client, generation);
            }
        }

        private static bool IsCurrentConnection(TcpClient client, long generation)
        {
            if (client == null)
                return false;

            if (!ReferenceEquals(_client, client))
                return false;

            if (generation != Interlocked.Read(ref _connectionGeneration))
                return false;

            return true;
        }

        private static void HandleConnectFailure(TcpClient client, long generation)
        {
            bool retry = false;
            bool finalFailure = false;

            lock (_connectionLock)
            {
                if (!IsCurrentConnection(client, generation))
                {
                    CloseClient(client);
                    return;
                }

                _client = null;
                Connected = false;
                TimeConnected = 0;

                StopKeepAliveTimer();
                StopNetworkWatchdog();

                _receiveList = null;
                _sendList = null;
                _receiveState = null;
                _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);
                Interlocked.Increment(ref _connectionGeneration);

                if (ConnectAttempt >= MaxAttempts)
                {
                    _connectionState = ConnectionState.Failed;
                    finalFailure = true;
                }
                else
                {
                    _connectionState = ConnectionState.Reconnecting;
                    retry = true;
                }
            }

            CloseClient(client);

            if (finalFailure)
            {
                HandleConnectionAttemptsExceeded();
                return;
            }

            if (retry)
                ScheduleReconnect();
        }

        private static void HandleConnectionAttemptsExceeded()
        {
            bool showInitialConnectionError = false;
            bool notifyLostConnection = false;

            StopReconnectTimer();

            lock (_connectionLock)
            {
                if (_connectionState == ConnectionState.Failed && !Connected)
                {
                    if (_hasConnected)
                    {
                        notifyLostConnection = true;
                    }
                    else if (!ErrorShown)
                    {
                        ErrorShown = true;
                        showInitialConnectionError = true;
                    }
                }
            }

            if (notifyLostConnection)
            {
                Interlocked.Exchange(ref _connectionLostNotification, 1);
            }

            if (!showInitialConnectionError)
                return;

            MirMessageBox errorBox = new MirMessageBox(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.ErrorConnectingToServer), MirMessageBoxButtons.Cancel);
            errorBox.CancelButton.Click += (o, e) => Program.Form.Close();
            errorBox.Label.Text = GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.MaximumConnectionAttemptsReached, MaxAttempts);
            errorBox.Show();
        }

        private static void BeginReceive(ReceiveState state)
        {
            if (state == null)
                return;

            TcpClient client = state.Client;

            if (client == null)
                return;

            if (!IsCurrentConnection(client, state.Generation))
                return;

            int free = state.ReceiveBuffer.Length - state.ReceiveCount;

            if (free <= 0)
            {
                Disconnect(client, state.Generation);
                return;
            }

            try
            {
                client.Client.BeginReceive(state.ReceiveBuffer, state.ReceiveCount, free, SocketFlags.None, ReceiveData, state);
            }
            catch (ObjectDisposedException)
            {
                Disconnect(client, state.Generation);
            }
            catch (SocketException)
            {
                Disconnect(client, state.Generation);
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                Disconnect(client, state.Generation);
            }
        }

        private static void ReceiveData(IAsyncResult result)
        {
            ReceiveState state = result.AsyncState as ReceiveState;

            if (state == null)
                return;

            TcpClient client = state.Client;

            if (client == null)
                return;

            if (!IsCurrentConnection(client, state.Generation))
            {
                CloseClient(client);
                return;
            }

            int dataRead;

            try
            {
                dataRead = client.Client.EndReceive(result);
            }
            catch (ObjectDisposedException)
            {
                Disconnect(client, state.Generation);
                return;
            }
            catch (SocketException)
            {
                Disconnect(client, state.Generation);
                return;
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                Disconnect(client, state.Generation);
                return;
            }

            if (dataRead == 0)
            {
                Disconnect(client, state.Generation);
                return;
            }

            try
            {
                CMain.BytesReceived += dataRead;
                state.ReceiveCount += dataRead;

                int offset = 0;

                while (offset < state.ReceiveCount)
                {
                    Packet p = Packet.ReceivePacket(state.ReceiveBuffer, offset, state.ReceiveCount - offset, out int consumed);

                    if (p == null)
                        break;

                    if (consumed <= 0)
                        throw new InvalidDataException("数据包解析长度无效");

                    offset += consumed;

                    ConcurrentQueue<ReceivedPacket> queue = state.ReceiveQueue;

                    if (queue != null)
                        queue.Enqueue(new ReceivedPacket(p, state.Generation));
                }

                if (offset > 0)
                {
                    _lastReceiveTime = CMain.Time;
                    TimeOutTime = _lastReceiveTime + ConnectionTimeout;

                    int remaining = state.ReceiveCount - offset;

                    if (remaining > 0)
                        Buffer.BlockCopy(state.ReceiveBuffer, offset, state.ReceiveBuffer, 0, remaining);

                    state.ReceiveCount = remaining;
                }
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                Disconnect(client, state.Generation);
                return;
            }

            BeginReceive(state);
        }

        private sealed class ConnectionAttempt
        {
            public TcpClient Client { get; }
            public long Generation { get; }

            public ConnectionAttempt(TcpClient client, long generation)
            {
                Client = client;
                Generation = generation;
            }
        }

        private sealed class ReceivedPacket
        {
            public Packet Packet { get; }
            public long Generation { get; }

            public ReceivedPacket(Packet packet, long generation)
            {
                Packet = packet;
                Generation = generation;
            }
        }

        private sealed class ReceiveState
        {
            public TcpClient Client { get; }
            public long Generation { get; }
            public ConcurrentQueue<ReceivedPacket> ReceiveQueue { get; }
            public byte[] ReceiveBuffer { get; }
            public int ReceiveCount { get; set; }

            public ReceiveState(TcpClient client, long generation, ConcurrentQueue<ReceivedPacket> receiveQueue)
            {
                Client = client;
                Generation = generation;
                ReceiveQueue = receiveQueue;
                ReceiveBuffer = new byte[ushort.MaxValue];
                ReceiveCount = 0;
            }
        }

        private static void BeginSend(List<byte> data)
        {
            if (data == null || data.Count == 0)
                return;

            TcpClient client = _client;

            if (client == null || !Connected)
                return;

            long generation = Interlocked.Read(ref _connectionGeneration);

            if (!IsCurrentConnection(client, generation))
                return;

            if (_sendState != null)
                return;

            byte[] bytes = data.ToArray();
            SendState state = new SendState(client, generation, bytes);

            _sendState = state;

            try
            {
                client.Client.BeginSend(state.Data, state.Offset, state.Data.Length - state.Offset, SocketFlags.None, SendData, state);
            }
            catch (ObjectDisposedException)
            {
                if (ReferenceEquals(_sendState, state))
                    _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                Disconnect(client, generation);
            }
            catch (SocketException ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                if (ReferenceEquals(_sendState, state))
                    _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                Disconnect(client, generation);
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                if (ReferenceEquals(_sendState, state))
                    _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                Disconnect(client, generation);
            }
        }

        private static void StartSendPump()
        {
            if (!Connected)
                return;

            ConcurrentQueue<Packet> queue = _sendList;

            if (queue == null)
                return;

            if (Interlocked.CompareExchange(ref _sendPumpRunning, 1, 0) != 0)
                return;

            try
            {
                PumpSendQueue();
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                TcpClient client = _client;

                if (client != null)
                {
                    long generation = Interlocked.Read(ref _connectionGeneration);
                    Disconnect(client, generation);
                }
            }
        }

        private static void PumpSendQueue()
        {
            if (!Connected)
            {
                Interlocked.Exchange(ref _sendPumpRunning, 0);
                return;
            }

            if (_sendState != null)
            {
                Interlocked.Exchange(ref _sendPumpRunning, 0);
                return;
            }

            ConcurrentQueue<Packet> queue = _sendList;

            if (queue == null)
            {
                Interlocked.Exchange(ref _sendPumpRunning, 0);
                return;
            }

            if (queue.IsEmpty)
            {
                Interlocked.Exchange(ref _sendPumpRunning, 0);

                if (!queue.IsEmpty && Connected)
                    ThreadPool.QueueUserWorkItem(SendPumpWorkItem);

                return;
            }

            List<byte> data = new List<byte>();

            while (queue.TryDequeue(out Packet p))
            {
                if (p == null)
                    continue;

                data.AddRange(p.GetPacketBytes());
            }

            if (data.Count == 0)
            {
                Interlocked.Exchange(ref _sendPumpRunning, 0);

                if (!queue.IsEmpty && Connected)
                    ThreadPool.QueueUserWorkItem(SendPumpWorkItem);

                return;
            }

            BeginSend(data);
        }

        private static void SendData(IAsyncResult result)
        {
            SendState state = result.AsyncState as SendState;

            if (state == null)
                return;

            TcpClient client = state.Client;

            if (client == null)
                return;

            if (!IsCurrentConnection(client, state.Generation))
                return;

            if (!ReferenceEquals(_sendState, state))
                return;

            try
            {
                int sent = client.Client.EndSend(result);

                if (sent <= 0)
                {
                    Disconnect(client, state.Generation);
                    return;
                }

                state.Offset += sent;

                if (state.Offset < state.Data.Length)
                {
                    if (!IsCurrentConnection(client, state.Generation))
                        return;

                    client.Client.BeginSend(state.Data, state.Offset, state.Data.Length - state.Offset, SocketFlags.None, SendData, state);
                    return;
                }

                CMain.BytesSent += state.Data.Length;

                if (ReferenceEquals(_sendState, state))
                    _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                ConcurrentQueue<Packet> queue = _sendList;

                if (queue != null && !queue.IsEmpty && Connected)
                    ThreadPool.QueueUserWorkItem(SendPumpWorkItem);
            }
            catch (ObjectDisposedException)
            {
                if (ReferenceEquals(_sendState, state))
                    _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                Disconnect(client, state.Generation);
            }
            catch (SocketException ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                if (ReferenceEquals(_sendState, state))
                    _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                Disconnect(client, state.Generation);
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                if (ReferenceEquals(_sendState, state))
                    _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);

                Disconnect(client, state.Generation);
            }
        }

        private sealed class SendState
        {
            public TcpClient Client { get; }
            public long Generation { get; }
            public byte[] Data { get; }
            public int Offset { get; set; }

            public SendState(TcpClient client, long generation, byte[] data)
            {
                Client = client;
                Generation = generation;
                Data = data;
                Offset = 0;
            }
        }

        private static void StartKeepAliveTimer()
        {
            StopKeepAliveTimer();
            _keepAliveTimer = new System.Threading.Timer(KeepAliveTimerCallback, null, KeepAliveInterval, KeepAliveInterval);
        }

        private static void StopKeepAliveTimer()
        {
            System.Threading.Timer timer = Interlocked.Exchange(ref _keepAliveTimer, null);

            if (timer == null)
                return;

            try
            {
                timer.Dispose();
            }
            catch
            {
            }
        }

        private static void StartNetworkWatchdog()
        {
            StopNetworkWatchdog();
            _networkWatchdogTimer = new System.Threading.Timer(NetworkWatchdogCallback, null, 1000, 1000);
        }

        private static void StopNetworkWatchdog()
        {
            System.Threading.Timer timer = Interlocked.Exchange(ref _networkWatchdogTimer, null);

            if (timer == null)
                return;

            try
            {
                timer.Dispose();
            }
            catch
            {
            }
        }

        private static void NetworkWatchdogCallback(object state)
        {
            try
            {
                TcpClient client = _client;

                if (client == null || !Connected)
                    return;

                long now = CMain.Time;
                long lastReceive = Interlocked.Read(ref _lastReceiveTime);

                if (now - lastReceive >= ConnectionTimeout)
                {
                    long generation = Interlocked.Read(ref _connectionGeneration);
                    Disconnect(client, generation);
                }
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());
            }
        }

        private static void KeepAliveTimerCallback(object state)
        {
            try
            {
                TcpClient client = _client;

                if (client == null || !Connected)
                    return;

                long generation = Interlocked.Read(ref _connectionGeneration);

                if (!IsCurrentConnection(client, generation))
                    return;

                ConcurrentQueue<Packet> queue = _sendList;

                if (queue == null)
                    return;

                queue.Enqueue(new C.KeepAlive());

                ThreadPool.QueueUserWorkItem(SendPumpWorkItem);
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());
            }
        }

        private static void ScheduleReconnect()
        {
            lock (_connectionLock)
            {
                if (_connectionState != ConnectionState.Reconnecting)
                    return;

                if (Connected)
                    return;

                if (_reconnectTimer != null)
                    return;

                if (ConnectAttempt >= MaxAttempts)
                {
                    _connectionState = ConnectionState.Failed;
                    Interlocked.Exchange(ref _connectionLostNotification, _hasConnected ? 1 : 0);
                    return;
                }

                RetryTime = CMain.Time + ReconnectDelay;
                _reconnectTimer = new System.Threading.Timer(ReconnectTimerCallback, null, ReconnectDelay, Timeout.Infinite);
            }
        }

        private static void ReconnectTimerCallback(object state)
        {
            System.Threading.Timer timer = Interlocked.Exchange(ref _reconnectTimer, null);

            if (timer != null)
            {
                try
                {
                    timer.Dispose();
                }
                catch
                {
                }
            }

            try
            {
                lock (_connectionLock)
                {
                    if (_connectionState != ConnectionState.Reconnecting)
                        return;

                    if (Connected)
                        return;

                    if (ConnectAttempt >= MaxAttempts)
                    {
                        _connectionState = ConnectionState.Failed;
                        Interlocked.Exchange(ref _connectionLostNotification, _hasConnected ? 1 : 0);
                        return;
                    }
                }

                Connect();
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());

                lock (_connectionLock)
                {
                    if (_connectionState == ConnectionState.Reconnecting && !Connected && ConnectAttempt < MaxAttempts)
                        ScheduleReconnect();
                }
            }
        }

        private static void StopReconnectTimer()
        {
            System.Threading.Timer timer = Interlocked.Exchange(ref _reconnectTimer, null);

            if (timer == null)
                return;

            try
            {
                timer.Dispose();
            }
            catch
            {
            }
        }

        public static void Disconnect()
        {
            TcpClient client;
            bool retry = false;
            bool finalFailure = false;

            lock (_connectionLock)
            {
                if (_connectionState == ConnectionState.Failed)
                    return;

                if (_connectionState == ConnectionState.Disconnected)
                    return;

                if (_connectionState == ConnectionState.Reconnecting && _client == null)
                    return;

                client = _client;

                _client = null;
                Connected = false;
                TimeConnected = 0;

                StopKeepAliveTimer();
                StopNetworkWatchdog();

                _receiveList = null;
                _sendList = null;
                _receiveState = null;
                _sendState = null;

                Interlocked.Exchange(ref _sendPumpRunning, 0);
                Interlocked.Increment(ref _connectionGeneration);

                _connectionState = ConnectionState.Reconnecting;

                if (ConnectAttempt >= MaxAttempts)
                    finalFailure = true;
                else
                    retry = true;
            }

            CloseClient(client);

            if (finalFailure)
            {
                _connectionState = ConnectionState.Failed;
                HandleConnectionAttemptsExceeded();
                return;
            }

            if (retry)
                ScheduleReconnect();
        }

        private static void Disconnect(TcpClient client, long generation)
        {
            lock (_connectionLock)
            {
                if (!IsCurrentConnection(client, generation))
                    return;
            }

            Disconnect();
        }

        private static void CloseClient(TcpClient client)
        {
            if (client == null)
                return;

            try
            {
                if (client.Client != null)
                {
                    try
                    {
                        client.Client.Shutdown(SocketShutdown.Both);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }

            try
            {
                client.Close();
            }
            catch
            {
            }
        }

        public static void Process()
        {
            ConcurrentQueue<ReceivedPacket> queue = _receiveList;

            if (queue != null)
            {
                long currentGeneration = Interlocked.Read(ref _connectionGeneration);

                while (queue.TryDequeue(out ReceivedPacket received))
                {
                    if (received == null)
                        continue;

                    if (received.Generation != currentGeneration)
                        continue;

                    if (!Connected)
                        continue;

                    if (received.Generation != Interlocked.Read(ref _connectionGeneration))
                        continue;

                    Packet p = received.Packet;

                    if (p == null)
                        continue;

                    MirScene.ActiveScene.ProcessPacket(p);
                }
            }

            if (Interlocked.Exchange(ref _connectionLostNotification, 0) != 0)
            {
                bool showLostConnection = false;

                lock (_connectionLock)
                {
                    if (!Connected && _connectionState == ConnectionState.Failed)
                        showLostConnection = true;
                }

                if (showLostConnection)
                    MirMessageBox.Show(GameLanguage.ClientTextMap.GetLocalization(ClientTextKeys.LostConnectionWithServer), true);
            }
        }

        public static void Enqueue(Packet p)
        {
            if (p == null)
                return;

            ConcurrentQueue<Packet> queue;

            lock (_connectionLock)
            {
                if (!Connected || _client == null)
                    return;

                queue = _sendList;

                if (queue == null)
                    return;

                queue.Enqueue(p);
            }

            ThreadPool.QueueUserWorkItem(SendPumpWorkItem);
        }

        private static void SendPumpWorkItem(object state)
        {
            try
            {
                StartSendPump();
            }
            catch (Exception ex)
            {
                if (Settings.LogErrors)
                    CMain.SaveError(ex.ToString());
            }
        }
    }
}