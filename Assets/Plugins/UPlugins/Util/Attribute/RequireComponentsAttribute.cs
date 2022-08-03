/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RequireComponents.cs
//  Info     : 特性 - 包含所有组件
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
    public sealed class RequireComponents : Attribute
    {
        public Type[] Types;

        public RequireComponents(params Type[] requiredComponents)
        {
            Types = requiredComponents;
        }

        /// <summary>
        /// 检查是否包含所有组件
        /// </summary>
        /// <param name="type">目标类类型</param>
        /// <param name="target">目标对象</param>
        /// <returns>结果</returns>
        public static bool Check(Type type, GameObject target)
        {
            var classAttribute = (RequireOneOfComponents)GetCustomAttribute(type, typeof(RequireOneOfComponents));
            var types = classAttribute.Types;
            var find = true;
            for (var i = 0; i < types.Length; i++)
            {
                var component = target.GetComponent(types[i]);
                if (component != null) continue;
                find = false;
                break;
            }
            return find;
        }
    }
}
