/////////////////////////////////////////////////////////////////////////////
//
//  Script   : EntityPool.cs
//  Info     : 对象池，用于管理多个对象的对象列表
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;

namespace Aya.Pool
{
    public class EntityPool
    {
        #region Protected

        /// <summary>
        /// 对象池列表字典
        /// </summary>
        protected readonly Dictionary<GameObject, EntityPoolList> PoolListDic = new Dictionary<GameObject, EntityPoolList>();

        /// <summary>
        /// 父物体
        /// </summary>
        protected Transform Parent;

        #endregion

        #region Property

        /// <summary>
        /// 每个对象池保留单个物品的最大数量
        /// </summary>
        public int MaxPrefabs
        {
            get => _maxPrefabs;
            set => _maxPrefabs = Mathf.Clamp(value, -1, 99999);
        }

        private int _maxPrefabs = -1;

        /// <summary>
        /// 自动销毁
        /// </summary>
        public bool AutoDestroy;

        /// <summary>
        /// 自动销毁间隔（每x秒销毁一个）
        /// </summary>
        public float AutoDestroyInterval
        {
            get => _autoDestroyInterval;
            set => _autoDestroyInterval = Mathf.Clamp(value, 0.1f, 99999f);
        }

        private float _autoDestroyInterval = 5f;

        /// <summary>
        /// 自动销毁计时
        /// </summary>
        private float _autoDestroyTimer = 0f;

        /// <summary>
        /// 构造方法
        /// </summary>
        public EntityPool()
        {
            Set(-1, false, 5f);
            Parent = null;
        }

        #endregion

        #region Construct

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="parent">父物体</param>
        /// <param name="maxPrefabs">每个对象最大预制数量(-1为不限制)</param>
        /// <param name="autoDestroy">是否自动销毁</param>
        /// <param name="autoDestroyInterval">自动销毁间隔时间(每x秒一个)</param>
        public EntityPool(Transform parent, int maxPrefabs = -1, bool autoDestroy = false, float autoDestroyInterval = 5f)
        {
            Set(maxPrefabs, autoDestroy, autoDestroyInterval);
            Parent = parent;
        }

        #endregion

        #region Set

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="maxPrefabs">每个对象最大预制数量(-1为不限制)</param>
        /// <param name="autoDestroy">是否自动销毁</param>
        /// <param name="autoDestroyInterval">自动销毁间隔时间(每x秒一个)</param>
        public void Set(int maxPrefabs, bool autoDestroy, float autoDestroyInterval)
        {
            MaxPrefabs = maxPrefabs;
            AutoDestroy = autoDestroy;
            AutoDestroyInterval = autoDestroyInterval;
        }

        /// <summary>
        /// 设置父物体
        /// </summary>
        /// <param name="parent">父物体</param>
        public void SetParent(Transform parent)
        {
            if (parent == null) return;
            Parent = parent;
        }

        #endregion

        #region Index

        /// <summary>
        /// 索引器<para/>
        /// 以名字获取对象池，如果填入第二个参数[预制]，则不存在时会以该名字和预制创建对象池并返回。
        /// </summary>
        /// <param name="prefab">预制</param>
        /// <returns>对象池列表</returns>
        public EntityPoolList this[GameObject prefab]
        {
            get
            {
                var pool = PoolListDic.GetValue(prefab) ?? CreatePoolList(prefab);
                return pool;
            }
        }

        #endregion

        #region PreLoad

        /// <summary>
        /// 预加载指定数量的预制
        /// </summary>
        /// <param name="prefab">预制</param>
        /// <param name="count">数量</param>
        /// <param name="parent">父物体</param>
        /// <param name="onSpawn">生成回调</param>
        public void PreLoad(GameObject prefab, int count = 1, Transform parent = null, Action<GameObject> onSpawn = null)
        {
            if (prefab == null) return;
            var insList = new List<GameObject>();
            for (var i = 0; i < count; i++)
            {
                var ins = Spawn(prefab, parent, Vector3.zero, onSpawn);
                insList.Add(ins);
            }

            for (var i = 0; i < count; i++)
            {
                var ins = insList[i];
                DeSpawn(ins);
            }
        }

        /// <summary>
        /// 预加载指定数量的预制
        /// </summary>
        /// <param name="prefab">预制</param>
        /// <param name="count">数量</param>
        /// <param name="parent">父物体</param>
        /// <param name="yiledInterval">yiled return 间隔个数</param>
        /// <param name="onSpawn">生成回调</param>
        public IEnumerator PreLoadAsync(GameObject prefab, int count = 1, Transform parent = null, int yiledInterval = 10, Action<GameObject> onSpawn = null)
        {
            if (prefab == null) yield break;
            // 测试接口
            var insList = new List<GameObject>();
            for (var i = 0; i < count; i++)
            {
                var ins = Spawn(prefab, parent, Vector3.zero, onSpawn);
                insList.Add(ins);
                if (i % yiledInterval == 0) yield return null;
            }

            for (var i = 0; i < count; i++)
            {
                var ins = insList[i];
                DeSpawn(ins);
                if (i % yiledInterval == 0) yield return null;
            }
        }

        #endregion

        #region Spawn List

