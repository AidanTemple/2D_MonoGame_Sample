#region Using Statements
using Microsoft.Xna.Framework;
using System;
#endregion

namespace _2D_MonoGame_Sample
{
    static class CollisionHandler
    {
        /// <summary>
        /// Axis-Aligned intersection test
        /// </summary>
        /// <param name="rectA"></param>
        /// <param name="rectB"></param>
        /// <returns></returns>
        public static bool Intersects(Rectangle rectA, Rectangle rectB)
        {
            if(rectA.X < rectB.X + rectB.Width && rectA.X + rectA.Width
                > rectB.X && rectA.Y < rectB.Y + rectB.Height && 
                rectA.Height + rectA.Y > rectB.Y)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Per-Pixel intersection test
        /// </summary>
        /// <param name="rectA"></param>
        /// <param name="dataA"></param>
        /// <param name="rectB"></param>
        /// <param name="dataB"></param>
        /// <returns></returns>
        public static bool PerPixelIntersect(Rectangle rectA, Color[] dataA, Rectangle rectB, Color[] dataB)
        {
            int top = Math.Max(rectA.Top, rectB.Top);
            int bottom = Math.Min(rectA.Bottom, rectB.Bottom);
            int left = Math.Max(rectA.Left, rectB.Left);
            int right = Math.Min(rectA.Right, rectB.Right);

            for (int y = top; y < bottom; y++)
            {
                for(int x = left; x < right; x++)
                {
                    Color colorA = dataA[(x - rectA.Left) + (y - rectA.Top) * rectA.Width];
                    Color colorB = dataB[(x - rectB.Left) + (y - rectB.Top) * rectB.Width];

                    if(colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}