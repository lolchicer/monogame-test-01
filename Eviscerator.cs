using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogametest;

public class Eviscerator : Entity
{
    public override string SpriteTextureName => "Sprite-0001";

    public Eviscerator(Game game)
    : base(game) { }
}