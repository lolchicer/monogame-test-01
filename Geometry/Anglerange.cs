using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace monogametest;

public static class янехочувставлятьэтовContainsExtensionsпокачто
{
    public static bool Contains(this (float Angle, TupleAngleRange.Sign Sign) containing, float angle) =>
    containing.Sign switch
    {
        // не хватает отдельного типа для углов
        TupleAngleRange.Sign.Lesser => angle.GetAngle() <= containing.Angle,
        TupleAngleRange.Sign.Greater => angle.GetAngle() >= containing.Angle,
        _ => false
    };





    public static bool Contains(this IEnumerable<(float Lesser, float Greater)> containing, float angle) =>
    containing.Any(tuple => tuple.Contains(angle));
}

public interface IAngleRange
{
    public enum Sign
    {
        Lesser,
        Greater
    }

    public struct Constraint
    {
        public Angle Angle;
        public Sign Sign;
    }

    public bool Contains(Angle value);
    public IEnumerable<Constraint> Constraints { get; }
}

public class CompleteAngleRange : IAngleRange
{
    public bool Contains(Angle value) => true;

    public IEnumerable<IAngleRange.Constraint> Constraints => new IAngleRange.Constraint[] {
        new() { Angle = new() { Value = 0 }, Sign = IAngleRange.Sign.Greater },
        new() { Angle = new() { Value = 360 }, Sign = IAngleRange.Sign.Lesser } };
}

public class TupleAngleRange : IAngleRange
{
    private struct AngleTuple
    {
        public Angle Lesser = new();
        public Angle Greater = new();

        public bool Contains(Angle angle) =>
        Lesser.Value <= angle.Value &&
        Greater.Value >= angle.Value;

        public AngleTuple() { }
    }

    private List<(float, Sign)> _constraints = new();

    private List<AngleTuple> _tuples = new();

    public TupleAngleRange() { }
    private TupleAngleRange(IEnumerable<AngleTuple> tuples)
    {
        _tuples.AddRange(tuples);
    }

    public bool Contains(Angle angle)
    {
        var constraints = new List<(float, Sign)>();

        constraints.AddRange(new[] { (0, Sign.Greater), (0, Sign.Lesser) })
    }

    public static bool Complete(this AngleTuple value1, AngleTuple value2)
    {
        
    }

    // стоит избавиться от этого
    private static IAngleRange Sum(this AngleTuple value1, AngleTuple value2) => (
    value1.Lesser.Value <= value2.Greater.Value,
    value2.Lesser.Value <= value2.Greater.Value) switch
    {
        (true, true) => new CompleteAngleRange(),
        (false, false) => new TupleAngleRange(new[] { value1, value2 }),
        _ => new TupleAngleRange(new[] { new AngleTuple() {
            Lesser = new Angle() { Value = Math.Min(value1.Lesser.Value, value2.Lesser.Value) },
            Greater = new Angle() { Value = Math.Max(value1.Greater.Value, value2.Greater.Value) } } })
    };

    // вэ тфэ
    private static IAngleRange Sum(IAngleRange value)
    {
        foreach (var value1 in value.Constraints)
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
}
