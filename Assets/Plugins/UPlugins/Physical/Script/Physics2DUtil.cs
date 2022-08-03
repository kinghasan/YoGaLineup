/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Physics2DUtil.cs
//  Info     : 通用2D物理工具类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using Aya.Maths;

namespace Aya.Physical
{
    public static class Physics2DUtil
    {
        #region  OverlapCircle

        /// <summary>
        /// 搜索范围内所有指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static List<T> OverlapCircleAll<T>(Vector2 position, float radius, LayerMask layerMask) where T : Component
        {
            var result = OverlapCircleAll<T>(position, radius, layerMask.value);
            return result;
        }

        /// <summary>
        /// 搜索范围内所有指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static List<T> OverlapCircleAll<T>(Vector2 position, float radius, int layerMask) where T : Component
        {
            var result = new List<T>();
            var cols = Physics2D.OverlapCircleAll(position, radius, layerMask);
            foreach (var col in cols)
            {
                var com = col.gameObject.GetComponentInParent<T>();
                if (com == null) continue;
                result.Add(com);
            }

            return result;
        }

        /// <summary>
        /// 搜索范围内最近的目标
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static T OverlapCircleAllNearest<T>(Vector2 position, float radius, LayerMask layerMask) where T : Component
        {
            var result = OverlapCircleAllNearest<T>(position, radius, layerMask.value);
            return result;
        }

        /// <summary>
        /// 搜索范围内最近的目标
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static T OverlapCircleAllNearest<T>(Vector2 position, float radius, int layerMask) where T : Component
        {
            var components = OverlapCircleAll<T>(position, radius, layerMask);
            if (components.Count == 0) return null;
            var result = components[0];
            var minDis = MathUtil.SqrDistance(position, (Vector2)result.transform.position);
            for (var i = 1; i < components.Count; i++)
            {
                var com = components[i];
                var dis = MathUtil.SqrDistance(position, (Vector2)com.transform.position);
                if (dis < minDis)
                {
                    minDis = dis;
                    result = com;
                }
            }

            return result;
        }

        /// <summary>
        /// 搜索范围内最近的目标
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static List<T> OverlapCircleAllNearest<T>(Vector3 position, float radius, int layerMask, int count) where T : Component
        {
            var components = OverlapCircleAll<T>(position, radius, layerMask);
            if (components.Count == 0) return null;
            var result = new List<T>();

            var comList = new List<T>();

            for (var i = 0; i < components.Count; i++)
            {
                var com = components[i];
                comList.Add(com);
            }

            comList.Sort((c1, c2) =>
            {
                var dis1 = MathUtil.SqrDistance(c1.transform.position, position);
                var dis2 = MathUtil.SqrDistance(c2.transform.position, position);
                return dis1.CompareTo(dis2);
            });

            for (var i = 0; i < count && i < comList.Count; i++)
            {
                result.Add(comList[i]);
            }

            return result;
        }

        /// <summary>
        /// 搜索范围内最远的目标
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static T OverlapCircleAllFarthest<T>(Vector2 position, float radius, LayerMask layerMask) where T : Component
        {
            var result = OverlapCircleAllFarthest<T>(position, radius, layerMask.value);
            return result;
        }

        /// <summary>
        /// 搜索范围内最远的目标
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static T OverlapCircleAllFarthest<T>(Vector2 position, float radius, int layerMask) where T : Component
        {
            var components = OverlapCircleAll<T>(position, radius, layerMask);
            if (components.Count == 0) return null;
            var result = components[0];
            var maxDis = MathUtil.SqrDistance(position, (Vector2)result.transform.position);
            for (var i = 1; i < components.Count; i++)
            {
                var com = components[i];
                var dis = MathUtil.SqrDistance(position, (Vector2)com.transform.position);
                if (dis > maxDis)
                {
                    maxDis = dis;
                    result = com;
                }
            }

            return result;
        }

        /// <summary>
        /// 搜索范围内最远的目标
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="position">中心位置</param>
        /// <param name="radius">半径</param>
        /// <param name="layerMask">层</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static List<T> OverlapCircleAllFarthest<T>(Vector3 position, float radius, int layerMask, int count) where T : Component
        {
            var components = OverlapCircleAll<T>(position, radius, layerMask);
            if (components.Count == 0) return null;
            var result = new List<T>();

            var comList = new List<T>();

            for (var i = 0; i < components.Count; i++)
            {
                var com = components[i];
                comList.Add(com);
            }

            comList.Sort((c1, c2) =>
            {
                var dis1 = MathUtil.SqrDistance(c1.transform.position, position);
                var dis2 = MathUtil.SqrDistance(c2.transform.position, position);
                return dis2.CompareTo(dis1);
            });

            for (var i = 0; i < count && i < comList.Count; i++)
            {
                result.Add(comList[i]);
            }

            return result;
        }

        #endregion
    }
}
