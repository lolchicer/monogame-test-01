using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MonogameTest01;

public static class CoversExtensions
{
    public static bool Covers(this Vector2 covering, Vector2 covered, Vector2 value) =>
    covering.Silhouette(value).Contains(covered.Silhouette(value));

    public static bool Covers(this ISilhouetteGiving covering, Vector2 covered, Vector2 value) =>
    covering.Silhouette(value).Contains(covered.Silhouette(value));

    public static bool Covers(this Segment covering, Vector2 covered, Vector2 value) =>
    covering.Silhouette(value).Contains(covered.Silhouette(value));

    public static bool Covers(this IPolygon covering, Vector2 covered, Vector2 value) =>
    covering.Silhouette(value).Contains(covered.Silhouette(value));
}
