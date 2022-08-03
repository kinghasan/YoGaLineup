/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ComparerUtil.cs
//  Info     : 比较器辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class ComparerUtil
    {
        #region IComparer

        /// <summary>
        /// 获取升序比较器
        /// </summary>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static IComparer GetAscComparer(params Func<object, IComparable>[] keyGetters)
        {
            return new AscComparer(keyGetters);
        }

        /// <summary>
        /// 获取降序比较器
        /// </summary>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static IComparer GetDescComparer(params Func<object, IComparable>[] keyGetters)
        {
            return new DescComparer(keyGetters);
        }

        /// <summary>
        /// 获取自定义比较器
        /// </summary>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static IComparer GetCustomComparer(params Func<object, (IComparable, bool)>[] keyGetters)
        {
            return new CustomComparer(keyGetters);
        }

        #endregion

        #region IComparer<T>
       
        /// <summary>
        /// 获取升序比较器
        /// </summary>
        /// <typeparam name="T">比较类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static IComparer<T> GetAscComparer<T>(params Func<T, IComparable>[] keyGetters)
        {
            return new AscComparer<T>(keyGetters);
        }

        /// <summary>
        /// 获取降序比较器
        /// </summary>
        /// <typeparam name="T">比较类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static IComparer<T> GetDescComparer<T>(params Func<T, IComparable>[] keyGetters)
        {
            return new DescComparer<T>(keyGetters);
        }

        /// <summary>
        /// 获取自定义比较器
        /// </summary>
        /// <typeparam name="T">比较类型</typeparam>
        /// <param name="keyGetters">比较值访问器</param>
        /// <returns>结果</returns>
        public static IComparer<T> GetCustomComparer<T>(params Func<T, (IComparable, bool)>[] keyGetters)
        {
            return new CustomComparer<T>(keyGetters);
        } 

        #endregion
    }

    #region Common Comparer : IComparer<T>

    public abstract class CommonComparer<T> : IComparer<T>
    {
        public Comparison<T> Comparison { get; protected set; }

        protected CommonComparer()
        {
        }

        public abstract int Compare(T v1, T v2);
    }

    public class AscComparer<T> : CommonComparer<T>
    {
        public Func<T, IComparable>[] KeyGetters { get; }

        public AscComparer(params Func<T, IComparable>[] keyGetters)
        {
            KeyGetters = keyGetters;
            Comparison = ComparisonUtil.GetAscComparison(keyGetters);
        }

        public override int Compare(T v1, T v2)
        {
            return Comparison(v1, v2);
        }
    }

    public class DescComparer<T> : CommonComparer<T>
    {
        public Func<T, IComparable>[] KeyGetters { get; }

        public DescComparer(params Func<T, IComparable>[] keyGetters)
        {
            KeyGetters = keyGetters;
            Comparison = ComparisonUtil.GetDescComparison(keyGetters);
        }

        public override int Compare(T v1, T v2)
        {
            return Comparison(v1, v2);
        }
    }

    public class CustomComparer<T> : CommonComparer<T>
    {
        public Func<T, (IComparable, bool)>[] KeyGetters { get; }

        public CustomComparer(params Func<T, (IComparable, bool)>[] keyGetters)
        {
            KeyGetters = keyGetters;
            Comparison = ComparisonUtil.GetCustomComparison(keyGetters);
        }

        public override int Compare(T v1, T v2)
        {
            return Comparison(v1, v2);
        }
    }

    #endregion

    #region Common Comparer : IComparer

    public abstract class CommonComparer : IComparer
    {
        public Comparison<object> Comparison { get; protected set; }

        protected CommonComparer()
        {
        }

        public abstract int Compare(object v1, object v2);
    }

    public class AscComparer : CommonComparer
    {
        public Func<object, IComparable>[] KeyGetters { get; }

        public AscComparer(params Func<object, IComparable>[] keyGetters)
        {
            KeyGetters = keyGetters;
            Comparison = ComparisonUtil.GetAscComparison(keyGetters);
        }

        public override int Compare(object v1, object v2)
        {
            return Comparison(v1, v2);
        }
    }

    public class DescComparer : CommonComparer
    {
        public Func<object, IComparable>[] KeyGetters { get; }

        public DescComparer(params Func<object, IComparable>[] keyGetters)
        {
            KeyGetters = keyGetters;
            Comparison = ComparisonUtil.GetDescComparison(keyGetters);
        }

        public override int Compare(object v1, object v2)
        {
            return Comparison(v1, v2);
        }
    }

    public class CustomComparer : CommonComparer
    {
        public Func<object, (IComparable, bool)>[] KeyGetters { get; }

        public CustomComparer(params Func<object, (IComparable, bool)>[] keyGetters)
        {
            KeyGetters = keyGetters;
            Comparison = ComparisonUtil.GetCustomComparison(keyGetters);
        }

        public override int Compare(object v1, object v2)
        {
            return Comparison(v1, v2);
        }
    }

    #endregion
}
