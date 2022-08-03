using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("GameObject Active", "Misc")]
    [Serializable]
    public class TweenGameObjectActive : TweenValueBoolean<GameObject>
    {
        public override bool Value
        {
            get => Target.activeSelf;
            set => Target.SetActive(value);
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenGameObjectActive Active(GameObject gameObject, bool to, float duration)
        {
            var tweener = Play<TweenGameObjectActive, GameObject, bool>(gameObject, to, duration);
            return tweener;
        }

        public static TweenGameObjectActive Active(GameObject gameObject, bool from, bool to, float duration)
        {
            var tweener = Play<TweenGameObjectActive, GameObject, bool>(gameObject, from, to, duration);
            return tweener;
        }
    }

    public static partial class GameObjectExtension
    {
        public static TweenGameObjectActive TweenActive(this GameObject gameObject, bool to, float duration)
        {
            var tweener = UTween.Active(gameObject, to, duration);
            return tweener;
        }

        public static TweenGameObjectActive TweenActive(this GameObject gameObject, bool from, bool to, float duration)
        {
            var tweener = UTween.Active(gameObject, from, to, duration);
            return tweener;
        }
    }

    #endregion
}