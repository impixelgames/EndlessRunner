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
        Player player;
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
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //player.Texture = this.Content.Load<Texture2D>("fishie2");
            Texture2D texture = this.Content.Load<Texture2D>("running2ver1");
            obs.Texture = this.Content.Load<Texture2D>("fishie"); 
            obs.Velocity = 500f;

            // Main Character
            player = new Player(texture, 4, 5);
            player.Velocity = 750;
            player.hasJumped = false;
           
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

            player.Update();

            base.Update(gameTime);
            previousState = keyState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (numObstacles == 0)
            {
                obs.GenerateObstacle();
                numObstacles++;
            }

            if (obs.Position.X <= 0)
                numObstacles = 0;

            player.Draw(spriteBatch, player.Position);

            spriteBatch.Begin();
            //player.Draw(spriteBatch, player.Position);
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
            else
            {
                player.Position = new Vector2(50, player.Position.Y - player.Velocity * dt);
            }
            //player.Position.Y -= velocity.Y * dt;
            
        }
    }
}
