/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IPoolList.cs
//  Info     : 对象池列表接口定义
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////

namespace Aya.Pool
{
    public interface IPoolList<in TSource, TResult> 
        where TSource : class, new() 
        where TResult : class, new()
    {
        int Count { get; }
        int SpawnCount { get; }
        int DeSpawnCount { get; }

        TResult Instantiate(TSource source);
        void Destroy(TResult instance);

        TResult Spawn(TSource source);
        T Spawn<T>(TSource source) where T : class, new();

        void DeSpawn(TResult instance, bool destroy = false);
        void DeSpawnAll(bool destroy = false);
    }
}
