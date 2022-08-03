using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Image Alpha", "UGUI")]
    [Serializable]
    public class TweenImageAlpha : TweenValueFloat<Image>
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
        public static TweenImageAlpha Alpha(Image image, float to, float duration)
        {
            var tweener = Play<TweenImageAlpha, Image, float>(image, to, duration);
            return tweener;
        }

        public static TweenImageAlpha Alpha(Image image, float from, float to, float duration)
        {
            var tweener = Play<TweenImageAlpha, Image, float>(image, from, to, duration);
            return tweener;
        }
    }

    public static partial class ImageExtension
    {
        public static TweenImageAlpha TweenAlpha(this Image image, float to, float duration)
        {
            var tweener = UTween.Alpha(image, to, duration);
            return tweener;
        }

        public static TweenImageAlpha TweenAlpha(this Image image, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(image, from, to, duration);
            return tweener;
        }
    }

    #endregion
}