/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MultiDictionary.fcs
//  Info     : 复合字典，使用两个Key对应一个Value
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Data
{
    public class MultiDictionary<TKey1, TKey2, TValue> : Dictionary<CompositeKey<TKey1, TKey2>, TValue>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        public bool CheckKeySequence { get; protected set; } = false;

        public MultiDictionary(bool checkKeySequence = false)
        {
            CheckKeySequence = checkKeySequence;
        }

        public TValue this[TKey1 key1, TKey2 key2]
        {
            get
            {
                var key = new CompositeKey<TKey1, TKey2>(CheckKeySequence, key1, key2);
                var result = this[key];
                return result;
            }
            set
            {
                var key = new CompositeKey<TKey1, TKey2>(CheckKeySequence, key1, key2);
                this[key] = value;
            }
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            var key = new CompositeKey<TKey1, TKey2>(CheckKeySequence, key1, key2);
            Add(key, value);
        }

        public void Remove(TKey1 key1, TKey2 key2)
        {
            var key = new CompositeKey<TKey1, TKey2>(CheckKeySequence, key1, key2);
            Remove(key);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2)
        {
            var key = new CompositeKey<TKey1, TKey2>(CheckKeySequence, key1, key2);
            var result = ContainsKey(key);
            return result;
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value)
        {
            var key = new CompositeKey<TKey1, TKey2>(CheckKeySequence, key1, key2);
            var result = TryGetValue(key, out value);
            return result;
        }
    }
}
