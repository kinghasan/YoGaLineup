
namespace Aya.TweenPro
{
    public class WaitForDuration : TweenYieldInstruction
    {
        public float Duration;

        public WaitForDuration(TweenData tweenData, float duration) : base(tweenData)
        {
            Duration = duration;
            if (Duration > tweenData.Duration) Duration = tweenData.Duration;
        }

        public override bool keepWaiting => Data.PlayTimer < Duration;
    }
}