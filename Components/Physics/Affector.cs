using Microsoft.Xna.Framework;

namespace MonogameTest01;

public abstract class Affector : GameComponent
{
    protected Vector2 _velocity;
    protected Mechanics _mechanics;

    protected abstract void UpdateVelocity(GameTime gameTime);

    // пока неясно как надо ли использовать Mechanics целиком
    public Vector2 Velocity => _velocity;

    public override void Update(GameTime gameTime)
    {
        _velocity = new Vector2();
        UpdateVelocity(gameTime);

        base.Update(gameTime);
    }

    public Affector(Mechanics mechanics) : base(mechanics.Game)
    {
        _mechanics = mechanics;
    }
}