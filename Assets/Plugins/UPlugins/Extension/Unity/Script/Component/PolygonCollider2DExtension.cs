/////////////////////////////////////////////////////////////////////////////
//
//  Script   : PolygonCollider2DExtension.cs
//  Info     : PolygonCollider2D 多边形碰撞器扩展
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
    public static class PolygonCollider2DExtension
    {
        /// <summary>
        /// 根据路径点设置多边形碰撞
        /// </summary>
        /// <param name="collider2D">碰撞器</param>
        /// <param name="width">路径宽度</param>
        /// <param name="path">路径点列表</param>
        /// <returns>collider2D</returns>
        public static PolygonCollider2D SetColliderWithPath(this PolygonCollider2D collider2D, float width, List<Vector2> path)
        {
            var pointListCache = new List<Vector2>();
            for (var i = 0; i < path.Count; i++)
            {
                pointListCache.Add(path[i]);
            }

            var edgePointList = new List<Vector2>();
            // 以LineRenderer的点位为中心, 沿法线方向与法线反方向各偏移一定距离, 形成一个闭合且不交叉的折线
            for (var j = 1; j < pointListCache.Count; j++)
            {
                // 当前点指向前一点的向量
                var distanceVector = pointListCache[j - 1] - pointListCache[j];
                // 法线向量
                var crossVector = Vector3.Cross(distanceVector, Vector3.forward);
                Vector2 offectVector = crossVector.normalized;
                // 沿法线方向与法线反方向各偏移一定距离
                var up = pointListCache[j - 1] + 0.5f * width * offectVector;
                var down = pointListCache[j - 1] - 0.5f * width * offectVector;
                // 分别加到List的首位和末尾, 保证List中的点位可以围成一个闭合且不交叉的折线
                edgePointList.Insert(0, down);
                edgePointList.Add(up);
                // 加入最后一点
                if (j == pointListCache.Count - 1)
                {
                    up = pointListCache[j] + 0.5f * width * offectVector;
                    down = pointListCache[j] - 0.5f * width * offectVector;
                    edgePointList.Insert(0, down);
                    edgePointList.Add(up);
                }
            }
            collider2D.SetPath(0, pointListCache.ToArray());
            return collider2D;
        }
    }
}
