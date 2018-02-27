using Networking.DDZ.Protocols;

namespace Networking.Protocol
{
    public static class Protocols
    {
        public static IProtocol GetNewInstance(this ProtocolType protocolType)
        {
            switch (protocolType)
            {
                case ProtocolType.Test: return new StringProtocol();
                case ProtocolType.DDZ_Bootstrap: return new BootstrapProtocol();
                case ProtocolType.DDZ_BringOutCard: return new BringOutCardProtocol();
                case ProtocolType.DDZ_DispatchCard: return new DispatchCardProtocol();
                case ProtocolType.DDZ_RoleDecision: return new RoleDecisionProtocol();
                case ProtocolType.DDZ_RoleDecisionStart: return new RoleDecisionStartProtocol();
                case ProtocolType.DDZ_UpdateInfo_C: return new UpdateInfoProtocol_C();
                case ProtocolType.DDZ_UpdateInfo_S: return new UpdateInfoProtocol_S();
                default: return null;
            }
        }
    }
}
