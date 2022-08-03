/////////////////////////////////////////////////////////////////////////////
//
//  Script   : GizmosCube.cs
//  Info     : 测试辅助类 Gizmos - Cube
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Render
{
    public class GizmosCube : MonoBehaviour
    {
        public float Size = 1f;
        public Color Color = new Color(0, 0, 1f, 0.5f);
        public bool IsWire = false;

        void OnDrawGizmos()
        {
            Gizmos.color = Color;
            if (!IsWire)
            {
                Gizmos.DrawCube(transform.position, new Vector3(Size, Size, Size));
            }
            else
            {
                Gizmos.DrawWireCube(transform.position, new Vector3(Size, Size, Size));
            }
        }
    }
}