using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public class EaseCos : EaseFunction
    {
        public override int Type => EaseType.Cos;
        public override bool SupportStrength => true;
        public override float DefaultStrength => 0f;

        public override float Ease(float from, float to, float delta)
        {
            return Ease(from, to, delta, DefaultStrength);
        }

        public override float Ease(float from, float to, float delta, float strength)
        {
            var count = Mathf.RoundToInt(Mathf.Lerp(1, 20, strength));
            var range = to - from;
            var result = (float)Math.Cos(Math.PI * (delta * count + 0.5f)) * range;
            return result;
        }
    }
}