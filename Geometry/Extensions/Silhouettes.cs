using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

public static class SihlouettesExtensions
{
    private static IEnumerable<IEnumerable<ValueTuple<float, float>>> Sihlouettes(this IEnumerable<Vector2> perimeter, IPolygon covering) =>
    from vector in perimeter
    select vector.Sihlouette(covering);

    public static IEnumerable<IEnumerable<ValueTuple<float, float>>> Sihlouettes(this IPolygon value, IPolygon covering)
    {
        // кто бы мог подумать
        var sihlouettes = new List<IEnumerable<ValueTuple<float, float>>>();
        foreach (var perimiter in value.Perimeters)
            sihlouettes.AddRange(perimiter.Sihlouettes(covering));
        return sihlouettes;
    }
}