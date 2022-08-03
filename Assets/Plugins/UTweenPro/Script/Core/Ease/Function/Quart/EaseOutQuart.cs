namespace Aya.TweenPro
{
    public class EaseOutQuart : EaseFunction
    {
        public override int Type => EaseType.OutQuart;

        public override float Ease(float from, float to, float delta)
        {
            delta--;
            to -= from;
            var result = -to * (delta * delta * delta * delta - 1) + from;
            return result;
        }
    }
}
