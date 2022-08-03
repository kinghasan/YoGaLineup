
namespace Aya.TweenPro
{
    public class WaitForElapsedLoops : TweenYieldInstruction
    {
        public int Loop;

        public WaitForElapsedLoops(TweenData tweenData, int loop) : base(tweenData)
        {
            Loop = loop;
        }

        public override bool keepWaiting => Data.LoopCounter < Loop;
    }
}