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
        Random rng = new Random();
        const int WINDOW_HEIGHT = 540;
        const int WINDOW_WIDTH = 960;
        int obsLength = 32;

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public float Velocity { get; set; }

        public void GenerateObstacle() {
            int x = WINDOW_WIDTH - obsLength;
            int y = rng.Next(100, WINDOW_HEIGHT - obsLength);

            this.Position = new Vector2(x, y);
            return;
        }
    }
}
