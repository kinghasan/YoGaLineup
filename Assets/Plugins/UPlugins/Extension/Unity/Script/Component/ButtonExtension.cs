/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ButtonExtension.cs
//  Info     : Button扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.UI;

namespace Aya.Extension
{
    public static class ButtonExtension
    {
        /// <summary>
        /// 设置正常状态背景颜色
        /// </summary>
        /// <param name="button">按钮</param>
        /// <param name="color">颜色</param>
        /// <returns>button</returns>
        public static Button SetNormalColor(this Button button, Color color)
        {
            var cb = button.colors;
            cb.normalColor = color;
            button.colors = cb;
            return button;
        }

        /// <summary>
        /// 设置高亮状态背景颜色
        /// </summary>
        /// <param name="button">按钮</param>
        /// <param name="color">颜色</param>
        /// <returns>button</returns>
        public static Button SetHighLightedColor(this Button button, Color color)
        {
            var cb = button.colors;
            cb.highlightedColor = color;
            button.colors = cb;
            return button;
        }

        /// <summary>
        /// 设置按下状态背景颜色
        /// </summary>
        /// <param name="button">按钮</param>
        /// <param name="color">颜色</param>
        /// <returns>button</returns>
        public static Button SetPressedColor(this Button button, Color color)
        {
            var cb = button.colors;
            cb.pressedColor = color;
            button.colors = cb;
            return button;
        }

        /// <summary>
        /// 设置禁用状态背景颜色
        /// </summary>
        /// <param name="button">按钮</param>
        /// <param name="color">颜色</param>
        /// <returns>button</returns>
        public static Button SetDisabledColor(this Button button, Color color)
        {
            var cb = button.colors;
            cb.disabledColor = color;
            button.colors = cb;
            return button;
        }
    }
}