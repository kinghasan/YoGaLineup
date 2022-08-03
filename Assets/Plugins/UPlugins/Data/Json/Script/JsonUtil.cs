/////////////////////////////////////////////////////////////////////////////
//
//  Script : JsonUtil.cs
//  Info   : Json 辅助操作类
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;

namespace Aya.Data.Json
{
    public static class JsonUtil
    {
        #region Json <-> Object

        /// <summary>
        /// 转换为对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <param name="defaultObj">默认值</param>
        /// <returns>结果</returns>
        public static T ToObject<T>(string jsonString, T defaultObj = default(T))
        {
            try
            {
                var obj = JsonMapper.ToObject<T>(jsonString);
                return obj;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
                return defaultObj;
            }
        }

        /// <summary>
        /// 转换为对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="jsonString">json字符串</param>
        /// <param name="defaultObj">默认值</param>
        /// <returns>结果</returns>
        public static object ToObject(Type type, string jsonString, object defaultObj = null)
        {
            try
            {
                var obj = JsonMapper.ToObject(type, jsonString);
                return obj;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
                return defaultObj;
            }
        }

        /// <summary>
        /// 转换为json字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="format">格式化</param>
        /// <param name="defaultJson">默认结果</param>
        /// <returns>结果</returns>
        public static string ToJson<T>(T obj, bool format = false, string defaultJson = default(string))
        {
            try
            {
                var jsonString = JsonMapper.ToJson(obj);
                if (format)
                {
                    jsonString = Format(jsonString);
                }

                return jsonString;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
                return defaultJson;
            }
        }

        #endregion

        #region Fomart

        /// <summary>
        /// 格式化json字符串（除了值内容外，没有多余空格和换行）
        /// </summary>
        /// <param name="jsonString">原json字符串</param>
        /// <returns>结果</returns>
        public static string Format(string jsonString)
        {
            var builder = new StringBuilder(jsonString);
            var index = 0;
            var quote = 0;
            var tab = "";
            while (index < builder.Length)
            {
                if (builder[index] == '\"')
                {
                    if (!(index > 0 && builder[index - 1] == '\\'))
                    {
                        quote++;
                    }
                }

                if (builder[index] == '\"' && quote % 2 == 1)
                {
                    // {"
                    if (index > 0 && builder[index - 1] == '{')
                    {
                        tab += "\t";
                        builder.Insert(index, "\n" + tab);
                        index += 1 + tab.Length;
                    }

                    // ,"
                    if (index > 0 && builder[index - 1] == ',')
                    {
                        builder.Insert(index, "\n" + tab);
                        index += 1 + tab.Length;
                    }
                }
                else if (builder[index] == '}' && quote % 2 == 0)
                {
                    if (tab.Length > 0)
                    {
                        tab = tab.Substring(0, tab.Length - 1);
                        builder.Insert(index, "\n" + tab);
                        index += 1 + tab.Length;
                    }

                }

                index++;
            }

            return builder.ToString();
        }

        /// <summary>
        /// 将json合并成一行，去除除了值内容以外的空格和换行
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static string MergeOneLine(string jsonString)
        {
            var builder = new StringBuilder(jsonString);
            var index = 0;
            var quote = 0;
            while (index < builder.Length)
            {
                if (builder[index] == '\"')
                {
                    if (!(index > 0 && builder[index - 1] == '\\'))
                    {
                        quote++;
                    }
                }

                if (quote % 2 == 0)
                {
                    if (builder[index] == '\n'
                        || builder[index] == ' '
                        || builder[index] == '\t'
                        || builder[index] == '\r')
                    {
                        builder.Remove(index, 1);
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                {
                    index++;
                }
            }

            return builder.ToString();
        }

        #endregion
    }
}