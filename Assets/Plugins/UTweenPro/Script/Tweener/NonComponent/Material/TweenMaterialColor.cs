using UnityEngine;

namespace Aya.TweenPro
{
    public partial class TweenMaterialColor : TweenValueColor<Material>
    {
        public string Property;

        public override Color Value
        {
            get => Target.GetColor(Property);
            set => Target.SetColor(Property, value);
        }
    }

    #region Extension

    public partial class TweenMaterialColor : TweenValueColor<Material>
    {
        public TweenMaterialColor SetPropertyName(string propertyName)
        {
            Property = propertyName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialColor Color(Material material, string propertyName, Color to, float duration)
        {
            var tweener = Play<TweenMaterialColor, Material, Color>(material, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialColor Color(Material material, string propertyName, Color from, Color to, float duration)
        {
            var tweener = Play<TweenMaterialColor, Material, Color>(material, from, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialColor Color(Material material, string propertyName, Gradient gradient, float duration)
        {
            var tweener = Create<TweenMaterialColor>()
                .SetTarget(material)
                .SetPropertyName(propertyName)
                .SetGradient(gradient)
                .SetDuration(duration)
                .Play() as TweenMaterialColor;
            return tweener;
        }
    }

    public static partial class MaterialExtension
    {
        public static TweenMaterialColor TweenColor(this Material material, string propertyName, Color to, float duration)
        {
            var tweener = UTween.Color(material, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialColor TweenColor(this Material material, string propertyName, Color from, Color to, float duration)
        {
            var tweener = UTween.Color(material, propertyName, from, to, duration);
            return tweener;
        }

        public static TweenMaterialColor TweenColor(this Material material, string propertyName, Gradient gradient, float duration)
        {
            var tweener = UTween.Color(material, propertyName, gradient, duration);
            return tweener;
        }
    }

    #endregion
}
