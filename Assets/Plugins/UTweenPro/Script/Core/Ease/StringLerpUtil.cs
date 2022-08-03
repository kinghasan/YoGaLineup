using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Aya.TweenPro
{
    public static class StringLerpUtil
    {
        public static readonly StringBuilder StringBuilder = new StringBuilder();

        private static readonly StringBuilder LerpBuilder = new StringBuilder();
        private static readonly List<char> OpenedTags = new List<char>();

        public static int GetLength(string value, bool richTextEnabled)
        {
            var strLength = value.Length;
            if (richTextEnabled)
            {
                var index = 0;
                var len = 0;
                var findStart = false;
                var findStartIndex = 0;
                var findEnd = false;
                var findEndIndex = 0;
                while (index < value.Length)
                {
                    var ch = value[index];
                    if (ch == '<')
                    {
                        findStart = true;
                        findStartIndex = index;
                    }

                    if (ch == '>')
                    {
                        findEnd = true;
                        findEndIndex = index;
                    }

                    len++;
                    index++;

                    if (findStart && findEnd)
                    {
                        len -= findEndIndex - findStartIndex + 1;
                        findStart = false;
                        findEnd = false;
                    }
                }

                strLength = len;
            }

            return strLength;
        }

        public static string Lerp(string value, float delta, bool richTextEnabled)
        {
            LerpBuilder.Clear();
            var strLength = GetLength(value, richTextEnabled);
            var startIndex = 0;
            var length = (int)(delta * strLength);
            length = Mathf.Clamp(length, 0, value.Length);
            if (!richTextEnabled)
            {
                LerpBuilder.Append(value, startIndex, length);
                return LerpBuilder.ToString();
            }

            OpenedTags.Clear();
            var flag = false;
            var num = value.Length;
            var num2 = 0;
            while (num2 < length)
            {
                var ch = value[num2];
                if (ch == '<')
                {
                    var flag2 = flag;
                    var ch2 = value[num2 + 1];
                    flag = (num2 >= (num - 1)) || (ch2 != '/');
                    if (flag)
                    {
                        OpenedTags.Add((ch2 == '#') ? 'c' : ch2);
                    }
                    else
                    {
                        OpenedTags.RemoveAt(OpenedTags.Count - 1);
                    }

                    var match = Regex.Match(value.Substring(num2), "<.*?(>)");
                    if (match.Success)
                    {
                        if (!flag && !flag2)
                        {
                            var ch3 = value[num2 + 1];
                            var chArray = ch3 == 'c' ? new char[] { '#', 'c' } : new char[] { ch3 };

                            for (var i = num2 - 1; i > -1; i--)
                            {
                                if (((value[i] == '<') && (value[i + 1] != '/')) && (Array.IndexOf<char>(chArray, value[i + 2]) != -1))
                                {
                                    LerpBuilder.Insert(0, value.Substring(i, (value.IndexOf('>', i) + 1) - i));
                                    break;
                                }
                            }
                        }

                        LerpBuilder.Append(match.Value);
                        var num3 = match.Groups[1].Index + 1;
                        length += num3;
                        startIndex += num3;
                        num2 += num3 - 1;
                    }
                }
                else if (num2 >= startIndex)
                {
                    LerpBuilder.Append(ch);
                }

                num2++;
            }

            if ((OpenedTags.Count > 0) && (num2 < (num - 1)))
            {
                while ((OpenedTags.Count > 0) && (num2 < (num - 1)))
                {
                    var match2 = Regex.Match(value.Substring(num2), "(</).*?>");
                    if (!match2.Success)
                    {
                        break;
                    }

                    if (match2.Value[2] == OpenedTags[OpenedTags.Count - 1])
                    {
                        LerpBuilder.Append(match2.Value);
                        OpenedTags.RemoveAt(OpenedTags.Count - 1);
                    }

                    num2 += match2.Value.Length;
                }
            }

            return LerpBuilder.ToString();
        }
    }
}