using System;

namespace Aya.TweenPro
{
    public class EaseInSine : EaseFunction
    {
        public override int Type => EaseType.InSine;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = (float)(-to * Math.Cos(delta / 1 * (Math.PI / 2)) + to + from);
            return result;
        }
    }
}
