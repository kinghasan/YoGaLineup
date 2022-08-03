/////////////////////////////////////////////////////////////////////////////
//
//  Script   : FileUtil.cs
//  Info     : 文件操作辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;

namespace Aya.Util
{
	/// <summary>
	/// 文件类型枚举
	/// </summary>
	public enum FileType
	{
		Picture,
		Code,
		Script,
		Temp
	}

	/// <summary>
	/// 文件操作辅助类
	/// </summary>
	public static class FileUtil
	{
		#region File Formart

		/// <summary>
		/// 图片文件格式
		/// </summary>
		private static readonly string[] FileFormatPicture = { "jpg", "jpeg", "bmp", "png", "tga", "psd", "gif", "tiff" };
		/// <summary>
		/// 代码文件格式
		/// </summary>
		private static readonly string[] FileFormatCode = { "cs", "js", "shader", "cginc" };
		/// <summary>
		/// 脚本文件格式
		/// </summary>
		private static readonly string[] FileFormatScript = { "lua" };
		/// <summary>
		/// 元数据文件格式
		/// </summary>
		private static readonly string[] FileFormatTemp = { "meta" };

		#endregion

		#region Formart Check

		/// <summary>
		/// 检查文件类型
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <param name="type">文件类型</param>
		/// <returns>结果</returns>
		public static bool FileFormatCheck(string filePath, FileType type)
		{
			string[] fileFormart = { };
			switch (type)
			{
				case FileType.Picture:
					fileFormart = FileFormatPicture;
					break;
				case FileType.Code:
					fileFormart = FileFormatCode;
					break;
				case FileType.Script:
					fileFormart = FileFormatScript;
					break;
				case FileType.Temp:
					fileFormart = FileFormatTemp;
					break;
			}
			var pathSplit = filePath.Split('.');
			if (pathSplit.Length < 1) return false;
			var formart = pathSplit[pathSplit.Length - 1];
			return Array.IndexOf(fileFormart, formart) > -1;
		}

        #endregion

        #region Directory

        /// <summary>
        /// 判断文件夹是否为空（不存在的路径也认为是空）
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>结果</returns>
	    public static bool IsDirectoryEmpty(string path)
	    {
	        if (string.IsNullOrEmpty(path)) return true;
	        if (!Directory.Exists(path)) return true;
	        var result = Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0;
	        return result;
	    }

        #endregion
    }

}
