using UnityEngine;
using UnityEngine.UI;

public class UIHandTip : GameEntity
{
    public Image Target;

    public void Update()
    {
        var click = Input.GetMouseButton(0);
        Target.gameObject.SetActive(click);
        Rect.anchoredPosition = Input.mousePosition;
    }
}
