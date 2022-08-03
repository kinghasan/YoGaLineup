namespace Aya.TweenPro
{
    public class EaseInCubic : EaseFunction
    {
        public override int Type => EaseType.InCubic;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = to * delta * delta * delta + from;
            return result;
        }
    }
}
