using Microsoft.Xna.Framework;
using System;

namespace monogametest;

public static class Angle
{
    public static class Arithmetics
    {
        // 0 в cycle не вставлять
        public static float Addition(float value1, int cycle1, float value2, int cycle2) =>
            value1 * cycle1 + value2 * cycle2;

        public static float Addition(float greater, float lesser) =>
            Addition(greater, 2, lesser, 1);

        public static float Subtraction(float value1, int cycle1, float value2, int cycle2) =>
            Addition(value1, cycle1, -value2, cycle2);

        public static float Subtraction(float greater, float lesser) =>
            Subtraction(greater, 2, lesser, 1);
    }

    public static float GetAngle(this Vector2 value) =>
    (float)Math.Atan2(value.Y, value.X);

    public static float GetAngle(this float value) =>
    value % 360;
}
