namespace Aya.TweenPro
{
    public class EaseOutQuint : EaseFunction
    {
        public override int Type => EaseType.OutQuint;

        public override float Ease(float from, float to, float delta)
        {
            delta--;
            to -= from;
            var result = to * (delta * delta * delta * delta * delta + 1) + from;
            return result;
        }
    }
}
