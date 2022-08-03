/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Timer.cs
//  Info     : 定时触发器，用于创建指定类型的定时任务。
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Function : 1.延时t,执行1次
//             2.延时t1,间隔t2,执行n次
//             3.延时t1,间隔t2,重复执行
//             4.间隔t,执行n次
//             5.间隔t,重复执行
//			   6.每天整x点,执行n次
//			   7.每天整x点,重复执行
//
//  Warning  : 1.此定时器依赖Update，因此当帧率过低时，或者触发频率高于帧率时，将不可靠。
//             2.创建重复执行（不限次数）的事件后不会被自动释放，请自行管理这些事件（通过Key），在必要时Stop(Key)，以免造成不必要的资源消耗。
//			   3.默认均为unTimeScale,如需要依赖时间缩放，请注意将最后一个参数设置为true，按现实时间触发的定时器，不支持timeScale。
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using UnityEngine;
using Aya.Singleton;

namespace Aya.Timers
{
	/// <summary>
	/// 定时触发器
	/// </summary>
	public class Timer : MonoSingleton<Timer>
	{
		#region Private

		/// <summary>
		/// 定时触发器事件字典
		/// </summary>
		private static readonly Dictionary<int, TimerEvent> TimerEventDic = new Dictionary<int, TimerEvent>();
		/// <summary>
		/// 定时触发器事件列表
		/// </summary>
		private static readonly List<TimerEvent> TimerEventList = new List<TimerEvent>();

		/// <summary>
		/// 待删除事件队列
		/// </summary>
		private static readonly List<int> RemoveKeys = new List<int>();

		#endregion

		#region EveryDay

		/// <summary>
		/// 每天指定小时触发事件
		/// </summary>
		/// <param name="action">事件</param>
		/// <param name="hour">小时</param>
		/// <param name="count">次数(为0则重复执行)</param>
		/// <returns>键值</returns>
		public int UpdateTimer(Action action, int hour, int count = 0)
		{
			// 计算到下一次设定时间的延迟时间
			var delay = GetSecondsToNextHour(hour);
			var interval = 60 * 60 * 24;
			return CreateTimerEvent(action, delay, interval, count, false);
		}

	    /// <summary>
	    /// 获取距离下一个整点的秒数
	    /// </summary>
	    private static long GetSecondsToNextHour(int hour)
	    {
	        hour = Mathf.Clamp(hour, 0, 23);
	        var time = DateTime.Now;
	        if (time.Hour >= hour)
	        {
	            time = time.AddDays(1);
	        }
	        time = new DateTime(time.Year, time.Month, time.Day, hour, 0, 0);
	        return (long)(time - DateTime.Now).TotalSeconds;
	    }

        #endregion

        #region Delay

        /// <summary>
        /// 延时执行1次
        /// </summary>
        /// <param name="action">事件</param>
        /// <param name="delay">延时时间</param>
        /// <param name="timeScale">缩放时间</param>
        /// <returns>键值</returns>
        public int Delay(Action action, float delay, bool timeScale = true)
		{
			return CreateTimerEvent(action, delay, 0, 1, timeScale);
		}

		/// <summary>
		/// 延时后按间隔执行N次
		/// </summary>
		/// <param name="action">事件</param>
		/// <param name="delay">延时</param>
		/// <param name="interval">间隔时间</param>
		/// <param name="count">次数</param>
		/// <param name="timeScale">缩放时间</param>
		/// <returns>键值</returns>
		public int Delay(Action action, float delay, float interval, int count, bool timeScale = true)
		{
			return CreateTimerEvent(action, delay, interval, count, timeScale);
		}

		/// <summary>
		/// 延时后按间隔重复执行
		/// </summary>
		/// <param name="action">事件</param>
		/// <param name="delay">延时</param>
		/// <param name="interval">间隔时间</param>
		/// <param name="timeScale">缩放时间</param>
		/// <returns>键值</returns>
		public int Delay(Action action, float delay, float interval, bool timeScale = true)
		{
			return CreateTimerEvent(action, delay, interval, 0, timeScale);
		}

