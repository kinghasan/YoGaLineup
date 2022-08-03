/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CameraFollow.cs
//  Info     : 跟随相机
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Util
{
    [ExecuteInEditMode]
    public class CameraFollow : MonoBehaviour
    {
        public enum InterpolationType
        {
            Lerp = 0,
            MoveTowards = 1,
        }

        public enum UpdateType
        {
            Update = 0,
            LateUpdate = 1,
            FixedUpdate = 2,
        }

        public enum RotateType
        {
            Normal = 0,
            LookAtTarget = 1,
            KeepTargetForward = 2,
        }

        [Header("General")]
        public Transform Target;
        public UpdateType UpdateMode = UpdateType.FixedUpdate;
        [Header("Position")]
        public Vector3 Position;
        public InterpolationType MoveInterpolationMode = InterpolationType.Lerp;
        public bool EnablePosX = true;
        public bool EnablePosY = true;
        public bool EnablePosZ = true;
        public float MoveSpeed;
        [Header("Rotation")]
        public Vector3 Rotation;
        public RotateType RotateMode = RotateType.Normal;
        public InterpolationType RotateInterpolationMode = InterpolationType.Lerp;
        public bool EnableRotX = true;
        public bool EnableRotY = true;
        public bool EnableRotZ = true;
        public float RotateSpeed;

        public void Update()
        {
            if (UpdateMode != UpdateType.Update) return;
            UpdateInternal(Time.deltaTime);
        }

        public void LateUpdate()
        {
            if (UpdateMode != UpdateType.LateUpdate) return;
            UpdateInternal(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            if (UpdateMode != UpdateType.FixedUpdate) return;
            UpdateInternal(Time.fixedDeltaTime);
        }

        public virtual void UpdateInternal(float deltaTime)
        {
            // Move
            var targetPos = Target.position + Position;
            var resultPos = targetPos;
            switch (MoveInterpolationMode)
            {
                case InterpolationType.Lerp:
                    resultPos = Vector3.Lerp(transform.position, targetPos, MoveSpeed * deltaTime);
                    break;
                case InterpolationType.MoveTowards:
                    resultPos = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * deltaTime);
                    break;
            }

            resultPos = new Vector3(
                EnablePosX ? resultPos.x : transform.position.x,
                EnablePosY ? resultPos.y : transform.position.y,
                EnablePosZ ? resultPos.z : transform.position.z);
            transform.position = resultPos;

            // Rotate
            var resultRot = transform.eulerAngles;
            switch (RotateMode)
            {
                case RotateType.Normal:
                    resultRot = Rotation;
                    break;
                case RotateType.LookAtTarget:
                    var rotation = Quaternion.LookRotation(Target.position - transform.position).eulerAngles;
                    resultRot = rotation + Rotation;
                    break;
                case RotateType.KeepTargetForward:
                    resultRot = Target.eulerAngles + Rotation;
                    break;
            }

            switch (RotateInterpolationMode)
            {
                case InterpolationType.Lerp:
                    resultRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(resultRot), RotateSpeed * deltaTime).eulerAngles;
                    break;
                case InterpolationType.MoveTowards:
                    resultRot = Vector3.MoveTowards(transform.eulerAngles, resultRot, RotateSpeed * deltaTime);
                    break;
            }

            resultRot = new Vector3(
                EnableRotX ? resultRot.x : transform.eulerAngles.x,
                EnableRotY ? resultRot.y : transform.eulerAngles.y,
                EnableRotZ ? resultRot.z : transform.eulerAngles.z);
            transform.eulerAngles = resultRot;
        }

        public void OnValidate()
        {
            if (Target == null) return;
            UpdateInternal(1f);
        }
    }
}
