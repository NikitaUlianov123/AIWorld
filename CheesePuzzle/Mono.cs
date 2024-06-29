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
        public AgentRunner<CheeseState> Runner;
        protected override void Initialize()
        {
            Runner = new AgentRunner<CheeseState>(new CheeseEnvironment(), new QAgent<CheeseState>(new CheeseState()));
        }

        protected override void Update(GameTime gameTime)
        {
            Runner.DoTurn();
        }

        protected override void Draw()
        {
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
            Editor.spriteBatch.End();
        }
    }
}
