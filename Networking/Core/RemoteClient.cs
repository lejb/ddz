using System.Net.Sockets;

namespace Networking.Core
{
    public class RemoteClient : NetworkClientBase
    {
        public RemoteClient(TcpClient client)
        {
            tcpClient = client;
            RemoteEndPoint = tcpClient.Client.RemoteEndPoint;
            LocalEndPoint = tcpClient.Client.LocalEndPoint;

            buffer = new byte[m_bufferSize];
            networkStream = tcpClient.GetStream();
            BeginRead();
        }
    }
}
