namespace Aya.TweenPro
{
    public class EaseInQuart : EaseFunction
    {
        public override int Type => EaseType.InQuart;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = to * delta * delta * delta * delta + from;
            return result;
        }
    }
}
