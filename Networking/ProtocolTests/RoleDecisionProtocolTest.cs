using Microsoft.VisualStudio.TestTools.UnitTesting;
using Networking.DDZ.Protocols;
using GameFlow.Core;

namespace ProtocolTests
{
    [TestClass]
    public class RoleDecisionProtocolTest : ProtocolTestBase<RoleDecisionProtocol>
    {
        protected override void TestEqual(RoleDecisionProtocol p1, RoleDecisionProtocol p2)
        {
            Assert.AreEqual(p1.PlayerID, p2.PlayerID);
            Assert.AreEqual(p1.Level, p2.Level);
        }

        [TestMethod]
        public void RoleDecisionProtocol1()
        {
            RoleDecisionProtocol protocol = new RoleDecisionProtocol(PlayerID.P1, 5);
            Test(protocol);
        }
    }
}
