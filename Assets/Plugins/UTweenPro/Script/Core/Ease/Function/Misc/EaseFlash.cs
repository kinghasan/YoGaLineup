using UnityEngine;

namespace Aya.TweenPro
{
    public class EaseFlash : EaseFunction
    {
        public override int Type => EaseType.Flash;
        public override bool SupportStrength => true;
        public override float DefaultStrength => 0f;

        public override float Ease(float from, float to, float delta)
        {
            return Ease(from, to, delta, DefaultStrength);
        }

        public override float Ease(float from, float to, float delta, float strength)
        {
            var count = Mathf.RoundToInt(Mathf.Lerp(1, 5, strength)) * 2 + 1;
            var step = 1f / count;
            var current = (int)(delta / step);
            if (current % 2 == 0) return 0f;
            return 1f;
        }
    }
}
