using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MonogameTest01;

public abstract class Entity : DrawableGameComponent
{
    protected enum SpriteState
    {
        Idle,
        Walking
    }

    protected enum SpriteDirection
    {
        Left,
        Right
    }

    private SpriteDirection _spriteDirection = SpriteDirection.Right;

    private IEnumerable<Affector> _affectors;
    private Level _level;

    // _
    protected SpriteState SpriteState_
    {
        get
        {
            // я когда-нибудь получу по башне за поиск свойств в коллекциях
            if (_affectors.First(affector => affector is Input).Velocity != new Vector2() { X = 0, Y = 0 })
                return SpriteState.Walking;
            return SpriteState.Idle;
        }
    }

    // чё ваще
    protected SpriteDirection SpriteDirection_
    {
        get
        {
            var velocity = _affectors.First(affector => affector is Input).Velocity;
            if (velocity.X < 0)
                _spriteDirection = SpriteDirection.Left;
            if (velocity.X > 0)
                _spriteDirection = SpriteDirection.Right;
            return _spriteDirection;
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
        Texture2D texture;
        switch (SpriteState_)
        {
            case SpriteState.Walking:
                texture = SprintingTexture.GetCurrentTexture(gameTime);
                break;
            default:
                texture = IdleTexture;
                break;
        }
        switch (SpriteDirection_)
        {
            case SpriteDirection.Left:
                SpriteBatch.Draw(texture, Position, null, Color.White, 0,
                new() { X = 0, Y = 0 }, 1, SpriteEffects.FlipHorizontally, 1);
                break;
            default:
                SpriteBatch.Draw(texture, Position, Color.White);
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
