using AIWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheesePuzzle
{
    public class CheeseEnvironment : IEnvironment<MouseSensors>
    {
        public CheeseState TargetState => throw new NotImplementedException();


        Random randy = new Random();

        CheeseState StartState = new CheeseState(-1);

        public Dictionary<int, CheeseState> AgentInfo;

        public CheeseEnvironment()
        {
            AgentInfo = new Dictionary<int, CheeseState>();
        }

        public void AddAgent(int id, CheeseState state)
        {
            AgentInfo.Add(id, state);
        }

        public List<Akshun<MouseSensors>> GetActions(int ID)
        {
            List<Akshun<MouseSensors>> results = new List<Akshun<MouseSensors>>();
            var State = AgentInfo[ID];
            //Up
            results.Add(new Akshun<MouseSensors>(new MouseSensors(State),
                [new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y - 1))), 1, .8f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X + 1, State.Mouse.Y))), 1, .1f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X - 1, State.Mouse.Y))), 1, .1f)],
                "Up"));

            //Down
            results.Add(new Akshun<MouseSensors>(new MouseSensors(State),
                [new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y + 1))), 1, .8f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X + 1, State.Mouse.Y))), 1, .1f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X - 1, State.Mouse.Y))), 1, .1f)],
                "Down"));

            //Left
            results.Add(new Akshun<MouseSensors>(new MouseSensors(State),
                [new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X - 1, State.Mouse.Y))), 1, .8f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y + 1))), 1, .1f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y - 1))), 1, .1f)],
                "Left"));

            //Right
            results.Add(new Akshun<MouseSensors>(new MouseSensors(State),
                [new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X + 1, State.Mouse.Y))), 1, .8f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y + 1))), 1, .1f),
                 new Successor<MouseSensors>(new MouseSensors(new CheeseState(State, new Point(State.Mouse.X, State.Mouse.Y - 1))), 1, .1f)],
                "Right"));

            return results;
        }

        public MouseSensors MakeMove(Akshun<MouseSensors> move, int ID)
        {
            if (AgentInfo[ID].IsTerminal)
            {
                AgentInfo[ID] = new CheeseState();
                return new MouseSensors(AgentInfo[ID]);
            }

            var actions = GetActions(ID);
            if (actions.Count() > 0)
            {
                var action = actions.First(x => x.Equals(move));
                var number = randy.NextDouble();

                double threshold = 0;
                foreach (var result in action.Results)
                {
                    if (number < result.Chance + threshold)
                    {
                        AgentInfo[ID].Mouse = new Point(result.State.values[0], result.State.values[1]);
                        return result.State;
                    }
                    threshold += result.Chance;
                }
                return move.Start;
            }
            throw new InvalidOperationException("Cannot move to requested spot");
        }

        public void AddAgent(int ID, MouseSensors state)
        {
            throw new NotImplementedException();
        }

        public List<Akshun<MouseSensors>> GetActions(MouseSensors state)
        {
            throw new NotImplementedException();
        }
    }
}
