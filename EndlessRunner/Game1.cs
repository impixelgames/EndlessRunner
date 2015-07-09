using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace EndlessRunner
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 texturePos;
        KeyboardState previousState;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 540;
            graphics.PreferredBackBufferWidth = 960;
            Content.RootDirectory = "Content";

            // base
            texturePos = new Vector2(50, 500);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = this.Content.Load<Texture2D>("fishie");
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {


            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape)) Exit();

            // Player Movement
            if (state.IsKeyDown(Keys.D))
                texturePos.X += 10;
            if (state.IsKeyDown(Keys.A))
                texturePos.X -= 10;

            // Previous state requires discrete presses, W cannot be held down
            if (state.IsKeyDown(Keys.W) && !previousState.IsKeyDown(Keys.W))
                texturePos.Y -= 10;

            base.Update(gameTime);
            previousState = state;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(texture, texturePos);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
