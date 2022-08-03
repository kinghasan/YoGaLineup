using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[Serializable]
public class CameraData
{
    public string Key;
    public CinemachineVirtualCamera Camera;
}

public class CameraManager : GameEntity<CameraManager>
{
    public string DefaultCamera;
    public new Camera Camera;
    public List<CameraData> Cameras;

    public string CurrentKey { get; set; }
    public CameraData Current { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Switch(DefaultCamera);
    }

    public CameraData GetCamera(string key)
    {
        foreach (var cameraData in Cameras)
        {
            if (cameraData.Key == key)
            {
                return cameraData;
            }
        }

        return null;
    }

    public void SetFollow(Transform target)
    {
        if (target == null) return;
        Current.Camera.Follow = target;
    }

    public void SetLookAt(Transform target)
    {
        if (target == null) return;
        Current.Camera.LookAt = target;
    }

    public void Switch(string key, Transform follow = null, Transform lookAt = null)
    {
        if (CurrentKey == key) return;
        var cam = GetCamera(key);
        if (cam == null) return;

        foreach (var cameraData in Cameras)
        {
            if (cameraData.Key == key)
            {
                CurrentKey = key;
                Current = cameraData;
                cameraData.Camera.gameObject.SetActive(true);
            }
            else
            {
                cameraData.Camera.gameObject.SetActive(false);
            }
        }

        SetFollow(follow);
        SetLookAt(lookAt);
    }
}
