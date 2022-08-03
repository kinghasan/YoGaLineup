using System;
using System.Collections.Generic;
using Aya.Extension;
using Aya.Simplify;
using UnityEngine;

public abstract class EntityListRender<T> : GameEntity where T : GameEntity
{
    public T Prefab;
    public Transform RenderTrans;

    public List<T> InsList { get; set; } = new List<T>();

    public virtual void Init()
    {
        DeSpawnAll();
    }

    public virtual T Spawn(Action<T> onSpawned = null)
    {
        var ins = GamePool.Spawn(Prefab, RenderTrans);
        InsList.Add(ins);
        Refresh();
        onSpawned?.Invoke(ins);
        return ins;
    }

    public virtual List<T> Spawn(int count, Action<T> onSpawned = null)
    {
        var result = new List<T>();
        Loop.For(count, i =>
        {
            result.Add(Spawn(onSpawned));
        });

        return result;
    }

    public virtual void DeSpawn()
    {
        if (InsList.Count == 0) return;
        var ins = InsList.Last();
        GamePool.DeSpawn(ins);
        InsList.Remove(ins);
        Refresh();
    }

    public virtual void DeSpawn(int count)
    {
        Loop.For(count, i =>
        {
            DeSpawn();
        });
    }

    public virtual void DeSpawnAll()
    {
        DeSpawn(InsList.Count);
    }

    public virtual void Refresh()
    {
        for (var i = 0; i < InsList.Count; i++)
        {
            var ins = InsList[i];
            var (pos, rot, scale) = GetTransInfo(i);
            ins.Position = pos;
            ins.EulerAngles = rot;
            ins.LocalScale = scale;
        }
    }

    public virtual (Vector3, Vector3, Vector3) GetTransInfo(int index)
    {
        var pos = RenderTrans.position;
        var rot = Vector3.zero;
        var scale = Vector3.one;
        return (pos, rot, scale);
    }
}
