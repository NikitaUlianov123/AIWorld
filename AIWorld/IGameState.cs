using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface IGameState
    {
        bool IsTerminal { get; }

        float Score { get; set; }
    }

    public struct Akshun<T> where T : IGameState
    {
        public T Start;
        public List<Successor<T>> Results;

        public Akshun(T start, List<Successor<T>> results)
        {
            Start = start;
            Results = results;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj.GetType() != typeof(Akshun<T>)) return false;

            var other = (Akshun<T>)obj;

            if (!Start.Equals(other.Start)) return false;

            for (int i = 0; i < Results.Count; i++)
            {
                if (!Results[i].Equals(other.Results[i])) return false;
            }

            return true;
        }
    }

    public struct Successor<T> where T : IGameState
    {
        public T State;
        public int Cost;
        public float Chance;

        public Successor(T state, int cost, float chance)
        {
            State = state;
            Cost = cost;
            Chance = chance;
        }
    }
}
