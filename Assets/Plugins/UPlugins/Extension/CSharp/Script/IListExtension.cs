/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IListExtension.cs
//  Info     : IList 扩展方法
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
    public static class IListExtension
    {
        internal static Random Rand = new Random();

        #region Null / Empty

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            var result = list == null || list.Count == 0;
            return result;
        }

        /// <summary>
        /// 是否为空，不可以为 NULL
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>结果</returns>
        public static bool IsEmpty<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new NullReferenceException();
            }

            var result = list.Count == 0;
            return result;
        }

        #endregion

        #region Switch

        /// <summary>
        /// 交换两个元素的位置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="index1">索引1</param>
        /// <param name="index2">索引2</param>
        /// <returns>list</returns>
        public static IList<T> SwitchPos<T>(this IList<T> list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
            return list;
        }

        #endregion

        #region Add / Insert

        /// <summary>
        /// 向列表添加元素，且保证不重复
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="item">元素</param>
        /// <returns>结果</returns>
        public static bool AddUnique<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 插入，保证唯一
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="index">插入位置索引</param>
        /// <param name="item">元素</param>
        /// <returns>结果</returns>
        public static bool InsertUnique<T>(this IList<T> list, int index, T item)
        {
            if (list.Contains(item)) return false;
            list.Insert(index, item);
            return true;
        }

        /// <summary>
        /// 插入，保证唯一
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="startIndex">插入位置索引</param>
        /// <param name="items">待插入元素</param>
        /// <returns>插入成功数量</returns>
        public static int InsertRangeUnique<T>(this IList<T> list, int startIndex, IEnumerable<T> items)
        {
            var index = startIndex;
            var count = 0;
            foreach (var item in items)
            {
                if (list.Contains(item)) continue;
                list.Insert(index, item);
                count++;
                index++;
            }

            return count;
        }

        #endregion

        #region Get

        /// <summary>
        /// 尝试从列表中获取值，不存在返回默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static T TryGetValue<T>(this IList<T> list, T value)
        {
            var index = list.IndexOf(value);
            var result = index < 0 ? default(T) : value;
            return result;
        }

        /// <summary>
        /// 获取值的同时移除，不存在返回默认值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static T GetAndRemove<T>(this IList<T> list, T value)
        {
            var result = TryGetValue(list, value);
            if (result != null) list.Remove(result);
            return result;
        }

        /// <summary>
        /// 根据坐标获取同时移除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="index">下标</param>
        /// <returns>结果</returns>
        public static T GetAndRemove<T>(this IList<T> list, int index)
        {
            if (index > list.Count - 1) return default(T);
            var result = list[index];
            list.Remove(result);
            return result;
        }

        /// <summary>
        /// 第一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>结果</returns>
        public static T First<T>(this IList<T> list)
        {
            var result = list != null && list.Count > 0 ? list[0] : default(T);
            return result;
        }

        /// <summary>
        /// 第一个符合条件的元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static T First<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item)) return item;
            }

            return default(T);
        }

        /// <summary>
        /// 获取列表开头的N个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">数量</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static List<T> First<T>(this IList<T> list, int count, Predicate<T> predicate = null)
        {
            if (list == null) return null;
            var result = new List<T>();
            for (var i = 0; result.Count < count && i < list.Count; i++)
            {
                var item = list[i];
                if (predicate != null)
                {
                    if (predicate(item))
                    {
                        result.Add(item);
                    }
                }
                else
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// 最后一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>结果</returns>
        public static T Last<T>(this IList<T> list)
        {
            var result = list != null && list.Count > 0 ? list[list.Count - 1] : default(T);
            return result;
        }

        /// <summary>
        /// 最后一个符合条件的元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static T Last<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (predicate(item)) return item;
            }

            return default(T);
        }

        /// <summary>
        /// 获取列表结尾的N个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">数量</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static List<T> Last<T>(this IList<T> list, int count, Predicate<T> predicate = null)
        {
            if (list == null) return null;
            var result = new List<T>();
            for (var i = list.Count - 1; result.Count < count && i >= 0; i--)
            {
                var item = list[i];
                if (predicate != null)
                {
                    if (predicate(item))
                    {
                        result.Add(item);
                    }
                }
                else
                {
                    result.Add(item);
                }
            }

            return result;
        }

        #endregion

        #region Get Before / After

        /// <summary>
        /// 某元素的前一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="item">元素</param>
        /// <returns>结果</returns>
        public static T Before<T>(this IList<T> list, T item)
        {
            var index = list.IndexOf(item);
            var result = index < 1 ? default(T) : list[index - 1];
            return result;
        }

        /// <summary>
        /// 某元素的后一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="item">元素</param>
        /// <returns>结果</returns>
        public static T After<T>(this IList<T> list, T item)
        {
            var index = list.IndexOf(item);
            var result = index > list.Count - 2 ? default(T) : list[index + 1];
            return result;
        }

        #endregion

        #region Get Find

        /// <summary>
        /// 查找符合条件的元素，返回符合条件的第一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static T Find<T>(this IList<T> list, Predicate<T> predicate)
        {
            var result = default(T);
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 查找符合条件的元素，返回列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static List<T> FindAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            var result = new List<T>();
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item)) result.Add(item);
            }

            return result;
        }

        #endregion

        #region Max / Min

        /// <summary>
        /// 获取最大的N个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static List<T> Max<T>(this IList<T> list, int count) where T : IComparable
        {
            return Max(list, count, i => i);
        }

        /// <summary>
        /// 获取最大的N个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">数量</param>
        /// <param name="keyGetter">比较值访问器</param>
        /// <returns>结果</returns>
        public static List<T> Max<T>(this IList<T> list, int count, Func<T, IComparable> keyGetter)
        {
            if (list == null || list.Count == 0) return default;
            if (count > list.Count) throw new ArgumentOutOfRangeException();
            var indexList = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                indexList.Add(i);
            }

            indexList.SortDesc(i => keyGetter(list[i]));
            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                result.Add(list[indexList[i]]);
            }

            return result;
        }

        /// <summary>
        /// 获取最小的N个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static List<T> Min<T>(this IList<T> list, int count) where T : IComparable
        {
            return Min(list, count, i => i);
        }

        /// <summary>
        /// 获取最小的N个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">数量</param>
        /// <param name="keyGetter">比较值访问器</param>
        /// <returns>结果</returns>
        public static List<T> Min<T>(this IList<T> list, int count, Func<T, IComparable> keyGetter)
        {
            if (list == null || list.Count == 0) return default;
            if (count > list.Count) throw new ArgumentOutOfRangeException();
            var indexList = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                indexList.Add(i);
            }

            indexList.SortAsc(i => keyGetter(list[i]));
            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                result.Add(list[indexList[i]]);
            }

            return result;
        }

        #endregion

        #region Get Random

        /// <summary>
        /// 获取随机元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>结果</returns>
        public static T Random<T>(this IList<T> list)
        {
            var result = list.Count > 0 ? list[Rand.Next(0, list.Count)] : default(T);
            return result;
        }

        /// <summary>
        /// 获取满足条件的随机元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static T Random<T>(this IList<T> list, Predicate<T> predicate)
        {
            var indexes = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    indexes.Add(i);
                }
            }

            if (indexes.Count == 0) return default(T);
            var randIndex = indexes.Random();
            var result = list[randIndex];
            return result;
        }

        /// <summary>
        /// 获取随机元素集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">获取数量</param>
        /// <param name="allowRepeat">允许重复(针对值类型,去重后返回结果可能小于需要的数量)</param>
        /// <returns>结果</returns>
        public static List<T> Random<T>(this IList<T> list, int count, bool allowRepeat = false)
        {
            var result = new List<T>();
            if (count > list.Count)
            {
                throw new IndexOutOfRangeException();
            }

            for (var i = 0; i < count; i++)
            {
                var item = list[Rand.Next(0, list.Count)];
                if (!allowRepeat && result.Contains(item))
                {
                    i--;
                    continue;
                }

                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 根据权重获取一个随机元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="weights">权重列表</param>
        /// <returns>结果</returns>
        public static T Random<T>(this IList<T> list, List<int> weights)
        {
            var weightCount = 0;
            for (var i = 0; i < weights.Count; i++)
            {
                weightCount += weights[i];
            }

            var rand = Rand.Next(0, weightCount);
            weightCount = 0;
            var index = -1;
            do
            {
                weightCount += weights[index + 1];
                index++;
            } while (weightCount < rand);

            var result = list[index];
            return result;
        }

        /// <summary>
        /// 根据权重获取若干个随机元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="count">数量</param>
        /// <param name="weights">权重列表</param>
        /// <returns>结果</returns>
        public static List<T> Random<T>(this IList<T> list, int count, List<int> weights)
        {
            var result = new List<T>();
            do
            {
                var item = list.Random(weights);
                if (result.Contains(item)) continue;
                result.Add(item);
            } while (result.Count < count && result.Count < list.Count);

            return result;
        }

        #endregion

        #region Index

        /// <summary>
        /// 返回第一个匹配的元素索引
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="comparison">判断方法</param>
        /// <returns>结果</returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> comparison)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(list[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion

        #region Contains

        /// <summary>
        /// 是否包含满足条件的某个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static bool Contains<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                if (predicate(temp)) return true;
            }

            return false;
        }

        #endregion=

        #region Foreach

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="action">事件</param>
        /// <returns>list</returns>
        public static IList<T> ForEach<T>(this IList<T> list, Action<T> action)
        {
            if (action == null) return list;
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                action(item);
            }

            return list;
        }

        /// <summary>
        /// 遍历 - 满足某条件时执行事件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="selector">条件</param>
        /// <param name="action">事件</param>
        /// <returns>list</returns>
        public static IList<T> ForEach<T>(this IList<T> list, Func<T, bool> selector, Action<T> action)
        {
            if (selector == null || action == null) return list;
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (!selector(item)) continue;
                action(item);
            }

            return list;
        }

        /// <summary>
        /// 倒序遍历
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="action">事件</param>
        /// <returns>list</returns>
        public static IList<T> ForEachReverse<T>(this IList<T> list, Action<T> action)
        {
            if (action == null) return list;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                action(item);
            }

            return list;
        }

        /// <summary>
        /// 倒序遍历 - 满足某条件时执行事件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="selector">条件</param>
        /// <param name="action">事件</param>
        /// <returns>list</returns>
        public static IList<T> ForEachReverse<T>(this IList<T> list, Func<T, bool> selector, Action<T> action)
        {
            if (selector == null || action == null) return list;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (!selector(item)) continue;
                action(item);
            }

            return list;
        }

        #endregion

        #region Move Up / Down

        /// <summary>
        /// 向上移动元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="item">元素</param>
        /// <param name="step">移动量</param>
        /// <returns>移动结果</returns>
        public static bool MoveUp<T>(this IList<T> list, T item, int step = 1)
        {
            if (list == null || !list.Contains(item) || step < 1) return false;
            var index = list.IndexOf(item);
            if (index <= step - 1) return false;
            list.Remove(item);
            list.Insert(index - step, item);
            return true;
        }

        /// <summary>
        /// 向下移动元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="item">元素</param>
        /// <param name="step">移动量</param>
        /// <returns>移动结果</returns>
        public static bool MoveDown<T>(this IList<T> list, T item, int step = 1)
        {
            if (list == null || !list.Contains(item) || step < 1) return false;
            var index = list.IndexOf(item);
            if (index >= list.Count - step) return false;
            list.Remove(item);
            list.Insert(index + step, item);
            return true;
        }

        #endregion

        #region To Dictionary / List

        /// <summary>
        /// 将 List 转换为 Dictionary
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="T">列表类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="getKeyFunc">获取元素键值的方法</param>
        /// <returns>结果</returns>
        public static Dictionary<TKey, T> ToDictionary<TKey, T>(this IList<T> list, Func<T, TKey> getKeyFunc)
        {
            var result = new Dictionary<TKey, T>();
            if (list == null) return result;
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                result.Add(getKeyFunc(item), item);
            }

            return result;
        }

        /// <summary>
        /// 将 List 转换为 Dictionary
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <typeparam name="T">列表类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="getKeyFunc">获取元素键值的方法</param>
        /// <param name="getValueFunc">获取元素值的方法</param>
        /// <returns>结果</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue, T>(this IList<T> list, Func<T, TKey> getKeyFunc, Func<T, TValue> getValueFunc)
        {
            var result = new Dictionary<TKey, TValue>();
            if (list == null) return result;
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                result.Add(getKeyFunc(item), getValueFunc(item));
            }

            return result;
        }

        /// <summary>
        /// 转换为指定类型的列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TResult">目标类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="selector">选择器</param>
        /// <returns>结果</returns>
        public static List<TResult> ToList<T, TResult>(this IList<T> list, Func<T, TResult> selector)
        {
            var result = new List<TResult>();
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var value = selector(item);
                result.Add(value);
            }

            return result;
        }

        #endregion

        #region Remove

        /// <summary>
        /// 移除列表开头重复元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="persistOne">保留一个</param>
        /// <returns>结果</returns>
        public static IList<T> RemoveStartRepeat<T>(this IList<T> list, bool persistOne = true)
        {
            var first = list.First();
            if (first == null) return list;
            var remove = false;
            var index = 1;
            while (index < list.Count && list[index].Equals(first))
            {
                var temp = list[index];
                if (temp.Equals(first))
                {
                    remove = true;
                    list.RemoveAt(index);
                }
            }

            if (remove && !persistOne)
            {
                list.RemoveAt(0);
            }

            return list;
        }

        /// <summary>
        /// 移除列表尾部重复元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="persistOne">保留一个</param>
        /// <returns>结果</returns>
        public static IList<T> RemoveEndRepeat<T>(this IList<T> list, bool persistOne = true)
        {
            var last = list.Last();
            if (last == null) return list;
            var remove = false;
            for (var i = list.Count - 2; i >= 0; i--)
            {
                var temp = list[i];
                if (temp.Equals(last))
                {
                    remove = true;
                    list.RemoveAt(i + 1);
                }
            }

            if (remove && !persistOne)
            {
                list.RemoveAt(list.Count - 1);
            }

            return list;
        }

        /// <summary>
        /// 移除列表中的重复元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>结果</returns>
        public static IList<T> Distinct<T>(this IList<T> list)
        {
            var ret = new List<T>();
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                if (!ret.Contains(temp))
                {
                    ret.Add(temp);
                }
            }

            list.Clear();
            for (var i = 0; i < ret.Count; i++)
            {
                var temp = ret[i];
                list.Add(temp);
            }

            return list;
        }

        #endregion
    }
}