/////////////////////////////////////////////////////////////////////////////
//
//  Script   : UIGradient.cs
//  Info     : UI 渐变色效果
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.UI
{
    [RequireComponent(typeof(MaskableGraphic))]
    [AddComponentMenu("UI/Effects/UI Gradient")]
    public class UIGradient : BaseMeshEffect
    {
        /// <summary>
        /// 渐变样式
        /// </summary>
        public enum GradientType
        {
            Horizontal,
            Vertical,
            LeftBank,
            RightBank
        }

        /// <summary>
        /// 颜色混合模式
        /// </summary>
        public enum ColorBlendMode
        {
            Override,
            Additive,
            Multiply,
            Minus
        }

        public GradientType Type = GradientType.Vertical;
        public ColorBlendMode BlendMode = ColorBlendMode.Override;
        public Color32 StartColor = Color.white;
        public Color32 EndColor = Color.black;
        public bool UseGraphicAlpha = true;

        public override void ModifyMesh(VertexHelper helper)
        {
            if (!IsActive() || helper.currentVertCount == 0)
            {
                return;
            }
            var vertices = new List<UIVertex>();
            helper.GetUIVertexStream(vertices);
            var startColor = StartColor;
            var endColor = EndColor;
            // 计算中间色，倾斜时需要使用
            var middleColor = Color.Lerp(StartColor, EndColor, 0.5f);
            // 根据不同类型设置网格每个顶点的颜色
            switch (Type)
            {
                case GradientType.Vertical:
                    for (var i = 0; i < vertices.Count && vertices.Count - i >= 6;)
                    {
                        ChangeColor(ref vertices, i + 0, startColor);
                        ChangeColor(ref vertices, i + 1, startColor);
                        ChangeColor(ref vertices, i + 2, endColor);
                        ChangeColor(ref vertices, i + 3, endColor);
                        ChangeColor(ref vertices, i + 4, endColor);
                        ChangeColor(ref vertices, i + 5, startColor);
                        i += 6;
                    }
                    break;
                case GradientType.Horizontal:
                    for (var i = 0; i < vertices.Count && vertices.Count - i >= 6;)
                    {
                        ChangeColor(ref vertices, i + 0, endColor);
                        ChangeColor(ref vertices, i + 1, startColor);
                        ChangeColor(ref vertices, i + 2, startColor);
                        ChangeColor(ref vertices, i + 3, startColor);
                        ChangeColor(ref vertices, i + 4, endColor);
                        ChangeColor(ref vertices, i + 5, endColor);
                        i += 6;
                    }
                    break;
                case GradientType.LeftBank:
                    for (var i = 0; i < vertices.Count && vertices.Count - i >= 6;)
                    {
                        ChangeColor(ref vertices, i + 0, startColor);
                        ChangeColor(ref vertices, i + 1, middleColor);
                        ChangeColor(ref vertices, i + 2, endColor);
                        ChangeColor(ref vertices, i + 3, endColor);
                        ChangeColor(ref vertices, i + 4, middleColor);
                        ChangeColor(ref vertices, i + 5, startColor);
                        i += 6;
                    }
                    break;
                case GradientType.RightBank:
                    for (var i = 0; i < vertices.Count && vertices.Count - i >= 6;)
                    {
                        ChangeColor(ref vertices, i + 0, middleColor);
                        ChangeColor(ref vertices, i + 1, startColor);
                        ChangeColor(ref vertices, i + 2, middleColor);
                        ChangeColor(ref vertices, i + 3, middleColor);
                        ChangeColor(ref vertices, i + 4, endColor);
                        ChangeColor(ref vertices, i + 5, middleColor);
                        i += 6;
                    }
                    break;
            }

            helper.Clear();
            helper.AddUIVertexTriangleStream(vertices);
        }

        protected void ChangeColor(ref List<UIVertex> verList, int index, Color color)
        {
            var temp = verList[index];
            var newColor = BlendColor(temp.color, color, BlendMode);
            if (UseGraphicAlpha)
            {
                newColor.a *= temp.color.a * 1f / 255f;
            }

            temp.color = newColor;
            verList[index] = temp;
        }

        protected Color BlendColor(Color oldColor, Color newColor, ColorBlendMode blendMode)
        {
            if (blendMode == ColorBlendMode.Override)
            {
                return newColor;
            }
            else if (blendMode == ColorBlendMode.Additive)
            {
                return oldColor + newColor;
            }
            else if (blendMode == ColorBlendMode.Multiply)
            {
                return oldColor * newColor;
            }
            else if (blendMode == ColorBlendMode.Minus)
            {
                return oldColor - newColor;
            }

            return newColor;
        }
    }
}