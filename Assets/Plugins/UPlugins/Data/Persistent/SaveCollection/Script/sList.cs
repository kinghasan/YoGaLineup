/////////////////////////////////////////////////////////////////////////////
//
//  Script   : sList.cs
//  Info     : 可存储列表
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
    public class sList<T> : List<T>
    {
        [JsonIgnore]
        public string Key { get; protected set; }

        #region Construct

        /// <summary>
        /// 仅供反射创建用构造方法
        /// </summary>
        public sList()
        {
        }

        public sList(string key)
        {
            Key = key;
        }

        #endregion

        #region Override List

        public new T this[int index]
        {
            get => base[index];
            set
            {
                base[index] = value;
                Save();
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            Save();
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            Save();
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            Save();
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            Save();
        }

        public new bool Remove(T item)
        {
            var ret = base.Remove(item);
            Save();
            return ret;
        }

        public new int RemoveAll(Predicate<T> match)
        {
            var ret = base.RemoveAll(match);
            Save();
            return ret;
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            Save();
        }

        public new void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            Save();
        }

        public new void Reverse()
        {
            base.Reverse();
            Save();
        }

        public new void Reverse(int index, int count)
        {
            base.Reverse(index, count);
            Save();
        }

        public new void Sort()
        {
            base.Sort();
            Save();
        }

        public new void Sort(IComparer<T> comparer)
        {
            base.Sort(comparer);
            Save();
        }

        public new void Sort(int index, int count, IComparer<T> comparer)
        {
            base.Sort(index, count, comparer);
            Save();
        }

        public new void Sort(Comparison<T> comparison)
        {
            base.Sort(comparison);
            Save();
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
        public static sList<T> Load(string key)
        {
            var result = SaveData.GetObject(key, new sList<T>(key));
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
