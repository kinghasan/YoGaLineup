/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MeshFilterExtension.cs
//  Info     : MeshFilter 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class MeshFilterExtension
    {
        /// <summary>
        /// 获取包围盒
        /// </summary>
        /// <param name="meshFilter">网格过滤器</param>
        /// <param name="includeChildren">是否包含子节点</param>
        /// <returns>包围盒</returns>
        public static Bounds GetBounds(this MeshFilter meshFilter, bool includeChildren = true)
        {
            if (includeChildren)
            {
                var center = meshFilter.transform.position;
                var bounds = new Bounds(center, Vector3.zero);
                var meshFilters = meshFilter.gameObject.GetComponentsInChildren<MeshFilter>();
                if (meshFilters.Length == 0) return bounds;
                foreach (var filter in meshFilters)
                {
                    if (filter.mesh != null)
                    {
                        bounds.Encapsulate(filter.mesh.bounds);
                    }
                }
                return bounds;
            }
            else
            {
                return meshFilter.mesh.bounds;
            }
        }
    }
}
