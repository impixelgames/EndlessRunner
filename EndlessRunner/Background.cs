using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EndlessRunner
{
    class Background
    {
        private Texture2D Texture;
        private Vector2 Offset;
        private Vector2 Speed;
        private Vector2 TextureSize;
        public float Zoom;

        private Viewport Viewport;

        private Rectangle Rectangle
        {
            get { return new Rectangle((int)(Offset.X), (int)(Offset.Y), (int)(Viewport.Width / Zoom), (int)(Viewport.Height / Zoom)); }
        }

        public Background(Texture2D texture, Vector2 speed, float zoom)
        {
            Texture = texture;
            Offset = Vector2.Zero;
            Speed = speed;
            Zoom = zoom;
            TextureSize = new Vector2(Texture.Width, 0);
        }

        public void Update(GameTime gameTime, Vector2 direction, Viewport viewport) 
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Viewport = viewport;
            Vector2 distance = direction * Speed * elapsed;
            Offset += distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Offset.X + 960 < Texture.Width)
            {
                spriteBatch.Draw(Texture, new Vector2(Viewport.X, Viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(Texture, new Vector2(Viewport.X, Viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
            }
        }
    }
}
