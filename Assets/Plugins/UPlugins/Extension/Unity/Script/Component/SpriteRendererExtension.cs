/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SpriteRenderrtExtension.cs
//  Info     : 精灵渲染器扩展
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class SpriteRendererExtension
    {
        /// <summary>
        /// 设置颜色A
        /// </summary>
        /// <param name="spriteRenderer">精灵渲染器</param>
        /// <param name="alpha">A</param>
        /// <returns>spriteRenderer</returns>
        public static SpriteRenderer SetColorA(this SpriteRenderer spriteRenderer, float alpha)
        {
            var color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            spriteRenderer.color = color;
            return spriteRenderer;
        }

        /// <summary>
        /// 设置颜色R
        /// </summary>
        /// <param name="spriteRenderer">精灵渲染器</param>
        /// <param name="red">R</param>
        /// <returns>spriteRenderer</returns>
        public static SpriteRenderer SetColorR(this SpriteRenderer spriteRenderer, float red)
        {
            var color = new Color(red, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a);
            spriteRenderer.color = color;
            return spriteRenderer;
        }

        /// <summary>
        /// 设置颜色G
        /// </summary>
        /// <param name="spriteRenderer">精灵渲染器</param>
        /// <param name="green">G</param>
        /// <returns>spriteRenderer</returns>
        public static SpriteRenderer SetColorG(this SpriteRenderer spriteRenderer, float green)
        {
            var color = new Color(spriteRenderer.color.r, green, spriteRenderer.color.b, spriteRenderer.color.a);
            spriteRenderer.color = color;
            return spriteRenderer;
        }

        /// <summary>
        /// 设置颜色B
        /// </summary>
        /// <param name="spriteRenderer">精灵渲染器</param>
        /// <param name="blue">B</param>
        /// <returns>spriteRenderer</returns>
        public static SpriteRenderer SetColorB(this SpriteRenderer spriteRenderer, float blue)
        {
            var color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, blue, spriteRenderer.color.a);
            spriteRenderer.color = color;
            return spriteRenderer;
        }
    }
}