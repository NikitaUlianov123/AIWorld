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

        List<Successor<T>> GetSuccessors(T currentState);

        T MakeMove(T move, T currentState)
        {
            if (GetSuccessors(currentState).Where(x => x.State.Equals(move)).Count() > 0) return move;
            throw new InvalidOperationException("Cannot move to requested spot");
        }
    }
}