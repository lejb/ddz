using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Networking.DDZ.Protocols;
using Logic.Core;
using static Logic.Core.Cards;

namespace ProtocolTests
{
    [TestClass]
    public class DispatchCardProtocolTest : ProtocolTestBase<DispatchCardProtocol>
    {
        protected override void TestEqual(DispatchCardProtocol p1, DispatchCardProtocol p2)
        {
            var list1 = p1.Dispatch;
            var list2 = p2.Dispatch;
            Assert.AreEqual(list1.Count, list2.Count);
            for (int i = 0; i < list1.Count; i++)
            {
                Assert.AreEqual(list1[i].Count, list2[i].Count);
                for (int j = 0; j < list1[i].Count; j++)
                {
                    Assert.AreEqual(list1[i][j], list2[i][j]);
                }
            }
        }

        [TestMethod]
        public void DispatchCardProtocol1()
        {
            DispatchCardProtocol protocol = new DispatchCardProtocol(new List<IList<Card>>
            {
                new List<Card> {C3, C4, C5},
                new List<Card> {C3, JO},
                new List<Card> {S5},
                new List<Card> {},
                new List<Card> {H3, H4, H5},
            });

            Test(protocol);
        }

        [TestMethod]
        public void DispatchCardProtocol2()
        {
            DispatchCardProtocol protocol = new DispatchCardProtocol(new List<IList<Card>>
            {
                new List<Card> {},
            });

            Test(protocol);
        }

        [TestMethod]
        public void DispatchCardProtocol3()
        {
            DispatchCardProtocol protocol = new DispatchCardProtocol(new List<IList<Card>>
            {
                new List<Card> {},
                new List<Card> {},
            });

            Test(protocol);
        }

        [TestMethod]
        public void DispatchCardProtocol4()
        {
            DispatchCardProtocol protocol = new DispatchCardProtocol(new List<IList<Card>>
            {
            });

            Test(protocol);
        }
    }
}
