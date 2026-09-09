using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Server.MirEnvir;

namespace Server.MirNetwork
{
    public class MirStatusConnection
    {
        protected static Envir Envir
        {
            get { return Envir.Main; }
        }

        protected static MessageQueue MessageQueue
        {
            get { return MessageQueue.Instance; }
        }

        public readonly string IPAddress;

        private TcpClient _client;

        private long NextSendTime;

        private const long ConnectionTimeout = 30000;
        private const long StatusSendInterval = 10000;
        private const long DisconnectGracePeriod = 500;

        private long _lastActivityTime;
        private long _disconnectDeadline;

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

        private SendState _sendState;

        private bool _disconnecting;

        public bool Connected;

        public bool Disconnecting
        {
            get
            {
                return _disconnecting;
            }
            private set
            {
                if (_disconnecting == value)
                    return;

                _disconnecting = value;

                if (value)
                    _disconnectDeadline = Envir.Time + DisconnectGracePeriod;
                else
                    _disconnectDeadline = 0;
            }
        }

        public readonly long TimeConnected;

        public long TimeOutTime;

        public MirStatusConnection(TcpClient client)
        {
            try
            {
                if (client == null)
                    throw new ArgumentNullException(nameof(client));

                if (client.Client == null)
                    throw new SocketException();

                if (client.Client.RemoteEndPoint != null)
                {
                    string remoteEndPoint = client.Client.RemoteEndPoint.ToString();
                    int separator = remoteEndPoint.LastIndexOf(':');
                    IPAddress = separator > 0 ? remoteEndPoint.Substring(0, separator) : remoteEndPoint;
                }
                else
                {
                    IPAddress = string.Empty;
                }

                _client = client;
                _client.NoDelay = true;

                TimeConnected = Envir.Time;
                _lastActivityTime = TimeConnected;
                TimeOutTime = _lastActivityTime + ConnectionTimeout;
                NextSendTime = TimeConnected;

                Connected = true;
                _disconnecting = false;
                _disconnectDeadline = 0;
            }
            catch (Exception ex)
            {
                MessageQueue.Enqueue(ex);
                Connected = false;
                _disconnecting = true;
                _client = null;
            }
        }

        private void BeginSend(byte[] data)
        {
            if (data == null || data.Length == 0)
                return;

            SendState state;

            lock (_sendLock)
            {
                if (!Connected || Disconnecting || _client == null || _sendState != null)
                    return;

                state = new SendState(_client, data);
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
                if (!Connected || Disconnecting || _client != client || _sendState != state)
                    return;
            }

            int remaining = state.Data.Length - state.Offset;

            if (remaining <= 0)
            {
                CompleteSend(state);
                return;
            }

            try
            {
                client.Client.BeginSend(state.Data, state.Offset, remaining, SocketFlags.None, SendData, state);
            }
            catch
            {
                FailSend(state);
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
                FailSend(state);
                return;
            }

            if (sent <= 0)
            {
                FailSend(state);
                return;
            }

            bool continueSending = false;
            bool completed = false;

            lock (_sendLock)
            {
                if (_sendState != state)
                    return;

                state.Offset += sent;

                if (state.Offset < state.Data.Length)
                {
                    continueSending = true;
                }
                else
                {
                    _sendState = null;
                    completed = true;
                    _lastActivityTime = Envir.Time;
                    TimeOutTime = _lastActivityTime + ConnectionTimeout;
                }
            }

            if (continueSending)
            {
                BeginSend(state);
                return;
            }

            if (completed)
                SendCompleted(state);
        }

        private void CompleteSend(SendState state)
        {
            bool completed = false;

            lock (_sendLock)
            {
                if (_sendState == state)
                {
                    _sendState = null;
                    _lastActivityTime = Envir.Time;
                    TimeOutTime = _lastActivityTime + ConnectionTimeout;
                    completed = true;
                }
            }

            if (completed)
                SendCompleted(state);
        }

        private void FailSend(SendState state)
        {
            bool owned = false;

            lock (_sendLock)
            {
                if (_sendState == state)
                {
                    _sendState = null;
                    owned = true;
                }
            }

            if (owned)
                Disconnect();
        }

        private void SendCompleted(SendState state)
        {
            if (!Connected)
                return;

            if (Disconnecting)
            {
                Disconnect();
                return;
            }
        }

        public void Process()
        {
            try
            {
                if (!Connected)
                    return;

                if (Disconnecting)
                {
                    if (Envir.Time >= _disconnectDeadline)
                        Disconnect();

                    return;
                }

                if (Envir.Time >= TimeOutTime)
                {
                    Disconnect();
                    return;
                }

                if (Envir.Time >= NextSendTime)
                {
                    NextSendTime = Envir.Time + StatusSendInterval;

                    string output = string.Format("c;/NoName/{0}/CrystalM2/{1}//;", Envir.PlayerCount, Assembly.GetCallingAssembly().GetName().Version);

                    BeginSend(Encoding.ASCII.GetBytes(output));
                }
            }
            catch (Exception ex)
            {
                MessageQueue.Enqueue(ex);
                Disconnect();
            }
        }

        public void Disconnect()
        {
            TcpClient client;

            lock (_connectionLock)
            {
                if (!Connected)
                    return;

                Connected = false;
                _disconnecting = true;
                _disconnectDeadline = 0;

                client = _client;
                _client = null;
            }

            lock (_sendLock)
            {
                _sendState = null;
            }

            lock (Envir.StatusConnections)
                Envir.StatusConnections.Remove(this);

            if (client == null)
                return;

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

        public void SendDisconnect()
        {
            bool disconnectNow = false;

            lock (_connectionLock)
            {
                if (!Connected || Disconnecting)
                    return;

                Disconnecting = true;
            }

            lock (_sendLock)
            {
                if (_sendState == null)
                    disconnectNow = true;
            }

            if (disconnectNow)
                Disconnect();
        }
    }
}