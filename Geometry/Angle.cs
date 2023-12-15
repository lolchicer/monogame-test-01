using Microsoft.Xna.Framework;
using System;

namespace monogametest;

public interface IAngle
{
    public float Value { get; set; }
}

public class Angle : IAngle
{
    private float _value;
    private int _lap;

    public float Value
    {
        get => _value + _lap * 360;
        set
        {
            _value = value % 360;
            _lap = (int)value / 360;
        }
    }
    public int Lap { get => _lap; set => _lap = value; }

    // чего это за название такое
    public void Flatten() => _lap = 0;

    public Angle() { }
    public Angle(FlatAngle flatAngle)
    {
        _value = flatAngle.Value;
    }
    public Angle(Vector2 vector)
    {
        _value = (float)Math.Atan2(vector.Y, vector.X);
    }
}

public class FlatAngle : IAngle
{
    private float _value;

    public float Value { get => _value; set => _value = value % 360; }

    public FlatAngle() { }
    public FlatAngle(Angle angle)
    {
        Value = angle.Value;
    }
    public FlatAngle(Vector2 vector)
    {
        _value = (float)Math.Atan2(vector.Y, vector.X);
    }
}
