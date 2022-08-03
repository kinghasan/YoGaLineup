using System;
using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public abstract class ComponentBase<T> : GameEntity where T : ComponentBase<T>
{
    public Dictionary<Type, T> ComponentDic { get; set; } = new Dictionary<Type, T>();

    public virtual void InitAllComponent()
    {
        foreach (var component in ComponentDic.Values)
        {
            component.InitComponent();
        }
        InitAnimator();
    }

    public virtual void InitComponent()
    {

    }

    public override void CacheComponent()
    {
        base.CacheComponent();

        ComponentDic.Clear();
        var properties = this.GetPropertiesWithAttribute<SubComponentAttribute>();
        foreach (var property in properties)
        {
            var component = GetComponent(property.PropertyType);
            property.SetValue(this, component);
            ComponentDic.Add(property.PropertyType, component as T);
        }
    }

    public TComponent Get<TComponent>() where TComponent : T
    {
        var type = typeof(TComponent);
        if (ComponentDic.TryGetValue(type, out var component))
        {
            return component as TComponent;
        }

        return default;
    }
}