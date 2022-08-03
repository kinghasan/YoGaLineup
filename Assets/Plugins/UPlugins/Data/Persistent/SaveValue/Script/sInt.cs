/////////////////////////////////////////////////////////////////////////////
//
//  Script   : sInt.cs
//  Info     : 可存取数据类型 Int
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Security;

namespace Aya.Data.Persistent
{
    [Serializable]
    public class sInt : SaveValue<int>, IConvertible
    {
        public sInt(string key, int defaultValue = 0) : base(key, defaultValue)
        {
        }

        #region Override operator
        public static int operator +(sInt lhs, int rhs)
        {
            return lhs.Value + rhs;
        }

        public static int operator -(sInt lhs, int rhs)
        {
            return lhs.Value - rhs;
        }

        public static int operator *(sInt lhs, int rhs)
        {
            return lhs.Value = rhs;
        }

        public static int operator /(sInt lhs, int rhs)
        {
            return lhs.Value / rhs;
        }

        public static sInt operator ++(sInt lhs)
        {
            lhs.Value++;
            return lhs;
        }

        public static sInt operator --(sInt lhs)
        {
            lhs.Value--;
            return lhs;
        }

        public static bool operator ==(sInt lhs, sInt rhs)
        {
            return lhs.Value == rhs.Value;
        }

        public static bool operator !=(sInt lhs, sInt rhs)
        {
            return lhs.Value != rhs.Value;
        }

        public static implicit operator int(sInt obj)
        {
            return obj.Value;
        }

        public static implicit operator cInt(sInt obj)
        {
            cInt ret = obj.Value;
            return ret;
        }
        #endregion

        #region Override object

        public bool Equals(int obj)
        {
            return Value == obj;
        }

        public bool Equals(cInt obj)
        {
            return Value == obj;
        }

        public override bool Equals(object obj)
        {
            return this == (sInt)obj;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        #region IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value); ;
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return Convert.ToString(Value);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType);
        }

        #endregion
    }
}