		#endregion

		#region Interval

		/// <summary>
		/// 按间隔时间执行N次
		/// </summary>
		/// <param name="action">事件</param>
		/// <param name="interval">间隔时间</param>
		/// <param name="count">次数</param>
		/// <param name="timeScale">缩放时间</param>
		/// <returns>键值</returns>
		public int Interval(Action action, float interval, int count, bool timeScale = true)
		{
			return CreateTimerEvent(action, 0, interval, count, timeScale);
		}

		/// <summary>
		/// 按间隔时间重复执行
		/// </summary>
		/// <param name="action">事件</param>
		/// <param name="interval">间隔时间</param>
		/// <param name="timeScale">缩放时间</param>
		/// <returns>键值</returns>
		public int Interval(Action action, float interval, bool timeScale = true)
		{
			return CreateTimerEvent(action, 0, interval, 0, timeScale);
		}

		#endregion

		#region Get & Count

		/// <summary>
		/// 通过键获取定时器事件
		/// </summary>
		/// <param name="key">键</param>
		/// <returns>定时器事件</returns>
		public TimerEvent GetTimerEvent(int key)
		{
			return TimerEventDic[key];
		}

		/// <summary>
		/// 当前定时器事件数（包含Update和LateUpdate）
		/// </summary>
		/// <returns>数量</returns>
		public int Count()
		{
			return TimerEventList.Count;
		}

		#endregion

		#region Stop

		/// <summary>
		/// 停止指定的事件
		/// </summary>
		/// <param name="key">键值</param>
		public void Stop(int key)
		{
			var timer = TimerEventDic[key];
			TimerEventDic.Remove(key);
			TimerEventList.Remove(timer);
		}

		/// <summary>
		/// 停止一组事件
		/// </summary>
		/// <param name="keys">键值队列</param>
		public void Stop(List<int> keys)
		{
			if (keys == null)
			{
				return;
			}
			foreach (var key in keys)
			{
				Stop(key);
			}
		}

		/// <summary>
		/// 停止所有事件
		/// </summary>
		public void StopAll()
		{
			// 清除删除的事件队列
			RemoveKeys.Clear();
			// 遍历添加事件Key到移除队列
			for (var i = 0; i < TimerEventList.Count; i++)
			{
				var timerEvent = TimerEventList[i];
				RemoveKeys.Add(timerEvent.Key);
			}
			// 删除事件
			for (var i = 0; i < RemoveKeys.Count; i++)
			{
				var key = RemoveKeys[i];
				Stop(key);
			}
		}

		#endregion

		#region Private Method

		#region MonoBehaviour

		private void Update()
		{
			// 清除删除的事件队列
			RemoveKeys.Clear();
			// 遍历执行事件更新
			for (var i = 0; i < TimerEventList.Count; i++)
			{
				var timerEvent = TimerEventList[i];
				if (timerEvent.Enable)
				{
					timerEvent.Update();
				}
				else
				{
					// 已停止的事件加入删除队列
					RemoveKeys.Add(timerEvent.Key);
				}
			}
		}

		#endregion

		/// <summary>
		/// 创建定时器事件
		/// </summary>
		/// <param name="action">事件</param>
		/// <param name="delay">延时</param>
		/// <param name="interval">间隔</param>
		/// <param name="count">次数</param>
		/// <param name="timeScale">缩放时间</param>
		/// <returns>键值</returns>
		private int CreateTimerEvent(Action action, float delay, float interval, int count = 1, bool timeScale = true)
		{
			var timerEvent = new TimerEvent(action, delay, interval, count, timeScale);
			TimerEventDic.Add(timerEvent.Key, timerEvent);
			TimerEventList.Add(timerEvent);
			return timerEvent.Key;
		}

		#endregion
	}
}
