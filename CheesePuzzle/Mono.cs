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
                var agent = (QAgent<MouseSensors>)(Runner.agents[0]);
                agent.LearningRate = value;
            }
        }

        public float Epsilon
        {
            set
            {
                var agent = (QAgent<MouseSensors>)(Runner.agents[0]);
                agent.Epsilon = value;
            }
        }

        public bool ShowQ = false;

        public AgentRunner<MouseSensors> Runner;
        public TimeSpan delay;
        TimeSpan elapsed;

        protected override void Initialize()
        {
            delay = TimeSpan.Zero;
            elapsed = TimeSpan.Zero;
            Runner = new AgentRunner<MouseSensors>(new CheeseEnvironment(), new QAgent<MouseSensors>(new MouseSensors(new CheeseState(-1)), 0.3f, 0.05f));

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

            int tileSize = Editor.GraphicsDevice.Viewport.Width / ((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Grid.GetLength(1);
            Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

            var QValues = Normalize(GetQValues(), 0, 1);

            for (int x = 0; x < ((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Grid.GetLength(1); x++)
            {
                for (int y = 0; y < ((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Grid.GetLength(0); y++)
                {
                    switch (((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Grid[y, x])
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
                                color = new Microsoft.Xna.Framework.Color(Microsoft.Xna.Framework.Color.Black, QValues[y, x]);
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
            Editor.spriteBatch.FillRectangle(new Microsoft.Xna.Framework.Rectangle(((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Mouse.X * tileSize + 10, ((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Mouse.Y * tileSize + 10, tileSize - 20, tileSize - 20), Microsoft.Xna.Framework.Color.Sienna);

            Editor.spriteBatch.End();
        }


        public float[,] GetQValues()
        {
            float[,] values = new float[((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Grid.GetLength(0), ((CheeseEnvironment)(Runner.environment)).AgentInfo[0].Grid.GetLength(1)];

            var Model = ((QAgent<MouseSensors>)(Runner.agents[0])).Model;

            for (int y = 0; y < values.GetLength(0); y++)
            {
                for (int x = 0; x < values.GetLength(1); x++)
                {
                    //Moves that result in this state:
                    var Moves = new List<Akshun<MouseSensors>>();
                    foreach (var action in Model)
                    {
                        if (action.Key.action.Results[0].State.values[0] == x && action.Key.action.Results[0].State.values[1] == y)
                        {
                            values[y, x] += action.Value.score;
                        }
                    }
                }
            }

            return values;
        }

        public float[,] Normalize(float[,] input, float nMin, float nMax)
        {
            float min = float.MaxValue;
            float max = float.MinValue;

            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    if (input[y, x] < min) min = input[y, x];

                    if (input[y, x] > max) max = input[y, x];
                }
            }

            float[,] result = new float[input.GetLength(0), input.GetLength(1)];
            if (min == max) return result;
            for (int y = 0; y < input.GetLength(0); y++)
            {
                for (int x = 0; x < input.GetLength(1); x++)
                {
                    result[y, x] = (((input[y, x] - min) / (max - min)) * (nMax - nMin)) + nMin;
                }
            }

            return result;
        }
    }
}