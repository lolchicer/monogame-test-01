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
    // нужно добавить Side.Center
    public static (AngleRange Left, AngleRange Right) Side(this Line value1, AngleRange value2) => (
        Left: value2.Range(
            new AngleRange(
                new Angle { Value = value1.Angle.Value },
                new Angle { Value = value1.Angle.Value + 180 }
                )),
        Right: value2.Range(
            new AngleRange(
                new Angle { Value = value1.Angle.Value + 180 },
                new Angle { Value = value1.Angle.Value }
            )
        )
    );

    // значения типа float в данном случае обозначают величины углов в градусах
    public static Side Side(this FlatAngle value1, FlatAngle value2)
    => (value2.Value - value1.Value) switch
    {
        < 180 => monogametest.Side.Left,
        180 => monogametest.Side.Center,
        _ => monogametest.Side.Right
    };

    public static Side Side(this Line value1, FlatAngle value2) =>
    value1.Angle.Side(value2);
}
