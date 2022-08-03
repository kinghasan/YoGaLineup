/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ArrayExtension.cs
//  Info     : 数组 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class ArrayExtension
    {
        #region Null / Empty

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty(this Array array)
        {
            var result = array == null || array.Length == 0;
            return result;
        }

        /// <summary>
        /// 是否为空，不可以为 NULL
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>结果</returns>
        public static bool IsEmpty(this Array array)
        {
            if (array == null)
            {
                throw new NullReferenceException();
            }

            var result = array.Length == 0;
            return result;
        }

        #endregion

        #region Index

        /// <summary>
        /// 是否包含索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <returns>索引</returns>
        public static bool WithinIndex(this Array array, int index)
        {
            var result = array != null && index >= 0 && index < array.Length;
            return result;
        }

        /// <summary>
        /// 指定维度是否包含索引
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="dimension">维度</param>
        /// <returns></returns>
        public static bool WithinIndex(this Array array, int index, int dimension)
        {
            return array != null && index >= array.GetLowerBound(dimension) && index <= array.GetUpperBound(dimension);
        }

        #endregion

        #region Switch

        /// <summary>
        /// 交换两个元素的位置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index1">索引1</param>
        /// <param name="index2">索引2</param>
        /// <returns>array</returns>
        public static T[] SwitchPos<T>(this T[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
            return array;
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取匹配的第一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="predicate">匹配条件</param>
        /// <returns>结果</returns>
        public static T First<T>(this T[] array, Predicate<T> predicate)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var item = array[i];
                if (predicate(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        /// <summary>
        /// 获取匹配的最后一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="predicate">匹配条件</param>
        /// <returns>结果</returns>
        public static T Last<T>(this T[] array, Predicate<T> predicate)
        {
            for (var i = array.Length - 1; i >= 0; i--)
            {
                var item = array[i];
                if (predicate(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        /// <summary>
        /// 获取所有匹配条件的元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="predicate">匹配条件</param>
        /// <returns>结果</returns>
        public static List<T> Find<T>(this T[] array, Predicate<T> predicate)
        {
            var ret = new List<T>();
            for (var i = 0; i < array.Length; i++)
            {
                var item = array[i];
                if (predicate(item))
                {
                    ret.Add(item);
                }
            }

            return ret;
        }

        /// <summary>
        /// 获取对象在数组中的索引
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="item">对象</param>
        /// <returns>索引</returns>
        public static int IndexOf<T>(this T[] array, object item)
        {
            var index = 0;
            foreach (var i in array)
            {
                if (item.Equals(i)) break;
                index++;
            }

            if (index >= array.Length) index = -1;
            return index;
        }

        #endregion

        #region Contains

        /// <summary>
        /// 是否包含某个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="item">元素</param>
        /// <returns>结果</returns>
        public static bool Contains<T>(this T[] array, T item)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var temp = array[i];
                if (temp.Equals(item)) return true;
            }

            return false;
        }

        /// <summary>
        /// 是否包含满足条件的某个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static bool Contains<T>(this T[] array, Predicate<T> predicate)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var temp = array[i];
                if (predicate(temp)) return true;
            }

            return false;
        }

        #endregion

        #region Combine

        /// <summary>
        /// 合并数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="other">另一个数组</param>
        /// <returns>结果</returns>
        public static T[] CombineArray<T>(this T[] array, T[] other)
        {
            if (array == default(T[]) || other == default(T[])) return array;
            var initialSize = array.Length;
            Array.Resize<T>(ref array, initialSize + other.Length);
            Array.Copy(other, other.GetLowerBound(0), array, initialSize, other.Length);
            return array;
        }

        #endregion

        #region Clear

        /// <summary>
        /// 清除指定位置的元素
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static Array ClearAt(this Array array, int index)
        {
            if (array == null) return null;
            if (index >= 0 && index < array.Length)
            {
                Array.Clear(array, index, 1);
            }

            return array;
        }

        /// <summary>
        /// 清除指定位置的元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static T[] ClearAt<T>(this T[] array, int index)
        {
            if (array == null) return null;
            if (index >= 0 && index < array.Length)
            {
                array[index] = default(T);
            }

            return array;
        }

        /// <summary>
        /// 全部清空
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>结果</returns>
        public static Array ClearAll(this Array array)
        {
            if (array != null)
            {
                Array.Clear(array, 0, array.Length);
            }

            return array;
        }

        /// <summary>
        /// 全部清空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>结果</returns>
        public static T[] ClearAll<T>(this T[] array)
        {
            if (array == null) return null;
            for (var i = array.GetLowerBound(0); i <= array.GetUpperBound(0); ++i)
            {
                array[i] = default(T);
            }

            return array;
        }

        #endregion

        #region Block Copy

        /// <summary>
        /// 拷贝指定位置开始指定长度的数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static T[] BlockCopy<T>(this T[] array, int index, int length)
        {
            var result = BlockCopy(array, index, length, false);
            return result;
        }

        /// <summary>
        /// 拷贝指定位置考试指定长度的数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        /// <param name="padToLength">补足长度</param>
        /// <returns></returns>
        public static T[] BlockCopy<T>(this T[] array, int index, int length, bool padToLength)
        {
            if (array == null)
            {
                throw new NullReferenceException();
            }

            var n = length;
            T[] result = null;
            if (array.Length < index + length)
            {
                n = array.Length - index;
                if (padToLength)
                {
                    result = new T[length];
                }
            }

            if (result == null) result = new T[n];
            Array.Copy(array, index, result, 0, n);
            return result;
        }

        /// <summary>
        /// 拷贝数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="count">数量</param>
        /// <param name="padToLength">补足长度</param>
        /// <returns>结果</returns>
        public static IEnumerable<T[]> BlockCopy<T>(this T[] array, int count, bool padToLength = false)
        {
            for (var i = 0; i < array.Length; i += count)
            {
                yield return array.BlockCopy(i, count, padToLength);
            }
        }

        #endregion

        #region Sort

        internal static Random Rand = new Random();

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="comparison">比较器</param>
        /// <returns>array</returns>
        public static T[] Sort<T>(this T[] array, Comparison<T> comparison)
        {
            if (comparison == null)
            {
                throw new ArgumentNullException();
            }

            if (array.Length == 0)
            {
                return array;
            }

            Array.Sort(array, comparison);
            return array;
        }

        /// <summary>
        /// 乱序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">列表</param>
        /// <returns>结果</returns>
        public static T[] RandSort<T>(this T[] array)
        {
            var count = array.Length * 3;
            for (var i = 0; i < count; i++)
            {
                var index1 = Rand.Next(0, array.Length);
                var item1 = array[index1];
                var index2 = Rand.Next(0, array.Length);
                var item2 = array[index2];
                var temp = item2;
                array[index2] = item1;
                array[index1] = temp;
            }

            return array;
        }

        /// <summary>
        /// 升序排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>结果</returns>
        public static T[] SortAsc<T>(this T[] array) where T : IComparable
        {
            array.SortAsc(i => i);
            return array;
        }

        /// <summary>
        /// 按 Key 优先级升序排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">列表</param>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static T[] SortAsc<T>(this T[] array, params Func<T, IComparable>[] keyGetters)
        {
            array.Sort(ComparisonUtil.GetAscComparison(keyGetters));
            return array;
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>结果</returns>
        public static T[] SortDesc<T>(this T[] array) where T : IComparable
        {
            array.SortDesc(i => i);
            return array;
        }

        /// <summary>
        /// 按 Key 优先级降序排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">列表</param>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static T[] SortDesc<T>(this T[] array, params Func<T, IComparable>[] keyGetters)
        {
            array.Sort(ComparisonUtil.GetDescComparison(keyGetters));
            return array;
        }

        /// <summary>
        /// 按 Key 优先级分别进行制定方式的排序<para/>
        /// getter 返回值 int : 排序数值<para/>
        /// getter 返回值 bool : 排序方式 true 升序 false 降序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">列表</param>
        /// <param name="keyGetters">Key访问器</param>
        /// <returns>结果</returns>
        public static T[] Sort<T>(this T[] array, params Func<T, (IComparable, bool)>[] keyGetters)
        {
            array.Sort(ComparisonUtil.GetCustomComparison(keyGetters));
            return array;
        }

        #endregion
    }
}
