using System;

namespace Aya.TweenPro
{
    public class EaseInExpo : EaseFunction
    {
        public override int Type => EaseType.InExpo;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = (float)(to * Math.Pow(2, 10 * (delta / 1 - 1)) + from);
            return result;
        }
    }
}
