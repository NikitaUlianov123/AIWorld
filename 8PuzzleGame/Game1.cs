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
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            runner.DoTurn();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
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
    }
}
