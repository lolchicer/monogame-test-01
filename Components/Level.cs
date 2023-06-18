using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace monogametest;

public class Level : DrawableGameComponent
{
    public CollisionMeta CollisionMeta { get; } = new();
    public List<Entity> Entities { get; } = new();

    public override void Update(GameTime gameTime)
    {
        Entities.ForEach(entity => entity.Update(gameTime));

        base.Update(gameTime);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {
        Entities.ForEach(entity => entity.Draw(gameTime));

        base.Draw(gameTime);
    }

    public Level(Game game) : base(game) { }
}
