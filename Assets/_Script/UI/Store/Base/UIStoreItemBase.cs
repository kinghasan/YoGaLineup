using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIStoreItemBase<TData> : GameEntity
    where TData : StoreData
{
    public TData Data { get; set; }

    [Header("State")]
    public Image Icon;
    public Image UnGetIcon;
    public GameObject Locked;
    public GameObject UnBuy;
    public GameObject Highlight;
    public GameObject SelectState;
    [Header("Button")]
    public Button UnlockBtn;
    public Button RewardUnlockBtn;
    public Button BuyBtn;
    public Button RewardBuyBtn;

    public virtual string SaveKey => typeof(TData).Name.Replace("Data", "");

    public virtual void Init(TData data)
    {
        Data = data;
    }

    public virtual void Refresh()
    {
        Highlight.gameObject.SetActive(false);
        SelectState.gameObject.SetActive(Data.IsSelected);

        Icon.sprite = Data.Icon;
        UnGetIcon.sprite = Data.UnGetIcon;
        Locked?.gameObject.SetActive(!Data.IsUnLock);
        UnBuy?.gameObject.SetActive(!Data.IsBuy);

        UnlockBtn?.gameObject.SetActive(!Data.IsUnLock);
        RewardUnlockBtn?.gameObject.SetActive(!Data.IsUnLock && SDKUtil.IsRewardVideoReady(SaveKey));

        BuyBtn?.gameObject.SetActive(Data.IsUnLock && !Data.IsBuy);
        RewardBuyBtn?.gameObject.SetActive(Data.IsUnLock && !Data.IsBuy && SDKUtil.IsRewardVideoReady(SaveKey));
    }

    public virtual void Select()
    {
        if (!Data.IsBuy) return;
        Data.Select();
        UIController.Ins.Current.Refresh();
    }

    public virtual void Unlock()
    {
        var success = Data.Unlock();
        if (success)
        {
            Select();
        }
    }

    public virtual void Buy()
    {
        var success = Data.Buy(true);
        if (success)
        {
            Select();
        }
    }

    public virtual void RewardUnlock()
    {
        SDKUtil.RewardVideo($"Unlock {SaveKey}", () =>
        {
            var success = Data.Unlock();
            if (success)
            {
                Select();
            }
        });
    }

    public virtual void RewardBuy()
    {
        SDKUtil.RewardVideo($"Buy {SaveKey}", () =>
        {
            var success = Data.Buy(false);
            if (success)
            {
                Select();
            }
        });
    }
}