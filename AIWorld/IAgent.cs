using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace AIWorld
{
    public interface IAgent<T> where T : IGameState
    {
        int Cost { get; }

        List<T> Visited { get; }

        IFrontier<T> Frontier { get; }

        T CurrentGameState { get; set; }

        Akshun<T> Move(List<Akshun<T>> actions);
    }

    public class BFSAgent<T> : IAgent<T> where T : IGameState
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public BFSAgent(T startState)
        {
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            Frontier.RemoveNext();
            foreach (var action in actions)
            {
                foreach (var next in action.Results)
                {
                    if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                    {
                        Frontier.Add(next.State, next.Cost);
                    }
                }
            }
            Visited.Add(CurrentGameState);
            return actions.First(x => x.Results.Contains(x.Results.FirstOrDefault(y => y.State.Equals(Frontier.Next))));
        }
    }

    public class DFSAgent<T> : IAgent<T> where T : IGameState
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public DFSAgent(T startState)
        {
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            Frontier.RemoveNext();
            foreach (var action in actions)
            {
                foreach (var next in action.Results)
                {
                    if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                    {
                        Frontier.Add(next.State, -next.Cost);
                    }
                }
            }
            Visited.Add(CurrentGameState);
            return actions.First(x => x.Results.Contains(x.Results.FirstOrDefault(y => y.State.Equals(Frontier.Next))));
        }
    }

    public class UCSAgent<T> : IAgent<T> where T : IGameState
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public UCSAgent(T startState)
        {
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            Frontier.RemoveNext();
            foreach (var action in actions)
            {
                foreach (var next in action.Results)
                {
                    if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                    {
                        Frontier.Add(next.State, 1);
                    }
                }
            }
            Visited.Add(CurrentGameState);
            return actions.First(x => x.Results.Contains(x.Results.FirstOrDefault(y => y.State.Equals(Frontier.Next))));
        }
    }

    public class AStarAgent<T> : IAgent<T> where T : IGameState
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public Func<T, int> Heuristic { get; set; }

        bool Rewinding = false;
        int RewindCounter = 0;

        public AStarAgent(Func<T, int> heuristic, T startState)
        {
            Heuristic = heuristic;
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)//Assuming moves can always be undone, rewrite if not
        {
            if(!Rewinding) Frontier.RemoveNext();
            foreach (var action in actions)
            {
                foreach (var next in action.Results)
                {
                    if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                    {
                        Frontier.Add(next.State, Heuristic(next.State) + Cost);
                    }
                }
            }

            Visited.Add(CurrentGameState);
            if (Visited.Contains(Frontier.Next)) Frontier.RemoveNext();//To prevent loops

            if (actions.Where(x => x.Results.Where(x => x.State.Equals(Frontier.Next)).Count() > 0).Count() > 0)//Frontier.Next is a direct successor
            {
                Rewinding = false;
                return actions.First(x => x.Results.Contains(x.Results.FirstOrDefault(y => y.State.Equals(Frontier.Next))));
            }
            else//Frontier.Next was a successor to a previous state, backtracking
            {
                if (!Rewinding) RewindCounter = 2;
                else RewindCounter++;
                Rewinding = true;
                return actions.First(x => x.Results.Contains(x.Results.FirstOrDefault(y => y.State.Equals(Visited[^RewindCounter]))));
            }
        }
    }

    public class BogoAgent<T> : IAgent<T> where T : IGameState
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
            Frontier.RemoveNext();
            foreach (var action in actions)
            {
                foreach (var next in action.Results)
                {
                    if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                    {
                        Frontier.Add(next.State, random.Next());
                    }
                }
            }
            Visited.Add(CurrentGameState);
            return actions.First(x => x.Results.Contains(x.Results.FirstOrDefault(y => y.State.Equals(Frontier.Next))));
        }
    }

    public interface IFullInfoAgent<T> : IAgent<T> where T : IGameState
    {
        Func<T, List<Akshun<T>>> GetActions { get; set; }
    }

    public class ExpectiMax<T> : IFullInfoAgent<T> where T : IGameState
    {
        class Node
        {
            public T State;
            public float Score;
            public List<(Node node, float chance)> Children;
            public Node(Func<T, List<Akshun<T>>> getActions, T startState, Dictionary<T, Node> prev = null)
            {
                Children = new List<(Node, float chance)>();
                if(prev == null) prev = new Dictionary<T, Node>();
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
                        State.Score = Children.Average(x => x.node.Score * x.chance);
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

    public class MarkovAgent<T> : IFullInfoAgent<T> where T : IGameState
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

    public class QAgent<T> : IAgent<T> where T : IGameState
    {

        public int Cost { get; set; }

        public List<T> Visited { get; set; }

        public IFrontier<T> Frontier => null;

        public T CurrentGameState { get; set; }

        (T state, Akshun<T> action) prev;

        Dictionary<(T state, Akshun<T> action), (float score, float bestScore)> Model;

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
            Visited = new List<T>();
        }

        public Akshun<T> Move(List<Akshun<T>> actions)
        {
            if (!prev.Equals(default))
            {
                //record result of prev move in Model
                if (Model.ContainsKey(prev))
                {
                    Model[prev] = ((Model[prev].score * (1 - LearningRate)) + (LearningRate * (CurrentGameState.Score + (Model[prev].bestScore < CurrentGameState.Score ? CurrentGameState.Score : Model[prev].bestScore)/*maybe with inflation*/)), Model[prev].bestScore);
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
            Visited.Add(CurrentGameState);
            return prev.action;
        }
    }
}