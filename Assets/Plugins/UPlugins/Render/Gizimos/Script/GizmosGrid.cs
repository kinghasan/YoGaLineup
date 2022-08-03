/////////////////////////////////////////////////////////////////////////////
//
//  Script   : GizmosGrid.cs
//  Info     : 测试辅助类 Gizmos - Grid
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Render
{
    public class GizmosGrid : MonoBehaviour
    {
        public GizmosCoordinateSpace Space;
        public GizmosCoordinateDirection Direction;
        public Vector3 CellSize = Vector3.one;
        public Vector3Int Size = new Vector3Int(10, 10, 10);
        public Color Color = Color.green;

#if UNITY_EDITOR

        public void OnDrawGizmos()
        {
            var center = Space == GizmosCoordinateSpace.World ? Vector3.zero : transform.position;
            var forward = Direction == GizmosCoordinateDirection.World ? Vector3.forward : transform.forward;
            var right = Direction == GizmosCoordinateDirection.World ? Vector3.right : transform.right;
            var up = Direction == GizmosCoordinateDirection.World ? Vector3.up : transform.up;

            Gizmos.color = Color;
            for (var x = 0; x <= Size.x; x++)
            {
                for (var y = 0; y <= Size.y; y++)
                {
                    for (var z = 0; z <= Size.z; z++)
                    {
                        var start = center;
                        var offset = new Vector3(0, CellSize.y * y, CellSize.z * z);
                        if (Direction == GizmosCoordinateDirection.Local) offset = transform.rotation * offset;
                        start += offset;
                        Gizmos.DrawLine(start, start + right * CellSize.x * Size.x);
                    }
                }
            }

            for (var y = 0; y <= Size.y; y++)
            {
                for (var x = 0; x <= Size.x; x++)
                {
                    for (var z = 0; z <= Size.z; z++)
                    {
                        var start = center;
                        var offset = new Vector3(CellSize.x * x, 0, CellSize.z * z);
                        if (Direction == GizmosCoordinateDirection.Local) offset = transform.rotation * offset;
                        start += offset;
                        Gizmos.DrawLine(start, start + up * CellSize.y * Size.y);
                    }
                }
            }

            for (var z = 0; z <= Size.z; z++)
            {
                for (var y = 0; y <= Size.y; y++)
                {
                    for (var x = 0; x <= Size.x; x++)
                    {
                        var start = center;
                        var offset = new Vector3(CellSize.x * x, CellSize.y * y, 0);
                        if (Direction == GizmosCoordinateDirection.Local) offset = transform.rotation * offset;
                        start += offset;
                        Gizmos.DrawLine(start, start + forward * CellSize.z * Size.z);
                    }
                }
            }
        }
#endif
    }
}