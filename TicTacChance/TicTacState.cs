using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIWorld;

namespace TicTacChance
{
    public class TicTacState : IGameState
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

        State state;

        public bool?[,] Grid; //X = true, O = false

        public bool Turn = true;

        public bool missed = false;

        public TicTacState()
        {
            Grid = new bool?[3,3];
        }

        public TicTacState(bool?[,] grid)
        {
            Grid = grid;
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
        }
    }
}
