using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : GameEntity
{
    public virtual void Show(params object[] args)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
