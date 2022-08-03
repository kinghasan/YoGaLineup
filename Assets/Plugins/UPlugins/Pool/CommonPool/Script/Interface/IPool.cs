/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IPool.cs
//  Info     : 对象池接口定义
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
    public interface IPool<TSource, TResult, TPoolList> 
        where TSource : class, new() 
        where TResult : class, new()
        where TPoolList : IPoolList<TSource, TResult>, new()
    {
        Action<TResult> OnSpawn { get; }
        Action<TResult> OnDeSpawn { get; }

        Dictionary<TSource, TPoolList> Cache { get; }

        int Count { get; }
        int SpawnCount { get; }
        int DeSpawnCount { get; }

        TResult Spawn(TSource source);
        T Spawn<T>(TSource source) where T : class, new();

        void DeSpawn(TResult instance, bool destroy = false);
        void DeSpawnAll(bool destroy = false);
    }
}
