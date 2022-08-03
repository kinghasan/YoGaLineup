/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DateTimeExtension.cs
//  Info     : DateTime 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Globalization;
using System.Text;

namespace Aya.Extension
{
    public static class DateTimeExtension
    {
        const int EveningEnds = 2;
        const int MorningEnds = 12;
        const int AfternoonEnds = 18;
        static readonly DateTime Date1970 = new DateTime(1970, 1, 1);

        /// <summary>
        /// Utc时间偏移
        /// </summary>
        public static double UtcOffset => DateTime.Now.Subtract(DateTime.UtcNow).TotalHours;

        #region Age
        
        /// <summary>
        /// 此时间作为出生时间计算年龄
        /// </summary>
        /// <param name="dateTime">出生日期</param>
        /// <param name="currentDateTime">当前时间</param>
        /// <returns>结果</returns>
        public static int CalculateAge(this DateTime dateTime, DateTime currentDateTime)
        {
            var years = currentDateTime.Year - dateTime.Year;
            if (currentDateTime.Month < dateTime.Month || (currentDateTime.Month == dateTime.Month && currentDateTime.Day < dateTime.Day))
            {
                --years;
            }
            return years;
        }

        #endregion

        #region Day

        /// <summary>
        /// 获取两个日期的间隔天数
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns>结果</returns>
        public static int GetDays(this DateTime dateTime, DateTime toDate)
        {
            var result = Convert.ToInt32(toDate.Subtract(dateTime).TotalDays);
            return result;
        }

        /// <summary>
        /// 获取当前时间在一天中的时间段(morning(2-12)/afternoon(13-18)/evening(18-2))
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static string GetPeriodOfDay(this DateTime dateTime)
        {
            var hour = dateTime.Hour;
            if (hour < EveningEnds)
            {
                return "evening";
            }

            if (hour < MorningEnds)
            {
                return "morning";
            }
            return hour < AfternoonEnds ? "afternoon" : "evening";
        }

        /// <summary>
        /// 是否是今天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static bool IsToday(this DateTime dateTime)
        {
            var result = dateTime.Date == DateTime.Today;
            return result;
        }

        /// <summary>
        /// 明天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static DateTime Tomorrow(this DateTime dateTime)
        {
            var result = dateTime.AddDays(1);
            return result;
        }

        /// <summary>
        /// 昨天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static DateTime Yesterday(this DateTime dateTime)
        {
            var result = dateTime.AddDays(-1);
            return result;
        }

        #endregion

        #region Week

        /// <summary>
        /// 获取当前时间所在星期的第一天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
        {
            var result = dateTime.GetFirstDayOfWeek(CultureInfo.CurrentCulture);
            return result;
        }

        /// <summary>
        /// 获取当前时间所在星期的第一天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="cultureInfo">区域信息</param>
        /// <returns>结果</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime dateTime, CultureInfo cultureInfo)
        {
            cultureInfo = (cultureInfo ?? CultureInfo.CurrentCulture);
            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            while (dateTime.DayOfWeek != firstDayOfWeek)
            {
                dateTime = dateTime.AddDays(-1);
            }

            return dateTime;
        }

        /// <summary>
        /// 获取当前时间所在星期的最后一天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static DateTime GetLastDayOfWeek(this DateTime dateTime)
        {
            var result = dateTime.GetLastDayOfWeek(CultureInfo.CurrentCulture);
            return result;
        }

        /// <summary>
        /// 获取当前时间所在星期的最后一天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="cultureInfo">区域信息</param>
        /// <returns>结果</returns>
        public static DateTime GetLastDayOfWeek(this DateTime dateTime, CultureInfo cultureInfo)
        {
            var result = dateTime.GetFirstDayOfWeek(cultureInfo).AddDays(6);
            return result;
        }

        /// <summary>
        /// 获取当前时间开始第一次出现星期几的时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="weekday">星期几</param>
        /// <returns>结果</returns>
        public static DateTime GetWeeksWeekday(this DateTime dateTime, DayOfWeek weekday)
        {
            var result = dateTime.GetWeeksWeekday(weekday, CultureInfo.CurrentCulture);
            return result;
        }

