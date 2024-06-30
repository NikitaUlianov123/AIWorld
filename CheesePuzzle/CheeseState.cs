using AIWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheesePuzzle
{
    public class CheeseState : IGameState
    {
        public enum Tile
        { 
            Empty,
            Wall,
            Cheese,
            FirePit
        }

        public bool IsTerminal
        {
            get
            {
                return (Mouse.X < 0 || Mouse.X > Grid.GetLength(1) || Mouse.Y < 0 || Mouse.Y > Grid.GetLength(0)) || Grid[Mouse.Y, Mouse.X] != Tile.Empty;
            }
        }

        public float Score
        {
            get
            {
                int reward = 0;
                if (IsTerminal)
                {
                    switch (Grid[Mouse.Y, Mouse.X])
                    { 
                        case Tile.Wall:
                            reward = 0;
                            break;

                        case Tile.Cheese:
                            reward = 1000;
                            break;

                        case Tile.FirePit:
                            reward = -1000;
                            break;
                    }
                }
                return reward + (Lived * CostOfLiving);
            }
            set {}
        }

        public Tile[,] Grid;
        public Point Mouse;

        int Lived;
        int CostOfLiving;

        public CheeseState(int costOfLiving)
        {
            Grid = new Tile[3, 4];
            Grid[1, 1] = Tile.Wall;
            Grid[0, 3] = Tile.Cheese;
            Grid[1, 3] = Tile.FirePit;

            Mouse = new Point(0, 0);

            Lived = 0;
            CostOfLiving = costOfLiving;
        }

        public CheeseState(Tile[,] grid, Point mouse)
        {
            Grid = Copy(grid);
            Mouse = mouse;
            Lived++;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(CheeseState)) return false;

            CheeseState other = obj as CheeseState;

            for (int i = 0; i < other.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < other.Grid.GetLength(1); j++)
                {
                    if (other.Grid[i, j] != Grid[i, j]) return false;
                }
            }

            return Mouse == other.Mouse;
        }

        public override int GetHashCode()
        {
            return Mouse.GetHashCode();
        }

        Tile[,] Copy<Tile>(Tile[,] input)
        {
            Tile[,] thing = new Tile[input.GetLength(0), input.GetLength(1)];

            for (int i = 0; i < thing.GetLength(0); i++)
            {
                for (int j = 0; j < thing.GetLength(1); j++)
                {
                    thing[i, j] = input[i, j];
                }
            }
            return thing;
        }
    }
}
