using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface IAgent<T>
    {
        int Cost { get; }

        List<T> Visited { get; }

        IFrontier<T> Frontier { get; }

        T CurrentGameState { get; set; }

        T Move(List<(T state, int cost)> successors);
    }

    public class BFSAgent<T> : IAgent<T>
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

        public T Move(List<(T state, int cost)> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.state) && !Visited.Contains(next.state))
                {
                    Frontier.Add(next.state, next.cost);
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
        }
    }

    public class DFSAgent<T> : IAgent<T>
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

        public T Move(List<(T state, int cost)> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.state) && !Visited.Contains(next.state))
                {
                    Frontier.Add(next.state, -next.cost);
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
        }
    }

    public class UCSAgent<T> : IAgent<T>
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

        public T Move(List<(T state, int cost)> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.state) && !Visited.Contains(next.state))
                {
                    Frontier.Add(next.state, 1);
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
        }
    }

    public class AStarAgent<T> : IAgent<T>
    {
        public int Cost { get; private set; }

        public List<T> Visited { get; private set; }

        public IFrontier<T> Frontier { get; private set; }

        public T CurrentGameState { get; set; }

        public Func<T, int> Heuristic { get; set; }

        public AStarAgent(Func<T, int> heuristic, T startState)
        { 
            Heuristic = heuristic;
            CurrentGameState = startState;
            Frontier = new Frontier<T>();
            Frontier.Add(CurrentGameState, 1);
            Visited = new List<T>();
        }

        public T Move(List<(T state, int cost)> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.state) && !Visited.Contains(next.state))
                {
                    Frontier.Add(next.state, Heuristic(next.state) + Cost);
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
        }
    }

    public class BogoAgent<T> : IAgent<T>
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

        public T Move(List<(T state, int cost)> successors)
        {
            Frontier.RemoveNext();
            foreach (var next in successors)
            {
                if (!Frontier.Contains(next.state) && !Visited.Contains(next.state))
                {
                    Frontier.Add(next.state, random.Next());
                }
            }
            Visited.Add(CurrentGameState);
            return Frontier.Next;
        }
    }
}