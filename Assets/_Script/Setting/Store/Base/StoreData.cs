using System;
using Aya.Analysis;
using Aya.Data.Persistent;
using Aya.Extension;
using UnityEngine;

[Serializable]
public abstract class StoreData
{
    public int ID { get; set; }
    public Sprite Icon;
    public Sprite UnGetIcon;

    public bool PreUnlock;
    public bool PreBuy;
    public bool Show = true;

    public IStoreSetting<StoreData> Setting { get; set; }
    public abstract int Cost { get; }
    public abstract UnlockMode UnlockMode { get; }
    public abstract int UnlockValue { get; }

    [NonSerialized] public sBool IsUnLock;
    [NonSerialized] public sBool IsBuy;
    [NonSerialized] public sBool IsShow;

    public bool CanUnlock => Setting.UnlockProgress >= 100;
    public bool IsSelected => Setting.SelectIndex == ID;
    public bool IsCoinEnough => SaveManager.Ins.Coin >= Cost;
    public bool NeedShow => IsUnLock && IsBuy && !IsShow;

    public virtual string SaveKey => GetType().Name.Replace("Data", "");

    public virtual void Init()
    {
        IsUnLock = new sBool(SaveKey + "_" + ID + "_Unlock", PreUnlock);
        IsBuy = new sBool(SaveKey + "_" + ID + "_Buy", PreBuy);
        IsShow = new sBool(SaveKey + "_" + ID + "_Show", IsUnLock && IsBuy);
    }

    public virtual bool Unlock()
    {
        if (IsUnLock) return false;
        AnalysisManager.Instance.Event($"Unlock {SaveKey} {ID}");
        IsUnLock.Value = true;
        Setting.PreNextUnlock();
        return true;
    }

    public virtual void Select()
    {
        if (Setting.SelectIndex == ID) return;
        AnalysisManager.Instance.Event($"Select {SaveKey} {ID}");
        Setting.SelectIndex = ID;
        if (!IsShow) IsShow.Value = true;
    }

    public virtual bool Buy(bool needCost = true)
    {
        if (IsBuy) return false;
        if (needCost)
        {
            if (IsCoinEnough)
            {
                SaveManager.Ins.Coin.Value -= Cost;
                Setting.BuyCount++;
            }
            else
            {
                return false;
            }
        }

        AnalysisManager.Instance.Event($"Buy {SaveKey} {ID}");
        IsBuy.Value = true;
        return true;
    }
}
