using Aya.Extension;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControl : PlayerBase
{
    public float AnimationScale { get; set; }
    [HideInInspector]
    public List<string> _yogaList = new List<string>();
    [HideInInspector]
    /// <summary>
    /// 当前的瑜伽动作
    /// </summary>
    public int _yogaIndex;
    [HideInInspector]
    /// <summary>
    /// 目标瑜伽动作
    /// </summary>
    public int _targetIndex;
    public virtual void Update()
    {
        var deltaTime = DeltaTime;
        if (Game.GamePhase != GamePhaseType.Gaming) return;
        if (!State.EnableInput) return;
        UpdateImpl(deltaTime);
    }

    public virtual void InitYoga() { }

    public virtual void UpdateImpl(float deltaTime)
    {

    }
}
