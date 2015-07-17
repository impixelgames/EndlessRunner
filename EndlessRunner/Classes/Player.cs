using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRunner
{
    class Player
    {
        private const int defaultVelocity = 750;
        private const int maxJumpHeight = 450;
        private const float playerGravity = 0.15f;
        private const int width = 50;
        private const int height = 61;

        private int currentFrame;
        private int totalFrames;
        private int timeSinceLastFrame = 0;
        private int millisecondsPerFrame = 100;
        
        // Properties
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Velocity { get; set; }
        public bool hasJumped { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        
        // Min and Max for bounding box collision detection
        public Vector2 min;
        public Vector2 max;

        public Player(Texture2D texture, int rows, int columns)
        {
            this.Texture = texture;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = Rows * Columns;
            this.Position = new Vector2(50, 300);
        }


        public void Update(KeyboardState keyState, GameTime gameTime)
        {

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }

            // Check for Key input
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Prevent multiple jumps
            if (keyState.IsKeyUp(Keys.W) && Position.Y <= 500)
                hasJumped = true;

            if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
            {
                Velocity = defaultVelocity;
                Jump(keyState, delta);
            }

            // Affect player location with gravity
            // Prevents player from exiting the bottom of window
            if (Position.Y <= maxJumpHeight)
            {
                Position = new Vector2(50, Position.Y + Velocity * delta * playerGravity);
                Velocity += playerGravity * 1000;
            }
            else
            {
                hasJumped = false;
            }

            // Set AABB collision points
            this.min = new Vector2((int)Position.X, (int)Position.Y);
            this.max = new Vector2((int)Position.X + width, (int)Position.Y + height);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

        public void Jump(KeyboardState state, float dt)
        {
            if ((maxJumpHeight - Position.Y) >= 125f)
            {
                hasJumped = true;
                Velocity = defaultVelocity;
            }
            else
            {
                Position = new Vector2(50, Position.Y - Velocity * dt);
            }
        }
    }
}
