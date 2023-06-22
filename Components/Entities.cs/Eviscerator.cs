using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogametest;

public class Eviscerator : Entity
{
    public override string IdleTextureName => "entity-idle";
    public override string SprintingTextureName => "entity-sprinting";
    public override int SprintingTexturesCount => 1;

    public Eviscerator(Level _level)
    : base(_level) { }
    public Eviscerator(Level _level, Player player)
    : base(_level, player) { }
}