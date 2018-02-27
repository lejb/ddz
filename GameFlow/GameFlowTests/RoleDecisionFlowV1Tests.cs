using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFlow.Core;
using GameFlow.DDZ;
using GameFlow.Pseudo;

namespace GameFlow.Tests
{
    [TestClass]
    public class RoleDecisionFlowV1Tests
    {
        private DDZGameData gameData0, gameData1, gameData2;
        private PseudoRoleDecisionFlowConnection connection;
        private PseudoErrorLogger error;
        private PseudoRoleDecisionFlowIO io0, io1, io2;
        private DDZRoleDecisionFlowV1 flow0, flow1, flow2;
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
            connection = new PseudoRoleDecisionFlowConnection();
            error = new PseudoErrorLogger();
            io0 = new PseudoRoleDecisionFlowIO();
            io1 = new PseudoRoleDecisionFlowIO();
            io2 = new PseudoRoleDecisionFlowIO();
            flow0 = new DDZRoleDecisionFlowV1(connection, io0, error, gameData0);
            flow1 = new DDZRoleDecisionFlowV1(connection, io1, error, gameData1);
            flow2 = new DDZRoleDecisionFlowV1(connection, io2, error, gameData2);
            connection.StartRoleDecision(PlayerID.P0);
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_1()
        {
            Init();

            io0.Decide(2); // causing error
            io0.Decide(-1); // causing error
            io0.Decide(1);
            io1.Decide(1); // causing error
            io1.Decide(2);
            io1.Decide(2); // causing error
            io2.Decide(3);
            io0.Decide(4);
            io0.Decide(4); // causing error
            io1.Decide(4); // causing error
            io2.Decide(4); // causing error

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "out of range decision",
                "out of range decision",
                "end my decision",
                "decision: P1->2",
                "decision: P2->3",
                "start my decision: available 4;0;",
                "end my decision",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->1",
                "start my decision: available 2;0;",
                "out of range decision",
                "end my decision",
                "decision: P2->3",
                "decision: P0->4",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->1",
                "decision: P1->2",
                "start my decision: available 3;0;",
                "end my decision",
                "decision: P0->4",
                "final decision",
            };

            errOutput = new List<string>()
            {
                "Invalid decision when not my turn",
                "Invalid decisoin when decision ended",
                "Invalid decisoin when decision ended",
                "Invalid decisoin when decision ended",
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_2()
        {
            Init();
            
            io0.Decide(1);
            io1.Decide(0);
            io2.Decide(2);
            io0.Decide(3);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->0",
                "decision: P2->2",
                "start my decision: available 3;0;",
                "end my decision",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->1",
                "start my decision: available 2;0;",
                "end my decision",
                "decision: P2->2",
                "decision: P0->3",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->1",
                "decision: P1->0",
                "start my decision: available 2;0;",
                "end my decision",
                "decision: P0->3",
                "final decision",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_3()
        {
            Init();

            io0.Decide(1);
            io1.Decide(0);
            io2.Decide(0);
            io0.Decide(2);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->0",
                "decision: P2->0",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->1",
                "start my decision: available 2;0;",
                "end my decision",
                "decision: P2->0",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->1",
                "decision: P1->0",
                "start my decision: available 2;0;",
                "end my decision",
                "final decision",
            };

            errOutput = new List<string>()
            {
                "Invalid decisoin when decision ended",
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_4()
        {
            Init();

            io0.Decide(0);
            io1.Decide(0);
            io2.Decide(0);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->0",
                "decision: P2->0",
                "all give up",
            };

            output1 = new List<string>()
            {
                "decision: P0->0",
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P2->0",
                "all give up",
            };

            output2 = new List<string>()
            {
                "decision: P0->0",
                "decision: P1->0",
                "start my decision: available 1;0;",
                "end my decision",
                "all give up",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_5()
        {
            Init();

            io0.Decide(0);
            io1.Decide(0);
            io2.Decide(1);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->0",
                "decision: P2->1",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->0",
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P2->1",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->0",
                "decision: P1->0",
                "start my decision: available 1;0;",
                "end my decision",
                "final decision",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_6()
        {
            Init();

            io0.Decide(0);
            io1.Decide(1);
            io2.Decide(0);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->1",
                "decision: P2->0",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->0",
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P2->0",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->0",
                "decision: P1->1",
                "start my decision: available 2;0;",
                "end my decision",
                "final decision",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_7()
        {
            Init();

            io0.Decide(0);
            io1.Decide(1);
            io2.Decide(2);
            io0.Decide(0);
            io1.Decide(3);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->1",
                "decision: P2->2",
                "start my decision: available 0;",
                "end my decision",
                "decision: P1->3",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->0",
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P2->2",
                "decision: P0->0",
                "start my decision: available 3;0;",
                "end my decision",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->0",
                "decision: P1->1",
                "start my decision: available 2;0;",
                "end my decision",
                "decision: P0->0",
                "decision: P1->3",
                "final decision",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_8()
        {
            Init();

            io0.Decide(1);
            io1.Decide(0);
            io2.Decide(2);
            io0.Decide(0);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->0",
                "decision: P2->2",
                "start my decision: available 3;0;",
                "end my decision",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->1",
                "start my decision: available 2;0;",
                "end my decision",
                "decision: P2->2",
                "decision: P0->0",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->1",
                "decision: P1->0",
                "start my decision: available 2;0;",
                "end my decision",
                "decision: P0->0",
                "final decision",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }

        [TestMethod]
        public void RoleDecisioinFlowV1_9()
        {
            Init();

            io0.Decide(0);
            io1.Decide(1);
            io2.Decide(2);
            io0.Decide(0);
            io1.Decide(0);

            output0 = new List<string>()
            {
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P1->1",
                "decision: P2->2",
                "start my decision: available 0;",
                "end my decision",
                "decision: P1->0",
                "final decision",
            };

            output1 = new List<string>()
            {
                "decision: P0->0",
                "start my decision: available 1;0;",
                "end my decision",
                "decision: P2->2",
                "decision: P0->0",
                "start my decision: available 3;0;",
                "end my decision",
                "final decision",
            };

            output2 = new List<string>()
            {
                "decision: P0->0",
                "decision: P1->1",
                "start my decision: available 2;0;",
                "end my decision",
                "decision: P0->0",
                "decision: P1->0",
                "final decision",
            };

            errOutput = new List<string>()
            {
            };

            CompareOutput();
        }
    }
}
