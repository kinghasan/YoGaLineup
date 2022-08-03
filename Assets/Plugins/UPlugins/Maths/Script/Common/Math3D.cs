/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Math3D.cs
//  Info     : 数学辅助计算类 —— 2D
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
        #region Length

        /// <summary>
        /// 增加向量的长度
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static Vector3 AddVectorLength(Vector3 vector, float length)
        {
            var num = Vector3.Magnitude(vector);
            num += length;
            var normalize = Vector3.Normalize(vector);
            var result = Vector3.Scale(normalize, new Vector3(num, num, num));
            return result;
        }

        /// <summary>
        /// 设置向量的长度
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static Vector3 SetVectorLength(Vector3 vector, float length)
        {
            var normalize = Vector3.Normalize(vector);
            var result = normalize * length;
            return result;
        }

        #endregion

        #region Abs

        /// <summary>
        /// 对向量的所有值取绝对值
        /// </summary>
        /// <param name="vector3">向量</param>
        /// <returns>结果</returns>
        public static Vector3 Abs(Vector3 vector3)
        {
            var result = new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
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
        public static float SqrDistance(Vector3 from, Vector3 to)
        {
            var result = Mathf.Pow(from.x - to.x, 2) + Mathf.Pow(from.y - to.y, 2) + Mathf.Pow(from.z - to.z, 2);
            return result;
        }

        /// <summary>
        /// 点到直线距离
        /// </summary>
        /// <param name="point">点坐标</param>
        /// <param name="p1">直线上一个点的坐标</param>
        /// <param name="p2">直线上另一个点的坐标</param>
        /// <returns>距离</returns>
        public static float DistancePointToLine(Vector3 point, Vector3 p1, Vector3 p2)
        {
            var vec1 = point - p1;
            var vec2 = p2 - p1;
            var project = Vector3.Project(vec1, vec2);
            var dis = Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(project), 2));
            return dis;
        }

        /// <summary>
        /// 点到平面的距离
        /// </summary>
        /// <param name="point"></param>
        /// <param name="planePoint"></param>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public static float DistancePointToPlane(Vector3 point, Vector3 planePoint, Vector3 planeNormal)
        {
            var result = Vector3.Dot(planeNormal, point - planePoint);
            return result;
        }

        #endregion

        #region Closest Points

        /// <summary>
        /// 寻找点距离某线上的最近点<para/>
        /// 可用于物体跟踪路线<para/>
        /// </summary>
        /// <param name="p1">线上的点1</param>
        /// <param name="p2">线上的点2</param>
        /// <param name="point">点</param>
        /// <returns>结果</returns>
        public static Vector3 NearestPointToLine(Vector3 p1, Vector3 p2, Vector3 point)
        {
            var lineDirection = Vector3.Normalize(p2 - p1);
            var closestPoint = Vector3.Dot((point - p1), lineDirection);
            var result = p1 + (closestPoint * lineDirection);
            return result;
        }

        /// <summary>
        /// 寻找点距离某线段的最近点<para/>
        /// 可用于物体跟踪路线<para/>
        /// </summary>
        /// <param name="p1">线起点</param>
        /// <param name="p2">线终点</param>
        /// <param name="point">点</param>
        /// <returns>结果</returns>
        public static Vector3 NearestPointToLineSegment(Vector3 p1, Vector3 p2, Vector3 point)
        {
            var fullDirection = p2 - p1;
            var lineDirection = Vector3.Normalize(fullDirection);
            var closestPoint = Vector3.Dot((point - p1), lineDirection);
            var result = p1 + (Mathf.Clamp(closestPoint, 0.0f, Vector3.Magnitude(fullDirection)) * lineDirection);
            return result;
        }

        /// <summary>
        /// 三维空间中两条直线上彼此最近的两个点
        /// </summary>
        /// <param name="p1">直线1起点</param>
        /// <param name="v1">直线1向量</param>
        /// <param name="p2">直线2起点</param>
        /// <param name="v2">直线2向量</param>
        /// <param name="closestPoint1">直线1上的最近点</param>
        /// <param name="closestPoint2">直线2上的最近点</param>
        /// <returns>是否有结果</returns>
        public static bool NearestPointsOnLines(Vector3 p1, Vector3 v1, Vector3 p2, Vector3 v2, out Vector3 closestPoint1, out Vector3 closestPoint2)
        {
            closestPoint1 = Vector3.zero;
            closestPoint2 = Vector3.zero;
            var num = Vector3.Dot(v1, v1);
            var num2 = Vector3.Dot(v1, v2);
            var num3 = Vector3.Dot(v2, v2);
            var num4 = num * num3 - num2 * num2;
            if (Math.Abs(num4) > FloatPrecision)
            {
                var rhs = p1 - p2;
                var num5 = Vector3.Dot(v1, rhs);
                var num6 = Vector3.Dot(v2, rhs);
                var d = (num2 * num6 - num5 * num3) / num4;
                var d2 = (num * num6 - num5 * num2) / num4;
                closestPoint1 = p1 + v1 * d;
                closestPoint2 = p2 + v2 * d2;
                return true;
            }
            return false;
        }

        #endregion

        #region Check Side

        /// <summary>
        /// 判断一个点在一个线段的哪一边
        /// </summary>
        /// <param name="p1">线段起点</param>
        /// <param name="p2">线段终点</param>
        /// <param name="point">点</param>
        /// <returns>1 0 2</returns>
        public static int PointOnWhichSideOfLineSegment(Vector3 p1, Vector3 p2, Vector3 point)
        {
            var rhs = p2 - p1;
            var lhs = point - p1;
            var num = Vector3.Dot(lhs, rhs);
            if (num <= 0f)
            {
                return 1;
            }
            if (lhs.magnitude <= rhs.magnitude)
            {
                return 0;
            }
            return 2;
        }

        #endregion

        #region Project

        /// <summary>
        /// 一个点在一条直线上的投影点
        /// </summary>
        /// <param name="linePoint">直线上的点</param>
        /// <param name="lineVector">直线的向量</param>
        /// <param name="point">点</param>
        /// <returns>结果</returns>
        public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVector, Vector3 point)
        {
            var lhs = point - linePoint;
            var d = Vector3.Dot(lhs, lineVector);
            var result = linePoint + lineVector * d;
            return result;
        }

        /// <summary>
        ///  一个点在一条线段上的投影点
        /// </summary>
        /// <param name="p1">线段起点</param>
        /// <param name="p2">线段终点</param>
        /// <param name="point"></param>
        /// <returns>结果</returns>
        public static Vector3 ProjectPointOnLineSegment(Vector3 p1, Vector3 p2, Vector3 point)
        {
            var vector = ProjectPointOnLine(p1, (p2 - p1).normalized, point);
            var num = PointOnWhichSideOfLineSegment(p1, p2, vector);
            if (num == 0)
            {
                return vector;
            }
            if (num == 1)
            {
                return p1;
            }
            if (num == 2)
            {
                return p2;
            }
            return Vector3.zero;
        }

        /// <summary>
        /// 空间中一个点在一个面上的投影点
        /// </summary>
        /// <param name="planePoint">平面上一个点</param>
        /// <param name="planeNormal">平面的法线</param>
        /// <param name="point">点</param>
        /// <returns>结果</returns>
        public static Vector3 ProjectPointOnPlane(Vector3 planePoint, Vector3 planeNormal, Vector3 point)
        {
            var num = DistancePointToPlane(point, planePoint, planeNormal);
            num *= -1f;
            var b = SetVectorLength(planeNormal, num);
            var result = point + b;
            return result;
        }

        /// <summary>
        /// 一个向量在一个面上的投影向量
        /// </summary>
        /// <param name="planeNormal"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 ProjectVectorOnPlane(Vector3 planeNormal, Vector3 vector)
        {
            var result = vector - Vector3.Dot(vector, planeNormal) * planeNormal;
            return result;
        }

        #endregion

        #region Intersection

        /// <summary>
        /// 判断线与线之间的相交
        /// </summary>
        /// <param name="intersection">交点</param>
        /// <param name="p1">直线1上一点</param>
        /// <param name="v1">直线1方向</param>
        /// <param name="p2">直线2上一点</param>
        /// <param name="v2">直线2方向</param>
        /// <returns>是否相交</returns>
        public static bool LineIntersection(Vector3 p1, Vector3 v1, Vector3 p2, Vector3 v2, out Vector3 intersection)
        {
            intersection = Vector3.zero;
            if (Math.Abs(Vector3.Dot(v1, v2) - 1) <= FloatPrecision)
            {
                // 两线平行
                return false;
            }

            var startPointSeg = p2 - p1;
            var vecS1 = Vector3.Cross(v1, v2);            // 有向面积1
            var vecS2 = Vector3.Cross(startPointSeg, v2); // 有向面积2
            var num = Vector3.Dot(startPointSeg, vecS1);

            // 判断两这直线是否共面
            if (num >= FloatPrecision || num <= -FloatPrecision)
            {
                return false;
            }

            // 有向面积比值，利用点乘是因为结果可能是正数或者负数
            var num2 = Vector3.Dot(vecS2, vecS1) / vecS1.sqrMagnitude;

            intersection = p1 + v1 * num2;
            return true;
        }

        /// <summary>
        /// 计算两直线的起点分别到交点的有向距离
        /// </summary>
        /// <param name="p1">直线1起点</param>
        /// <param name="v1">直线1向量</param>
        /// <param name="p2">直线2起点</param>
        /// <param name="v2">直线2向量</param>
        /// <param name="len1">直线1起点到交点的距离</param>
        /// <param name="len2">直线2起点到交点的距离</param>
        /// <returns>是否相交</returns>
        public static bool LineIntersection(Vector3 p1, Vector3 v1, Vector3 p2, Vector3 v2, out float len1, out float len2)
        {
            len1 = float.PositiveInfinity;
            len2 = float.PositiveInfinity;
            var lhs = p2 - p1;
            var rhs = Vector3.Cross(v1, v2);
            var lhs2 = Vector3.Cross(lhs, v2);
            var lhs3 = Vector3.Cross(lhs, v1);
            var num = Vector3.Dot(lhs, rhs);
            if (num >= FloatPrecision || num <= -FloatPrecision)
            {
                return false;
            }
            len1 = Vector3.Dot(lhs2, rhs) / rhs.sqrMagnitude;
            len2 = Vector3.Dot(lhs3, rhs) / rhs.sqrMagnitude;
            return true;
        }

        /// <summary>
        /// 两个面是否相交
        /// </summary>
        /// <param name="planePoint1">平面1上的点</param>
        /// <param name="planeNormal1">平面1的法线</param>
        /// <param name="planePoint2">平面2上的点</param>
        /// <param name="planeNormal2">平面2的法线</param>
        /// <param name="linePoint">相交点</param>
        /// <param name="lineVector">相交线的向量</param>
        /// <returns>结果</returns>
        public static bool PlaneIntersection(Vector3 planePoint1, Vector3 planeNormal1, Vector3 planePoint2, Vector3 planeNormal2, out Vector3 linePoint, out Vector3 lineVector)
        {
            linePoint = Vector3.zero;
            lineVector = Vector3.zero;
            lineVector = Vector3.Cross(planeNormal1, planeNormal2);
            var vector = Vector3.Cross(planeNormal2, lineVector);
            var num = Vector3.Dot(planeNormal1, vector);
            if (Mathf.Abs(num) > 0.006f)
            {
                var rhs = planePoint1 - planePoint2;
                var d = Vector3.Dot(planeNormal1, rhs) / num;
                linePoint = planePoint2 + d * vector;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 线面是否相交
        /// </summary>
        /// <param name="linePoint">线上一个点</param>
        /// <param name="lineVector">线的向量</param>
        /// <param name="planePoint">平面上一个点</param>
        /// <param name="planeNormal">平面的法线</param>
        /// <param name="intersection">交点</param>
        /// <returns>结果</returns>
        public static bool LinePlaneIntersection(Vector3 linePoint, Vector3 lineVector, Vector3 planePoint, Vector3 planeNormal, out Vector3 intersection)
        {
            intersection = Vector3.zero;
            var num = Vector3.Dot(planePoint - linePoint, planeNormal);
            var num2 = Vector3.Dot(lineVector, planeNormal);
            if (Math.Abs(num2) > FloatPrecision)
            {
                var size = num / num2;
                var vector = SetVectorLength(lineVector, size);
                intersection = linePoint + vector;
                return true;
            }
            return false;
        }



        #endregion

        #region Angle

        /// <summary>
        /// 计算两个向量的夹角度数<para/>
        /// </summary>
        /// <param name="vector1">向量1</param>
        /// <param name="vector2">向量2</param>
        /// <returns>结果</returns>
        public static float Angle(Vector3 vector1, Vector3 vector2)
        {
            var result = Mathf.Acos(Vector3.Dot(vector1.normalized, vector2.normalized)) * Mathf.Rad2Deg;
            return result;
        }

        /// <summary>
        /// 计算两个向量的夹角度数(带正负号)
        /// </summary>
        /// <param name="vector1">向量1</param>
        /// <param name="vector2">向量2</param>
        /// <param name="normal">所处平面法向量</param>
        /// <returns>结果</returns>
        public static float AngleSigned(Vector3 vector1, Vector3 vector2, Vector3 normal)
        {
            var result = Mathf.Atan2(Vector3.Dot(normal, Vector3.Cross(vector1, vector2)), Vector3.Dot(vector1, vector2)) * Mathf.Rad2Deg;
            return result;
        }

        /// <summary>
        /// 一个向量和平面的夹角
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="planeNormal">平面法线</param>
        /// <returns>结果</returns>
        public static float AngleVectorPlane(Vector3 vector, Vector3 planeNormal)
        {
            var num = Vector3.Dot(vector, planeNormal);
            var num2 = (float)Math.Acos((double)num);
            var result = 1.57079637f - num2;
            return result;
        }

        #endregion

        #region Plane

        /// <summary>
        /// 三个点确定一个平面
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <param name="p3">点3</param>
        /// <returns>平面的法线</returns>
        public static Vector3 PlaneFrom3Points(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var vector = p2 - p1;
            var vector2 = p3 - p1;
            var planeNormal = Vector3.Normalize(Vector3.Cross(vector, vector2));
            var vector3 = p1 + vector / 2f;
            var vector4 = p1 + vector2 / 2f;
            var lineVec = p3 - vector3;
            var lineVec2 = p2 - vector4;
            NearestPointsOnLines(vector3, lineVec, vector4, lineVec2, out _, out _);
            return planeNormal;
        }

        #endregion

        #region  Sphere

        /// <summary>
        /// 获取球体所有顶点坐标
        /// </summary>
        /// <param name="center">球心</param>
        /// <param name="radius">半径</param>
        /// <param name="angle">角度</param>
        /// <returns>结果</returns>
        public static Vector3[] GetSphereVertices(Vector3 center, float radius, int angle)
        {
            var vertices = new List<Vector3>();
            vertices.Clear();
            var direction = Vector3.zero; // 射线方向
            var point = new Vector3(center.x, center.y + radius, center.z);
            vertices.Add(point); // 添加圆球最高点
            // 通过球坐标系方式遍历所需的点并加入网格顶点中
            for (var theta = 1; theta <= angle; theta += 1)
            {
                for (var alpha = 0; alpha < 360; alpha += 1)
                {
                    var radTheta = theta * Mathf.Deg2Rad;
                    var radAlpha = alpha * Mathf.Deg2Rad;

                    // 计算出方向向量
                    direction.Set(Mathf.Sin(radTheta) * Mathf.Cos(radAlpha), Mathf.Cos(radTheta), Mathf.Sin(radTheta) * Mathf.Sin(radAlpha));
                    point = center + radius * direction;
                    vertices.Add(point);
                }
            }

            // 加入圆心点
            vertices.Add(center);
            return vertices.ToArray();
        }

        #endregion

        #region Rotate
        
        /// <summary>
        /// 点绕指定坐标按轴旋转指定角度
        /// </summary>
        /// <param name="origin">原始点</param>
        /// <param name="point">旋转中心</param>
        /// <param name="axis">旋转轴</param>
        /// <param name="angle">旋转角度</param>
        /// <returns>结果</returns>
        public static Vector3 Rotate(Vector3 origin, Vector3 point, Vector3 axis, float angle)
        {
            // 旋转系数
            var quaternion = Quaternion.AngleAxis(angle, axis);
            // 旋转中心到源点的偏移向量
            var offset = origin - point;
            // 旋转偏移向量，得到旋转中心到目标点的偏移向量
            offset = quaternion * offset;
            var result = point + offset;
            return result;
        } 

        #endregion

        #region Circle

        /// <summary>
        /// 获取半径为r的圆的点坐标集合
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="radius">半径</param>
        /// <param name="deltaAngle">间隔角度</param>
        /// <returns>结果</returns>
        public static Vector3[] GetCirclePoints(Vector3 center, float radius, int deltaAngle)
        {
            var points = new Vector3[deltaAngle];
            for (int i = 0, j = 0; i < 360; j++, i += 360 / deltaAngle)
            {
                var angle = Mathf.Deg2Rad * i;
                points[j] = center + new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            }
            return points;
        }

        #endregion

        #region Ellipse

        /// <summary>
        /// 获取椭圆上的点
        /// </summary>
        /// <param name="center">中点</param>
        /// <param name="radiusShort">短轴</param>
        /// <param name="radiusLong">长轴</param>
        /// <param name="deltaAngle">间隔角度</param>
        /// <returns>结果</returns>
        public static Vector3[] GetEllipsePoints(Vector3 center, float radiusShort, float radiusLong, int deltaAngle)
        {
            var points = new Vector3[deltaAngle];
            var j = 0;
            for (float i = 0; i < 360; j++, i += 360f / deltaAngle)
            {
                var angle = (i / 180) * Mathf.PI;
                points[j] = center + new Vector3(radiusShort * Mathf.Cos(angle), 0, radiusLong * Mathf.Sin(angle));
            }
            return points;
        }

        #endregion

        #region Sector

        /// <summary>
        /// 获取扇形区域的点集合
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="egdePoint">边界点</param>
        /// <param name="angleDiffer">角度</param>
        /// <returns>结果</returns>
        public static Vector3[] GetSectorPoints(Vector3 center, Vector3 egdePoint, float angleDiffer)
        {
            // 取两点的向量
            var dir = egdePoint - center;                               
            // 获取扇形的半径
            var radius = dir.magnitude;                                   

            // 取数组长度 如60度的弧边取61个点 0~60 再加上一个圆心点
            var points = new Vector3[(int)(angleDiffer / 3) + 2];
            // 取圆心点
            points[0] = center;                                             
            var startEuler = (int)Vector2.Angle(Vector2.right, new Vector2(dir.x, dir.z));
            for (int i = startEuler, j = 1; i <= angleDiffer + startEuler; j++, i += 3)
            {
                var angle = Mathf.Deg2Rad * i;
                // 高度差的绝对值
                var differ = Mathf.Abs(Mathf.Cos(angle - (float)(0.5 * angleDiffer * Mathf.Deg2Rad)) * egdePoint.y - egdePoint.y);
                // 给底面点赋值
                points[j] = center + new Vector3(radius * Mathf.Cos(angle), egdePoint.y + differ, radius * Mathf.Sin(angle));       
            }
            return points;
        }

        #endregion
    }
}
