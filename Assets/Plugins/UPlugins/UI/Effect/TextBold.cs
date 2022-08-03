/////////////////////////////////////////////////////////////////////////////
//
//  Script   : TextBold.cs
//  Info     : UI 文本加粗效果
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Text))]
    [AddComponentMenu("UI/Effects/Text Bold")]
    public class TextBold : BaseMeshEffect
    {
        [Range(0, 1)] 
        public float Alpha;
        [Range(1, 5)] 
        public int Strength;

        protected string RichText = "";
        private Text _text = null;

        private Text TextComp
        {
            get
            {
                if (_text == null)
                {
                    _text = GetComponent<Text>();
                }

                return _text;
            }
        }

        protected Color EffectColor => TextComp == null ? Color.black : TextComp.color;

        protected void ApplyShadowZeroAlloc(List<UIVertex> vertexes, Color32 color, int start, int end, float x, float y)
        {
            var num = vertexes.Count + end - start;
            if (vertexes.Capacity < num)
            {
                vertexes.Capacity = num;
            }

            for (var index = start; index < end; ++index)
            {
                var vertex = vertexes[index];
                vertexes.Add(vertex);
                var position = vertex.position;
                position.x += x;
                position.y += y;
                vertex.position = position;
                var color32 = color;
                color32.a = (byte) ((int) color32.a * (int) vertexes[index].color.a / (int) byte.MaxValue);
                color32.a = (byte) (Alpha * color32.a);
                vertex.color = color32;
                vertexes[index] = vertex;
            }
        }

        private static readonly Regex boldBeginRegex = new Regex("<b>", RegexOptions.Singleline);
        private static readonly Regex boldEndRegex = new Regex("</b>", RegexOptions.Singleline);

        private MatchCollection _begin = null;
        private MatchCollection _end = null;


        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
            {
                return;
            }

            var vertexes = new List<UIVertex>();
            vh.GetUIVertexStream(vertexes);

            if (!string.IsNullOrEmpty(RichText) && _begin != null && _end != null)
            {
                var offset = 0;
                for (var i = 0; i < _begin.Count && i < _end.Count; ++i)
                {
                    for (var j = 0; j < Strength; ++j)
                    {
                        ApplyShadowZeroAlloc(vertexes, EffectColor, (_begin[i].Index - offset) * 6,
                            (_end[i].Index - offset - 3) * 6, 0, 0);
                    }

                    offset += 7;
                }
            }
            else
            {
                for (var i = 0; i < Strength; ++i)
                {
                    ApplyShadowZeroAlloc(vertexes, EffectColor, 0, vertexes.Count, 0, 0);
                }
            }

            vh.Clear();
            vh.AddUIVertexTriangleStream(vertexes);
        }
        public void SetText(string text)
        {
            RichText = text;
            _begin = boldBeginRegex.Matches(RichText);
            _end = boldEndRegex.Matches(RichText);

            text = text.Replace("<b>", "");
            text = text.Replace("</b>", "");

            if (_text != null)
            {
                _text.text = text;
            }
        }
    }
}
