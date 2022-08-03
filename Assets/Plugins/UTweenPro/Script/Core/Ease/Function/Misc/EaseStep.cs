using UnityEngine;

namespace Aya.TweenPro
{
    public class EaseStep : EaseFunction
    {
        public override int Type => EaseType.Step;
        public override bool SupportStrength => true;
        public override float DefaultStrength => 0f;

        public override float Ease(float from, float to, float delta)
        {
            return Ease(from, to, delta, DefaultStrength);
        }

        public override float Ease(float from, float to, float delta, float strength)
        {
            var count = Mathf.RoundToInt(Mathf.Lerp(2, 10, strength));
            var step = 1f / count;
            var current = (int)(delta / step);
            var result = current * step;
            return result;
        }
    }
}