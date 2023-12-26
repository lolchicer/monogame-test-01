using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MonogameTest01;

// возможно можно оставить как IEnumerable<AngleTuple> в пространстве имён для SihlouetteExtensions
public partial class AngleRange
{
    private List<AngleTuple> _tuples = new();

    private AngleRange(IEnumerable<AngleTuple> tuples)
    {
        _tuples.AddRange(tuples);
    }
    public AngleRange() { }
    public AngleRange(Angle Lesser, Angle Greater)
    {
        _tuples.Add(new() { Lesser = Lesser, Greater = Greater });
    }
    public AngleRange(Vector2 o, Vector2 vector)
    {
        _tuples.Add(new AngleTuple(o, vector));
    }
    public AngleRange(Vector2 o, Segment segment)
    {
        _tuples.Add(new AngleTuple(o, segment));
    }
    public AngleRange(Vector2 o, IEnumerable<Segment> segments)
    {
        _tuples.AddRange(
            from segment in segments
            select new AngleTuple(o, segment)
        );
    }

    public bool Contains(FlatAngle value) =>
    _tuples.Any(tuple => tuple.Contains(value));

    public bool Contains(AngleRange value) =>
    value._tuples.All(
        valueTuple => _tuples.Any(
            tuple => tuple.Contains(valueTuple)));

    // оавпрвкервпар
    public AngleRange Range(AngleRange value)
    {
        throw new NotImplementedException();
    }
}
