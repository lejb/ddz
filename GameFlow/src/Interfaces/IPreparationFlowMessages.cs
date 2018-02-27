using System.Collections.Generic;
using GameFlow.Core;

namespace GameFlow.Interfaces
{
    public delegate void PlayerInfoUpdatedEventHandler(IList<IPlayerForInfo> playerInfo);

    public interface IPreparationFlowMessages
    {
        event PlayerInfoUpdatedEventHandler PlayerInfoUpdated;

        void SendPlayerUpdateMsg(IPlayerForInfo info);
    }
}
