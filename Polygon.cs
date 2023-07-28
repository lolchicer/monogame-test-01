using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace monogametest;

public class Polygon : IShape
{
    private Point _location;
    private Point _size;

    public int X { get => _location.X; set => _location.X = value; }
    public int Y { get => _location.Y; set => _location.Y = value; }
    public int Width { get => _size.X; set => _size.X = value; }
    public int Height { get => _size.Y; set => _size.Y = value; }

    public Point Location { get => _location; set => _location = value; }
    /* public bool IsEmpty { get; } что это должно делать
    public int Bottom { get; }
    public int Top { get; }
    public int Right { get; }
    public int Left { get; } */
    public Point Size { get => _size; set => _size = value; }
    public Point Center { get => _location + (_size * _size); }

    // сбивал с поезда разрабов .net
    List<Point> _points = new List<Point>();
    public IList<Point> Points => _points;

    /* public static Rectangle Intersect(Rectangle value1, Rectangle value2);
    public static void Intersect(ref Rectangle value1, ref Rectangle value2, out Rectangle result);
    public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result);
    public static Rectangle Union(Rectangle value1, Rectangle value2); */

    public bool Intersects(IShape shape)
    {
        for (int j = 0; j < Points.Count; j++)
            for (int i = 0; i < shape.Points.Count; i++)
                if (new Segment(
                    Points[j],
                    Points[(j + 1) % Points.Count])
                    .Intersects(shape))
                    return false;
        return true;
    }

    // что за кал нахуй???
    public bool Contains(Point point)
    {
        var angles =
        from angle in
        Points.Zip(Points.Skip(-1), Points.Skip(1))
        select angle;

        var missedAngles =
        from angle in angles
        where !(
        (angle.Second - angle.First).ToVector2().GetAngle() >=
        (point - angle.First - _location).ToVector2().GetAngle() &&
        (point - angle.First - _location).ToVector2().GetAngle() >=
        (angle.Third - angle.First).ToVector2().GetAngle())
        select angle;

        var hitAngles = from angle in angles
                        where missedAngles.Contains(angle)
                        select angle;

        foreach (var missedAngle in missedAngles)
        {
            if (hitAngles.Any(hitAngle =>
            (missedAngle.Second - missedAngle.First).ToVector2().GetAngle() >=
            (hitAngle.First - _location).ToVector2().GetAngle() &&
            (hitAngle.First - _location).ToVector2().GetAngle() >=
            (missedAngle.Second - missedAngle.First).ToVector2().GetAngle()))
            {
                hitAngles.Append(missedAngle);
                break;
            }
            return false;
        }

        return true;
    }

    public bool Contains(IShape shape)
    {
        foreach (var point in shape.Points)
            if (!Contains(point))
                return false;
        return true;
    }

    public Polygon(Point location, IEnumerable<Point> points)
    {
        Location = location;
        _points.AddRange(from point in points select point - Location);
    }
}