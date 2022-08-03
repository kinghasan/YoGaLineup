using System;
using Aya.Extension;
using Aya.Physical;
using Fishtail.PlayTheBall.Vibration;
using UnityEngine;

public abstract class ItemBase<TTarget> : ItemBase 
    where TTarget : Component
{
    public override Type TargetType => typeof(TTarget);

    public TTarget Target { get; set; }

    public override void Init()
    {
        base.Init();
        Target = null;
    }

    public override void CacheComponents()
    {
        base.CacheComponents();

        foreach (var col in ColliderList)
        {
            var listener = col.gameObject.GetComponent<ColliderListener>();
            if (listener == null) listener = col.gameObject.AddComponent<ColliderListener>();

            listener.Clear();
            listener.onTriggerEnter.Clear();
            listener.onTriggerEnter.Add<TTarget>(LayerMask, OnEnter);
            listener.onTriggerStay.Clear();
            listener.onTriggerStay.Add<TTarget>(LayerMask, OnStay);
            listener.onTriggerExit.Clear();
            listener.onTriggerExit.Add<TTarget>(LayerMask, OnExit);

            ColliderListeners.Add(listener);
        }
    }

    public virtual void OnEnter<T>(T target) where T : Component
    {
        TryCatch(()=> { OnTargetEnter(target as TTarget); }, "On Item Enter");
        OnEffect(target);
    }

    public virtual void OnStay<T>(T target) where T : Component
    {
        if (EffectMode != ItemEffectMode.Stay) return;
        TryCatch(() => { OnTargetStay(target as TTarget); }, "On Item Stay");

        EffectIntervalTimer += DeltaTime;
        if (EffectIntervalTimer >= EffectInterval)
        {
            EffectIntervalTimer -= EffectInterval;
            OnEffect(target);
        }
    }

    public virtual void OnExit<T>(T target) where T : Component
    {
        TryCatch(() => { OnTargetExit(target as TTarget); }, "On Item Exit");
        Target = null;

        if (DeSpawnMode == ItemDeSpawnMode.Exit)
        {
            DeSpawn();
        }
    }

    public virtual void OnEffect<T>(T target) where T : Component
    {
        if (!Active) return;

        if (Target == null)
        {
            Target = target as TTarget;
        }

        if (Target == null) return;

        // Condition
        foreach (var condition in Conditions)
        {
            var check = condition.CheckCondition(target);
            if (!check) return;
        }

        // Effect Counter
        EffectCounter++;
        if (EffectMode == ItemEffectMode.Once)
        {
            Active = false;
        }
        else if (EffectMode == ItemEffectMode.Count && EffectCount > 0)
        {
            if (EffectCounter >= EffectCount)
            {
                Active = false;
            }
            else
            {
                return;
            }
        }

        // Exclude
        foreach (var item in ExcludeItems)
        {
            if (item == null) continue;
            item.Active = false;
        }

        // On Effect
        try
        {
            TryCatch(() => { OnTargetEffect(target as TTarget); }, "On Item Effect");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        // Vibrate
        VibrationController.Instance.Impact(VibrationType);

        // DeActive Render
        if (DeActiveRender && RendererTrans != null)
        {
            RendererTrans.gameObject.SetActive(false);
        }

        // Active / De Active
        if (EffectCounter == 1)
        {
            foreach (var go in ActiveList)
            {
                if (go == null) continue;
                go.SetActive(true);
            }

            foreach (var go in DeActiveList)
            {
                if (go == null) continue;
                go.SetActive(false);
            }
        }

        // Self Fx
        if (SelfFx != null && SelfFx.Count > 0)
        {
            foreach (var fx in SelfFx)
            {
                SpawnFx(fx, CurrentLevel.Trans, Position);
            }
        }

        if (SelfRandomFx != null && SelfRandomFx.Count > 0)
        {
            var fx = SelfRandomFx.Random();
            SpawnFx(fx, CurrentLevel.Trans, Position);
        }

        // Target Fx
        var targetFxTrans = target.transform;
        if (target is Player player) targetFxTrans = player.Render.RenderTrans;
        else if (target is GameEntity entity) targetFxTrans = entity.Trans;

        if (TargetFx != null && TargetFx.Count > 0)
        {
            foreach (var fx in TargetFx)
            {
                SpawnFx(fx, targetFxTrans, targetFxTrans.position);
            }
        }

        if (TargetRandomFx != null && TargetRandomFx.Count > 0)
        {
            var fx = TargetRandomFx.Random();
            SpawnFx(fx, targetFxTrans, targetFxTrans.position);
        }

        // Animation
        foreach (var animatorData in AnimatorDataList)
        {
            if (animatorData == null) continue;
            if (animatorData.Target == ItemAnimatorTargetMode.Self) animatorData.Animator.Play(animatorData.Clip);
            else if (animatorData.Target == ItemAnimatorTargetMode.Target) (target as GameEntity)?.Play(animatorData.Clip);
        }

        foreach (var tweenAnimation in TweenAnimationList)
        {
            if (tweenAnimation == null) continue;
            tweenAnimation.Data.Play();
        }

        // DeSpawn
        if (DeSpawnMode == ItemDeSpawnMode.Effect)
        {
            DeSpawn();
            return;
        }

        if (!Active && DeSpawnMode != ItemDeSpawnMode.None)
        {
            DeSpawn();
            return;
        }
    }

    public abstract void OnTargetEffect(TTarget target);

    public virtual void OnTargetEnter(TTarget target)
    {

    }

    public virtual void OnTargetStay(TTarget target)
    {

    }

    public virtual void OnTargetExit(TTarget target)
    {

    }
}
