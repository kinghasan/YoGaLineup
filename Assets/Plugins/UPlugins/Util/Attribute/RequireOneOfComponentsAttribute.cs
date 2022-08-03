/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RequireOneOfComponents.cs
//  Info     : 特性 - 包含至少一个组件
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Util
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequireOneOfComponents : Attribute
    {
        public Type[] Types;

        public RequireOneOfComponents(params Type[] requiredComponents) { Types = requiredComponents; }

        /// <summary>
        /// 检查是否包含组件
        /// </summary>
        /// <param name="type">目标类的类型</param>
        /// <param name="target">目标对象</param>
        /// <returns>结果</returns>
        public static bool Check(Type type, GameObject target)
        {
            var com = Find(type, target);
            return com != null;
        }

        /// <summary>
        /// 查找包含的组件(返回找到的第一个)
        /// </summary>
        /// <param name="type">目标类类型</param>
        /// <param name="target">目标对象</param>
        /// <returns>结果</returns>
        public static Component Find(Type type, GameObject target)
        {
            var classAttribute = (RequireOneOfComponents) GetCustomAttribute(type, typeof(RequireOneOfComponents));
            var types = classAttribute.Types;
            Component ret = null;
            for (var i = 0; i < types.Length; i++)
            {
                var component = target.GetComponent(types[i]);
                if (component == null) continue;
                ret = component;
                break;
            }

            return ret;
        }
    }
}