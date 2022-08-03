/////////////////////////////////////////////////////////////////////////////
//
//  Script   : LineRendererExtension.cs
//  Info     : LineRenderer 线渲染器扩展
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class LineRendererExtension
    {
        /// <summary>
        /// 设置路径点
        /// </summary>
        /// <param name="lineRenderer">线渲染器</param>
        /// <param name="path">路径点列表</param>
        /// <returns>lineRenderer</returns>
        public static LineRenderer SetPath(this LineRenderer lineRenderer, List<Vector3> path)
        {
            lineRenderer.positionCount = path.Count;
            lineRenderer.SetPositions(path.ToArray());
            return lineRenderer;
        }

        /// <summary>
        /// 设置路径点
        /// </summary>
        /// <param name="lineRenderer">线渲染器</param>
        /// <param name="path">路径点列表</param>
        /// <returns>lineRenderer</returns>
        public static LineRenderer SetPath(this LineRenderer lineRenderer, Vector3[] path)
        {
            lineRenderer.positionCount = path.Length;
            lineRenderer.SetPositions(path);
            return lineRenderer;
        }

        /// <summary>
        /// 清除路径点
        /// </summary>
        /// <param name="lineRenderer">线渲染器</param>
        /// <returns>lineRenderer</returns>
        public static LineRenderer Clear(this LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = 0;
            return lineRenderer;
        }
    }
}
