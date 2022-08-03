using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Toggle Value", "UGUI")]
    [Serializable]
    public class TweenToggle : TweenValueBoolean<Toggle>
    {
        public override bool Value
        {
            get => Target.interactable;
            set => Target.interactable = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenToggle Value(Toggle toggle, bool to, float duration)
        {
            var tweener = Play<TweenToggle, Toggle, bool>(toggle, to, duration);
            return tweener;
        }

        public static TweenToggle Value(Toggle toggle, bool from, bool to, float duration)
        {
            var tweener = Play<TweenToggle, Toggle, bool>(toggle, from, to, duration);
            return tweener;
        }
    }

    public static partial class ToggleExtension
    {
        public static TweenToggle TweenValue(this Toggle toggle, bool to, float duration)
        {
            var tweener = UTween.Value(toggle, to, duration);
            return tweener;
        }

        public static TweenToggle TweenValue(this Toggle toggle, bool from, bool to, float duration)
        {
            var tweener = UTween.Value(toggle, from, to, duration);
            return tweener;
        }
    }

    #endregion
}