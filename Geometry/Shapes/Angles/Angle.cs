using Microsoft.Xna.Framework;
using System;

namespace MonogameTest01;

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
    public Angle(Vector2 vector)
    {
        _value = (float)Math.Atan2(vector.Y, vector.X);
    }

    // закрытое поле в статическом методе
    public static implicit operator FlatAngle(Angle value) => new() { Value = value._value };
}
