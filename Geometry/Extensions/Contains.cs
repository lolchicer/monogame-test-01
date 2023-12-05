using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

public static class ContainsExtensions
{
    // что за кал нахуй???
    private static bool Contains<T>(this IPolygon value1, T value2, Func<Corner, T, bool> contains)
    {
        foreach (var perimeter in value1.Perimeters)
        {
            // IEnumerable.Skip(...) не работает
            // не самый удобный зип
            var corners =
            from corner in
            perimeter.Zip(perimeter.Skip(-1), perimeter.Skip(1))
            select new Corner(corner.First, corner.Second, corner.Third);

            var hitCorners = new List<Corner>(
                from hitCorner in corners
                where contains(hitCorner, value2)
                select hitCorner);

            IEnumerable<Corner> NewHitCorners() => (
                from missedCorner in
                    from corner in corners
                    where !hitCorners.Contains(corner)
                    select corner
                where hitCorners.Any(
                    hitCorner =>
                    missedCorner.Contains(hitCorner.Location + hitCorner.Vector))
                select missedCorner).Distinct();

            for (var newHitCorners = NewHitCorners();
                newHitCorners.Count() != 0;
                newHitCorners = NewHitCorners())
                hitCorners.AddRange(newHitCorners); // зачем

            if (corners.All(corner => hitCorners.Contains(corner)))
                return true;
        }
        return false;
    }

    // value - vector - location - что же ещё тебе вычесть
    public static bool Contains(this Corner value1, Vector2 value2) =>
    value1.Angle1 >= (value2 - value1.Vector - value1.Location).GetAngle() ==
    (value2 - value1.Vector - value1.Location).GetAngle() >= value1.Angle2;

    public static bool Contains(this Corner value1, Segment value2) =>
    value1.Contains(value2.Location + value2.Vector1) &&
    value1.Contains(value2.Location + value2.Vector2) &&
    !new Ray(value1.Location + value1.Vector, value1.Angle1).Intersects(value2) &&
    !new Ray(value1.Location + value1.Vector, value1.Angle2).Intersects(value2);

    private static bool Contains(this IList<Vector2> perimeter, Vector2 value)
    {
        var shiftedPerimeter = new List<Vector2>(perimeter);
        shiftedPerimeter.RemoveAt(0);
        shiftedPerimeter.Add(perimeter.Last());
        var segments =
        from pair in perimeter.Zip(shiftedPerimeter)
        select new Segment(pair.First, pair.Second);
        return segments.Any(
                segment => value.Covers segments == Side.Right &&
                !perimeter.Any() value.Intersects(segment));
    }

    public static bool Contains(this IPolygon value1, Vector2 value2)
    => Contains(value1, value2, Contains);

    public static bool Contains(this IPolygon value1, Segment value2)
    => Contains(value1, value2, Contains);

    public static bool Contains(this IPolygon value1, IPolygon value2)
    {
        foreach (var perimeter in value2.Perimeters)
            foreach (var vector in perimeter)
                if (!value1.Contains(vector))
                    return false;
        return true;
    }
}
