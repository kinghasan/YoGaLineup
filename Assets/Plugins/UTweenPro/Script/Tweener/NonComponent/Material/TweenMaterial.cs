using UnityEngine;

namespace Aya.TweenPro
{
    public partial class TweenMaterial : TweenValueFloat<Material>
    {
        public Material Start;
        public Material End;

        public override float Value
        {
            get => _value;
            set
            {
                _value = value;
                Target.Lerp(Start, End, _value);
            }
        }

        private float _value;

        public override void Reset()
        {
            base.Reset();
            Start = null;
            End = null;
        }
    }

    #region Extension

    public partial class TweenMaterial : TweenValueFloat<Material>
    {
        public TweenMaterial SetStartMaterial(Material startMaterial)
        {
            Start = startMaterial;
            return this;
        }

        public TweenMaterial SetEndMaterial(Material endMaterial)
        {
            End = endMaterial;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterial Properties(Material material, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Properties(material, 0f, 1f, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterial Properties(Material material, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Properties(material, 0f, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterial Properties(Material material, float from, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Create<TweenMaterial>()
                .SetTarget(material)
                .SetStartMaterial(startMaterial)
                .SetEndMaterial(endMaterial)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenMaterial;
            return tweener;
        }
    }

    public static partial class MaterialExtension
    {
        public static TweenMaterial TweenProperties(this Material material, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(material, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterial TweenProperties(this Material material, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(material, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterial TweenProperties(this Material material, float from, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(material, from, to, startMaterial, endMaterial, duration);
            return tweener;
        }
    }

    #endregion
}
