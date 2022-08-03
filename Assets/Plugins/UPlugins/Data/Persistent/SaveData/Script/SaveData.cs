/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveData.cs
//  Info     : 存档快速调用接口
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using UnityEngine;
using Aya.Security;

namespace Aya.Data.Persistent
{
    public static class SaveData
    {
        #region Setting

        /// <summary>
        /// 指定存储类型
        /// </summary>
        public static SaveType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _saveImpl = null;
                }

                _type = value;
            }
        }

        private static SaveType _type = SaveType.PlayerPrefs;

        /// <summary>
        /// 默认模块名
        /// </summary>
        public static string DefaultModuleName { get; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public static string SavePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_savePath)) return _savePath;
                _savePath = Path.Combine(Application.persistentDataPath, "Save");
                if (!Directory.Exists(_savePath))
                {
                    Directory.CreateDirectory(_savePath);
                }

                return _savePath;
            }
        }

        private static string _savePath;

        /// <summary>
        /// 存储文件扩展名
        /// </summary>
        public static string SaveFileExt => ".sav";

        /// <summary>
        /// 加密存储
        /// </summary>
        public static cBool EncryptSave { get; set; }

        /// <summary>
        /// 存储实现实例
        /// </summary>
        private static ISaveData SaveImpl => _saveImpl ?? (_saveImpl = CreateSaveDataInstance(_type, EncryptSave));

        private static ISaveData _saveImpl;

        #endregion

        #region Construct

        static SaveData()
        {
            DefaultModuleName = Application.productName;
            var savePath = SavePath;
        }

        #endregion

        #region Create Instance

        /// <summary>
        /// 创建指定类型的存档实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="encrypt">是否加密</param>
        /// <returns>实例</returns>
        public static ISaveData CreateSaveDataInstance(SaveType type, bool encrypt = false)
        {
            switch (type)
            {
                case SaveType.PlayerPrefs:
                    return new SaveDataPlayerPrefs(encrypt);
                case SaveType.Json:
                    return new SaveDataJson(encrypt);
#if SAVEDATA_XML
                case SaveType.Xml:
                    return new SaveDataXml();
#endif
                case SaveType.Serialization:
                    return new SaveDataSerialization(encrypt);
            }

            return null;
        }

        #endregion

        #region Quick API - Set / Get

        public static void Set<T>(string key, T value)
        {
            SaveImpl.Set(key, value);
        }

        public static void Set<T>(string module, string key, T value)
        {
            SaveImpl.Set(module, key, value);
        }

        public static T Get<T>(string key, T defaultValue = default(T))
        {
            return SaveImpl.Get(key, defaultValue);
        }

        public static T Get<T>(string module, string key, T defaultValue = default(T))
        {
            return SaveImpl.Get(module, key, defaultValue);
        }

        #endregion

        #region Impl API - Set Value / Set Object / Get Value / Get Object / Load / Delete / Clear / Save / Release

        #region Set Value / Object

        public static void SetValue<T>(string module, string key, T value)
        {
            SaveImpl.SetValue(module, key, value);
        }

        public static void SetValue<T>(string key, T value)
        {
            SaveImpl.SetValue(key, value);
        }

        public static void SetObject<T>(string module, string key, T obj)
        {
            SaveImpl.SetObject(module, key, obj);
        }

        public static void SetObject<T>(string key, T obj)
        {
            SaveImpl.SetObject(key, obj);
        }

        #endregion

        #region Get Value / Object

        public static T GetValue<T>(string module, string key, T defaultValue = default(T))
        {
            return SaveImpl.GetValue(module, key, defaultValue);
        }

        public static T GetValue<T>(string key, T defaultValue = default(T))
        {
            return SaveImpl.GetValue(key, defaultValue);
        }

        public static T GetObject<T>(string module, string key, T defaultObj = default(T))
        {
            return SaveImpl.GetObject(module, key, defaultObj);
        }

        public static T GetObject<T>(string key, T defaultObj = default(T))
        {
            return SaveImpl.GetObject(key, defaultObj);
        }

        #endregion

        #region Load

        public static void Load(string module)
        {
            SaveImpl.Load(module);
        }

        public static void Load()
        {
            SaveImpl.Load();
        }

        #endregion

        #region Load Async

        public static void LoadAsync(string module, Action onDone)
        {
            SaveImpl.LoadAsync(module, onDone);
        }

        public static void LoadAsync(Action onDone)
        {
            SaveImpl.LoadAsync(onDone);
        }

        #endregion

        #region Delete

        public static void Delete(string module, string key)
        {
            SaveImpl.Delete(module, key);
        }

        public static void Delete(string key)
        {
            SaveImpl.Delete(key);
        }

        #endregion

        #region Save

        public static void Save(string module)
        {
            SaveImpl.Save(module);
        }

        public static void Save()
        {
            SaveImpl.Save();
        }

        #endregion

        #region Save Async

        public static void SaveAsync(string module, Action onDone)
        {
            SaveImpl.SaveAsync(module, onDone);
        }

        public static void SaveAsync(Action onDone)
        {
            SaveImpl.SaveAsync(onDone);
        }

        #endregion

        #region Clear

        public static void Clear(string module)
        {
            SaveImpl.Clear(module);
        }

        public static void Clear()
        {
            SaveImpl.Clear();
        }

        #endregion

        #region Release

        public static void Release()
        {
            SaveImpl.Release();
        }

        public static void Release(string module)
        {
            SaveImpl.Release(module);
        }

        #endregion

        #endregion
    }
}