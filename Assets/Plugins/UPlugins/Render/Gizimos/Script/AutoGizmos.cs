/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AutoGizmos.cs
//  Info     : Gizmos 自动显示
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using UnityEditor;
using UnityEngine;

namespace Aya.Render
{
    [ExecuteInEditMode]
    public class AutoGizmos : MonoBehaviour
    {
        public Color Color = Color.green;
        public bool IsWire = true;
        public bool IsWireFill = true;

        public BoxCollider BoxCollider { get; set; }
        public SphereCollider SphereCollider { get; set; }
        public MeshCollider MeshCollider { get; set; }
        public CapsuleCollider CapsuleCollider { get; set; }

        public Camera Camera { get; set; }

        public float WireFillAlpha { get; set; } = 0.15f;

#if UNITY_EDITOR
        public void Awake()
        {
            BoxCollider = gameObject.GetComponent<BoxCollider>();
            SphereCollider = gameObject.GetComponent<SphereCollider>();
            MeshCollider = gameObject.GetComponent<MeshCollider>();
            CapsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            Camera = gameObject.GetComponent<Camera>();
        }

        public void OnDrawGizmos()
        {
            // BoxCollider
            if (BoxCollider != null)
            {
                Gizmos.color = Color;
                Gizmos.matrix = transform.localToWorldMatrix;
                if (IsWire)
                {
                    Gizmos.DrawWireCube(BoxCollider.center, BoxCollider.size);
                }

                if (IsWireFill)
                {
                    Gizmos.color = Color * WireFillAlpha;
                }

                if ((IsWire && IsWireFill) || !IsWire)
                {
                    Gizmos.DrawCube(BoxCollider.center, BoxCollider.size);
                }
            }

            // SphereCollider
            if (SphereCollider != null)
            {
                Gizmos.color = Color;
                Gizmos.matrix = transform.localToWorldMatrix;
                if (IsWire)
                {
                    Gizmos.DrawWireSphere(SphereCollider.center, SphereCollider.radius);
                }

                if (IsWireFill)
                {
                    Gizmos.color = Color * WireFillAlpha;
                }

                if ((IsWire && IsWireFill) || !IsWire)
                {
                    Gizmos.DrawSphere(SphereCollider.center, SphereCollider.radius);
                }
            }

            // MeshCollider
            if (MeshCollider != null)
            {
                Gizmos.color = Color;
                if (IsWire)
                {
                    Gizmos.DrawWireMesh(MeshCollider.sharedMesh, 0, transform.position, transform.rotation, transform.localScale);
                }

                if (IsWireFill)
                {
                    Gizmos.color = Color * WireFillAlpha;
                }

                if ((IsWire && IsWireFill) || !IsWire)
                {
                    Gizmos.DrawMesh(MeshCollider.sharedMesh, 0, transform.position, transform.rotation, transform.localScale);
                }
            }

            // CapsuleCollider
            if (CapsuleCollider != null)
            {
                Gizmos.color = Color;
                Gizmos.matrix = transform.localToWorldMatrix;
                DrawWireCapsule(CapsuleCollider.center, CapsuleCollider.radius, CapsuleCollider.height);
            }

            // Camera
            if (Camera != null && !Camera.orthographic)
            {
                Gizmos.color = Color;
                var posRotation = Matrix4x4.Translate(transform.position);
                var rotMatrix = Matrix4x4.Rotate(transform.rotation);
                Gizmos.matrix = posRotation * rotMatrix;
                Gizmos.DrawFrustum(Vector3.zero, Camera.fieldOfView, Camera.farClipPlane, Camera.nearClipPlane, Camera.aspect);
            }
        }

        public static void DrawWireCapsule(Vector3 center, float radius, float height)
        {
            Handles.color = Gizmos.color;
            var angleMatrix = Gizmos.matrix;

            using (new Handles.DrawingScope(angleMatrix))
            {
                var pointOffset = (height - (radius * 2F)) / 2F;

                //draw sideways
                Handles.DrawWireArc(center + Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, radius);
                Handles.DrawLine(center + new Vector3(0, pointOffset, -radius), center + new Vector3(0, -pointOffset, -radius));
                Handles.DrawLine(center + new Vector3(0, pointOffset, radius), center + new Vector3(0, -pointOffset, radius));
                Handles.DrawWireArc(center + Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, radius);
                //draw frontways
                Handles.DrawWireArc(center + Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, radius);
                Handles.DrawLine(center + new Vector3(-radius, pointOffset, 0), center + new Vector3(-radius, -pointOffset, 0));
                Handles.DrawLine(center + new Vector3(radius, pointOffset, 0), center + new Vector3(radius, -pointOffset, 0));
                Handles.DrawWireArc(center + Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, radius);
                //draw center
                Handles.DrawWireDisc(center + Vector3.up * pointOffset, Vector3.up, radius);
                Handles.DrawWireDisc(center + Vector3.down * pointOffset, Vector3.up, radius);
            }
        }
#endif
    }
}

