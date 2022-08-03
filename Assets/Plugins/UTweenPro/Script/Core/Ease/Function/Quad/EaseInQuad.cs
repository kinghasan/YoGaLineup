namespace Aya.TweenPro
{
    public class EaseInQuad : EaseFunction
    {
        public override int Type => EaseType.InQuad;

        public override float Ease(float from, float to, float delta)
        {
            to -= from;
            var result = to * delta * delta + from;
            return result;
        }
    }
}
