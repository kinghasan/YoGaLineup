/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DeviceUtil.cs
//  Info     : 设备辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using Aya.Extension;

namespace Aya.Util
{
    public static class DeviceUtil
    {
        #region Vibrate

        /// <summary>
        /// 震动(需替换原生实现以调节震动强度)
        /// </summary>
        public static void Vibrate()
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }

        #endregion

        #region Platform

        /// <summary>
        /// 是否为 Apple 设备平台
        /// </summary>
        /// <returns></returns>
        public static bool IsApple()
        {
            return RuntimePlatform.IPhonePlayer == Application.platform
                   || RuntimePlatform.OSXEditor == Application.platform
                   || RuntimePlatform.OSXPlayer == Application.platform
                   || RuntimePlatform.tvOS == Application.platform;
        }

        /// <summary>
        /// 是否为 Android 设备平台
        /// </summary>
        /// <returns></returns>
        public static bool IsAndroid()
        {
            return RuntimePlatform.Android == Application.platform;
        }

        /// <summary>
        /// 是否为编辑器
        /// </summary>
        /// <returns></returns>
        public static bool IsEditor()
        {
            return RuntimePlatform.OSXEditor == Application.platform
                   || RuntimePlatform.WindowsEditor == Application.platform
                   || RuntimePlatform.LinuxEditor == Application.platform;
        }

        #endregion

        #region Apple Device

        /// <summary>
        /// IPhone 设备代数
        /// </summary>
        public static int[] IPhoneIDs =
        {
            1, 2, 3, 8, 10, 11, 13, 17, 18, 21, 22, 25, 26, 29, 31, 32, 10001
        };

        /// <summary>
        /// IPad 设备代数
        /// </summary>
        public static int[] IPadIDs =
        {
            7, 10, 12, 15, 16, 19, 20, 24, 25, 27, 28, 30, 10002
        };

        /// <summary>
        /// 是否为 iPhone
        /// </summary>
        /// <returns></returns>
        public static bool IsIPhone()
        {
            var deviceID = GetAppleDeviceGeneration();
            return IPhoneIDs.Contains(deviceID);
        }

        /// <summary>
        /// 是否为 iPad
        /// </summary>
        /// <returns></returns>
        public static bool IsIPad()
        {
            var deviceID = GetAppleDeviceGeneration();
            return IPadIDs.Contains(deviceID);
        }

        /// <summary>
        /// 获取苹果设备代数
        /// </summary>
        /// <returns></returns>
        public static int GetAppleDeviceGeneration()
        {
#if UNITY_EDITOR || UNITY_IOS
            return (int)UnityEngine.iOS.Device.generation;
#else
			return -100;
#endif
        }

        #endregion

        #region Android Device

        /// <summary>
        /// 是否为 Android Pad
        /// </summary>
        /// <returns></returns>
        public static bool IsAndroidPad()
        {
            return IsAndroid() && PadResolution();
        }

        /// <summary>
        /// 是否为 Android Phone
        /// </summary>
        /// <returns></returns>
        public static bool IsAndroidPhone()
        {
            return IsAndroid() && PhoneResolution();
        }

        /// <summary>
        /// 是否为 1.67 比例安卓设备
        /// </summary>
        /// <returns></returns>
        public static bool IsAndroidPhone167()
        {
            return IsAndroid() && Phone167Resolution();
        }

        /// <summary>
        /// 是否为 1.60 比例安卓设备
        /// </summary>
        /// <returns></returns>
        public static bool IsAndroidPhone160()
        {
            return IsAndroid() && Phone160Resolution();
        } 
        
        #endregion

        #region Resolution

        /// <summary>
        /// iPhone 5s height/width 1.78 1280x720
        /// </summary>
        /// <returns></returns>
        public static bool PhoneResolution()
        {
            var aspect = Screen.height > Screen.width ? (float)Screen.height / Screen.width : (float)Screen.width / Screen.height;
            return aspect > (16.0f / 9 - 0.05) && aspect < (16.0f / 9 + 0.05);
        }

        /// <summary>
        /// height/width = 1.67  iPhone 4/4s 960x640
        /// </summary>
        /// <returns></returns>
        public static bool Phone167Resolution()
        {
            var aspect = Screen.height > Screen.width ? (float)Screen.height / Screen.width : (float)Screen.width / Screen.height;
            return aspect > (1920.0f / 1152 - 0.05) && aspect < (1920.0f / 1152 + 0.05);
        }

        /// <summary>
        /// height/width = 1.5  iPhone 4/4s 960x640
        /// </summary>
        /// <returns></returns>
        public static bool Phone15Resolution()
        {
            var aspect = Screen.height > Screen.width ? (float)Screen.height / Screen.width : (float)Screen.width / Screen.height;
            return aspect > (960.0f / 640 - 0.05) && aspect < (960.0f / 640 + 0.05);
        }

        /// <summary>
        /// height/width = 1.60  HuaWei Pad?  2560 / 1600
        /// </summary>
        /// <returns></returns>
        public static bool Phone160Resolution()
        {
            var aspect = Screen.height > Screen.width ? (float)Screen.height / Screen.width : (float)Screen.width / Screen.height;
            return aspect > (2560.0f / 1600 - 0.05) && aspect < (2560.0f / 1600 + 0.05);
        }


        /// <summary>
        /// Pad resolution  iPad   2048 / 1536
        /// </summary>
        /// <returns></returns>
        public static bool PadResolution()
        {
            var aspect = Screen.height > Screen.width ? (float)Screen.height / Screen.width : (float)Screen.width / Screen.height;
            return aspect > (4.0f / 3 - 0.05) && aspect < (4.0f / 3 + 0.05);
        }

        #endregion
    }
}
