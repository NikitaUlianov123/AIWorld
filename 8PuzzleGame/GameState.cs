using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleGame
{
    public class GameState
    {
        enum Directions { Up, Down, Left, Right }

        public int value
        {
            get
            {
                int result = 0;
                for (int i = 0; i < 10; i++)
                {
                    result += Grid[i % 3, i / 3] * (int)Math.Pow(10, 9 - i);
                }

                return result;
            }
        }
        int[,] Grid;

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
        }

        private GameState(int[,] Grid, Directions move)
        {
            Grid.CopyTo(this.Grid, 0);
            int Zero = FindZero();
            switch (move)
            {
                case Directions.Up:
                    Grid[Zero % 3, Zero / 3] = Grid[Zero % 3, Zero / 3 + 1];
                    Grid[Zero % 3, Zero / 3 + 1] = 0;
                    break;

                case Directions.Down:
                    Grid[Zero % 3, Zero / 3] = Grid[Zero % 3, Zero / 3 - 1];
                    Grid[Zero % 3, Zero / 3 - 1] = 0;
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
            if (Zero / 3 > 0) result.Add(new GameState(Grid, Directions.Down));
            if (Zero % 3 > 0) result.Add(new GameState(Grid, Directions.Left));
            if (Zero % 3 < 2) result.Add(new GameState(Grid, Directions.Right));

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
    }
}