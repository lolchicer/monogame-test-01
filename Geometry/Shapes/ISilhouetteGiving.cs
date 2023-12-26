using Microsoft.Xna.Framework;

namespace MonogameTest01;

public interface ISilhouetteGiving
{
    public AngleRange Silhouette(Vector2 view);
}
