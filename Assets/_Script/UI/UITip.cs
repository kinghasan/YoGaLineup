using Aya.Pool;
using UnityEngine;

public class UITip : GameEntity<UITip>
{
    public Camera TargetCamera;
    public UITipItem DefaultTipPrefab;

    public EntityPool Pool => PoolManager.Ins["Tip"];

    public UITipItem ShowTip()
    {
        var prefab = DefaultTipPrefab;
        return ShowTipWithUiPos(prefab, Vector3.zero);
    }

    public UITipItem ShowTip(Vector3 position)
    {
        var prefab = DefaultTipPrefab;
        return ShowTip(prefab, position);
    }

    public UITipItem ShowTip(UITipItem tipPrefab, Vector3 position)
    {
        var uiPosition = WorldToUiPos(position, TargetCamera);
        return ShowTipWithUiPos(tipPrefab, uiPosition);
    }

    public UITipItem ShowTipWithUiPos(UITipItem tipPrefab, Vector3 uiPosition)
    {
        var tip = Pool.Spawn(tipPrefab, transform, uiPosition);
        tip.Rect.anchoredPosition = uiPosition;
        tip.Show();
        return tip;
    }

    public Vector3 WorldToUiPos(Vector3 worldPosition, Camera targetCamera)
    {
        var uiPosition = RectTransformUtility.WorldToScreenPoint(targetCamera, worldPosition);
        return uiPosition;
    }
}
