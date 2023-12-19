using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

public enum Side
{
    Left,
    Right,
    Center
}

public static class SideExtensions
{
    // значения типа float в данном случае обозначают величины углов в градусах
    public static Side Side(this FlatAngle value1, FlatAngle value2)
    => (value2.Value - value1.Value) switch
    {
        < 180 => monogametest.Side.Left,
        180 => monogametest.Side.Center,
        _ => monogametest.Side.Right
    };

    public static Side Side(this IAngleGiveable value1, FlatAngle value2) =>
    value1.Angle.Side(value2);
    
    public static Side Side(this Line value1, Vector2 value2) =>
    value1.Side(new FlatAngle(value2 - new Vector2(0, value1.C)));

    // точно ли можно определить с какой стороны от луча находится угол?
    public static Side Side(this Ray value1, Vector2 value2) =>
    value1.Side(new FlatAngle(value2 - value1.Vector));

    // а от отрезка?
    // не хватает свойства Segment.Angle
    public static Side Side(this Segment value1, FlatAngle value2) =>
    new FlatAngle(value1.Vector).Side(value2);

    public static Side Side(this Segment value1, Vector2 value2) =>
    value1.Side(new FlatAngle(value2 - value1.Vector));
}
