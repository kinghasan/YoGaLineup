/////////////////////////////////////////////////////////////////////////////
//
//  Script : SingletonManager.cs
//  Info   : 单例管理器
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.Singleton
{
    public static class SingletonManager
    {
        #region Cache
        private static readonly Dictionary<Type, Singleton> SingletonDic = new Dictionary<Type, Singleton>();
        private static readonly Dictionary<Type, MonoSingleton> MonoSingletonDic = new Dictionary<Type, MonoSingleton>();
        #endregion

        #region Singleton

        public static T GetSingleton<T>() where T : Singleton
        {
            T value;
            var type = typeof(T);
            if (SingletonDic.ContainsKey(type))
            {
                value = SingletonDic[type] as T;
            }
            else
            {
                value = CreateSingleton<T>();
            }
            return value;
        }

        internal static T CreateSingleton<T>() where T : class, ISingleton
        {
            // 获取私有构造函数
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            // 获取无参构造函数
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-Public Constructor() not found! in " + typeof(T));
            }

            // 通过构造函数，常见实例
            var retInstance = ctor.Invoke(null) as T;

            return retInstance;
        }

        internal static void RegisterSingleton(Singleton singleton)
        {
            if (SingletonDic.ContainsKey(singleton.Type))
            {
                SingletonDic[singleton.Type] = singleton;
            }
            else
            {
                SingletonDic.Add(singleton.Type, singleton);
            }
        }

        internal static void DeRegisterSingleton(Singleton singleton)
        {
            SingletonDic.Remove(singleton.Type);
        }

        #endregion

        #region MonoSingleton

        public static T GetMonoSingleton<T>() where T : MonoSingleton
        {
            T value;
            var type = typeof(T);
            if (MonoSingletonDic.ContainsKey(type))
            {
                value = MonoSingletonDic[type] as T;
            }
            else
            {
                value = CreateMonoSingleton<T>();
            }
            return value;
        }

        internal static T CreateMonoSingleton<T>() where T : MonoSingleton
        {
            var ins = Object.FindObjectOfType<T>();
            if (ins != null) return ins;
            var obj = new GameObject
            {
                name = typeof(T).Name,
                hideFlags = HideFlags.HideAndDontSave,
            };
            ins = obj.AddComponent<T>();
            Object.DontDestroyOnLoad(ins);
            return ins;
        }

        internal static void RegisterMonoSingleton(MonoSingleton singleton)
        {
            if (!Application.isPlaying) return;
            if (MonoSingletonDic.ContainsKey(singleton.Type))
            {
                MonoSingletonDic[singleton.Type] = singleton;
            }
            else
            {
                MonoSingletonDic.Add(singleton.Type, singleton);
            }
        }

        internal static void DeRegisterMonoSingleton(MonoSingleton singleton)
        {
            if (!Application.isPlaying) return;
            MonoSingletonDic.Remove(singleton.Type);
        } 
        #endregion
    }
}

