using UnityEngine;

namespace Aya.TweenPro
{
    public partial class TweenMaterialAlpha : TweenValueFloat<Material>
    {
        public string Property;

        public override float Value
        {
            get => Target.GetColor(Property).a;
            set
            {
                var color = Target.GetColor(Property);
                color.a = value;
                Target.SetColor(Property, color);
            }
        }
    }

    #region Extension

    public partial class TweenMaterialAlpha : TweenValueFloat<Material>
    {
        public TweenMaterialAlpha SetPropertyName(string propertyName)
        {
            Property = propertyName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialAlpha Alpha(Material material, string propertyName, float to, float duration)
        {
            var tweener = Play<TweenMaterialAlpha, Material, float>(material, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialAlpha Alpha(Material material, string propertyName, float from, float to, float duration)
        {
            var tweener = Play<TweenMaterialAlpha, Material, float>(material, from, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }
    }

    public static partial class MaterialExtension
    {
        public static TweenMaterialAlpha TweenAlpha(this Material material, string propertyName, float to, float duration)
        {
            var tweener = UTween.Alpha(material, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialAlpha TweenAlpha(this Material material, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(material, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
