using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace monogametest;

public class CollisionMeta
{
    public List<Collision> Collisions { get; } = new();

    public IEnumerable<Vector2> GetOutside(Collision excludedCollision)
    {
        return from collision in Collisions where collision != excludedCollision select collision.BoxSize;
    }
}