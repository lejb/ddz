using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Networking.Core
{
    public delegate void NewClientEventHandler(EndPoint clientEndPoint);
    public delegate void ReadClientDataEventHandler(EndPoint clientEndPoint, byte[] data, int size);
    public delegate void ClientDisconnectEventHandler(EndPoint clientEndPoint);

    public class TCPServer : Dictionary<EndPoint, RemoteClient>
    {
        public event NewClientEventHandler NewClient;
        public event ReadClientDataEventHandler ReadClientData;
        public event ClientDisconnectEventHandler ClientDisconnect;

        private TcpListener listener;

        public bool BeListening { get; private set; }

        public void StartListening(IPAddress ipBind, int port)
        {
            if (BeListening == false)
            {
                BeListening = true;
                listener = new TcpListener(ipBind, port);
                listener.Start();
                listener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), listener);
            }
        }

        public void StopListening()
        {
            if (BeListening == true)
            {
                BeListening = false;
                listener.Stop();
            }
        }

        public void CloseClient(EndPoint clientEndPoint)
        {
            RemoteClient client = this[clientEndPoint];

            client.Disconnect -= OnClientDisconnect;
            client.ReadData -= OnClientRead;

            OnClientDisconnect(clientEndPoint);
        }

        public void CloseAll()
        {
            foreach (RemoteClient client in Values)
            {
                client.Disconnect -= OnClientDisconnect;
                client.ReadData -= OnClientRead;
                ClientDisconnect?.Invoke(client.RemoteEndPoint);
                client.Close();
            }

            Clear();
        }

        public void SendToClient(EndPoint clientEndPoint, byte[] data, int size)
        {
            this[clientEndPoint].Send(data, size);
        }

        private void OnClientRead(EndPoint clientEndPoint, byte[] data, int size)
        {
            ReadClientData?.Invoke(clientEndPoint, data, size);
        }

        private void OnClientDisconnect(EndPoint clientEndPoint)
        {
            RemoteClient client = this[clientEndPoint];
            client.Close();
            Remove(clientEndPoint);
            ClientDisconnect?.Invoke(clientEndPoint);
        }

        private void AcceptTcpClientCallback(IAsyncResult ar)
        {
            TcpClient newClient = null;

            if (BeListening == true) newClient = listener.EndAcceptTcpClient(ar);
            else return;

            RemoteClient newRemoteClient = new RemoteClient(newClient);

            newRemoteClient.ReadData += OnClientRead;
            newRemoteClient.Disconnect += OnClientDisconnect;

            this[newRemoteClient.RemoteEndPoint] = newRemoteClient;
            NewClient?.Invoke(newRemoteClient.RemoteEndPoint);

            listener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), listener);
        }
    }
}
