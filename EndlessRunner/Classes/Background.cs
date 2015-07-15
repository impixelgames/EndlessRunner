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
        /// <summary>
        /// Viewport: Describes the view-bounds for the render-target surface. Essentially describes the height and width 
        /// of the game window. 
        /// Offset: The means by which we are calculating the new position of the background texture.
        /// 
        /// Draw Parameters:
        /// public void Draw(
        ///     Texture2D texture,
        ///     Nullable<Vector> Position,
        ///     Nullable<Rectangle> destinationRectangle,
        ///     Nullable<Rectangle> sourceRectangle,
        ///     Nullable<Vector2> origin,
        ///     float rotation,
        ///     Nullable<Vector2> scale,
        ///     Nullable<Color> color,
        ///     SpriteEffects effects,
        ///     float layerDepth
        /// )
        /// </summary>
        
        private Texture2D Texture;
        private Vector2 Speed;
        private Vector2 TextureSize;
        private Vector2 screenPos;
        public float Zoom;
        private int screenWidth;        

        private Viewport Viewport;

        public Background(Texture2D texture, Vector2 speed, float zoom)
        {
            Texture = texture;            
            screenPos = Vector2.Zero;
            Speed = speed;
            Zoom = zoom;
            TextureSize = new Vector2(Texture.Width, 0);
        }

        public void Update(GameTime gameTime, Viewport viewport) 
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Viewport = viewport;
            screenWidth = Viewport.Width;

            Vector2 distance = Speed * elapsed;

            screenPos += distance;
            screenPos.X = screenPos.X % Texture.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle((int)screenPos.X, 0, Texture.Width, Texture.Height);
            var sourceLoop = new Rectangle((int)screenPos.X - (int)TextureSize.X, 0, Texture.Width, Texture.Height);

            if (screenPos.X < screenWidth)
            {
                spriteBatch.Draw(Texture, new Vector2(0, 0), source, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(Texture, new Vector2(0, 0), sourceLoop, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 0f);
        }
    }
}
