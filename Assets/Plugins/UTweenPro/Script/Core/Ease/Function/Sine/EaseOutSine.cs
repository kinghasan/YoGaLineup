using System;

namespace Aya.TweenPro
{
    public class EaseOutSine : EaseFunction
    {
        public override int Type => EaseType.OutSine;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = (float) (to * Math.Sin(delta / 1 * (Math.PI / 2)) + from);
            return result;
        }
    }
}
