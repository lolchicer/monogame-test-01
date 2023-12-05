using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace monogametest;

public class Ray
{
    private Vector2 _vector;
    private float _angle;

    public Vector2 Location { get => _vector; set => _vector = value; }
    public Vector2 Vector { get => new(0, 0); set => _vector += value; }
    public float Angle { get => _angle; set => _angle = value; }

    public Ray(Vector2 vector, float angle)
    {
        _vector = vector;
        _angle = angle;
    }
}