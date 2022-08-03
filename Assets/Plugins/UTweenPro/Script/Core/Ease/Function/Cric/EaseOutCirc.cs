using System;

namespace Aya.TweenPro
{
    public class EaseOutCirc : EaseFunction
    {
        public override int Type => EaseType.OutCirc;

        public override float Ease(float from, float to, float delta)
        {
            delta--;
            to -= from;
            var result = (float) (to * Math.Sqrt(1 - delta * delta) + from);
            return result;
        }
    }
}
