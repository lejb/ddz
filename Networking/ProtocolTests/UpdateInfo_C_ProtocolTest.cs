using Microsoft.VisualStudio.TestTools.UnitTesting;
using Networking.DDZ;
using Networking.DDZ.Protocols;
using GameFlow.Core;

namespace ProtocolTests
{
    [TestClass]
    public class UpdateInfo_C_ProtocolTest : ProtocolTestBase<UpdateInfoProtocol_C>
    {
        protected override void TestEqual(UpdateInfoProtocol_C p1, UpdateInfoProtocol_C p2)
        {
            Assert.AreEqual(p1.Info.ID, p2.Info.ID);
            Assert.AreEqual(p1.Info.Name, p2.Info.Name);
            Assert.AreEqual(p1.Info.Exist, p2.Info.Exist);
            Assert.AreEqual(p1.Info.Ready, p2.Info.Ready);
        }

        [TestMethod]
        public void UpdateInfoProtocol_C_1()
        {
            var info = new PlayerInfo
            {
                ID = PlayerID.P0,
                Name = "gdhnfzsvdzfbgnhdfgd",
                Ready = false,
                Exist = true
            };
            UpdateInfoProtocol_C protocol = new UpdateInfoProtocol_C(info);
            Test(protocol);
        }
    }
}
