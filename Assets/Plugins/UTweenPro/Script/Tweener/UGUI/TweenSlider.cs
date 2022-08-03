using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Slider Value", "UGUI")]
    [Serializable]
    public class TweenSlider : TweenValueFloat<Slider>
    {
        public override float Value
        {
            get => Target.value;
            set => Target.value = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenSlider Value(Slider slider, float to, float duration)
        {
            var tweener = Play<TweenSlider, Slider, float>(slider, to, duration);
            return tweener;
        }

        public static TweenSlider Value(Slider slider, float from, float to, float duration)
        {
            var tweener = Play<TweenSlider, Slider, float>(slider, from, to, duration);
            return tweener;
        }
    }

    public static partial class SliderExtension
    {
        public static TweenSlider TweenValue(this Slider slider, float to, float duration)
        {
            var tweener = UTween.Value(slider, to, duration);
            return tweener;
        }

        public static TweenSlider TweenValue(this Slider slider, float from, float to, float duration)
        {
            var tweener = UTween.Value(slider, from, to, duration);
            return tweener;
        }
    }

    #endregion
}