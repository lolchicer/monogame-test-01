using Microsoft.Xna.Framework;

namespace monogametest;

public class Friction : GameComponent
{
    private const int _speed = 2;

    private Entity _entity;

    public override void Update(GameTime gameTime)
    {
        if (_entity.Velocity.Length() < _speed)
            _entity.Velocity = new() { X = 0, Y = 0 };
        else
        {
            var normalizedVelocity = _entity.Velocity;
            _entity.Velocity.Normalize();
            _entity.Velocity -= normalizedVelocity * _speed;
        }

        base.Update(gameTime);
    }

    public Friction(Entity entity) : base(entity.Game)
    {
        _entity = entity;
    }
}