/////////////////////////////////////////////////////////////////////////////
//
//  Script   : QuaternionExtension.cs
//  Info     : Quaternion扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class QuaternionExtension
    {
        #region Math

        /// <summary>
        /// 次幂
        /// </summary>
        /// <param name="quaternion">四元数</param>
        /// <param name="power">幂</param>
        /// <returns>结果</returns>
        public static Quaternion Pow(this Quaternion quaternion, float power)
        {
            var inputMagnitude = quaternion.Magnitude();
            var nHat = new Vector3(quaternion.x, quaternion.y, quaternion.z).normalized;
            var vectorBit = new Quaternion(nHat.x, nHat.y, nHat.z, 0).ScalarMultiply(power * Mathf.Acos(quaternion.w / inputMagnitude)).Exp();
            return vectorBit.ScalarMultiply(Mathf.Pow(inputMagnitude, power));
        }

        /// <summary>
        /// 指数
        /// </summary>
        /// <param name="quaternion">四元数</param>
        /// <returns>结果</returns>
        public static Quaternion Exp(this Quaternion quaternion)
        {
            var inputA = quaternion.w;
            var inputV = new Vector3(quaternion.x, quaternion.y, quaternion.z);
            var outputA = Mathf.Exp(inputA) * Mathf.Cos(inputV.magnitude);
            var outputV = Mathf.Exp(inputA) * (inputV.normalized * Mathf.Sin(inputV.magnitude));
            return new Quaternion(outputV.x, outputV.y, outputV.z, outputA);
        }

        /// <summary>
        /// 长度
        /// </summary>
        /// <param name="quaternion">四元数</param>
        /// <returns>结果</returns>
        public static float Magnitude(this Quaternion quaternion)
        {
            return Mathf.Sqrt(quaternion.x * quaternion.x + quaternion.y * quaternion.y + quaternion.z * quaternion.z + quaternion.w * quaternion.w);
        }

        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="quaternion">四元数</param>
        /// <param name="scalar">缩放</param>
        /// <returns>结果</returns>
        public static Quaternion ScalarMultiply(this Quaternion quaternion, float scalar)
        {
            return new Quaternion(quaternion.x * scalar, quaternion.y * scalar, quaternion.z * scalar, quaternion.w * scalar);
        }

        #endregion
    }
}