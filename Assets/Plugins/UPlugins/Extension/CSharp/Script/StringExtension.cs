/////////////////////////////////////////////////////////////////////////////
//
//  Script   : StringExtension.cs
//  Info     : String 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Aya.Extension
{
    public static class StringExtension
    {
        private static readonly StringBuilder StringBuilder = new StringBuilder();

        #region Null / Empty

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            var result = string.IsNullOrEmpty(value);
            return result;
        }

        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            var result = !string.IsNullOrEmpty(value);
            return result;
        }

        /// <summary>
        /// 是否为空或者全部是空白字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsEmptyOrWhiteSpace(this string value)
        {
            if (value.IsEmpty()) return true;
            for (var i = 0; i < value.Length; i++)
            {
                var c = value[i];
                if (!char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 是否非空或者是空白字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNotEmptyOrWhiteSpace(this string value)
        {
            var result = value.IsEmptyOrWhiteSpace() == false;
            return result;
        }

        /// <summary>
        /// 是否为空或者空白字符，如果是则替换为指定默认字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>结果</returns>
        public static string IfEmptyOrWhiteSpace(this string value, string defaultValue)
        {
            var result = value.IsEmptyOrWhiteSpace() ? defaultValue : value;
            return result;
        }

        #endregion

        #region Convert To short,int,long,float,double

        /// <summary>
        /// 是否为 short
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsShort(this string value)
        {
            var result = short.TryParse(value, out _);
            return result;
        }

        /// <summary>
        /// 转换为 short
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static short AsShort(this string value)
        {
            var result = short.Parse(value);
            return result;
        }

        /// <summary>
        /// 是否为 int
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsInt(this string value)
        {
            var result = int.TryParse(value, out _);
            return result;
        }

        /// <summary>
        /// 转换为 int
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static int AsInt(this string value)
        {
            var result = int.Parse(value);
            return result;
        }

        /// <summary>
        /// 是否为 long
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsLong(this string value)
        {
            var result = long.TryParse(value, out _);
            return result;
        }

        /// <summary>
        /// 转换为 long
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static long AsLong(this string value)
        {
            var result = long.Parse(value);
            return result;
        }

        /// <summary>
        /// 是否为 float
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsFloat(this string value)
        {
            var result = float.TryParse(value, out _);
            return result;
        }

        /// <summary>
        /// 转换为 float
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static float AsFloat(this string value)
        {
            var result = float.Parse(value);
            return result;
        }

        /// <summary>
        /// 是否为 double
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsDouble(this string value)
        {
            var result = double.TryParse(value, out _);
            return result;
        }

        /// <summary>
        /// 转换为 double
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static double AsDouble(this string value)
        {
            var result = double.Parse(value);
            return result;
        }

        /// <summary>
        /// 是否为 decimal
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsDecimal(this string value)
        {
            var result = decimal.TryParse(value, out _);
            return result;
        }

        /// <summary>
        /// 转换为 decimal
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static decimal AsDecimal(this string value)
        {
            var result = decimal.Parse(value);
            return result;
        }

        #endregion

        #region Trim

        /// <summary>
        /// Trim 并保留到最大长度
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>结果</returns>
        public static string TrimToMaxLength(this string value, int maxLength)
        {
            var result = value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength);
            return result;
        }

        /// <summary>
        /// Trim 并保留到最大长度并添加后缀
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">后缀</param>
        /// <returns>结果</returns>
        public static string TrimToMaxLength(this string value, int maxLength, string suffix)
        {
            var result = value == null || value.Length <= maxLength ? value : string.Concat(value.Substring(0, maxLength), suffix);
            return result;
        }

        #endregion

        #region Contains

        /// <summary>
        /// 是否包含字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="comparisonValue">比较值</param>
        /// <param name="comparisonType">比较方式</param>
        /// <returns>结果</returns>
        public static bool Contains(this string value, string comparisonValue, StringComparison comparisonType)
        {
            var result = (value.IndexOf(comparisonValue, comparisonType) != -1);
            return result;
        }

        /// <summary>
        /// 包含任意一个
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="values">比较值</param>
        /// <returns>结果</returns>
        public static bool ContainsAny(this string value, params string[] values)
        {
            var result = value.ContainsAny(StringComparison.CurrentCulture, values);
            return result;
        }

        /// <summary>
        /// 包含任意一个
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="comparisonType">比较类型</param>
        /// <param name="values">比较值</param>
        /// <returns>结果</returns>
        public static bool ContainsAny(this string value, StringComparison comparisonType, params string[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                var str = values[i];
                if (Contains(value, str, comparisonType)) return true;
            }

            return false;
        }

        /// <summary>
        /// 包含所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="values">比较值</param>
        /// <returns>结果</returns>
        public static bool ContainsAll(this string value, params string[] values)
        {
            var result = value.ContainsAll(StringComparison.CurrentCulture, values);
            return result;
        }

        /// <summary>
        /// 包含所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="comparisonType">比较类型</param>
        /// <param name="values">比较值</param>
        /// <returns>结果</returns>
        public static bool ContainsAll(this string value, StringComparison comparisonType, params string[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                var str = values[i];
                if (Contains(value, str, comparisonType)) return true;
            }

            return false;
        }

        /// <summary>
        /// 包含任意一个
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="comparisonType">比较类型</param>
        /// <param name="values">比较值</param>
        /// <returns>结果</returns>
        public static bool EqualsAny(this string value, StringComparison comparisonType, params string[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                var str = values[i];
                if (value.Equals(str, comparisonType)) return true;
            }

            return false;
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取指定字符串千面的部分
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="x">字符串X</param>
        /// <returns>结果</returns>
        public static string Before(this string value, string x)
        {
            var xPos = value.IndexOf(x, StringComparison.Ordinal);
            var result = xPos == -1 ? string.Empty : value.Substring(0, xPos);
            return result;
        }

        /// <summary>
        /// 获取指定字符串中间的部分
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="x">字符串X</param>
        /// <param name="y">字符串Y</param>
        /// <returns>结果</returns>
        public static string Between(this string value, string x, string y)
        {
            var xPos = value.IndexOf(x, StringComparison.Ordinal);
            var yPos = value.LastIndexOf(y, StringComparison.Ordinal);

            if (xPos == -1 || xPos == -1)
            {
                return string.Empty;
            }

            var startIndex = xPos + x.Length;
            var result = startIndex >= yPos ? string.Empty : value.Substring(startIndex, yPos - startIndex).Trim();
            return result;
        }

        /// <summary>
        /// 获取指定字符串后面的部分
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="x">字符串X</param>
        /// <returns>结果</returns>
        public static string After(this string value, string x)
        {
            var xPos = value.LastIndexOf(x, StringComparison.Ordinal);

            if (xPos == -1)
            {
                return string.Empty;
            }

            var startIndex = xPos + x.Length;
            var result = startIndex >= value.Length ? string.Empty : value.Substring(startIndex).Trim();
            return result;
        }

        /// <summary>
        /// 获取字符串左边指定长度的部分
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="characterCount">长度</param>
        /// <returns>结果</returns>
        public static string Left(this string value, int characterCount)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (characterCount >= value.Length)
                throw new ArgumentOutOfRangeException(nameof(characterCount), characterCount, "characterCount must be less than length of string");
            var result = value.Substring(0, characterCount);
            return result;
        }

        /// <summary>
        /// 获取字符串右边指定长度的部分
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="characterCount">长度</param>
        /// <returns>结果</returns>
        public static string Right(this string value, int characterCount)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (characterCount >= value.Length)
                throw new ArgumentOutOfRangeException(nameof(characterCount), characterCount, "characterCount must be less than length of string");
            var result = value.Substring(value.Length - characterCount);
            return result;
        }

        /// <summary>
        /// 从指定位置开始截取字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static string Substring(this string value, int index)
        {
            var result = index < 0 ? value : value.Substring(index, value.Length - index);
            return result;
        }

        #endregion

        #region Join

        /// <summary>
        /// System.Join 泛型版本，把字符串本身当作分隔符串联一组对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="separator">分隔符</param>
        /// <param name="values">数组</param>
        /// <returns>结果</returns>
        public static string Join<T>(this string separator, T[] values)
        {
            if (values == null || values.Length == 0)
            {
                return string.Empty;
            }

            if (separator == null)
            {
                separator = string.Empty;
            }

            string Converter(T o) => o.ToString();
            var result = string.Join(separator, Array.ConvertAll(values, (Converter<T, string>) Converter));
            return result;
        }

        #endregion

        #region Remove

        /// <summary>
        /// 移除指定字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="chars">字符</param>
        /// <returns>结果</returns>
        public static string Remove(this string value, params char[] chars)
        {
            var result = value;
            if (!string.IsNullOrEmpty(result) && chars != null)
            {
                Array.ForEach(chars, c => result = result.Remove(c.ToString()));
            }

            return result;

        }

        /// <summary>
        /// 移除指定字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="strings">字符串</param>
        /// <returns>结果</returns>
        public static string Remove(this string value, params string[] strings)
        {
            for (var i = 0; i < strings.Length; i++)
            {
                var str = strings[i];
                value = value.Replace(str, string.Empty);
            }

            return value;
        }

        #endregion

        #region Pad

        /// <summary>
        /// 两边补足
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="width">宽度</param>
        /// <param name="padChar">补充字符</param>
        /// <param name="truncate">截短</param>
        /// <returns>结果</returns>
        public static string PadBoth(this string value, int width, char padChar, bool truncate = false)
        {
            var diff = width - value.Length;
            if (diff == 0 || diff < 0 && !(truncate))
            {
                return value;
            }
            else if (diff < 0)
            {
                return value.Substring(0, width);
            }
            else
            {
                return value.PadLeft(width - diff / 2, padChar).PadRight(width, padChar);
            }
        }

        #endregion

        #region Start / End With

        /// <summary>
        /// 确保以指定前缀开始
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns>结果</returns>
        public static string EnsureStartsWith(this string value, string prefix)
        {
            var result = value.StartsWith(prefix) ? value : string.Concat(prefix, value);
            return result;
        }

        /// <summary>
        /// 确保以指定后缀结束
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="suffix">后缀</param>
        /// <returns>结果</returns>
        public static string EnsureEndsWith(this string value, string suffix)
        {
            var result = value.EndsWith(suffix) ? value : string.Concat(value, suffix);
            return result;
        }

        #endregion

        #region Contact

        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="values">待拼接字符串</param>
        /// <returns>结果</returns>
        public static string ConcatWith(this string value, params string[] values)
        {
            var result = string.Concat(value, string.Concat(values));
            return result;
        }

        #endregion

        #region Repeat

        /// <summary>
        /// 重复指定次数
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="repeatCount">重复次数</param>
        /// <returns>结果</returns>
        public static string Repeat(this string value, int repeatCount)
        {
            if (value.Length == 1)
            {
                return new string(value[0], repeatCount);
            }

            var stringBuilder = new StringBuilder(repeatCount * value.Length);
            while (repeatCount-- > 0)
            {
                stringBuilder.Append(value);
            }

            var result = stringBuilder.ToString();
            return result;
        }

        #endregion

        #region Replace

        /// <summary>
        /// 替换所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="oldValues">待替换值</param>
        /// <param name="replacePredicate">替换方法</param>
        /// <returns>结果</returns>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, Func<string, string> replacePredicate)
        {
            var stringBuilder = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                var newValue = replacePredicate(oldValue);
                stringBuilder.Replace(oldValue, newValue);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 替换所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="oldValues">待替换值</param>
        /// <param name="newValue">替换值</param>
        /// <returns>结果</returns>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, string newValue)
        {
            var stringBuilder = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                stringBuilder.Replace(oldValue, newValue);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 替换所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="oldValues">待替换值</param>
        /// <param name="newValues">替换值</param>
        /// <returns>结果</returns>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, IEnumerable<string> newValues)
        {
            var stringBuilder = new StringBuilder(value);
            var newValueEnum = newValues.GetEnumerator();
            foreach (var old in oldValues)
            {
                if (!newValueEnum.MoveNext()) throw new ArgumentOutOfRangeException(nameof(newValues), "newValues sequence is shorter than oldValues sequence");
                stringBuilder.Replace(old, newValueEnum.Current ?? throw new InvalidOperationException());
            }

            if (newValueEnum.MoveNext()) throw new ArgumentOutOfRangeException(nameof(newValues), "newValues sequence is longer than oldValues sequence");
            newValueEnum.Dispose();
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 使用正则表达式替换
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">匹配表达式</param>
        /// <param name="replaceValue">替换值</param>
        /// <returns>结果</returns>
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue)
        {
            var result = ReplaceWith(value, regexPattern, replaceValue, RegexOptions.None);
            return result;
        }

        /// <summary>
        /// 使用正则表达式替换
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">匹配表达式</param>
        /// <param name="replaceValue">替换值</param>
        /// <param name="options">匹配选项</param>
        /// <returns>结果</returns>
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue, RegexOptions options)
        {
            var result = Regex.Replace(value, regexPattern, replaceValue, options);
            return result;
        }

        #endregion

        #region Spilt

        /// <summary>
        /// 使用正则表达式分割
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">正则表达式</param>
        /// <returns>结果</returns>
        public static string[] SplitWith(this string value, string regexPattern)
        {
            var result = value.SplitWith(regexPattern, RegexOptions.None);
            return result;
        }

        /// <summary>
        /// 使用正则表达式分割
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">正则表达式</param>
        /// <param name="options">匹配选项</param>
        /// <returns>结果</returns>
        public static string[] SplitWith(this string value, string regexPattern, RegexOptions options)
        {
            var result = Regex.Split(value, regexPattern, options);
            return result;
        }

        /// <summary>
        /// 获取所有单词
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string[] GetWords(this string value)
        {
            var result = value.SplitWith(@"\W");
            return result;
        }

        /// <summary>
        /// 获取指定索引的单词
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static string GetWordByIndex(this string value, int index)
        {
            var words = value.GetWords();
            if ((index < 0) || (index > words.Length - 1)) throw new IndexOutOfRangeException("The word number is out of range.");
            var result = words[index];
            return result;
        }

        #endregion

        #region In

        /// <summary>
        /// 是否在数组中
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="strings">字符串数组</param>
        /// <returns>结果</returns>
        public static bool In(this string value, params string[] strings)
        {
            var result = Array.IndexOf(strings, value) > -1;
            return result;
        }

        #endregion

        #region Line

        /// <summary>
        /// 获取字符串的行数
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static int GetLines(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            var splits = value.Split('\r');
            var result = splits.Length;
            return result;
        }

        /// <summary>
        /// 获取字符串从第a行到最后一行的内容
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="startLine">开始行数</param>
        /// <param name="endStr">字符串换行符</param>
        /// <returns>结果</returns>
        public static string SubStringWithLine(this string value, int startLine, string endStr = "\n")
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NullReferenceException();
            }

            var splits = value.Split(new[] {endStr}, StringSplitOptions.None);
            var lineCount = splits.Length;
            var lines = lineCount - startLine;
            if (startLine < 0 || lines + startLine > lineCount)
            {
                throw new IndexOutOfRangeException(
                    "startLine = " + startLine +
                    ",\tlines = " + lines +
                    ",\t Line Count : " + lineCount + ".");
            }

            var startIndex = 0;
            var length = 0;
            var index = 0;
            for (; index < startLine; index++)
            {
                startIndex += splits[index].Length + endStr.Length;
            }

            for (; index < startLine + lines; index++)
            {
                length += splits[index].Length + endStr.Length;
            }

            length--;
            var result = value.Substring(startIndex, length);
            return result;
        }

        /// <summary>
        /// 获取字符串从第a行到最后一行的内容
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="startLine">开始行数</param>
        /// <param name="lines">行数</param>
        /// <param name="endStr">字符串换行符</param>
        /// <returns>结果</returns>
        public static string SubStringWithLine(this string value, int startLine, int lines, string endStr = "\n")
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new NullReferenceException();
            }

            var splits = value.Split(new[] {endStr}, StringSplitOptions.None);
            var lineCount = splits.Length;
            if (startLine < 0 || lines + startLine > lineCount)
            {
                throw new IndexOutOfRangeException(
                    "startLine = " + startLine +
                    ",\tlines = " + lines +
                    ",\t Line Count : " + lineCount + ".");
            }

            var startIndex = 0;
            var length = 0;
            var index = 0;
            for (; index < startLine; index++)
            {
                startIndex += splits[index].Length + endStr.Length;
            }

            for (; index < startLine + lines; index++)
            {
                length += splits[index].Length + endStr.Length;
            }

            length--;
            var result = value.Substring(startIndex, length);
            return result;
        }

        #endregion

        #region Formart

        /// <summary>
        /// 格式化<para/>
        /// 如需要高效执行，请使用具体参数的重载。
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="args">参数</param>
        /// <returns>结果</returns>
        public static string Format(this string value, params object[] args)
        {
            var result = string.Format(value, args);
            return result;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="arg0">参数0</param>
        /// <returns>结果</returns>
        public static string Format(this string value, object arg0)
        {
            var result = string.Format(value, arg0);
            return result;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="arg0">参数0</param>
        /// <param name="arg1">参数1</param>
        /// <returns>结果</returns>
        public static string Format(this string value, object arg0, object arg1)
        {
            var result = string.Format(value, arg0, arg1);
            return result;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="arg0">参数0</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        /// <returns>结果</returns>
        public static string Format(this string value, object arg0, object arg1, object arg2)
        {
            var result = string.Format(value, arg0, arg1, arg2);
            return result;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="provider">格式</param>
        /// <param name="args">参数</param>
        /// <returns>结果</returns>
        public static string Format(this string value, IFormatProvider provider, params object[] args)
        {
            var result = string.Format(provider, value, args);
            return result;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string ToUpperFirstLetter(this string value)
        {
            if (value.IsEmptyOrWhiteSpace()) return string.Empty;
            var valueChars = value.ToCharArray();
            valueChars[0] = char.ToUpper(valueChars[0]);
            var result = new string(valueChars);
            return result;
        }

        /// <summary>
        /// 标题样式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string ToTitleCase(this string value)
        {
            var result = ToTitleCase(value, CultureInfo.CurrentCulture);
            return result;
        }

        /// <summary>
        /// 标题样式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="cultureInfo">区域信息</param>
        /// <returns>结果</returns>
        public static string ToTitleCase(this string value, CultureInfo cultureInfo)
        {
            var result = cultureInfo.TextInfo.ToTitleCase(value);
            return result;
        }

        /// <summary>
        /// 单词转换为复数形式
        /// </summary>
        /// <param name="singular">单数形式单词</param>
        /// <returns>结果</returns>
        public static string ToPlural(this string singular)
        {
            var index = singular.LastIndexOf(" of ", StringComparison.Ordinal);
            if (index > 0) return (singular.Substring(0, index)) + singular.Remove(0, index).ToPlural();
            if (singular.EndsWith("sh")) return singular + "es";
            if (singular.EndsWith("ch")) return singular + "es";
            if (singular.EndsWith("us")) return singular + "es";
            if (singular.EndsWith("ss")) return singular + "es";
            if (singular.EndsWith("y")) return singular.Remove(singular.Length - 1, 1) + "ies";
            if (singular.EndsWith("o")) return singular.Remove(singular.Length - 1, 1) + "oes";
            return singular + "s";
        }

        /// <summary>
        /// 在所有大写字母前添加空格
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string SpaceOnUpper(this string value)
        {
            var result = Regex.Replace(value, "([A-Z])(?=[a-z])|(?<=[a-z])([A-Z]|[0-9]+)", " $1$2").TrimStart();
            return result;
        }

        #endregion

        #region Regex

        /// <summary>
        /// 正则 - 是否匹配
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>结果</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            var result = value != null && Regex.IsMatch(value, pattern);
            return result;
        }

        /// <summary>
        /// 正则 - 是否匹配
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>结果</returns>
        public static bool IsMatch(this string value, Regex regex)
        {
            var result = value != null && regex.IsMatch(value);
            return result;
        }

        /// <summary>
        /// 正则 - 匹配结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>结果</returns>
        public static string Match(this string value, string pattern)
        {
            var result = value == null ? "" : Regex.Match(value, pattern).Value;
            return result;
        }

        /// <summary>
        /// 正则 - 匹配结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>结果</returns>
        public static string Match(this string value, Regex regex)
        {
            var result = value == null ? "" : regex.Match(value).Value;
            return result;
        }

        /// <summary>
        /// 正则 - 匹配所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">正则表达式</param>
        /// <returns>结果</returns>
        public static MatchCollection Matches(this string value, string regexPattern)
        {
            var result = Matches(value, regexPattern, RegexOptions.None);
            return result;
        }

        /// <summary>
        /// 正则 - 匹配所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">正则表达式</param>
        /// <param name="options">匹配选项</param>
        /// <returns>结果</returns>
        public static MatchCollection Matches(this string value, string regexPattern, RegexOptions options)
        {
            var result = Regex.Matches(value, regexPattern, options);
            return result;
        }

        /// <summary>
        /// 正则 - 匹配所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">正则表达式</param>
        /// <returns>结果</returns>
        public static IEnumerable<string> MatchValues(this string value, string regexPattern)
        {
            var result = MatchValues(value, regexPattern, RegexOptions.None);
            return result;
        }

        /// <summary>
        /// 正则 - 匹配所有
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regexPattern">正则表达式</param>
        /// <param name="options">匹配选项</param>
        /// <returns>结果</returns>
        public static IEnumerable<string> MatchValues(this string value, string regexPattern, RegexOptions options)
        {
            foreach (Match match in Matches(value, regexPattern, options))
            {
                if (match.Success) yield return match.Value;
            }
        }

        #endregion

        #region Like

        /// <summary>
        /// 是否像任意一个模式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="patterns">模式</param>
        /// <returns>结果</returns>
        public static bool IsLikeAny(this string value, params string[] patterns)
        {
            for (var i = 0; i < patterns.Length; i++)
            {
                var pattern = patterns[i];
                if (IsLike(value, pattern)) return true;
            }

            return false;
        }

        /// <summary>
        /// 是否像某个模式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="pattern">模式</param>
        /// <returns>结果</returns>
        public static bool IsLike(this string value, string pattern)
        {
            if (value == pattern) return true;
            if (pattern[0] == '*' && pattern.Length > 1)
            {
                for (var index = 0; index < value.Length; index++)
                {
                    if (value.Substring(index).IsLike(pattern.Substring(1)))
                    {
                        return true;
                    }
                }
            }
            else if (pattern[0] == '*')
            {
                return true;
            }
            else if (pattern[0] == value[0])
            {
                return value.Substring(1).IsLike(pattern.Substring(1));
            }

            return false;
        }

        #endregion

        #region Guid

        /// <summary>
        /// 转换为GUID
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static Guid ToGuid(this string value)
        {
            var result = new Guid(value);
            return result;
        }

        /// <summary>
        /// 转换为GUID（安全）
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static Guid ToGuidSafe(this string value)
        {
            var result = value.ToGuidSafe(Guid.Empty);
            return result;
        }

        /// <summary>
        /// 转换为GUID（安全）
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认GUID</param>
        /// <returns>结果</returns>
        public static Guid ToGuidSafe(this string value, Guid defaultValue)
        {
            if (value.IsEmpty())
            {
                return defaultValue;
            }

            try
            {
                return value.ToGuid();
            }
            catch
            {
                //
            }

            return defaultValue;
        }

        #endregion

        #region Stream

        /// <summary>
        /// 将指定字符串转为Stream流
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="encoding">使用的编码</param>
        /// <returns></returns>
        public static Stream ToStream(this string value, Encoding encoding = null)
        {
            var result = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(value));
            return result;
        }

        #endregion

        #region Check Type

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNumber(this string value)
        {
            var result = !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"\d+");
            return result;
        }

        /// <summary>
        /// 是否为中文
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsChinese(this string value)
        {
            var result = !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^[\u4e00-\u9fa5]$");
            return result;
        }

        #endregion

        #region Reverse

        /// <summary>
        /// 字符串逆序
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string Reverse(this string value)
        {
            if (value.IsEmpty() || (value.Length == 1))
            {
                return value;
            }

            var chars = value.ToCharArray();
            Array.Reverse(chars);
            var result = new string(chars);
            return result;
        }

        /// <summary>
        /// 按行逆序
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string ReverseLine(this string value)
        {
            lock (StringBuilder)
            {
                StringBuilder.Remove(0, StringBuilder.Length);
                var lines = value.Split('\n');
                for (var i = lines.Length - 1; i >= 0; i--)
                {
                    StringBuilder.Append(lines[i]);
                    // 末行结尾没有\n，补上
                    if (i == lines.Length - 1 && !lines[i].EndsWith("\n")) StringBuilder.Append("\n");
                }

                var result = StringBuilder.ToString();
                return result;
            }
        }

        /// <summary>
        /// 移除左边
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static string RemoveLeft(this string value, int length)
        {
            var result = value.Length <= length ? "" : value.Substring(length);
            return result;
        }

        /// <summary>
        /// 移除右边
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        public static string RemoveRight(this string value, int length)
        {
            var result = value.Length <= length ? "" : value.Substring(0, value.Length - length);
            return result;
        }

        #endregion

        #region Enum

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            var result = value.IsInEnum<TEnum>() ? default(TEnum) : (TEnum) Enum.Parse(typeof(TEnum), value, default(bool));
            return result;
        }

        /// <summary>
        /// 是否在枚举中
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static bool IsInEnum<TEnum>(this string value) where TEnum : struct
        {
            var result = string.IsNullOrEmpty(value) || !Enum.IsDefined(typeof(TEnum), value);
            return result;
        }

        #endregion

        #region Encoding

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static byte[] GetBytes(this string value)
        {
            var result = Encoding.Default.GetBytes(value);
            return result;
        }

        /// <summary>
        /// 使用指定编码转换为字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns>结果</returns>
        public static byte[] GetBytes(this string value, Encoding encoding)
        {
            var result = encoding.GetBytes(value);
            return result;
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="oldEncode">原编码</param>
        /// <param name="newEncode">目标编码</param>
        /// <returns>结果</returns>
        public static string Encode(this string value, Encoding oldEncode, Encoding newEncode)
        {
            var oldBytes = oldEncode.GetBytes(value);
            var newBytes = Encoding.Convert(oldEncode, newEncode, oldBytes);
            var result = newEncode.GetString(newBytes);
            return result;
        }

        #endregion

        #region Base64

        /// <summary>
        /// Base64 编码
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string EncodeBase64(this string value)
        {
            var result = value.EncodeBase64(null);
            return result;
        }

        /// <summary>
        /// Base64 编码
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="encoding">字符串编码</param>
        /// <returns>结果</returns>
        public static string EncodeBase64(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = encoding.GetBytes(value);
            var result = Convert.ToBase64String(bytes);
            return result;
        }

        /// <summary>
        /// Base64 解码
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string DecodeBase64(this string value)
        {
            var result = value.DecodeBase64(null);
            return result;
        }

        /// <summary>
        /// Base64 解码
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="encoding">字符串编码</param>
        /// <returns>结果</returns>
        public static string DecodeBase64(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = Convert.FromBase64String(value);
            var result = encoding.GetString(bytes);
            return result;
        }

        #endregion

        #region Compress

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="value">未压缩的字符串</param>
        /// <returns>结果</returns>
        public static string Compress(this string value)
        {
            var inputBytes = Encoding.Default.GetBytes(value);
            using (var outStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
                {
                    zipStream.Write(inputBytes, 0, inputBytes.Length);
                    zipStream.Close();
                    var result = Convert.ToBase64String(outStream.ToArray());
                    return result;
                }
            }
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="value">被压缩的字符串</param>
        /// <returns>结果</returns>
        public static string Decompress(this string value)
        {
            var inputBytes = Convert.FromBase64String(value);
            using (var inputStream = new MemoryStream(inputBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        var bytes = new byte[4096];
                        int n;
                        while ((n = zipStream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            outStream.Write(bytes, 0, n);
                        }

                        var result = Encoding.Default.GetString(outStream.ToArray());
                        return result;
                    }
                }
            }
        }

        #endregion

        #region IO Directory

        /// <summary>
        /// 文件夹是否存在
        /// </summary>
        /// <param name="directoryPath">文件夹路径</param>
        /// <returns>结果</returns>
        public static bool ExistDirectory(this string directoryPath)
        {
            var result = Directory.Exists(directoryPath);
            return result;
        }

        /// <summary>
        /// 如果不存在，则创建文件夹
        /// </summary>
        /// <param name="directoryPath">文件夹路径</param>
        /// <returns>directoryPath</returns>
        public static string CreateDirectory(this string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }

        /// <summary>
        /// 如果存在，则删除文件夹
        /// </summary>
        /// <param name="directoryPath">文件夹路径</param>
        /// <returns>directoryPath</returns>
        public static string DeleteDirectory(this string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath);
            }

            return directoryPath;
        }

        #endregion

        #region IO File

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>结果</returns>
        public static bool ExistFile(this string filePath)
        {
            var result = File.Exists(filePath);
            return result;
        }

        /// <summary>
        /// 如果存在，额删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>filePath</returns>
        public static string DeleteFile(this string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return filePath;
        }

        /// <summary>
        /// 保存文本到指定路径
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="saveFilePath">保存文件路径</param>
        /// <returns>text</returns>
        public static string WriteAllText(this string text, string saveFilePath)
        {
            File.WriteAllText(saveFilePath, text);
            return text;
        }

        /// <summary>
        /// 从指定路径读取所有文本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>结果</returns>
        public static string ReadAllText(this string filePath)
        {
            var result = File.ReadAllText(filePath);
            return result;
        }

        /// <summary>
        /// 从指定路径读取字节数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>结果</returns>
        public static byte[] ReadAllBytes(this string filePath)
        {
            var result = File.ReadAllBytes(filePath);
            return result;
        }

        /// <summary>
        /// 从指定路径读取所有行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>结果</returns>
        public static string[] ReadAllLines(this string filePath)
        {
            var result = File.ReadAllLines(filePath);
            return result;
        }

        #endregion

        #region IO Path

        /// <summary>
        /// 拼合路径
        /// </summary>
        /// <param name="path">原路径</param>
        /// <param name="nextPath">待拼合路径</param>
        /// <returns>结果</returns>
        public static string CombinePath(this string path, string nextPath)
        {
            var result = Path.Combine(path, nextPath);
            return result;
        }

        /// <summary>
        /// 拼合路径
        /// </summary>
        /// <param name="path">原路径</param>
        /// <param name="paths">待拼合路径</param>
        /// <returns>结果</returns>
        public static string CombinePaths(this string path, params string[] paths)
        {
            var result = path;
            for (var i = 0; i < paths.Length; i++)
            {
                var next = paths[i];
                result = Path.Combine(result, next);
            }

            return result;
        }

        #endregion

        #region Html

        /// <summary>
        /// 转换成HTML字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>结果</returns>
        public static string ToHtmlSafe(this string value)
        {
            var result = value.ToHtmlSafe(false, false);
            return result;
        }

        /// <summary>
        /// 转换成HTML字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="all">所有</param>
        /// <returns>结果</returns>
        public static string ToHtmlSafe(this string value, bool all)
        {
            var result = value.ToHtmlSafe(all, false);
            return result;
        }

        /// <summary>
        /// 转换成HTML字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="all">所有</param>
        /// <param name="replace">替换换行和空格</param>
        /// <returns>结果</returns>
        public static string ToHtmlSafe(this string value, bool all, bool replace)
        {
            if (value.IsEmptyOrWhiteSpace()) return string.Empty;
            var entities = new[]
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 28, 29, 30, 31, 34, 39, 38, 60, 62, 123, 124, 125, 126,
                127, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
                190, 191, 215, 247, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218,
                219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249,
                250, 251, 252, 253, 254, 255, 256, 8704, 8706, 8707, 8709, 8711, 8712, 8713, 8715, 8719, 8721, 8722, 8727, 8730, 8733, 8734, 8736, 8743, 8744, 8745, 8746,
                8747, 8756, 8764, 8773, 8776, 8800, 8801, 8804, 8805, 8834, 8835, 8836, 8838, 8839, 8853, 8855, 8869, 8901, 913, 914, 915, 916, 917, 918, 919, 920, 921,
                922, 923, 924, 925, 926, 927, 928, 929, 931, 932, 933, 934, 935, 936, 937, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960,
                961, 962, 963, 964, 965, 966, 967, 968, 969, 977, 978, 982, 338, 339, 352, 353, 376, 402, 710, 732, 8194, 8195, 8201, 8204, 8205, 8206, 8207, 8211, 8212,
                8216, 8217, 8218, 8220, 8221, 8222, 8224, 8225, 8226, 8230, 8240, 8242, 8243, 8249, 8250, 8254, 8364, 8482, 8592, 8593, 8594, 8595, 8596, 8629, 8968,
                8969, 8970, 8971, 9674, 9824, 9827, 9829, 9830
            };
            var stringBuilder = new StringBuilder();
            foreach (var c in value)
            {
                if (all || entities.Contains(c))
                {
                    stringBuilder.Append("&#" + ((int) c) + ";");
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }

            var result = replace ? stringBuilder.Replace("", "<br />").Replace("\n", "<br />").Replace(" ", "&nbsp;").ToString() : stringBuilder.ToString();
            return result;
        }

        #endregion
    }
}