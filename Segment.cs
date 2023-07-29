using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

// бляяя как же
public static class Выплдважп
{
    public static float GetAngle(this Vector2 value) =>
    (float)Math.Atan2(value.Y, value.X);
}

public class Segment : IShape
{
    private Point _location;
    private Point _size;
    private Point _point1;
    private Point _point2;

    private bool IntersectsLine(Segment value)
    {
        var vector = (_point2 - _point1).ToVector2();
        var valueVector1 = (value.Point1 + value.Location - _point1 - Location).ToVector2();
        var valueVector2 = (value.Point2 + value.Location - _point1 - Location).ToVector2();

        var valueSign1 = Math.Sign(valueVector1.GetAngle() - vector.GetAngle());
        var valueSign2 = Math.Sign(valueVector2.GetAngle() - vector.GetAngle());

        return
        (valueSign1 == 0 && valueSign2 == 0) ||
        (valueSign1 != valueSign2);
    }

    private bool Intersects(Segment value) =>
    this.IntersectsLine(value) &&
    value.IntersectsLine(this);

    private bool ContainsLine(Segment value)
    {
        var vector = (_point2 - _point1).ToVector2();
        var valueVector1 = vector - (value.Point1 - _point1).ToVector2();
        var valueVector2 = vector - (value.Point2 - _point1).ToVector2();

        var valueSign1 = Math.Sign(valueVector1.GetAngle() - vector.GetAngle());
        var valueSign2 = Math.Sign(valueVector2.GetAngle() - vector.GetAngle());

        return (valueSign1 == 0 && valueSign2 == 0);
    }

    private bool Contains(Segment value) =>
    this.ContainsLine(value);

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Point Location { get => _location; set => _location = value; }
    public Point Size { get => _size; set => _size = value; }
    public Point Center { get => _location + (_size * _size); }

    public IList<Point> Points { get => new Point[] { _point1, _point2 }; }

    public Point Point1 { get => _point1; set => _point1 = value; }
    public Point Point2 { get => _point2; set => _point2 = value; }

    public bool Intersects(IShape value)
    {
        for (
            int i = 0, nextI = i + 1, n = value.Points.Count();
            i < n;
            i++, nextI = (i + 1) % n)
            if (Intersects(new Segment(
                value.Points.ToArray()[i],
                value.Points.ToArray()[nextI]
            )))
                return true;
        return false;
    }

    public bool Contains(Point value)
    {
        var vector = (_point2 - _point1).ToVector2();
        var valueVector = vector - (value - _point1).ToVector2();

        var valueSign = Math.Sign(valueVector.GetAngle() - vector.GetAngle());

        return (valueSign == 0);
    }

    public bool Contains(IShape value)
    {
        for (
            int i = 0, nextI = i + 1, n = Points.Count();
            i < n;
            i++, nextI = (i + 1) % n)
            if (!Contains(new Segment(
                Points.ToArray()[i],
                Points.ToArray()[nextI]
            )))
                return false;
        return true;
    }

    public Segment(Point point1, Point point2)
    {
        _location = point1;

        _point1 = new();
        _point2 = point2 - _location;
    }
}