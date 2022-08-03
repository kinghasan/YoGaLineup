/////////////////////////////////////////////////////////////////////////////
//
//  Script : Singleton.cs
//  Info   : CSharp 单例基类
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Singleton
{
    public abstract class Singleton : ISingleton
    {
        public Type Type { get; protected set; }
    }

    public abstract class Singleton<T> : Singleton where T : Singleton<T>, new()
    {
        protected static T Instance;
        protected static readonly object InstanceLock = new object();

        public static T Ins
        {
            get
            {
                lock (InstanceLock)
                {
                    if (Instance == null)
                    {
                        Instance = SingletonManager.GetSingleton<T>();
                    }
                }
                return Instance;
            }
        }

        protected Singleton()
        {
            Type = typeof(T);
            SingletonManager.RegisterSingleton(this);
        }

        ~Singleton()
        {
            SingletonManager.DeRegisterSingleton(this);
        }

        public virtual void Dispose()
        {
            Instance = null;
        }
    }
}