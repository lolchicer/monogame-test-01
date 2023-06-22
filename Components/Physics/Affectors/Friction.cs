using Microsoft.Xna.Framework;

namespace monogametest;

public class Friction : Affector
{
    private const float _speed = 4;

    protected override void UpdateVelocity(GameTime gameTime)
    {
        if (_mechanics.Velocity.Length() < _speed)
            _velocity = -_mechanics.Velocity;
        else
        {
            var normalizedVelocity = _mechanics.Velocity;
            normalizedVelocity.Normalize();
            _velocity = -normalizedVelocity * _speed;
        }
    }

    public Friction(Mechanics mechanics) : base(mechanics) { }
}