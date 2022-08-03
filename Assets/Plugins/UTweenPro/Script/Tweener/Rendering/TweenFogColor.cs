using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.TweenPro
{
    [Tweener("Fog Color", "Rendering", "Skybox Icon")]
    [Serializable]
    public partial class TweenFogColor : TweenValueColor<Object>
    {
        public override bool SupportTarget => false;

        public override Color Value
        {
            get => RenderSettings.fogColor;
            set => RenderSettings.fogColor = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenFogColor FogColor(Color to, float duration)
        {
            var tweener = Create<TweenFogColor>()
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenFogColor;
            return tweener;
        }

        public static TweenFogColor FogColor(Color from, Color to, float duration)
        {
            var tweener = Create<TweenFogColor>()
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenFogColor;
            return tweener;
        }

        public static TweenFogColor FogColor(Gradient gradient, float duration)
        {
            var tweener = Create<TweenFogColor>()
                .SetDuration(duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient)
                .Play() as TweenFogColor;
            return tweener;
        }
    }

    #endregion
}