using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPhase
{
    public float Progress;
    public int Count;
    public Action<int> Action;

    public IEnumerator Loading()
    {
        Progress = 0f;
        for (var i = 0; i < Count; i++)
        {
            Progress = i * 1f / Count;
            Action?.Invoke(i);
            yield return null;
        }

        Progress = 1f;

        yield return null;
    }
}

public class UILoading : UiWindow<UILoading>
{
    public Image Progress;
    public float MinLoadDuration = 1f;

    public List<LoadingPhase> LoadingPhases { get; set; } = new List<LoadingPhase>();
    private float _startTime;

    public override void Show(params object[] args)
    {
        base.Show(args);
        SetProgress(0f);
    }

    public void SetProgress(float progress)
    {
        Progress.fillAmount = progress;
    }

    public void Clear()
    {
        LoadingPhases.Clear();
    }


    public void RegisterLoading(int count, Action<int> action)
    {
        LoadingPhases.Add(new LoadingPhase()
        {
            Count = count,
            Action = action
        });
    }

    public void StartLoading(Action onDone)
    {
        StartCoroutine(Loading(onDone));
    }

    protected IEnumerator Loading(Action onDone)
    {
        _startTime = Time.realtimeSinceStartup;
        var progress = 0f;
        SetProgress(progress);
        for (var i = 0; i < LoadingPhases.Count; i++)
        {
            var load = LoadingPhases[i];
            load.Action += index =>
            {
                progress = i * 1f / LoadingPhases.Count + load.Progress * (1f / LoadingPhases.Count);
                SetProgress(progress);
            };

            yield return load.Loading();
        }

        SetProgress(1f);
        yield return null;

        var currentTime = Time.realtimeSinceStartup;
        var costTime = currentTime - _startTime;
        if (costTime < MinLoadDuration) yield return new WaitForSeconds(MinLoadDuration - costTime);

        onDone();
    }
}
