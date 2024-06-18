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
        T Next { get;}

        void Add(T state, int priority);

        void RemoveNext();

        bool Contains(T state);
    }

    public class Frontier<T> : IFrontier<T>
    {
        public PriorityQueue<T, int> frontier;
        public T Next { get => frontier.Peek(); }

        public Frontier()
        {
            frontier = new PriorityQueue<T, int>();
        }

        public void Add(T state, int priority)
        {
            frontier.Enqueue(state, priority);
        }

        public void RemoveNext()
        {
            frontier.Dequeue();
        }

        public bool Contains(T state) => frontier.UnorderedItems.Where(x => x.Equals(state)).Count() > 0;
    }
}