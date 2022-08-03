/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Vector2Extension.cs
//  Info     : Vector2扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class Vector2Extension
    {
        #region Distance

        /// <summary>
        /// 点到直线距离
        /// </summary>
        /// <param name="vector">点</param>
        /// <param name="linePoint1">直线上一个点的坐标</param>
        /// <param name="linePoint2">直线上另一个点的坐标</param>
        /// <returns>距离</returns>
        public static float Distance2Line(this Vector2 vector, Vector2 linePoint1, Vector2 linePoint2)
        {
            var vec1 = vector - linePoint1;
            var vec2 = linePoint2 - linePoint1;
            var project = Vector3.Project(vec1, vec2);
            var dis = Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(project), 2));
            return dis;
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
            var quaternion = Quaternion.AngleAxis(angle, axis);
            var offset = origin - point;
            offset = quaternion * offset;
            var result = point + offset;
            return result;
        }

        #endregion

        #region Flip

        /// <summary>
        /// 反转向量
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector2 Flip(this Vector2 vector)
        {
            vector = -vector;
            return vector;
        }

        /// <summary>
        /// 反转向量X轴
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector2 FlipX(this Vector2 vector)
        {
            vector.x = -vector.x;
            return vector;
        }

        /// <summary>
        /// 反转向量Y轴
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector2 FlipY(this Vector2 vector)
        {
            vector.y = -vector.y;
            return vector;
        }

        #endregion

        #region Clamp

        /// <summary>
        /// 将向量值限制在0，1之间
        /// </summary>
        /// <param name="vector">向量</param>
        /// <returns>结果</returns>
        public static Vector2 Clamp01(this Vector2 vector)
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
        public static Vector2 Clamp(this Vector2 vector, float min, float max)
        {
            vector.x = Mathf.Clamp(vector.x, min, max);
            vector.y = Mathf.Clamp(vector.y, min, max);
            return vector;
        }

        /// <summary>
        /// 将向量值限制在两个箱梁之间
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="min">最小向量</param>
        /// <param name="max">最大向量</param>
        /// <returns>结果</returns>
        public static Vector2 Clamp(this Vector2 vector, Vector2 min, Vector2 max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            return vector;
        }

        #endregion
    }
}