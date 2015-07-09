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

        // game variables
        float gravity = 0.04f;
        float friction = 0.3f;
        Vector2 velocity = new Vector2(10, 125);
        bool hasJumped = false;
        
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
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape)) Exit();

            // Player Movement
            if (keyState.IsKeyDown(Keys.D))
                texturePos.X += velocity.X;
            if (keyState.IsKeyDown(Keys.A))
                texturePos.X -= velocity.X;

            // Previous state requires discrete presses, W cannot be held down
            if (keyState.IsKeyDown(Keys.W) && !previousState.IsKeyDown(Keys.W) && hasJumped == false)
            {
                texturePos.Y -= velocity.Y;
                hasJumped = true;
            }

            if (texturePos.Y >= 500)
            {
                hasJumped = false;
                texturePos.Y = 500;
            }

            // gravity and friction
            texturePos.Y += velocity.Y * gravity;

            base.Update(gameTime);
            previousState = keyState;
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
