using Dreamteck.Splines;
using UnityEngine;

public class LevelPathSpline : LevelPath
{
    public SplineComputer Path { get; set; }

    public override Vector3 StartPosition => GetPositionByFactor(0f);
    public override Vector3 EndPosition => GetPositionByFactor(1f);
    public override Vector3 StartForward => GetForward(0f);
    public override Vector3 EndForward => GetForward(1f);

    public override void Init()
    {
        Path = GetComponent<SplineComputer>();
        Path.RebuildImmediate(true, true);
        Length = Path.CalculateLength();
    }

    public override Vector3 GetPositionByFactor(float factor)
    {
        if (Path == null) return Vector3.zero;
        var position = Path.Evaluate(factor).position;
        return position;
    }

    public override (bool, Vector3, float) GetPositionByDistance(float distance)
    {
        if (distance > Length)
        {
            return (true, GetPositionByFactor(1f), distance - Length);
        }
        else
        {
            return (false, GetPositionByFactor((float) Path.Travel(0f, distance)), 0f);
        }
    }

    public virtual Vector3 GetForward(float factor)
    {
        if (Path == null) return Vector3.forward;
        var position = Path.Evaluate(factor).forward;
        return position;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (var i = 0; i < 100; i++)
        {
            Gizmos.DrawLine(GetPositionByFactor(i * 1f / 100f), GetPositionByFactor((i + 1) * 1f / 100f));
        }
    }
}