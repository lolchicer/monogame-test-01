using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace monogametest;

public class Line
{
    private float _angle;
    private float _c;

    public float Angle { get => _angle; set => _angle = value; }
    public float C { get => _c; set => _c = value; }

    public Line(float angle, Vector2 vector)
    {
        _angle = angle;
        _c = vector.Y - (vector.Y / (float)(Math.PI / 180 * angle));
    }

    public Line(Ray ray) : this(
        ray.Angle,
        ray.Location
    )
    { }

    public Line(Segment segment) : this(
        (segment.Vector2 - segment.Vector1).GetAngle(),
        segment.Location
    )
    { }
}
