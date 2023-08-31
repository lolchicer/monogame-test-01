using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace monogametest;

public struct Edge
{
    public Vector2 First;
    public Vector2 Second;

    public readonly IEnumerable<Vector2> Vertices => new[] { First, Second };
}
