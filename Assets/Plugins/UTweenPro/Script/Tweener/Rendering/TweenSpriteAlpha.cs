using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Sprite Alpha", "Rendering")]
    [Serializable]
    public class TweenSpriteAlpha : TweenValueFloat<SpriteRenderer>
    {
        public override float Value
        {
            get => Target.color.a;
            set
            {
                var color = Target.color;
                color.a = value;
                Target.color = color;
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenSpriteAlpha Alpha(SpriteRenderer spriteRenderer, float to, float duration)
        {
            var tweener = Play<TweenSpriteAlpha, SpriteRenderer, float>(spriteRenderer, to, duration);
            return tweener;
        }

        public static TweenSpriteAlpha Alpha(SpriteRenderer spriteRenderer, float from, float to, float duration)
        {
            var tweener = Play<TweenSpriteAlpha, SpriteRenderer, float>(spriteRenderer, from, to, duration);
            return tweener;
        }
    }

    public static partial class SpriteRendererExtension
    {
        public static TweenSpriteAlpha TweenAlpha(this SpriteRenderer spriteRenderer, float to, float duration)
        {
            var tweener = UTween.Alpha(spriteRenderer, to, duration);
            return tweener;
        }

        public static TweenSpriteAlpha TweenAlpha(this SpriteRenderer spriteRenderer, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(spriteRenderer, from, to, duration);
            return tweener;
        }
    }

    #endregion
}