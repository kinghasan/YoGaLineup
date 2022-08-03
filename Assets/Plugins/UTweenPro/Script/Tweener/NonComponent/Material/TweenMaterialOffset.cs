using UnityEngine;

namespace Aya.TweenPro
{
    public partial class TweenMaterialOffset : TweenValueVector2<Material>
    {
        public string Property;

        public override Vector2 Value
        {
            get => Target.GetTextureOffset(Property);
            set => Target.SetTextureOffset(Property, value);
        }
    }

    #region Extension

    public partial class TweenMaterialOffset : TweenValueVector2<Material>
    {
        public TweenMaterialOffset SetPropertyName(string propertyName)
        {
            Property = propertyName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialOffset Offset(Material material, string propertyName, Vector2 to, float duration)
        {
            var tweener = Play<TweenMaterialOffset, Material, Vector2>(material, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialOffset Offset(Material material, string propertyName, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenMaterialOffset, Material, Vector2>(material, from, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }
    }

    public static partial class MaterialExtension
    {
        public static TweenMaterialOffset TweenOffset(this Material material, string propertyName, Vector2 to, float duration)
        {
            var tweener = UTween.Offset(material, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialOffset TweenOffset(this Material material, string propertyName, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.Offset(material, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
