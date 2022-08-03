/////////////////////////////////////////////////////////////////////////////
//
//  Script   : StringBuilderExtension.cs
//  Info     : StringBuilder 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System.Text;

namespace Aya.Extension
{
    public static class StringBuilderExtension
    {
        /// <summary>
        /// 字符串逆序
        /// </summary>
        /// <param name="builder">字符串</param>
        /// <returns>builder</returns>
        public static StringBuilder Reverse(this StringBuilder builder)
        {
            var result = new StringBuilder();
            for (var i = builder.Length - 1; i >= 0; i--)
            {
                result.Append(builder[i]);
            }

            builder.Remove(0, builder.Length);
            builder.Append(result);
            return builder;
        }

        /// <summary>
        /// 清除所有内容
        /// </summary>
        /// <param name="builder">字符串构造器</param>
        /// <returns>builder</returns>
        public static StringBuilder Clear(this StringBuilder builder)
        {
            builder.Remove(0, builder.Length);
            return builder;
        }
    }
}