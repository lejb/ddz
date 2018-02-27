using System.Collections.Generic;

namespace GameFlow.DDZ
{
    public delegate void AllGiveUpEventHandler();

    public interface IDDZRoleRangeLogic
    {
        event AllGiveUpEventHandler AllGiveUp;

        IList<int> CurrentRange { get; }

        bool End { get; }

        void Accept(int decision);

        void Reset();
    }
}
