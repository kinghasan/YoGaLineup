using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISceneItem : UIBase
{
    public GameEntity Target { get; set; }
    public Vector3 Offset;

    public virtual void Show(GameEntity target, params object[] args)
    {
        base.Show(args);
        Target = target;
    }

    public override void Hide()
    {
        base.Hide();
        Target = null;
    }

    public virtual void LateUpdate()
    {
        if (Target == null) return;
        var position = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, Target.Position);
        Rect.position = (Vector3)position + Offset;
    }
}

public abstract class UISceneItem<TTarget> : UISceneItem where TTarget : GameEntity
{
    public TTarget TargetGeneric => Target as TTarget;

    public override void Show(GameEntity target, params object[] args)
    {
        base.Show(args);
        Target = target as TTarget;
    }
}