/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Math3D.cs
//  Info     : 数学辅助计算类 —— 3D
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Maths
{
    public static partial class MathUtil
    {
        #region Length

        /// <summary>
        /// 增加向量的长度
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static Vector2 AddVectorLength(Vector2 vector, float length)
        {
            var num = Mathf.Sqrt(Vector2.SqrMagnitude(vector));
            num += length;
            vector.Normalize();
            var result = Vector2.Scale(vector, new Vector2(num, num));
            return result;
        }

        /// <summary>
        /// 设置向量的长度
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static Vector2 SetVectorLength(Vector2 vector, float length)
        {
            vector.Normalize();
            var result = vector * length;
            return result;
        }

        #endregion

        #region Abs

        /// <summary>
        /// 对向量的所有值取绝对值
        /// </summary>
        /// <param name="vector2">向量</param>
        /// <returns>结果</returns>
        public static Vector2 Abs(Vector2 vector2)
        {
            var result = new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
            return result;
        }

        #endregion

        #region Distance

        /// <summary>
        /// 距离的平方
        /// 计算距离的开根计算有一定开销，可使用距离的平方比较大小
        /// </summary>
        /// <param name="from">开始</param>
        /// <param name="to">结束</param>
        /// <returns>结果</returns>
        public static float SqrDistance(Vector2 from, Vector2 to)
        {
            var result = Mathf.Pow(from.x - to.x, 2) + Mathf.Pow(from.y - to.y, 2);
            return result;
        }

        /// <summary>
        /// 点到直线距离
        /// </summary>
        /// <param name="vector">点坐标</param>
        /// <param name="linePoint1">直线上一个点的坐标</param>
        /// <param name="linePoint2">直线上另一个点的坐标</param>
        /// <returns>距离</returns>
        public static float DistancePointToLine(Vector2 vector, Vector2 linePoint1, Vector2 linePoint2)
        {
            float space;
            // 线段的长度  
            var a = Vector2.Distance(linePoint1, linePoint2);
            // lineA到点的距离
            var b = Vector2.Distance(linePoint1, vector);
            // lineB到点的距离
            var c = Vector2.Distance(linePoint2, vector);
            if (c <= FloatPrecision || b <= FloatPrecision)
            {
                space = 0;
                return space;
            }
            if (a <= FloatPrecision)
            {
                space = b;
                return space;
            }
            if (c * c >= a * a + b * b)
            {
                space = b;
                return space;
            }
            if (b * b >= a * a + c * c)
            {
                space = c;
                return space;
            }
            // 半周长 
            var p = (a + b + c) / 2;
            // 海伦公式求面积  
            var s = Mathf.Sqrt(p * (p - a) * (p - b) * (p - c));
            // 返回点到线的距离（利用三角形面积公式求高）
            space = 2 * s / a;
            return space;
        }

        #endregion

        #region Segment

        /// <summary>
        /// 判断点是否在线段上
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="p1">线段端点1</param>
        /// <param name="p2">线段端点2</param>
        /// <returns>结果</returns>
        public static bool IsPointOnSegment(Vector2 point, Vector2 p1, Vector2 p2)
        {
            var disLine = Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2);
            var dis1 = Math.Pow(point.x - p1.x, 2) + Math.Pow(point.y - p1.y, 2);
            var dis2 = Math.Pow(point.x - p2.x, 2) + Math.Pow(point.y - p2.y, 2);
            var result = Math.Abs(dis1 + dis2 - disLine) < FloatPrecision;
            return result;
        }

        #endregion

        #region Intersection

        /// <summary>
        /// 判断两条线是否相交
        /// </summary>
        /// <param name="p1">线段1起点坐标</param>
        /// <param name="p2">线段1终点坐标</param>
        /// <param name="p3">线段2起点坐标</param>
        /// <param name="p4">线段2终点坐标</param>
        /// <param name="intersection">相交点坐标</param>
        /// <returns>是否相交 0:两线平行  -1:不平行且未相交  1:两线相交</returns>
        public static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out Vector2 intersection)
        {
            // 判断相交性
            Vector3 vec0 = p4 - p3;   // 线段q的方向
            Vector3 vec1 = p3 - p1;   // 线段q到p两个端点连线的方向
            Vector3 vec2 = p3 - p2;
            if (Vector3.Dot(Vector3.Cross(vec0, vec1), Vector3.Cross(vec0, vec2)) > 0)
            {
                intersection = Vector2.zero;
                return false;
            }

            intersection = Vector2.zero;
            return false;
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 限制角度到360之间
        /// </summary>
        /// <param name="angle">角度</param>
        /// <returns>结果</returns>
        public static float ClampAngle360(float angle)
        {
            while (angle < 0)
            {
                angle += 360f;
            }

            while (angle > 360)
            {
                angle -= 360f;
            }

            return angle;
        }

        /// <summary>
        /// 点绕指定坐标旋转指定角度
        /// </summary>
        /// <param name="origin">原始点</param>
        /// <param name="point">旋转中心</param>
        /// <param name="angle">旋转角度</param>
        /// <returns>旋转后的点</returns>
        public static Vector2 Rotate(Vector2 origin, Vector2 point, float angle)
        {
            try
            {
                // 与中心点相同则不处理
                if (origin == point) return origin;
                // 中心点
                var xx = point.x;
                var yy = point.x;
                var L = Mathf.Sqrt(Mathf.Pow(origin.x - xx, 2) + Mathf.Pow(origin.y - yy, 2));
                var tanSita = (origin.y - yy) * 1.0f / (origin.x - xx);
                var sita = Mathf.Atan(tanSita);
                if (origin.y > yy && sita < 0) sita += Mathf.PI;
                if (origin.y < yy && sita > 0) sita += Mathf.PI;
                if (origin.y < yy && sita < 0) sita += 2 * Mathf.PI;
                if (Math.Abs(origin.y - yy) < FloatPrecision && origin.x > xx) sita = 0;
                if (Math.Abs(origin.y - yy) < FloatPrecision && origin.x < xx) sita = Mathf.PI;
                // 旋转后的点
                var rx = xx + L * Mathf.Cos(sita - angle * 2 * Mathf.PI / 360);
                var ry = yy + L * Mathf.Sin(sita - angle * 2 * Mathf.PI / 360);
                var result = new Vector2(rx, ry);
                return result;
            }
            catch
            {
                return Vector2.zero;
            }
        }

        #endregion

        #region Triangle

        /// <summary>
        /// 点T是否在三角形内
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="a">三角形A点坐标</param>
        /// <param name="b">三角形B点坐标</param>
        /// <param name="c">三角形C点坐标</param>
        /// <returns>结果</returns>
        public static bool IsPointInTriangle(Vector2 point, Vector2 a, Vector2 b, Vector2 c)
        {
            var temp1 = IsPointInIntercept(a, b, c, point);
            var temp2 = IsPointInIntercept(a, c, b, point);
            var temp3 = IsPointInIntercept(b, c, a, point);
            var result = temp1 && temp2 && temp3;
            return result;
        }

        /// <summary>
        /// 点T是否在线AB和C的截距范围内 (三角形碰撞检测辅助方法)
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="a">线端点A</param>
        /// <param name="b">线端点B</param>
        /// <param name="c">三角形的另一个点C</param>
        /// <returns>结果</returns>
        public static bool IsPointInIntercept(Vector2 point, Vector2 a, Vector2 b, Vector2 c)
        {
            // AB延长线在Y轴上的截点，AB过C和T平行线在坐标轴上的截点
            float p1, p2, pt;
            // 斜率不存在时
            if (Math.Abs(a.x - b.x) < FloatPrecision)
            {
                p1 = a.x;
                p2 = c.x;
                pt = point.x;
            }
            else
            {
                // 斜率为0时
                if (Math.Abs(a.y - b.y) < FloatPrecision)
                {
                    p1 = a.y;
                    p2 = c.y;
                    pt = point.y;
                }
                // 斜率不为0时
                else
                {
                    // 斜率
                    var k = (a.y - b.y) / (a.x - b.x);
                    var bb = a.y - k * a.x;
                    // Y轴的截距
                    p1 = bb;
                    p2 = c.y - k * c.x;
                    pt = point.y - k * point.x;
                }
            }
            var result = (pt <= p2 && pt >= p1) || (pt <= p1 && pt >= p2);
            return result;
        }

        #endregion

        #region Polygon

        /// <summary>
        /// 判断点是否在多边形区域内（射线法/支持凹多边形）
        /// </summary>
        /// <param name="point">待判断的点</param>
        /// <param name="polygonPoints">多边形顶点数组</param>
        /// <param name="containEdge">包含边</param>
        /// <returns>true:在多边形内，凹点   false：在多边形外，凸点</returns>
        public static bool IsPointInPolygon(Vector2 point, Vector2[] polygonPoints, bool containEdge = true)
        {
            var result = false;
            var j = polygonPoints.Length - 1;
            for (var i = 0; i < polygonPoints.Length; j = i++)
            {
                var p1 = polygonPoints[i];
                var p2 = polygonPoints[j];
                // 这里判断是否刚好被测点在多边形的边上
                if (IsPointOnSegment(point, p1, p2)) return containEdge;
                if ((p1.y > point.y != p2.y > point.y) && (point.x < (point.y - p1.y) * (p1.x - p2.x) / (p1.y - p2.y) + p1.x))
                {
                    result = !result;
                }
            }
            return result;
        }

        /// <summary>
        /// 计算多边形面积(忽略y轴)
        /// </summary>
        /// <param name="polygonPoints">多边形顶点数组</param>
        /// <returns>结果</returns>
        public static float PolygonArea(Vector3[] polygonPoints)
        {
            var iArea = 0f;
            for (int iCycle = 0, iCount = polygonPoints.Length; iCycle < iCount; iCycle++)
            {
                iArea += (polygonPoints[iCycle].x * polygonPoints[(iCycle + 1) % iCount].z - polygonPoints[(iCycle + 1) % iCount].x * polygonPoints[iCycle].z);
            }

            var result = (float) Math.Abs(0.5 * iArea);
            return result;
        }

        #endregion
    }
}
