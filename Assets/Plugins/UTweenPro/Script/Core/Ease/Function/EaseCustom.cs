using UnityEngine;

namespace Aya.TweenPro
{
    public class EaseCustom : EaseFunction
    {
        public override int Type => EaseType.Custom;
        public override float Ease(float from, float to, float delta)
        {
            return Mathf.LerpUnclamped(from, to, delta);
        }
    }
}