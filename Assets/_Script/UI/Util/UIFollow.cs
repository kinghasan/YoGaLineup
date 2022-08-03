using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : GameEntity
{
    public Transform Target;
    public Vector3 Offset;

    private void FixedUpdate()
    {
        if (Target == null) return;
        var position = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, Target.position);
        Rect.position = (Vector3)position + Offset;
    }
}