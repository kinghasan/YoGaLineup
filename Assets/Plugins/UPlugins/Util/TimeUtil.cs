/////////////////////////////////////////////////////////////////////////////
//
//  Script   : TimeUtil.cs
//  Info     : 时间辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Util
{
	public static class TimeUtil
	{
        #region Private

	    /// <summary>
	    /// 使用UTC时间
	    /// </summary>
	    public static readonly bool UseUtcTime = false;

        /// <summary>
        /// 起始时间，用于时间戳计算
        /// </summary>
        public static DateTime StartTime => UseUtcTime
	        ? TimeZone.CurrentTimeZone.ToUniversalTime(new DateTime(1970, 1, 1))
	        : TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

	    /// <summary>
	    /// 当前时间
	    /// </summary>
	    public static DateTime Now => UseUtcTime ? DateTime.UtcNow : DateTime.Now;

		#endregion

		#region Static Value

	    /// <summary>
	    /// 一年的秒数
	    /// </summary>
	    /// <returns></returns>
	    public static long SecondsForOneYear => SecondsForOneDay * 365;

	    /// <summary>
	    /// 一天的秒数
	    /// </summary>
	    /// <returns></returns>
	    public static long SecondsForOneDay => SecondsForOneHour * 24;

	    /// <summary>
	    /// 一小时的秒数
	    /// </summary>
	    /// <returns></returns>
	    public static long SecondsForOneHour => SecondsForOneMinute * 60;

	    /// <summary>
	    /// 一分钟的秒数
	    /// </summary>
	    /// <returns></returns>
	    public static long SecondsForOneMinute => 60;

        #endregion

        #region TimeStamp & DateTime

        #region TimeStamp 10

        /// <summary>
        /// 时间戳 10 位
        /// </summary>
        public static long TimeStamp => UseUtcTime ? UtcTimeStamp : LocalTimeStamp;

        /// <summary>
        /// 当地时间 时间戳 10 位
        /// </summary>
        public static long LocalTimeStamp => DateTime2TimeStamp(DateTime.Now, false);

	    /// <summary>
        /// Utc时间 时间戳 10 位
        /// </summary>
        public static long UtcTimeStamp => DateTime2TimeStamp(DateTime.Now, true);

        #endregion

        #region TimeStamp 13
        /// <summary>
        /// 时间戳 13 位
        /// </summary>
        public static long TimeStamp13 => UseUtcTime ? UtcTimeStamp13 : LocalTimeStamp13;

	    /// <summary>
	    /// 当地时间 时间戳 13位
	    /// </summary>
	    public static long LocalTimeStamp13 => DateTime2TimeStamp13(DateTime.Now, false);

        /// <summary>
        /// Utc时间 时间戳 13位
        /// </summary>
        public static long UtcTimeStamp13 => DateTime2TimeStamp13(DateTime.Now, true);
        #endregion

        /// <summary>
        /// 时间戳转为 DateTime (自动判断10位/13位)
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>时间</returns>
        public static DateTime TimeStamp2DateTime(long timeStamp)
        {
            var start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime ret;
            if (timeStamp <= 9999999999)
            {
                ret = start.AddSeconds(timeStamp);
            }
            else
            {
                ret = start.AddMilliseconds(timeStamp);
            }
			return ret;
		}

	    /// <summary>
	    /// DateTime 转为时间戳 10位
	    /// </summary>
	    /// <param name="dt">时间</param>
	    /// <param name="isUtc">是否使用UCT时间</param>
	    /// <returns>时间戳</returns>
	    public static long DateTime2TimeStamp(DateTime dt, bool isUtc = false)
		{
		    var start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		    var end = isUtc ? dt.ToUniversalTime() : dt;
		    var timestamp = (long)(end - start).TotalSeconds;
		    return timestamp;
		}

	    /// <summary>
	    /// DateTime 转为时间戳 13位
	    /// </summary>
	    /// <param name="dt">时间</param>
	    /// <param name="isUtc">是否使用UCT时间</param>
	    /// <returns>时间戳</returns>
        public static long DateTime2TimeStamp13(DateTime dt, bool isUtc = false)
	    {
	        var start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
	        var end = isUtc ? dt.ToUniversalTime() : dt;
            var timestamp = (long)(end - start).TotalMilliseconds;
	        return timestamp;
	    }

        #endregion

        #region Now Year / Month / Day / Hour / Minute / Second / Milesecond

        /// <summary>
        /// 当前时间 - 年
        /// </summary>
        public static int Year => Now.Year;

	    /// <summary>
		/// 当前时间 - 月
		/// </summary>
		public static int Month => Now.Month;

	    /// <summary>
		/// 当前时间 - 日
		/// </summary>
		public static int Day => Now.Day;

	    /// <summary>
		/// 当前时间 - 时
		/// </summary>
		public static int Hour => Now.Hour;

	    /// <summary>
		/// 当前时间 - 分
		/// </summary>
		public static int Minute => Now.Minute;

	    /// <summary>
		/// 当前时间 - 秒
		/// </summary>
		public static int Second => Now.Second;

	    /// <summary>
		/// 当前时间 - 毫秒
		/// </summary>
		public static int Millisecond => Now.Millisecond;

	    #endregion

		#region Get Seconds

		/// <summary>
		/// 获取距离下一天0点的秒数
		/// </summary>
		/// <returns>结果</returns>
		public static long GetSecondsToNextDay()
		{
			return GetSecondsToNextHour(0);
		}

		/// <summary>
		/// 获取距离下一个整点的秒数
		/// </summary>
		/// <param name="hour"></param>
		/// <returns>结果</returns>
		public static long GetSecondsToNextHour(int hour)
		{
			hour = Mathf.Clamp(hour, 0, 23);
			var time = Now;
			if (time.Hour >= hour)
			{
				time = time.AddDays(1);
			}
			time = new DateTime(time.Year, time.Month, time.Day, hour, 0, 0);
			return (long) (time - Now).TotalSeconds;
		}

		#endregion
	}
}