/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ComparisonUtil.cs
//  Info     : 比较排序辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Extension
{
    public static class ComparisonUtil
    {
        #region Comparison T,IComparable

        /// <summary>
        /// 获取升序比较器
        /// </summary>
        /// <typeparam name="T">排序对象类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>比较器</returns>
        public static Comparison<T> GetAscComparison<T>(params Func<T, IComparable>[] keyGetters)
        {
            int Comparison(T t1, T t2)
            {
                var index = 0;
                int diff;
                do
                {
                    var getter = keyGetters[index];
                    diff = getter(t1).CompareTo(getter(t2));
                    index++;
                } while (index < keyGetters.Length && diff == 0);

                return diff;
            }

            return Comparison;
        }

        /// <summary>
        /// 获取降序比较器
        /// </summary>
        /// <typeparam name="T">排序对象类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>比较器</returns>
        public static Comparison<T> GetDescComparison<T>(params Func<T, IComparable>[] keyGetters)
        {
            int Comparison(T t1, T t2)
            {
                var index = 0;
                int diff;
                do
                {
                    var getter = keyGetters[index];
                    diff = getter(t2).CompareTo(getter(t1));
                    index++;
                } while (index < keyGetters.Length && diff == 0);

                return diff;
            }

            return Comparison;
        }

        /// <summary>
        /// 获取自定义升降序比较器
        /// </summary>
        /// <typeparam name="T">排序对象类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>比较器</returns>
        public static Comparison<T> GetCustomComparison<T>(params Func<T, (IComparable, bool)>[] keyGetters)
        {
            int Comparison(T t1, T t2)
            {
                var index = 0;
                int diff;
                do
                {
                    var getter = keyGetters[index];
                    var (v1, asc) = getter(t1);
                    var (v2, _) = getter(t2);
                    diff = asc ? v1.CompareTo(v2) : v2.CompareTo(v1);
                    index++;
                } while (index < keyGetters.Length && diff == 0);

                return diff;
            }

            return Comparison;
        }

        #endregion

        #region Comparison T,IComparable<TValue>

        /// <summary>
        /// 获取升序比较器
        /// </summary>
        /// <typeparam name="T">排序对象类型</typeparam>
        /// <typeparam name="TValue">比较值类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>比较器</returns>
        public static Comparison<T> GetAscComparison<T, TValue>(params Func<T, TValue>[] keyGetters) where TValue : IComparable<TValue>
        {
            int Comparison(T t1, T t2)
            {
                var index = 0;
                int diff;
                do
                {
                    var getter = keyGetters[index];
                    diff = getter(t1).CompareTo(getter(t2));
                    index++;
                } while (index < keyGetters.Length && diff == 0);

                return diff;
            }

            return Comparison;
        }

        /// <summary>
        /// 获取降序比较器
        /// </summary>
        /// <typeparam name="T">排序对象类型</typeparam>
        /// <typeparam name="TValue">比较值类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>比较器</returns>
        public static Comparison<T> GetDescComparison<T, TValue>(params Func<T, TValue>[] keyGetters) where TValue : IComparable<TValue>
        {
            int Comparison(T t1, T t2)
            {
                var index = 0;
                int diff;
                do
                {
                    var getter = keyGetters[index];
                    diff = getter(t2).CompareTo(getter(t1));
                    index++;
                } while (index < keyGetters.Length && diff == 0);

                return diff;
            }

            return Comparison;
        }

        /// <summary>
        /// 获取自定义升降序比较器
        /// </summary>
        /// <typeparam name="T">排序对象类型</typeparam>
        /// <typeparam name="TValue">比较值类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>比较器</returns>
        public static Comparison<T> GetCustomComparison<T, TValue>(params Func<T, (TValue, bool)>[] keyGetters) where TValue : IComparable<TValue>
        {
            int Comparison(T t1, T t2)
            {
                var index = 0;
                int diff;
                do
                {
                    var getter = keyGetters[index];
                    var (v1, asc) = getter(t1);
                    var (v2, _) = getter(t2);
                    diff = asc ? v1.CompareTo(v2) : v2.CompareTo(v1);
                    index++;
                } while (index < keyGetters.Length && diff == 0);

                return diff;
            }

            return Comparison;
        }

        #endregion
    }
}
