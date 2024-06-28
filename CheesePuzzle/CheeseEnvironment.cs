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

        public List<Akshun<CheeseState>> GetActions(CheeseState State)
        {
            List<Akshun<CheeseState>> results = new List<Akshun<CheeseState>>();

            //Up
            results.Add(new Akshun<CheeseState>(State,
                [new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X, State.Mouse.Y + 1)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X + 1, State.Mouse.Y)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X - 1, State.Mouse.Y)), 1, .1f)]));

            //Down
            results.Add(new Akshun<CheeseState>(State,
                [new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X, State.Mouse.Y - 1)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X + 1, State.Mouse.Y)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X - 1, State.Mouse.Y)), 1, .1f)]));

            //Left
            results.Add(new Akshun<CheeseState>(State,
                [new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X - 1, State.Mouse.Y)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X, State.Mouse.Y + 1)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X, State.Mouse.Y - 1)), 1, .1f)]));

            //Right
            results.Add(new Akshun<CheeseState>(State, 
                [new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X + 1, State.Mouse.Y)), 1, .8f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X, State.Mouse.Y + 1)), 1, .1f),
                 new Successor<CheeseState>(new CheeseState(State.Grid, new Point(State.Mouse.X, State.Mouse.Y - 1)), 1, .1f)]));

            return results;
        }

        public CheeseState MakeMove(Akshun<CheeseState> move, CheeseState currentState)
        {
            throw new NotImplementedException();
        }
    }
}
