/////////////////////////////////////////////////////////////////////////////
//
//  Script   : FaceToCamera.cs
//  Info     : 面向相机
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Util
{
    public class FaceToCamera : MonoBehaviour
    {
        public enum FaceToMode
        {
            CameraForward = 1,
            SelfForward = 2,
        }

        public Camera Camera;
        public FaceToMode Mode = FaceToMode.CameraForward;

        protected void Update()
        {
            if (Camera == null)
            {
                Camera = Camera.main;
            }

            if (Camera == null) return;

            Vector3 forward;
            switch (Mode)
            {
                case FaceToMode.CameraForward:
                    forward = Camera.transform.forward;
                    break;
                case FaceToMode.SelfForward:
                    forward = transform.position - Camera.transform.position;
                    break;
                default:
                    forward = Vector3.forward;
                    break;
            }

            transform.rotation = Quaternion.LookRotation(forward, Camera.transform.up);
        }
    }
}
