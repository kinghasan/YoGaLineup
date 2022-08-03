namespace Aya.TweenPro
{
    public class EaseInQuint : EaseFunction
    {
        public override int Type => EaseType.InQuint;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = to * delta * delta * delta * delta * delta + from;
            return result;
        }
    }
}
