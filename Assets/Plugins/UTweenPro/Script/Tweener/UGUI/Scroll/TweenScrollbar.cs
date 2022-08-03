using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Scrollbar Value", "UGUI Scroll")]
    [Serializable]
    public class TweenScrollbar : TweenValueFloat<Scrollbar>
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
        public static TweenScrollbar Value(Scrollbar slider, float to, float duration)
        {
            var tweener = Play<TweenScrollbar, Scrollbar, float>(slider, to, duration);
            return tweener;
        }

        public static TweenScrollbar Value(Scrollbar slider, float from, float to, float duration)
        {
            var tweener = Play<TweenScrollbar, Scrollbar, float>(slider, from, to, duration);
            return tweener;
        }
    }

    public static partial class ScrollbarExtension
    {
        public static TweenScrollbar TweenValue(this Scrollbar slider, float to, float duration)
        {
            var tweener = UTween.Value(slider, to, duration);
            return tweener;
        }

        public static TweenScrollbar TweenValue(this Scrollbar slider, float from, float to, float duration)
        {
            var tweener = UTween.Value(slider, from, to, duration);
            return tweener;
        }
    }

    #endregion
}