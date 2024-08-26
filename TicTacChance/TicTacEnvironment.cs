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

        public Dictionary<int, TicTacState> AgentInfo = new Dictionary<int, TicTacState>();

        public static float[,] chances =
            {
            { .5f, 1, .5f },
            { 1, .25f, 1  },
            { .5f, 1, .5f }
            };//this array makes me sad

        public List<Akshun<TicTacState>> GetActions(int agentID)
        {
            return GetActions(AgentInfo[agentID]);
        }
        public List<Akshun<TicTacState>> GetActions(TicTacState State)
        {
            List<Akshun<TicTacState>> result = new List<Akshun<TicTacState>>();
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
                            result.Add(new Akshun<TicTacState>(State,
                                [new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, false, State.Missed, (i, j)), 1, 1)], $"Place {i}, {j}"));
                        }
                        else
                        {
                            result.Add(new Akshun<TicTacState>(State,
                                [new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, false, State.Missed, (i, j)), 1, chances[i, j]),
                                 new Successor<TicTacState>(new TicTacState(State.Grid, State.Turn, true, State.Missed, (i, j)), 1, 1 - chances[i, j])], $"Attempt {i}, {j}"));
                        }
                    }
                }
            }

            return result;
        }

        public TicTacState MakeMove(Akshun<TicTacState> move, int agentID)
        {
            var resultState = MakeMove(move, AgentInfo[agentID]);
            AgentInfo[agentID] = resultState;
            return resultState;
        }

        public TicTacState MakeMove(Akshun<TicTacState> move, TicTacState currentState)
        {
            var actions = GetActions(currentState);
            if (actions.Count() > 0)
            {
                var action = actions.First(x => x.Equals(move));
                var number = randy.NextDouble();

                double threshold = 0;
                foreach (var result in action.Results)
                {
                    if (number + threshold < result.Chance)
                    {
                        return result.State;
                    }
                    threshold += result.Chance;
                }
                currentState.Missed[currentState.Turn] = true;
                currentState.Turn = !currentState.Turn;
                return currentState;
            }
            throw new InvalidOperationException("Cannot move to requested spot");
        }

        public void AddAgent(int ID, TicTacState state)
        {
            AgentInfo.Add(ID, state);
        }
    }
}