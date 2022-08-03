/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ObjectPoolList.cs
//  Info     : 对象池列表
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Pool
{
    public class ObjectPoolList<TSource, TResult> : IPoolList<TSource, TResult>
        where TSource : class, new() 
        where TResult : class, new()
    {
        internal List<TResult> SpawnList { get; }
        internal List<TResult> DeSpawnList { get; }

        public int Count => SpawnCount + DeSpawnCount;
        public int SpawnCount => SpawnList.Count;
        public int DeSpawnCount => DeSpawnList.Count;

        public ObjectPoolList()
        {
            SpawnList = new List<TResult>();
            DeSpawnList = new List<TResult>();
        }

        public virtual TResult Instantiate(TSource source)
        {
            var instance = Activator.CreateInstance<TSource>();
            var result = instance as TResult;
            return result;
        }

        public virtual void Destroy(TResult instance)
        {
            
        }

        public virtual TResult Spawn(TSource source)
        {
            if (DeSpawnList.Count > 0)
            {
                var index = DeSpawnList.Count - 1;
                var result = DeSpawnList[index];
                DeSpawnList.RemoveAt(index);
                SpawnList.Add(result);
                return result;
            }
            else
            {
                var instance = Instantiate(source);
                SpawnList.Add(instance);
                return instance;
            }
        }

        public virtual T Spawn<T>(TSource source) where T : class, new()
        {
            var instance = Spawn(source);
            var result = instance as T;
            return result;
        }

        public virtual void DeSpawn(TResult instance, bool destroy = false)
        {
            if(!SpawnList.Contains(instance)) return;
            SpawnList.Remove(instance);
            if (destroy)
            {
                Destroy(instance);
            }
            else
            {
                DeSpawnList.Add(instance);
            }
        }

        public virtual void DeSpawnAll(bool destroy = false)
        {
            for (var i = SpawnList.Count - 1; i >= 0; i--)
            {
                var instance = SpawnList[i];
                DeSpawn(instance, destroy);
            }
        }
    }
}
