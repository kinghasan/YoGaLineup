/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IEnumerableExtenssion.cs
//  Info     : IEnumerable 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class IEnumerableExtension
    {
        #region Null / Empty / Any

        /// <summary>
        /// 确定序列是否包含任何元素。<para/>
        /// 实现Any接口是为了避免引入Linq.
        /// </summary>
        /// <param name="source">要检查是否为空的 <see cref="T:ModSystem.Collections.Generic.IEnumerable`1"/>。</param>
        /// <typeparam name="T"><paramref name="source"/> 中的元素的类型。 </typeparam>
        /// <returns>如果源序列包含任何元素，则为 true；否则为 false。 </returns>
        public static bool Any<T>(this IEnumerable<T> source)
        {
            if (source == null) return false;
            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext()) return true;
            }

            return false;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="source">数组</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            var result = source == null || !source.Any();
            return result;
        }

        /// <summary>
        /// 集合是否空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        /// <summary>
        /// 集合是否非空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        {
            return source.Any();
        }

        #endregion

        #region Get

        /// <summary>
        /// 是否存在满足条件的元素，满足返回该元素，不存在则返回默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static T Has<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item)) return item;
            }

            return default(T);
        }

        /// <summary>
        /// 查找符合条件的元素，返回符合条件的第一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static T Find<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var ret = default(T);
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    ret = item;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// 查找符合条件的元素，返回列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static List<T> FindAll<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                if (predicate(item)) list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TValue">比较值类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="selector">选择器</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>结果</returns>
        public static T Max<T, TValue>(this IEnumerable<T> source, Func<T, TValue> selector, out TValue maxValue)
            where T : class
            where TValue : IComparable<TValue>
        {
            T maxItem = null;
            maxValue = default(TValue);
            foreach (var item in source)
            {
                if (item == null) continue;
                var itemValue = selector(item);
                if ((maxItem != null) && (itemValue.CompareTo(maxValue) <= 0)) continue;
                maxValue = itemValue;
                maxItem = item;
            }

            return maxItem;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TValue">比较值类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="keyGetter">选择器</param>
        /// <returns>结果</returns>
        public static T Max<T, TValue>(this IEnumerable<T> source, Func<T, TValue> keyGetter)
            where T : class
            where TValue : IComparable<TValue>
        {
            var result = source.Max(keyGetter, out _);
            return result;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TValue">比较值类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="keyGetter">选择器</param>
        /// <param name="minValue">最小值</param>
        /// <returns>结果</returns>
        public static T Min<T, TValue>(this IEnumerable<T> source, Func<T, TValue> keyGetter, out TValue minValue)
            where T : class
            where TValue : IComparable
        {
            T minItem = null;
            minValue = default(TValue);
            foreach (var item in source)
            {
                if (item == null) continue;
                var itemValue = keyGetter(item);
                if ((minItem != null) && (itemValue.CompareTo(minValue) >= 0)) continue;
                minValue = itemValue;
                minItem = item;
            }

            return minItem;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TValue">比较值类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="selector">选择器</param>
        /// <returns>结果</returns>
        public static T Min<T, TValue>(this IEnumerable<T> source, Func<T, TValue> selector)
            where T : class
            where TValue : IComparable
        {
            var result = source.Min(selector, out _);
            return result;
        }

        /// <summary>
        /// 选择出元素中的指定内容
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TResult">结果值类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="selector">选择器</param>
        /// <param name="allowNull">允许空元素</param>
        /// <returns>结果</returns>
        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector, bool allowNull = true)
        {
            foreach (var item in source)
            {
                var select = selector(item);
                if (allowNull || !Equals(select, default(T)))
                {
                    yield return select;
                }
            }
        }

        /// <summary>
        /// 获取第一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static T First<T>(this IEnumerable<T> source)
        {
            if (source == null) return default(T);
            foreach (var item in source)
            {
                return item;
            }

            return default(T);
        }

        /// <summary>
        /// 获取最前面的若干个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static IList<T> First<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) return null;
            var result = new List<T>();
            var counter = 0;
            foreach (var item in source)
            {
                result.Add(item);
                counter++;
                if (counter >= count) break;
            }

            return result;
        }

        /// <summary>
        /// 获取最后一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static T Last<T>(this IEnumerable<T> source)
        {
            if (source == null) return default(T);
            var result = default(T);
            foreach (var item in source)
            {
                result = item;
            }

            return result;
        }

        /// <summary>
        /// 获取最后面的若干个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static IList<T> Last<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) return null;
            var result = new List<T>();
            var sum = 0;
            foreach (var _ in source)
            {
                sum++;
            }

            var counter = 0;
            foreach (var item in source)
            {
                counter++;
                if (counter > sum - count)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        #endregion

        #region Foreach

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="action">事件</param>
        /// <returns>source</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null) return source;
            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }

        /// <summary>
        /// 遍历 - 满足某条件时执行事件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="selector">条件</param>
        /// <param name="action">事件</param>
        /// <returns>source</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Func<T, bool> selector, Action<T> action)
        {
            if (selector == null || action == null) return source;
            foreach (var item in source)
            {
                if (!selector(item)) continue;
                action(item);
            }

            return source;
        }

        #endregion

        #region Array \ List \ HashSet

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="selector">选择器</param>
        /// <returns>结果</returns>
        public static List<TResult> ToList<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            var list = new List<TResult>();
            foreach (var item in source)
            {
                list.Add(selector(item));
            }

            return list;
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static T[] ToArray<T>(this IEnumerable<T> source)
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="selector">选择器</param>
        /// <returns>结果</returns>
        public static TResult[] ToArray<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            var list = new List<TResult>();
            foreach (var item in source)
            {
                list.Add(selector(item));
            }

            return list.ToArray();
        }

        /// <summary>
        /// 转换为 HashSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            var result = new HashSet<T>(source);
            return result;
        }

        #endregion
    }
}