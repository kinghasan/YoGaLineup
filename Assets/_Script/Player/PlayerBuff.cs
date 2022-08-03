using System;
using UnityEngine;

public class PlayerBuff : PlayerBase
{
    public BuffManager BuffManager { get; set; } = new BuffManager();

    public override void InitComponent()
    {
        BuffManager.Init(Self);
    }

    public void AddBuff<T>(float duration, float[] args, GameObject[] assets = null, AnimationCurve[] curves = null) where T : BuffBase
    {
        BuffManager.AddBuff<T>(duration, args, assets, curves);
    }

    public void AddBuff(Type buffType, float duration, float[] args, GameObject[] assets = null, AnimationCurve[] curves = null)
    {
        BuffManager.AddBuff(buffType, duration, args, assets, curves);
    }

    public void StopBuff<T>() where T : BuffBase
    {
        BuffManager.StopBuff<T>();
    }

    public void StopBuff(Type buffType)
    {
        BuffManager.StopBuff(buffType);
    }

    public void Update()
    {
        var deltaTime = DeltaTime;
        if (Game.GamePhase != GamePhaseType.Gaming) return;
        BuffManager.Update(deltaTime);
    }
}