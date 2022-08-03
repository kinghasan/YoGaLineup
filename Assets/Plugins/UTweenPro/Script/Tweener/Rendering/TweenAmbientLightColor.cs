using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Ambient Light Color", "Rendering", "Skybox Icon")]
    [Serializable]
    public partial class TweenAmbientLightColor : TweenValueColor<UnityEngine.Object>
    {
        public override bool SupportTarget => false;

        public override Color Value
        {
            get => RenderSettings.ambientLight;
            set => RenderSettings.ambientLight = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenAmbientLightColor AmbientLightColor(Color to, float duration)
        {
            var tweener = Create<TweenAmbientLightColor>()
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenAmbientLightColor;
            return tweener;
        }

        public static TweenAmbientLightColor AmbientLightColor(Color from, Color to, float duration)
        {
            var tweener = Create<TweenAmbientLightColor>()
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenAmbientLightColor;
            return tweener;
        }

        public static TweenAmbientLightColor AmbientLightColor(Gradient gradient, float duration)
        {
            var tweener = Create<TweenAmbientLightColor>()
                .SetDuration(duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient)
                .Play() as TweenAmbientLightColor;
            return tweener;
        }
    }

    #endregion
}