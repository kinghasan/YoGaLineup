/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MathLerp.cs
//  Info     : 数学辅助计算类 - 插值
//  Author   : ls9512 / Internet
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017 / Internet http://wiki.unity3d.com/index.php/Mathfx
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Maths
{
    public static partial class MathUtil
    {
        #region MoveTowards

        /// <summary>
        /// 转向某角度 - Vector2
        /// </summary>
        /// <param name="from">开始</param>
        /// <param name="to">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector2 MoveTowardsAngle(Vector2 from, Vector2 to, float delta)
        {
            var result = from;
            result.x = Mathf.MoveTowardsAngle(result.x, to.x, delta);
            result.y = Mathf.MoveTowardsAngle(result.y, to.y, delta);
            return result;
        }

        /// <summary>
        /// 转向某角度 - Vector3
        /// </summary>
        /// <param name="from">开始</param>
        /// <param name="to">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector3 MoveTowardsAngle(Vector3 from, Vector3 to, float delta)
        {
            var result = from;
            result.x = Mathf.MoveTowardsAngle(result.x, to.x, delta);
            result.y = Mathf.MoveTowardsAngle(result.y, to.y, delta);
            result.z = Mathf.MoveTowardsAngle(result.z, to.z, delta);
            return result;
        }

        #endregion

        #region Hermite / Ease in out

        /// <summary>
        /// 艾米插值 - float
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static float Hermite(float start, float end, float delta)
        {
            var result = Mathf.Lerp(start, end, delta * delta * (3.0f - 2.0f * delta));
            return result;
        }

        /// <summary>
        /// 艾米插值 - Vector2
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector2 Hermite(Vector2 start, Vector2 end, float delta)
        {
            var result = new Vector2(Hermite(start.x, end.x, delta), Hermite(start.y, end.y, delta));
            return result;
        }

        /// <summary>
        /// 艾米插值 - Vector3
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector3 Hermite(Vector3 start, Vector3 end, float delta)
        {
            var result = new Vector3(Hermite(start.x, end.x, delta), Hermite(start.y, end.y, delta), Hermite(start.z, end.z, delta));
            return result;
        }

        #endregion

        #region Sinerp / Ease out

        /// <summary>
        /// 正弦插值 - float
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static float Sinerp(float start, float end, float delta)
        {
            var result = Mathf.Lerp(start, end, Mathf.Sin(delta * Mathf.PI * 0.5f));
            return result;
        }

        /// <summary>
        /// 正弦插值 - Vector2
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector2 Sinerp(Vector2 start, Vector2 end, float delta)
        {
            var result = new Vector2(Mathf.Lerp(start.x, end.x, Mathf.Sin(delta * Mathf.PI * 0.5f)), Mathf.Lerp(start.y, end.y, Mathf.Sin(delta * Mathf.PI * 0.5f)));
            return result;
        }

        /// <summary>
        /// 正弦插值 - Vector3
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector3 Sinerp(Vector3 start, Vector3 end, float delta)
        {
            var result = new Vector3(Mathf.Lerp(start.x, end.x, Mathf.Sin(delta * Mathf.PI * 0.5f)),
                Mathf.Lerp(start.y, end.y, Mathf.Sin(delta * Mathf.PI * 0.5f)),
                Mathf.Lerp(start.z, end.z, Mathf.Sin(delta * Mathf.PI * 0.5f)));
            return result;
        }

        #endregion

        #region Coserp / Ease in

        /// <summary>
        /// 余弦插值 - float
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static float Coserp(float start, float end, float delta)
        {
            var result = Mathf.Lerp(start, end, 1.0f - Mathf.Cos(delta * Mathf.PI * 0.5f));
            return result;
        }

        /// <summary>
        /// 余弦插值 - Vector2
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector2 Coserp(Vector2 start, Vector2 end, float delta)
        {
            var result = new Vector2(Coserp(start.x, end.x, delta), Coserp(start.y, end.y, delta));
            return result;
        }

        /// <summary>
        /// 余弦插值 - Vector3
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector3 Coserp(Vector3 start, Vector3 end, float delta)
        {
            var result = new Vector3(Coserp(start.x, end.x, delta), Coserp(start.y, end.y, delta), Coserp(start.z, end.z, delta));
            return result;
        }

        #endregion

        #region Boing-Like / Berp

        /// <summary>
        /// 弹性差值 - float
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static float Berp(float start, float end, float delta)
        {
            delta = Mathf.Clamp01(delta);
            delta = (Mathf.Sin(delta * Mathf.PI * (0.2f + 2.5f * delta * delta * delta)) * Mathf.Pow(1f - delta, 2.2f) + delta) * (1f + (1.2f * (1f - delta)));
            var result = start + (end - start) * delta;
            return result;
        }

        /// <summary>
        /// 弹性差值 - Vector2
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector2 Berp(Vector2 start, Vector2 end, float delta)
        {
            var result = new Vector2(Berp(start.x, end.x, delta), Berp(start.y, end.y, delta));
            return result;
        }

        /// <summary>
        /// 弹性差值 - Vector3
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector3 Berp(Vector3 start, Vector3 end, float delta)
        {
            var result = new Vector3(Berp(start.x, end.x, delta), Berp(start.y, end.y, delta), Berp(start.z, end.z, delta));
            return result;
        }

        #endregion

        #region SmoothStep - Like lerp with ease in ease out

        /// <summary>
        /// 平滑插值 - float
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">最小</param>
        /// <param name="max">最大</param>
        /// <returns>结果</returns>
        public static float SmoothStep(float value, float min, float max)
        {
            value = Mathf.Clamp(value, min, max);
            var v1 = (value - min) / (max - min);
            var v2 = (value - min) / (max - min);
            var result = -2 * v1 * v1 * v1 + 3 * v2 * v2;
            return result;
        }

        /// <summary>
        /// 平滑插值 - Vector2
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">最小</param>
        /// <param name="max">最大</param>
        /// <returns>结果</returns>
        public static Vector2 SmoothStep(Vector2 value, float min, float max)
        {
            var result = new Vector2(SmoothStep(value.x, min, max), SmoothStep(value.y, min, max));
            return result;
        }

        /// <summary>
        /// 平滑插值 - Vector3
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">最小</param>
        /// <param name="max">最大</param>
        /// <returns>结果</returns>
        public static Vector3 SmoothStep(Vector3 value, float min, float max)
        {
            var result = new Vector3(SmoothStep(value.x, min, max), SmoothStep(value.y, min, max), SmoothStep(value.z, min, max));
            return result;
        }

        #endregion

        #region Bounce

        /// <summary>
        /// 弹跳插值 - float
        /// </summary>
        /// <param name="x">值</param>
        /// <returns>结果</returns>
        public static float Bounce(float x)
        {
            var result = Mathf.Abs(Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
            return result;
        }

        /// <summary>
        /// 弹跳插值 - Vector2
        /// </summary>
        /// <param name="vec">值</param>
        /// <returns>结果</returns>
        public static Vector2 Bounce(Vector2 vec)
        {
            var result = new Vector2(Bounce(vec.x), Bounce(vec.y));
            return result;
        }

        /// <summary>
        /// 弹跳插值 - Vector3
        /// </summary>
        /// <param name="vec">值</param>
        /// <returns>结果</returns>
        public static Vector3 Bounce(Vector3 vec)
        {
            var result = new Vector3(Bounce(vec.x), Bounce(vec.y), Bounce(vec.z));
            return result;
        }

        #endregion

        #region CLerp / Circular Lerp

        /// <summary>
        /// 循环插值 <para/>
        /// 效果与Lerp接近，但处理了0-360的首尾循环， 用于插值旋转时很有用 <para/>
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static float Clerp(float start, float end, float delta)
        {
            var min = 0f;
            var max = 360f;
            var half = Mathf.Abs((max - min) / 2.0f); // half the distance between min and max
            float retval;
            float diff;
            if ((end - start) < -half)
            {
                diff = ((max - start) + end) * delta;
                retval = start + diff;
            }
            else if ((end - start) > half)
            {
                diff = -((max - end) + start) * delta;
                retval = start + diff;
            }
            else retval = start + (end - start) * delta;

            return retval;
        }

        /// <summary>
        /// 循环插值 <para/>
        /// 效果与Lerp接近，但处理了0-360的首尾循环， 用于插值旋转时很有用 <para/>
        /// </summary>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="delta">delta</param>
        /// <returns>结果</returns>
        public static Vector3 Clerp(Vector3 start, Vector3 end, float delta)
        {
            var ret = new Vector3
            {
                x = Clerp(start.x, end.x, delta),
                y = Clerp(start.y, end.y, delta),
                z = Clerp(start.z, end.z, delta)
            };
            return ret;
        }

        #endregion
    }
}