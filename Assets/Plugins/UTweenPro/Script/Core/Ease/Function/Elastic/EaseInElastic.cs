using System;

namespace Aya.TweenPro
{
    public class EaseInElastic : EaseFunction
    {
        public override int Type => EaseType.InElastic;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;

            var d = 1f;
            var p = d * 0.3;
            var s = 0.0;
            var a = 0.0;

            if (Math.Abs(delta) < 1e-6) return from;

            if (Math.Abs((delta /= d) - 1) < 1e-6) return from + to;

            if (Math.Abs(a) < 1e-6 || a < Math.Abs(to))
            {
                a = to;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Math.PI) * Math.Asin(to / a);
            }

            var result = (float) (-(a * Math.Pow(2, 10 * (delta -= 1)) * Math.Sin((delta * d - s) * (2 * Math.PI) / p)) + from);
            return result;
        }
    }
}
