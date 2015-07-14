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

        public Obstacle(Texture2D texture, int obsLength, float velocity)
        {
            this.obsLength = obsLength;
            this.Texture = texture;
            this.Velocity = velocity;
        }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Velocity { get; set; }

        public void GenerateObstacle(int y1, int y2) {
            // objects always appear from end of screen
            // parameters are the range of y values we want to accept
            int x = 960 - obsLength;
            int y = rng.Next(y1, y2 - obsLength);

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
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, location);
            spriteBatch.End();
        }
    }
}
