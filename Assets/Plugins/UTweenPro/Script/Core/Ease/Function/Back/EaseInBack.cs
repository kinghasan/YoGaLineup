namespace Aya.TweenPro
{
    public class EaseInBack : EaseFunction
    {
        public override int Type => EaseType.InBack;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            delta /= 1;
            var s = 1.70158f;
            var result = to * (delta) * delta * ((s + 1) * delta - s) + from;
            return result;
        }
    }
}
