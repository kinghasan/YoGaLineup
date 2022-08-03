/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SaveDataMenu.cs
//  Info     : SaveData 菜单
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using System.IO;
using Aya.Util;
using UnityEditor;

namespace Aya.Data.Persistent
{
    public static class SaveDataMenu
    {
        [MenuItem(MenuUtil.MenuTitle + "Data/Save Data/Open SaveData Folder", false, 0)]
        public static void OpenSaveDataPath()
        {
            var path = SaveData.SavePath;
            EditorUtility.RevealInFinder(path);
        }

        [MenuItem(MenuUtil.MenuTitle + "Data/Save Data/Clear SaveData Path", false, 1)]
        public static void ClearSaveDataPath()
        {
            var path = SaveData.SavePath;
            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
    }
}
#endif