        /// <summary>
        /// 获取当前时间开始第一次出现星期几的时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="weekday">星期几</param>
        /// <param name="cultureInfo">区域信息</param>
        /// <returns>结果</returns>
        public static DateTime GetWeeksWeekday(this DateTime dateTime, DayOfWeek weekday, CultureInfo cultureInfo)
        {
            var firstDayOfWeek = dateTime.GetFirstDayOfWeek(cultureInfo);
            var result = firstDayOfWeek.GetNextWeekday(weekday);
            return result;
        }

        /// <summary>
        /// 获取当前时间开始下一个星期几
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="weekday">星期几</param>
        /// <returns>结果</returns>
        public static DateTime GetNextWeekday(this DateTime dateTime, DayOfWeek weekday)
        {
            while (dateTime.DayOfWeek != weekday)
            {
                dateTime = dateTime.AddDays(1);
            }
            return dateTime;
        }

        /// <summary>
        /// 获取上一个星期几
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="weekday">星期几</param>
        /// <returns>结果</returns>
        public static DateTime GetPreviousWeekday(this DateTime dateTime, DayOfWeek weekday)
        {
            while (dateTime.DayOfWeek != weekday)
            {
                dateTime = dateTime.AddDays(-1);
            }
            return dateTime;
        }

        /// <summary>
        /// 是否是周末（周六，周日）
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            var result = dateTime.DayOfWeek.EqualsAny(DayOfWeek.Saturday, DayOfWeek.Sunday);
            return result;
        }

        /// <summary>
        /// 是否是工作日（非周六，周日）
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static bool IsWorkDay(this DateTime dateTime)
        {
            var result = !dateTime.IsWeekend();
            return result;
        }

