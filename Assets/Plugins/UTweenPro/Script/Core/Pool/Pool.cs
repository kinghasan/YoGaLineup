using System;
using System.Collections.Generic;

namespace Aya.TweenPro
{
    public static class Pool
    {
        internal static Dictionary<Type, PoolList> PoolListDic = new Dictionary<Type, PoolList>();

        public static T Spawn<T>()
        {
            var poolList = GetPoolList<T>();
            var instance = (T)poolList.Spawn();
            return instance;
        }

        public static object Spawn(Type type)
        {
            var poolList = GetPoolList(type);
            var instance = poolList.Spawn();
            return instance;
        }

        public static void DeSpawn(object instance)
        {
            if (instance == null) return;
            var type = instance.GetType();
            var poolList = GetPoolList(type);
            poolList.DeSpawn(instance);
        }

        public static PoolList GetPoolList<T>()
        {
            var type = typeof(T);
            var poolList = GetPoolList(type);
            return poolList;
        }

        public static PoolList GetPoolList(Type type)
        {
            if (!PoolListDic.TryGetValue(type, out var poolList))
            {
                poolList= new PoolList(type);
                PoolListDic.Add(type, poolList);
            }

            return poolList;
        }
    }
}