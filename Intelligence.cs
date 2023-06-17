using Microsoft.Xna.Framework;

namespace monogametest;

public class Intelligence : GameComponent
{
    protected Entity _entity;

    public Intelligence(Entity entity) : base(entity.Game)
    {
        _entity = entity;
    }
}