using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public interface ISensorReading
    {
        byte[] values { get; }

        bool IsTerminal { get; }

        float Score { get; }
    }

    public struct Akshun<T> where T : ISensorReading
    {
        public T Start;
        public List<Successor<T>> Results;
        public string Name;

        public Akshun(T start, List<Successor<T>> results, string name)
        {
            Start = start;
            Results = results;
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            if(obj == null) return Start == null && Results == null;

            if (obj.GetType() != typeof(Akshun<T>)) return false;

            var other = (Akshun<T>)obj;

            if(Start == null) return other.Start == null;

            if (!Start.Equals(other.Start)) return false;

            if(Results == null) return other.Results == null;

            for (int i = 0; i < Results.Count; i++)
            {
                if (!Results[i].Equals(other.Results[i])) return false;
            }

            return true;
        }
    }

    public struct Successor<T> where T : ISensorReading
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
