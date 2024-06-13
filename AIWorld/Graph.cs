using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public void AddEdge(T start, T end, int weight)
        {
            vertices.First(x => x.Value.Equals(start)).Neighbors.Add((vertices.First(y => y.Value.Equals(end)), weight));
        }


        public List<Vertex<T>> Search(T start, T end, IFrontier<T> frontier)
        {
            List<Vertex<T>> visited = new List<Vertex<T>>();
            Dictionary<Vertex<T>, Vertex<T>> founders = new Dictionary<Vertex<T>, Vertex<T>>();//Node, founder

            var first = vertices.First(x => x.Value.Equals(start));
            frontier.Add(first);
            visited.Add(first);


            while (!frontier.Contains(end))
            {
                GoNext();
            }

            List<Vertex<T>> result = new List<Vertex<T>>();
            var current = vertices.First(y => y.Value.Equals(end));//fix later
            while (current != null)
            {
                result.Insert(0, current);
                current = founders[current];
            }

            return result;

            void GoNext()
            {
                foreach (var neighbor in frontier.Next.Neighbors)
                {
                    frontier.Add(neighbor.destination);
                    founders.Add(frontier.Next, neighbor.destination);
                }
                frontier.RemoveNext();
            }
        }
    }
}