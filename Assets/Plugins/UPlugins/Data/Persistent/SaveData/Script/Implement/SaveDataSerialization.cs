/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveDataSerialization.cs
//  Info     : 存档管理 —— CSharp Serialization 实现
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Aya.Extension;

namespace Aya.Data.Persistent
{
    internal class SaveDataSerialization : SaveDataBase, ISaveData
    {
        #region Private SaveDataObject

        [Serializable]
        public class SaveDataObject
        {
            public Dictionary<string, object> Data = new Dictionary<string, object>();
        }

        protected Dictionary<string, SaveDataObject> ModDic = new Dictionary<string, SaveDataObject>();

        private SaveDataObject GetSerializeObject(string module)
        {
            var path = GetSavePath(module);
            if (!File.Exists(path)) return null;
            var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var binFormat = new BinaryFormatter();
            var ret = binFormat.Deserialize(stream) as SaveDataObject;
            stream.Close();
            return ret;
        }

        private SaveDataObject GetData(string module)
        {
            var json = ModDic.GetValue(module);
            if (json != null)
            {
                return json;
            }

            var mod = GetSerializeObject(module);
            if (mod == null)
            {
                json = new SaveDataObject();
                ModDic.Add(module, json);
            }

            return json;
        }

        private void SetData<T>(string module, string key, T value)
        {
            var mod = GetData(module);
            mod.Data[key] = value;
            ModDic[module] = mod;
        }

        #endregion

        public SaveDataSerialization(bool encrypt = false) : base(encrypt)
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
            SetData(module, key, value);
        }

        public void SetValue<T>(string key, T value)
        {
            SetData(SaveData.DefaultModuleName, key, value);
        }

        public void SetObject<T>(string module, string key, T obj)
        {
            SetData(module, key, obj);
        }

        public void SetObject<T>(string key, T obj)
        {
            SetData(SaveData.DefaultModuleName, key, obj);
        }

        #endregion

        #region Get Value / Object

        public T GetValue<T>(string module, string key, T defaultValue = default(T))
        {
            var value = GetData(module).Data.GetValue(key);
            if (value != null)
            {
                var ret = (value as string).CastType<T>();
                return ret;
            }
            else
            {
                return defaultValue;
            }
        }

        public T GetValue<T>(string key, T defaultValue = default(T))
        {
            return GetValue(SaveData.DefaultModuleName, key, defaultValue);
        }

        public T GetObject<T>(string module, string key, T defaultValue = default(T))
        {
            var value = GetData(module).Data.GetValue(key);
            if (value != null)
            {
                var obj = (T) value;
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
            GetData(module);
        }

        public void Load()
        {
            GetData(SaveData.DefaultModuleName);
        }

        #endregion

        #region Load Async

        public async void LoadAsync(string module, Action onDone)
        {
            await Task.Run(() => { Load(module); });
            onDone();
        }

        public async void LoadAsync(Action onDone)
        {
            await Task.Run(() => { Load(); });
            onDone();
        }

        #endregion

        #region Delete

        public void Delete(string module, string key)
        {
            var mod = GetData(module);
            mod.Data.Remove(key);
        }

        public void Delete(string key)
        {
            Delete(SaveData.DefaultModuleName, key);
        }

        #endregion

        #region Save

        public void Save(string module)
        {
            var path = GetSavePath(module);
            var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var binFormat = new BinaryFormatter();
            binFormat.Serialize(stream, path);
            stream.Close();
        }

        public void Save()
        {
            foreach (var kv in ModDic)
            {
                Save(kv.Key);
            }
        }

        #endregion

        #region Save Async

        public async void SaveAsync(string module, Action onDone)
        {
            await Task.Run(() => { Save(module); });
            onDone();
        }

        public async void SaveAsync(Action onDone)
        {
            await Task.Run(() => { Save(); });
            onDone();
        }

        #endregion

        #region Clear

        public void Clear(string module)
        {
            var path = GetSavePath(module);
            ModDic.Remove(module);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void Clear()
        {
            ModDic.Clear();
            var files = Directory.GetFiles(SaveData.SavePath);
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                File.Delete(file);
            }
        }

        #endregion

        #region Release

        public void Release()
        {
            ModDic.Clear();
        }

        public void Release(string module)
        {
            if (ModDic.ContainsKey(module))
            {
                ModDic.Remove(module);
            }
        }

        #endregion
    }
}