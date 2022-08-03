/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CompareUtil.cs
//  Info     : 比较工具类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Util
{
    public static class CompareUtil<T>
    {
        #region IEqualityComparer
        
        public static IEqualityComparer<T> CreateEqualityComparer<TV>(Func<T, TV> keySelector)
        {
            var result = new CommonEqualityComparer<T, TV>(keySelector);
            return result;
        }

        public static IEqualityComparer<T> CreateEqualityComparer<TV>(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
        {
            var result = new CommonEqualityComparer<T, TV>(keySelector, comparer);
            return result;
        }

        #endregion

        #region IComparer

        public static IComparer<T> CreateComparer<TV>(Func<T, TV> keySelector)
        {
            var result = new CommonComparer<T, TV>(keySelector);
            return result;
        }
        public static IComparer<T> CreateComparer<TV>(Func<T, TV> keySelector, IComparer<TV> comparer)
        {
            var result = new CommonComparer<T, TV>(keySelector, comparer);
            return result;
        } 

        #endregion
    }

    public class CommonEqualityComparer<T, TV> : IEqualityComparer<T>
    {
        private readonly Func<T, TV> _keySelector;
        private readonly IEqualityComparer<TV> _comparer;

        public CommonEqualityComparer(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
        {
            _keySelector = keySelector;
            _comparer = comparer;
        }

        public CommonEqualityComparer(Func<T, TV> keySelector) : this(keySelector, EqualityComparer<TV>.Default)
        {
        }

        public bool Equals(T x, T y)
        {
            var result = _comparer.Equals(_keySelector(x), _keySelector(y));
            return result;
        }

        public int GetHashCode(T obj)
        {
            var result = _comparer.GetHashCode(_keySelector(obj));
            return result;
        }
    }

    public class CommonComparer<T, TV> : IComparer<T>
    {
        private readonly Func<T, TV> _keySelector;
        private readonly IComparer<TV> _comparer;

        public CommonComparer(Func<T, TV> keySelector, IComparer<TV> comparer)
        {
            _keySelector = keySelector;
            _comparer = comparer;
        }

        public CommonComparer(Func<T, TV> keySelector) : this(keySelector, Comparer<TV>.Default)
        {
        }

        public int Compare(T x, T y)
        {
            var result = _comparer.Compare(_keySelector(x), _keySelector(y));
            return result;
        }
    }
}
