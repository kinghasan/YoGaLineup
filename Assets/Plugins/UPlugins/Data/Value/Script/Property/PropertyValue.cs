/////////////////////////////////////////////////////////////////////////////
//
//  Script   : PropertyValue.cs
//  Info     : 属性值 —— 泛型基类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Data
{
    public class PropertyValue<T>
    {
        public virtual T Value { get; set; }

        /// <summary>
        /// 基准数值<para/>
        /// 一般来自于初始配置，人物初始属性，用于计算百分比加成的参考数值
        /// </summary>
        public T BaseValue
        {
            get => _baseValue;
            set
            {
                var oriBaseValue = _baseValue;
                var oriValue = Value;
                _baseValue = value;
                OnBaseValueChange(oriBaseValue, _baseValue);
                OnValueChange(oriValue, Value);
            }
        }
        private T _baseValue = default(T);


        /// <summary>
        /// Buff 附加或者扣除数值
        /// </summary>
        public T BuffValue
        {
            get => _buffValue;
            set
            {
                var oriBuffValue = _buffValue;
                var oriValue = Value;
                _buffValue = value;
                OnBuffValueChange(oriBuffValue, _buffValue);
                OnValueChange(oriValue, Value);
            }
        }
        private T _buffValue = default(T);

        /// <summary>
        /// 装备 附加或者扣除数值
        /// </summary>
        public T EquipValue
        {
            get => _euqipValue;
            set
            {
                var oriEquipValue = _euqipValue;
                var oriValue = Value;
                _euqipValue = value;
                OnEquipValueChange(oriEquipValue, _euqipValue);
                OnValueChange(oriValue, Value);
            }
        }
        private T _euqipValue = default(T);

        public Action<T, T> OnValueChange = delegate {};
        public Action<T, T> OnBaseValueChange = delegate { };
        public Action<T, T> OnBuffValueChange = delegate { };
        public Action<T, T> OnEquipValueChange = delegate { };

        public PropertyValue()
        {
        }

        public PropertyValue(T baseValue)
        {
            BaseValue = baseValue;
        }

        public virtual void Reset(T baseValue = default(T))
        {
            BaseValue = baseValue;
            BuffValue = default(T);
            EquipValue = default(T);
        }
    }
}
