using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface IEnvironment<T> where T : IGameState
    {
        T TargetState { get; }

        List<Akshun<T>> GetActions(T State);

        T MakeMove(Akshun<T> move, T currentState);
    }
}