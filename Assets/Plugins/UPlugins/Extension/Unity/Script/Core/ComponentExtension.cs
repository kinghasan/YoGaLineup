/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ComponentExtension.cs
//  Info     : Component 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.Reflection;
using UnityEngine;

namespace Aya.Extension
{
	public static class ComponentExtension
    {
        /// <summary>
        /// 自动缓存所有字段/属性的组件值
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="component">结果</param>
        public static void CacheAllComponents<T>(this T component) where T : Component
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
            var type = component.GetType();
            var fieldInfos = type.GetFields(bindingFlags);
            var propertyInfos = type.GetProperties(bindingFlags);

            foreach (var fieldInfo in fieldInfos)
            {
                var componentType = fieldInfo.FieldType;
                if (!componentType.IsSubclassOf(typeof(Component))) continue;
                var value = component.GetComponent(componentType);
                if (value == null)
                {
                    value = component.GetComponentInChildren(componentType, true);
                }

                if (value != null)
                {
                    fieldInfo.SetValue(component, value);
                }
            }


            foreach (var propertyInfo in propertyInfos)
            {
                var componentType = propertyInfo.PropertyType;
                if (!componentType.IsSubclassOf(typeof(Component))) continue;
                var value = component.GetComponent(componentType);
                if (value == null)
                {
                    value = component.GetComponentInChildren(componentType, true);
                }

                if (value != null)
                {
                    var setMethod = propertyInfo.GetSetMethod(true);
                    setMethod?.Invoke(component, new object[] { value });
                }
            }
        }

        /// <summary>
        /// 尝试获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="component">组件</param>
        /// <param name="outComponent">获取组件结果</param>
        /// <returns>获取成功</returns>
        public static bool TryGetComponent<T>(this Component component, out T outComponent)
        {
			var result = component.gameObject.TryGetComponent(out outComponent);
            return result;
        }

        /// <summary>
        /// 尝试从父物体获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="component">组件</param>
        /// <param name="outComponent">获取组件结果</param>
        /// <returns>获取成功</returns>
        public static bool TryGetComponentInParent<T>(this Component component, out T outComponent)
		{
            var result = component.gameObject.TryGetComponentInParent(out outComponent);
            return result;
		}

        /// <summary>
        /// 尝试从子物体获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="component">组件</param>
        /// <param name="outComponent">获取组件结果</param>
        /// <returns>获取成功</returns>
        public static bool TryGetComponentInChildren<T>(this Component component, out T outComponent)
        {
            var result = component.gameObject.TryGetComponentInChildren(out outComponent);
            return result;
        }
    }
}
