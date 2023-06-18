using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace monogametest;

public class Input : Affector
{
    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }

    private void Accelerate(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                _mechanics.Velocity += new Vector2 { X = 0, Y = -1 };
                break;
            case Direction.Left:
                _mechanics.Velocity += new Vector2 { X = -1, Y = 0 };
                break;
            case Direction.Down:
                _mechanics.Velocity += new Vector2 { X = 0, Y = 1 };
                break;
            case Direction.Right:
                _mechanics.Velocity += new Vector2 { X = 1, Y = 0 };
                break;
            default:
                break;
        }
    }

    private void Accelerate(IEnumerable<Direction> directions)
    {
        foreach (var direction in directions)
            Accelerate(direction);
    }

    public List<Direction> Directions { get; } = new();

    public override void Update(GameTime gameTime)
    {
        Accelerate(Directions);
        Directions.Clear();
        
        base.Update(gameTime);
    }

    public Input(Mechanics mechanics) : base(mechanics) { }
}