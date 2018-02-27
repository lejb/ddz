using System;
using System.Collections.Generic;
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
    public class MainFlowTests
    {
        private DDZGameData gameData0, gameData1, gameData2;
        private PseudoMainFlowConnection connection;
        private PseudoErrorLogger error;
        private PseudoMainFlowIO io0, io1, io2;
        private DDZMainFlow flow0, flow1, flow2;
        private IList<string> output0, output1, output2, errOutput;

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
            connection = new PseudoMainFlowConnection();
            error = new PseudoErrorLogger();
            io0 = new PseudoMainFlowIO();
            io1 = new PseudoMainFlowIO();
            io2 = new PseudoMainFlowIO();
            flow0 = new DDZMainFlow(connection, io0, error, gameData0);
            flow1 = new DDZMainFlow(connection, io1, error, gameData1);
            flow2 = new DDZMainFlow(connection, io2, error, gameData2);
        }

        private void Init1()
        {
            Init();

            gameData0.Players[PlayerID.P0].AddCards(new HashSet<Card>()
            {
                JO, jo, S2, C2, H2, SA, DA, SJ, D0, S9, C8, S7, C6, S6, D6, C5, S5, H5, C3, S4
            });

            gameData0.Players[PlayerID.P1].AddCards(new HashSet<Card>()
            {
                D2, SK, DK, HK, CK, CQ, HQ, DQ, DJ, HJ, H0, C0, C9, D9, S8, H8, S3
            });

            gameData0.Players[PlayerID.P2].AddCards(new HashSet<Card>()
            {
                HA, CA, SQ, CJ, S0, H9, D8, C7, D7, H7, H6, D5, D4, H4, C4, D3, H3
            });

            gameData1.Players[PlayerID.P0].AddCards(new HashSet<Card>()
            {
                JO, jo, S2, C2, H2, SA, DA, SJ, D0, S9, C8, S7, C6, S6, D6, C5, S5, H5, C3, S4
            });

            gameData1.Players[PlayerID.P1].AddCards(new HashSet<Card>()
            {
                D2, SK, DK, HK, CK, CQ, HQ, DQ, DJ, HJ, H0, C0, C9, D9, S8, H8, S3
            });

            gameData1.Players[PlayerID.P2].AddCards(new HashSet<Card>()
            {
                HA, CA, SQ, CJ, S0, H9, D8, C7, D7, H7, H6, D5, D4, H4, C4, D3, H3
            });

            gameData2.Players[PlayerID.P0].AddCards(new HashSet<Card>()
            {
                JO, jo, S2, C2, H2, SA, DA, SJ, D0, S9, C8, S7, C6, S6, D6, C5, S5, H5, C3, S4
            });

            gameData2.Players[PlayerID.P1].AddCards(new HashSet<Card>()
            {
                D2, SK, DK, HK, CK, CQ, HQ, DQ, DJ, HJ, H0, C0, C9, D9, S8, H8, S3
            });

            gameData2.Players[PlayerID.P2].AddCards(new HashSet<Card>()
            {
                HA, CA, SQ, CJ, S0, H9, D8, C7, D7, H7, H6, D5, D4, H4, C4, D3, H3
            });

            flow0.StartMainFlow(PlayerID.P0);
            flow1.StartMainFlow(PlayerID.P0);
            flow2.StartMainFlow(PlayerID.P0);
        }

        [TestMethod]
        public void MainFlow1()
        {
            Init1();

            io0.BringOut(new List<Card>() { });
            io0.BringOut(new List<Card>() { C3, S4 });
            io0.BringOut(new List<Card>() { C6, S6, D6, C5, S5, H5, C3, S4 });
            io0.BringOut(new List<Card>() { S7 }); // causing error
            io2.BringOut(new List<Card>() { H3 }); // causing error
            io1.BringOut(new List<Card>() { S3 });
            io1.BringOut(new List<Card>() { });
            io2.BringOut(new List<Card>() { });
            io0.BringOut(new List<Card>() { }); // causing error
            io0.BringOut(new List<Card>() { SJ, D0, S9, C8, S7 });
            io1.BringOut(new List<Card>() { SK, DK, HK, CK });
            io2.BringOut(new List<Card>() { });
            io0.BringOut(new List<Card>() { });
            io1.BringOut(new List<Card>() { CQ, HQ, DQ, DJ, HJ });
            io2.BringOut(new List<Card>() { });
            io0.BringOut(new List<Card>() { S2, C2, H2, SA, DA });
            io1.BringOut(new List<Card>() { });
            io2.BringOut(new List<Card>() { });
            io0.BringOut(new List<Card>() { JO, jo });
            io0.BringOut(new List<Card>() { S7 }); // causing error
            io2.BringOut(new List<Card>() { H3 }); // causing error
            io1.BringOut(new List<Card>() { S3 }); // causing error

            output0 = new List<string>()
            {
                "start my turn",
                "illegal card",
                "illegal card",
                "end my turn",
                "player bring out: P1",
                "player bring out: P2",
                "start my turn",
                "illegal card",
                "end my turn",
                "player bring out: P1",
                "player bring out: P2",
                "start my turn",
                "end my turn",
                "player bring out: P1",
                "player bring out: P2",
                "start my turn",
                "end my turn",
                "player bring out: P1",
                "player bring out: P2",
                "start my turn",
                "end my turn",
                "final card: P0",
            };

            output1 = new List<string>()
            {
                "player bring out: P0",
                "start my turn",
                "illegal card",
                "end my turn",
                "player bring out: P2",
                "player bring out: P0",
                "start my turn",
                "end my turn",
                "player bring out: P2",
                "player bring out: P0",
                "start my turn",
                "end my turn",
                "player bring out: P2",
                "player bring out: P0",
                "start my turn",
                "end my turn",
                "player bring out: P2",
                "player bring out: P0",
                "final card: P0",
            };

            output2 = new List<string>()
            {
                "player bring out: P0",
                "player bring out: P1",
                "start my turn",
                "end my turn",
                "player bring out: P0",
                "player bring out: P1",
                "start my turn",
                "end my turn",
                "player bring out: P0",
                "player bring out: P1",
                "start my turn",
                "end my turn",
                "player bring out: P0",
                "player bring out: P1",
                "start my turn",
                "end my turn",
                "player bring out: P0",
                "final card: P0",
            };

            errOutput = new List<string>()
            {
                "Invalid bring out when not my turn",
                "Invalid bring out when not my turn",
                "Invalid bring out when game ended",
                "Invalid bring out when game ended",
                "Invalid bring out when game ended",
            };

            CompareOutput();
        }
    }
}
