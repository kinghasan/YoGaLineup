using UnityEngine;

namespace Aya.TweenPro
{
    public partial class TweenMaterialVector4 : TweenValueVector4<Material>
    {
        public string Property;

        public override Vector4 Value
        {
            get => Target.GetVector(Property);
            set => Target.SetVector(Property, value);
        }
    }

    #region Extension

    public partial class TweenMaterialVector4 : TweenValueVector4<Material>
    {
        public TweenMaterialVector4 SetPropertyName(string propertyName)
        {
            Property = propertyName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialVector4 Vector4(Material material, string propertyName, Vector4 to, float duration)
        {
            var tweener = Play<TweenMaterialVector4, Material, Vector4>(material, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialVector4 Vector4(Material material, string propertyName, Vector4 from, Vector4 to, float duration)
        {
            var tweener = Play<TweenMaterialVector4, Material, Vector4>(material, from, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }
    }

    public static partial class MaterialExtension
    {
        public static TweenMaterialVector4 TweenVector4(this Material material, string propertyName, Vector4 to, float duration)
        {
            var tweener = UTween.Vector4(material, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialVector4 TweenVector4(this Material material, string propertyName, Vector4 from, Vector4 to, float duration)
        {
            var tweener = UTween.Vector4(material, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
