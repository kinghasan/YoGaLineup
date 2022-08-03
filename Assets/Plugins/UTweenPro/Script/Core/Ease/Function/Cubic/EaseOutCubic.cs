namespace Aya.TweenPro
{
    public class EaseOutCubic : EaseFunction
    {
        public override int Type => EaseType.OutCubic;

        public override float Ease(float from, float to, float delta)
        {
            delta--;
            to -= from;
            var result = to * (delta * delta * delta + 1) + from;
            return result;
        }
    }
}
