/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CompressUtil.cs
//  Info     : 压缩操作辅助类
//  Warning  : 基于GZipStream，效率和压缩率一般
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System.IO;
using System.IO.Compression;

namespace Aya.Util
{
    public static class CompressUtil
    {
        #region Bytes

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="inputBytes">未压缩的字节数组</param>
        /// <returns>结果</returns>
        public static byte[] Compress(byte[] inputBytes)
        {
            using (var outStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(outStream, CompressionMode.Compress, true))
                {
                    zipStream.Write(inputBytes, 0, inputBytes.Length);
                    zipStream.Close();
                    return outStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="inputBytes">压缩字节数组</param>
        /// <returns>结果</returns>
        public static byte[] Decompress(byte[] inputBytes)
        {
            using (var inputStream = new MemoryStream(inputBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        var bytes = new byte[4096];
                        int n;
                        while ((n = zipStream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            outStream.Write(bytes, 0, n);
                        }

                        return outStream.ToArray();
                    }
                }
            }
        }

        #endregion

        #region Directory

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="dir">目录</param>
        public static void Compress(DirectoryInfo dir)
        {
            for (var i = 0; i < dir.GetFiles().Length; i++)
            {
                var fileToCompress = dir.GetFiles()[i];
                Compress(fileToCompress);
            }
        }

        /// <summary>
        /// 解压缩目录
        /// </summary>
        /// <param name="dir">目录</param>
        public static void Decompress(DirectoryInfo dir)
        {
            for (var i = 0; i < dir.GetFiles().Length; i++)
            {
                var fileToCompress = dir.GetFiles()[i];
                Decompress(fileToCompress);
            }
        }

        #endregion

        #region File

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToCompress">未压缩文件</param>
        public static void Compress(FileInfo fileToCompress)
        {
            using (var originalFileStream = fileToCompress.OpenRead())
            {
                if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (var compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                    {
                        using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            var bytes = new byte[4096];
                            int n;
                            while ((n = compressionStream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                originalFileStream.Write(bytes, 0, n);
                            }

                            originalFileStream.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="fileToDecompress">压缩文件</param>
        public static void Decompress(FileInfo fileToDecompress)
        {
            using (var originalFileStream = fileToDecompress.OpenRead())
            {
                var currentFileName = fileToDecompress.FullName;
                var newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
                using (var decompressedFileStream = File.Create(newFileName))
                {
                    using (var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        var bytes = new byte[4096];
                        int n;
                        while ((n = decompressionStream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            decompressedFileStream.Write(bytes, 0, n);
                        }

                        decompressedFileStream.Close();
                    }
                }
            }
        }

        #endregion
    }
}