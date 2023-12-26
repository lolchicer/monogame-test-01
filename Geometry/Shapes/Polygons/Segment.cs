using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MonogameTest01;

public class Segment : IPolygon
{
    private Vector2 _vector1;
    private Vector2 _vector2;

    // Item1 – вершина this перед пересечением
    // Item2 – вершина value перед пересечением
    // Item3 – значение пересечения
    private IList<ValueTuple<int, int, Vector2>> IntersectionsNeighbours(IList<Vector2> Vectors)
    {
        var intersections = new List<ValueTuple<int, int, Vector2>>();

        for (
            int j = 0, nextJ = j + 1, m = Vectors.Count;
            j < m;
            j++, nextJ = (j + 1) % m)
            for (
                int i = 0, nextI = i + 1, n = Vectors.Count;
                i < n;
                i++, nextI = (i + 1) % n)
            {
                var segment = new Segment(
                    Location + Vectors[j], Location + Vectors[nextJ]);
                var valueSegment = new Segment(
                    Vectors[i], Vectors[nextI]);

                if (segment.Intersects(valueSegment))
                    intersections.Add((
                        j, i,
                        segment.Intersection(valueSegment)));
            }

        return intersections;
    }

    private IList<Vector2> Intersection(IList<Vector2> Vectors)
    {
        return new List<Vector2>(from intersection in IntersectionsNeighbours(Vectors) select intersection.Item3);
    }

    private IList<Vector2> Sum(IList<Vector2> Vectors)
    {
        var newVectors = new List<Vector2>();

        var intersections = IntersectionsNeighbours(Vectors);

        for (
            var j = (
                current: 0,
                next: 0,
                side:
                (intersections.First().Item3 - Vectors[intersections.First().Item1]).GetAngle() <
                (intersections.First().Item3 - Vectors[intersections.First().Item2]).GetAngle());
            j.current < intersections.Count;
            j.current++, j.next = (j.next + 1) / intersections.Count, j.side = !j.side)
        {
            Func<ValueTuple<int, int, Vector2>, int> usage;

            if (j.side)
                usage = (ValueTuple<int, int, Vector2> intersection) => intersection.Item1;
            else
                usage = (ValueTuple<int, int, Vector2> intersection) => intersection.Item2;

            if (j.side)
                for (int i = usage(intersections[j.current]) + 1; i < usage(intersections[j.next]); i += 1 % Vectors.Count)
                    newVectors.Add(Vectors[i]);
        }

        return newVectors;
    }

    public Vector2 Location { get => _vector1; set => _vector1 = value; }

    public IList<Vector2> Vectors { get => new Vector2[] { Vector1, Vector2 }; }
    public IList<IList<Vector2>> Perimeters { get => new IList<Vector2>[] { new Vector2[] { Vector1, Vector2 } }; }

    public Vector2 Vector1 { get => new(0, 0); set => _vector1 = Location + value; } // :tf:
    public Vector2 Vector2 { get => _vector2; set => _vector2 = value; }

    public Vector2 Vector { get => Vector2; }

    public Vector2 Intersection(Segment value)
    {
        var a = Vector2.X / Vector2.Y;
        var c = Location.Y - Location.X / a;

        var valueA = value.Vector2.X / value.Vector2.Y;
        var valueC = Location.Y - Location.X / valueA;

        var equationA = a + valueA;
        var equationC = c + valueC;

        return new Vector2(
            Location.X,
            (equationC / equationA) + Location.Y
        );
    }

    // value.Perimeters тоже должен быть заключён в Intersection(...), так как число периметров может меняться
    // в таком случае не нужно было бы заключать perimeter в Intersection(...)
    public IPolygon Intersection(IPolygon value)
    {
        return new Polygon(
            Location,
            new List<IList<Vector2>>(
                from perimeter in value.Perimeters
                select Intersection(
                    new List<Vector2>(
                        from Vector2 in perimeter
                        select Vector2 + (value.Location - Location))))
        );
    }

    public IPolygon Sum(IPolygon value)
    {
        return new Polygon(
            Location,
            new List<IList<Vector2>>(
                from perimeter in value.Perimeters
                select Sum(
                    new List<Vector2>(
                        from Vector2 in perimeter
                        select Vector2 + (value.Location - Location)))));
    }

    public Segment(Vector2 vector1, Vector2 vector2)
    {
        _vector1 = vector1;
        _vector2 = vector2;
    }
}
