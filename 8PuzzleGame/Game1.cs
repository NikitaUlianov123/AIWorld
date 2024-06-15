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

        Graph<GameState> graf;

        int[,] startState;

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
                { 1, 2, 3 },
                { 7, 4, 5 },
                { 8, 0, 6 } };

            graf = new Graph<GameState>();
            var fml = new GameState(invert(startState));
            graf.AddVertex(new Vertex<GameState>(fml, GetNeighborVertecies));

            base.Initialize();
        }

        public List<(Vertex<GameState> destination, int weight)> GetNeighborVertecies(Vertex<GameState> curr)
        { 
            var neighbors = curr.Value.GetNeighbors();
            return neighbors.Select(x => (new Vertex<GameState>(x, GetNeighborVertecies), 1)).ToList();
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

            

            var yeet = graf.Search(new GameState(invert(startState)), new GameState(), new Frontier<GameState>(), GameState.Search);

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
