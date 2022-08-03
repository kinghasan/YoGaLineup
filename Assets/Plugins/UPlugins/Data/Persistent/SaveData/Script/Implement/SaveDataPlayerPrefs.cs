/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveDataPlayerPrefs.cs
//  Info     : 存档管理 —— PlayerPrefs 实现
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;
using Aya.Data.Json;
using Aya.Extension;
using Aya.Security;

namespace Aya.Data.Persistent
{
    internal class SaveDataPlayerPrefs : SaveDataBase, ISaveData
    {
        public SaveDataPlayerPrefs(bool encrypt = false) : base(encrypt)
        {
        }

        #region Set / Get

        public void Set<T>(string module, string key, T value)
        {
            var type = typeof(T);
            if (IsValueType(type))
            {
                SetValue(module, key, value);
            }
            else
            {
                SetObject(module, key, value);
            }
        }

        public void Set<T>(string key, T value)
        {
            Set(SaveData.DefaultModuleName, key, value);
        }

        public T Get<T>(string module, string key, T defaultValue = default(T))
        {
            var type = typeof(T);
            var result = IsValueType(type) ? GetValue(module, key, defaultValue) : GetObject(module, key, defaultValue);
            return result;
        }

        public T Get<T>(string key, T defaultValue = default(T))
        {
            return Get(SaveData.DefaultModuleName, key, defaultValue);
        }

        #endregion

        #region Set Value / Object

        public void SetValue<T>(string module, string key, T value)
        {
            var k = module + "." + key;
            var v = value.CastType<string>();
            if (SaveData.EncryptSave)
            {
                PlayerPrefsAES.SetString(k, v);
            }
            else
            {
                PlayerPrefs.SetString(k, v);
            }
        }

        public void SetValue<T>(string key, T value)
        {
            SetValue(SaveData.DefaultModuleName, key, value);
        }

        public void SetObject<T>(string module, string key, T obj)
        {
            var k = module + "." + key;
            var v = JsonUtil.ToJson(obj);
            if (SaveData.EncryptSave)
            {
                PlayerPrefsAES.SetString(k, v);
            }
            else
            {
                PlayerPrefs.SetString(k, v);
            }
        }

        public void SetObject<T>(string key, T obj)
        {
            SetObject(SaveData.DefaultModuleName, key, obj);
        }

        #endregion

        #region Get Value / Object

        public T GetValue<T>(string module, string key, T defaultValue = default(T))
        {
            var k = module + "." + key;
            var v = SaveData.EncryptSave ? PlayerPrefsAES.GetString(k) : PlayerPrefs.GetString(k);
            if (!string.IsNullOrEmpty(v))
            {
                var value = v.CastType<T>();
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public T GetValue<T>(string key, T defaultValue = default(T))
        {
            return GetValue<T>(SaveData.DefaultModuleName, key, defaultValue);
        }

        public T GetObject<T>(string module, string key, T defaultValue = default(T))
        {
            var k = module + "." + key;
            var v = SaveData.EncryptSave ? PlayerPrefsAES.GetString(k) : PlayerPrefs.GetString(k);
            if (!string.IsNullOrEmpty(v))
            {
                var obj = JsonUtil.ToObject(v, defaultValue);
                return obj;
            }
            else
            {
                return defaultValue;
            }
        }

        public T GetObject<T>(string key, T defaultValue = default(T))
        {
            return GetObject(SaveData.DefaultModuleName, key, defaultValue);
        }

        #endregion

        #region Load

        public void Load(string module)
        {
            throw new Exception("SaveData - PlayerPrefs not support load!");
        }

        public void Load()
        {
            throw new Exception("SaveData - PlayerPrefs not support load!");
        }

        #endregion

        #region Load Async

        public void LoadAsync(string module, Action onDone)
        {
            throw new Exception("SaveData - PlayerPrefs not support async load!");
        }

        public void LoadAsync(Action onDone)
        {
            throw new Exception("SaveData - PlayerPrefs not support async load!");
        }

        #endregion

        #region Delete

        public void Delete(string moudule, string key)
        {
            var k = moudule + "." + key;
            if (SaveData.EncryptSave)
            {
                PlayerPrefsAES.DeleteKey(k);
            }
            else
            {
                PlayerPrefs.DeleteKey(k);
            }
        }

        public void Delete(string key)
        {
            Delete(SaveData.DefaultModuleName, key);
        }

        #endregion

        #region Save

        public void Save(string module)
        {
            PlayerPrefs.Save();
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }

        #endregion

        #region Save Async

        public void SaveAsync(string module, Action onDone)
        {
            Save(module);
            throw new Exception("SaveData - PlayerPrefs not support async save!");
        }

        public void SaveAsync(Action onDone)
        {
            Save();
            throw new Exception("SaveData - PlayerPrefs not support async save!");
        }

        #endregion

        #region Clear

        public void Clear(string module)
        {
            throw new Exception("SaveData - PlayerPrefs not support clear with module!");
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion

        #region Release

        public void Release()
        {
        }

        public void Release(string module)
        {
        }

        #endregion
    }
}
