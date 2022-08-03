using Aya.Maths;
using UnityEngine;

public abstract class LevelPath : GameEntity
{
    public float Width;
    public float HalfWidth => Width / 2f;
    public Vector2 TurnRange => new Vector2(-HalfWidth, HalfWidth);

    public virtual Vector3 StartPosition { get; set; }
    public virtual Vector3 EndPosition { get; set; }

    public virtual Vector3 StartForward { get; set; }
    public virtual Vector3 EndForward { get; set; }

    public float Length { get; set; }
    public Vector3 CurrentPosition { get; set; }

    public virtual void Init()
    {
        CurrentPosition = GetPositionByFactor(0f);
    }

    public abstract Vector3 GetPositionByFactor(float factor);
    public abstract (bool, Vector3, float) GetPositionByDistance(float distance);

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(GetPositionByFactor(0f), GetPositionByFactor(1f));
    }
}