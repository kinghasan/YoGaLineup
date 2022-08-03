/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RendererExtension.cs
//  Info     : Renderer 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class RendererExtension
    {
        /// <summary>
        /// 是否可见
        /// </summary>
        /// <param name="renderer">渲染器</param>
        /// <param name="camera">摄像机</param>
        /// <returns>结果</returns>
        public static bool IsVisible(this Renderer renderer, Camera camera)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        /// <param name="renderer">渲染器</param>
        /// <param name="includeChildren">是否包含子节点</param>
        /// <returns>包围盒</returns>
        public static Bounds GetBounds(this Renderer renderer, bool includeChildren = true)
        {
            if (includeChildren)
            {
                var center = renderer.transform.position;
                var bounds = new Bounds(center, Vector3.zero);
                var renderers = renderer.gameObject.GetComponentsInChildren<Renderer>();
                if (renderers.Length == 0) return bounds;
                foreach (var r in renderers)
                {
                    bounds.Encapsulate(r.bounds);
                }
                return bounds;
            }
            else
            {
                return renderer.bounds;
            }
        }
    }
}