using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameTest01;

public class Eviscerator : Entity
{
    private static EntityDrawingConfig Config() => new()
    {
        IdleTextureName = "entity-idle",
        SprintingTextureName = "entity-sprinting",
        SprintingTexturesCount = 4
    };

    public Eviscerator(Level _level)
    : base(Config(), _level) { }
    public Eviscerator(Level _level, Player player)
    : base(Config(), _level, player) { }
}
