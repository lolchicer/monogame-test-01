using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace monogametest;

public abstract class Entity : DrawableGameComponent
{
    protected enum SpriteState
    {
        Idle,
        Walking
    }

    private IEnumerable<Affector> _affectors;
    private Level _level;

    // _
    protected SpriteState SpriteState_
    {
        get
        {
            if (_affectors.First(affector => affector is Input).Velocity != new Vector2() { X = 0, Y = 0 })
                return SpriteState.Walking;
            return SpriteState.Idle;
        }
    }

    public abstract string IdleTextureName { get; }
    public abstract string SprintingTextureName { get; }
    public abstract int SprintingTexturesCount { get; }

    public SpriteBatch SpriteBatch { get; set; }
    public Texture2D IdleTexture { get; set; }
    public IAnimation SprintingTexture { get; set; }
    public Mechanics Mechanics { get; }
    public Vector2 Position => Mechanics.Position;

    public override void Update(GameTime gameTime)
    {
        foreach (var affector in _affectors)
        {
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
        switch (SpriteState_)
        {
            case SpriteState.Idle:
                SpriteBatch.Draw(IdleTexture, Position, Color.White);
                break;
            case SpriteState.Walking:
                SpriteBatch.Draw(SprintingTexture.GetCurrentTexture(gameTime), Position, Color.White);
                break;
            default:
                break;
        }
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
