namespace Aya.TweenPro
{
    public class EaseInOutQuad : EaseFunction
    {
        public override int Type => EaseType.InOutQuad;

        public override float Ease(float from, float to, float delta)
        {
            delta /= .5f;
            to -= from;
            if (delta < 1)
            {
                var result = to / 2 * delta * delta + from;
                return result;
            }
            else
            {
                delta--;
                var result = -to / 2 * (delta * (delta - 2) - 1) + from;
                return result;
            }
        }
    }
}
