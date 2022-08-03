/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CameraExtension.cs
//  Info     : Camera扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class CameraExtension
    {
        #region Coordinate

        /// <summary>
        /// 屏幕坐标转换为世界坐标（忽略深度）
        /// </summary>
        /// <param name="camera">相机</param>
        /// <param name="position">屏幕坐标</param>
        /// <returns>结果</returns>
        public static Vector3 ScreenToWorldPointIgnoreDeep(this Camera camera, Vector3 position)
        {
            var deep = camera.transform.position.z;
            var result = camera.ScreenToWorldPoint(new Vector3(position.x, position.y, deep));
            return result;
        }

        #endregion

        #region Unit / Pixel

        /// <summary>
        /// 计算正交相机屏幕上，每像素的世界单位（单位：米）大小
        /// </summary>
        /// <param name="camera">正交相机对象</param>
        /// <returns>每像素的世界单位大小</returns>
        public static float UnitsPerPixel(this Camera camera)
        {
            var result = camera.orthographicSize * 2 / Screen.height;
            return result;
        }


        /// <summary>
        /// 计算正交相机屏幕上，每世界单位（单位：米）的像素大小
        /// </summary>
        /// <param name="camera">正交相机对象</param>
        /// <returns>每世界单位的像素大小</returns>
        public static float PixelsPerUnit(this Camera camera)
        {
            var result = Screen.height * 0.5f / camera.orthographicSize;
            return result;
        }

        #endregion

        #region Capture

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="camera">相机</param>
        /// <returns>结果</returns>
        public static Texture2D Caputre(this Camera camera)
        {
            var texture = camera.Capture(new Rect(0, 0, Screen.width, Screen.height));
            return texture;
        }

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="camera">相机</param>
        /// <param name="rect">截图区域</param>
        /// <returns>结果</returns>
        public static Texture2D Capture(this Camera camera, Rect rect)
        {
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            var screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            camera.targetTexture = null;
            RenderTexture.active = null;
            Object.Destroy(renderTexture);

            return screenShot;
        }

        #endregion

        #region Size

        /// <summary>
        /// 获取屏幕尺寸对应的世界尺寸
        /// </summary>
        /// <param name="camera">相机</param>
        /// <param name="pixelSize">像素尺寸</param>
        /// <param name="clipPlane">裁切面</param>
        /// <returns>结果</returns>
        public static float ScreenToWorldSize(this Camera camera, float pixelSize, float clipPlane)
        {
            float result;
            if (camera.orthographic)
            {
                result = pixelSize * camera.orthographicSize * 2f / camera.pixelHeight;
            }
            else
            {
                result = pixelSize * clipPlane * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * 2f / camera.pixelHeight;
            }

            return result;
        }

        /// <summary>
        /// 获取世界尺寸对应的屏幕尺寸
        /// </summary>
        /// <param name="camera">相机</param>
        /// <param name="worldSize">世界尺寸</param>
        /// <param name="clipPlane">裁切面</param>
        /// <returns>结果</returns>
        public static float WorldToScreenSize(this Camera camera, float worldSize, float clipPlane)
        {
            float result;
            if (camera.orthographic)
            {
                result = worldSize * camera.pixelHeight * 0.5f / camera.orthographicSize;
            }
            else
            {
                result = worldSize * camera.pixelHeight * 0.5f / (clipPlane * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad));
            }

            return result;
        }

        #endregion

        #region Clip Plane

        /// <summary>
        /// 获取指定位置的裁切面
        /// </summary>
        /// <param name="camera">相机</param>
        /// <param name="point">点</param>
        /// <param name="normal">法线</param>
        /// <returns>结果</returns>
        public static Vector4 GetClipPlane(this Camera camera, Vector3 point, Vector3 normal)
        {
            var wtoc = camera.worldToCameraMatrix;
            point = wtoc.MultiplyPoint(point);
            normal = wtoc.MultiplyVector(normal).normalized;
            var result = new Vector4(normal.x, normal.y, normal.z, -Vector3.Dot(point, normal));
            return result;
        }

        #endregion

        #region ZBuffer
       
        /// <summary>
        /// 计算Z缓冲参数(Compute Shader)
        /// </summary>
        /// <param name="camera">相机</param>
        /// <returns>结果</returns>
        public static Vector4 GetZBufferParams(this Camera camera)
        {
            double f = camera.farClipPlane;
            double n = camera.nearClipPlane;

            var rn = 1f / n;
            var rf = 1f / f;
            var fpn = f / n;

            var result = SystemInfo.usesReversedZBuffer
                ? new Vector4((float)(fpn - 1.0), 1f, (float)(rn - rf), (float)rf)
                : new Vector4((float)(1.0 - fpn), (float)fpn, (float)(rf - rn), (float)rn);
            return result;
        } 

        #endregion
    }
}