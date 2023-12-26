using Microsoft.Xna.Framework;
using System;

namespace MonogameTest01;

public class FlatAngle : IAngle
{
    private float _value;

    public float Value { get => _value; set => _value = value % 360; }

    public FlatAngle() { }
    public FlatAngle(Vector2 vector)
    {
        _value = (float)Math.Atan2(vector.Y, vector.X);
    }
    
    public static implicit operator Angle(FlatAngle value) => new() { Value = value._value };
}
