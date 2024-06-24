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

        List<Successor<T>> GetSuccessors(T State);

        T MakeMove(T move, T currentState);
    }
}