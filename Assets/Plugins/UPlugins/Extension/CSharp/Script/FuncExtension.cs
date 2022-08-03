/////////////////////////////////////////////////////////////////////////////
//
//  Script   : FuncExtension.cs
//  Info     : Func 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Extension
{
    public static class FuncExtension
    {
        /// <summary>
        /// 将两个函数复合成为一个函数
        /// </summary>
        /// <typeparam name="T1">参数类型1</typeparam>
        /// <typeparam name="T2">参数类型2</typeparam>
        /// <typeparam name="T3">参数类型3</typeparam>
        /// <param name="func">函数1</param>
        /// <param name="composeFunc">待符合函数</param>
        /// <returns>结果</returns>
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> func, Func<T2, T3> composeFunc)
        {
            return result => composeFunc(func(result));
        }
    }
}
