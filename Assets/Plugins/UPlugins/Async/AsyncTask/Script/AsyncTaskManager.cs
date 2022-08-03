/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AsyncTaskManager.cs
//  Info     : 异步任务管理器，用于承载任务的执行
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Async
{
	public class AsyncTaskManager : MonoBehaviour
	{
	    protected static AsyncTaskManager Instance;

	    public static AsyncTaskManager Ins
	    {
	        get
	        {
	            if (Instance == null)
	            {
	                Instance = (AsyncTaskManager)FindObjectOfType(typeof(AsyncTaskManager));
	                if (Instance == null)
	                {
	                    var obj = new GameObject
	                    {
	                        hideFlags = HideFlags.HideAndDontSave,
	                        name = "AsyncTaskManager"
	                    };
	                    DontDestroyOnLoad(obj);
	                    Instance = obj.AddComponent(typeof(AsyncTaskManager)) as AsyncTaskManager;
	                }
	            }
	            return Instance;
	        }
	    }

	    protected AsyncTaskManager()
	    {
	    }
    }
}
