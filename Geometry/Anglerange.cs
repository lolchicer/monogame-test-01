using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace monogametest;

// возможно можно оставить как IEnumerable<AngleTuple> в пространстве имён для SihlouetteExtensions
public class AngleRange
{   
    // возможно можно оставить как ValueTuple<Angle, Angle>
    private struct AngleTuple
    {
        public Angle Lesser = new();
        public Angle Greater = new();

        public bool Contains(IAngle value) =>
        Lesser.Value <= value.Value &&
        Greater.Value >= value.Value;

        // это нужно оставить в IntersectsExtensions
        public bool Intersects(AngleTuple value) =>
        Lesser.Value <= value.Greater.Value ||
        value.Lesser.Value <= Greater.Value;

        public AngleTuple() { }
    }

    private List<AngleTuple> _tuples = new();

    public AngleRange() { }
    private AngleRange(IEnumerable<AngleTuple> tuples)
    {
        _tuples.AddRange(tuples);
    }

    public bool Contains(IAngle angle) =>
    _tuples.Any(tuple => tuple.Contains(angle));

    // можно попробовать сделать методом расширения
    // выглядит как лишнее звено в Sum(...)
    private static AngleRange Sum(AngleTuple value1, AngleTuple value2) =>
    value1.Intersects(value2) switch
    {
        false => new AngleRange(new[] { value1, value2 }),
        _ => new AngleRange(new[] { new AngleTuple() {
            Lesser = new Angle() { Value = Math.Min(value1.Lesser.Value, value2.Lesser.Value) },
            Greater = new Angle() { Value = Math.Max(value1.Greater.Value, value2.Greater.Value) } } })
    };

    // вэ тфэ
    // обращение к приватным методам объектов в статической функции
    private static AngleRange Sum(AngleRange value)
    {
        foreach (var value1 in value._tuples)
        {
            foreach (var value2 in value._tuples.Except(new[] { value1 }))
            {
                var tupleSum = Sum(value1, value2);
                // должна быть функция if и по функции на каждый вариант
                if (tupleSum._tuples.Count == 1)
                {
                    var valueNext = new AngleRange();
                    valueNext._tuples.AddRange(value._tuples.Except(new[] { value1, value2 }));
                    valueNext._tuples.AddRange(tupleSum._tuples);
                    return Sum(valueNext);
                }
            }
        }

        return value;
    }
}
