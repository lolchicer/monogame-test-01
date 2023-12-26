using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MonogameTest01;

public partial class AngleRange
{
    // возможно можно оставить как ValueTuple<Angle, Angle>
    private struct AngleTuple
    {
        public Angle Lesser = new();
        public Angle Greater = new();

        public bool Contains(FlatAngle value) =>
        Lesser.Value <= value.Value &&
        Greater.Value >= value.Value;

        public bool Contains(AngleTuple value) =>
        Lesser.Value <= value.Lesser.Value &&
        Greater.Value >= value.Greater.Value;

        // это нужно оставить в IntersectsExtensions
        public bool Intersects(AngleTuple value) =>
        Lesser.Value <= value.Greater.Value ||
        value.Lesser.Value <= Greater.Value;

        public AngleTuple() { }
        public AngleTuple(Vector2 o, Vector2 vector)
        {
            var difference = o - vector;
            var intersects = o == vector;

            switch (intersects)
            {
                case false:
                    Lesser = new Angle(difference);
                    Greater = new Angle(difference);
                    break;
                default:
                    Lesser = new Angle { Value = 0 };
                    Greater = new Angle { Value = 360 };
                    break;
            }
        }
        public AngleTuple(Vector2 o, Segment segment)
        {
            var difference = (
            o - (segment.Location + segment.Vector1),
            o - (segment.Location + segment.Vector2));
            var intersects = (
                o == difference.Item1,
                o == difference.Item2
            );

            switch (intersects)
            {
                case (false, false):
                    Lesser = new Angle(difference.Item1);
                    Greater = new Angle(difference.Item2);
                    break;
                default:
                    Lesser = new Angle { Value = 0 };
                    Greater = new Angle { Value = 360 };
                    break;
            }
        }

        // можно попробовать сделать методом расширения
        // выглядит как лишнее звено в Sum(...)
        public static IEnumerable<AngleTuple> Sum(AngleTuple value1, AngleTuple value2) =>
        value1.Intersects(value2) switch
        {
            false => new[] { value1, value2 },
            _ => new[] { new AngleTuple() {
            Lesser = new Angle() { Value = Math.Min(value1.Lesser.Value, value2.Lesser.Value) },
            Greater = new Angle() { Value = Math.Max(value1.Greater.Value, value2.Greater.Value) } } }
        };

        // вэ тфэ
        // обращение к приватным методам объектов в статической функции
        public static IEnumerable<AngleTuple> Sum(IEnumerable<AngleTuple> value)
        {
            foreach (var value1 in value)
            {
                foreach (var value2 in value.Except(new[] { value1 }))
                {
                    var tupleSum = Sum(value1, value2);
                    // должна быть функция if и по функции на каждый вариант
                    if (tupleSum.Count() == 1)
                    {
                        var valueNext = new List<AngleTuple>();
                        valueNext.AddRange(value.Except(new[] { value1, value2 }));
                        valueNext.AddRange(tupleSum);
                        return Sum(valueNext);
                    }
                }
            }

            return value;
        }
    }
}
