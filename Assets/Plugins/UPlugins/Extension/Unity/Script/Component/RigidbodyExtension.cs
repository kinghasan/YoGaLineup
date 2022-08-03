/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RigidbodyExtension.cs
//  Info     : Rigidbody 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class RigidbodyExtension
    {
        #region Clear
       
        /// <summary>
        /// 清除动量
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody ClearMomentum(this Rigidbody rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            // rigidbody.inertiaTensorRotation = Quaternion.identity;
            return rigidbody;
        } 

        #endregion

        #region Position

        #region Set

        /// <summary>
        /// 设置位置X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody SetPositionX(this Rigidbody rigidbody, float x)
        {
            var position = rigidbody.position;
            position.x = x;
            rigidbody.position = position;
            return rigidbody;
        }

        /// <summary>
        /// 设置位置Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="y">y</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetPositionY(this Rigidbody rigidbody, float y)
        {
            var position = rigidbody.position;
            position.y = y;
            rigidbody.position = position;
            return rigidbody;
        }

        /// <summary>
        /// 设置位置Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="z">z</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetPositionZ(this Rigidbody rigidbody, float z)
        {
            var position = rigidbody.position;
            position.z = z;
            rigidbody.position = position;
            return rigidbody;
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取位置X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>x</returns>
        public static float GetPositionX(this Rigidbody rigidbody)
        {
            return rigidbody.position.x;
        }

        /// <summary>
        /// 获取位置Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>y</returns>
        public static float GetPositionY(this Rigidbody rigidbody)
        {
            return rigidbody.position.y;
        }

        /// <summary>
        /// 获取位置Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>z</returns>
        public static float GetPositionZ(this Rigidbody rigidbody)
        {
            return rigidbody.position.z;
        }

        #endregion

        #endregion

        #region Rotation

        #region Get

        /// <summary>
        /// 获取旋转四元数X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>x</returns>
        public static float GetRotationX(this Rigidbody rigidbody)
        {
            return rigidbody.rotation.x;
        }

        /// <summary>
        /// 获取旋转四元数Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>y</returns>
        public static float GetRotationY(this Transform rigidbody)
        {
            return rigidbody.rotation.y;
        }

        /// <summary>
        /// 获取旋转四元数Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>z</returns>
        public static float GetRotationZ(this Rigidbody rigidbody)
        {
            return rigidbody.rotation.z;
        }

        /// <summary>
        /// 获取旋转四元数W
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>w</returns>
        public static float GetRotationW(this Rigidbody rigidbody)
        {
            return rigidbody.rotation.w;
        }

        #endregion

        #region Set

        /// <summary>
        /// 设置旋转四元数X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetRotationX(this Rigidbody rigidbody, float x)
        {
            var rotation = rigidbody.rotation;
            rotation.x = x;
            rigidbody.rotation = rotation;
            return rigidbody;
        }

        /// <summary>
        /// 设置旋转四元数Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="y">y</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetRotationY(this Rigidbody rigidbody, float y)
        {
            var rotation = rigidbody.rotation;
            rotation.y = y;
            rigidbody.rotation = rotation;
            return rigidbody;
        }

        /// <summary>
        /// 设置旋转四元数Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="z">z</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetRotationZ(this Rigidbody rigidbody, float z)
        {
            var rotation = rigidbody.rotation;
            rotation.z = z;
            rigidbody.rotation = rotation;
            return rigidbody;
        }

        /// <summary>
        /// 设置旋转四元数w
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="w">w</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetRotationW(this Rigidbody rigidbody, float w)
        {
            var rotation = rigidbody.rotation;
            rotation.w = w;
            rigidbody.rotation = rotation;
            return rigidbody;
        }

        #endregion

        #endregion

        #region EulerAngles

        #region Set

        /// <summary>
        /// 设置旋转角度X
        /// </summary>
        /// <param name="transform">刚体</param>
        /// <param name="x">x</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetEulerAnglesX(this Rigidbody transform, float x)
        {
            var eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.x = x;
            transform.rotation = Quaternion.Euler(eulerAngles);
            return transform;
        }

        /// <summary>
        /// 设置旋转角度Y
        /// </summary>
        /// <param name="transform">刚体</param>
        /// <param name="y">y</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetEulerAnglesY(this Rigidbody transform, float y)
        {
            var eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.y = y;
            transform.rotation = Quaternion.Euler(eulerAngles);
            return transform;
        }

        /// <summary>
        /// 设置旋转角度Z
        /// </summary>
        /// <param name="transform">刚体</param>
        /// <param name="z">z</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetEulerAnglesZ(this Rigidbody transform, float z)
        {
            var eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.z = z;
            transform.rotation = Quaternion.Euler(eulerAngles);
            return transform;
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取旋转角度X
        /// </summary>
        /// <param name="transform">刚体</param>
        /// <returns>x</returns>
        public static float GetEulerAnglesX(this Rigidbody transform)
        {
            return transform.rotation.eulerAngles.x;
        }

        /// <summary>
        /// 获取旋转角度Y
        /// </summary>
        /// <param name="transform">刚体</param>
        /// <returns>y</returns>
        public static float GetEulerAnglesY(this Rigidbody transform)
        {
            return transform.rotation.eulerAngles.y;
        }

        /// <summary>
        /// 获取旋转角度Z
        /// </summary>
        /// <param name="transform">刚体</param>
        /// <returns>z</returns>
        public static float GetEulerAnglesZ(this Rigidbody transform)
        {
            return transform.rotation.eulerAngles.z;
        }

        #endregion

        #endregion

        #region Velocity

        #region Set

        /// <summary>
        /// 设置速度X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody SetVelocityX(this Rigidbody rigidbody, float x)
        {
            var velocity = rigidbody.velocity;
            velocity.x = x;
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        /// <summary>
        /// 设置速度Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="y">y</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetVelocityY(this Rigidbody rigidbody, float y)
        {
            var velocity = rigidbody.velocity;
            velocity.y = y;
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        /// <summary>
        /// 设置速度Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="z">z</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetVelocityZ(this Rigidbody rigidbody, float z)
        {
            var velocity = rigidbody.velocity;
            velocity.z = z;
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取速度X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>x</returns>
        public static float GetVelocityX(this Rigidbody rigidbody)
        {
            return rigidbody.velocity.x;
        }

        /// <summary>
        /// 获取速度Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>y</returns>
        public static float GetVelocityY(this Rigidbody rigidbody)
        {
            return rigidbody.velocity.y;
        }

        /// <summary>
        /// 获取速度Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>z</returns>
        public static float GetVelocityZ(this Rigidbody rigidbody)
        {
            return rigidbody.velocity.z;
        }

        #endregion

        #region Add

        /// <summary>
        /// 增加速度X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody AddVelocityX(this Rigidbody rigidbody, float x)
        {
            var velocity = rigidbody.velocity;
            velocity.x += x;
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        /// <summary>
        /// 增加速度Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="y">y</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody AddVelocityY(this Rigidbody rigidbody, float y)
        {
            var velocity = rigidbody.velocity;
            velocity.y += y;
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        /// <summary>
        /// 增加速度Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="z">z</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody AddVelocityZ(this Rigidbody rigidbody, float z)
        {
            var velocity = rigidbody.velocity;
            velocity.z += z;
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        #endregion

        #endregion

        #region AngularVelocity

        #region Set

        /// <summary>
        /// 设置角速度X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody SetAngularVelocityX(this Rigidbody rigidbody, float x)
        {
            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.x = x;
            rigidbody.angularVelocity = angularVelocity;
            return rigidbody;
        }

        /// <summary>
        /// 设置角速度Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="y">y</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetAngularVelocityY(this Rigidbody rigidbody, float y)
        {
            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.y = y;
            rigidbody.angularVelocity = angularVelocity;
            return rigidbody;
        }

        /// <summary>
        /// 设置角速度Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="z">z</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody SetAngularVelocityZ(this Rigidbody rigidbody, float z)
        {
            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.z = z;
            rigidbody.angularVelocity = angularVelocity;
            return rigidbody;
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取角速度X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>x</returns>
        public static float GetAngularVelocityX(this Rigidbody rigidbody)
        {
            return rigidbody.angularVelocity.x;
        }

        /// <summary>
        /// 获取角速度Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>y</returns>
        public static float GetAngularVelocityY(this Rigidbody rigidbody)
        {
            return rigidbody.angularVelocity.y;
        }

        /// <summary>
        /// 获取角速度Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>z</returns>
        public static float GetAngularVelocityZ(this Rigidbody rigidbody)
        {
            return rigidbody.angularVelocity.z;
        }

        #endregion

        #region Add

        /// <summary>
        /// 增加角速度X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody AddAngularVelocityX(this Rigidbody rigidbody, float x)
        {
            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.x += x;
            rigidbody.angularVelocity = angularVelocity;
            return rigidbody;
        }

        /// <summary>
        /// 增加角速度Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="y">y</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody AddAngularVelocityY(this Rigidbody rigidbody, float y)
        {
            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.y += y;
            rigidbody.angularVelocity = angularVelocity;
            return rigidbody;
        }

        /// <summary>
        /// 增加角速度Z
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="z">z</param>
        ///  <returns>rigidbody</returns>
        public static Rigidbody AddAngularVelocityZ(this Rigidbody rigidbody, float z)
        {
            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.z += z;
            rigidbody.angularVelocity = angularVelocity;
            return rigidbody;
        }

        #endregion

        #endregion
    }
}

