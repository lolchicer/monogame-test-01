using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MonogameTest01;

public static class SidesExtensions
{
    private static IEnumerable<(AngleRange Left, AngleRange Right)> Sides(this IEnumerable<Vector2> perimeter, AngleRange value) =>
    from segment in perimeter.Segments()
    select new Line(segment).Side(value);

    public static IEnumerable<(AngleRange Left, AngleRange Right)> Sides(this IPolygon value1, AngleRange value2)
    {
        var sides = new List<(AngleRange Left, AngleRange Right)>();
        foreach (var perimeter in value1.Perimeters)
            sides.AddRange(Sides(perimeter, value2));
        return sides;
    }

    public static IEnumerable<Side> Sides(this IPolygon value1, ISilhouetteGiving value2) =>
    Sides(value1, value2.Silhouette(value2));
}
