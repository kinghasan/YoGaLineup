/////////////////////////////////////////////////////////////////////////////
//
//  Script   : GizmosCuboid.cs
//  Info     : 测试辅助类 Gizmos - Cuboid
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Render
{
    public class GizmosCuboid : MonoBehaviour
    {
        public Vector3 Size = new Vector3(1, 1, 2f);
        public Color Color = new Color(0, 0, 1f, 0.5f);
        public bool IsWire = false;

        void OnDrawGizmos()
        {
            Gizmos.color = Color;
            if (!IsWire)
            {
                Gizmos.DrawCube(transform.position, Size);
            }
            else
            {
                Gizmos.DrawWireCube(transform.position, Size);
            }
        }
    }
}