using System;
using System.Collections.Generic;
using UnityEngine;
using Aya.Physical;
using Aya.Extension;
using Aya.TweenPro;
using MoreMountains.NiceVibrations;
using Sirenix.OdinInspector;

public enum ItemDeSpawnMode
{
    None = 0,
    Effect = 1,
    Exit = 2,
}

public enum ItemEffectMode
{
    Once = 0,
    UnLimit = 1,
    Count = 2,
    Stay = 3,
}

public abstract class ItemBase : GameEntity
{
    [FoldoutGroup("Param")] public LayerMask LayerMask;
    [FoldoutGroup("Param"), EnumToggleButtons] public ItemDeSpawnMode DeSpawnMode = ItemDeSpawnMode.Effect;
    [FoldoutGroup("Param")] public bool DeActiveRender;
    [FoldoutGroup("Param"), EnumToggleButtons] public ItemEffectMode EffectMode = ItemEffectMode.Once;
    [FoldoutGroup("Param"), ShowIf("EffectMode", ItemEffectMode.Count)] public int EffectCount = 1;
    [FoldoutGroup("Param"), ShowIf("EffectMode", ItemEffectMode.Stay)] public float EffectInterval = 1f;
    [FoldoutGroup("Param"), SerializeReference] public List<ItemCondition> Conditions = new List<ItemCondition>();

    [FoldoutGroup("Renderer")] public List<GameObject> RenderPrefabs;
    [FoldoutGroup("Renderer")] public List<GameObject> RenderRandomPrefabs;
    [FoldoutGroup("Renderer")] public int ItemGroupIndex = -1;

    [FoldoutGroup("Active & Exclude")] public List<GameObject> ActiveList;
    [FoldoutGroup("Active & Exclude")] public List<GameObject> DeActiveList;
    [FoldoutGroup("Active & Exclude")] public List<ItemBase> ExcludeItems;

    [FoldoutGroup("Effect")] public List<GameObject> SelfFx;
    [FoldoutGroup("Effect")] public List<GameObject> SelfRandomFx;
    [FoldoutGroup("Effect")] public List<GameObject> TargetFx;
    [FoldoutGroup("Effect")] public List<GameObject> TargetRandomFx;
    [FoldoutGroup("Effect")] public HapticTypes VibrationType = HapticTypes.None;

    [FoldoutGroup("Animation")] public List<UTweenAnimation> TweenAnimationList;
    [FoldoutGroup("Animation"), TableList] public List<ItemAnimatorData> AnimatorDataList;

    public List<Collider> ColliderList { get; set; }
    public List<ColliderListener> ColliderListeners { get; set; }
    public virtual Type TargetType { get; set; }

    public bool Active { get; set; }
    public List<GameObject> RenderInstanceList { get; set; }
    public List<ItemBase> SubItems { get; set; }
    public virtual bool IsUseful => true;
    public int EffectCounter { get; set; }
    public float EffectTimer { get; set; }
    public float EffectIntervalTimer { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Init()
    {
        InitRenderer();
        CacheComponents();

        gameObject.SetActive(true);
        RendererTrans?.gameObject.SetActive(true);

        foreach (var animatorData in AnimatorDataList)
        {
            if (animatorData == null) continue;
            if (animatorData.Target == ItemAnimatorTargetMode.Self) animatorData.Animator.Play(animatorData.DefaultClip);
        }

        foreach (var tweenAnimation in TweenAnimationList)
        {
            if (tweenAnimation == null) continue;
            tweenAnimation.Data.Sample(0f);
        }

        foreach (var go in ActiveList)
        {
            if (go == null) continue;
            go.SetActive(false);
        }

        foreach (var go in DeActiveList)
        {
            if (go == null) continue;
            go.SetActive(true);
        }

        EffectCounter = 0;
        EffectTimer = 0f;
        EffectIntervalTimer = 0f;
        Active = true;
    }

    public virtual void InitRenderer()
    {
        if (RenderInstanceList != null && RenderInstanceList.Count > 0)
        {
            foreach (var ins in RenderInstanceList)
            {
                // 嵌套道具循环回收导致生成出错，需要过滤
                if (!ins.activeSelf) continue;
                if (ins.activeSelf && ins.transform.parent != RendererTrans) continue;
                GamePool.DeSpawn(ins);
            }
        }

        RenderInstanceList = new List<GameObject>();
        if (RenderPrefabs != null && RenderPrefabs.Count > 0)
        {
            foreach (var prefab in RenderPrefabs)
            {
                if (prefab == null) continue;
                var ins = GamePool.Spawn(prefab, RendererTrans);
                RenderInstanceList.Add(ins);
            }
        }

        if (RenderRandomPrefabs != null && RenderRandomPrefabs.Count > 0)
        {
            var prefab = RenderRandomPrefabs.Random();
            if (prefab != null)
            {
                var ins = GamePool.Spawn(prefab, RendererTrans);
                RenderInstanceList.Add(ins);
            }
        }

        if (ItemGroupIndex >= 0)
        {
            var itemGroupData = ItemGroupSetting.Ins.CurrentSelectData;
            var prefab = itemGroupData[ItemGroupIndex];
            if (prefab != null)
            {
                var ins = GamePool.Spawn(prefab, RendererTrans);
                RenderInstanceList.Add(ins);
            }
        }
    }

    public virtual void CacheComponents()
    {
        Animator = GetComponentInChildren<Animator>();
        ColliderList = GetComponentsInChildren<Collider>().ToList();
        ColliderListeners = new List<ColliderListener>();
    }

    public virtual void DeSpawn()
    {
        Active = false;
        CurrentLevel.RemoveItem(this);

        foreach (var ins in RenderInstanceList)
        {
            GamePool.DeSpawn(ins);
        }

        gameObject.SetActive(false);
    }

    public virtual void OnDrawGizmos()
    {
        if (ActiveList != null)
        {
            foreach (var go in ActiveList)
            {
                if (go == null) continue;
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, go.transform.position);
            }
        }

        if (DeActiveList != null)
        {
            foreach (var go in DeActiveList)
            {
                if (go == null) continue;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, go.transform.position);
            }
        }
    }
}