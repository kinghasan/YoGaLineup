/////////////////////////////////////////////////////////////////////////////
//
//  Script : MonoSingleton.cs
//  Info   : MonoBehaviour 单例基类
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Singleton
{
    public abstract class MonoSingleton : MonoBehaviour, ISingleton
    {
        public Type Type { get; protected set; }
    }

    public abstract class MonoSingleton<T> : MonoSingleton where T : MonoSingleton<T>
    {
        protected static T Instance;
        protected static bool IsAppQuiting = false;

        public static T Ins
        {
            get
            {
                if (IsAppQuiting || !Application.isPlaying)
                {
                    return null;
                }
                if (Instance == null)
                {
                    Instance = SingletonManager.GetMonoSingleton<T>();
                }
                return Instance;
            }
        }

        protected MonoSingleton()
        {
            IsAppQuiting = false;
        }

        protected virtual void Awake()
        {
            if (Application.isPlaying && transform.root == transform)
            {
                DontDestroyOnLoad(gameObject);
            }
            IsAppQuiting = false;
            Type = typeof(T);
            SingletonManager.RegisterMonoSingleton(this);
            if (Instance == null)
            {
                Instance = this as T;
            }
        }

	    protected virtual void OnDestroy()
	    {
	        SingletonManager.RegisterMonoSingleton(this);
            Instance = null;
		    IsAppQuiting = true;
	    }
    }
}