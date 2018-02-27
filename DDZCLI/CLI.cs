using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Logic.Core;
using GameFlow.Core;
using GameFlow.DDZ;
using GameFlow.Interfaces;
using Networking.Core;
using Networking.DDZ;
using Networking.Protocol;
using static Logic.DDZ.Tools;
using static System.ConsoleColor;

namespace DDZ.CLI
{
    class MainClass
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();
            cli.Start();
        }
    }

    class CLI : IDDZIO, IProtocolHandler
    {
        private DDZServer server;
        private DDZClient client;
        private DDZGameData gameData;
        private DDZFlow flow;
        private string myName;

        public DDZGameData GameData { get; private set; }

        public event MyInfoChangeEventHandler MyInfoChange;
        public event MyRoleDecisionEventHandler MyRoleDecision;
        public event MyBringOutEventHandler MyBringOut;

        private void WriteLine(string msg, ConsoleColor color = White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = White;
        }

        public void Start()
        {
            client = new DDZClient(this);
            client.ConnectResult += OnConnectionResult;
            client.Bootstrap += OnBootstrap;
            client.Disconnect += OnDisconnect;

            WriteLine("Please enter your name");
            myName = Console.ReadLine();
            WriteLine($"Hello, {myName}! Welcome to the game!", Yellow);

            string choice = "x";
            do
            {
                WriteLine("Do you want to start a server? y/n");
                choice = Console.ReadLine().Trim().ToLower();
            } while (choice != "y" && choice != "n");
            if (choice == "y")
            {
                server = new DDZServer();
                IPAddress ip = NetTools.GetLocalIP();
                if (ip == null) ip = IPAddress.Parse("127.0.0.1");
                server.StartListening(ip, 54321);
                WriteLine($"Start listening, your IP address is {ip}");
                client.ConnectTo(ip, 54321);
            }
            else
            {
                IPAddress serverAddr = null;
                do
                {
                    Console.WriteLine("Please enter the server's IP address:");
                    serverAddr = ParseIP(Console.ReadLine().Trim());
                } while (serverAddr == null);
                client.ConnectTo(serverAddr, 54321);
            }

            while (true) Thread.Sleep(500);
        }

        private IPAddress ParseIP(string str)
        {
            try
            {
                return IPAddress.Parse(str);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void OnConnectionResult(EndPoint endPoint, bool success)
        {
            if (!success)
            {
                WriteLine("Cannot connect to server!");
                Environment.Exit(1);
            }
            WriteLine($"Connect to {endPoint} complete, waiting for server's response ...");
        }

        private void OnDisconnect(EndPoint endPoint)
        {
            WriteLine("Fatal: Broken Connection", Red);
            Environment.Exit(2);
        }

        private void OnBootstrap(PlayerID myID)
        {
            gameData = new DDZGameData(myID);
            flow = new DDZFlow(client, this, gameData);
            WriteLine($"Registration complete! Waiting for other players ...", Yellow);
            gameData.Players[myID].Name = myName;
            gameData.Players[myID].Exist = gameData.Players[myID].Ready = true;
            MyInfoChange?.Invoke();
        }

        public void OnDispatchFinished()
        {
            ShowCards();
        }

        private void ShowCards()
        {
            string msg = "My Cards: ";
            var myCards = gameData.Players[gameData.MyPlayerID].Cards.ToList().SortedOrder();
            foreach (Card c in myCards) msg += c.ToString() + " ";
            WriteLine(msg, Green);
        }

        public void OnFinalCard(PlayerID player)
        {
            string teamMate = "";
            if (gameData.DZPlayer != player)
            {
                PlayerID teamMateID = PlayerID.P0;
                if (gameData.DZPlayer == teamMateID || player == teamMateID) teamMateID = PlayerID.P1;
                if (gameData.DZPlayer == teamMateID || player == teamMateID) teamMateID = PlayerID.P2;
                if (gameData.DZPlayer == teamMateID || player == teamMateID) teamMateID = PlayerID.P0;
                teamMate = $" and {gameData.Players[teamMateID].Name}";
            }
            WriteLine($"{gameData.Players[player].Name}{teamMate} win the game!", Red);

            flow.Reset();
            WriteLine("Restarting the game, press enter if you are ready!", Yellow);
            Console.ReadLine();

            gameData.Players[gameData.MyPlayerID].Ready = true;
            MyInfoChange?.Invoke();
        }

        public void OnGameStart()
        {
            maxRoleDecisionLevel = 0;
            WriteLine("Game starts!", Yellow);
        }

        public void OnPlayerInfoUpdated(IList<IPlayerForInfo> info)
        {
            WriteLine("current players:");
            foreach (var x in info)
            {
                string ready = x.Ready ? "Ready" : "";
                WriteLine($"{x.Name}: {ready}");
            }
        }

        public void OnStartMyRoleDecision(IList<int> availableDecisionLevels)
        {
            string msg = "It's my turn to decide the role! Avalible:";
            int max = availableDecisionLevels.Max();
            string getMsg = max == 1 ? "1. Ask DZ" : $"{max}. Rob DZ";
            if (max == 0) getMsg = "-----";
            string giveUpMsg = max == 1 ? "0. No Ask" : "0. No Rob";
            WriteLine($"{msg} {getMsg} {giveUpMsg}", Yellow);
            int? decision = null;
            do
            {
                WriteLine("Please enter your choice!");
                decision = GetRoleLevel(availableDecisionLevels);
            } while (decision == null);
            MyRoleDecision?.Invoke(decision.Value);
            maxRoleDecisionLevel = Math.Max(maxRoleDecisionLevel, decision.Value);
        }

        private int? GetRoleLevel(IList<int> availableDecisionLevels)
        {
            try
            {
                int result = Convert.ToInt32(Console.ReadLine().Trim());
                if (availableDecisionLevels.Contains(result)) return result;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void OnIllegalRoleDecision()
        {
            WriteLine("Illegal decision! This should not happen!", Red);
        }

        private int maxRoleDecisionLevel = 0;

        public void OnRoleDecision(PlayerID player, int roleLevel)
        {
            string getMsg = maxRoleDecisionLevel == 0 ? "Ask DZ" : $"Rob DZ";
            string giveUpMsg = maxRoleDecisionLevel == 0 ? "No Ask" : "No Rob";
            string choice = roleLevel > 0 ? getMsg : giveUpMsg;
            string msg = $"[{gameData.Players[player].Name}]: {choice}";
            WriteLine(msg, Magenta);
            maxRoleDecisionLevel = Math.Max(maxRoleDecisionLevel, roleLevel);
        }

        public void OnEndMyRoleDecision()
        {
            WriteLine("Choice OK.");
        }

        public void OnRoleDecisionFinished()
        {
            string msg = $"DZ Player is {gameData.Players[gameData.DZPlayer].Name}, Secret Cards are ";
            foreach (var c in gameData.SecretCards) msg += c.ToString() + " ";
            WriteLine(msg, Cyan);
        }

        public void OnAllGiveUp()
        {
            WriteLine("All players give up! We will restart now!", Red);
            maxRoleDecisionLevel = 0;
        }

        public void OnStartMyTurn()
        {
            WriteLine("It's my turn now! [Press enter directly to pass]", Yellow);
            ISet<Card> cards = null;
            do
            {
                cards = ChooseCards();
            } while (cards == null);
            MyBringOut?.Invoke(cards);
        }

        public void OnIllegalCard()
        {
            WriteLine("Illegal cards!", Red);
            OnStartMyTurn();
        }

        public void OnEndMyTurn()
        {
            WriteLine("Cards OK.");
        }

        public void OnBringOutCard(PlayerID player, IEnumerable<Card> cards)
        {
            string msg = $"[{gameData.Players[player].Name}]: ";
            var orderedCards = cards.ToList().DisplayOrder();
            foreach (Card c in orderedCards) msg += c.ToString() + " ";
            if (cards.Count() == 0) msg += "Pass ";
            msg += $"-- {gameData.Players[player].Cards.Count - cards.Count()} Cards Left";
            WriteLine(msg, player == gameData.DZPlayer ? Cyan : Magenta);
        }

        private ISet<Card> ChooseCards()
        {
            ShowCards();
            WriteLine("Your choice: ");
            string choice = Console.ReadLine().Trim();
            string[] segs = choice.Split(' ');
            var validSegs = from seg in segs where seg != "" select seg;

            foreach (var seg in validSegs)
            {
                if (Cards.FromString(seg) == null)
                {
                    WriteLine("Invalid choice!", Red);
                    return null;
                }
            }

            return new HashSet<Card>(from seg in validSegs select Cards.FromString(seg));
        }

        public void ProcessProtocol(IProtocol protocol)
        {
        }
    }
}
