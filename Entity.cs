using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogametest;

public abstract class Entity : DrawableGameComponent
{
    private Player _player;
    private Friction _friction;

    public abstract string SpriteTextureName { get; }

    public SpriteBatch SpriteBatch { get; set; }
    public Texture2D SpriteTexture { get; set; }

    public Vector2 Velocity { get; set; } = new() { X = 0, Y = 0 };
    public Vector2 Position { get; set; } = new() { X = 0, Y = 0 };

    public override void Update(GameTime gameTime)
    {
        _friction.Update(gameTime);
        _player.Update(gameTime);
        
        Position += Velocity;

        base.Update(gameTime);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch.Begin();
        SpriteBatch.Draw(SpriteTexture, Position, Color.White);
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    public Entity(Game game) : base(game)
    {
        _friction = new(this);
        _player = new(this);
    }
}
