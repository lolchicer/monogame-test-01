using Microsoft.Xna.Framework;
using System;

namespace monogametest;

public class Angle
{
    private float _value;

    public float Value { get => _value; set => _value = value % 360; }

    public Angle() { }
    public Angle(Vector2 vector)
    {
        _value = (float)Math.Atan2(vector.Y, vector.X);
    }
}
