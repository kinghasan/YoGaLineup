/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveValue.cs
//  Info     : 可存储值类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Aya.Data.Json;
using Aya.Extension;

namespace Aya.Data.Persistent
{
    [Serializable]
    public abstract class SaveValue<T> : SaveValue
    {
        // 存储功能的默认实现，可按需替换
        protected static string SaveModule => SaveData.DefaultModuleName;

        [JsonIgnore]
        public static Dictionary<string, SaveValue<T>> ParamDic = new Dictionary<string, SaveValue<T>>();
        [JsonIgnore]
        protected static Dictionary<string, Action<T>> OnChangeValueDic = new Dictionary<string, Action<T>>();

        [JsonIgnore]
        [NonSerialized]
        private readonly T _defaultValue = default(T);

        // 缓存当前值，避免频繁读取存储数据，减小IO开销
        private T _currentValue = default(T);
        private bool _needUpdateValue = true;

        [JsonIgnore]
        public T Value
        {
            get
            {
                if (!_needUpdateValue) return _currentValue;
                _currentValue = SaveData.GetValue<T>(SaveModule, Key, _defaultValue);
                _needUpdateValue = false;
                return _currentValue;
            }
            set
            {
                SaveData.SetValue<T>(SaveModule, Key, value);
                _needUpdateValue = true;
                OnChangeValueDic.GetOrAdd(Key, delegate { })(value);
            }
        }

        [JsonIgnore]
        public Action<T> OnChangeValue
        {
            get
            {
                var onChangeValue = OnChangeValueDic.GetOrAdd(Key, delegate { });
                return onChangeValue;
            }
            set
            {
                OnChangeValueDic.GetOrAdd(Key, delegate { });
                OnChangeValueDic[Key] = value;
            }
        }

        protected SaveValue(string key, T defaultValue = default(T)) : base(key)
        {
            _defaultValue = defaultValue;
            ParamDic.AddOrReplace(Key, this);
            var onChangeValue = OnChangeValueDic.GetOrAdd(Key, delegate {});
            onChangeValue += OnChangeValue;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static Action<T> GetCallback(string key)
        {
            var onChangeValue = OnChangeValueDic.GetOrAdd(key, delegate{});
            return onChangeValue;
        }
    }

    public abstract class SaveValue
    {
        public string Key { get; set; }

        protected SaveValue(string key)
        {
            Key = key;
        }
    }
}
