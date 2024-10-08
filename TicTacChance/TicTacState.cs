﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIWorld;

namespace TicTacChance
{
    public class TicTacState : ISensorReading
    {
        public enum State
        {
            Gaming,
            XWin,
            OWin,
            Tie,
        };

        public bool IsTerminal
        {
            get
            {
                CheckGameOver();
                return state != State.Gaming;
            }
        }

        public float Score { get; set; }

        public byte[] values
        {
            get
            {
                byte[] result = new byte[Grid.Length];

                for (int i = 0; i < Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        switch (Grid[i, j])
                        {
                            case null:
                                result[(i * Grid.GetLength(0)) + j] = 0;
                                break;

                            case true:
                                result[(i * Grid.GetLength(0)) + j] = 1;
                                break;

                            case false:
                                result[(i * Grid.GetLength(0)) + j] = 2;
                                break;
                        }
                    }
                }

                return result;
            }
        }

        State state;

        public bool?[,] Grid; //X = true, O = false

        public bool Turn = true;

        public Dictionary<bool, bool> Missed;

        public TicTacState()
        {
            Grid = new bool?[3, 3];
            Missed = new Dictionary<bool, bool>();
            Missed.Add(true, false);
            Missed.Add(false, false);
        }

        public TicTacState(bool?[,] grid, bool currTurn, bool missed, Dictionary<bool, bool> prevMissed, (int x, int y) move)
        {
            Grid = Copy(grid);
            if(!missed) Grid[move.x, move.y] = currTurn;


            Missed = new Dictionary<bool, bool>();
            Missed.Add(true, prevMissed[true]);
            Missed.Add(false, prevMissed[false]);

            Missed[currTurn] = missed;

            Turn = !currTurn;
        }
        public TicTacState(TicTacState ticTacState)
        {
            Grid = Copy(ticTacState.Grid);
            Turn = ticTacState.Turn;
            Missed = ticTacState.Missed;
        }

        public void setValue()
        {
            switch (state)
            {
                case State.XWin:
                    Score = 1;
                    break;

                case State.OWin:
                    Score = -1;
                    break;

                case State.Tie:
                    Score = 0;
                    break;

                default:
                    Score = 0;
                    break;
            }
        }

        public void CheckGameOver()
        {
            //Horizontal lines
            bool XWin = true;
            bool OWin = true;
            for (int y = 0; y < 3; y++)
            {
                XWin = true;
                OWin = true;
                for (int x = 0; x < 3; x++)
                {
                    if (Grid[y, x] != true) XWin = false;
                    if (Grid[y, x] != false) OWin = false;
                }
                if (XWin) state = State.XWin;
                if (OWin) state = State.OWin;

                if (XWin || OWin)
                {
                    setValue();
                    return;
                }
            }


            //Vertical lines
            for (int x = 0; x < 3; x++)
            {
                XWin = true;
                OWin = true;
                for (int y = 0; y < 3; y++)
                {
                    if (Grid[y, x] != true) XWin = false;
                    if (Grid[y, x] != false) OWin = false;
                }
                if (XWin) state = State.XWin;
                if (OWin) state = State.OWin;

                if (XWin || OWin)
                {
                    setValue();
                    return;
                }
            }


            //Diagonal lines
            bool diagonal = true;
            var g = Grid[0, 0];
            //down right diagonal
            if (g != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Grid[i, i] != g)
                    {
                        diagonal = false;
                    }
                }
                if (diagonal)
                {
                    if (g == false) state = State.OWin;
                    else state = State.XWin;
                    setValue();
                    return;
                }
            }

            //down left diagonal
            diagonal = true;
            var h = Grid[0, 3 - 1];
            if (h != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Grid[i, 3 - (1 + i)] != h)
                    {
                        diagonal = false;
                    }
                }
                if (diagonal)
                {
                    if (h == false) state = State.OWin;
                    else state = State.XWin;
                    setValue();
                    return;
                }
            }


            //Tie
            bool tie = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Grid[i, j] == null) tie = false;
                }
            }
            if(tie) state = State.Tie;
            setValue();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(TicTacState)) return false;

            TicTacState other = obj as TicTacState;

            for (int i = 0; i < other.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < other.Grid.GetLength(1); j++)
                {
                    if (other.Grid[i, j] != Grid[i, j]) return false;
                }
            }

            return Missed[true] == other.Missed[true] && Missed[false] == other.Missed[false] && Turn == other.Turn;
        }

        public override int GetHashCode()
        {
            int result = 1;
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (Grid[y, x] == true) result += 1;
                    else if (Grid[y, x] == false) result += 2;
                    result *= 10;
                }
            }

            if (Missed[true]) result += 1;
            if (Missed[false]) result += 2;
            if (Turn) result += 4;

            return result;
        }

        T[,] Copy<T>(T[,] input)
        {
            T[,] thing = new T[input.GetLength(0), input.GetLength(1)];

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