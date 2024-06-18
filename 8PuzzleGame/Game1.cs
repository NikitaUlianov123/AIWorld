using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

using AIWorld;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;


namespace _8PuzzleGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        SpriteFont font;
        TimeSpan timer;


        int[,] startState;
        AgentRunner<GameState> runner;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            timer = TimeSpan.Zero;


            startState = new int[,]{
                //{ 1, 2, 3 },
                //{ 7, 4, 5 },
                //{ 8, 0, 6 } };
                { 4, 1, 2 },
                { 7, 5, 3 },
                { 0, 8, 6 } };

            var agent = new AStarAgent<GameState>(x => x.value, new GameState(invert(startState)));

            runner = new AgentRunner<GameState>(new EightPuzzle(), agent);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            timer += gameTime.ElapsedGameTime;

            if (true)//timer.Seconds >= 1)
            {
                runner.DoTurn();
                timer = TimeSpan.Zero;
            }

            base.Update(gameTime);
        }

        int[,] invert(int[,] input)
        {
            int[,] output = new int[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    output[i, j] = input[j, i];
                }
            }
            return output;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    spriteBatch.FillRectangle(new Rectangle(x * 100, y * 100, 100, 100), runner.agents[0].CurrentGameState.Grid[x, y] == 0 ? Color.BlanchedAlmond : Color.Peru);
                    spriteBatch.DrawString(font, runner.agents[0].CurrentGameState.Grid[x, y].ToString(), new Vector2((x * 100) + 50, (y * 100) + 50), Color.Black);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
