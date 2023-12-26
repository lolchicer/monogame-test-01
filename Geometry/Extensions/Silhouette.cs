using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonogameTest01;

public static class SihlouetteExtensions
{
    // очень плохой способ описать множество
    public static AngleRange Silhouette(this Vector2 value, Vector2 view) =>
    new(view, value);

    public static AngleRange Silhouette(this Segment value, Vector2 view) =>
    new(view, value);

    // отстутсвует Silhouette(IEnumerable<Vector2>, Vector2). в таком случае, следует удалить его отовсюду.

    // нужно объединение этих так называемых "множеств"
    public static AngleRange Silhouette(this IPolygon value, Vector2 view)
    {
        var segments = new List<Segment>();
        // это тоже нужно на что-то заменить
        foreach (var perimeter in value.Perimeters)
            segments.AddRange(perimeter.Segments());
        return new AngleRange(view, segments);
    }
}
