using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessRunner
{
    public static class Physics
    {
        /// <summary>
        /// USAGE: bool doesCollide = Physics.checkCollide(a_min, a_max, b_min, b_max);
        /// </summary>

        public static bool checkCollide(Vector2 amin, Vector2 amax, Vector2 bmin, Vector2 bmax)
        {
            // Axis-Aligned Bounding Box calculation
            if (amin.X < bmax.X && amax.X > bmin.X && 
                amin.Y < bmax.Y && amax.Y > bmin.Y)
            {
                return true;
            }

            return false;
        }
    }
}
