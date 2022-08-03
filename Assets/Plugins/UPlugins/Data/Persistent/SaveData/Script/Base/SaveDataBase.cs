/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveDataBase.cs
//  Info     : 存档管理基类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;

namespace Aya.Data.Persistent
{
    public abstract class SaveDataBase
    {
        /// <summary>
        /// 值类型定义
        /// </summary>
        public static readonly List<Type> ValueTypeDefine = new List<Type>()
        {
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(string),
            typeof(byte),
            typeof(Enum)
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="encrypt">是否加密</param>
        protected SaveDataBase(bool encrypt = false)
        {
            SaveData.EncryptSave = encrypt;
        }

        /// <summary>
        /// 获取存储路径
        /// </summary>
        /// <param name="module">模块名</param>
        /// <returns>路径</returns>
        protected string GetSavePath(string module)
        {
            var fileName = (SaveData.EncryptSave ? SaveDataInterface.GetMd5(module) : module) + SaveData.SaveFileExt;
            var path = Path.Combine(SaveData.SavePath, fileName);
            return path;
        }

        /// <summary>
        /// 是否作为值类型存储
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public bool IsValueType(Type type)
        {
            return ValueTypeDefine.Contains(type);
        }
    }
}
