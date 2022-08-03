
namespace Aya.TweenPro
{
    public class WaitForComplete: TweenYieldInstruction
    {
        public WaitForComplete(TweenData tweenData) : base(tweenData)
        {
        }

        public override bool keepWaiting => !Data.IsCompleted;
    }
}