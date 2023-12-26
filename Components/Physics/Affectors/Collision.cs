using Microsoft.Xna.Framework;

namespace MonogameTest01;

public class Collision : Affector
{
    private CollisionMeta _meta;

    public Mechanics Mechanics => _mechanics;
    public Vector2 BoxSize { get; }

    protected override void UpdateVelocity(GameTime gameTime)
    {
        foreach (var collisionBox in _meta.GetOutside(this))
            if (collisionBox.Intersects(
                new Polygon(
                (_mechanics.Position + _mechanics.Velocity).ToPoint(),
                new[] {
                    new Point(0, 0),
                    new Vector2(BoxSize.X, 0).ToPoint(),
                    new Vector2(0, BoxSize.Y).ToPoint(),
                    BoxSize.ToPoint()
                })))
                _velocity = -_mechanics.Velocity;
    }

    // конструктор с побочными эффектами
    public Collision(Mechanics mechanics, CollisionMeta meta, Vector2 boxSize) : base(mechanics)
    {
        meta.Collisions.Add(this);

        _meta = meta;
        BoxSize = boxSize;
    }
}