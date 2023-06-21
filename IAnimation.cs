using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogametest;

public interface IAnimation
{
    public Texture2D GetCurrentTexture(GameTime gameTime);
}