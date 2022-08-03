using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    public Player Target;
    public List<BuffBase> BuffList = new List<BuffBase>();
    public Dictionary<Type, BuffBase> BuffDic = new Dictionary<Type, BuffBase>();

    public void Init(Player target)
    {
        Target = target;
        foreach (var buff in BuffList)
        {
            buff.End();
        }
    }

    public void AddBuff<T>(float duration, float[] args, GameObject[] assets = null, AnimationCurve[] curves = null) where T : BuffBase
    {
        AddBuff(typeof(T), duration, args);
    }

    public void AddBuff(Type buffType, float duration, float[] args, GameObject[] assets = null, AnimationCurve[] curves = null)
    {
        if (!BuffDic.TryGetValue(buffType, out var buff))
        {
            buff = Activator.CreateInstance(buffType) as BuffBase;
            if (buff == null) return;
            buff.Target = Target;
            BuffDic.Add(buffType, buff);
            BuffList.Add(buff);
        }

        if (buff.Active)
        {
            buff.Duration += duration;
        }
        else
        {
            buff.Start(duration, args, assets, curves);
        }
    }

    public void StopBuff<T>() where T : BuffBase
    {
        StopBuff(typeof(T));
    }

    public void StopBuff(Type buffType)
    {
        if(BuffDic.TryGetValue(buffType, out var buff))
        {
            if (!buff.Active) return;
            buff.End();
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var buff in BuffList)
        {
            if (!buff.Active) continue;
            buff.Update(deltaTime);
        }
    }
}