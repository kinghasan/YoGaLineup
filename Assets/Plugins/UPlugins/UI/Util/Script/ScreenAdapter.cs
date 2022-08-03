/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ScreenAdapter.cs
//  Info     : UI辅助 - 屏幕适配
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.UI
{
    public class ScreenAdapter
    {

        /// <summary>
        /// 以屏幕中心为(0,0)的偏移量，适配到当前的屏幕比例
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="orginalRate">原屏幕比例，默认16:9</param>
        /// <returns>结果</returns>
        public static Vector3 OffsetWithScreen(Vector3 offset, float orginalRate = 16f / 9f)
        {
            return offset / orginalRate * (Screen.width * 1f / Screen.height);
        }

        /// <summary>
        /// 以屏幕中心为(0.5,0.5)的偏移量，适配到当前的屏幕比例
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="UICamera">UI相机</param>
        /// <returns>结果</returns>
        public static Vector3 OffsetWithViewport(Vector3 offset, Camera UICamera)
        {
            return UICamera.ViewportToWorldPoint(offset);
        }
    }
}