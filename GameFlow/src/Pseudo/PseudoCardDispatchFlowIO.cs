using GameFlow.Interfaces;

namespace GameFlow.Pseudo
{
    public class PseudoCardDispatchFlowIO : PseudoIOBase, ICardDispatchFlowActions
    {
        public void OnDispatchFinished()
        {
            Log("dispatch finished");
        }
    }
}
