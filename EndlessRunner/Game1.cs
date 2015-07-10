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
        float gravity = 0.06f;
        float friction = 0.3f;
        float bgFloor = 450f;
        Vector2 velocity = new Vector2(10, 10);
        bool hasJumped = false;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 540;
            graphics.PreferredBackBufferWidth = 960;
            Content.RootDirectory = "Content";

            // base
            texturePos = new Vector2(50, bgFloor);
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

            // Prevent multiple jumps
            if (keyState.IsKeyUp(Keys.W) && texturePos.Y <= 500)
                hasJumped = true;

            if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
            {
                velocity.Y = 10;
                playerJump(keyState);
            }

            // Affect player location with gravity
            // Prevents player from exiting the bottom of window
            if (texturePos.Y <= bgFloor)
            {
                texturePos.Y += velocity.Y * (gravity);
                velocity.Y += gravity*45;
            }
            else
            {
                hasJumped = false;
            }

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

        public void playerJump(KeyboardState state) {
            // bgFloor is the bottom platform
            if ((bgFloor - texturePos.Y) >= 100f)
            {
                hasJumped = true;
                velocity.Y = 10;
            }
            texturePos.Y -= velocity.Y;
        }
    }
}
