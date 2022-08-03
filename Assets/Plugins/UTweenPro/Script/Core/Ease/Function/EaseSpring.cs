using System;

namespace Aya.TweenPro
{
    public class EaseSpring : EaseFunction
    {
        public override int Type => EaseType.Spring;

        public override float Ease(float from, float to, float delta)
        {
            delta = (float)(Math.Sin(delta * Math.PI * (0.2f + 2.5f * delta * delta * delta)) * Math.Pow(1f - delta, 2.2f) + delta) * (1f + (1.2f * (1f - delta)));
            var result = from + (to - from) * delta;
            return result;
        }
    }
}
