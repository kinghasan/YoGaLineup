/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RectTransformExtension.cs
//  Info     : RectTransform扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public enum AnchorType
    {
        TopLeft,
        TopCenter,
        TopRight,
        TopStretch,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        MiddleStretch,
        BottomLeft,
        BottomCenter,
        BottomRight,
        BottomStretch,
        StretchLeft,
        StretchCenter,
        StretchRight,
        StretchFill
    }

    public static class RectTransformExtension
    {
        #region Size

        /// <summary>
        /// 获取UI尺寸
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <returns>尺寸</returns>
        public static Vector2 GetSize(this RectTransform rectTransform)
        {
            return rectTransform.rect.size;
        }

        /// <summary>
        /// 设置UI尺寸
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>rectTransform</returns>
        public static RectTransform SetSize(this RectTransform rectTransform, float width, float height)
        {
            rectTransform.SetWidth(width);
            rectTransform.SetHeight(height);
            return rectTransform;
        }

        /// <summary>
        /// 设置UI尺寸
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="size">尺寸</param>
        /// <returns>rectTransform</returns>
        public static RectTransform SetSize(this RectTransform rectTransform, Vector2 size)
        {
            rectTransform.SetWidth(size.x);
            rectTransform.SetHeight(size.y);
            return rectTransform;
        }

        /// <summary>
        /// 设置UI宽度
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="width">宽度</param>
        /// <returns>rectTransform</returns>
        public static RectTransform SetWidth(this RectTransform rectTransform, float width)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            return rectTransform;
        }

        /// <summary>
        /// 设置UI高度
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="height">高度</param>
        /// <returns>rectTransform</returns>
        public static RectTransform SetHeight(this RectTransform rectTransform, float height)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            return rectTransform;
        }

        #endregion

        #region Corner

        /// <summary>
        /// 获取顶点坐标
        /// 1  2
        /// 0  3
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="isWorldSpace">是否为世界坐标系</param>
        /// <returns>结果</returns>
        public static Vector3[] GetCorners(this RectTransform rectTransform, bool isWorldSpace = true)
        {
            var corners = new Vector3[4];
            if (isWorldSpace)
            {
                rectTransform.GetWorldCorners(corners);
            }
            else
            {
                rectTransform.GetLocalCorners(corners);
            }

            return corners;
        }

        /// <summary>
        /// 获取左下角坐标
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="isWorldSpace">是否为世界坐标系</param>
        /// <returns>结果</returns>
        public static Vector3 GetLeftBottomCorner(this RectTransform rectTransform, bool isWorldSpace = true)
        {
            var corners = rectTransform.GetCorners(isWorldSpace);
            return corners[0];
        }

        /// <summary>
        /// 获取左上角坐标
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="isWorldSpace">是否为世界坐标系</param>
        /// <returns>结果</returns>
        public static Vector3 GetLeftTopCorner(this RectTransform rectTransform, bool isWorldSpace = true)
        {
            var corners = rectTransform.GetCorners(isWorldSpace);
            return corners[1];
        }

        /// <summary>
        /// 获取右上角坐标
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="isWorldSpace">是否为世界坐标系</param>
        /// <returns>结果</returns>
        public static Vector3 GetRightTopCorner(this RectTransform rectTransform, bool isWorldSpace = true)
        {
            var corners = rectTransform.GetCorners(isWorldSpace);
            return corners[2];
        }

        /// <summary>
        /// 获取右下角坐标
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="isWorldSpace">是否为世界坐标系</param>
        /// <returns>结果</returns>
        public static Vector3 GetRightBottomCorner(this RectTransform rectTransform, bool isWorldSpace = true)
        {
            var corners = rectTransform.GetCorners(isWorldSpace);
            return corners[3];
        }

        #endregion

        #region Anchor
        
        /// <summary>
        /// 设置锚点
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="anchorType">锚点类型</param>
        /// <returns>UI</returns>
        public static RectTransform SetAnchor(this RectTransform rectTransform, AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.TopLeft:
                    rectTransform.SetAnchor(0f, 1f);
                    break;
                case AnchorType.TopCenter:
                    rectTransform.SetAnchor(0.5f, 1f);
                    break;
                case AnchorType.TopRight:
                    rectTransform.SetAnchor(1f, 1f);
                    break;
                case AnchorType.TopStretch:
                    rectTransform.SetAnchor(0f, 1f, 1f, 1f);
                    break;
                case AnchorType.MiddleLeft:
                    rectTransform.SetAnchor(0f, 0.5f);
                    break;
                case AnchorType.MiddleCenter:
                    rectTransform.SetAnchor(0.5f, 0.5f);
                    break;
                case AnchorType.MiddleRight:
                    rectTransform.SetAnchor(1f, 0.5f);
                    break;
                case AnchorType.MiddleStretch:
                    rectTransform.SetAnchor(0f, 0.5f, 1f, 0.5f);
                    break;
                case AnchorType.BottomLeft:
                    rectTransform.SetAnchor(0f, 0f);
                    break;
                case AnchorType.BottomCenter:
                    rectTransform.SetAnchor(0.5f, 0f);
                    break;
                case AnchorType.BottomRight:
                    rectTransform.SetAnchor(1f, 0f);
                    break;
                case AnchorType.BottomStretch:
                    rectTransform.SetAnchor(0f, 0f, 1f, 0f);
                    break;
                case AnchorType.StretchLeft:
                    rectTransform.SetAnchor(0f, 0f, 0f, 1f);
                    break;
                case AnchorType.StretchCenter:
                    rectTransform.SetAnchor(0.5f, 0f, 0.5f, 1f);
                    break;
                case AnchorType.StretchRight:
                    rectTransform.SetAnchor(1f, 0f, 1f, 1f);
                    break;
                case AnchorType.StretchFill:
                    rectTransform.SetAnchor(0f, 0f, 1f, 1f);
                    break;
            }

            return rectTransform;
        }

        /// <summary>
        /// 设置锚点
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="minX">min X</param>
        /// <param name="minY">min Y</param>
        /// <param name="maxX">max X</param>
        /// <param name="maxY">max Y</param>
        /// <returns>UI</returns>
        public static RectTransform SetAnchor(this RectTransform rectTransform, float minX, float minY, float maxX, float maxY)
        {
            rectTransform.anchorMin = new Vector2(minX, minY);
            rectTransform.anchorMax = new Vector2(maxX, maxY);
            return rectTransform;
        }

        /// <summary>
        /// 设置锚点
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>UI</returns>
        public static RectTransform SetAnchor(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.SetAnchor(x, y, x, y);
            return rectTransform;
        }

        /// <summary>
        /// 设置锚点 X
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="anchorPosX">X</param>
        /// <returns>结果</returns>
        public static RectTransform SetAnchorX(this RectTransform rectTransform, float anchorPosX)
        {
            var anchorPos = rectTransform.anchoredPosition;
            anchorPos.x = anchorPosX;
            rectTransform.anchoredPosition = anchorPos;
            return rectTransform;
        }

        /// <summary>
        /// 设置锚点 Y
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="anchorPosY">Y</param>
        /// <returns>结果</returns>
        public static RectTransform SetAnchorY(this RectTransform rectTransform, float anchorPosY)
        {
            var anchorPos = rectTransform.anchoredPosition;
            anchorPos.y = anchorPosY;
            rectTransform.anchoredPosition = anchorPos;
            return rectTransform;
        }

        #endregion

        #region Stretch

        /// <summary>
        /// 设置为锚点模式为 <see cref="AnchorType.StretchFill"/> 并且所有参数为0
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="padding">边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetStretchFill(this RectTransform rectTransform, float padding = 0)
        {
            rectTransform.SetAnchor(AnchorType.StretchFill);
            rectTransform.SetStretch(padding, padding, padding, padding);
            return rectTransform;
        }

        /// <summary>
        /// 设置拉伸边距
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="paddingTop">上边距</param>
        /// <param name="paddingBottom">下边距</param>
        /// <param name="paddingLeft">左边距</param>
        /// <param name="paddingRight">右边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetStretch(this RectTransform rectTransform, float paddingTop, float paddingBottom, float paddingLeft, float paddingRight)
        {
            rectTransform.offsetMin = new Vector2(paddingLeft, paddingBottom);
            rectTransform.offsetMax = -new Vector2(paddingRight, paddingTop);
            return rectTransform;
        }

        /// <summary>
        /// 设置水平拉伸
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="paddingLeft">左边距</param>
        /// <param name="paddingRight">右边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetHorizontalStretch(this RectTransform rectTransform, float paddingLeft, float paddingRight)
        {
            rectTransform.offsetMin = new Vector2(paddingLeft, rectTransform.offsetMin.y);
            rectTransform.offsetMax = new Vector2(paddingRight, rectTransform.offsetMax.y);
            return rectTransform;
        }

        /// <summary>
        /// 设置垂直拉伸
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="paddingTop">上边距</param>
        /// <param name="paddingBottom">下边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetVerticalStretch(this RectTransform rectTransform, float paddingTop, float paddingBottom)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, paddingBottom);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, paddingTop);
            return rectTransform;
        }

        /// <summary>
        /// 设置水平拉伸模式下的左边距
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="paddingLeft">左边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetLeft(this RectTransform rectTransform, float paddingLeft)
        {
            rectTransform.offsetMin = new Vector2(paddingLeft, rectTransform.offsetMin.y);
            return rectTransform;
        }

        /// <summary>
        /// 设置水平拉伸模式下的右边距
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="paddingRight">右边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetRight(this RectTransform rectTransform, float paddingRight)
        {
            rectTransform.offsetMax = new Vector2(paddingRight, rectTransform.offsetMax.y);
            return rectTransform;
        }

        /// <summary>
        /// 设置垂直拉伸模式下的上边距
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="paddingTop">上边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetTop(this RectTransform rectTransform, float paddingTop)
        {
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, paddingTop);
            return rectTransform;
        }

        /// <summary>
        /// 设置垂直拉伸模式下的下边距
        /// </summary>
        /// <param name="rectTransform">UI</param>
        /// <param name="paddingBottom">下边距</param>
        /// <returns>UI</returns>
        public static RectTransform SetBottom(this RectTransform rectTransform, float paddingBottom)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, paddingBottom);
            return rectTransform;
        }

        #endregion
    }
}