using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace monogametest;

public class Player : Intelligence
{
    enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }

    IEnumerable<Direction> FromInput()
    {
        var keyboadrdState = Keyboard.GetState();

        var directions = new List<Direction>();

        if (keyboadrdState.IsKeyDown(Keys.W))
            directions.Add(Direction.Up);
        if (keyboadrdState.IsKeyDown(Keys.A))
            directions.Add(Direction.Left);
        if (keyboadrdState.IsKeyDown(Keys.S))
            directions.Add(Direction.Down);
        if (keyboadrdState.IsKeyDown(Keys.D))
            directions.Add(Direction.Right);

        return directions;
    }

    void Accelerate(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                _entity.Velocity += new Vector2 { X = 0, Y = -1 };
                break;
            case Direction.Left:
                _entity.Velocity += new Vector2 { X = -1, Y = 0 };
                break;
            case Direction.Down:
                _entity.Velocity += new Vector2 { X = 0, Y = 1 };
                break;
            case Direction.Right:
                _entity.Velocity += new Vector2 { X = 1, Y = 0 };
                break;
            default:
                break;
        }
    }

    void Accelerate(IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
            Accelerate(direction);
    }

    public override void Update(GameTime gameTime)
    {
        Accelerate(FromInput());

        base.Update(gameTime);
    }

    public Player(Entity entity) : base(entity) { }
}