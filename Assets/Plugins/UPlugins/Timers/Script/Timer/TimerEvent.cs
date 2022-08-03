/////////////////////////////////////////////////////////////////////////////
//
//  Script : TimerEvent.cs
//  Info   : 定时触发器事件封装，负责自身的计时和执行触发。
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Timers
{
	/// <summary>
	/// 定时触发器事件
	/// </summary>
	public class TimerEvent
	{
		/// <summary>
		/// 定时器键值计数
		/// </summary>
		private static int _timerKey = 0;

		/// <summary>
		/// 键值
		/// </summary>
		public int Key { get; private set; }

		/// <summary>
		/// 触发事件
		/// </summary>
		public Action Action { get; private set; }

		/// <summary>
		/// 启用状态
		/// </summary>
		public bool Enable { private set; get; }

		/// <summary>
		/// 延时执行时间，小于0为不延时
		/// </summary>
		public float Delay { private set; get; }

		/// <summary>
		/// 间隔执行时间
		/// </summary>
		public float Interval { get; private set; }

		/// <summary>
		/// 执行次数， 小于1则为重复执行，大于等于1则为实际执行次数，到达后停止
		/// </summary>
		public int Count { get; private set; }

		/// <summary>
		/// 是否使用缩放时间
		/// </summary>
		public bool TimeScale { get; private set; }

		/// <summary>
		/// 计时
		/// </summary>
		private float _time;

		/// <summary>
		/// 计次
		/// </summary>
		private int _count;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="action">事件</param>
		/// <param name="delay">延时</param>
		/// <param name="interval">间隔</param>
		/// <param name="count">次数</param>
		/// <param name="timeScale">缩放时间</param>
		public TimerEvent(Action action, float delay, float interval, int count = 1, bool timeScale = true)
		{
			// 防溢出
			delay = delay < 0 ? 0 : delay;
			interval = Interval < 0 ? 0 : interval;
			count = Count < 0 ? 0 : count;
			// 设置属性
			Enable = true;
			Key = _timerKey++;
			Action = action;
			Delay = delay;
			Interval = interval;
			Count = count;
			TimeScale = timeScale;
		}

		/// <summary>
		/// 开启
		/// </summary>
		public void Start()
		{
			_time = 0;
			_count = 0;
			Enable = true;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
			// 计时
			if (TimeScale)
			{
				_time += Time.deltaTime;
			}
			else
			{
				_time += Time.unscaledDeltaTime;
			}
			// 是否需要延时
			if (Delay > 0)
			{
				// 延时到达，触发第一次执行
				if (_time >= Delay)
				{
					Delay = -1;
					// 只需要延时执行一次
					if (Count == 1)
					{
						DoAction();
						Stop();
						return;
					}
					_time = Interval;
				}
			}
			// 定时执行
			else if (_time >= Interval)
			{
				_time = 0;
				DoAction();
			}
			// 是否停止
			if (_count == Count && Count >= 1)
			{
				Stop();
			}
		}

		/// <summary>
		/// 执行
		/// </summary>
		public void DoAction()
		{
			_count++;
			if (Action != null)
			{
				Action.Invoke();
			}
		}

		/// <summary>
		/// 停止
		/// </summary>
		public void Stop()
		{
			Enable = false;
		}
	}
}
