/////////////////////////////////////////////////////////////////////////////
//
//  Script   : FileInfoExtension.cs
//  Info     : FileInfo 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;

namespace Aya.Extension
{
    public static class FileInfoExtension
    {
        /// <summary>
        /// 重命名（包含扩展名）
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <param name="newName">新名称</param>
        /// <returns>文件信息</returns>
        public static FileInfo Rename(this FileInfo fileInfo, string newName)
        {
            var directoryName = Path.GetDirectoryName(fileInfo.FullName);
            if (directoryName == null) throw new NullReferenceException(nameof(directoryName));
            var filePath = Path.Combine(directoryName, newName);
            fileInfo.MoveTo(filePath);
            return fileInfo;
        }

        /// <summary>
        /// 重命名（不包含扩展名）
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <param name="newName">新名称</param>
        /// <returns>文件信息</returns>
        public static FileInfo RenameFileWithoutExtension(this FileInfo fileInfo, string newName)
        {
            var fileName = string.Concat(newName, fileInfo.Extension);
            fileInfo.Rename(fileName);
            return fileInfo;
        }

        /// <summary>
        /// 修改扩展名
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <param name="newExtension">新扩展名</param>
        /// <returns>文件信息</returns>
        public static FileInfo ChangeExtension(this FileInfo fileInfo, string newExtension)
        {
            newExtension = newExtension.EnsureStartsWith(".");
            var fileName = string.Concat(Path.GetFileNameWithoutExtension(fileInfo.FullName), newExtension);
            fileInfo.Rename(fileName);
            return fileInfo;
        }

        /// <summary>
        /// 批量修改扩展名
        /// </summary>
        /// <param name="fileInfos">文件信息</param>
        /// <param name="newExtension">新扩展名</param>
        /// <returns>文件信息</returns>
        public static FileInfo[] ChangeExtensions(this FileInfo[] fileInfos, string newExtension)
        {
            fileInfos.ForEach(f => f.ChangeExtension(newExtension));
            return fileInfos;
        }

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileInfos">文件信息</param>
        public static void Delete(this FileInfo[] fileInfos)
        {
            foreach (var file in fileInfos)
            {
                file.Delete();
            }
        }
    }
}
