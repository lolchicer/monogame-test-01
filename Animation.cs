using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonogameTest01;

public class Animation : IAnimation
{
    private IEnumerable<Texture2D> _textures;
    private int _frameDurationMilliseconds = 125;

    public Texture2D GetCurrentTexture(GameTime gameTime) =>
        _textures.ToArray()[
            (gameTime.TotalGameTime.Milliseconds / _frameDurationMilliseconds) 
            % _textures.ToArray().Length];

    public Animation(IEnumerable<Texture2D> textures)
    {
        _textures = textures;
    }
}