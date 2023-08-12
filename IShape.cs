using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace monogametest;

public interface IShape // : IEquatable<IShape>
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Point Location { get; set; }
    /* public bool IsEmpty { get; } что это должно делать
    public int Bottom { get; }
    public int Top { get; }
    public int Right { get; }
    public int Left { get; } */
    public Point Size { get; set; }
    public Point Center { get; }

    public IList<Point> Points { get; }

    /* public static IShape Intersect(IShape value1, IShape value2);
        public static void Intersect(ref IShape value1, ref IShape value2, out IShape result);
        public static void Union(ref IShape value1, ref IShape value2, out IShape result);
        public static IShape Union(IShape value1, IShape value2);
        public void Contains(ref IShape value, out bool result);
    public bool Contains(int x, int y);
    public void Contains(ref Vector2 value, out bool result);
    public bool Contains(float x, float y); */
    public bool Contains(Point value);
    /*  public void Contains(ref Point value, out bool result);
        public bool Contains(Vector2 value); */
    public bool Contains(IShape value);
    /*  public void Deconstruct(out int x, out int y, out int width, out int height);
        public override bool Equals(object obj);
        public bool Equals(IShape other);
    public override int GetHashCode();
    public void Inflate(float horizontalAmount, float verticalAmount);
        public void Inflate(int horizontalAmount, int verticalAmount);
        public void Intersects(ref IShape value, out bool result); */
    public bool Intersects(IShape value);
    /*  public void Offset(Point amount);
        public void Offset(float offsetX, float offsetY);
        public void Offset(int offsetX, int offsetY);
        public void Offset(Vector2 amount);
        public override string ToString(); */

    /* public static bool operator ==(IShape a, IShape b);
    public static bool operator !=(IShape a, IShape b); */

    public IShape Sum(IShape shape);
}