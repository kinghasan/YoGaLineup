/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ExcelToJson.cs
//  Info     : Excel 转换为 Json
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using Aya.Data.Json;
using Aya.Extension;
using Aya.Security;

namespace Aya.Data.Excel
{
    public static class ExcelToJson
    {
        #region Cache

        /// <summary>
        /// 类型/文件夹名 缓存
        /// </summary>
        private static readonly List<string> ClassList = new List<string>();

        /// <summary>
        /// 类型(文件夹名) - 文件列表 缓存
        /// </summary>
        private static readonly Dictionary<string, List<string>> FileDic = new Dictionary<string, List<string>>();

        /// <summary>
        /// 类型(文件夹名) - 表格列表 缓存
        /// </summary>
        private static readonly Dictionary<string, List<DataTable>> TableDic = new Dictionary<string, List<DataTable>>();

        /// <summary>
        /// 清除导出缓存
        /// </summary>
        public static void ClearCache()
        {
            ClassList.Clear();
            FileDic.Clear();
            TableDic.Clear();
        }

        #endregion

        #region Table To Json

        /// <summary>
        /// 导出所有的表到 Json 文件
        /// </summary>
        /// <param name="inputPath">导入路径</param>
        /// <param name="outputPath">导出路径</param>
        public static void ExportAllTableToJson(string inputPath, string outputPath)
        {
            var dataSet = ExcelReader.ReadAsDataSet(inputPath);
            var tables = dataSet.Tables;
            for (var i = 0; i < tables.Count; i++)
            {
                var table = tables[i];
                if (ExcelToJsonSetting.Ins.IndependentJsonFile)
                {
                    // 多文件
                    var className = GetTableName(table);
                    if (string.IsNullOrEmpty(className)) continue;

                    ICollectionExtension.AddUnique(ClassList, className);
                    var jsons = GetJsonList(table, className);
                    var outputDir = Path.Combine(outputPath, className);
                    if (!Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    for (var j = 0; j < jsons.Count; j++)
                    {
                        var json = jsons[j];
                        var recordID = JObject.Parse(json)[ExcelToJsonSetting.Ins.RecordKey].AsInt().ToString();
                        var fileName = recordID + ExcelToJsonSetting.Ins.Extension;
                        var outputFile = Path.Combine(outputDir, fileName);
                        if (ExcelToJsonSetting.Ins.Encrypt)
                        {
                            json = AESUtil.Encrypt(json);
                        }

                        File.WriteAllText(outputFile, json);
                    }
                }
                else
                {
                    // 单文件，先缓存所有同名数据表
                    var className = GetTableName(table);
                    if (string.IsNullOrEmpty(className)) continue;

                    ICollectionExtension.AddUnique(ClassList, className);

                    if (!TableDic.TryGetValue(className, out var tableList))
                    {
                        tableList = new List<DataTable>();
                        TableDic.Add(className, tableList);
                    }

                    tableList.Add(table);
                }
            }

            if (!ExcelToJsonSetting.Ins.IndependentJsonFile)
            {
                // 但文件模式，将同名数据表合并为一个文件
                foreach (var kv in TableDic)
                {
                    var className = kv.Key;
                    var tableList = kv.Value;

                    var json = GetJson(tableList);
                    var fileName = className + ExcelToJsonSetting.Ins.Extension;
                    var outputFile = Path.Combine(outputPath, fileName);
                    if (ExcelToJsonSetting.Ins.Encrypt)
                    {
                        json = AESUtil.Encrypt(json);
                    }
                    File.WriteAllText(outputFile, json);
                }
            }
        }

        /// <summary>
        /// 获取表明（类名）
        /// </summary>
        /// <param name="table">表</param>
        /// <returns>结果</returns>
        public static string GetTableName(DataTable table)
        {
            var result = table.Rows[0][0].ToString();
            return result;
        }

        #endregion

        #region Table To CSharp

        /// <summary>
        /// 导出所有的表到 CSharp 脚本
        /// </summary>
        /// <param name="inputPath">导入路径</param>
        /// <param name="outputPath">导出路径</param>
        public static void ExportAllTableToCSharp(string inputPath, string outputPath)
        {
            var dataSet = ExcelReader.ReadAsDataSet(inputPath);
            var tables = dataSet.Tables;
            for (var i = 0; i < tables.Count; i++)
            {
                var table = tables[i];
                var className = GetTableName(table);
                GetClassFileds(table, out var types, out var fields);
                var script = GetCSharpScript(className, types, fields);
                var fileName = className + ".cs";
                var outputFile = Path.Combine(outputPath, fileName);
                File.WriteAllText(outputFile, script);
            }
        }

        /// <summary>
        /// 获取类的字段类型和名称列表
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="types">类型列表</param>
        /// <param name="fields">字段列表</param>
        public static void GetClassFileds(DataTable table, out List<string> types, out List<string> fields)
        {
            types = new List<string>();
            fields = new List<string>();

            types.Add("long");
            fields.Add(ExcelToJsonSetting.Ins.RecordKey);

            var columnsNum = table.Columns.Count;

            var index = 0;
            for (var i = 1; i < columnsNum; i++)
            {
                var field = table.Rows[index][i].ToString();
                if (string.IsNullOrEmpty(field)) break;
                fields.Add(field);
            }
            index = 2;
            for (var i = 1; i < columnsNum; i++)
            {
                var type = table.Rows[index][i].ToString();
                if (string.IsNullOrEmpty(type)) break;
                types.Add(type);
            }
        }

        /// <summary>
        /// 创建 CSharp 脚本
        /// </summary>
        /// <param name="className">类型</param>
        /// <param name="types">类型列表</param>
        /// <param name="fields">字段列表</param>
        /// <returns>结果</returns>
        public static string GetCSharpScript(string className, List<string> types, List<string> fields)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("// This script is created by programed, don't change it pls.\n\n");
            if (!string.IsNullOrEmpty(ExcelToJsonSetting.Ins.Namespace))
            {
                stringBuilder.Append("namespace ");
                stringBuilder.Append(ExcelToJsonSetting.Ins.Namespace);
                stringBuilder.Append("\n");
            }

            stringBuilder.Append("{\n");
            stringBuilder.Append("\tpublic class ");
            stringBuilder.Append(className);
            stringBuilder.Append("\n");
            stringBuilder.Append("\t{\n");
            for (var i = 0; i < types.Count; i++)
            {
                var type = types[i];
                var field = fields[i];
                stringBuilder.Append("\t\tpublic ");
                stringBuilder.Append(type);
                stringBuilder.Append(" ");
                stringBuilder.Append(field);
                stringBuilder.Append(";\n");
            }

            stringBuilder.Append("\t}\n");
            stringBuilder.Append("}");
            var script = stringBuilder.ToString();
            return script;
        }

