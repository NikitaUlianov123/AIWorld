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

        Func<List<(Vertex<T> destination, int weight)>> getNeighbors;
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

        public Vertex(T value, Func<List<(Vertex<T> destination, int weight)>> getNeighbors)
        {
            this.Value = value;
            this.getNeighbors = getNeighbors;
        }

        public List<(Vertex<T> destination, int weight)> GetNeighbors()
        { 
            if(Neighbors != null) return Neighbors;

            return getNeighbors();
        }
    }
}
