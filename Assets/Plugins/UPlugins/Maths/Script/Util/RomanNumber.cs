/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RomanNumber.cs
//  Info     : 罗马数字处理类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.Text;

namespace Aya.Maths
{
    public static class RomanNumber
    {
        private static readonly Dictionary<char, int> RomanMap = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        private static readonly string[,] RomanArray = new string[4, 10]
        {
            {"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"},
            {"", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"},
            {"", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"},
            {"", "M", "MM", "MMM", "", "", "", "", "", ""}
        };

        /// <summary>
        /// 阿拉伯数字转换成罗马数字
        /// </summary>
        /// <param name="arabicNumber">阿拉伯数字(>=0)</param>
        /// <returns>结果</returns>
        public static string ArabicToRoman(int arabicNumber)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(RomanArray[3, arabicNumber / 1000 % 10]);
            stringBuilder.Append(RomanArray[2, arabicNumber / 100 % 10]);
            stringBuilder.Append(RomanArray[1, arabicNumber / 10 % 10]);
            stringBuilder.Append(RomanArray[0, arabicNumber % 10]);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 罗马数字转换成阿拉伯数字
        /// </summary>
        /// <param name="romanNumber">罗马数字(0-3999)</param>
        /// <returns>结果</returns>
        public static int RomanToArabic(string romanNumber)
        {
            if (string.IsNullOrEmpty(romanNumber)) return 0;
            var length = romanNumber.Length;
            if (length == 1)
            {
                return RomanMap[romanNumber[0]];
            }

            var i = 0;
            var result = RomanMap[romanNumber[length - 1]];
            for (i = length - 2; i >= 0; i--)
            {
                if (RomanMap[romanNumber[i]] >= RomanMap[romanNumber[i + 1]])
                {
                    result += RomanMap[romanNumber[i]];
                }
                else
                {
                    result -= RomanMap[romanNumber[i]];
                }
            }

            return result;
        }
    }
}
