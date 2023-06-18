using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace monogametest;

public class Player : GameComponent
{
    private Input _input;

    // стоит ли писать мне в этом названии Input 🤔
    private void SetDirections()
    {
        var keyboadrdState = Keyboard.GetState();

        var directions = new List<Input.Direction>();

        if (keyboadrdState.IsKeyDown(Keys.W))
            directions.Add(Input.Direction.Up);
        if (keyboadrdState.IsKeyDown(Keys.A))
            directions.Add(Input.Direction.Left);
        if (keyboadrdState.IsKeyDown(Keys.S))
            directions.Add(Input.Direction.Down);
        if (keyboadrdState.IsKeyDown(Keys.D))
            directions.Add(Input.Direction.Right);

        _input.Directions.AddRange(directions);
    }

    public override void Update(GameTime gameTime)
    {
        SetDirections();

        base.Update(gameTime);
    }

    public Player(Input input) : base(input.Game)
    {
        _input = input;
    }
}