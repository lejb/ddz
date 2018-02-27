using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Networking.Core
{
    public delegate void ConnectResultEventHandler(EndPoint endPoint, bool success);
    public delegate void ReadDataEventHandler(EndPoint endPoint, byte[] data, int size);
    public delegate void DisconnectEventHandler(EndPoint endPoint);

    public abstract class NetworkClientBase
    {
        public event ReadDataEventHandler ReadData;
        public event DisconnectEventHandler Disconnect;

        protected const int m_bufferSize = 65536;

        protected byte[] buffer;

        protected TcpClient tcpClient;
        protected NetworkStream networkStream;

        public bool Closed { get; protected set; }

        public EndPoint RemoteEndPoint { get; protected set; }

        public EndPoint LocalEndPoint { get; protected set; }

        public void Close()
        {
            Closed = true;

            networkStream?.Close();
            tcpClient?.Close();

            RemoteEndPoint = null;
            LocalEndPoint = null;
        }

        public void Send(byte[] data, int size)
        {
            try
            {
                networkStream.Write(data, 0, size);
                networkStream.Flush();
            }
            catch (Exception)
            {
                Close();
                Disconnect?.Invoke(RemoteEndPoint);
                return;
            }
        }

        protected void BeginRead()
        {
            lock (networkStream)
            {
                AsyncCallback callBack = new AsyncCallback(ReadCallBack);
                networkStream.BeginRead(buffer, 0, m_bufferSize, callBack, null);
            }
        }

        protected void ReadCallBack(IAsyncResult ar)
        {
            int bytesRead = 0;

            try
            {
                lock (networkStream)
                {
                    bytesRead = networkStream.EndRead(ar);
                }

                ReadData?.Invoke(RemoteEndPoint, buffer, bytesRead);

                Array.Clear(buffer, 0, buffer.Length);
                BeginRead();
            }
            catch (Exception)
            {
                Disconnect?.Invoke(RemoteEndPoint);
                Close();
            }
        }
    }
}
