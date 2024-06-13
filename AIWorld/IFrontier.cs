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

        void Add(Vertex<T> vertex);

        void RemoveNext();

        bool Contains(Vertex<T> vertex);
        bool Contains(T value) => frontier.Where(x => x.Value.Equals(value)).Count() > 0;
    }

    public class BFS<T> : IFrontier<T>
    {
        public ICollection<Vertex<T>> frontier { get => Collections.sort(myList, pq.comparator()); }
        public Vertex<T> Next { get => priorityQueue.Peek(); }

        PriorityQueue<Vertex<T>, int> priorityQueue;

        public BFS()
        { 
            
        }

        public void Add(Vertex<T> vertex)
        {
            priorityQueue.Enqueue(vertex);
        }

        public void RemoveNext()
        {
            priorityQueue.Dequeue();
        }

        bool Contains(Vertex<T> vertex) => frontier.Contains(vertex);
    }
}