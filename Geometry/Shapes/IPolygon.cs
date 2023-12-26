using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonogameTest01;

public interface IPolygon : ISidesGiving // : IEquatable<IPolygon>
{
    /* public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; } */

    public Vector2 Location { get; set; }
    /* public bool IsEmpty { get; } что это должно делать
    public float Bottom { get; }
    public float Top { get; }
    public float Right { get; }
    public float Left { get; }
    public Vector2 Size { get; set; }
    public Vector2 Center { get; } */

    // почему каждый раз когда в программе затрагиваются множества становится можно умывать руки 
    public IList<IList<Vector2>> Perimeters { get; }

    /* public static IPolygon Intersect(IPolygon value1, IPolygon value2);
        public static void Intersect(ref IPolygon value1, ref IPolygon value2, out IPolygon result);
        public static void Union(ref IPolygon value1, ref IPolygon value2, out IPolygon result);
        public static IPolygon Union(IPolygon value1, IPolygon value2);
        public void Contains(ref IPolygon value, out bool result);
    public bool Contains(float x, float y);
    public void Contains(ref Vector2 value, out bool result);
    public bool Contains(float x, float y); */
    // public bool Contains(Vector2 value);
    /*  public void Contains(ref Vector2 value, out bool result);
        public bool Contains(Vector2 value); */
    // public bool Contains(IPolygon value);
    /*  public void Deconstruct(out int x, out int y, out int width, out int height);
        public override bool Equals(object obj);
        public bool Equals(IPolygon other);
    public override int GetHashCode();
    public void Inflate(float horizontalAmount, float verticalAmount);
        public void Inflate(int horizontalAmount, int verticalAmount);
        public void Intersects(ref IPolygon value, out bool result); */
    public bool Intersects(IPolygon value);
    /*  public void Offset(Vector2 amount);
        public void Offset(float offsetX, float offsetY);
        public void Offset(int offsetX, int offsetY);
        public void Offset(Vector2 amount);
        public override string ToString(); */

    /* public static bool operator ==(IPolygon a, IPolygon b);
    public static bool operator !=(IPolygon a, IPolygon b); */

    public IEnumerable<IPolygon> Intersections(IPolygon value);
    public IPolygon Sum(IPolygon value);
}