using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace monogametest;

public static class CoversExtensions
{
    public static bool Covers(this Vector2 value, Vector2 covering, Vector2 covered) =>
    (covering - value).GetAngle() ==
    (covered - value).GetAngle();

    public static bool Covers(this Vector2 value, Line covering, Vector2 covered) =>
    covering.Side(value) !=
    covering.Side(covered);

    public static bool Covers(this Vector2 value, Ray covering, Vector2 covered) => 
    new Segment(value, covered).Side(covering.Angle) !=
    new Segment(value, covered).Side(covering.Location - value) || (
    new Segment(value, covered).Side(covering.Angle) == Side.Center &&
    new Segment(value, covered).Side(covering.Location - value) == Side.Center) &&
    covering.Side(value) !=
    covering.Side(covered) || (
    covering.Side(value) == Side.Center &&
    covering.Side(covered) == Side.Center);

    public static bool Covers(this Vector2 value, Segment covering, Vector2 covered) => 
    new Segment(value, covered).Side(covering.Location + covering.Vector1 - value) !=
    new Segment(value, covered).Side(covering.Location + covering.Vector2 - value) || (
    new Segment(value, covered).Side(covering.Location + covering.Vector1 - value) == Side.Center &&
    new Segment(value, covered).Side(covering.Location + covering.Vector2 - value) == Side.Center) &&
    covering.Side(value) !=
    covering.Side(covered) || (
    covering.Side(value) == Side.Center &&
    covering.Side(covered) == Side.Center);

    public static bool Covers(this Vector2 value, Segment covering, Vector2 covered) => 
    new Segment(value, covered).Side(covering.Location + covering.Vector1 - value) !=
    new Segment(value, covered).Side(covering.Location + covering.Vector2 - value) || (
    new Segment(value, covered).Side(covering.Location + covering.Vector1 - value) == Side.Center &&
    new Segment(value, covered).Side(covering.Location + covering.Vector2 - value) == Side.Center) &&
    covering.Side(value) !=
    covering.Side(covered) || (
    covering.Side(value) == Side.Center &&
    covering.Side(covered) == Side.Center);
}