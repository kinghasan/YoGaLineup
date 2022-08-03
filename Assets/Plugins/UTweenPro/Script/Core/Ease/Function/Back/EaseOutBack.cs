namespace Aya.TweenPro
{
    public class EaseOutBack : EaseFunction
    {
        public override int Type => EaseType.OutBack;

        public override float Ease(float from, float to, float delta)
        {
            var s = 1.70158f;
            to -= from;
            delta = (delta / 1) - 1;
            var result = to * ((delta) * delta * ((s + 1) * delta + s) + 1) + from;
            return result;
        }
    }
}
