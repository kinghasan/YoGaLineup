/////////////////////////////////////////////////////////////////////////////
//
//  Script : UnityPackageExporter.cs
//  Info   : UnityPackage 导出配置工具
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aya.EditorScript
{
    [CreateAssetMenu(menuName = "Editor Tools/UnityPackageExporter", fileName = "UnityPackageExporter")]
    public class UnityPackageExporter : ScriptableObject
    {
        [Serializable]
        public class FileNameReplaceData
        {
            public string ReplaceStr;
            public string PlaceHolder;
        }

        [Serializable]
        public class ExportData
        {
            public string File;
            public ExportPackageOptions Options = ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies;
            public bool Dependencies;
            public List<Object> Assets;
        }

        // [SerializeReference] 
        public List<FileNameReplaceData> ReplaceDataList = new List<FileNameReplaceData>();

        // [SerializeReference] 
        public List<ExportData> ExportDataList = new List<ExportData>();

        [ContextMenu("Export")]
        public void Export()
        {
            var folderPath = EditorUtility.OpenFolderPanel("Select Export Folder", Application.dataPath, "");
            if (folderPath == "")
            {
                return;
            }

            Export(folderPath);
        }

        public void Export(string folderPath)
        {
            var extension = ".unitypackage";
            foreach (var exportData in ExportDataList)
            {
                var assetPathNames = new string[exportData.Assets.Count];

                for (var i = 0; i < assetPathNames.Length; i++)
                {
                    assetPathNames[i] = AssetDatabase.GetAssetPath(exportData.Assets[i]);
                }

                if (exportData.Dependencies)
                {
                    assetPathNames = AssetDatabase.GetDependencies(assetPathNames);
                }

                var packageFileName = exportData.File;
                foreach (var replaceData in ReplaceDataList)
                {
                    packageFileName = packageFileName.Replace(replaceData.PlaceHolder, replaceData.ReplaceStr);
                }

                if (!packageFileName.EndsWith(extension))
                {
                    packageFileName += extension;
                }

                var exportPath = Path.Combine(folderPath, packageFileName);
                AssetDatabase.ExportPackage(assetPathNames, exportPath, exportData.Options);
            }
        }
    }
}
#endif