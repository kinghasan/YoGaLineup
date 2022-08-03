/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DateTimeOffsetExtension.cs
//  Info     : DateTimeOffset 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Extension
{
    public static class DateTimeOffsetExtension
    {
        /// <summary>
        /// 是否是今天
        /// </summary>
        /// <param name="dateTimeOffset">时间点</param>
        /// <returns>结果</returns>
        public static bool IsToday(this DateTimeOffset dateTimeOffset)
        {
            var result = dateTimeOffset.Date.IsToday();
            return result;
        }

        /// <summary>
        /// UTC时间点转换为本地时间
        /// </summary>
        /// <param name="dateTimeOffset">UTC时间点</param>
        /// <returns>结果</returns>
        public static DateTime ToLocalDateTime(this DateTimeOffset dateTimeOffset)
        {
            var result = dateTimeOffset.ToLocalDateTime(null);
            return result;
        }

        /// <summary>
        /// UTC时间点转换为本地时间
        /// </summary>
        /// <param name="dateTimeOffset">UTC时间点</param>
        /// <param name="localTimeZone">本地时区信息</param>
        /// <returns>结果</returns>
        public static DateTime ToLocalDateTime(this DateTimeOffset dateTimeOffset, TimeZoneInfo localTimeZone)
        {
            localTimeZone = (localTimeZone ?? TimeZoneInfo.Local);
            var result = TimeZoneInfo.ConvertTime(dateTimeOffset, localTimeZone).DateTime;
            return result;
        }
    }
}