        #endregion

        #region Manifest

        /// <summary>
        /// 创建清单文件
        /// </summary>
        public static void CreateManifestFile()
        {
            var json = new JArray();
            var jConfig = new JObject { ["IndependentJsonFile"] = ExcelToJsonSetting.Ins.IndependentJsonFile };
            json.Add(jConfig);
            foreach (var className in ClassList)
            {
                var jClass = new JObject { ["ClassName"] = className };
                var jFiles = new JArray();
                var files = FileDic.GetValue(className);
                foreach (var fileName in files)
                {
                    jFiles.Add(fileName);
                }

                jClass[className] = jFiles;
                json.Add(jClass);
            }

            var text = json.ToFormatString();
            if (ExcelToJsonSetting.Ins.Encrypt)
            {
                text = AESUtil.Encrypt(text);
            }

            File.WriteAllText(ExcelToJsonSetting.Ins.ManifestFilePath, text);
        }

        #endregion

        #region Json

        /// <summary>
        /// 获取所有数据，合并为一个 Json 数组
        /// </summary>
        /// <param name="table">表</param>
        /// <returns>结果</returns>
        public static string GetJson(DataTable table)
        {
            var className = GetTableName(table);
            var stringBuilder = new StringBuilder();
            var jsonList = GetJsonList(table, className, 0);
            stringBuilder.Append("[");

            for (var i = 0; i < jsonList.Count; i++)
            {
                var json = jsonList[i];
                stringBuilder.Append(json);
                if (i == jsonList.Count - 1) continue;
                stringBuilder.Append(",\n");
            }

            stringBuilder.Append("]");
            var result = stringBuilder.ToString();
            return result;
        }

        /// <summary>
        /// 获取所有数据，合并为一个 Json 数组
        /// </summary>
        /// <param name="tables">表</param>
        /// <returns>结果</returns>
        public static string GetJson(List<DataTable> tables)
        {
            var className = GetTableName(tables[0]);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");

            for (var i = 0; i < tables.Count; i++)
            {
                var table = tables[i];
                var jsonList = GetJsonList(table, className, 0);
                for (var j = 0; j < jsonList.Count; j++)
                {
                    var json = jsonList[j];
                    stringBuilder.Append(json);
                    if (j == jsonList.Count - 1 && i == tables.Count - 1) continue;
                    stringBuilder.Append(",\n");
                }
            }

            stringBuilder.Append("]");
            var result = stringBuilder.ToString();
            return result;
        }

