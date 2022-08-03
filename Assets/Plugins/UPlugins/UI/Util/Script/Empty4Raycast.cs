/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Empty4Raycast.cs
//  Info     : UI辅助 - 空网格射线接收，用于无渲染的情况下接收点击事件
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine.UI;

namespace Aya.UI
{
    public class Empty4Raycast : MaskableGraphic
    {

        protected Empty4Raycast() { useLegacyMeshGeneration = false; }

        protected override void OnPopulateMesh(VertexHelper toFill) { toFill.Clear(); }
    }
}