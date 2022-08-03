using UnityEngine;

namespace Aya.TweenPro
{
    public static partial class TweenerExtension
    {
        public static TTweener SetTarget<TTweener, TTarget>(this TTweener tweener, TTarget target) where TTweener : Tweener<TTarget> where TTarget : Object
        {
            if (!tweener.SupportTarget) return tweener;
            tweener.Target = target;
            return tweener;
        }
    }
}