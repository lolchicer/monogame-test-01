using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogametest;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private List<Texture2D> _textures = new();

    private Level _level;

    public Game1()
    {
        _level = new Level(this);
        _level.Entities.AddRange(new[] {
            new Eviscerator(this)
        });
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        foreach (var entity in _level.Entities)
        {
            entity.SpriteBatch = new SpriteBatch(GraphicsDevice);
            entity.SpriteTexture = Content.Load<Texture2D>($"Sprites/{entity.SpriteTextureName}");
        };

        // TODO: use this.Content to load your game content here

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _level.Update(gameTime);

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _level.Draw(gameTime);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
