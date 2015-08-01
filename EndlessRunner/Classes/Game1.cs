using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Text;

namespace EndlessRunner
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        KeyboardState previousState;

        // Parallax Backgrounds
        List<Background> Backgrounds;
        
        // Non-player variables
        Obstacle trash;

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

            // Music
            Song lvl1 = Content.Load<Song>("lvl1");
            MediaPlayer.Play(lvl1);
            MediaPlayer.Volume = 0.5f;
            
            // Load Textures
            Texture2D texture = this.Content.Load<Texture2D>("player_final");
            Texture2D trashTexture = this.Content.Load<Texture2D>("trashbagv1");
            Texture2D bgTexture3 = this.Content.Load<Texture2D>("background3");
            Texture2D bgTexture2 = this.Content.Load<Texture2D>("background2ver1");
            Texture2D bgTexture1 = this.Content.Load<Texture2D>("bg1");

            // Backgrounds
            Backgrounds = new List<Background>();
            Backgrounds.Add(new Background(bgTexture3, new Vector2(25, 0), 1f));
            Backgrounds.Add(new Background(bgTexture2, new Vector2(75, 0), 1f));
            Backgrounds.Add(new Background(bgTexture1, new Vector2(300, 0), 1f));

            // Obstacles
            trash = new Obstacle(trashTexture, 32, 300f);

            // Main Character
            player = new Player(texture, 1, 8);
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

            // Call Update functions for Objects
            foreach (Background bg in Backgrounds)
                bg.Update(gameTime, GraphicsDevice.Viewport);

            player.Update(keyState, gameTime);
            trash.Update(gameTime);

            if (Physics.checkCollide(player.min, player.max, trash.min, trash.max))
            {
                System.Console.WriteLine("Collision!!!!");
            }

            base.Update(gameTime);
            previousState = keyState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (trash.GetObstacles() == 0)
            {
                trash.GenerateObstacle(960, 480);
            }

            if (trash.Position.X <= 0)
                trash.ResetObstacles();
        
            // Avoid multiple spritebatch calls for backgrounds
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            foreach (Background bg in Backgrounds)
                bg.Draw(spriteBatch);
            spriteBatch.End();

            player.Draw(spriteBatch, player.Position);
            trash.Draw(spriteBatch, trash.Position);

            base.Draw(gameTime);
        }

    }
}
