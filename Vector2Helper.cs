using Microsoft.Xna.Framework;

namespace Monochrome.GUI
{
    public static class Vector2Helper
    {
        /// <summary>
        ///     Return a vector made up of the smallest components of the provided vectors.
        /// </summary>
        public static Vector2 ComponentMin(Vector2 a, Vector2 b)
        {
            return new Vector2(
                a.X < b.X ? a.X : b.X,
                a.Y < b.Y ? a.Y : b.Y
            );
        }

        /// <summary>
        ///     Return a vector made up of the largest components of the provided vectors.
        /// </summary>
        public static Vector2 ComponentMax(Vector2 a, Vector2 b)
        {
            return new Vector2(
                a.X > b.X ? a.X : b.X,
                a.Y > b.Y ? a.Y : b.Y
            );
        }
    }
}