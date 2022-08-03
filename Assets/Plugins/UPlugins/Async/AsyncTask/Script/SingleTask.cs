/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SingleTask.cs
//  Info     : 单个任务，由任务和结束条件构成
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Async
{
	public class SingleTask
	{
		protected Action Action = delegate {};
		protected Func<bool> Condition;

		private bool _executing;

		public SingleTask(): this(null)
		{
		}

		public SingleTask(Action action, Func<bool> condition = null)
		{
			Action += action;
			Condition = condition ?? _defaultCheck;
			_executing = false;
		}

		public void Execute()
		{
			if (_executing) return;
			_executing = true;
			Action();
		}

		public bool Check()
		{
			return Condition();
		}

		private static bool _defaultCheck()
		{
			return true;
		}
	}
}