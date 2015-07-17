using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRunner
{
    class Obstacle
    {
        private Random rng = new Random();
        private int obsLength;
        private static int numObstacles = 0;

        // Properties
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Velocity { get; set; }

        // AABB collision
        public Vector2 min;
        public Vector2 max;

        public Obstacle(Texture2D texture, int obsLength, float velocity)
        {
            this.obsLength = obsLength;
            this.Texture = texture;
            this.Velocity = velocity;
        }


        public void GenerateObstacle(int x, int y) 
        {
            this.Position = new Vector2(x, y);
            numObstacles++;
        }

        public void DecrementObstacles()
        {
            numObstacles--;
        }

        public void ResetObstacles()
        {
            numObstacles = 0;
        }

        public int GetObstacles()
        {
            return numObstacles;
        }

        public void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position = new Vector2(Position.X - Velocity * delta, Position.Y);

            // Define AABB collision vectors
            this.min = new Vector2((int)Position.X, (int)Position.Y);
            this.max = new Vector2((int)Position.X + (int)Texture.Width, (int)Position.Y + (int)Texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, location);
            spriteBatch.End();
        }
    }
}
