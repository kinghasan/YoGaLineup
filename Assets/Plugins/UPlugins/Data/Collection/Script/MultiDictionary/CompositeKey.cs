/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CompositeKey.fcs
//  Info     : 复合键，可选区分键顺序
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Data
{
    public struct CompositeKey<TKey1, TKey2> : IEquatable<CompositeKey<TKey1, TKey2>>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        public bool CheckSequence;

        public readonly TKey1 Key1;
        public readonly TKey2 Key2;

        public CompositeKey(TKey1 key1, TKey2 key2) : this(false, key1, key2)
        {
        }

        public CompositeKey(bool checkSequence, TKey1 key1, TKey2 key2)
        {
            CheckSequence = checkSequence;

            Key1 = key1;
            Key2 = key2;
        }

        public bool Equals(CompositeKey<TKey1, TKey2> other)
        {
            bool result;
            if (CheckSequence)
            {
                result = Key1.Equals(other.Key1) && Key2.Equals(other.Key2);
            }
            else
            {
                result = GetHashCode() == other.GetHashCode();
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            var result = obj != null && Equals((CompositeKey<TKey1, TKey2>)obj);
            return result;
        }

        public override int GetHashCode()
        {
            var hashCode1 = Key1 == null ? 0 : Key1.GetHashCode();
            var hashCode2 = Key2 == null ? 0 : Key2.GetHashCode();
            var result = hashCode1 ^ hashCode2;
            return result;
        }
    }
}