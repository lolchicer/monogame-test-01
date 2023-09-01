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
        var vector = Vector.ToVector2();
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
        var vector = Vector.ToVector2();
        var valueVector1 = vector - (value.Point1 - _point1).ToVector2();
        var valueVector2 = vector - (value.Point2 - _point1).ToVector2();

        var valueSign1 = Math.Sign(valueVector1.GetAngle() - vector.GetAngle());
        var valueSign2 = Math.Sign(valueVector2.GetAngle() - vector.GetAngle());

        return valueSign1 == 0 && valueSign2 == 0;
    }

    private bool Contains(Segment value) =>
    this.ContainsLine(value);

    public Point Intersection(Segment value)
    {
        var a = Point2.X / Point2.Y;
        var c = Y - X / a;

        var valueA = value.Point2.X / value.Point2.Y;
        var valueC = Y - X / valueA;

        var equationA = a + valueA;
        var equationC = c + valueC;

        return new Point(
            X,
            (equationC / equationA) + Y
        );
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Point Location { get => _location; set => _location = value; }
    public Point Size { get => _size; set => _size = value; }
    public Point Center { get => _location + (_size * _size); }

    private IList<Point> Points { get => new Point[] { _point1, _point2 }; }
    public IList<IList<Point>> Perimeters { get => new IList<Point>[] { new Point[] { _point1, _point2 } }; }

    public Point Point1 { get => _point1; set => _point1 = value; }
    public Point Point2 { get => _point2; set => _point2 = value; }

    public Point Vector { get => Point2; }

    private bool Intersects(IList<Point> points)
    {
        for (
            int i = 0, nextI = i + 1, n = points.Count();
            i < n;
            i++, nextI = (i + 1) % n)
            if (Intersects(new Segment(
                points.ToArray()[i],
                points.ToArray()[nextI]
            )))
                return true;
        return false;
    }

    public bool Intersects(IShape value)
    {
        var perimeters = from perimeter in value.Perimeters
                         select new List<Point>(
                            from point in perimeter
                            select point + (value.Location - Location));

        foreach (var perimeter in perimeters)
            if (Intersects(perimeter))
                return true;
        return false;
    }

    public bool Contains(Point value)
    {
        var vector = Vector.ToVector2();
        var valueVector = vector - (value - _point1).ToVector2();

        var valueSign = Math.Sign(valueVector.GetAngle() - vector.GetAngle());

        return valueSign == 0;
    }

    public bool Contains(IList<Point> points)
    {
        for (
            int i = 0, nextI = i + 1, n = points.Count();
            i < n;
            i++, nextI = (i + 1) % n)
            if (!Contains(new Segment(
                points.ToArray()[i],
                points.ToArray()[nextI]
            )))
                return false;
        return true;
    }

    // выглядит недостаточно последовательно
    public bool Contains(IShape value)
    {
        var perimeters = from perimeter in value.Perimeters
                         select new List<Point>(
                            from point in perimeter
                            select point + (value.Location - Location));

        foreach (var perimeter in perimeters)
            if (Intersects(perimeter))
                return false;

        foreach (var perimeter in perimeters)
            if (Contains(perimeter))
                return true;

        return false;
    }

    // Item1 – вершина this перед пересечением
    // Item2 – вершина value перед пересечением
    // Item3 – значение пересечения
    public IList<ValueTuple<int, int, Point>> IntersectionsNeighbours(IList<Point> points)
    {
        var intersections = new List<ValueTuple<int, int, Point>>();

        for (
            int j = 0, nextJ = j + 1, m = Points.Count();
            j < m;
            j++, nextJ = (j + 1) % m)
            for (
                int i = 0, nextI = i + 1, n = points.Count();
                i < n;
                i++, nextI = (i + 1) % n)
            {
                var segment = new Segment(
                    Location + Points[j], Location + Points[nextJ]);
                var valueSegment = new Segment(
                    points[i], points[nextI]);

                if (segment.Intersects(valueSegment))
                    intersections.Add((
                        j, i,
                        segment.Intersection(valueSegment)));
            }

        return intersections;
    }

    public IList<Point> Intersection(IList<Point> points)
    {
        return new List<Point>(from intersection in IntersectionsNeighbours(points) select intersection.Item3);
    }

    // value.Perimeters тоже должен быть заключён в Intersection(...), так как число периметров может меняться
    // в таком случае не нужно было бы заключать perimeter в Intersection(...)
    public IShape Intersection(IShape value)
    {
        return new Polygon(
            Location,
            new List<IList<Point>>(
                from perimeter in value.Perimeters
                select Intersection(
                    new List<Point>(
                        from point in perimeter
                        select point + (value.Location - Location))))
        );
    }

    public IList<Point> Sum(IList<Point> points)
    {
        var newPoints = new List<Point>();

        var intersections = IntersectionsNeighbours(points);

        int j = intersections.Last().Item1 + 1;
        int i = intersections.Last().Item2 + 1;

        bool side = false;

        if ((intersections.First().Item3 - Points[intersections.First().Item1]).ToVector2().GetAngle() <
        (intersections.First().Item3 - Points[intersections.First().Item2]).ToVector2().GetAngle())
            side = true;

        foreach (var intersection in intersections)
        {
            if (side)
            {
                for (; j < intersection.Item1; j += 1 % Points.Count)
                    newPoints.Add(Points[j]);
            }
            else
            {
                for (; i < intersection.Item2; i += 1 % Points.Count)
                    newPoints.Add(Points[i]);
            }
            side = !side;
        }

        return newPoints;
    }

    public IShape Sum(IShape value)
    {
        return new Polygon(
            Location,
            new List<IList<Point>>(
                from perimeter in value.Perimeters
                select Sum(
                    new List<Point>(
                        from point in perimeter
                        select point + (value.Location - Location)))));
    }

    public Segment(Point point1, Point point2)
    {
        _location = point1;

        _point1 = new();
        _point2 = point2 - _location;
    }
}