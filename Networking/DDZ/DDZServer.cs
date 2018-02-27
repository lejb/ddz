using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameFlow.Core;
using Networking.Core;
using Networking.Protocol;
using Networking.DDZ.Protocols;

namespace Networking.DDZ
{
    public class DDZServer : TCPServer
    {
        private HeaderProtocolInterpreter interpreter = new HeaderProtocolInterpreter();
        private Dictionary<EndPoint, PlayerInfo> players = new Dictionary<EndPoint, PlayerInfo>();
        private HashSet<PlayerID> fullPlayerSet = new HashSet<PlayerID> { PlayerID.P0, PlayerID.P1, PlayerID.P2 };
        private HashSet<PlayerID> currentPlayerSet = new HashSet<PlayerID>();

        public DDZServer()
        {
            NewClient += OnNewClient;
            ClientDisconnect += OnClientDisconnect;
            ReadClientData += OnReadClientData;
            interpreter.NewMessage += OnProtocol;
        }

        private void SendProtocol(EndPoint clientEndPoint, IProtocol protocol)
        {
            byte[] bytes = protocol.WithHeader();
            SendToClient(clientEndPoint, bytes, bytes.Length);
        }

        private void BroadCastProtocol(IProtocol protocol)
        {
            byte[] bytes = protocol.WithHeader();
            foreach (var p in players.Keys) SendToClient(p, bytes, bytes.Length);
        }

        private void OnNewClient(EndPoint clientEndPoint)
        {
            if (players.Count >= 3)
            {
                CloseClient(clientEndPoint);
                return;
            }

            var tempFullSet = new HashSet<PlayerID>(fullPlayerSet);
            tempFullSet.ExceptWith(currentPlayerSet);
            PlayerID givenID = tempFullSet.First();

            currentPlayerSet.Add(givenID);
            players.Add(clientEndPoint, new PlayerInfo() { ID = givenID });

            SendProtocol(clientEndPoint, new BootstrapProtocol(givenID));
        }

        private void OnClientDisconnect(EndPoint clientEndPoint)
        {
            if (players.ContainsKey(clientEndPoint))
            {
                currentPlayerSet.Remove(players[clientEndPoint].ID);
                players.Remove(clientEndPoint);
            }
        }

        private void OnReadClientData(EndPoint clientEndPoint, byte[] data, int size)
        {
            interpreter.AddToBuffer(data, 0, size);
        }

        private void OnProtocol(IProtocol protocol)
        {
            switch (protocol)
            {
                case UpdateInfoProtocol_C c:
                    PlayerID player = c.Info.ID;
                    if (!currentPlayerSet.Contains(player)) return;
                    var kvp = (from pair in players where pair.Value.ID == player select pair).First();
                    players[kvp.Key] = new PlayerInfo(c.Info);
                    IList<IPlayerForInfo> infos = (from info in players.Values select (IPlayerForInfo)info).ToList();
                    BroadCastProtocol(new UpdateInfoProtocol_S(infos));
                    break;
                default:
                    BroadCastProtocol(protocol);
                    break;
            }
        }
    }
}
