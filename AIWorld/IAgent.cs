using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace AIWorld
{
    //new() results in the goal state for search agents
    public interface IAgent<T> where T : ISensorReading
    {
        List<T> Visited { get; }

        IFrontier<T> Frontier { get; }

        T CurrentGameState { get; set; }

        Akshun<T> Move(List<Akshun<T>> actions);
    }
    public interface IFullInfoAgent<T> : IAgent<T> where T : ISensorReading
    {
        Func<T, List<Akshun<T>>> GetActions { get; set; }
    }

    public class BFSAgent<T> : IFullInfoAgent<T> where T : ISensorReading, new()
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public Func<T, List<Akshun<T>>> GetActions { get; set; }

        private List<T> path;

        private Dictionary<T, (T founder, int distance)> founders;

        private int currentPathIndex;

        public BFSAgent(T startState, Func<T, List<Akshun<T>>> getActions)
        {
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
            GetActions = getActions;

            founders = new Dictionary<T, (T, int)>();
            path = new List<T>();
            GeneratePath(new T());
            currentPathIndex = path.Count - 1;
        }

        private void GeneratePath(T destination)
        {
            founders.Clear();
            founders.Add(CurrentGameState, (CurrentGameState, 0));


            while (!Visited.Contains(destination))
            {
                var currentState = Frontier.Next;
                Frontier.RemoveNext();

                var actions = GetActions(currentState);
                foreach (var action in actions)
                {
                    foreach (var next in action.Results)
                    {
                        if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                        {
                            Frontier.Add(next.State, next.Cost);
                        }

                        if (founders.ContainsKey(next.State))
                        {
                            if (founders[next.State].distance > founders[currentState].distance + next.Cost)
                            {
                                founders[next.State] = (currentState, founders[currentState].distance + next.Cost);
                            }
                        }
                        else
                        {
                            founders.Add(next.State, (currentState, founders[currentState].distance + next.Cost));
                        }
                    }
                }

                Visited.Add(currentState);
            }

            T current = destination;
            while (!current.Equals(CurrentGameState))
            {
                path.Add(current);
                current = founders[current].founder;
            }
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            var next = path[currentPathIndex];
            currentPathIndex--;
            return actions.First(x => x.Results[0].State.Equals(next));
        }
    }

    public class DFSAgent<T> : IFullInfoAgent<T> where T : ISensorReading, new()
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public Func<T, List<Akshun<T>>> GetActions { get; set; }

        private List<T> path;

        private Dictionary<T, (T founder, int distance)> founders;

        private int currentPathIndex;

        public DFSAgent(T startState, Func<T, List<Akshun<T>>> getActions)
        {
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
            GetActions = getActions;

            founders = new Dictionary<T, (T, int)>();
            path = new List<T>();
            GeneratePath(new T());
            currentPathIndex = path.Count - 1;
        }

        private void GeneratePath(T destination)
        {
            founders.Clear();
            founders.Add(CurrentGameState, (CurrentGameState, 0));


            while (!Visited.Contains(destination))
            {
                var currentState = Frontier.Next;
                Frontier.RemoveNext();

                var actions = GetActions(currentState);
                foreach (var action in actions)
                {
                    foreach (var next in action.Results)
                    {
                        if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                        {
                            Frontier.Add(next.State, -next.Cost);
                        }

                        if (founders.ContainsKey(next.State))
                        {
                            if (founders[next.State].distance > founders[currentState].distance + next.Cost)
                            {
                                founders[next.State] = (currentState, founders[currentState].distance + next.Cost);
                            }
                        }
                        else
                        {
                            founders.Add(next.State, (currentState, founders[currentState].distance + next.Cost));
                        }
                    }
                }

                Visited.Add(currentState);
            }

            T current = destination;
            while (!current.Equals(CurrentGameState))
            {
                path.Add(current);
                current = founders[current].founder;
            }
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            var next = path[currentPathIndex];
            currentPathIndex--;
            return actions.First(x => x.Results[0].State.Equals(next));
        }
    }

    public class UCSAgent<T> : IFullInfoAgent<T> where T : ISensorReading, new()
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public Func<T, List<Akshun<T>>> GetActions { get; set; }

        private List<T> path;

        private Dictionary<T, (T founder, int distance)> founders;

        private int currentPathIndex;

        public UCSAgent(T startState, Func<T, List<Akshun<T>>> getActions)
        {
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
            GetActions = getActions;

            founders = new Dictionary<T, (T, int)>();
            path = new List<T>();
            GeneratePath(new T());
            currentPathIndex = path.Count - 1;
        }

        private void GeneratePath(T destination)
        {
            founders.Clear();
            founders.Add(CurrentGameState, (CurrentGameState, 0));


            while (!Visited.Contains(destination))
            {
                var currentState = Frontier.Next;
                Frontier.RemoveNext();

                var actions = GetActions(currentState);
                foreach (var action in actions)
                {
                    foreach (var next in action.Results)
                    {
                        if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                        {
                            Frontier.Add(next.State, 1);
                        }

                        if (founders.ContainsKey(next.State))
                        {
                            if (founders[next.State].distance > founders[currentState].distance + next.Cost)
                            {
                                founders[next.State] = (currentState, founders[currentState].distance + next.Cost);
                            }
                        }
                        else
                        {
                            founders.Add(next.State, (currentState, founders[currentState].distance + next.Cost));
                        }
                    }
                }

                Visited.Add(currentState);
            }

            T current = destination;
            while (!current.Equals(CurrentGameState))
            {
                path.Add(current);
                current = founders[current].founder;
            }
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            var next = path[currentPathIndex];
            currentPathIndex--;
            return actions.First(x => x.Results[0].State.Equals(next));
        }
    }

    public class AStarAgent<T> : IFullInfoAgent<T> where T : ISensorReading, new()
    {
        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public Func<T, List<Akshun<T>>> GetActions { get; set; }

        private List<T> path;

        private Dictionary<T, (T founder, int distance)> founders;

        private int currentPathIndex;

        public AStarAgent(T startState, Func<T, List<Akshun<T>>> getActions)
        {
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
            GetActions = getActions;

            founders = new Dictionary<T, (T, int)>();
            path = new List<T>();
            GeneratePath(new T());
            currentPathIndex = path.Count - 1;
        }

        private void GeneratePath(T destination)
        {
            founders.Clear();
            founders.Add(CurrentGameState, (CurrentGameState, 0));

            while (/*!founders.ContainsKey(destination) || founders[destination].distance > 32)*/!Visited.Contains(destination))
            {
                var currentState = Frontier.Next;
                Frontier.RemoveNext();

                var actions = GetActions(currentState);
                foreach (var action in actions)
                {
                    foreach (var next in action.Results)
                    {
                        if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                        {
                            Frontier.Add(next.State, (int)next.State.Score + next.Cost);
                        }

                        if (founders.ContainsKey(next.State))
                        {
                            if (founders[next.State].distance > founders[currentState].distance + next.Cost)
                            {
                                founders[next.State] = (currentState, founders[currentState].distance + next.Cost);
                            }
                        }
                        else
                        {
                            founders.Add(next.State, (currentState, founders[currentState].distance + next.Cost));
                        }
                    }
                }

                Visited.Add(currentState);
            }

            T current = destination;
            while (!current.Equals(CurrentGameState))
            {
                path.Add(current);
                current = founders[current].founder;
            }
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            var next = path[currentPathIndex];
            currentPathIndex--;
            return actions.First(x => x.Results[0].State.Equals(next));
        }
    }

    public class BogoAgent<T> : IAgent<T> where T : ISensorReading, new()
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        Random random;

        public BogoAgent(T startState)
        {
            random = new Random();
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            return actions[random.Next(actions.Count)];
        }
    }


    public class ExpectiMax<T> : IFullInfoAgent<T> where T : ISensorReading
    {
        class Node
        {
            public T State;
            public float Score;
            public List<(Node node, float chance)> Children;
            public Node(Func<T, List<Akshun<T>>> getActions, T startState, Dictionary<T, Node> prev = null)
            {
                Children = new List<(Node, float chance)>();
                if (prev == null) prev = new Dictionary<T, Node>();
                State = startState;
                if (!prev.ContainsKey(State))
                {
                    prev.Add(State, this);
                    if (!State.IsTerminal)
                    {
                        var actions = getActions(State);
                        foreach (var action in actions)
                        {
                            foreach (var nex in action.Results)
                            {
                                if (prev.ContainsKey(nex.State))
                                {
                                    //changed this last
                                    //Children.AddRange(prev[nex.State].Children);
                                    Children.Add((prev[nex.State], nex.Chance));
                                }
                                else
                                {
                                    Children.Add((new Node(getActions, nex.State, prev), nex.Chance));
                                    //if (nex.Chance < 1)
                                    //{
                                    //    Children.Add((new Node(successors, State, prev), 1 - nex.Chance));
                                    //}
                                }
                            }
                        }
                        Score = Children.Average(x => x.node.Score * x.chance);
                    }
                    Score = State.Score;
                }
            }
        }

        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get => node.State; set => node = node.Children.First(x => x.Item1.State.Equals(value)).Item1; }

        public Func<T, List<Akshun<T>>> GetActions { get; set; }

        Node node;

        public ExpectiMax(Func<T, List<Akshun<T>>> getActions, T startState)
        {
            GetActions = getActions;
            node = new Node(getActions, startState);
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            int Best = 0;
            for (var i = 0; i < node.Children.Count; i++)
            {
                if (node.Children[i].Item1.Score > node.Children[Best].Item1.Score) Best = i;
            }

            return actions.First(x => x.Results.Contains(x.Results.FirstOrDefault(y => y.State.Equals(node.Children[Best].Item1.State))));
        }
    }

    public class MarkovAgent<T> : IFullInfoAgent<T> where T : ISensorReading
    {
        public Func<T, List<Akshun<T>>> GetActions { get; set; }

        public int Cost { get; set; }

        public List<T> Visited => null;

        public IFrontier<T> Frontier => null;

        public T CurrentGameState { get; set; }

        Dictionary<T, List<T>> Successors;

        public MarkovAgent(Func<T, List<Akshun<T>>> getActions, List<T> starts)
        {
            GetActions = getActions;

            Successors = new Dictionary<T, List<T>>();
            foreach (var start in starts)
            {
                Successors.Add(start, new List<T>());
                for (int i = 0; i < 100; i++)
                {
                    var temp = start;
                    for (int j = 0; j < 100; j++)
                    {

                    }
                    //Successors[start].Add(successors[]);
                }
            }
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            //choose best move
            throw new NotImplementedException();
        }
    }

    public class QAgent<T> : IAgent<T> where T : ISensorReading
    {
        public int Cost { get; set; }

        public List<T> Visited { get => visited.ToList(); set => visited = value.ToHashSet(); }

        HashSet<T> visited;

        public IFrontier<T> Frontier => null;

        public T CurrentGameState { get; set; }

        (T state, Akshun<T> action) prev;

        public Dictionary<(T state, Akshun<T> action), (float score, float bestScore)> Model;

        Random random;

        public float Epsilon;
        public float LearningRate;


        public QAgent(T start, float epsilon, float learningRate)
        {
            CurrentGameState = start;
            Model = new Dictionary<(T state, Akshun<T> action), (float score, float bestScore)>();
            Epsilon = epsilon;
            random = new Random();
            LearningRate = learningRate;
            visited = new HashSet<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            if (!prev.Equals(default))
            {
                //record result of prev move in Model
                if (Model.ContainsKey(prev))
                {
                    //Best score to current state:
                    float best = float.MinValue;
                    foreach (var action in Model.Where(x => x.Key.action.Results.Where(y => y.State.Equals(CurrentGameState)).Count() > 0))
                    {
                        best = Math.Max(best, Model[action.Key].score);
                    }

                    Model[prev] = ((Model[prev].score * (1 - LearningRate)) + (LearningRate * (CurrentGameState.Score + best/*maybe with inflation*/)), Model[prev].bestScore);
                }
                else
                {
                    Model.Add(prev, (CurrentGameState.Score, CurrentGameState.Score));
                }
            }


            //do move
            double randy = random.NextDouble();
            if (randy < Epsilon || prev.Equals(default) || CurrentGameState.IsTerminal)
            {
                //add random move to model
                //random move
                prev = (CurrentGameState, actions[random.Next(actions.Count)]);
            }
            else
            {
                //best move

                Akshun<T> best = default;
                foreach (var action in Model.Where(x => x.Key.state.Equals(CurrentGameState)))
                {
                    if (best.Equals(default) || action.Value.score > Model[(CurrentGameState, best)].score)
                    {
                        best = action.Key.action;
                    }
                }

                if (best.Equals(default))//current state is not in model
                {
                    //random move
                    prev = (CurrentGameState, actions[random.Next(actions.Count)]);
                }
                else
                {
                    prev = (CurrentGameState, best);
                }
            }
            visited.Add(CurrentGameState);
            return prev.action;
        }
    }
}