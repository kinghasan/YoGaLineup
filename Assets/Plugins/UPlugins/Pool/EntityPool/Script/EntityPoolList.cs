/////////////////////////////////////////////////////////////////////////////
//
//  Script   : EntityPoolList.cs
//  Info     : 对象池列表，用于管理单个对象的生成和回收
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Function : 用来快速生成和回收大量的某一个游戏对象，比如连射的子弹。
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;
using System.Collections.Generic;
using Aya.Extension;
using Object = UnityEngine.Object;

namespace Aya.Pool
{
	public class EntityPoolList
	{
		/// <summary>
		/// 生成对象列表
		/// </summary>
		private readonly List<GameObject> _spawnList = new List<GameObject>();

		/// <summary>
		/// 销毁对象列表
		/// </summary>
		private readonly List<GameObject> _deSpawnList = new List<GameObject>();

		/// <summary>
		/// 生成预制数量
		/// </summary>
		public int SpawnPrefabsCount => _spawnList.Count;

	    /// <summary>
		/// 销毁预制数量
		/// </summary>
		public int DeSpawnPrefabsCount => _deSpawnList.Count;

	    /// <summary>
		/// 所有预制数量
		/// </summary>
		public int AllPrefabsCount => _spawnList.Count + _deSpawnList.Count;

	    /// <summary>
		/// 值
		/// </summary>
		public List<GameObject> Values => _spawnList;

	    /// <summary>
		/// 父物体
		/// </summary>
		protected Transform Parent;

		/// <summary>
		/// 构造方法
		/// </summary>
		public EntityPoolList()
		{
			Parent = null;
		}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="parent">父物体</param>
		public EntityPoolList(Transform parent)
		{
			Parent = parent;
		}

        #region Spawn

        /// <summary>
        /// 生成一个实例并获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>组件</returns>
        public T Spawn<T>(T prefab, Transform parent = null, Action<T> onSpawn = null) where T : Component
	    {
	        var component = Spawn(prefab, parent, Vector3.zero, onSpawn);
            return component;
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
        public T Spawn<T>(T prefab, Transform parent = null, Vector3 position = default(Vector3), Action<T> onSpawn = null) where T : Component
        {
            var ins = Spawn(prefab.gameObject, parent, position, onSpawn);
            var component = ins.GetComponent<T>();
            return component;
        }

        /// <summary>
        /// 生成一个实例并获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>组件</returns>
        public T Spawn<T>(GameObject prefab, Transform parent, Action<T> onSpawn = null) where T : Component
	    {
	        var component = Spawn(prefab, parent, Vector3.zero, onSpawn);
	        return component;
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
        public T Spawn<T>(GameObject prefab, Transform parent, Vector3 position, Action<T> onSpawn = null) where T : Component
        {
            var ins = Spawn(prefab, parent, position);
            var component = ins.GetComponent<T>();
            onSpawn?.Invoke(component);
            return component;
        }

        #endregion

        #region Spawn Impl

        /// <summary>
        /// 生成实例
        /// </summary>
        /// <param name="prefab">预制</param>
        /// <param name="parent">父物体</param>
        /// <param name="position">生成位置</param>
        /// <param name="onSpawn">生成回调</param>
        /// <returns>实例</returns>
        public GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Action<GameObject> onSpawn = null)
        {
            GameObject ins;
            if (_deSpawnList.Count > 0)
            {
                // 从对象池取出
                ins = _deSpawnList.GetAndRemove(0);
                if (parent != null && ins.transform.parent != parent)
                {
                    ins.transform.SetParent(parent, false);
                }
            }
            else
            {
                // 对象池空，实例化新对象
                if (parent == null)
                {
                    ins = Object.Instantiate(prefab, position, Quaternion.identity);
                }
                else
                {
                    ins = Object.Instantiate(prefab, position, Quaternion.identity, parent);
                }
                ins.name = prefab.name + "_" + (_spawnList.Count + 1);
                if (Parent != null && parent == null)
                {
                    ins.transform.SetParent(Parent, false);
                }
            }
            // 重设位置 / 缩放
            if (parent != null)
            {
                ins.transform.localPosition = position;
            }
            else
            {
                ins.transform.position = position;
            }
            ins.transform.localScale = Vector3.one;
            ins.transform.localRotation = Quaternion.identity;
            ins.SetActive(true);
            _spawnList.Add(ins);
            onSpawn?.Invoke(ins);
            return ins;
        }

        #endregion

        #region DeSpawn

        /// <summary>
        /// 销毁实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ins">对象实例</param>
        /// <param name="destroy">是否销毁</param>
        public void DeSpawn<T>(T ins, bool destroy = false) where T : Component
        {
            DeSpawn(ins.gameObject);
        }

        /// <summary>
        /// 销毁实例
        /// </summary>
        /// <param name="ins">对象实例</param>
        /// <param name="destroy">是否销毁</param>
        public void DeSpawn(GameObject ins, bool destroy = false)
        {
            if (!_spawnList.Contains(ins)) return;
            if (_deSpawnList.Contains(ins)) return;
            _spawnList.Remove(ins);
            if (destroy)
            {
                Object.Destroy(ins);
            }
            else
            {
                _deSpawnList.Add(ins);
                ins.transform.SetParent(Parent);
                ins.SetActive(false);
            }
        }

        /// <summary>
        /// 回收所有实例
        /// </summary>
        /// <param name="destroy">是否销毁</param>
        public void DeSpawnAll(bool destroy = false)
        {
            for (var i = _spawnList.Count - 1; i >= 0; i--)
            {
                var ins = _spawnList[i];
                DeSpawn(ins, destroy);
            }
        } 

        #endregion

        /// <summary>
        /// 是否包含实例
        /// </summary>
        /// <param name="ins">实例</param>
        /// <returns>结果</returns>
        public bool Contains(GameObject ins)
	    {
	        for (var i = 0; i < _spawnList.Count; i++)
	        {
	            var obj = _spawnList[i];
	            if (obj == ins) return true;
	        }
	        for (var i = 0; i < _deSpawnList.Count; i++)
	        {
	            var obj = _deSpawnList[i];
	            if (obj == ins) return true;
	        }
	        return false;
	    }

        /// <summary>
        /// 自动销毁（列表中已存在的最初生成的）
        /// </summary>
        public void AutoDestroy()
		{
			GameObject ins = null;
			if (_deSpawnList.Count > 0)
			{
				ins = _deSpawnList[0];
				_deSpawnList.Remove(ins);
			}
			else if (_spawnList.Count > 0)
			{
				ins = _spawnList[0];
				_spawnList.Remove(ins);
			}
			if (ins != null)
			{
				Object.Destroy(ins);
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		/// <param name="destroy">是否销毁</param>
		public void Clear(bool destroy = true)
		{
			if (destroy)
			{
				foreach (var ins in _spawnList)
				{
					Object.Destroy(ins);
				}
				foreach (var ins in _deSpawnList)
				{
					Object.Destroy(ins);
				}
			}
			_spawnList.Clear();
			_deSpawnList.Clear();
		}
	}
}
