using System;
using System.Net;
using System.Threading;
using Networking.Core;
using Networking.Protocol;

namespace TestClient1
{
    class TestClient1
    {
        static ProtocolBasedClient client = new ProtocolBasedClient();

        static void Main(string[] args)
        {
            client.ConnectResult += OnConnectResult;
            client.Disconnect += OnDisconnect;
            client.ReadProtocol += OnProtocol;
            client.ConnectTo(IPAddress.Parse("192.168.1.66"), 54321);
            while (true)
            {
                string str = Console.ReadLine();
                StringProtocol stringProtocol = new StringProtocol(str);
                client.SendProtocol(stringProtocol);
                Console.WriteLine($"sent");
            }
        }

        static void OnConnectResult(EndPoint point, bool result)
        {
            Console.WriteLine($"Connect to {point}: {result}");
            if (!result)
            {
                Thread.Sleep(500);
                client.ConnectTo(IPAddress.Parse("192.168.1.66"), 54321);
            }
        }

        static void OnDisconnect(EndPoint point)
        {
            Console.WriteLine($"Disconnect {point}");
        }

        static void OnProtocol(IProtocol protocol)
        {
            Console.WriteLine($"receive: {((StringProtocol)protocol).Message}");
        }
    }
}
