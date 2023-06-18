using Microsoft.Xna.Framework;

namespace monogametest;

public class Collision : Affector
{
    private CollisionMeta _meta;

    public Mechanics Mechanics => _mechanics;
    public Rectangle Box { get; }

    public override void Update(GameTime gameTime)
    {
        foreach (var collision in _meta.GetOutside(this))
            if (Box.Contains(
                _mechanics.Position.X + _mechanics.Velocity.X,
                _mechanics.Position.Y + _mechanics.Velocity.Y))
                _mechanics.Velocity = new() { X = 0, Y = 0 };
    }

    public Collision(Mechanics mechanics, CollisionMeta meta, Rectangle box) : base(mechanics)
    {
        _meta = meta;
        Box = box;
    }
}