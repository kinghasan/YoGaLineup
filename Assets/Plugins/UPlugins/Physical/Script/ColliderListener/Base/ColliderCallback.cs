/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ColliderCallback.cs
//  Info     : 碰撞回调 用于维护一组碰撞过滤器
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Physical
{
    public class ColliderCallback<T>
    {
        internal List<ColliderFilter<T>> Filters = new List<ColliderFilter<T>>();

        public ColliderFilter<T> Add<TComponent>(Action<TComponent> callback) where TComponent : Component
        {
            return Add(typeof(TComponent), null, callback);
        }

        public ColliderFilter<T> Add<TComponent>(LayerMask? layerMask, Action<TComponent> callback) where TComponent : Component
        {
            return Add(typeof(TComponent), layerMask, callback);
        }

        public ColliderFilter<T> Add<TComponent>(Type type, Action<TComponent> callback) where TComponent : Component
        {
            return Add(type, null, callback);
        }

        public ColliderFilter<T> Add<TComponent>(Type type, LayerMask? layerMask, Action<TComponent> callback) where TComponent : Component
        {
            var filter = new ColliderFilter<T>()
            {
                Type = type,
                Layer = layerMask,
                Callback = obj => { callback(obj as TComponent); },
                ActionHashCode = callback.GetHashCode()
            };

            Filters.Add(filter);
            return filter;
        }

        public void Remove<TComponent>(Action<TComponent> callback) where TComponent : Component
        {
            for (var i = Filters.Count - 1; i >= 0; i--)
            {
                var filter = Filters[i];
                if (filter.Callback == null) continue;
                if (filter.GetHashCode() == callback.GetHashCode())
                {
                    Filters.Remove(filter);
                }
            }
        }

        public void Clear()
        {
            Filters.Clear();
        }

        public void Trigger(T target)
        {
            if (target == null) return;

            if (target is Collider collider)
            {
                foreach (var filter in Filters)
                {
                    var component = collider.gameObject.GetComponentInParent(filter.Type);
                    TriggerComponent(collider.gameObject, component, filter);
                }
            }

            if (target is Collision collision)
            {
                foreach (var filter in Filters)
                {
                    var component = collision.gameObject.GetComponentInParent(filter.Type);
                    TriggerComponent(collision.gameObject, component, filter);
                }
            }

            if (target is Collider2D collider2D)
            {
                foreach (var filter in Filters)
                {
                    var component = collider2D.gameObject.GetComponentInParent(filter.Type);
                    TriggerComponent(collider2D.gameObject, component, filter);
                }
            }

            if (target is Collision2D collision2D)
            {
                foreach (var filter in Filters)
                {
                    var component = collision2D.gameObject.GetComponentInParent(filter.Type);
                    TriggerComponent(collision2D.gameObject, component, filter);
                }
            }
        }

        internal void TriggerComponent(GameObject gameObject, Component component, ColliderFilter<T> filter)
        {
            if (component == null) return;

            var action = filter.Callback;
            if (action == null) return;

            if (filter.Layer == null)
            {
                action(component);
            }
            else
            {
                var targetLayer = gameObject.layer;
                var filterLayer = filter.Layer.Value;
                if (((1 << targetLayer) & filterLayer.value) > 0)
                {
                    action(component);
                }
            }
        }
    }
}