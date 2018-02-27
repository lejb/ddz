using Microsoft.VisualStudio.TestTools.UnitTesting;
using Networking.DDZ;
using Networking.DDZ.Protocols;
using GameFlow.Core;
using System.Collections.Generic;

namespace ProtocolTests
{
    [TestClass]
    public class UpdateInfo_S_ProtocolTest : ProtocolTestBase<UpdateInfoProtocol_S>
    {
        protected override void TestEqual(UpdateInfoProtocol_S p1, UpdateInfoProtocol_S p2)
        {
            Assert.AreEqual(p1.Infos.Count, p2.Infos.Count);
            for (int i = 0; i < p1.Infos.Count; i++)
            {
                Assert.AreEqual(p1.Infos[i].ID, p2.Infos[i].ID);
                Assert.AreEqual(p1.Infos[i].Name, p2.Infos[i].Name);
                Assert.AreEqual(p1.Infos[i].Exist, p2.Infos[i].Exist);
                Assert.AreEqual(p1.Infos[i].Ready, p2.Infos[i].Ready);
            }
        }

        [TestMethod]
        public void UpdateInfoProtocol_S_1()
        {
            var infos = new List<IPlayerForInfo>
            {
                new PlayerInfo
                {
                    ID = PlayerID.P0,
                    Name = "gdhnfzsvdzfbgnhdfgd",
                    Ready = false,
                    Exist = true
                },
                new PlayerInfo
                {
                    ID = PlayerID.P1,
                    Name = "asdsaasdsad",
                    Ready = true,
                    Exist = true
                },
                new PlayerInfo
                {
                    ID = PlayerID.P2,
                    Name = "",
                    Ready = false,
                    Exist = false
                }
            };

            UpdateInfoProtocol_S protocol = new UpdateInfoProtocol_S(infos);
            Test(protocol);
        }

        [TestMethod]
        public void UpdateInfoProtocol_S_2()
        {
            var infos = new List<IPlayerForInfo>
            {
            };

            UpdateInfoProtocol_S protocol = new UpdateInfoProtocol_S(infos);
            Test(protocol);
        }
    }
}
