using System;

namespace Amakazor.Cellular
{
    public class Point : IEquatable<Point>
    {
        public long X { get; set; }
        public long Y { get; set; }

        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point first, Point second) => new Point(first.X + second.X, first.Y + second.Y);
        public static Point operator -(Point first, Point second) => new Point(first.X - second.X, first.Y - second.Y);

        public static Point operator /(Point first, int second) => new Point(first.X / second, first.Y / second);
        public static Point operator *(Point first, int second) => new Point(first.X * second, first.Y * second);

        public override int GetHashCode()
        {
            int hash = 8747;
            hash = (hash * 6793) + X.GetHashCode();
            hash = (hash * 6793) + Y.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }
    }
}