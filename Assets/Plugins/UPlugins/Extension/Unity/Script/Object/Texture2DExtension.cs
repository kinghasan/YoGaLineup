/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Texture2DExtension.cs
//  Info     : Texture2D 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System.IO;
using UnityEngine;

namespace Aya.Extension
{
    public static class Texture2DExtension
    {
        #region Load File

        /// <summary>
        /// 从文件读取图片
        /// </summary>
        /// <param name="texture2D">贴图</param>
        /// <param name="path">路径</param>
        /// <returns>贴图</returns>
        public static Texture2D LoadFromFile(this Texture2D texture2D, string path)
        {
            var bytes = File.ReadAllBytes(path);
            texture2D.LoadImage(bytes);
            return texture2D;
        } 

        #endregion

        #region Save File

        /// <summary>
        /// 保存到文件（默认PNG格式）
        /// </summary>
        /// <param name="texture2D">贴图</param>
        /// <param name="path">路径</param>
        /// <returns>贴图</returns>
        public static Texture2D SaveToFile(this Texture2D texture2D, string path)
        {
            SaveToFilePNG(texture2D, path);
            return texture2D;
        }

        /// <summary>
        /// 保存到文件（EXR格式）
        /// </summary>
        /// <param name="texture2D">贴图</param>
        /// <param name="path">路径</param>
        /// <returns>贴图</returns>
        public static Texture2D SaveToFileEXR(this Texture2D texture2D, string path)
        {
            var bytes = texture2D.EncodeToEXR();
            File.WriteAllBytes(path, bytes);
            return texture2D;
        }

        /// <summary>
        /// 保存到文件（TGA格式）
        /// </summary>
        /// <param name="texture2D">贴图</param>
        /// <param name="path">路径</param>
        /// <returns>贴图</returns>
        public static Texture2D SaveToFileTGA(this Texture2D texture2D, string path)
        {
            var bytes = texture2D.EncodeToTGA();
            File.WriteAllBytes(path, bytes);
            return texture2D;
        }

        /// <summary>
        /// 保存到文件（JPG格式）
        /// </summary>
        /// <param name="texture2D">贴图</param>
        /// <param name="path">路径</param>
        /// <returns>贴图</returns>
        public static Texture2D SaveToFileJPG(this Texture2D texture2D, string path)
        {
            var bytes = texture2D.EncodeToJPG();
            File.WriteAllBytes(path, bytes);
            return texture2D;
        }

        /// <summary>
        /// 保存到文件（PNG格式）
        /// </summary>
        /// <param name="texture2D">贴图</param>
        /// <param name="path">路径</param>
        /// <returns>贴图</returns>
        public static Texture2D SaveToFilePNG(this Texture2D texture2D, string path)
        {
            var bytes = texture2D.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
            return texture2D;
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="texture2D">贴图</param>
        /// <param name="clockwise">顺时针</param>
        /// <returns>结果</returns>
        public static Texture2D Rotate(this Texture2D texture2D, bool clockwise = true)
        {
            var original = texture2D.GetPixels32();
            var rotated = new Color32[original.Length];
            var textureWidth = texture2D.width;
            var textureHeight = texture2D.height;
            var origLength = original.Length;

            for (var heightIndex = 0; heightIndex < textureHeight; ++heightIndex)
            {
                for (var widthIndex = 0; widthIndex < textureWidth; ++widthIndex)
                {
                    var rotIndex = (widthIndex + 1) * textureHeight - heightIndex - 1;

                    var origIndex = clockwise
                        ? origLength - 1 - (heightIndex * textureWidth + widthIndex)
                        : heightIndex * textureWidth + widthIndex;

                    rotated[rotIndex] = original[origIndex];
                }
            }

            var rotatedTexture = new Texture2D(textureHeight, textureWidth);
            rotatedTexture.SetPixels32(rotated);
            rotatedTexture.Apply();
            return rotatedTexture;
        }


        #endregion
    }
}
