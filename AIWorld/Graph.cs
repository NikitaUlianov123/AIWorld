using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public class Graph<T>
    {
        List<Vertex<T>> vertices;

        public Graph()
        {
            vertices = new List<Vertex<T>>();
        }

        public void AddVertex(T value)
        {
            vertices.Add(new Vertex<T>(value));
        }
        public void AddVertex(Vertex<T> vertex)
        {
            vertices.Add(vertex);
        }

        public void AddEdge(T start, T end, int weight)
        {
            vertices.First(x => x.Value.Equals(start)).Neighbors.Add((vertices.First(y => y.Value.Equals(end)), weight));
        }

        public void AddDoubleEdge(T start, T end, int weight)
        {
            AddEdge(start, end, weight);
            AddEdge(end, start, weight);
        }

        public List<Vertex<T>> Search(T start, T end, IFrontier<T> frontier, Func<List<(Vertex<T> destination, int weight)>, List<(Vertex<T> vertex, int priority)>> search)
        {
            List<Vertex<T>> visited = new List<Vertex<T>>();
            Dictionary<Vertex<T>, Vertex<T>> founders = new Dictionary<Vertex<T>, Vertex<T>>();//Node, founder

            var first = vertices.First(x => x.Value.Equals(start));
            frontier.Add(first, 0);
            visited.Add(first);

            
            while (!frontier.Next.Value.Equals(end))
            {
                var priorities = search(frontier.Next.GetNeighbors());
                foreach (var neighbor in priorities)
                {
                    if (!frontier.Contains(neighbor.vertex) && !visited.Contains(neighbor.vertex))
                    {
                        frontier.Add(neighbor.vertex, neighbor.priority);
                    }
                    if (founders.ContainsKey(neighbor.vertex))
                    {
                        if (founders[neighbor.vertex].GetNeighbors().First(x => x.destination == neighbor.vertex).weight
                            > frontier.Next.GetNeighbors().First(x => x.destination == neighbor.vertex).weight)
                        {
                            founders[neighbor.vertex] = frontier.Next;
                        }
                    }
                    else
                    {
                        founders.Add(neighbor.vertex, frontier.Next);
                    }
                }
                visited.Add(frontier.Next);
                frontier.RemoveNext();
            }

            List<Vertex<T>> result = new List<Vertex<T>>();
            var current = frontier.Next;
            while (founders.ContainsKey(current))
            {
                result.Insert(0, current);
                current = founders[current];
            }
            result.Insert(0, first);
            return result;
        }
    }
}