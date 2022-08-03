/////////////////////////////////////////////////////////////////////////////
//
//  Script   : BehaviourExtension.cs
//  Info     : Behaviour 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class BehaviourExtension
    {
        /// <summary>
        /// 开启组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="behaviour">组件</param>
        /// <returns>组件</returns>
        public static T Enable<T>(this T behaviour) where T : Behaviour
        {
            behaviour.enabled = true;
            return behaviour;
        }

        /// <summary>
        /// 关闭组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="behaviour">组件</param>
        /// <returns>组件</returns>
        public static T Disable<T>(this T behaviour) where T : Behaviour
        {
            behaviour.enabled = false;
            return behaviour;
        }
    }
}
