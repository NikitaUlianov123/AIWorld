using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIWorld;

namespace TicTacChance
{
    public class TicTacState : IGameState
    {
        public bool IsTerminal => throw new NotImplementedException();

        public float Score { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
