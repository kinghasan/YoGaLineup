namespace Aya.TweenPro
{
    public class EaseOutQuad : EaseFunction
    {
        public override int Type => EaseType.OutQuad;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = -to * delta * (delta - 2f) + from;
            return result;
        }
    }
}
