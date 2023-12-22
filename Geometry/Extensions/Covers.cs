using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

public static class CoversExtensions
{
    public static bool Covers(this Vector2 covering, Vector2 covered, Vector2 value) =>
    covering.Sihlouette(value).Contains(covered.Sihlouette(value));

    public static bool Covers(this ISilhouetteGiving covering, Vector2 covered, Vector2 value) =>
    covering.Sihlouette(value).Contains(covered.Sihlouette(value));

    public static bool Covers(this Segment covering, Vector2 covered, Vector2 value) =>
    covering.Sihlouette(value).Contains(covered.Sihlouette(value));

    public static bool Covers(this IPolygon covering, Vector2 covered, Vector2 value) =>
    covering.Sihlouette(value).Contains(covered.Sihlouette(value));
}