/////////////////////////////////////////////////////////////////////////////
//
//  Script   : VariantValue.cs
//  Info     : 可变弱类型 实现测试
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Aya.Data
{
    public enum VariantType : uint
    {
        Null = 0,
        Boolean = 1,
        Byte = 2,
        UInt16 = 3,
        Int16 = 4,
        UInt32 = 5,
        Int32 = 6,
        UInt64 = 7,
        Int64 = 8,
        Float32 = 9,
        Float64 = 10,
        Float128 = 11,
        String = 12,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct VariantInternal
    {
        [FieldOffset(0)]
        public bool Boolean;
        [FieldOffset(0)]
        public byte Byte;
        [FieldOffset(0)]
        public ushort UInt16;
        [FieldOffset(0)]
        public short Int16;
        [FieldOffset(0)]
        public uint UInt32;
        [FieldOffset(0)]
        public int Int32;
        [FieldOffset(0)]
        public ulong UInt64;
        [FieldOffset(0)]
        public long Int64;
        [FieldOffset(0)]
        public float Float32;
        [FieldOffset(0)]
        public double Float64;
        [FieldOffset(0)]
        public decimal Float128;
        [FieldOffset(0)]
        public string String;

        #region Override Operator / Equals

        public static bool operator ==(VariantInternal lhs, VariantInternal rhs)
        {
            return lhs.Int64 == rhs.Int64;
        }

        public static bool operator !=(VariantInternal lhs, VariantInternal rhs)
        {
            return lhs.Int64 != rhs.Int64;
        }

        public bool Equals(VariantInternal obj)
        {
            return Int64 == obj.Int64;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is VariantInternal))
            {
                return false;
            }

            return this == (VariantInternal)obj;
        }

        public override int GetHashCode()
        {
            return Int64.GetHashCode();
        } 
        
        #endregion
    }

    [Obsolete("This is a test class !!!")]
    public struct VariantValue
    {
        #region Cache

        internal static Dictionary<VariantType, Type> VariantTypeDic = new Dictionary<VariantType, Type>()
        {
            {VariantType.Boolean, typeof(bool)},
            {VariantType.Byte, typeof(byte)},
            {VariantType.UInt16, typeof(ushort)},
            {VariantType.Int16, typeof(short)},
            {VariantType.UInt32, typeof(uint)},
            {VariantType.Int32, typeof(int)},
            {VariantType.UInt64, typeof(ulong)},
            {VariantType.Int64, typeof(long)},
            {VariantType.Float32, typeof(float)},
            {VariantType.Float64, typeof(double)},
            {VariantType.Float128, typeof(decimal)},
            {VariantType.String, typeof(string)},
        };

        internal static Dictionary<Type, VariantType> TypeVariantDic = new Dictionary<Type, VariantType>()
        {
            {typeof(bool), VariantType.Boolean},
            {typeof(byte), VariantType.Byte},
            {typeof(ushort), VariantType.UInt16},
            {typeof(short), VariantType.Int16},
            {typeof(uint), VariantType.UInt32},
            {typeof(int), VariantType.Int32},
            {typeof(ulong), VariantType.UInt64},
            {typeof(long), VariantType.Int64},
            {typeof(float), VariantType.Float32},
            {typeof(double), VariantType.Float64},
            {typeof(decimal), VariantType.Float128},
            {typeof(string), VariantType.String},
        };

        #endregion

        #region Field / Property

        public VariantType Type;

        public VariantInternal Value
        {
            get => _value;
            set => _value = value;
        }
        private VariantInternal _value; 
        
        #endregion

        #region Get

        public object Get()
        {
            var result = Get(Type);
            return result;
        }

        public object Get(VariantType variantType)
        {
            switch (variantType)
            {
                case VariantType.Null:
                    return null;
                case VariantType.Boolean:
                    return _value.Boolean;
                case VariantType.Byte:
                    return _value.Byte;
                case VariantType.UInt16:
                    return _value.UInt16;
                case VariantType.Int16:
                    return _value.Int16;
                case VariantType.UInt32:
                    return _value.UInt32;
                case VariantType.Int32:
                    return _value.Int32;
                case VariantType.UInt64:
                    return _value.UInt64;
                case VariantType.Int64:
                    return _value.Int64;
                case VariantType.Float32:
                    return _value.Float32;
                case VariantType.Float64:
                    return _value.Float64;
                case VariantType.Float128:
                    return _value.Float128;
                case VariantType.String:
                    return _value.String;
            }

            return default;
        }

        public T Get<T>()
        {
            var type = typeof(T);
            var result = Get(type);
            if (result != null)
            {
                return (T) result;
            }

            return default;
        }

        public object Get(Type type)
        {
            var value = Get(Type);
            if (value == null)
            {
                return default;
            }

            var result = Convert.ChangeType(value, type);
            if (result != null)
            {
                return result;
            }

            return default;
        }

        #endregion

        #region Set

        public void Set<T>(T value)
        {
            var type = typeof(T);

            if (value == null)
            {
                Type = VariantType.Null;
                return;
            }

            if (type == typeof(bool))
            {
                Type = VariantType.Boolean;
                _value.Boolean = (bool)(object)value;
            }

            if (type == typeof(byte))
            {
                Type = VariantType.Byte;
                _value.Byte = (byte)(object)value;
            }

            if (type == typeof(ushort))
            {
                Type = VariantType.UInt16;
                _value.UInt16 = (ushort)(object)value;
            }

            if (type == typeof(short))
            {
                Type = VariantType.Int16;
                _value.Int16 = (short)(object)value;
            }

            if (type == typeof(uint))
            {
                Type = VariantType.UInt32;
                _value.UInt32 = (uint)(object)value;
            }

            if (type == typeof(int))
            {
                Type = VariantType.Int32;
                _value.Int32 = (int)(object)value;
            }

            if (type == typeof(ulong))
            {
                Type = VariantType.UInt64;
                _value.UInt64 = (ulong)(object)value;
            }

            if (type == typeof(long))
            {
                Type = VariantType.Int64;
                _value.Int64 = (long)(object)value;
            }

            if (type == typeof(float))
            {
                Type = VariantType.Float32;
                _value.Float32 = (float)(object)value;
            }

            if (type == typeof(double))
            {
                Type = VariantType.Float64;
                _value.Float64 = (double)(object)value;
            }

            if (type == typeof(decimal))
            {
                Type = VariantType.Float128;
                _value.Float128 = (decimal)(object)value;
            }

            if (type == typeof(string))
            {
                Type = VariantType.String;
                _value.String = value.ToString();
            }
        }

        #endregion

        #region Type Operator

        public static implicit operator VariantValue(bool value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator bool(VariantValue value)
        {
            return value.Get<bool>();
        }

        public static implicit operator VariantValue(byte value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator byte(VariantValue value)
        {
            return value.Get<byte>();
        }

        public static implicit operator VariantValue(ushort value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator ushort(VariantValue value)
        {
            return value.Get<ushort>();
        }

        public static implicit operator VariantValue(short value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator short(VariantValue value)
        {
            return value.Get<short>();
        }

        public static implicit operator VariantValue(uint value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator uint(VariantValue value)
        {
            return value.Get<uint>();
        }

        public static implicit operator VariantValue(int value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator int(VariantValue value)
        {
            return value.Get<int>();
        }

        public static implicit operator VariantValue(ulong value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator ulong(VariantValue value)
        {
            return value.Get<ulong>();
        }

        public static implicit operator VariantValue(long value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator long(VariantValue value)
        {
            return value.Get<long>();
        }

        public static implicit operator VariantValue(float value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator float(VariantValue value)
        {
            return value.Get<float>();
        }

        public static implicit operator VariantValue(double value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator double(VariantValue value)
        {
            return value.Get<double>();
        }

        public static implicit operator VariantValue(decimal value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator decimal(VariantValue value)
        {
            return value.Get<decimal>();
        }

        public static implicit operator VariantValue(string value)
        {
            var result = new VariantValue();
            result.Set(value);
            return result;
        }

        public static implicit operator string(VariantValue value)
        {
            return value.Get<string>();
        }

        #endregion

        #region Arithmetic Operator

        public static VariantValue operator +(VariantValue lhs, VariantValue rhs)
        {
            var variantType = lhs.Type;
            switch (variantType)
            {
                case VariantType.Boolean:
                    return lhs.Get<int>() + rhs.Get<int>();
                case VariantType.Byte:
                    return lhs.Get<byte>() + rhs.Get<byte>();
                case VariantType.UInt16:
                    return lhs.Get<ushort>() + rhs.Get<ushort>();
                case VariantType.Int16:
                    return lhs.Get<short>() + rhs.Get<short>();
                case VariantType.UInt32:
                    return lhs.Get<uint>() + rhs.Get<uint>();
                case VariantType.Int32:
                    return lhs.Get<int>() + rhs.Get<int>();
                case VariantType.UInt64:
                    return lhs.Get<ulong>() + rhs.Get<ulong>();
                case VariantType.Int64:
                    return lhs.Get<long>() + rhs.Get<long>();
                case VariantType.Float32:
                    return lhs.Get<float>() + rhs.Get<float>();
                case VariantType.Float64:
                    return lhs.Get<double>() + rhs.Get<double>();
                case VariantType.Float128:
                    return lhs.Get<decimal>() + rhs.Get<decimal>();
                case VariantType.String:
                    return lhs.Get<string>() + rhs.Get<string>();
            }
           
            return default;
        }

        public static VariantValue operator -(VariantValue lhs, VariantValue rhs)
        {
            var variantType = lhs.Type;
            switch (variantType)
            {
                case VariantType.Boolean:
                    return lhs.Get<int>() - rhs.Get<int>();
                case VariantType.Byte:
                    return lhs.Get<byte>() - rhs.Get<byte>();
                case VariantType.UInt16:
                    return lhs.Get<ushort>() - rhs.Get<ushort>();
                case VariantType.Int16:
                    return lhs.Get<short>() - rhs.Get<short>();
                case VariantType.UInt32:
                    return lhs.Get<uint>() - rhs.Get<uint>();
                case VariantType.Int32:
                    return lhs.Get<int>() - rhs.Get<int>();
                case VariantType.UInt64:
                    return lhs.Get<ulong>() - rhs.Get<ulong>();
                case VariantType.Int64:
                    return lhs.Get<long>() - rhs.Get<long>();
                case VariantType.Float32:
                    return lhs.Get<float>() - rhs.Get<float>();
                case VariantType.Float64:
                    return lhs.Get<double>() - rhs.Get<double>();
                case VariantType.Float128:
                    return lhs.Get<decimal>() - rhs.Get<decimal>();
                case VariantType.String:
                    var leftStr = lhs.Get<string>();
                    var rightStr = rhs.Get<string>();
                    if (!string.IsNullOrEmpty(leftStr) && !string.IsNullOrEmpty(rightStr))
                    {
                        var result = leftStr.Replace(rightStr, "");
                        return result;
                    }
                    return leftStr;
            }

            return default;
        }

        public static VariantValue operator *(VariantValue lhs, VariantValue rhs)
        {
            var variantType = lhs.Type;
            switch (variantType)
            {
                case VariantType.Boolean:
                    return lhs.Get<int>() * rhs.Get<int>();
                case VariantType.Byte:
                    return lhs.Get<byte>() * rhs.Get<byte>();
                case VariantType.UInt16:
                    return lhs.Get<ushort>() * rhs.Get<ushort>();
                case VariantType.Int16:
                    return lhs.Get<short>() * rhs.Get<short>();
                case VariantType.UInt32:
                    return lhs.Get<uint>() * rhs.Get<uint>();
                case VariantType.Int32:
                    return lhs.Get<int>() * rhs.Get<int>();
                case VariantType.UInt64:
                    return lhs.Get<ulong>() * rhs.Get<ulong>();
                case VariantType.Int64:
                    return lhs.Get<long>() * rhs.Get<long>();
                case VariantType.Float32:
                    return lhs.Get<float>() * rhs.Get<float>();
                case VariantType.Float64:
                    return lhs.Get<double>() * rhs.Get<double>();
                case VariantType.Float128:
                    return lhs.Get<decimal>() * rhs.Get<decimal>();
                case VariantType.String:
                    return lhs.Get<string>();
            }

            return default;
        }

        public static VariantValue operator /(VariantValue lhs, VariantValue rhs)
        {
            var variantType = lhs.Type;
            switch (variantType)
            {
                case VariantType.Boolean:
                    return lhs.Get<int>() / rhs.Get<int>();
                case VariantType.Byte:
                    return lhs.Get<byte>() / rhs.Get<byte>();
                case VariantType.UInt16:
                    return lhs.Get<ushort>() / rhs.Get<ushort>();
                case VariantType.Int16:
                    return lhs.Get<short>() / rhs.Get<short>();
                case VariantType.UInt32:
                    return lhs.Get<uint>() / rhs.Get<uint>();
                case VariantType.Int32:
                    return lhs.Get<int>() / rhs.Get<int>();
                case VariantType.UInt64:
                    return lhs.Get<ulong>() / rhs.Get<ulong>();
                case VariantType.Int64:
                    return lhs.Get<long>() / rhs.Get<long>();
                case VariantType.Float32:
                    return lhs.Get<float>() / rhs.Get<float>();
                case VariantType.Float64:
                    return lhs.Get<double>() / rhs.Get<double>();
                case VariantType.Float128:
                    return lhs.Get<decimal>() / rhs.Get<decimal>();
                case VariantType.String:
                    return lhs.Get<string>();
            }

            return default;
        }

        public static VariantValue operator ++(VariantValue lhs)
        {
            var variantType = lhs.Type;
            switch (variantType)
            {
                case VariantType.Boolean:
                    return lhs.Get<bool>();
                case VariantType.Byte:
                    return lhs.Get<byte>() + 1;
                case VariantType.UInt16:
                    return lhs.Get<ushort>() + 1;
                case VariantType.Int16:
                    return lhs.Get<short>() + 1;
                case VariantType.UInt32:
                    return lhs.Get<uint>() + 1;
                case VariantType.Int32:
                    return lhs.Get<int>() + 1;
                case VariantType.UInt64:
                    return lhs.Get<ulong>() + 1;
                case VariantType.Int64:
                    return lhs.Get<long>() + 1;
                case VariantType.Float32:
                    return lhs.Get<float>() + 1;
                case VariantType.Float64:
                    return lhs.Get<double>() + 1;
                case VariantType.Float128:
                    return lhs.Get<decimal>() + 1;
                case VariantType.String:
                    return lhs.Get<string>();
            }

            return default;
        }

        public static VariantValue operator --(VariantValue lhs)
        {
            var variantType = lhs.Type;
            switch (variantType)
            {
                case VariantType.Boolean:
                    return lhs.Get<bool>();
                case VariantType.Byte:
                    return lhs.Get<byte>() - 1;
                case VariantType.UInt16:
                    return lhs.Get<ushort>() - 1;
                case VariantType.Int16:
                    return lhs.Get<short>() - 1;
                case VariantType.UInt32:
                    return lhs.Get<uint>() - 1;
                case VariantType.Int32:
                    return lhs.Get<int>() - 1;
                case VariantType.UInt64:
                    return lhs.Get<ulong>() - 1;
                case VariantType.Int64:
                    return lhs.Get<long>() - 1;
                case VariantType.Float32:
                    return lhs.Get<float>() - 1;
                case VariantType.Float64:
                    return lhs.Get<double>() - 1;
                case VariantType.Float128:
                    return lhs.Get<decimal>() - 1;
                case VariantType.String:
                    return lhs.Get<string>();
            }

            return default;
        }

        #endregion

        #region Override object

        public bool Equals(VariantValue obj)
        {
            return _value == obj._value && Type == obj.Type;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is VariantValue))
            {
                return false;
            }

            var variant = (VariantValue) obj;
            return _value == variant._value && Type == variant.Type;
        }

        public override string ToString()
        {
            return Get().ToString();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion
    }
}
