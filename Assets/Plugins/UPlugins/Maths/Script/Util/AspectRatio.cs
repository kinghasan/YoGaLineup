/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AspectRatio.cs
//  Info     : 长宽比计算
//  Author   : Internet
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Internet http://wiki.unity3d.com/index.php/Get_Aspect_Ratio
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Maths
{
    public static class AspectRatio
    {
        /// <summary>
        /// 获取长宽比
        /// </summary>
        /// <param name="x">宽度</param>
        /// <param name="y">高度</param>
        /// <returns>结果(x,y) - x : y </returns>
        public static Vector2 GetAspectRatio(int x, int y)
        {
            var f = (float) x / y;
            var i = 0;
            while (true)
            {
                i++;
                if (Math.Abs(Math.Round(f * i, 2) - Mathf.RoundToInt(f * i)) < 1e-6)
                    break;
            }

            return new Vector2((float) Math.Round(f * i, 2), i);
        }

        /// <summary>
        /// 获取长宽比
        /// </summary>
        /// <param name="xy">长宽</param>
        /// <returns>结果(x,y) - x : y </returns>
        public static Vector2 GetAspectRatio(Vector2 xy)
        {
            var f = xy.x / xy.y;
            var i = 0;
            while (true)
            {
                i++;
                if (Math.Abs(Math.Round(f * i, 2) - Mathf.RoundToInt(f * i)) < 1e-6)
                    break;
            }

            return new Vector2((float) Math.Round(f * i, 2), i);
        }
    }
}