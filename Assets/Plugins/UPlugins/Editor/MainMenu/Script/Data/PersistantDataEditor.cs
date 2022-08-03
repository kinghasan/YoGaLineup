/////////////////////////////////////////////////////////////////////////////
//
//  Script : PersistantDataEditor.cs
//  Info   : 目录操作
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using System;
using System.IO;
using Aya.Util;
using UnityEngine;
using UnityEditor;

namespace Aya.EditorScript
{
    public class PersistantDataEditor : MonoBehaviour
    {
        /// <summary>
        /// 清除清除PersistantData目录
        /// </summary>
        [MenuItem(MenuUtil.MenuTitle + "Data/Persistant/Clear PersistantData Path", false)]
        private static void DeletePersistantDataPath()
        {
            try
            {
                var path = Application.persistentDataPath;
                Directory.Delete(path, true);
                Debug.Log("Clear PersistantData Path Success! ");
            }
            catch (Exception e)
            {
                Debug.LogError("Clear PersistantDataPath Filed : " + e);
            }
        }
    }
}

#endif