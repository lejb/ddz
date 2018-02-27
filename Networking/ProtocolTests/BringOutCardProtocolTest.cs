using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Networking.DDZ.Protocols;
using Logic.Core;
using GameFlow.Core;
using static Logic.Core.Cards;

namespace ProtocolTests
{
    [TestClass]
    public class BringOutCardProtocolTest : ProtocolTestBase<BringOutCardProtocol>
    {
        protected override void TestEqual(BringOutCardProtocol p1, BringOutCardProtocol p2)
        {
            Assert.AreEqual(p1.PlayerID, p2.PlayerID);

            var list1 = p1.Cards;
            var list2 = p2.Cards;
            Assert.AreEqual(list1.Count, list2.Count);

            for (int i = 0; i < list1.Count; i++)
            {
                Assert.AreEqual(list1[i], list2[i]);
            }
        }

        [TestMethod]
        public void BringOutCardProtocol1()
        {
            BringOutCardProtocol protocol = new BringOutCardProtocol(PlayerID.P2, new List<Card>
            {
                C3, C4, C5, JO
            });

            Test(protocol);
        }

        [TestMethod]
        public void BringOutCardProtocol2()
        {
            BringOutCardProtocol protocol = new BringOutCardProtocol(PlayerID.P2, new List<Card>
            {
            });

            Test(protocol);
        }
    }
}
