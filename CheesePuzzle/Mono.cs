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

        public AgentRunner<CheeseState> Runner;
        public TimeSpan delay;
        TimeSpan elapsed;

        protected override void Initialize()
        {
            delay = TimeSpan.Zero;
            elapsed = TimeSpan.Zero;
            Runner = new AgentRunner<CheeseState>(new CheeseEnvironment(), new QAgent<CheeseState>(new CheeseState(-1), 0.4f, 0.5f));

            for (int i = 0; i < 10000; i++)
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
                            color = Microsoft.Xna.Framework.Color.White;
                            break;
                    }

                    Editor.spriteBatch.FillRectangle(new Microsoft.Xna.Framework.Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), color);

                    Editor.spriteBatch.DrawRectangle(new Microsoft.Xna.Framework.Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Microsoft.Xna.Framework.Color.Black);

                }
            }
            Editor.spriteBatch.FillRectangle(new Microsoft.Xna.Framework.Rectangle(Runner.agents[0].CurrentGameState.Mouse.X * tileSize + 10, Runner.agents[0].CurrentGameState.Mouse.Y * tileSize + 10, tileSize - 20, tileSize - 20), Microsoft.Xna.Framework.Color.Gray);
            
            Editor.spriteBatch.End();
        }
    }
}
