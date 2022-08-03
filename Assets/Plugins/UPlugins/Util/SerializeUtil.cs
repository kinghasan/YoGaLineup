/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SerializeUtil.cs
//  Info     : 序列化操作辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aya.Util
{
    public static class SerializeUtil
    {
        /// <summary>
        /// 二进制序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="filePath">存储路径</param>
        /// <param name="obj">序列化对象</param>
        public static void SerializaBinary<T>(string filePath, T obj) where T : class
        {
            var stream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, obj);
            stream.Close();
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="filePath">读取路径</param>
        /// <returns>反序列化结果</returns>
        public static T DeserializenBinary<T>(string filePath) where T : class
        {
            var stream = File.Open(filePath, FileMode.Open);
            var binaryFormatter = new BinaryFormatter();
            var obj = binaryFormatter.Deserialize(stream);
            stream.Close();
            return obj as T;
        }
    }
}
