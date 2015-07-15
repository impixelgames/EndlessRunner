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
        private Vector2 Offset;
        private Vector2 Speed;
        private Vector2 TextureSize;
        private Vector2 screenPos;
        public float Zoom;
        private int screenWidth;        

        private Viewport Viewport;

        private Rectangle Rectangle
        {
            // Rectangle: X, Y, Width, Height
            get { return new Rectangle((int)(Offset.X), (int)(Offset.Y), (int)(Viewport.Width / Zoom), (int)(Viewport.Height / Zoom)); }
        }

        public Background(Texture2D texture, Vector2 speed, float zoom)
        {
            Texture = texture;
            Offset = Vector2.Zero;
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
            //Offset += distance;
            //Offset.X = (Offset.X % TextureSize.X);

            screenPos -= distance;
            screenPos.X = screenPos.X % Texture.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (screenPos.X < screenWidth)
            {
                //spriteBatch.Draw(Texture, new Vector2(Viewport.X, Viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
                spriteBatch.Draw(Texture, screenPos, null, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 0f);
            }

            //spriteBatch.Draw(Texture, new Vector2(Viewport.X, Viewport.Y) - TextureSize, Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
            spriteBatch.Draw(Texture, screenPos - TextureSize, null, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 0f);
        }
    }
}
