﻿using System;
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
                throw new NotImplementedException("Sometimes throws out of range exception");
                int result = 0;
                for (int i = 0; i < 10; i++)
                {
                    result += Grid[i % 3, i / 3] * (int)Math.Pow(10, 9 - i);
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
    }
}