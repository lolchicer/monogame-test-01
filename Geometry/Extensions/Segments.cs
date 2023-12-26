using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MonogameTest01;

public static class SegmentsExtensions
{
    public static IEnumerable<Segment> Segments(this IEnumerable<Vector2> perimeter)
    {
        var shiftedPerimeter = new List<Vector2>(perimeter);
        shiftedPerimeter.RemoveAt(0);
        shiftedPerimeter.Add(perimeter.Last());
        return (
            from pair in perimeter.Zip(shiftedPerimeter)
            select new Segment(pair.First, pair.Second));
    }

    public static IEnumerable<Segment> Segments(this IPolygon value)
    {
        var segments = new List<Segment>();
        foreach (var perimeter in value.Perimeters)
            segments.AddRange(perimeter.Segments());
        return segments;
    }
}
