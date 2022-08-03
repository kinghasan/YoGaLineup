using System;

namespace Aya.TweenPro
{
    public class EaseParabola : EaseFunction
    {
        public override int Type => EaseType.Parabola;

        public override float Ease(float from, float to, float delta)
        {
            var range = to - from;
            var half = range / 2f;
            var result = range - (float)Math.Pow((delta - half) * range * 2f, 2f);
            return result;
        }
    }
}
