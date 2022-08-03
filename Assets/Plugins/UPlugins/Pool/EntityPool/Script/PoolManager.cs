/////////////////////////////////////////////////////////////////////////////
//
//  Script   : PoolManager.cs
//  Info     : 对象池管理器，用于通过名字创建多个对象池进行独立管理
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;
using Aya.Singleton;

namespace Aya.Pool
{
    public class PoolManager : MonoSingleton<PoolManager>
	{
        #region Protected
        
	    /// <summary>
        /// 对象池字典
        /// </summary>
        protected Dictionary<string, EntityPool> PoolDic = new Dictionary<string, EntityPool>();
        /// <summary>
        /// 对象池列表
        /// </summary>
        protected List<EntityPool> PoolList = new List<EntityPool>();

        /// <summary>
        /// 挂载点字典
        /// </summary>
        protected Dictionary<string, Transform> TransDic = new Dictionary<string, Transform>();

        #endregion

        #region Cache

        /// <summary>
        /// 默认对象池
        /// </summary>
	    public EntityPool Default => this["Default"]; 
       
	    #endregion

        #region Index

        /// <summary>
        /// 通过名字访问对象池
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <returns>结果</returns>
        public EntityPool this[string poolName]
        {
            get
            {
                var pool = PoolDic.GetValue(poolName) ?? CreatePool(poolName);
                return pool;
            }
        }
        
	    #endregion

        #region Create Pool
        
	    /// <summary>
        /// 创建一个i额新的对象池，如果对对象池的数量和自动销毁等有需求，请使用此接口进行创建。
        /// </summary>
        /// <param name="poolName">对象池名字</param>
        /// <param name="maxPrefabs">每个对象最大预制数量(-1为不限制)</param>
        /// <param name="autoDestory">是否自动销毁</param>
        /// <param name="autoDestoryIntervial">自动销毁间隔时间(每x秒一个)</param>
        /// <returns>结果</returns>
        public EntityPool CreatePool(string poolName, int maxPrefabs = -1, bool autoDestory = false, float autoDestoryIntervial = 5f)
        {
            var pool = PoolDic.GetValue(poolName);
            // 新建挂载点
            var parent = new GameObject { name = poolName };
            parent.transform.SetParent(transform);
            TransDic.Add(poolName, parent.transform);
            // 新建对象池
            pool = new EntityPool(parent.transform, maxPrefabs, autoDestory, autoDestoryIntervial);
            PoolDic.Add(poolName, pool);
            PoolList.Add(pool);
            return pool;
        }
       
	    #endregion

        #region Clear
        
	    /// <summary>
        /// 清空
        /// </summary>
        /// <param name="destroy">是否销毁</param>
        public void Clear(bool destroy = true)
        {
            foreach (var pool in PoolDic.Values)
            {
                pool.Clear(destroy);
            }
            PoolDic.Clear();
            if (destroy)
            {
                foreach (var trans in TransDic.Values)
                {
                    Destroy(trans.gameObject);
                }
            }
            TransDic.Clear();
        }
        
	    #endregion

        #region Monobehaviour
       
	    /// <summary>
        /// 自动更新列表中对象池的状态
        /// </summary>
        private void Update()
        {
            for (var i = 0; i < PoolList.Count; i++)
            {
                var pool = PoolList[i];
                pool.Update();
            }
        } 
        
	    #endregion
    }
}
