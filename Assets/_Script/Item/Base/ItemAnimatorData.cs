using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ItemAnimatorTargetMode
{
    Self = 0,
    Target = 1,
}

[Serializable]
public class ItemAnimatorData
{
    public ItemAnimatorTargetMode Target;
    [ShowIf("Target", ItemAnimatorTargetMode.Self)] public Animator Animator;
    public string Clip;
    [ShowIf("Target", ItemAnimatorTargetMode.Self)] public string DefaultClip;
}