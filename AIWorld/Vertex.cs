using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public class Vertex<T>
    {
        public T Value;

        public List<(Vertex<T> destination, int weight)> Neighbors;

        Func<Vertex<T>, List<(Vertex<T> destination, int weight)>> getNeighbors;
        public Vertex(T value)
        {
            Value = value;
            //Make this work when in possesion of more sanity:
            //if (typeof(T).GetMembers().First(x => x.Name == "GetNeighbors") != null)
            //{
            //    getNeighbors = ;
            //    Expression.Call(value, typeof(T).GetMembers().First(x => x.Name == "GetNeighbors"));
            //}
            Neighbors = new List<(AIWorld.Vertex<T> destination, int weight)>();
        }

        public Vertex(T value, Func<Vertex<T>, List<(Vertex<T> destination, int weight)>> getNeighbors)
        {
            this.Value = value;
            this.getNeighbors = getNeighbors;
        }

        public List<(Vertex<T> destination, int weight)> GetNeighbors()
        { 
            if(Neighbors != null) return Neighbors;

            return getNeighbors(this);
        }

        public override bool Equals(object? obj)
        {
            if(obj.GetType() != typeof(Vertex<T>)) return false;

            var other = obj as Vertex<T>;//also intellisense

            return other.Value.Equals(Value);// && other.Neighbors.SequenceEqual(Neighbors);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
