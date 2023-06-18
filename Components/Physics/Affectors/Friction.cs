using Microsoft.Xna.Framework;

namespace monogametest;

public class Friction : Affector
{
    private const int _speed = 2;

    public override void Update(GameTime gameTime)
    {
        if (_mechanics.Velocity.Length() < _speed)
            _mechanics.Velocity = new() { X = 0, Y = 0 };
        else
        {
            var normalizedVelocity = _mechanics.Velocity;
            _mechanics.Velocity.Normalize();
            _mechanics.Velocity -= normalizedVelocity * _speed;
        }

        base.Update(gameTime);
    }

    public Friction(Mechanics mechanics) : base(mechanics) { }
}