/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ICollectionExtension.cs
//  Info     : ICollection 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class ICollectionExtension
    {
        #region Null / Empty

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            var result = collection == null || collection.Count == 0;
            return result;
        }

        /// <summary>
        /// 是否为空，不可以为 NULL
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>结果</returns>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null)
            {
                throw new NullReferenceException();
            }

            var result = collection.Count == 0;
            return result;
        }

        #endregion

        #region Add Unique

        /// <summary>
        /// 添加元素，保证唯一
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="value">待添加元素</param>
        /// <returns>是否已经包含</returns>
        public static bool AddUnique<T>(this ICollection<T> collection, T value)
        {
            var alreadyHas = collection.Contains(value);
            if (!alreadyHas)
            {
                collection.Add(value);
            }

            return alreadyHas;
        }

        /// <summary>
        /// 添加元素，保证唯一
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="values">待添加元素</param>
        /// <returns>已经包含的数量/添加失败的数量</returns>
        public static int AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            var count = 0;
            foreach (var value in values)
            {
                if (collection.AddUnique(value))
                {
                    count++;
                }
            }

            return count;
        }

        #endregion
    }
}
