
namespace Aya.TweenPro
{
    public abstract class EaseFunction
    {
        public abstract int Type { get; }
        public virtual bool SupportStrength { get; } = false;
        public virtual float DefaultStrength { get; } = 1f;

        public virtual float Ease(float from, float to, float delta)
        {
            return default;
        }

        public virtual float Ease(float from, float to, float delta, float strength)
        {
            return Ease(from, to, delta);
        }
    }
}
