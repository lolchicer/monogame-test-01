using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonogameTest01;

public class Polygon : IPolygon
{
    private Vector2 _location;
    private Vector2 _size;

    public float X { get => _location.X; set => _location.X = value; }
    public float Y { get => _location.Y; set => _location.Y = value; }
    public float Width { get => _size.X; set => _size.X = value; }
    public float Height { get => _size.Y; set => _size.Y = value; }

    public Vector2 Location { get => _location; set => _location = value; }
    /* public bool IsEmpty { get; } что это должно делать
    public int Bottom { get; }
    public int Top { get; }
    public int Right { get; }
    public int Left { get; } */
    public Vector2 Size { get => _size; set => _size = value; }
    public Vector2 Center { get => _location + (_size * _size); }

    // сбивал с поезда авторов .net
    readonly List<IList<Vector2>> _perimeters = new();
    public IList<IList<Vector2>> Perimeters => _perimeters;

    /* public static Rectangle Intersect(Rectangle value1, Rectangle value2);
    public static void Intersect(ref Rectangle value1, ref Rectangle value2, out Rectangle result);
    public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result);
    public static Rectangle Union(Rectangle value1, Rectangle value2); */
    private bool Intersects(Vector2 value)
    {
        for (int j = 0; j < Perimeters.Count; j++)
            for (int i = 0; i < Perimeters[j].Count; i++)
                if (new Segment(
                    Perimeters[j][i],
                    Perimeters[j][(i + 1) % Perimeters[j].Count])
                    .Intersects(value))
                    return true;
        return false;
    }

    private bool Intersects(Segment value)
    {
        for (int j = 0; j < Perimeters.Count; j++)
            for (int i = 0; i < Perimeters[j].Count; i++)
                if (new Segment(
                    Perimeters[j][i],
                    Perimeters[j][(i + 1) % Perimeters[j].Count])
                    .Intersects(value))
                    return true;
        return false;
    }

    private bool Intersects(IList<Vector2> value)
    {
        for (int i = 0; i < value.Count; i++)
            if (Intersects(new Segment(
                value[i],
                value[(i + 1) % value.Count])))
                return true;
        return false;
    }

    public bool Intersects(IPolygon value)
    {
        for (int i = 0; i < value.Perimeters.Count; i++)
            if (Intersects(Perimeters[i]))
                return true;
        return false;
    }

    public Polygon(Vector2 location, IEnumerable<IEnumerable<Vector2>> perimeters)
    {
        Location = location;
        _perimeters.AddRange(
            from perimeter in perimeters
            select new List<Vector2>(perimeter));
    }
}