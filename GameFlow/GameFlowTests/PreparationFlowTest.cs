using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFlow.Core;
using GameFlow.DDZ;
using GameFlow.Pseudo;

namespace GameFlow.Tests
{
    [TestClass]
    public class PreparationFlowTests
    {
        private DDZGameData gameData0, gameData1, gameData2;
        private PseudoPreparationFlowConnection connection;
        private PseudoErrorLogger error;
        private PseudoPreparationFlowIO io0, io1, io2;
        private PreparationFlow flow0, flow1, flow2;
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
            connection = new PseudoPreparationFlowConnection();
            error = new PseudoErrorLogger();
            io0 = new PseudoPreparationFlowIO(gameData0);
            io1 = new PseudoPreparationFlowIO(gameData1);
            io2 = new PseudoPreparationFlowIO(gameData2);
            flow0 = new DDZPreparationFlow(connection, io0, error, gameData0);
            flow1 = new DDZPreparationFlow(connection, io1, error, gameData1);
            flow2 = new DDZPreparationFlow(connection, io2, error, gameData2);
        }

        [TestMethod]
        public void PreparationFlow()
        {
            Init();

            IPlayerForInfo p0 = gameData0.Players[gameData0.MyPlayerID];
            IPlayerForInfo p1 = gameData1.Players[gameData1.MyPlayerID];
            IPlayerForInfo p2 = gameData2.Players[gameData2.MyPlayerID];

            p0.Name = "a";
            p0.Exist = true;
            io0.MyInfoHasChanged();
            p1.Name = "b";
            p1.Exist = true;
            io1.MyInfoHasChanged();
            p0.Ready = true;
            io0.MyInfoHasChanged();
            p2.Name = "c";
            p2.Exist = true;
            io2.MyInfoHasChanged();
            p2.Ready = true;
            io2.MyInfoHasChanged();
            p0.Exist = false;
            io0.MyInfoHasChanged();
            p0.Name = "asd";
            p0.Exist = true;
            io0.MyInfoHasChanged();
            p0.Ready = true;
            io0.MyInfoHasChanged();
            p1.Ready = true;
            io1.MyInfoHasChanged();
            p0.Exist = false;
            io0.MyInfoHasChanged();
            p0.Exist = true;
            io0.MyInfoHasChanged();

            output0 = new List<string>()
            {
                "P0:a,exist,not-ready;P1:,non-exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:c,exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:a,non-exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
                "P0:asd,non-exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
            };

            output1 = new List<string>()
            {
                "P0:a,exist,not-ready;P1:,non-exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:c,exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:a,non-exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
                "P0:asd,non-exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
            };

            output2 = new List<string>()
            {
                "P0:a,exist,not-ready;P1:,non-exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,not-ready;P1:b,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:,non-exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:c,exist,not-ready;",
                "P0:a,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:a,non-exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,not-ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
                "P0:asd,non-exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "P0:asd,exist,ready;P1:b,exist,ready;P2:c,exist,ready;",
                "game started",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }
    }
}
