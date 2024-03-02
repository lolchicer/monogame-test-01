namespace MonogameTest01;

public class Eviscerator : Entity
{
    public override EntityDrawingConfig DrawingConfig => new()
    {
        IdleTextureName = "entity-idle",
        SprintingTextureName = "entity-sprinting",
        SprintingTexturesCount = 4
    };

    public Eviscerator(Level _level)
    : base(_level) { }
    public Eviscerator(Level _level, Player player)
    : base(_level, player) { }
}
