/////////////////////////////////////////////////////////////////////////////
//
//  Script   : PropertyObject.cs
//  Info     : 属性值 —— object 类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////

namespace Aya.Data
{
    public class PropertyObject<T> : PropertyValue<T> where T : class 
    {
        public override T Value
        {
            get
            {
                if (BuffValue != null) return BuffValue;
                return BaseValue;
            }
            set => BaseValue = value;
        }

        public PropertyObject(T baseValue) : base(baseValue)
        {
        }

        #region Override operator

        public static implicit operator PropertyObject<T>(T value)
        {
            return new PropertyObject<T>(value);
        }

        public static implicit operator T(PropertyObject<T> obj)
        {
            return obj.Value;
        }

        #endregion
    }
}
