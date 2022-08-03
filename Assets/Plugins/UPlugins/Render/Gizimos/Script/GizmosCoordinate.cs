using UnityEngine;

namespace Aya.Render
{
    public enum GizmosCoordinateSpace
    {
        World = 0,
        Local = 1,
    }

    public enum GizmosCoordinateDirection
    {
        World = 0,
        Local = 1,
    }

    public class GizmosCoordinate : MonoBehaviour
    {
        public GizmosCoordinateSpace Space;
        public GizmosCoordinateDirection Direction;
        public float Length = 100f;

#if UNITY_EDITOR

        public void OnDrawGizmos()
        {
            var length = Length;

            var center = Space == GizmosCoordinateSpace.World ? Vector3.zero : transform.position;
            var forward = Direction == GizmosCoordinateDirection.World ? Vector3.forward : transform.forward;
            var right = Direction == GizmosCoordinateDirection.World ? Vector3.right : transform.right;
            var up = Direction == GizmosCoordinateDirection.World ? Vector3.up : transform.up;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(center, center + right * length);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(center, center + up * length);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(center, center + forward * length);

            var alpha = 0.5f;
            Gizmos.color = Color.red * alpha;
            Gizmos.DrawLine(center, center - right * length);
            Gizmos.color = Color.green * alpha;
            Gizmos.DrawLine(center, center - up * length);
            Gizmos.color = Color.blue * alpha;
            Gizmos.DrawLine(center, center - forward * length);
        }
#endif
    }
}