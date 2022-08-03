using System;

namespace Aya.TweenPro
{
    public class EaseInOutExpo : EaseFunction
    {
        public override int Type => EaseType.InOutExpo;

        public override float Ease(float from, float to, float delta)
        {
            delta /= .5f;
            to -= from;
            if (delta < 1)
            {
                var result = (float) (to / 2 * Math.Pow(2, 10 * (delta - 1)) + from);
                return result;
            }
            else
            {
                delta--;
                var result = (float) (to / 2 * (-Math.Pow(2, -10 * delta) + 2) + from);
                return result;
            }
        }
    }
}
