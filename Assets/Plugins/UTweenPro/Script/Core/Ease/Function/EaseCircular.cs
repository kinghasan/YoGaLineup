﻿using System;

namespace Aya.TweenPro
{
    public class EaseCircular : EaseFunction
    {
        public override int Type => EaseType.Circular;

        public override float Ease(float from, float to, float delta)
        {
            const float min = 0.0f;
            const float max = 360.0f;
            var half = Math.Abs((max - min) / 2.0f);
            float result;
            float diff;
            if ((to - from) < -half)
            {
                diff = ((max - from) + to) * delta;
                result = from + diff;
            }
            else if ((to - from) > half)
            {
                diff = -((max - to) + from) * delta;
                result = from + diff;
            }
            else result = from + (to - from) * delta;

            return result;
        }
    }
}
