using System;
using System.Collections.Generic;

namespace Aya.TweenPro
{
    public class PoolList
    {
        public Type Type { get; }

        internal List<object> SpawnList = new List<object>();
        internal List<object> DeSpawnList = new List<object>();

        public int ActiveCount => SpawnList.Count;
        public int DeActiveCount => DeSpawnList.Count;
        public int Count => ActiveCount + DeActiveCount;

        public PoolList(Type type)
        {
            Type = type;
        }

        public object Spawn()
        {
            object instance;
            if (DeSpawnList.Count > 0)
            {
                instance = DeSpawnList[DeSpawnList.Count - 1];
                DeSpawnList.Remove(instance);
            }
            else
            {
                instance = Activator.CreateInstance(Type);
            }

            SpawnList.Add(instance);
            return instance;
        }

        public void DeSpawn(object instance)
        {
            if (instance == null) return;
            if (!SpawnList.Contains(instance)) return;
            SpawnList.Remove(instance);
            DeSpawnList.Add(instance);
        }
    }
}