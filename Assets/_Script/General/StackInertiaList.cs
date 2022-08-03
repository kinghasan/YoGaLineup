using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using Aya.Util;
using UnityEngine;

public class StackInertiaList : GameEntity
{
    public GameEntity Prefab;
    public float Scale = 1f;
    public float Height;
    public AnimationCurve OffsetCurve;
    public float MaxOffset;
    public AnimationCurve HeightCurve;
    public float MaxHeight;
    public AnimationCurve RotateCurve;
    public float MaxRotate;
    public float MaxDistance;
    public int EffectFrame;
    public float EffectSpeed;
    public float SpreadForce;

    private readonly List<Vector3> _positionList = new List<Vector3>();
    private Vector3 _currentDirection;

    public List<GameEntity> List { get; set; } = new List<GameEntity>();
    public int Count => List.Count;

    public bool Active { get; set; }

    public void Init()
    {
        foreach (var entity in List)
        {
            GamePool.DeSpawn(entity);
        }

        List.Clear();
        Active = true;
    }

    public void Add(int count = 1)
    {
        for (var i = 0; i < count; i++)
        {
            var instance = GamePool.Spawn(Prefab, Trans);
            instance.LocalScale = Vector3.one * Scale;
            if (instance.Rigidbody != null) instance.Rigidbody.isKinematic = true;
            List.Add(instance);
        }
    }

    public void Remove(int count = 1)
    {
        for (var i = 0; i < count; i++)
        {
            var instance = List.Last();
            if (instance == null) return;
            List.Remove(instance);
            GamePool.DeSpawn(instance);
        }
    }

    public void Remove(GameEntity instance)
    {
        if (instance == null) return;
        if (!List.Contains(instance)) return;
        List.Remove(instance);
        GamePool.DeSpawn(instance);
    }

    public void Update()
    {
        if (!Active) return;
        var position = Position;
        _positionList.Add(position);

        if (_positionList.Count > EffectFrame)
        {
            _positionList.RemoveAt(0);
        }

        var direction = _positionList.First() - _positionList.Last();
        _currentDirection = Vector3.Lerp(_currentDirection, direction, DeltaTime * EffectSpeed);
        var normalizedDirection = _currentDirection.normalized;
        var factor = _currentDirection.magnitude * 1f / MaxDistance;

        factor = Mathf.Clamp01(factor);

        for (var i = 0; i < List.Count; i++)
        {
            var item = List[i];
            var pos = Trans.position + Vector3.up * i * Height;
            var rot = Vector3.zero;

            var heightFactor = i * 1f / List.Count;
            var height = HeightCurve.Evaluate(heightFactor * factor) * MaxHeight;
            var offset = OffsetCurve.Evaluate(heightFactor * factor) * MaxOffset * normalizedDirection + Vector3.down * height;

            var angle = new Vector3(normalizedDirection.z, 0f, -normalizedDirection.x);
            var rotate = RotateCurve.Evaluate(heightFactor * factor) * MaxRotate * angle;

            item.Position = pos + offset;
            item.EulerAngles = rot + rotate;
        }
    }

    public void Spread()
    {
        Active = false;
        foreach (var item in List)
        {
            item.Rigidbody.isKinematic = false;
            item.Rigidbody.AddExplosionForce(RandUtil.RandFloat(Mathf.Sqrt(SpreadForce), SpreadForce), item.Position + RandUtil.RandVector3(-1, 1f), 5f);
        }
    }
}
