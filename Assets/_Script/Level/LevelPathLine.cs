using UnityEngine;

public class LevelPathLine : LevelPath
{
    public Transform Start;
    public Transform End;

    public override Vector3 StartPosition => GetPositionByFactor(0f);
    public override Vector3 EndPosition => GetPositionByFactor(1f);

    public override Vector3 StartForward => EndPosition - StartPosition;
    public override Vector3 EndForward => EndPosition - StartPosition;

    public override void Init()
    {
        base.Init();
        Length = Vector3.Distance(StartPosition, EndPosition);
    }

    public override Vector3 GetPositionByFactor(float factor)
    {
        if (Start == null || End == null) return Vector3.zero;
        var position = Vector3.Lerp(Start.position, End.position, factor);
        return position;
    }

    public override (bool, Vector3, float) GetPositionByDistance(float distance)
    {
        var factor = distance / Length;
        if (factor >= 1f)
        {
            return (true, GetPositionByFactor(1f), distance - Length);
        }
        else
        {
            return (false, GetPositionByFactor(factor), 0f);
        }
    }


}