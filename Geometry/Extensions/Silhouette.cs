using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace monogametest;

public static class SihlouetteExtensions
{
    // очень плохой способ описать множество
    public static AngleRange Sihlouette(this Vector2 value, Vector2 covering) =>
    new(value, covering);

    public static AngleRange Sihlouette(this Vector2 value, Segment covering) =>
    new(value, covering);

    // отстутсвует Sihlouette(Vector2, IEnumerable<Vector2>). в таком случае, следует удалить его отовсюду.

    // нужно объединение этих так называемых "множеств"
    public static AngleRange Sihlouette(this Vector2 value, IPolygon covering)
    {
        var segments = new List<Segment>();
        // это тоже нужно на что-то заменить
        foreach (var perimeter in covering.Perimeters)
            segments.AddRange(perimeter.Segments());
        return new AngleRange(value, segments);
    }
}
