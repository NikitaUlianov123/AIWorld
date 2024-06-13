using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public class Vertex<T>
    {
        public T Value;

        public List<(Vertex<T> destination, int weight)> Neighbors;

        public Vertex<T> Founder;

        public Vertex(T value)
        {
            Value = value;
            Neighbors = new List<(AIWorld.Vertex<T> destination, int weight)>();
        }
    }
}
