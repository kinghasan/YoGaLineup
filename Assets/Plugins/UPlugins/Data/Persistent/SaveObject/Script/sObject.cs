/////////////////////////////////////////////////////////////////////////////
//
//  Script   : sObject.cs
//  Info     : 可存储对象类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Data.Persistent
{
    [Serializable]
    public class sObject<T> : sObject where T : sObject
    {
        protected static string SaveModule => SaveData.DefaultModuleName;

        protected sObject(string key) : base(key)
        {
        }

        public void Save()
        {
            SaveData.SetObject(SaveModule, Key, this);
            SaveData.Save(SaveModule);
        }

        public static T Load(string key)
        {
            var ret = SaveData.GetObject<T>(SaveModule, key);
            return ret;
        }

        public static void Save(T obj)
        {
            SaveData.SetObject(SaveModule, obj.Key, obj);
            SaveData.Save(SaveModule);
        }
    }

    public abstract class sObject
    {
        public string Key { get; protected set; }

        protected sObject(string key)
        {
            Key = key;
        }
    }
}