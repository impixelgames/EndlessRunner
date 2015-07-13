﻿using System;
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
        private int currentFrame;
        private int totalFrames;
        private int width;
        private int height;

        public Player(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Velocity { get; set; }
        public bool hasJumped { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            //int width = Texture.Width / Columns;
            //int height = Texture.Height / Rows;
            width = 50;
            height = 61;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }

        public void Jump(KeyboardState state, float dt, float maxHeight, float defaultVel)
        {
            if ((maxHeight - this.Position.Y) >= 125f)
            {
                this.hasJumped = true;
                this.Velocity = defaultVel;
            }
            else
            {
                this.Position = new Vector2(50, this.Position.Y - this.Velocity * dt);
            }
        }
    }
}
