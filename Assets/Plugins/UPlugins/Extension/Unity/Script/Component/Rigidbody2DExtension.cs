/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Rigidbody2DExtension.cs
//  Info     : Rigidbody2D 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class Rigidbody2DExtension
    {
        #region Position

        #region Set

        /// <summary>
        /// 设置位置X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody2D SetPositionX(this Rigidbody2D rigidbody, float x)
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
        public static Rigidbody2D SetPositionY(this Rigidbody2D rigidbody, float y)
        {
            var position = rigidbody.position;
            position.y = y;
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
        public static float GetPositionX(this Rigidbody2D rigidbody)
        {
            return rigidbody.position.x;
        }

        /// <summary>
        /// 获取位置Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>y</returns>
        public static float GetPositionY(this Rigidbody2D rigidbody)
        {
            return rigidbody.position.y;
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
        public static Rigidbody2D SetVelocityX(this Rigidbody2D rigidbody, float x)
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
        public static Rigidbody2D SetVelocityY(this Rigidbody2D rigidbody, float y)
        {
            var velocity = rigidbody.velocity;
            velocity.y = y;
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
        public static float GetVelocityX(this Rigidbody2D rigidbody)
        {
            return rigidbody.velocity.x;
        }

        /// <summary>
        /// 获取速度Y
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <returns>y</returns>
        public static float GetVelocityY(this Rigidbody2D rigidbody)
        {
            return rigidbody.velocity.y;
        }

        #endregion

        #region Add

        /// <summary>
        /// 增加速度X
        /// </summary>
        /// <param name="rigidbody">刚体</param>
        /// <param name="x">x</param>
        /// <returns>rigidbody</returns>
        public static Rigidbody2D AddVelocityX(this Rigidbody2D rigidbody, float x)
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
        public static Rigidbody2D AddVelocityY(this Rigidbody2D rigidbody, float y)
        {
            var velocity = rigidbody.velocity;
            velocity.y += y;
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        #endregion

        #endregion
    }
}

