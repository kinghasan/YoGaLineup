/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ExcelToJsonSetting.cs
//  Info     : Excel 转 Json 配置
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Aya.Extension;

namespace Aya.Data.Excel
{
    [CreateAssetMenu(menuName = "Setting/Excel To Json Setting", fileName = "ExcelToJsonSetting")]
    public class ExcelToJsonSetting : ScriptableObject
    {
        #region Instance

        public static ExcelToJsonSetting Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = Resources.Load<ExcelToJsonSetting>("Setting/" + nameof(ExcelToJsonSetting));
                    if (_ins == null)
                    {
                        _ins = Resources.Load<ExcelToJsonSetting>(nameof(ExcelToJsonSetting));
                    }
                }
                return _ins;
            }
        }
        private static ExcelToJsonSetting _ins;

        #endregion

        #region Field

        /// <summary>
        /// 输入文件夹
        /// </summary>
        [Header("Path")]
        public DefaultAsset InputExcelFolder;

        /// <summary>
        /// 输出 Json 文件夹
        /// </summary>
        public DefaultAsset OutputJsonFolder;

        /// <summary>
        /// 导出 CSharp 文件夹
        /// </summary>
        public DefaultAsset OutputCSharpFolder;

        /// <summary>
        /// 调控表格中的空值<para/>
        /// 仅针对 string 类型<para/>
        /// 会导致每个json结构长度不同，但可以节省空间
        /// </summary>
        [Header("Export Json")]
        public bool SkipNullValue = false;

        /// <summary>
        /// 允许同表不同列<para/>
        /// 同一个类存在大量可空参数时可以使用
        /// </summary>
        public bool AllowUnequalColumn = true;

        /// <summary>
        /// 每个项目使用单独的Json文件<para/>
        /// 如果使用，则以ID命名，以类名为文件夹
        /// </summary>
        public bool IndependentJsonFile = false;

        /// <summary>
        /// 加密导出的文件
        /// </summary>
        public bool Encrypt = true;

        /// <summary>
        /// 导出代码
        /// </summary>
        [Header("Export CSharp")]
        public string Namespace = "Client";

        /// <summary>
        /// 导出文件的扩展名
        /// </summary>
        [Header("Param")]
        public string Extension = ".json";

        /// <summary>
        /// 记录的主键名称
        /// </summary>
        public string RecordKey = "RecordID";

        /// <summary>
        /// 清单文件文件名
        /// </summary>
        public string ManifestFileName = "Manifest";

        /// <summary>
        /// 同字段多个值分隔符
        /// </summary>
        public string MultiParamSplit = ",";

        /// <summary>
        /// 同字段多组值分隔符
        /// </summary>
        public string MultiGroupSplit = "|";

        #endregion

        #region Property

        /// <summary>
        /// 导入 Excel 路径
        /// </summary>
        public string InputExcelPath => AssetDatabase.GetAssetPath(InputExcelFolder);

        /// <summary>
        /// 导出 Json 路径
        /// </summary>
        public string OutputJsonPath => AssetDatabase.GetAssetPath(OutputJsonFolder);

        /// <summary>
        /// 导出 CSharp 路径
        /// </summary>
        public string OutputCSharpPath => AssetDatabase.GetAssetPath(OutputCSharpFolder);

        /// <summary>
        /// 清单文件完整文件名
        /// </summary>
        public string ManifestFileFullName => ManifestFileName + Extension;

        /// <summary>
        /// 清单文件路径
        /// </summary>
        public string ManifestFilePath => Path.Combine(OutputJsonPath, ManifestFileFullName);

        #endregion

        #region Context Menu

        [ContextMenu("Export Json")]
        public void ExportJson()
        {
            var inputPath = InputExcelPath;
            var outputPath = OutputJsonPath;

            Directory.Delete(outputPath, true);
            Directory.CreateDirectory(outputPath);

            ExcelToJson.ClearCache();
            var files = Directory.GetFiles(inputPath, "*.*", SearchOption.AllDirectories);
            var excelFiles = files.FindAll(path => !path.EndsWith(".meta"));
            try
            {
                for (var i = 0; i < excelFiles.Count; i++)
                {
                    var path = excelFiles[i];
                    var fileName = Path.GetFileName(path);
                    var title = "Excel To Json " + (i + 1) + "/" + excelFiles.Count;
                    var info = "Import " + fileName;
                    var progress = i * 1f / excelFiles.Count;
                    EditorUtility.DisplayProgressBar(title, info, progress);
                    ExcelToJson.ExportAllTableToJson(path, outputPath);
                }
                ExcelToJson.CreateManifestFile();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            EditorUtility.ClearProgressBar();
            ExcelToJson.ClearCache();
            AssetDatabase.Refresh();
        }

        [ContextMenu("Export C#")]
        public void ExportCSharp()
        {
            var inputPath = InputExcelPath;
            var outputPath = OutputCSharpPath;

            Directory.Delete(outputPath, true);
            Directory.CreateDirectory(outputPath);

            ExcelToJson.ClearCache();
            var files = Directory.GetFiles(inputPath, "*.*", SearchOption.AllDirectories);
            var excelFiles = files.FindAll(path => !path.EndsWith(".meta"));
            try
            {
                for (var i = 0; i < excelFiles.Count; i++)
                {
                    var path = excelFiles[i];
                    var fileName = Path.GetFileName(path);
                    var title = "Excel To CSharp " + (i + 1) + "/" + excelFiles.Count;
                    var info = "Import " + fileName;
                    var progress = i * 1f / excelFiles.Count;
                    EditorUtility.DisplayProgressBar(title, info, progress);
                    ExcelToJson.ExportAllTableToCSharp(path, outputPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            EditorUtility.ClearProgressBar();
            ExcelToJson.ClearCache();
            AssetDatabase.Refresh();
        }

        [ContextMenu("Open Input Excel Path")]
        public void OpenInputExcelPath()
        {
            EditorUtility.RevealInFinder(InputExcelPath);
        }

        [ContextMenu("Open 0utput Json Path")]
        public void OpenOutputJsonPath()
        {
            EditorUtility.RevealInFinder(OutputJsonPath);
        }

        [ContextMenu("Open 0utput C# Path")]
        public void OpenOutputCSharpPath()
        {
            EditorUtility.RevealInFinder(OutputCSharpPath);
        }

        #endregion
    }
}
#endif