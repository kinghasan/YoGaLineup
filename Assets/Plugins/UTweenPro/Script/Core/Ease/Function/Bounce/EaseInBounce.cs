namespace Aya.TweenPro
{
    public class EaseInBounce : EaseFunction
    {
        public override int Type => EaseType.InBounce;
        public EaseOutBounce EaseOutBounce = new EaseOutBounce();

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result =  to - EaseOutBounce.Ease(0, to, 1f - delta) + from;
            return result;
        }
    }
}
