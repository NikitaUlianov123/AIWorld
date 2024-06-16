using AIWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleGame
{
    public class GameState
    {
        static int defaultValue = 123456780;

        enum Directions { Up, Down, Left, Right }

        public int value
        {
            get
            {
                int result = 100 - (Grid[0, 0] == 1 ? 25 : 0) - (Grid[1, 0] == 2 ? 25 : 0) - (Grid[2, 0] == 3 ? 25 : 0);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Grid[i, j] > 3 && Grid[i, j] != 0)
                        {
                            result -= (Math.Abs(j - (Grid[i, j] / 3)) + Math.Abs(i - (Grid[i, j] % 3)));
                        }
                    }
                }

                return result;
            }
        }
        public int[,] Grid { get; private set; }

        public GameState()
        {
            Grid = new int[3, 3];

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Grid[x, y] = x + y * 3 + 1;
                }
            }
            Grid[2, 2] = 0;
        }

        public GameState(int[,] grid)
        {
            Grid = Copy(grid);
        }

        private GameState(int[,] grid, Directions move)
        {
            Grid = Copy(grid);
            int Zero = FindZero();
            switch (move)
            {
                case Directions.Up:
                    Grid[Zero % 3, Zero / 3] = Grid[Zero % 3, Zero / 3 - 1];
                    Grid[Zero % 3, Zero / 3 - 1] = 0;
                    break;

                case Directions.Down:
                    Grid[Zero % 3, Zero / 3] = Grid[Zero % 3, Zero / 3 + 1];
                    Grid[Zero % 3, Zero / 3 + 1] = 0;
                    break;

                case Directions.Left:
                    Grid[Zero % 3, Zero / 3] = Grid[Zero % 3 - 1, Zero / 3];
                    Grid[Zero % 3 - 1, Zero / 3] = 0;
                    break;

                case Directions.Right:
                    Grid[Zero % 3, Zero / 3] = Grid[Zero % 3 + 1, Zero / 3];
                    Grid[Zero % 3 + 1, Zero / 3] = 0;
                    break;
            }
        }

        public List<GameState> GetNeighbors()
        {
            List<GameState> result = new List<GameState>();
            int Zero = FindZero();

            if (Zero / 3 > 0) result.Add(new GameState(Grid, Directions.Up));
            if (Zero / 3 < 2) result.Add(new GameState(Grid, Directions.Down));
            if (Zero % 3 > 0) result.Add(new GameState(Grid, Directions.Left));
            if (Zero % 3 < 2) result.Add(new GameState(Grid, Directions.Right));

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(GameState)) return false;

            GameState other = obj as GameState;//idk, intelisense suggested it

            for (int i = 0; i < other.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < other.Grid.GetLength(1); j++)
                {
                    if (other.Grid[i, j] != Grid[i, j]) return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            for (var x = 0;x < 3;x ++)
            {
                for (var y = 0;y < 3;y ++)
                {
                    result *= 10;
                    result += Grid[x, y];
                }
            }

            /*for (int i = 0; i < 9; i++)
            {
                result *= 10;
                result += Grid[i % 3, i / 3];
                //result += Grid[i % 3, i / 3] * (int)Math.Pow(10, 8 - i);
            }*/

            return result;
        }

        int FindZero()
        {
            for (int i = 0; i < 10; i++)
            {
                if (Grid[i % 3, i / 3] == 0) return i;
            }
            return 0;
        }

        int[,] Copy(int[,] input)
        {
            int[,] thing = new int[input.GetLength(0), input.GetLength(1)];

            for (int i = 0; i < thing.GetLength(0); i++)
            {
                for (int j = 0; j < thing.GetLength(1); j++)
                {
                    thing[i, j] = input[i, j];
                }
            }
            return thing;
        }

        public static List<(Vertex<GameState> vertex, int priority)> Search(List<(Vertex<GameState> destination, int weight)> neighbors)
        {
            List<(Vertex<GameState> vertex, int priority)> result = new List<(Vertex<GameState> vertex, int priority)>();

            foreach (var neighbor in neighbors)
            {
                result.Add((neighbor.destination, neighbor.destination.Value.value));
            }

            return result;
        }
    }
}