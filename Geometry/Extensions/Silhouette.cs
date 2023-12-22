using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace monogametest;

public static class SihlouetteExtensions
{
    // очень плохой способ описать множество
    public static AngleRange Sihlouette(this Vector2 value, Vector2 view) =>
    new(view, value);

    public static AngleRange Sihlouette(this Segment value, Vector2 view) =>
    new(view, value);

    // отстутсвует Sihlouette(IEnumerable<Vector2>, Vector2). в таком случае, следует удалить его отовсюду.

    // нужно объединение этих так называемых "множеств"
    public static AngleRange Sihlouette(this IPolygon value, Vector2 view)
    {
        var segments = new List<Segment>();
        // это тоже нужно на что-то заменить
        foreach (var perimeter in value.Perimeters)
            segments.AddRange(perimeter.Segments());
        return new AngleRange(view, segments);
    }
}
