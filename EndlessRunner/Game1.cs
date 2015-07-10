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
        float gravity = 0.15f;
        float friction = 0.3f;
        float bgFloor = 450f;
        Vector2 velocity = new Vector2(0, 750);
        float baseVel = 750;
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
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyState.IsKeyDown(Keys.Escape)) Exit();

            // Prevent multiple jumps
            if (keyState.IsKeyUp(Keys.W) && texturePos.Y <= 500)
                hasJumped = true;

            if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
            {
                velocity.Y = baseVel;
                playerJump(keyState, delta);
            }

            // Affect player location with gravity
            // Prevents player from exiting the bottom of window
            if (texturePos.Y <= bgFloor)
            {
                texturePos.Y += velocity.Y * delta * gravity;
                velocity.Y += gravity * 1000;
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

        public void playerJump(KeyboardState state, float dt) {
            // bgFloor is the bottom platform
            if ((bgFloor - texturePos.Y) >= 225f)
            {
                hasJumped = true;
                velocity.Y = baseVel;
            }
            texturePos.Y -= velocity.Y * dt;
        }
    }
}
