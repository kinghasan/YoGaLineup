using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Path Revolution", "Path")]
    [Serializable]
    public partial class TweenRevolution : TweenPathBase
    {
        public TargetPositionData Center = new TargetPositionData();
        public Vector3 Radius;
        public Vector3 Deviation;

        public override Vector3 GetPositionByFactor(float factor)
        {
            var angle = Mathf.LerpUnclamped(0f, 360f, factor);
            var centerPosition = Center.GetPosition();
            var x = centerPosition.x + Deviation.x + Mathf.Cos(angle * (Mathf.PI / 180f)) * Radius.x;
            var y = centerPosition.y + Deviation.y + Mathf.Sin(angle * (Mathf.PI / 180f)) * Radius.y;
            var z = centerPosition.z + Deviation.z + Mathf.Sin(angle * (Mathf.PI / 180f)) * Radius.z;
            var position = new Vector3(x, y, z);
            return position;
        }

        public override void Reset()
        {
            base.Reset();
            Center.Reset();
            Radius = Vector3.one;
            Deviation = Vector3.zero;
        }

        public override void OnDrawGizmos()
        {
            if (Target == null) return;
            base.OnDrawGizmos();
            var center = Center.GetPosition();
            var pathCenter = center + Deviation;
            var p = GetPositionByFactor(0f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(center, p);
            Gizmos.DrawLine(center, pathCenter);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(p, pathCenter);
        }
    }

#if UNITY_EDITOR

    public partial class TweenRevolution : TweenPathBase
    {
        [TweenerProperty, NonSerialized] public SerializedProperty CenterProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty RadiusProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty DeviationProperty;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            Center.InitEditor(CenterProperty);
        }

        public override void DrawBody()
        {
            Center.DrawTargetPosition();
            EditorGUILayout.PropertyField(RadiusProperty);
            EditorGUILayout.PropertyField(DeviationProperty);
        }
    }

#endif
}