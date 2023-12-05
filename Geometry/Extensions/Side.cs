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
    public static Side Side(this float value1, float value2)
    {
        value2 -= value1;
        if (value2 % 180 == 0)
            return monogametest.Side.Center;
        else if (value2 / 180 % 2 == 0)
            return monogametest.Side.Left;
        else
            return monogametest.Side.Right;
    }


    public static Side Side(this Line value1, float value2) =>
    value1.Angle.Side(value2);

    public static Side Side(this Line value1, Vector2 value2) =>
    value1.Side((value2 - new Vector2(0, value1.C)).GetAngle());

    public static Side Side(this Ray value1, float value2) =>
    value1.Angle.Side(value2);

    public static Side Side(this Ray value1, Vector2 value2) =>
    value1.Side((value2 - value1.Vector).GetAngle());

    // не хватает свойства Segment.Angle
    public static Side Side(this Segment value1, float value2) =>
    value1.Vector.GetAngle().Side(value2);

    public static Side Side(this Segment value1, Vector2 value2) =>
    value1.Side((value2 - value1.Vector).GetAngle());
}
