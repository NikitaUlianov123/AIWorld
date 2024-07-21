using AIWorld;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheesePuzzle
{
    public class CheeseState
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
                return (Mouse.X < 0 || Mouse.X >= Grid.GetLength(1) || Mouse.Y < 0 || Mouse.Y >= Grid.GetLength(0)) || Grid[Mouse.Y, Mouse.X] != Tile.Empty;
            }
        }

        public float Score
        {
            get
            {
                int reward = 0;
                if (IsTerminal)
                {
                    if (Mouse.X < 0 || Mouse.X >= Grid.GetLength(1) || Mouse.Y < 0 || Mouse.Y >= Grid.GetLength(0))//Out of bounds
                    {
                        reward = -1000;
                    }
                    else
                    {
                        switch (Grid[Mouse.Y, Mouse.X])
                        {
                            case Tile.Wall:
                                reward = -15;
                                break;

                            case Tile.Cheese:
                                reward = 1000;
                                break;

                            case Tile.FirePit:
                                reward = -1000;
                                break;
                        }
                    }
                }
                return reward + (/*Lived * */CostOfLiving);
            }
            set {}
        }

        public Tile[,] Grid;
        public Point Mouse;

        public int Lived;
        public int CostOfLiving;

        public CheeseState()
            : this(-1)
        {
            
        }

        public CheeseState(int costOfLiving)
        {
            //Grid = new Tile[6, 6];
            //Grid[4, 3] = Tile.Cheese;

            //Mouse = new Point(1, 1);

            Mouse = new Point(0, 0);
            Grid = new Tile[10, 10];
            Grid[2, 7] = Tile.Cheese;
            Grid[4, 0] = Tile.FirePit;
            Grid[4, 1] = Tile.FirePit;
            Grid[4, 2] = Tile.FirePit;
            Grid[4, 3] = Tile.FirePit;
            Grid[4, 4] = Tile.FirePit;
            Grid[4, 5] = Tile.FirePit;



            Lived = 0;
            CostOfLiving = costOfLiving;
        }

        public CheeseState(CheeseState prev, Point mouse)
        {
            Grid = Copy(prev.Grid);
            Mouse = mouse;
            Lived = prev.Lived + 1;
            CostOfLiving = prev.CostOfLiving;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(CheeseState)) return false;

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
