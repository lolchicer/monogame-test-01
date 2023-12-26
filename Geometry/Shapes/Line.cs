using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonogameTest01;

public class Line
{
    private FlatAngle _angle;
    private float _c;
    
    public FlatAngle Angle { get => _angle; set => _angle = value; }
    public float C { get => _c; set => _c = value; }

    public Line(FlatAngle angle, Vector2 vector)
    {
        _angle = angle;
        _c = vector.Y - (vector.Y / (float)(Math.PI / 180 * angle.Value));
    }

    public Line(Ray ray) : this(
        ray.Angle,
        ray.Location
    )
    { }

    public Line(Segment segment) : this(
        new FlatAngle(segment.Vector2 - segment.Vector1),
        segment.Location
    )
    { }
}
