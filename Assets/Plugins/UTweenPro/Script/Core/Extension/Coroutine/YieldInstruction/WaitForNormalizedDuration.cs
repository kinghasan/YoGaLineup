
using UnityEngine;

namespace Aya.TweenPro
{
    public class WaitForNormalizedDuration : TweenYieldInstruction
    {
        public float NormalizedDuration;

        public WaitForNormalizedDuration(TweenData tweenData, float normalizedDuration) : base(tweenData)
        {
            NormalizedDuration = Mathf.Clamp01(normalizedDuration);
        }

        public override bool keepWaiting => Data.RuntimeNormalizedProgress < NormalizedDuration;
    }
}