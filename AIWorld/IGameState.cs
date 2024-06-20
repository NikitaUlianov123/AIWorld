using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface IGameState
    {
        bool IsTerminal { get; }

        float Score { get; set; }
    }

    public struct Successor<T> where T : IGameState
    {
        public T State;
        public int Cost;
        public float Chance;

        public Successor(T state, int cost, float chance)
        {
            State = state;
            Cost = cost;
            Chance = chance;
        }
    }
}
