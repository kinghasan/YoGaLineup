using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeData
{
    public int ID;
    public int Value;
    public int IntValue => Mathf.RoundToInt(Value);

    public virtual bool CanUpgrade()
    {
        return true;
    }

    public virtual void Upgrade()
    {

    }
}
