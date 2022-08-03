/////////////////////////////////////////////////////////////////////////////
//
//  Script   : sDictionary.cs
//  Info     : 可存储字典
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Aya.Data.Json;

namespace Aya.Data.Persistent
{
    [Serializable]
    public class sDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        [JsonIgnore]
        public string Key { get; protected set; }

        #region Construct

        /// <summary>
        /// 仅供反射创建用构造方法
        /// </summary>
        public sDictionary()
        {
        }

        public sDictionary(string key)
        {
            Key = key;
        }

        #endregion

        #region Override Dictionary

        public new TValue this[TKey key]
        {
            get => base[key];
            set
            {
                base[key] = value;
                Save();
            }
        }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            Save();
        }

        public new bool Remove(TKey key)
        {
            var ret = base.Remove(key);
            Save();
            return ret;
        }

        public new void Clear()
        {
            base.Clear();
            Save();
        }

        #endregion

        #region Load / Save

        /// <summary>
        /// 读取值，不能在MonoBehaviour构造方法中调用
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static sDictionary<TKey, TValue> Load(string key)
        {
            var result = SaveData.GetObject(key, new sDictionary<TKey, TValue>(key));
            result.Key = key;
            return result;
        }

        /// <summary>
        /// 保存值
        /// </summary>
        public void Save()
        {
            SaveData.SetObject(Key, this);
        }

        #endregion
    }
}
