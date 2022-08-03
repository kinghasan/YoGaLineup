using System;

namespace Aya.TweenPro
{
    public class EaseInOutSine : EaseFunction
    {
        public override int Type => EaseType.InOutSine;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = (float) (-to / 2 * (Math.Cos(Math.PI * delta / 1) - 1) + from);
            return result;
        }
    }
}
