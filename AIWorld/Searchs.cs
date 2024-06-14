using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIWorld
{
    public static class Searchs
    {
        public static List<(Vertex<T> vertex, int priority)> BFS<T>(List<(Vertex<T> destination, int weight)> neighbors)
        {
            List<(Vertex<T> vertex, int priority)> result = new List<(Vertex<T> vertex, int priority)>();

            foreach (var neighbor in neighbors)
            {
                result.Add((neighbor.destination, 1));
            }

            return result;
        }

        public static List<(Vertex<T> vertex, int priority)> DFS<T>(List<(Vertex<T> destination, int weight)> neighbors)
        {
            List<(Vertex<T> vertex, int priority)> result = new List<(Vertex<T> vertex, int priority)>();

            foreach (var neighbor in neighbors)
            {
                //result.Add((neighbor, 1));
            }

            return result;
        }

        public static List<(Vertex<T> vertex, int priority)> UCS<T>(List<(Vertex<T> destination, int weight)> neighbors)
        {
            List<(Vertex<T> vertex, int priority)> result = new List<(Vertex<T> vertex, int priority)>();

            foreach (var neighbor in neighbors)
            {
                result.Add((neighbor.destination, neighbor.weight));
            }

            return result;
        }
    }
}
