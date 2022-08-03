
namespace Aya.TweenPro
{
    public class WaitForStart : TweenYieldInstruction
    {
        public WaitForStart(TweenData tweenData) : base(tweenData)
        {
        }

        public override bool keepWaiting => !Data.IsPlaying || (Data.IsPlaying && Data.IsDelaying);
    }
}