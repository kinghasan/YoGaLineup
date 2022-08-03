using System;
using UnityEngine;
using UnityEngine.UI;
#if  UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    public enum ColorBlockPropertyType
    {
        Normal = 0,
        Highlighted = 1,
        Pressed = 2,
        Selected = 3,
        Disabled = 5,
    }

    [Tweener("Selectable Color Block", "UGUI")]
    [Serializable]
    public partial class TweenSelectableColorBlock : TweenValueColor<Selectable>
    {
        public ColorBlockPropertyType Property;

        public override Color Value
        {
            get
            {
                if (Property == ColorBlockPropertyType.Normal) return Normal;
                if (Property == ColorBlockPropertyType.Highlighted) return Highlighted;
                if (Property == ColorBlockPropertyType.Pressed) return Pressed;
                if (Property == ColorBlockPropertyType.Selected) return Selected;
                if (Property == ColorBlockPropertyType.Disabled) return Disabled;
                return Normal;
            }
            set
            {
                if (Property == ColorBlockPropertyType.Normal) Normal = value;
                if (Property == ColorBlockPropertyType.Highlighted) Highlighted = value;
                if (Property == ColorBlockPropertyType.Pressed) Pressed = value;
                if (Property == ColorBlockPropertyType.Selected) Selected = value;
                if (Property == ColorBlockPropertyType.Disabled) Disabled = value;
            }
        }

        public Color Normal
        {
            get => Target.colors.normalColor;
            set
            {
                var block = Target.colors;
                block.normalColor = value;
                Target.colors = block;
            }
        }

        public Color Highlighted
        {
            get => Target.colors.highlightedColor;
            set
            {
                var block = Target.colors;
                block.highlightedColor = value;
                Target.colors = block;
            }
        }

        public Color Pressed
        {
            get => Target.colors.pressedColor;
            set
            {
                var block = Target.colors;
                block.pressedColor = value;
                Target.colors = block;
            }
        }

        public Color Selected
        {
            get => Target.colors.selectedColor;
            set
            {
                var block = Target.colors;
                block.selectedColor = value;
                Target.colors = block;
            }
        }

        public Color Disabled
        {
            get => Target.colors.disabledColor;
            set
            {
                var block = Target.colors;
                block.disabledColor = value;
                Target.colors = block;
            }
        }
    }

#if UNITY_EDITOR

    public partial class TweenSelectableColorBlock : TweenValueColor<Selectable>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty PropertyProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();
            EditorGUILayout.PropertyField(PropertyProperty, new GUIContent(nameof(Property)));
        }
    }

#endif

    #region Extension

    public partial class TweenSelectableColorBlock : TweenValueColor<Selectable>
    {
        public TweenSelectableColorBlock SetColorBlock(ColorBlockPropertyType colorBlockPropertyType)
        {
            Property = colorBlockPropertyType;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenSelectableColorBlock ColorBlock(Selectable selectable, ColorBlockPropertyType propertyType, Color to, float duration)
        {
            var tweener = Play<TweenSelectableColorBlock, Selectable, Color>(selectable, to, duration)
                .SetColorBlock(propertyType);
            return tweener;
        }

        public static TweenSelectableColorBlock ColorBlock(Selectable selectable, ColorBlockPropertyType propertyType, Color from, Color to, float duration)
        {
            var tweener = Play<TweenSelectableColorBlock, Selectable, Color>(selectable, from, to, duration)
                .SetColorBlock(propertyType);
            return tweener;
        }
    }

    public static partial class SelectableExtension
    {
        public static TweenSelectableColorBlock TweenColorBlock(this Selectable selectable, ColorBlockPropertyType propertyType, Color to, float duration)
        {
            var tweener = UTween.ColorBlock(selectable, propertyType, to, duration);
            return tweener;
        }

        public static TweenSelectableColorBlock TweenColorBlock(this Selectable selectable, ColorBlockPropertyType propertyType, Color from, Color to, float duration)
        {
            var tweener = UTween.ColorBlock(selectable, propertyType, from, to, duration);
            return tweener;
        }
    }

    #endregion
}