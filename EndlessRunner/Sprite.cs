using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRunner
{
    class Sprite
    {
        public Sprite() { }

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public void SetSpriteSheet(GameTime gameTime) {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float frameTime = 0.2f;
            int frameIndex = 1;
            int totalFrames = 20;

            while (delta > frameTime)
            {
                frameIndex++;
                delta = 0f;
            }

            if (frameIndex > totalFrames) frameIndex = 1;

        }
    }
}
