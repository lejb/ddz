using Microsoft.VisualStudio.TestTools.UnitTesting;
using Networking.DDZ.Protocols;
using GameFlow.Core;

namespace ProtocolTests
{
    [TestClass]
    public class RoleDecisionStartProtocolTest : ProtocolTestBase<RoleDecisionStartProtocol>
    {
        protected override void TestEqual(RoleDecisionStartProtocol p1, RoleDecisionStartProtocol p2)
        {
            Assert.AreEqual(p1.Starter, p2.Starter);
        }

        [TestMethod]
        public void RoleDecisionStartProtocol1()
        {
            RoleDecisionStartProtocol protocol = new RoleDecisionStartProtocol(PlayerID.P1);
            Test(protocol);
        }
    }
}
