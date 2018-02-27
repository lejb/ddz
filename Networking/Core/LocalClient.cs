using System;
using System.Net;
using System.Net.Sockets;

namespace Networking.Core
{
    public class LocalClient : NetworkClientBase
    {
        public event ConnectResultEventHandler ConnectResult;

        public LocalClient()
        {
            buffer = new byte[m_bufferSize];
        }

        public void ConnectTo(IPAddress ip, int port)
        {
            tcpClient = new TcpClient();
            AsyncCallback callBack = new AsyncCallback(ConnectCallBack);
            tcpClient.BeginConnect(ip, port, callBack, null);
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                tcpClient.EndConnect(ar);
            }
            catch (Exception)
            {
                ConnectResult?.Invoke(RemoteEndPoint, false);
                return;
            }

            networkStream?.Dispose();
            networkStream = tcpClient.GetStream();

            RemoteEndPoint = tcpClient.Client.RemoteEndPoint;
            LocalEndPoint = tcpClient.Client.LocalEndPoint;

            ConnectResult?.Invoke(RemoteEndPoint, true);

            Closed = false;

            BeginRead();
        }
    }
}
