using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace monogametest;

public static class янехочувставлятьэтовContainsExtensionsпокачто
{
    public static bool Contains(this (float Angle, Anglerange.Sign Sign) containing, float angle) =>
    containing.Sign switch
    {
        // не хватает отдельного типа для углов
        Anglerange.Sign.Lesser => angle.GetAngle() <= containing.Angle,
        Anglerange.Sign.Greater => angle.GetAngle() >= containing.Angle,
        _ => false
    };

    public static bool Contains(this (float Lesser, float Greater) containing, float angle) =>
    containing.Lesser <= angle.GetAngle() &&
    containing.Greater >= angle.GetAngle();

    public static (float Lesser, float Greater) Aboba(float lesser, float greater)
    {
        if (greater - lesser >= 360)
            return (0, 360);
        else
            return (lesser.GetAngle(), greater.GetAngle());
    }

    // стоит избавиться от этого
    public static IEnumerable<(float Lesser, float Greater)> Sum(this (float Lesser, float Greater) value1, (float Lesser, float Greater) value2)
    {
        if (value1.Lesser <= value2.Greater || value2.Lesser <= value2.Greater)
            return new[] { (Math.Min(value1.Lesser, value2.Lesser), Math.Max(value1.Greater, value2.Greater)) };
        else
            return new[] { value1, value2 };
    }

    // вэ тфэ
    public static IEnumerable<(float Lesser, float Greater)> Sum(this IEnumerable<(float Lesser, float Greater)> value)
    {
        foreach (var value1 in value)
        {
            foreach (var value2 in value.Except(new[] { value1 }))
            {
                var tupleSum = Sum(value1, value2);
                if (tupleSum.Count() == 1)
                {
                    var valueNext = new List<(float Lesser, float Greater)>();
                    valueNext.AddRange(value.Except(new[] { value1, value2 }));
                    valueNext.AddRange(tupleSum);
                    return valueNext.Sum();
                }
            }
        }

        return value;
    }

    public static bool Contains(this IEnumerable<(float Lesser, float Greater)> containing, float angle) =>
    containing.Any(tuple => tuple.Contains(angle));
}

public class Anglerange
{
    public enum Sign
    {
        Lesser,
        Greater
    }

    private List<(float, Sign)> _constraints = new();



    private bool Contains(float angle)
    {
        var constraints = new List<(float, Sign)>();

        constraints.AddRange(new[] { (0, Sign.Greater), (0, Sign.Lesser) })
    }
}
