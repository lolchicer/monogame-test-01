using Microsoft.Xna.Framework;

namespace MonogameTest01;

public class Friction : Affector
{
    private const float _speed = 2;

    protected override void UpdateVelocity(GameTime gameTime)
    {
        if (_mechanics.Velocity.Length() == 0)
            return;
        else if (_mechanics.Velocity.Length() < _speed)
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