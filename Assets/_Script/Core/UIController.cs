using System;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public class UIController : GameEntity<UIController>
{
    public new Camera Camera;

    public List<UISceneItem> SceneItemList { get; set; } = new List<UISceneItem>();

    protected override void Awake()
    {
        base.Awake();

        var uis = transform.GetComponentsInChildren<UIWindow>(true);
        foreach (var ui in uis)
        {
            WindowDic.Add(ui.GetType(), ui);
            ui.gameObject.SetActive(false);
        }

        HideAllWindow();
    }

    public void HideAll()
    {
        HideAllWindow();
        HideAllSceneItem();
    }

    #region Window

    public UIWindow Current { get; set; }
    public Dictionary<Type, UIWindow> WindowDic = new Dictionary<Type, UIWindow>();
    public List<UIWindow> WindowStack { get; set; } = new List<UIWindow>();

    #region Get Window

    public TWindow GetWindow<TWindow>() where TWindow : UIWindow
    {
        var type = typeof(TWindow);
        return GetWindow(type) as TWindow;
        ;
    }

    public UIWindow GetWindow(Type type)
    {
        if (WindowDic.TryGetValue(type, out var ui))
        {
            return ui as UIWindow;
        }

        return default;
    }

    #endregion

    #region Show Window

    public void ShowWindow<TWindow>(params object[] args) where TWindow : UIWindow
    {
        var type = typeof(TWindow);
        ShowWindow(type, args);
    }

    public void ShowWindow(Type type, params object[] args)
    {
        if (!WindowDic.TryGetValue(type, out var ui)) return;
        ShowWindow(ui, args);
    }

    public void ShowWindow(UIWindow ui, params object[] args)
    {
        if (!WindowStack.Contains(ui))
        {
            if (WindowStack.Count > 0)
            {
                WindowStack.Last().Hide();
            }

            ui.Show(args, args);
            WindowStack.Add(ui);
            Current = ui;
        }
        else
        {
            var uiIndex = WindowStack.IndexOf(ui);
            for (var i = WindowStack.Count - 1; i > uiIndex; i++)
            {
                var coverUi = WindowStack[i];
                HideWindow(coverUi);
            }
        }
    }

    #endregion

    #region Hide Window

    public void HideWindow<TWindow>() where TWindow : UIWindow
    {
        var type = typeof(TWindow);
        HideWindow(type);
    }

    public void HideWindow(Type type)
    {
        if (!WindowDic.TryGetValue(type, out var ui)) return;
        HideWindow(ui);
    }

    public void HideWindow(UIWindow ui)
    {
        ui.Hide();
        if (WindowStack.Contains(ui)) WindowStack.Remove(ui);
        if (WindowStack.Count <= 0) return;
        var lastUi = WindowStack.Last();
        lastUi.Show();
        Current = lastUi;
    }

    public void HideAllWindow()
    {
        for (var i = WindowStack.Count - 1; i >= 0; i--)
        {
            var ui = WindowStack[i];
            ui.Hide();
            WindowStack.Remove(ui);
        }
    }

    #endregion

    #endregion

    #region Scene Item

    public TSceneItem ShowSceneItem<TSceneItem>(TSceneItem itemPrefab, GameEntity target, params object[] args)
        where TSceneItem : UISceneItem
    {
        var item = UIPool.Spawn(itemPrefab, Trans);
        SceneItemList.Add(item);
        item.Show(target, args);
        return item;
    }

    public void HideSceneItem<TSceneItem>(TSceneItem item)
        where TSceneItem : UISceneItem
    {
        SceneItemList.Remove(item);
        UIPool.DeSpawn(item);
    }

    public void HideAllSceneItem(Predicate<UISceneItem> predicate = null)
    {
        for (var i = SceneItemList.Count - 1; i >= 0; i--)
        {
            var uiSceneItem = SceneItemList[i];
            if (predicate == null || predicate(uiSceneItem)) HideSceneItem(uiSceneItem);
        }
    }

    #endregion
}
