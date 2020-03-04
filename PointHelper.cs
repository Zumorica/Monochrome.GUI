using System;
using Microsoft.Xna.Framework;

namespace Monochrome.GUI
{
    public static class PointHelper
    {
        public static Point ComponentMax(Point a, Point b)
        {
            return new Point(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
        }

        public static Point ComponentMin(Point a, Point b)
        {
            return new Point(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
        }

        public static Vector2 ToVector2(this Point point)
        {
            var (x, y) = point;
            return new Vector2(x, y);
        }
        
        public static Point ToPoint(this Vector2 vector)
        {
            var (x, y) = vector;
            return new Point((int) x, (int) y);
        }
    }
}