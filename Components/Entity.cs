using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace monogametest;

public abstract class Entity : DrawableGameComponent
{
    private ICollection<Affector> _affectors;
    private Level _level;

    public abstract string SpriteTextureName { get; }

    public SpriteBatch SpriteBatch { get; set; }
    public Texture2D SpriteTexture { get; set; }
    public Mechanics Mechanics { get; }
    public Vector2 Position => Mechanics.Position;

    public override void Update(GameTime gameTime)
    {
        foreach (var affector in _affectors) {
            affector.Update(gameTime);
            Mechanics.Velocity += affector.Velocity;
        }
        Mechanics.Update(gameTime);

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

    public Entity(Level level) : base(level.Game)
    {
        var mechanics = new Mechanics(level.Game);
        var input = new Input(mechanics);

        _level = level;
        Mechanics = mechanics;
        _affectors = new Affector[] {
            new Friction(Mechanics),
            new Collision(Mechanics, _level.CollisionMeta, new Vector2() { X = 10, Y = 10 }),
            input
        };
    }

    // побочные эффекты для Player
    public Entity(Level level, Player player) : base(level.Game)
    {
        var mechanics = new Mechanics(level.Game);
        var input = new Input(mechanics);
        player.Inputs.Add(input);

        _level = level;
        Mechanics = mechanics;
        _affectors = new Affector[] {
            new Friction(Mechanics),
            input,
            new Collision(Mechanics, _level.CollisionMeta, new Vector2() { X = 10, Y = 10 })
        };
    }
}
