/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ArrayListExtension.cs
//  Info     : ArrayList 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;

namespace Aya.Extension
{
    public static class ArrayListExtension
    {
        internal static Random Rand = new Random();

        #region Null / Empty

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="arrayList">数组</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty(this ArrayList arrayList)
        {
            var result = arrayList == null || arrayList.Count == 0;
            return result;
        }

        /// <summary>
        /// 是否为空，不可以为 NULL
        /// </summary>
        /// <param name="arrayList">数组</param>
        /// <returns>结果</returns>
        public static bool IsEmpty(this ArrayList arrayList)
        {
            if (arrayList == null)
            {
                throw new NullReferenceException();
            }

            var result = arrayList.Count == 0;
            return result;
        }

        #endregion

        #region Get

        /// <summary>
        /// 第一个元素
        /// </summary>
        /// <param name="arrayList">列表</param>
        /// <returns>结果</returns>
        public static object First(this ArrayList arrayList)
        {
            var result = arrayList != null && arrayList.Count > 0 ? arrayList[0] : null;
            return result;
        }

        /// <summary>
        /// 第一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="arrayList">列表</param>
        /// <returns>结果</returns>
        public static T First<T>(this ArrayList arrayList)
        {
            var result = arrayList != null && arrayList.Count > 0 ? (T)arrayList[0] : default(T);
            return result;
        }

        /// <summary>
        /// 最后一个元素
        /// </summary>
        /// <param name="arrayList">列表</param>
        /// <returns>结果</returns>
        public static object Last(this ArrayList arrayList)
        {
            var result = arrayList != null && arrayList.Count > 0 ? arrayList[arrayList.Count - 1] : null;
            return result;
        }

        /// <summary>
        /// 最后一个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="arrayList">列表</param>
        /// <returns>结果</returns>
        public static T Last<T>(this ArrayList arrayList)
        {
            var result = arrayList != null && arrayList.Count > 0 ? (T)arrayList[arrayList.Count - 1] : default(T);
            return result;
        }

        /// <summary>
        /// 获取随机元素
        /// </summary>
        /// <param name="arrayList">列表</param>
        /// <returns>结果</returns>
        public static object Random(this ArrayList arrayList)
        {
            var result = arrayList.Count > 0 ? arrayList[Rand.Next(0, arrayList.Count)] : null;
            return result;
        }

        /// <summary>
        /// 获取随机元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="arrayList">列表</param>
        /// <returns>结果</returns>
        public static T Random<T>(this ArrayList arrayList)
        {
            var result = arrayList.Count > 0 ? (T)arrayList[Rand.Next(0, arrayList.Count)] : default(T);
            return result;
        }

        #endregion

        #region Sort

        /// <summary>
        /// 乱序
        /// </summary>
        /// <param name="arrayList">列表</param>
        /// <returns>结果</returns>
        public static ArrayList RandSort(this ArrayList arrayList)
        {
            var count = arrayList.Count * 3;
            for (var i = 0; i < count; i++)
            {
                var index1 = Rand.Next(0, arrayList.Count);
                var item1 = arrayList[index1];
                var index2 = Rand.Next(0, arrayList.Count);
                var item2 = arrayList[index2];
                var temp = item2;
                arrayList[index2] = item1;
                arrayList[index1] = temp;
            }

            return arrayList;
        }

        /// <summary>
        /// 按 Key 优先级升序排序
        /// </summary>
        /// <param name="arrayList">列表</param>
        /// <param name="keyGetters">Key访问器</param>
        /// <returns>结果</returns>
        public static ArrayList SortAsc(this ArrayList arrayList, params Func<object, IComparable>[] keyGetters)
        {
            arrayList.Sort(ComparerUtil.GetAscComparer(keyGetters));
            return arrayList;
        }

        /// <summary>
        /// 按 Key 优先级降序排序
        /// </summary>
        /// <param name="arrayList">列表</param>
        /// <param name="keyGetters">Key访问器</param>
        /// <returns>结果</returns>
        public static ArrayList SortDesc(this ArrayList arrayList, params Func<object, IComparable>[] keyGetters)
        {
            arrayList.Sort(ComparerUtil.GetDescComparer(keyGetters));
            return arrayList;
        }

        /// <summary>
        /// 按自定义优先级和自定义升降序排序
        /// </summary>
        /// <param name="arrayList">列表</param>
        /// <param name="keyGetters">Key访问器</param>
        /// <returns>结果</returns>
        public static ArrayList Sort(this ArrayList arrayList, params Func<object, (IComparable, bool)>[] keyGetters)
        {
            arrayList.Sort(ComparerUtil.GetCustomComparer(keyGetters));
            return arrayList;
        }

        #endregion
    }
}
