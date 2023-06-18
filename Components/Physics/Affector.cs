using Microsoft.Xna.Framework;

namespace monogametest;

public class Affector : GameComponent
{
    protected Mechanics _mechanics;

    public Affector(Mechanics mechanics) : base(mechanics.Game)
    {
        _mechanics = mechanics;
    }
}