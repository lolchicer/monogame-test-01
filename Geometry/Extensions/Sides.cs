using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

public static class SidesExtensions
{
    // не определено, какое значение Side представляет внешнюю сторону, и какое – внутреннюю
    // для value можно выделить отдельный интерфейс
    private static IEnumerable<Side> Sides(this IEnumerable<Vector2> perimeter, Vector2 value) =>
    from segment in perimeter.Segments()
    select new Line(segment).Side(new Angle(value));

    public static IEnumerable<Side> Sides(this IPolygon value1, Vector2 value2)
    {
        var sides = new List<Side>();
        foreach (var perimeter in value1.Perimeters)
            sides.AddRange(Sides(perimeter, value2));
        return sides;
    }
}
