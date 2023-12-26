using Microsoft.Xna.Framework;

namespace MonogameTest01;

public class Mechanics : GameComponent
{
    public Vector2 Velocity { get; set; } = new() { X = 0, Y = 0 };
    public Vector2 Position { get; set; } = new() { X = 0, Y = 0 };

    public override void Update(GameTime gameTime)
    {
        Position += Velocity;

        base.Update(gameTime);
    }

    public Mechanics(Game game) : base(game) { }
}