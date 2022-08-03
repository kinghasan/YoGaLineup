using System;

namespace Aya.TweenPro
{
    public class EaseInCirc : EaseFunction
    {
        public override int Type => EaseType.InCirc;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = (float) (-to * (Math.Sqrt(1 - delta * delta) - 1) + from);
            return result;
        }
    }
}
