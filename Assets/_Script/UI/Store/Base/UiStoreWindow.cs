using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;
using UnityEngine.UI;

public abstract class UiStoreWindow<TUI, TItem, TData> : UiWindow<TUI>
    where TUI : UiStoreWindow<TUI, TItem, TData>
    where TItem : UIStoreItemBase<TData>
    where TData : StoreData
{
    public IStoreSetting<TData> Setting { get; set; }

    [Header("Item")]
    public TItem ItemPrefab;
    public Transform ItemTrans;
    [Header("Button")]
    public Button BtnBuy;
    public Text TextCost;
    [Header("Buy")]
    public float BuyAnimaInterval = 0.25f;

    public List<TItem> ItemInsList { get; set; } = new List<TItem>();

    public abstract List<TData> DataSources { get; }

    public override void Show(params object[] args)
    {
        base.Show(args);

        foreach (var uiShopItem in ItemInsList)
        {
            UIPool.DeSpawn(uiShopItem.gameObject);
        }

        ItemInsList.Clear();

        foreach (var storeData in DataSources)
        {
            if (!storeData.Show) continue;
            var item = UIPool.Spawn(ItemPrefab, ItemTrans);
            item.Init(storeData);
            ItemInsList.Add(item);
        }

        Refresh();
    }

    public override void Refresh()
    {
        foreach (var item in ItemInsList)
        {
            item.Refresh();
        }

        var canBuyItems = DataSources.FindAll(a => !a.IsUnLock);
        var canBuy = canBuyItems.Count > 0;
        BtnBuy.gameObject.SetActive(canBuy);
        if (canBuy)
        {
            var cost = canBuyItems.First().Cost;
            var enough = canBuyItems.First().IsCoinEnough;
            BtnBuy.enabled = enough;
            TextCost.text = cost.ToString();
        }
    }

    public override void Back()
    {
        UI.ShowWindow<UIReady>();
    }

    public void Buy()
    {
        if (BtnBuy.isActiveAndEnabled)
        {
            StartCoroutine(BuyCoroutine());
        }
    }

    IEnumerator BuyCoroutine()
    {
        BtnBuy.gameObject.SetActive(false);
        var interval = BuyAnimaInterval;
        var canBuyItems = ItemInsList.FindAll(a => !a.Data.IsUnLock);
        if (canBuyItems.Count == 0) yield break;

        var count = canBuyItems.Count * 2f;
        var randList = new List<TItem>();
        canBuyItems.RandSort();
        randList.AddRange(canBuyItems);
        canBuyItems.RandSort();
        randList.AddRange(canBuyItems);

        foreach (var item in randList)
        {
            item.Highlight.gameObject.SetActive(true);
            yield return new WaitForSeconds(interval);
            item.Highlight.gameObject.SetActive(false);
        }

        var buyItem = randList.Last();
        buyItem.Data.Unlock();
        buyItem.Data.Buy();
        buyItem.Select();
        Refresh();

        yield return null;
    }
}
