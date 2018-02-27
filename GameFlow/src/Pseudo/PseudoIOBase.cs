using System.Collections.Generic;
namespace GameFlow.Pseudo
{
    public abstract class PseudoIOBase
    {
        public List<string> Outputs { get; } = new List<string>();

        protected void Log(string msg)
        {
            Outputs.Add(msg);
        }
    }
}
