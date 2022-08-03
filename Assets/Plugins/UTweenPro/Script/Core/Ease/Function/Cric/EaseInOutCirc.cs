using System;

namespace Aya.TweenPro
{
    public class EaseInOutCirc : EaseFunction
    {
        public override int Type => EaseType.InOutCirc;

        public override float Ease(float from, float to, float delta)
        {
            float result;
            delta /= .5f;
            to -= from;
            if (delta < 1)
            {
                result = (float) (-to / 2 * (Math.Sqrt(1 - delta * delta) - 1) + from);
            }
            else
            {
                delta -= 2;
                result = (float) (to / 2 * (Math.Sqrt(1 - delta * delta) + 1) + from);
            }
            
            return result;
        }
    }
}
