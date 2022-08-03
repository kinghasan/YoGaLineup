using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : UIBase
{
    public override void Show(params object[] args)
    {
        base.Show(args);
    }

    public override void Hide()
    {
        base.Hide();
    }

    public virtual void Refresh()
    {

    }

    public virtual void Back()
    {

    }
}

public abstract class UiWindow<T> : UIWindow where T : UiWindow<T>
{
    public static T Ins { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        Ins = this as T;
    }
}