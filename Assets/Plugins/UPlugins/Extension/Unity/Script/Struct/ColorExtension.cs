/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ColorExtension.cs
//  Info     : Color 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Extension
{
    public static class ColorExtension
    {
        #region RGB

        /// <summary>
        /// 设置颜色 R
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="a">红色分量</param>
        /// <returns>结果</returns>
        public static Color SetR(this Color color, float a)
        {
            a = Mathf.Clamp01(a);
            color.a = a;
            return color;
        }

        /// <summary>
        /// 设置颜色 G
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="g">绿色分量</param>
        /// <returns>结果</returns>
        public static Color SetG(this Color color, float g)
        {
            g = Mathf.Clamp01(g);
            color = new Color(color.r, g, color.b, color.a);
            return color;
        }

        /// <summary>
        /// 设置颜色 B
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="b">蓝色分量</param>
        /// <returns>结果</returns>
        public static Color SetB(this Color color, float b)
        {
            b = Mathf.Clamp01(b);
            color = new Color(color.r, color.g, b, color.a);
            return color;
        }

        /// <summary>
        /// 设置颜色 A
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="a">Alpha分量</param>
        /// <returns>结果</returns>
        public static Color SetA(this Color color, float a)
        {
            a = Mathf.Clamp01(a);
            color.a = a;
            return color;
        }

        /// <summary>
        /// 获取RGB值
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static int GetRgbValue(this Color color)
        {
            var r = (int) (color.r * 255);
            var g = (int) (color.g * 255);
            var b = (int) (color.b * 255);
            return r * 255 * 255 + g * 255 + b;
        }

        #endregion

        #region HSV

        /// <summary>
        /// 获取颜色 H
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static float GetH(this Color color)
        {
            Color.RGBToHSV(color, out var h, out _, out _);
            return h;
        }

        /// <summary>
        /// 获取颜色 S
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static float GetS(this Color color)
        {
            Color.RGBToHSV(color, out _, out var s, out _);
            return s;
        }

        /// <summary>
        /// 获取颜色 V
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static float GetV(this Color color)
        {
            Color.RGBToHSV(color, out _, out _, out var v);
            return v;
        }

        /// <summary>
        /// 设置颜色 H
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="h">H [0,1]</param>
        /// <returns>结果</returns>
        public static Color SetH(this Color color, float h)
        {
            Color.RGBToHSV(color, out _, out var ts, out var tv);
            var tc = Color.HSVToRGB(h, ts, tv);
            color.r = tc.r;
            color.g = tc.g;
            color.b = tc.b;
            return color;
        }

        /// <summary>
        /// 设置颜色 S
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="s">S [0,1]</param>
        /// <returns>结果</returns>
        public static Color SetS(this Color color, float s)
        {
            Color.RGBToHSV(color, out var th, out _, out var tv);
            var tc = Color.HSVToRGB(th, s, tv);
            color.r = tc.r;
            color.g = tc.g;
            color.b = tc.b;
            return color;
        }

        /// <summary>
        /// 设置颜色 V
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="v">V [0,1]</param>
        /// <returns>结果</returns>
        public static Color SetV(this Color color, float v)
        {
            Color.RGBToHSV(color, out var th, out var ts, out _);
            var tc = Color.HSVToRGB(th, ts, v);
            color.r = tc.r;
            color.g = tc.g;
            color.b = tc.b;
            return color;
        }

        /// <summary>
        /// 设置颜色 HSV
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="h">H [0,1]</param>
        /// <param name="s">S [0,1]</param>
        /// <param name="v">V [0,1]</param>
        /// <returns>结果</returns>
        public static Color SetHsv(this Color color, float h, float s, float v)
        {
            var tc = Color.HSVToRGB(h, s, v);
            color.r = tc.r;
            color.g = tc.g;
            color.b = tc.b;
            return color;
        }

        #endregion

        #region Color SDR

        /// <summary>
        /// 计算互补色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="inverseAlpha">计算Alpha反色(注意，非透明色会变透明)</param>
        /// <returns>结果</returns>
        public static Color ComplementaryColor(this Color color, bool inverseAlpha = false)
        {
            var result = new Color(
                1f - color.r,
                1f - color.g,
                1f - color.b,
                inverseAlpha ? 1f - color.a : color.a);
            return result;
        }

        /// <summary>
        /// 计算灰度
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static float GrayLevel(this Color color)
        {
            var result = (color.r + color.g + color.b) / 3f;
            return result;
        }

        /// <summary>
        /// 获取感知亮度 (LDR/忽略Alpha)
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static float GetPerceivedBrightness(this Color color)
        {
            var result = Mathf.Sqrt(
                0.241f * color.r * color.r +
                0.691f * color.g * color.g +
                0.068f * color.b * color.b);
            return result;
        }

        #endregion

        #region AGBA32

        /// <summary>
        /// 将 LDR 颜色转换为 ARGB32 格式 uint
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static uint ToArgb32(this Color color)
        {
            var result = ((uint)(color.a * 255) << 24)
                         | ((uint)(color.r * 255) << 16)
                         | ((uint)(color.g * 255) << 8)
                         | ((uint)(color.b * 255));
            return result;
        }


        /// <summary>
        ///  将 ARGB32 颜色值转换为 Color
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="argb32">ARGB32</param>
        /// <returns>结果</returns>
        public static Color FromArgb32(this Color color, uint argb32)
        {
            var result = new Color(
                ((argb32 >> 16) & 0xFF) / 255f,
                ((argb32 >> 8) & 0xFF) / 255f,
                ((argb32) & 0xFF) / 255f,
                ((argb32 >> 24) & 0xFF) / 255f);
            return result;
        }

        #endregion

        #region HTML

        /// <summary>
        /// 获取HTML颜色RGB值
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static string GetHtmlRgb(this Color color)
        {
            var r = (int)(color.r * 255);
            var g = (int)(color.g * 255);
            var b = (int)(color.b * 255);
            var result = "#" + r.ToString("x2") + g.ToString("x2") + b.ToString("x2");
            return result;
        }

        /// <summary>
        /// 获取HTML颜色RGBA值
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static string GetHtmlRgba(this Color color)
        {
            var r = (int)(color.r * 255);
            var g = (int)(color.g * 255);
            var b = (int)(color.b * 255);
            var a = (int)(color.a * 255);
            var result = "#" + r.ToString("x2") + g.ToString("x2") + b.ToString("x2") + a.ToString("x2");
            return result;
        }

        /// <summary>
        /// 设置HTML颜色RGB值
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="html">HTML颜色值</param>
        /// <returns>结果</returns>
        public static Color SetHtmlRgb(this Color color, string html)
        {
            html = html.Replace("#", "");
            var r = Convert.ToInt32(html.Substring(0, 2), 16);
            var g = Convert.ToInt32(html.Substring(2, 2), 16);
            var b = Convert.ToInt32(html.Substring(4, 2), 16);
            color.r = r;
            color.g = g;
            color.b = b;
            return color;
        }

        /// <summary>
        /// 设置HTML颜色RGBA值
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="html">HTML颜色值</param>
        /// <returns>结果</returns>
        public static Color SetHtmlRgba(this Color color, string html)
        {
            html = html.Replace("#", "");
            var r = Convert.ToInt32(html.Substring(0, 2), 16);
            var g = Convert.ToInt32(html.Substring(2, 2), 16);
            var b = Convert.ToInt32(html.Substring(4, 2), 16);
            var a = Convert.ToInt32(html.Substring(6, 2), 16);
            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;
            return color;
        }

        #endregion

        #region Normalize

        /// <summary>
        /// 归一化
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>结果</returns>
        public static Color Normalize(this Color color)
        {
            var vector = new Vector3(color.r, color.g, color.b).normalized;
            color.r = vector.x;
            color.g = vector.y;
            color.b = vector.z;
            return color;
        }

        #endregion

        #region Clamp

        /// <summary>
        /// 将颜色限定在两个值范围内
        /// </summary>
        /// <param name="value">原色</param>
        /// <param name="min">最小色</param>
        /// <param name="max">最大色</param>
        /// <returns>结果</returns>
        public static Color Clamp(Color value, float min, float max)
        {
            var result = new Color(
                Mathf.Clamp(value.r, min, max),
                Mathf.Clamp(value.g, min, max),
                Mathf.Clamp(value.b, min, max),
                Mathf.Clamp(value.a, min, max)
            );
            return result;
        }

        /// <summary>
        /// 将颜色限定在两个颜色范围内
        /// </summary>
        /// <param name="value">原色</param>
        /// <param name="min">最小色</param>
        /// <param name="max">最大色</param>
        /// <returns>结果</returns>
        public static Color Clamp(Color value, Color min, Color max)
        {
            float t;
            if (min.r > max.r)
            {
                t = min.r;
                min.r = max.r;
                max.r = t;
            }

            if (min.g > max.g)
            {
                t = min.g;
                min.g = max.g;
                max.g = t;
            }

            if (min.b > max.b)
            {
                t = min.b;
                min.b = max.b;
                max.b = t;
            }

            if (min.a > max.a)
            {
                t = min.a;
                min.a = max.a;
                max.a = t;
            }

            var result = new Color(
                Mathf.Clamp(value.r, min.r, max.r),
                Mathf.Clamp(value.g, min.g, max.g),
                Mathf.Clamp(value.b, min.b, max.b),
                Mathf.Clamp(value.a, min.a, max.a)
            );
            return result;
        }

        #endregion
    }
}