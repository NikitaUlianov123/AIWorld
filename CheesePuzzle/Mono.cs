using AIWorld;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

namespace CheesePuzzle
{
    public class Mono : MonoGame.Forms.NET.Controls.MonoGameControl
    {
        public float LearningRate
        {
            set
            {
                var agent = (QAgent<CheeseState>)(Runner.agents[0]);
                agent.LearningRate = value;
            }
        }

        public float Epsilon
        {
            set
            {
                var agent = (QAgent<CheeseState>)(Runner.agents[0]);
                agent.Epsilon = value;
            }
        }

        public bool ShowQ = false;

        public AgentRunner<CheeseState> Runner;
        public TimeSpan delay;
        TimeSpan elapsed;

        protected override void Initialize()
        {
            delay = TimeSpan.Zero;
            elapsed = TimeSpan.Zero;
            Runner = new AgentRunner<CheeseState>(new CheeseEnvironment(), new QAgent<CheeseState>(new CheeseState(-1), 0.3f, 0.1f));

            for (int i = 0; i < 1000; i++)
            {
                Runner.DoTurn();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (elapsed > delay)
            {
                Runner.DoTurn();
                elapsed = TimeSpan.Zero;
            }
            elapsed += gameTime.ElapsedGameTime;
        }

        protected override void Draw()
        {
            Editor.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);

            Editor.spriteBatch.Begin();

            int tileSize = Editor.GraphicsDevice.Viewport.Width / Runner.agents[0].CurrentGameState.Grid.GetLength(1);
            Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

            var QValues = Normalize(GetQValues(), 0, 255);

            for (int x = 0; x < Runner.agents[0].CurrentGameState.Grid.GetLength(1); x++)
            {
                for (int y = 0; y < Runner.agents[0].CurrentGameState.Grid.GetLength(0); y++)
                {
                    switch (Runner.agents[0].CurrentGameState.Grid[y, x])
                    {
                        case CheeseState.Tile.Wall:
                            color = Microsoft.Xna.Framework.Color.Black;
                            break;

                        case CheeseState.Tile.Cheese:
                            color = Microsoft.Xna.Framework.Color.Yellow;
                            break;

                        case CheeseState.Tile.FirePit:
                            color = Microsoft.Xna.Framework.Color.Firebrick;
                            break;

                        default:
                            if (ShowQ)
                            {
                                color = new Microsoft.Xna.Framework.Color(Microsoft.Xna.Framework.Color.Black, 255 - QValues[y, x]);
                            }
                            else
                            {
                                color = Microsoft.Xna.Framework.Color.White;
                            }
                            break;
                    }

                    Editor.spriteBatch.FillRectangle(new Microsoft.Xna.Framework.Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), color);

                    Editor.spriteBatch.DrawRectangle(new Microsoft.Xna.Framework.Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Microsoft.Xna.Framework.Color.Black);

                }
            }
            //Mouse
            Editor.spriteBatch.FillRectangle(new Microsoft.Xna.Framework.Rectangle(Runner.agents[0].CurrentGameState.Mouse.X * tileSize + 10, Runner.agents[0].CurrentGameState.Mouse.Y * tileSize + 10, tileSize - 20, tileSize - 20), Microsoft.Xna.Framework.Color.Sienna);

            Editor.spriteBatch.End();
        }


        public int[,] GetQValues()
        {
            int[,] values = new int[Runner.agents[0].CurrentGameState.Grid.GetLength(0), Runner.agents[0].CurrentGameState.Grid.GetLength(1)];

            var Model = ((QAgent<CheeseState>)(Runner.agents[0])).Model;

            for (int y = 0; y < values.GetLength(0); y++)
            {
                for (int x = 0; x < values.GetLength(1); x++)
                {
                    //Moves that result in this state:
                    var Moves = new List<Akshun<CheeseState>>();
                    foreach (var action in Model)
                    {
                        if (action.Key.action.Results[0].State.Mouse.X == x && action.Key.action.Results[0].State.Mouse.Y == y)
                        {
                            values[y, x] += (int)(action.Value.score);
                        }
                    }
                }
            }

            return values;
        }

        public int[,] Normalize(int[,] input, int nMin, int nMax)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    if (input[y, x] < min && input[y, x] != int.MinValue) min = input[y, x];

                    if (input[y, x] > max && input[y, x] != int.MaxValue) max = input[y, x];
                }
            }

            int[,] result = new int[input.GetLength(0), input.GetLength(1)];
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    if (input[y, x] == int.MinValue || input[y, x] == int.MaxValue) result[y, x] = input[y, x];

                    else
                    {
                        result[y, x] = (((input[y, x] - min) / (max - min)) * (nMax - nMin)) + nMin;
                    }
                }
            }

            return result;
        }
    }
}
