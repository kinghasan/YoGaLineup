using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CostUpgradeData : UpgradeData
{
    public int Cost;

    public virtual bool Enough => SaveManager.Ins.Coin >= Cost;

    public override bool CanUpgrade()
    {
        return Enough;
    }

    public override void Upgrade()
    {
        SaveManager.Ins.Coin.Value -= Cost;
    }
}
