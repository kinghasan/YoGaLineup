using System.Collections;
using System.Collections.Generic;
using Aya.Types;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ItemShowUIMode
{
    Window = 0,
    SceneItem = 1,
}

public class ItemShowUI : ItemBase<Player>
{
    [BoxGroup("UI")] public ItemShowUIMode UIMode;
    [BoxGroup("UI"), ShowIf("UIMode", ItemShowUIMode.Window), TypeReference(typeof(UIWindow))] public TypeReference WindowType;
    [BoxGroup("UI"), ShowIf("UIMode", ItemShowUIMode.SceneItem)] public UISceneItem SceneItemPrefab;
    [BoxGroup("UI"), ShowIf("UIMode", ItemShowUIMode.SceneItem)] public GameEntity SceneItemTarget;
    [BoxGroup("UI")] public bool AutoHideAfterExit = true;
    [BoxGroup("UI")] public string[] Args;

    public UISceneItem UISceneItemInstance { get; set; }

    public override void OnTargetEffect(Player target)
    {
        if (!target.IsPlayer) return;
        switch (UIMode)
        {
            case ItemShowUIMode.Window:
                UI.ShowWindow(WindowType, Args as object[]);
                break;
            case ItemShowUIMode.SceneItem:
                UISceneItemInstance = UI.ShowSceneItem(SceneItemPrefab, SceneItemTarget, Args as object[]);
                break;
        }
    }

    public override void OnTargetExit(Player target)
    {
        if (!target.IsPlayer) return;
        if (!AutoHideAfterExit) return;
        switch (UIMode)
        {
            case ItemShowUIMode.Window:
                UI.HideWindow(WindowType);
                break;
            case ItemShowUIMode.SceneItem:
                UI.HideSceneItem(UISceneItemInstance);
                break;
        }
        
    }
}
