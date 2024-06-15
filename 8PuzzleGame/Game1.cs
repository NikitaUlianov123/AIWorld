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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            graf = new Graph<GameState>();
            var fml = new GameState();
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

            var yeet = graf.Search(new GameState(), new GameState(new int[,] { { 1, 4, 0 }, { 2, 5, 7 }, { 3, 6, 8 } }), new Frontier<GameState>(), Searchs.BFS);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
