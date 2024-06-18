using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface IEnvironment<T>
    {
        T TargetState { get; }

        List<(T state, int cost)> GetSuccessors(T currentState);

        T MakeMove(T move, T currentState)
        {
            if (GetSuccessors(currentState).Where(x => x.state.Equals(move)).Count() > 0) return move;
            throw new InvalidOperationException("Cannot move to requested spot");
        }
    }
}
