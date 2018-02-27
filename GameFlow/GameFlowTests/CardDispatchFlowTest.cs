using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFlow.Core;
using GameFlow.DDZ;
using GameFlow.Pseudo;

namespace GameFlow.Tests
{
    [TestClass]
    public class CardDispatchFlowTests
    {
        private DDZGameData gameData0, gameData1, gameData2;
        private PseudoCardDispatchFlowConnection connection;
        private PseudoErrorLogger error;
        private PseudoCardDispatchFlowIO io0, io1, io2;
        private DDZCardDispatchFlow flow0, flow1, flow2;
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
            connection = new PseudoCardDispatchFlowConnection();
            error = new PseudoErrorLogger();
            io0 = new PseudoCardDispatchFlowIO();
            io1 = new PseudoCardDispatchFlowIO();
            io2 = new PseudoCardDispatchFlowIO();
            flow0 = new DDZCardDispatchFlow(connection, io0, error, gameData0);
            flow1 = new DDZCardDispatchFlow(connection, io1, error, gameData1);
            flow2 = new DDZCardDispatchFlow(connection, io2, error, gameData2);
        }

        [TestMethod]
        public void PreparationFlow()
        {
            Init();

            flow0.StartMasterDispatchCard();
            flow1.StartMasterDispatchCard();
            flow2.StartMasterDispatchCard();

            output0 = new List<string>()
            {
                "dispatch finished"
            };

            output1 = new List<string>()
            {
                "dispatch finished"
            };

            output2 = new List<string>()
            {
                "dispatch finished"
            };

            errOutput = new List<string>()
            {
            };

            var cards0 =
            (
                from card in gameData0.Players[gameData0.MyPlayerID].Cards
                select card.ToString()
            );

            var cards1 =
            (
                from card in gameData1.Players[gameData1.MyPlayerID].Cards
                select card.ToString()
            );

            var cards2 =
            (
                from card in gameData2.Players[gameData2.MyPlayerID].Cards
                select card.ToString()
            );

            var cardsLeft =
            (
                from card in gameData0.SecretCards
                select card.ToString()
            );

            CompareOutput();
        }
    }
}
