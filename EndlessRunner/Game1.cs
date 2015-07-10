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
        
        // Non-player variables
        Obstacle obs = new Obstacle();
        int numObstacles = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 540;
            graphics.PreferredBackBufferWidth = 960;
            Content.RootDirectory = "Content";

            // base
            player.Position = new Vector2(50, bgFloor);
            player.Velocity = 750;
            player.hasJumped = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.Texture = this.Content.Load<Texture2D>("fishie2");
            obs.Texture = this.Content.Load<Texture2D>("fishie"); 
            obs.Velocity = 500f;
           
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState(); 
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyState.IsKeyDown(Keys.Escape)) Exit();

            // Prevent multiple jumps
            if (keyState.IsKeyUp(Keys.W) && player.Position.Y <= 500)
                player.hasJumped = true;

            if (keyState.IsKeyDown(Keys.W) && player.hasJumped == false)
            {
                player.Velocity = baseVel;
                playerJump(keyState, delta);
            }

            // Affect player location with gravity
            // Prevents player from exiting the bottom of window
            if (player.Position.Y <= bgFloor)
            {
                player.Position = new Vector2(50, player.Position.Y + player.Velocity * delta * gravity);
                player.Velocity += gravity * 1000;
            }
            else
            {
                player.hasJumped = false;
            }

            obs.Position = new Vector2(obs.Position.X - obs.Velocity * delta, obs.Position.Y);

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

            if (numObstacles == 0)
            {
                obs.GenerateObstacle();
                numObstacles++;
            }

            if (obs.Position.X <= 0)
                numObstacles = 0;

            spriteBatch.Begin();
            spriteBatch.Draw(player.Texture, player.Position);
            spriteBatch.Draw(obs.Texture, obs.Position);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void playerJump(KeyboardState state, float dt) {
            // bgFloor is the bottom platform
            if ((bgFloor - player.Position.Y) >= 125f)
            {
                player.hasJumped = true;
                player.Velocity = baseVel;
            }
            //player.Position.Y -= velocity.Y * dt;
            player.Position = new Vector2(50, player.Position.Y - player.Velocity * dt);
        }
    }
}
