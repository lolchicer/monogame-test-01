using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonogameTest01;

public class Corner
{
    private Vector2 _vector;
    private float _angle1;
    private float _angle2;

    public Vector2 Location { get => _vector; set => _vector = value; }
    public Vector2 Vector { get => new(0, 0); set => _vector = Location + value; }
    public float Angle1 { get => _angle1; set => _angle1 = value; }
    public float Angle2 { get => _angle2; set => _angle2 = value; }

    public Corner(Vector2 vector, Vector2 angleVector1, Vector2 angleVector2)
    {
        _vector = vector;
        _angle1 = (vector - angleVector1).GetAngle();
        _angle2 = (vector - angleVector2).GetAngle();
    }
}
