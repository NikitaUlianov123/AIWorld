using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AIWorld
{
    public interface IFrontier<T>
    {
        Vertex<T> Next { get;}

        void Add(Vertex<T> vertex, int priority);

        void RemoveNext();

        bool Contains(Vertex<T> vertex);
        bool Contains(T value);
    }

    public class Frontier<T> : IFrontier<T>
    {
        public PriorityQueue<Vertex<T>, int> frontier;
        public Vertex<T> Next { get => frontier.Peek(); }

        public Frontier()
        {
            frontier = new PriorityQueue<Vertex<T>, int>();
        }

        public void Add(Vertex<T> vertex, int priority)
        {
            frontier.Enqueue(vertex, priority);
        }

        public void RemoveNext()
        {
            frontier.Dequeue();
        }

        public bool Contains(Vertex<T> vertex) => frontier.UnorderedItems.Where(x => x.Element.Value.Equals(vertex.Value)).Count() > 0;
        public bool Contains(T value) => frontier.UnorderedItems.Where(x => x.Element.Value.Equals(value)).Count() > 0;
    }
}