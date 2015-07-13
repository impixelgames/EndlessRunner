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
        Obstacle fish;

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
            
            // Load Textures
            Texture2D texture = this.Content.Load<Texture2D>("try4");
            Texture2D fishTexture = this.Content.Load<Texture2D>("fishie");

            // Obstacles
            fish = new Obstacle(fishTexture, 32, 500f);

            // Main Character
            player = new Player(texture, 2, 20);
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
                player.Jump(keyState, delta, bgFloor, baseVel);
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

            fish.Position = new Vector2(fish.Position.X - fish.Velocity * delta, fish.Position.Y);

            player.Update();

            base.Update(gameTime);
            previousState = keyState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (fish.GetObstacles() == 0)
            {
                fish.GenerateObstacle(400, 520);
            }

            if (fish.Position.X <= 0)
                fish.ResetObstacles();
        
            // spritebatch begin
            player.Draw(spriteBatch, player.Position);
            fish.Draw(spriteBatch, fish.Position);
            // spritebatch end

            base.Draw(gameTime);
        }

    }
}
