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

    private const float _speed = 2;

    private void Accelerate(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                _velocity += new Vector2 { X = 0, Y = -_speed };
                break;
            case Direction.Left:
                _velocity += new Vector2 { X = -_speed, Y = 0 };
                break;
            case Direction.Down:
                _velocity += new Vector2 { X = 0, Y = _speed };
                break;
            case Direction.Right:
                _velocity += new Vector2 { X = _speed, Y = 0 };
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

    protected override void UpdateVelocity(GameTime gameTime)
    {
        Accelerate(Directions);
        Directions.Clear();
    }

    public Input(Mechanics mechanics) : base(mechanics) { }
}