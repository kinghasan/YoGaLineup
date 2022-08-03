using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Behaviour Enabled", "Misc", "cs Script Icon")]
    [Serializable]
    public class TweenBehaviourEnabled : TweenValueBoolean<Behaviour>
    {
        public override bool Value
        {
            get => Target.enabled;
            set => Target.enabled = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenBehaviourEnabled Enabled(Behaviour behaviour, bool to, float duration)
        {
            var tweener = Play<TweenBehaviourEnabled, Behaviour, bool>(behaviour, to, duration);
            return tweener;
        }

        public static TweenBehaviourEnabled Enabled(Behaviour behaviour, bool from, bool to, float duration)
        {
            var tweener = Play<TweenBehaviourEnabled, Behaviour, bool>(behaviour, from, to, duration);
            return tweener;
        }
    }

    public static partial class BehaviourExtension
    {
        public static TweenBehaviourEnabled TweenEnabled(this Behaviour behaviour, bool to, float duration)
        {
            var tweener = UTween.Enabled(behaviour, to, duration);
            return tweener;
        }

        public static TweenBehaviourEnabled TweenEnabled(this Behaviour behaviour, bool from, bool to, float duration)
        {
            var tweener = UTween.Enabled(behaviour, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
