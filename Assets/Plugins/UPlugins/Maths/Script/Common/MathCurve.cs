/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MathCurve.cs
//  Info     : 数学辅助计算类 —— 曲线
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Maths
{
    public static partial class MathUtil
    {
        #region CatmullRom

        /// <summary>
        /// 样条曲线
        /// </summary>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">控制点3</param>
        /// <param name="p4">控制点4</param>
        /// <param name="delta">插值因子</param>
        /// <returns>结果</returns>
        public static Vector3 CatmullRom(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float delta)
        {
            var factor = 0.5f;
            var c0 = p2;
            var c1 = (p3 - p1) * factor;
            var c2 = (p3 - p2) * 3f - (p4 - p2) * factor - (p3 - p1) * 2f * factor;
            var c3 = (p3 - p2) * -2f + (p4 - p2) * factor + (p3 - p1) * factor;
            var result = c3 * delta * delta * delta + c2 * delta * delta + c1 * delta + c0;
            return result;
        }

        #endregion

        #region Bezier
        
        /// <summary>
        /// 获取贝塞尔曲线(3个点为二次,4个点为三次,其他返回空)
        /// </summary>
        /// <param name="points">控制点集</param>
        /// <param name="count">曲线段数</param>
        /// <returns>结果</returns>
        public static List<Vector3> GetBezierPoints(Vector3[] points, int count = 10)
        {
            var outList = new List<Vector3>();
            if (points.Length == 3)
            {
                for (float i = 0f; i <= count; i++)
                {
                    outList.Add(GetBezierPoint(points[0], points[1], points[2], i / count));
                }
            }
            if (points.Length == 4)
            {
                for (float i = 0f; i <= count; i++)
                {
                    outList.Add(GetBezierPoint(points[0], points[1], points[2], points[3], i / count));
                }
            }
            return outList;
        }

        /// <summary>
        /// 获取二次贝塞尔曲线上点
        /// </summary>
        /// <param name="p1">起始点</param>
        /// <param name="p2">控制点</param>
        /// <param name="p3">末尾点</param>
        /// <param name="delta">间隔</param>
        /// <returns>结果</returns>
        public static Vector3 GetBezierPoint(Vector3 p1, Vector3 p2, Vector3 p3, float delta)
        {
            var u = 1 - delta;
            var result = u * u * p1 + 2 * delta * u * p2 + delta * delta * p3;
            return result;
        }

        /// <summary>
        /// 获取三次贝塞尔曲线上点
        /// </summary>
        /// <param name="p1">起始点</param>
        /// <param name="p2">控制点1</param>
        /// <param name="p3">控制点2</param>
        /// <param name="p4">末尾点</param>
        /// <param name="delta">间隔</param>
        /// <returns>结果</returns>
        public static Vector3 GetBezierPoint(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float delta)
        {
            var u = 1 - delta;
            var result = u * u * u * p1 + 3 * u * u * delta * p2 + 3 * delta * delta * u * p3 + delta * delta * delta * p4;
            return result;
        }

        #endregion

        #region Curve Path

        /// <summary>
        /// 获取曲线上面的所有路径点列表（自动计算所需点数量）
        /// </summary>
        /// <param name="wayPoints">路径点列表</param>
        /// <returns>结果</returns>
        public static Vector3[] GetCurvePathPoints(Vector3[] wayPoints)
        {
            var controlPointList = GetPathControlPointGenerator(wayPoints);
            var smoothAmount = 0;
            // 根据 路点 间的距离计算所需 路径点 的数量
            for (int i = 0, length = wayPoints.Length - 1; i < length; i++)
            {
                smoothAmount += (int)Vector3.Distance(wayPoints[i + 1], wayPoints[i]) / 4;
            }

            var pointArr = new Vector3[smoothAmount];
            for (var index = 1; index <= smoothAmount; index++)
            {
                pointArr[index - 1] = GetPathInterpValue(controlPointList, (float)index / smoothAmount);
            }

            return pointArr;
        }

        /// <summary>
        /// 获取控制点
        /// </summary>et
        /// <param name="path">路径点</param>
        /// <returns>控制点</returns>
        private static Vector3[] GetPathControlPointGenerator(Vector3[] path)
        {
            var offset = 2;
            var suppliedPath = path;
            var controlPoint = new Vector3[suppliedPath.Length + offset];
            Array.Copy(suppliedPath, 0, controlPoint, 1, suppliedPath.Length);
            controlPoint[0] = controlPoint[1] + (controlPoint[1] - controlPoint[2]);
            controlPoint[controlPoint.Length - 1] =
                controlPoint[controlPoint.Length - 2] + (controlPoint[controlPoint.Length - 2] - controlPoint[controlPoint.Length - 3]);

            if (controlPoint[1] == controlPoint[controlPoint.Length - 2])
            {
                var tmpLoopSpline = new Vector3[controlPoint.Length];
                Array.Copy(controlPoint, tmpLoopSpline, controlPoint.Length);
                tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
                tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
                controlPoint = new Vector3[tmpLoopSpline.Length];
                Array.Copy(tmpLoopSpline, controlPoint, tmpLoopSpline.Length);
            }

            return (controlPoint);
        }

        /// <summary>
        /// 插值获取曲线路径上的点
        /// </summary>
        /// <param name="points">点列表</param>
        /// <param name="delta">插值因子</param>
        /// <returns>结果</returns>
        private static Vector3 GetPathInterpValue(Vector3[] points, float delta)
        {
            var numSections = points.Length - 3;  
            var currPt = Mathf.Min(Mathf.FloorToInt(delta * (float)numSections), numSections - 1);
            var u = delta * (float)numSections - (float)currPt;
            var a = points[currPt];
            var b = points[currPt + 1];
            var c = points[currPt + 2];
            var d = points[currPt + 3];
            var result = 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
            return result;
        }

        #endregion
    }
}