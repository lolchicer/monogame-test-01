using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MonogameTest01;

public static class IntersectsExtensions
{
    public static bool Intersects(this Line value1, Line value2) =>
        value1.Angle != value2.Angle ||
        value1.C == value2.C;

    public static bool Intersects(this Line value1, Ray value2)
    {
        var valueSide1 = value1.Side(value2.Vector + value2.Location);
        var valueSide2 = value1.Side(value2.Angle);

        return
        (valueSide1 == Side.Center && valueSide2 == Side.Center) || (
            (valueSide1 != valueSide2) &&
            (valueSide1 != Side.Center) &&
            (valueSide2 != Side.Center));
    }

    public static bool Intersects(this Line value1, Segment value2)
    {
        var valueSide1 = value1.Side(value2.Vector1 + value2.Location);
        var valueSide2 = value1.Side(value2.Vector2 + value2.Location);

        return valueSide1 != valueSide2;
    }

    public static bool Intersects(this Ray value1, Line value2) =>
        value2.Intersects(value1);

    public static bool Intersects(this Ray value1, Ray value2) =>
        new Line(value1).Intersects(value2) &&
        new Line(value2).Intersects(value1);

    public static bool Intersects(this Ray value1, Segment value2) =>
        new Line(value1).Intersects(value2) &&
        new Line(value2).Intersects(value1);

    // пох
    public static bool Intersects(this Segment value1, Vector2 value2) =>
    value1.Contains(value2);

    public static bool Intersects(this Segment value1, Line value2) =>
    value2.Intersects(value1);

    public static bool Intersects(this Segment value1, Ray value2) =>
    value2.Intersects(value1);

    public static bool Intersects(this Segment value1, Segment value2) =>
    new Line(value1).Intersects(value2) &&
    new Line(value2).Intersects(value1);

    private static bool Intersects(this Segment value1, IList<Vector2> perimeter)
    {
        var shiftedPerimeter = new List<Vector2>(perimeter);
        shiftedPerimeter.RemoveAt(0);
        shiftedPerimeter.Add(perimeter.Last());
        return (
            from pair in perimeter.Zip(shiftedPerimeter)
            select new Segment(pair.First, pair.Second))
            .Any(segment => value1.Intersects(segment)); // как тебя назвать
    }

    public static bool Intersects(this Segment value1, IPolygon value2) => (
        from perimeter in value2.Perimeters
        select new List<Vector2>(
            from Vector2 in perimeter
            select Vector2 + (value2.Location - value1.Location)))
            .Any(perimeter => value1.Intersects(perimeter));
}
