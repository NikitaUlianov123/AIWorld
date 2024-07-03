using AIWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheesePuzzle
{
    public class CheeseEnvironment : IEnvironment<CheeseState>
    {
        public CheeseState TargetState => throw new NotImplementedException();

        Random randy = new Random();

        CheeseState StartState = new CheeseState(-1);

        public List<Akshun<CheeseState>> GetActions(CheeseState State)
        {
            List<Akshun<CheeseState>> results = new List<Akshun<CheeseState>>();

            //Up
            results.Add(new Akshun<CheeseState>(State,
                [new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y + 1)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X + 1, State.Mouse.Y)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X - 1, State.Mouse.Y)), 1, .1f)]));

            //Down
            results.Add(new Akshun<CheeseState>(State,
                [new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y - 1)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X + 1, State.Mouse.Y)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X - 1, State.Mouse.Y)), 1, .1f)]));

            //Left
            results.Add(new Akshun<CheeseState>(State,
                [new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X - 1, State.Mouse.Y)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y + 1)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y - 1)), 1, .1f)]));

            //Right
            results.Add(new Akshun<CheeseState>(State, 
                [new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X + 1, State.Mouse.Y)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y + 1)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y - 1)), 1, .1f)]));

            return results;
        }

        public CheeseState MakeMove(Akshun<CheeseState> move, CheeseState currentState)
        {
            if (currentState.IsTerminal) return StartState;

            var actions = GetActions(currentState);
            if (actions.Count() > 0)
            {
                var action = actions.First(x => x.Equals(move));
                var number = randy.NextDouble();

                double threshold = 0;
                foreach (var result in action.Results)
                {
                    if (number < result.Chance + threshold)
                    {
                        return result.State;
                    }
                    threshold += result.Chance;
                }
                return currentState;
            }
            throw new InvalidOperationException("Cannot move to requested spot");
        }
    }
}