        /// <summary>
        /// 获取所有 Json
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="className">类名</param>
        /// <param name="startRowIndex">开始索引</param>
        /// <returns></returns>
        public static List<string> GetJsonList(DataTable table, string className, int startRowIndex = 0)
        {
            var result = new List<string>();
            var index = startRowIndex;
            var columnsNum = table.Columns.Count;
            var fileList = FileDic.GetOrAdd(className, new List<string>());

            do
            {
                // 第1行 确定类名，和属性名列表
                if (!CheckRowValid(table, index)) return result;
                var propertyNames = new List<string>();
                var values = new List<string>();
                propertyNames.Add(ExcelToJsonSetting.Ins.RecordKey);
                for (var i = 1; i < columnsNum; i++)
                {
                    var propertyName = table.Rows[index][i].ToString();
                    if (string.IsNullOrEmpty(propertyName)) break;
                    propertyNames.Add(propertyName);
                }

                // 第2行 属性描述，无需读取
                index++;
                if (!CheckRowValid(table, index)) return result;
                var isDescription = table.Rows[index][0].ToString() == "Description";

                // 第3行 确定属性类型
                if (isDescription) index++;
                if (!CheckRowValid(table, index)) return result;
                var types = new List<string> { "long" };
                for (var i = 1; i < columnsNum; i++)
                {
                    var type = table.Rows[index][i].ToString();
                    if (string.IsNullOrEmpty(type)) break;
                    types.Add(type);
                }

                // 第3行 开始读取数据
                index++;
                do
                {
                    if (!CheckRowValid(table, index)) return result;
                    var isClassName = table.Rows[index][0].ToString() == className;
                    if (isClassName)
                    {
                        break;
                    }

                    var isRecordID = int.TryParse(table.Rows[index][0].ToString(), out var recordId);
                    if (!isRecordID)
                    {
                        return result;
                    }

                    values.Clear();
                    values.Add(recordId.ToString());
                    fileList.Add(recordId.ToString());
                    for (var i = 1; i < propertyNames.Count; i++)
                    {
                        var sourceValue = table.Rows[index][i];
                        var type = types[i];
                        var value = GetValueWithType(sourceValue, type);
                        values.Add(value);
                    }

                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("{\n");
                    for (var i = 0; i < propertyNames.Count; i++)
                    {
                        var value = values[i];
                        if (string.IsNullOrEmpty(value) && ExcelToJsonSetting.Ins.SkipNullValue) continue;
                        stringBuilder.Append("\t\"");
                        stringBuilder.Append(propertyNames[i]);
                        stringBuilder.Append("\":");
                        stringBuilder.Append(value);
                        if (i < propertyNames.Count - 1)
                        {
                            stringBuilder.Append(",");
                        }

                        stringBuilder.Append("\n");
                    }

                    stringBuilder.Append("}");
                    var record = stringBuilder.ToString();
                    result.Add(record);

                    index++;
                } while (true);
            } while (true);
        }

        /// <summary>
        /// 检查指定行是否为有效行
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="rowIndex">行索引</param>
        /// <returns>结果</returns>
        public static bool CheckRowValid(DataTable table, int rowIndex)
        {
            var inRange = rowIndex <= table.Rows.Count - 1;
            if (!inRange) return false;
            var key = table.Rows[rowIndex][0].ToString();
            var keyValid = !string.IsNullOrEmpty(key);
            if (!keyValid) return false;
            return true;
        }

        #endregion

        #region Value

        /// <summary>
        /// 根据类型获取值
        /// </summary>
        /// <param name="sourceValue">元数据</param>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static string GetValueWithType(object sourceValue, string type)
        {
            switch (type)
            {
                case "int":
                {
                    var success = int.TryParse(sourceValue.ToString(), out var result);
                    if (!success)
                    {
                        result = default(int);
                    }

                    return result.ToString();
                }
                case "json":
                {
                    var result = sourceValue.ToString();
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "null";
                    }

                    return result;
                }
                case "array":
                {
                    var result = sourceValue.ToString();
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "";
                    }

                    result = "[" + result + "]";
                    return result;
                }
                case "string":
                {
                    var result = sourceValue.ToString();
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "";
                    }

                    result = "\"" + result + "\"";
                    return result;
                }
                case "float":
                {
                    var success = float.TryParse(sourceValue.ToString(), out var result);
                    if (!success)
                    {
                        result = default(float);
                    }

                    return result.ToString(CultureInfo.InvariantCulture);
                }
                case "double":
                {
                    var success = double.TryParse(sourceValue.ToString(), out var result);
                    if (!success)
                    {
                        result = default(double);
                    }

                    return result.ToString(CultureInfo.InvariantCulture);
                }
                case "long":
                {
                    var success = long.TryParse(sourceValue.ToString(), out var result);
                    if (!success)
                    {
                        result = default(long);
                    }

                    return result.ToString();
                }
                case "bool":
                {
                    var success = bool.TryParse(sourceValue.ToString(), out var result);
                    if (!success)
                    {
                        result = default(bool);
                    }

                    return result ? "true" : "false";
                }
            }

            return "";
        }

        #endregion
    }
}
#endif