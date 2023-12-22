using Microsoft.Xna.Framework;

namespace monogametest;

public interface ISilhouetteGiving
{
    public AngleRange Silhouette(Vector2 view);
}
