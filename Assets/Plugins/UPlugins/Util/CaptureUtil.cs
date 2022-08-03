/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CaptureUtil.cs
//  Info     : 截图辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Warning  : 需要在 EndOfFrame 后执行
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System.IO;
using UnityEngine;

namespace Aya.Util
{
	public static class CaptureUtil
	{
	    /// <summary>
	    /// 获取截图
	    /// </summary>
	    /// <param name="compress">是否压缩</param>
	    /// <param name="highQuality">高质量压缩</param>
	    /// <returns>结果</returns>
	    public static Texture2D GetCapture(bool compress = false, bool highQuality = true)
	    {
	        var width = Screen.width;
	        var height = Screen.height;
	        var tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
	        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
	        if (compress)
	        {
	            tex.Compress(highQuality);
	        }

	        tex.Apply();
	        return tex;
	    }

	    /// <summary>
		/// 获取截图并保存（Application.persistentDataPath）
		/// </summary>
		/// <param name="fileName">文件名（需要包含扩展名，建议 *.png）</param>
		/// <returns>结果</returns>
		public static Texture2D GetCaptureAndSave(string fileName)
		{
			var tex = GetCapture(); 
			var imagebytes = tex.EncodeToPNG();
			File.WriteAllBytes(Application.persistentDataPath + "/" + fileName, imagebytes);
			return tex;
		}
	}
}