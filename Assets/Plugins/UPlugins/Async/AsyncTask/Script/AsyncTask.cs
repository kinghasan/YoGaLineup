/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AsyncTask.cs
//  Info     : 异步任务队列，由多个任务构成
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aya.Async
{
	public class AsyncTask : AsyncTask<SingleTask>
	{
		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="onFinish">结束回调</param>
		public AsyncTask(Action onFinish = null)
			: base(onFinish)
		{
		}
	}

	public class AsyncTask<T> where T : SingleTask
	{
		protected List<T> Tasks = new List<T>();
		protected Action OnFinish = delegate { };

		public bool IsExecute { get; private set; }
		public bool IsFinish { get; private set; }

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="onFinish">结束回调</param>
		public AsyncTask(Action onFinish = null)
		{
			IsExecute = false;
			IsFinish = false;
			if (onFinish != null) OnFinish += onFinish;
		}

		/// <summary>
		/// 清除（会清空事件）
		/// </summary>
		public void Clear()
		{
			Tasks.Clear();
			IsExecute = false;
			IsFinish = false;
		}

		/// <summary>
        /// 执行
        /// </summary>
		public void Execute()
		{
			if (IsExecute) return;
			IsExecute = true;
			AsyncTaskManager.Ins.StartCoroutine(_executeTask());
		}

		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="task">任务</param>
		/// <returns>任务</returns>
		public T Append(T task)
		{
			Tasks.Add(task);
			return task;
		}

        /// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="action">任务</param>
		/// <param name="doneCondition">结束条件检查</param>
		/// <returns>任务</returns>>
		public T Append(Action action, Func<bool> doneCondition = null)
		{
			var task = new SingleTask(action, doneCondition) as T;
			Tasks.Add(task);
			return task;
		}

		private IEnumerator _executeTask()
		{
			var task = Tasks.Count > 0 ? Tasks[0] : null;
			while (task != null)
			{
				task.Execute();
				if (!task.Check())
				{
					yield return null;
					continue;
				}
				Tasks.Remove(task);
				task = Tasks.Count > 0 ? Tasks[0] : null;
			}
			OnFinish();
			IsFinish = true;
			IsExecute = false;
		}
	}
}