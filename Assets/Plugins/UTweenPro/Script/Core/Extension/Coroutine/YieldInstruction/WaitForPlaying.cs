
namespace Aya.TweenPro
{
    public class WaitForPlaying : TweenYieldInstruction
    {
        public WaitForPlaying(TweenData tweenData) : base(tweenData)
        {
        }

        public override bool keepWaiting => !Data.IsPlaying;
    }
}