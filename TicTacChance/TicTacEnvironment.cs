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

        public static float[,] chances =
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
                        //If we missed before, we can only succeed
                        //otherwise, chance of success and 1 - chance of failure

                        if (State.Missed[State.Turn] || chances[i, j] >= 1)
                        {
                            result.Add(new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, false, State.Missed, (i, j)), 1, 1));
                        }
                        else
                        {
                            result.Add(new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, false, State.Missed, (i, j)), 1, chances[i, j]));
                            result.Add(new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, true, State.Missed, (i, j)), 1, 1 - chances[i, j]));
                        }

                        //if (State.Missed[State.Turn])
                        //{
                        //    result.Add(new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, false, (i, j)), 1, 1));
                        //}
                        //else
                        //{
                        //    result.Add(new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, false, (i, j)), 1, chances[i, j]));
                        //}
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
                if (number < successor.Chance)
                {
                    return successor.State;
                }
                currentState.Missed[currentState.Turn] = true;
                currentState.Turn = !currentState.Turn;
                return currentState;
            }
            throw new InvalidOperationException("Cannot move to requested spot");
        }
    }
}