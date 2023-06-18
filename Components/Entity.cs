using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace monogametest;

public abstract class Entity : DrawableGameComponent
{
    private Mechanics _mechanics;
    private ICollection<Affector> _affectors;
    private Player _player;

    public abstract string SpriteTextureName { get; }

    public SpriteBatch SpriteBatch { get; set; }
    public Texture2D SpriteTexture { get; set; }
    public Vector2 Position => _mechanics.Position;

    public override void Update(GameTime gameTime)
    {
        _player.Update(gameTime);
        foreach (var affector in _affectors)
            affector.Update(gameTime);
        _mechanics.Update(gameTime);

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
        var mechanics = new Mechanics(game);
        var input = new Input(mechanics);
        
        _mechanics = mechanics;
        _affectors = new Affector[] {
            new Friction(_mechanics),
            input
        };
        _player = new(input);
    }
}
