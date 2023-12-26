using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MonogameTest01;

public static class SilhouettesExtensions
{
    private static IEnumerable<AngleRange> Sihlouettes(this IPolygon value, IEnumerable<Vector2> perimeter) =>
    from vector in perimeter
    select value.Silhouette(vector);

    public static IEnumerable<AngleRange> Sihlouettes(this IPolygon value, IPolygon view)
    {
        // кто бы мог подумать
        var sihlouettes = new List<AngleRange>();
        foreach (var perimiter in view.Perimeters)
            sihlouettes.AddRange(value.Sihlouettes(perimiter));
        return sihlouettes;
    }
}
