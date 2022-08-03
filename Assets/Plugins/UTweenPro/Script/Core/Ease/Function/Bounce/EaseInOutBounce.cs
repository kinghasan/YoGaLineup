namespace Aya.TweenPro
{
    public class EaseInOutBounce : EaseFunction
    {
        public override int Type => EaseType.InOutBounce;
        public EaseInBounce EaseInBounce = new EaseInBounce();
        public EaseOutBounce EaseOutBounce = new EaseOutBounce();

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var d = 1f;
            if (delta < d / 2f)
            {
                var result = EaseInBounce.Ease(0, to, delta * 2f) * 0.5f + from;
                return result;
            }
            else
            {
                var result = EaseOutBounce.Ease(0, to, delta * 2f - d) * 0.5f + to * 0.5f + from;
                return result;
            }
        }
    }
}
