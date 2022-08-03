/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveDataXml.cs
//  Info     : 存档管理 —— XML 实现
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Info     : 1.由于 Xml 不支持某些类的序列化(比如 dic)，所以序列化部分仍使用json格式
//             2.Sytem.Xml 会增加包体积，慎重使用，如确定要使用则打开 SAVEDATA_XML 宏
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
// #define SAVEDATA_XML
#if SAVEDATA_XML
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aya.Data.Json;
using Aya.Data.Persistent;
using Aya.Extension;

namespace Aya.Data.Save
{
    internal class SaveDataXml : SaveDataBase, ISaveData
    {
#region Private Xml

        protected Dictionary<string, XmlDocument> ModDic = new Dictionary<string, XmlDocument>();

        private XmlDocument GetTextAsset(string module)
        {
            var path = GetSavePath(module);
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                var ret = SaveData.EncryptSave ? SaveDataInterface.Decrypt(text) : text;
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(ret);
                return xmlDoc;
            }

            return null;
        }

        private XmlDocument GetXml(string module)
        {
            var xml = ModDic.GetValue(module);
            if (xml != null)
            {
                return xml;
            }

            var xmlDoc = GetTextAsset(module);
            if (xmlDoc == null)
            {
                xmlDoc = new XmlDocument();
                var xmlRoot = xmlDoc.CreateElement(module);
                xmlDoc.AppendChild(xmlRoot);
                ModDic.Add(module, xmlDoc);
            }

            return xmlDoc;
        }

        private void SetXml<T>(string module, string key, T value)
        {
            var xmlDoc = GetXml(module);
            var xmlRoot = xmlDoc.DocumentElement;
            var xmlElement = xmlRoot.SelectSingleNode(key);
            if (xmlElement == null)
            {
                xmlElement = xmlDoc.CreateElement(key);
                xmlRoot.AppendChild(xmlElement);
            }

            xmlElement.InnerText = value.CastType<string>();
            ModDic[module] = xmlDoc;
        }

#endregion

        public SaveDataXml(bool encrypt = false) : base(encrypt)
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
            SetXml(module, key, value);
        }

        public void SetValue<T>(string key, T value)
        {
            SetXml(SaveData.DefaultModuleName, key, value);
        }

        public void SetObject<T>(string module, string key, T obj)
        {
            var value = JsonUtil.ToJson(obj);
            SetXml(module, key, value);
        }

        public void SetObject<T>(string key, T obj)
        {
            SetObject(SaveData.DefaultModuleName, key, obj);
        }

#endregion

#region Get Value / Object

        public T GetValue<T>(string module, string key, T defaultValue = default(T))
        {
            var xmlDoc = GetXml(module);
            var xmlRoot = xmlDoc.DocumentElement;
            var xmlElement = xmlRoot.SelectSingleNode(key);
            if (xmlElement == null)
            {
                xmlElement = xmlDoc.CreateElement(key);
                xmlRoot.AppendChild(xmlElement);
            }

            var value = xmlElement.InnerText;
            if (!string.IsNullOrEmpty(value))
            {
                var ret = xmlElement.InnerText.CastType<T>();
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

        public T GetObject<T>(string module, string key, T defaultObj = default(T))
        {
            var xmlDoc = GetXml(module);
            var xmlRoot = xmlDoc.DocumentElement;
            var xmlElement = xmlRoot.SelectSingleNode(key);
            if (xmlElement != null)
            {
                var value = xmlElement.InnerText;
                if (!string.IsNullOrEmpty(value))
                {
                    var ret = JsonUtil.ToObject<T>(value);
                    return ret;
                }
                else
                {
                    return defaultObj;
                }
            }
            else
            {
                return defaultObj;
            }
        }

        public T GetObject<T>(string key, T defaultObj = default(T))
        {
            return GetObject(SaveData.DefaultModuleName, key, defaultObj);
        }

#endregion

#region Load

        public void Load(string module)
        {
            GetXml(module);
        }

        public void Load()
        {
            GetXml(SaveData.DefaultModuleName);
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

        public void Delete(string moudule, string key)
        {
            var xmlDoc = GetXml(moudule);
            var xmlRoot = xmlDoc.DocumentElement;
            var xmlElement = xmlRoot.SelectSingleNode(key);
            if (xmlElement != null)
            {
                xmlRoot.RemoveChild(xmlElement);
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
            var xmlDoc = GetXml(module);
            var text = FormatXml(xmlDoc.InnerXml);
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

#region Private

        private string FormatXml(string sUnformattedXml)
        {
            var xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                };
                xd.WriteTo(xtw);
            }
            finally
            {
                xtw?.Close();
            }

            return sb.ToString();
        }

#endregion
    }
}
#endif
