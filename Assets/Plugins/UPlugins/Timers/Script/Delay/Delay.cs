/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Delay.cs
//  Info     : 延时触发器，到达指定时间后执行指定事件。
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Function : 1.延时t,执行1次（Coroutines）
//
//  Explain  : 由协程实现，需要依赖承载对象。
//			   该功能保留以兼容部分代码，可以由MonoBehaviour.ExcuteDaley(...)替代。
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using UnityEngine;
using Aya.Singleton;

namespace Aya.Timers
{
	/// <summary>
	/// 延时执行
	/// </summary>
	public class Delay : MonoSingleton<Delay> 
	{
		/// <summary>
		/// 延迟指定时间指定某个方法
		/// </summary>
		/// <param name="action">方法</param>
		/// <param name="delaySeconds">时间</param>
		/// <param name="timeScale">时间缩放</param>
		/// <param name="host">承载对象</param>
		public Coroutine Invoke(Action action, float delaySeconds, bool timeScale = true, MonoBehaviour host = null)
		{
			return host == null
				? StartCoroutine(DoEvent(action, delaySeconds, timeScale))
				: host.StartCoroutine(DoEvent(action, delaySeconds, timeScale));
		}

		/// <summary>
		/// 延时执行
		/// </summary>
		/// <param name="action">方法</param>
		/// <param name="delaySeconds">时间</param>
		/// <param name="timeScale">时间缩放</param>
		/// <returns>枚举接口</returns>
		private static IEnumerator DoEvent(Action action, float delaySeconds, bool timeScale = true) 
		{
			if (timeScale)
			{
				yield return new WaitForSeconds(delaySeconds);
			} else
			{
				yield return new WaitForSecondsRealtime(delaySeconds);
			}
			action();
		}
	}
}
