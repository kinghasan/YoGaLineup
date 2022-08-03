/////////////////////////////////////////////////////////////////////////////
//
//  Script   : GameObjectExtension.cs
//  Info     : GameObject扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.Extension
{
    public static class GameObjectExtension
    {
        #region Prefab

        public static bool IsPrefab(this GameObject gameObject)
        {
            var result = gameObject.scene.name == null;
            return result;
        }

        #endregion

        #region HasComponent?

        /// <summary>
        /// 是否有刚体
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <returns>结果</returns>
        public static bool HasRigidbody(this GameObject gameObject)
        {
            var result = gameObject.GetComponent<Rigidbody>() != null;
            return result;
        }

        /// <summary>
        /// 是否有动画
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <returns>结果</returns>
        public static bool HasAnimation(this GameObject gameObject)
        {
            var result = gameObject.GetComponent<Animation>() != null;
            return result;
        }

        /// <summary>
        /// 是否有Macanim动画
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <returns>结果</returns>
        public static bool HasAnimator(this GameObject gameObject)
        {
            var result = gameObject.GetComponent<Animator>() != null;
            return result;
        }

        #endregion

        #region Component

        /// <summary>
        /// 尝试获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="gameObject">物体</param>
        /// <param name="outComponent">获取组件结果</param>
        /// <returns>获取成功</returns>
        public static bool TryGetComponent<T>(this GameObject gameObject, out T outComponent)
        { 
            outComponent = gameObject.GetComponent<T>();
            var result = outComponent != null;
            return result;
        }

        /// <summary>
        /// 尝试从父物体获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="gameObject">物体</param>
        /// <param name="outComponent">获取组件结果</param>
        /// <returns>获取成功</returns>
        public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T outComponent)
        {
            outComponent = gameObject.GetComponentInParent<T>();
            var result = outComponent != null;
            return result;
        }

        /// <summary>
        /// 尝试从子物体获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="gameObject">物体</param>
        /// <param name="outComponent">获取组件结果</param>
        /// <returns>获取成功</returns>
        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T outComponent)
        {
            outComponent = gameObject.GetComponentInChildren<T>();
            var result = outComponent != null;
            return result;
        }

        /// <summary>
        /// 获取组件，不存在则添加
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="gameObject">物体</param>
        /// <returns>结果</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// 获取组件，不存在则添加
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="type">组件类型</param>
        /// <returns>结果</returns>
        public static Component GetOrAddComponent(this GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component == null)
            {
                component = gameObject.AddComponent(type);
            }

            return component;
        }

        /// <summary>
        /// 销毁组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="gameObject">物体</param>
        /// <returns>结果</returns>
        public static bool DestroyComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) return false;
            Object.DestroyImmediate(component);
            return true;
        }

        /// <summary>
        /// 销毁组件
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="type">组件类型</param>
        /// <returns>结果</returns>
        public static bool DestroyComponent(this GameObject gameObject, Type type) 
        {
            var component = gameObject.GetComponent(type);
            if (component == null) return false;
            Object.DestroyImmediate(component);
            return true;
        }

        /// <summary>
        /// 搜索指定名字的组件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="gameObject">物体</param>
        /// <param name="searchName">名字</param>
        /// <returns>结果</returns>
        public static T SearchComponent<T>(this GameObject gameObject, string searchName) where T : Component
        {
            var gos = gameObject.GetComponentsInChildren<T>(true);
            var length = gos.Length;
            for (var i = 0; i < length; i++)
            {
                var local = gos[i];
                if (searchName == local.name)
                {
                    return local;
                }
            }

            return null;
        }

        /// <summary>
        /// 向上查找父物体中的组件，返回第一个
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="gameObject">物体</param>
        /// <returns>结果</returns>
        public static T FindComponentInParents<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component != null)
            {
                return component;
            }

            return gameObject.transform.parent != null ? FindComponentInParents<T>(gameObject.transform.parent.gameObject) : null;
        }

        /// <summary>
        /// 向上查找父物体中的组件，返回第一个
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="type">组件类型</param>
        /// <returns>结果</returns>
        public static Component FindComponentInParents(this GameObject gameObject, Type type)
        {
            var component = gameObject.GetComponent(type);
            if (component != null)
            {
                return component;
            }

            return gameObject.transform.parent != null ? FindComponentInParents(gameObject.transform.parent.gameObject, type) : null;
        }

        #endregion

        #region Path

        /// <summary>
        /// 创建指定路径的子节点
        /// </summary>
        /// <param name="gameObject">物体(根节点)</param>
        /// <param name="pathName">路径</param>
        /// <param name="splitChar">分隔符</param>
        /// <returns>创建的结果集合</returns>
        public static GameObject[] CreateChild(this GameObject gameObject, string pathName, char splitChar = '/')
        {
            GameObject obj2 = null;
            var list = new List<GameObject>();
            var separator = new char[] {splitChar};
            for (var i = 0; i < pathName.Split(separator).Length; i++)
            {
                var str = pathName.Split(separator)[i];
                var item = new GameObject(str);
                list.Add(item);
                if (obj2 != null)
                {
                    item.transform.SetParent(obj2.transform);
                }

                obj2 = item;
            }

            list[0].transform.SetParent(gameObject.transform);
            return list.ToArray();
        }

        /// <summary>
        /// 获取物体的完整路径
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <returns>路径</returns>
        public static string Path(this GameObject gameObject)
        {
            var path = "/" + gameObject.name;
            while (gameObject.transform.parent != null)
            {
                gameObject = gameObject.transform.parent.gameObject;
                path = "/" + gameObject.name + path;
            }

            return path;
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <param name="go">当前节点</param>
        /// <returns>结果</returns>
        public static GameObject Root(this GameObject go)
        {
            var current = go;
            GameObject result;
            do
            {
                var trans = current.transform.parent;
                if (trans != null)
                {
                    result = trans.gameObject;
                    current = trans.gameObject;
                }
                else
                {
                    result = current;
                    current = null;
                }
            } while (current != null);

            return result;
        }

        /// <summary>
        /// 获取当前节点在场景中的层次深度（从0开始）
        /// </summary>
        /// <param name="go">当前节点</param>
        /// <returns>深度</returns>
        public static int Depth(this GameObject go)
        {
            var depth = 0;
            var current = go.transform;
            do
            {
                current = current.transform.parent;
                if (current != null)
                {
                    depth++;
                }
            } while (current != null);

            return depth;
        }

        #endregion

        #region Layer

        /// <summary>
        /// 是否包含层级
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="otherLayerIndex">层索引</param>
        /// <returns>结果</returns>
        public static bool ContainLayer(this GameObject gameObject, int otherLayerIndex)
        {
            var value = 1 << gameObject.layer;
            var result = (value & otherLayerIndex) > 0;
            return result;
        }

        /// <summary>
        /// 是否包含层级
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="otherLayerMask">层</param>
        /// <returns>结果</returns>
        public static bool ContainLayer(this GameObject gameObject, LayerMask otherLayerMask)
        {
            var value = 1 << gameObject.layer;
            var result = (value & otherLayerMask.value) > 0;
            return result;
        }

        /// <summary>
        /// 是否包含层级
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="otherLayerMasks">层</param>
        /// <returns>结果</returns>
        public static bool ContainLayers(this GameObject gameObject, params LayerMask[] otherLayerMasks)
        {
            foreach (var layerMask in otherLayerMasks)
            {
                var result = gameObject.ContainLayer(layerMask);
                if (!result) return false;
            }
            return true;
        }

        /// <summary>
        /// 是否包含层级之一
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="otherLayerMasks">层级</param>
        /// <returns>结果</returns>
        public static bool ContainOneOfLayers(this GameObject gameObject, params LayerMask[] otherLayerMasks)
        {
            foreach (var layerMask in otherLayerMasks)
            {
                var result = gameObject.ContainLayer(layerMask);
                if (result) return true;
            }
            return false;
        }

        /// <summary>
        /// 设置层
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="layer">层</param>
        /// <returns>gameObject</returns>
        public static GameObject SetLayer(this GameObject gameObject, LayerMask layer)
        {
            gameObject.layer = layer.GetLayerIndex();
            return gameObject;
        }

        /// <summary>
        /// 递归设置层
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="layer">层</param>
        /// <returns>gameObject</returns>
        public static GameObject SetLayerRecursion(this GameObject gameObject, LayerMask layer)
        {
            gameObject.layer = layer.GetLayerIndex();
            foreach (Transform child in gameObject.transform)
            {
                SetLayerRecursion(child.gameObject, layer);
            }

            return gameObject;
        }

        /// <summary>
        /// 递归通过索引号设置层
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="layerIndex">层索引</param>
        /// <returns>gameObject</returns>
        public static GameObject SetLayerRecursion(this GameObject gameObject, int layerIndex)
        {
            gameObject.layer = layerIndex;
            foreach (Transform child in gameObject.transform)
            {
                SetLayerRecursion(child.gameObject, layerIndex);
            }

            return gameObject;
        }

        #endregion

        #region Tag

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="tag">标签</param>
        /// <returns>gameObject</returns>
        public static GameObject SetTag(this GameObject gameObject, string tag)
        {
            gameObject.tag = tag;
            return gameObject;
        }

        /// <summary>
        /// 递归设置标签
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="tag">标签</param>
        ///  <returns>gameObject</returns>
        public static GameObject SetTagRecursion(this GameObject gameObject, string tag)
        {
            gameObject.tag = tag;
            foreach (Transform child in gameObject.transform)
            {
                SetTagRecursion(child.gameObject, tag);
            }

            return gameObject;
        }

        #endregion

        #region Particle

        /// <summary>
        /// 获取粒子特效的时间*（获取失败位-1，allowLoop = true 时，Loop 特效计算一个周期的时长，否则为-1）
        /// </summary>
        /// <param name="gameObject">特效所在物体或者父节点</param>
        /// <param name="includeChildren">是否遍历子节点计算最长时间</param>
        /// <param name="includeInactive">包含隐藏节点</param>
        /// <param name="allowLoop">是否允许计算循环特效时间</param>
        /// <returns>结果</returns>
        public static float GetParticleDuration(this GameObject gameObject, bool includeChildren = true, bool includeInactive = false, bool allowLoop = false)
        {
            if (includeChildren)
            {
                var particles = gameObject.GetComponentsInChildren<ParticleSystem>(includeInactive);
                var duration = -1f;
                for (var i = 0; i < particles.Length; i++)
                {
                    var ps = particles[i];
                    var time = ps.GetDuration(allowLoop);
                    if (time > duration)
                    {
                        duration = time;
                    }
                }

                return duration;
            }
            else
            {
                var ps = gameObject.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    return ps.GetDuration(allowLoop);
                }
                else
                {
                    return -1f;
                }
            }
        }

        #endregion

        #region Trail Renderer

        /// <summary>
        /// 获取拖尾渲染器的时长
        /// </summary>
        /// <param name="gameObject">拖尾渲染器所在的父节点</param>
        /// <param name="includeChildren">是否遍历子节点计算最长时间</param>
        /// <returns>结果</returns>
        public static float GetTraiRendererTime(this GameObject gameObject, bool includeChildren = true)
        {
            var trailRenderers = gameObject.GetComponentsInChildren<TrailRenderer>();
            var duration = 0f;
            for (var i = 0; i < trailRenderers.Length; i++)
            {
                var trailRenderer = trailRenderers[i];
                var time = trailRenderer.time;
                if (time > duration)
                {
                    duration = time;
                }
            }

            return duration;
        }

        #endregion

        #region Bounds

        /// <summary>
        /// 获取包围盒
        /// </summary>
        /// <param name="gameObject">物体</param>
        /// <param name="includeChildren">是否包含子节点</param>
        /// <returns>包围盒</returns>
        public static Bounds GetBounds(this GameObject gameObject, bool includeChildren = true)
        {
            var renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                return renderer.GetBounds(includeChildren);
            }

            var meshFliter = gameObject.GetComponent<MeshFilter>();
            if (meshFliter != null)
            {
                return meshFliter.GetBounds(includeChildren);
            }

            var bounds = new Bounds(gameObject.transform.position, Vector3.zero);
            return bounds;
        }

        #endregion
    }
}