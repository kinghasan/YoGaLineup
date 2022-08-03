using UnityEngine;

namespace Aya.TweenPro
{
    public abstract class TweenYieldInstruction : CustomYieldInstruction
    {
        public TweenData Data;

        protected TweenYieldInstruction(TweenData tweenData)
        {
            Data = tweenData;
        }
    }
}