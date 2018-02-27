using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFlow.DDZ
{
    public class DDZRoleRangeLogicV1 : IDDZRoleRangeLogic
    {
        private int currentPlayer;
        private int level;
        private int firstPlayerWantDZ;
        private int[] playerDecision;

        public event AllGiveUpEventHandler AllGiveUp;

        public IList<int> CurrentRange { get; private set; }

        public bool End { get; private set; }

        public DDZRoleRangeLogicV1()
        {
            Reset();
        }

        public void Accept(int decision)
        {
            if (decision > 0)
            {
                if (firstPlayerWantDZ == -1) firstPlayerWantDZ = currentPlayer;
                level++;
            }

            playerDecision[currentPlayer % 3] = decision;
            currentPlayer++;

            if (currentPlayer == firstPlayerWantDZ + 4 || currentPlayer >= 3 && level <= 2)
            {
                if (level == 1) AllGiveUp?.Invoke();
                End = true;
                return;
            }

            if (playerDecision[currentPlayer % 3] == 0) CurrentRange = new List<int>() { 0 };
            else CurrentRange = new List<int>() { level, 0 };
        }

        public void Reset()
        {
            End = false;
            currentPlayer = 0;
            level = 1;
            firstPlayerWantDZ = -1;
            CurrentRange= new List<int>() { 1, 0 };
            playerDecision = new int[3] { -1, -1, -1 };
        }
    }
}
