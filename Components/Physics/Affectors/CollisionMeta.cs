using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MonogameTest01;

public class CollisionMeta
{
    public List<Collision> Collisions { get; } = new();

    public IEnumerable<IPolygon> GetOutside(Collision excludedCollision)
    {
        return from collision in Collisions
               where collision != excludedCollision
               select new Polygon(
                collision.Mechanics.Position.ToPoint(),
                new[] {
                    new Point(0, 0),
                    new Vector2(collision.BoxSize.X, 0).ToPoint(),
                    new Vector2(0, collision.BoxSize.Y).ToPoint(),
                    collision.BoxSize.ToPoint()
                });
    }
}