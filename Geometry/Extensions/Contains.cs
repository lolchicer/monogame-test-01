using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MonogameTest01;

public static class ContainsExtensions
{
    // что за кал нахуй???
    // T нужно заменить на специальный интерфейс
    private static bool Contains<T>(this IPolygon value1, T value2)
    {
        var sides = value1.Sides(value2);
        var segments = value1.Segments();
        // внезапно в системе появились рёбра
        // не определено, какое значение Side представляет внешнюю сторону, и какое – внутреннюю
        var containingEdges =
        from tuple in sides.Zip(segments)
        where
        tuple.First == Side.Right ||
        tuple.First == Side.Center
        select tuple.Second;

        var notContainingEdges =
        from tuple in sides.Zip(segments)
        where tuple.First == Side.Left
        select tuple.Second;

        return containingEdges.Any(
            containingEdge => notContainingEdges.All(
                notContainingEdge => !notContainingEdge.Covers(containingEdge, value2)
            ));
    }
}
