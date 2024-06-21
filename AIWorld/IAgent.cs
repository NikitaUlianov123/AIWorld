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

        T Move(List<Successor<T>> successors);
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

        public T Move(List<Successor<T>> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                {
                    Frontier.Add(next.State, next.Cost);
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
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

        public T Move(List<Successor<T>> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                {
                    Frontier.Add(next.State, -next.Cost);
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
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

        public T Move(List<Successor<T>> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                {
                    Frontier.Add(next.State, 1);
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
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

        public T Move(List<Successor<T>> successors)//Assuming moves can always be undone, rewrite if not
        {
            if(!Rewinding) Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                {
                    Frontier.Add(next.State, Heuristic(next.State) + Cost);
                }
            }

            Visited.Add(CurrentGameState);
            if (Visited.Contains(Frontier.Next)) Frontier.RemoveNext();//To prevent loops

            if (successors.Where(x => x.State.Equals(Frontier.Next)).Count() > 0)//Frontier.Next is a direct successor
            {
                Rewinding = false;
                return Frontier.Next;
            }
            else//Frontier.Next was a successor to a previous state, backtracking
            {
                if (!Rewinding) RewindCounter = 2;
                else RewindCounter++;
                Rewinding = true;
                return Visited[^RewindCounter];
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

        public T Move(List<Successor<T>> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.State) && !Visited.Contains(next.State))
                {
                    Frontier.Add(next.State, random.Next());
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
        }
    }

    public interface IFullInfoAgent<T> : IAgent<T> where T : IGameState
    {
        Func<T, List<Successor<T>>> GetSuccessors { get; set; }
    }

    public class ExpectiMax<T> : IFullInfoAgent<T> where T : IGameState
    {
        class Node<t> where t : IGameState
        {
            public t State;
            public List<(Node<t>, float chance)> Successors;
            public Node(Func<t, List<Successor<t>>> successors, t startState)
            {
                State = startState;
                if (!State.IsTerminal)
                { 
                    var next = successors(State);
                    foreach (var nex in next)
                    {
                        Successors.Add((new Node<t>(successors, nex.State), nex.Chance));
                    }
                    State.Score = Successors.Max(x => x.Item1.State.Score * x.Item2);
                }
            }
        }

        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get => node.State; set => node = node.Successors.First(x => x.Item1.State.Equals(CurrentGameState)).Item1; }

        public Func<T, List<Successor<T>>> GetSuccessors { get; set; }

        Node<T> node;

        public ExpectiMax(Func<T, List<Successor<T>>> successors, T startState)
        {
            GetSuccessors = successors;
            node = new Node<T>(successors, startState);
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public T Move(List<Successor<T>> successors)
        {
            int Best = 0;
            for (var i = 0; i < node.Successors.Count; i++)
            {
                if (node.Successors[i].Item1.State.Score > node.Successors[Best].Item1.State.Score) Best = i;
            }

            return node.Successors[Best].Item1.State;
        }
    }
}