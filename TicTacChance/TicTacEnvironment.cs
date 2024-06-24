using AIWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacChance
{
    public class TicTacEnvironment : IEnvironment<TicTacState>
    {
        Random randy = new Random();
        public TicTacState TargetState => throw new NotImplementedException();

        float[,] chances =
            {
            { .5f, 1, .5f },
            { 1, .25f, 1  },
            { .5f, 1, .5f }
            };//this array makes me sad

        public List<Successor<TicTacState>> GetSuccessors(TicTacState State)
        {
            List<Successor<TicTacState>> result = new List<Successor<TicTacState>>();
            if(State.IsTerminal) return result;

            for (int i = 0; i < State.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < State.Grid.GetLength(1); j++)
                {
                    if (State.Grid[i, j] == null)
                    {
                        bool?[,] temp = new bool?[0, 0];
                        State.Grid.CopyTo(temp, 0);

                        result.Add(new Successor<TicTacState>(new TicTacState(temp), 1, chances[i, j]));
                    }
                }
            }

            return result;
        }

        public TicTacState MakeMove(TicTacState move, TicTacState currentState)
        {
            var successors = GetSuccessors(currentState).Where(x => x.State.Equals(move));
            if (successors.Count() > 0)
            {
                var successor = successors.First();
                var number = randy.NextDouble();
                if (number < successor.Chance || currentState.missed)
                {
                    return successor.State;
                }
                currentState.missed = true;
                return currentState;
            }
            throw new InvalidOperationException("Cannot move to requested spot");
        }
    }
}
