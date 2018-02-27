using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using Networking.Core;

namespace TestServer1
{
    class TestServer1
    {
        static TCPServer server = new TCPServer();

        static void Main(string[] args)
        {
            server.NewClient += OnNewClient;
            server.ReadClientData += OnReadClientData;
            server.ClientDisconnect += OnClientDisconnect;
            server.StartListening(IPAddress.Parse("192.168.1.66"), 54321);
            while (true)
            {
                Thread.Sleep(200);
            }
        }

        static void SendString(EndPoint clientEndPoint, string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            server.SendToClient(clientEndPoint, bytes, bytes.Length);
        }

        static void OnNewClient(EndPoint clientEndPoint)
        {
            Console.WriteLine($"Welcome {clientEndPoint}");
        }

        static void OnReadClientData(EndPoint clientEndPoint, byte[] data, int size)
        {
            /*byte[] databytes = new byte[size];
            Array.Copy(data, databytes, size);
            string str = Encoding.UTF8.GetString(databytes);
            Console.WriteLine($"client[{clientEndPoint}]:{str}");
            SendString(clientEndPoint, "Server: Echo!"); */
            server.SendToClient(clientEndPoint, data, size);
            Console.WriteLine($"echo complete: {clientEndPoint}");
        }

        static void OnClientDisconnect(EndPoint clientEndPoint)
        {
            Console.WriteLine($"client[{clientEndPoint}] disconnect, count = {server.Count}");
        }
    }
}
