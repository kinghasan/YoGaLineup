/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ObjectPool.cs
//  Info     : 对象池
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
    public class ObjectPool<TSource, TResult> : ObjectPool<TSource, TResult, ObjectPoolList<TSource, TResult>>
        where TSource : class, new()
        where TResult : class, new()
    {
    }

    public class ObjectPool<TSource, TResult, TPoolList> : IPool<TSource, TResult, TPoolList> 
        where TSource : class, new() 
        where TResult : class, new()
        where TPoolList : ObjectPoolList<TSource, TResult>, new()
    {
        public Action<TResult> OnSpawn { get; } = delegate { };
        public Action<TResult> OnDeSpawn { get; } = delegate { };

        public Dictionary<TSource, TPoolList> Cache { get; }
        public int Count { get; protected set; } = 0;
        public int SpawnCount { get; protected set; } = 0;
        public int DeSpawnCount { get; protected set; } = 0;

        public ObjectPool()
        {
            Cache = new Dictionary<TSource, TPoolList>();
        }

        protected virtual ObjectPoolList<TSource, TResult> GetPoolList(TSource source)
        {
            if (Cache.TryGetValue(source, out var poolList)) return poolList;
            poolList = new TPoolList();
            Cache.Add(source, poolList);
            return poolList;
        }

        public virtual TResult Spawn(TSource source)
        {
            var poolList = GetPoolList(source);
            var instance = poolList.Spawn(source);
            OnSpawn(instance);
            return instance;
        }

        public virtual T Spawn<T>(TSource source) where T : class, new()
        {
            var instance = Spawn(source);
            var result = instance as T;
            return result;
        }

        public virtual void DeSpawn(TResult instance, bool destroy = false)
        {
            foreach (var poolList in Cache.Values)
            {
                for (var i = poolList.SpawnList.Count - 1; i >= 0 ; i--)
                {
                    var value = poolList.SpawnList[i];
                    if (value != instance) continue;
                    OnDeSpawn(value);
                    poolList.DeSpawn(instance, destroy);
                    return;
                }
            }
        }

        public virtual void DeSpawnAll(bool destroy = false)
        {
            foreach (var poolList in Cache.Values)
            {
                poolList.DeSpawnAll(destroy);
            }
        }
    }
}
