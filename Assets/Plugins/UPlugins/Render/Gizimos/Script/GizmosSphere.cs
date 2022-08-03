/////////////////////////////////////////////////////////////////////////////
//
//  Script   : GizmosSphere.cs
//  Info     : 测试辅助类 Gizmos - Sphere
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Render
{
    public class GizmosSphere : MonoBehaviour
    {
        public float Radius = 1f;
        public Color Color = new Color(0, 0, 1f, 0.5f);
        public bool IsWire = false;

        void OnDrawGizmos()
        {
            Gizmos.color = Color;
            if (!IsWire)
            {
                Gizmos.DrawSphere(transform.position, Radius);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, Radius);
            }
        }
    }
}