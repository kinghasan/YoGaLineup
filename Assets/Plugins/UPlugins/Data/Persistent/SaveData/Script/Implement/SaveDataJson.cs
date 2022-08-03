/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveDataJson.cs
//  Info     : 存档管理 —— Json 实现
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aya.Data.Json;
using Aya.Extension;

namespace Aya.Data.Persistent
{
    internal class SaveDataJson : SaveDataBase, ISaveData
    {
        #region Private Json

        protected Dictionary<string, JObject> ModDic = new Dictionary<string, JObject>();

        private string GetTextAsset(string module)
        {
            var path = GetSavePath(module);
            if (!File.Exists(path)) return null;
            var text = File.ReadAllText(path);
            var ret = SaveData.EncryptSave ? SaveDataInterface.Decrypt(text) : text;
            return ret;
        }

        private JObject GetJson(string module)
        {
            var json = ModDic.GetValue(module);
            if (json != null)
            {
                return json;
            }

            var text = GetTextAsset(module);
            if (string.IsNullOrEmpty(text))
            {
                json = new JObject();
                ModDic.Add(module, json);
            }
            else
            {
                json = JObject.Parse(text);
            }

            return json;
        }

        private void SetJsonValue(string module, string key, string value)
        {
            var json = GetJson(module);
            json[key] = value;
            ModDic[module] = json;
        }

        private void SetJsonObj(string module, string key, string value)
        {
            var json = GetJson(module);
            var jsonValue = JObject.Parse(value);
            json[key] = jsonValue;
            ModDic[module] = json;
        }

        #endregion

        public SaveDataJson(bool encrypt = false) : base(encrypt)
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
            var result = Get(SaveData.DefaultModuleName, key, defaultValue);
            return result;
        }

        #endregion

        #region Set Value / Object

        public void SetValue<T>(string module, string key, T value)
        {
            SetJsonValue(module, key, value.ToString());
        }

        public void SetValue<T>(string key, T value)
        {
            SetValue(SaveData.DefaultModuleName, key, value);
        }

        public void SetObject<T>(string module, string key, T obj)
        {
            var value = JsonUtil.ToJson(obj);
            SetJsonObj(module, key, value);
        }

        public void SetObject<T>(string key, T obj)
        {
            SetObject(SaveData.DefaultModuleName, key, obj);
        }

        #endregion

        #region Get Value / Object

        public T GetValue<T>(string module, string key, T defaultValue = default(T))
        {
            var json = GetJson(module);
            var value = json.Get(key);
            if (value != null && value.IsValid)
            {
                var ret = value.GetValue().CastType<T>();
                return ret;
            }
            else
            {
                return defaultValue;
            }
        }

        public T GetValue<T>(string key, T defaultValue = default(T))
        {
            var result = GetValue(SaveData.DefaultModuleName, key, defaultValue);
            return result;
        }

        public T GetObject<T>(string module, string key, T defaultValue = default(T))
        {
            var json = GetJson(module);
            var value = json.Get(key);
            if (value != null && value.IsValid)
            {
                var ret = value.ToString();
                var obj = JsonUtil.ToObject(ret, defaultValue);
                return obj;
            }
            else
            {
                return defaultValue;
            }
        }

        public T GetObject<T>(string key, T defaultValue = default(T))
        {
            var result = GetObject(SaveData.DefaultModuleName, key, defaultValue);
            return result;
        }

        #endregion

        #region Load

        public void Load(string module)
        {
            GetJson(module);
        }

        public void Load()
        {
            GetJson(SaveData.DefaultModuleName);
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
            var json = GetJson(module);
            json.Remove(key);
        }

        public void Delete(string key)
        {
            Delete(SaveData.DefaultModuleName, key);
        }

        #endregion

        #region Save

        public void Save(string module)
        {
            var json = GetJson(module);
            var text = "";
            if (SaveData.EncryptSave)
            {
                text = json.ToString();
            }
            else
            {
                text = json.ToFormatString();
            }

            var path = GetSavePath(module);
            var saveText = SaveData.EncryptSave ? SaveDataInterface.Encrypt(text) : text;

            File.WriteAllText(path, saveText);
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