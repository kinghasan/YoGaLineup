/////////////////////////////////////////////////////////////////////////////
//
//  Script   : BigNumber.cs
//  Info     : BigNumber 大数显示辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.Text;

namespace Aya.Maths
{
    public static class BigNumber
    {
        // Kilo
        // Meg
        // Giga
        // Tera
        // Peta
        // Era
        // Zetta
        // Yotta

        /// <summary>
        /// 缩写单位
        /// </summary>
        public static string[] Unit = {"K", "M", "G", "T", "P", "E", "Z", "Y"};
        /// <summary>
        /// 千位分隔符
        /// </summary>
        public static string Split = ",";

        private static readonly StringBuilder Builder = new StringBuilder();

        /// <summary>
        /// 格式化数字字符串
        /// </summary>
        /// <param name="number">数字字符串</param>
        /// <param name="useSplit">是否使用千位分隔符</param>
        /// <param name="retentionDigit">保留不缩写的位数</param>
        /// <returns>结果</returns>
        public static string Formart(string number, bool useSplit = true, int retentionDigit = 6)
        {
            var numArray = number.ToCharArray();
            var startIndex = 0;
            var endIndex = numArray.Length - 1;
            var unitArray = new List<string>();
            while (endIndex - startIndex > retentionDigit)
            {
                var remain = endIndex + 1 - retentionDigit;
                var level = remain / 3;
                if (endIndex + 1 - level * 3 > retentionDigit && retentionDigit > 3) remain += 3;
                level = remain / 3;
                if (level <= 0) break;
                if (level > Unit.Length)
                {
                    level = Unit.Length;
                }
                unitArray.Add(Unit[level - 1]);
                endIndex -= level * 3;
            }
            Builder.Remove(0, Builder.Length);
            for (var i = 0; i <= endIndex; i++)
            {
                Builder.Append(numArray[i]);
                if (useSplit && i != endIndex && (endIndex - i) % 3 == 0)
                {
                    Builder.Append(Split);
                }
            }
            Builder.Append(" ");
            for (var i = unitArray.Count - 1; i >= 0 ; i--)
            {
                Builder.Append(unitArray[i]);
            }
            return Builder.ToString();
        }
    }
}
