﻿using AIWorld;

using MonoGame.Extended.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleGame
{
    public class EightPuzzle : IEnvironment<EightState>
    {
        public EightState TargetState => new EightState();

        public Dictionary<int, EightState> AgentInfo = new Dictionary<int, EightState>();

        public void AddAgent(int ID, EightState state) => AgentInfo.Add(ID, state);

        public List<Akshun<EightState>> GetActions(int agentID)
        {
            return GetActions(AgentInfo[agentID]);
        }

        public List<Akshun<EightState>> GetActions(EightState state)
        {
            var neighbors = state.GetNeighbors();
            List<Akshun<EightState>> result = new List<Akshun<EightState>>();

            foreach (var item in neighbors)
            {
                result.Add(new Akshun<EightState>(state, [new Successor<EightState>(item.state, 1, 1)], item.name));
            }

            return result;
        }

        public EightState MakeMove(Akshun<EightState> move, int agentID)
        {
            if (GetActions(agentID).Where(x => x.Equals(move)).Count() > 0)
            {
                AgentInfo[agentID] = move.Results[0].State;
                return move.Results[0].State;
            }
            throw new InvalidOperationException("Cannot move to requested spot");
        }
    }

    public class EightState : ISensorReading
    {
        enum Directions { Up, Down, Left, Right }

        public int[,] Grid { get; private set; }

        public bool IsTerminal
        {
            get
            {
                var solved = new EightState();
                return Score == solved.Score;
            }
        }

        public float Score
        {
            get
            {
                int result = 100 - (Grid[0, 0] == 1 ? 25 : 0) - (Grid[1, 0] == 2 ? 25 : 0) - (Grid[2, 0] == 3 ? 25 : 0);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Grid[i, j] > 3)
                        {
                            result += (Math.Abs(j - ((Grid[i, j] - 1) / 3)) + Math.Abs(i - ((Grid[i, j] - 1) % 3)));
                        }
                    }
                }

                return result;
            }
        }

        public byte[] values
        {
            get
            {
                byte[,] result = new byte[Grid.GetLength(0), Grid.GetLength(1)];
                for (int i = 0; i <= Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        result[i, j] = (byte)Grid[i, j];
                    }
                }

                return result.Cast<byte>().Select(x => x).ToArray();
            }
        }

        public EightState()
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

        public EightState(int[,] grid)
        {
            Grid = Copy(grid);
        }

        private EightState(int[,] grid, Directions move)
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

        public List<(EightState state, string name)> GetNeighbors()
        {
            List<(EightState, string)> result = new List<(EightState, string)>();
            int Zero = FindZero();

            if (Zero / 3 > 0) result.Add((new EightState(Grid, Directions.Up), "Up"));
            if (Zero / 3 < 2) result.Add((new EightState(Grid, Directions.Down), "Down"));
            if (Zero % 3 > 0) result.Add((new EightState(Grid, Directions.Left), "Left"));
            if (Zero % 3 < 2) result.Add((new EightState(Grid, Directions.Right), "Right"));

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(EightState)) return false;

            EightState other = obj as EightState;//idk, intelisense suggested it

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
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
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
    }
}