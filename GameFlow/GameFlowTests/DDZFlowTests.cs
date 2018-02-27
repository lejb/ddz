using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Core;
using GameFlow.Core;
using GameFlow.DDZ;
using GameFlow.Pseudo;
using static Logic.Core.Cards;
using static GameFlow.Core.Tools;

namespace GameFlow.Tests
{
    [TestClass]
    public class DDZFlowTests
    {
        private DDZGameData gameData0, gameData1, gameData2;
        private PseudoDDZFlowConnection connection;
        private PseudoErrorLogger error;
        private PseudoDDZFlowIO io0, io1, io2;
        private DDZFlowStatic flow0, flow1, flow2;
        private List<string> output0, output1, output2, errOutput;

        private void CompareList(IList<string> expected, IList<string> output)
        {
            Assert.AreEqual(expected.Count, output.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], output[i]);
            }
        }

        private void CompareOutput()
        {
            output0.Sort();
            output1.Sort();
            output2.Sort();
            io0.Outputs.Sort();
            io1.Outputs.Sort();
            io2.Outputs.Sort();
            CompareList(output0, io0.Outputs);
            CompareList(output1, io1.Outputs);
            CompareList(output2, io2.Outputs);
            CompareList(errOutput, error);
        }

        private void Init()
        {
            gameData0 = new DDZGameData(PlayerID.P0);
            gameData1 = new DDZGameData(PlayerID.P1);
            gameData2 = new DDZGameData(PlayerID.P2);
            connection = new PseudoDDZFlowConnection();
            error = new PseudoErrorLogger();
            io0 = new PseudoDDZFlowIO(gameData0);
            io1 = new PseudoDDZFlowIO(gameData1);
            io2 = new PseudoDDZFlowIO(gameData2);
            flow0 = new DDZFlowStatic(connection, io0, gameData0);
            flow1 = new DDZFlowStatic(connection, io1, gameData1);
            flow2 = new DDZFlowStatic(connection, io2, gameData2);
        }

        private void InitCards()
        {
            var l0 = new List<Card> { jo };
            var l1 = new List<Card> { JO };
            var l2 = new List<Card> { C3, C4 };
            gameData0.Players[PlayerID.P0].ClearCards(); gameData0.Players[PlayerID.P0].AddCards(new HashSet<Card>(l0));
            gameData0.Players[PlayerID.P1].ClearCards(); gameData0.Players[PlayerID.P1].AddCards(new HashSet<Card>(l1));
            gameData0.Players[PlayerID.P2].ClearCards(); gameData0.Players[PlayerID.P2].AddCards(new HashSet<Card>(l2));
            gameData1.Players[PlayerID.P0].ClearCards(); gameData1.Players[PlayerID.P0].AddCards(new HashSet<Card>(l0));
            gameData1.Players[PlayerID.P1].ClearCards(); gameData1.Players[PlayerID.P1].AddCards(new HashSet<Card>(l1));
            gameData1.Players[PlayerID.P2].ClearCards(); gameData1.Players[PlayerID.P2].AddCards(new HashSet<Card>(l2));
            gameData2.Players[PlayerID.P0].ClearCards(); gameData2.Players[PlayerID.P0].AddCards(new HashSet<Card>(l0));
            gameData2.Players[PlayerID.P1].ClearCards(); gameData2.Players[PlayerID.P1].AddCards(new HashSet<Card>(l1));
            gameData2.Players[PlayerID.P2].ClearCards(); gameData2.Players[PlayerID.P2].AddCards(new HashSet<Card>(l2));
        }

        [TestMethod]
        public void DDZFlow()
        {
            Init();

            try
            {
                Player p0self = gameData0.Players[gameData0.MyPlayerID];
                Player p1self = gameData1.Players[gameData1.MyPlayerID];
                Player p2self = gameData2.Players[gameData2.MyPlayerID];
                p0self.Name = "a";
                flow0.Reset();
                flow1.Reset();
                flow2.Reset();
                p1self.Name = "b";
                p2self.Name = "c";
                p1self.Ready = true;
                p2self.Ready = true;
                p0self.Ready = true;
                io1.MyInfoHasChanged();
                io2.MyInfoHasChanged();
                io0.MyInfoHasChanged();
                io0.Decide(0);
                io1.Decide(0);
                io2.Decide(1);
                Assert.AreEqual(17, p0self.Cards.Count);
                Assert.AreEqual(17, p1self.Cards.Count);
                Assert.AreEqual(20, p2self.Cards.Count);
                InitCards();
                io2.BringOut(new List<Card> { C3 });
                Assert.AreEqual(1, p0self.Cards.Count);
                Assert.AreEqual(1, p1self.Cards.Count);
                Assert.AreEqual(1, p2self.Cards.Count);
                io0.BringOut(new List<Card> { jo });
            }
            catch (DDZException e)
            {
                error.OnError(e.Message);
            }

            output0 = new List<string>()
            {
                "P0:a,exist,not-ready;P1:,non-exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:,exist,not-ready;P2:,exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,ready;P2:,exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,ready;P2:c,exist,ready;",
                "P0:a,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
                "dispatch finished",
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->0",
                "decision: P2->1",
                "final decision",
                "player bring out: P2",
                "start my turn",
                "end my turn",
                "final card: P0",
            };

            output1 = new List<string>()
            {
                "P0:a,exist,not-ready;P1:,non-exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:,exist,not-ready;P2:,exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,ready;P2:,exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,ready;P2:c,exist,ready;",
                "P0:a,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
                "dispatch finished",
                "decision: P0->0",
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P2->1",
                "final decision",
                "player bring out: P2",
                "player bring out: P0",
                "final card: P0",
            };

            output2 = new List<string>()
            {
                "P0:a,exist,not-ready;P1:,non-exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:,exist,not-ready;P2:,exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,ready;P2:,exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,ready;P2:c,exist,ready;",
                "P0:a,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
                "dispatch finished",
                "decision: P0->0",
                "decision: P1->0",
                "start my decision: available 1;0;",
                "end my decision",
                "final decision",
                "start my turn",
                "end my turn",
                "player bring out: P0",
                "final card: P0",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }
    }
}
