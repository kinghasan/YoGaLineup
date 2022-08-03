/////////////////////////////////////////////////////////////////////////////
//
//  Script : FindPrefabUsingEditor.cs
//  Info   : 查找在所有场景中的引用。
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio / Change from http://www.xuanyusong.com/archives/2576 http://www.xuanyusong.com/archives/4207
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Aya.Util;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Aya.EditorScript
{
    public class FindReferenceEditor : MonoBehaviour
    {
        #region File Reference

        [MenuItem(MenuUtil.MenuTitle + "Resources/Find selected file reference", false)]
        public static void FindSelectedFileReference()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Selected file is NULL");
                return;
            }

            var guid = AssetDatabase.AssetPathToGUID(path);
            var extensions = new List<string>() {".prefab", ".unity", ".mat", ".asset"};
            var files = Enumerable.ToArray(Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => extensions.Contains(Path.GetExtension(s).ToLower())));
            var startIndex = 0;

            EditorApplication.update = delegate()
            {
                var file = files[startIndex];
                var isCancel = EditorUtility.DisplayCancelableProgressBar("Searching...", file,
                    (float) startIndex / (float) files.Length);
                if (Regex.IsMatch(File.ReadAllText(file), guid))
                {
                    Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
                }

                startIndex++;
                if (isCancel || startIndex >= files.Length)
                {
                    EditorUtility.ClearProgressBar();
                    EditorApplication.update = null;
                    startIndex = 0;
                    Debug.Log("Search Complete");
                }
            };
        }

        [MenuItem(MenuUtil.MenuTitle + "Resources/Find copied file GUID reference", false)]
        public static void FindCopiedFileGUIDReference()
        {
            var findGUID = GUIUtility.systemCopyBuffer;
            if (string.IsNullOrEmpty(findGUID))
            {
                Debug.LogError("Copied GUID is NULL");
                return;
            }

            EditorSettings.serializationMode = SerializationMode.ForceText;
            var extensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset", ".meta" };
            var directoryPath = Application.dataPath;
            var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                .Where(s => extensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
            var startIndex = 0;

            EditorApplication.update = delegate ()
            {
                var file = files[startIndex];
                var isCancel = EditorUtility.DisplayCancelableProgressBar("Searching...", file, (float)startIndex / (float)files.Length);
                if (Regex.IsMatch(File.ReadAllText(file), findGUID))
                {
                    Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
                }

                startIndex++;
                if (isCancel || startIndex >= files.Length)
                {
                    EditorUtility.ClearProgressBar();
                    EditorApplication.update = null;
                    startIndex = 0;
                    Debug.Log("Search Complete");
                }
            };
        }

        [MenuItem(MenuUtil.MenuTitle + "Resources/Find copied file GUID reference in PackageCache", false)]
        public static void FindCopiedFileGUIDPackageCacheReference()
        {
            var findGUID = GUIUtility.systemCopyBuffer;
            if (string.IsNullOrEmpty(findGUID))
            {
                Debug.LogError("Copied GUID is NULL");
                return;
            }

            EditorSettings.serializationMode = SerializationMode.ForceText;
            var extensions = new List<string>() { ".prefab", ".unity", ".mat", ".asset", ".meta" };
            var directoryPath = Application.dataPath.Replace("Assets", "") + "Library\\PackageCache";
            var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                .Where(s => extensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
            var startIndex = 0;

            EditorApplication.update = delegate ()
            {
                var file = files[startIndex];
                var isCancel = EditorUtility.DisplayCancelableProgressBar("Searching...", file, (float)startIndex / (float)files.Length);

                if (Regex.IsMatch(File.ReadAllText(file), findGUID))
                {
                    Debug.Log(file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(file)));
                }

                startIndex++;
                if (isCancel || startIndex >= files.Length)
                {
                    EditorUtility.ClearProgressBar();
                    EditorApplication.update = null;
                    startIndex = 0;
                    Debug.Log("Search Complete");
                }
            };
        }

        [MenuItem(MenuUtil.MenuTitle + "Resources/Find file with copied GUID", false)]
        public static void FindCopiedFileGUID()
        {
            var findGUID = GUIUtility.systemCopyBuffer;
            if (string.IsNullOrEmpty(findGUID))
            {
                Debug.LogError("Copied GUID is NULL");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(findGUID);
            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log(path, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(path)));
            }
            else
            {
                Debug.LogError("Not found " + findGUID);
            }
        }

        public static string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }

        #endregion

        #region Prefab Reference

        /// <summary>
        /// 查找预制在所有场景中的引用
        /// </summary>
        [MenuItem(MenuUtil.MenuTitle + "Resources/Find prefab reference in scene", false)]
        private static void FindPrefabUsingInAllScenes()
        {
            // 确保鼠标右键选择的是一个Prefab
            if (Selection.gameObjects.Length != 1)
            {
                Debug.LogError("Selected GameObject is NULL");
                return;
            }
            // 遍历所有游戏场景
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled) continue;
                // 打开场景
                EditorSceneManager.OpenScene(scene.path);
                // 获取场景中的所有游戏对象
                var gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
                foreach (var go in gos)
                {
                    // 判断GameObject是否为一个Prefab的引用
                    if (PrefabUtility.GetPrefabAssetType(go) == PrefabAssetType.Regular)
                    {
                        var parentObject = PrefabUtility.GetCorrespondingObjectFromSource(go);
                        // 判断GameObject的Prefab是否和右键选择的Prefab是同一路径。
                        if (parentObject == Selection.activeGameObject)
                        {
                            // 输出场景名，以及Prefab引用的路径
                            Debug.Log(scene.path + "/" + GetGameObjectPath(go));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查找预制在所有场景中的引用(包含子节点)
        /// </summary>
        [MenuItem(MenuUtil.MenuTitle + "Resources/Find prefab reference in scene (Include Child)", false)]
        private static void FindPrefabAllUsingInAllScenes()
        {
            // 确保鼠标右键选择的是一个Prefab
            if (Selection.gameObjects.Length != 1)
            {
                Debug.LogError("Selected GameObject is NULL");
                return;
            }

            // 遍历所有游戏场景
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled) continue;
                // 打开场景
                EditorSceneManager.OpenScene(scene.path);
                // 获取场景中的所有游戏对象
                var gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
                foreach (var go in gos)
                {
                    // 判断GameObject是否为一个Prefab的引用
                    if (PrefabUtility.GetPrefabAssetType(go) == PrefabAssetType.Regular)
                    {
                        var parentObject = PrefabUtility.GetCorrespondingObjectFromSource(go);
                        var path = AssetDatabase.GetAssetPath(parentObject);
                        // 判断GameObject的Prefab是否和右键选择的Prefab是同一路径。
                        if (path == AssetDatabase.GetAssetPath(Selection.activeGameObject))
                        {
                            Debug.Log(scene.path + "/" + GetGameObjectPath(go));
                        }
                    }
                }
            }
        } 

        #endregion

        /// <summary>
        /// 获取对象的引用路径
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>路径</returns>
        public static string GetGameObjectPath(GameObject obj)
        {
            var path = "/" + obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }

            return path;
        }
    }
}
#endif