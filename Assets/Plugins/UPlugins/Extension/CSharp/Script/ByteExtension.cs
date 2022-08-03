/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ByteExtension.cs
//  Info     : Byte 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Aya.Extension
{
    public static class ByteExtension
    {
        #region IndexOf

        /// <summary>
        /// 一个字节数组在当前字节数组中首次出现的位置
        /// </summary>
        /// <param name="bytes">当前字节数组</param>
        /// <param name="other">另一个字节数组</param>
        /// <returns>索引</returns>
        public static int IndexOf(this byte[] bytes, byte[] other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (other.Length == 0)
            {
                return 0;
            }

            var j = -1;
            var end = bytes.Length - other.Length;
            while ((j = Array.IndexOf(bytes, other[0], j + 1)) <= end && j != -1)
            {
                var i = 1;
                while (bytes[j + i] == other[i])
                {
                    if (++i == other.Length)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }

        #endregion

        #region Hex

        /// <summary>
        /// 转为16进制
        /// </summary>
        /// <param name="b">字节</param>
        /// <returns>结果</returns>
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary>
        /// 转为16进制
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>结果</returns>
        public static string ToHex(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 转为16进制(带空格)
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>结果</returns>
        public static string ToHexWithSpace(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
                sb.Append(" ");
            }

            return sb.ToString();
        }

        #endregion

        #region Base64

        /// <summary>
        /// 转换为Base64字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>结果</returns>
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        #endregion

        #region Convert To int,long,float,double,bool,string,char

        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns>结果</returns>
        public static int ToInt(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt32(bytes, startIndex);
        }

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns>结果</returns>
        public static long ToLong(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToInt64(bytes, startIndex);
        }

        /// <summary>
        /// 转换为char
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns>结果</returns>
        public static char ToChar(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToChar(bytes, startIndex);
        }

        /// <summary>
        /// 转换为float
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns>结果</returns>
        public static float ToFloat(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToSingle(bytes, startIndex);
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns>结果</returns>
        public static double ToDouble(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToDouble(bytes, startIndex);
        }

        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns>结果</returns>
        public static bool ToBoolean(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToBoolean(bytes, startIndex);
        }

        /// <summary>
        /// 转换为string
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns>结果</returns>
        public static string ToString(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToString(bytes, startIndex);
        }

        /// <summary>
        /// 转换为指定类型 T
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns>结果</returns>
        public static T ToType<T>(this byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                var bf = new BinaryFormatter();
                ms.Position = 0;
                var x = bf.Deserialize(ms);
                return (T)x;
            }
        }

        #endregion

        #region Encoding

        /// <summary>
        /// 转换为指定编码的字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encoding">编码</param>
        /// <returns>结果</returns>
        public static string Decode(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        #endregion

        #region Hash

        /// <summary>
        /// 使用指定算法计算Hash值
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="hashName">hash算法名</param>
        /// <returns>结果</returns>
        public static byte[] Hash(this byte[] bytes, string hashName = "ModSystem.Security.Cryptography.HashAlgorithm")
        {
            var algorithm = string.IsNullOrEmpty(hashName) ? HashAlgorithm.Create() : HashAlgorithm.Create(hashName);
            return algorithm?.ComputeHash(bytes);
        }

        #endregion

        #region Bitwise

        /// <summary>
        /// 第index是否为1
        /// </summary>
        /// <param name="b">字节</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }

        /// <summary>
        /// 将第index设为1
        /// </summary>
        /// <param name="b">字节</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte) (1 << index);
            return b;
        }

        /// <summary>
        /// 将第index设为0
        /// </summary>
        /// <param name="b">字节</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static byte ClearBit(this byte b, int index)
        {
            b &= (byte) ((1 << 8) - 1 - (1 << index));
            return b;
        }

        /// <summary>
        /// 将第index位取反
        /// </summary>
        /// <param name="b">字节</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte) (1 << index);
            return b;
        }

        #endregion

        #region File

        /// <summary>
        /// 保存为文件
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="path">路径</param>
        ///  <returns>bytes</returns>
        public static byte[] Save(this byte[] bytes, string path)
        {
            File.WriteAllBytes(path, bytes);
            return bytes;
        }

        #endregion

        #region MemoryStream

        /// <summary>
        /// 转换为内存流
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>结果</returns>
        public static MemoryStream ToMemoryStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        #endregion
    }
}