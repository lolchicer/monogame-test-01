using Microsoft.Xna.Framework;

namespace monogametest;

public class Collision : Affector
{
    private CollisionMeta _meta;

    public Mechanics Mechanics => _mechanics;
    public Vector2 BoxSize { get; }

    protected override void UpdateVelocity(GameTime gameTime)
    {
        foreach (var collisionBox in _meta.GetOutside(this))
            if (collisionBox.Intersects(
                new Rectangle(
                    (_mechanics.Position + _mechanics.Velocity).ToPoint(),
                    BoxSize.ToPoint())))
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