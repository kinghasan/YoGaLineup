/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RectExtension.cs
//  Info     : Rect 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class RectExtension
    {
        #region Width / Height / Size

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="width">宽度</param>
        /// <returns>结果</returns>
        public static Rect SetWidth(this Rect rect, float width)
        {
            rect.width = width;
            return rect;
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="height">高度</param>
        /// <returns>结果</returns>
        public static Rect SetHeight(this Rect rect, float height)
        {
            rect.height = height;
            return rect;
        }

        /// <summary>
        /// 设置尺寸
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>结果</returns>
        public static Rect SetSize(this Rect rect, float width, float height)
        {
            rect.width = width;
            rect.height = height;
            return rect;
        }

        /// <summary>
        /// 设置尺寸
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="size">尺寸</param>
        /// <returns>结果</returns>
        public static Rect SetSize(this Rect rect, Vector2 size)
        {
            rect.size = size;
            return rect;
        }

        #endregion

        #region Center

        /// <summary>
        /// 设置中心点X
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="x">X</param>
        /// <returns>结果</returns>
        public static Rect SetCenterX(this Rect rect, float x)
        {
            rect.center = new Vector2(x, rect.center.y);
            return rect;
        }

        /// <summary>
        /// 设置中心点Y
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="y">Y</param>
        /// <returns>结果</returns>
        public static Rect SetCenterY(this Rect rect, float y)
        {
            rect.center = new Vector2(rect.center.x, y);
            return rect;
        }

        /// <summary>
        /// 设置中心点
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>结果</returns>
        public static Rect SetCenter(this Rect rect, float x, float y)
        {
            rect.center = new Vector2(x, y);
            return rect;
        }

        /// <summary>
        /// 设置中心点
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="center">中心点</param>
        /// <returns>结果</returns>
        public static Rect SetCenter(this Rect rect, Vector2 center)
        {
            rect.center = center;
            return rect;
        }

        #endregion

        #region X / Y / Position

        /// <summary>
        /// 设置X
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="x">X</param>
        /// <returns>结果</returns>
        public static Rect SetX(this Rect rect, float x)
        {
            rect.x = x;
            return rect;
        } 

        /// <summary>
        /// 设置Y
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="y">Y</param>
        /// <returns>结果</returns>
        public static Rect SetY(this Rect rect, float y)
        {
            rect.y = y;
            return rect;
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="position">位置</param>
        /// <returns>结果</returns>
        public static Rect SetPosition(this Rect rect, Vector2 position)
        {
            rect.position = position;
            return rect;
        }

        #endregion

        #region Expand / Reduce

        /// <summary>
        /// 扩大
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="expandSize">扩大尺寸</param>
        /// <returns>结果</returns>
        public static Rect Expand(this Rect rect, float expandSize)
        {
            var ret = new Rect(rect.x - expandSize, rect.y - expandSize, rect.width + expandSize * 2, rect.height + expandSize * 2);
            return ret;
        }

        /// <summary>
        /// 扩大
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="expandRect">扩大尺寸</param>
        /// <returns>结果</returns>
        public static Rect Expand(this Rect rect, Rect expandRect)
        {
            var ret = new Rect(rect.x - expandRect.x, rect.y - expandRect.y, rect.width + expandRect.width, rect.height + expandRect.height);
            return ret;
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="reduceSize">缩小尺寸</param>
        /// <returns>结果</returns>
        public static Rect Reduce(this Rect rect, float reduceSize)
        {
            var ret = new Rect(rect.x + reduceSize, rect.y + reduceSize, rect.width - reduceSize * 2, rect.height - reduceSize * 2);
            return ret;
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="reduceRect">缩小尺寸</param>
        /// <returns>结果</returns>
        public static Rect Reduce(this Rect rect, Rect reduceRect)
        {
            var ret = new Rect(rect.x + reduceRect.x, rect.y + reduceRect.y, rect.width - reduceRect.width, rect.height - reduceRect.height);
            return ret;
        }

        #endregion

        #region Split

        /// <summary>
        /// 横向切分矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="index">索引</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static Rect SplitHorizontal(this Rect rect, int index, int count)
        {
            var num = rect.width / (float)count;
            rect.width = num;
            rect.x += num * (float)index;
            return rect;
        }

        /// <summary>
        /// 纵向切分矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="index">索引</param>
        /// <param name="count">数量</param>
        /// <returns>结果</returns>
        public static Rect SplitVertical(this Rect rect, int index, int count)
        {
            var num = rect.height / (float)count;
            rect.height = num;
            rect.y += num * (float)index;
            return rect;
        }

        /// <summary>
        /// 矩阵切分矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static Rect SplitGrid(this Rect rect, float width, float height, int index)
        {
            var num1 = (int)((double)rect.width / (double)width);
            var num2 = num1 > 0 ? num1 : 1;
            var num3 = index % num2;
            var num4 = index / num2;
            rect.x += (float)num3 * width;
            rect.y += (float)num4 * height;
            rect.width = width;
            rect.height = height;
            return rect;
        }

        /// <summary>
        /// 表格切分矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="columnCount">列数</param>
        /// <param name="rowHeight">行数</param>
        /// <param name="index">索引</param>
        /// <returns>结果</returns>
        public static Rect SplitTableGrid(this Rect rect, int columnCount, float rowHeight, int index)
        {
            var num1 = index % columnCount;
            var num2 = index / columnCount;
            var num3 = rect.width / (float)columnCount;
            rect.x += (float)num1 * num3;
            rect.y += (float)num2 * rowHeight;
            rect.width = num3;
            rect.height = rowHeight;
            return rect;
        }

        #endregion
    }
}
