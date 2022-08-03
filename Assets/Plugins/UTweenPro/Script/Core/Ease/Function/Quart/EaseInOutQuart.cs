﻿namespace Aya.TweenPro
{
    public class EaseInOutQuart : EaseFunction
    {
        public override int Type => EaseType.InOutQuart;

        public override float Ease(float from, float to, float delta)
        {
            delta /= .5f;
            to -= from;
            if (delta < 1)
            {
                var result = to / 2 * delta * delta * delta * delta + from;
                return result;
            }
            else
            {
                delta -= 2;
                var result = -to / 2 * (delta * delta * delta * delta - 2) + from;
                return result;
            }
        }
    }
}
