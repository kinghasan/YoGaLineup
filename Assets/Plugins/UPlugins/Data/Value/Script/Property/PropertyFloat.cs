/////////////////////////////////////////////////////////////////////////////
//
//  Script   : PropertyFloat.cs
//  Info     : 属性值 —— Float 类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Globalization;

namespace Aya.Data
{
    public class PropertyFloat : PropertyValue<float>
    {
        public override float Value
        {
            get => BaseValue + BuffValue + EquipValue;
            set => BaseValue = value;
        }

        public PropertyFloat()
        {
        }

        public PropertyFloat(float baseValue) : base(baseValue)
        {
        }

        #region Override operator

        public static implicit operator PropertyFloat(float value)
        {
            return new PropertyFloat(value);
        }

        public static implicit operator float(PropertyFloat obj)
        {
            return obj.Value;
        }

        public static PropertyFloat operator ++(PropertyFloat lhs)
        {
            return lhs.Value + 1;
        }

        public static PropertyFloat operator --(PropertyFloat lhs)
        {
            return lhs.Value - 1;
        }

        public static bool operator ==(PropertyFloat lhs, PropertyFloat rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            return Math.Abs(lhs.Value - rhs.Value) < 1e-6;
        }

        public static bool operator !=(PropertyFloat lhs, PropertyFloat rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return false;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return true;
            return Math.Abs(lhs.Value - rhs.Value) > 1e-6;
        }

        #endregion

        #region Override object

        public bool Equals(float obj)
        {
            return Math.Abs(Value - obj) < 1e-6;
        }

        public bool Equals(PropertyFloat obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PropertyFloat))
                return false;
            return this == (PropertyFloat)obj;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(string format)
        {
            return Value.ToString(format);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion
    }
}
