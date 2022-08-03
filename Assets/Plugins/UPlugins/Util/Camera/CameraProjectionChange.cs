/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CameraProjectionChange.cs
//  Info     : 相机投影模式过渡
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : https://answers.unity.com/questions/583316/fluent-animation-from-orthographic-to-perspective.html
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Util
{
    [RequireComponent(typeof(Camera))]
    public class CameraProjectionChange : MonoBehaviour
    {
        public Camera Camera;
        public float ProjectionChangeTime = 0.5f;
        public bool ChangeProjection = false;

        private bool _changing = false;
        private float _currentT = 0.0f;

        public void Awake()
        {
            Camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (_changing)
            {
                ChangeProjection = false;
            }
            else if (ChangeProjection)
            {
                _changing = true;
                _currentT = 0.0f;
            }
        }

        private void LateUpdate()
        {
            if (!_changing)
            {
                return;
            }

            var currentlyOrthographic = Camera.orthographic;
            Matrix4x4 orthographicMatrix, perspectiveMatrix;
            if (currentlyOrthographic)
            {
                orthographicMatrix = Camera.projectionMatrix;

                Camera.orthographic = false;
                Camera.ResetProjectionMatrix();
                perspectiveMatrix = Camera.projectionMatrix;
            }
            else
            {
                perspectiveMatrix = Camera.projectionMatrix;

                Camera.orthographic = true;
                Camera.ResetProjectionMatrix();
                orthographicMatrix = Camera.projectionMatrix;
            }

            Camera.orthographic = currentlyOrthographic;

            _currentT += (Time.deltaTime / ProjectionChangeTime);
            if (_currentT < 1.0f)
            {
                if (currentlyOrthographic)
                {
                    Camera.projectionMatrix = MatrixLerp(orthographicMatrix, perspectiveMatrix, _currentT * _currentT);
                }
                else
                {
                    Camera.projectionMatrix = MatrixLerp(perspectiveMatrix, orthographicMatrix, Mathf.Sqrt(_currentT));
                }
            }
            else
            {
                _changing = false;
                Camera.orthographic = !currentlyOrthographic;
                Camera.ResetProjectionMatrix();
            }
        }

        private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
        {
            t = Mathf.Clamp(t, 0.0f, 1.0f);
            var newMatrix = new Matrix4x4();
            newMatrix.SetRow(0, Vector4.Lerp(from.GetRow(0), to.GetRow(0), t));
            newMatrix.SetRow(1, Vector4.Lerp(from.GetRow(1), to.GetRow(1), t));
            newMatrix.SetRow(2, Vector4.Lerp(from.GetRow(2), to.GetRow(2), t));
            newMatrix.SetRow(3, Vector4.Lerp(from.GetRow(3), to.GetRow(3), t));
            return newMatrix;
        }
    }
}