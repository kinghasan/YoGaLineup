using UnityEngine;

namespace Aya.TweenPro
{
    public partial class TweenMaterialFloat : TweenValueFloat<Material>
    {
        public string Property;

        public override float Value
        {
            get => Target.GetFloat(Property);
            set => Target.SetFloat(Property, value);
        }
    }

    #region Extension

    public partial class TweenMaterialFloat : TweenValueFloat<Material>
    {
        public TweenMaterialFloat SetPropertyName(string propertyName)
        {
            Property = propertyName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialFloat Float(Material material, string propertyName, float to, float duration)
        {
            var tweener = Play<TweenMaterialFloat, Material, float>(material, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialFloat Float(Material material, string propertyName, float from, float to, float duration)
        {
            var tweener = Play<TweenMaterialFloat, Material, float>(material, from, to, duration)
                .SetPropertyName(propertyName);
            return tweener;
        }
    }

    public static partial class MaterialExtension
    {
        public static TweenMaterialFloat TweenFloat(this Material material, string propertyName, float to, float duration)
        {
            var tweener = UTween.Float(material, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialFloat TweenFloat(this Material material, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Float(material, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
