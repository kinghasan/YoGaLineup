using System;
using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using Aya.Pool;
using Aya.TweenPro;
using Aya.Util;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class UIFlyCoinData
{
    public string Name;
    public Transform Target;
    public GameObject Prefab;
}

public class UIFlyCoin : GameEntity<UIFlyCoin>
{
    public const int Coin = 0;
    public const int Key = 1;
    public const int Ball = 2;

    public float FlyDuration = 1f;
    public float Interval = 0.05f;
    public float RandomStartPos = 100f;
    public int PerFrameLimit = 5;
    public int MaxCount = 100;
    public AnimationCurve CurveX;
    public AnimationCurve CurveY;
    public AnimationCurve CurveScaleCoin;
    public float ScaleTargetDuration;
    public AnimationCurve CurveScaleTarget;

    [TableList] public List<UIFlyCoinData> TargetList;

    public EntityPool CoinPool => PoolManager.Ins["UIFlyCoin"];

    public void Fly(int index, Vector3 startPos, int count, Action onEach = null, Action onDone = null)
    {
        var data = TargetList[index];
        Fly(data.Prefab, startPos, data.Target.position, data.Target, count, Interval, onEach, onDone);
    }

    public void Fly(int index, Vector3 startPos, int count, float interval, Action onEach = null, Action onDone = null)
    {
        var data = TargetList[index];
        Fly(data.Prefab, startPos, data.Target.position, data.Target, count, interval, onEach, onDone);
    }

    public void Fly(int index, Vector3 startPos, Vector3 endPos, Transform scaleTargetTrans, int count, Action onEach = null, Action onDone = null)
    {
        var data = TargetList[index];
        Fly(data.Prefab, startPos, endPos, scaleTargetTrans, count, Interval, onEach, onDone);
    }

    public void Fly(int index, Vector3 startPos, Vector3 endPos, Transform scaleTargetTrans, int count, float interval, Action onEach = null, Action onDone = null)
    {
        var data = TargetList[index];
        Fly(data.Prefab, startPos, endPos, scaleTargetTrans, count, interval, onEach, onDone);
    }

    public void Fly(GameObject coinPrefab, Vector3 startPos, Vector3 endPos, Transform scaleTargetTrans, int count, float interval, Action onEach = null, Action onDone = null)
    {
        StartCoroutine(FlyCo(coinPrefab, startPos, endPos, scaleTargetTrans, count, interval, onEach, onDone));
    }

    protected IEnumerator FlyCo(GameObject coinPrefab, Vector3 startPos, Vector3 endPos, Transform scaleTargetTrans, int count, float interval, Action onEach = null, Action onDone = null)
    {
        for (var i = 0; i < count;)
        {
            while (CoinPool[coinPrefab].SpawnPrefabsCount >= MaxCount)
            {
                yield return null;
            }

            var frameCounter = 0;
            while (frameCounter < PerFrameLimit && i < count)
            {
                var coinStartPos = startPos + RandUtil.RandVector3(-RandomStartPos, RandomStartPos);
                var coinEndPos = endPos;
                var coinIns = CoinPool.Spawn(coinPrefab, transform, coinStartPos);
                coinIns.transform.position = coinStartPos;
                UTween.Scale(coinIns.transform, Vector3.zero, Vector3.one, FlyDuration).SetCurve(CurveScaleCoin);
                UTween.Value(0f, 1f, FlyDuration, value =>
                {
                    var x = Vector3.Lerp(coinStartPos, coinEndPos, CurveX.Evaluate(value)).x;
                    var y = Vector3.Lerp(coinStartPos, coinEndPos, CurveY.Evaluate(value)).y;
                    coinIns.transform.SetPositionXY(x, y);
                })
                    .SetOnStop(() =>
                    {
                        onEach?.Invoke();
                        if (scaleTargetTrans != null)
                        {
                            UTween.Scale(scaleTargetTrans, Vector3.zero, Vector3.one, ScaleTargetDuration).SetCurve(CurveScaleTarget);
                        }

                        CoinPool.DeSpawn(coinIns);
                    });
                i++;
                frameCounter++;
            }

            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(FlyDuration);
        onDone?.Invoke();
        yield return null;
    }
}