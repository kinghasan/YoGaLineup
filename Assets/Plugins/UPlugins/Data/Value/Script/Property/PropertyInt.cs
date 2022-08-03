/////////////////////////////////////////////////////////////////////////////
//
//  Script   : PropertyInt.cs
//  Info     : 属性值 —— Int 类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////

namespace Aya.Data
{
    public class PropertyInt : PropertyValue<int>
    {
        public override int Value
        {
            get => BaseValue + BuffValue + EquipValue;
            set => BaseValue = value;
        }

        public PropertyInt()
        {
        }

        public PropertyInt(int baseValue) : base(baseValue)
        {
        }

        #region Override operator

        public static implicit operator PropertyInt(int value)
        {
            return new PropertyInt(value);
        }

        public static implicit operator int(PropertyInt obj)
        {
            return obj.Value;
        }

        public static PropertyInt operator ++(PropertyInt lhs)
        {           
            return lhs.Value + 1;
        }

        public static PropertyInt operator --(PropertyInt lhs)
        {

            return lhs.Value - 1;
        }

        public static bool operator ==(PropertyInt lhs, PropertyInt rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            return lhs.Value == rhs.Value;
        }

        public static bool operator !=(PropertyInt lhs, PropertyInt rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return false;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return true;
            return lhs.Value != rhs.Value;
        }

        #endregion

        #region Override object

        public bool Equals(int obj)
        {
            return obj == Value;
        }

        public bool Equals(PropertyInt obj)
        {
            return Value == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PropertyInt))
                return false;

            return this == (PropertyInt)obj;
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

        #region CompareTo

        public int CompareTo(int other)
        {
            return Value.CompareTo(other);
        }

        #endregion
    }
}
