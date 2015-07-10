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
        Sprite player = new Sprite();
        KeyboardState previousState;

        // Constants
        float gravity = 0.15f;
        float bgFloor = 450f;
        float baseVel = 750;

        // Player Variables
        Vector2 velocity = new Vector2(0, 750);
        bool hasJumped = false;
        
        // obstacles
        //Obstacle obs = new Obstacle();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 540;
            graphics.PreferredBackBufferWidth = 960;
            Content.RootDirectory = "Content";

            // base
            player.Position = new Vector2(50, bgFloor);
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
            player.Texture = this.Content.Load<Texture2D>("fishie2");
           
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
            if (keyState.IsKeyUp(Keys.W) && player.Position.Y <= 500)
                hasJumped = true;

            if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
            {
                velocity.Y = baseVel;
                playerJump(keyState, delta);
            }

            // Affect player location with gravity
            // Prevents player from exiting the bottom of window
            if (player.Position.Y <= bgFloor)
            {
                //player.Position.Y = player.Position.Y + velocity.Y * delta * gravity;
                player.Position = new Vector2(50, player.Position.Y + velocity.Y * delta * gravity);
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

            //time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //while (time > frameTime)
            //{
            //    // Play the next frame in the SpriteSheet
            //    frameIndex++;

            //    // reset elapsed time
            //    time = 0f;
            //}
            //if (frameIndex > totalFrames) frameIndex = 1;
            //Rectangle source = new Rectangle(frameIndex * frameWidth,
            //                                 0, frameWidth, frameHeight);
            //Vector2 position = new Vector2(this.Window.ClientBounds.Width / 2,
            //                   this.Window.ClientBounds.Height / 2);
            //Vector2 origin = new Vector2(frameWidth / 2.0f, frameHeight);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(player.Texture, player.Position);
            //spriteBatch.Draw(spriteSheet, position, source, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void playerJump(KeyboardState state, float dt) {
            // bgFloor is the bottom platform
            if ((bgFloor - player.Position.Y) >= 125f)
            {
                hasJumped = true;
                velocity.Y = baseVel;
            }
            //player.Position.Y -= velocity.Y * dt;
            player.Position = new Vector2(50, player.Position.Y - velocity.Y * dt);
        }
    }
}