        /// <summary>
        /// 生成一组实例并获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="count">数量</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>组件</returns>
        public List<T> SpawnList<T>(T prefab, Transform parent = null, int count = 1, Action<T> onSpawn = null) where T : Component
        {
            var ppol = this[prefab.gameObject];
            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                var ins = ppol.Spawn<T>(prefab.gameObject, parent, onSpawn);
                result.Add(ins);
            }

            return result;
        }

        /// <summary>
        /// 生成一组实例并获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="count">数量</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>组件</returns>
        public List<T> SpawnList<T>(GameObject prefab, Transform parent = null, int count = 1, Action<T> onSpawn = null) where T : Component
        {
            var pool = this[prefab];
            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                var ins = pool.Spawn<T>(prefab, parent, onSpawn);
                result.Add(ins);
            }

            return result;
        }

        /// <summary>
        /// 生成一组实例
        /// </summary>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="count">数量</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>组件</returns>
        public List<GameObject> SpawnList(GameObject prefab, Transform parent = null, int count = 1, Action<GameObject> onSpawn = null)
        {
            var pool = this[prefab];
            var result = new List<GameObject>();
            for (var i = 0; i < count; i++)
            {
                var ins = pool.Spawn(prefab, parent, Vector3.zero, onSpawn);
                result.Add(ins);
            }

            return result;
        }

        #endregion

        #region Spawn

        /// <summary>
        /// 生成一个实例并获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="position">生成位置</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>组件</returns>
        public T Spawn<T>(T prefab, Transform parent = null, Vector3 position = default(Vector3), Action<T> onSpawn = null) where T : Component
        {
            return Spawn<T>(prefab.gameObject, parent, position, onSpawn);
        }

        /// <summary>
        /// 生成一个实例并获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="position">生成位置</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>组件</returns>
        public T Spawn<T>(GameObject prefab, Transform parent = null, Vector3 position = default(Vector3), Action<T> onSpawn = null) where T : Component
        {
            return this[prefab].Spawn<T>(prefab, parent, position, onSpawn);
        }

        /// <summary>
        /// 生成一个实例
        /// </summary>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="position">生成位置</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>实例</returns>
        public GameObject Spawn(GameObject prefab, Transform parent = null, Vector3 position = default(Vector3), Action<GameObject> onSpawn = null)
        {
            return this[prefab].Spawn(prefab, parent, position, onSpawn);
        }

        #endregion

        #region DeSpawn

        /// <summary>
        /// 回收一个实例<para/>
        /// 注意：此接口需遍历对象池，效率较低
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ins">对象实例</param>
        /// <param name="destroy">是否销毁</param>
        public void DeSpawn<T>(T ins, bool destroy = false) where T : Component
        {
            if (ins == null) return;
            DeSpawn(ins.gameObject, destroy);
        }

        /// <summary>
        /// 回收一个实例<para/>
        /// 注意：此接口需遍历对象池，效率较低
        /// </summary>
        /// <param name="ins">实例</param>
        /// <param name="destroy">是否销毁</param>
        public void DeSpawn(GameObject ins, bool destroy = false)
        {
            if (ins == null) return;
            foreach (var pool in PoolListDic.Values)
            {
                for (var i = pool.Values.Count - 1; i >= 0; i--)
                {
                    var value = pool.Values[i];
                    if (value != ins) continue;
                    pool.DeSpawn(ins, destroy);
                    return;
                }
            }
        }

        /// <summary>
        /// 回收所有实例
        /// </summary>
        /// <param name="destroy">是否销毁</param>
        public void DeSpawnAll(bool destroy = false)
        {
            foreach (var pool in PoolListDic.Values)
            {
                pool.DeSpawnAll(destroy);
            }
        }

        #endregion

        #region Contains

        /// <summary>
        /// 是否包含某个实例
        /// </summary>
        /// <param name="ins">实例</param>
        /// <returns>结果</returns>
        public bool Contains(GameObject ins)
        {
            foreach (var pool in PoolListDic.Values)
            {
                var ret = pool.Contains(ins);
                if (ret) return true;
            }

            return false;
        }

        #endregion

        #region Clear

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="destroy">销毁</param>
        public void Clear(bool destroy = true)
        {
            foreach (var pool in PoolListDic.Values)
            {
                pool.Clear(destroy);
            }
        }

        #endregion

        #region Internal

        /// <summary>
        /// 创建一个对象池
        /// </summary>
        /// <param name="prefab">预制</param>
        internal EntityPoolList CreatePoolList(GameObject prefab)
        {
            var pool = PoolListDic.GetValue(prefab);
            var poolList = new EntityPoolList(Parent);
            PoolListDic.Add(prefab, poolList);
            return poolList;
        }

        /// <summary>
        /// 更新，用于维持数量限制和自动销毁，须由PoolManager或用户调用
        /// </summary>
        internal void Update()
        {
            if (MaxPrefabs < 1 && !AutoDestroy) return;
            if (AutoDestroy) _autoDestroyTimer += Time.deltaTime;
            foreach (var pool in PoolListDic.Values)
            {
                if (MaxPrefabs > 0 && pool.AllPrefabsCount > MaxPrefabs)
                {
                    pool.AutoDestroy();
                }

                if (AutoDestroy && pool.AllPrefabsCount > 0 && _autoDestroyTimer > AutoDestroyInterval)
                {
                    _autoDestroyTimer = 0f;
                    pool.AutoDestroy();
                }
            }
        }

        #endregion
    }
}