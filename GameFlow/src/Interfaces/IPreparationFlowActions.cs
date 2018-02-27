using System.Collections.Generic;
using GameFlow.Core;

namespace GameFlow.Interfaces
{
    public delegate void MyInfoChangeEventHandler();

    public interface IPreparationFlowActions
    {
        event MyInfoChangeEventHandler MyInfoChange;

        void OnPlayerInfoUpdated(IList<IPlayerForInfo> info);

        void OnGameStart();
    }
}