        /// <summary>
        /// 获取当前时间所在年份的星期数
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            var result = GetWeekOfYear(dateTime, CultureInfo.CurrentCulture);
            return result;
        }

        /// <summary>
        /// 获取当前时间所在年份的星期数
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="cultureInfo">区域信息</param>
        /// <returns>结果</returns>
        public static int GetWeekOfYear(this DateTime dateTime, CultureInfo cultureInfo)
        {
            var calendar = cultureInfo.Calendar;
            var dateTimeFormat = cultureInfo.DateTimeFormat;
            var result = calendar.GetWeekOfYear(dateTime, dateTimeFormat.CalendarWeekRule, dateTimeFormat.FirstDayOfWeek);
            return result;
        }

        #endregion

        #region Month

        /// <summary>
        /// 获取当前时间所在月的天数
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static int GetCountDaysOfMonth(this DateTime dateTime)
        {
            var nextMonth = dateTime.AddMonths(1);
            var result = new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
            return result;
        }

        /// <summary>
        /// 获取当前时间所在月份的第一天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            var result = new DateTime(dateTime.Year, dateTime.Month, 1);
            return result;
        }

        /// <summary>
        /// 获取当前时间所在月份的第一个星期几
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <returns>结果</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            var dt = dateTime.GetFirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(1);
            }
            return dt;
        }

        /// <summary>
        /// 获取当前时间所在月份的最后一天
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, GetCountDaysOfMonth(dateTime));
        }

        /// <summary>
        /// 获取当前时间所在月份的最后一个星期几
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <returns>结果</returns>
        public static DateTime GetLastDayOfMonth(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            var dt = dateTime.GetLastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        #endregion

        #region Timestamp

        /// <summary>
        /// 获取时间戳(1970年开始的总毫秒数)
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>毫秒数</returns>
        public static long GetTimestamp(this DateTime datetime)
        {
            var ts = datetime.Subtract(Date1970);
            var result = (long)ts.TotalMilliseconds;
            return result;
        }

        #endregion

        #region Equal

        /// <summary>
        /// 日期是否相同
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="dateToCompare">待比较时间</param>
        /// <returns>结果</returns>
        public static bool IsDateEqual(this DateTime dateTime, DateTime dateToCompare)
        {
            var result = dateTime.Date == dateToCompare.Date;
            return result;
        }

        /// <summary>
        /// 一天内的时间是否相同
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="timeToCompare">待比较时间</param>
        /// <returns>结果</returns>
        public static bool IsTimeEqual(this DateTime dateTime, DateTime timeToCompare)
        {
            var result = dateTime.TimeOfDay == timeToCompare.TimeOfDay;
            return result;
        }

        /// <summary>
        /// 是否在某个时间之前
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="other">另一个时间</param>
        /// <returns>结果</returns>
        public static bool IsBefore(this DateTime dateTime, DateTime other)
        {
            var result = dateTime.CompareTo(other) < 0;
            return result;
        }

        /// <summary>
        /// 是否在某个时间之后
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="other">另一个时间</param>
        /// <returns>结果</returns>
        public static bool IsAfter(this DateTime dateTime, DateTime other)
        {
            var result = dateTime.CompareTo(other) > 0;
            return result;
        }

        #endregion

        #region DateTimeOffset

        /// <summary>
        /// 转换为时间点
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime)
        {
            return dateTime.ToDateTimeOffset(null);
        }

        /// <summary>
        /// 转换为时间点
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="localTimeZone">时区信息</param>
        /// <returns>结果</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime, TimeZoneInfo localTimeZone)
        {
            localTimeZone = (localTimeZone ?? TimeZoneInfo.Local);
            if (dateTime.Kind != DateTimeKind.Unspecified)
            {
                dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
            }
            var result = TimeZoneInfo.ConvertTimeToUtc(dateTime, localTimeZone);
            return result;
        }

        #endregion

        #region Festival

        /// <summary>
        /// 是否是基督教日历上的复活节
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static bool IsEaster(this DateTime dateTime)
        {
            var y = dateTime.Year;
            var a = y % 19;
            var b = y / 100;
            var c = y % 100;
            var d = b / 4;
            var e = b % 4;
            var f = (b + 8) / 25;
            var g = (b - f + 1) / 3;
            var h = (19 * a + b - d - g + 15) % 30;
            var i = c / 4;
            var k = c % 4;
            var l = (32 + 2 * e + 2 * i - h - k) % 7;
            var m = (a + 11 * h + 22 * l) / 451;
            var month = (h + l - 7 * m + 114) / 31;
            var day = ((h + l - 7 * m + 114) % 31) + 1;
            var dtEasterSunday = new DateTime(y, month, day);
            var result = dateTime == dtEasterSunday;
            return result;
        }

        #endregion

        #region Format

        /// <summary>
        /// 友好时间格式<para/>
        /// Example ; Today, 3:33 PM
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>结果</returns>
        public static string ToFriendlyDateString(this DateTime dateTime)
        {
            var result = ToFriendlyDateString(dateTime, CultureInfo.CurrentCulture);
            return result;
        }

        /// <summary>
        /// 友好时间格式<para/>
        /// Example ; Today, 3:33 PM
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="cultureInfo">区域信息</param>
        /// <returns>结果</returns>
        public static string ToFriendlyDateString(this DateTime dateTime, CultureInfo cultureInfo)
        {
            var sbFormattedDate = new StringBuilder();
            if (dateTime.Date == DateTime.Today)
            {
                sbFormattedDate.Append("Today");
            }
            else if (dateTime.Date == DateTime.Today.AddDays(-1))
            {
                sbFormattedDate.Append("Yesterday");
            }
            else if (dateTime.Date > DateTime.Today.AddDays(-6))
            {
                // 星期几
                sbFormattedDate.Append(dateTime.ToString("dddd").ToString(cultureInfo));
            }
            else
            {
                sbFormattedDate.Append(dateTime.ToString("MMMM dd, yyyy").ToString(cultureInfo));
            }

            // 上午 / 下午
            sbFormattedDate.Append(" at ").Append(dateTime.ToString("t").ToLower());
            return sbFormattedDate.ToString();
        }

        #endregion
    }
}
