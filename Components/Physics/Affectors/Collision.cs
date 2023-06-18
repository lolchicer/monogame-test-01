using Microsoft.Xna.Framework;

namespace monogametest;

public class Collision : Affector
{
    private CollisionMeta _meta;

    public Mechanics Mechanics => _mechanics;
    public Vector2 BoxSize { get; }

    public override void Update(GameTime gameTime)
    {
        foreach (var collision in _meta.GetOutside(this))
            if (new Rectangle(
                Mechanics.Position.ToPoint(), 
                BoxSize.ToPoint())
                .Contains(
                _mechanics.Position.X + _mechanics.Velocity.X,
                _mechanics.Position.Y + _mechanics.Velocity.Y))
                _mechanics.Velocity = new() { X = 0, Y = 0 };
    }

    public Collision(Mechanics mechanics, CollisionMeta meta, Vector2 boxSize) : base(mechanics)
    {
        _meta = meta;
        BoxSize = boxSize;
    }
}