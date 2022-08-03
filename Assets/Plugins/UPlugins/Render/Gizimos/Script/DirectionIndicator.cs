using System;
using UnityEngine;

namespace Aya.Render
{
    public class DirectionIndicator : MonoBehaviour
    {
        [Flags]
        public enum Axis
        {
            X = 0x0001,
            Y = 0x0002,
            Z = 0x0004,
        }
        public Axis ShowAxis = Axis.X | Axis.Y | Axis.Z;

        [Range(0.5f, 10f)]
        public float HandleSize = 1f;

        public Color HandleColor = Color.green;
        public Color AxisXColor = Color.red;
        public Color AxisYColor = Color.green;
        public Color AxisZColor = Color.blue;
       
        public float IndicatorSize => HandleSize * 0.2f;

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            Gizmos.color = HandleColor;
            Gizmos.DrawWireSphere(transform.position, IndicatorSize * 0.5f);

            if ((ShowAxis & Axis.X) > 0)
            {
                Gizmos.color = HandleColor;
                var to = transform.position + transform.right * HandleSize;
                Gizmos.DrawLine(transform.position, to);
                Gizmos.color = AxisXColor;
                Gizmos.DrawCube(to, Vector3.one * IndicatorSize);
            }

            if ((ShowAxis & Axis.Y) > 0)
            {
                Gizmos.color = HandleColor;
                var to = transform.position + transform.up * HandleSize;
                Gizmos.DrawLine(transform.position, to);
                Gizmos.color = AxisYColor;
                Gizmos.DrawCube(to, Vector3.one * IndicatorSize);
            }

            if ((ShowAxis & Axis.Z) > 0)
            {
                Gizmos.color = HandleColor;
                var to = transform.position + transform.forward * HandleSize;
                Gizmos.DrawLine(transform.position, to);
                Gizmos.color = AxisZColor;
                Gizmos.DrawCube(to, Vector3.one * IndicatorSize);
            }
        }
#endif
    }
}