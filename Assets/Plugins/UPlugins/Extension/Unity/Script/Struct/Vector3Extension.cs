/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Vector3Extension.cs
//  Info     : Vector3扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class Vector3Extension
    {
        #region Distance

        /// <summary>
        /// 点到点的距离
        /// </summary>
        /// <param name="vector">点</param>
        /// <param name="point">直线上一个点的坐标</param>
        /// <returns>距离</returns>
        public static float Distance2Point(this Vector3 vector, Vector3 point)
        {
            var result = Vector3.Distance(vector, point);
            return result;
        }

        /// <summary>
        /// 点到直线的距离
        /// </summary>
        /// <param name="vector">点</param>
        /// <param name="linePoint1">直线上一个点的坐标</param>
        /// <param name="linePoint2">直线上另一个点的坐标</param>
        /// <returns>距离</returns>
        public static float Distance2Line(Vector2 vector, Vector2 linePoint1, Vector2 linePoint2)
        {
            float space;
            var a = Vector2.Distance(linePoint1, linePoint2);
            var b = Vector2.Distance(linePoint1, vector);
            var c = Vector2.Distance(linePoint2, vector);
            if (c <= 1e-6 || b <= 1e-6)
            {
                space = 0;
                return space;
            }

            if (a <= 1e-6)
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

            var p = (a + b + c) / 2;
            var s = Mathf.Sqrt(p * (p - a) * (p - b) * (p - c));
            space = 2 * s / a;
            return space;
        }

        #endregion

        #region Clamp

        /// <summary>
        /// 将向量值限制在0，1之间
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector3 Clamp01(this Vector3 vector)
        {
            vector = vector.Clamp(0f, 1f);
            return vector;
        }

        /// <summary>
        /// 将向量值限制在两个值之间
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>结果</returns>
        public static Vector3 Clamp(this Vector3 vector, float min, float max)
        {
            vector.x = Mathf.Clamp(vector.x, min, max);
            vector.y = Mathf.Clamp(vector.y, min, max);
            vector.z = Mathf.Clamp(vector.z, min, max);
            return vector;
        }

        /// <summary>
        /// 将向量值限制在两个箱梁之间
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="min">最小向量</param>
        /// <param name="max">最大向量</param>
        /// <returns>结果</returns>
        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            vector.z = Mathf.Clamp(vector.z, min.z, max.z);
            return vector;
        }

        #endregion

        #region Flip
       
        /// <summary>
        /// 反转向量
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector3 Flip(this Vector3 vector)
        {
            vector = -vector;
            return vector;
        }

        /// <summary>
        /// 反转向量X轴
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector3 FlipX(this Vector3 vector)
        {
            vector.x = -vector.x;
            return vector;
        }

        /// <summary>
        /// 反转向量Y轴
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector3 FlipY(this Vector3 vector)
        {
            vector.y = -vector.y;
            return vector;
        }

        /// <summary>
        /// 反转向量Z轴
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector3 FlipZ(this Vector3 vector)
        {
            vector.z = -vector.z;
            return vector;
        }

        #endregion

        #region XYZ -> Vecotr2

        /// <summary>
        /// 获取 X Y 构成的 Vector2
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector2 GetXy(this Vector3 vector)
        {
            var result = new Vector2(vector.x, vector.y);
            return result;
        }

        /// <summary>
        /// 获取 Y Z 构成的 Vector2
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector2 GetYz(this Vector3 vector)
        {
            var result = new Vector2(vector.y, vector.z);
            return result;
        }

        /// <summary>
        /// 获取 X Z 构成的 Vector2
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector2 GetXz(this Vector3 vector)
        {
            var result = new Vector2(vector.x, vector.z);
            return result;
        }

        #endregion
    }
}