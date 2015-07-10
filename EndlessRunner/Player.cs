using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRunner
{
    class Player
    {
        int frameHeight = 61;
        int frameWidth = 50;
        int totalFrames = 16;
        int frameIndex = 1;
        float frameTime = 0.2f;

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public float Velocity { get; set; }

        public bool hasJumped { get; set; }

        public void SetSpriteSheet(GameTime gameTime) {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (delta > frameTime)
            {
                frameIndex++;
                delta = 0f;
                //Rectangle source = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
                //Vector2 position = new Vector2(this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2);
                //Vector2 origin = new Vector2(frameWidth / 2.0f, frameHeight);
            }

            if (frameIndex > totalFrames) frameIndex = 1;

        }
    }
}
