/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ExcelReader.cs
//  Info     : Excel 读取工具类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.Data;
using System.IO;
using Excel;

namespace Aya.Data.Excel
{
    public static class ExcelReader
    {
        /// <summary>
        /// 将 excel 文件读取为 DataSet 格式
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>结果</returns>
        public static DataSet ReadAsDataSet(string filePath)
        {
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = null;
            var extension = Path.GetExtension(filePath).ToLower();
            if (extension.EndsWith(".xls"))
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }

            if (extension.EndsWith(".xlsx") || extension.EndsWith(".xlsm"))
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            if (excelReader == null)
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }

            var result = excelReader.AsDataSet();
            return result;
        }
    }
}
