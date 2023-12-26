using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameTest01;

public interface IAnimation
{
    public Texture2D GetCurrentTexture(GameTime gameTime);
}