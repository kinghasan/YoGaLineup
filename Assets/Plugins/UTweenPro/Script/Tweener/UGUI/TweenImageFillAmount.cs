using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Image Fill Amount", "UGUI")]
    [Serializable]
    public class TweenImageFillAmount : TweenValueFloat<Image>
    {
        public override float Value
        {
            get => Target.fillAmount;
            set => Target.fillAmount = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenImageFillAmount FillAmount(Image image, float to, float duration)
        {
            var tweener = Play<TweenImageFillAmount, Image, float>(image, to, duration);
            return tweener;
        }

        public static TweenImageFillAmount FillAmount(Image image, float from, float to, float duration)
        {
            var tweener = Play<TweenImageFillAmount, Image, float>(image, from, to, duration);
            return tweener;
        }
    }

    public static partial class ImageExtension
    {
        public static TweenImageFillAmount TweenFillAmount(this Image image, float to, float duration)
        {
            var tweener = UTween.FillAmount(image, to, duration);
            return tweener;
        }

        public static TweenImageFillAmount TweenFillAmount(this Image image, float from, float to, float duration)
        {
            var tweener = UTween.FillAmount(image, from, to, duration);
            return tweener;
        }
    }

    #endregion
}