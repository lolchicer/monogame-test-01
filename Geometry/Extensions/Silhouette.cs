using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

public static class SihlouetteExtensions
{
    // очень плохой способ описать множество
    public static ValueTuple<float, float> Sihlouette(this IEnumerable<float> angles) =>
    // это точно работает?
    (angles.Min(), angles.Max());

    public static ValueTuple<float, float> Sihlouette(this Vector2 value, Vector2 covering)
    {
        var difference = covering - value;

        return difference switch
        {
            (0, 0) => (0, 360),
            _ => (difference.GetAngle(), difference.GetAngle())
        };
    }

    public static ValueTuple<float, float> Sihlouette(this Vector2 value, Segment covering) => (
        from vector in covering.Vectors
        select value.Sihlouette(covering.Location + vector))
        . //Unite() или что-то в этом духе
        ;

    private static IEnumerable<ValueTuple<float, float>> Sihlouette(this Vector2 value, IEnumerable<Vector2> perimeter)
    {
        // нужно объединение этих так называемых "множеств"
        return
        from segment in perimeter.Segments()
        select value.Sihlouette(segment);
    }

    public static IEnumerable<ValueTuple<float, float>> Sihlouette(this Vector2 value, IPolygon covering)
    {
        // для этого типа данных даже отдельного от функции названия нет
        var sihlouettes = new List<ValueTuple<float, float>>();
        // это тоже нужно на что-то заменить
        foreach (var perimeter in covering.Perimeters)
            sihlouettes.AddRange(value.Sihlouette(perimeter));

        return sihlouettes;
    }
}
