/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ClickOnListener.cs
//  Info     : UI辅助 - 点击事件监听器
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Aya.UI
{
    public class ClickOnListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region Action

        public Action ActClick = delegate { };
        public Action ActPress = delegate { };
        public Action ActRelease = delegate { };

        #endregion

        #region Click Event

        /// <summary>
        /// 当鼠标点击时
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData) { ActClick(); }

        /// <summary>
        /// 当鼠标按下时
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData) { ActPress(); }

        /// <summary>
        /// 当鼠标抬起时
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData) { ActRelease(); }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            ActClick = null;
            ActPress = null;
            ActRelease = null;
        }

        #endregion

        #region Static

        /// <summary>
        /// 向指定对象发送点击事件
        /// </summary>
        /// <param name="target">目标</param>
        public static void SendOnClick(GameObject target)
        {
            PointerEventData eventData = new PointerEventData(null);
            target.SendMessage("OnPointerClick", eventData);
        }

        #endregion
    }
}