using System;
using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using Aya.TweenPro;
using Aya.Util;
using UnityEngine;

public class StackDownAnimation : GameEntity
{
    public string PlayerAnimation;
    public float PlayerAnimationDuration;
    public float Height;
    public float Scale;
    public float SpawnInterval;
    public float DumpDistance;
    public float RandPos;
    public AnimationCurve DumpCurve;
    public AnimationCurve OffsetCurve;
    public float DumpSpeed;
    public float Delay;
    public GameObject WinFx;
    public float WaitForWin;

    public bool WithRainbow = true;

    public EndlessRainbow Rainbow { get; set; }

    public void Init()
    {
        if (WithRainbow)
        {
            Rainbow = CurrentLevel.GetComponentInChildren<EndlessRainbow>();
            Rainbow.FollowDummy.SetPositionZ(Rainbow.StartPos.position.z);
        }
    }

    public void StartAnimation<TPrefab, TInstance>(TPrefab prefab, List<TInstance> list, Action<TInstance> onOverlay, Action onDone = null)
        where TPrefab : GameEntity
        where TInstance : GameEntity
    {
        StartCoroutine(Animation(prefab, list, onOverlay, onDone));
    }

    public IEnumerator Animation<TPrefab, TInstance>(TPrefab prefab, List<TInstance> list, Action<TInstance> onOverlay, Action onDone = null)
        where TPrefab : GameEntity
        where TInstance : GameEntity
    {
        var start = Rainbow.StartPos.position;
        var count = list.Count;
        var chipList = new List<TPrefab>();
        for (var i = 0; i < count; i++)
        {
            var pos = start + Vector3.up * i * Height;
            var item = GamePool.Spawn(prefab, CurrentLevel.Trans);
            item.LocalScale = Vector3.one * Scale;
            item.Position = pos;
            chipList.Add(item);

            var instance = list[i];
            onOverlay?.Invoke(instance);
            if (i % 2 == 0) yield return new WaitForSeconds(SpawnInterval);
        }

        list.Clear();

        if (!string.IsNullOrEmpty(PlayerAnimation))
        {
            Player.Play(PlayerAnimation);
            yield return new WaitForSeconds(PlayerAnimationDuration);
        }

        var duration = Vector3.Distance(start, start + count * Vector3.forward * DumpDistance) / DumpSpeed;
        var rainbowIndex = -1;
        for (var i = 0; i < count; i++)
        {
            var chip = chipList[i];
            var startPos = chip.Position;
            var endPos = start + i * Vector3.forward * DumpDistance;
            var endPosRand = endPos + new Vector3(RandUtil.RandFloat(-RandPos, RandPos), 0f, RandUtil.RandFloat(-RandPos, RandPos));

            var index = i;
            UTween.Value(0f, 1f, duration + i * Delay, value =>
            {
                var posFactor = DumpCurve.Evaluate(value);
                var pos1 = Vector3.Lerp(startPos, endPos, posFactor);
                var pos2 = Vector3.Lerp(startPos, endPosRand, posFactor);
                var pos = Vector3.Lerp(pos1, pos2, OffsetCurve.Evaluate(value));
                chip.Position = pos;

                if (index == count - 1)
                {
                    if (WithRainbow)
                    {
                        Rainbow.FollowDummy.SetPositionZ(pos.z);

                        for (var j = 0; j < Rainbow.RainbowList.Count; j++)
                        {
                            var rainbowData = Rainbow.RainbowList[j];
                            if (pos.z > rainbowData.Renderer.transform.position.z)
                            {
                                if (j > rainbowIndex)
                                {
                                    rainbowIndex = j;
                                    Rainbow.Animation(rainbowIndex);
                                }
                            }
                        }
                    }
                }
            }).SetOnStop(() =>
            {
                if (index == count - 1)
                {
                    SpawnFx(WinFx, CurrentLevel.Trans, chip.Position);
                }
            });
        }

        var delay = duration + count * Delay;
        yield return new WaitForSeconds(delay + WaitForWin);

        onDone?.Invoke();

        yield return null;
    }

}
